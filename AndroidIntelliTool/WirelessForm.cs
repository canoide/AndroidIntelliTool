using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndroidIntelliTool
{
    public partial class WirelessForm : Form
    {
        public Dictionary<string, string> Config { get; private set; }
        private readonly string _adbPath;
        private List<string> _savedIps = new List<string>();

        public WirelessForm(Dictionary<string, string> config)
        {
            InitializeComponent();
            Config = config;
            _adbPath = Config.ContainsKey("adb") ? Config["adb"] : null;

            this.Load += async (s, e) => await WirelessForm_Load();
            btnConnectSaved.Click += async (s, e) => await ConnectToSelectedIp();
            btnRemoveIp.Click += (s, e) => RemoveSelectedIp();
            btnConnectManual.Click += async (s, e) => await ConnectToIp(textIpAddress.Text);
            btnStartWirelessSetup.Click += async (s, e) => await StartGuidedSetup();
        }

        private async Task WirelessForm_Load()
        {
            if (string.IsNullOrEmpty(_adbPath) || !System.IO.File.Exists(_adbPath))
            {
                MessageBox.Show("ADB path is not configured correctly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            LoadSavedIps();
            await RefreshUsbDevices();
        }

        private void LoadSavedIps()
        {
            if (Config.ContainsKey("WirelessIPs"))
            {
                _savedIps = Config["WirelessIPs"].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                listSavedIps.DataSource = _savedIps;
            }
        }

        private async Task RefreshUsbDevices()
        {
            var (output, _) = await RunCommandAsync(_adbPath, "devices");
            var devices = new List<string>();
            var lines = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines.Skip(1))
            {
                if (line.Contains("device") && !line.Contains(":")) // Filter out wireless devices
                {
                    devices.Add(line.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries)[0]);
                }
            }
            comboUsbDevices.DataSource = devices;
        }

        private async Task ConnectToSelectedIp()
        {
            if (listSavedIps.SelectedItem is string ip)
            {
                await ConnectToIp(ip);
            }
            else
            {
                MessageBox.Show("Please select an IP from the list.", "No IP Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async Task ConnectToIp(string ip)
        {
            if (string.IsNullOrWhiteSpace(ip)) return;

            var (output, error) = await RunCommandAsync(_adbPath, $"connect {ip}");
            if (!string.IsNullOrEmpty(error) && !output.Contains("already connected"))
            {
                MessageBox.Show($"Failed to connect to {ip}:\n{error}", "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show($"Successfully connected to {ip}.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (!_savedIps.Contains(ip))
                {
                    _savedIps.Add(ip);
                    listSavedIps.DataSource = null;
                    listSavedIps.DataSource = _savedIps;
                    Config["WirelessIPs"] = string.Join(",", _savedIps);
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void RemoveSelectedIp()
        {
            if (listSavedIps.SelectedItem is string ip)
            {
                _savedIps.Remove(ip);
                listSavedIps.DataSource = null;
                listSavedIps.DataSource = _savedIps;
                Config["WirelessIPs"] = string.Join(",", _savedIps);
                MessageBox.Show($"{ip} has been removed.", "IP Removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async Task StartGuidedSetup()
        {
            if (!(comboUsbDevices.SelectedItem is string usbDevice))
            {
                MessageBox.Show("Please select a USB device first.", "No Device", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lblSetupInstructions.Text = "Step 1: Trying to find device IP address...";
            var (ipOutput, _) = await RunCommandAsync(_adbPath, $"-s {usbDevice} shell ip addr show wlan0");
            var ipMatch = Regex.Match(ipOutput, @"inet (\d{{1,3}}\.\d{{1,3}}\.\d{{1,3}}\.\d{{1,3}}")
;

            if (!ipMatch.Success)
            {
                lblSetupInstructions.Text = "Could not automatically find the device IP. Please connect manually.";
                return;
            }
            string foundIp = ipMatch.Groups[1].Value;

            lblSetupInstructions.Text = $"Step 2: Found IP: {foundIp}. Enabling wireless mode...";
            await RunCommandAsync(_adbPath, $"-s {usbDevice} tcpip 5555");

            lblSetupInstructions.Text = "Step 3: Wireless mode enabled! You can now DISCONNECT the USB cable from your device.\n\nClick the button below to connect.";

            btnStartWirelessSetup.Text = $"Connect to {foundIp}";
            btnStartWirelessSetup.Click -= async (s, e) => await StartGuidedSetup(); // Remove old handler
            btnStartWirelessSetup.Click += async (s, e) => await ConnectToIp(foundIp); // Add new handler
        }

        private Task<(string output, string error)> RunCommandAsync(string fileName, string arguments)
        {
            return Task.Run(() =>
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = fileName,
                        Arguments = arguments,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };
                var output = new System.Text.StringBuilder();
                var error = new System.Text.StringBuilder();

                using (var outputWaitHandle = new System.Threading.AutoResetEvent(false))
                using (var errorWaitHandle = new System.Threading.AutoResetEvent(false))
                {
                    process.OutputDataReceived += (sender, e) => { if (e.Data != null) output.AppendLine(e.Data); else outputWaitHandle.Set(); };
                    process.ErrorDataReceived += (sender, e) => { if (e.Data != null) error.AppendLine(e.Data); else errorWaitHandle.Set(); };

                    process.Start();

                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    process.WaitForExit();
                    outputWaitHandle.WaitOne();
                    errorWaitHandle.WaitOne();

                    return (output.ToString(), error.ToString());
                }
            });
        }
    }
}
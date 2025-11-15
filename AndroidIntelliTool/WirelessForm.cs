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
            btnStartWirelessSetup.Click += OnStartWirelessSetupClick;
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
                    devices.Add(line.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries)[0].Trim());
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

            // Normalize output (trim whitespace and convert to lowercase for checking)
            string outputLower = output?.Trim().ToLower() ?? "";
            string errorLower = error?.Trim().ToLower() ?? "";

            // Debug: Show raw output
            string debugInfo = $"Raw Output: '{output}'\nRaw Error: '{error}'\n\nTrimmed Output: '{outputLower}'\nTrimmed Error: '{errorLower}'";

            // Check if connection was successful - be VERY strict
            // Only consider it success if we explicitly see these messages
            bool isConnected = outputLower.Contains("connected to") || outputLower.Contains("already connected");

            // Check for various failure patterns
            bool isFailed = outputLower.Contains("failed") ||
                           outputLower.Contains("cannot connect") ||
                           outputLower.Contains("connection refused") ||
                           outputLower.Contains("no route to host") ||
                           outputLower.Contains("unable to connect") ||
                           errorLower.Contains("failed") ||
                           errorLower.Contains("error");

            // If EXPLICITLY connected, show success
            if (isConnected)
            {
                // TEMPORARY: Always show debug to diagnose the issue
                MessageBox.Show($"Successfully connected to {ip}.\n\n[Debug - PLEASE REPORT THIS]\n{debugInfo}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            // If explicitly failed, show error
            else if (isFailed)
            {
                string errorMsg = !string.IsNullOrEmpty(output) ? output : error;
                MessageBox.Show($"Failed to connect to {ip}:\n{errorMsg}\n\n[Debug]\n{debugInfo}", "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Otherwise, we don't know what happened - show debug
            else
            {
                MessageBox.Show($"Unable to determine connection status for {ip}\n\nThis usually means the connection failed.\n\n[Debug Info]\n{debugInfo}", "Connection Status Unknown", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private enum WirelessSetupState { Idle, ReadyToConnect }
        private WirelessSetupState _setupState = WirelessSetupState.Idle;
        private string _foundIpForConnection;

        private async void OnStartWirelessSetupClick(object sender, EventArgs e)
        {
            if (_setupState == WirelessSetupState.Idle)
            {
                await StartGuidedSetup();
            }
            else if (_setupState == WirelessSetupState.ReadyToConnect)
            {
                await ConnectToIp(_foundIpForConnection);
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
            var ipMatch = Regex.Match(ipOutput, @"inet (\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})");

            if (!ipMatch.Success)
            {
                lblSetupInstructions.Text += "\nCould not automatically find the device IP. Please connect manually.";
                return;
            }
            string foundIp = ipMatch.Groups[1].Value;
            _foundIpForConnection = foundIp;

            lblSetupInstructions.Text += $"\nStep 2: Found IP: {foundIp}. Enabling wireless mode...";
            await RunCommandAsync(_adbPath, $"-s {usbDevice} tcpip 5555");

            lblSetupInstructions.Text += "\nStep 3: Wireless mode enabled! You can now DISCONNECT the USB cable from your device.\n\nClick the button below to connect.";

            btnStartWirelessSetup.Text = $"Connect to {foundIp}";
            _setupState = WirelessSetupState.ReadyToConnect;
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
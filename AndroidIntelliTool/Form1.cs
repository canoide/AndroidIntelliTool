using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndroidIntelliTool
{
    public partial class Form1 : Form
    {
        private Dictionary<string, string> _config = new Dictionary<string, string>();
        private const string ConfigFileName = "AndroidIntelliTool.cfg";


        public Form1(string[] args)
        {
            InitializeComponent();
            this.Icon = new Icon("Icon.ico");
            this.Load += async (s, e) => await Form1_Load(s, e, args);
        }

        private async Task Form1_Load(object sender, EventArgs e, string[] args)
        {
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);

            // Main Tab
            refreshDevicesButton.Click += async (s, ev) => await RefreshDevices();
            wirelessConnectButton.Click += async (s, ev) => await WirelessConnect();
            disconnectAllButton.Click += async (s, ev) => await DisconnectAll();
            selectApkButton.Click += async (s, ev) => await SelectApkFile();
            installButton.Click += async (s, ev) => await InstallApk();
            restartAppButton.Click += async (s, ev) => await RunAppCommand("Restarting", "shell am force-stop {{pkg}}", "shell monkey -p {{pkg}} -c android.intent.category.LAUNCHER 1");
            uninstallAppButton.Click += async (s, ev) => await RunAppCommand("Uninstalling", "uninstall {{pkg}}");
            clearDataButton.Click += async (s, ev) => await RunAppCommand("Clearing data for", "shell pm clear {{pkg}}");
            screenshotButton.Click += async (s, ev) => await TakeScreenshot();
            logcatButton.Click += (s, ev) => ShowLogcat();
            deviceComboBox.SelectedIndexChanged += (s, ev) => UpdateConnectionStatus();
            screenMirrorButton.Click += (s, ev) => RunScrcpy(false);
            screenRecordButton.Click += (s, ev) => RunScrcpy(true);
            forceStopAppButton.Click += async (s, ev) => await RunAppCommand("Force Stopping", "shell am force-stop {{pkg}}");
            fileExplorerButton.Click += (s, ev) => OpenFileExplorer(); // New button handler

            // Menu
            exitToolStripMenuItem.Click += (s, ev) => this.Close();
            settingsToolStripMenuItem.Click += (s, ev) => OpenSettings();

            // Main Tab Control
            // mainTabControl.SelectedIndexChanged += new System.EventHandler(this.mainTabControl_SelectedIndexChanged); // Removed as File Explorer is now a separate form

            if (!LoadConfigAndCheckPaths())
            {
                OpenSettings(true); // Force settings if config is bad
            }

            await ProcessInitialApk(args);
            await RefreshDevices();
        }

                #region File Explorer (Moved to FileExplorerForm)
        #endregion
        #region Config and Startup

        private bool LoadConfigAndCheckPaths()
        {
            LoadConfig();
            if (!_config.ContainsKey("adb") || !File.Exists(_config["adb"])) return false;
            if (!_config.ContainsKey("aapt2") || !File.Exists(_config["aapt2"])) return false;
            return true;
        }

        private void LoadConfig()
        {
            if (!File.Exists(ConfigFileName)) return;
            _config = File.ReadAllLines(ConfigFileName)
                        .Where(line => !string.IsNullOrWhiteSpace(line) && line.Contains('='))
                        .Select(line => line.Split(new[] { '=' }, 2))
                        .ToDictionary(parts => parts[0].Trim(), parts => parts[1].Trim(), StringComparer.OrdinalIgnoreCase);
        }

        private void SaveConfig()
        {
            var lines = _config.Select(kvp => $"{kvp.Key}={kvp.Value}");
            File.WriteAllLines(ConfigFileName, lines);
        }

        private void OpenSettings(bool isFirstTime = false)
        {
            if (isFirstTime)
            {
                MessageBox.Show("Welcome! Please configure the paths to ADB and AAPT2 to continue.", "Initial Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            using (var settingsForm = new SettingsForm(_config))
            {
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    _config = settingsForm.Config;
                    SaveConfig();
                    MessageBox.Show("Settings saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (!LoadConfigAndCheckPaths())
                    {
                        MessageBox.Show("Configuration is still incomplete. The application may not function correctly.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else if (isFirstTime && !LoadConfigAndCheckPaths())
                {
                    MessageBox.Show("Configuration is required to run the application. Exiting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }
        }

        private async Task ProcessInitialApk(string[] args)
        {
            string apkPath = null;
            if (args.Length > 0 && File.Exists(args[0]))
            {
                apkPath = args[0];
            }
            else if (_config.ContainsKey("LastApkPath") && File.Exists(_config["LastApkPath"]))
            {
                apkPath = _config["LastApkPath"];
            }

            if (apkPath != null)
            {
                await ProcessApkFile(apkPath);
            }
        }

        #endregion

        #region Main Tab UI Handlers

        private async Task SelectApkFile()
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Android Packages (*.apk;*.aab)|*.apk;*.aab|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    await ProcessApkFile(openFileDialog.FileName);
                }
            }
        }

        private async Task InstallApk()
        {
            string device = deviceComboBox.SelectedItem as string;
            if (string.IsNullOrEmpty(device)) { MessageBox.Show("Please select a device."); return; }

            string filePath = apkPathTextBox.Text;
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath)) { MessageBox.Show("Please select a valid file."); return; }

            outputTextBox.Text = $"Installing {Path.GetFileName(filePath)} on {device}...\n";
            string arguments = $"-s {device} install -r -d \"{filePath}\"";
            var (output, exitCode) = await RunCommandAsync(_config["adb"], arguments);
            if (exitCode != 0)
            {
                outputTextBox.AppendText($"\nError installing APK: {output}");
            }
            else
            {
                outputTextBox.AppendText("Success!");
            }
        }

        private async Task TakeScreenshot()
        {
            string device = deviceComboBox.SelectedItem as string;
            if (string.IsNullOrEmpty(device)) { MessageBox.Show("Please select a device."); return; }

            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var fileName = $"screenshot_{timestamp}.png";
            var devicePath = $"/sdcard/{fileName}";
            var desktopPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

            outputTextBox.Text = "Taking screenshot...\n";
            var (output1, exitCode1) = await RunCommandAsync(_config["adb"], $"-s {device} shell screencap \"{devicePath}\"");
            if (exitCode1 != 0) { outputTextBox.AppendText($"\nError taking screenshot: {output1}"); return; }

            var (output2, exitCode2) = await RunCommandAsync(_config["adb"], $"-s {device} pull \"{devicePath}\" \"{desktopPath}\"");
            if (exitCode2 != 0) { outputTextBox.AppendText($"\nError pulling screenshot: {output2}"); return; }

            var (output3, exitCode3) = await RunCommandAsync(_config["adb"], $"-s {device} shell rm \"{devicePath}\"");
            if (exitCode3 != 0) { outputTextBox.AppendText($"\nError removing temp screenshot: {output3}"); return; }

            outputTextBox.AppendText($"\nScreenshot saved to: {desktopPath}");
        }

        private void ShowLogcat()
        {
            string device = deviceComboBox.SelectedItem as string;
            if (string.IsNullOrEmpty(device)) { MessageBox.Show("Please select a device."); return; }
            new LogcatForm(device, _config["adb"]).Show();
        }

        private void UpdateConnectionStatus()
        {
            string device = deviceComboBox.SelectedItem as string;
            if (!string.IsNullOrEmpty(device))
            {
                connectionStatusLabel.Text = $"Status: Connected to {device}";
                connectionStatusLabel.ForeColor = Color.Green;
                _config["LastDevice"] = device;
                SaveConfig();
            }
            else
            {
                deviceComboBox.Text = "";
                connectionStatusLabel.Text = "Status: Not Connected";
                connectionStatusLabel.ForeColor = Color.Red;
            }
        }

        #endregion

        #region Wireless

        private async Task WirelessConnect()
        {
            using (var wirelessForm = new WirelessForm(_config))
            {
                if (wirelessForm.ShowDialog() == DialogResult.OK)
                {
                    _config = wirelessForm.Config;
                    SaveConfig();
                    await RefreshDevices();
                }
            }
        }

        private async Task DisconnectAll()
        {
            outputTextBox.Text = "Disconnecting all devices...\n";
            var (output, exitCode) = await RunCommandAsync(_config["adb"], "disconnect");
            if (exitCode != 0)
            {
                outputTextBox.AppendText($"\nError disconnecting: {output}");
            }
            else
            {
                outputTextBox.AppendText("Success!");
            }
            await RefreshDevices();
        }

        #endregion

        #region Drag and Drop

        void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        async void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0 && (files[0].EndsWith(".apk") || files[0].EndsWith(".aab")))
            {
                await ProcessApkFile(files[0]);
            }
        }

        #endregion

        #region Core Logic

        private async Task ProcessApkFile(string filePath)
        {
            apkPathTextBox.Text = filePath;
            var (pkg, version) = await GetApkInfo(filePath);
            packageNameTextBox.Text = pkg;
            apkVersionLabel.Text = $"Version: {version}";
            _config["LastApkPath"] = filePath;
            SaveConfig();
        }

        private async Task<(string, string)> GetApkInfo(string apkPath)
        {
            var (output, exitCode) = await RunCommandAsync(_config["aapt2"], $"dump badging \"{apkPath}\"");
            if (exitCode != 0)
            {
                outputTextBox.AppendText("\nCould not get package info: " + output);
                return (null, null);
            }

            var pkgMatch = Regex.Match(output, "package: name='([^']+)'");
            var versionMatch = Regex.Match(output, "versionName='([^']+)'");

            string pkg = pkgMatch.Success ? pkgMatch.Groups[1].Value : "not found";
            string version = versionMatch.Success ? versionMatch.Groups[1].Value : "---";

            return (pkg, version);
        }

        private async Task RunAppCommand(string action, params string[] adbCommands)
        {
            string device = deviceComboBox.SelectedItem as string;
            string pkg = packageNameTextBox.Text;

            if (string.IsNullOrEmpty(device) || string.IsNullOrEmpty(pkg)) { MessageBox.Show("Please select a device and ensure a package name is present."); return; }

            outputTextBox.Text = $"{action} {pkg} on {device}...\n";
            foreach (var command in adbCommands)
            {
                string arguments = $"-s {device} " + command.Replace("{{pkg}}", pkg);
                var (output, exitCode) = await RunCommandAsync(_config["adb"], arguments);
                if (exitCode != 0)
                {
                    outputTextBox.AppendText($"\nError running '{{command}}':\n" + output);
                    return; 
                }
                else
                {
                    outputTextBox.AppendText($"\nSuccess running '{{command}}':\n" + output);
                }
            }
        }

        private async Task RefreshDevices()
        {
            if (!_config.ContainsKey("adb")) return;
            var (output, exitCode) = await RunCommandAsync(_config["adb"], "devices");
            if (exitCode != 0)
            {
                outputTextBox.AppendText($"\nError refreshing devices: {output}");
                return;
            }

            var devices = new List<string>();
            var lines = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines.Skip(1))
            {
                if (line.Contains("device"))
                {
                    devices.Add(line.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries)[0]);
                }
            }

            string lastDevice = _config.ContainsKey("LastDevice") ? _config["LastDevice"] : null;
            deviceComboBox.DataSource = devices;

            if (devices.Contains(lastDevice))
            {
                deviceComboBox.SelectedItem = lastDevice;
            }
            else if (devices.Count > 0)
            {
                deviceComboBox.SelectedIndex = 0;
            }

            UpdateConnectionStatus();
        }

        private Task<(string output, int exitCode)> RunCommandAsync(string fileName, string arguments)
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
                var combinedOutput = new StringBuilder();

                using (var outputWaitHandle = new System.Threading.AutoResetEvent(false))
                using (var errorWaitHandle = new System.Threading.AutoResetEvent(false))
                {
                    process.OutputDataReceived += (s, e) => { if (e.Data != null) combinedOutput.AppendLine(e.Data); else outputWaitHandle.Set(); };
                    process.ErrorDataReceived += (s, e) => { if (e.Data != null) combinedOutput.AppendLine(e.Data); else errorWaitHandle.Set(); };

                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    process.WaitForExit();
                    outputWaitHandle.WaitOne();
                    errorWaitHandle.WaitOne();

                    return (combinedOutput.ToString(), process.ExitCode);
                }
            });
        }

        public void ShowOutput(string text)
        {
            outputTextBox.AppendText(text);
        }

        private void OpenFileExplorer()
        {
            string device = deviceComboBox.SelectedItem as string;
            if (string.IsNullOrEmpty(device))
            {
                MessageBox.Show("Please select a device first.", "No Device", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            new FileExplorerForm(this, _config, device).Show();
        }

        #region Helper Methods

        public void ShowMessageBoxWithOpenFile(string message, string title, string filePath = null)
        {
            using (Form msgForm = new Form()
            {
                Width = 500,
                Height = 200,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = title,
                StartPosition = FormStartPosition.CenterScreen
            })
            {
                Label textLabel = new Label() { Left = 20, Top = 20, Text = message, AutoSize = true, MaximumSize = new Size(450, 0) };
                Button okButton = new Button() { Text = "OK", Left = 350, Width = 100, Top = 120, DialogResult = DialogResult.OK };
                okButton.Click += (s, e) => { msgForm.Close(); };
                msgForm.Controls.Add(textLabel);
                msgForm.Controls.Add(okButton);

                if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                {
                    Button openPathButton = new Button() { Text = "Open Path", Left = 240, Width = 100, Top = 120 };
                    openPathButton.Click += (s, e) =>
                    {
                        try
                        {
                            Process.Start("explorer.exe", $"/select,\"{filePath}\"");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error opening file explorer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    };
                    msgForm.Controls.Add(openPathButton);
                }
                msgForm.ShowDialog();
            }
        }

        #endregion

        private string ShowInputDialog(string text)
        {
            using (Form prompt = new Form() { Width = 500, Height = 150, FormBorderStyle = FormBorderStyle.FixedDialog, Text = text, StartPosition = FormStartPosition.CenterScreen })
            {
                Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
                TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
                Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
                confirmation.Click += (s, e) => { prompt.Close(); };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;
                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
            }
        }

        #endregion

        #region Scrcpy

        private void RunScrcpy(bool record)
        {
            if (!_config.ContainsKey("scrcpy") || !File.Exists(_config["scrcpy"])) 
            {
                MessageBox.Show("Scrcpy path is not configured. Please set it in Tools -> Settings.", "Not Configured", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string device = deviceComboBox.SelectedItem as string;
            if (string.IsNullOrEmpty(device)) 
            {
                MessageBox.Show("Please select a device first.", "No Device", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string scrcpyPath = _config["scrcpy"];
            string arguments = $"-s {device}";

            if (record)
            {
                using (var sfd = new SaveFileDialog())
                {
                    sfd.Filter = "MP4 Video|*.mp4";
                    sfd.FileName = "recording_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".mp4";
                    if (sfd.ShowDialog() != DialogResult.OK) return;

                    arguments += $" --record \"{sfd.FileName}\"" ;
                }
            }

            Process.Start(scrcpyPath, arguments);
        }

        #endregion
    }
}
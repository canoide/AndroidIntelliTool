using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace AndroidIntelliTool
{
    public partial class SettingsForm : Form
    {
        public Dictionary<string, string> Config { get; private set; }

        public SettingsForm(Dictionary<string, string> config)
        {
            InitializeComponent();
            Config = new Dictionary<string, string>(config); // Work on a copy

            btnBrowseAdb.Click += (s, e) => BrowseForFile(textAdbPath, "adb.exe");
            btnBrowseAapt2.Click += (s, e) => BrowseForFile(textAapt2Path, "aapt2.exe");
            btnBrowseBundleTool.Click += (s, e) => BrowseForFile(textBundleToolPath, "bundletool.jar");
            btnBrowseScrcpy.Click += (s, e) => BrowseForFile(textScrcpyPath, "scrcpy.exe");
            btnBrowseNdk.Click += (s, e) => BrowseForFolder(textNdkPath);
            btnSave.Click += (s, e) => { SaveSettings(); this.DialogResult = DialogResult.OK; this.Close(); };
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            linkAdb.LinkClicked += (s, e) => OpenUrl("https://developer.android.com/studio/releases/platform-tools");
            linkAapt2.LinkClicked += (s, e) => OpenUrl("https://developer.android.com/studio");
            linkBundletool.LinkClicked += (s, e) => OpenUrl("https://github.com/google/bundletool/releases");
            linkScrcpy.LinkClicked += (s, e) => OpenUrl("https://github.com/Genymobile/scrcpy/releases");
            linkNdk.LinkClicked += (s, e) => OpenUrl("https://developer.android.com/ndk/downloads");

            LoadSettings();
        }

        private void LoadSettings()
        {
            if (Config.ContainsKey("adb")) textAdbPath.Text = Config["adb"];
            if (Config.ContainsKey("aapt2")) textAapt2Path.Text = Config["aapt2"];
            if (Config.ContainsKey("bundletool")) textBundleToolPath.Text = Config["bundletool"];
            if (Config.ContainsKey("scrcpy")) textScrcpyPath.Text = Config["scrcpy"];
            if (Config.ContainsKey("ndk")) textNdkPath.Text = Config["ndk"];
        }

        private void SaveSettings()
        {
            Config["adb"] = textAdbPath.Text;
            Config["aapt2"] = textAapt2Path.Text;
            Config["bundletool"] = textBundleToolPath.Text;
            Config["scrcpy"] = textScrcpyPath.Text;
            Config["ndk"] = textNdkPath.Text;
        }

        private void BrowseForFile(TextBox textBox, string defaultFileName)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.FileName = defaultFileName;
                ofd.Filter = $"{defaultFileName}|{defaultFileName}|All Files|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    textBox.Text = ofd.FileName;
                }
            }
        }

        private void BrowseForFolder(TextBox textBox)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    textBox.Text = fbd.SelectedPath;
                }
            }
        }

        private void OpenUrl(string url)
        {
            try
            {
                // In .NET Core, Process.Start needs UseShellExecute=true to open URLs.
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            catch
            {
                MessageBox.Show($"Could not open URL: {url}", "Error");
            }
        }
    }
}

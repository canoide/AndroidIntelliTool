using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndroidIntelliTool
{
    public partial class CrashLogAnalyzerForm : Form
    {
        private Dictionary<string, string> _config;
        private List<string> _soFilePaths = new List<string>();

        public CrashLogAnalyzerForm(Dictionary<string, string> config)
        {
            InitializeComponent();
            _config = config;

            this.DragEnter += new DragEventHandler(CrashLogAnalyzerForm_DragEnter);
            this.DragDrop += new DragEventHandler(CrashLogAnalyzerForm_DragDrop);
        }

        private void CrashLogAnalyzerForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void CrashLogAnalyzerForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length > 0)
            {
                foreach (string file in files)
                {
                    if (file.EndsWith(".so", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!_soFilePaths.Contains(file))
                        {
                            _soFilePaths.Add(file);
                        }
                    }
                }
                RefreshSoFilesListBox();
            }
        }

        private void selectSoFilesButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Shared Libraries (*.so)|*.so|All files (*.*)|*.*";
                openFileDialog.Multiselect = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _soFilePaths.AddRange(openFileDialog.FileNames);
                    RefreshSoFilesListBox();
                }
            }
        }

                        private async void analyzeButton_Click(object sender, EventArgs e)

                        {

                            if (!_config.ContainsKey("ndk") || string.IsNullOrWhiteSpace(_config["ndk"]))

                            {

                                MessageBox.Show("NDK path is not configured. Please set it in Tools -> Settings.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                return;

                            }

                

                            if (string.IsNullOrWhiteSpace(crashLogTextBox.Text))

                            {

                                MessageBox.Show("Please paste the crash log into the text box.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                return;

                            }

                

                            if (_soFilePaths.Count == 0)

                            {

                                MessageBox.Show("Please select at least one .so file.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                return;

                            }

                

                            symbolizedOutputTextBox.Text = "Analyzing crash log with llvm-addr2line...\n";

                

                            try

                            {

                                string ndkPath = _config["ndk"];

                                string llvmAddr2LinePath = Directory.GetFiles(ndkPath, "llvm-addr2line.exe", SearchOption.AllDirectories).FirstOrDefault();

                

                                if (string.IsNullOrEmpty(llvmAddr2LinePath))

                                {

                                    MessageBox.Show($"llvm-addr2line.exe not found in NDK path: \"{ndkPath}\".", "Tool Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    symbolizedOutputTextBox.Text = "Error: llvm-addr2line.exe not found.";

                                    return;

                                }

                

                                var backtraceRegex = new System.Text.RegularExpressions.Regex(@"#\d{2}\s+pc\s+((?:0x)?[0-9a-fA-F]+)\s+([^\s]+\.so)");

                                var matches = backtraceRegex.Matches(crashLogTextBox.Text);

                

                                if (matches.Count == 0)

                                {

                                    symbolizedOutputTextBox.AppendText("\nNo native backtrace found to symbolize.");

                                    return;

                                }

                

                                var outputBuilder = new StringBuilder();

                                outputBuilder.AppendLine("--- Symbolized Backtrace ---");

                

                                foreach (System.Text.RegularExpressions.Match match in matches)

                                {

                                    string originalLine = match.Value.Trim();

                                    string address = match.Groups[1].Value;

                                    string libraryName = Path.GetFileName(match.Groups[2].Value);

                

                                    outputBuilder.AppendLine($"\n{originalLine}"); // Print original line

                

                                    string soFilePath = _soFilePaths.FirstOrDefault(p => Path.GetFileName(p).Equals(libraryName, StringComparison.OrdinalIgnoreCase));

                

                                    if (soFilePath != null)

                                    {

                                        string arguments = $"-e \"{soFilePath}\" -f -C {address}";

                                        var (output, exitCode) = await RunCommandAsync(llvmAddr2LinePath, arguments);

                

                                        if (exitCode == 0 && !string.IsNullOrWhiteSpace(output))

                                        {

                                            // Indent the symbolized result

                                            outputBuilder.AppendLine($"    └─> {output.Trim().Replace("\r\n", " -> ").Replace("\n", " -> ")}");

                                        }

                                        else

                                        {

                                            outputBuilder.AppendLine($"    └─> Failed to symbolize address {address}.");

                                        }

                                    }

                                    else

                                    {

                                        outputBuilder.AppendLine($"    └─> Symbol file not provided for {libraryName}.");

                                    }

                                }

                

                                symbolizedOutputTextBox.Text = outputBuilder.ToString();

                            }

                            catch (Exception ex)

                            {

                                MessageBox.Show($"Error during symbolization: {ex.Message}", "Execution Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                symbolizedOutputTextBox.Text = $"Error: {ex.Message}";

                            }

                        }

        private void RefreshSoFilesListBox()
        {
            soFilesListBox.Items.Clear();
            foreach (var path in _soFilePaths)
            {
                soFilesListBox.Items.Add(path);
            }
        }

        // Helper method to run shell commands, similar to Form1
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


    }
}

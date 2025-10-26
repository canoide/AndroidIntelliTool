using System;
using System.Collections.Concurrent;
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
    public partial class LogcatForm : Form
    {
        private class LogEntry
        {
            public string Time { get; set; }
            public char PriorityChar { get; set; }
            public string Tag { get; set; }
            public string Message { get; set; }

            public ListViewItem ToListViewItem()
            {
                var item = new ListViewItem(Time) { ForeColor = GetColorForPriority(PriorityChar) };
                item.SubItems.Add(Tag);
                item.SubItems.Add(Message);
                item.Tag = this; // Store the original LogEntry object
                return item;
            }

            private static Color GetColorForPriority(char priority)
            {
                switch (priority)
                {
                    case 'E': return Color.Red;
                    case 'W': return Color.Orange;
                    case 'I': return Color.Green;
                    case 'D': return Color.Blue;
                    default: return Color.Black;
                }
            }
        }

        private Process _logcatProcess;
        private readonly string _device;
        private readonly string _adbPath;

        private readonly ConcurrentQueue<string> _rawLogQueue = new ConcurrentQueue<string>();
        private readonly List<LogEntry> _allLogEntries = new List<LogEntry>();
        private readonly Timer _updateTimer = new Timer();
        private static readonly Regex LogcatRegex = new Regex(@"^(\S+\s+\S+)\s+\d+\s+\d+\s+([VDIWEFS])\s+([^:]+):\s(.*)$", RegexOptions.Compiled);

        public LogcatForm(string device, string adbPath)
        {
            InitializeComponent();
            _device = device;
            _adbPath = adbPath;
            this.Load += LogcatForm_Load;
            this.FormClosing += LogcatForm_FormClosing;
        }

        private void LogcatForm_Load(object sender, EventArgs e)
        {
            applyFilterButton.Click += (s, ev) => ApplyFilters();
            pidFilterButton.Click += async (s, ev) => await ApplyPidFilter();
            clearLogButton.Click += (s, ev) => ClearLogs();
            exportSelectedButton.Click += (s, ev) => ExportLogs(true);
            exportAllButton.Click += (s, ev) => ExportLogs(false);
            logListView.KeyDown += LogListView_KeyDown;
            priorityComboBox.SelectedIndex = 0; // Verbose

            _updateTimer.Interval = 300;
            _updateTimer.Tick += UpdateUI;
            _updateTimer.Start();

            StartLogcat(); // Start with default, wide-open logcat
        }

        private void StartLogcat(string extraArgs = "*:V")
        {
            StopLogcat();
            _allLogEntries.Clear();
            logListView.Items.Clear();

            string arguments = $"-s {_device} logcat -v threadtime {extraArgs}";

            _logcatProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = _adbPath,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.UTF8
                },
                EnableRaisingEvents = true
            };

            _logcatProcess.OutputDataReceived += (s, e) => { if (!string.IsNullOrEmpty(e.Data)) _rawLogQueue.Enqueue(e.Data); };
            _logcatProcess.ErrorDataReceived += (s, e) => { if (!string.IsNullOrEmpty(e.Data)) _rawLogQueue.Enqueue("ERROR: " + e.Data); };

            _logcatProcess.Start();
            _logcatProcess.BeginOutputReadLine();
            _logcatProcess.BeginErrorReadLine();
        }

        private void UpdateUI(object sender, EventArgs e)
        {
            if (_rawLogQueue.IsEmpty) return;

            var itemsToAdd = new List<ListViewItem>();
            while (_rawLogQueue.TryDequeue(out string line))
            {
                var match = LogcatRegex.Match(line);
                if (!match.Success) continue;

                var logEntry = new LogEntry
                {
                    Time = match.Groups[1].Value,
                    PriorityChar = match.Groups[2].Value[0],
                    Tag = match.Groups[3].Value.Trim(),
                    Message = match.Groups[4].Value.Trim()
                };
                _allLogEntries.Add(logEntry);

                if (PassesClientFilters(logEntry))
                {
                    itemsToAdd.Add(logEntry.ToListViewItem());
                }
            }

            if (itemsToAdd.Any())
            {
                logListView.BeginUpdate();
                logListView.Items.AddRange(itemsToAdd.ToArray());
                logListView.EndUpdate();
            }
        }

        #region Filtering

        private async Task ApplyPidFilter()
        {
            string packageName = packageFilterTextBox.Text.Trim();
            if (string.IsNullOrEmpty(packageName))
            {
                // If clearing the PID filter, revert to general logging
                applyFilterButton.Enabled = true;
                StartLogcat();
                return;
            }

            var (pid, error) = await RunCommandAsync(_adbPath, $"-s {_device} shell pidof -s {packageName}");
            if (string.IsNullOrEmpty(pid) || !string.IsNullOrEmpty(error))
            {
                MessageBox.Show($"Could not find PID for package: {packageName}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            applyFilterButton.Enabled = false; // Disable other filters when filtering by PID
            StartLogcat($"--pid={pid.Trim()}");
        }

        private void ApplyFilters()
        {
            var filteredEntries = _allLogEntries.Where(PassesClientFilters).Select(e => e.ToListViewItem()).ToArray();
            logListView.BeginUpdate();
            logListView.Items.Clear();
            logListView.Items.AddRange(filteredEntries);
            logListView.EndUpdate();
        }

        private bool PassesClientFilters(LogEntry entry)
        {
            int selectedPriorityIndex = priorityComboBox.SelectedIndex;
            int entryPriorityIndex = "VDIWEFS".IndexOf(entry.PriorityChar);
            if (entryPriorityIndex < selectedPriorityIndex) return false;

            string tagFilter = tagFilterTextBox.Text;
            if (!string.IsNullOrEmpty(tagFilter) && !entry.Tag.Contains(tagFilter)) return false;

            string msgFilter = messageFilterTextBox.Text;
            if (!string.IsNullOrEmpty(msgFilter) && !entry.Message.Contains(msgFilter)) return false;

            return true;
        }

        #endregion

        #region Actions

        private void ClearLogs()
        {
            _allLogEntries.Clear();
            logListView.Items.Clear();
            Task.Run(() => RunCommandAsync(_adbPath, $"-s {_device} logcat -c"));
        }

        private void ExportLogs(bool selectedOnly)
        {
            var itemsToExport = (selectedOnly ? logListView.SelectedItems.Cast<ListViewItem>() : logListView.Items.Cast<ListViewItem>()).ToList();
            if (!itemsToExport.Any())
            {
                MessageBox.Show("No logs to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var sfd = new SaveFileDialog { Filter = "Text File|*.txt", FileName = "logcat_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt" })
            {
                if (sfd.ShowDialog() != DialogResult.OK) return;

                var lines = itemsToExport.Select(item =>
                {
                    var entry = (LogEntry)item.Tag;
                    return $"{entry.Time} {entry.PriorityChar}/{entry.Tag}: {entry.Message}";
                });
                File.WriteAllLines(sfd.FileName, lines);
                MessageBox.Show($"Exported {lines.Count()} lines to {sfd.FileName}", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LogListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                if (logListView.SelectedItems.Count == 0) return;
                var textToCopy = logListView.SelectedItems.Cast<ListViewItem>()
                    .Select(item => ((LogEntry)item.Tag).Message);
                Clipboard.SetText(string.Join("\n", textToCopy));
            }
        }

        #endregion

        private void StopLogcat()
        {
            if (_logcatProcess != null && !_logcatProcess.HasExited)
            {
                _logcatProcess.Kill();
                _logcatProcess = null;
            }
        }

        private void LogcatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _updateTimer.Stop();
            StopLogcat();
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
                var output = new StringBuilder();
                var error = new StringBuilder();
                using (var outputWaitHandle = new System.Threading.AutoResetEvent(false))
                using (var errorWaitHandle = new System.Threading.AutoResetEvent(false))
                {
                    process.OutputDataReceived += (s, e) => { if (e.Data != null) output.AppendLine(e.Data); else outputWaitHandle.Set(); };
                    process.ErrorDataReceived += (s, e) => { if (e.Data != null) error.AppendLine(e.Data); else errorWaitHandle.Set(); };
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

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

            public static Color GetColorForPriority(char priority)
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
        private readonly Form1 _mainForm;
        private readonly string _device;
        private readonly string _adbPath;
        private readonly Dictionary<string, string> _config;
        private bool _isClosing = false;

        // State fields for PID monitoring
        private string _pidFilteredPackageName;
        private string _currentPid;
        private Timer _pidMonitorTimer;

        private readonly ConcurrentQueue<string> _rawLogQueue = new ConcurrentQueue<string>();
        private readonly List<LogEntry> _allLogEntries = new List<LogEntry>();
        private readonly Timer _updateTimer = new Timer();
        private static readonly Regex LogcatRegex = new Regex(@"^(\S+\s+\S+)\s+\d+\s+\d+\s+([VDIWEFS])\s+([^:]+):\s(.*)$", RegexOptions.Compiled);

        // New features
        private bool _isPaused = false;
        private const int MAX_LOG_ENTRIES = 50000; // Limit to prevent memory issues
        private string _searchText = "";
        private int[] _logCountByPriority = new int[7]; // V,D,I,W,E,F,S

        public LogcatForm(Form1 mainForm, string device, string adbPath, Dictionary<string, string> config)
        {
            InitializeComponent();
            _mainForm = mainForm;
            _device = device;
            _adbPath = adbPath;
            _config = config;
            this.Load += async (s, e) => await LogcatForm_Load(s, e);
            this.FormClosing += LogcatForm_FormClosing;
        }

        private async Task LogcatForm_Load(object sender, EventArgs e)
        {
            applyFilterButton.Click += (s, ev) => ApplyFilters();
            pidFilterButton.Click += async (s, ev) => await ApplyPidFilter();
            clearLogButton.Click += (s, ev) => ClearLogs();
            exportSelectedButton.Click += (s, ev) => ExportLogs(true);
            exportAllButton.Click += (s, ev) => ExportLogs(false);
            copyButton.Click += (s, ev) => CopySelectedLogs();
            pauseButton.Click += (s, ev) => TogglePause();
            searchButton.Click += (s, ev) => ShowSearchDialog();
            logListView.KeyDown += LogListView_KeyDown;

            // Load saved filters
            tagFilterTextBox.Text = _config.GetValueOrDefault("LogcatTagFilter", "");
            priorityComboBox.SelectedIndex = int.TryParse(_config.GetValueOrDefault("LogcatPriorityFilter", "0"), out var priority) ? priority : 0;
            messageFilterTextBox.Text = _config.GetValueOrDefault("LogcatMessageFilter", "");
            packageFilterTextBox.Text = _config.GetValueOrDefault("LogcatPackageFilter", "");

            // Save filters on change
            tagFilterTextBox.TextChanged += (s, e) => 
            {
                _config["LogcatTagFilter"] = tagFilterTextBox.Text;
                applyFilterButton.Enabled = true;
            };
            priorityComboBox.SelectedIndexChanged += (s, e) =>
            {
                _config["LogcatPriorityFilter"] = priorityComboBox.SelectedIndex.ToString();
                applyFilterButton.Enabled = true;
            };
            messageFilterTextBox.TextChanged += (s, e) =>
            {
                _config["LogcatMessageFilter"] = messageFilterTextBox.Text;
                applyFilterButton.Enabled = true;
            };
            packageFilterTextBox.TextChanged += (s, e) => _config["LogcatPackageFilter"] = packageFilterTextBox.Text;

            _updateTimer.Interval = 300;
            _updateTimer.Tick += UpdateUI;
            _updateTimer.Start();

            if (!string.IsNullOrEmpty(packageFilterTextBox.Text))
            {
                await ApplyPidFilter();
            }
            else
            {
                StartLogcat();
            }
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
                EnableRaisingEvents = false // No longer using the Exited event
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

            // If paused, don't process new entries but keep them in queue
            if (_isPaused) return;

            var itemsToAdd = new List<ListViewItem>();
            const int maxItemsPerTick = 500;
            int processedCount = 0;

            while (processedCount < maxItemsPerTick && _rawLogQueue.TryDequeue(out string line))
            {
                processedCount++;
                var match = LogcatRegex.Match(line);
                LogEntry logEntry;
                if (match.Success)
                {
                    logEntry = new LogEntry
                    {
                        Time = match.Groups[1].Value,
                        PriorityChar = match.Groups[2].Value[0],
                        Tag = match.Groups[3].Value.Trim(),
                        Message = match.Groups[4].Value.Trim()
                    };
                }
                else
                {
                    logEntry = new LogEntry
                    {
                        Time = DateTime.Now.ToString("MM-dd HH:mm:ss.fff"),
                        PriorityChar = 'I',
                        Tag = "Unknown",
                        Message = line
                    };
                }

                // Update statistics
                int priorityIndex = "VDIWEFS".IndexOf(logEntry.PriorityChar);
                if (priorityIndex >= 0 && priorityIndex < _logCountByPriority.Length)
                {
                    _logCountByPriority[priorityIndex]++;
                }

                _allLogEntries.Add(logEntry);

                // Apply max entries limit to prevent memory issues
                if (_allLogEntries.Count > MAX_LOG_ENTRIES)
                {
                    int itemsToRemove = _allLogEntries.Count - MAX_LOG_ENTRIES;
                    _allLogEntries.RemoveRange(0, itemsToRemove);
                    // Also remove from ListView if needed
                    if (logListView.Items.Count > MAX_LOG_ENTRIES)
                    {
                        for (int i = 0; i < itemsToRemove && i < logListView.Items.Count; i++)
                        {
                            logListView.Items.RemoveAt(0);
                        }
                    }
                }

                if (PassesClientFilters(logEntry))
                {
                    var item = logEntry.ToListViewItem();

                    // Highlight search text if present
                    if (!string.IsNullOrEmpty(_searchText) &&
                        logEntry.Message.Contains(_searchText, StringComparison.OrdinalIgnoreCase))
                    {
                        item.BackColor = Color.Yellow;
                    }

                    itemsToAdd.Add(item);
                }
            }

            if (itemsToAdd.Any())
            {
                logListView.BeginUpdate();
                logListView.Items.AddRange(itemsToAdd.ToArray());

                if (autoScrollCheckBox.Checked && logListView.Items.Count > 0)
                {
                    logListView.EnsureVisible(logListView.Items.Count - 1);
                }

                logListView.EndUpdate();
            }

            // Update statistics display
            UpdateStatistics();
        }

        #region Filtering

        private async Task ApplyPidFilter()
        {
            StopPidMonitor();
            string packageName = packageFilterTextBox.Text.Trim();
            if (string.IsNullOrEmpty(packageName))
            {
                applyFilterButton.Enabled = true;
                pidStatusLabel.Text = "";
                _pidFilteredPackageName = null;
                _currentPid = null;
                StartLogcat();
                return;
            }

            _pidFilteredPackageName = packageName;
            pidStatusLabel.Text = $"Searching for PID for {packageName}...";
            string pid = await FindPidForPackage(packageName);

            if (string.IsNullOrEmpty(pid))
            {
                pidStatusLabel.Text = $"Watching for package: {packageName}...";
                StartPidMonitor(true); // isWatchingForNew = true
                _allLogEntries.Clear();
                logListView.Items.Clear();
            }
            else
            {
                pidStatusLabel.Text = $"Filtering by PID: {pid} ({packageName})";
                applyFilterButton.Enabled = false;
                _currentPid = pid;
                StartLogcat($"--pid={_currentPid}");
                StartPidMonitor(false); // isWatchingForNew = false
            }
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
            if (!string.IsNullOrEmpty(tagFilter) && !entry.Tag.Contains(tagFilter, StringComparison.OrdinalIgnoreCase)) return false;

            string msgFilter = messageFilterTextBox.Text;
            if (!string.IsNullOrEmpty(msgFilter) && !entry.Message.Contains(msgFilter, StringComparison.OrdinalIgnoreCase)) return false;

            return true;
        }

        #endregion

        #region Actions

        private void ClearLogs()
        {
            _allLogEntries.Clear();
            logListView.Items.Clear();
            Task.Run(async () => await RunCommandAsync(_adbPath, $"-s {_device} logcat -c"));
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

        private void CopySelectedLogs()
        {
            if (logListView.SelectedItems.Count == 0) return;
            var textToCopy = logListView.SelectedItems.Cast<ListViewItem>()
                .Select(item => ((LogEntry)item.Tag).Message);
            Clipboard.SetText(string.Join("\n", textToCopy));
        }

        private void LogListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                if (e.Shift)
                {
                    // Ctrl+Shift+C: Copy full line with format
                    CopySelectedLogsFullFormat();
                }
                else
                {
                    // Ctrl+C: Copy just messages
                    CopySelectedLogs();
                }
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                // Ctrl+F: Show search dialog
                ShowSearchDialog();
                e.Handled = true;
            }
        }

        private void CopySelectedLogsFullFormat()
        {
            if (logListView.SelectedItems.Count == 0) return;
            var textToCopy = logListView.SelectedItems.Cast<ListViewItem>()
                .Select(item =>
                {
                    var entry = (LogEntry)item.Tag;
                    return $"{entry.Time} {entry.PriorityChar}/{entry.Tag}: {entry.Message}";
                });
            Clipboard.SetText(string.Join("\n", textToCopy));
        }

        #endregion

        #region New Features

        private void TogglePause()
        {
            _isPaused = !_isPaused;
            pauseButton.Text = _isPaused ? "Resume" : "Pause";
            pauseButton.BackColor = _isPaused ? Color.LightCoral : SystemColors.Control;

            if (!_isPaused)
            {
                // When resuming, process any queued logs
                UpdateUI(null, null);
            }
        }

        private void ShowSearchDialog()
        {
            using (Form searchForm = new Form()
            {
                Width = 400,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Search Logs",
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            })
            {
                Label promptLabel = new Label() { Left = 20, Top = 20, Text = "Search for:", Width = 350 };
                TextBox searchTextBox = new TextBox() { Left = 20, Top = 50, Width = 350, Text = _searchText };
                Button searchButton = new Button() { Text = "Search", Left = 150, Width = 100, Top = 80, DialogResult = DialogResult.OK };
                Button clearButton = new Button() { Text = "Clear", Left = 260, Width = 100, Top = 80 };

                clearButton.Click += (s, e) =>
                {
                    _searchText = "";
                    ApplySearchHighlight();
                    searchForm.Close();
                };

                searchButton.Click += (s, e) =>
                {
                    _searchText = searchTextBox.Text;
                    ApplySearchHighlight();
                    searchForm.Close();
                };

                searchForm.Controls.Add(promptLabel);
                searchForm.Controls.Add(searchTextBox);
                searchForm.Controls.Add(searchButton);
                searchForm.Controls.Add(clearButton);
                searchForm.AcceptButton = searchButton;

                searchForm.ShowDialog();
            }
        }

        private void ApplySearchHighlight()
        {
            logListView.BeginUpdate();
            foreach (ListViewItem item in logListView.Items)
            {
                var entry = (LogEntry)item.Tag;

                // Reset colors first
                item.BackColor = Color.White;
                item.ForeColor = LogEntry.GetColorForPriority(entry.PriorityChar);

                // Apply highlight if search text matches
                if (!string.IsNullOrEmpty(_searchText) &&
                    (entry.Message.Contains(_searchText, StringComparison.OrdinalIgnoreCase) ||
                     entry.Tag.Contains(_searchText, StringComparison.OrdinalIgnoreCase)))
                {
                    item.BackColor = Color.Yellow;
                    item.ForeColor = Color.Black;
                }
            }
            logListView.EndUpdate();
        }

        private void UpdateStatistics()
        {
            // Update status bar with statistics
            int errors = _logCountByPriority[4]; // E
            int warnings = _logCountByPriority[3]; // W
            int infos = _logCountByPriority[2]; // I
            int total = _logCountByPriority.Sum();

            string stats = $"Total: {total}";
            if (errors > 0) stats += $" | Errors: {errors}";
            if (warnings > 0) stats += $" | Warnings: {warnings}";
            if (infos > 0) stats += $" | Info: {infos}";
            stats += $" | Visible: {logListView.Items.Count}/{_allLogEntries.Count}";

            if (_isPaused) stats += " | PAUSED";

            statsLabel.Text = stats;
        }

        #endregion

        #region PID Monitor

        private async Task<string> FindPidForPackage(string packageName)
        {
            // Primero intenta pidof
            var (pidofOutput, pidofError, pidofExitCode) = await RunCommandAsync(_adbPath, $"-s {_device} shell pidof {packageName}");
            string pid = pidofOutput.Trim();

            // Si devuelve múltiples PIDs (algunas apps tienen procesos hijos), toma el primero
            if (pid.Contains(' '))
                pid = pid.Split(' ').First();

            // Si pidof no funciona o devuelve nada, usa ps como fallback
            if (string.IsNullOrEmpty(pid))
            {
                var (psOutput, psError, psExitCode) = await RunCommandAsync(_adbPath, $"-s {_device} shell ps | grep {packageName}");
                if (psExitCode == 0 && !string.IsNullOrEmpty(psOutput))
                {
                    // Algunos Android ponen PID en columna 2, otros en 9, así que detectamos
                    var lines = psOutput.Split('\n').Where(l => l.Contains(packageName)).ToArray();
                    if (lines.Length > 0)
                    {
                        var parts = Regex.Split(lines[0].Trim(), @"\s+");
                        // Encuentra la columna que es puramente numérica y tiene sentido como PID
                        string pidCandidate = parts.FirstOrDefault(p => Regex.IsMatch(p, @"^\d+$"));
                        pid = pidCandidate ?? "";
                    }
                }
            }

            // Devuelve PID solo si es un número válido
            if (!Regex.IsMatch(pid, @"^\d+$"))
                pid = "";

            return pid;
        }

        private async Task<bool> IsProcessAlive(string pid)
        {
            if (string.IsNullOrEmpty(pid)) return false;
            var (output, error, exitCode) = await RunCommandAsync(_adbPath, $"-s {_device} shell test -d /proc/{pid}");
            return exitCode == 0;
        }

        private void StartPidMonitor(bool isWatchingForNew)
        {
            StopPidMonitor();
            _pidMonitorTimer = new Timer();
            _pidMonitorTimer.Interval = 2000;
            _pidMonitorTimer.Tag = isWatchingForNew;
            _pidMonitorTimer.Tick += _pidMonitorTimer_Tick;
            _pidMonitorTimer.Start();
        }

        private void StopPidMonitor()
        {
            if (_pidMonitorTimer != null)
            {
                _pidMonitorTimer.Stop();
                _pidMonitorTimer.Dispose();
                _pidMonitorTimer = null;
            }
        }

        private async void _pidMonitorTimer_Tick(object sender, EventArgs e)
        {
            var timer = sender as Timer;
            if (timer == null) return;
            bool isWatchingForNew = (bool)timer.Tag;

            if (isWatchingForNew)
            {
                pidStatusLabel.Text = $"Searching for PID for {_pidFilteredPackageName}...";
                string newPid = await FindPidForPackage(_pidFilteredPackageName);
                if (!string.IsNullOrEmpty(newPid))
                {
                    StopPidMonitor();
                    _currentPid = newPid;
                    pidStatusLabel.Text = $"Found PID: {_currentPid}. Filtering...";
                    applyFilterButton.Enabled = false;
                    StartLogcat($"--pid={_currentPid}");
                    StartPidMonitor(false);
                }
                else
                {
                    pidStatusLabel.Text = $"Watching for package: {_pidFilteredPackageName}...";
                }
            }
            else
            {
                bool isAlive = await IsProcessAlive(_currentPid);
                if (!isAlive)
                {
                    StopPidMonitor();
                    pidStatusLabel.Text = $"App stopped. Watching for restart...";
                    StartPidMonitor(true);
                }
            }
        }

        #endregion

        private void StopLogcat()
        {
            if (_logcatProcess != null)
            {
                if (!_logcatProcess.HasExited)
                {
                    _logcatProcess.Kill();
                }
                _logcatProcess = null;
            }
        }

        private void LogcatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _isClosing = true;
            _updateTimer.Stop();
            StopLogcat();
            StopPidMonitor();
            _mainForm.SaveConfiguration();
        }

        private Task<(string output, string error, int exitCode)> RunCommandAsync(string fileName, string arguments)
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
                    return (output.ToString(), error.ToString(), process.ExitCode);
                }
            });
        }
    }
}

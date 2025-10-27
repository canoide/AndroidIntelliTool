using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndroidIntelliTool
{
    public partial class FileExplorerForm : Form
    {
        private readonly Dictionary<string, string> _config;
        private readonly Form1 _mainForm;
        private readonly string _device;
        private string _currentLocalPath;
        private string _currentDevicePath = "/sdcard/";
        private Dictionary<string, int> _iconCache;

        public FileExplorerForm(Form1 mainForm, Dictionary<string, string> config, string device)
        {
            InitializeComponent();
            _mainForm = mainForm;
            _config = config;
            _device = device;

            this.Load += async (s, e) => await FileExplorerForm_Load(s, e);
        }

        private async Task FileExplorerForm_Load(object sender, EventArgs e)
        {
            _iconCache = new Dictionary<string, int>();

            // Add generic folder and file icons
            fileExplorerImageList.Images.Add(IconReader.GetFolderIcon().ToBitmap());
            _iconCache.Add("folder", 0);
            fileExplorerImageList.Images.Add(IconReader.GetGenericFileIcon().ToBitmap());
            _iconCache.Add("file", 1);

            // Populate drive combo box
            foreach (var drive in Directory.GetLogicalDrives())
            {
                driveComboBox.Items.Add(drive);
            }
            if (driveComboBox.Items.Count > 0)
            {
                driveComboBox.SelectedIndex = 0;
                _currentLocalPath = driveComboBox.SelectedItem.ToString();
            }
            else
            {
                _currentLocalPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }


            // File Explorer Tab
            driveComboBox.SelectedIndexChanged += (s, ev) => driveComboBox_SelectedIndexChanged(s, ev);
            localPathTextBox.KeyDown += (s, ev) => localPathTextBox_KeyDown(s, ev);
            localUpButton.Click += (s, ev) => localUpButton_Click(s, ev);
            refreshExplorerButton.Click += async (s, ev) => await RefreshFileExplorer();
            localFileListView.DoubleClick += (s, ev) => NavigateLocalDirectory();
            deviceFileListView.DoubleClick += async (s, ev) => await NavigateDeviceDirectory();
            deviceUpButton.Click += async (s, ev) => await NavigateDeviceUp();
            uploadButton.Click += async (s, ev) => await UploadFile();
            downloadButton.Click += async (s, ev) => await DownloadFile();
            deleteDeviceFileButton.Click += async (s, ev) => await DeleteDeviceFile();

            await RefreshFileExplorer();
        }

        #region File Explorer

        private async Task RefreshFileExplorer()
        {
            PopulateLocalFiles(_currentLocalPath);
            await PopulateDeviceFiles(_currentDevicePath);
        }

        private void PopulateLocalFiles(string path)
        {
            try
            {
                localFileListView.Items.Clear();
                var dirInfo = new DirectoryInfo(path);
                var items = new List<ListViewItem>();
                // Add parent directory navigation ".."
                if (dirInfo.Parent != null)
                {
                    var parentItem = new ListViewItem("..", _iconCache["folder"]);
                    parentItem.Tag = "dir_up";
                    items.Add(parentItem);
                }
                foreach (var dir in dirInfo.GetDirectories())
                {
                    // Skip hidden and system directories, and specific known system folders
                    if (((dir.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) ||
                        ((dir.Attributes & FileAttributes.System) == FileAttributes.System) ||
                        dir.Name.StartsWith("$") ||
                        dir.Name.Equals("System Volume Information", StringComparison.OrdinalIgnoreCase) ||
                        dir.Name.Equals("Recycle.Bin", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                    var item = new ListViewItem(dir.Name, _iconCache["folder"]);
                    item.Tag = dir.FullName;
                    items.Add(item);
                }
                foreach (var file in dirInfo.GetFiles())
                {
                    string extension = file.Extension.ToLowerInvariant();
                    int iconIndex;
                    if (!_iconCache.TryGetValue(extension, out iconIndex))
                    {
                        // Get icon for the file type and add to ImageList
                        Icon fileIcon = IconReader.GetFileIcon(file.FullName);
                        fileExplorerImageList.Images.Add(fileIcon.ToBitmap());
                        iconIndex = fileExplorerImageList.Images.Count - 1;
                        _iconCache.Add(extension, iconIndex);
                    }
                    var item = new ListViewItem(file.Name, iconIndex);
                    item.Tag = file.FullName;
                    items.Add(item);
                }
                localFileListView.Items.AddRange(items.ToArray());
                _currentLocalPath = path;
                localPathTextBox.Text = path;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error listing local files: {ex.Message}");
            }
        }


        private async Task PopulateDeviceFiles(string path)
        {
            if (string.IsNullOrEmpty(_device)) return;

            devicePathTextBox.Text = path;
            deviceFileListView.Items.Clear();

            var (output, exitCode) = await RunCommandAsync(_config["adb"], string.Format("-s {0} shell ls -F \"{1}\"", _device, path));
            if (exitCode != 0)
            {
                _mainForm.ShowOutput($"Error listing device files: {output}");
                return;
            }

            var items = new List<ListViewItem>();
            var lines = output.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                bool isDir = line.EndsWith("/");
                string name = isDir ? line.TrimEnd('/') : line;
                if (name.StartsWith("."))
                {
                    continue; // Skip hidden files/directories on device
                }
                var item = new ListViewItem(name, isDir ? _iconCache["folder"] : _iconCache["file"]);
                item.Tag = path + name + (isDir ? "/" : "");
                items.Add(item);
            }
            deviceFileListView.Items.AddRange(items.ToArray());
            _currentDevicePath = path;
        }

        private void NavigateLocalDirectory()
        {
            if (localFileListView.SelectedItems.Count == 0) return;
            var selectedItem = localFileListView.SelectedItems[0];
            string tag = selectedItem.Tag as string;

            if (tag == "dir_up")
            {
                PopulateLocalFiles(Directory.GetParent(_currentLocalPath).FullName);
            }
            else if (Directory.Exists(tag))
            {
                PopulateLocalFiles(tag);
            }
        }

        private async Task NavigateDeviceDirectory()
        {
            if (deviceFileListView.SelectedItems.Count == 0) return;
            var selectedItem = deviceFileListView.SelectedItems[0];
            string path = selectedItem.Tag as string;
            if (path.EndsWith("/"))
            {
                await PopulateDeviceFiles(path);
            }
        }

        private async Task NavigateDeviceUp()
        {
            if (_currentDevicePath == "/" || _currentDevicePath == "/sdcard/") return;
            string currentDir = _currentDevicePath.TrimEnd('/');
            var parentPath = Path.GetDirectoryName(currentDir).Replace('\\', '/') + "/";
            await PopulateDeviceFiles(parentPath);
        }

        private async Task UploadFile()
        {
            if (localFileListView.SelectedItems.Count == 0) { MessageBox.Show("Select a local file to upload."); return; }
            string localPath = localFileListView.SelectedItems[0].Tag as string;
            if (Directory.Exists(localPath)) { MessageBox.Show("Folder upload not supported yet."); return; }
            
            if (string.IsNullOrEmpty(_device)) { MessageBox.Show("Select a device."); return; }

            string devicePath = _currentDevicePath + Path.GetFileName(localPath);
            _mainForm.ShowOutput($"Uploading {Path.GetFileName(localPath)} to {devicePath}...");
            var (output, exitCode) = await RunCommandAsync(_config["adb"], string.Format("-s {0} push \"{1}\" \"{2}\"", _device, localPath, devicePath));
            if (exitCode != 0)
            {
                _mainForm.ShowOutput($"Error uploading file: {output}");
            }
            else
            {
                _mainForm.ShowOutput("Success!");
            }
            await PopulateDeviceFiles(_currentDevicePath);
        }

        private async Task DownloadFile()
        {
            if (deviceFileListView.SelectedItems.Count == 0) { MessageBox.Show("Select a device file to download."); return; }
            string devicePath = deviceFileListView.SelectedItems[0].Tag as string;
            if (devicePath.EndsWith("/")) { MessageBox.Show("Folder download not supported yet."); return; }
            
            if (string.IsNullOrEmpty(_device)) { MessageBox.Show("Select a device."); return; }

            string localPath = Path.Combine(_currentLocalPath, Path.GetFileName(devicePath));
            string tempPath = Path.Combine(Path.GetTempPath(), Path.GetFileName(devicePath));

            _mainForm.ShowOutput($"Downloading {Path.GetFileName(devicePath)} to temporary location...");
            var (output, exitCode) = await RunCommandAsync(_config["adb"], string.Format("-s {0} pull \"{1}\" \"{2}\"", _device, devicePath, tempPath));

            if (exitCode != 0)
            {
                _mainForm.ShowOutput($"Error during adb pull to temp: {output}");
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(_currentLocalPath);
                    File.Move(tempPath, localPath, true); 
                    _mainForm.ShowOutput($"Success! File moved to {localPath}");
                }
                catch (Exception ex)
                {
                    _mainForm.ShowOutput($"Error moving file from temp to final destination: {ex.Message}");
                    _mainForm.ShowMessageBoxWithOpenFile($"Error moviendo el archivo de la carpeta temporal a '{localPath}': {ex.Message} El archivo podría estar en la carpeta temporal: {tempPath}", "Error de Movimiento", tempPath);
                }
            }
            PopulateLocalFiles(_currentLocalPath);
        }

        private async Task DeleteDeviceFile()
        {
            if (deviceFileListView.SelectedItems.Count == 0) { MessageBox.Show("Select a device file to delete."); return; }
            string devicePath = deviceFileListView.SelectedItems[0].Tag as string;

            if (MessageBox.Show($"Are you sure you want to delete {devicePath}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            
            if (string.IsNullOrEmpty(_device)) { MessageBox.Show("Select a device."); return; }

            _mainForm.ShowOutput($"Deleting {devicePath}...");
            string command = devicePath.EndsWith("/") ? "shell rm -r" : "shell rm";
            var (output, exitCode) = await RunCommandAsync(_config["adb"], string.Format("-s {0} {1} \"{2}\"", _device, command, devicePath));
            if (exitCode != 0)
            {
                _mainForm.ShowOutput($"Error deleting file: {output}");
            }
            else
            {
                _mainForm.ShowOutput("Success!");
            }
            await PopulateDeviceFiles(_currentDevicePath);
        }

        #endregion

        #region File Explorer Handlers

        private void driveComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentLocalPath = driveComboBox.SelectedItem.ToString();
            PopulateLocalFiles(_currentLocalPath);
        }

        private void localPathTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var path = localPathTextBox.Text;
                if (Directory.Exists(path))
                {
                    PopulateLocalFiles(path);
                }
                else
                {
                    MessageBox.Show("The specified path does not exist.", "Invalid Path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void localUpButton_Click(object sender, EventArgs e)
        {
            if (Directory.GetParent(_currentLocalPath) != null)
            {
                PopulateLocalFiles(Directory.GetParent(_currentLocalPath).FullName);
            }
        }

        #endregion
        
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

    public static class IconReader
    {
        private const uint SHGFI_ICON = 0x100;
        private const uint SHGFI_LARGEICON = 0x0;
        private const uint SHGFI_SMALLICON = 0x1;
        private const uint SHGFI_USEFILEATTRIBUTES = 0x10;
        private const uint FILE_ATTRIBUTE_DIRECTORY = 0x10;
        private const uint FILE_ATTRIBUTE_NORMAL = 0x80;

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SHGetFileInfo(
            string pszPath,
            uint dwFileAttributes,
            ref SHFILEINFO psfi,
            uint cbSizeFileInfo,
            uint uFlags);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        public static Icon GetFolderIcon()
        {
            // Use a non-existent path with FILE_ATTRIBUTE_DIRECTORY to get a generic folder icon
            return GetIcon(null, FILE_ATTRIBUTE_DIRECTORY);
        }

        public static Icon GetGenericFileIcon()
        {
            // Use a non-existent path with FILE_ATTRIBUTE_NORMAL to get a generic file icon
            return GetIcon(null, FILE_ATTRIBUTE_NORMAL);
        }

        public static Icon GetFileIcon(string filePath)
        {
            return GetIcon(filePath, FILE_ATTRIBUTE_NORMAL);
        }

        private static Icon GetIcon(string path, uint attributes)
        {
            SHFILEINFO shinfo = new SHFILEINFO();
            uint flags = SHGFI_ICON | SHGFI_SMALLICON;

            if (path == null)
            {
                flags |= SHGFI_USEFILEATTRIBUTES;
                if (attributes == FILE_ATTRIBUTE_DIRECTORY)
                {
                    path = Environment.GetFolderPath(Environment.SpecialFolder.Windows); 
                }
                else 
                {
                    path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "notepad.exe"); 
                }
            }

            IntPtr hImgSmall = SHGetFileInfo(path, attributes, ref shinfo,
                                               (uint)Marshal.SizeOf(shinfo),
                                               flags);

            if (hImgSmall == IntPtr.Zero)
            {
                return SystemIcons.WinLogo;
            }

            Icon icon = (Icon)Icon.FromHandle(shinfo.hIcon).Clone();
            DestroyIcon(shinfo.hIcon);
            return icon;
        }

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DestroyIcon(IntPtr hIcon);
    }
}
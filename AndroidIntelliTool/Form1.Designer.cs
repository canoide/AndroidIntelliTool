namespace AndroidIntelliTool
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.mainTabPage = new System.Windows.Forms.TabPage();
            this.fileExplorerTabPage = new System.Windows.Forms.TabPage();
            this.deviceComboBox = new System.Windows.Forms.ComboBox();
            this.refreshDevicesButton = new System.Windows.Forms.Button();
            this.connectionStatusLabel = new System.Windows.Forms.Label();
            this.apkPathTextBox = new System.Windows.Forms.TextBox();
            this.selectApkButton = new System.Windows.Forms.Button();
            this.installButton = new System.Windows.Forms.Button();
            this.packageNameTextBox = new System.Windows.Forms.TextBox();
            this.restartAppButton = new System.Windows.Forms.Button();
            this.uninstallAppButton = new System.Windows.Forms.Button();
            this.clearDataButton = new System.Windows.Forms.Button();
            this.screenshotButton = new System.Windows.Forms.Button();
            this.logcatButton = new System.Windows.Forms.Button();
            this.outputTextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.apkVersionLabel = new System.Windows.Forms.Label();
            this.wirelessConnectButton = new System.Windows.Forms.Button();
            this.disconnectAllButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.screenMirrorButton = new System.Windows.Forms.Button();
            this.screenRecordButton = new System.Windows.Forms.Button();
            this.localFileListView = new System.Windows.Forms.ListView();
            this.deviceFileListView = new System.Windows.Forms.ListView();
            this.devicePathTextBox = new System.Windows.Forms.TextBox();
            this.deviceUpButton = new System.Windows.Forms.Button();
            this.uploadButton = new System.Windows.Forms.Button();
            this.downloadButton = new System.Windows.Forms.Button();
            this.deleteDeviceFileButton = new System.Windows.Forms.Button();
            this.refreshExplorerButton = new System.Windows.Forms.Button();
            this.fileExplorerImageList = new System.Windows.Forms.ImageList(this.components);
            this.forceStopAppButton = new System.Windows.Forms.Button();
            this.setDefaultDownloadFolderButton = new System.Windows.Forms.Button();
            this.mainTabControl.SuspendLayout();
            this.mainTabPage.SuspendLayout();
            this.fileExplorerTabPage.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileExplorerImageList
            // 
            this.fileExplorerImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("fileExplorerImageList.ImageStream")));
            this.fileExplorerImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.mainTabPage);
            this.mainTabControl.Controls.Add(this.fileExplorerTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 24);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(561, 426);
            this.mainTabControl.TabIndex = 21;
            // 
            // mainTabPage
            // 
            this.mainTabPage.Controls.Add(this.deviceComboBox);
            this.mainTabPage.Controls.Add(this.refreshDevicesButton);
            this.mainTabPage.Controls.Add(this.connectionStatusLabel);
            this.mainTabPage.Controls.Add(this.label1);
            this.mainTabPage.Controls.Add(this.wirelessConnectButton);
            this.mainTabPage.Controls.Add(this.disconnectAllButton);
            this.mainTabPage.Controls.Add(this.apkPathTextBox);
            this.mainTabPage.Controls.Add(this.label2);
            this.mainTabPage.Controls.Add(this.selectApkButton);
            this.mainTabPage.Controls.Add(this.installButton);
            this.mainTabPage.Controls.Add(this.packageNameTextBox);
            this.mainTabPage.Controls.Add(this.label3);
            this.mainTabPage.Controls.Add(this.apkVersionLabel);
            this.mainTabPage.Controls.Add(this.restartAppButton);
            this.mainTabPage.Controls.Add(this.uninstallAppButton);
            this.mainTabPage.Controls.Add(this.clearDataButton);
            this.mainTabPage.Controls.Add(this.screenshotButton);
            this.mainTabPage.Controls.Add(this.logcatButton);
            this.mainTabPage.Controls.Add(this.screenMirrorButton);
            this.mainTabPage.Controls.Add(this.screenRecordButton);
            this.mainTabPage.Controls.Add(this.outputTextBox);
            this.mainTabPage.Controls.Add(this.forceStopAppButton);
            this.mainTabPage.Location = new System.Drawing.Point(4, 22);
            this.mainTabPage.Name = "mainTabPage";
            this.mainTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.mainTabPage.Size = new System.Drawing.Size(553, 400);
            this.mainTabPage.TabIndex = 0;
            this.mainTabPage.Text = "Main";
            this.mainTabPage.UseVisualStyleBackColor = true;
            // 
            // fileExplorerTabPage
            // 
            this.fileExplorerTabPage.Controls.Add(this.setDefaultDownloadFolderButton);
            this.fileExplorerTabPage.Controls.Add(this.downloadButton);
            this.fileExplorerTabPage.Controls.Add(this.uploadButton);
            this.fileExplorerTabPage.Controls.Add(this.deleteDeviceFileButton);
            this.fileExplorerTabPage.Controls.Add(this.refreshExplorerButton);
            this.fileExplorerTabPage.Controls.Add(this.deviceUpButton);
            this.fileExplorerTabPage.Controls.Add(this.devicePathTextBox);
            this.fileExplorerTabPage.Controls.Add(this.deviceFileListView);
            this.fileExplorerTabPage.Controls.Add(this.localFileListView);
            this.fileExplorerTabPage.Location = new System.Drawing.Point(4, 22);
            this.fileExplorerTabPage.Name = "fileExplorerTabPage";
            this.fileExplorerTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.fileExplorerTabPage.Size = new System.Drawing.Size(553, 400);
            this.fileExplorerTabPage.TabIndex = 1;
            this.fileExplorerTabPage.Text = "File Explorer";
            this.fileExplorerTabPage.UseVisualStyleBackColor = true;
            // 
            // deviceComboBox
            // 
            this.deviceComboBox.FormattingEnabled = true;
            this.deviceComboBox.Location = new System.Drawing.Point(8, 29);
            this.deviceComboBox.Name = "deviceComboBox";
            this.deviceComboBox.Size = new System.Drawing.Size(237, 21);
            this.deviceComboBox.TabIndex = 0;
            // 
            // refreshDevicesButton
            // 
            this.refreshDevicesButton.Location = new System.Drawing.Point(251, 28);
            this.refreshDevicesButton.Name = "refreshDevicesButton";
            this.refreshDevicesButton.Size = new System.Drawing.Size(75, 23);
            this.refreshDevicesButton.TabIndex = 1;
            this.refreshDevicesButton.Text = "Refresh";
            this.refreshDevicesButton.UseVisualStyleBackColor = true;
            // 
            // connectionStatusLabel
            // 
            this.connectionStatusLabel.AutoSize = true;
            this.connectionStatusLabel.Location = new System.Drawing.Point(8, 53);
            this.connectionStatusLabel.Name = "connectionStatusLabel";
            this.connectionStatusLabel.Size = new System.Drawing.Size(79, 13);
            this.connectionStatusLabel.TabIndex = 2;
            this.connectionStatusLabel.Text = "Status: (none)";
            // 
            // apkPathTextBox
            // 
            this.apkPathTextBox.AllowDrop = true;
            this.apkPathTextBox.Location = new System.Drawing.Point(8, 106);
            this.apkPathTextBox.Name = "apkPathTextBox";
            this.apkPathTextBox.Size = new System.Drawing.Size(453, 20);
            this.apkPathTextBox.TabIndex = 3;
            // 
            // selectApkButton
            // 
            this.selectApkButton.Location = new System.Drawing.Point(467, 104);
            this.selectApkButton.Name = "selectApkButton";
            this.selectApkButton.Size = new System.Drawing.Size(75, 23);
            this.selectApkButton.TabIndex = 4;
            this.selectApkButton.Text = "Select APK";
            this.selectApkButton.UseVisualStyleBackColor = true;
            // 
            // installButton
            // 
            this.installButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.installButton.Location = new System.Drawing.Point(8, 132);
            this.installButton.Name = "installButton";
            this.installButton.Size = new System.Drawing.Size(534, 30);
            this.installButton.TabIndex = 5;
            this.installButton.Text = "Install";
            this.installButton.UseVisualStyleBackColor = true;
            // 
            // packageNameTextBox
            // 
            this.packageNameTextBox.Location = new System.Drawing.Point(8, 191);
            this.packageNameTextBox.Name = "packageNameTextBox";
            this.packageNameTextBox.Size = new System.Drawing.Size(237, 20);
            this.packageNameTextBox.TabIndex = 6;
            // 
            // restartAppButton
            // 
            this.restartAppButton.Location = new System.Drawing.Point(8, 217);
            this.restartAppButton.Name = "restartAppButton";
            this.restartAppButton.Size = new System.Drawing.Size(81, 23);
            this.restartAppButton.TabIndex = 7;
            this.restartAppButton.Text = "Restart App";
            this.restartAppButton.UseVisualStyleBackColor = true;
            // 
            // uninstallAppButton
            // 
            this.uninstallAppButton.Location = new System.Drawing.Point(95, 217);
            this.uninstallAppButton.Name = "uninstallAppButton";
            this.uninstallAppButton.Size = new System.Drawing.Size(75, 23);
            this.uninstallAppButton.TabIndex = 8;
            this.uninstallAppButton.Text = "Uninstall";
            this.uninstallAppButton.UseVisualStyleBackColor = true;
            // 
            // clearDataButton
            // 
            this.clearDataButton.Location = new System.Drawing.Point(176, 217);
            this.clearDataButton.Name = "clearDataButton";
            this.clearDataButton.Size = new System.Drawing.Size(75, 23);
            this.clearDataButton.TabIndex = 9;
            this.clearDataButton.Text = "Clear Data";
            this.clearDataButton.UseVisualStyleBackColor = true;
            // 
            // screenshotButton
            // 
            this.screenshotButton.Location = new System.Drawing.Point(257, 217);
            this.screenshotButton.Name = "screenshotButton";
            this.screenshotButton.Size = new System.Drawing.Size(100, 23);
            this.screenshotButton.TabIndex = 10;
            this.screenshotButton.Text = "Take Screenshot";
            this.screenshotButton.UseVisualStyleBackColor = true;
            // 
            // logcatButton
            // 
            this.logcatButton.Location = new System.Drawing.Point(363, 217);
            this.logcatButton.Name = "logcatButton";
            this.logcatButton.Size = new System.Drawing.Size(75, 23);
            this.logcatButton.TabIndex = 11;
            this.logcatButton.Text = "Show Logs";
            this.logcatButton.UseVisualStyleBackColor = true;
            // 
            // outputTextBox
            // 
            this.outputTextBox.Location = new System.Drawing.Point(8, 299);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.Size = new System.Drawing.Size(534, 84);
            this.outputTextBox.TabIndex = 12;
            this.outputTextBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Device";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "APK/AAB File (or drag it)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Package Name";
            // 
            // apkVersionLabel
            // 
            this.apkVersionLabel.AutoSize = true;
            this.apkVersionLabel.Location = new System.Drawing.Point(254, 194);
            this.apkVersionLabel.Name = "apkVersionLabel";
            this.apkVersionLabel.Size = new System.Drawing.Size(63, 13);
            this.apkVersionLabel.TabIndex = 16;
            this.apkVersionLabel.Text = "Version: ---";
            // 
            // wirelessConnectButton
            // 
            this.wirelessConnectButton.Location = new System.Drawing.Point(332, 28);
            this.wirelessConnectButton.Name = "wirelessConnectButton";
            this.wirelessConnectButton.Size = new System.Drawing.Size(90, 23);
            this.wirelessConnectButton.TabIndex = 17;
            this.wirelessConnectButton.Text = "Wireless Connect";
            this.wirelessConnectButton.UseVisualStyleBackColor = true;
            // 
            // disconnectAllButton
            // 
            this.disconnectAllButton.Location = new System.Drawing.Point(428, 28);
            this.disconnectAllButton.Name = "disconnectAllButton";
            this.disconnectAllButton.Size = new System.Drawing.Size(114, 23);
            this.disconnectAllButton.TabIndex = 18;
            this.disconnectAllButton.Text = "Disconnect All";
            this.disconnectAllButton.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(561, 24);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // screenMirrorButton
            // 
            this.screenMirrorButton.Location = new System.Drawing.Point(8, 246);
            this.screenMirrorButton.Name = "screenMirrorButton";
            this.screenMirrorButton.Size = new System.Drawing.Size(162, 23);
            this.screenMirrorButton.TabIndex = 21;
            this.screenMirrorButton.Text = "View Screen (scrcpy)";
            this.screenMirrorButton.UseVisualStyleBackColor = true;
            // 
            // screenRecordButton
            // 
            this.screenRecordButton.Location = new System.Drawing.Point(176, 246);
            this.screenRecordButton.Name = "screenRecordButton";
            this.screenRecordButton.Size = new System.Drawing.Size(162, 23);
            this.screenRecordButton.TabIndex = 22;
            this.screenRecordButton.Text = "Record Screen (scrcpy)";
            this.screenRecordButton.UseVisualStyleBackColor = true;
            // 
            // forceStopAppButton
            // 
            this.forceStopAppButton.Location = new System.Drawing.Point(440, 217);
            this.forceStopAppButton.Name = "forceStopAppButton";
            this.forceStopAppButton.Size = new System.Drawing.Size(102, 23);
            this.forceStopAppButton.TabIndex = 23;
            this.forceStopAppButton.Text = "Force Stop App";
            this.forceStopAppButton.UseVisualStyleBackColor = true;
            // 
            // localFileListView
            // 
            this.localFileListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            new System.Windows.Forms.ColumnHeader { Text = "Name", Width = 240 }
            });
            this.localFileListView.Location = new System.Drawing.Point(6, 6);
            this.localFileListView.Name = "localFileListView";
            this.localFileListView.Size = new System.Drawing.Size(260, 350);
            this.localFileListView.SmallImageList = this.fileExplorerImageList;
            this.localFileListView.UseCompatibleStateImageBehavior = false;
            this.localFileListView.View = System.Windows.Forms.View.Details;
            // 
            // deviceFileListView
            // 
            this.deviceFileListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            new System.Windows.Forms.ColumnHeader { Text = "Name", Width = 240 }
            });
            this.deviceFileListView.Location = new System.Drawing.Point(285, 32);
            this.deviceFileListView.Name = "deviceFileListView";
            this.deviceFileListView.Size = new System.Drawing.Size(260, 324);
            this.deviceFileListView.SmallImageList = this.fileExplorerImageList;
            this.deviceFileListView.UseCompatibleStateImageBehavior = false;
            this.deviceFileListView.View = System.Windows.Forms.View.Details;
            // 
            // devicePathTextBox
            // 
            this.devicePathTextBox.Location = new System.Drawing.Point(315, 6);
            this.devicePathTextBox.Name = "devicePathTextBox";
            this.devicePathTextBox.ReadOnly = true;
            this.devicePathTextBox.Size = new System.Drawing.Size(230, 20);
            // 
            // deviceUpButton
            // 
            this.deviceUpButton.Location = new System.Drawing.Point(285, 5);
            this.deviceUpButton.Name = "deviceUpButton";
            this.deviceUpButton.Size = new System.Drawing.Size(24, 22);
            this.deviceUpButton.Text = "^";
            // 
            // uploadButton
            // 
            this.uploadButton.Location = new System.Drawing.Point(200, 362);
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.Size = new System.Drawing.Size(66, 23);
            this.uploadButton.TabIndex = 0;
            this.uploadButton.Text = "Upload";
            // 
            // downloadButton
            // 
            this.downloadButton.Location = new System.Drawing.Point(285, 362);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(75, 23);
            this.downloadButton.TabIndex = 0;
            this.downloadButton.Text = "Download";
            // 
            // deleteDeviceFileButton
            // 
            this.deleteDeviceFileButton.Location = new System.Drawing.Point(366, 362);
            this.deleteDeviceFileButton.Name = "deleteDeviceFileButton";
            this.deleteDeviceFileButton.Size = new System.Drawing.Size(75, 23);
            this.deleteDeviceFileButton.Text = "Delete";
            // 
            // refreshExplorerButton
            // 
            this.refreshExplorerButton.Location = new System.Drawing.Point(447, 362);
            this.refreshExplorerButton.Name = "refreshExplorerButton";
            this.refreshExplorerButton.Size = new System.Drawing.Size(98, 23);
            this.refreshExplorerButton.TabIndex = 0;
            this.refreshExplorerButton.Text = "Refresh Lists";
            // 
            // setDefaultDownloadFolderButton
            // 
            this.setDefaultDownloadFolderButton.Location = new System.Drawing.Point(6, 389);
            this.setDefaultDownloadFolderButton.Name = "setDefaultDownloadFolderButton";
            this.setDefaultDownloadFolderButton.Size = new System.Drawing.Size(188, 23);
            this.setDefaultDownloadFolderButton.Text = "Set Default Download Folder";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 450);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Android IntelliTool";
            this.mainTabControl.ResumeLayout(false);
            this.mainTabPage.ResumeLayout(false);
            this.mainTabPage.PerformLayout();
            this.fileExplorerTabPage.ResumeLayout(false);
            this.fileExplorerTabPage.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage mainTabPage;
        private System.Windows.Forms.TabPage fileExplorerTabPage;
        private System.Windows.Forms.ComboBox deviceComboBox;
        private System.Windows.Forms.Button refreshDevicesButton;
        private System.Windows.Forms.Label connectionStatusLabel;
        private System.Windows.Forms.TextBox apkPathTextBox;
        private System.Windows.Forms.Button selectApkButton;
        private System.Windows.Forms.Button installButton;
        private System.Windows.Forms.TextBox packageNameTextBox;
        private System.Windows.Forms.Button restartAppButton;
        private System.Windows.Forms.Button uninstallAppButton;
        private System.Windows.Forms.Button clearDataButton;
        private System.Windows.Forms.Button screenshotButton;
        private System.Windows.Forms.Button logcatButton;
        private System.Windows.Forms.RichTextBox outputTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label apkVersionLabel;
        private System.Windows.Forms.Button wirelessConnectButton;
        private System.Windows.Forms.Button disconnectAllButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.Button screenMirrorButton;
        private System.Windows.Forms.Button screenRecordButton;
        // File Explorer Controls
        private System.Windows.Forms.ListView localFileListView;
        private System.Windows.Forms.ListView deviceFileListView;
        private System.Windows.Forms.TextBox devicePathTextBox;
        private System.Windows.Forms.Button deviceUpButton;
        private System.Windows.Forms.Button uploadButton;
        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.Button deleteDeviceFileButton;
        private System.Windows.Forms.Button refreshExplorerButton;
        private System.Windows.Forms.ImageList fileExplorerImageList;
        private System.Windows.Forms.Button forceStopAppButton;
        private System.Windows.Forms.Button setDefaultDownloadFolderButton;

    }
}

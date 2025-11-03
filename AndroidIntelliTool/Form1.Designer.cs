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
            this.helpMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.crashLogAnalyzerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.screenMirrorButton = new System.Windows.Forms.Button();
            this.screenRecordButton = new System.Windows.Forms.Button();
            this.forceStopAppButton = new System.Windows.Forms.Button();
            this.fileExplorerButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // deviceComboBox
            // 
            this.deviceComboBox.FormattingEnabled = true;
            this.deviceComboBox.Location = new System.Drawing.Point(8, 53);
            this.deviceComboBox.Name = "deviceComboBox";
            this.deviceComboBox.Size = new System.Drawing.Size(237, 21);
            this.deviceComboBox.TabIndex = 0;
            // 
            // refreshDevicesButton
            // 
            this.refreshDevicesButton.Location = new System.Drawing.Point(251, 52);
            this.refreshDevicesButton.Name = "refreshDevicesButton";
            this.refreshDevicesButton.Size = new System.Drawing.Size(75, 23);
            this.refreshDevicesButton.TabIndex = 1;
            this.refreshDevicesButton.Text = "Refresh";
            this.refreshDevicesButton.UseVisualStyleBackColor = true;
            // 
            // connectionStatusLabel
            // 
            this.connectionStatusLabel.AutoSize = true;
            this.connectionStatusLabel.Location = new System.Drawing.Point(8, 77);
            this.connectionStatusLabel.Name = "connectionStatusLabel";
            this.connectionStatusLabel.Size = new System.Drawing.Size(79, 13);
            this.connectionStatusLabel.TabIndex = 2;
            this.connectionStatusLabel.Text = "Status: (none)";
            // 
            // apkPathTextBox
            // 
            this.apkPathTextBox.AllowDrop = true;
            this.apkPathTextBox.Location = new System.Drawing.Point(8, 174);
            this.apkPathTextBox.Name = "apkPathTextBox";
            this.apkPathTextBox.Size = new System.Drawing.Size(453, 20);
            this.apkPathTextBox.TabIndex = 3;
            // 
            // selectApkButton
            // 
            this.selectApkButton.Location = new System.Drawing.Point(467, 172);
            this.selectApkButton.Name = "selectApkButton";
            this.selectApkButton.Size = new System.Drawing.Size(75, 23);
            this.selectApkButton.TabIndex = 4;
            this.selectApkButton.Text = "Select APK";
            this.selectApkButton.UseVisualStyleBackColor = true;
            // 
            // installButton
            // 
            this.installButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.installButton.Location = new System.Drawing.Point(8, 194);
            this.installButton.Name = "installButton";
            this.installButton.Size = new System.Drawing.Size(534, 30);
            this.installButton.TabIndex = 5;
            this.installButton.Text = "Install and Run";
            this.installButton.UseVisualStyleBackColor = true;
            // 
            // packageNameTextBox
            // 
            this.packageNameTextBox.Location = new System.Drawing.Point(8, 246);
            this.packageNameTextBox.Name = "packageNameTextBox";
            this.packageNameTextBox.Size = new System.Drawing.Size(237, 20);
            this.packageNameTextBox.TabIndex = 6;
            // 
            // restartAppButton
            // 
            this.restartAppButton.Location = new System.Drawing.Point(8, 272);
            this.restartAppButton.Name = "restartAppButton";
            this.restartAppButton.Size = new System.Drawing.Size(136, 23);
            this.restartAppButton.TabIndex = 7;
            this.restartAppButton.Text = "Restart App";
            this.restartAppButton.UseVisualStyleBackColor = true;
            // 
            // uninstallAppButton
            // 
            this.uninstallAppButton.Location = new System.Drawing.Point(146, 272);
            this.uninstallAppButton.Name = "uninstallAppButton";
            this.uninstallAppButton.Size = new System.Drawing.Size(136, 23);
            this.uninstallAppButton.TabIndex = 8;
            this.uninstallAppButton.Text = "Uninstall";
            this.uninstallAppButton.UseVisualStyleBackColor = true;
            // 
            // clearDataButton
            // 
            this.clearDataButton.Location = new System.Drawing.Point(284, 272);
            this.clearDataButton.Name = "clearDataButton";
            this.clearDataButton.Size = new System.Drawing.Size(136, 23);
            this.clearDataButton.TabIndex = 9;
            this.clearDataButton.Text = "Clear Data";
            this.clearDataButton.UseVisualStyleBackColor = true;
            // 
            // screenshotButton
            // 
            this.screenshotButton.Location = new System.Drawing.Point(8, 129);
            this.screenshotButton.Name = "screenshotButton";
            this.screenshotButton.Size = new System.Drawing.Size(180, 23);
            this.screenshotButton.TabIndex = 10;
            this.screenshotButton.Text = "Take Screenshot";
            this.screenshotButton.UseVisualStyleBackColor = true;
            // 
            // logcatButton
            // 
            this.logcatButton.Location = new System.Drawing.Point(8, 100);
            this.logcatButton.Name = "logcatButton";
            this.logcatButton.Size = new System.Drawing.Size(269, 23);
            this.logcatButton.TabIndex = 11;
            this.logcatButton.Text = "Show Logs";
            this.logcatButton.UseVisualStyleBackColor = true;
            // 
            // outputTextBox
            // 
            this.outputTextBox.Location = new System.Drawing.Point(8, 301);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.Size = new System.Drawing.Size(534, 123);
            this.outputTextBox.TabIndex = 12;
            this.outputTextBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Device";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "APK/AAB File (or drag it)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 230);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Package Name";
            // 
            // apkVersionLabel
            // 
            this.apkVersionLabel.AutoSize = true;
            this.apkVersionLabel.Location = new System.Drawing.Point(254, 249);
            this.apkVersionLabel.Name = "apkVersionLabel";
            this.apkVersionLabel.Size = new System.Drawing.Size(63, 13);
            this.apkVersionLabel.TabIndex = 16;
            this.apkVersionLabel.Text = "Version: ---";
            // 
            // wirelessConnectButton
            // 
            this.wirelessConnectButton.Location = new System.Drawing.Point(332, 52);
            this.wirelessConnectButton.Name = "wirelessConnectButton";
            this.wirelessConnectButton.Size = new System.Drawing.Size(90, 23);
            this.wirelessConnectButton.TabIndex = 17;
            this.wirelessConnectButton.Text = "Wireless Connect";
            this.wirelessConnectButton.UseVisualStyleBackColor = true;
            // 
            // disconnectAllButton
            // 
            this.disconnectAllButton.Location = new System.Drawing.Point(428, 52);
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
            this.toolsToolStripMenuItem,
            this.helpMenuToolStripMenuItem});
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
            this.settingsToolStripMenuItem,
            this.crashLogAnalyzerToolStripMenuItem});
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
            // helpMenuToolStripMenuItem
            // 
            this.helpMenuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpMenuToolStripMenuItem.Name = "helpMenuToolStripMenuItem";
            this.helpMenuToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpMenuToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // crashLogAnalyzerToolStripMenuItem
            // 
            this.crashLogAnalyzerToolStripMenuItem.Name = "crashLogAnalyzerToolStripMenuItem";
            this.crashLogAnalyzerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.crashLogAnalyzerToolStripMenuItem.Text = "Crash Log Analyzer";
            this.crashLogAnalyzerToolStripMenuItem.Click += new System.EventHandler(this.OpenCrashLogAnalyzer);
            // 
            // screenMirrorButton
            // 
            this.screenMirrorButton.Location = new System.Drawing.Point(190, 129);
            this.screenMirrorButton.Name = "screenMirrorButton";
            this.screenMirrorButton.Size = new System.Drawing.Size(180, 23);
            this.screenMirrorButton.TabIndex = 21;
            this.screenMirrorButton.Text = "View Screen";
            this.screenMirrorButton.UseVisualStyleBackColor = true;
            // 
            // screenRecordButton
            // 
            this.screenRecordButton.Location = new System.Drawing.Point(372, 129);
            this.screenRecordButton.Name = "screenRecordButton";
            this.screenRecordButton.Size = new System.Drawing.Size(180, 23);
            this.screenRecordButton.TabIndex = 22;
            this.screenRecordButton.Text = "Record Screen";
            this.screenRecordButton.UseVisualStyleBackColor = true;
            // 
            // forceStopAppButton
            // 
            this.forceStopAppButton.Location = new System.Drawing.Point(422, 272);
            this.forceStopAppButton.Name = "forceStopAppButton";
            this.forceStopAppButton.Size = new System.Drawing.Size(136, 23);
            this.forceStopAppButton.TabIndex = 23;
            this.forceStopAppButton.Text = "Force Stop App";
            this.forceStopAppButton.UseVisualStyleBackColor = true;
            // 
            // fileExplorerButton
            // 
            this.fileExplorerButton.Location = new System.Drawing.Point(283, 100);
            this.fileExplorerButton.Name = "fileExplorerButton";
            this.fileExplorerButton.Size = new System.Drawing.Size(269, 23);
            this.fileExplorerButton.TabIndex = 24;
            this.fileExplorerButton.Text = "File Explorer";
            this.fileExplorerButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 450);
            this.Controls.Add(this.fileExplorerButton);
            this.Controls.Add(this.forceStopAppButton);
            this.Controls.Add(this.screenRecordButton);
            this.Controls.Add(this.screenMirrorButton);
            this.Controls.Add(this.disconnectAllButton);
            this.Controls.Add(this.wirelessConnectButton);
            this.Controls.Add(this.apkVersionLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.logcatButton);
            this.Controls.Add(this.screenshotButton);
            this.Controls.Add(this.clearDataButton);
            this.Controls.Add(this.uninstallAppButton);
            this.Controls.Add(this.restartAppButton);
            this.Controls.Add(this.packageNameTextBox);
            this.Controls.Add(this.installButton);
            this.Controls.Add(this.selectApkButton);
            this.Controls.Add(this.apkPathTextBox);
            this.Controls.Add(this.connectionStatusLabel);
            this.Controls.Add(this.refreshDevicesButton);
            this.Controls.Add(this.deviceComboBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "AndroidIntelliTool";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
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
        private System.Windows.Forms.ToolStripMenuItem helpMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button screenMirrorButton;
        private System.Windows.Forms.Button screenRecordButton;
        private System.Windows.Forms.Button forceStopAppButton;
        private System.Windows.Forms.Button fileExplorerButton;
        private System.Windows.Forms.ToolStripMenuItem crashLogAnalyzerToolStripMenuItem;

    }
}
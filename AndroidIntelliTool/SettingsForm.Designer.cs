namespace AndroidIntelliTool
{
    partial class SettingsForm
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
            this.labelAdb = new System.Windows.Forms.Label();
            this.textAdbPath = new System.Windows.Forms.TextBox();
            this.btnBrowseAdb = new System.Windows.Forms.Button();
            this.labelAapt2 = new System.Windows.Forms.Label();
            this.textAapt2Path = new System.Windows.Forms.TextBox();
            this.btnBrowseAapt2 = new System.Windows.Forms.Button();
            this.labelBundleTool = new System.Windows.Forms.Label();
            this.textBundleToolPath = new System.Windows.Forms.TextBox();
            this.btnBrowseBundleTool = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.linkAdb = new System.Windows.Forms.LinkLabel();
            this.linkAapt2 = new System.Windows.Forms.LinkLabel();
            this.linkBundletool = new System.Windows.Forms.LinkLabel();
            this.labelScrcpy = new System.Windows.Forms.Label();
            this.textScrcpyPath = new System.Windows.Forms.TextBox();
            this.btnBrowseScrcpy = new System.Windows.Forms.Button();
            this.linkScrcpy = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // labelAdb
            // 
            this.labelAdb.AutoSize = true;
            this.labelAdb.Location = new System.Drawing.Point(13, 13);
            this.labelAdb.Name = "labelAdb";
            this.labelAdb.Size = new System.Drawing.Size(56, 13);
            this.labelAdb.TabIndex = 0;
            this.labelAdb.Text = "ADB Path:";
            // 
            // textAdbPath
            // 
            this.textAdbPath.Location = new System.Drawing.Point(16, 30);
            this.textAdbPath.Name = "textAdbPath";
            this.textAdbPath.Size = new System.Drawing.Size(375, 20);
            this.textAdbPath.TabIndex = 1;
            // 
            // btnBrowseAdb
            // 
            this.btnBrowseAdb.Location = new System.Drawing.Point(397, 28);
            this.btnBrowseAdb.Name = "btnBrowseAdb";
            this.btnBrowseAdb.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseAdb.TabIndex = 2;
            this.btnBrowseAdb.Text = "Browse...";
            this.btnBrowseAdb.UseVisualStyleBackColor = true;
            // 
            // labelAapt2
            // 
            this.labelAapt2.AutoSize = true;
            this.labelAapt2.Location = new System.Drawing.Point(13, 60);
            this.labelAapt2.Name = "labelAapt2";
            this.labelAapt2.Size = new System.Drawing.Size(68, 13);
            this.labelAapt2.TabIndex = 3;
            this.labelAapt2.Text = "AAPT2 Path:";
            // 
            // textAapt2Path
            // 
            this.textAapt2Path.Location = new System.Drawing.Point(16, 77);
            this.textAapt2Path.Name = "textAapt2Path";
            this.textAapt2Path.Size = new System.Drawing.Size(375, 20);
            this.textAapt2Path.TabIndex = 4;
            // 
            // btnBrowseAapt2
            // 
            this.btnBrowseAapt2.Location = new System.Drawing.Point(397, 75);
            this.btnBrowseAapt2.Name = "btnBrowseAapt2";
            this.btnBrowseAapt2.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseAapt2.TabIndex = 5;
            this.btnBrowseAapt2.Text = "Browse...";
            this.btnBrowseAapt2.UseVisualStyleBackColor = true;
            // 
            // labelBundleTool
            // 
            this.labelBundleTool.AutoSize = true;
            this.labelBundleTool.Location = new System.Drawing.Point(13, 107);
            this.labelBundleTool.Name = "labelBundleTool";
            this.labelBundleTool.Size = new System.Drawing.Size(92, 13);
            this.labelBundleTool.TabIndex = 6;
            this.labelBundleTool.Text = "BundleTool Path:";
            // 
            // textBundleToolPath
            // 
            this.textBundleToolPath.Location = new System.Drawing.Point(16, 124);
            this.textBundleToolPath.Name = "textBundleToolPath";
            this.textBundleToolPath.Size = new System.Drawing.Size(375, 20);
            this.textBundleToolPath.TabIndex = 7;
            // 
            // btnBrowseBundleTool
            // 
            this.btnBrowseBundleTool.Location = new System.Drawing.Point(397, 122);
            this.btnBrowseBundleTool.Name = "btnBrowseBundleTool";
            this.btnBrowseBundleTool.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseBundleTool.TabIndex = 8;
            this.btnBrowseBundleTool.Text = "Browse...";
            this.btnBrowseBundleTool.UseVisualStyleBackColor = true;
            // 
            // labelScrcpy
            // 
            this.labelScrcpy.AutoSize = true;
            this.labelScrcpy.Location = new System.Drawing.Point(13, 154);
            this.labelScrcpy.Name = "labelScrcpy";
            this.labelScrcpy.Size = new System.Drawing.Size(68, 13);
            this.labelScrcpy.TabIndex = 14;
            this.labelScrcpy.Text = "Scrcpy Path:";
            // 
            // textScrcpyPath
            // 
            this.textScrcpyPath.Location = new System.Drawing.Point(16, 171);
            this.textScrcpyPath.Name = "textScrcpyPath";
            this.textScrcpyPath.Size = new System.Drawing.Size(375, 20);
            this.textScrcpyPath.TabIndex = 15;
            // 
            // btnBrowseScrcpy
            // 
            this.btnBrowseScrcpy.Location = new System.Drawing.Point(397, 169);
            this.btnBrowseScrcpy.Name = "btnBrowseScrcpy";
            this.btnBrowseScrcpy.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseScrcpy.TabIndex = 16;
            this.btnBrowseScrcpy.Text = "Browse...";
            this.btnBrowseScrcpy.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(316, 210);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(397, 210);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // linkAdb
            // 
            this.linkAdb.AutoSize = true;
            this.linkAdb.Location = new System.Drawing.Point(75, 13);
            this.linkAdb.Name = "linkAdb";
            this.linkAdb.Size = new System.Drawing.Size(59, 13);
            this.linkAdb.TabIndex = 11;
            this.linkAdb.TabStop = true;
            this.linkAdb.Text = "(Download)";
            // 
            // linkAapt2
            // 
            this.linkAapt2.AutoSize = true;
            this.linkAapt2.Location = new System.Drawing.Point(80, 60);
            this.linkAapt2.Name = "linkAapt2";
            this.linkAapt2.Size = new System.Drawing.Size(214, 13);
            this.linkAapt2.TabIndex = 12;
            this.linkAapt2.TabStop = true;
            this.linkAapt2.Text = "(Download - Part of Android Studio Build-Tools)";
            // 
            // linkBundletool
            // 
            this.linkBundletool.AutoSize = true;
            this.linkBundletool.Location = new System.Drawing.Point(102, 107);
            this.linkBundletool.Name = "linkBundletool";
            this.linkBundletool.Size = new System.Drawing.Size(59, 13);
            this.linkBundletool.TabIndex = 13;
            this.linkBundletool.TabStop = true;
            this.linkBundletool.Text = "(Download)";
            // 
            // linkScrcpy
            // 
            this.linkScrcpy.AutoSize = true;
            this.linkScrcpy.Location = new System.Drawing.Point(80, 154);
            this.linkScrcpy.Name = "linkScrcpy";
            this.linkScrcpy.Size = new System.Drawing.Size(59, 13);
            this.linkScrcpy.TabIndex = 17;
            this.linkScrcpy.TabStop = true;
            this.linkScrcpy.Text = "(Download)";
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(484, 245);
            this.Controls.Add(this.linkScrcpy);
            this.Controls.Add(this.btnBrowseScrcpy);
            this.Controls.Add(this.textScrcpyPath);
            this.Controls.Add(this.labelScrcpy);
            this.Controls.Add(this.linkBundletool);
            this.Controls.Add(this.linkAapt2);
            this.Controls.Add(this.linkAdb);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnBrowseBundleTool);
            this.Controls.Add(this.textBundleToolPath);
            this.Controls.Add(this.labelBundleTool);
            this.Controls.Add(this.btnBrowseAapt2);
            this.Controls.Add(this.textAapt2Path);
            this.Controls.Add(this.labelAapt2);
            this.Controls.Add(this.btnBrowseAdb);
            this.Controls.Add(this.textAdbPath);
            this.Controls.Add(this.labelAdb);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAdb;
        private System.Windows.Forms.TextBox textAdbPath;
        private System.Windows.Forms.Button btnBrowseAdb;
        private System.Windows.Forms.Label labelAapt2;
        private System.Windows.Forms.TextBox textAapt2Path;
        private System.Windows.Forms.Button btnBrowseAapt2;
        private System.Windows.Forms.Label labelBundleTool;
        private System.Windows.Forms.TextBox textBundleToolPath;
        private System.Windows.Forms.Button btnBrowseBundleTool;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.LinkLabel linkAdb;
        private System.Windows.Forms.LinkLabel linkAapt2;
        private System.Windows.Forms.LinkLabel linkBundletool;
        private System.Windows.Forms.Label labelScrcpy;
        private System.Windows.Forms.TextBox textScrcpyPath;
        private System.Windows.Forms.Button btnBrowseScrcpy;
        private System.Windows.Forms.LinkLabel linkScrcpy;
    }
}

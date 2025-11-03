namespace AndroidIntelliTool
{
    partial class CrashLogAnalyzerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.crashLogTextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.selectSoFilesButton = new System.Windows.Forms.Button();
            this.soFilesListBox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.analyzeButton = new System.Windows.Forms.Button();
            this.symbolizedOutputTextBox = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ndkInfoLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // crashLogTextBox
            // 
            this.crashLogTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.crashLogTextBox.Location = new System.Drawing.Point(12, 29);
            this.crashLogTextBox.Name = "crashLogTextBox";
            this.crashLogTextBox.Size = new System.Drawing.Size(760, 150);
            this.crashLogTextBox.TabIndex = 0;
            this.crashLogTextBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Paste Crash Log:";
            // 
            // selectSoFilesButton
            // 
            this.selectSoFilesButton.Location = new System.Drawing.Point(12, 185);
            this.selectSoFilesButton.Name = "selectSoFilesButton";
            this.selectSoFilesButton.Size = new System.Drawing.Size(120, 23);
            this.selectSoFilesButton.TabIndex = 2;
            this.selectSoFilesButton.Text = "Select .so Files";
            this.selectSoFilesButton.UseVisualStyleBackColor = true;
            this.selectSoFilesButton.Click += new System.EventHandler(this.selectSoFilesButton_Click);
            // 
            // soFilesListBox
            // 
            this.soFilesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.soFilesListBox.FormattingEnabled = true;
            this.soFilesListBox.Location = new System.Drawing.Point(138, 185);
            this.soFilesListBox.Name = "soFilesListBox";
            this.soFilesListBox.Size = new System.Drawing.Size(634, 69);
            this.soFilesListBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 211);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 4;
            // 
            // analyzeButton
            // 
            this.analyzeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.analyzeButton.Location = new System.Drawing.Point(652, 260);
            this.analyzeButton.Name = "analyzeButton";
            this.analyzeButton.Size = new System.Drawing.Size(120, 23);
            this.analyzeButton.TabIndex = 5;
            this.analyzeButton.Text = "Analyze";
            this.analyzeButton.UseVisualStyleBackColor = true;
            this.analyzeButton.Click += new System.EventHandler(this.analyzeButton_Click);
            // 
            // symbolizedOutputTextBox
            // 
            this.symbolizedOutputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.symbolizedOutputTextBox.Location = new System.Drawing.Point(12, 302);
            this.symbolizedOutputTextBox.Name = "symbolizedOutputTextBox";
            this.symbolizedOutputTextBox.ReadOnly = true;
            this.symbolizedOutputTextBox.Size = new System.Drawing.Size(760, 136);
            this.symbolizedOutputTextBox.TabIndex = 6;
            this.symbolizedOutputTextBox.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 286);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Symbolized Output:";
            // 
            // ndkInfoLabel
            // 
            this.ndkInfoLabel.AutoSize = true;
            this.ndkInfoLabel.Location = new System.Drawing.Point(12, 265);
            this.ndkInfoLabel.Name = "ndkInfoLabel";
            this.ndkInfoLabel.Size = new System.Drawing.Size(490, 13);
            this.ndkInfoLabel.TabIndex = 8;
            this.ndkInfoLabel.Text = "Note: Android NDK (ndk-stack or addr2line) must be installed and configured in system PATH.";
            // 
            // CrashLogAnalyzerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 450);
            this.Controls.Add(this.ndkInfoLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.symbolizedOutputTextBox);
            this.Controls.Add(this.analyzeButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.soFilesListBox);
            this.Controls.Add(this.selectSoFilesButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.crashLogTextBox);
            this.Name = "CrashLogAnalyzerForm";
            this.Text = "Crash Log Analyzer";
            this.AllowDrop = true; // Enable drag and drop for the form
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox crashLogTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button selectSoFilesButton;
        private System.Windows.Forms.ListBox soFilesListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button analyzeButton;
        private System.Windows.Forms.RichTextBox symbolizedOutputTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label ndkInfoLabel;
    }
}

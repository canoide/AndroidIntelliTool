namespace AndroidIntelliTool
{
    partial class LogcatForm
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
            this.logListView = new AndroidIntelliTool.DoubleBufferedListView();
            this.colTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTag = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tagFilterTextBox = new System.Windows.Forms.TextBox();
            this.applyFilterButton = new System.Windows.Forms.Button();
            this.clearLogButton = new System.Windows.Forms.Button();
            this.labelTag = new System.Windows.Forms.Label();
            this.labelPriority = new System.Windows.Forms.Label();
            this.priorityComboBox = new System.Windows.Forms.ComboBox();
            this.exportSelectedButton = new System.Windows.Forms.Button();
            this.messageFilterTextBox = new System.Windows.Forms.TextBox();
            this.labelMessage = new System.Windows.Forms.Label();
            this.exportAllButton = new System.Windows.Forms.Button();
            this.packageFilterTextBox = new System.Windows.Forms.TextBox();
            this.labelPackage = new System.Windows.Forms.Label();
            this.pidFilterButton = new System.Windows.Forms.Button();
            this.autoScrollCheckBox = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.pidStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.copyButton = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // logListView
            // 
            this.logListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTime,
            this.colTag,
            this.colMessage});
            this.logListView.FullRowSelect = true;
            this.logListView.HideSelection = false;
            this.logListView.Location = new System.Drawing.Point(12, 91);
            this.logListView.Name = "logListView";
            this.logListView.Size = new System.Drawing.Size(760, 337);
            this.logListView.TabIndex = 0;
            this.logListView.UseCompatibleStateImageBehavior = false;
            this.logListView.View = System.Windows.Forms.View.Details;
            // 
            // colTime
            // 
            this.colTime.Text = "Time";
            this.colTime.Width = 120;
            // 
            // colTag
            // 
            this.colTag.Text = "Tag";
            this.colTag.Width = 150;
            // 
            // colMessage
            // 
            this.colMessage.Text = "Message";
            this.colMessage.Width = 450;
            // 
            // tagFilterTextBox
            // 
            this.tagFilterTextBox.Location = new System.Drawing.Point(41, 38);
            this.tagFilterTextBox.Name = "tagFilterTextBox";
            this.tagFilterTextBox.Size = new System.Drawing.Size(153, 20);
            this.tagFilterTextBox.TabIndex = 1;
            // 
            // applyFilterButton
            // 
            this.applyFilterButton.Location = new System.Drawing.Point(356, 38);
            this.applyFilterButton.Name = "applyFilterButton";
            this.applyFilterButton.Size = new System.Drawing.Size(103, 23);
            this.applyFilterButton.TabIndex = 2;
            this.applyFilterButton.Text = "Apply Filters";
            this.applyFilterButton.UseVisualStyleBackColor = true;
            // 
            // clearLogButton
            // 
            this.clearLogButton.Location = new System.Drawing.Point(518, 49);
            this.clearLogButton.Name = "clearLogButton";
            this.clearLogButton.Size = new System.Drawing.Size(75, 23);
            this.clearLogButton.TabIndex = 3;
            this.clearLogButton.Text = "Clear";
            this.clearLogButton.UseVisualStyleBackColor = true;
            // 
            // labelTag
            // 
            this.labelTag.AutoSize = true;
            this.labelTag.Location = new System.Drawing.Point(9, 41);
            this.labelTag.Name = "labelTag";
            this.labelTag.Size = new System.Drawing.Size(29, 13);
            this.labelTag.TabIndex = 4;
            this.labelTag.Text = "Tag:";
            // 
            // labelPriority
            // 
            this.labelPriority.AutoSize = true;
            this.labelPriority.Location = new System.Drawing.Point(200, 41);
            this.labelPriority.Name = "labelPriority";
            this.labelPriority.Size = new System.Drawing.Size(41, 13);
            this.labelPriority.TabIndex = 5;
            this.labelPriority.Text = "Priority:";
            // 
            // priorityComboBox
            // 
            this.priorityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.priorityComboBox.FormattingEnabled = true;
            this.priorityComboBox.Items.AddRange(new object[] {
            "Verbose",
            "Debug",
            "Info",
            "Warning",
            "Error",
            "Fatal",
            "Silent"});
            this.priorityComboBox.Location = new System.Drawing.Point(247, 38);
            this.priorityComboBox.Name = "priorityComboBox";
            this.priorityComboBox.Size = new System.Drawing.Size(103, 21);
            this.priorityComboBox.TabIndex = 6;
            // 
            // exportSelectedButton
            // 
            this.exportSelectedButton.Location = new System.Drawing.Point(680, 36);
            this.exportSelectedButton.Name = "exportSelectedButton";
            this.exportSelectedButton.Size = new System.Drawing.Size(92, 23);
            this.exportSelectedButton.TabIndex = 7;
            this.exportSelectedButton.Text = "Export Selected";
            this.exportSelectedButton.UseVisualStyleBackColor = true;
            // 
            // messageFilterTextBox
            // 
            this.messageFilterTextBox.Location = new System.Drawing.Point(41, 65);
            this.messageFilterTextBox.Name = "messageFilterTextBox";
            this.messageFilterTextBox.Size = new System.Drawing.Size(309, 20);
            this.messageFilterTextBox.TabIndex = 8;
            // 
            // labelMessage
            // 
            this.labelMessage.AutoSize = true;
            this.labelMessage.Location = new System.Drawing.Point(9, 68);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(31, 13);
            this.labelMessage.TabIndex = 9;
            this.labelMessage.Text = "Msg:";
            // 
            // exportAllButton
            // 
            this.exportAllButton.Location = new System.Drawing.Point(680, 62);
            this.exportAllButton.Name = "exportAllButton";
            this.exportAllButton.Size = new System.Drawing.Size(92, 23);
            this.exportAllButton.TabIndex = 10;
            this.exportAllButton.Text = "Export All (Filt.)";
            this.exportAllButton.UseVisualStyleBackColor = true;
            // 
            // packageFilterTextBox
            // 
            this.packageFilterTextBox.Location = new System.Drawing.Point(92, 12);
            this.packageFilterTextBox.Name = "packageFilterTextBox";
            this.packageFilterTextBox.Size = new System.Drawing.Size(258, 20);
            this.packageFilterTextBox.TabIndex = 11;
            // 
            // labelPackage
            // 
            this.labelPackage.AutoSize = true;
            this.labelPackage.Location = new System.Drawing.Point(9, 15);
            this.labelPackage.Name = "labelPackage";
            this.labelPackage.Size = new System.Drawing.Size(80, 13);
            this.labelPackage.TabIndex = 12;
            this.labelPackage.Text = "Package Name:";
            // 
            // pidFilterButton
            // 
            this.pidFilterButton.Location = new System.Drawing.Point(356, 10);
            this.pidFilterButton.Name = "pidFilterButton";
            this.pidFilterButton.Size = new System.Drawing.Size(103, 23);
            this.pidFilterButton.TabIndex = 13;
            this.pidFilterButton.Text = "Filter by PID";
            this.pidFilterButton.UseVisualStyleBackColor = true;
            // 
            // autoScrollCheckBox
            // 
            this.autoScrollCheckBox.AutoSize = true;
            this.autoScrollCheckBox.Checked = true;
            this.autoScrollCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoScrollCheckBox.Location = new System.Drawing.Point(518, 23);
            this.autoScrollCheckBox.Name = "autoScrollCheckBox";
            this.autoScrollCheckBox.Size = new System.Drawing.Size(80, 17);
            this.autoScrollCheckBox.TabIndex = 14;
            this.autoScrollCheckBox.Text = "Auto-scroll";
            this.autoScrollCheckBox.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pidStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 15;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // pidStatusLabel
            // 
            this.pidStatusLabel.Name = "pidStatusLabel";
            this.pidStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // copyButton
            // 
            this.copyButton.Location = new System.Drawing.Point(680, 10);
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(92, 23);
            this.copyButton.TabIndex = 16;
            this.copyButton.Text = "Copy";
            this.copyButton.UseVisualStyleBackColor = true;
            // 
            // LogcatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 450);
            this.Controls.Add(this.copyButton);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pidFilterButton);
            this.Controls.Add(this.autoScrollCheckBox);
            this.Controls.Add(this.labelPackage);
            this.Controls.Add(this.packageFilterTextBox);
            this.Controls.Add(this.exportAllButton);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.messageFilterTextBox);
            this.Controls.Add(this.exportSelectedButton);
            this.Controls.Add(this.priorityComboBox);
            this.Controls.Add(this.labelPriority);
            this.Controls.Add(this.labelTag);
            this.Controls.Add(this.clearLogButton);
            this.Controls.Add(this.applyFilterButton);
            this.Controls.Add(this.tagFilterTextBox);
            this.Controls.Add(this.logListView);
            this.Name = "LogcatForm";
            this.Text = "Logcat Viewer";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DoubleBufferedListView logListView;
        private System.Windows.Forms.ColumnHeader colTime;
        private System.Windows.Forms.ColumnHeader colTag;
        private System.Windows.Forms.ColumnHeader colMessage;
        private System.Windows.Forms.TextBox tagFilterTextBox;
        private System.Windows.Forms.Button applyFilterButton;
        private System.Windows.Forms.Button clearLogButton;
        private System.Windows.Forms.Label labelTag;
        private System.Windows.Forms.Label labelPriority;
        private System.Windows.Forms.ComboBox priorityComboBox;
        private System.Windows.Forms.Button exportSelectedButton;
        private System.Windows.Forms.TextBox messageFilterTextBox;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.Button exportAllButton;
        private System.Windows.Forms.TextBox packageFilterTextBox;
        private System.Windows.Forms.Label labelPackage;
        private System.Windows.Forms.Button pidFilterButton;
        private System.Windows.Forms.CheckBox autoScrollCheckBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel pidStatusLabel;
        private System.Windows.Forms.Button copyButton;
    }
}

namespace AndroidIntelliTool
{
    partial class WirelessForm
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
            this.groupBoxSaved = new System.Windows.Forms.GroupBox();
            this.btnRemoveIp = new System.Windows.Forms.Button();
            this.btnConnectSaved = new System.Windows.Forms.Button();
            this.listSavedIps = new System.Windows.Forms.ListBox();
            this.groupBoxSetup = new System.Windows.Forms.GroupBox();
            this.lblSetupInstructions = new System.Windows.Forms.Label();
            this.btnStartWirelessSetup = new System.Windows.Forms.Button();
            this.comboUsbDevices = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxManual = new System.Windows.Forms.GroupBox();
            this.btnConnectManual = new System.Windows.Forms.Button();
            this.textIpAddress = new System.Windows.Forms.TextBox();
            this.groupBoxSaved.SuspendLayout();
            this.groupBoxSetup.SuspendLayout();
            this.groupBoxManual.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxSaved
            // 
            this.groupBoxSaved.Controls.Add(this.btnRemoveIp);
            this.groupBoxSaved.Controls.Add(this.btnConnectSaved);
            this.groupBoxSaved.Controls.Add(this.listSavedIps);
            this.groupBoxSaved.Location = new System.Drawing.Point(12, 12);
            this.groupBoxSaved.Name = "groupBoxSaved";
            this.groupBoxSaved.Size = new System.Drawing.Size(460, 145);
            this.groupBoxSaved.TabIndex = 0;
            this.groupBoxSaved.TabStop = false;
            this.groupBoxSaved.Text = "Saved Connections";
            // 
            // btnRemoveIp
            // 
            this.btnRemoveIp.Location = new System.Drawing.Point(349, 50);
            this.btnRemoveIp.Name = "btnRemoveIp";
            this.btnRemoveIp.Size = new System.Drawing.Size(105, 23);
            this.btnRemoveIp.TabIndex = 2;
            this.btnRemoveIp.Text = "Remove Selected";
            this.btnRemoveIp.UseVisualStyleBackColor = true;
            // 
            // btnConnectSaved
            // 
            this.btnConnectSaved.Location = new System.Drawing.Point(349, 20);
            this.btnConnectSaved.Name = "btnConnectSaved";
            this.btnConnectSaved.Size = new System.Drawing.Size(105, 23);
            this.btnConnectSaved.TabIndex = 1;
            this.btnConnectSaved.Text = "Connect to Selected";
            this.btnConnectSaved.UseVisualStyleBackColor = true;
            // 
            // listSavedIps
            // 
            this.listSavedIps.FormattingEnabled = true;
            this.listSavedIps.Location = new System.Drawing.Point(7, 20);
            this.listSavedIps.Name = "listSavedIps";
            this.listSavedIps.Size = new System.Drawing.Size(336, 108);
            this.listSavedIps.TabIndex = 0;
            // 
            // groupBoxSetup
            // 
            this.groupBoxSetup.Controls.Add(this.lblSetupInstructions);
            this.groupBoxSetup.Controls.Add(this.btnStartWirelessSetup);
            this.groupBoxSetup.Controls.Add(this.comboUsbDevices);
            this.groupBoxSetup.Controls.Add(this.label1);
            this.groupBoxSetup.Location = new System.Drawing.Point(12, 245);
            this.groupBoxSetup.Name = "groupBoxSetup";
            this.groupBoxSetup.Size = new System.Drawing.Size(460, 193);
            this.groupBoxSetup.TabIndex = 1;
            this.groupBoxSetup.TabStop = false;
            this.groupBoxSetup.Text = "Guided Setup (via USB)";
            // 
            // lblSetupInstructions
            // 
            this.lblSetupInstructions.Location = new System.Drawing.Point(10, 84);
            this.lblSetupInstructions.Name = "lblSetupInstructions";
            this.lblSetupInstructions.Size = new System.Drawing.Size(444, 97);
            this.lblSetupInstructions.TabIndex = 3;
            this.lblSetupInstructions.Text = "Instructions: 1. Connect a single device via USB. 2. Select it from the list above. 3. Click \"Start Setup\".";
            // 
            // btnStartWirelessSetup
            // 
            this.btnStartWirelessSetup.Location = new System.Drawing.Point(289, 45);
            this.btnStartWirelessSetup.Name = "btnStartWirelessSetup";
            this.btnStartWirelessSetup.Size = new System.Drawing.Size(165, 23);
            this.btnStartWirelessSetup.TabIndex = 2;
            this.btnStartWirelessSetup.Text = "Start Wireless Setup";
            this.btnStartWirelessSetup.UseVisualStyleBackColor = true;
            // 
            // comboUsbDevices
            // 
            this.comboUsbDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboUsbDevices.FormattingEnabled = true;
            this.comboUsbDevices.Location = new System.Drawing.Point(10, 47);
            this.comboUsbDevices.Name = "comboUsbDevices";
            this.comboUsbDevices.Size = new System.Drawing.Size(273, 21);
            this.comboUsbDevices.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select USB Device:";
            // 
            // groupBoxManual
            // 
            this.groupBoxManual.Controls.Add(this.btnConnectManual);
            this.groupBoxManual.Controls.Add(this.textIpAddress);
            this.groupBoxManual.Location = new System.Drawing.Point(12, 163);
            this.groupBoxManual.Name = "groupBoxManual";
            this.groupBoxManual.Size = new System.Drawing.Size(460, 76);
            this.groupBoxManual.TabIndex = 2;
            this.groupBoxManual.TabStop = false;
            this.groupBoxManual.Text = "Manual Connection";
            // 
            // btnConnectManual
            // 
            this.btnConnectManual.Location = new System.Drawing.Point(349, 30);
            this.btnConnectManual.Name = "btnConnectManual";
            this.btnConnectManual.Size = new System.Drawing.Size(105, 23);
            this.btnConnectManual.TabIndex = 1;
            this.btnConnectManual.Text = "Connect";
            this.btnConnectManual.UseVisualStyleBackColor = true;
            // 
            // textIpAddress
            // 
            this.textIpAddress.Location = new System.Drawing.Point(7, 32);
            this.textIpAddress.Name = "textIpAddress";
            this.textIpAddress.Size = new System.Drawing.Size(336, 20);
            this.textIpAddress.TabIndex = 0;
            // 
            // WirelessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 450);
            this.Controls.Add(this.groupBoxManual);
            this.Controls.Add(this.groupBoxSetup);
            this.Controls.Add(this.groupBoxSaved);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WirelessForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Wireless Connections";
            this.groupBoxSaved.ResumeLayout(false);
            this.groupBoxSetup.ResumeLayout(false);
            this.groupBoxSetup.PerformLayout();
            this.groupBoxManual.ResumeLayout(false);
            this.groupBoxManual.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxSaved;
        private System.Windows.Forms.Button btnRemoveIp;
        private System.Windows.Forms.Button btnConnectSaved;
        private System.Windows.Forms.ListBox listSavedIps;
        private System.Windows.Forms.GroupBox groupBoxSetup;
        private System.Windows.Forms.Label lblSetupInstructions;
        private System.Windows.Forms.Button btnStartWirelessSetup;
        private System.Windows.Forms.ComboBox comboUsbDevices;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxManual;
        private System.Windows.Forms.Button btnConnectManual;
        private System.Windows.Forms.TextBox textIpAddress;
    }
}

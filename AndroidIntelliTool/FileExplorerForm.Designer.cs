
namespace AndroidIntelliTool
{
    partial class FileExplorerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileExplorerForm));
            this.localUpButton = new System.Windows.Forms.Button();
            this.driveComboBox = new System.Windows.Forms.ComboBox();
            this.localPathTextBox = new System.Windows.Forms.TextBox();
            
            this.downloadButton = new System.Windows.Forms.Button();
            this.uploadButton = new System.Windows.Forms.Button();
            this.deleteDeviceFileButton = new System.Windows.Forms.Button();
            this.refreshExplorerButton = new System.Windows.Forms.Button();
            this.deviceUpButton = new System.Windows.Forms.Button();
            this.devicePathTextBox = new System.Windows.Forms.TextBox();
            this.deviceFileListView = new System.Windows.Forms.ListView();
            this.localFileListView = new System.Windows.Forms.ListView();
            this.fileExplorerImageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // localUpButton
            // 
            this.localUpButton.Location = new System.Drawing.Point(12, 12);
            this.localUpButton.Name = "localUpButton";
            this.localUpButton.Size = new System.Drawing.Size(24, 22);
            this.localUpButton.Text = "^";
            // 
            // driveComboBox
            // 
            this.driveComboBox.FormattingEnabled = true;
            this.driveComboBox.Location = new System.Drawing.Point(42, 12);
            this.driveComboBox.Name = "driveComboBox";
            this.driveComboBox.Size = new System.Drawing.Size(53, 21);
            // 
            // localPathTextBox
            // 
            this.localPathTextBox.Location = new System.Drawing.Point(101, 13);
            this.localPathTextBox.Name = "localPathTextBox";
            this.localPathTextBox.Size = new System.Drawing.Size(171, 20);
            // 
            // localFileListView
            // 
            this.localFileListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            new System.Windows.Forms.ColumnHeader { Text = "Name", Width = 240 }
            });
            this.localFileListView.Location = new System.Drawing.Point(12, 39);
            this.localFileListView.Name = "localFileListView";
            this.localFileListView.Size = new System.Drawing.Size(260, 324);
            this.localFileListView.SmallImageList = this.fileExplorerImageList;
            this.localFileListView.UseCompatibleStateImageBehavior = false;
            this.localFileListView.View = System.Windows.Forms.View.Details;
            // 
            // deviceUpButton
            // 
            this.deviceUpButton.Location = new System.Drawing.Point(291, 12);
            this.deviceUpButton.Name = "deviceUpButton";
            this.deviceUpButton.Size = new System.Drawing.Size(24, 22);
            this.deviceUpButton.Text = "^";
            // 
            // devicePathTextBox
            // 
            this.devicePathTextBox.Location = new System.Drawing.Point(321, 13);
            this.devicePathTextBox.Name = "devicePathTextBox";
            this.devicePathTextBox.ReadOnly = true;
            this.devicePathTextBox.Size = new System.Drawing.Size(230, 20);
            // 
            // deviceFileListView
            // 
            this.deviceFileListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            new System.Windows.Forms.ColumnHeader { Text = "Name", Width = 240 }
            });
            this.deviceFileListView.Location = new System.Drawing.Point(291, 39);
            this.deviceFileListView.Name = "deviceFileListView";
            this.deviceFileListView.Size = new System.Drawing.Size(260, 324);
            this.deviceFileListView.SmallImageList = this.fileExplorerImageList;
            this.deviceFileListView.UseCompatibleStateImageBehavior = false;
            this.deviceFileListView.View = System.Windows.Forms.View.Details;
            // 
            // uploadButton
            // 
            this.uploadButton.Location = new System.Drawing.Point(206, 369);
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.Size = new System.Drawing.Size(66, 23);
            this.uploadButton.TabIndex = 0;
            this.uploadButton.Text = "Upload";
            // 
            // downloadButton
            // 
            this.downloadButton.Location = new System.Drawing.Point(291, 369);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(75, 23);
            this.downloadButton.TabIndex = 0;
            this.downloadButton.Text = "Download";
            // 
            // deleteDeviceFileButton
            // 
            this.deleteDeviceFileButton.Location = new System.Drawing.Point(372, 369);
            this.deleteDeviceFileButton.Name = "deleteDeviceFileButton";
            this.deleteDeviceFileButton.Size = new System.Drawing.Size(75, 23);
            this.deleteDeviceFileButton.Text = "Delete";
            // 
            // refreshExplorerButton
            // 
            this.refreshExplorerButton.Location = new System.Drawing.Point(453, 369);
            this.refreshExplorerButton.Name = "refreshExplorerButton";
            this.refreshExplorerButton.Size = new System.Drawing.Size(98, 23);
            this.refreshExplorerButton.TabIndex = 0;
            this.refreshExplorerButton.Text = "Refresh Lists";

            // 
            // fileExplorerImageList
            // 
            this.fileExplorerImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.fileExplorerImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.fileExplorerImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // FileExplorerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 404);
            this.Controls.Add(this.localUpButton);
            this.Controls.Add(this.driveComboBox);
            this.Controls.Add(this.localPathTextBox);
            this.Controls.Add(this.localFileListView);
            this.Controls.Add(this.deviceUpButton);
            this.Controls.Add(this.devicePathTextBox);
            this.Controls.Add(this.deviceFileListView);
            this.Controls.Add(this.uploadButton);
            this.Controls.Add(this.downloadButton);
            this.Controls.Add(this.deleteDeviceFileButton);
            this.Controls.Add(this.refreshExplorerButton);
            
            this.Name = "FileExplorerForm";
            this.Text = "File Explorer";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button localUpButton;
        private System.Windows.Forms.ComboBox driveComboBox;
        private System.Windows.Forms.TextBox localPathTextBox;
        private System.Windows.Forms.ListView localFileListView;
        private System.Windows.Forms.Button deviceUpButton;
        private System.Windows.Forms.TextBox devicePathTextBox;
        private System.Windows.Forms.ListView deviceFileListView;
        private System.Windows.Forms.Button uploadButton;
        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.Button deleteDeviceFileButton;
        private System.Windows.Forms.Button refreshExplorerButton;
        
        private System.Windows.Forms.ImageList fileExplorerImageList;
    }
}

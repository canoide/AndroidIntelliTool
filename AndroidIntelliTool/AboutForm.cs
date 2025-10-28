using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace AndroidIntelliTool
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            this.githubLinkLabel.LinkClicked += (s, e) => OpenUrl("https://github.com/canoide");
            this.emailLinkLabel.LinkClicked += (s, e) => OpenUrl("mailto:jorgecano017@gmail.com");
        }

        private void OpenUrl(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while trying to open the URL: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

using System.Diagnostics;
using System.Windows.Forms;

namespace AndroidIntelliTool
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            this.githubLinkLabel.LinkClicked += (s, e) => Process.Start("https://github.com/canoide");
            this.emailLinkLabel.LinkClicked += (s, e) => Process.Start("mailto:jorgecano017@gmail.com");
        }
    }
}

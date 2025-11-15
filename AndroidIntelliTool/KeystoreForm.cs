using System;
using System.IO;
using System.Windows.Forms;

namespace AndroidIntelliTool
{
    public partial class KeystoreForm : Form
    {
        public string KeystorePath { get; private set; }
        public string KeystorePassword { get; private set; }
        public string KeyAlias { get; private set; }
        public string KeyPassword { get; private set; }

        private TextBox textKeystorePath;
        private TextBox textKeystorePassword;
        private TextBox textKeyAlias;
        private TextBox textKeyPassword;
        private Button btnBrowse;
        private Button btnOk;
        private Button btnCancel;
        private CheckBox checkUseDebugKey;

        public KeystoreForm()
        {
            InitializeComponent();
            this.Text = "Keystore Information";
            this.Width = 500;
            this.Height = 300;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            int yPos = 20;

            // Debug keystore checkbox
            checkUseDebugKey = new CheckBox
            {
                Text = "Use Android Debug Keystore",
                Location = new System.Drawing.Point(20, yPos),
                Width = 250,
                Checked = true
            };
            checkUseDebugKey.CheckedChanged += CheckUseDebugKey_CheckedChanged;
            this.Controls.Add(checkUseDebugKey);
            yPos += 40;

            // Keystore path
            Label lblKeystorePath = new Label { Text = "Keystore Path:", Location = new System.Drawing.Point(20, yPos), Width = 120 };
            this.Controls.Add(lblKeystorePath);

            textKeystorePath = new TextBox { Location = new System.Drawing.Point(140, yPos), Width = 250, Enabled = false };
            this.Controls.Add(textKeystorePath);

            btnBrowse = new Button { Text = "Browse", Location = new System.Drawing.Point(400, yPos - 2), Width = 70, Enabled = false };
            btnBrowse.Click += BtnBrowse_Click;
            this.Controls.Add(btnBrowse);
            yPos += 35;

            // Keystore password
            Label lblKeystorePassword = new Label { Text = "Keystore Password:", Location = new System.Drawing.Point(20, yPos), Width = 120 };
            this.Controls.Add(lblKeystorePassword);

            textKeystorePassword = new TextBox { Location = new System.Drawing.Point(140, yPos), Width = 250, UseSystemPasswordChar = true, Enabled = false };
            this.Controls.Add(textKeystorePassword);
            yPos += 35;

            // Key alias
            Label lblKeyAlias = new Label { Text = "Key Alias:", Location = new System.Drawing.Point(20, yPos), Width = 120 };
            this.Controls.Add(lblKeyAlias);

            textKeyAlias = new TextBox { Location = new System.Drawing.Point(140, yPos), Width = 250, Enabled = false };
            this.Controls.Add(textKeyAlias);
            yPos += 35;

            // Key password
            Label lblKeyPassword = new Label { Text = "Key Password:", Location = new System.Drawing.Point(20, yPos), Width = 120 };
            this.Controls.Add(lblKeyPassword);

            textKeyPassword = new TextBox { Location = new System.Drawing.Point(140, yPos), Width = 250, UseSystemPasswordChar = true, Enabled = false };
            this.Controls.Add(textKeyPassword);
            yPos += 50;

            // Buttons
            btnOk = new Button { Text = "OK", Location = new System.Drawing.Point(310, yPos), Width = 80, DialogResult = DialogResult.OK };
            btnOk.Click += BtnOk_Click;
            this.Controls.Add(btnOk);

            btnCancel = new Button { Text = "Cancel", Location = new System.Drawing.Point(400, yPos), Width = 80, DialogResult = DialogResult.Cancel };
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;

            // Set default debug keystore values
            SetDebugKeystoreDefaults();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);
        }

        private void CheckUseDebugKey_CheckedChanged(object sender, EventArgs e)
        {
            bool useCustom = !checkUseDebugKey.Checked;
            textKeystorePath.Enabled = useCustom;
            textKeystorePassword.Enabled = useCustom;
            textKeyAlias.Enabled = useCustom;
            textKeyPassword.Enabled = useCustom;
            btnBrowse.Enabled = useCustom;

            if (checkUseDebugKey.Checked)
            {
                SetDebugKeystoreDefaults();
            }
            else
            {
                textKeystorePath.Text = "";
                textKeystorePassword.Text = "";
                textKeyAlias.Text = "";
                textKeyPassword.Text = "";
            }
        }

        private void SetDebugKeystoreDefaults()
        {
            // Android debug keystore default location
            string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string debugKeystorePath = Path.Combine(userProfile, ".android", "debug.keystore");

            textKeystorePath.Text = debugKeystorePath;
            textKeystorePassword.Text = "android";
            textKeyAlias.Text = "androiddebugkey";
            textKeyPassword.Text = "android";
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Keystore files (*.keystore;*.jks)|*.keystore;*.jks|All files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    textKeystorePath.Text = ofd.FileName;
                }
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (checkUseDebugKey.Checked)
            {
                // Use debug keystore
                string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                KeystorePath = Path.Combine(userProfile, ".android", "debug.keystore");
                KeystorePassword = "android";
                KeyAlias = "androiddebugkey";
                KeyPassword = "android";
            }
            else
            {
                // Validate custom keystore
                if (string.IsNullOrWhiteSpace(textKeystorePath.Text))
                {
                    MessageBox.Show("Please specify a keystore path.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.DialogResult = DialogResult.None;
                    return;
                }

                if (!File.Exists(textKeystorePath.Text))
                {
                    MessageBox.Show("The specified keystore file does not exist.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.DialogResult = DialogResult.None;
                    return;
                }

                if (string.IsNullOrWhiteSpace(textKeyAlias.Text))
                {
                    MessageBox.Show("Please specify a key alias.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.DialogResult = DialogResult.None;
                    return;
                }

                KeystorePath = textKeystorePath.Text;
                KeystorePassword = textKeystorePassword.Text;
                KeyAlias = textKeyAlias.Text;
                KeyPassword = textKeyPassword.Text;
            }
        }
    }
}

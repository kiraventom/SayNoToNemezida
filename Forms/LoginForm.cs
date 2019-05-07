using System;
using System.Windows.Forms;

namespace SNTN
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            AppIdTextBox.Text = Properties.Settings.Default.AppId;
            if (!string.IsNullOrWhiteSpace(AppIdTextBox.Text))
            {
                TryLogin();
            }
            else
            {
                LoginButton.Enabled = true;
            }
        }

        ToolTip toolTip = new ToolTip();

        private bool GetTokenViaBrowser(Uri URL, out string token)
        {
            using (var browserForm = new BrowserForm(URL))
            {
                var dr = browserForm.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    token = browserForm.Token;
                    return true;
                }
                else
                {
                    token = string.Empty;
                    return false;
                }
            }
        }

        private void TryLogin()
        {
            string appId = AppIdTextBox.Text;
            var sb = new System.Text.StringBuilder();
            sb.Append(@"https://oauth.vk.com/authorize?client_id=");
            sb.Append(appId);
            sb.Append(@"&display=page&redirect_uri=https://oauth.vk.com/blank.html&scope=wall,groups,photos,offline&response_type=token&v=5.92");
            var ub = new UriBuilder(sb.ToString());
            string token = string.Empty;
            if (GetTokenViaBrowser(ub.Uri, out token))
            {
                Hide();
                Properties.Settings.Default.AppId = AppIdTextBox.Text;
                Properties.Settings.Default.Save();
                using (var mainForm = new MainForm(token))
                {
                    mainForm.ShowDialog();
                }
                Close();
            }
            else
            {
                AppIdTextBox.Enabled = true;
                LoginButton.Enabled = true;
            }
        }

        private void LoginButton_Click(object sender, System.EventArgs e)
        {
            AppIdTextBox.Enabled = false;
            LoginButton.Enabled = false;
            toolTip.RemoveAll();
            toolTip.IsBalloon = true;
            if (string.IsNullOrWhiteSpace(AppIdTextBox.Text))
            {
                toolTip.Show("Введите ID приложения", AppIdTextBox, AppIdTextBox.Width * 4 / 5, -AppIdTextBox.Height * 2, 2000);
            }
            else
            {
                TryLogin();
            }
        }

        private void LoginForm_Move(object sender, System.EventArgs e)
        {
            toolTip.RemoveAll();
        }
    }
}

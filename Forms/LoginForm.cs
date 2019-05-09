using System;
using System.Windows.Forms;

namespace SNTN
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            Icon = Properties.Resources.icon;
            AppIdTextBox.Text = Properties.Settings.Default.AppId;
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

        private bool TryLogin()
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
                    var dr = mainForm.ShowDialog();
                    bool isResetRequested = dr == DialogResult.Yes;
                    if (isResetRequested)
                    {
                        #region StackOverflow magic
                        System.Diagnostics.Process.Start("CMD.exe", "/C RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 2");
                        #endregion
                    }
                }
                return true;
            }
            else
            {
                AppIdTextBox.Enabled = true;
                LoginButton.Enabled = true;
                return false;
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
                if (TryLogin())
                {
                    Close();
                }
            }
        }

        private void LoginForm_Move(object sender, System.EventArgs e)
        {
            toolTip.RemoveAll();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(AppIdTextBox.Text))
            {
                Opacity = 0;
                if (TryLogin())
                {
                    Close();
                }
            }
            else
            {
                LoginButton.Enabled = true;
            }
        }
    }
}

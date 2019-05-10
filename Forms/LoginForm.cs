using System;
using System.Windows.Forms;

namespace SNTN
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        ToolTip _emptyFieldToolTip = new ToolTip()
        {
            IsBalloon = true
        };

        private string GetTokenViaBrowser(Uri _url)
        {
            string _token;
            using (var _browserForm = new BrowserForm(_url))
            {
                bool _isLoginSuccessful = _browserForm.ShowDialog() == DialogResult.OK;
                if (_isLoginSuccessful)
                {
                    _token = _browserForm.Token;
                }
                else
                {
                    _token = null;
                }
            }
            return _token;
        }

        private void CleanCookies()
        {
            #region StackOverflow magic

            System.Diagnostics.Process.Start("CMD.exe", "/C RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 2");

            #endregion
        }

        private void OpenMainForm(string _token)
        {
            using (var _mainForm = new MainForm(_token))
            {
                var _mainFormDialogResult = _mainForm.ShowDialog();
                bool _isResetRequested = _mainFormDialogResult == DialogResult.Yes;
                if (_isResetRequested)
                {
                    Properties.Settings.Default.Reset();
                    CleanCookies();
                }
            }
        }

        private bool TryLogin(string _appId, out string _token)
        {
            string _url = @"https://oauth.vk.com/authorize?client_id=" +
                          $"{_appId}" +
                          @"&display=page&redirect_uri=https://oauth.vk.com/blank.html&scope=wall,groups,photos,offline&response_type=token&v=5.92";
            var _uriBuilder = new UriBuilder(_url);
            _token = GetTokenViaBrowser(_uriBuilder.Uri);
            bool _isLoginSuccessful = !string.IsNullOrWhiteSpace(_token);
            if (_isLoginSuccessful)
            {
                Hide();
                Properties.Settings.Default.AppId = AppIdTextBox.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                AppIdTextBox.Enabled = true;
                LoginButton.Enabled = true;
            }
            return _isLoginSuccessful;
        }

        private void LoginButton_Click(object sender, System.EventArgs e)
        {
            AppIdTextBox.Enabled = false;
            LoginButton.Enabled = false;
            _emptyFieldToolTip.RemoveAll();

            if (string.IsNullOrWhiteSpace(AppIdTextBox.Text))
            {
                _emptyFieldToolTip.Show("Введите ID приложения", 
                                        AppIdTextBox, 
                                        AppIdTextBox.Width * 4 / 5, 
                                        -AppIdTextBox.Height * 2, 
                                        2000);
            }
            else
            {
                bool _isLoginSuccessful = TryLogin(AppIdTextBox.Text, out string _token);
                if (_isLoginSuccessful)
                {
                    OpenMainForm(_token);
                    Close();
                }
            }
        }

        private void LoginForm_Move(object sender, System.EventArgs e)
        {
            _emptyFieldToolTip.RemoveAll();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.icon;
            AppIdTextBox.Text = Properties.Settings.Default.AppId;

            if (!string.IsNullOrWhiteSpace(AppIdTextBox.Text))
            {
                Opacity = 0;
                bool _isLoginSuccessful = TryLogin(AppIdTextBox.Text, out string _token);
                if (_isLoginSuccessful)
                {
                    OpenMainForm(_token);
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

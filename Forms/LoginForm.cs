using System.Windows.Forms;

namespace SNTN
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        ToolTip toolTip = new ToolTip();

        private void LoginButton_Click(object sender, System.EventArgs e)
        {
            toolTip.RemoveAll();
            toolTip.IsBalloon = true;
            if (string.IsNullOrWhiteSpace(LoginTextBox.Text))
            {
                toolTip.Show("Введите логин", LoginTextBox, LoginTextBox.Width * 4 / 5, - LoginTextBox.Height * 2, 2000);
            }
            else if (string.IsNullOrWhiteSpace(PasswordTextBox.Text))
            {
                toolTip.Show("Введите пароль", PasswordTextBox, PasswordTextBox.Width * 4 / 5, -PasswordTextBox.Height * 2, 2000);
            }
            else
            {
                //login
            }
        }

        private void LoginForm_Move(object sender, System.EventArgs e)
        {
            toolTip.RemoveAll();
        }
    }
}

using System.Windows.Forms;
using System.Threading.Tasks;

namespace SNTN
{
    public partial class BrowserForm : Form
    {
        public BrowserForm(System.Uri _uri)
        {
            InitializeComponent();
            MainWebBrowser.Url = _uri;
        }

        public string Token { get; private set; } = null;

        private async void MainWebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string _currentUrl = MainWebBrowser.Url.ToString();
            if (_currentUrl.Contains(@"#access_token="))
            {
                Opacity = 0;
                DialogResult = DialogResult.OK;
                const int _tokenLength = 85;
                const char _charBeforeToken = '=';
                Token = _currentUrl.Substring(_currentUrl.IndexOf(_charBeforeToken) + 1, _tokenLength);
                var _finishedProgress = new System.Progress<bool>(x => Close());
                await Task.Factory.StartNew(() => CloseAfterTimeOut(_finishedProgress), 
                                            TaskCreationOptions.LongRunning);
            }
        }

        private Task CloseAfterTimeOut(System.IProgress<bool> _finishedProgress)
        {
            // we should wait a little bit to have the page shown, 
            // because otherwise it will be opened in the IE.
            // we're dong this in a separate thread because Thread.Sleep in UI thread locks it 
            // and page is not shown
            System.Threading.Thread.Sleep(100);
            _finishedProgress.Report(true);
            return Task.CompletedTask;
        }

        private void BrowserForm_Load(object sender, System.EventArgs e)
        {
            Icon = Properties.Resources.icon;
        }
    }
}

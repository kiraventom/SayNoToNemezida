using System.Windows.Forms;
using System.Threading.Tasks;

namespace SNTN
{
    public partial class BrowserForm : Form
    {
        public BrowserForm(System.Uri uri)
        {
            InitializeComponent();
            MainWebBrowser.Url = uri;
        }

        public string Token { get; private set; }

        private async void MainWebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string url = MainWebBrowser.Url.ToString();
            if (url.Contains(@"#access_token="))
            {
                Token = url.Substring(url.IndexOf('=') + 1, 85);
                var progress = new System.Progress<bool>(i => Close());
                await Task.Factory.StartNew(() => CloseAfterTimeOut(progress), TaskCreationOptions.LongRunning);
            }
        }

        private Task CloseAfterTimeOut(System.IProgress<bool> progress)
        {
            System.Threading.Thread.Sleep(100);
            DialogResult = DialogResult.OK;
            progress.Report(true);
            return Task.CompletedTask;
        }
    }
}

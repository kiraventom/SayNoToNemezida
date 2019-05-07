using Extensions;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SNTN
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            
            GroupIdTextBox.Text = Properties.Settings.Default.GroupId.ToString();
            TokenTextBox.Text = Properties.Settings.Default.Token;
            PathToPhotosTextBox.Text = Properties.Settings.Default.PhotosDirPath;

            OpenCalendarButton.Text = Constants.Dates.CorrectMinimumDateTime.ToString("dd/MM/yyyy");
        }

        private static DateTime Date { get; set; } = Constants.Dates.CorrectMinimumDateTime;

        private void SaveSettings(long ownerId, long groupId, string token, string path)
        {
            Properties.Settings.Default.OwnerId = ownerId;
            Properties.Settings.Default.GroupId = groupId;
            Properties.Settings.Default.Token = token;
            Properties.Settings.Default.PhotosDirPath = path;
            Properties.Settings.Default.Save();
        }

        private void SwitchControls(bool isWorking)
        {
            GroupIdTextBox.Enabled = !isWorking;
            TokenTextBox.Enabled = !isWorking;
            //MainButton.Text = isWorking ? "Отменить" : "Начать";
            MainButton.Enabled = !isWorking;
            ChoosePathButton.Enabled = !isWorking;
            OpenCalendarButton.Enabled = !isWorking;
        }

        private bool TryAuth(VkNet.VkApi api, string token)
        {
            bool isAuthorized = Core.VK.AuthViaToken(api, token);
            if (!isAuthorized)
            {
                MessageBox.Show(caption: "Ошибка авторизации",
                                text: "Данные некорректны",
                                buttons: MessageBoxButtons.OK);
            }
            return isAuthorized;
        }
            
        private bool IsThereEnoughPhotos(string path, int amount)
        {
            var di = new System.IO.DirectoryInfo(path);
            System.IO.FileInfo[] array = di.GetFiles();
            int count = 0;
            foreach (var fi in array)
            {
                if (fi.Extension == ".jpg" ||
                    fi.Extension == ".jpeg" ||
                    fi.Extension == ".png")
                {
                    ++count;
                }
            }

            bool isThereEnoughPhotos = count >= amount;
            if (!isThereEnoughPhotos)
            {
                MessageBox.Show("Недостаточно файлов в папке. " +
                                $"Необходимо минимум {amount} файлов" +
                                " формата jpg, jpeg и/или png");
            }
            return isThereEnoughPhotos;
        }

        private void AskToDelete(string path, int amount)
        {
            var dr = MessageBox.Show(caption: "Подтвердите удаление",
                                     text: "Картинки загружены в отложку. Удалить их из папки?",
                                     buttons: MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                var di = new System.IO.DirectoryInfo(path);
                var fi = di.GetFiles();
                for (int i = 0; i < amount; ++i)
                {
                    fi[i].Delete();
                }
            }
        }

        public static void ShowErrorMessage(Exception e)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            string path = System.IO.Path.GetDirectoryName(asm.Location) + 
                          $"{DateTime.Now.ToString("hh-mm--dd-MM")}.log";
            System.IO.File.CreateText(path);
            using (var sw = new System.IO.StreamWriter(path))
            {
                sw.WriteLine(e.StackTrace);
            };
            MessageBox.Show("Произошла ошибка!\r" +
                            $"Лог сохранён в {path}.\r" +
                            $"Программа попробует продолжить работу.");
        }
        
        private async void MainButton_Click(object sender, EventArgs e)
        {
            var barProgress = new Progress<int>(i => PostingProgressBar.Value = i);
            var statusProgress = new Progress<string>(i => StatusLabel.Text = i);
            VkNet.VkApi api = new VkNet.VkApi();
            long groupId = GroupIdTextBox.Text.ToInt();
            long ownerId = groupId * -1;
            string token = TokenTextBox.Text;
            string pathToPhotos = PathToPhotosTextBox.Text;
            GroupIdTextBox.Text = groupId.ToString();
            if (TryAuth(api, token))
            {
                SwitchControls(false);
                
                SaveSettings(ownerId, groupId, token, pathToPhotos);
                var curricular = Core.Curricular.GetCurricular(Date);
                int postsAmount = curricular.Length;
                PostingProgressBar.Maximum = postsAmount;
                if (IsThereEnoughPhotos(pathToPhotos, postsAmount))
                {
                    var finishedProgress = new Progress<bool>(i =>
                    {
                        if (i)
                        {
                            AskToDelete(pathToPhotos, postsAmount);
                            SwitchControls(true);
                        }
                    });
                    await Task.Factory.StartNew(() =>
                        Core.VK.AddPosts(api, pathToPhotos, curricular, Date, 
                                         barProgress, statusProgress, finishedProgress),
                        TaskCreationOptions.LongRunning);
                }
            }
        }

        private void ChoosePathButton_Click(object sender, EventArgs e)
        {
            if (FolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                PathToPhotosTextBox.Text = FolderBrowserDialog.SelectedPath;
            }
        }

        private void PathToPhotosTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(PathToPhotosTextBox.Text) &&
                !string.IsNullOrEmpty(GroupIdTextBox.Text) &&
                !string.IsNullOrEmpty(TokenTextBox.Text))
            {
                MainButton.Enabled = true;
            }
            else
            {
                MainButton.Enabled = false;
            }
        }

        private void OpenCalendarButton_Click(object sender, EventArgs e)
        {
            using (var calendarForm = new CalendarForm(Constants.Dates.CorrectMinimumDateTime))
            {
                var dr = calendarForm.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    OpenCalendarButton.Text = calendarForm.SelectedDate.ToString("dd/MM/yyyy");
                    Date = calendarForm.SelectedDate;
                }
            }
        }
    }    
}

using System;
using System.Windows.Forms;

namespace SNTN
{
    public partial class MainForm : Form
    {
        public MainForm(string token)
        {
            InitializeComponent();
            Icon = Properties.Resources.icon;
            api.Authorize(new VkNet.Model.ApiAuthParams
            {
                AccessToken = token
            });
            Groups = api.Groups.Get(new VkNet.Model.RequestParams.GroupsGetParams
            {
                Filter = VkNet.Enums.Filters.GroupsFilters.Moderator,
                Extended = true
            });
            foreach (var group in Groups)
            {
                GroupsComboBox.Items.Add(group.Name);
            }
            PathToPhotosTextBox.Text = Properties.Settings.Default.PhotosDirPath;
            OpenCalendarButton.Text = Constants.Dates.CorrectMinimumDateTime.ToString("dd/MM/yyyy");
        }

        private VkNet.Utils.VkCollection<VkNet.Model.Group> Groups { get; set; }
        VkNet.VkApi api = new VkNet.VkApi();
        private static DateTime Date { get; set; } = Constants.Dates.CorrectMinimumDateTime;

        private void SaveSettings(string path)
        {
            Properties.Settings.Default.PhotosDirPath = path;
            Properties.Settings.Default.Save();
        }

        private void SwitchControls(bool isWorking)
        {
            GroupsComboBox.Enabled = !isWorking;
            ChoosePathButton.Enabled = !isWorking;
            OpenCalendarButton.Enabled = !isWorking;
            MainButton.Enabled = true;
            MainButton.Text = isWorking ? "Отменить" : "Начать";
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
            if (amount > 0)
            {
                var dr = MessageBox.Show(caption: "Подтверждение действия",
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
        }

        public static void ShowErrorMessage(Exception e)
        {
            MessageBox.Show("Произошла ошибка!\r" +
                            $"{e.StackTrace}.\r" +
                            $"Нажмите OK и программа попробует продолжить работу.");
        }
        
        private bool IsWorking
        {
            get
            {
                return PostingProgressBar.Value != 0 && !string.IsNullOrWhiteSpace(StatusLabel.Text);
            }
        }

        System.Threading.CancellationTokenSource cts = new System.Threading.CancellationTokenSource();

        private async void MainButton_Click(object sender, EventArgs e)
        {
            if (IsWorking)
            {
                var dr = MessageBox.Show(caption: "Подтверждение",
                                         text: "Отменить загрузку?",
                                         buttons: MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    StatusLabel.Text = "Отменяем...";
                    MainButton.Enabled = false;
                    cts.Cancel();
                }
            }
            else
            {
                var barProgress = new Progress<int>(i => PostingProgressBar.Value = i);
                var statusProgress = new Progress<string>(i => StatusLabel.Text = i);
                long groupId = Groups[GroupsComboBox.SelectedIndex].Id;
                long ownerId = groupId * -1;
                string pathToPhotos = PathToPhotosTextBox.Text;
                SwitchControls(true);
                SaveSettings(pathToPhotos);
                var curricular = Core.Curricular.GetCurricular(Date);
                int postsAmount = curricular.Length;
                PostingProgressBar.Maximum = postsAmount;
                if (IsThereEnoughPhotos(pathToPhotos, postsAmount))
                {
                    var finishedProgress = new Progress<int>(i =>
                    {
                        AskToDelete(pathToPhotos, i);
                        SwitchControls(false);
                    });
                    var task = await System.Threading.Tasks.Task.Factory.StartNew(() =>
                        Core.VK.AddPosts(api, pathToPhotos, curricular, Date, groupId,
                                         barProgress, statusProgress, finishedProgress, cts.Token),
                        System.Threading.Tasks.TaskCreationOptions.LongRunning);
                }
                else
                {
                    SwitchControls(false);
                }
            }
        }

        private void ChoosePathButton_Click(object sender, EventArgs e)
        {
            if (FolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                PathToPhotosTextBox.Text = FolderBrowserDialog.SelectedPath;
                if (GroupsComboBox.SelectedIndex != -1)
                {
                    MainButton.Enabled = true;
                }
                else
                {
                    MainButton.Enabled = false;
                }
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

        private void GroupsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(PathToPhotosTextBox.Text) &&
                GroupsComboBox.SelectedIndex != -1)
            { 
                MainButton.Enabled = true;
            }
            else
            {
                MainButton.Enabled = false;
            }
        }

        private bool IsResetRequired { get; set; } = false;

        private void ResetAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dr = MessageBox.Show(caption: "Подтверждение действия",
                                     text: "Вы уверены, что хотите выйти из аккаунта?",
                                     buttons: MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                IsResetRequired = true;
                Properties.Settings.Default.Reset();
                Close();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsResetRequired == false)
            {
                DialogResult = DialogResult.No;
            }
            else
            {
                DialogResult = DialogResult.Yes;
            }
        }
    }    
}

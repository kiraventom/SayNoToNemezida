using System;
using System.Windows.Forms;

namespace SNTN
{
    public partial class MainForm : Form
    {
        private VkNet.VkApi _API { get; set} = new VkNet.VkApi();
        private VkNet.Utils.VkCollection<VkNet.Model.Group> _Groups { get; set; }
        private DateTime _Date { get; set; } = Constants.Dates.CorrectMinimumDateTime;
        private bool _IsWorking { get; set; }
        private System.Threading.CancellationTokenSource _CancellationTokenSource { get; set; } =
            new System.Threading.CancellationTokenSource();
        private bool _IsResetRequested { get; set; } = false;

        public MainForm(string _token)
        {
            InitializeComponent();

            _API.Authorize(new VkNet.Model.ApiAuthParams
            {
                AccessToken = _token
            });
        }

        private void SaveSettings(string _path)
        {
            Properties.Settings.Default.PhotosDirPath = _path;
            Properties.Settings.Default.Save();
        }

        private void SetAppWorkingMode(bool _isWorking)
        {
            _IsWorking = _isWorking;
            GroupsComboBox.Enabled = !_isWorking;
            ChoosePathButton.Enabled = !_isWorking;
            OpenCalendarButton.Enabled = !_isWorking;
            MainButton.Enabled = true;
            MainButton.Text = _isWorking ? "Отменить" : "Начать";
        }
            
        private bool IsThereEnoughImages(string _path, int _requiredAmount)
        {
            var _directoryInfo = new System.IO.DirectoryInfo(_path);
            System.IO.FileInfo[] filesInfo = _directoryInfo.GetFiles();
            int _imagesCount = 0;
            foreach (var fileInfo in filesInfo)
            {
                if (fileInfo.Extension == ".jpg" ||
                    fileInfo.Extension == ".jpeg" ||
                    fileInfo.Extension == ".png")
                {
                    ++_imagesCount;
                }
            }
        
            bool isThereEnoughImages = _imagesCount >= _requiredAmount;
            return isThereEnoughImages;
        }

        private void ConfirmDeletion(string _path, int _amount)
        {
            if (_amount > 0)
            {
                var _userChoice = 
                    MessageBox.Show(caption: "Подтверждение действия",
                                    text: $"{_amount} изображений загружено. Удалить их?",
                                    buttons: MessageBoxButtons.YesNo);
                if (_userChoice == DialogResult.Yes)
                {
                    var _directoryInfo = new System.IO.DirectoryInfo(_path);
                    var _filesInfo = _directoryInfo.GetFiles();
                    for (int i = 0; i < _amount; ++i)
                    {
                        _filesInfo[i].Delete();
                    }
                }
            }
        }

        public static void ShowVKErrorMessage(Exception e)
        {
            MessageBox.Show("Произошла ошибка, свяазнная с ВК!\r\r" +
                            $"{e.StackTrace}.\r\r" +
                            "Нажмите OK и программа попробует продолжить работу.");
        }

        private void CancelWorking()
        {
            var _userChoice = MessageBox.Show(caption: "Подтверждение",
                                              text: "Отменить загрузку?",
                                              buttons: MessageBoxButtons.YesNo);
            if (_userChoice == DialogResult.Yes)
            {
                StatusLabel.Text = "Отменяем...";
                MainButton.Enabled = false;
                _CancellationTokenSource.Cancel();
            }
        }

        private async void StartWorking()
        {
            var _barProgress = new Progress<int>(x => PostingProgressBar.Value = x);
            var _statusProgress = new Progress<string>(x => StatusLabel.Text = x);
            long _groupId = _Groups[GroupsComboBox.SelectedIndex].Id;
            long _ownerId = _groupId * -1;
            string _pathToPhotos = PathToPhotosTextBox.Text;

            SetAppWorkingMode(true);
            SaveSettings(_pathToPhotos);

            var _curricular = Core.Curricular.GetCurricular(_Date);
            int _postsAmount = _curricular.Length;
            PostingProgressBar.Maximum = _postsAmount;

            if (IsThereEnoughImages(_pathToPhotos, _postsAmount))
            {
                var _finishedProgress = new Progress<int>(x =>
                {
                    ConfirmDeletion(_pathToPhotos, x);
                    SetAppWorkingMode(false);
                });

                await System.Threading.Tasks.Task.Factory.StartNew(
                    () => Core.VkManager.AddPosts(_API,
                                           _pathToPhotos,
                                           _curricular,
                                           _Date,
                                           _groupId,
                                           _barProgress,
                                           _statusProgress,
                                           _finishedProgress,
                                           _CancellationTokenSource.Token),
                    System.Threading.Tasks.TaskCreationOptions.LongRunning);
            }
            else
            {
                MessageBox.Show("Недостаточно изображений в папке. " +
                                $"Необходимо минимум {_postsAmount} файлов" +
                                " формата jpg, jpeg и/или png");
                SetAppWorkingMode(false);
            }
        }

        private void MainButton_Click(object sender, EventArgs e)
        {
            if (_IsWorking)
            {
                CancelWorking();
            }
            else
            {
                StartWorking();
            }
        }

        private void ChoosePathButton_Click(object sender, EventArgs e)
        {
            bool _isFolderChosen = FolderBrowserDialog.ShowDialog() == DialogResult.OK;
            if (_isFolderChosen)
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
            using (var _calendarForm = new CalendarForm(Constants.Dates.CorrectMinimumDateTime))
            {
                var _isDateSelected = _calendarForm.ShowDialog();
                if (_isDateSelected == DialogResult.OK)
                {
                    OpenCalendarButton.Text = _calendarForm.SelectedDate.ToString("dd/MM/yyyy");
                    _Date = _calendarForm.SelectedDate;
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

        private void ResetAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var _userChoice = MessageBox.Show(caption: "Подтверждение действия",
                                     text: "Вы уверены, что хотите выйти из аккаунта?",
                                     buttons: MessageBoxButtons.YesNo);
            if (_userChoice == DialogResult.Yes)
            {
                _IsResetRequested = true;
                Close();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_IsResetRequested)
            {
                DialogResult = DialogResult.Yes;
            }
            else
            {
                DialogResult = DialogResult.No;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.icon;

            _Groups = _API.Groups.Get(new VkNet.Model.RequestParams.GroupsGetParams
            {
                Filter = VkNet.Enums.Filters.GroupsFilters.Moderator,
                Extended = true
            });
            foreach (var _group in _Groups)
            {
                GroupsComboBox.Items.Add(_group.Name);
            }

            PathToPhotosTextBox.Text = Properties.Settings.Default.PhotosDirPath;
            OpenCalendarButton.Text = Constants.Dates.CorrectMinimumDateTime.ToString("dd/MM/yyyy");
        }
    }    
}

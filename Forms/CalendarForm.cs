using System;
using System.Windows.Forms;

namespace SNTN
{
    public partial class CalendarForm : Form
    {
        public CalendarForm(DateTime minimumDate)
        {
            InitializeComponent();
            MainMonthCalendar.MinDate = minimumDate;
        }

        public DateTime SelectedDate { get; private set; }

        private void MainMonthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            SelectedDate = new DateTime (MainMonthCalendar.SelectionStart.Year,
                                         MainMonthCalendar.SelectionStart.Month,
                                         MainMonthCalendar.SelectionStart.Day,
                                         DateTime.Now.Hour, 
                                         DateTime.Now.Minute,
                                         DateTime.Now.Second);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CalendarForm_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void MainMonthCalendar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }
    }
}

namespace SNTN
{
    partial class CalendarForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MainMonthCalendar = new System.Windows.Forms.MonthCalendar();
            this.SuspendLayout();
            // 
            // MainMonthCalendar
            // 
            this.MainMonthCalendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainMonthCalendar.Location = new System.Drawing.Point(0, 0);
            this.MainMonthCalendar.MaxDate = new System.DateTime(2030, 12, 31, 0, 0, 0, 0);
            this.MainMonthCalendar.MaxSelectionCount = 1;
            this.MainMonthCalendar.Name = "MainMonthCalendar";
            this.MainMonthCalendar.TabIndex = 0;
            this.MainMonthCalendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.MainMonthCalendar_DateSelected);
            this.MainMonthCalendar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainMonthCalendar_KeyDown);
            // 
            // CalendarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 161);
            this.Controls.Add(this.MainMonthCalendar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CalendarForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CalendarForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MonthCalendar MainMonthCalendar;
    }
}
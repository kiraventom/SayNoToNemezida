namespace SNTN
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.GroupLabel = new System.Windows.Forms.Label();
            this.MainButton = new System.Windows.Forms.Button();
            this.PathToPhotosLabel = new System.Windows.Forms.Label();
            this.PathToPhotosTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.PathToPhotosTextBox = new System.Windows.Forms.TextBox();
            this.ChoosePathButton = new System.Windows.Forms.Button();
            this.PostingProgressBar = new System.Windows.Forms.ProgressBar();
            this.OpenCalendarButton = new System.Windows.Forms.Button();
            this.StatusTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.GroupsComboBox = new System.Windows.Forms.ComboBox();
            this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ResetAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.MainTableLayoutPanel.SuspendLayout();
            this.PathToPhotosTableLayoutPanel.SuspendLayout();
            this.StatusTableLayoutPanel.SuspendLayout();
            this.MainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTableLayoutPanel
            // 
            this.MainTableLayoutPanel.ColumnCount = 1;
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel.Controls.Add(this.GroupLabel, 0, 1);
            this.MainTableLayoutPanel.Controls.Add(this.MainButton, 0, 8);
            this.MainTableLayoutPanel.Controls.Add(this.PathToPhotosLabel, 0, 3);
            this.MainTableLayoutPanel.Controls.Add(this.PathToPhotosTableLayoutPanel, 0, 4);
            this.MainTableLayoutPanel.Controls.Add(this.PostingProgressBar, 0, 6);
            this.MainTableLayoutPanel.Controls.Add(this.OpenCalendarButton, 0, 5);
            this.MainTableLayoutPanel.Controls.Add(this.StatusTableLayoutPanel, 0, 7);
            this.MainTableLayoutPanel.Controls.Add(this.GroupsComboBox, 0, 2);
            this.MainTableLayoutPanel.Controls.Add(this.MainMenuStrip, 0, 0);
            this.MainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.MainTableLayoutPanel.Name = "MainTableLayoutPanel";
            this.MainTableLayoutPanel.RowCount = 9;
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.MainTableLayoutPanel.Size = new System.Drawing.Size(239, 272);
            this.MainTableLayoutPanel.TabIndex = 0;
            // 
            // GroupLabel
            // 
            this.GroupLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.GroupLabel.AutoSize = true;
            this.GroupLabel.Location = new System.Drawing.Point(97, 31);
            this.GroupLabel.Name = "GroupLabel";
            this.GroupLabel.Size = new System.Drawing.Size(45, 13);
            this.GroupLabel.TabIndex = 0;
            this.GroupLabel.Text = "Группа:";
            // 
            // MainButton
            // 
            this.MainButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainButton.Enabled = false;
            this.MainButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainButton.ForeColor = System.Drawing.Color.Black;
            this.MainButton.Location = new System.Drawing.Point(3, 235);
            this.MainButton.Name = "MainButton";
            this.MainButton.Size = new System.Drawing.Size(233, 34);
            this.MainButton.TabIndex = 4;
            this.MainButton.Text = "Начать";
            this.MainButton.UseVisualStyleBackColor = true;
            this.MainButton.Click += new System.EventHandler(this.MainButton_Click);
            // 
            // PathToPhotosLabel
            // 
            this.PathToPhotosLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PathToPhotosLabel.AutoSize = true;
            this.PathToPhotosLabel.Location = new System.Drawing.Point(45, 81);
            this.PathToPhotosLabel.Name = "PathToPhotosLabel";
            this.PathToPhotosLabel.Size = new System.Drawing.Size(149, 13);
            this.PathToPhotosLabel.TabIndex = 5;
            this.PathToPhotosLabel.Text = "Путь к папке с картинками:";
            // 
            // PathToPhotosTableLayoutPanel
            // 
            this.PathToPhotosTableLayoutPanel.ColumnCount = 2;
            this.PathToPhotosTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PathToPhotosTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.PathToPhotosTableLayoutPanel.Controls.Add(this.PathToPhotosTextBox, 0, 0);
            this.PathToPhotosTableLayoutPanel.Controls.Add(this.ChoosePathButton, 1, 0);
            this.PathToPhotosTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PathToPhotosTableLayoutPanel.Location = new System.Drawing.Point(3, 103);
            this.PathToPhotosTableLayoutPanel.Name = "PathToPhotosTableLayoutPanel";
            this.PathToPhotosTableLayoutPanel.RowCount = 1;
            this.PathToPhotosTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.PathToPhotosTableLayoutPanel.Size = new System.Drawing.Size(233, 29);
            this.PathToPhotosTableLayoutPanel.TabIndex = 6;
            // 
            // PathToPhotosTextBox
            // 
            this.PathToPhotosTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PathToPhotosTextBox.Location = new System.Drawing.Point(3, 4);
            this.PathToPhotosTextBox.MaxLength = 90;
            this.PathToPhotosTextBox.Name = "PathToPhotosTextBox";
            this.PathToPhotosTextBox.ReadOnly = true;
            this.PathToPhotosTextBox.Size = new System.Drawing.Size(192, 20);
            this.PathToPhotosTextBox.TabIndex = 4;
            this.PathToPhotosTextBox.TextChanged += new System.EventHandler(this.PathToPhotosTextBox_TextChanged);
            // 
            // ChoosePathButton
            // 
            this.ChoosePathButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ChoosePathButton.AutoSize = true;
            this.ChoosePathButton.Location = new System.Drawing.Point(202, 3);
            this.ChoosePathButton.Name = "ChoosePathButton";
            this.ChoosePathButton.Size = new System.Drawing.Size(26, 23);
            this.ChoosePathButton.TabIndex = 5;
            this.ChoosePathButton.Text = "...";
            this.ChoosePathButton.UseVisualStyleBackColor = true;
            this.ChoosePathButton.Click += new System.EventHandler(this.ChoosePathButton_Click);
            // 
            // PostingProgressBar
            // 
            this.PostingProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PostingProgressBar.Location = new System.Drawing.Point(3, 173);
            this.PostingProgressBar.Maximum = 17;
            this.PostingProgressBar.Name = "PostingProgressBar";
            this.PostingProgressBar.Size = new System.Drawing.Size(233, 24);
            this.PostingProgressBar.Step = 1;
            this.PostingProgressBar.TabIndex = 8;
            // 
            // OpenCalendarButton
            // 
            this.OpenCalendarButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OpenCalendarButton.Location = new System.Drawing.Point(3, 138);
            this.OpenCalendarButton.Name = "OpenCalendarButton";
            this.OpenCalendarButton.Size = new System.Drawing.Size(233, 29);
            this.OpenCalendarButton.TabIndex = 9;
            this.OpenCalendarButton.Text = "*DATE HERE*";
            this.OpenCalendarButton.UseVisualStyleBackColor = true;
            this.OpenCalendarButton.Click += new System.EventHandler(this.OpenCalendarButton_Click);
            // 
            // StatusTableLayoutPanel
            // 
            this.StatusTableLayoutPanel.ColumnCount = 2;
            this.StatusTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 96.56652F));
            this.StatusTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.433476F));
            this.StatusTableLayoutPanel.Controls.Add(this.StatusLabel, 0, 0);
            this.StatusTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StatusTableLayoutPanel.Location = new System.Drawing.Point(3, 203);
            this.StatusTableLayoutPanel.Name = "StatusTableLayoutPanel";
            this.StatusTableLayoutPanel.RowCount = 1;
            this.StatusTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.StatusTableLayoutPanel.Size = new System.Drawing.Size(233, 26);
            this.StatusTableLayoutPanel.TabIndex = 10;
            // 
            // StatusLabel
            // 
            this.StatusLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(3, 6);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(0, 13);
            this.StatusLabel.TabIndex = 11;
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupsComboBox
            // 
            this.GroupsComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GroupsComboBox.FormattingEnabled = true;
            this.GroupsComboBox.Location = new System.Drawing.Point(3, 53);
            this.GroupsComboBox.MaxDropDownItems = 20;
            this.GroupsComboBox.Name = "GroupsComboBox";
            this.GroupsComboBox.Size = new System.Drawing.Size(233, 21);
            this.GroupsComboBox.TabIndex = 11;
            this.GroupsComboBox.SelectedIndexChanged += new System.EventHandler(this.GroupsComboBox_SelectedIndexChanged);
            // 
            // MainMenuStrip
            // 
            this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem});
            this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip.Name = "MainMenuStrip";
            this.MainMenuStrip.Size = new System.Drawing.Size(239, 24);
            this.MainMenuStrip.TabIndex = 12;
            this.MainMenuStrip.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ResetAccountToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.FileToolStripMenuItem.Text = "Файл";
            // 
            // ResetAccountToolStripMenuItem
            // 
            this.ResetAccountToolStripMenuItem.Name = "ResetAccountToolStripMenuItem";
            this.ResetAccountToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.ResetAccountToolStripMenuItem.Text = "Сбросить аккаунт";
            this.ResetAccountToolStripMenuItem.Click += new System.EventHandler(this.ResetAccountToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(239, 272);
            this.Controls.Add(this.MainTableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Say No To Nemezida";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.MainTableLayoutPanel.ResumeLayout(false);
            this.MainTableLayoutPanel.PerformLayout();
            this.PathToPhotosTableLayoutPanel.ResumeLayout(false);
            this.PathToPhotosTableLayoutPanel.PerformLayout();
            this.StatusTableLayoutPanel.ResumeLayout(false);
            this.StatusTableLayoutPanel.PerformLayout();
            this.MainMenuStrip.ResumeLayout(false);
            this.MainMenuStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainTableLayoutPanel;
        private System.Windows.Forms.Label GroupLabel;
        private System.Windows.Forms.Button MainButton;
        private System.Windows.Forms.Label PathToPhotosLabel;
        private System.Windows.Forms.TableLayoutPanel PathToPhotosTableLayoutPanel;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog;
        private System.Windows.Forms.TextBox PathToPhotosTextBox;
        private System.Windows.Forms.Button ChoosePathButton;
        private System.Windows.Forms.ProgressBar PostingProgressBar;
        private System.Windows.Forms.Button OpenCalendarButton;
        private System.Windows.Forms.TableLayoutPanel StatusTableLayoutPanel;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.ComboBox GroupsComboBox;
        private System.Windows.Forms.MenuStrip MainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ResetAccountToolStripMenuItem;
    }
}


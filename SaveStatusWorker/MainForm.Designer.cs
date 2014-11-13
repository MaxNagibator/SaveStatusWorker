namespace SaveStatusWorker
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.uiTimerIntervalTextBox = new System.Windows.Forms.TextBox();
            this.uiStartButton = new System.Windows.Forms.Button();
            this.uiStopButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.uiShowStatusesButton = new System.Windows.Forms.Button();
            this.uiOutTextBox = new System.Windows.Forms.TextBox();
            this.uiMessageLabel = new System.Windows.Forms.Label();
            this.uiGetFriendsButton = new System.Windows.Forms.Button();
            this.uiFriendsCombobox = new System.Windows.Forms.ComboBox();
            this.uiAlbumComboBox = new System.Windows.Forms.ComboBox();
            this.uiGetAlbumsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // uiTimerIntervalTextBox
            // 
            this.uiTimerIntervalTextBox.Location = new System.Drawing.Point(12, 12);
            this.uiTimerIntervalTextBox.Name = "uiTimerIntervalTextBox";
            this.uiTimerIntervalTextBox.Size = new System.Drawing.Size(51, 20);
            this.uiTimerIntervalTextBox.TabIndex = 0;
            this.uiTimerIntervalTextBox.Text = "300";
            // 
            // uiStartButton
            // 
            this.uiStartButton.Location = new System.Drawing.Point(69, 9);
            this.uiStartButton.Name = "uiStartButton";
            this.uiStartButton.Size = new System.Drawing.Size(75, 23);
            this.uiStartButton.TabIndex = 1;
            this.uiStartButton.Text = "start";
            this.uiStartButton.UseVisualStyleBackColor = true;
            this.uiStartButton.Click += new System.EventHandler(this.uiStartButton_Click);
            // 
            // uiStopButton
            // 
            this.uiStopButton.Location = new System.Drawing.Point(150, 9);
            this.uiStopButton.Name = "uiStopButton";
            this.uiStopButton.Size = new System.Drawing.Size(75, 23);
            this.uiStopButton.TabIndex = 2;
            this.uiStopButton.Text = "stop";
            this.uiStopButton.UseVisualStyleBackColor = true;
            this.uiStopButton.Click += new System.EventHandler(this.uiStopButton_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // uiShowStatusesButton
            // 
            this.uiShowStatusesButton.Location = new System.Drawing.Point(12, 51);
            this.uiShowStatusesButton.Name = "uiShowStatusesButton";
            this.uiShowStatusesButton.Size = new System.Drawing.Size(213, 23);
            this.uiShowStatusesButton.TabIndex = 3;
            this.uiShowStatusesButton.Text = "Показать последние статусы";
            this.uiShowStatusesButton.UseVisualStyleBackColor = true;
            this.uiShowStatusesButton.Click += new System.EventHandler(this.uiShowStatusesButton_Click);
            // 
            // uiOutTextBox
            // 
            this.uiOutTextBox.Location = new System.Drawing.Point(12, 80);
            this.uiOutTextBox.Multiline = true;
            this.uiOutTextBox.Name = "uiOutTextBox";
            this.uiOutTextBox.Size = new System.Drawing.Size(347, 200);
            this.uiOutTextBox.TabIndex = 4;
            // 
            // uiMessageLabel
            // 
            this.uiMessageLabel.AutoSize = true;
            this.uiMessageLabel.Location = new System.Drawing.Point(12, 35);
            this.uiMessageLabel.Name = "uiMessageLabel";
            this.uiMessageLabel.Size = new System.Drawing.Size(313, 13);
            this.uiMessageLabel.TabIndex = 5;
            this.uiMessageLabel.Text = "Жми старт и начнется получение статусов раз в 300 секунд";
            // 
            // uiGetFriendsButton
            // 
            this.uiGetFriendsButton.Location = new System.Drawing.Point(364, 9);
            this.uiGetFriendsButton.Name = "uiGetFriendsButton";
            this.uiGetFriendsButton.Size = new System.Drawing.Size(84, 23);
            this.uiGetFriendsButton.TabIndex = 6;
            this.uiGetFriendsButton.Text = "Get friends";
            this.uiGetFriendsButton.UseVisualStyleBackColor = true;
            this.uiGetFriendsButton.Click += new System.EventHandler(this.uiGetFriendsButton_Click);
            // 
            // uiFriendsCombobox
            // 
            this.uiFriendsCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiFriendsCombobox.FormattingEnabled = true;
            this.uiFriendsCombobox.Location = new System.Drawing.Point(454, 11);
            this.uiFriendsCombobox.Name = "uiFriendsCombobox";
            this.uiFriendsCombobox.Size = new System.Drawing.Size(402, 21);
            this.uiFriendsCombobox.TabIndex = 7;
            // 
            // uiAlbumComboBox
            // 
            this.uiAlbumComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiAlbumComboBox.FormattingEnabled = true;
            this.uiAlbumComboBox.Location = new System.Drawing.Point(454, 40);
            this.uiAlbumComboBox.Name = "uiAlbumComboBox";
            this.uiAlbumComboBox.Size = new System.Drawing.Size(402, 21);
            this.uiAlbumComboBox.TabIndex = 9;
            // 
            // uiGetAlbumsButton
            // 
            this.uiGetAlbumsButton.Location = new System.Drawing.Point(364, 38);
            this.uiGetAlbumsButton.Name = "uiGetAlbumsButton";
            this.uiGetAlbumsButton.Size = new System.Drawing.Size(84, 23);
            this.uiGetAlbumsButton.TabIndex = 8;
            this.uiGetAlbumsButton.Text = "Get albums";
            this.uiGetAlbumsButton.UseVisualStyleBackColor = true;
            this.uiGetAlbumsButton.Click += new System.EventHandler(this.uiGetAlbumsButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 297);
            this.Controls.Add(this.uiAlbumComboBox);
            this.Controls.Add(this.uiGetAlbumsButton);
            this.Controls.Add(this.uiFriendsCombobox);
            this.Controls.Add(this.uiGetFriendsButton);
            this.Controls.Add(this.uiMessageLabel);
            this.Controls.Add(this.uiOutTextBox);
            this.Controls.Add(this.uiShowStatusesButton);
            this.Controls.Add(this.uiStopButton);
            this.Controls.Add(this.uiStartButton);
            this.Controls.Add(this.uiTimerIntervalTextBox);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox uiTimerIntervalTextBox;
        private System.Windows.Forms.Button uiStartButton;
        private System.Windows.Forms.Button uiStopButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button uiShowStatusesButton;
        private System.Windows.Forms.TextBox uiOutTextBox;
        private System.Windows.Forms.Label uiMessageLabel;
        private System.Windows.Forms.Button uiGetFriendsButton;
        private System.Windows.Forms.ComboBox uiFriendsCombobox;
        private System.Windows.Forms.ComboBox uiAlbumComboBox;
        private System.Windows.Forms.Button uiGetAlbumsButton;
    }
}


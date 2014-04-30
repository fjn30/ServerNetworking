namespace ServerExample
{
    partial class ServerControlForm
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
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.buttonExit = new System.Windows.Forms.Button();
            this.listBoxMessages = new System.Windows.Forms.ListBox();
            this.listBoxClientMessages = new System.Windows.Forms.ListBox();
            this.comboBoxConnectedUsers = new System.Windows.Forms.ComboBox();
            this.buttonChangeUser = new System.Windows.Forms.Button();
            this.buttonDisconnectUser = new System.Windows.Forms.Button();
            this.textBoxStringMessage = new System.Windows.Forms.TextBox();
            this.buttonSendString = new System.Windows.Forms.Button();
            this.textBoxSendToAllString = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.AccessibleName = "";
            this.buttonStart.Location = new System.Drawing.Point(13, 13);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(108, 23);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Start Server";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.AccessibleName = "";
            this.buttonStop.Location = new System.Drawing.Point(144, 13);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(108, 23);
            this.buttonStop.TabIndex = 1;
            this.buttonStop.Text = "Stop Server";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(297, 18);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(57, 13);
            this.labelStatus.TabIndex = 2;
            this.labelStatus.Text = "Offline 0/0";
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(885, 2);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(75, 29);
            this.buttonExit.TabIndex = 3;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // listBoxMessages
            // 
            this.listBoxMessages.FormattingEnabled = true;
            this.listBoxMessages.HorizontalScrollbar = true;
            this.listBoxMessages.Location = new System.Drawing.Point(13, 62);
            this.listBoxMessages.Name = "listBoxMessages";
            this.listBoxMessages.Size = new System.Drawing.Size(458, 368);
            this.listBoxMessages.TabIndex = 4;
            // 
            // listBoxClientMessages
            // 
            this.listBoxClientMessages.FormattingEnabled = true;
            this.listBoxClientMessages.HorizontalScrollbar = true;
            this.listBoxClientMessages.Location = new System.Drawing.Point(487, 179);
            this.listBoxClientMessages.Name = "listBoxClientMessages";
            this.listBoxClientMessages.Size = new System.Drawing.Size(464, 251);
            this.listBoxClientMessages.TabIndex = 5;
            // 
            // comboBoxConnectedUsers
            // 
            this.comboBoxConnectedUsers.FormattingEnabled = true;
            this.comboBoxConnectedUsers.Location = new System.Drawing.Point(487, 62);
            this.comboBoxConnectedUsers.Name = "comboBoxConnectedUsers";
            this.comboBoxConnectedUsers.Size = new System.Drawing.Size(143, 21);
            this.comboBoxConnectedUsers.TabIndex = 6;
            // 
            // buttonChangeUser
            // 
            this.buttonChangeUser.Location = new System.Drawing.Point(636, 60);
            this.buttonChangeUser.Name = "buttonChangeUser";
            this.buttonChangeUser.Size = new System.Drawing.Size(97, 23);
            this.buttonChangeUser.TabIndex = 7;
            this.buttonChangeUser.Text = "Change User";
            this.buttonChangeUser.UseVisualStyleBackColor = true;
            // 
            // buttonDisconnectUser
            // 
            this.buttonDisconnectUser.Location = new System.Drawing.Point(739, 60);
            this.buttonDisconnectUser.Name = "buttonDisconnectUser";
            this.buttonDisconnectUser.Size = new System.Drawing.Size(104, 23);
            this.buttonDisconnectUser.TabIndex = 8;
            this.buttonDisconnectUser.Text = "Disconnect User";
            this.buttonDisconnectUser.UseVisualStyleBackColor = true;
            // 
            // textBoxStringMessage
            // 
            this.textBoxStringMessage.Location = new System.Drawing.Point(487, 151);
            this.textBoxStringMessage.Name = "textBoxStringMessage";
            this.textBoxStringMessage.Size = new System.Drawing.Size(246, 20);
            this.textBoxStringMessage.TabIndex = 9;
            // 
            // buttonSendString
            // 
            this.buttonSendString.Location = new System.Drawing.Point(739, 150);
            this.buttonSendString.Name = "buttonSendString";
            this.buttonSendString.Size = new System.Drawing.Size(83, 23);
            this.buttonSendString.TabIndex = 10;
            this.buttonSendString.Text = "Send String";
            this.buttonSendString.UseVisualStyleBackColor = true;
            // 
            // textBoxSendToAllString
            // 
            this.textBoxSendToAllString.Location = new System.Drawing.Point(487, 110);
            this.textBoxSendToAllString.Name = "textBoxSendToAllString";
            this.textBoxSendToAllString.Size = new System.Drawing.Size(246, 20);
            this.textBoxSendToAllString.TabIndex = 11;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(739, 108);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Send String To All";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // ServerControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 446);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxSendToAllString);
            this.Controls.Add(this.buttonSendString);
            this.Controls.Add(this.textBoxStringMessage);
            this.Controls.Add(this.buttonDisconnectUser);
            this.Controls.Add(this.buttonChangeUser);
            this.Controls.Add(this.comboBoxConnectedUsers);
            this.Controls.Add(this.listBoxClientMessages);
            this.Controls.Add(this.listBoxMessages);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.MaximizeBox = false;
            this.Name = "ServerControlForm";
            this.ShowIcon = false;
            this.Text = "Server Example";
            this.Load += new System.EventHandler(this.ServerControlForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonStart;
        public System.Windows.Forms.Button buttonStop;
        public System.Windows.Forms.Label labelStatus;
        public System.Windows.Forms.Button buttonExit;
        public System.Windows.Forms.ListBox listBoxMessages;
        private System.Windows.Forms.ListBox listBoxClientMessages;
        private System.Windows.Forms.ComboBox comboBoxConnectedUsers;
        private System.Windows.Forms.Button buttonChangeUser;
        private System.Windows.Forms.Button buttonDisconnectUser;
        private System.Windows.Forms.TextBox textBoxStringMessage;
        private System.Windows.Forms.Button buttonSendString;
        private System.Windows.Forms.TextBox textBoxSendToAllString;
        private System.Windows.Forms.Button button1;
    }
}
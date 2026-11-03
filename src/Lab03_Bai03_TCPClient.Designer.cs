namespace LAB03
{
    partial class Lab03_Bai03_TCPClient
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
            txtIP = new TextBox();
            txtPort = new TextBox();
            txtMessage = new TextBox();
            btnDisconnect = new Button();
            btnSend = new Button();
            btnConnect = new Button();
            SuspendLayout();
            // 
            // txtIP
            // 
            txtIP.BackColor = SystemColors.Highlight;
            txtIP.Font = new Font("Times New Roman", 14.25F, FontStyle.Bold);
            txtIP.Location = new Point(64, 42);
            txtIP.Name = "txtIP";
            txtIP.Size = new Size(150, 29);
            txtIP.TabIndex = 0;
            txtIP.Text = "127.0.0.1";
            // 
            // txtPort
            // 
            txtPort.BackColor = SystemColors.Highlight;
            txtPort.Font = new Font("Times New Roman", 14.25F, FontStyle.Bold);
            txtPort.Location = new Point(574, 42);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(150, 29);
            txtPort.TabIndex = 1;
            txtPort.Text = "8080";
            // 
            // txtMessage
            // 
            txtMessage.BackColor = SystemColors.Highlight;
            txtMessage.Font = new Font("Times New Roman", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtMessage.Location = new Point(64, 108);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.ScrollBars = ScrollBars.Horizontal;
            txtMessage.Size = new Size(660, 230);
            txtMessage.TabIndex = 2;
            // 
            // btnDisconnect
            // 
            btnDisconnect.BackgroundImage = Properties.Resources._240_F_1661858452_xa7JJqJPWlDqdD5h9wjISHWQWhOpmbeI2;
            btnDisconnect.BackgroundImageLayout = ImageLayout.Stretch;
            btnDisconnect.Font = new Font("Times New Roman", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDisconnect.ForeColor = Color.White;
            btnDisconnect.Location = new Point(594, 380);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(130, 45);
            btnDisconnect.TabIndex = 3;
            btnDisconnect.Text = "Disconnect";
            btnDisconnect.UseVisualStyleBackColor = true;
            btnDisconnect.Click += btnDisconnect_Click;
            // 
            // btnSend
            // 
            btnSend.BackgroundImage = Properties.Resources._240_F_1661858452_xa7JJqJPWlDqdD5h9wjISHWQWhOpmbeI2;
            btnSend.BackgroundImageLayout = ImageLayout.Stretch;
            btnSend.Font = new Font("Times New Roman", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSend.ForeColor = Color.White;
            btnSend.Location = new Point(333, 380);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(130, 45);
            btnSend.TabIndex = 4;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // btnConnect
            // 
            btnConnect.BackgroundImage = Properties.Resources._240_F_1661858452_xa7JJqJPWlDqdD5h9wjISHWQWhOpmbeI2;
            btnConnect.BackgroundImageLayout = ImageLayout.Stretch;
            btnConnect.Font = new Font("Times New Roman", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnConnect.ForeColor = Color.White;
            btnConnect.Location = new Point(64, 380);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(130, 45);
            btnConnect.TabIndex = 5;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // Lab03_Bai03_TCPClient
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.images1;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(784, 461);
            Controls.Add(btnConnect);
            Controls.Add(btnSend);
            Controls.Add(btnDisconnect);
            Controls.Add(txtMessage);
            Controls.Add(txtPort);
            Controls.Add(txtIP);
            Name = "Lab03_Bai03_TCPClient";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormTCPClient";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtIP;
        private TextBox txtPort;
        private TextBox txtMessage;
        private Button btnDisconnect;
        private Button btnSend;
        private Button btnConnect;
    }
}
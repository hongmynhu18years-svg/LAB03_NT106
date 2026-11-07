namespace LAB03
{
    partial class Lab03_Bai06_Client
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
            lblClient = new Label();
            txtIP = new TextBox();
            lblIP = new Label();
            lblName = new Label();
            txtName = new TextBox();
            btnConnect = new Button();
            btnDisconnect = new Button();
            lstChat = new ListBox();
            cboNguoi = new ComboBox();
            lblNguoi = new Label();
            txtMessage = new TextBox();
            btnSend = new Button();
            lblMessage = new Label();
            SuspendLayout();
            // 
            // lblClient
            // 
            lblClient.AutoSize = true;
            lblClient.BackColor = Color.Transparent;
            lblClient.Font = new Font("Times New Roman", 19.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblClient.ForeColor = Color.FromArgb(234, 248, 255);
            lblClient.Location = new Point(175, 28);
            lblClient.Name = "lblClient";
            lblClient.Size = new Size(229, 61);
            lblClient.TabIndex = 0;
            lblClient.Text = "CLIENT";
            // 
            // txtIP
            // 
            txtIP.BackColor = Color.FromArgb(21, 34, 57);
            txtIP.BorderStyle = BorderStyle.FixedSingle;
            txtIP.Font = new Font("Times New Roman", 13.875F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtIP.ForeColor = Color.FromArgb(234, 248, 255);
            txtIP.Location = new Point(201, 128);
            txtIP.Name = "txtIP";
            txtIP.Size = new Size(282, 50);
            txtIP.TabIndex = 1;
            // 
            // lblIP
            // 
            lblIP.AutoSize = true;
            lblIP.BackColor = Color.Transparent;
            lblIP.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblIP.ForeColor = Color.FromArgb(200, 222, 249);
            lblIP.Location = new Point(31, 134);
            lblIP.Name = "lblIP";
            lblIP.Size = new Size(164, 42);
            lblIP.TabIndex = 2;
            lblIP.Text = "IP Server";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.BackColor = Color.Transparent;
            lblName.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblName.ForeColor = Color.FromArgb(200, 222, 249);
            lblName.Location = new Point(555, 134);
            lblName.Name = "lblName";
            lblName.Size = new Size(109, 42);
            lblName.TabIndex = 3;
            lblName.Text = "Name";
            // 
            // txtName
            // 
            txtName.BackColor = Color.FromArgb(21, 34, 57);
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Font = new Font("Times New Roman", 13.875F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtName.ForeColor = Color.FromArgb(234, 248, 255);
            txtName.Location = new Point(681, 128);
            txtName.Name = "txtName";
            txtName.Size = new Size(350, 50);
            txtName.TabIndex = 4;
            // 
            // btnConnect
            // 
            btnConnect.BackColor = Color.FromArgb(16, 37, 68);
            btnConnect.FlatAppearance.BorderColor = Color.FromArgb(48, 100, 155);
            btnConnect.FlatStyle = FlatStyle.Flat;
            btnConnect.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnConnect.ForeColor = Color.FromArgb(234, 248, 255);
            btnConnect.Location = new Point(1085, 107);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(212, 80);
            btnConnect.TabIndex = 5;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = false;
            btnConnect.Click += btnConnect_Click;
            // 
            // btnDisconnect
            // 
            btnDisconnect.BackColor = Color.FromArgb(40, 28, 65);
            btnDisconnect.FlatAppearance.BorderColor = Color.FromArgb(131, 94, 190);
            btnDisconnect.FlatStyle = FlatStyle.Flat;
            btnDisconnect.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDisconnect.ForeColor = Color.FromArgb(239, 229, 255);
            btnDisconnect.Location = new Point(1345, 107);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(212, 80);
            btnDisconnect.TabIndex = 6;
            btnDisconnect.Text = "Disconnect";
            btnDisconnect.UseVisualStyleBackColor = false;
            btnDisconnect.Click += btnDisconnect_Click;
            // 
            // lstChat
            // 
            lstChat.BackColor = Color.FromArgb(10, 19, 38);
            lstChat.ForeColor = Color.FromArgb(225, 238, 255);
            lstChat.FormattingEnabled = true;
            lstChat.Location = new Point(27, 208);
            lstChat.Name = "lstChat";
            lstChat.ScrollAlwaysVisible = true;
            lstChat.Size = new Size(1530, 580);
            lstChat.TabIndex = 7;
            // 
            // cboNguoi
            // 
            cboNguoi.BackColor = Color.FromArgb(21, 34, 57);
            cboNguoi.DropDownStyle = ComboBoxStyle.DropDownList;
            cboNguoi.Font = new Font("Times New Roman", 13.875F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboNguoi.ForeColor = Color.FromArgb(234, 248, 255);
            cboNguoi.FormattingEnabled = true;
            cboNguoi.Location = new Point(27, 860);
            cboNguoi.Name = "cboNguoi";
            cboNguoi.Size = new Size(294, 50);
            cboNguoi.TabIndex = 8;
            // 
            // lblNguoi
            // 
            lblNguoi.AutoSize = true;
            lblNguoi.BackColor = Color.Transparent;
            lblNguoi.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNguoi.ForeColor = Color.FromArgb(200, 222, 249);
            lblNguoi.Location = new Point(27, 805);
            lblNguoi.Name = "lblNguoi";
            lblNguoi.Size = new Size(178, 36);
            lblNguoi.TabIndex = 9;
            lblNguoi.Text = "Người nhận";
            // 
            // txtMessage
            // 
            txtMessage.BackColor = Color.FromArgb(21, 34, 57);
            txtMessage.BorderStyle = BorderStyle.FixedSingle;
            txtMessage.Font = new Font("Times New Roman", 13.875F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtMessage.ForeColor = Color.FromArgb(234, 248, 255);
            txtMessage.Location = new Point(627, 823);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(646, 76);
            txtMessage.TabIndex = 10;
            // 
            // btnSend
            // 
            btnSend.BackColor = Color.FromArgb(22, 55, 92);
            btnSend.FlatAppearance.BorderColor = Color.FromArgb(67, 145, 205);
            btnSend.FlatStyle = FlatStyle.Flat;
            btnSend.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSend.ForeColor = Color.FromArgb(234, 248, 255);
            btnSend.Location = new Point(1317, 823);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(240, 76);
            btnSend.TabIndex = 11;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = false;
            btnSend.Click += btnSend_Click;
            // 
            // lblMessage
            // 
            lblMessage.AutoSize = true;
            lblMessage.BackColor = Color.Transparent;
            lblMessage.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMessage.ForeColor = Color.FromArgb(200, 222, 249);
            lblMessage.Location = new Point(437, 849);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(152, 42);
            lblMessage.TabIndex = 12;
            lblMessage.Text = "Message";
            // 
            // Lab03_Bai06_Client
            // 
            AutoScaleDimensions = new SizeF(18F, 36F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.TCP_Client;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1592, 934);
            Controls.Add(lblMessage);
            Controls.Add(btnSend);
            Controls.Add(txtMessage);
            Controls.Add(lblNguoi);
            Controls.Add(cboNguoi);
            Controls.Add(lstChat);
            Controls.Add(btnDisconnect);
            Controls.Add(btnConnect);
            Controls.Add(txtName);
            Controls.Add(lblName);
            Controls.Add(lblIP);
            Controls.Add(txtIP);
            Controls.Add(lblClient);
            Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Lab03_Bai06_Client";
            Text = "TCP CHAT ROOM - CLIENT";
            Load += Lab03_Bai06_Client_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblClient;
        private TextBox txtIP;
        private Label lblIP;
        private Label lblName;
        private TextBox txtName;
        private Button btnConnect;
        private Button btnDisconnect;
        private ListBox lstChat;
        private ComboBox cboNguoi;
        private Label lblNguoi;
        private TextBox txtMessage;
        private Button btnSend;
        private Label lblMessage;
    }
}
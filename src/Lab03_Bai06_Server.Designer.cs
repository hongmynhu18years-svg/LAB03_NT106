namespace LAB03
{
    partial class Lab03_Bai06_Server
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
            btnListen = new Button();
            lstServer = new ListBox();
            lblServer = new Label();
            btnThoat = new Button();
            SuspendLayout();
            // 
            // btnListen
            // 
            btnListen.BackColor = Color.FromArgb(16, 37, 68);
            btnListen.FlatStyle = FlatStyle.Flat;
            btnListen.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnListen.ForeColor = Color.FromArgb(234, 248, 255);
            btnListen.Location = new Point(78, 136);
            btnListen.Name = "btnListen";
            btnListen.Size = new Size(346, 72);
            btnListen.TabIndex = 0;
            btnListen.Text = "LISTEN";
            btnListen.UseVisualStyleBackColor = false;
            btnListen.Click += btnListen_Click;
            // 
            // lstServer
            // 
            lstServer.BackColor = Color.FromArgb(10, 19, 38);
            lstServer.ForeColor = Color.FromArgb(225, 238, 255);
            lstServer.FormattingEnabled = true;
            lstServer.Location = new Point(38, 232);
            lstServer.Name = "lstServer";
            lstServer.Size = new Size(1248, 724);
            lstServer.TabIndex = 1;
            // 
            // lblServer
            // 
            lblServer.AutoSize = true;
            lblServer.BackColor = Color.Transparent;
            lblServer.Font = new Font("Times New Roman", 19.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblServer.ForeColor = Color.FromArgb(234, 248, 255);
            lblServer.Location = new Point(226, 40);
            lblServer.Name = "lblServer";
            lblServer.Size = new Size(238, 61);
            lblServer.TabIndex = 2;
            lblServer.Text = "SERVER";
            // 
            // btnThoat
            // 
            btnThoat.BackColor = Color.FromArgb(40, 28, 65);
            btnThoat.FlatAppearance.BorderColor = Color.FromArgb(131, 94, 190);
            btnThoat.FlatStyle = FlatStyle.Flat;
            btnThoat.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnThoat.ForeColor = Color.FromArgb(239, 229, 255);
            btnThoat.Location = new Point(974, 136);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(312, 72);
            btnThoat.TabIndex = 7;
            btnThoat.Text = "Thoát";
            btnThoat.UseVisualStyleBackColor = false;
            btnThoat.Click += btnThoat_Click;
            // 
            // Lab03_Bai06_Server
            // 
            AutoScaleDimensions = new SizeF(18F, 36F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.TCP_Server;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1322, 1006);
            Controls.Add(btnThoat);
            Controls.Add(lblServer);
            Controls.Add(lstServer);
            Controls.Add(btnListen);
            Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Lab03_Bai06_Server";
            Text = "TCP CHAT ROOM - SERVER";
            Load += Lab03_Bai06_Server_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnListen;
        private ListBox lstServer;
        private Label lblServer;
        private Button btnThoat;
    }
}
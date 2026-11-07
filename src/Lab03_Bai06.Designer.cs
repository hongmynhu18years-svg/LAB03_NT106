namespace LAB03
{
    partial class Lab03_Bai06
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
            btnOpenServer = new Button();
            btnOpenClient = new Button();
            btnThoat = new Button();
            SuspendLayout();
            // 
            // btnOpenServer
            // 
            btnOpenServer.BackColor = Color.FromArgb(16, 31, 58);
            btnOpenServer.Font = new Font("Times New Roman", 16.125F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnOpenServer.ForeColor = Color.FromArgb(234, 248, 255);
            btnOpenServer.ImageAlign = ContentAlignment.MiddleLeft;
            btnOpenServer.Location = new Point(289, 80);
            btnOpenServer.Name = "btnOpenServer";
            btnOpenServer.Size = new Size(502, 118);
            btnOpenServer.TabIndex = 0;
            btnOpenServer.Text = "SERVER";
            btnOpenServer.UseVisualStyleBackColor = false;
            btnOpenServer.Click += btnOpenServer_Click;
            // 
            // btnOpenClient
            // 
            btnOpenClient.BackColor = Color.FromArgb(16, 31, 58);
            btnOpenClient.Font = new Font("Times New Roman", 16.125F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnOpenClient.ForeColor = Color.FromArgb(234, 248, 255);
            btnOpenClient.Location = new Point(289, 280);
            btnOpenClient.Name = "btnOpenClient";
            btnOpenClient.Size = new Size(502, 118);
            btnOpenClient.TabIndex = 1;
            btnOpenClient.Text = "CLIENT";
            btnOpenClient.UseVisualStyleBackColor = false;
            btnOpenClient.Click += btnOpenClient_Click;
            // 
            // btnThoat
            // 
            btnThoat.BackColor = Color.FromArgb(50, 24, 44);
            btnThoat.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnThoat.ForeColor = Color.FromArgb(255, 214, 224);
            btnThoat.Location = new Point(844, 476);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(206, 82);
            btnThoat.TabIndex = 2;
            btnThoat.Text = "Thoát";
            btnThoat.UseVisualStyleBackColor = false;
            btnThoat.Click += btnThoat_Click;
            // 
            // Lab03_Bai06
            // 
            AutoScaleDimensions = new SizeF(18F, 36F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.TCP_CHAT_ROOM;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1085, 583);
            Controls.Add(btnThoat);
            Controls.Add(btnOpenClient);
            Controls.Add(btnOpenServer);
            Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Lab03_Bai06";
            Text = "TCP CHAT ROOM";
            ResumeLayout(false);
        }

        #endregion

        private Button btnOpenServer;
        private Button btnOpenClient;
        private Button btnThoat;
    }
}
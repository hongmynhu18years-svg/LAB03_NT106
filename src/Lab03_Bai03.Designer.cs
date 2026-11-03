namespace LAB03
{
    partial class Lab03_Bai03
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
            btnThoat = new Button();
            btnUDPCLient = new Button();
            btnServer = new Button();
            SuspendLayout();
            // 
            // btnThoat
            // 
            btnThoat.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 163);
            btnThoat.Location = new Point(362, 152);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(98, 37);
            btnThoat.TabIndex = 11;
            btnThoat.Text = "Thoát";
            btnThoat.UseVisualStyleBackColor = true;
            btnThoat.Click += btnThoat_Click;
            // 
            // btnUDPCLient
            // 
            btnUDPCLient.BackgroundImage = Properties.Resources.Download_Modern_Background_Technology_Realistic_Glossy_Hexagonal_Metallic_Purple_and_Dark_for_free1;
            btnUDPCLient.BackgroundImageLayout = ImageLayout.Stretch;
            btnUDPCLient.Font = new Font("Times New Roman", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 163);
            btnUDPCLient.ForeColor = Color.Violet;
            btnUDPCLient.Location = new Point(299, 72);
            btnUDPCLient.Name = "btnUDPCLient";
            btnUDPCLient.Size = new Size(142, 49);
            btnUDPCLient.TabIndex = 10;
            btnUDPCLient.Text = "TCP Client";
            btnUDPCLient.UseVisualStyleBackColor = true;
            btnUDPCLient.Click += btnUDPCLient_Click;
            // 
            // btnServer
            // 
            btnServer.BackgroundImage = Properties.Resources.Download_Modern_Background_Technology_Realistic_Glossy_Hexagonal_Metallic_Purple_and_Dark_for_free1;
            btnServer.BackgroundImageLayout = ImageLayout.Stretch;
            btnServer.Font = new Font("Times New Roman", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnServer.ForeColor = Color.Violet;
            btnServer.Location = new Point(36, 72);
            btnServer.Name = "btnServer";
            btnServer.Size = new Size(142, 49);
            btnServer.TabIndex = 9;
            btnServer.Text = "TCP Server";
            btnServer.UseVisualStyleBackColor = true;
            btnServer.Click += btnServer_Click;
            // 
            // Lab03_Bai03
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Download_Modern_Background_Technology_Realistic_Glossy_Hexagonal_Metallic_Purple_and_Dark_for_free2;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(472, 201);
            Controls.Add(btnThoat);
            Controls.Add(btnUDPCLient);
            Controls.Add(btnServer);
            Name = "Lab03_Bai03";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormDashboard";
            ResumeLayout(false);
        }

        #endregion

        private Button btnThoat;
        private Button btnUDPCLient;
        private Button btnServer;
    }
}
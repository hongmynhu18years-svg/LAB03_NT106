namespace LAB03
{
    partial class Lab03_Bai04_PasswordForm
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
            panelMouseDown = new FlowLayoutPanel();
            btnClose = new Button();
            btnMinimize = new Button();
            btnMaximize = new Button();
            label1 = new Label();
            txtPassword = new TextBox();
            btnConfirm = new Button();
            lblThongBao = new Label();
            bg = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)bg).BeginInit();
            SuspendLayout();
            // 
            // panelMouseDown
            // 
            panelMouseDown.BackColor = Color.SeaShell;
            panelMouseDown.Dock = DockStyle.Top;
            panelMouseDown.Location = new Point(0, 0);
            panelMouseDown.Name = "panelMouseDown";
            panelMouseDown.Size = new Size(550, 36);
            panelMouseDown.TabIndex = 0;
            panelMouseDown.MouseDown += panelMouseDown_MouseDown;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnClose.Location = new Point(503, 1);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(46, 35);
            btnClose.TabIndex = 1;
            btnClose.TabStop = false;
            btnClose.Text = "✕";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnMinimize
            // 
            btnMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMinimize.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnMinimize.Location = new Point(399, 1);
            btnMinimize.Name = "btnMinimize";
            btnMinimize.Size = new Size(46, 35);
            btnMinimize.TabIndex = 2;
            btnMinimize.TabStop = false;
            btnMinimize.Text = "—";
            btnMinimize.UseVisualStyleBackColor = true;
            btnMinimize.Click += btnMinimize_Click;
            // 
            // btnMaximize
            // 
            btnMaximize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMaximize.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnMaximize.Location = new Point(451, 1);
            btnMaximize.Name = "btnMaximize";
            btnMaximize.Size = new Size(46, 35);
            btnMaximize.TabIndex = 3;
            btnMaximize.TabStop = false;
            btnMaximize.Text = "❐";
            btnMaximize.UseVisualStyleBackColor = true;
            btnMaximize.Click += btnMaximize_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Tahoma", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.SeaShell;
            label1.Location = new Point(173, 72);
            label1.Name = "label1";
            label1.Size = new Size(179, 27);
            label1.TabIndex = 4;
            label1.Text = "Nhập mật khẩu";
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.SeaShell;
            txtPassword.Font = new Font("Tahoma", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtPassword.Location = new Point(146, 102);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(221, 32);
            txtPassword.TabIndex = 0;
            txtPassword.Click += txtPassword_Click;
            txtPassword.TextChanged += txtPassword_TextChanged;
            // 
            // btnConfirm
            // 
            btnConfirm.BackColor = Color.SeaShell;
            btnConfirm.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnConfirm.ForeColor = Color.Peru;
            btnConfirm.Location = new Point(255, 163);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(112, 34);
            btnConfirm.TabIndex = 6;
            btnConfirm.Text = "Xác nhận";
            btnConfirm.UseVisualStyleBackColor = false;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // lblThongBao
            // 
            lblThongBao.AutoSize = true;
            lblThongBao.BackColor = Color.Transparent;
            lblThongBao.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblThongBao.ForeColor = Color.OrangeRed;
            lblThongBao.Location = new Point(146, 225);
            lblThongBao.Name = "lblThongBao";
            lblThongBao.Size = new Size(0, 19);
            lblThongBao.TabIndex = 7;
            // 
            // bg
            // 
            bg.BackgroundImage = Properties.Resources.cinema1;
            bg.BackgroundImageLayout = ImageLayout.Stretch;
            bg.Dock = DockStyle.Fill;
            bg.Location = new Point(0, 0);
            bg.Name = "bg";
            bg.Size = new Size(550, 266);
            bg.SizeMode = PictureBoxSizeMode.StretchImage;
            bg.TabIndex = 8;
            bg.TabStop = false;
            // 
            // PasswordForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.BurlyWood;
            BackgroundImage = Properties.Resources.Gemini_Generated_Image_6456hv6456hv6456;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(550, 266);
            Controls.Add(lblThongBao);
            Controls.Add(btnConfirm);
            Controls.Add(txtPassword);
            Controls.Add(label1);
            Controls.Add(btnMinimize);
            Controls.Add(btnMaximize);
            Controls.Add(btnClose);
            Controls.Add(panelMouseDown);
            Controls.Add(bg);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Name = "PasswordForm";
            Text = "Form2";
            Load += lblThongBa_Load;
            ((System.ComponentModel.ISupportInitialize)bg).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel panelMouseDown;
        private Button btnClose;
        private Button btnMinimize;
        private Button btnMaximize;
        private Label label1;
        private TextBox txtPassword;
        private Button btnConfirm;
        private Label lblThongBao;
        private PictureBox bg;
    }
}
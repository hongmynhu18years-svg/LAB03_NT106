namespace LAB03
{
    partial class Lab03_Bai04_AdminForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lab03_Bai04_AdminForm));
            bg = new PictureBox();
            pictureBoxMouseDown = new PictureBox();
            btnClose = new Button();
            btnMinimize = new Button();
            btnMaximize = new Button();
            progressBar1 = new ProgressBar();
            btnXuatThongKe = new Button();
            lvThongKe = new ListView();
            btnLuu = new Button();
            ((System.ComponentModel.ISupportInitialize)bg).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMouseDown).BeginInit();
            SuspendLayout();
            // 
            // bg
            // 
            bg.BackgroundImage = (Image)resources.GetObject("bg.BackgroundImage");
            bg.BackgroundImageLayout = ImageLayout.Stretch;
            bg.Dock = DockStyle.Fill;
            bg.Location = new Point(0, 0);
            bg.Name = "bg";
            bg.Size = new Size(1138, 450);
            bg.SizeMode = PictureBoxSizeMode.StretchImage;
            bg.TabIndex = 0;
            bg.TabStop = false;
            // 
            // pictureBoxMouseDown
            // 
            pictureBoxMouseDown.BackgroundImage = (Image)resources.GetObject("pictureBoxMouseDown.BackgroundImage");
            pictureBoxMouseDown.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBoxMouseDown.Dock = DockStyle.Top;
            pictureBoxMouseDown.Location = new Point(0, 0);
            pictureBoxMouseDown.Name = "pictureBoxMouseDown";
            pictureBoxMouseDown.Size = new Size(1138, 51);
            pictureBoxMouseDown.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxMouseDown.TabIndex = 1;
            pictureBoxMouseDown.TabStop = false;
            pictureBoxMouseDown.MouseDown += pictureBoxMouseDown_MouseDown;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnClose.Location = new Point(1076, 12);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(50, 34);
            btnClose.TabIndex = 2;
            btnClose.Text = "✕";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnMinimize
            // 
            btnMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMinimize.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnMinimize.Location = new Point(964, 12);
            btnMinimize.Name = "btnMinimize";
            btnMinimize.Size = new Size(50, 34);
            btnMinimize.TabIndex = 3;
            btnMinimize.Text = "—";
            btnMinimize.UseVisualStyleBackColor = true;
            btnMinimize.Click += btnMinimize_Click;
            // 
            // btnMaximize
            // 
            btnMaximize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMaximize.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnMaximize.Location = new Point(1020, 12);
            btnMaximize.Name = "btnMaximize";
            btnMaximize.Size = new Size(50, 34);
            btnMaximize.TabIndex = 4;
            btnMaximize.Text = "❐";
            btnMaximize.UseVisualStyleBackColor = true;
            btnMaximize.Click += btnMaximize_Click;
            // 
            // progressBar1
            // 
            progressBar1.BackColor = Color.SaddleBrown;
            progressBar1.Location = new Point(324, 371);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(455, 22);
            progressBar1.TabIndex = 5;
            // 
            // btnXuatThongKe
            // 
            btnXuatThongKe.BackgroundImage = Properties.Resources.Gemini_Generated_Gif_j8cnlfj8cnlfj8cn;
            btnXuatThongKe.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnXuatThongKe.ForeColor = Color.BurlyWood;
            btnXuatThongKe.Location = new Point(785, 388);
            btnXuatThongKe.Name = "btnXuatThongKe";
            btnXuatThongKe.Size = new Size(164, 50);
            btnXuatThongKe.TabIndex = 6;
            btnXuatThongKe.Text = "Xuất thống kê";
            btnXuatThongKe.UseVisualStyleBackColor = true;
            btnXuatThongKe.Click += btnXuatThongKe_Click;
            // 
            // lvThongKe
            // 
            lvThongKe.BackColor = Color.DarkSlateBlue;
            lvThongKe.BorderStyle = BorderStyle.FixedSingle;
            lvThongKe.Font = new Font("Tahoma", 7F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lvThongKe.ForeColor = SystemColors.Window;
            lvThongKe.FullRowSelect = true;
            lvThongKe.GridLines = true;
            lvThongKe.Location = new Point(221, 107);
            lvThongKe.Name = "lvThongKe";
            lvThongKe.Size = new Size(684, 238);
            lvThongKe.TabIndex = 7;
            lvThongKe.UseCompatibleStateImageBehavior = false;
            // 
            // btnLuu
            // 
            btnLuu.BackgroundImage = Properties.Resources.Gemini_Generated_Gif_j8cnlfj8cnlfj8cn;
            btnLuu.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLuu.ForeColor = Color.BurlyWood;
            btnLuu.Location = new Point(971, 388);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(155, 50);
            btnLuu.TabIndex = 8;
            btnLuu.Text = "Ghi File";
            btnLuu.UseVisualStyleBackColor = true;
            btnLuu.Click += btnLuu_Click;
            // 
            // AdminForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Gemini_Generated_Gif_52bpar52bpar52bp;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1138, 450);
            Controls.Add(btnLuu);
            Controls.Add(lvThongKe);
            Controls.Add(btnXuatThongKe);
            Controls.Add(progressBar1);
            Controls.Add(btnMaximize);
            Controls.Add(btnMinimize);
            Controls.Add(btnClose);
            Controls.Add(pictureBoxMouseDown);
            Controls.Add(bg);
            FormBorderStyle = FormBorderStyle.None;
            Name = "AdminForm";
            Text = "AdminForm";
            Load += AdminForm_Load;
            ((System.ComponentModel.ISupportInitialize)bg).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMouseDown).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox bg;
        private PictureBox pictureBoxMouseDown;
        private Button btnClose;
        private Button btnMinimize;
        private Button btnMaximize;
        private ProgressBar progressBar1;
        private Button btnXuatThongKe;
        private ListView lvThongKe;
        private Button btnLuu;
    }
}
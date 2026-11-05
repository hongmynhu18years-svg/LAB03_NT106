namespace LAB03
{
    partial class Lab03_Bai04_BookingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lab03_Bai04_BookingForm));
            pictureBox1 = new PictureBox();
            pictureBoxMouseDown = new PictureBox();
            btnClose = new Button();
            btnMinimize = new Button();
            btnMaximize = new Button();
            label1 = new Label();
            cbPhongChieu = new ComboBox();
            btnThanhToan = new Button();
            btnHuy = new Button();
            btnThoat = new Button();
            txtTenKhachHang = new TextBox();
            txtTenPhim = new TextBox();
            panelGhe = new Panel();
            btnC5 = new Button();
            btnC4 = new Button();
            btnC3 = new Button();
            btnC2 = new Button();
            btnC1 = new Button();
            btnB5 = new Button();
            btnB4 = new Button();
            btnB3 = new Button();
            btnB2 = new Button();
            btnB1 = new Button();
            btnA5 = new Button();
            btnA4 = new Button();
            btnA3 = new Button();
            btnA2 = new Button();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            btnA1 = new Button();
            lblTongTien = new Label();
            lblTinhTien = new Label();
            lblLoi = new Label();
            rtbHoaDon = new RichTextBox();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMouseDown).BeginInit();
            panelGhe.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(800, 450);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pictureBoxMouseDown
            // 
            pictureBoxMouseDown.BackgroundImage = (Image)resources.GetObject("pictureBoxMouseDown.BackgroundImage");
            pictureBoxMouseDown.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBoxMouseDown.Dock = DockStyle.Top;
            pictureBoxMouseDown.Location = new Point(0, 0);
            pictureBoxMouseDown.Name = "pictureBoxMouseDown";
            pictureBoxMouseDown.Size = new Size(800, 36);
            pictureBoxMouseDown.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxMouseDown.TabIndex = 1;
            pictureBoxMouseDown.TabStop = false;
            pictureBoxMouseDown.MouseDown += pictureBox2_MouseDown;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnClose.Location = new Point(743, 0);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(57, 34);
            btnClose.TabIndex = 2;
            btnClose.Text = "✕";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnMinimize
            // 
            btnMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMinimize.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnMinimize.Location = new Point(617, 0);
            btnMinimize.Name = "btnMinimize";
            btnMinimize.Size = new Size(57, 34);
            btnMinimize.TabIndex = 3;
            btnMinimize.Text = "—";
            btnMinimize.UseVisualStyleBackColor = true;
            btnMinimize.Click += btnMinimize_Click;
            // 
            // btnMaximize
            // 
            btnMaximize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMaximize.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnMaximize.Location = new Point(680, 0);
            btnMaximize.Name = "btnMaximize";
            btnMaximize.Size = new Size(57, 34);
            btnMaximize.TabIndex = 4;
            btnMaximize.Text = "❐";
            btnMaximize.UseVisualStyleBackColor = true;
            btnMaximize.Click += btnMaximize_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Tahoma", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.BurlyWood;
            label1.Location = new Point(74, 65);
            label1.Name = "label1";
            label1.Size = new Size(192, 24);
            label1.TabIndex = 5;
            label1.Text = "Chọn phòng chiếu";
            // 
            // cbPhongChieu
            // 
            cbPhongChieu.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cbPhongChieu.FormattingEnabled = true;
            cbPhongChieu.Location = new Point(12, 92);
            cbPhongChieu.Name = "cbPhongChieu";
            cbPhongChieu.Size = new Size(289, 30);
            cbPhongChieu.TabIndex = 0;
            cbPhongChieu.TabStop = false;
            cbPhongChieu.SelectedIndexChanged += cbPhongChieu_SelectedIndexChanged;
            // 
            // btnThanhToan
            // 
            btnThanhToan.BackColor = Color.MediumPurple;
            btnThanhToan.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnThanhToan.ForeColor = SystemColors.ButtonHighlight;
            btnThanhToan.Location = new Point(473, 369);
            btnThanhToan.Name = "btnThanhToan";
            btnThanhToan.Size = new Size(131, 41);
            btnThanhToan.TabIndex = 16;
            btnThanhToan.Text = "Thanh Toán";
            btnThanhToan.UseVisualStyleBackColor = false;
            btnThanhToan.Click += btnThanhToan_Click;
            // 
            // btnHuy
            // 
            btnHuy.BackColor = Color.MediumSlateBlue;
            btnHuy.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnHuy.ForeColor = SystemColors.ButtonHighlight;
            btnHuy.Location = new Point(610, 368);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(79, 41);
            btnHuy.TabIndex = 17;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = false;
            btnHuy.Click += btnHuy_Click;
            // 
            // btnThoat
            // 
            btnThoat.BackColor = Color.DarkSlateBlue;
            btnThoat.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnThoat.ForeColor = SystemColors.ButtonHighlight;
            btnThoat.Location = new Point(695, 369);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(84, 41);
            btnThoat.TabIndex = 18;
            btnThoat.Text = "Thoát";
            btnThoat.UseVisualStyleBackColor = false;
            btnThoat.Click += btnThoat_Click;
            // 
            // txtTenKhachHang
            // 
            txtTenKhachHang.BackColor = Color.BurlyWood;
            txtTenKhachHang.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtTenKhachHang.Location = new Point(491, 51);
            txtTenKhachHang.Name = "txtTenKhachHang";
            txtTenKhachHang.ReadOnly = true;
            txtTenKhachHang.Size = new Size(226, 29);
            txtTenKhachHang.TabIndex = 11;
            txtTenKhachHang.TabStop = false;
            // 
            // txtTenPhim
            // 
            txtTenPhim.BackColor = Color.BurlyWood;
            txtTenPhim.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtTenPhim.Location = new Point(491, 86);
            txtTenPhim.Name = "txtTenPhim";
            txtTenPhim.ReadOnly = true;
            txtTenPhim.Size = new Size(226, 29);
            txtTenPhim.TabIndex = 12;
            txtTenPhim.TabStop = false;
            // 
            // panelGhe
            // 
            panelGhe.BackColor = Color.Transparent;
            panelGhe.Controls.Add(btnC5);
            panelGhe.Controls.Add(btnC4);
            panelGhe.Controls.Add(btnC3);
            panelGhe.Controls.Add(btnC2);
            panelGhe.Controls.Add(btnC1);
            panelGhe.Controls.Add(btnB5);
            panelGhe.Controls.Add(btnB4);
            panelGhe.Controls.Add(btnB3);
            panelGhe.Controls.Add(btnB2);
            panelGhe.Controls.Add(btnB1);
            panelGhe.Controls.Add(btnA5);
            panelGhe.Controls.Add(btnA4);
            panelGhe.Controls.Add(btnA3);
            panelGhe.Controls.Add(btnA2);
            panelGhe.Controls.Add(label4);
            panelGhe.Controls.Add(label3);
            panelGhe.Controls.Add(label2);
            panelGhe.Controls.Add(btnA1);
            panelGhe.Location = new Point(12, 177);
            panelGhe.Name = "panelGhe";
            panelGhe.Size = new Size(292, 160);
            panelGhe.TabIndex = 13;
            // 
            // btnC5
            // 
            btnC5.BackColor = Color.DarkSlateBlue;
            btnC5.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnC5.Location = new Point(242, 113);
            btnC5.Name = "btnC5";
            btnC5.Size = new Size(45, 31);
            btnC5.TabIndex = 15;
            btnC5.Text = "C5";
            btnC5.UseVisualStyleBackColor = false;
            btnC5.Click += Ghe_Click;
            // 
            // btnC4
            // 
            btnC4.BackColor = Color.DarkSlateBlue;
            btnC4.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnC4.Location = new Point(189, 113);
            btnC4.Name = "btnC4";
            btnC4.Size = new Size(45, 31);
            btnC4.TabIndex = 14;
            btnC4.Text = "C4";
            btnC4.UseVisualStyleBackColor = false;
            btnC4.Click += Ghe_Click;
            // 
            // btnC3
            // 
            btnC3.BackColor = Color.DarkSlateBlue;
            btnC3.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnC3.Location = new Point(138, 113);
            btnC3.Name = "btnC3";
            btnC3.Size = new Size(45, 31);
            btnC3.TabIndex = 13;
            btnC3.Text = "C3";
            btnC3.UseVisualStyleBackColor = false;
            btnC3.Click += Ghe_Click;
            // 
            // btnC2
            // 
            btnC2.BackColor = Color.DarkSlateBlue;
            btnC2.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnC2.Location = new Point(87, 113);
            btnC2.Name = "btnC2";
            btnC2.Size = new Size(45, 31);
            btnC2.TabIndex = 12;
            btnC2.Text = "C2";
            btnC2.UseVisualStyleBackColor = false;
            btnC2.Click += Ghe_Click;
            // 
            // btnC1
            // 
            btnC1.BackColor = Color.DarkSlateBlue;
            btnC1.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnC1.Location = new Point(36, 113);
            btnC1.Name = "btnC1";
            btnC1.Size = new Size(45, 31);
            btnC1.TabIndex = 11;
            btnC1.Text = "C1";
            btnC1.UseVisualStyleBackColor = false;
            btnC1.Click += Ghe_Click;
            // 
            // btnB5
            // 
            btnB5.BackColor = Color.MediumSlateBlue;
            btnB5.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnB5.Location = new Point(242, 65);
            btnB5.Name = "btnB5";
            btnB5.Size = new Size(45, 31);
            btnB5.TabIndex = 10;
            btnB5.Text = "B5";
            btnB5.UseVisualStyleBackColor = false;
            btnB5.Click += Ghe_Click;
            // 
            // btnB4
            // 
            btnB4.BackColor = Color.MediumSlateBlue;
            btnB4.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnB4.Location = new Point(189, 65);
            btnB4.Name = "btnB4";
            btnB4.Size = new Size(45, 31);
            btnB4.TabIndex = 9;
            btnB4.Text = "B4";
            btnB4.UseVisualStyleBackColor = false;
            btnB4.Click += Ghe_Click;
            // 
            // btnB3
            // 
            btnB3.BackColor = Color.MediumSlateBlue;
            btnB3.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnB3.Location = new Point(138, 65);
            btnB3.Name = "btnB3";
            btnB3.Size = new Size(45, 31);
            btnB3.TabIndex = 8;
            btnB3.Text = "B3";
            btnB3.UseVisualStyleBackColor = false;
            btnB3.Click += Ghe_Click;
            // 
            // btnB2
            // 
            btnB2.BackColor = Color.MediumSlateBlue;
            btnB2.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnB2.Location = new Point(87, 65);
            btnB2.Name = "btnB2";
            btnB2.Size = new Size(45, 31);
            btnB2.TabIndex = 7;
            btnB2.Text = "B2";
            btnB2.UseVisualStyleBackColor = false;
            btnB2.Click += Ghe_Click;
            // 
            // btnB1
            // 
            btnB1.BackColor = Color.MediumSlateBlue;
            btnB1.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnB1.Location = new Point(36, 65);
            btnB1.Name = "btnB1";
            btnB1.Size = new Size(45, 31);
            btnB1.TabIndex = 6;
            btnB1.Text = "B1";
            btnB1.UseVisualStyleBackColor = false;
            btnB1.Click += Ghe_Click;
            // 
            // btnA5
            // 
            btnA5.BackColor = Color.MediumPurple;
            btnA5.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnA5.Location = new Point(242, 13);
            btnA5.Name = "btnA5";
            btnA5.Size = new Size(45, 31);
            btnA5.TabIndex = 5;
            btnA5.Text = "A5";
            btnA5.UseVisualStyleBackColor = false;
            btnA5.Click += Ghe_Click;
            // 
            // btnA4
            // 
            btnA4.BackColor = Color.MediumPurple;
            btnA4.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnA4.Location = new Point(189, 13);
            btnA4.Name = "btnA4";
            btnA4.Size = new Size(45, 31);
            btnA4.TabIndex = 4;
            btnA4.Text = "A4";
            btnA4.UseVisualStyleBackColor = false;
            btnA4.Click += Ghe_Click;
            // 
            // btnA3
            // 
            btnA3.BackColor = Color.MediumPurple;
            btnA3.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnA3.Location = new Point(138, 13);
            btnA3.Name = "btnA3";
            btnA3.Size = new Size(45, 31);
            btnA3.TabIndex = 3;
            btnA3.Text = "A3";
            btnA3.UseVisualStyleBackColor = false;
            btnA3.Click += Ghe_Click;
            // 
            // btnA2
            // 
            btnA2.BackColor = Color.MediumPurple;
            btnA2.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnA2.Location = new Point(87, 13);
            btnA2.Name = "btnA2";
            btnA2.Size = new Size(45, 31);
            btnA2.TabIndex = 2;
            btnA2.Text = "A2";
            btnA2.UseVisualStyleBackColor = false;
            btnA2.Click += Ghe_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Tahoma", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.BurlyWood;
            label4.Location = new Point(3, 113);
            label4.Name = "label4";
            label4.Size = new Size(27, 27);
            label4.TabIndex = 17;
            label4.Text = "C";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Tahoma", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.BurlyWood;
            label3.Location = new Point(3, 65);
            label3.Name = "label3";
            label3.Size = new Size(27, 27);
            label3.TabIndex = 16;
            label3.Text = "B";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Tahoma", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.BurlyWood;
            label2.Location = new Point(3, 13);
            label2.Name = "label2";
            label2.Size = new Size(27, 27);
            label2.TabIndex = 15;
            label2.Text = "A";
            // 
            // btnA1
            // 
            btnA1.BackColor = Color.MediumPurple;
            btnA1.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnA1.Location = new Point(36, 13);
            btnA1.Name = "btnA1";
            btnA1.Size = new Size(45, 31);
            btnA1.TabIndex = 1;
            btnA1.Text = "A1";
            btnA1.UseVisualStyleBackColor = false;
            btnA1.Click += Ghe_Click;
            // 
            // lblTongTien
            // 
            lblTongTien.AutoSize = true;
            lblTongTien.BackColor = Color.Transparent;
            lblTongTien.Location = new Point(12, 375);
            lblTongTien.Name = "lblTongTien";
            lblTongTien.Size = new Size(0, 25);
            lblTongTien.TabIndex = 14;
            // 
            // lblTinhTien
            // 
            lblTinhTien.AutoSize = true;
            lblTinhTien.BackColor = Color.Transparent;
            lblTinhTien.Font = new Font("Tahoma", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTinhTien.ForeColor = Color.Snow;
            lblTinhTien.Location = new Point(18, 376);
            lblTinhTien.Name = "lblTinhTien";
            lblTinhTien.Size = new Size(0, 24);
            lblTinhTien.TabIndex = 19;
            // 
            // lblLoi
            // 
            lblLoi.AutoSize = true;
            lblLoi.BackColor = Color.Transparent;
            lblLoi.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLoi.ForeColor = Color.OrangeRed;
            lblLoi.Location = new Point(0, 376);
            lblLoi.Name = "lblLoi";
            lblLoi.Size = new Size(0, 19);
            lblLoi.TabIndex = 20;
            // 
            // rtbHoaDon
            // 
            rtbHoaDon.BackColor = Color.MediumPurple;
            rtbHoaDon.Font = new Font("Tahoma", 8F, FontStyle.Italic, GraphicsUnit.Point, 0);
            rtbHoaDon.ForeColor = Color.BurlyWood;
            rtbHoaDon.Location = new Point(438, 135);
            rtbHoaDon.Name = "rtbHoaDon";
            rtbHoaDon.ReadOnly = true;
            rtbHoaDon.Size = new Size(321, 186);
            rtbHoaDon.TabIndex = 21;
            rtbHoaDon.Text = "";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Tahoma", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.BurlyWood;
            label5.Location = new Point(74, 150);
            label5.Name = "label5";
            label5.Size = new Size(155, 24);
            label5.TabIndex = 22;
            label5.Text = "Chọn ghế ngồi";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.BurlyWood;
            label6.Location = new Point(346, 51);
            label6.Name = "label6";
            label6.Size = new Size(139, 19);
            label6.TabIndex = 23;
            label6.Text = "Customer Name";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.BurlyWood;
            label7.Location = new Point(428, 91);
            label7.Name = "label7";
            label7.Size = new Size(57, 19);
            label7.TabIndex = 24;
            label7.Text = "Movie";
            // 
            // BookingForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Gemini_Generated_Gif_r7m1tgr7m1tgr7m1;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(rtbHoaDon);
            Controls.Add(lblLoi);
            Controls.Add(lblTinhTien);
            Controls.Add(lblTongTien);
            Controls.Add(panelGhe);
            Controls.Add(txtTenPhim);
            Controls.Add(txtTenKhachHang);
            Controls.Add(btnThoat);
            Controls.Add(btnHuy);
            Controls.Add(btnThanhToan);
            Controls.Add(cbPhongChieu);
            Controls.Add(label1);
            Controls.Add(btnMaximize);
            Controls.Add(btnMinimize);
            Controls.Add(btnClose);
            Controls.Add(pictureBoxMouseDown);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "BookingForm";
            Text = "BookingForm";
            Load += BookingForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMouseDown).EndInit();
            panelGhe.ResumeLayout(false);
            panelGhe.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBoxMouseDown;
        private Button btnClose;
        private Button btnMinimize;
        private Button btnMaximize;
        private Label label1;
        private ComboBox cbPhongChieu;
        private Button btnThanhToan;
        private Button btnHuy;
        private Button btnThoat;
        private TextBox txtTenKhachHang;
        private TextBox txtTenPhim;
        private Panel panelGhe;
        private Label lblTongTien;
        private Button btnA1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button btnA4;
        private Button btnA3;
        private Button btnA2;
        private Button btnB1;
        private Button btnA5;
        private Button btnB3;
        private Button btnB2;
        private Button btnC1;
        private Button btnB5;
        private Button btnB4;
        private Button btnC5;
        private Button btnC4;
        private Button btnC3;
        private Button btnC2;
        private Label lblTinhTien;
        private Label lblLoi;
        private RichTextBox rtbHoaDon;
        private Label label5;
        private Label label6;
        private Label label7;
    }
}
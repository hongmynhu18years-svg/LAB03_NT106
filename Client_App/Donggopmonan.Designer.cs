namespace LAB02
{
    partial class Donggopmonan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Donggopmonan));
            listView1 = new ListView();
            column0_stt = new ColumnHeader();
            column1_TenNguoi = new ColumnHeader();
            column2_TenMon = new ColumnHeader();
            column3_Thoigian = new ColumnHeader();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            send = new Button();
            add_picture = new Button();
            txt_TenMonAnDongGop = new TextBox();
            txt_TenNguoiDongGop = new TextBox();
            TenMonAnDongGop = new Label();
            TenNguoiDongGop = new Label();
            tabPage2 = new TabPage();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.BackColor = Color.DarkOliveGreen;
            listView1.Columns.AddRange(new ColumnHeader[] { column0_stt, column1_TenNguoi, column2_TenMon, column3_Thoigian });
            listView1.Cursor = Cursors.IBeam;
            listView1.Font = new Font("Times New Roman", 11F);
            listView1.ForeColor = Color.White;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Location = new Point(0, 0);
            listView1.Name = "listView1";
            listView1.Size = new Size(430, 176);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.SelectedIndexChanged += bang_danh_gia_SelectedIndexChanged;
            // 
            // column0_stt
            // 
            column0_stt.Text = "STT";
            column0_stt.Width = 40;
            // 
            // column1_TenNguoi
            // 
            column1_TenNguoi.Text = "Tên Người Đóng Góp";
            column1_TenNguoi.TextAlign = HorizontalAlignment.Center;
            column1_TenNguoi.Width = 140;
            // 
            // column2_TenMon
            // 
            column2_TenMon.Text = "Tên Món Ăn";
            column2_TenMon.TextAlign = HorizontalAlignment.Center;
            column2_TenMon.Width = 125;
            // 
            // column3_Thoigian
            // 
            column3_Thoigian.Text = "Thời Gian";
            column3_Thoigian.TextAlign = HorizontalAlignment.Center;
            column3_Thoigian.Width = 120;
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.None;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(88, 55);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(438, 208);
            tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(send);
            tabPage1.Controls.Add(add_picture);
            tabPage1.Controls.Add(txt_TenMonAnDongGop);
            tabPage1.Controls.Add(txt_TenNguoiDongGop);
            tabPage1.Controls.Add(TenMonAnDongGop);
            tabPage1.Controls.Add(TenNguoiDongGop);
            tabPage1.Location = new Point(4, 28);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(430, 176);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Nhập thông tin";
            tabPage1.UseVisualStyleBackColor = true;
            tabPage1.Click += tabPage1_Click;
            // 
            // send
            // 
            send.Cursor = Cursors.Hand;
            send.FlatStyle = FlatStyle.Flat;
            send.Location = new Point(244, 113);
            send.Name = "send";
            send.Size = new Size(168, 29);
            send.TabIndex = 5;
            send.Text = "📌 Send";
            send.UseVisualStyleBackColor = true;
            send.Click += send_Click;
            // 
            // add_picture
            // 
            add_picture.Cursor = Cursors.Hand;
            add_picture.FlatStyle = FlatStyle.Flat;
            add_picture.Location = new Point(15, 113);
            add_picture.Name = "add_picture";
            add_picture.Size = new Size(223, 29);
            add_picture.TabIndex = 4;
            add_picture.Text = "🔗 Thêm hình ảnh món ăn";
            add_picture.UseVisualStyleBackColor = true;
            add_picture.Click += add_picture_Click;
            // 
            // txt_TenMonAnDongGop
            // 
            txt_TenMonAnDongGop.Location = new Point(189, 57);
            txt_TenMonAnDongGop.MaxLength = 25;
            txt_TenMonAnDongGop.Name = "txt_TenMonAnDongGop";
            txt_TenMonAnDongGop.Size = new Size(223, 27);
            txt_TenMonAnDongGop.TabIndex = 3;
            txt_TenMonAnDongGop.TextChanged += txt_TenMonAnDongGop_TextChanged;
            // 
            // txt_TenNguoiDongGop
            // 
            txt_TenNguoiDongGop.Location = new Point(176, 19);
            txt_TenNguoiDongGop.MaxLength = 25;
            txt_TenNguoiDongGop.Name = "txt_TenNguoiDongGop";
            txt_TenNguoiDongGop.Size = new Size(236, 27);
            txt_TenNguoiDongGop.TabIndex = 2;
            txt_TenNguoiDongGop.TextChanged += txt_TenNguoiDongGop_TextChanged;
            // 
            // TenMonAnDongGop
            // 
            TenMonAnDongGop.AutoSize = true;
            TenMonAnDongGop.Location = new Point(15, 60);
            TenMonAnDongGop.Name = "TenMonAnDongGop";
            TenMonAnDongGop.Size = new Size(168, 20);
            TenMonAnDongGop.TabIndex = 1;
            TenMonAnDongGop.Text = "Tên món ăn đóng góp";
            TenMonAnDongGop.Click += TenMonAnDongGop_Click;
            // 
            // TenNguoiDongGop
            // 
            TenNguoiDongGop.AutoSize = true;
            TenNguoiDongGop.Location = new Point(15, 22);
            TenNguoiDongGop.Name = "TenNguoiDongGop";
            TenNguoiDongGop.Size = new Size(155, 20);
            TenNguoiDongGop.TabIndex = 0;
            TenNguoiDongGop.Text = "Tên người đóng góp";
            TenNguoiDongGop.Click += TenNguoiDongGop_Click;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(listView1);
            tabPage2.Location = new Point(4, 28);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(430, 176);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Danh sách món đã thêm";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // Donggopmonan
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(915, 431);
            Controls.Add(tabControl1);
            Font = new Font("Times New Roman", 13F);
            Margin = new Padding(4);
            Name = "Donggopmonan";
            Text = "s";
            Load += Donggopmonan_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ListView listView1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private ColumnHeader column0_stt;
        private ColumnHeader column1_TenNguoi;
        private Button add_picture;
        private TextBox txt_TenMonAnDongGop;
        private TextBox txt_TenNguoiDongGop;
        private Label TenMonAnDongGop;
        private Label TenNguoiDongGop;
        private Button send;
        private ColumnHeader column2_TenMon;
        private ColumnHeader column3_Thoigian;
    }
}
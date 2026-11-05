using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace LAB03
{
    public partial class Lab03_Bai04_BookingForm : Form
    {
        public Lab03_Bai04_BookingForm()
        {
            InitializeComponent();
        }

        private int currentFrame = 0;
        private int frameCount1 = 0;
        private int frameCount2 = 0;
        private FrameDimension dimension1;
        private FrameDimension dimension2;

        private void BookingForm_Load(object sender, EventArgs e)
        {
            rtbHoaDon.Visible = false;

            if (pictureBoxMouseDown != null)
            {
                Image gifImg1 = Properties.Resources.Gemini_Generated_Gif_j8cnlfj8cnlfj8cn;
                pictureBoxMouseDown.Image = gifImg1;
                pictureBoxMouseDown.SendToBack();

                if (gifImg1 != null && gifImg1.FrameDimensionsList.Length > 0)
                {
                    dimension1 = new FrameDimension(gifImg1.FrameDimensionsList[0]);
                    frameCount1 = gifImg1.GetFrameCount(dimension1);
                }
            }

            if (pictureBox1 != null)
            {
                Image gifImg2 = Properties.Resources.Gemini_Generated_Gif_j8cnlfj8cnlfj8cn;
                pictureBox1.Image = gifImg2;
                pictureBox1.SendToBack();

                if (gifImg2 != null && gifImg2.FrameDimensionsList.Length > 0)
                {
                    dimension2 = new FrameDimension(gifImg2.FrameDimensionsList[0]);
                    frameCount2 = gifImg2.GetFrameCount(dimension2);
                }
            }

            System.Windows.Forms.Timer timerGif = new System.Windows.Forms.Timer();
            timerGif.Interval = 120; 
            timerGif.Tick += (s, ev) =>
            {
                currentFrame++;

                if (pictureBoxMouseDown != null && pictureBoxMouseDown.Image != null && frameCount1 > 0)
                {
                    int f1 = currentFrame % frameCount1;
                    pictureBoxMouseDown.Image.SelectActiveFrame(dimension1, f1);
                    pictureBoxMouseDown.Invalidate();
                }

                if (pictureBox1 != null && pictureBox1.Image != null && frameCount2 > 0)
                {
                    int f2 = currentFrame % frameCount2;
                    pictureBox1.Image.SelectActiveFrame(dimension2, f2);
                    pictureBox1.Invalidate();
                }
            };
            timerGif.Start();
        }
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private static extern void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private static extern void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            }
        }

        private string tenPhim;
        private decimal giaVeChuan;
        private string tenKhachHang;
        private decimal tongTien = 0;

        public Lab03_Bai04_BookingForm(string phim, decimal giaChuan, string khachHang)
        {
            InitializeComponent();
            tenPhim = phim;
            giaVeChuan = giaChuan;
            tenKhachHang = khachHang;

            txtTenPhim.Text = tenPhim;
            txtTenKhachHang.Text = tenKhachHang;
            txtTenPhim.ReadOnly = true;
            txtTenKhachHang.ReadOnly = true;

            cbPhongChieu.Items.Clear();
            if (tenPhim == "Đào, phở và piano")
            {
                cbPhongChieu.Items.Add("Phòng chiếu 1");
                cbPhongChieu.Items.Add("Phòng chiếu 2");
                cbPhongChieu.Items.Add("Phòng chiếu 3");
            }
            else if (tenPhim == "Mai")
            {
                cbPhongChieu.Items.Add("Phòng chiếu 2");
                cbPhongChieu.Items.Add("Phòng chiếu 3");
            }
            else if (tenPhim == "Gặp lại chị bầu")
            {
                cbPhongChieu.Items.Add("Phòng chiếu 1");
            }
            else if (tenPhim == "Tarot")
            {
                cbPhongChieu.Items.Add("Phòng chiếu 3");
            }

            if (cbPhongChieu.Items.Count > 0)
                cbPhongChieu.SelectedIndex = 0;

            LoadTrangThaiGheDaDat();
        }

        private void LoadTrangThaiGheDaDat()
        {
            string seatPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "booked_seats5.txt");
            if (!File.Exists(seatPath)) return;

            string[] lines = File.ReadAllLines(seatPath);
            string phongChieuHienTai = cbPhongChieu.SelectedItem?.ToString() ?? "";

            for (int i = 0; i < lines.Length; i += 3)
            {
                if (i + 2 < lines.Length)
                {
                    string phimInFile = lines[i].Trim();
                    string phongInFile = lines[i + 1].Trim();
                    string[] danhSachGheDaMua = lines[i + 2].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (phimInFile == tenPhim && phongInFile == phongChieuHienTai)
                    {
                        foreach (string tenGhe in danhSachGheDaMua)
                        {
                            foreach (Control ctrl in panelGhe.Controls)
                            {
                                if (ctrl is Button btn && btn.Text.Trim() == tenGhe)
                                {
                                    btn.BackColor = Color.Red;                                         btn.ForeColor = Color.White;
                                    btn.Enabled = false;           
                                }
                            }
                        }
                    }
                }
            }
        }

        private void cbPhongChieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Control ctrl in panelGhe.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.BackColor = Color.FromKnownColor(KnownColor.Control);
                    btn.ForeColor = Color.Black;
                    btn.Enabled = true;
                }
            }

            LoadTrangThaiGheDaDat();
        }

        private void Ghe_Click(object sender, EventArgs e)
        {
            lblTinhTien.ForeColor = Color.White;

            Button btnGhe = sender as Button;
            if (btnGhe == null || !btnGhe.Enabled) return;

            
            if (btnGhe.BackColor != Color.Green)
            {
                btnGhe.BackColor = Color.Green;
                btnGhe.ForeColor = Color.White;
            }
            else 
            {
                btnGhe.BackColor = Color.FromKnownColor(KnownColor.Control);
                btnGhe.ForeColor = Color.Black;
            }

            TinhTongTien();
        }

        private void TinhTongTien()
        {
            tongTien = 0;

            foreach (Control ctrl in panelGhe.Controls)
            {
                if (ctrl is Button btn && btn.BackColor == Color.Green)
                {
                    string tenGhe = btn.Text.Trim(); 

                    if (tenGhe == "A1" || tenGhe == "A5" || tenGhe == "C1" || tenGhe == "C5")
                    {
                        tongTien += giaVeChuan * 0.25m; 
                    }
                    else if (tenGhe == "B2" || tenGhe == "B3" || tenGhe == "B4")
                    {
                        tongTien += giaVeChuan * 2.0m;  
                    }
                    else
                    {
                        tongTien += giaVeChuan * 1.0m;  
                    }
                }
            }

            lblTinhTien.Text = $"Tổng tiền: {tongTien:N0} VNĐ";
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (tongTien == 0)
            {
                lblTinhTien.ForeColor = Color.Red;
                lblTinhTien.Text = "Vui lòng chọn ít nhất một ghế ngồi!";
                return;
            }
            lblTinhTien.Text = "";

            string phongChieu = cbPhongChieu.SelectedItem.ToString();

            string danhSachGhe = "";
            foreach (Control ctrl in panelGhe.Controls)
            {
                if (ctrl is Button btn && btn.BackColor == Color.Green)
                {
                    danhSachGhe += btn.Text + " ";
                }
            }

            rtbHoaDon.Visible = true;

            
            rtbHoaDon.Clear();
            rtbHoaDon.AppendText("         HÓA ĐƠN ĐẶT VÉ XEM PHIM     \n");
            rtbHoaDon.AppendText($" - Khách hàng : {tenKhachHang}\n");
            rtbHoaDon.AppendText($" - Tên phim   : {tenPhim}\n");
            rtbHoaDon.AppendText($" - Phòng chiếu: {phongChieu}\n");
            rtbHoaDon.AppendText($" - Ghế đã chọn: {danhSachGhe}\n");
            rtbHoaDon.AppendText($" - Tổng tiền  : {tongTien:N0} VNĐ\n");
            rtbHoaDon.AppendText("     Cảm ơn quý khách và chúc xem phim vui vẻ!     ");

            
            int soLuongVeBan = 0;
            int tongSoGheThucTe = 0;

            foreach (Control ctrl in panelGhe.Controls)
            {
                if (ctrl is Button btn)
                {
                    tongSoGheThucTe++;
                    if (btn.BackColor == Color.Green)
                    {
                        soLuongVeBan++;
                    }
                }
            }

            try
            {
                string inputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input5.txt");

                using (StreamWriter writer = new StreamWriter(inputPath, true))
                {
                    writer.WriteLine(tenPhim);
                    writer.WriteLine(giaVeChuan);
                    writer.WriteLine(phongChieu);
                    writer.WriteLine(tongSoGheThucTe);
                    writer.WriteLine(soLuongVeBan);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi file input5.txt: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                string seatPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "booked_seats5.txt");
                using (StreamWriter seatWriter = new StreamWriter(seatPath, true))
                {
                    seatWriter.WriteLine(tenPhim);
                    seatWriter.WriteLine(phongChieu);
                    seatWriter.WriteLine(danhSachGhe);
                }

                MessageBox.Show("Thanh toán thành công và đã lưu trạng thái ghế!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu trạng thái ghế: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            foreach (Control ctrl in panelGhe.Controls)
            {
                if (ctrl is Button btn && btn.BackColor == Color.Green)
                {
                    btn.BackColor = Color.Red;
                    btn.ForeColor = Color.White;
                    btn.Enabled = false;
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            rtbHoaDon.Visible = false;
            foreach (Control ctrl in panelGhe.Controls)
            {
                if (ctrl is Button btn && btn.BackColor == Color.Green)
                {
                    btn.BackColor = Color.FromKnownColor(KnownColor.Control);
                    btn.ForeColor = Color.Black;
                }
            }
            tongTien = 0;
            lblTinhTien.Text = "Tổng tiền: 0 VNĐ";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
using Lab02;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace LAB03
{
    public partial class Lab03_Bai04 : Form
    {
        public Lab03_Bai04()
        {
            InitializeComponent();
        }

        private System.Windows.Forms.Timer timerBai05Gif;
        private int currentFrameBai05 = 0;
        private int frameCountBai05_1 = 0;
        private int frameCountBai05_2 = 0;
        private FrameDimension dimensionBai05_1;
        private FrameDimension dimensionBai05_2;

        private void Bai05_Load(object sender, EventArgs e)
        {
            txtCustomerName.Focus();

            try
            {
                if (pictureBox1 != null && System.IO.File.Exists("Resources/cinema.gif"))
                {
                    Image gifImg1 = Image.FromFile("Resources/cinema.gif");
                    pictureBox1.Image = gifImg1;
                    pictureBox1.SendToBack();

                    if (gifImg1 != null && gifImg1.FrameDimensionsList.Length > 0)
                    {
                        dimensionBai05_1 = new FrameDimension(gifImg1.FrameDimensionsList[0]);
                        frameCountBai05_1 = gifImg1.GetFrameCount(dimensionBai05_1);
                    }
                }

                if (pictureBoxMouseDown != null && System.IO.File.Exists("Resources/Gemini_Generated_Gif_j8cnlfj8cnlfj8cn.gif"))
                {
                    Image gifImg2 = Image.FromFile("Resources/Gemini_Generated_Gif_j8cnlfj8cnlfj8cn.gif");
                    pictureBoxMouseDown.Image = gifImg2;
                    pictureBoxMouseDown.SendToBack();

                    if (gifImg2 != null && gifImg2.FrameDimensionsList.Length > 0)
                    {
                        dimensionBai05_2 = new FrameDimension(gifImg2.FrameDimensionsList[0]);
                        frameCountBai05_2 = gifImg2.GetFrameCount(dimensionBai05_2);
                    }
                }
            }
            catch { }
        }

        private void Bai05_Shown(object sender, EventArgs e)
        {
            timerBai05Gif = new System.Windows.Forms.Timer();
            timerBai05Gif.Interval = 80;
            timerBai05Gif.Tick += (s, ev) =>
            {
                currentFrameBai05++;

                if (pictureBox1 != null && pictureBox1.Image != null && frameCountBai05_1 > 0)
                {
                    int f1 = currentFrameBai05 % frameCountBai05_1;
                    pictureBox1.Image.SelectActiveFrame(dimensionBai05_1, f1);
                    pictureBox1.Invalidate();
                }

                if (pictureBoxMouseDown != null && pictureBoxMouseDown.Image != null && frameCountBai05_2 > 0)
                {
                    int f2 = currentFrameBai05 % frameCountBai05_2;
                    pictureBoxMouseDown.Image.SelectActiveFrame(dimensionBai05_2, f2);
                    pictureBoxMouseDown.Invalidate();
                }
            };
            timerBai05Gif.Start();
        }

        private void KiemTraVaMoBooking(string tenPhim, decimal giaChuan)
        {
            string tenKhach = txtCustomerName.Text.Trim();
            if (string.IsNullOrWhiteSpace(tenKhach))
            {
                lblThongBao.ForeColor = Color.Red;
                lblThongBao.Text = "Vui lòng nhập tên khách hàng trước khi chọn phim!";
                txtCustomerName.Focus();
                return;
            }
            lblThongBao.Text = "";

            this.Hide();
            Lab03_Bai04_BookingForm bookingForm = new Lab03_Bai04_BookingForm(tenPhim, giaChuan, tenKhach);
            bookingForm.ShowDialog();
            this.Show();
        }

        private void btnDaoPhoPiano_Click(object sender, EventArgs e) => KiemTraVaMoBooking("Đào, phở và piano", 45000m);
        private void btnMai_Click(object sender, EventArgs e) => KiemTraVaMoBooking("Mai", 100000m);
        private void btnGapLaiChiBau_Click(object sender, EventArgs e) => KiemTraVaMoBooking("Gặp lại chị bầu", 70000m);
        private void btnTarot_Click(object sender, EventArgs e) => KiemTraVaMoBooking("Tarot", 90000m);

        private void txtCustomerName_TextChanged(object sender, EventArgs e) => lblThongBao.Text = "";
        private void btnMinimize_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;
        private void btnMaximize_Click(object sender, EventArgs e) => this.WindowState = (this.WindowState == FormWindowState.Maximized) ? FormWindowState.Normal : FormWindowState.Maximized;
        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private static extern void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private static extern void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void pictureBoxMouseDown_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            }
        }

        private void lnkAdmin_LinkClicked_Click(object sender, EventArgs e)
        {
            this.Hide();
            Lab03_Bai04_PasswordForm pwdForm = new Lab03_Bai04_PasswordForm();
            if (pwdForm.ShowDialog(this) == DialogResult.OK)
            {
                Lab03_Bai04_AdminForm adminForm = new Lab03_Bai04_AdminForm();
                adminForm.ShowDialog(this);
            }
            this.Show();
        }
    }
}
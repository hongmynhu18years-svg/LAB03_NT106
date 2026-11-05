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
    public partial class Lab03_Bai04_PasswordForm : Form
    {
        public Lab03_Bai04_PasswordForm()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private static extern void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private static extern void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);


        private void panelMouseDown_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            }
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
        private void KiemTraPassword(string password)
        {
            string Password = txtPassword.Text.Trim();
            if (string.IsNullOrWhiteSpace(Password))
            {
                lblThongBao.ForeColor = Color.Red;
                lblThongBao.Text = "Vui lòng nhập mật khẩu!";
                txtPassword.Focus();
                return;
            }

            lblThongBao.Text = "";


        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string Password = txtPassword.Text.Trim();
            if (string.IsNullOrWhiteSpace(Password))
            {
                lblThongBao.ForeColor = Color.Red;
                lblThongBao.Text = "Vui lòng nhập mật khẩu!";
                txtPassword.Focus();
                return;
            }

            lblThongBao.Text = "";
            string matKhauDung = "admin123"; 

            if (txtPassword.Text.Trim() == matKhauDung)
            {
                this.Hide(); 

                Lab03_Bai04_AdminForm adminForm = new Lab03_Bai04_AdminForm();
                adminForm.ShowDialog(this);

                this.Close(); 
            }
            else
            {
                lblThongBao.ForeColor = Color.Red;
                lblThongBao.Text = "Mật khẩu không chính xác! Vui lòng thử lại.";
                txtPassword.Focus();
                return;
            }
        }

        private int currentFrame = 0;
        private int frameCount = 0;
        private FrameDimension dimension;

        private void lblThongBa_Load(object sender, EventArgs e)
        {
            txtPassword.Focus();

            Image gifImg = Properties.Resources.cinema;
            bg.Image = gifImg;
            bg.SendToBack();

            if (gifImg != null && gifImg.FrameDimensionsList.Length > 0)
            {
                dimension = new FrameDimension(gifImg.FrameDimensionsList[0]);
                frameCount = gifImg.GetFrameCount(dimension);

                System.Windows.Forms.Timer timerGif = new System.Windows.Forms.Timer();
                timerGif.Interval = 120; 
                timerGif.Tick += (s, ev) =>
                {
                    if (bg.Image != null && frameCount > 0)
                    {
                        currentFrame = (currentFrame + 1) % frameCount;
                        bg.Image.SelectActiveFrame(dimension, currentFrame);
                        bg.Invalidate();
                    }
                };
                timerGif.Start();
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            lblThongBao.Text = "";

        }

        private void txtPassword_Click(object sender, EventArgs e)
        {
            lblThongBao.Text = "";

        }
    }
}

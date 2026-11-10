using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LAB03
{
    public partial class Lab03_Bai04_BookingForm : Form
    {
        private TcpClient tcpClient;
        private StreamReader reader;
        private StreamWriter writer;
        private Thread listenThread;

        private string tenPhim;
        private decimal giaVeChuan;
        private string tenKhachHang;
        private decimal tongTien = 0;
        private volatile string currentMovieKey = "";

        // Các biến cho hiệu ứng GIF (nếu có sẵn trong form của bạn)
        private int currentFrame = 0;
        private int frameCount1 = 0;
        private int frameCount2 = 0;
        private FrameDimension dimension1;
        private FrameDimension dimension2;

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
            rtbHoaDon.Visible = false;

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
            UpdateCurrentMovieKey();

            // Kết nối khi form đã có Handle để Invoke từ luồng ngầm không bị lỗi
            this.Load += (s, e) => ConnectToServerAndListen();
        }

        private void UpdateCurrentMovieKey()
        {
            string phong = cbPhongChieu.SelectedItem?.ToString() ?? "Phòng chiếu 1";
            currentMovieKey = $"{tenPhim}_{phong}";
        }

        private string GetCurrentMovieKey()
        {
            return currentMovieKey;
        }

        private void RunOnUI(Action action)
        {
            if (IsDisposed || !IsHandleCreated) return;
            try { Invoke(action); }
            catch (ObjectDisposedException) { }
            catch (InvalidOperationException) { }
        }

        private void ConnectToServerAndListen()
        {
            for (int attempt = 0; attempt < 10; attempt++)
            {
                try
                {
                    tcpClient = new TcpClient("10.139.209.54", 8080);
                    NetworkStream stream = tcpClient.GetStream();
                    UTF8Encoding utf8 = new UTF8Encoding(false);
                    reader = new StreamReader(stream, utf8);
                    writer = new StreamWriter(stream, utf8) { AutoFlush = true };

                    listenThread = new Thread(ListenFromServer);
                    listenThread.IsBackground = true;
                    listenThread.Start();

                    RequestServerSeats();
                    return;
                }
                catch
                {
                    Thread.Sleep(200); // Chờ Server khởi động xong
                }
            }

            MessageBox.Show("Không thể kết nối đến Server 8080! Hãy kiểm tra lại Server.",
                "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void SendToServer(string message)
        {
            lock (this)
            {
                writer.WriteLine(message);
            }
        }

        private void RequestServerSeats()
        {
            if (writer == null) return;
            try { SendToServer($"GET_SEATS|{GetCurrentMovieKey()}"); }
            catch { }
        }

        private void ListenFromServer()
        {
            try
            {
                string message;
                while ((message = reader.ReadLine()) != null)
                {
                    string[] parts = message.Split('|');
                    string command = parts[0];

                    if (command == "SEATS" || command == "SEAT_UPDATED")
                    {
                        if (parts.Length > 2 && parts[1] == GetCurrentMovieKey())
                        {
                            string seatsStr = parts[2];
                            RunOnUI(() => ApplyBookedSeatsToUI(seatsStr));
                        }
                    }
                    else if (command == "SUCCESS")
                    {
                        string msg = parts.Length > 1 ? parts[1] : "Đặt vé thành công!";
                        RunOnUI(() =>
                        {
                            HienThiHoaDonThanhCong();
                            LockSelectedSeats();
                            MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        });
                    }
                    else if (command == "FAILED")
                    {
                        string msg = parts.Length > 1 ? parts[1] : "Ghế đã có người khác đặt!";
                        RunOnUI(() =>
                        {
                            MessageBox.Show(msg, "Cảnh báo tranh chấp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            RequestServerSeats();
                        });
                    }
                }
            }
            catch { }
        }

        // Sau khi đặt thành công: khóa các ghế mình vừa chọn
        private void LockSelectedSeats()
        {
            foreach (Control ctrl in panelGhe.Controls)
            {
                if (ctrl is Button btn && btn.BackColor == Color.Green)
                {
                    btn.BackColor = Color.Red;
                    btn.ForeColor = Color.White;
                    btn.Enabled = false;
                }
            }
            tongTien = 0;
            lblTinhTien.ForeColor = Color.White;
            lblTinhTien.Text = "Tổng tiền: 0 VNĐ";
        }

        private void ApplyBookedSeatsToUI(string seatsStr)
        {
            HashSet<string> bookedSeats = new HashSet<string>(
                seatsStr.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            bool conflict = false;

            foreach (Control ctrl in panelGhe.Controls)
            {
                if (ctrl is Button btn)
                {
                    string seatName = btn.Text.Trim();
                    if (bookedSeats.Contains(seatName))
                    {
                        if (btn.BackColor == Color.Green) conflict = true; // ghế đang chọn bị người khác đặt trước
                        btn.BackColor = Color.Red;
                        btn.ForeColor = Color.White;
                        btn.Enabled = false;
                    }
                    else if (btn.BackColor != Color.Green)
                    {
                        btn.BackColor = Color.FromKnownColor(KnownColor.Control);
                        btn.ForeColor = Color.Black;
                        btn.Enabled = true;
                    }
                }
            }

            TinhTongTien();

            if (conflict)
            {
                lblTinhTien.ForeColor = Color.Red;
                lblTinhTien.Text = "Ghế bạn chọn vừa có người khác đặt!";
            }
        }

        private void cbPhongChieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCurrentMovieKey();

            foreach (Control ctrl in panelGhe.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.BackColor = Color.FromKnownColor(KnownColor.Control);
                    btn.ForeColor = Color.Black;
                    btn.Enabled = true;
                }
            }
            tongTien = 0;
            lblTinhTien.ForeColor = Color.White;
            lblTinhTien.Text = "Tổng tiền: 0 VNĐ";
            rtbHoaDon.Visible = false;

            RequestServerSeats();
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
                        tongTien += giaVeChuan * 0.25m;
                    else if (tenGhe == "B2" || tenGhe == "B3" || tenGhe == "B4")
                        tongTien += giaVeChuan * 2.0m;
                    else
                        tongTien += giaVeChuan * 1.0m;
                }
            }

            lblTinhTien.ForeColor = Color.White;
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

            List<string> listGheChon = new List<string>();
            foreach (Control ctrl in panelGhe.Controls)
            {
                if (ctrl is Button btn && btn.BackColor == Color.Green)
                    listGheChon.Add(btn.Text.Trim());
            }

            try
            {
                SendToServer($"BOOK|{GetCurrentMovieKey()}|{string.Join(",", listGheChon)}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi giao tiếp với Server: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HienThiHoaDonThanhCong()
        {
            string phongChieu = cbPhongChieu.SelectedItem.ToString();
            string danhSachGhe = "";
            foreach (Control ctrl in panelGhe.Controls)
            {
                if (ctrl is Button btn && btn.BackColor == Color.Green)
                    danhSachGhe += btn.Text + " ";
            }

            rtbHoaDon.Visible = true;
            rtbHoaDon.Clear();
            rtbHoaDon.AppendText("       HÓA ĐƠN ĐẶT VÉ XEM PHIM      \n");
            rtbHoaDon.AppendText($" - Khách hàng : {tenKhachHang}\n");
            rtbHoaDon.AppendText($" - Tên phim   : {tenPhim}\n");
            rtbHoaDon.AppendText($" - Phòng chiếu: {phongChieu}\n");
            rtbHoaDon.AppendText($" - Ghế đã chọn: {danhSachGhe}\n");
            rtbHoaDon.AppendText($" - Tổng tiền  : {tongTien:N0} VNĐ\n");
            rtbHoaDon.AppendText("   Cảm ơn quý khách và chúc xem phim vui vẻ!   ");
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
            lblTinhTien.ForeColor = Color.White;
            lblTinhTien.Text = "Tổng tiền: 0 VNĐ";
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try { tcpClient?.Close(); } catch { }
            base.OnFormClosing(e);
        }

        private void btnThoat_Click(object sender, EventArgs e) => this.Close();

        private void btnMinimize_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;
        private void btnMaximize_Click(object sender, EventArgs e) => this.WindowState = (this.WindowState == FormWindowState.Maximized) ? FormWindowState.Normal : FormWindowState.Maximized;
        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            }
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private static extern void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private static extern void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
    }
}
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace LAB03
{
    public partial class Lab03_Bai01_UDPClient : Form
    {
        public Lab03_Bai01_UDPClient()
        {
            InitializeComponent();

            // Địa chỉ mặc định để chạy thử trên cùng máy
            tbxRemoteIP.Text = "127.0.0.1";
            tbxRemotePort.Text = "9000";
        }

        // Nút Send
        private void btnSend_Click(object sender, EventArgs e)
        {
            string ip = tbxRemoteIP.Text.Trim();
            string portText = tbxRemotePort.Text.Trim();
            string message = rtbMessage.Text;

            if (!IPAddress.TryParse(ip, out IPAddress? remoteAddress))
            {
                MessageBox.Show("Địa chỉ IP không hợp lệ!");
                return;
            }

            if (!int.TryParse(portText, out int port)
                || port < 1 || port > 65535)
            {
                MessageBox.Show("Port phải nằm trong khoảng 1-65535!");
                return;
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("Vui lòng nhập thông điệp cần gửi!");
                rtbMessage.Focus();
                return;
            }

            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);

                using (UdpClient client = new UdpClient())
                {
                    client.Send(
                        data,
                        data.Length,
                        new IPEndPoint(remoteAddress, port));
                }

                MessageBox.Show(
                    "Đã gửi thông điệp qua UDP!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (SocketException ex)
            {
                MessageBox.Show(
                    "Lỗi gửi dữ liệu: " + ex.Message,
                    "Lỗi UDP Client",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Nút Thoát
        private void btnClientExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
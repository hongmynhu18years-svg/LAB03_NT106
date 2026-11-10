
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

            // Địa chỉ mặc định khi Client và Server chạy cùng máy
            tbxRemoteIP.Text = "127.0.0.1";
            tbxRemotePort.Text = "9000";
        }

        // 1. Nút Send
        private void btnSend_Click(object sender, EventArgs e)
        {
            string ip = tbxRemoteIP.Text.Trim();
            string portText = tbxRemotePort.Text.Trim();
            string message = rtbMessage.Text.Trim();

            // Kiểm tra địa chỉ IP
            if (!IPAddress.TryParse(ip, out IPAddress? remoteAddress))
            {
                MessageBox.Show(
                    "Địa chỉ IP không hợp lệ!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tbxRemoteIP.Focus();
                return;
            }

            // Kiểm tra Port
            if (!int.TryParse(portText, out int port)
                || port < 1 || port > 65535)
            {
                MessageBox.Show(
                    "Port phải nằm trong khoảng 1-65535!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tbxRemotePort.Focus();
                return;
            }

            // Kiểm tra nội dung
            if (string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show(
                    "Vui lòng nhập thông điệp cần gửi!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                rtbMessage.Focus();
                return;
            }

            try
            {
                // Chuyển thông điệp thành mảng byte UTF-8
                byte[] data = Encoding.UTF8.GetBytes(message);

                // Khởi tạo UDP Client và gửi dữ liệu
                using (UdpClient client = new UdpClient())
                {
                    client.Send(
                        data,
                        data.Length,
                        new IPEndPoint(remoteAddress, port)
                    );
                }

                MessageBox.Show(
                    "Đã gửi thông điệp qua UDP!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Xóa nội dung sau khi gửi
                rtbMessage.Clear();
                rtbMessage.Focus();
            }
            catch (SocketException ex)
            {
                MessageBox.Show(
                    "Lỗi gửi dữ liệu UDP:\n" + ex.Message,
                    "Lỗi UDP Client",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Đã xảy ra lỗi:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // 2. Nút Thoát
        private void btnClientExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

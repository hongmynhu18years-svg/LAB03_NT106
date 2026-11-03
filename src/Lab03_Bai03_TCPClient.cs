using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace LAB03
{
    public partial class Lab03_Bai03_TCPClient : Form
    {
        // Khai báo kết nối
        private TcpClient? tcpClient;
        private NetworkStream? stream;

        public Lab03_Bai03_TCPClient()
        {
            InitializeComponent();
        }

        // 1. Hàm Kết nối
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (tcpClient == null || !tcpClient.Connected)
                {
                    tcpClient = new TcpClient();
                    string ip = txtIP.Text.Trim();
                    int port = int.Parse(txtPort.Text.Trim());

                    tcpClient.Connect(ip, port);
                    stream = tcpClient.GetStream();

                    MessageBox.Show("Kết nối đến Server thành công!", "Thông báo");
                    btnConnect.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi");
            }
        }

        // 2. Hàm Gửi dữ liệu
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (tcpClient != null && tcpClient.Connected)
            {
                try
                {
                    string message = txtMessage.Text + "\n";
                    byte[] data = Encoding.ASCII.GetBytes(message);

                    stream?.Write(data, 0, data.Length);

                    txtMessage.Clear();
                    txtMessage.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi gửi dữ liệu: " + ex.Message, "Lỗi");
                }
            }
            else
            {
                MessageBox.Show("Chưa kết nối đến Server!", "Cảnh báo");
            }
        }

        // 3. Hàm Ngắt kết nối
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (tcpClient != null) // Chỉ cần kiểm tra khác null là được, không cần Connected
            {
                try
                {
                    // Gửi tín hiệu quit (nếu muốn)
                    if (tcpClient.Connected && stream != null)
                    {
                        string quitMsg = "quit\n";
                        byte[] data = Encoding.ASCII.GetBytes(quitMsg);
                        stream.Write(data, 0, data.Length);
                    }

                    stream?.Close();
                    tcpClient.Close();
                    tcpClient = null;

                    MessageBox.Show("Đã ngắt kết nối khỏi Server.", "Thông báo");
                    btnConnect.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi ngắt kết nối: " + ex.Message);
                }
            }
        }
    }
}
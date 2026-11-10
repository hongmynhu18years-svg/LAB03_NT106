using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LAB03
{
    public partial class Lab03_Bai02 : Form
    {
        private TcpListener listener;
        private bool isListening = false;

        public Lab03_Bai02()
        {
            InitializeComponent();
            // Đảm bảo dừng Server khi tắt form để giải phóng Port
            this.FormClosing += Lab03_Bai02_FormClosing;
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            if (!isListening)
            {
                isListening = true;
                btnListen.Text = "Listening...";
                btnListen.Enabled = false;

                // Khởi chạy Server trên một luồng nền riêng biệt để không làm đơ giao diện WinForms
                Thread serverThread = new Thread(StartServer);
                serverThread.IsBackground = true;
                serverThread.Start();
            }
        }

        private void StartServer()
        {
            try
            {
                // IPAddress.Any cho phép lắng nghe kết nối từ mọi card mạng (cả Localhost và máy khác trong mạng LAN)
                listener = new TcpListener(IPAddress.Any, 9008);
                listener.Start();

                string localIP = GetLocalIPAddress();
                AddMessage($"Server đang chạy tại IP: {localIP} - Port: 8080");

                while (isListening)
                {
                    // Chấp nhận kết nối từ Client (Telnet hoặc chương trình Client khác)
                    TcpClient client = listener.AcceptTcpClient();

                    string clientIP = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
                    AddMessage($"Có Client mới kết nối từ IP: {clientIP}");

                    // Lấy NetworkStream để đọc dữ liệu gửi lên từ Client
                    NetworkStream stream = client.GetStream();
                    StreamReader reader = new StreamReader(stream, Encoding.ASCII);

                    string line;
                    // Vòng lặp đọc từng dòng liên tục cho đến khi Client ngắt kết nối
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Nếu dòng rỗng hoặc chỉ chứa khoảng trắng thì bỏ qua
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        AddMessage($"{clientIP}: {line}");
                    }

                    client.Close();
                    AddMessage($"Client {clientIP} đã ngắt kết nối.");
                }
            }
            catch (Exception ex)
            {
                if (isListening)
                {
                    AddMessage("Lỗi Server: " + ex.Message);
                }
            }
        }

        // Hàm bổ trợ tự động lấy địa chỉ IP LAN hiện tại của máy chủ
        private string GetLocalIPAddress()
        {
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
            }
            catch
            {
                // Bỏ qua nếu có lỗi mạng
            }
            return "127.0.0.1";
        }

        private void AddMessage(string message)
        {
            if (this.IsDisposed || lvMessages.IsDisposed) return;

            // Đảm bảo cập nhật giao diện ListView an toàn từ luồng nền thông qua Invoke
            if (lvMessages.InvokeRequired)
            {
                lvMessages.Invoke(new Action<string>(AddMessage), message);
            }
            else
            {
                string time = DateTime.Now.ToString("HH:mm:ss");
                string displayText = $"[{time}] {message}";

                lvMessages.Items.Add(new ListViewItem(displayText));

                if (lvMessages.Items.Count > 0)
                {
                    lvMessages.EnsureVisible(lvMessages.Items.Count - 1);
                }
            }
        }

        private void Lab03_Bai02_FormClosing(object sender, FormClosingEventArgs e)
        {
            isListening = false;
            try
            {
                listener?.Stop();
            }
            catch
            {
                // Bỏ qua lỗi khi đóng
            }
        }
    }
}
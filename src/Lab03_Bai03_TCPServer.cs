using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LAB03
{
    public partial class Lab03_Bai03_TCPServer : Form
    {
        private TcpListener? listener;
        private bool isListening = false;

        public Lab03_Bai03_TCPServer()
        {
            InitializeComponent();
            lvServer.FullRowSelect = true;
        }

        // 1. Sự kiện nút Listen
        private void btnListen_Click(object sender, EventArgs e)
        {
            if (!isListening)
            {
                try
                {
                    // Lấy port từ giao diện
                    int port = int.Parse(txtPort.Text.Trim());

                    isListening = true;
                    btnListen.Text = "Listening...";
                    btnListen.Enabled = false;
                    txtPort.Enabled = false;

                    // Tạo Thread để lắng nghe không làm treo giao diện
                    Thread serverThread = new Thread(() => StartServer(port));
                    serverThread.IsBackground = true;
                    serverThread.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khởi tạo: " + ex.Message);
                }
            }
        }

        // 2. Hàm xử lý lắng nghe (chạy ngầm)
        private void StartServer(int port)
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, 9090);
                listener.Start();
                AddMessage("Server started! Waiting for connection...");

                while (isListening)
                {
                    // Chấp nhận kết nối từ Client
                    TcpClient client = listener.AcceptTcpClient();
                    AddMessage($"Connection accepted from {client.Client.RemoteEndPoint}");

                    // Tạo luồng đọc dữ liệu
                    NetworkStream stream = client.GetStream();
                    byte[] buffer = new byte[1024];
                    int bytesRead;

                    // Đọc dữ liệu liên tục từ Client
                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                        AddMessage($"From client: {message.Trim()}");
                    }

                    client.Close();
                    AddMessage("Client disconnected.");
                }
            }
            catch (Exception ex)
            {
                if (isListening) // Chỉ báo lỗi nếu không phải do tự tắt
                    AddMessage("Lỗi Server: " + ex.Message);
            }
        }

        // 3. Hàm cập nhật ListView an toàn từ Thread khác
        private void AddMessage(string message)
        {
            if (lvServer.InvokeRequired)
            {
                lvServer.Invoke(new Action<string>(AddMessage), message);
            }
            else
            {
                lvServer.Items.Add(new ListViewItem(message));
                lvServer.EnsureVisible(lvServer.Items.Count - 1); // Tự cuộn xuống
            }
        }

        // 4. Đảm bảo tắt Server an toàn khi đóng Form
        private void Lab03_Bai03_TCPServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            isListening = false;
            listener?.Stop();
        }
    }
}
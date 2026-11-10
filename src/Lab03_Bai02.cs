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
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            if (!isListening)
            {
                isListening = true;
                btnListen.Text = "Listening...";
                btnListen.Enabled = false;

                // Luồng chính để đón Client mới
                Thread serverThread = new Thread(StartServer);
                serverThread.IsBackground = true;
                serverThread.Start();
            }
        }

        // Hàm chạy trên luồng chính, chỉ làm nhiệm vụ đón kết nối
        private void StartServer()
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, 8080);
                listener.Start();
                AddMessage("Telnet running on 127.0.0.1:8080");

                while (isListening)
                {
                    // Chờ Client kết nối (Hàm này sẽ block cho đến khi có Client)
                    TcpClient client = listener.AcceptTcpClient();
                    AddMessage($"New client connected: {client.Client.RemoteEndPoint}");

                    // TẠO LUỒNG MỚI cho Client này để luồng chính quay lại đón Client khác
                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.IsBackground = true;
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                if (isListening) AddMessage("Lỗi: " + ex.Message);
            }
        }

        // Hàm xử lý dữ liệu riêng cho từng Client
        private void HandleClient(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream, Encoding.ASCII);
                string line;

                // Vòng lặp đọc dữ liệu từ Client này
                while ((line = reader.ReadLine()) != null)
                {
                    AddMessage(line);
                }
            }
            catch (Exception)
            {
                // Bỏ qua lỗi khi Client ngắt kết nối đột ngột
            }
            finally
            {
                // Đảm bảo đóng kết nối khi xong việc
                if (client.Connected)
                {
                    AddMessage("Client disconnected.");
                }
                client.Close();
            }
        }

        // Hàm cập nhật giao diện an toàn (Thread-safe)
        private void AddMessage(string message)
        {
            if (this.IsDisposed || lvMessages.IsDisposed) return;

            if (lvMessages.InvokeRequired)
            {
                lvMessages.Invoke(new Action<string>(AddMessage), message);
            }
            else
            {
                string time = DateTime.Now.ToString("HH:mm:ss");
                string displayText = $"[{time}] {message}";

                lvMessages.Items.Add(new ListViewItem(displayText));
                lvMessages.EnsureVisible(lvMessages.Items.Count - 1);
            }
        }

        // Dọn dẹp tài nguyên khi đóng Form
        private void Lab03_Bai02_FormClosing(object sender, FormClosingEventArgs e)
        {
            isListening = false;
            listener?.Stop();
        }
    }
}
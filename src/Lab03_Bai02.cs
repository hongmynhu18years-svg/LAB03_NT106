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

                Thread serverThread = new Thread(StartServer);
                serverThread.IsBackground = true;
                serverThread.Start();
            }
        }

        private void StartServer()
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, 9008);
                listener.Start();
                AddMessage("Telnet running on 127.0.0.1:9008");

                while (isListening)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    AddMessage("New client connected!");

                    // Sử dụng StreamReader để đọc từng dòng khi ấn Enter
                    NetworkStream stream = client.GetStream();
                    StreamReader reader = new StreamReader(stream, Encoding.ASCII);

                    string line;
                    // Vòng lặp đọc từng dòng cho đến khi Client ngắt kết nối
                    while ((line = reader.ReadLine()) != null)
                    {
                        AddMessage(line);
                    }

                    client.Close();
                }
            }
            catch (Exception ex)
            {
                if (isListening) AddMessage("Lỗi: " + ex.Message);
            }
        }

        private void AddMessage(string message)
        {
            // Kiểm tra an toàn trước khi cập nhật giao diện
            if (this.IsDisposed || lvMessages.IsDisposed) return;

            if (lvMessages.InvokeRequired)
            {
                lvMessages.Invoke(new Action<string>(AddMessage), message);
            }
            else
            {
                string time = DateTime.Now.ToString("HH:mm:ss");
                string displayText = $"[{time}] {message}";

                // Thêm vào ListView
                lvMessages.Items.Add(new ListViewItem(displayText));

                // Tự động cuộn xuống dòng mới nhất
                lvMessages.EnsureVisible(lvMessages.Items.Count - 1);
            }
        }
    }
}
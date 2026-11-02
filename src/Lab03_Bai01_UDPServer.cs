using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB03
{
    public partial class Lab03_Bai01_UDPServer : Form
    {
        private UdpClient? udpServer;
        private CancellationTokenSource? cancellationTokenSource;

        public Lab03_Bai01_UDPServer()
        {
            InitializeComponent();
        }

        // Nút Listen
        private async void btnListen_Click(object sender, EventArgs e)
        {
            if (udpServer != null)
            {
                MessageBox.Show("Server đang lắng nghe!");
                return;
            }

            if (!int.TryParse(tbxServerPort.Text.Trim(), out int port)
                || port < 1 || port > 65535)
            {
                MessageBox.Show("Vui lòng nhập Port hợp lệ (1-65535)!");
                return;
            }

            try
            {
                udpServer = new UdpClient(port);
                cancellationTokenSource = new CancellationTokenSource();

                btnListen.Enabled = false;
                tbxServerPort.Enabled = false;

                AppendMessage($"Server đang lắng nghe tại Port {port}...\r\n");

                await ReceiveMessagesAsync(cancellationTokenSource.Token);
            }
            catch (SocketException ex)
            {
                MessageBox.Show(
                    "Không thể khởi động Server: " + ex.Message,
                    "Lỗi UDP Server",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (ObjectDisposedException)
            {
                // Socket đã được đóng khi người dùng thoát.
            }
            finally
            {
                udpServer?.Dispose();
                udpServer = null;

                cancellationTokenSource?.Dispose();
                cancellationTokenSource = null;

                if (!IsDisposed && !Disposing)
                {
                    btnListen.Enabled = true;
                    tbxServerPort.Enabled = true;
                }
            }
        }

        // Nhận dữ liệu từ Client
        private async Task ReceiveMessagesAsync(
            CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                UdpReceiveResult result;

                try
                {
                    UdpClient? server = udpServer;

                    if (server == null)
                        break;

                    result = await server.ReceiveAsync(cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (ObjectDisposedException)
                {
                    break;
                }
                catch (SocketException)
                {
                    if (cancellationToken.IsCancellationRequested)
                        break;

                    throw;
                }

                string message = Encoding.UTF8.GetString(result.Buffer);

                string remoteIP = result.RemoteEndPoint.Address.ToString();
                int remotePort = result.RemoteEndPoint.Port;

                AppendMessage(
                    $"[{DateTime.Now:HH:mm:ss}] " +
                    $"{remoteIP}:{remotePort}\r\n" +
                    $"{message}\r\n\r\n");
            }
        }

        // Cập nhật giao diện an toàn giữa các luồng
        private void AppendMessage(string message)
{
    // Kiểm tra xem Form đã bị hủy hay Handle chưa được tạo / đang đóng chưa
    if (IsDisposed || !IsHandleCreated)
        return;

    if (InvokeRequired)
    {
        try
        {
            // Dùng Invoke hoặc BeginInvoke nhưng bọc try-catch bên trong delegate
            BeginInvoke(new Action(() =>
            {
                try
                {
                    if (!IsDisposed && IsHandleCreated)
                    {
                        lstReceivedMessages.Items.Add(message);

                        // Tự động cuộn xuống mục mới nhất trong ListView
                        int lastIndex = lstReceivedMessages.Items.Count - 1;
                        if (lastIndex >= 0)
                        {
                            lstReceivedMessages.EnsureVisible(lastIndex);
                        }
                    }
                }
                catch (ObjectDisposedException)
                {
                    // Form đã đóng trong lúc chờ thực thi lệnh
                }
                catch (InvalidOperationException)
                {
                    // Handle đã bị hủy
                }
            }));
        }
        catch (InvalidOperationException)
        {
            // Bắt lỗi nếu gọi BeginInvoke vào thời điểm Handle đang bị hủy
        }
    }
    else
    {
        if (!IsDisposed && IsHandleCreated)
        {
            lstReceivedMessages.Items.Add(message);
        }
    }
}

        // Nút Thoát
        private void btnServerExit_Click(object sender, EventArgs e)
        {
            cancellationTokenSource?.Cancel();
            udpServer?.Dispose();
            Close();
        }

        // Đóng Server khi đóng Form
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            cancellationTokenSource?.Cancel();
            udpServer?.Dispose();

            base.OnFormClosing(e);
        }
    }
}

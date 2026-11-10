
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

            // Cấu hình ListView hiển thị thông điệp
            lstReceivedMessages.View = View.Details;
            lstReceivedMessages.FullRowSelect = true;
            lstReceivedMessages.GridLines = false;
            lstReceivedMessages.MultiSelect = false;
            lstReceivedMessages.HeaderStyle = ColumnHeaderStyle.None;

            lstReceivedMessages.Columns.Clear();
            lstReceivedMessages.Columns.Add(
                "Messages",
                Math.Max(100, lstReceivedMessages.ClientSize.Width - 5)
            );

            // Cập nhật độ rộng cột khi thay đổi kích thước
            lstReceivedMessages.Resize += (s, e) =>
            {
                if (lstReceivedMessages.Columns.Count > 0)
                {
                    lstReceivedMessages.Columns[0].Width =
                        Math.Max(100, lstReceivedMessages.ClientSize.Width - 5);
                }
            };
        }

        // 1. Nút Listen
        private async void btnListen_Click(object sender, EventArgs e)
        {
            // Kiểm tra Server đã chạy chưa
            if (udpServer != null)
            {
                MessageBox.Show(
                    "Server đang lắng nghe!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            // Kiểm tra Port
            if (!int.TryParse(tbxServerPort.Text.Trim(), out int port)
                || port < 1 || port > 65535)
            {
                MessageBox.Show(
                    "Vui lòng nhập Port hợp lệ (1-65535)!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                // Khởi tạo UDP Server
                udpServer = new UdpClient(port);
                cancellationTokenSource = new CancellationTokenSource();

                btnListen.Enabled = false;
                tbxServerPort.Enabled = false;

                AppendMessage(
                    $"[{DateTime.Now:HH:mm:ss}] SERVER: Đang lắng nghe tại Port {port}"
                );

                // Bắt đầu nhận dữ liệu
                await ReceiveMessagesAsync(cancellationTokenSource.Token);
            }
            catch (SocketException ex)
            {
                AppendMessage(
                    $"[{DateTime.Now:HH:mm:ss}] LỖI SERVER: {ex.Message}"
                );

                MessageBox.Show(
                    "Không thể khởi động Server!\n" + ex.Message,
                    "Lỗi UDP Server",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (ObjectDisposedException)
            {
                // Socket đã được đóng
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

        // 2. Nhận dữ liệu liên tục từ Client
        private async Task ReceiveMessagesAsync(
            CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                UdpClient? server = udpServer;

                if (server == null)
                    break;

                try
                {
                    // Chờ nhận một gói tin UDP
                    UdpReceiveResult result =
                        await server.ReceiveAsync(cancellationToken);

                    // Giải mã dữ liệu UTF-8
                    string message =
                        Encoding.UTF8.GetString(result.Buffer);

                    string remoteIP =
                        result.RemoteEndPoint.Address.ToString();

                    int remotePort =
                        result.RemoteEndPoint.Port;

                    // Hiển thị thông điệp
                    AppendMessage(
                        $"[{DateTime.Now:HH:mm:ss}] {remoteIP}:{remotePort} - {message}"
                    );
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (ObjectDisposedException)
                {
                    break;
                }
                catch (SocketException ex)
                {
                    if (cancellationToken.IsCancellationRequested)
                        break;

                    AppendMessage(
                        $"[{DateTime.Now:HH:mm:ss}] LỖI NHẬN DỮ LIỆU: {ex.Message}"
                    );
                    break;
                }
            }
        }

        // 3. Hiển thị thông điệp lên ListView
        private void AppendMessage(string message)
        {
            if (IsDisposed || !IsHandleCreated)
                return;

            if (InvokeRequired)
            {
                try
                {
                    BeginInvoke(new Action<string>(AppendMessage), message);
                }
                catch (InvalidOperationException)
                {
                    // Form đang đóng
                }

                return;
            }

            if (IsDisposed || lstReceivedMessages.IsDisposed)
                return;

            // Đảm bảo mỗi thông điệp nằm trên một dòng
            message = message
                .Replace("\r", " ")
                .Replace("\n", " ");

            ListViewItem item = new ListViewItem(message);

            lstReceivedMessages.BeginUpdate();

            try
            {
                lstReceivedMessages.Items.Add(item);

                // Tự động cuộn đến thông điệp mới nhất
                lstReceivedMessages.EnsureVisible(
                    lstReceivedMessages.Items.Count - 1
                );
            }
            finally
            {
                lstReceivedMessages.EndUpdate();
            }
        }

        // 4. Nút Thoát
        private void btnServerExit_Click(object sender, EventArgs e)
        {
            cancellationTokenSource?.Cancel();
            udpServer?.Dispose();

            Close();
        }

        // 5. Đóng Server khi đóng Form
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            cancellationTokenSource?.Cancel();
            udpServer?.Dispose();

            base.OnFormClosing(e);
        }
    }
}

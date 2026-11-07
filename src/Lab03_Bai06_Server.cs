using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;


namespace LAB03
{
    public partial class Lab03_Bai06_Server : Form
    {
        private const int Port = 8006;
        private readonly HashSet<TcpClient> connections = new HashSet<TcpClient>();

        private readonly Dictionary<string, Session> users = new Dictionary<string, Session>(StringComparer.OrdinalIgnoreCase);

        // Xử lý lần lượt các lượt gửi và cập nhật danh sách online.
        private readonly SemaphoreSlim dispatch = new SemaphoreSlim(1, 1);

        private TcpListener listener;
        private bool closing;

        private sealed class Session
        {
            public TcpClient Client { get; }
            public StreamWriter Writer { get; }

            public Session(TcpClient client, StreamWriter writer)
            {
                Client = client;
                Writer = writer;
            }
        }

        public Lab03_Bai06_Server()
        {
            InitializeComponent();
        }

        private void Lab03_Bai06_Server_Load(object sender, EventArgs e)
        {

        }

        private async void btnListen_Click(object sender, EventArgs e)
        {
            if (listener != null)
                return;

            var currentListener = new TcpListener(IPAddress.Any, Port);
            listener = currentListener;
            btnListen.Enabled = false;

            try
            {
                currentListener.Start();
                Log($"Đang lắng nghe TCP cổng {Port}.");

                while (!closing)
                {
                    TcpClient client = await currentListener.AcceptTcpClientAsync();

                    if (closing)
                    {
                        client.Close();
                        break;
                    }

                    connections.Add(client);

                    // Giao kết nối này cho một tác vụ xử lý riêng.
                    // Vòng lặp tiếp tục chờ Client tiếp theo.
                    _ = HandleClientAsync(client);
                }
            }
            catch (Exception ex) when (
                ex is SocketException || ex is ObjectDisposedException)
            {
                if (!closing)
                    Log("Lỗi lắng nghe: " + ex.Message);
            }
            finally
            {
                currentListener.Stop();
                listener = null;

                if (!closing)
                    btnListen.Enabled = true;
            }
        }
        private async Task HandleClientAsync(TcpClient client)
        {
            string registeredName = null;

            try
            {
                NetworkStream stream = client.GetStream();

                using (var reader = new StreamReader(
                    stream, Encoding.UTF8, true, 1024, leaveOpen: true))
                using (var writer = new StreamWriter(
                    stream, new UTF8Encoding(false), 1024, leaveOpen: true))
                {
                    writer.AutoFlush = true;
                    var session = new Session(client, writer);

                    try
                    {
                        string firstLine = await reader.ReadLineAsync();

                        if (firstLine == null || closing)
                            return;

                        if (!firstLine.StartsWith("JOIN\t", StringComparison.Ordinal))
                        {
                            await SendAsync(session, "ERROR\tHãy gửi tên trước.");
                            return;
                        }

                        string name = firstLine.Substring(5).Trim();

                        await dispatch.WaitAsync();
                        try
                        {
                            if (closing)
                                return;

                            if (name.Length == 0 || name.Length > 30 ||
                                name.Contains("\t") ||
                                string.Equals(name, "Gửi chung",
                                    StringComparison.OrdinalIgnoreCase))
                            {
                                await SendAsync(session, "ERROR\tTên không hợp lệ.");
                                return;
                            }

                            if (users.ContainsKey(name))
                            {
                                await SendAsync(session,
                                    "ERROR\tTên đã được sử dụng.");
                                return;
                            }

                            if (!await SendAsync(session, "OK"))
                                return;

                            if (closing)
                                return;

                            registeredName = name;
                            users.Add(name, session);

                            Log($"{name} đã vào phòng.");
                            await SendUserListAsync();
                        }
                        finally
                        {
                            dispatch.Release();
                        }

                        while (!closing)
                        {
                            string line = await reader.ReadLineAsync();

                            if (line == null)
                                break;

                            await dispatch.WaitAsync();
                            try
                            {
                                if (!closing)
                                    await RouteMessageAsync(registeredName, session, line);
                            }
                            finally
                            {
                                dispatch.Release();
                            }
                        }
                    }
                    finally
                    {
                        if (registeredName != null)
                        {
                            await dispatch.WaitAsync();
                            try
                            {
                                users.Remove(registeredName);

                                if (!closing)
                                {
                                    Log($"{registeredName} đã rời phòng.");
                                    await SendUserListAsync();
                                }
                            }
                            finally
                            {
                                dispatch.Release();
                            }
                        }
                    }
                }
            }
            catch (Exception ex) when (
                ex is IOException || ex is SocketException ||
                ex is ObjectDisposedException)
            {
                if (!closing && registeredName == null)
                    Log("Kết nối kết thúc: " + ex.Message);
            }
            finally
            {
                connections.Remove(client);
                client.Close();
            }
        }
        private async Task RouteMessageAsync(
           string name, Session sender, string line)
        {
            if (line.StartsWith("ALL\t", StringComparison.Ordinal))
            {
                string message = line.Substring(4).Trim();

                if (message.Length > 0)
                    await BroadcastAsync($"CHAT\t{name}: {message}");

                return;
            }

            string[] parts = line.Split(new[] { '\t' }, 3);

            if (parts.Length != 3 || parts[0] != "PM")
            {
                await SendAsync(sender, "ERROR\tLệnh gửi tin không hợp lệ.");
                return;
            }

            string recipient = parts[1];
            string content = parts[2].Trim();

            if (content.Length == 0)
                return;

            if (!users.TryGetValue(recipient, out Session target))
            {
                await SendAsync(sender, "ERROR\tNgười nhận đã rời phòng.");
                return;
            }

            string privateMessage =
                $"CHAT\t[Riêng] {name} → {recipient}: {content}";

            if (!await SendAsync(target, privateMessage))
            {
                await SendAsync(sender, "ERROR\tKhông gửi được tin riêng.");
                return;
            }

            if (target != sender)
                await SendAsync(sender, privateMessage);
        }
        private Task SendUserListAsync()
        {
            return BroadcastAsync("USERS\t" + string.Join("\t", users.Keys));
        }
        private async Task BroadcastAsync(string line)
        {
            foreach (Session session in users.Values.ToArray())
                await SendAsync(session, line);
        }
        private static async Task<bool> SendAsync(Session session, string line)
        {
            try
            {
                await session.Writer.WriteLineAsync(line);
                return true;
            }
            catch (Exception ex) when (
                ex is IOException || ex is SocketException ||
                ex is ObjectDisposedException)
            {
                session.Client.Close();
                return false;
            }
        }
        private void Log(string message)
        {
            if (closing)
                return;

            lstServer.Items.Add($"[{DateTime.Now:HH:mm:ss}] {message}");
            lstServer.TopIndex = lstServer.Items.Count - 1;
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.Cancel)
                return;

            closing = true;
            listener?.Stop();

            foreach (TcpClient client in connections.ToArray())
                client.Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

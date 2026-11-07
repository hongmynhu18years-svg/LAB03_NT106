using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Windows.Forms;

namespace LAB03
{
    public partial class Lab03_Bai06_Client : Form
    {
        private const int Port = 8006;
        private const string Everyone = "Gửi chung";

        private TcpClient connection;
        private StreamWriter writer;
        private string myName = "";
        private bool closing;
        private bool disconnectRequested;

        public Lab03_Bai06_Client()
        {
            InitializeComponent();
            txtIP.Text = "127.0.0.1";
            txtName.MaxLength = 30;
            txtMessage.MaxLength = 1000;
            cboNguoi.DropDownStyle = ComboBoxStyle.DropDownList;

            ResetRecipients();
            SetConnected(false);
        }

        private void Lab03_Bai06_Client_Load(object sender, EventArgs e)
        {

        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            if (connection != null)
                return;

            string ip = txtIP.Text.Trim();
            string name = txtName.Text.Trim();

            if (ip.Length == 0 || name.Length == 0 ||
                name.Length > 30 || name.Contains("\t") ||
                string.Equals(name, Everyone, StringComparison.OrdinalIgnoreCase))
            {
                AddChat("Hãy nhập IP và tên hợp lệ từ 1 đến 30 ký tự.");
                return;
            }

            var client = new TcpClient();
            connection = client;
            disconnectRequested = false;

            btnConnect.Enabled = false;
            txtIP.Enabled = false;
            txtName.Enabled = false;

            try
            {
                await client.ConnectAsync(ip, Port);

                if (closing)
                    return;

                NetworkStream stream = client.GetStream();
                using (var reader = new StreamReader(
                    stream, Encoding.UTF8, true, 1024, leaveOpen: true))
                {
                    var output = new StreamWriter(
                        stream, new UTF8Encoding(false), 1024, leaveOpen: true);

                    output.AutoFlush = true;

                    await output.WriteLineAsync("JOIN\t" + name);
                    string reply = await reader.ReadLineAsync();

                    if (closing)
                        return;

                    if (reply != "OK")
                    {
                        if (reply != null &&
                            reply.StartsWith("ERROR\t", StringComparison.Ordinal))
                        {
                            AddChat(reply.Substring(6));
                        }
                        else
                        {
                            AddChat("Server đã đóng hoặc từ chối kết nối.");
                        }

                        return;
                    }

                    myName = name;
                    writer = output;
                    ResetRecipients();
                    SetConnected(true);
                    AddChat("Đã vào phòng với tên " + myName + ".");

                    while (!closing && !disconnectRequested)
                    {
                        string line = await reader.ReadLineAsync();

                        if (line == null || closing || disconnectRequested)
                            break;

                        HandleServerLine(line);
                    }

                    if (!closing)
                    {
                        AddChat(disconnectRequested
                            ? "Đã ngắt kết nối."
                            : "Server đã đóng kết nối.");
                    }
                }
            }
            catch (Exception ex) when (
                ex is IOException || ex is SocketException ||
                ex is ObjectDisposedException)
            {
                if (!closing)
                {
                    AddChat(disconnectRequested
                        ? "Đã ngắt kết nối."
                        : "Lỗi kết nối: " + ex.Message);
                }
            }
            finally
            {
                writer = null;
                connection = null;
                client.Close();
                myName = "";

                if (!closing)
                {
                    ResetRecipients();
                    SetConnected(false);
                }
            }
        }
        private void HandleServerLine(string line)
        {
            if (line.StartsWith("USERS\t", StringComparison.Ordinal))
                UpdateRecipients(line.Substring(6));
            else if (line.StartsWith("CHAT\t", StringComparison.Ordinal))
                AddChat(line.Substring(5));
            else if (line.StartsWith("ERROR\t", StringComparison.Ordinal))
                AddChat(line.Substring(6));
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            StreamWriter output = writer;
            if (output == null || disconnectRequested)
                return;

            string message = txtMessage.Text
                .Replace('\r', ' ')
                .Replace('\n', ' ')
                .Trim();

            if (message.Length == 0)
                return;

            int selectedIndex = cboNguoi.SelectedIndex;

            if (selectedIndex < 0)
            {
                AddChat("Hãy chọn lại người nhận trước khi gửi.");
                return;
            }

            string recipient = cboNguoi.SelectedItem?.ToString() ?? "";
            string line = selectedIndex == 0
                ? "ALL\t" + message
                : "PM\t" + recipient + "\t" + message;

            btnSend.Enabled = false;
            txtMessage.ReadOnly = true;

            try
            {
                await output.WriteLineAsync(line);

                if (!closing && writer == output)
                    txtMessage.Clear();
            }
            catch (Exception ex) when (
                ex is IOException || ex is SocketException ||
                ex is ObjectDisposedException)
            {
                if (!closing && !disconnectRequested && writer == output)
                    AddChat("Không gửi được: " + ex.Message);
            }
            finally
            {
                if (!closing && writer == output && !disconnectRequested)
                {
                    txtMessage.ReadOnly = false;
                    btnSend.Enabled = true;
                }
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            disconnectRequested = true;
            btnSend.Enabled = false;
            btnDisconnect.Enabled = false;
            connection?.Close();
        }
        private void ResetRecipients()
        {
            cboNguoi.Items.Clear();
            cboNguoi.Items.Add(Everyone);
            cboNguoi.SelectedIndex = 0;
        }
        private void UpdateRecipients(string names)
        {
            int oldIndex = cboNguoi.SelectedIndex;
            string previous = cboNguoi.SelectedItem?.ToString() ?? "";

            cboNguoi.Items.Clear();
            cboNguoi.Items.Add(Everyone);

            foreach (string name in names.Split('\t'))
            {
                if (name.Length > 0 &&
                    !string.Equals(name, myName, StringComparison.OrdinalIgnoreCase))
                {
                    cboNguoi.Items.Add(name);
                }
            }

            // Người nhận đã rời phòng thì giữ trạng thái chưa chọn.
            // Các lượt cập nhật sau cũng giữ trạng thái này.
            cboNguoi.SelectedIndex = oldIndex == 0
                ? 0
                : oldIndex < 0 ? -1 : cboNguoi.Items.IndexOf(previous);
        }
        private void SetConnected(bool connected)
        {
            btnConnect.Enabled = !connected;
            btnDisconnect.Enabled = connected;
            btnSend.Enabled = connected;
            txtIP.Enabled = !connected;
            txtName.Enabled = !connected;
            txtMessage.Enabled = connected;
            txtMessage.ReadOnly = false;
            cboNguoi.Enabled = connected;
        }
        private void AddChat(string message)
        {
            if (closing)
                return;

            lstChat.Items.Add(message);
            lstChat.TopIndex = lstChat.Items.Count - 1;
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.Cancel)
                return;

            closing = true;
            connection?.Close();
        }

    }
}

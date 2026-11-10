using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LAB03
{
    public partial class Lab03_Bai06_Client : Form
    {
        private const int Port = 8006;
        private const string Everyone = "Gửi chung";
        private const string AttachmentPrefix = "ATTACH_V1|";
        private const int MaxAttachmentBytes = 2 * 1024 * 1024;

        private TcpClient connection;
        private StreamWriter writer;
        private string myName = "";
        private bool closing;
        private bool disconnectRequested;

        public Lab03_Bai06_Client()
        {
            InitializeComponent();
            lstChat.DoubleClick += lstChat_DoubleClick;
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
            {
                UpdateRecipients(line.Substring(6));
            }
            else if (line.StartsWith("CHAT\t", StringComparison.Ordinal))
            {
                string chatPayload = line.Substring(5);
                ParseAndDisplayChat(chatPayload);
            }
            else if (line.StartsWith("ERROR\t", StringComparison.Ordinal))
            {
                AddChat(line.Substring(6));
            }
        }
        private void ParseAndDisplayChat(string payload)
        {
            int attachIdx = payload.IndexOf(AttachmentPrefix, StringComparison.Ordinal);
            if (attachIdx >= 0)
            {
                string header = payload.Substring(0, attachIdx);
                string dataPart = payload.Substring(attachIdx + AttachmentPrefix.Length);
                string[] parts = dataPart.Split('|');

                if (parts.Length == 2)
                {
                    try
                    {
                        string fileName = Encoding.UTF8.GetString(Convert.FromBase64String(parts[0]));
                        byte[] fileBytes = Convert.FromBase64String(parts[1]);
                        string ext = Path.GetExtension(fileName).ToLowerInvariant();

                        if (ext == ".txt" || ext == ".jpg" || ext == ".png")
                        {
                            string fileTypeLabel = (ext == ".txt") ? "File TXT" : "Hình ảnh";
                            string displayMsg = $"{header}[{fileTypeLabel}: {fileName}] ({fileBytes.Length / 1024} KB) - (Double click để xem)";

                            // Lưu dữ liệu file vào cache theo vị trí dòng
                            int lineIndex = lstChat.Items.Count;
                            fileCache[lineIndex] = (fileName, ext, fileBytes);

                            AddChat(displayMsg);
                            return;
                        }
                    }
                    catch
                    {
                        AddChat(header + "[Lỗi giải mã file đính kèm]");
                        return;
                    }
                }
            }

            AddChat(payload);
        }

        private void lstChat_DoubleClick(object sender, EventArgs e)
        {
            int selectedIndex = lstChat.SelectedIndex;

            if (selectedIndex < 0)
                return;

            // Kiểm tra xem dòng được chọn có chứa dữ liệu file đính kèm không
            if (fileCache.TryGetValue(selectedIndex, out var fileData))
            {
                if (fileData.Extension == ".txt")
                {
                    // Giải mã byte[] thành chuỗi văn bản UTF-8 và bật FormXemText
                    string textContent = Encoding.UTF8.GetString(fileData.Data);
                    using (var frm = new Lab03_Bai06_Client_TEXT($"Xem file: {fileData.FileName}", textContent))
                    {
                        frm.ShowDialog(this);
                    }
                }
                else if (fileData.Extension == ".jpg" || fileData.Extension == ".png")
                {
                    // Bật FormXemAnh cho hình ảnh
                    using (var frm = new Lab03_Bai06_Client_Anh($"Xem ảnh: {fileData.FileName}", fileData.Data))
                    {
                        frm.ShowDialog(this);
                    }
                }
            }
        }
        private async Task SendChatAsync(string rawMessage)
        {
            StreamWriter output = writer;
            if (output == null || disconnectRequested)
                return;

            int selectedIndex = cboNguoi.SelectedIndex;
            if (selectedIndex < 0)
            {
                AddChat("Hãy chọn người nhận trước khi gửi.");
                return;
            }

            string recipient = cboNguoi.SelectedItem?.ToString() ?? "";
            string line = selectedIndex == 0
                ? "ALL\t" + rawMessage
                : "PM\t" + recipient + "\t" + rawMessage;

            try
            {
                await output.WriteLineAsync(line);
            }
            catch (Exception ex) when (ex is IOException || ex is SocketException || ex is ObjectDisposedException)
            {
                if (!closing && !disconnectRequested && writer == output)
                    AddChat("Không gửi được: " + ex.Message);
            }
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            string message = txtMessage.Text
                .Replace('\r', ' ')
                .Replace('\n', ' ')
                .Trim();

            if (message.Length == 0)
                return;

            btnSend.Enabled = false;
            txtMessage.ReadOnly = true;

            try
            {
                await SendChatAsync(message);
                if (!closing && writer != null)
                    txtMessage.Clear();
            }
            finally
            {
                if (!closing && writer != null && !disconnectRequested)
                {
                    txtMessage.ReadOnly = false;
                    btnSend.Enabled = true;
                }
            }
        }
        private static bool IsSupportedAttachment(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension == ".jpg" || extension == ".png" || extension == ".txt";
        }
        private async void btnDK_Click(object sender, EventArgs e)
        {
            if (writer == null || disconnectRequested)
            {
                MessageBox.Show(this, "Chưa kết nối đến Server.", "Thông báo");
                return;
            }

            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Ảnh JPG, PNG và file TXT|*.jpg;*.png;*.txt";
                dialog.Multiselect = false;

                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;

                string fileName = Path.GetFileName(dialog.FileName);

                if (!IsSupportedAttachment(fileName))
                {
                    MessageBox.Show(this, "Chỉ hỗ trợ file .jpg, .png và .txt.");
                    return;
                }

                btnDK.Enabled = false;

                try
                {
                    byte[] fileBytes = await Task.Run(() =>
                    {
                        using (var file = File.OpenRead(dialog.FileName))
                        {
                            if (file.Length > MaxAttachmentBytes)
                                throw new IOException("File vượt quá giới hạn 2 MiB.");

                            using (var reader = new BinaryReader(file))
                                return reader.ReadBytes((int)file.Length);
                        }
                    });

                    string encodedName = Convert.ToBase64String(Encoding.UTF8.GetBytes(fileName));
                    string encodedData = Convert.ToBase64String(fileBytes);
                    string payload = AttachmentPrefix + encodedName + "|" + encodedData;

                    await SendChatAsync(payload);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Không gửi được file");
                }
                finally
                {
                    if (!closing && writer != null && !disconnectRequested)
                        btnDK.Enabled = true;
                }
            }
        }

        private readonly Dictionary<int, (string FileName, string Extension, byte[] Data)> fileCache = new Dictionary<int, (string, string, byte[])>();

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
            btnDK.Enabled = connected;
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

        private void lstChat_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
}

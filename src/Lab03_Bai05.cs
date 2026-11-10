using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LAB03
{
    public partial class Lab03_Bai05 : Form
    {
        private string monAnHienTai = "";
        private string currentUser = "Người dùng ẩn danh";

        public Lab03_Bai05()
        {
            InitializeComponent();
        }

        private void Lab03_Bai05_Load(object sender, EventArgs e)
        {
            ten_mon.BringToFront();
            anh_mon.BringToFront();
            LoadMenu();
        }

        private void LoadMenu()
        {
            list_mon.Items.Clear();
            string serverIP = txtNhapIPServer.Text.Trim();

            // 1. Nếu chưa có IP, báo trạng thái và KHÔNG kết nối
            if (string.IsNullOrEmpty(serverIP))
            {
                list_mon.Items.Add("Vui lòng nhập IP Server");
                list_mon.Items.Add("để tải Menu...");
                return;
            }

            try
            {
                Lab03_Bai05_packet p = new Lab03_Bai05_packet { Action = "GET_LIST" };
                string jsonRequest = JsonSerializer.Serialize(p);
                byte[] requestData = Encoding.UTF8.GetBytes(jsonRequest);

                using (TcpClient client = new TcpClient(serverIP, 8888))
                {
                    NetworkStream stream = client.GetStream();
                    stream.Write(requestData, 0, requestData.Length);

                    byte[] buffer = new byte[1024 * 5000];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                    if (bytesRead > 0)
                    {
                        string jsonResponse = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        Lab03_Bai05_packet response = JsonSerializer.Deserialize<Lab03_Bai05_packet>(jsonResponse);

                        if (response.Action == "LIST_RESULT" && !string.IsNullOrEmpty(response.TenMonAn))
                        {
                            List<string> danhSachMon = JsonSerializer.Deserialize<List<string>>(response.TenMonAn);
                            foreach (var mon in danhSachMon)
                            {
                                list_mon.Items.Add(mon);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                list_mon.Items.Add("Chưa kết nối được Server");
            }
        }

        private async void xoa_Click(object sender, EventArgs e)
        {
            string serverIP = txtNhapIPServer.Text.Trim();
            // 2. Chặn lỗi báo trống IP
            if (string.IsNullOrEmpty(serverIP))
            {
                MessageBox.Show("Vui lòng nhập IP của Server trước khi Xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (list_mon.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng nhấp chọn một món bên MENU để xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string monCanXoa = list_mon.SelectedItem.ToString();
            DialogResult xacNhan = MessageBox.Show("Bạn có chắc chắn muốn xóa món '" + monCanXoa + "' không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (xacNhan == DialogResult.Yes)
            {
                if (sender is Button btnXoa) btnXoa.Enabled = false;

                try
                {
                    Lab03_Bai05_packet p = new Lab03_Bai05_packet
                    {
                        Action = "DELETE",
                        TenMonAn = monCanXoa
                    };

                    string jsonRequest = JsonSerializer.Serialize(p);
                    byte[] requestData = Encoding.UTF8.GetBytes(jsonRequest);
                    Lab03_Bai05_packet response = null;

                    await Task.Run(() =>
                    {
                        using (TcpClient client = new TcpClient(serverIP, 8888))
                        {
                            client.ReceiveTimeout = 3000;
                            client.SendTimeout = 3000;

                            NetworkStream stream = client.GetStream();
                            stream.Write(requestData, 0, requestData.Length);

                            byte[] buffer = new byte[1024];
                            int bytesRead = stream.Read(buffer, 0, buffer.Length);

                            if (bytesRead > 0)
                            {
                                string jsonResponse = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                                response = JsonSerializer.Deserialize<Lab03_Bai05_packet>(jsonResponse);
                            }
                        }
                    });

                    if (response != null)
                    {
                        if (response.Action == "DELETE_SUCCESS")
                        {
                            MessageBox.Show("Đã xóa thành công món: " + monCanXoa, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await LoadMenuAsync();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy món ăn này trên Server để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể kết nối đến Server: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (sender is Button xoa) xoa.Enabled = true;
                }
            }
        }

        private async void tim_Click(object sender, EventArgs e)
        {
            string serverIP = txtNhapIPServer.Text.Trim();
            // 3. Chặn lỗi báo trống IP
            if (string.IsNullOrEmpty(serverIP))
            {
                MessageBox.Show("Vui lòng nhập IP của Server trước khi Tìm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nguoiDongGop = txtNguoiDung.Text.Trim();
            bool isCongDong = rdoCongDong.Checked;

            if (!isCongDong && string.IsNullOrEmpty(nguoiDongGop))
            {
                MessageBox.Show("Vui lòng nhập tên của bạn để tìm món của chính mình!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (sender is Button btnTim) btnTim.Enabled = false;

            try
            {
                Lab03_Bai05_packet p = new Lab03_Bai05_packet();
                p.NguoiDongGop = nguoiDongGop;
                p.Action = isCongDong ? "RANDOM_ALL" : "RANDOM_MINE";

                string jsonRequest = JsonSerializer.Serialize(p);
                byte[] requestData = Encoding.UTF8.GetBytes(jsonRequest);
                Lab03_Bai05_packet response = null;

                await Task.Run(() =>
                {
                    using (TcpClient client = new TcpClient(serverIP, 8888))
                    {
                        NetworkStream stream = client.GetStream();
                        stream.Write(requestData, 0, requestData.Length);

                        byte[] buffer = new byte[1024 * 5000];
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);

                        if (bytesRead > 0)
                        {
                            string jsonResponse = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                            response = JsonSerializer.Deserialize<Lab03_Bai05_packet>(jsonResponse);
                        }
                    }
                });

                if (response != null)
                {
                    if (response.Action == "RESULT")
                    {
                        ten_mon.Text = $"{response.TenMonAn} (Bởi: {response.NguoiDongGop})";
                        ten_mon.ForeColor = Color.Black;

                        if (!string.IsNullOrEmpty(response.HinhAnhBase64))
                        {
                            try
                            {
                                byte[] imgBytes = Convert.FromBase64String(response.HinhAnhBase64);
                                using (MemoryStream ms = new MemoryStream(imgBytes))
                                {
                                    anh_mon.Image = new Bitmap(Image.FromStream(ms));
                                }
                            }
                            catch
                            {
                                anh_mon.Image = null;
                            }
                        }
                        else
                        {
                            anh_mon.Image = null;
                        }
                    }
                    else if (response.Action == "EMPTY")
                    {
                        MessageBox.Show("Không tìm thấy món ăn nào phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ten_mon.Text = "Chưa có món ăn";
                        anh_mon.Image = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối tới Server: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sender is Button tim) tim.Enabled = true;
            }
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            string serverIP = txtNhapIPServer.Text.Trim();
            // 4. Chặn lỗi báo trống IP
            if (string.IsNullOrEmpty(serverIP))
            {
                MessageBox.Show("Vui lòng nhập IP của Server trước khi Thêm đóng góp!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Lab03_Bai05_Donggopmonan frmDongGop = new Lab03_Bai05_Donggopmonan(serverIP);
            frmDongGop.ShowDialog();

            await LoadMenuAsync();
        }

        private async Task LoadMenuAsync()
        {
            string serverIP = txtNhapIPServer.Text.Trim();
            // Thoát ngầm nếu IP trống
            if (string.IsNullOrEmpty(serverIP)) return;

            try
            {
                Lab03_Bai05_packet p = new Lab03_Bai05_packet { Action = "GET_LIST" };
                string jsonRequest = JsonSerializer.Serialize(p);
                byte[] requestData = Encoding.UTF8.GetBytes(jsonRequest);
                Lab03_Bai05_packet response = null;

                await Task.Run(() =>
                {
                    using (TcpClient client = new TcpClient(serverIP, 8888))
                    {
                        client.ReceiveTimeout = 3000;
                        client.SendTimeout = 3000;
                        NetworkStream stream = client.GetStream();
                        stream.Write(requestData, 0, requestData.Length);
                        byte[] buffer = new byte[1024 * 5000];
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        if (bytesRead > 0)
                        {
                            string jsonResponse = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                            response = JsonSerializer.Deserialize<Lab03_Bai05_packet>(jsonResponse);
                        }
                    }
                });

                if (response != null && response.Action == "LIST_RESULT")
                {
                    List<string> menuItems = JsonSerializer.Deserialize<List<string>>(response.TenMonAn);
                    list_mon.Items.Clear();
                    foreach (string item in menuItems)
                    {
                        list_mon.Items.Add(item);
                    }
                }
            }
            catch
            {
                // Bỏ qua lỗi ngầm
            }
        }

        private void list_mon_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            e.DrawBackground();
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            string tenMon = list_mon.Items[e.Index].ToString();
            using (Brush textBrush = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(tenMon, e.Font, textBrush, e.Bounds, format);
            }
            e.DrawFocusRectangle();
        }

        private void list_mon_SelectedIndexChanged(object sender, EventArgs e) { }
        private void ten_mon_Click(object sender, EventArgs e) { }
        private void anh_mon_Click(object sender, EventArgs e) { }
        private void menu_Click(object sender, EventArgs e) { }
        private void list_mon_Click(object sender, EventArgs e) { }
        private void txtNguoiDung_TextChanged(object sender, EventArgs e) { }
        private void rdoNguoiDung_CheckedChanged(object sender, EventArgs e) { }
        private void rdoCongDong_CheckedChanged(object sender, EventArgs e) { }
        private void lblTenNguoiDung_Click(object sender, EventArgs e) { }
        private void txtNhapIPServer_TextChanged(object sender, EventArgs e) { }
        private void lblNhapIP_Click(object sender, EventArgs e) { }
    }
}
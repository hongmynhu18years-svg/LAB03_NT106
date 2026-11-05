using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;            // Thêm thư viện xử lý MemoryStream cho ảnh
using System.Net.Sockets;   // Thêm thư viện mạng
using System.Text;
using System.Text.Json;     // Thêm thư viện phân tích JSON
using System.Windows.Forms;

namespace LAB02
{
    public partial class Lab03_Bai05 : Form
    {
        private string monAnHienTai = "";
        // Tên user mặc định (Dùng cho chức năng RANDOM_MINE sau này)
        private string currentUser = "Người dùng ẩn danh";
        public Lab03_Bai05()
        {
            InitializeComponent();
        }

        private void Lab03_Bai05_Load(object sender, EventArgs e)
        {
            // Đưa label tên món và khung ảnh lên trên cùng để không bị tấm thớt/đĩa che khuất
            ten_mon.BringToFront();
            anh_mon.BringToFront();

            LoadMenu();
        }
        private void LoadMenu()
        {
            list_mon.Items.Clear();

            try
            {
                // 1. Tạo gói tin yêu cầu lấy danh sách
                Packet p = new Packet { Action = "GET_LIST" };
                string jsonRequest = JsonSerializer.Serialize(p);
                byte[] requestData = Encoding.UTF8.GetBytes(jsonRequest);

                // 2. Kết nối tới Server
                using (TcpClient client = new TcpClient("127.0.0.1", 8888))
                {
                    NetworkStream stream = client.GetStream();
                    stream.Write(requestData, 0, requestData.Length);

                    // 3. Nhận phản hồi danh sách từ Server
                    byte[] buffer = new byte[1024 * 5000];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                    if (bytesRead > 0)
                    {
                        string jsonResponse = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        Packet response = JsonSerializer.Deserialize<Packet>(jsonResponse);

                        if (response.Action == "LIST_RESULT" && !string.IsNullOrEmpty(response.TenMonAn))
                        {
                            // Giải mã chuỗi JSON danh sách món ăn
                            List<string> danhSachMon = JsonSerializer.Deserialize<List<string>>(response.TenMonAn);

                            // Đưa từng món lên ListBox (khung MENU)
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

        private void xoa_Click(object sender, EventArgs e)
        {
            if (list_mon.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng nhấp chọn một món bên MENU để xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string monCanXoa = list_mon.SelectedItem.ToString();
            DialogResult xacNhan = MessageBox.Show("Bạn có chắc chắn muốn xóa món '" + monCanXoa + "' không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (xacNhan == DialogResult.Yes)
            {
                try
                {
                    // Tạo gói tin yêu cầu xóa
                    Packet p = new Packet
                    {
                        Action = "DELETE",
                        TenMonAn = monCanXoa
                    };

                    string jsonRequest = JsonSerializer.Serialize(p);
                    byte[] requestData = Encoding.UTF8.GetBytes(jsonRequest);

                    using (TcpClient client = new TcpClient("127.0.0.1", 8888))
                    {
                        NetworkStream stream = client.GetStream();
                        stream.Write(requestData, 0, requestData.Length);

                        byte[] buffer = new byte[1024];
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);

                        if (bytesRead > 0)
                        {
                            string jsonResponse = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                            Packet response = JsonSerializer.Deserialize<Packet>(jsonResponse);

                            if (response.Action == "DELETE_SUCCESS")
                            {
                                MessageBox.Show("Đã xóa thành công món: " + monCanXoa, "Thông báo");
                                // Tải lại danh sách menu ngay lập tức
                                LoadMenu();
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy món ăn này trên Server để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể kết nối đến Server: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            LoadMenu();
        }

        // CHỨC NĂNG QUAN TRỌNG: TÌM MÓN NGẪU NHIÊN QUA TCP
        private void tim_Click(object sender, EventArgs e)
        {
            try
            {
                Packet p = new Packet();
                p.NguoiDongGop = txtNguoiDung.Text.Trim(); // Ô nhập tên người dùng

                // Kiểm tra chọn chế độ (Cộng đồng hay Người dùng)
                if (rdoCongDong.Checked)
                {
                    p.Action = "RANDOM_ALL";
                }
                else
                {
                    if (string.IsNullOrEmpty(p.NguoiDongGop))
                    {
                        MessageBox.Show("Vui lòng nhập tên của bạn để tìm món của chính mình!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    p.Action = "RANDOM_MINE";
                }

                string jsonRequest = JsonSerializer.Serialize(p);
                byte[] requestData = Encoding.UTF8.GetBytes(jsonRequest);

                using (TcpClient client = new TcpClient("127.0.0.1", 8888))
                {
                    NetworkStream stream = client.GetStream();
                    stream.Write(requestData, 0, requestData.Length);

                    byte[] buffer = new byte[1024 * 5000];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                    if (bytesRead > 0)
                    {
                        string jsonResponse = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        Packet response = JsonSerializer.Deserialize<Packet>(jsonResponse);

                        if (response.Action == "RESULT")
                        {

                            // 1. Hiển thị tên món ăn
                            ten_mon.Text = response.TenMonAn + " (Bởi: " + response.NguoiDongGop + ")";
                            ten_mon.ForeColor = Color.Black; // Ép màu chữ thành đen để chắc chắn nhìn thấy

                            // 2. Hiển thị hình ảnh nếu có
                            if (!string.IsNullOrEmpty(response.HinhAnhBase64))
                            {
                                try
                                {
                                    byte[] imgBytes = Convert.FromBase64String(response.HinhAnhBase64);
                                    using (MemoryStream ms = new MemoryStream(imgBytes))
                                    {
                                        anh_mon.Image = Image.FromStream(ms);
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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối tới Server: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        private void them_Click(object sender, EventArgs e)
        {
            Donggopmonan frm2 = new Donggopmonan();
            frm2.ShowDialog();
            LoadMenu();
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

        private void txtNguoiDung_TextChanged(object sender, EventArgs e)
        {

        }

        private void rdoNguoiDung_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdoCongDong_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void lblTenNguoiDung_Click(object sender, EventArgs e)
        {

        }
    }
}
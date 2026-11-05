using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net.Sockets;   // Thêm thư viện mạng
using System.Text;
using System.Text.Json;     // Thêm thư viện xử lý JSON
using System.Windows.Forms;

namespace LAB02
{
    public partial class Donggopmonan : Form
    {
        // Đổi biến này để lưu tạm đường dẫn thật của ảnh trước khi gửi đi
        private string duongDanAnhGoc = "";

        public Donggopmonan()
        {
            InitializeComponent();
        }

        private void add_picture_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFile.Title = "Chọn hình ảnh món ăn";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                duongDanAnhGoc = openFile.FileName;
                MessageBox.Show("Đã chọn ảnh: " + Path.GetFileName(duongDanAnhGoc), "Thông báo");
            }
        }

        private void send_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_TenNguoiDongGop.Text) || string.IsNullOrWhiteSpace(txt_TenMonAnDongGop.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ tên người và tên món!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 1. Chuyển ảnh thành chuỗi Base64 để truyền qua mạng
                string base64Image = "";
                if (!string.IsNullOrEmpty(duongDanAnhGoc) && File.Exists(duongDanAnhGoc))
                {
                    byte[] imageArray = File.ReadAllBytes(duongDanAnhGoc);
                    base64Image = Convert.ToBase64String(imageArray);
                }

                // 2. Đóng gói dữ liệu thành Object Packet
                Packet p = new Packet
                {
                    Action = "ADD",
                    TenMonAn = txt_TenMonAnDongGop.Text.Trim(),
                    NguoiDongGop = txt_TenNguoiDongGop.Text.Trim(),
                    HinhAnhBase64 = base64Image
                };

                string json = JsonSerializer.Serialize(p);
                byte[] data = Encoding.UTF8.GetBytes(json);

                // 3. Gửi gói tin qua TCP tới Server (IP: 127.0.0.1, Port: 8888)
                using (TcpClient client = new TcpClient("127.0.0.1", 8888))
                {
                    NetworkStream stream = client.GetStream();
                    stream.Write(data, 0, data.Length);
                }

                MessageBox.Show("Đã gửi đóng góp tới Server thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Xóa trắng form sau khi gửi
                txt_TenNguoiDongGop.Clear();
                txt_TenMonAnDongGop.Clear();
                duongDanAnhGoc = "";

                // (Tùy chọn) Gọi lại hàm tải danh sách nếu Server hỗ trợ trả về list
                LoadDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối đến Server. Đảm bảo Server đang chạy!\nLỗi: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bang_danh_gia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                LoadDanhSach();
            }
        }

        private void LoadDanhSach()
        {
            listView1.Items.Clear();
            listView1.View = View.Details; // Đảm bảo ListView hiển thị dạng bảng

            try
            {
                Packet p = new Packet { Action = "GET_ALL_DETAILS" };
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

                        if (response.Action == "DETAILS_RESULT" && !string.IsNullOrEmpty(response.TenMonAn))
                        {
                            // Giải mã danh sách chi tiết
                            using (JsonDocument doc = JsonDocument.Parse(response.TenMonAn))
                            {
                                JsonElement root = doc.RootElement;
                                foreach (JsonElement element in root.EnumerateArray())
                                {
                                    string stt = element.GetProperty("STT").GetInt32().ToString();
                                    string nguoi = element.GetProperty("NguoiDongGop").GetString();
                                    string mon = element.GetProperty("TenMonAn").GetString();
                                    string time = element.GetProperty("ThoiGian").GetString();

                                    ListViewItem item = new ListViewItem(stt);
                                    item.SubItems.Add(nguoi);
                                    item.SubItems.Add(mon);
                                    item.SubItems.Add(time);

                                    listView1.Items.Add(item);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải danh sách chi tiết: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void xem_danh_gia_Click(object sender, EventArgs e) { }
        private void tabPage1_Click(object sender, EventArgs e) { }
        private void TenNguoiDongGop_Click(object sender, EventArgs e) { }
        private void txt_TenNguoiDongGop_TextChanged(object sender, EventArgs e) { }
        private void TenMonAnDongGop_Click(object sender, EventArgs e) { }
        private void txt_TenMonAnDongGop_TextChanged(object sender, EventArgs e) { }

        private void Donggopmonan_Load(object sender, EventArgs e)
        {
            LoadDanhSach();
        }
    }
}
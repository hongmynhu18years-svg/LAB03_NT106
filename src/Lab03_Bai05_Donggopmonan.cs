using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace LAB03
{
    public partial class Lab03_Bai05_Donggopmonan : Form
    {
        private string duongDanAnhGoc = "";

        public Lab03_Bai05_Donggopmonan()
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

        private async void send_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_TenNguoiDongGop.Text) || string.IsNullOrWhiteSpace(txt_TenMonAnDongGop.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ tên người và tên món!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lưu lại button gửi để mở khóa ở khối finally
            Button btnSend = sender as Button;
            if (btnSend != null) btnSend.Enabled = false;

            try
            {
                string base64Image = "";
                if (!string.IsNullOrEmpty(duongDanAnhGoc) && File.Exists(duongDanAnhGoc))
                {
                    byte[] imageArray = File.ReadAllBytes(duongDanAnhGoc);
                    base64Image = Convert.ToBase64String(imageArray);
                }

                Lab03_Bai05_packet p = new Lab03_Bai05_packet
                {
                    Action = "ADD",
                    TenMonAn = txt_TenMonAnDongGop.Text.Trim(),
                    NguoiDongGop = txt_TenNguoiDongGop.Text.Trim(),
                    HinhAnhBase64 = base64Image
                };

                string json = JsonSerializer.Serialize(p);
                byte[] data = Encoding.UTF8.GetBytes(json);
                Lab03_Bai05_packet response = null;

                // Gửi lệnh ngầm xuống background thread
                await Task.Run(() =>
                {
                    using (TcpClient client = new TcpClient("10.139.209.54", 8888))
                    {
                        NetworkStream stream = client.GetStream();
                        stream.Write(data, 0, data.Length);

                        byte[] buffer = new byte[1024 * 5000];
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        if (bytesRead > 0)
                        {
                            string jsonResponse = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                            response = JsonSerializer.Deserialize<Lab03_Bai05_packet>(jsonResponse);
                        }
                    }
                });

                if (response != null && response.Action == "ERROR")
                {
                    MessageBox.Show(response.TenMonAn, "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                MessageBox.Show("Đã gửi đóng góp tới Server thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txt_TenNguoiDongGop.Clear();
                txt_TenMonAnDongGop.Clear();
                duongDanAnhGoc = "";

                await LoadDanhSachAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối đến Server.\nLỗi: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Sửa lỗi cú pháp biến mở khóa nút bấm
                if (btnSend != null) btnSend.Enabled = true;
            }
        }

        private async Task LoadDanhSachAsync()
        {
            listView1.Items.Clear();
            listView1.View = View.Details;

            try
            {
                Lab03_Bai05_packet p = new Lab03_Bai05_packet { Action = "GET_ALL_DETAILS" };
                string jsonRequest = JsonSerializer.Serialize(p);
                byte[] requestData = Encoding.UTF8.GetBytes(jsonRequest);
                Lab03_Bai05_packet response = null;

                await Task.Run(() =>
                {
                    using (TcpClient client = new TcpClient("127.0.0.1", 8888))
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

                if (response != null && response.Action == "DETAILS_RESULT" && !string.IsNullOrEmpty(response.TenMonAn))
                {
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
            catch
            {
                // Bỏ qua lỗi ngầm
            }
        }

        // Sự kiện chuyển Tab trên giao diện TabControl
        private async void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu đang ở tab danh sách/đánh giá (index 1 tùy theo thiết kế giao diện của bạn)
            if (tabControl1.SelectedIndex == 1)
            {
                await LoadDanhSachAsync();
            }
        }

        private async void Donggopmonan_Load(object sender, EventArgs e)
        {
            await LoadDanhSachAsync();
        }

        private void xem_danh_gia_Click(object sender, EventArgs e) { }
        private void tabPage1_Click(object sender, EventArgs e) { }
        private void TenNguoiDongGop_Click(object sender, EventArgs e) { }
        private void txt_TenNguoiDongGop_TextChanged(object sender, EventArgs e) { }
        private void TenMonAnDongGop_Click(object sender, EventArgs e) { }
        private void txt_TenMonAnDongGop_TextChanged(object sender, EventArgs e) { }
    }
}
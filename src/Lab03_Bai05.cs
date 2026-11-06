using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;            // Thêm thư viện xử lý MemoryStream cho ảnh
using System.Net.Sockets;   // Thêm thư viện mạng
using System.Text;
using System.Text.Json;     // Thêm thư viện phân tích JSON
using System.Windows.Forms;

namespace LAB03
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

        private async void xoa_Click(object sender, EventArgs e)
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
                // 1. Tạm khóa nút xóa để tránh người dùng bấm liên tục gây treo
                if (sender is Button btnXoa) btnXoa.Enabled = false;

                try
                {
                    Packet p = new Packet
                    {
                        Action = "DELETE",
                        TenMonAn = monCanXoa
                    };

                    string jsonRequest = JsonSerializer.Serialize(p);
                    byte[] requestData = Encoding.UTF8.GetBytes(jsonRequest);
                    Packet response = null;

                    // 2. ĐẨY LỆNH GIAO TIẾP MẠNG XUỐNG BACKGROUND THREAD
                    await Task.Run(() =>
                    {
                        using (TcpClient client = new TcpClient("127.0.0.1", 8888))
                        {
                            // Thêm Timeout 3 giây chống kẹt mạng
                            client.ReceiveTimeout = 3000;
                            client.SendTimeout = 3000;

                            NetworkStream stream = client.GetStream();
                            stream.Write(requestData, 0, requestData.Length);

                            byte[] buffer = new byte[1024];
                            int bytesRead = stream.Read(buffer, 0, buffer.Length);

                            if (bytesRead > 0)
                            {
                                string jsonResponse = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                                response = JsonSerializer.Deserialize<Packet>(jsonResponse);
                            }
                        }
                    });

                    // 3. XỬ LÝ GIAO DIỆN SAU KHI SERVER TRẢ KẾT QUẢ
                    if (response != null)
                    {
                        if (response.Action == "DELETE_SUCCESS")
                        {
                            MessageBox.Show("Đã xóa thành công món: " + monCanXoa, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Gọi hàm LoadMenuAsync bất đồng bộ thay vì LoadMenu() cũ
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
                    // 4. Mở khóa lại nút xóa
                    if (sender is Button xoa) xoa.Enabled = true;
                }
            }
        }

        // CHỨC NĂNG QUAN TRỌNG: TÌM MÓN NGẪU NHIÊN QUA TCP
        private async void tim_Click(object sender, EventArgs e)
        {
            // 1. ĐỌC DỮ LIỆU TỪ GIAO DIỆN TRƯỚC (Bắt buộc làm ngoài Task.Run)
            string nguoiDongGop = txtNguoiDung.Text.Trim();
            bool isCongDong = rdoCongDong.Checked;

            if (!isCongDong && string.IsNullOrEmpty(nguoiDongGop))
            {
                MessageBox.Show("Vui lòng nhập tên của bạn để tìm món của chính mình!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạm khóa nút tìm kiếm để tránh người dùng bấm liên tục gây treo
            if (sender is Button btnTim) btnTim.Enabled = false;

            try
            {
                Packet p = new Packet();
                p.NguoiDongGop = nguoiDongGop;
                p.Action = isCongDong ? "RANDOM_ALL" : "RANDOM_MINE";

                string jsonRequest = JsonSerializer.Serialize(p);
                byte[] requestData = Encoding.UTF8.GetBytes(jsonRequest);

                Packet response = null;

                // 2. ĐẨY TÁC VỤ MẠNG XUỐNG LUỒNG NGẦM (Không làm đơ Form)
                await Task.Run(() =>
                {
                    using (TcpClient client = new TcpClient("127.0.0.1", 8888))
                    {
                        NetworkStream stream = client.GetStream();
                        stream.Write(requestData, 0, requestData.Length);

                        byte[] buffer = new byte[1024 * 5000];
                        // Lệnh Read này sẽ chờ ngầm, Form vẫn kéo thả mượt mà
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);

                        if (bytesRead > 0)
                        {
                            string jsonResponse = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                            response = JsonSerializer.Deserialize<Packet>(jsonResponse);
                        }
                    }
                });

                // 3. CẬP NHẬT GIAO DIỆN (Sau khi Task.Run chạy xong, an toàn để cập nhật UI)
                if (response != null)
                {
                    if (response.Action == "RESULT")
                    {
                        // Hiển thị tên món ăn
                        ten_mon.Text = $"{response.TenMonAn} (Bởi: {response.NguoiDongGop})";
                        ten_mon.ForeColor = Color.Black;

                        // Hiển thị hình ảnh
                        if (!string.IsNullOrEmpty(response.HinhAnhBase64))
                        {
                            try
                            {
                                byte[] imgBytes = Convert.FromBase64String(response.HinhAnhBase64);
                                using (MemoryStream ms = new MemoryStream(imgBytes))
                                {
                                    // Thay vì gán trực tiếp bằng ms, dùng bản sao để tránh lỗi khóa file/stream
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
                // 4. MỞ KHÓA NÚT BẤM SAU KHI XONG HOẶC CÓ LỖI
                if (sender is Button tim) tim.Enabled = true;
            }
        }


        private async void btnThem_Click(object sender, EventArgs e)
        {
            // Mở form Đóng Góp
            Donggopmonan frmDongGop = new Donggopmonan();
            // Dùng ShowDialog() để ứng dụng chờ cho đến khi bạn tắt form Đóng góp đi
            frmDongGop.ShowDialog();
            // Ngay khi form Đóng góp vừa bị tắt, lập tức tự động tải lại Menu ở form chính
            await LoadMenuAsync();
        }
        private async Task LoadMenuAsync()
        {
            try
            {
                Packet p = new Packet { Action = "GET_LIST" };
                string jsonRequest = JsonSerializer.Serialize(p);
                byte[] requestData = Encoding.UTF8.GetBytes(jsonRequest);
                Packet response = null;

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
                            response = JsonSerializer.Deserialize<Packet>(jsonResponse);
                        }
                    }
                });

                if (response != null && response.Action == "LIST_RESULT")
                {
                    List<string> menuItems = JsonSerializer.Deserialize<List<string>>(response.TenMonAn);

                    // Ở đây tôi thấy bạn dùng ListBox có tên là list_mon thay vì RichTextBox
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
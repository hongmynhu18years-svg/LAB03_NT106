using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text.Json; // Dùng để đọc chuỗi JSON
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;
namespace Lab03_Bai05
{
    public partial class Server : Form
    {
        // 1. DI CHUYỂN CÁC BIẾN TOÀN CỤC VÀO BÊN TRONG CLASS
        private TcpListener server;
        private Thread listenThread;
        private bool isRunning = false;

        public Server()
        {
            InitializeComponent();
        }
        // 2. XỬ LÝ SỰ KIỆN NÚT BẮT ĐẦU SERVER
        private void start_Click(object sender, EventArgs e)
        {
            if (isRunning) return;

            try
            {
                string connectionString = "Data Source=data.db;Version=3;";
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    // 1. Tự động tạo bảng nếu chưa có
                    string createTableNguoiDung = @"CREATE TABLE IF NOT EXISTS NguoiDung (
                IDNCC INTEGER PRIMARY KEY AUTOINCREMENT,
                HoVaTen TEXT,
                QuyenHan TEXT
            );";
                    string createTableMonAn = @"CREATE TABLE IF NOT EXISTS MonAn (
                IDMA INTEGER PRIMARY KEY AUTOINCREMENT,
                TenMonAn TEXT,
                HinhAnh TEXT,
                IDNCC INTEGER,
                ThoiGian TEXT
            );";

                    using (var cmd = new SQLiteCommand(createTableNguoiDung, conn)) cmd.ExecuteNonQuery();
                    using (var cmd = new SQLiteCommand(createTableMonAn, conn)) cmd.ExecuteNonQuery();

                    // 2. Reset dữ liệu cũ
                    using (var cmd = new SQLiteCommand("DELETE FROM MonAn; DELETE FROM NguoiDung;", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // 3. Nạp dữ liệu Quản lý quán kèm tên file ảnh có sẵn
                    string sqlUser = "INSERT INTO NguoiDung (HoVaTen, QuyenHan) VALUES ('Quản lý quán', 'Quản trị'); SELECT last_insert_rowid();";
                    long adminId = 0;
                    using (var cmdUser = new SQLiteCommand(sqlUser, conn))
                    {
                        adminId = (long)cmdUser.ExecuteScalar();
                    }

                    // Khai báo danh sách món kèm tên file ảnh tương ứng
                    var defaultFoods = new[]
                    {
    new { Ten = "Phở", Hinh = "pho.png" },
    new { Ten = "Cơm tấm", Hinh = "comtam.png" },
    new { Ten = "Hủ tiếu", Hinh = "hutieu.png" }
};

                    foreach (var item in defaultFoods)
                    {
                        string sqlFood = "INSERT INTO MonAn (TenMonAn, HinhAnh, IDNCC, ThoiGian) VALUES (@food, @img, @idncc, @time)";
                        using (var cmdFood = new SQLiteCommand(sqlFood, conn))
                        {
                            cmdFood.Parameters.AddWithValue("@food", item.Ten);
                            cmdFood.Parameters.AddWithValue("@img", item.Hinh); // Gán tên file ảnh có sẵn
                            cmdFood.Parameters.AddWithValue("@idncc", adminId);
                            cmdFood.Parameters.AddWithValue("@time", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                            cmdFood.ExecuteNonQuery();
                        }
                    }
                }

                server = new TcpListener(IPAddress.Any, 8888);
                server.Start();
                isRunning = true;

                Rtb_log.AppendText("Server đã reset CSDL và đang chạy ở Port 8888...\n");
                listenThread = new Thread(ListenForClients);
                listenThread.IsBackground = true;
                listenThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi động Server: " + ex.Message);
            }
        }

        // 3. HÀM LẮNG NGHE KẾT NỐI
        private void ListenForClients()
        {
            while (isRunning)
            {
                try
                {
                    TcpClient client = server.AcceptTcpClient();
                    // Cập nhật giao diện từ luồng phụ bằng Invoke
                    Invoke(new Action(() => Rtb_log.AppendText($"Có Client mới kết nối: {client.Client.RemoteEndPoint}\n")));

                    // Tạo luồng riêng để xử lý yêu cầu của Client này
                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.IsBackground = true;
                    clientThread.Start();
                }
                catch
                {
                    // Bỏ qua lỗi khi server bị tắt ngắt quãng
                }
            }
        }

        // 4. HÀM XỬ LÝ DỮ LIỆU TỪ CLIENT
        private void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024 * 5000]; // Buffer 5MB

            try
            {
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                if (bytesRead == 0) return;

                string jsonReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                // 1. GIẢI MÃ JSON TỪ CLIENT
                Packet p = JsonSerializer.Deserialize<Packet>(jsonReceived);

                string connectionString = "Data Source=data.db;Version=3;";
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    // 2. XỬ LÝ YÊU CẦU "THÊM MÓN"
                    if (p.Action == "ADD")
                    {
                        // Kiểm tra xem món ăn đã tồn tại trong CSDL chưa (không phân biệt hoa thường)
                        string checkSql = "SELECT COUNT(*) FROM MonAn WHERE LOWER(TenMonAn) = LOWER(@foodCheck)";
                        using (var checkCmd = new SQLiteCommand(checkSql, conn))
                        {
                            checkCmd.Parameters.AddWithValue("@foodCheck", p.TenMonAn.Trim());
                            long count = (long)checkCmd.ExecuteScalar();

                            if (count > 0)
                            {
                                // Nếu đã tồn tại, gửi thông báo lỗi về cho Client
                                Packet errPacket = new Packet { Action = "ERROR", TenMonAn = "Món ăn này đã tồn tại trong danh sách!" };
                                byte[] errBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(errPacket));
                                stream.Write(errBytes, 0, errBytes.Length);

                                Invoke(new Action(() => Rtb_log.AppendText($"Từ chối món trùng: {p.TenMonAn}\n")));
                                return;
                            }
                        }

                        Invoke(new Action(() => Rtb_log.AppendText($"{p.NguoiDongGop} yêu cầu THÊM món: {p.TenMonAn}\n")));

                        // Giải mã Base64 thành file ảnh và lưu vào ổ cứng Server
                        string fileName = "img_" + DateTime.Now.Ticks + ".png";
                        byte[] imageBytes = Convert.FromBase64String(p.HinhAnhBase64);
                        File.WriteAllBytes(Path.Combine(Application.StartupPath, fileName), imageBytes);

                        // Lưu vào CSDL
                        string sqlUser = "INSERT INTO NguoiDung (HoVaTen, QuyenHan) VALUES (@name, 'Thành viên'); SELECT last_insert_rowid();";
                        long userId = 0;
                        using (var cmdUser = new SQLiteCommand(sqlUser, conn))
                        {
                            cmdUser.Parameters.AddWithValue("@name", p.NguoiDongGop);
                            userId = (long)cmdUser.ExecuteScalar();
                        }

                        string sqlFood = "INSERT INTO MonAn (TenMonAn, HinhAnh, IDNCC, ThoiGian) VALUES (@food, @img, @idncc, @time)";
                        using (var cmdFood = new SQLiteCommand(sqlFood, conn))
                        {
                            cmdFood.Parameters.AddWithValue("@food", p.TenMonAn.Trim());
                            cmdFood.Parameters.AddWithValue("@img", fileName);
                            cmdFood.Parameters.AddWithValue("@idncc", userId);
                            cmdFood.Parameters.AddWithValue("@time", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                            cmdFood.ExecuteNonQuery();
                        }

                        // Gửi phản hồi thành công về Client
                        Packet successPacket = new Packet { Action = "SUCCESS" };
                        byte[] successBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(successPacket));
                        stream.Write(successBytes, 0, successBytes.Length);
                    }
                    // 3. XỬ LÝ YÊU CẦU "TÌM MÓN NGẪU NHIÊN"
                    else if (p.Action == "RANDOM_ALL" || p.Action == "RANDOM_MINE")
                    {
                        Invoke(new Action(() => Rtb_log.AppendText($"Yêu cầu TÌM MÓN ({p.Action}) từ {p.NguoiDongGop}\n")));

                        string sql = (p.Action == "RANDOM_ALL")
                            ? "SELECT m.TenMonAn, m.HinhAnh, n.HoVaTen FROM MonAn m JOIN NguoiDung n ON m.IDNCC = n.IDNCC ORDER BY RANDOM() LIMIT 1"
                            : "SELECT m.TenMonAn, m.HinhAnh, n.HoVaTen FROM MonAn m JOIN NguoiDung n ON m.IDNCC = n.IDNCC WHERE LOWER(n.HoVaTen) = LOWER(@name) ORDER BY RANDOM() LIMIT 1";

                        using (var cmd = new SQLiteCommand(sql, conn))
                        {
                            if (p.Action == "RANDOM_MINE")
                            {
                                cmd.Parameters.AddWithValue("@name", p.NguoiDongGop.Trim());
                            }

                            using (var reader = cmd.ExecuteReader())
                            {
                                Packet responsePacket = new Packet();
                                if (reader.Read())
                                {
                                    responsePacket.Action = "RESULT";
                                    responsePacket.TenMonAn = reader["TenMonAn"].ToString();
                                    responsePacket.NguoiDongGop = reader["HoVaTen"].ToString();

                                    string imgFile = reader["HinhAnh"]?.ToString();
                                    if (!string.IsNullOrEmpty(imgFile))
                                    {
                                        string imgPath = Path.Combine(Application.StartupPath, imgFile);
                                        if (File.Exists(imgPath))
                                        {
                                            responsePacket.HinhAnhBase64 = Convert.ToBase64String(File.ReadAllBytes(imgPath));
                                        }
                                    }
                                }
                                else
                                {
                                    responsePacket.Action = "EMPTY";
                                }

                                string jsonResponse = JsonSerializer.Serialize(responsePacket);
                                byte[] outBuffer = Encoding.UTF8.GetBytes(jsonResponse);
                                stream.Write(outBuffer, 0, outBuffer.Length);
                            }
                        }
                    }

                    // 4. XỬ LÝ YÊU CẦU "LẤY DANH SÁCH MENU" (Sử dụng DISTINCT để lọc bỏ các tên món bị trùng nếu có)
                    else if (p.Action == "GET_LIST")
                    {
                        List<string> danhSachMon = new List<string>();
                        string sql = "SELECT DISTINCT TenMonAn FROM MonAn"; // Lọc trùng bằng DISTINCT
                        using (var cmd = new SQLiteCommand(sql, conn))
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                danhSachMon.Add(reader["TenMonAn"].ToString());
                            }
                        }

                        Packet responsePacket = new Packet
                        {
                            Action = "LIST_RESULT",
                            TenMonAn = JsonSerializer.Serialize(danhSachMon)
                        };

                        string jsonResponse = JsonSerializer.Serialize(responsePacket);
                        byte[] outBuffer = Encoding.UTF8.GetBytes(jsonResponse);
                        stream.Write(outBuffer, 0, outBuffer.Length);
                    }
                    // 5. XỬ LÝ YÊU CẦU "XÓA MÓN ĂN"
                    else if (p.Action == "DELETE")
                    {
                        Invoke(new Action(() => Rtb_log.AppendText($"Yêu cầu XÓA món: {p.TenMonAn}\n")));

                        // Xóa món ăn trong cơ sở dữ liệu dựa vào tên món
                        string sql = "DELETE FROM MonAn WHERE TenMonAn = @ten";
                        using (var cmd = new SQLiteCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@ten", p.TenMonAn.Trim());
                            int rowsAffected = cmd.ExecuteNonQuery();

                            Packet responsePacket = new Packet();
                            if (rowsAffected > 0)
                            {
                                responsePacket.Action = "DELETE_SUCCESS";
                                Invoke(new Action(() => Rtb_log.AppendText($"Đã xóa thành công món: {p.TenMonAn}\n")));
                            }
                            else
                            {
                                responsePacket.Action = "DELETE_FAIL";
                            }

                            string jsonResponse = JsonSerializer.Serialize(responsePacket);
                            byte[] outBuffer = Encoding.UTF8.GetBytes(jsonResponse);
                            stream.Write(outBuffer, 0, outBuffer.Length);
                        }
                    }
                    else if (p.Action == "GET_ALL_DETAILS")
                    {
                        // Tạo danh sách chứa các thông tin chi tiết dưới dạng Dictionary hoặc Class phụ trợ
                        var listDetails = new List<object>();
                        string sql = "SELECT m.TenMonAn, n.HoVaTen, m.ThoiGian FROM MonAn m JOIN NguoiDung n ON m.IDNCC = n.IDNCC";

                        using (var cmd = new SQLiteCommand(sql, conn))
                        using (var reader = cmd.ExecuteReader())
                        {
                            int stt = 1;
                            while (reader.Read())
                            {
                                listDetails.Add(new
                                {
                                    STT = stt++,
                                    NguoiDongGop = reader["HoVaTen"].ToString(),
                                    TenMonAn = reader["TenMonAn"].ToString(),
                                    ThoiGian = reader["ThoiGian"].ToString()
                                });
                            }
                        }

                        Packet responsePacket = new Packet
                        {
                            Action = "DETAILS_RESULT",
                            TenMonAn = JsonSerializer.Serialize(listDetails) // Dùng tạm trường này chứa JSON chi tiết
                        };

                        string jsonResponse = JsonSerializer.Serialize(responsePacket);
                        byte[] outBuffer = Encoding.UTF8.GetBytes(jsonResponse);
                        stream.Write(outBuffer, 0, outBuffer.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                Invoke(new Action(() => Rtb_log.AppendText("Lỗi giao tiếp: " + ex.Message + "\n")));
            }
            finally
            {
                client.Close();
            }
        }
        private void Rtb_log_TextChanged(object sender, EventArgs e)
        {
            // Có thể dùng để tự động cuộn xuống cuối log
            Rtb_log.SelectionStart = Rtb_log.Text.Length;
            Rtb_log.ScrollToCaret();
        }

        private void log_server_Click(object sender, EventArgs e)
        {

        }

        private void Server_Load(object sender, EventArgs e)
        {

        }
    }
}
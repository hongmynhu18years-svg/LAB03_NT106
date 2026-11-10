using LAB03;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;

namespace LAB03
{
    public partial class Lab03_Bai05_server : Form
    {
        private TcpListener server;
        private bool isRunning = false;

        // Dùng để khóa chống đụng độ SQLite khi nhiều Client truy cập cùng lúc
        private static readonly object dbLock = new object();

        public Lab03_Bai05_server()
        {
            InitializeComponent();
            // Đăng ký sự kiện tắt form để đóng Port
            this.FormClosing += Lab03_Bai05_server_FormClosing;
        }

        private async void start_Click(object sender, EventArgs e)
        {
            if (isRunning) return;
            start.Enabled = false; // Tạm khóa nút để không bấm 2 lần

            try
            {
                await Task.Run(() =>
                {
                    string connectionString = "Data Source=data.db;Version=3;";
                    using (var conn = new SQLiteConnection(connectionString))
                    {
                        conn.Open();

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

                        using (var cmd = new SQLiteCommand("DELETE FROM MonAn; DELETE FROM NguoiDung;", conn))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        string sqlUser = "INSERT INTO NguoiDung (HoVaTen, QuyenHan) VALUES ('Quản lý quán', 'Quản trị'); SELECT last_insert_rowid();";
                        long adminId = 0;
                        using (var cmdUser = new SQLiteCommand(sqlUser, conn))
                        {
                            adminId = (long)cmdUser.ExecuteScalar();
                        }

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
                                cmdFood.Parameters.AddWithValue("@img", item.Hinh);
                                cmdFood.Parameters.AddWithValue("@idncc", adminId);
                                cmdFood.Parameters.AddWithValue("@time", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                                cmdFood.ExecuteNonQuery();
                            }
                        }
                    }
                });

                server = new TcpListener(IPAddress.Any, 8888);
                server.Start();
                isRunning = true;

                Rtb_log.AppendText("Server đã reset CSDL và đang chạy ở Port 8888...\n");

                // Khởi chạy vòng lặp nghe mạng ngầm
                _ = Task.Run(() => ListenForClients());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi động Server: " + ex.Message);
                start.Enabled = true;
            }
        }

        private void ListenForClients()
        {
            while (isRunning)
            {
                try
                {
                    TcpClient client = server.AcceptTcpClient();
                    Invoke(new Action(() => Rtb_log.AppendText($"Có Client kết nối: {client.Client.RemoteEndPoint}\n")));

                    // Mỗi Client vào sẽ được cấp 1 Task riêng chạy song song
                    _ = Task.Run(() => HandleClient(client));
                }
                catch
                {
                    // Bỏ qua lỗi khi server đang bị ép đóng
                }
            }
        }

        private void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024 * 5000]; // Buffer 5MB
            string clientIP = client.Client.RemoteEndPoint.ToString();

            try
            {
                int bytesRead;
                // VÒNG LẶP DUY TRÌ KẾT NỐI LIÊN TỤC VỚI CLIENT
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    string jsonReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Lab03_Bai05_packet p = JsonSerializer.Deserialize<Lab03_Bai05_packet>(jsonReceived);

                    string connectionString = "Data Source=data.db;Version=3;";

                    // Khóa DB lại để các Client không ghi đè lên nhau
                    lock (dbLock)
                    {
                        using (var conn = new SQLiteConnection(connectionString))
                        {
                            conn.Open();

                            if (p.Action == "ADD")
                            {
                                string checkSql = "SELECT COUNT(*) FROM MonAn WHERE LOWER(TenMonAn) = LOWER(@foodCheck)";
                                using (var checkCmd = new SQLiteCommand(checkSql, conn))
                                {
                                    checkCmd.Parameters.AddWithValue("@foodCheck", p.TenMonAn?.Trim() ?? "");
                                    long count = (long)checkCmd.ExecuteScalar();

                                    if (count > 0)
                                    {
                                        Lab03_Bai05_packet errPacket = new Lab03_Bai05_packet { Action = "ERROR", TenMonAn = "Món ăn này đã tồn tại trong danh sách!" };
                                        byte[] errBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(errPacket));
                                        stream.Write(errBytes, 0, errBytes.Length);

                                        Invoke(new Action(() => Rtb_log.AppendText($"Từ chối món trùng: {p.TenMonAn}\n")));
                                        continue; // Dùng continue thay vì return để không thoát vòng lặp while
                                    }
                                }

                                string tenNguoiDung = string.IsNullOrWhiteSpace(p.NguoiDongGop) ? "Quản lý quán" : p.NguoiDongGop;
                                Invoke(new Action(() => Rtb_log.AppendText($"Yêu cầu THÊM MÓN ({p.Action}) từ {tenNguoiDung} (IP: {clientIP})\n")));
                                string fileName = "img_" + DateTime.Now.Ticks + ".png";
                                if (!string.IsNullOrEmpty(p.HinhAnhBase64))
                                {
                                    byte[] imageBytes = Convert.FromBase64String(p.HinhAnhBase64);
                                    File.WriteAllBytes(Path.Combine(Application.StartupPath, fileName), imageBytes);
                                }
                                else
                                {
                                    fileName = "";
                                }

                                string sqlUser = "INSERT INTO NguoiDung (HoVaTen, QuyenHan) VALUES (@name, 'Thành viên'); SELECT last_insert_rowid();";
                                long userId = 0;
                                using (var cmdUser = new SQLiteCommand(sqlUser, conn))
                                {
                                    cmdUser.Parameters.AddWithValue("@name", p.NguoiDongGop ?? "Người dùng ẩn danh");
                                    userId = (long)cmdUser.ExecuteScalar();
                                }

                                string sqlFood = "INSERT INTO MonAn (TenMonAn, HinhAnh, IDNCC, ThoiGian) VALUES (@food, @img, @idncc, @time)";
                                using (var cmdFood = new SQLiteCommand(sqlFood, conn))
                                {
                                    cmdFood.Parameters.AddWithValue("@food", p.TenMonAn?.Trim() ?? "");
                                    cmdFood.Parameters.AddWithValue("@img", fileName);
                                    cmdFood.Parameters.AddWithValue("@idncc", userId);
                                    cmdFood.Parameters.AddWithValue("@time", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                                    cmdFood.ExecuteNonQuery();
                                }

                                Lab03_Bai05_packet successPacket = new Lab03_Bai05_packet { Action = "SUCCESS" };
                                byte[] successBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(successPacket));
                                stream.Write(successBytes, 0, successBytes.Length);
                            }
                            else if (p.Action == "RANDOM_ALL" || p.Action == "RANDOM_MINE")
                            {
                                string tenNguoiDung = string.IsNullOrWhiteSpace(p.NguoiDongGop) ? "Quản lý quán" : p.NguoiDongGop;
                                Invoke(new Action(() => Rtb_log.AppendText($"Yêu cầu TÌM MÓN ({p.Action}) từ {tenNguoiDung} (IP: {clientIP})\n")));

                                string sql = (p.Action == "RANDOM_ALL")
                                    ? "SELECT m.TenMonAn, m.HinhAnh, n.HoVaTen FROM MonAn m JOIN NguoiDung n ON m.IDNCC = n.IDNCC ORDER BY RANDOM() LIMIT 1"
                                    : "SELECT m.TenMonAn, m.HinhAnh, n.HoVaTen FROM MonAn m JOIN NguoiDung n ON m.IDNCC = n.IDNCC WHERE LOWER(n.HoVaTen) = LOWER(@name) ORDER BY RANDOM() LIMIT 1";

                                using (var cmd = new SQLiteCommand(sql, conn))
                                {
                                    if (p.Action == "RANDOM_MINE")
                                    {
                                        cmd.Parameters.AddWithValue("@name", p.NguoiDongGop?.Trim() ?? "");
                                    }

                                    using (var reader = cmd.ExecuteReader())
                                    {
                                        Lab03_Bai05_packet responsePacket = new Lab03_Bai05_packet();
                                        if (reader.Read())
                                        {
                                            responsePacket.Action = "RESULT";
                                            responsePacket.TenMonAn = reader["TenMonAn"].ToString();
                                            responsePacket.NguoiDongGop = reader["HoVaTen"].ToString();

                                            string imgFile = reader["HinhAnh"]?.ToString();
                                            if (!string.IsNullOrEmpty(imgFile))
                                            {
                                                string pathInResources = Path.Combine(@"C:\C#\NT106\Lab03\LAB03_NT106\src\Resources", imgFile);
                                                string pathInDebug = Path.Combine(Application.StartupPath, imgFile);

                                                if (File.Exists(pathInResources))
                                                {
                                                    responsePacket.HinhAnhBase64 = Convert.ToBase64String(File.ReadAllBytes(pathInResources));
                                                }
                                                else if (File.Exists(pathInDebug))
                                                {
                                                    responsePacket.HinhAnhBase64 = Convert.ToBase64String(File.ReadAllBytes(pathInDebug));
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
                            else if (p.Action == "GET_LIST")
                            {
                                List<string> danhSachMon = new List<string>();
                                string sql = "SELECT DISTINCT TenMonAn FROM MonAn";
                                using (var cmd = new SQLiteCommand(sql, conn))
                                using (var reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        danhSachMon.Add(reader["TenMonAn"].ToString());
                                    }
                                }

                                Lab03_Bai05_packet responsePacket = new Lab03_Bai05_packet
                                {
                                    Action = "LIST_RESULT",
                                    TenMonAn = JsonSerializer.Serialize(danhSachMon)
                                };

                                string jsonResponse = JsonSerializer.Serialize(responsePacket);
                                byte[] outBuffer = Encoding.UTF8.GetBytes(jsonResponse);
                                stream.Write(outBuffer, 0, outBuffer.Length);
                            }
                            else if (p.Action == "DELETE")
                            {
                                string tenNguoiDung = string.IsNullOrWhiteSpace(p.NguoiDongGop) ? "Quản lý quán" : p.NguoiDongGop;
                                Invoke(new Action(() => Rtb_log.AppendText($"Yêu cầu XÓA MÓN ({p.Action}) từ {tenNguoiDung} (IP: {clientIP})\n")));

                                string sql = "DELETE FROM MonAn WHERE TenMonAn = @ten";
                                using (var cmd = new SQLiteCommand(sql, conn))
                                {
                                    cmd.Parameters.AddWithValue("@ten", p.TenMonAn?.Trim() ?? "");
                                    int rowsAffected = cmd.ExecuteNonQuery();

                                    Lab03_Bai05_packet responsePacket = new Lab03_Bai05_packet();
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

                                Lab03_Bai05_packet responsePacket = new Lab03_Bai05_packet
                                {
                                    Action = "DETAILS_RESULT",
                                    TenMonAn = JsonSerializer.Serialize(listDetails)
                                };

                                string jsonResponse = JsonSerializer.Serialize(responsePacket);
                                byte[] outBuffer = Encoding.UTF8.GetBytes(jsonResponse);
                                stream.Write(outBuffer, 0, outBuffer.Length);
                            }
                        }
                    } // Hết khối lock DB
                } // Hết vòng lặp while (Client ngắt kết nối sẽ thoát vòng lặp)
            }
            catch (Exception ex)
            {
                Invoke(new Action(() => Rtb_log.AppendText($"Client {clientIP} ngắt kết nối đột ngột.\n")));
            }
            finally
            {
                client.Close();
                Invoke(new Action(() => Rtb_log.AppendText($"Đã đóng kết nối với Client {clientIP}\n")));
            }
        }

        // Sự kiện giải phóng Port khi tắt Form
        private void Lab03_Bai05_server_FormClosing(object sender, FormClosingEventArgs e)
        {
            isRunning = false;
            if (server != null)
            {
                server.Stop();
            }
            Environment.Exit(0);
        }

        private void Rtb_log_TextChanged(object sender, EventArgs e)
        {
            Rtb_log.SelectionStart = Rtb_log.Text.Length;
            Rtb_log.ScrollToCaret();
        }

        private void log_server_Click(object sender, EventArgs e) { }
        private void Server_Load(object sender, EventArgs e) { }
    }
}
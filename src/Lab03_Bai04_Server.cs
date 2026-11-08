using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace LAB03
{
    public class Lab03_Bai04_Server
    {
        private const int Port = 8080;
        private static readonly string DataFile =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lab03_Bai04_Seats.txt");

        private static TcpListener server = null;
        private static bool isRunning = false;
        private static readonly Dictionary<string, HashSet<string>> bookedSeatsStore =
            new Dictionary<string, HashSet<string>>();
        private static readonly List<StreamWriter> clientWriters = new List<StreamWriter>();
        private static readonly object lockObj = new object();
        private static readonly object startLock = new object();

        public static void StartServer()
        {
            lock (startLock)
            {
                if (isRunning) return;
                isRunning = true;
            }

            Thread serverThread = new Thread(() =>
            {
                try
                {
                    server = new TcpListener(IPAddress.Any, Port);
                    server.Start();
                    LoadData();

                    while (isRunning)
                    {
                        TcpClient client = server.AcceptTcpClient();
                        Thread clientThread = new Thread(() => HandleClient(client));
                        clientThread.IsBackground = true;
                        clientThread.Start();
                    }
                }
                catch (SocketException)
                {
                    // Cổng đã được tiến trình khác mở -> tiến trình này chỉ đóng vai trò Client
                    isRunning = false;
                }
                catch
                {
                    isRunning = false;
                }
            });
            serverThread.IsBackground = true;
            serverThread.Start();
        }

        // Dữ liệu phòng vé mặc định đọc từ file: movieKey|A1,A2,...
        private static void LoadData()
        {
            lock (lockObj)
            {
                bookedSeatsStore.Clear();
                if (!File.Exists(DataFile)) return;

                foreach (string line in File.ReadAllLines(DataFile, Encoding.UTF8))
                {
                    string[] parts = line.Split('|');
                    if (parts.Length < 2 || string.IsNullOrWhiteSpace(parts[0])) continue;

                    bookedSeatsStore[parts[0]] = new HashSet<string>(
                        parts[1].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                }
            }
        }

        // Gọi trong lockObj
        private static void SaveData()
        {
            try
            {
                List<string> lines = new List<string>();
                foreach (var pair in bookedSeatsStore)
                    lines.Add(pair.Key + "|" + string.Join(",", pair.Value));
                File.WriteAllLines(DataFile, lines, Encoding.UTF8);
            }
            catch { }
        }

        private static void Send(StreamWriter writer, string message)
        {
            try
            {
                lock (writer)
                {
                    writer.WriteLine(message);
                }
            }
            catch { }
        }

        private static string GetSeatsString(string movieKey)
        {
            lock (lockObj)
            {
                if (!bookedSeatsStore.ContainsKey(movieKey))
                    bookedSeatsStore[movieKey] = new HashSet<string>();
                return string.Join(",", bookedSeatsStore[movieKey]);
            }
        }

        private static void HandleClient(TcpClient client)
        {
            StreamWriter writer = null;
            try
            {
                NetworkStream stream = client.GetStream();
                UTF8Encoding utf8 = new UTF8Encoding(false);
                StreamReader reader = new StreamReader(stream, utf8);
                writer = new StreamWriter(stream, utf8) { AutoFlush = true };

                lock (lockObj)
                {
                    clientWriters.Add(writer);
                }

                string request;
                while ((request = reader.ReadLine()) != null)
                {
                    string[] parts = request.Split('|');
                    string command = parts[0];

                    if (command == "GET_SEATS" && parts.Length > 1)
                    {
                        string movieKey = parts[1];
                        Send(writer, $"SEATS|{movieKey}|{GetSeatsString(movieKey)}");
                    }
                    else if (command == "GET_ALL")
                    {
                        // Trả toàn bộ dữ liệu: movieKey=A1,A2;movieKey2=B1
                        string data;
                        lock (lockObj)
                        {
                            data = string.Join(";", bookedSeatsStore.Select(
                                p => p.Key + "=" + string.Join(",", p.Value)));
                        }
                        Send(writer, "ALL_DATA|" + data);
                    }
                    else if (command == "BOOK" && parts.Length > 2)
                    {
                        string movieKey = parts[1];
                        string[] seatsToBook = parts[2].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                        if (seatsToBook.Length == 0)
                        {
                            Send(writer, "FAILED|Chưa chọn ghế nào!");
                            continue;
                        }

                        bool success = true;
                        lock (lockObj)
                        {
                            if (!bookedSeatsStore.ContainsKey(movieKey))
                                bookedSeatsStore[movieKey] = new HashSet<string>();

                            HashSet<string> booked = bookedSeatsStore[movieKey];
                            foreach (string seat in seatsToBook)
                            {
                                if (booked.Contains(seat))
                                {
                                    success = false;
                                    break;
                                }
                            }

                            if (success)
                            {
                                foreach (string seat in seatsToBook)
                                    booked.Add(seat);
                                SaveData();
                            }
                        }

                        if (success)
                        {
                            Send(writer, "SUCCESS|Đặt vé thành công!");
                            BroadcastUpdate(movieKey);
                        }
                        else
                        {
                            Send(writer, "FAILED|Ghế đã có người khác đặt trước đó!");
                        }
                    }
                }
            }
            catch
            {
                // Client ngắt kết nối
            }
            finally
            {
                if (writer != null)
                {
                    lock (lockObj)
                    {
                        clientWriters.Remove(writer);
                    }
                }
                client.Close();
            }
        }

        private static void BroadcastUpdate(string movieKey)
        {
            string updateMessage = $"SEAT_UPDATED|{movieKey}|{GetSeatsString(movieKey)}";

            List<StreamWriter> currentWriters;
            lock (lockObj)
            {
                currentWriters = new List<StreamWriter>(clientWriters);
            }

            foreach (StreamWriter w in currentWriters)
                Send(w, updateMessage);
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace LAB03
{
    public class Lab03_Bai04_Server
    {
        private static TcpListener server = null;
        private static bool isRunning = false;
        private static Dictionary<string, HashSet<string>> bookedSeatsStore = new Dictionary<string, HashSet<string>>();
        private static readonly object lockObj = new object();

        public static void StartServer()
        {
            if (isRunning) return;
            Thread serverThread = new Thread(() =>
            {
                int port = 8080;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                try
                {
                    server = new TcpListener(localAddr, port);
                    server.Start();
                    isRunning = true;
                    System.Diagnostics.Debug.WriteLine("[SERVER] Đã khởi động tại cổng 8080.");

                    while (isRunning)
                    {
                        TcpClient client = server.AcceptTcpClient();
                        Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClient));
                        clientThread.Start(client);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("[SERVER] Lỗi: " + ex.Message);
                }
            });
            serverThread.IsBackground = true;
            serverThread.Start();
        }

        private static void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            try
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true })
                {
                    string request = reader.ReadLine();
                    if (string.IsNullOrEmpty(request)) return;

                    string[] parts = request.Split('|');
                    string command = parts[0];

                    if (command == "GET_SEATS")
                    {
                        string phim = parts[1];
                        string phong = parts[2];
                        string key = $"{phim}_{phong}";

                        lock (lockObj)
                        {
                            if (bookedSeatsStore.ContainsKey(key))
                            {
                                writer.WriteLine(string.Join(" ", bookedSeatsStore[key]));
                            }
                            else
                            {
                                writer.WriteLine("");
                            }
                        }
                    }
                    else if (command == "BOOK")
                    {
                        string phim = parts[1];
                        string phong = parts[2];
                        string[] danhSachGheMoi = parts[3].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string key = $"{phim}_{phong}";

                        lock (lockObj)
                        {
                            if (!bookedSeatsStore.ContainsKey(key))
                            {
                                bookedSeatsStore[key] = new HashSet<string>();
                            }

                            bool conflict = false;
                            foreach (var ghe in danhSachGheMoi)
                            {
                                if (bookedSeatsStore[key].Contains(ghe))
                                {
                                    conflict = true;
                                    break;
                                }
                            }

                            if (conflict)
                            {
                                writer.WriteLine("FAILED|Ghế đã có quầy khác đặt!");
                            }
                            else
                            {
                                foreach (var ghe in danhSachGheMoi)
                                {
                                    bookedSeatsStore[key].Add(ghe);
                                }
                                writer.WriteLine("SUCCESS");
                            }
                        }
                    }
                }
            }
            catch { }
            finally
            {
                client.Close();
            }
        }
    }
}
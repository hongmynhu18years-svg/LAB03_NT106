<h1 align="center">🌐 LAB 3 – WORKING WITH SOCKETS IN C#</h1>

<div align="center">

### LẬP TRÌNH SOCKETS TRONG C#

*Bài thực hành nhóm môn Lập trình mạng căn bản – Xây dựng ứng dụng giao tiếp Client–Server sử dụng UDP, TCP và Multithreading.*

<a href ="https://learn.microsoft.com/vi-vn/dotnet/csharp/" target="_blank" rel="noopener noreferrer">
    <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white" alt="C#" />
  </a>
   <a href ="https://dotnet.microsoft.com/" target="_blank" rel="noopener noreferrer">
     <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET" />
  </a>
  <a href ="https://learn.microsoft.com/vi-vn/dotnet/desktop/winforms/" target="_blank" rel="noopener noreferrer">
     <img src="https://img.shields.io/badge/Windows_Forms-0078D4?style=for-the-badge&logo=windows&logoColor=white" alt="Windows Forms" />
  </a>
  <a href="https://www.sqlite.org/">
     <img src="https://img.shields.io/badge/SQLite-003B57?style=for-the-badge&logo=sqlite&logoColor=white" alt="SQLite">
  </a>
  <a href="https://vi.wikipedia.org/wiki/TCP">
     <img src="https://img.shields.io/badge/TCP-00599C?style=for-the-badge">
  </a>
   <a href="https://vi.wikipedia.org/wiki/UDP">
     <img src="https://img.shields.io/badge/UDP-00897B?style=for-the-badge">
  </a>
  <a href ="https://git-scm.com/" target="_blank" rel="noopener noreferrer">
     <img src="https://img.shields.io/badge/Git-F05032?style=for-the-badge&logo=git&logoColor=white" alt="Git" />
  </a>
  <a href ="https://github.com/hongmynhu18years-svg/LAB03_NT106" target="_blank" rel="noopener noreferrer">
     <img src="https://img.shields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white" alt="GitHub" />
  </a>
  
</div>

---

## 👥 Thông tin nhóm thực hiện

| Nội dung | Thông tin |
|---|---|
| **Môn học** | Lập trình mạng căn bản |
| **Bài thực hành** | Lab 3 – Working with Sockets in C# |
| **Nhóm** | 3 |
| **Lớp** | NT106.R14.2 |

## 🧑‍💻 Danh sách thành viên

| STT | Họ và tên | MSSV | 
|:---:|---|:---:|
| 1 | Hồng Mỹ Như | 25521342 | 
| 2 | Tô Thị Thanh Nhi | 25521332 | 
| 3 | Trương Vũ Hoàng Ngân | 25521185 | 
| 4 | Võ Thị Minh Thư | 25521816 |
| 5 | Lê Hoàn Phúc | 25521422 |

---

## 📖 Giới thiệu

**LAB 3 – Working with Sockets in C#** là bài thực hành nhóm thuộc môn **Lập trình mạng căn bản**, tập trung xây dựng các ứng dụng giao tiếp qua mạng bằng ngôn ngữ **C#** trên nền tảng **Windows Forms**.

Bài thực hành giúp sinh viên làm quen với lập trình **Socket**, triển khai mô hình **Client–Server**, sử dụng hai giao thức **UDP** và **TCP** để truyền nhận dữ liệu. Đồng thời, nhóm vận dụng **Multithreading** để xử lý kết nối và giao tiếp giữa nhiều Client, kết hợp quản lý và đồng bộ dữ liệu trong các ứng dụng thực tế.

Tập hợp các bài thực hành Lab 3 trên một giao diện Windows Forms. Người dùng chọn bài tập từ form chính để thực hiện các chức năng từng bài tương ứng.



### 🎯 Mục tiêu thực hành

- Hiểu nguyên lý hoạt động của Socket và mô hình Client–Server.
- Xây dựng ứng dụng gửi và nhận thông điệp bằng UDP và TCP.
- Sử dụng các lớp `Socket`, `UdpClient`, `TcpClient` và `TcpListener`.
- Áp dụng Multithreading để xử lý các kết nối mạng.
- Phát triển ứng dụng hỗ trợ nhiều Client và đồng bộ dữ liệu thông qua Server.
- Kết hợp SQLite để lưu trữ và quản lý dữ liệu tập trung.

### 📚 Nội dung thực hành

| Bài | Tên bài thực hành | Mô hình / Công nghệ |
|:---:|---|---|
| **01** | Gửi và nhận dữ liệu bằng UDP | UDP Client – UDP Server |
| **02** | Lắng nghe dữ liệu từ Telnet | TCP Socket |
| **03** | Gửi và nhận dữ liệu bằng TCP | 1 Server – 1 Client |
| **04** | Quản lý phòng vé | 1 Server – Multi Client |
| **05** | Hôm nay ăn gì? | 1 Server – Multi Client, SQLite |
| **06** | Chat Room – Nhắn tin chung và riêng | TCP Client – TCP Listener |

---

## 📡 Bài 1 — Gửi và nhận dữ liệu bằng UDP

### 🖼️ Giao diện chương trình

**1. Form điều hướng — Gửi và nhận dữ liệu**

<img width="439" height="244" alt="Giao diện Form điều hướng Bài 1" src="https://github.com/user-attachments/assets/2a9d7c16-5015-4bf3-af4f-87d6ded87d09" />

**2. Giao diện UDP Client**

<img width="777" height="431" alt="Giao diện UDP Client" src="https://github.com/user-attachments/assets/3ae1f0c5-f0c9-4257-aacf-67d310f9d9fd" />

**3. Giao diện UDP Server**

<img width="476" height="505" alt="Giao diện UDP Server" src="https://github.com/user-attachments/assets/c2a815ca-cdaa-42e9-9926-972b0b4f724a" />

### 🎯 Nội dung thực hành

- Xây dựng ứng dụng gửi và nhận dữ liệu giữa hai bên thông qua giao thức **UDP (User Datagram Protocol)**.
- Cho phép người dùng tại **UDP Client** nhập địa chỉ IP, Port và thông điệp cần gửi.
- **UDP Server** lắng nghe tại Port được chỉ định, tiếp nhận thông điệp từ Client và hiển thị trên giao diện.
- Hỗ trợ gửi và nhận nội dung tiếng Việt có dấu thông qua mã hóa **UTF-8**.
- Kiểm tra dữ liệu đầu vào, xử lý lỗi kết nối và quản lý tài nguyên khi đóng chương trình.

### 🛠️ Cách thức thực hiện

**1. Form điều hướng (`Lab03_Bai01`)**

- Khởi tạo giao diện bằng `InitializeComponent()`.
- Sử dụng `btnServer_Click()` để mở form `Lab03_Bai01_UDPServer`.
- Sử dụng `btnUDPCLient_Click()` để mở form `Lab03_Bai01_UDPClient`.
- Sử dụng `btnThoat_Click()` để đóng form điều hướng.

**2. UDP Client (`Lab03_Bai01_UDPClient`)**

- Khởi tạo địa chỉ IP mặc định `127.0.0.1` và Port `9000` để kiểm thử trên cùng một máy.
- Đọc địa chỉ IP từ `tbxRemoteIP`, Port từ `tbxRemotePort` và thông điệp từ `rtbMessage`.
- Sử dụng `IPAddress.TryParse()` để kiểm tra địa chỉ IP hợp lệ.
- Sử dụng `int.TryParse()` để kiểm tra Port trong khoảng `1–65535`.
- Kiểm tra thông điệp không được để trống bằng `string.IsNullOrWhiteSpace()`.
- Sử dụng `Encoding.UTF8.GetBytes()` để chuyển thông điệp thành mảng byte.
- Khởi tạo `UdpClient` và sử dụng `Send()` để gửi dữ liệu đến IP và Port của Server.
- Sử dụng `using` để tự động giải phóng `UdpClient` sau khi gửi.
- Xử lý lỗi gửi dữ liệu bằng `try-catch` với `SocketException`.
- Hiển thị thông báo sau khi thực hiện gửi và sử dụng `Close()` để thoát form.

**3. UDP Server (`Lab03_Bai01_UDPServer`)**

- Đọc và kiểm tra Port từ `tbxServerPort`, đảm bảo giá trị trong khoảng `1–65535`.
- Sử dụng `new UdpClient(port)` để khởi tạo Server lắng nghe tại Port được chỉ định.
- Sử dụng `CancellationTokenSource` để quản lý việc hủy tác vụ nhận dữ liệu.
- Sử dụng `ReceiveMessagesAsync()` và `ReceiveAsync()` để nhận dữ liệu UDP bất đồng bộ.
- Sử dụng `Encoding.UTF8.GetString()` để chuyển dữ liệu nhận được từ mảng byte thành chuỗi.
- Lấy địa chỉ IP và Port của Client thông qua `result.RemoteEndPoint`.
- Hiển thị thời gian nhận, địa chỉ IP, Port và nội dung thông điệp trên `lstReceivedMessages`.
- Sử dụng `InvokeRequired` kết hợp `BeginInvoke()` trong `AppendMessage()` để cập nhật giao diện an toàn giữa các luồng.
- Tự động cuộn đến thông điệp mới nhất bằng `EnsureVisible()`.
- Vô hiệu hóa nút Listen và ô nhập Port trong lúc Server đang lắng nghe.
- Sử dụng `Cancel()` và `Dispose()` để hủy tác vụ nhận, đóng Socket và giải phóng tài nguyên khi thoát.

### ▶️ Hướng dẫn sử dụng

**1. Khởi động UDP Server**

1. Mở **Bài 1** từ giao diện chính của Lab 3.
2. Nhấn **UDP Server** để mở cửa sổ Server.
3. Nhập Port cần lắng nghe, ví dụ `9000`.
4. Nhấn **Listen** để bắt đầu nhận dữ liệu.
5. Kiểm tra thông báo `Server đang lắng nghe tại Port 9000...`.
6. Giữ cửa sổ Server hoạt động để tiếp nhận thông điệp.

**2. Gửi thông điệp từ UDP Client**

1. Tại form điều hướng, nhấn **UDP Client**.
2. Nhập địa chỉ IP của Server vào ô **IP Remote host**.
   - Cùng máy: `127.0.0.1`.
   - Khác máy trong mạng LAN: nhập địa chỉ IPv4 của máy chạy Server.
3. Nhập Port `9000` vào ô **Port**.
4. Nhập nội dung cần gửi vào ô **Message**, ví dụ `Xin chào UDP Server!`.
5. Nhấn **Send** để gửi thông điệp.
6. Client hiển thị thông báo `Đã gửi thông điệp qua UDP!` nếu thao tác gửi không phát sinh lỗi.
7. Kiểm tra cửa sổ Server để xem thông điệp được tiếp nhận.

**3. Kiểm tra kết quả tại Server**

Khi nhận được thông điệp, Server hiển thị dữ liệu theo cấu trúc:

```text
[14:30:25] 127.0.0.1:54321
Xin chào UDP Server!
```

Trong đó:

- `14:30:25`: Thời gian Server nhận thông điệp.
- `127.0.0.1`: Địa chỉ IP của Client.
- `54321`: Port nguồn của Client (ví dụ minh họa).
- `Xin chào UDP Server!`: Nội dung thông điệp nhận được.

**4. Thoát chương trình**

1. Nhấn **Thoát** tại UDP Client để đóng cửa sổ Client.
2. Nhấn **Thoát** tại UDP Server để dừng lắng nghe và đóng Socket.
3. Nhấn **Thoát** tại form điều hướng để kết thúc Bài 1.

> [!NOTE]
> - IP `127.0.0.1` chỉ dùng để giao tiếp trên cùng một máy. Khi chạy trên hai máy khác nhau trong LAN, phải sử dụng địa chỉ IP thực tế của Server.
> - Port tại Client phải trùng với Port mà Server đang lắng nghe.
> - Port hợp lệ nằm trong khoảng `1–65535`. Port mặc định được thiết lập tại Client là `9000`.
> - Thông điệp không được để trống hoặc chỉ chứa khoảng trắng.
> - Chương trình hỗ trợ Tiếng Việt có dấu nhờ mã hóa UTF-8.
> - UDP không đảm bảo thông điệp luôn được chuyển đến Server. Thông báo gửi thành công tại Client không đồng nghĩa Server đã nhận dữ liệu.
> - UDP Client sử dụng Port nguồn do hệ điều hành cấp phát, vì vậy Port nguồn hiển thị tại Server có thể thay đổi giữa các lần gửi.
> - Khi Port đã được ứng dụng khác sử dụng, Server có thể không khởi động được và sẽ hiển thị thông báo lỗi.
> - Khi kiểm thử trên các máy khác nhau, cần đảm bảo kết nối mạng và cấu hình tường lửa cho phép lưu lượng UDP tại Port tương ứng.
> - Khi đóng Server, chương trình hủy tác vụ nhận dữ liệu và giải phóng Socket để tránh chiếm dụng tài nguyên.

---


## 🖥️ Bài 2 — Lắng nghe dữ liệu từ Telnet bằng TCP

### 🖼️ Giao diện chương trình

**Giao diện TCP Server — FormTelnet**

<img width="730" height="521" alt="Giao diện TCP Server - FormTelnet" src="https://github.com/user-attachments/assets/ea802b0e-761e-47f8-a76f-ee0563f70a6c" />

### 🎯 Nội dung thực hành

- Xây dựng chương trình **TCP Server** có khả năng lắng nghe và tiếp nhận dữ liệu được gửi từ Telnet.
- Sử dụng giao thức **TCP (Transmission Control Protocol)** để thiết lập kết nối giữa Telnet Client và Server.
- Server lắng nghe tại địa chỉ IP `127.0.0.1` và Port `8080`.
- Cho phép người dùng gửi thông điệp từ Telnet thông qua Command Prompt (CMD).
- Nhận và hiển thị các thông điệp trên giao diện **Windows Forms**.
- Sử dụng **Thread** để chạy Server trên luồng riêng, tránh làm treo giao diện.
- Hiển thị thời gian nhận và tự động cuộn đến thông điệp mới nhất.

### 🛠️ Cách thức thực hiện

**1. Khởi tạo TCP Server**

- Sử dụng lớp `TcpListener` trong `System.Net.Sockets` để tạo Server.
- Thiết lập địa chỉ IP `127.0.0.1` và Port `8080` bằng `new TcpListener(IPAddress.Parse("127.0.0.1"), 8080)`.
- Sử dụng `listener.Start()` để bắt đầu lắng nghe kết nối TCP.
- Hiển thị thông báo `Telnet running on 127.0.0.1:8080` khi Server khởi động thành công.

**2. Xử lý sự kiện nút Listen**

- Bắt sự kiện `btnListen_Click()` khi người dùng nhấn nút **Listen**.
- Sử dụng biến `isListening` để kiểm tra trạng thái hoạt động của Server.
- Đổi nội dung nút thành `Listening...` và vô hiệu hóa nút để tránh khởi động Server nhiều lần.
- Khởi tạo luồng riêng bằng `new Thread(StartServer)`.
- Thiết lập `IsBackground = true` để luồng không ngăn tiến trình kết thúc.
- Gọi `Start()` để bắt đầu thực thi hàm `StartServer()`.

**3. Chấp nhận kết nối từ Telnet Client**

- Sử dụng `listener.AcceptTcpClient()` để chờ và chấp nhận kết nối TCP từ Client.
- Hiển thị thông báo `New client connected!` khi có Client kết nối.
- Sử dụng `client.GetStream()` để lấy `NetworkStream` phục vụ việc nhận dữ liệu.
- Khởi tạo `StreamReader` với mã hóa `Encoding.ASCII` để đọc dữ liệu từ luồng mạng.

**4. Nhận và xử lý thông điệp**

- Sử dụng `reader.ReadLine()` để đọc dữ liệu theo từng dòng.
- Khi người dùng nhập thông điệp và nhấn **Enter** trong Telnet, Server nhận nội dung dòng tương ứng.
- Sử dụng vòng lặp `while` để tiếp tục đọc cho đến khi Client ngắt kết nối.
- Gọi `AddMessage()` để hiển thị thông điệp trên `ListView`.
- Sử dụng `client.Close()` để đóng kết nối Client sau khi kết thúc nhận dữ liệu.

**5. Cập nhật giao diện an toàn giữa các luồng**

- Sử dụng `InvokeRequired` để kiểm tra việc cập nhật `ListView` từ luồng khác.
- Sử dụng `Invoke()` để chuyển thao tác cập nhật về luồng giao diện khi cần thiết.
- Sử dụng `DateTime.Now.ToString("HH:mm:ss")` để lấy thời gian hiển thị thông điệp.
- Thêm dữ liệu vào `lvMessages` bằng `ListViewItem`.
- Sử dụng `EnsureVisible()` để tự động cuộn đến thông điệp mới nhất.
- Kiểm tra `IsDisposed` trước khi cập nhật để hạn chế thao tác trên Control đã bị hủy.

**6. Xử lý lỗi**

- Sử dụng `try-catch` để bắt các ngoại lệ phát sinh trong quá trình khởi tạo, kết nối và nhận dữ liệu.
- Hiển thị thông báo lỗi trong `ListView` thông qua `AddMessage()` khi Server đang ở trạng thái lắng nghe.

### ▶️ Hướng dẫn sử dụng

**1. Khởi động TCP Server**

1. Mở **Bài 2** từ giao diện chính của Lab 3.
2. Nhấn nút **Listen** trên cửa sổ `FormTelnet`.
3. Server bắt đầu lắng nghe tại địa chỉ `127.0.0.1` và Port `8080`.
4. Nút Listen chuyển thành `Listening...` và bị vô hiệu hóa.
5. Kiểm tra thông báo khởi động trong khu vực **Messages**.

**2. Kết nối bằng Telnet**

1. Mở **Command Prompt (CMD)** trên Windows.
2. Nhập lệnh:

   ```cmd
   telnet 127.0.0.1 8080
   ```

3. Nhấn **Enter** để thực hiện kết nối.
4. Khi kết nối thành công, Server hiển thị thông báo `New client connected!`.

**3. Gửi và nhận thông điệp**

1. Tại cửa sổ Telnet, nhập một thông điệp, ví dụ:

   ```text
   Hello TCP Server!
   ```

2. Nhấn **Enter** để gửi thông điệp.
3. Server nhận dữ liệu và hiển thị thông điệp trong khu vực **Messages**.
4. Tiếp tục nhập các thông điệp khác để kiểm tra khả năng nhận dữ liệu liên tục.

**4. Kiểm tra kết quả trên Server**

Ví dụ nội dung hiển thị:

```text
[15:20:01] Telnet running on 127.0.0.1:8080
[15:20:10] New client connected!
[15:20:15] Hello TCP Server!
[15:20:20] Testing TCP connection
```

Trong đó:

- `[15:20:01]`: Thời gian ghi nhận sự kiện.
- `Telnet running...`: Server đã bắt đầu lắng nghe.
- `New client connected!`: Có Client kết nối thành công.
- `Hello TCP Server!`: Nội dung được gửi từ Telnet Client.

**5. Kết thúc chương trình**

1. Đóng cửa sổ Telnet để ngắt kết nối Client.
2. Server kết thúc vòng lặp đọc dữ liệu của Client hiện tại.
3. Đóng cửa sổ `FormTelnet` để kết thúc chương trình.

> [!NOTE]
> - Chương trình sử dụng giao thức **TCP**, yêu cầu thiết lập kết nối trước khi truyền dữ liệu.
> - Địa chỉ `127.0.0.1` là địa chỉ Loopback, chỉ cho phép kết nối từ cùng một máy.
> - Server sử dụng Port cố định `8080`; lệnh Telnet phải kết nối đúng Port này.
> - Cần nhấn **Listen** trước khi thực hiện kết nối Telnet.
> - Nếu Windows chưa bật Telnet Client, cần kích hoạt tính năng này trong **Windows Features** hoặc sử dụng lệnh `dism /online /Enable-Feature /FeatureName:TelnetClient`.
> - Chương trình sử dụng `Encoding.ASCII` để đọc thông điệp, vì vậy không hỗ trợ đầy đủ tiếng Việt có dấu.
> - `ReadLine()` đọc thông điệp theo từng dòng; dữ liệu chỉ được xử lý khi nhận được ký tự kết thúc dòng.
> - Server xử lý lần lượt từng Client, chưa hỗ trợ giao tiếp đồng thời với nhiều Client.
> - Khi Port `8080` đang được chương trình khác sử dụng, Server có thể không khởi động được.
> - Code hiện tại chưa triển khai nút **Stop** và chưa có cơ chế dừng `TcpListener` một cách chủ động khi đóng Form.
> - `Thread` được thiết lập `IsBackground = true`, giúp luồng nền không giữ tiến trình tiếp tục chạy sau khi các luồng foreground đã kết thúc.

---


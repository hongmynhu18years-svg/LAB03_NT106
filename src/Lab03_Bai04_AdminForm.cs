using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LAB03.Properties;

namespace LAB03
{
    public partial class Lab03_Bai04_AdminForm : Form
    {
        public Lab03_Bai04_AdminForm()
        {
            InitializeComponent();
        }

        private List<ThongKePhim> danhSachSapXep = new List<ThongKePhim>();

        public class ThongKePhim
        {
            public string TenPhim { get; set; }
            public decimal GiaVeChuan { get; set; }
            public string PhongChieu { get; set; }
            public int TongSoVe { get; set; }
            public int SoLuongVeBan { get; set; }

            public int SoLuongVeTon => TongSoVe - SoLuongVeBan;
            public double TileBanRa => TongSoVe > 0 ? (double)SoLuongVeBan / TongSoVe * 100 : 0;
            public decimal DoanhThu => SoLuongVeBan * GiaVeChuan;
            public int XepHang { get; set; }
        }

        private int currentFrameAdmin = 0;
        private int frameCountAdmin1 = 0;
        private int frameCountAdmin2 = 0;
        private FrameDimension dimensionAdmin1;
        private FrameDimension dimensionAdmin2;

        private void AdminForm_Load(object sender, EventArgs e)
        {
            if (pictureBoxMouseDown != null)
            {
                Image gifImg1 = Properties.Resources.Gemini_Generated_Gif_j8cnlfj8cnlfj8cn;
                pictureBoxMouseDown.Image = gifImg1;
                pictureBoxMouseDown.SendToBack();

                if (gifImg1 != null && gifImg1.FrameDimensionsList.Length > 0)
                {
                    dimensionAdmin1 = new FrameDimension(gifImg1.FrameDimensionsList[0]);
                    frameCountAdmin1 = gifImg1.GetFrameCount(dimensionAdmin1);
                }
            }

            if (bg != null)
            {
                Image gifImg2 = Properties.Resources.cinema;
                bg.Image = gifImg2;
                bg.SendToBack();

                if (gifImg2 != null && gifImg2.FrameDimensionsList.Length > 0)
                {
                    dimensionAdmin2 = new FrameDimension(gifImg2.FrameDimensionsList[0]);
                    frameCountAdmin2 = gifImg2.GetFrameCount(dimensionAdmin2);
                }
            }

            System.Windows.Forms.Timer timerAdminGif = new System.Windows.Forms.Timer();
            timerAdminGif.Interval = 120;
            timerAdminGif.Tick += (s, ev) =>
            {
                currentFrameAdmin++;

                if (pictureBoxMouseDown != null && pictureBoxMouseDown.Image != null && frameCountAdmin1 > 0)
                {
                    int f1 = currentFrameAdmin % frameCountAdmin1;
                    pictureBoxMouseDown.Image.SelectActiveFrame(dimensionAdmin1, f1);
                    pictureBoxMouseDown.Invalidate();
                }

                if (bg != null && bg.Image != null && frameCountAdmin2 > 0)
                {
                    int f2 = currentFrameAdmin % frameCountAdmin2;
                    bg.Image.SelectActiveFrame(dimensionAdmin2, f2);
                    bg.Invalidate();
                }
            };
            timerAdminGif.Start();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private static extern void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private static extern void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void pictureBoxMouseDown_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            }
        }

        private async void btnXuatThongKe_Click(object sender, EventArgs e)
        {
            if (progressBar1 != null)
            {
                progressBar1.Visible = true;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = 100;
                progressBar1.Value = 0;
            }

            for (int i = 1; i <= 30; i += 5)
            {
                if (progressBar1 != null) progressBar1.Value = i;
                await Task.Delay(20);
            }

            List<ThongKePhim> danhSachtho = new List<ThongKePhim>();
            string inputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input5.txt");

            if (File.Exists(inputPath))
            {
                string[] lines = File.ReadAllLines(inputPath);
                for (int i = 0; i < lines.Length; i += 5)
                {
                    if (i + 4 < lines.Length)
                    {
                        string tenPhim = lines[i].Trim();
                        decimal.TryParse(lines[i + 1].Trim(), out decimal giaVe);
                        string phongChieu = lines[i + 2].Trim();
                        int.TryParse(lines[i + 3].Trim(), out int tongSoVe);
                        int.TryParse(lines[i + 4].Trim(), out int soVeBanThucTe);

                        danhSachtho.Add(new ThongKePhim
                        {
                            TenPhim = tenPhim,
                            GiaVeChuan = giaVe,
                            PhongChieu = phongChieu,
                            TongSoVe = tongSoVe,
                            SoLuongVeBan = soVeBanThucTe
                        });
                    }
                }
            }
            else
            {
                MessageBox.Show(this, "Không tìm thấy file input5.txt từ form Booking! Vui lòng đặt vé trước.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            for (int i = 35; i <= 70; i += 5)
            {
                if (progressBar1 != null) progressBar1.Value = i;
                await Task.Delay(20);
            }

            var danhSachGomNhom = danhSachtho
                .GroupBy(p => new { p.TenPhim, p.PhongChieu })
                .Select(g => new ThongKePhim
                {
                    TenPhim = g.Key.TenPhim,
                    PhongChieu = g.Key.PhongChieu,
                    GiaVeChuan = g.First().GiaVeChuan,
                    TongSoVe = g.First().TongSoVe,
                    SoLuongVeBan = g.Sum(x => x.SoLuongVeBan)
                })
                .ToList();

            danhSachSapXep = danhSachGomNhom.OrderByDescending(p => p.DoanhThu).ToList();
            for (int i = 0; i < danhSachSapXep.Count; i++)
            {
                danhSachSapXep[i].XepHang = i + 1;
            }

            for (int i = 75; i <= 90; i += 5)
            {
                if (progressBar1 != null) progressBar1.Value = i;
                await Task.Delay(20);
            }

            lvThongKe.View = View.Details;
            lvThongKe.FullRowSelect = true;
            lvThongKe.GridLines = true;

            if (lvThongKe.Columns.Count == 0)
            {
                lvThongKe.Columns.Add("Tên phim", 120);
                lvThongKe.Columns.Add("Phòng", 80);
                lvThongKe.Columns.Add("Giá vé", 90);
                lvThongKe.Columns.Add("Tổng vé", 70);
                lvThongKe.Columns.Add("Đã bán", 70);
                lvThongKe.Columns.Add("Còn lại", 70);
                lvThongKe.Columns.Add("Tỉ lệ bán", 80);
                lvThongKe.Columns.Add("Doanh thu", 100);
                lvThongKe.Columns.Add("Xếp hạng", 70);
            }

            lvThongKe.Items.Clear();


            foreach (var phim in danhSachSapXep)
            {
                ListViewItem item = new ListViewItem(phim.TenPhim);
                item.SubItems.Add(phim.PhongChieu);
                item.SubItems.Add(phim.GiaVeChuan.ToString("N0"));
                item.SubItems.Add(phim.TongSoVe.ToString());
                item.SubItems.Add(phim.SoLuongVeBan.ToString());
                item.SubItems.Add(phim.SoLuongVeTon.ToString());
                item.SubItems.Add(phim.TileBanRa.ToString("F2") + "%");
                item.SubItems.Add(phim.DoanhThu.ToString("N0"));
                item.SubItems.Add(phim.XepHang.ToString());

                lvThongKe.Items.Add(item);
            }

            foreach (ColumnHeader column in lvThongKe.Columns)
            {
                column.Width = -2;
            }

            bool isEven = true;
            foreach (ListViewItem item in lvThongKe.Items)
            {
                item.BackColor = isEven ? Color.Black : Color.FromArgb(245, 245, 245);
                isEven = !isEven;
            }

            if (progressBar1 != null) progressBar1.Value = 100;
            await Task.Delay(20);

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (danhSachSapXep == null || danhSachSapXep.Count == 0)
            {
                MessageBox.Show(this, "Chưa có dữ liệu thống kê! Vui lòng bấm Đọc file và kiểm tra bảng trước khi lưu.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files|*.txt|All Files|*.*";
            saveFileDialog.FileName = "output5.txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    writer.WriteLine("       BẢNG THỐNG KÊ DOANH THU PHÒNG VÉ   ");
                    foreach (var phim in danhSachSapXep)
                    {
                        writer.WriteLine($"Tên phim          : {phim.TenPhim}");
                        writer.WriteLine($"Phòng chiếu       : {phim.PhongChieu}");
                        writer.WriteLine($"Giá vé chuẩn      : {phim.GiaVeChuan:N0} VNĐ");
                        writer.WriteLine($"Tổng số vé        : {phim.TongSoVe}");
                        writer.WriteLine($"Số lượng vé bán ra: {phim.SoLuongVeBan}");
                        writer.WriteLine($"Số lượng vé tồn   : {phim.SoLuongVeTon}");
                        writer.WriteLine($"Tỉ lệ vé bán ra   : {phim.TileBanRa:F2}%");
                        writer.WriteLine($"Doanh thu         : {phim.DoanhThu:N0} VNĐ");
                        writer.WriteLine($"Xếp hạng doanh thu: {phim.XepHang}");
                    }
                }

                MessageBox.Show(this, "Đã ghi file thống kê 'output5.txt' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
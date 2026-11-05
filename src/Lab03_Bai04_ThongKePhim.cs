using System;
using System.Collections.Generic;
using System.Text;

namespace Lab02
{
    public class Lab03_Bai04_ThongKePhim
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
}

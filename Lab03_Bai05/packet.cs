using System;

namespace Lab03_Bai05
{
    public class Packet
    {
        public string Action { get; set; } // "ADD", "RANDOM_ALL", "RANDOM_MINE", "RESULT", "EMPTY"
        public string TenMonAn { get; set; }
        public string NguoiDongGop { get; set; }
        public string HinhAnhBase64 { get; set; }
    }
}
using System;

namespace ET.Models.HeThong
{
    // Bảng HeThong.CauHinh — Tham số cấu hình toàn hệ thống.
    // Primary Key: Khoa (varchar). Dùng ConfigKeys để truy xuất.
    public class CauHinh
    {
        public string Khoa { get; set; }
        public string GiaTri { get; set; }
        public string MoTa { get; set; }
        public DateTime? CapNhatLuc { get; set; }
        public int? CapNhatBoi { get; set; }
    }
}

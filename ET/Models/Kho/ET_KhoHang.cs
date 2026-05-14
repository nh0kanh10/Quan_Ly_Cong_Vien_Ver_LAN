using System;

namespace ET.Models.Kho
{
    public class ET_KhoHang
    {
        public int Id { get; set; }
        public string MaKho { get; set; }
        public string TenKho { get; set; }     
        // Đánh dấu là kho ảo (Kho không có thật ngoài đời, dùng để điều chuyển hoặc huỷ hàng).
        public bool LaKhoAo { get; set; }
        // Cho phép xuất hàng kể cả khi không tính được tồn kho (thường dùng cho Bếp).
        public bool ChoPhepTonAm { get; set; }
        public int? IdKhuVuc { get; set; }
        // Trạng thái hoạt động của kho: HoatDong, NgungHoatDong.
        public string TrangThai { get; set; }
    }
}

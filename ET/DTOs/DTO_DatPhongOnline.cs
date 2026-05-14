using System;

namespace ET.DTOs
{
    /// <summary>
    /// DTO cho danh sách đặt phòng online (từ Web) chờ check-in.
    /// Dùng cho chuông thông báo trong ucLuuTru_Main.
    /// </summary>
    public class DTO_DatPhongOnline
    {
        public int      IdPhieu       { get; set; }
        public string   MaDatPhong    { get; set; }
        public string   TenKhachHang  { get; set; }
        public string   SoDienThoai   { get; set; }
        public string   TenLoaiPhong  { get; set; }
        public DateTime NgayNhanPhong { get; set; }
        public DateTime NgayTraPhong  { get; set; }
        public decimal  TienCoc       { get; set; }
        public DateTime NgayDat       { get; set; }

        // Helper display
        public string ThoiGianDat  => NgayDat.ToString("dd/MM HH:mm");
        public string ThoiGianO    => $"{NgayNhanPhong:dd/MM} → {NgayTraPhong:dd/MM}";
        public string SoNgay       => $"{(NgayTraPhong - NgayNhanPhong).Days}";
    }
}

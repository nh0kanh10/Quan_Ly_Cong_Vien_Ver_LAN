using System;

namespace ET.DTOs
{
    public class DTO_TheRFID
    {
        public int Id { get; set; }
        public string MaThe { get; set; }
        public int? IdViDienTu { get; set; }
        public string TrangThai { get; set; }
        public int? IdKhachHang { get; set; }
        public string TenKhach { get; set; }
    }
}

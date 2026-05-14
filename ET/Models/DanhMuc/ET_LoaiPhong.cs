using ET.Interfaces;

namespace ET.Models.DanhMuc
{
    // Bảng DanhMuc.LoaiPhong — Standard, VIP, Suite...
    public class ET_LoaiPhong : IEntity
    {
        public int Id { get; set; }
        public string TenLoai { get; set; }
        public string MoTa { get; set; }
        public int? SoNguoiToiDa { get; set; }
        public int? SoTreEmToiDa { get; set; }
        public decimal? DienTich { get; set; }
        public string TienNghi { get; set; }
        public bool ConHoatDong { get; set; }
    }
}

using ET.Interfaces;

namespace ET.Models.DanhMuc
{
    // Bảng DanhMuc.KhuVuc — Khu vực hoạt động (hồ bơi, sở thú, nhà hàng...).
    public class ET_KhuVuc : IEntity, ISoftDelete
    {
        public int Id { get; set; }
        public string MaKhuVuc { get; set; }
        public string TenKhuVuc { get; set; }
        public string LoaiKhuVuc { get; set; }
        public int? SucChua { get; set; }
        public string TrangThai { get; set; }
        public decimal? ToaDoX { get; set; }
        public decimal? ToaDoY { get; set; }
        public string MoTa { get; set; }
        public string HinhAnh { get; set; }
        public bool DaXoa { get; set; }
    }
}

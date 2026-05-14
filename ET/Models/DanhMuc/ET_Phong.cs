using ET.Interfaces;

namespace ET.Models.DanhMuc
{
    // Bảng DanhMuc.Phong — Phòng khách sạn vật lý. .
    public class ET_Phong : IEntity
    {
        public int Id { get; set; }
        public string MaPhong { get; set; }
        public int IdLoaiPhong { get; set; }
        public int? IdKhuVuc { get; set; }
        public int? Tang { get; set; }
        public decimal? DienTich { get; set; }
        public string TrangThai { get; set; }
        public byte[] RowVersion { get; set; }
    }
}

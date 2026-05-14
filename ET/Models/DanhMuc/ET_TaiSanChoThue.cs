using ET.Interfaces;

namespace ET.Models.DanhMuc
{
    // Tài sản vật lý cho thuê (từng cái ván, xe cụ thể).
    // Chỉ dùng cho đồ lớn cần quét barcode riêng (xe điện, xe đạp...).
    // Đồ nhỏ (phao, ván) quản lý theo sản phẩm + số lượng, không cần bảng này.
    public class ET_TaiSanChoThue : IEntity, ISoftDelete
    {
        public int Id { get; set; }
        public string TenTaiSan { get; set; }
        public int? IdSanPham { get; set; }
        public string MaVachThietBi { get; set; }
        public string TrangThai { get; set; }
        public bool DaXoa { get; set; }
        public string TenSanPham { get; set; }
    }
}

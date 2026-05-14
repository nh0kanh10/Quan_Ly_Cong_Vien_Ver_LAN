namespace ET.Models.DanhMuc
{
    public class ET_QuyDoiDonVi
    {
        public int Id { get; set; }
        public int IdSanPham { get; set; }
        public int IdDonViGoc { get; set; }
        public int IdDonViDich { get; set; }
        public decimal TyLeQuyDoi { get; set; }
        public decimal? GiaBan { get; set; }  // NULL = tự tính đơn giá × hệ số
    }
}

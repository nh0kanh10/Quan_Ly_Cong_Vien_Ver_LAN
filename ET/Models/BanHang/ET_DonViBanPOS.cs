namespace ET.Models.BanHang
{
    /// <summary>
    /// DTO hiển thị dropdown chọn đơn vị bán tại POS.
    /// VD: Bia Tiger -> Lon (25k), Lốc 6 lon (140k), Thùng 24 lon (500k).
    /// </summary>
    public class ET_DonViBanPOS
    {
        public int IdDonViDich { get; set; }
        public string TenDonVi { get; set; }
        public decimal TyLeQuyDoi { get; set; }  // Dùng để trừ kho: bán 1 thùng -> trừ 24 đơn vị gốc
        public decimal GiaBan { get; set; }        // Giá riêng hoặc tự tính (đơn giá × hệ số)
    }
}

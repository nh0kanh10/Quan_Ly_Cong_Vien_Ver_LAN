namespace ET
{
    /// <summary>
    /// View model hiển thị chi tiết combo trên DataGridView
    /// </summary>
    public class ComboDetailView
    {
        public int MaChiTietCombo { get; set; }
        public int MaLoaiVeCha { get; set; }
        public int MaLoaiVeCon { get; set; }
        public string MaCodeVeCon { get; set; }
        public string TenVeCon { get; set; }
        public decimal GiaVe { get; set; }
        public int SoLuotChoPhep { get; set; }
    }
}

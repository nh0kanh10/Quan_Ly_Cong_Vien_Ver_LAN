namespace ET.Models.DoiTac
{
    // Bảng DoiTac.NhaCungCap — Bảng con kế thừa ThongTin.
    // PK = IdDoiTac. Dùng trong module Kho (nhập hàng, trả hàng NCC).
    public class NhaCungCap
    {
        public int IdDoiTac { get; set; }
        public string MaNhaCungCap { get; set; }
        public string MaSoThue { get; set; }
        public string NguoiLienHe { get; set; }
        public string DieuKhoanThanhToan { get; set; }
    }
}

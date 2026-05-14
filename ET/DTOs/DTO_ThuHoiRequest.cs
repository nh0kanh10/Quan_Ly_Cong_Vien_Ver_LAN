namespace ET.DTOs
{
    // Dữ liệu nhân viên nhập trên grid nhận trả (ucNhanTra).
    // Mỗi dòng = 1 sản phẩm, NV điền SL trả + SL báo mất.
    public class DTO_ThuHoiRequest
    {
        public int IdSanPham { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuongDangThue { get; set; }

        // NV nhập trên grid
        public int SoLuongTra { get; set; }
        public int SoLuongMat { get; set; }

        // NV nhập trên popup frmPhatMatDo
        public decimal TienPhat { get; set; }
    }
}

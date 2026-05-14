namespace ET.Models.BanHang
{
    // Dòng thanh toán — hỗ trợ chia nhỏ (Split Payment).
    // Ví dụ bill 500k: 1 dòng tiền mặt 200k + 1 dòng RFID 300k.
    public class ET_PaymentLine
    {
        public string PhuongThuc { get; set; }  // TienMat, ChuyenKhoan, ViRFID, QR
        public decimal SoTien { get; set; }
        public string GhiChu { get; set; }
        public string MaTheRFID { get; set; }
        public int? IdViDienTu { get; set; }
        public int? DiemQuyDoi { get; set; }    // Lưu số điểm tích lũy dùng cho dòng thanh toán này
    }
}

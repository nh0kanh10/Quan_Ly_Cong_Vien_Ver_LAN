namespace DaiNamWeb.Client.Models;

// ===== AUTH =====
public record RegisterRequest(string SoDienThoai, string MatKhau, string HoTen);
public record LoginRequest(string SoDienThoai, string MatKhau);
public record LoginResponse(int Id, string HoTen, string Role);

// ===== PROFILE =====
public class CustomerProfile
{
    public string HoTen { get; set; } = "";
    public string SoDienThoai { get; set; } = "";
    public string? Email { get; set; }
    public string? DiaChi { get; set; }
    public DateTime? NgaySinh { get; set; }
    public string? GioiTinh { get; set; }
    public int DiemTichLuy { get; set; }
    public decimal TongChiTieu { get; set; }
    public string LoaiKhach { get; set; } = "";
}

public record UpdateProfileRequest(int IdTaiKhoanWeb, CustomerProfile Profile);

public class TicketProduct
{
    public int Id { get; set; }
    public string TenSanPham { get; set; } = "";
    public string? MoTa { get; set; }
    public decimal DonGia { get; set; }
    public int? IdKhuVuc { get; set; }
    public string? TenKhuVuc { get; set; }
    public int SoLuotQuyDoi { get; set; }
}

public class TicketPrice
{
    public int IdSanPham { get; set; }
    public decimal GiaApDung { get; set; }
    public string LoaiGia { get; set; } = "Thuong";
}

// ===== PURCHASE =====
public record CartItem(int IdSanPham, int SoLuong);
public record PurchaseRequest(List<CartItem> Items, int IdTaiKhoanWeb, int DiemSuDung = 0, string PaymentMethod = "ChuyenKhoan");

public record CalculateOrderRequest(List<CartItem> Items, int IdTaiKhoanWeb, bool UsePoints = false);

public class CalculateOrderResult
{
    public decimal TongTienGoc { get; set; }
    public decimal TienGiam { get; set; }
    public string NguonGiam { get; set; } = "";
    public decimal ThucThu { get; set; }
    public int DiemKhaDung { get; set; }
    public int DiemSuDung { get; set; }
    public int DiemThuong { get; set; }
}

// ===== HISTORY =====
public class PointHistoryItem
{
    public DateTime ThoiGian { get; set; }
    public string LoaiGiaoDich { get; set; } = "";
    public int SoDiem { get; set; }
    public int TruocGD { get; set; }
    public int SauGD { get; set; }
    public string LyDo { get; set; } = "";
    public string NguoiXuLy { get; set; } = "Hệ thống";
}

public class WalletTransactionItem
{
    public DateTime ThoiGian { get; set; }
    public string LoaiGiaoDich { get; set; } = "";
    public decimal SoTien { get; set; }
    public string MaGiaoDich { get; set; } = "";
    public string LoaiGDHienThi => LoaiGiaoDich switch {
        "NapTienTienMat" or "NapTienNganHang" => "Nạp tiền vào ví",
        "ThanhToanDichVu" => "Thanh toán giao dịch",
        "ChuyenDiemThanhTien" => "Đổi điểm ra tiền",
        "ThuHoiSoDu" => "Thu hồi số dư",
        _ => LoaiGiaoDich
    };
}

public class PurchaseResult
{
    public int IdDonHang { get; set; }
    public string MaDonHang { get; set; } = "";
    public decimal TongTien { get; set; }
    public List<TicketInfo> Tickets { get; set; } = new();
}

public class TicketInfo
{
    public Guid Id { get; set; }
    public string MaCode { get; set; } = "";
    public string TenSanPham { get; set; } = "";
    public int SoLuotConLai { get; set; }
    public string TrangThai { get; set; } = "ChuaSuDung";
}

public record ScanRequest(string MaQR, int? IdKhuVuc = null, int? IdTroChoi = null);

public class ScanResult
{
    public bool IsValid { get; set; }
    public string Message { get; set; } = "";
    public string? TenSanPham { get; set; }
    public int SoLuotConLai { get; set; }
}

// ===== GATE CONFIG =====
public class GateKhuVuc
{
    public int Id { get; set; }
    public string TenKhuVuc { get; set; } = "";
}

public class GateThietBi
{
    public int Id { get; set; }
    public string TenThietBi { get; set; } = "";
    public int IdKhuVuc { get; set; }
}

public class TransactionHistoryItem
{
    public DateTime ThoiGian { get; set; }
    public string LoaiGiaoDich { get; set; } = "";
    public decimal SoTien { get; set; }
    public string MaCode { get; set; } = "";
    public string TrangThai { get; set; } = "";
    public string NguonBan { get; set; } = "";
}

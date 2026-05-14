namespace DaiNamWeb.Api.Models;

// Đăng ký & đăng nhập
public record RegisterRequest(string SoDienThoai, string MatKhau, string HoTen);
public record LoginRequest(string SoDienThoai, string MatKhau);
public record LoginResponse(int IdTaiKhoan, int IdDoiTac, string HoTen, string Role, string Token);


// Thông tin cá nhân khách hàng
public class CustomerProfile
{
    public string HoTen { get; set; } = "";
    public string SoDienThoai { get; set; } = "";
    public string? Email { get; set; }
    public string? DiaChi { get; set; }
    public string? LoaiKhach { get; set; }
    public string? HangThanhVien { get; set; }
}

// Sản phẩm vé hiển thị trên web
public class TicketProduct
{
    public int Id { get; set; }
    public string TenSanPham { get; set; } = "";
    public string? MoTa { get; set; }
    public decimal DonGia { get; set; }
    public int? IdKhuVuc { get; set; }
    public string? TenKhuVuc { get; set; }
    public string? LoaiSanPham { get; set; }
}

// Giá vé theo ngày (thường / cuối tuần / lễ)
public class TicketPrice
{
    public int IdSanPham { get; set; }
    public decimal GiaApDung { get; set; }
    public string LoaiGia { get; set; } = "MacDinh";
}

// Giỏ hàng & thanh toán vé
public record CartItem(int IdSanPham, int SoLuong);
public record PurchaseRequest(List<CartItem> Items, int IdTaiKhoan, string PaymentMethod = "ChuyenKhoan");

public class PurchaseResult
{
    public int IdDonHang { get; set; }
    public string MaDonHang { get; set; } = "";
    public decimal TongTien { get; set; }
    public List<TicketInfo> Tickets { get; set; } = new();
}

// Thông tin 1 vé điện tử
public class TicketInfo
{
    public int Id { get; set; }
    public string MaVach { get; set; } = "";
    public string TenSanPham { get; set; } = "";
    public int SoLuotConLai { get; set; }
    public string TrangThai { get; set; } = "ChuaSuDung";
}

// Loại phòng khách sạn (kèm số phòng trống + giá)
public class RoomTypeInfo
{
    public int IdLoaiPhong { get; set; }
    public string TenLoai { get; set; } = "";
    public string? MoTa { get; set; }
    public int? SoNguoiToiDa { get; set; }
    public decimal? DienTich { get; set; }
    public string? TienNghi { get; set; }
    public decimal GiaMoiDem { get; set; }
    public int SoPhongTrong { get; set; }
}

// Yêu cầu đặt phòng
public record BookingRequest(
    int IdTaiKhoan,
    int IdLoaiPhong,
    DateTime NgayNhanPhong,
    DateTime NgayTraPhong,
    string? GhiChu
);

// Kết quả sau khi đặt phòng thành công
public class BookingResult
{
    public int IdPhieuDatPhong { get; set; }
    public string MaDatPhong { get; set; } = "";
    public decimal TienPhong { get; set; }
    public decimal TienCoc { get; set; }
}

// Huỷ đặt phòng
public record CancelBookingRequest(int IdPhieuDatPhong, int IdTaiKhoan, string? LyDo);
public class CancelBookingResult
{
    public bool ThanhCong { get; set; }
    public decimal TienHoanCoc { get; set; }   // 0 nếu không hoàn
    public string ThongBao { get; set; } = "";
}

// Mỗi dòng đặt phòng trong "Đặt phòng của tôi"
public class MyBookingItem
{
    public int IdPhieu { get; set; }
    public string MaDatPhong { get; set; } = "";
    public string TenLoaiPhong { get; set; } = "";
    public string? MaPhong { get; set; }
    public DateTime NgayNhanPhong { get; set; }
    public DateTime NgayTraPhong { get; set; }
    public decimal TienPhong { get; set; }
    public decimal TienCoc { get; set; }
    public string TrangThai { get; set; } = "";
    public int SoDem => (NgayTraPhong - NgayNhanPhong).Days;
}

// POST body cho soát vé
public record ScanRequest(string MaVach, int? IdKhuVuc, int? IdTroChoi);

public class ScanResult
{
    public bool HopLe { get; set; }
    public string? LyDo { get; set; }
    public string? TenVe { get; set; }
    public string? KhuVuc { get; set; }
    public int SoLuotConLai { get; set; }
    public string? MaVach { get; set; }
    public DateTime? ThoiGianQuet { get; set; }
    public int MaKetQua { get; set; } // 0=HopLe, 1=SaiKhuVuc, 2=HetLuot, 3=KhongTimThay, 4=SaiTroChoi, 5=HetHan, 6=DaHuy
}

public class AreaItem
{
    public int Id { get; set; }
    public string MaKhuVuc { get; set; } = "";
    public string TenKhuVuc { get; set; } = "";
}

public class GameItem
{
    public int Id { get; set; }
    public string TenTroChoi { get; set; } = "";
    public int? IdKhuVuc { get; set; }
}

public class ScanHistoryItem
{
    public int Id { get; set; }
    public string MaVach { get; set; } = "";
    public string TenSanPham { get; set; } = "";
    public string KetQua { get; set; } = "";
    public string? TenKhuVuc { get; set; }
    public int SoLuotConLai { get; set; }
    public DateTime ThoiGianQuet { get; set; }
    public bool HopLe => KetQua == "ThanhCong";
}

// 1 dòng lệnh bếp trên màn hình KDS
public class KitchenOrderItem
{
    public int IdLenhBep { get; set; }
    public string TenSanPham { get; set; } = "";
    public int SoLuong { get; set; }
    public string TrangThai { get; set; } = "";
    public DateTime ThoiGianGui { get; set; }
    public DateTime? ThoiGianBatDauNau { get; set; }
    public string? GhiChuBep { get; set; }
    public string? GhiChuMon { get; set; }
    public string MaDonHang { get; set; } = "";
    public string? NguonBan { get; set; }
    public int PhutCho { get; set; }
}

// Yêu cầu cập nhật trạng thái lệnh bếp
public record UpdateKitchenStatusRequest(int IdLenhBep, string TrangThai);

// Thống kê nhanh cho KDS
public class KitchenStats
{
    public int SoMonCho { get; set; }
    public int SoMonDangNau { get; set; }
    public int SoMonXongHomNay { get; set; }
}

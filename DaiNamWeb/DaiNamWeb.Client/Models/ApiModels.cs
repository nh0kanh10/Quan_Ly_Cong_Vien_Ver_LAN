namespace DaiNamWeb.Client.Models;

// Đăng ký & đăng nhập
public record RegisterRequest(string SoDienThoai, string MatKhau, string HoTen);
public record LoginRequest(string SoDienThoai, string MatKhau);
public record LoginResponse(int IdTaiKhoan, int IdDoiTac, string HoTen, string Role);

// Thông tin cá nhân
public class CustomerProfile
{
    public string HoTen { get; set; } = "";
    public string SoDienThoai { get; set; } = "";
    public string? Email { get; set; }
    public string? DiaChi { get; set; }
    public string? LoaiKhach { get; set; }
    public string? HangThanhVien { get; set; }
}

// Vé
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

public class TicketPrice
{
    public int IdSanPham { get; set; }
    public decimal GiaApDung { get; set; }
    public string LoaiGia { get; set; } = "MacDinh";
}

public record CartItem(int IdSanPham, int SoLuong);
public record PurchaseRequest(List<CartItem> Items, int IdTaiKhoan, string PaymentMethod = "ChuyenKhoan");

public class PurchaseResult
{
    public int IdDonHang { get; set; }
    public string MaDonHang { get; set; } = "";
    public decimal TongTien { get; set; }
    public List<TicketInfo> Tickets { get; set; } = new();
}

public class TicketInfo
{
    public int Id { get; set; }
    public string MaVach { get; set; } = "";
    public string TenSanPham { get; set; } = "";
    public int SoLuotConLai { get; set; }
    public string TrangThai { get; set; } = "ChuaSuDung";
}

// Khách sạn
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

public record BookingRequest(int IdTaiKhoan, int IdLoaiPhong, DateTime NgayNhanPhong, DateTime NgayTraPhong, string? GhiChu);

public class BookingResult
{
    public int IdPhieuDatPhong { get; set; }
    public string MaDatPhong { get; set; } = "";
    public decimal TienPhong { get; set; }
    public decimal TienCoc { get; set; }
}

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

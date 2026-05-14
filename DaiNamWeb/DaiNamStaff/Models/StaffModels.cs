namespace DaiNamStaff.Models;

// Đăng nhập nhân viên
public record LoginRequest(string SoDienThoai, string MatKhau);
public record LoginResponse(int IdTaiKhoan, int IdDoiTac, string HoTen, string Role);

// Kết quả quét vé
public class ScanResult
{
    public bool HopLe { get; set; }
    public string? LyDo { get; set; }
    public string? TenVe { get; set; }
    public string? KhuVuc { get; set; }
    public int SoLuotConLai { get; set; }
    public string? MaVach { get; set; }
    public DateTime? ThoiGianQuet { get; set; }
    public int MaKetQua { get; set; }
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

// 1 dòng lệnh bếp
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

// Cập nhật trạng thái
public record UpdateStatusRequest(int IdLenhBep, string TrangThai);

// Thống kê bếp
public class KitchenStats
{
    public int SoMonCho { get; set; }
    public int SoMonDangNau { get; set; }
    public int SoMonXongHomNay { get; set; }
}

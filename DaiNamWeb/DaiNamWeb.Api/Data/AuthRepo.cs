using Dapper;
using DaiNamWeb.Api.Models;
using System.Security.Cryptography;
using System.Text;

namespace DaiNamWeb.Api.Data;

public class AuthRepo
{
    private readonly DbContext _db;
    public AuthRepo(DbContext db) => _db = db;

    // Hash SHA-256 một chiều — không reversible, đủ dùng cho prototype
    // Production nên dùng BCrypt.Net-Next
    private static string HashPassword(string raw)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(raw));
        return Convert.ToHexString(bytes).ToLower();
    }

    // Tạo JWT đơn giản dạng Base64 payload (không cần thư viện, đủ cho demo)
    // Production: dùng System.IdentityModel.Tokens.Jwt
    private static string GenerateToken(int idTaiKhoan, string role)
    {
        var payload = $"{idTaiKhoan}:{role}:{DateTime.UtcNow.AddDays(7):O}";
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(payload));
    }

    #region Đăng ký & đăng nhập

    public async Task<LoginResponse?> Register(RegisterRequest req)
    {
        using var conn = _db.CreateConnection();
        conn.Open();
        using var tx = conn.BeginTransaction();

        try
        {
            // Kiểm tra SĐT đã đăng ký chưa
            var exists = await conn.ExecuteScalarAsync<int>(
                @"SELECT COUNT(1) FROM DoiTac.TaiKhoan 
                  WHERE TenDangNhap = @SoDienThoai AND LoaiTaiKhoan = 'Web' AND ConHoatDong = 1",
                new { req.SoDienThoai }, tx);

            if (exists > 0) return null;

            // Tìm đối tác đã tồn tại theo SĐT, hoặc tạo mới
            var idDoiTac = await conn.ExecuteScalarAsync<int?>(
                "SELECT Id FROM DoiTac.ThongTin WHERE DienThoai = @SoDienThoai AND DaXoa = 0",
                new { req.SoDienThoai }, tx);

            if (idDoiTac == null || idDoiTac == 0)
            {
                idDoiTac = await conn.ExecuteScalarAsync<int>(@"
                    INSERT INTO DoiTac.ThongTin (HoTen, DienThoai, LoaiDoiTac, DaXoa, NgayTao)
                    VALUES (@HoTen, @SoDienThoai, 'CaNhan', 0, GETDATE());
                    SELECT CAST(SCOPE_IDENTITY() AS INT);",
                    new { req.HoTen, req.SoDienThoai }, tx);
            }

            // Tạo bản ghi KhachHang nếu chưa có
            var hasKh = await conn.ExecuteScalarAsync<int>(
                "SELECT COUNT(1) FROM DoiTac.KhachHang WHERE IdDoiTac = @idDoiTac",
                new { idDoiTac }, tx);

            if (hasKh == 0)
            {
                var maKh = $"KWEB{DateTime.Now:MMddHHmmss}";
                await conn.ExecuteAsync(@"
                    INSERT INTO DoiTac.KhachHang (IdDoiTac, MaKhachHang, LoaiKhach, HangThanhVien)
                    VALUES (@idDoiTac, @maKh, 'CaNhan', 'Thuong')",
                    new { idDoiTac, maKh }, tx);
            }

            var hashedPw = HashPassword(req.MatKhau);
            var idTk = await conn.ExecuteScalarAsync<int>(@"
                INSERT INTO DoiTac.TaiKhoan (IdDoiTac, TenDangNhap, MatKhauHash, LoaiTaiKhoan, LoaiDangNhap, ConHoatDong, NgayTao)
                VALUES (@idDoiTac, @SoDienThoai, @hashedPw, 'Web', 'SoDienThoai', 1, GETDATE());
                SELECT CAST(SCOPE_IDENTITY() AS INT);",
                new { idDoiTac, req.SoDienThoai, hashedPw }, tx);

            tx.Commit();
            return new LoginResponse(idTk, idDoiTac.Value, req.HoTen, "KhachHang",
                GenerateToken(idTk, "KhachHang"));
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }

    public async Task<LoginResponse?> Login(LoginRequest req)
    {
        using var conn = _db.CreateConnection();
        var hashedPw = HashPassword(req.MatKhau);
        var result = await conn.QueryFirstOrDefaultAsync<(int Id, int IdDoiTac, string HoTen)>(@"
            SELECT tk.Id, tk.IdDoiTac, tt.HoTen
            FROM DoiTac.TaiKhoan tk
            JOIN DoiTac.ThongTin tt ON tk.IdDoiTac = tt.Id
            WHERE tk.TenDangNhap = @SoDienThoai 
              AND tk.MatKhauHash = @hashedPw 
              AND tk.LoaiTaiKhoan = 'Web'
              AND tk.ConHoatDong = 1",
            new { req.SoDienThoai, hashedPw });

        if (result.Id == 0) return null;

        await conn.ExecuteAsync(
            "UPDATE DoiTac.TaiKhoan SET LanDangNhapCuoi = GETDATE() WHERE Id = @Id",
            new { result.Id });

        return new LoginResponse(result.Id, result.IdDoiTac, result.HoTen, "KhachHang",
            GenerateToken(result.Id, "KhachHang"));
    }

    #endregion

    #region Thông tin cá nhân

    public async Task<CustomerProfile?> GetProfile(int idTaiKhoan)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<CustomerProfile>(@"
            SELECT tt.HoTen, tt.DienThoai AS SoDienThoai, tt.Email, tt.DiaChi,
                   kh.LoaiKhach, kh.HangThanhVien
            FROM DoiTac.TaiKhoan tk
            JOIN DoiTac.ThongTin tt ON tk.IdDoiTac = tt.Id
            LEFT JOIN DoiTac.KhachHang kh ON kh.IdDoiTac = tt.Id
            WHERE tk.Id = @idTaiKhoan AND tk.ConHoatDong = 1",
            new { idTaiKhoan });
    }

    #endregion
}

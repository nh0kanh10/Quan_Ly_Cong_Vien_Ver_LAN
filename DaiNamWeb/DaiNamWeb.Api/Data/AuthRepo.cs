using Dapper;
using DaiNamWeb.Api.Models;

namespace DaiNamWeb.Api.Data;

public class AuthRepo
{
    private readonly DbContext _db;
    public AuthRepo(DbContext db) => _db = db;

    public async Task<LoginResponse?> Register(RegisterRequest req)
    {
        using var conn = _db.CreateConnection();
        conn.Open();
        using var tx = conn.BeginTransaction();

        try
        {
            var exists = await conn.ExecuteScalarAsync<int>(
                "SELECT COUNT(1) FROM TaiKhoanWeb WHERE SoDienThoai = @SoDienThoai AND IsDeleted = 0",
                new { req.SoDienThoai }, tx);

            if (exists > 0) return null;

            // 1. Tìm hoặc Tạo Khách Hàng
            var idKhachHang = await conn.ExecuteScalarAsync<int?>(
                "SELECT Id FROM KhachHang WHERE DienThoai = @SoDienThoai AND IsDeleted = 0",
                new { req.SoDienThoai }, tx);

            if (idKhachHang == null || idKhachHang == 0)
            {
                idKhachHang = await conn.ExecuteScalarAsync<int>(@"
                    INSERT INTO KhachHang (MaCode, HoTen, DienThoai, LoaiKhach, NgayDangKy)
                    VALUES (@maCode, @HoTen, @SoDienThoai, 'CaNhan', GETDATE());
                    SELECT CAST(SCOPE_IDENTITY() AS INT);",
                    new { 
                        maCode = "KHWEB" + DateTime.Now.ToString("MMddHHmmss"), 
                        req.HoTen, 
                        req.SoDienThoai 
                    }, tx);
            }

            // 2. Tạo Tài khoản Web nối với Khách Hàng
            var id = await conn.ExecuteScalarAsync<int>(@"
                INSERT INTO TaiKhoanWeb (SoDienThoai, MatKhau, HoTen, IdKhachHang)
                VALUES (@SoDienThoai, @MatKhau, @HoTen, @idKhachHang);
                SELECT CAST(SCOPE_IDENTITY() AS INT);",
                new { req.SoDienThoai, req.MatKhau, req.HoTen, idKhachHang }, tx);

            tx.Commit();
            return new LoginResponse(id, req.HoTen, AppConstants.Roles.Customer);
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }

    public async Task<LoginResponse?> LoginCustomer(LoginRequest req)
    {
        using var conn = _db.CreateConnection();
        var tk = await conn.QueryFirstOrDefaultAsync<(int Id, string HoTen)>(@"
            SELECT Id, HoTen FROM TaiKhoanWeb
            WHERE SoDienThoai = @SoDienThoai AND MatKhau = @MatKhau AND IsDeleted = 0",
            new { req.SoDienThoai, req.MatKhau });

        if (tk.Id == 0) return null;
        return new LoginResponse(tk.Id, tk.HoTen, AppConstants.Roles.Customer);
    }

    public async Task<LoginResponse?> LoginStaff(LoginRequest req)
    {
        using var conn = _db.CreateConnection();
        var nv = await conn.QueryFirstOrDefaultAsync<(int Id, string HoTen, string TenVaiTro)>(@"
            SELECT nv.Id, nv.HoTen, vt.TenVaiTro
            FROM NhanVien nv
            JOIN VaiTro vt ON nv.IdVaiTro = vt.Id
            WHERE nv.TenDangNhap = @SoDienThoai AND nv.MatKhau = @MatKhau AND nv.IsDeleted = 0",
            new { req.SoDienThoai, req.MatKhau });

        if (nv.Id == 0) return null;
        return new LoginResponse(nv.Id, nv.HoTen, nv.TenVaiTro);
    }
    public async Task<CustomerProfile?> GetProfile(int idTaiKhoanWeb)
    {
        using var conn = _db.CreateConnection();
        var tk = await conn.QueryFirstOrDefaultAsync<dynamic>("SELECT HoTen, SoDienThoai, IdKhachHang FROM TaiKhoanWeb WHERE Id = @idTaiKhoanWeb", new { idTaiKhoanWeb });
        if (tk == null) return null;

        if (tk.IdKhachHang != null)
        {
            return await conn.QueryFirstOrDefaultAsync<CustomerProfile>(
                "SELECT HoTen, DienThoai as SoDienThoai, Email, DiaChi, NgaySinh, GioiTinh, DiemTichLuy, LoaiKhach, TongChiTieu FROM KhachHang WHERE Id = @idKh", new { idKh = tk.IdKhachHang });
        }
        
        // Khách vãng lai / Tài khoản web chưa liên kết
        return new CustomerProfile 
        {
            HoTen = tk.HoTen,
            SoDienThoai = tk.SoDienThoai,
            LoaiKhach = AppConstants.LoaiKhachHang.DangKyNhanh,
            DiemTichLuy = 0
        };
    }

    public async Task<bool> UpdateProfile(UpdateProfileRequest req)
    {
        using var conn = _db.CreateConnection();
        var idKh = await conn.ExecuteScalarAsync<int?>("SELECT IdKhachHang FROM TaiKhoanWeb WHERE Id = @IdTaiKhoanWeb", new { req.IdTaiKhoanWeb });
        if (idKh == null) return false;
        var res = await conn.ExecuteAsync(
            "UPDATE KhachHang SET HoTen = @HoTen, Email = @Email, DiaChi = @DiaChi, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, UpdatedAt = GETDATE() WHERE Id = @idKh",
            new { req.Profile.HoTen, req.Profile.Email, req.Profile.DiaChi, req.Profile.NgaySinh, req.Profile.GioiTinh, idKh });
        return res > 0;
    }
}

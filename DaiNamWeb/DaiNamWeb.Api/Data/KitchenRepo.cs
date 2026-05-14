using Dapper;
using DaiNamWeb.Api.Models;

namespace DaiNamWeb.Api.Data;

public class KitchenRepo
{
    private readonly DbContext _db;
    public KitchenRepo(DbContext db) => _db = db;

    #region Danh sách lệnh bếp

    // Lấy các lệnh chưa xong (ChoNau + DangNau), sắp FIFO theo thời gian gửi
    public async Task<IEnumerable<KitchenOrderItem>> GetPendingOrders(int? idNhaHang = null)
    {
        using var conn = _db.CreateConnection();
        var sql = @"
            SELECT lb.Id AS IdLenhBep, lb.SoLuong, lb.TrangThai, lb.ThoiGianGui, 
                   lb.ThoiGianBatDauNau, lb.GhiChuBep,
                   sp.TenSanPham, ctdh.GhiChu AS GhiChuMon,
                   dh.MaDonHang, dh.NguonBan,
                   DATEDIFF(MINUTE, lb.ThoiGianGui, GETDATE()) AS PhutCho
            FROM BanHang.LenhBep lb
            JOIN BanHang.ChiTietDonHang ctdh ON lb.IdChiTietDonHang = ctdh.Id
            JOIN DanhMuc.SanPham sp ON ctdh.IdSanPham = sp.Id
            JOIN BanHang.DonHang dh ON ctdh.IdDonHang = dh.Id
            WHERE lb.TrangThai IN ('ChoNau', 'DangNau')";

        if (idNhaHang.HasValue)
            sql += " AND lb.IdNhaHang = @idNhaHang";

        sql += " ORDER BY lb.ThoiGianGui ASC";

        return await conn.QueryAsync<KitchenOrderItem>(sql, new { idNhaHang });
    }

    #endregion

    #region Cập nhật trạng thái

    // Chuyển trạng thái: ChoNau → DangNau → DaXong
    public async Task<bool> UpdateStatus(int idLenhBep, string trangThaiMoi)
    {
        using var conn = _db.CreateConnection();

        string updateSql = trangThaiMoi switch
        {
            "DangNau" => "UPDATE BanHang.LenhBep SET TrangThai = @trangThaiMoi, ThoiGianBatDauNau = GETDATE() WHERE Id = @idLenhBep",
            "DaXong"  => "UPDATE BanHang.LenhBep SET TrangThai = @trangThaiMoi, ThoiGianXong = GETDATE() WHERE Id = @idLenhBep",
            "DaHuy"   => "UPDATE BanHang.LenhBep SET TrangThai = @trangThaiMoi, ThoiGianXong = GETDATE() WHERE Id = @idLenhBep",
            _         => ""
        };

        if (string.IsNullOrEmpty(updateSql)) return false;

        var rows = await conn.ExecuteAsync(updateSql, new { idLenhBep, trangThaiMoi });
        return rows > 0;
    }

    #endregion

    #region Thống kê nhanh

    public async Task<KitchenStats> GetStats(int? idNhaHang = null)
    {
        using var conn = _db.CreateConnection();
        var where = idNhaHang.HasValue ? "AND lb.IdNhaHang = @idNhaHang" : "";

        return await conn.QueryFirstAsync<KitchenStats>($@"
            SELECT 
                SUM(CASE WHEN lb.TrangThai = 'ChoNau' THEN 1 ELSE 0 END) AS SoMonCho,
                SUM(CASE WHEN lb.TrangThai = 'DangNau' THEN 1 ELSE 0 END) AS SoMonDangNau,
                SUM(CASE WHEN lb.TrangThai = 'DaXong' AND CAST(lb.ThoiGianXong AS DATE) = CAST(GETDATE() AS DATE) THEN 1 ELSE 0 END) AS SoMonXongHomNay
            FROM BanHang.LenhBep lb
            WHERE 1=1 {where}",
            new { idNhaHang });
    }

    #endregion
}

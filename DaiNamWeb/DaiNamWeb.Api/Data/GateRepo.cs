using Dapper;
using DaiNamWeb.Api.Models;

namespace DaiNamWeb.Api.Data;

public class GateRepo
{
    private readonly DbContext _db;
    public GateRepo(DbContext db) => _db = db;

    #region Soát vé

    public async Task<List<AreaItem>> GetAreas()
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<AreaItem>(
            "SELECT Id, MaKhuVuc, TenKhuVuc FROM DanhMuc.KhuVuc WHERE DaXoa = 0")).ToList();
    }

    public async Task<List<GameItem>> GetGames(int? idKhuVuc)
    {
        using var conn = _db.CreateConnection();
        string sql = "SELECT Id, TenTroChoi, IdKhuVuc FROM DanhMuc.TroChoi WHERE DaXoa = 0";
        if (idKhuVuc.HasValue) sql += " AND IdKhuVuc = @idKhuVuc";
        return (await conn.QueryAsync<GameItem>(sql, new { idKhuVuc })).ToList();
    }

    public async Task<List<ScanHistoryItem>> GetRecentScans(int? idKhuVuc, int limit = 20)
    {
        using var conn = _db.CreateConnection();
        string sql = @"
            SELECT TOP (@limit) 
                lq.Id, ve.MaVach, sp.TenSanPham, lq.KetQua, 
                kv.TenKhuVuc, ve.SoLuotConLai, lq.ThoiGianQuet
            FROM BanHang.ChiTietLuotQuet lq
            JOIN BanHang.VeDienTu ve ON lq.IdVeDienTu = ve.Id
            JOIN DanhMuc.SanPham sp ON ve.IdSanPham = sp.Id
            JOIN DanhMuc.Ve_QuyenTruyCap qr ON lq.IdQuyenTruyCap = qr.Id
            LEFT JOIN DanhMuc.KhuVuc kv ON qr.IdKhuVuc = kv.Id
            WHERE 1=1";
        
        if (idKhuVuc.HasValue) sql += " AND qr.IdKhuVuc = @idKhuVuc";
        sql += " ORDER BY lq.ThoiGianQuet DESC";

        return (await conn.QueryAsync<ScanHistoryItem>(sql, new { limit, idKhuVuc })).ToList();
    }

    public async Task<ScanResult> ScanTicket(string maVach, int? idKhuVuc, int? idTroChoi)
    {
        using var conn = _db.CreateConnection();
        conn.Open();

        // 1. Tìm vé
        var ve = await conn.QueryFirstOrDefaultAsync<TicketInfo>(
            @"SELECT v.Id, v.MaVach, sp.TenSanPham, v.SoLuotConLai, v.TrangThai, v.IdSanPham, v.NgayHetHan
              FROM BanHang.VeDienTu v
              JOIN DanhMuc.SanPham sp ON v.IdSanPham = sp.Id
              WHERE v.MaVach = @maVach",
            new { maVach });

        if (ve == null)
            return new ScanResult { HopLe = false, LyDo = "Mã vé không tồn tại", MaKetQua = 3 };

        if (ve.TrangThai == "DaHuy")
            return new ScanResult { HopLe = false, LyDo = "Vé đã bị hủy", MaKetQua = 6, MaVach = maVach, TenVe = ve.TenSanPham };

        if (ve.TrangThai == "HetHan" || (ve.NgayHetHan.HasValue && ve.NgayHetHan.Value < DateTime.Today))
            return new ScanResult { HopLe = false, LyDo = "Vé đã hết hạn", MaKetQua = 5, MaVach = maVach, TenVe = ve.TenSanPham };

        if (ve.SoLuotConLai <= 0)
            return new ScanResult { HopLe = false, LyDo = "Vé đã sử dụng hết lượt", MaKetQua = 2, MaVach = maVach, TenVe = ve.TenSanPham };

        // 2. Kiểm tra quyền truy cập
        var dsQuyen = (await conn.QueryAsync<dynamic>(
            "SELECT Id, IdKhuVuc, IdTroChoi FROM DanhMuc.Ve_QuyenTruyCap WHERE IdSanPhamVe = @IdSanPham",
            new { IdSanPham = ve.IdSanPham })).ToList();

        int? idQuyenDung = null;
        string ketQuaQuet = "ThanhCong";
        string lyDoThatBai = "";
        int maKetQua = 0;

        if (dsQuyen.Any())
        {
            if (idKhuVuc.HasValue)
            {
                var quyenKhuVuc = dsQuyen.Where(q => q.IdKhuVuc == idKhuVuc.Value).ToList();
                if (!quyenKhuVuc.Any())
                {
                    ketQuaQuet = "SaiKhuVuc";
                    lyDoThatBai = "Vé không hợp lệ cho khu vực này";
                    maKetQua = 1;
                    idQuyenDung = dsQuyen.First().Id;
                }
                else if (idTroChoi.HasValue)
                {
                    var quyenTroChoi = quyenKhuVuc.FirstOrDefault(q => q.IdTroChoi == null || q.IdTroChoi == idTroChoi.Value);
                    if (quyenTroChoi == null)
                    {
                        ketQuaQuet = "SaiTroChoi";
                        lyDoThatBai = "Vé không hợp lệ cho trò chơi này";
                        maKetQua = 4;
                        idQuyenDung = quyenKhuVuc.First().Id;
                    }
                    else idQuyenDung = quyenTroChoi.Id;
                }
                else idQuyenDung = quyenKhuVuc.First().Id;
            }
            else idQuyenDung = dsQuyen.First().Id;
        }

        // 3. Thực hiện log và trừ lượt (nếu thành công)
        using var tx = conn.BeginTransaction();
        try
        {
            if (ketQuaQuet == "ThanhCong")
            {
                // UPDATE có WHERE SoLuotConLai > 0 đảm bảo:
                // nếu 2 cổng quét đồng thời, chỉ 1 UPDATE thành công
                string trangThaiMoi = (ve.SoLuotConLai - 1) <= 0 ? "DaSuDung" : "ChuaSuDung";
                var rows = await conn.ExecuteAsync(
                    @"UPDATE BanHang.VeDienTu 
                      SET SoLuotConLai = SoLuotConLai - 1, 
                          TrangThai = CASE WHEN SoLuotConLai - 1 <= 0 THEN 'DaSuDung' ELSE 'ChuaSuDung' END,
                          ThoiGianQuet = GETDATE()
                      WHERE Id = @Id AND SoLuotConLai > 0",
                    new { ve.Id }, tx);

                // rows=0 nghĩa là lượt vừa bị cổng khác trừ trước (race condition)
                if (rows == 0)
                {
                    tx.Rollback();
                    return new ScanResult { HopLe = false, LyDo = "Vé đã được sử dụng đồng thời ở cổng khác", MaKetQua = 2, MaVach = maVach, TenVe = ve.TenSanPham };
                }

                ve.SoLuotConLai -= 1;
            }

            if (idQuyenDung.HasValue)
            {
                await conn.ExecuteAsync(
                    @"INSERT INTO BanHang.ChiTietLuotQuet (IdVeDienTu, IdQuyenTruyCap, ThoiGianQuet, KetQua) 
                      VALUES (@IdVeDienTu, @IdQuyenTruyCap, GETDATE(), @KetQua)",
                    new { IdVeDienTu = ve.Id, IdQuyenTruyCap = idQuyenDung.Value, KetQua = ketQuaQuet }, tx);
            }

            tx.Commit();
        }
        catch
        {
            tx.Rollback();
            return new ScanResult { HopLe = false, LyDo = "Lỗi hệ thống khi ghi log", MaKetQua = -1 };
        }

        return new ScanResult
        {
            HopLe = ketQuaQuet == "ThanhCong",
            LyDo = lyDoThatBai,
            TenVe = ve.TenSanPham,
            SoLuotConLai = ve.SoLuotConLai,
            MaVach = maVach,
            MaKetQua = maKetQua,
            ThoiGianQuet = DateTime.Now
        };
    }

    #endregion
}

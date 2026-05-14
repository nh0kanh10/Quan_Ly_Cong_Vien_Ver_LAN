using Dapper;
using DaiNamWeb.Api.Models;
using System.Data;

namespace DaiNamWeb.Api.Data;

public class TicketRepo
{
    private readonly DbContext _db;
    public TicketRepo(DbContext db) => _db = db;

    #region Truy vấn sản phẩm vé & giá

    public async Task<IEnumerable<TicketProduct>> GetProducts()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TicketProduct>(@"
            SELECT sp.Id, sp.TenSanPham, sp.DonGia, sp.LoaiSanPham,
                   NULL AS IdKhuVuc, NULL AS TenKhuVuc, NULL AS MoTa
            FROM DanhMuc.SanPham sp
            WHERE sp.LoaiSanPham IN ('VeVaoKhu','VeTroChoi')
              AND sp.TrangThai = 'DangBan'
              AND sp.DaXoa = 0
            ORDER BY sp.DonGia");
    }

    // Lấy giá bán áp dụng: ưu tiên BangGia đang hiệu lực, fallback về DonGia gốc
    public async Task<TicketPrice> GetPrice(int idSanPham, DateTime date)
    {
        using var conn = _db.CreateConnection();
        return await GetPriceInternal(conn, null, idSanPham, date);
    }

    #endregion

    #region Mua vé

    // Tạo đơn hàng mua vé online, sinh vé điện tử, ghi phiếu thu giả lập
    public async Task<PurchaseResult> Purchase(PurchaseRequest req)
    {
        using var conn = _db.CreateConnection();
        conn.Open();
        using var tx = conn.BeginTransaction();

        try
        {
            // Lấy IdDoiTac từ TaiKhoan → tìm IdKhachHang
            var idDoiTac = await conn.ExecuteScalarAsync<int>(
                "SELECT IdDoiTac FROM DoiTac.TaiKhoan WHERE Id = @Id", new { Id = req.IdTaiKhoan }, tx);

            var maDH = $"WEB{DateTime.Now:yyMMddHHmmss}";
            decimal tongTien = 0;

            // Tính giá mỗi mặt hàng 1 lần duy nhất, lưu vào dict tránh lần 2 bị lệch giá
            var priceCache = new Dictionary<int, TicketPrice>();
            decimal tongTien = 0;
            foreach (var item in req.Items)
            {
                var price = await GetPriceInternal(conn, tx, item.IdSanPham, DateTime.Now);
                priceCache[item.IdSanPham] = price;
                tongTien += price.GiaApDung * item.SoLuong;
            }

            // Tạo đơn hàng (IdNhanVien = 1 vì bán online, không có nhân viên cụ thể)
            var idDonHang = await conn.ExecuteScalarAsync<int>(@"
                INSERT INTO BanHang.DonHang (MaDonHang, IdKhachHang, IdNhanVien, NguonBan, 
                    TongTienHang, TongGiamGia, TienThueVAT, TienPhiDichVu, TongThanhToan, TrangThai, NgayTao)
                VALUES (@maDH, @idDoiTac, 1, 'Online', 
                    @tongTien, 0, 0, 0, @tongTien, 'DaThanhToan', GETDATE());
                SELECT CAST(SCOPE_IDENTITY() AS INT);",
                new { maDH, idDoiTac, tongTien }, tx);

            var tickets = new List<TicketInfo>();
            var rng = new Random();

            // Tạo chi tiết đơn hàng + vé điện tử
            foreach (var item in req.Items)
            {
                var price = priceCache[item.IdSanPham];

                var idCtdh = await conn.ExecuteScalarAsync<int>(@"
                    INSERT INTO BanHang.ChiTietDonHang (IdDonHang, IdSanPham, SoLuong, DonGiaThucTe, TienThue)
                    VALUES (@idDonHang, @IdSanPham, @SoLuong, @gia, 0);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);",
                    new { idDonHang, item.IdSanPham, item.SoLuong, gia = price.GiaApDung }, tx);

                var tenSp = await conn.ExecuteScalarAsync<string>(
                    "SELECT TenSanPham FROM DanhMuc.SanPham WHERE Id = @Id", new { Id = item.IdSanPham }, tx) ?? "";

                // Mỗi đơn vị vé = 1 bản ghi VeDienTu
                for (int i = 0; i < item.SoLuong; i++)
                {
                    var maVach = GenerateTicketCode(rng);
                    await conn.ExecuteAsync(@"
                        INSERT INTO BanHang.VeDienTu (IdChiTietDonHang, IdSanPham, IdKhachHang, MaVach, SoLuotConLai, TrangThai, NgayTao)
                        VALUES (@idCtdh, @IdSanPham, @idDoiTac, @maVach, 1, 'ChuaSuDung', GETDATE())",
                        new { idCtdh, item.IdSanPham, idDoiTac, maVach }, tx);

                    tickets.Add(new TicketInfo
                    {
                        MaVach = maVach,
                        TenSanPham = tenSp,
                        SoLuotConLai = 1,
                        TrangThai = "ChuaSuDung"
                    });
                }
            }

            tx.Commit();
            return new PurchaseResult
            {
                IdDonHang = idDonHang,
                MaDonHang = maDH,
                TongTien = tongTien,
                Tickets = tickets
            };
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }

    #endregion

    #region Vé của tôi

    public async Task<IEnumerable<TicketInfo>> GetMyTickets(int idTaiKhoan)
    {
        using var conn = _db.CreateConnection();
        var idDoiTac = await conn.ExecuteScalarAsync<int>(
            "SELECT IdDoiTac FROM DoiTac.TaiKhoan WHERE Id = @idTaiKhoan", new { idTaiKhoan });

        return await conn.QueryAsync<TicketInfo>(@"
            SELECT v.Id, v.MaVach, sp.TenSanPham, v.SoLuotConLai, v.TrangThai
            FROM BanHang.VeDienTu v
            JOIN DanhMuc.SanPham sp ON v.IdSanPham = sp.Id
            WHERE v.IdKhachHang = @idDoiTac
            ORDER BY v.Id DESC",
            new { idDoiTac });
    }

    #endregion

    #region Hàm nội bộ

    // Lấy giá bán: ưu tiên BangGia hiệu lực → fallback DonGia gốc
    private async Task<TicketPrice> GetPriceInternal(IDbConnection conn, IDbTransaction? tx, int idSanPham, DateTime date)
    {
        var bangGia = await conn.QueryFirstOrDefaultAsync<(decimal GiaBan, string LoaiGia)>(@"
            SELECT GiaBan, LoaiGia
            FROM DanhMuc.BangGia
            WHERE IdSanPham = @idSanPham 
              AND TrangThai = 'HoatDong'
              AND (HieuLucTu IS NULL OR HieuLucTu <= @date)
              AND (HieuLucDen IS NULL OR HieuLucDen >= @date)
            ORDER BY CASE LoaiGia 
                WHEN 'NgayLe' THEN 1 
                WHEN 'CuoiTuan' THEN 2 
                ELSE 3 END",
            new { idSanPham, date }, tx);

        if (bangGia != default && bangGia.GiaBan > 0)
        {
            // Cuối tuần chỉ áp dụng nếu thật sự là thứ 7 / CN
            if (bangGia.LoaiGia == "CuoiTuan" && date.DayOfWeek is not (DayOfWeek.Saturday or DayOfWeek.Sunday))
            {
                // Bỏ qua giá cuối tuần nếu ngày thường → lấy giá mặc định
            }
            else
            {
                return new TicketPrice { IdSanPham = idSanPham, GiaApDung = bangGia.GiaBan, LoaiGia = bangGia.LoaiGia };
            }
        }

        // Fallback: lấy DonGia gốc từ bảng SanPham
        var donGia = await conn.ExecuteScalarAsync<decimal>(
            "SELECT DonGia FROM DanhMuc.SanPham WHERE Id = @idSanPham", new { idSanPham }, tx);
        return new TicketPrice { IdSanPham = idSanPham, GiaApDung = donGia, LoaiGia = "MacDinh" };
    }

    private static string GenerateTicketCode(Random rng)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Range(0, 12).Select(_ => chars[rng.Next(chars.Length)]).ToArray());
    }

    #endregion
}

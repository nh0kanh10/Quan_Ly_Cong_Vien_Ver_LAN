using System;
using System.Collections.Generic;
using System.Linq;
using ET.Models.Kho;

namespace DAL.Repositories.Kho
{
    /// <summary>
    /// Index IX_CTCT_TonKhoNhap và IX_CTCT_TonKhoXuat đảm bảo performance.
    /// </summary>
    public class DAL_SoCai
    {
        public static DAL_SoCai Instance { get; } = new DAL_SoCai();

        #region Truy vấn dữ liệu

        /// <summary>
        /// Lịch sử giao dịch kho của 1 sản phẩm — đọc từ ChiTietChungTu.
        /// </summary>
        public List<ET_SoCai> GetLichSuGiaoDich(int idSanPham, DateTime tuNgay, DateTime denNgay, int? idKhoDi = null, int? idKhoDen = null)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var query = from ct in db.ChiTietChungTus
                            join c in db.ChungTus on ct.IdChungTu equals c.Id
                            join kx in db.KhoHangs on ct.IdKhoXuat equals kx.Id
                            join kn in db.KhoHangs on ct.IdKhoNhap equals kn.Id
                            where ct.IdSanPham == idSanPham
                               && c.TrangThai == ET.Constants.AppConstants.TrangThaiChungTuKho.DaDuyet
                               && c.NgayChungTu >= tuNgay.Date
                               && c.NgayChungTu <= denNgay.Date.AddDays(1).AddTicks(-1)
                            select new { ct, c, kx, kn };

                if (idKhoDi.HasValue)
                    query = query.Where(x => x.ct.IdKhoXuat == idKhoDi.Value);

                if (idKhoDen.HasValue)
                    query = query.Where(x => x.ct.IdKhoNhap == idKhoDen.Value);

                return query.OrderByDescending(x => x.c.NgayChungTu)
                    .Select(x => new ET_SoCai
                    {
                        Id = x.ct.Id,
                        IdChungTu = x.ct.IdChungTu,
                        IdSanPham = x.ct.IdSanPham,
                        IdLoHang = x.ct.IdLoHang,
                        IdKhoXuat = x.ct.IdKhoXuat,
                        IdKhoNhap = x.ct.IdKhoNhap,
                        SoLuong = x.ct.SoLuong,
                        DonGia = x.ct.DonGia,
                        ThoiGian = x.c.NgayChungTu,
                        GhiChuCT = x.c.MaChungTu + " - " + x.c.LyDo
                    }).ToList();
            }
        }

        /// <summary>
        /// Tính tồn kho hiện tại của 1 sản phẩm tại 1 kho.
        /// SUM từ ChiTietChungTu (phiếu DaDuyet) .
        /// </summary>
        public decimal GetTonKhoHienTai(int idKho, int idSanPham, int? idLoHang = null)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var baseQuery = db.ChiTietChungTus
                    .Where(ct => ct.IdSanPham == idSanPham
                              && ct.ChungTu.TrangThai == ET.Constants.AppConstants.TrangThaiChungTuKho.DaDuyet);

                if (idLoHang.HasValue)
                    baseQuery = baseQuery.Where(ct => ct.IdLoHang == idLoHang.Value);

                decimal tongNhap = baseQuery
                    .Where(ct => ct.IdKhoNhap == idKho)
                    .Sum(ct => (decimal?)ct.SoLuong) ?? 0m;

                decimal tongXuat = baseQuery
                    .Where(ct => ct.IdKhoXuat == idKho)
                    .Sum(ct => (decimal?)ct.SoLuong) ?? 0m;

                return tongNhap - tongXuat;
            }
        }

        /// <summary>
        /// Báo cáo tồn kho tổng hợp.
        /// </summary>
        public List<ET_TonKho> GetBaoCaoTonKho(int? idKho = null)
        {
            using (var db = new DaiNamDBDataContext())
            {
                // Nhập vào kho thực (+)
                var nhap = db.ChiTietChungTus
                    .Where(ct => ct.ChungTu.TrangThai == ET.Constants.AppConstants.TrangThaiChungTuKho.DaDuyet
                              && !ct.KhoHang1.LaKhoAo)  // KhoHang1 = IdKhoNhap navigation
                    .Select(ct => new { ct.IdSanPham, IdKho = ct.IdKhoNhap, ct.SoLuong });

                // Xuất khỏi kho thực (-)
                var xuat = db.ChiTietChungTus
                    .Where(ct => ct.ChungTu.TrangThai == ET.Constants.AppConstants.TrangThaiChungTuKho.DaDuyet
                              && !ct.KhoHang.LaKhoAo)   // KhoHang = IdKhoXuat navigation
                    .Select(ct => new { ct.IdSanPham, IdKho = ct.IdKhoXuat, SoLuong = -ct.SoLuong });

                var all = nhap.Concat(xuat);

                if (idKho.HasValue)
                    all = all.Where(x => x.IdKho == idKho.Value);

                var grouped = from r in all
                              group r by new { r.IdSanPham, r.IdKho } into g
                              where g.Sum(x => x.SoLuong) != 0
                              select new { g.Key.IdSanPham, g.Key.IdKho, Ton = g.Sum(x => x.SoLuong) };

                var result = from g in grouped
                             join sp in db.SanPhams on g.IdSanPham equals sp.Id
                             join kh in db.KhoHangs on g.IdKho equals kh.Id
                             join dvt in db.DonViTinhs on sp.IdDonViTinh equals dvt.Id
                             orderby kh.TenKho, sp.TenSanPham
                             select new ET_TonKho
                             {
                                 IdSanPham = g.IdSanPham,
                                 MaSanPham = sp.MaSanPham,
                                 TenSanPham = sp.TenSanPham,
                                 DVT = dvt.TenDonVi,
                                 IdKho = g.IdKho,
                                 TenKho = kh.TenKho,
                                 TonHienTai = g.Ton
                             };

                return result.ToList();
            }
        }

        /// <summary>
        /// Cảnh báo tồn kho dưới mức tối thiểu .
        /// </summary>
        public List<ET_TonKho> GetCanhBaoTonToiThieu()
        {
            // Lấy toàn bộ tồn kho 1 lần, so sánh với mức cảnh báo trong memory
            var tonAll = GetBaoCaoTonKho();

            using (var db = new DaiNamDBDataContext())
            {
                var mucTon = db.MucTonToiThieus.Where(m => m.TrangThai == true).ToList();

                var result = new List<ET_TonKho>();
                foreach (var m in mucTon)
                {
                    var ton = tonAll.FirstOrDefault(t => t.IdSanPham == m.IdSanPham && t.IdKho == m.IdKho);
                    decimal tonHienTai = ton?.TonHienTai ?? 0;
                    if (tonHienTai < m.MucCanhBao)
                    {
                        result.Add(new ET_TonKho
                        {
                            IdSanPham = m.IdSanPham,
                            IdKho = m.IdKho,
                            TenKho = ton?.TenKho ?? "",
                            MaSanPham = ton?.MaSanPham ?? "",
                            TenSanPham = ton?.TenSanPham ?? "",
                            DVT = ton?.DVT ?? "",
                            TonHienTai = tonHienTai,
                            MucCanhBao = m.MucCanhBao,
                            Thieu = tonHienTai - m.MucCanhBao
                        });
                    }
                }
                return result;
            }
        }

        #endregion

    }
}



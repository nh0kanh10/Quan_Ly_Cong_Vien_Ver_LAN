using System;
using System.Collections.Generic;
using System.Linq;
using ET.Constants;
using ET.Models.BanHang;

namespace DAL.Repositories.BanHang
{
    public class DAL_KiemSoatVe
    {
        #region Khởi tạo (Singleton)

        public static DAL_KiemSoatVe Instance { get; } = new DAL_KiemSoatVe();

        #endregion

        #region Truy vấn dữ liệu

        public ET_VeDienTu TimVeTheoMaVach(string maVach)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var ve = (from v in db.VeDienTus
                          join sp in db.SanPhams on v.IdSanPham equals sp.Id
                          join spVe in db.SanPham_Ves on sp.Id equals spVe.IdSanPham into spVeGroup
                          from spVe in spVeGroup.DefaultIfEmpty()
                          join kh in db.ThongTins on v.IdKhachHang equals kh.Id into khGroup
                          from kh in khGroup.DefaultIfEmpty()
                          where v.MaVach == maVach
                          select new ET_VeDienTu
                          {
                              Id = v.Id,
                              IdChiTietDonHang = v.IdChiTietDonHang,
                              IdSanPham = v.IdSanPham,
                              IdKhachHang = v.IdKhachHang,
                              IdKhuVucHienTai = v.IdKhuVucHienTai,
                              MaVach = v.MaVach,
                              SoLuotConLai = v.SoLuotConLai,
                              TrangThai = v.TrangThai,
                              ThoiGianQuet = v.ThoiGianQuet,
                              NgayHetHan = v.NgayHetHan,
                              NgayTao = v.NgayTao,
                              RowVersion = v.RowVersion.ToArray(),
                              TenSanPham = sp.TenSanPham,
                              LoaiVe = spVe != null ? spVe.LoaiVe : null,
                              DoiTuongVe = spVe != null ? spVe.DoiTuongVe : null,
                              TenKhachHang = kh != null ? kh.HoTen : null
                          }).FirstOrDefault();

                return ve;
            }
        }

        public List<ET.Models.DanhMuc.ET_Ve_QuyenTruyCap> LayQuyenTruyCap(int idSanPhamVe)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.Ve_QuyenTruyCaps
                    .Where(q => q.IdSanPhamVe == idSanPhamVe)
                    .Select(q => new ET.Models.DanhMuc.ET_Ve_QuyenTruyCap
                    {
                        Id = q.Id,
                        IdSanPhamVe = q.IdSanPhamVe,
                        IdKhuVuc = q.IdKhuVuc ?? 0,
                        IdTroChoi = q.IdTroChoi,
                        SoLuotChoPhep = q.SoLuotChoPhep
                    }).ToList();
            }
        }

        public List<ET.Models.DanhMuc.ET_TroChoi> LayDanhSachTroChoi(int? idKhuVuc)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var query = db.TroChois.AsQueryable();
                if (idKhuVuc.HasValue)
                    query = query.Where(t => t.IdKhuVuc == idKhuVuc.Value);

                return query.Select(t => new ET.Models.DanhMuc.ET_TroChoi
                {
                    Id = t.Id,
                    TenTroChoi = t.TenTroChoi,
                    IdKhuVuc = t.IdKhuVuc,
                    DaXoa = t.DaXoa == true
                }).ToList();
            }
        }

        public List<ET_LichSuQuet> LayLichSuQuetHomNay(int? idKhuVuc)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var homNay = DateTime.Today;

                var query = from lq in db.ChiTietLuotQuets
                            join ve in db.VeDienTus on lq.IdVeDienTu equals ve.Id
                            join sp in db.SanPhams on ve.IdSanPham equals sp.Id
                            join qr in db.Ve_QuyenTruyCaps on lq.IdQuyenTruyCap equals qr.Id
                            join kv in db.KhuVucs on qr.IdKhuVuc equals kv.Id into kvGroup
                            from kv in kvGroup.DefaultIfEmpty()
                            where lq.ThoiGianQuet >= homNay
                            select new { lq, ve, sp, qr, kv };

                if (idKhuVuc.HasValue)
                    query = query.Where(x => x.qr.IdKhuVuc == idKhuVuc.Value);

                return query
                    .OrderByDescending(x => x.lq.ThoiGianQuet)
                    .Take(200)
                    .Select(x => new ET_LichSuQuet
                    {
                        ThoiGian = x.lq.ThoiGianQuet,
                        MaVach = x.ve.MaVach,
                        TenSanPham = x.sp.TenSanPham,
                        KetQua = x.lq.KetQua,
                        TenKhuVuc = x.kv != null ? x.kv.TenKhuVuc : "",
                        SoLuotConLai = x.ve.SoLuotConLai
                    }).ToList();
            }
        }

        #endregion

        #region Nghiệp vụ đặc thù

        // Trừ lượt + ghi log quét vào bảng ChiTietLuotQuet.
        // Dùng Transaction để chống 2 cổng quét cùng lúc (RowVersion).
        public void TruLuotVaGhiLog(int idVe, int idQuyenTruyCap, int? idThietBi, string ketQua)
        {
            using (var db = new DaiNamDBDataContext())
            {
                db.Connection.Open();
                using (var tx = db.Connection.BeginTransaction())
                {
                    db.Transaction = tx;
                    try
                    {
                        // Trừ lượt nếu quét thành công
                        if (ketQua == AppConstants.KetQuaQuetVe.ThanhCong)
                        {
                            var ve = db.VeDienTus.FirstOrDefault(v => v.Id == idVe);
                            if (ve != null && ve.SoLuotConLai > 0)
                            {
                                ve.SoLuotConLai--;
                                ve.ThoiGianQuet = DateTime.Now;

                                if (ve.TrangThai == AppConstants.TrangThaiVe.ChuaSuDung)
                                    ve.TrangThai = AppConstants.TrangThaiVe.DaSuDung;
                            }
                        }

                        var log = new ChiTietLuotQuet
                        {
                            IdVeDienTu = idVe,
                            IdQuyenTruyCap = idQuyenTruyCap,
                            IdThietBi = idThietBi,
                            ThoiGianQuet = DateTime.Now,
                            KetQua = ketQua
                        };
                        db.ChiTietLuotQuets.InsertOnSubmit(log);

                        db.SubmitChanges();
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        #endregion
    }
}



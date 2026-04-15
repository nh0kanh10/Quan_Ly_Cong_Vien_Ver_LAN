using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    /// <summary>
    /// DAL cho các bảng HR mới (ChungChiNhanVien, KyLuat, DonXinNghi, TaiNanLaoDong).
    /// Đã chuyển sang dùng LINQ to SQL (User sẽ cập nhật DBML).
    /// </summary>
    public partial class DAL_NhanVien
    {
        // ── CHỨNG CHỈ NHÂN VIÊN ─────────────────────────────────────────────

        public List<ET_ChungChiNhanVien> LoadChungChi(int idNhanVien)
        {
            try
            {
                // Dùng instance DataContext hiện tại của DAL_NhanVien (biến db đã khởi tạo ở file kia)
                var result = from c in db.GetTable<ChungChiNhanVien>()
                             where c.IdNhanVien == idNhanVien
                             orderby c.NgayHetHan ascending
                             select new ET_ChungChiNhanVien
                             {
                                 Id           = c.Id,
                                 IdNhanVien   = c.IdNhanVien,
                                 LoaiChungChi = c.LoaiChungChi,
                                 SoChungChi   = c.SoChungChi,
                                 NhaCap       = c.NhaCap,
                                 NgayCap      = c.NgayCap,
                                 NgayHetHan   = c.NgayHetHan,
                                 TrangThai    = c.TrangThai,
                                 HinhAnhFile  = c.HinhAnhFile
                             };
                return result.ToList();
            }
            catch { return new List<ET_ChungChiNhanVien>(); }
        }

        public bool ThemChungChi(ET_ChungChiNhanVien et)
        {
            try
            {
                var cc = new ChungChiNhanVien
                {
                    IdNhanVien   = et.IdNhanVien,
                    LoaiChungChi = et.LoaiChungChi,
                    SoChungChi   = et.SoChungChi,
                    NhaCap       = et.NhaCap,
                    NgayCap      = et.NgayCap,
                    NgayHetHan   = et.NgayHetHan,
                    HinhAnhFile  = et.HinhAnhFile
                };
                db.GetTable<ChungChiNhanVien>().InsertOnSubmit(cc);
                db.SubmitChanges();
                return true;
            }
            catch { return false; }
        }

        public bool XoaChungChi(int id)
        {
            try
            {
                var cc = db.GetTable<ChungChiNhanVien>().FirstOrDefault(x => x.Id == id);
                if (cc != null)
                {
                    db.GetTable<ChungChiNhanVien>().DeleteOnSubmit(cc);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        // ── KỶ LUẬT ─────────────────────────────────────────────────────────

        public List<ET_KyLuat> LoadKyLuat(int idNhanVien)
        {
            try
            {
                var result = from k in db.GetTable<KyLuat>()
                             join nv in db.GetTable<NhanVien>() on k.IdNguoiQuyetDinh equals nv.Id into nvGroup
                             from nv in nvGroup.DefaultIfEmpty()
                             where k.IdNhanVien == idNhanVien
                             orderby k.NgayApDung descending
                             select new ET_KyLuat
                             {
                                 Id                = k.Id,
                                 IdNhanVien        = k.IdNhanVien,
                                 NgayApDung        = k.NgayApDung,
                                 HinhThuc          = k.HinhThuc,
                                 SoTienTru         = k.SoTienTru,
                                 SoNgayDinhChi     = k.SoNgayDinhChi,
                                 MoTa              = k.MoTa,
                                 IdNguoiQuyetDinh  = k.IdNguoiQuyetDinh,
                                 TenNguoiQuyetDinh = nv != null ? nv.HoTen : "N/A",
                                 NgayHetHieuLuc    = k.NgayHetHieuLuc
                             };
                return result.ToList();
            }
            catch { return new List<ET_KyLuat>(); }
        }

        public bool ThemKyLuat(ET_KyLuat et)
        {
            try
            {
                var kl = new KyLuat
                {
                    IdNhanVien       = et.IdNhanVien,
                    NgayApDung       = et.NgayApDung,
                    HinhThuc         = et.HinhThuc,
                    SoTienTru        = et.SoTienTru,
                    SoNgayDinhChi    = et.SoNgayDinhChi,
                    MoTa             = et.MoTa,
                    IdNguoiQuyetDinh = et.IdNguoiQuyetDinh,
                    NgayHetHieuLuc   = et.NgayHetHieuLuc,
                    CreatedAt        = DateTime.Now
                };
                db.GetTable<KyLuat>().InsertOnSubmit(kl);
                db.SubmitChanges();
                return true;
            }
            catch { return false; }
        }

        public bool XoaKyLuat(int id)
        {
            try
            {
                var kl = db.GetTable<KyLuat>().FirstOrDefault(x => x.Id == id);
                if (kl != null)
                {
                    db.GetTable<KyLuat>().DeleteOnSubmit(kl);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        // ── ĐƠN XIN NGHỈ ────────────────────────────────────────────────────

        public List<ET_DonXinNghi> LoadDonXinNghi(int idNhanVien)
        {
            try
            {
                var result = from d in db.GetTable<DonXinNghi>()
                             where d.IdNhanVien == idNhanVien
                             orderby d.NgayBatDau descending
                             select new ET_DonXinNghi
                             {
                                 Id             = d.Id,
                                 IdNhanVien     = d.IdNhanVien,
                                 LoaiNghi       = d.LoaiNghi,
                                 NgayBatDau     = d.NgayBatDau,
                                 NgayKetThuc    = d.NgayKetThuc,
                                 SoNgay         = d.SoNgay ?? 0,
                                 TiLeLuongHuong = d.TiLeLuongHuong,
                                 NguonChiTra    = d.NguonChiTra,
                                 LyDo           = d.LyDo,
                                 TrangThai      = d.TrangThai,
                                 CreatedAt      = d.CreatedAt
                             };
                return result.ToList();
            }
            catch { return new List<ET_DonXinNghi>(); }
        }

        public bool ThemDonXinNghi(ET_DonXinNghi et)
        {
            try
            {
                var don = new DonXinNghi
                {
                    IdNhanVien     = et.IdNhanVien,
                    LoaiNghi       = et.LoaiNghi,
                    NgayBatDau     = et.NgayBatDau,
                    NgayKetThuc    = et.NgayKetThuc,
                    TiLeLuongHuong = et.TiLeLuongHuong,
                    NguonChiTra    = et.NguonChiTra,
                    LyDo           = et.LyDo,
                    TrangThai      = et.TrangThai ?? AppConstants.TrangThaiDonNghi.ChoDuyet,
                    CreatedAt      = DateTime.Now
                };
                db.GetTable<DonXinNghi>().InsertOnSubmit(don);
                db.SubmitChanges();
                return true;
            }
            catch { return false; }
        }

        public bool XoaDonXinNghi(int id)
        {
            try
            {
                var don = db.GetTable<DonXinNghi>().FirstOrDefault(x => x.Id == id);
                if (don != null)
                {
                    db.GetTable<DonXinNghi>().DeleteOnSubmit(don);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        // ── TAI NẠN LAO ĐỘNG ────────────────────────────────────────────────

        public List<ET_TaiNanLaoDong> LoadTaiNanLaoDong(int idNhanVien)
        {
            try
            {
                var result = from t in db.GetTable<TaiNanLaoDong>()
                             where t.IdNhanVien == idNhanVien
                             orderby t.NgayTaiNan descending
                             select new ET_TaiNanLaoDong
                             {
                                 Id         = t.Id,
                                 IdNhanVien = t.IdNhanVien,
                                 NgayTaiNan = t.NgayTaiNan,
                                 LoaiTaiNan = t.LoaiTaiNan,
                                 MucDo      = t.MucDo,
                                 MoTa       = t.MoTa,
                                 SoNgayNghi = t.SoNgayNghi,
                                 TrangThai  = t.TrangThai,
                                 CreatedAt  = t.CreatedAt
                             };
                return result.ToList();
            }
            catch { return new List<ET_TaiNanLaoDong>(); }
        }

        public bool ThemTaiNanLaoDong(ET_TaiNanLaoDong et)
        {
            try
            {
                var tn = new TaiNanLaoDong
                {
                    IdNhanVien = et.IdNhanVien,
                    NgayTaiNan = et.NgayTaiNan,
                    LoaiTaiNan = et.LoaiTaiNan,
                    MucDo      = et.MucDo,
                    MoTa       = et.MoTa,
                    TrangThai  = et.TrangThai ?? AppConstants.TrangThaiTaiNan.DangDieuTri,
                    CreatedAt  = DateTime.Now
                };
                db.GetTable<TaiNanLaoDong>().InsertOnSubmit(tn);
                db.SubmitChanges();
                return true;
            }
            catch { return false; }
        }

        public bool XoaTaiNanLaoDong(int id)
        {
            try
            {
                var tn = db.GetTable<TaiNanLaoDong>().FirstOrDefault(x => x.Id == id);
                if (tn != null)
                {
                    db.GetTable<TaiNanLaoDong>().DeleteOnSubmit(tn);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

    }
}

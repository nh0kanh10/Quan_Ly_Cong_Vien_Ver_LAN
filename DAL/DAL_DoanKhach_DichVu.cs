using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public class DAL_DoanKhach_DichVu
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_DoanKhach_DichVu instance;
        public static DAL_DoanKhach_DichVu Instance
        {
            get
            {
                if (instance == null) instance = new DAL_DoanKhach_DichVu();
                return instance;
            }
        }

        private ET_DoanKhach_DichVu MapToET(DoanKhach_DichVu s)
        {
            // Lookup tên sản phẩm / combo cho display
            string tenSP = null;
            string tenCombo = null;
            if (s.IdSanPham.HasValue)
            {
                var sp = db.GetTable<SanPham>().FirstOrDefault(x => x.Id == s.IdSanPham.Value);
                tenSP = sp?.Ten;
            }
            if (s.IdCombo.HasValue)
            {
                var cb = db.GetTable<Combo>().FirstOrDefault(x => x.Id == s.IdCombo.Value);
                tenCombo = cb?.Ten;
            }

            return new ET_DoanKhach_DichVu
            {
                Id = s.Id,
                IdDoan = s.IdDoan,
                LoaiDichVu = s.LoaiDichVu,
                IdCombo = s.IdCombo,
                IdSanPham = s.IdSanPham,
                SoLuong = s.SoLuong,
                DonGia = s.DonGia,
                ThanhTien = s.ThanhTien ?? 0,
                NgaySuDung = s.NgaySuDung,
                IdThamChieu = s.IdThamChieu,
                IdChiTietDonHang = s.IdChiTietDonHang,
                GhiChu = s.GhiChu,
                TrangThai = s.TrangThai,
                SoLuongDaDung = s.SoLuongDaDung,
                TenDichVu = tenSP,
                TenCombo = tenCombo
            };
        }

        public List<ET_DoanKhach_DichVu> LayTheoDoan(int idDoan)
        {
            return db.GetTable<DoanKhach_DichVu>()
                .Where(x => x.IdDoan == idDoan && x.TrangThai != AppConstants.TrangThaiDichVuDoan.DaHuy)
                .Select(s => MapToET(s))
                .ToList();
        }

        /// <summary>
        /// Lấy 1 dịch vụ đoàn theo Id — dùng fresh context để tránh stale data.
        /// </summary>
        public ET_DoanKhach_DichVu LayTheoId(int id)
        {
            using (var freshDb = new DataQuanLyDaiNamDataContext())
            {
                var s = freshDb.GetTable<DoanKhach_DichVu>().FirstOrDefault(x => x.Id == id);
                if (s == null) return null;

                string tenSP = null, tenCombo = null;
                if (s.IdSanPham.HasValue)
                    tenSP = freshDb.GetTable<SanPham>().FirstOrDefault(x => x.Id == s.IdSanPham.Value)?.Ten;
                if (s.IdCombo.HasValue)
                    tenCombo = freshDb.GetTable<Combo>().FirstOrDefault(x => x.Id == s.IdCombo.Value)?.Ten;

                return new ET_DoanKhach_DichVu
                {
                    Id = s.Id,
                    IdDoan = s.IdDoan,
                    LoaiDichVu = s.LoaiDichVu,
                    IdCombo = s.IdCombo,
                    IdSanPham = s.IdSanPham,
                    SoLuong = s.SoLuong,
                    DonGia = s.DonGia,
                    ThanhTien = s.ThanhTien ?? 0,
                    NgaySuDung = s.NgaySuDung,
                    IdThamChieu = s.IdThamChieu,
                    IdChiTietDonHang = s.IdChiTietDonHang,
                    GhiChu = s.GhiChu,
                    TrangThai = s.TrangThai,
                    SoLuongDaDung = s.SoLuongDaDung,
                    TenDichVu = tenSP,
                    TenCombo = tenCombo
                };
            }
        }

        public int Them(ET_DoanKhach_DichVu et)
        {
            try
            {
                var obj = new DoanKhach_DichVu();
                obj.IdDoan = et.IdDoan;
                obj.LoaiDichVu = et.LoaiDichVu;
                obj.IdCombo = et.IdCombo;
                obj.IdSanPham = et.IdSanPham;
                obj.SoLuong = et.SoLuong;
                obj.DonGia = et.DonGia;
                obj.NgaySuDung = et.NgaySuDung;
                obj.IdThamChieu = et.IdThamChieu;
                obj.IdChiTietDonHang = et.IdChiTietDonHang;
                obj.GhiChu = et.GhiChu;
                obj.TrangThai = et.TrangThai ?? AppConstants.TrangThaiDichVuDoan.ChuaXuLy;
                db.GetTable<DoanKhach_DichVu>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return obj.Id;
            }
            catch { return 0; }
        }

        public bool Sua(ET_DoanKhach_DichVu et)
        {
            try
            {
                var obj = db.GetTable<DoanKhach_DichVu>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null)
                {
                    obj.SoLuong = et.SoLuong;
                    obj.DonGia = et.DonGia;
                    obj.NgaySuDung = et.NgaySuDung;
                    obj.IdThamChieu = et.IdThamChieu;
                    obj.IdChiTietDonHang = et.IdChiTietDonHang;
                    obj.GhiChu = et.GhiChu;
                    obj.TrangThai = et.TrangThai;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        public bool Xoa(int id)
        {
            try
            {
                var obj = db.GetTable<DoanKhach_DichVu>().FirstOrDefault(x => x.Id == id);
                if (obj != null)
                {
                    obj.TrangThai = AppConstants.TrangThaiDichVuDoan.DaHuy;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        public bool CapNhatTrangThai(int id, string trangThai)
        {
            try
            {
                var obj = db.GetTable<DoanKhach_DichVu>().FirstOrDefault(x => x.Id == id);
                if (obj != null)
                {
                    obj.TrangThai = trangThai;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        public bool CapNhatThamChieu(int id, int idThamChieu)
        {
            try
            {
                var obj = db.GetTable<DoanKhach_DichVu>().FirstOrDefault(x => x.Id == id);
                if (obj != null)
                {
                    obj.IdThamChieu = idThamChieu;
                    obj.TrangThai = AppConstants.TrangThaiDichVuDoan.DaDatCho;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch { return false; }
        }
        /// <summary>
        /// Khấu trừ quota dịch vụ đoàn: SoLuongDaDung += soLuongDung (atomic)
        /// Dùng tại Gate, POS, Nhà hàng
        /// </summary>
        public bool KhauTruQuota(int idDichVuDoan, int soLuongDung)
        {
            try
            {
                var obj = db.GetTable<DoanKhach_DichVu>().FirstOrDefault(x => x.Id == idDichVuDoan);
                if (obj == null) return false;
                if (obj.SoLuongDaDung + soLuongDung > obj.SoLuong) return false;
                obj.SoLuongDaDung += soLuongDung;
                db.SubmitChanges();
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Lấy tất cả dịch vụ (chưa hủy) của đoàn — dùng fresh context để thấy data mới nhất
        /// </summary>
        public List<ET_DoanKhach_DichVu> LayQuotaConLai(int idDoan)
        {
            using (var freshDb = new DataQuanLyDaiNamDataContext())
            {
                var rows = freshDb.GetTable<DoanKhach_DichVu>()
                    .Where(x => x.IdDoan == idDoan
                        && x.TrangThai != AppConstants.TrangThaiDichVuDoan.DaHuy)
                    .ToList();

                return rows.Select(s =>
                {
                    string tenSP = null, tenCombo = null;
                    if (s.IdSanPham.HasValue)
                        tenSP = freshDb.GetTable<SanPham>().FirstOrDefault(x => x.Id == s.IdSanPham.Value)?.Ten;
                    if (s.IdCombo.HasValue)
                        tenCombo = freshDb.GetTable<Combo>().FirstOrDefault(x => x.Id == s.IdCombo.Value)?.Ten;

                    return new ET_DoanKhach_DichVu
                    {
                        Id = s.Id,
                        IdDoan = s.IdDoan,
                        LoaiDichVu = s.LoaiDichVu,
                        IdCombo = s.IdCombo,
                        IdSanPham = s.IdSanPham,
                        SoLuong = s.SoLuong,
                        DonGia = s.DonGia,
                        ThanhTien = s.ThanhTien ?? 0,
                        NgaySuDung = s.NgaySuDung,
                        IdThamChieu = s.IdThamChieu,
                        IdChiTietDonHang = s.IdChiTietDonHang,
                        GhiChu = s.GhiChu,
                        TrangThai = s.TrangThai,
                        SoLuongDaDung = s.SoLuongDaDung,
                        TenDichVu = tenSP,
                        TenCombo = tenCombo
                    };
                }).ToList();
            }
        }
    }
}

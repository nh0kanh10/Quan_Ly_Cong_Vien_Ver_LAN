using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_DonHang
    {
        private static DAL_DonHang instance;
        private DataQuanLyDaiNamDataContext CreateDb() { return new DataQuanLyDaiNamDataContext(); }
        public static DAL_DonHang Instance
        {
            get
            {
                if (instance == null) instance = new DAL_DonHang();
                return instance;
            }
        }

        public List<ET_DonHang> LoadDS()
        {
            using (var db = CreateDb())
            {
                return db.GetTable<DonHang>().Select(s => new ET_DonHang
                {
                    Id = s.Id,
                    MaCode = s.MaCode,
                    IdKhachHang = s.IdKhachHang,
                    IdDoan = s.IdDoan,
                    IdKhuyenMai = s.IdKhuyenMai,
                    TienGiamGia = s.TienGiamGia,
                    ThoiGian = s.ThoiGian,
                    TongTien = s.TongTien,
                    TrangThai = s.TrangThai,
                    GhiChu = s.GhiChu,
                    CreatedAt = s.CreatedAt,
                    CreatedBy = s.CreatedBy,
                    NguonBan = s.NguonBan
                }).ToList();
            }
        }

        public List<ET_DonHang> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_DonHang et)
        {
            if (et == null) return false;
            try {
                using (var db = CreateDb())
                {
                    DonHang obj = new DonHang();
                    obj.MaCode = et.MaCode;
                    obj.IdKhachHang = et.IdKhachHang;
                    obj.IdDoan = et.IdDoan;
                    obj.IdKhuyenMai = et.IdKhuyenMai;
                    obj.TienGiamGia = et.TienGiamGia;
                    obj.ThoiGian = et.ThoiGian;
                    obj.TongTien = et.TongTien;
                    obj.TrangThai = et.TrangThai;
                    obj.GhiChu = et.GhiChu;
                    obj.CreatedAt = et.CreatedAt;
                    obj.CreatedBy = et.CreatedBy;
                    obj.NguonBan = et.NguonBan ?? "POS";
                    db.GetTable<DonHang>().InsertOnSubmit(obj);
                    db.SubmitChanges();
                }
                return true;
            } catch (Exception ex) { DalErrorLogger.Log("DAL_DonHang.Them", ex); return false; }
        }

        public int ThemVaLayId(ET_DonHang et)
        {
            if (et == null) return 0;
            try
            {
                using (var db = CreateDb())
                {
                    DonHang obj = new DonHang();
                    obj.MaCode = et.MaCode;
                    obj.IdKhachHang = et.IdKhachHang;
                    obj.IdDoan = et.IdDoan;
                    obj.IdKhuyenMai = et.IdKhuyenMai;
                    obj.TienGiamGia = et.TienGiamGia;
                    obj.ThoiGian = et.ThoiGian;
                    obj.TongTien = et.TongTien;
                    obj.TrangThai = et.TrangThai;
                    obj.GhiChu = et.GhiChu;
                    obj.CreatedAt = et.CreatedAt;
                    obj.CreatedBy = et.CreatedBy;
                    obj.NguonBan = et.NguonBan ?? "POS";
                    db.GetTable<DonHang>().InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.Id;
                }
            }
            catch (Exception ex)
            {
                DalErrorLogger.Log("DAL_DonHang.ThemVaLayId", ex);
                return 0;
            }
        }

        public bool Sua(ET_DonHang et)
        {
            if (et == null) return false;
            try {
                using (var db = CreateDb())
                {
                    var obj = db.GetTable<DonHang>().FirstOrDefault(x => x.Id == et.Id);
                    if (obj != null) {
                        obj.MaCode = et.MaCode;
                        obj.IdKhachHang = et.IdKhachHang;
                        obj.IdDoan = et.IdDoan;
                        obj.IdKhuyenMai = et.IdKhuyenMai;
                        obj.TienGiamGia = et.TienGiamGia;
                        obj.ThoiGian = et.ThoiGian;
                        obj.TongTien = et.TongTien;
                        obj.TrangThai = et.TrangThai;
                        obj.GhiChu = et.GhiChu;
                        obj.CreatedAt = et.CreatedAt;
                        obj.CreatedBy = et.CreatedBy;
                        obj.NguonBan = et.NguonBan ?? obj.NguonBan;
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
            } catch (Exception ex) { DalErrorLogger.Log("DAL_DonHang.Sua", ex); return false; }
        }

        public bool Xoa(int id)
        {
            try {
                using (var db = CreateDb())
                {
                    var obj = db.GetTable<DonHang>().FirstOrDefault(x => x.Id == id);
                    if (obj != null) {
                        db.GetTable<DonHang>().DeleteOnSubmit(obj);
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
            } catch (Exception ex) { DalErrorLogger.Log("DAL_DonHang.Xoa", ex); return false; }
        }

        public ET_DonHang LayTheoId(int id)
        {
            using (var db = CreateDb())
            {
                return db.GetTable<DonHang>().Where(x => x.Id == id).Select(s => new ET_DonHang {
                    Id = s.Id,
                    MaCode = s.MaCode,
                    IdKhachHang = s.IdKhachHang,
                    IdDoan = s.IdDoan,
                    IdKhuyenMai = s.IdKhuyenMai,
                    TienGiamGia = s.TienGiamGia,
                    ThoiGian = s.ThoiGian,
                    TongTien = s.TongTien,
                    TrangThai = s.TrangThai,
                    GhiChu = s.GhiChu,
                    CreatedAt = s.CreatedAt,
                    CreatedBy = s.CreatedBy,
                    NguonBan = s.NguonBan
                }).FirstOrDefault();
            }
        }

        public ET_DonHang LayTheoMaCode(string maCode)
        {
            if (string.IsNullOrWhiteSpace(maCode)) return null;
            using (var db = CreateDb())
            {
                return db.GetTable<DonHang>().Where(x => x.MaCode == maCode).Select(s => new ET_DonHang {
                    Id = s.Id,
                    MaCode = s.MaCode,
                    IdKhachHang = s.IdKhachHang,
                    IdDoan = s.IdDoan,
                    IdKhuyenMai = s.IdKhuyenMai,
                    TienGiamGia = s.TienGiamGia,
                    ThoiGian = s.ThoiGian,
                    TongTien = s.TongTien,
                    TrangThai = s.TrangThai,
                    GhiChu = s.GhiChu,
                    CreatedAt = s.CreatedAt,
                    CreatedBy = s.CreatedBy,
                    NguonBan = s.NguonBan
                }).FirstOrDefault();
            }
        }
    }
}

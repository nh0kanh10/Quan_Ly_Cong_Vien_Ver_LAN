using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_KhuyenMai
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_KhuyenMai instance;
        public static DAL_KhuyenMai Instance
        {
            get
            {
                if (instance == null) instance = new DAL_KhuyenMai();
                return instance;
            }
        }

        public List<ET_KhuyenMai> LoadDS()
        {
            return db.GetTable<KhuyenMai>().Select(s => new ET_KhuyenMai
            {
                Id = s.Id,
                MaCode = s.MaCode,
                TenKhuyenMai = s.TenKhuyenMai,
                IdSuKien = s.IdSuKien,
                LoaiGiamGia = s.LoaiGiamGia,
                GiaTriGiam = s.GiaTriGiam,
                DonToiThieu = s.DonToiThieu,
                NgayBatDau = s.NgayBatDau,
                NgayKetThuc = s.NgayKetThuc,
                TrangThai = s.TrangThai,
                CreatedAt = s.CreatedAt,
                CreatedBy = s.CreatedBy,
                IsDeleted = s.IsDeleted
            }).ToList();
        }

        public List<ET_KhuyenMai> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_KhuyenMai et)
        {
            try {
                KhuyenMai obj = new KhuyenMai();
                obj.MaCode = et.MaCode;
                obj.TenKhuyenMai = et.TenKhuyenMai;
                obj.IdSuKien = et.IdSuKien;
                obj.LoaiGiamGia = et.LoaiGiamGia;
                obj.GiaTriGiam = et.GiaTriGiam;
                obj.DonToiThieu = et.DonToiThieu;
                obj.NgayBatDau = et.NgayBatDau;
                obj.NgayKetThuc = et.NgayKetThuc;
                obj.TrangThai = et.TrangThai;
                obj.CreatedAt = et.CreatedAt;
                obj.CreatedBy = et.CreatedBy;
                obj.IsDeleted = et.IsDeleted;
                db.GetTable<KhuyenMai>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_KhuyenMai et)
        {
            try {
                var obj = db.GetTable<KhuyenMai>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.MaCode = et.MaCode;
                    obj.TenKhuyenMai = et.TenKhuyenMai;
                    obj.IdSuKien = et.IdSuKien;
                    obj.LoaiGiamGia = et.LoaiGiamGia;
                    obj.GiaTriGiam = et.GiaTriGiam;
                    obj.DonToiThieu = et.DonToiThieu;
                    obj.NgayBatDau = et.NgayBatDau;
                    obj.NgayKetThuc = et.NgayKetThuc;
                    obj.TrangThai = et.TrangThai;
                    obj.CreatedAt = et.CreatedAt;
                    obj.CreatedBy = et.CreatedBy;
                    obj.IsDeleted = et.IsDeleted;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<KhuyenMai>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<KhuyenMai>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_KhuyenMai LayTheoId(int id)
        {
            return db.GetTable<KhuyenMai>().Where(x => x.Id == id).Select(s => new ET_KhuyenMai {
                Id = s.Id,
                MaCode = s.MaCode,
                TenKhuyenMai = s.TenKhuyenMai,
                IdSuKien = s.IdSuKien,
                LoaiGiamGia = s.LoaiGiamGia,
                GiaTriGiam = s.GiaTriGiam,
                DonToiThieu = s.DonToiThieu,
                NgayBatDau = s.NgayBatDau,
                NgayKetThuc = s.NgayKetThuc,
                TrangThai = s.TrangThai,
                CreatedAt = s.CreatedAt,
                CreatedBy = s.CreatedBy,
                IsDeleted = s.IsDeleted
            }).FirstOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_ChoiNghiMat
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_ChoiNghiMat instance;
        public static DAL_ChoiNghiMat Instance
        {
            get
            {
                if (instance == null) instance = new DAL_ChoiNghiMat();
                return instance;
            }
        }

        public List<ET_ChoiNghiMat> LoadDS()
        {
            return db.GetTable<ChoiNghiMat>().Select(s => new ET_ChoiNghiMat
            {
                Id = s.Id,
                MaCode = s.MaCode,
                IdKhuVucBien = s.IdKhuVucBien,
                IdSanPham = s.IdSanPham,
                TenChoi = s.TenChoi,
                SucChua = s.SucChua,
                TrangThai = s.TrangThai,
                CreatedAt = s.CreatedAt,
                CreatedBy = s.CreatedBy,
                IsDeleted = s.IsDeleted,
                RowVer = s.RowVer == null ? null : s.RowVer.ToArray()
            }).ToList();
        }

        public List<ET_ChoiNghiMat> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_ChoiNghiMat et)
        {
            try {
                ChoiNghiMat obj = new ChoiNghiMat();
                obj.MaCode = et.MaCode;
                obj.IdKhuVucBien = et.IdKhuVucBien;
                obj.IdSanPham = et.IdSanPham;
                obj.TenChoi = et.TenChoi;
                obj.SucChua = et.SucChua;
                obj.TrangThai = et.TrangThai;
                obj.CreatedAt = et.CreatedAt;
                obj.CreatedBy = et.CreatedBy;
                obj.IsDeleted = et.IsDeleted;
                obj.RowVer = et.RowVer;
                db.GetTable<ChoiNghiMat>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_ChoiNghiMat et)
        {
            try {
                var obj = db.GetTable<ChoiNghiMat>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.MaCode = et.MaCode;
                    obj.IdKhuVucBien = et.IdKhuVucBien;
                    obj.IdSanPham = et.IdSanPham;
                    obj.TenChoi = et.TenChoi;
                    obj.SucChua = et.SucChua;
                    obj.TrangThai = et.TrangThai;
                    obj.CreatedAt = et.CreatedAt;
                    obj.CreatedBy = et.CreatedBy;
                    obj.IsDeleted = et.IsDeleted;
                    obj.RowVer = et.RowVer;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<ChoiNghiMat>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<ChoiNghiMat>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_ChoiNghiMat LayTheoId(int id)
        {
            return db.GetTable<ChoiNghiMat>().Where(x => x.Id == id).Select(s => new ET_ChoiNghiMat {
                Id = s.Id,
                MaCode = s.MaCode,
                IdKhuVucBien = s.IdKhuVucBien,
                IdSanPham = s.IdSanPham,
                TenChoi = s.TenChoi,
                SucChua = s.SucChua,
                TrangThai = s.TrangThai,
                CreatedAt = s.CreatedAt,
                CreatedBy = s.CreatedBy,
                IsDeleted = s.IsDeleted,
                RowVer = s.RowVer == null ? null : s.RowVer.ToArray()
            }).FirstOrDefault();
        }
    }
}

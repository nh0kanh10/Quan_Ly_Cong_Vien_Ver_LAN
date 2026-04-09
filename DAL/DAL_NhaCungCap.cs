using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_NhaCungCap
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_NhaCungCap instance;
        public static DAL_NhaCungCap Instance
        {
            get
            {
                if (instance == null) instance = new DAL_NhaCungCap();
                return instance;
            }
        }

        public List<ET_NhaCungCap> LoadDS()
        {
            return db.GetTable<NhaCungCap>().Select(s => new ET_NhaCungCap
            {
                Id = s.Id,
                Ten = s.Ten,
                MaSoThue = s.MaSoThue,
                DiaChi = s.DiaChi,
                DienThoai = s.DienThoai,
                NguoiLienHe = s.NguoiLienHe,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt,
                CreatedBy = s.CreatedBy,
                IsDeleted = s.IsDeleted
            }).ToList();
        }

        public List<ET_NhaCungCap> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_NhaCungCap et)
        {
            try {
                NhaCungCap obj = new NhaCungCap();
                obj.Ten = et.Ten;
                obj.MaSoThue = et.MaSoThue;
                obj.DiaChi = et.DiaChi;
                obj.DienThoai = et.DienThoai;
                obj.NguoiLienHe = et.NguoiLienHe;
                obj.CreatedAt = et.CreatedAt;
                obj.UpdatedAt = et.UpdatedAt;
                obj.CreatedBy = et.CreatedBy;
                obj.IsDeleted = et.IsDeleted;
                db.GetTable<NhaCungCap>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_NhaCungCap et)
        {
            try {
                var obj = db.GetTable<NhaCungCap>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.Ten = et.Ten;
                    obj.MaSoThue = et.MaSoThue;
                    obj.DiaChi = et.DiaChi;
                    obj.DienThoai = et.DienThoai;
                    obj.NguoiLienHe = et.NguoiLienHe;
                    obj.CreatedAt = et.CreatedAt;
                    obj.UpdatedAt = et.UpdatedAt;
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
                var obj = db.GetTable<NhaCungCap>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<NhaCungCap>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_NhaCungCap LayTheoId(int id)
        {
            return db.GetTable<NhaCungCap>().Where(x => x.Id == id).Select(s => new ET_NhaCungCap {
                Id = s.Id,
                Ten = s.Ten,
                MaSoThue = s.MaSoThue,
                DiaChi = s.DiaChi,
                DienThoai = s.DienThoai,
                NguoiLienHe = s.NguoiLienHe,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt,
                CreatedBy = s.CreatedBy,
                IsDeleted = s.IsDeleted
            }).FirstOrDefault();
        }
    }
}

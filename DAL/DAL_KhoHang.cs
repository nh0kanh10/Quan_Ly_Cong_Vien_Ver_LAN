using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_KhoHang
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_KhoHang instance;
        public static DAL_KhoHang Instance
        {
            get
            {
                if (instance == null) instance = new DAL_KhoHang();
                return instance;
            }
        }

        public List<ET_KhoHang> LoadDS()
        {
            return db.GetTable<KhoHang>().Select(s => new ET_KhoHang
            {
                Id = s.Id,
                TenKho = s.TenKho,
                LoaiKho = s.LoaiKho,
                IdKhuVuc = s.IdKhuVuc,
                DiaChi = s.DiaChi,
                CreatedAt = s.CreatedAt,
                IsDeleted = s.IsDeleted
            }).ToList();
        }

        public List<ET_KhoHang> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_KhoHang et)
        {
            try {
                KhoHang obj = new KhoHang();
                obj.TenKho = et.TenKho;
                obj.LoaiKho = et.LoaiKho;
                obj.IdKhuVuc = et.IdKhuVuc;
                obj.DiaChi = et.DiaChi;
                obj.CreatedAt = et.CreatedAt;
                obj.IsDeleted = et.IsDeleted;
                db.GetTable<KhoHang>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_KhoHang et)
        {
            try {
                var obj = db.GetTable<KhoHang>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.TenKho = et.TenKho;
                    obj.LoaiKho = et.LoaiKho;
                    obj.IdKhuVuc = et.IdKhuVuc;
                    obj.DiaChi = et.DiaChi;
                    obj.CreatedAt = et.CreatedAt;
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
                var obj = db.GetTable<KhoHang>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<KhoHang>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_KhoHang LayTheoId(int id)
        {
            return db.GetTable<KhoHang>().Where(x => x.Id == id).Select(s => new ET_KhoHang {
                Id = s.Id,
                TenKho = s.TenKho,
                LoaiKho = s.LoaiKho,
                IdKhuVuc = s.IdKhuVuc,
                DiaChi = s.DiaChi,
                CreatedAt = s.CreatedAt,
                IsDeleted = s.IsDeleted
            }).FirstOrDefault();
        }
    }
}

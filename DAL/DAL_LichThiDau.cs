using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_LichThiDau
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_LichThiDau instance;
        public static DAL_LichThiDau Instance
        {
            get
            {
                if (instance == null) instance = new DAL_LichThiDau();
                return instance;
            }
        }

        public List<ET_LichThiDau> LoadDS()
        {
            return db.GetTable<LichThiDau>().Select(s => new ET_LichThiDau
            {
                Id = s.Id,
                IdGiaiDua = s.IdGiaiDua,
                IdDuongDua = s.IdDuongDua,
                IdLoaiHinh = s.IdLoaiHinh,
                ThoiGianDuKien = s.ThoiGianDuKien
            }).ToList();
        }

        public List<ET_LichThiDau> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_LichThiDau et)
        {
            try {
                LichThiDau obj = new LichThiDau();
                obj.IdGiaiDua = et.IdGiaiDua;
                obj.IdDuongDua = et.IdDuongDua;
                obj.IdLoaiHinh = et.IdLoaiHinh;
                obj.ThoiGianDuKien = et.ThoiGianDuKien;
                db.GetTable<LichThiDau>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_LichThiDau et)
        {
            try {
                var obj = db.GetTable<LichThiDau>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.IdGiaiDua = et.IdGiaiDua;
                    obj.IdDuongDua = et.IdDuongDua;
                    obj.IdLoaiHinh = et.IdLoaiHinh;
                    obj.ThoiGianDuKien = et.ThoiGianDuKien;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<LichThiDau>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<LichThiDau>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_LichThiDau LayTheoId(int id)
        {
            return db.GetTable<LichThiDau>().Where(x => x.Id == id).Select(s => new ET_LichThiDau {
                Id = s.Id,
                IdGiaiDua = s.IdGiaiDua,
                IdDuongDua = s.IdDuongDua,
                IdLoaiHinh = s.IdLoaiHinh,
                ThoiGianDuKien = s.ThoiGianDuKien
            }).FirstOrDefault();
        }
    }
}

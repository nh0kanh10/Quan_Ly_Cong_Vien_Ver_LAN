using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_VanDongVien
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_VanDongVien instance;
        public static DAL_VanDongVien Instance
        {
            get
            {
                if (instance == null) instance = new DAL_VanDongVien();
                return instance;
            }
        }

        public List<ET_VanDongVien> LoadDS()
        {
            return db.GetTable<VanDongVien>().Select(s => new ET_VanDongVien
            {
                Id = s.Id,
                HoTen = s.HoTen,
                LoaiVdv = s.LoaiVdv
            }).ToList();
        }

        public List<ET_VanDongVien> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_VanDongVien et)
        {
            try {
                VanDongVien obj = new VanDongVien();
                obj.HoTen = et.HoTen;
                obj.LoaiVdv = et.LoaiVdv;
                db.GetTable<VanDongVien>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_VanDongVien et)
        {
            try {
                var obj = db.GetTable<VanDongVien>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.HoTen = et.HoTen;
                    obj.LoaiVdv = et.LoaiVdv;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<VanDongVien>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<VanDongVien>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_VanDongVien LayTheoId(int id)
        {
            return db.GetTable<VanDongVien>().Where(x => x.Id == id).Select(s => new ET_VanDongVien {
                Id = s.Id,
                HoTen = s.HoTen,
                LoaiVdv = s.LoaiVdv
            }).FirstOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_Kiosk
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_Kiosk instance;
        public static DAL_Kiosk Instance
        {
            get
            {
                if (instance == null) instance = new DAL_Kiosk();
                return instance;
            }
        }

        public List<ET_Kiosk> LoadDS()
        {
            return db.GetTable<Kiosk>().Select(s => new ET_Kiosk
            {
                Id = s.Id,
                ViTri = s.ViTri,
                TrangThai = s.TrangThai
            }).ToList();
        }

        public List<ET_Kiosk> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_Kiosk et)
        {
            try {
                Kiosk obj = new Kiosk();
                obj.ViTri = et.ViTri;
                obj.TrangThai = et.TrangThai;
                db.GetTable<Kiosk>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_Kiosk et)
        {
            try {
                var obj = db.GetTable<Kiosk>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.ViTri = et.ViTri;
                    obj.TrangThai = et.TrangThai;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<Kiosk>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<Kiosk>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_Kiosk LayTheoId(int id)
        {
            return db.GetTable<Kiosk>().Where(x => x.Id == id).Select(s => new ET_Kiosk {
                Id = s.Id,
                ViTri = s.ViTri,
                TrangThai = s.TrangThai
            }).FirstOrDefault();
        }
    }
}

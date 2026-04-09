using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_GiaiDua
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_GiaiDua instance;
        public static DAL_GiaiDua Instance
        {
            get
            {
                if (instance == null) instance = new DAL_GiaiDua();
                return instance;
            }
        }

        public List<ET_GiaiDua> LoadDS()
        {
            return db.GetTable<GiaiDua>().Select(s => new ET_GiaiDua
            {
                Id = s.Id,
                TenGiai = s.TenGiai,
                ThoiGianBatDau = s.ThoiGianBatDau,
                ThoiGianKetThuc = s.ThoiGianKetThuc
            }).ToList();
        }

        public List<ET_GiaiDua> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_GiaiDua et)
        {
            try {
                GiaiDua obj = new GiaiDua();
                obj.TenGiai = et.TenGiai;
                obj.ThoiGianBatDau = et.ThoiGianBatDau;
                obj.ThoiGianKetThuc = et.ThoiGianKetThuc;
                db.GetTable<GiaiDua>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_GiaiDua et)
        {
            try {
                var obj = db.GetTable<GiaiDua>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.TenGiai = et.TenGiai;
                    obj.ThoiGianBatDau = et.ThoiGianBatDau;
                    obj.ThoiGianKetThuc = et.ThoiGianKetThuc;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<GiaiDua>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<GiaiDua>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_GiaiDua LayTheoId(int id)
        {
            return db.GetTable<GiaiDua>().Where(x => x.Id == id).Select(s => new ET_GiaiDua {
                Id = s.Id,
                TenGiai = s.TenGiai,
                ThoiGianBatDau = s.ThoiGianBatDau,
                ThoiGianKetThuc = s.ThoiGianKetThuc
            }).FirstOrDefault();
        }
    }
}

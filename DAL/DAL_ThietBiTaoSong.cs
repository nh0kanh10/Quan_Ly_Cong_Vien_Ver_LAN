using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_ThietBiTaoSong
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_ThietBiTaoSong instance;
        public static DAL_ThietBiTaoSong Instance
        {
            get
            {
                if (instance == null) instance = new DAL_ThietBiTaoSong();
                return instance;
            }
        }

        public List<ET_ThietBiTaoSong> LoadDS()
        {
            return db.GetTable<ThietBiTaoSong>().Select(s => new ET_ThietBiTaoSong
            {
                Id = s.Id,
                TenThietBi = s.TenThietBi,
                CongSuat = s.CongSuat,
                TrangThai = s.TrangThai
            }).ToList();
        }

        public List<ET_ThietBiTaoSong> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_ThietBiTaoSong et)
        {
            try {
                ThietBiTaoSong obj = new ThietBiTaoSong();
                obj.TenThietBi = et.TenThietBi;
                obj.CongSuat = et.CongSuat;
                obj.TrangThai = et.TrangThai;
                db.GetTable<ThietBiTaoSong>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_ThietBiTaoSong et)
        {
            try {
                var obj = db.GetTable<ThietBiTaoSong>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.TenThietBi = et.TenThietBi;
                    obj.CongSuat = et.CongSuat;
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
                var obj = db.GetTable<ThietBiTaoSong>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<ThietBiTaoSong>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_ThietBiTaoSong LayTheoId(int id)
        {
            return db.GetTable<ThietBiTaoSong>().Where(x => x.Id == id).Select(s => new ET_ThietBiTaoSong {
                Id = s.Id,
                TenThietBi = s.TenThietBi,
                CongSuat = s.CongSuat,
                TrangThai = s.TrangThai
            }).FirstOrDefault();
        }
    }
}

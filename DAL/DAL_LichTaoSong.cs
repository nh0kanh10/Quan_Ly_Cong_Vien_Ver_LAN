using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_LichTaoSong
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_LichTaoSong instance;
        public static DAL_LichTaoSong Instance
        {
            get
            {
                if (instance == null) instance = new DAL_LichTaoSong();
                return instance;
            }
        }

        public List<ET_LichTaoSong> LoadDS()
        {
            return db.GetTable<LichTaoSong>().Select(s => new ET_LichTaoSong
            {
                Id = s.Id,
                IdThietBi = s.IdThietBi,
                ThoiGianBatDau = s.ThoiGianBatDau,
                ThoiGianKetThuc = s.ThoiGianKetThuc,
                KieuSong = s.KieuSong
            }).ToList();
        }

        public List<ET_LichTaoSong> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_LichTaoSong et)
        {
            try {
                LichTaoSong obj = new LichTaoSong();
                obj.IdThietBi = et.IdThietBi;
                obj.ThoiGianBatDau = et.ThoiGianBatDau;
                obj.ThoiGianKetThuc = et.ThoiGianKetThuc;
                obj.KieuSong = et.KieuSong;
                db.GetTable<LichTaoSong>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_LichTaoSong et)
        {
            try {
                var obj = db.GetTable<LichTaoSong>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.IdThietBi = et.IdThietBi;
                    obj.ThoiGianBatDau = et.ThoiGianBatDau;
                    obj.ThoiGianKetThuc = et.ThoiGianKetThuc;
                    obj.KieuSong = et.KieuSong;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<LichTaoSong>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<LichTaoSong>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_LichTaoSong LayTheoId(int id)
        {
            return db.GetTable<LichTaoSong>().Where(x => x.Id == id).Select(s => new ET_LichTaoSong {
                Id = s.Id,
                IdThietBi = s.IdThietBi,
                ThoiGianBatDau = s.ThoiGianBatDau,
                ThoiGianKetThuc = s.ThoiGianKetThuc,
                KieuSong = s.KieuSong
            }).FirstOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_ChuongTrai
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_ChuongTrai instance;
        public static DAL_ChuongTrai Instance
        {
            get
            {
                if (instance == null) instance = new DAL_ChuongTrai();
                return instance;
            }
        }

        public List<ET_ChuongTrai> LoadDS()
        {
            return db.GetTable<ChuongTrai>().Select(s => new ET_ChuongTrai
            {
                Id = s.Id,
                IdKhuVucThu = s.IdKhuVucThu,
                IdDongVat = s.IdDongVat,
                TenChuong = s.TenChuong,
                SucChua = s.SucChua,
                TrangThai = s.TrangThai
            }).ToList();
        }

        public List<ET_ChuongTrai> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_ChuongTrai et)
        {
            try {
                ChuongTrai obj = new ChuongTrai();
                obj.IdKhuVucThu = et.IdKhuVucThu;
                obj.IdDongVat = et.IdDongVat;
                obj.TenChuong = et.TenChuong;
                obj.SucChua = et.SucChua;
                obj.TrangThai = et.TrangThai;
                db.GetTable<ChuongTrai>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_ChuongTrai et)
        {
            try {
                var obj = db.GetTable<ChuongTrai>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.IdKhuVucThu = et.IdKhuVucThu;
                    obj.IdDongVat = et.IdDongVat;
                    obj.TenChuong = et.TenChuong;
                    obj.SucChua = et.SucChua;
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
                var obj = db.GetTable<ChuongTrai>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<ChuongTrai>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_ChuongTrai LayTheoId(int id)
        {
            return db.GetTable<ChuongTrai>().Where(x => x.Id == id).Select(s => new ET_ChuongTrai {
                Id = s.Id,
                IdKhuVucThu = s.IdKhuVucThu,
                IdDongVat = s.IdDongVat,
                TenChuong = s.TenChuong,
                SucChua = s.SucChua,
                TrangThai = s.TrangThai
            }).FirstOrDefault();
        }
    }
}

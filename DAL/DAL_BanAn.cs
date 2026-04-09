using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_BanAn
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_BanAn instance;
        public static DAL_BanAn Instance
        {
            get
            {
                if (instance == null) instance = new DAL_BanAn();
                return instance;
            }
        }

        public List<ET_BanAn> LoadDS()
        {
            return db.GetTable<BanAn>().Select(s => new ET_BanAn
            {
                Id = s.Id,
                IdNhaHang = s.IdNhaHang,
                MaBan = s.MaBan,
                SucChua = s.SucChua,
                TrangThai = s.TrangThai,
                RowVer = s.RowVer == null ? null : s.RowVer.ToArray()
            }).ToList();
        }

        public List<ET_BanAn> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_BanAn et)
        {
            try {
                BanAn obj = new BanAn();
                obj.IdNhaHang = et.IdNhaHang;
                obj.MaBan = et.MaBan;
                obj.SucChua = et.SucChua;
                obj.TrangThai = et.TrangThai;
                obj.RowVer = et.RowVer;
                db.GetTable<BanAn>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_BanAn et)
        {
            try {
                var obj = db.GetTable<BanAn>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.IdNhaHang = et.IdNhaHang;
                    obj.MaBan = et.MaBan;
                    obj.SucChua = et.SucChua;
                    obj.TrangThai = et.TrangThai;
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
                var obj = db.GetTable<BanAn>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<BanAn>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_BanAn LayTheoId(int id)
        {
            return db.GetTable<BanAn>().Where(x => x.Id == id).Select(s => new ET_BanAn {
                Id = s.Id,
                IdNhaHang = s.IdNhaHang,
                MaBan = s.MaBan,
                SucChua = s.SucChua,
                TrangThai = s.TrangThai,
                RowVer = s.RowVer == null ? null : s.RowVer.ToArray()
            }).FirstOrDefault();
        }
    }
}

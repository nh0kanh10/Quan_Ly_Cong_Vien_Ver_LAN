using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_TheRFID
    {
        private static DAL_TheRFID instance;
        private DataQuanLyDaiNamDataContext CreateDb() { return new DataQuanLyDaiNamDataContext(); }
        public static DAL_TheRFID Instance
        {
            get
            {
                if (instance == null) instance = new DAL_TheRFID();
                return instance;
            }
        }

        public List<ET_TheRFID> LoadDS()
        {
            using (var db = CreateDb())
            {
                return db.GetTable<TheRFID>().Select(s => new ET_TheRFID
                {
                    MaRfid = s.MaRfid,
                    IdVi = s.IdVi,
                    TrangThai = s.TrangThai,
                    NgayKichHoat = s.NgayKichHoat,
                    NgayHuy = s.NgayHuy
                }).ToList();
            }
        }

        public List<ET_TheRFID> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_TheRFID et)
        {
            if (et == null || string.IsNullOrWhiteSpace(et.MaRfid)) return false;
            try {
                using (var db = CreateDb())
                {
                    TheRFID obj = new TheRFID();
                    obj.MaRfid = et.MaRfid;
                    obj.IdVi = et.IdVi;
                    obj.TrangThai = et.TrangThai;
                    obj.NgayKichHoat = et.NgayKichHoat;
                    obj.NgayHuy = et.NgayHuy;
                    db.GetTable<TheRFID>().InsertOnSubmit(obj);
                    db.SubmitChanges();
                }
                return true;
            } catch (Exception ex) { DalErrorLogger.Log("DAL_TheRFID.Them", ex); return false; }
        }

        public bool Sua(ET_TheRFID et)
        {
            if (et == null || string.IsNullOrWhiteSpace(et.MaRfid)) return false;
            try {
                using (var db = CreateDb())
                {
                    var obj = db.GetTable<TheRFID>().FirstOrDefault(x => x.MaRfid == et.MaRfid);
                    if (obj != null) {
                        obj.IdVi = et.IdVi;
                        obj.TrangThai = et.TrangThai;
                        obj.NgayKichHoat = et.NgayKichHoat;
                        obj.NgayHuy = et.NgayHuy;
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
            } catch (Exception ex) { DalErrorLogger.Log("DAL_TheRFID.Sua", ex); return false; }
        }

        public bool Xoa(string marfid)
        {
            if (string.IsNullOrWhiteSpace(marfid)) return false;
            try {
                using (var db = CreateDb())
                {
                    var obj = db.GetTable<TheRFID>().FirstOrDefault(x => x.MaRfid == marfid);
                    if (obj != null) {
                        db.GetTable<TheRFID>().DeleteOnSubmit(obj);
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
            } catch (Exception ex) { DalErrorLogger.Log("DAL_TheRFID.Xoa", ex); return false; }
        }

        public ET_TheRFID LayTheoId(string marfid)
        {
            if (string.IsNullOrWhiteSpace(marfid)) return null;
            using (var db = CreateDb())
            {
                return db.GetTable<TheRFID>().Where(x => x.MaRfid == marfid).Select(s => new ET_TheRFID {
                    MaRfid = s.MaRfid,
                    IdVi = s.IdVi,
                    TrangThai = s.TrangThai,
                    NgayKichHoat = s.NgayKichHoat,
                    NgayHuy = s.NgayHuy
                }).FirstOrDefault();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_NhaHang
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_NhaHang instance;
        public static DAL_NhaHang Instance
        {
            get
            {
                if (instance == null) instance = new DAL_NhaHang();
                return instance;
            }
        }

        public List<ET_NhaHang> LoadDS()
        {
            return db.GetTable<NhaHang>().Select(s => new ET_NhaHang
            {
                Id = s.Id,
                TenNhaHang = s.TenNhaHang,
                IdKhuVuc = s.IdKhuVuc,
                SucChua = s.SucChua,
                MoTa = s.MoTa
            }).ToList();
        }

        public List<ET_NhaHang> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_NhaHang et)
        {
            try {
                NhaHang obj = new NhaHang();
                obj.TenNhaHang = et.TenNhaHang;
                obj.IdKhuVuc = et.IdKhuVuc;
                obj.SucChua = et.SucChua;
                obj.MoTa = et.MoTa;
                db.GetTable<NhaHang>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_NhaHang et)
        {
            try {
                var obj = db.GetTable<NhaHang>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.TenNhaHang = et.TenNhaHang;
                    obj.IdKhuVuc = et.IdKhuVuc;
                    obj.SucChua = et.SucChua;
                    obj.MoTa = et.MoTa;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<NhaHang>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<NhaHang>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_NhaHang LayTheoId(int id)
        {
            return db.GetTable<NhaHang>().Where(x => x.Id == id).Select(s => new ET_NhaHang {
                Id = s.Id,
                TenNhaHang = s.TenNhaHang,
                IdKhuVuc = s.IdKhuVuc,
                SucChua = s.SucChua,
                MoTa = s.MoTa
            }).FirstOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_VeDoXeChiTiet
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_VeDoXeChiTiet instance;
        public static DAL_VeDoXeChiTiet Instance
        {
            get
            {
                if (instance == null) instance = new DAL_VeDoXeChiTiet();
                return instance;
            }
        }

        public List<ET_VeDoXeChiTiet> LoadDS()
        {
            return db.GetTable<VeDoXeChiTiet>().Select(s => new ET_VeDoXeChiTiet
            {
                Id = s.Id,
                IdChiTietDonHang = s.IdChiTietDonHang,
                IdLuotVaoRa = s.IdLuotVaoRa,
                TienPhaiTra = s.TienPhaiTra
            }).ToList();
        }

        public List<ET_VeDoXeChiTiet> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_VeDoXeChiTiet et)
        {
            try {
                VeDoXeChiTiet obj = new VeDoXeChiTiet();
                obj.IdChiTietDonHang = et.IdChiTietDonHang;
                obj.IdLuotVaoRa = et.IdLuotVaoRa;
                obj.TienPhaiTra = et.TienPhaiTra;
                db.GetTable<VeDoXeChiTiet>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_VeDoXeChiTiet et)
        {
            try {
                var obj = db.GetTable<VeDoXeChiTiet>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.IdChiTietDonHang = et.IdChiTietDonHang;
                    obj.IdLuotVaoRa = et.IdLuotVaoRa;
                    obj.TienPhaiTra = et.TienPhaiTra;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<VeDoXeChiTiet>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<VeDoXeChiTiet>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_VeDoXeChiTiet LayTheoId(int id)
        {
            return db.GetTable<VeDoXeChiTiet>().Where(x => x.Id == id).Select(s => new ET_VeDoXeChiTiet {
                Id = s.Id,
                IdChiTietDonHang = s.IdChiTietDonHang,
                IdLuotVaoRa = s.IdLuotVaoRa,
                TienPhaiTra = s.TienPhaiTra
            }).FirstOrDefault();
        }

        public List<ET_VeDoXeChiTiet> LoadByCTDH(int idCTDH)
        {
            return db.GetTable<VeDoXeChiTiet>().Where(x => x.IdChiTietDonHang == idCTDH).Select(s => new ET_VeDoXeChiTiet {
                Id = s.Id,
                IdChiTietDonHang = s.IdChiTietDonHang,
                IdLuotVaoRa = s.IdLuotVaoRa,
                TienPhaiTra = s.TienPhaiTra
            }).ToList();
        }
    }
}

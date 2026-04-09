using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_ComboChiTiet
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_ComboChiTiet instance;
        public static DAL_ComboChiTiet Instance
        {
            get
            {
                if (instance == null) instance = new DAL_ComboChiTiet();
                return instance;
            }
        }

        public List<ET_ComboChiTiet> LoadDS()
        {
            return db.GetTable<ComboChiTiet>().Select(s => new ET_ComboChiTiet
            {
                Id = s.Id,
                IdCombo = s.IdCombo,
                IdSanPham = s.IdSanPham,
                SoLuong = s.SoLuong,
                TyLePhanBo = s.TyLePhanBo
            }).ToList();
        }

        public List<ET_ComboChiTiet> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_ComboChiTiet et)
        {
            try {
                ComboChiTiet obj = new ComboChiTiet();
                obj.IdCombo = et.IdCombo;
                obj.IdSanPham = et.IdSanPham;
                obj.SoLuong = et.SoLuong;
                obj.TyLePhanBo = et.TyLePhanBo;
                db.GetTable<ComboChiTiet>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_ComboChiTiet et)
        {
            try {
                var obj = db.GetTable<ComboChiTiet>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.IdCombo = et.IdCombo;
                    obj.IdSanPham = et.IdSanPham;
                    obj.SoLuong = et.SoLuong;
                    obj.TyLePhanBo = et.TyLePhanBo;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<ComboChiTiet>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<ComboChiTiet>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_ComboChiTiet LayTheoId(int id)
        {
            return db.GetTable<ComboChiTiet>().Where(x => x.Id == id).Select(s => new ET_ComboChiTiet {
                Id = s.Id,
                IdCombo = s.IdCombo,
                IdSanPham = s.IdSanPham,
                SoLuong = s.SoLuong,
                TyLePhanBo = s.TyLePhanBo
            }).FirstOrDefault();
        }

        public bool XoaTheoCombo(int idCombo)
        {
            try
            {
                var items = db.GetTable<ComboChiTiet>().Where(x => x.IdCombo == idCombo);
                db.GetTable<ComboChiTiet>().DeleteAllOnSubmit(items);
                db.SubmitChanges();
                return true;
            }
            catch { return false; }
        }
    }
}

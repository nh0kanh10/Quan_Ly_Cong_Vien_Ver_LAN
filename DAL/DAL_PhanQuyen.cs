using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_PhanQuyen
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_PhanQuyen instance;
        public static DAL_PhanQuyen Instance
        {
            get
            {
                if (instance == null) instance = new DAL_PhanQuyen();
                return instance;
            }
        }

        public List<ET_PhanQuyen> LoadDS()
        {
            return db.GetTable<PhanQuyen>().Select(s => new ET_PhanQuyen
            {
                IdVaiTro = s.IdVaiTro,
                IdQuyen = s.IdQuyen
            }).ToList();
        }

        public List<ET_PhanQuyen> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_PhanQuyen et)
        {
            try {
                PhanQuyen obj = new PhanQuyen();
                obj.IdVaiTro = et.IdVaiTro;
                obj.IdQuyen = et.IdQuyen;
                db.GetTable<PhanQuyen>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool XoaTheoVaiTro(int idVaiTro)
        {
            try {
                var list = db.GetTable<PhanQuyen>().Where(x => x.IdVaiTro == idVaiTro).ToList();
                db.GetTable<PhanQuyen>().DeleteAllOnSubmit(list);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_VaiTro
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_VaiTro instance;
        public static DAL_VaiTro Instance
        {
            get
            {
                if (instance == null) instance = new DAL_VaiTro();
                return instance;
            }
        }

        public List<ET_VaiTro> LoadDS()
        {
            return db.GetTable<VaiTro>().Select(s => new ET_VaiTro
            {
                Id = s.Id,
                TenVaiTro = s.TenVaiTro
            }).ToList();
        }

        public List<ET_VaiTro> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_VaiTro et)
        {
            try {
                VaiTro obj = new VaiTro();
                obj.TenVaiTro = et.TenVaiTro;
                db.GetTable<VaiTro>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_VaiTro et)
        {
            try {
                var obj = db.GetTable<VaiTro>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.TenVaiTro = et.TenVaiTro;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<VaiTro>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<VaiTro>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_VaiTro LayTheoId(int id)
        {
            return db.GetTable<VaiTro>().Where(x => x.Id == id).Select(s => new ET_VaiTro {
                Id = s.Id,
                TenVaiTro = s.TenVaiTro
            }).FirstOrDefault();
        }
    }
}

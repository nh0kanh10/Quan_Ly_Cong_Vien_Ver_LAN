using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_DatPhongChiTiet
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_DatPhongChiTiet instance;
        public static DAL_DatPhongChiTiet Instance
        {
            get
            {
                if (instance == null) instance = new DAL_DatPhongChiTiet();
                return instance;
            }
        }

        public List<ET_DatPhongChiTiet> LoadDS()
        {
            return db.GetTable<DatPhongChiTiet>().Select(s => new ET_DatPhongChiTiet
            {
                Id = s.Id,
                IdChiTietDonHang = s.IdChiTietDonHang,
                NgayNhan = s.NgayNhan,
                NgayTra = s.NgayTra,
                TrangThai = s.TrangThai
            }).ToList();
        }

        public List<ET_DatPhongChiTiet> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_DatPhongChiTiet et)
        {
            try {
                DatPhongChiTiet obj = new DatPhongChiTiet();
                obj.IdChiTietDonHang = (int)et.IdChiTietDonHang;
                obj.NgayNhan = et.NgayNhan;
                obj.NgayTra = et.NgayTra;
                obj.TrangThai = et.TrangThai;
                db.GetTable<DatPhongChiTiet>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public int ThemVaLayId(ET_DatPhongChiTiet et)
        {
            try
            {
                DatPhongChiTiet obj = new DatPhongChiTiet();
                obj.IdChiTietDonHang = (int)et.IdChiTietDonHang;
                obj.NgayNhan = et.NgayNhan;
                obj.NgayTra = et.NgayTra;
                obj.TrangThai = et.TrangThai;
                db.GetTable<DatPhongChiTiet>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return obj.Id;
            }
            catch { return 0; }
        }

        public bool Sua(ET_DatPhongChiTiet et)
        {
            try {
                var obj = db.GetTable<DatPhongChiTiet>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.IdChiTietDonHang = (int)et.IdChiTietDonHang;
                    obj.NgayNhan = et.NgayNhan;
                    obj.NgayTra = et.NgayTra;
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
                var obj = db.GetTable<DatPhongChiTiet>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<DatPhongChiTiet>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_DatPhongChiTiet LayTheoId(int id)
        {
            return db.GetTable<DatPhongChiTiet>().Where(x => x.Id == id).Select(s => new ET_DatPhongChiTiet {
                Id = s.Id,
                IdChiTietDonHang = s.IdChiTietDonHang,
                NgayNhan = s.NgayNhan,
                NgayTra = s.NgayTra,
                TrangThai = s.TrangThai
            }).FirstOrDefault();
        }

        public List<ET_DatPhongChiTiet> LoadByCTDH(int idCTDH)
        {
            return db.GetTable<DatPhongChiTiet>().Where(x => x.IdChiTietDonHang == idCTDH).Select(s => new ET_DatPhongChiTiet {
                Id = s.Id,
                IdChiTietDonHang = s.IdChiTietDonHang,
                NgayNhan = s.NgayNhan,
                NgayTra = s.NgayTra,
                TrangThai = s.TrangThai
            }).ToList();
        }
    }
}

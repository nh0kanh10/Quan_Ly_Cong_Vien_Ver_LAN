using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_DatChoThuAn
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_DatChoThuAn instance;
        public static DAL_DatChoThuAn Instance
        {
            get
            {
                if (instance == null) instance = new DAL_DatChoThuAn();
                return instance;
            }
        }

        public List<ET_DatChoThuAn> LoadDS()
        {
            return db.GetTable<DatChoThuAn>().Select(s => new ET_DatChoThuAn
            {
                Id = s.Id,
                IdChiTietDonHang = s.IdChiTietDonHang,
                IdDongVat = s.IdDongVat,
                IdVeDienTu = s.IdVeDienTu,
                ThoiGianDuKien = s.ThoiGianDuKien,
                TrangThai = s.TrangThai
            }).ToList();
        }

        public List<ET_DatChoThuAn> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_DatChoThuAn et)
        {
            try {
                DatChoThuAn obj = new DatChoThuAn();
                obj.IdChiTietDonHang = et.IdChiTietDonHang;
                obj.IdDongVat = et.IdDongVat;
                obj.IdVeDienTu = et.IdVeDienTu;
                obj.ThoiGianDuKien = et.ThoiGianDuKien;
                obj.TrangThai = et.TrangThai;
                db.GetTable<DatChoThuAn>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_DatChoThuAn et)
        {
            try {
                var obj = db.GetTable<DatChoThuAn>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.IdChiTietDonHang = et.IdChiTietDonHang;
                    obj.IdDongVat = et.IdDongVat;
                    obj.IdVeDienTu = et.IdVeDienTu;
                    obj.ThoiGianDuKien = et.ThoiGianDuKien;
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
                var obj = db.GetTable<DatChoThuAn>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<DatChoThuAn>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_DatChoThuAn LayTheoId(int id)
        {
            return db.GetTable<DatChoThuAn>().Where(x => x.Id == id).Select(s => new ET_DatChoThuAn {
                Id = s.Id,
                IdChiTietDonHang = s.IdChiTietDonHang,
                IdDongVat = s.IdDongVat,
                IdVeDienTu = s.IdVeDienTu,
                ThoiGianDuKien = s.ThoiGianDuKien,
                TrangThai = s.TrangThai
            }).FirstOrDefault();
        }

        public List<ET_DatChoThuAn> LoadByCTDH(int idCTDH)
        {
            return db.GetTable<DatChoThuAn>().Where(x => x.IdChiTietDonHang == idCTDH).Select(s => new ET_DatChoThuAn {
                Id = s.Id,
                IdChiTietDonHang = s.IdChiTietDonHang,
                IdDongVat = s.IdDongVat,
                IdVeDienTu = s.IdVeDienTu,
                ThoiGianDuKien = s.ThoiGianDuKien,
                TrangThai = s.TrangThai
            }).ToList();
        }
    }
}

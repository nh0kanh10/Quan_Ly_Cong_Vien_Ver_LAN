using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_VeDienTu
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_VeDienTu instance;
        public static DAL_VeDienTu Instance
        {
            get
            {
                if (instance == null) instance = new DAL_VeDienTu();
                return instance;
            }
        }

        public List<ET_VeDienTu> LoadDS()
        {
            return db.GetTable<VeDienTu>().Select(s => new ET_VeDienTu
            {
                Id = s.Id,
                MaCode = s.MaCode,
                IdChiTietDonHang = s.IdChiTietDonHang,
                IdSanPham = s.IdSanPham,
                TrangThai = s.TrangThai,
                ThoiGianQuet = s.ThoiGianQuet,
                IdKhachHangSuDung = s.IdKhachHangSuDung,
                SoLuotConLai = s.SoLuotConLai
                // CreatedAt = s.CreatedAt,
                // CreatedBy = s.CreatedBy
            }).ToList();
        }

        public List<ET_VeDienTu> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_VeDienTu et)
        {
            if (et == null) return false;
            try {
                VeDienTu obj = new VeDienTu();
                obj.Id = et.Id;
                obj.MaCode = et.MaCode;
                obj.IdChiTietDonHang = et.IdChiTietDonHang;
                obj.IdSanPham = et.IdSanPham ?? -1;
                obj.TrangThai = et.TrangThai;
                obj.ThoiGianQuet = et.ThoiGianQuet;
                obj.IdKhachHangSuDung = et.IdKhachHangSuDung;
                obj.SoLuotConLai = et.SoLuotConLai;
                // obj.CreatedAt = et.CreatedAt ?? DateTime.Now;
                // obj.CreatedBy = et.CreatedBy;
                db.GetTable<VeDienTu>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch (Exception ex) { DalErrorLogger.Log("DAL_VeDienTu.Them", ex); return false; }
        }

        public bool Sua(ET_VeDienTu et)
        {
            if (et == null) return false;
            try {
                var obj = db.GetTable<VeDienTu>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.MaCode = et.MaCode;
                    obj.IdChiTietDonHang = et.IdChiTietDonHang;
                    obj.IdSanPham = et.IdSanPham ?? -1;
                    obj.TrangThai = et.TrangThai;
                    obj.ThoiGianQuet = et.ThoiGianQuet;
                    obj.IdKhachHangSuDung = et.IdKhachHangSuDung;
                    obj.SoLuotConLai = et.SoLuotConLai;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch (Exception ex) { DalErrorLogger.Log("DAL_VeDienTu.Sua", ex); return false; }
        }

        public bool Xoa(Guid id)
        {
            try {
                var obj = db.GetTable<VeDienTu>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<VeDienTu>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_VeDienTu LayTheoId(Guid id)
        {
            return db.GetTable<VeDienTu>().Where(x => x.Id == id).Select(s => new ET_VeDienTu {
                Id = s.Id,
                MaCode = s.MaCode,
                IdChiTietDonHang = s.IdChiTietDonHang,
                IdSanPham = s.IdSanPham,
                TrangThai = s.TrangThai,
                ThoiGianQuet = s.ThoiGianQuet,
                IdKhachHangSuDung = s.IdKhachHangSuDung,
                SoLuotConLai = s.SoLuotConLai
                // CreatedAt = s.CreatedAt,
                // CreatedBy = s.CreatedBy
            }).FirstOrDefault();
        }

        public ET_VeDienTu LayTheoMaCode(string code)
        {
            return db.GetTable<VeDienTu>().Where(x => x.MaCode == code).Select(s => new ET_VeDienTu {
                Id = s.Id,
                MaCode = s.MaCode,
                IdChiTietDonHang = s.IdChiTietDonHang,
                IdSanPham = s.IdSanPham,
                TrangThai = s.TrangThai,
                ThoiGianQuet = s.ThoiGianQuet,
                IdKhachHangSuDung = s.IdKhachHangSuDung,
                SoLuotConLai = s.SoLuotConLai
                // CreatedAt = s.CreatedAt,
                // CreatedBy = s.CreatedBy
            }).FirstOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_ChiTietDonHang
    {
        private static DAL_ChiTietDonHang instance;
        private DataQuanLyDaiNamDataContext CreateDb() { return new DataQuanLyDaiNamDataContext(); }
        public static DAL_ChiTietDonHang Instance
        {
            get
            {
                if (instance == null) instance = new DAL_ChiTietDonHang();
                return instance;
            }
        }

        public List<ET_ChiTietDonHang> LoadDS()
        {
            using (var db = CreateDb())
            {
                return db.GetTable<ChiTietDonHang>().Select(s => new ET_ChiTietDonHang
                {
                    Id = s.Id,
                    IdDonHang = s.IdDonHang,
                    IdSanPham = s.IdSanPham,
                    IdCombo = s.IdCombo,
                    SoLuong = s.SoLuong,
                    DonGiaGoc = s.DonGiaGoc,
                    TienGiamGiaDong = s.TienGiamGiaDong,
                    DonGiaThucTe = s.DonGiaThucTe
                }).ToList();
            }
        }

        public List<ET_ChiTietDonHang> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_ChiTietDonHang et)
        {
            if (et == null) return false;
            try {
                using (var db = CreateDb())
                {
                    ChiTietDonHang obj = new ChiTietDonHang();
                    obj.IdDonHang = et.IdDonHang;
                    obj.IdSanPham = et.IdSanPham;
                    obj.IdCombo = et.IdCombo;
                    obj.SoLuong = et.SoLuong;
                    obj.DonGiaGoc = et.DonGiaGoc;
                    obj.TienGiamGiaDong = et.TienGiamGiaDong;
                    obj.DonGiaThucTe = et.DonGiaThucTe;
                    db.GetTable<ChiTietDonHang>().InsertOnSubmit(obj);
                    db.SubmitChanges();
                    et.Id = obj.Id; 
                }
                return true;
            } catch (Exception ex) { DalErrorLogger.Log("DAL_ChiTietDonHang.Them", ex); return false; }
        }

        public bool Sua(ET_ChiTietDonHang et)
        {
            if (et == null) return false;
            try {
                using (var db = CreateDb())
                {
                    var obj = db.GetTable<ChiTietDonHang>().FirstOrDefault(x => x.Id == et.Id);
                    if (obj != null) {
                        obj.IdDonHang = et.IdDonHang;
                        obj.IdSanPham = et.IdSanPham;
                        obj.IdCombo = et.IdCombo;
                        obj.SoLuong = et.SoLuong;
                        obj.DonGiaGoc = et.DonGiaGoc;
                        obj.TienGiamGiaDong = et.TienGiamGiaDong;
                        obj.DonGiaThucTe = et.DonGiaThucTe;
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
            } catch (Exception ex) { DalErrorLogger.Log("DAL_ChiTietDonHang.Sua", ex); return false; }
        }

        public bool Xoa(int id)
        {
            try {
                using (var db = CreateDb())
                {
                    var obj = db.GetTable<ChiTietDonHang>().FirstOrDefault(x => x.Id == id);
                    if (obj != null) {
                        db.GetTable<ChiTietDonHang>().DeleteOnSubmit(obj);
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
            } catch (Exception ex) { DalErrorLogger.Log("DAL_ChiTietDonHang.Xoa", ex); return false; }
        }

        public ET_ChiTietDonHang LayTheoId(int id)
        {
            using (var db = CreateDb())
            {
                return db.GetTable<ChiTietDonHang>().Where(x => x.Id == id).Select(s => new ET_ChiTietDonHang {
                    Id = s.Id,
                    IdDonHang = s.IdDonHang,
                    IdSanPham = s.IdSanPham,
                    IdCombo = s.IdCombo,
                    SoLuong = s.SoLuong,
                    DonGiaGoc = s.DonGiaGoc,
                    TienGiamGiaDong = s.TienGiamGiaDong,
                    DonGiaThucTe = s.DonGiaThucTe
                }).FirstOrDefault();
            }
        }

        public List<ET_ChiTietDonHang> LoadByDonHang(int idDonHang)
        {
            using (var db = CreateDb())
            {
                return db.GetTable<ChiTietDonHang>().Where(x => x.IdDonHang == idDonHang).Select(s => new ET_ChiTietDonHang {
                    Id = s.Id,
                    IdDonHang = s.IdDonHang,
                    IdSanPham = s.IdSanPham,
                    IdCombo = s.IdCombo,
                    SoLuong = s.SoLuong,
                    DonGiaGoc = s.DonGiaGoc,
                    TienGiamGiaDong = s.TienGiamGiaDong,
                    DonGiaThucTe = s.DonGiaThucTe
                }).ToList();
            }
        }

        public int ThemVaLayId(ET_ChiTietDonHang et)
        {
            if (et == null) return 0;
            try {
                using (var db = CreateDb())
                {
                    ChiTietDonHang obj = new ChiTietDonHang();
                    obj.IdDonHang = et.IdDonHang;
                    obj.IdSanPham = et.IdSanPham;
                    obj.IdCombo = et.IdCombo;
                    obj.SoLuong = et.SoLuong;
                    obj.DonGiaGoc = et.DonGiaGoc;
                    obj.TienGiamGiaDong = et.TienGiamGiaDong;
                    obj.DonGiaThucTe = et.DonGiaThucTe;
                    db.GetTable<ChiTietDonHang>().InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.Id;
                }
            } catch (Exception ex) { DalErrorLogger.Log("DAL_ChiTietDonHang.ThemVaLayId", ex); return 0; }
        }
    }
}

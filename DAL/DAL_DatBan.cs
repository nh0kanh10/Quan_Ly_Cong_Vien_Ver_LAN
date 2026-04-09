using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_DatBan
    {
        private static DAL_DatBan instance;
        private DataQuanLyDaiNamDataContext CreateDb() { return new DataQuanLyDaiNamDataContext(); }
        public static DAL_DatBan Instance
        {
            get
            {
                if (instance == null) instance = new DAL_DatBan();
                return instance;
            }
        }

        public List<ET_DatBan> LoadDS()
        {
            using (var db = CreateDb())
            {
                return db.GetTable<DatBan>().Select(s => new ET_DatBan
                {
                    Id = s.Id,
                    IdChiTietDonHang = s.IdChiTietDonHang,
                    IdNhaHang = s.IdNhaHang,
                    ThoiGianDat = s.ThoiGianDat,
                    ThoiGianDenDuKien = s.ThoiGianDenDuKien,
                    SoLuongKhach = s.SoLuongKhach,
                    TrangThai = s.TrangThai,
                    TenNguoiDat = s.TenNguoiDat,
                    SoDienThoai = s.SoDienThoai,
                    GhiChu = s.GhiChu,
                    TienCoc = s.TienCoc,
                    IdPhieuThuCoc = s.IdPhieuThuCoc,
                    IdKhachHang = s.IdKhachHang
                }).ToList();
            }
        }

        public List<ET_DatBan> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_DatBan et)
        {
            try
            {
                using (var db = CreateDb())
                {
                    DatBan obj = new DatBan();
                    obj.IdChiTietDonHang = et.IdChiTietDonHang;
                    obj.IdNhaHang = et.IdNhaHang;
                    obj.ThoiGianDat = et.ThoiGianDat;
                    obj.ThoiGianDenDuKien = et.ThoiGianDenDuKien;
                    obj.SoLuongKhach = et.SoLuongKhach;
                    obj.TrangThai = et.TrangThai;
                    obj.TenNguoiDat = et.TenNguoiDat;
                    obj.SoDienThoai = et.SoDienThoai;
                    obj.GhiChu = et.GhiChu;
                    obj.TienCoc = et.TienCoc;
                    obj.IdPhieuThuCoc = et.IdPhieuThuCoc;
                    obj.IdKhachHang = et.IdKhachHang;
                    db.GetTable<DatBan>().InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex) { DalErrorLogger.Log("DAL_DatBan.Them", ex); return false; }
        }






        public int ThemVaLayId(ET_DatBan et)
        {
            try
            {
                using (var db = CreateDb())
                {
                    DatBan obj = new DatBan();
                    obj.IdChiTietDonHang = et.IdChiTietDonHang;
                    obj.IdNhaHang = et.IdNhaHang;
                    obj.ThoiGianDat = et.ThoiGianDat;
                    obj.ThoiGianDenDuKien = et.ThoiGianDenDuKien;
                    obj.SoLuongKhach = et.SoLuongKhach;
                    obj.TrangThai = et.TrangThai;
                    obj.TenNguoiDat = et.TenNguoiDat;
                    obj.SoDienThoai = et.SoDienThoai;
                    obj.GhiChu = et.GhiChu;
                    obj.TienCoc = et.TienCoc;
                    obj.IdPhieuThuCoc = et.IdPhieuThuCoc;
                    obj.IdKhachHang = et.IdKhachHang;
                    db.GetTable<DatBan>().InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.Id;
                }
            }
            catch (Exception ex) { DalErrorLogger.Log("DAL_DatBan.ThemVaLayId", ex); return 0; }
        }

        public bool Sua(ET_DatBan et)
        {
            try
            {
                using (var db = CreateDb())
                {
                    var obj = db.GetTable<DatBan>().FirstOrDefault(x => x.Id == et.Id);
                    if (obj != null)
                    {
                        obj.IdChiTietDonHang = et.IdChiTietDonHang;
                        obj.IdNhaHang = et.IdNhaHang;
                        obj.ThoiGianDat = et.ThoiGianDat;
                        obj.ThoiGianDenDuKien = et.ThoiGianDenDuKien;
                        obj.SoLuongKhach = et.SoLuongKhach;
                        obj.TrangThai = et.TrangThai;
                        obj.TenNguoiDat = et.TenNguoiDat;
                        obj.SoDienThoai = et.SoDienThoai;
                        obj.GhiChu = et.GhiChu;
                        obj.TienCoc = et.TienCoc;
                        obj.IdPhieuThuCoc = et.IdPhieuThuCoc;
                        obj.IdKhachHang = et.IdKhachHang;
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex) { DalErrorLogger.Log("DAL_DatBan.Sua", ex); return false; }
        }

        public bool Xoa(int id)
        {
            try
            {
                using (var db = CreateDb())
                {
                    var obj = db.GetTable<DatBan>().FirstOrDefault(x => x.Id == id);
                    if (obj != null)
                    {
                        db.GetTable<DatBan>().DeleteOnSubmit(obj);
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex) { DalErrorLogger.Log("DAL_DatBan.Xoa", ex); return false; }
        }

        public ET_DatBan LayTheoId(int id)
        {
            using (var db = CreateDb())
            {
                return db.GetTable<DatBan>().Where(x => x.Id == id).Select(s => new ET_DatBan
                {
                    Id = s.Id,
                    IdChiTietDonHang = s.IdChiTietDonHang,
                    IdNhaHang = s.IdNhaHang,
                    ThoiGianDat = s.ThoiGianDat,
                    ThoiGianDenDuKien = s.ThoiGianDenDuKien,
                    SoLuongKhach = s.SoLuongKhach,
                    TrangThai = s.TrangThai,
                    TenNguoiDat = s.TenNguoiDat,
                    SoDienThoai = s.SoDienThoai,
                    GhiChu = s.GhiChu,
                    TienCoc = s.TienCoc,
                    IdPhieuThuCoc = s.IdPhieuThuCoc,
                    IdKhachHang = s.IdKhachHang
                }).FirstOrDefault();
            }
        }

        public List<ET_DatBan> LoadByCTDH(int idCTDH)
        {
            using (var db = CreateDb())
            {
                return db.GetTable<DatBan>().Where(x => x.IdChiTietDonHang == idCTDH).Select(s => new ET_DatBan
                {
                    Id = s.Id,
                    IdChiTietDonHang = s.IdChiTietDonHang,
                    IdNhaHang = s.IdNhaHang,
                    ThoiGianDat = s.ThoiGianDat,
                    ThoiGianDenDuKien = s.ThoiGianDenDuKien,
                    SoLuongKhach = s.SoLuongKhach,
                    TrangThai = s.TrangThai,
                    TenNguoiDat = s.TenNguoiDat,
                    SoDienThoai = s.SoDienThoai,
                    GhiChu = s.GhiChu,
                    TienCoc = s.TienCoc,
                    IdPhieuThuCoc = s.IdPhieuThuCoc,
                    IdKhachHang = s.IdKhachHang
                }).ToList();
            }
        }
    }
}

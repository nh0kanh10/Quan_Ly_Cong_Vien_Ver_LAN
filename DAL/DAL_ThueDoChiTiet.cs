using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_ThueDoChiTiet
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_ThueDoChiTiet instance;
        public static DAL_ThueDoChiTiet Instance
        {
            get
            {
                if (instance == null) instance = new DAL_ThueDoChiTiet();
                return instance;
            }
        }

        public List<ET_ThueDoChiTiet> LoadDS()
        {
            return db.GetTable<ThueDoChiTiet>().Select(s => new ET_ThueDoChiTiet
            {
                Id = s.Id,
                IdChiTietDonHang = s.IdChiTietDonHang,
                IdSanPham = s.IdSanPham,
                SoLuong = s.SoLuong,
                ThoiGianBatDau = s.ThoiGianBatDau,
                ThoiGianKetThuc = s.ThoiGianKetThuc,
                SoTienCoc = s.SoTienCoc,
                TrangThaiCoc = s.TrangThaiCoc,
                IdGiaoDichCoc = s.IdGiaoDichCoc,
                IdGiaoDichHoanCoc = s.IdGiaoDichHoanCoc,
                IdGiaoDichPhat = s.IdGiaoDichPhat,
                TienThueDaThu = s.TienThueDaThu
            }).ToList();
        }

        public List<ET_ThueDoChiTiet> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_ThueDoChiTiet et)
        {
            try {
                ThueDoChiTiet obj = new ThueDoChiTiet();
                obj.IdChiTietDonHang = et.IdChiTietDonHang;
                obj.IdSanPham = et.IdSanPham;
                obj.SoLuong = et.SoLuong;
                obj.ThoiGianBatDau = et.ThoiGianBatDau;
                obj.ThoiGianKetThuc = et.ThoiGianKetThuc;
                obj.SoTienCoc = et.SoTienCoc;
                obj.TrangThaiCoc = et.TrangThaiCoc;
                obj.IdGiaoDichCoc = et.IdGiaoDichCoc;
                obj.IdGiaoDichHoanCoc = et.IdGiaoDichHoanCoc;
                obj.IdGiaoDichPhat = et.IdGiaoDichPhat;
                obj.TienThueDaThu = et.TienThueDaThu;
                db.GetTable<ThueDoChiTiet>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public int ThemVaLayId(ET_ThueDoChiTiet et)
        {
            try
            {
                ThueDoChiTiet obj = new ThueDoChiTiet();
                obj.IdChiTietDonHang = et.IdChiTietDonHang;
                obj.IdSanPham = et.IdSanPham;
                obj.SoLuong = et.SoLuong;
                obj.ThoiGianBatDau = et.ThoiGianBatDau;
                obj.ThoiGianKetThuc = et.ThoiGianKetThuc;
                obj.SoTienCoc = et.SoTienCoc;
                obj.TrangThaiCoc = et.TrangThaiCoc;
                obj.IdGiaoDichCoc = et.IdGiaoDichCoc;
                obj.IdGiaoDichHoanCoc = et.IdGiaoDichHoanCoc;
                obj.IdGiaoDichPhat = et.IdGiaoDichPhat;
                obj.TienThueDaThu = et.TienThueDaThu;
                db.GetTable<ThueDoChiTiet>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return obj.Id;
            }
            catch { return 0; }
        }

        public bool Sua(ET_ThueDoChiTiet et)
        {
            try {
                var obj = db.GetTable<ThueDoChiTiet>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.IdChiTietDonHang = et.IdChiTietDonHang;
                    obj.IdSanPham = et.IdSanPham;
                    obj.SoLuong = et.SoLuong;
                    obj.ThoiGianBatDau = et.ThoiGianBatDau;
                    obj.ThoiGianKetThuc = et.ThoiGianKetThuc;
                    obj.SoTienCoc = et.SoTienCoc;
                    obj.TrangThaiCoc = et.TrangThaiCoc;
                    obj.IdGiaoDichCoc = et.IdGiaoDichCoc;
                    obj.IdGiaoDichHoanCoc = et.IdGiaoDichHoanCoc;
                    obj.IdGiaoDichPhat = et.IdGiaoDichPhat;
                    obj.TienThueDaThu = et.TienThueDaThu;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<ThueDoChiTiet>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<ThueDoChiTiet>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_ThueDoChiTiet LayTheoId(int id)
        {
            return db.GetTable<ThueDoChiTiet>().Where(x => x.Id == id).Select(s => new ET_ThueDoChiTiet {
                Id = s.Id,
                IdChiTietDonHang = s.IdChiTietDonHang,
                IdSanPham = s.IdSanPham,
                SoLuong = s.SoLuong,
                ThoiGianBatDau = s.ThoiGianBatDau,
                ThoiGianKetThuc = s.ThoiGianKetThuc,
                SoTienCoc = s.SoTienCoc,
                TrangThaiCoc = s.TrangThaiCoc,
                IdGiaoDichCoc = s.IdGiaoDichCoc,
                IdGiaoDichHoanCoc = s.IdGiaoDichHoanCoc,
                IdGiaoDichPhat = s.IdGiaoDichPhat,
                TienThueDaThu = s.TienThueDaThu
            }).FirstOrDefault();
        }

        /// <summary>
        /// Lấy tất cả ThueDoChiTiet theo IdChiTietDonHang.
        /// </summary>
        public List<ET_ThueDoChiTiet> LoadByCTDH(int idCTDH)
        {
            return db.GetTable<ThueDoChiTiet>().Where(x => x.IdChiTietDonHang == idCTDH).Select(s => new ET_ThueDoChiTiet
            {
                Id = s.Id,
                IdChiTietDonHang = s.IdChiTietDonHang,
                IdSanPham = s.IdSanPham,
                SoLuong = s.SoLuong,
                ThoiGianBatDau = s.ThoiGianBatDau,
                ThoiGianKetThuc = s.ThoiGianKetThuc,
                SoTienCoc = s.SoTienCoc,
                TrangThaiCoc = s.TrangThaiCoc,
                IdGiaoDichCoc = s.IdGiaoDichCoc,
                IdGiaoDichHoanCoc = s.IdGiaoDichHoanCoc,
                IdGiaoDichPhat = s.IdGiaoDichPhat,
                TienThueDaThu = s.TienThueDaThu
            }).ToList();
        }

        /// <summary>
        /// Lấy ThueDoChiTiet "ChuaHoan" theo nhiều IdChiTietDonHang 
        /// </summary>
        public List<ET_ThueDoChiTiet> LoadChuaHoanByCTDHs(List<int> ctdhIds)
        {
            return db.GetTable<ThueDoChiTiet>()
                .Where(x => ctdhIds.Contains(x.IdChiTietDonHang) && x.TrangThaiCoc == "ChuaHoan")
                .Select(s => new ET_ThueDoChiTiet
            {
                Id = s.Id,
                IdChiTietDonHang = s.IdChiTietDonHang,
                IdSanPham = s.IdSanPham,
                SoLuong = s.SoLuong,
                ThoiGianBatDau = s.ThoiGianBatDau,
                ThoiGianKetThuc = s.ThoiGianKetThuc,
                SoTienCoc = s.SoTienCoc,
                TrangThaiCoc = s.TrangThaiCoc,
                IdGiaoDichCoc = s.IdGiaoDichCoc,
                IdGiaoDichHoanCoc = s.IdGiaoDichHoanCoc,
                IdGiaoDichPhat = s.IdGiaoDichPhat,
                TienThueDaThu = s.TienThueDaThu
            }).ToList();
        }

        /// <summary>
        /// Lấy danh sách thống kê đồ chưa trả có filter ngày bắt đầu
        /// </summary>
        public List<ET_DanhSachChuaTraView> LoadDanhSachChuaTra(DateTime tuNgay, DateTime denNgay)
        {
            var q = from td in db.GetTable<ThueDoChiTiet>()
                    where td.TrangThaiCoc == "ChuaHoan"
                       && td.ThoiGianBatDau >= tuNgay.Date
                       && td.ThoiGianBatDau < denNgay.Date.AddDays(1)
                    join ct in db.GetTable<ChiTietDonHang>() on td.IdChiTietDonHang equals ct.Id
                    join dh in db.GetTable<DonHang>() on ct.IdDonHang equals dh.Id
                    join sp in db.GetTable<SanPham>() on td.IdSanPham equals sp.Id
                    join kh in db.GetTable<KhachHang>() on dh.IdKhachHang equals kh.Id into KhachHangGroup
                    from khg in KhachHangGroup.DefaultIfEmpty()
                    orderby td.ThoiGianBatDau descending
                    select new ET_DanhSachChuaTraView
                    {
                        IdThueDo = td.Id,
                        MaCode = dh.MaCode,
                        TenKhachHang = khg != null ? khg.HoTen : "Khách vãng lai",
                        HeaderNhom = dh.MaCode + " - " + (khg != null ? khg.HoTen : "Khách vãng lai"),
                        TenSanPham = sp.Ten,
                        SoLuong = td.SoLuong,
                        ThoiGianThue = td.ThoiGianBatDau
                    };
            return q.ToList();
        }
    }
}

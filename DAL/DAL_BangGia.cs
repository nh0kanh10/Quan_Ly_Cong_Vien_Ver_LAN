using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public class DAL_BangGia
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_BangGia instance;
        public static DAL_BangGia Instance
        {
            get
            {
                if (instance == null) instance = new DAL_BangGia();
                return instance;
            }
        }

        private ET_BangGia Map(BangGia s) => new ET_BangGia
        {
            Id = s.Id,
            IdSanPham = s.IdSanPham,
            GiaNgayThuong = s.GiaNgayThuong,
            GiaCuoiTuan = s.GiaCuoiTuan,
            GiaNgayLe = s.GiaNgayLe,
            GioBatDau = s.GioBatDau,
            GioKetThuc = s.GioKetThuc,
            PhutBlock = s.PhutBlock,
            PhutTiep = s.PhutTiep,
            GiaPhuThu = s.GiaPhuThu,
            TienCoc = s.TienCoc,
            TrangThai = s.TrangThai,
            CreatedAt = s.CreatedAt,
            CreatedBy = s.CreatedBy
        };

        public List<ET_BangGia> LoadDS()
        {
            return db.GetTable<BangGia>().Select(s => Map(s)).ToList();
        }

        public List<ET_BangGia> LayTheoSanPham(int idSanPham)
        {
            return db.GetTable<BangGia>()
                .Where(x => x.IdSanPham == idSanPham && x.TrangThai == "HoạtĐộng")
                .ToList()
                .Select(s => Map(s))
                .ToList();
        }

        public ET_BangGia LayTheoId(int id)
        {
            var s = db.GetTable<BangGia>().FirstOrDefault(x => x.Id == id);
            return s == null ? null : Map(s);
        }

        /// <summary>
        /// Lấy giá active của SP, lọc theo khung giờ hiện tại
        /// </summary>
        public List<ET_BangGia> LayGiaHienTai(int idSanPham, TimeSpan gioHienTai)
        {
            return db.GetTable<BangGia>()
                .Where(x => x.IdSanPham == idSanPham
                         && x.TrangThai == "HoạtĐộng"
                         && x.GioBatDau <= gioHienTai
                         && x.GioKetThuc >= gioHienTai)
                .ToList()
                .Select(s => Map(s))
                .OrderBy(x => (x.GioKetThuc - x.GioBatDau).TotalMinutes) // Khung hẹp ưu tiên
                .ToList();
        }

        public bool Them(ET_BangGia et)
        {
            try
            {
                var obj = new BangGia
                {
                    IdSanPham = et.IdSanPham,
                    GiaNgayThuong = et.GiaNgayThuong,
                    GiaCuoiTuan = et.GiaCuoiTuan,
                    GiaNgayLe = et.GiaNgayLe,
                    GioBatDau = et.GioBatDau,
                    GioKetThuc = et.GioKetThuc,
                    PhutBlock = et.PhutBlock,
                    PhutTiep = et.PhutTiep,
                    GiaPhuThu = et.GiaPhuThu,
                    TienCoc = et.TienCoc,
                    TrangThai = et.TrangThai ?? "HoạtĐộng",
                    CreatedAt = DateTime.Now,
                    CreatedBy = et.CreatedBy
                };
                db.GetTable<BangGia>().InsertOnSubmit(obj);
                db.SubmitChanges();
                et.Id = obj.Id;
                return true;
            }
            catch { return false; }
        }

        public bool Sua(ET_BangGia et)
        {
            try
            {
                var obj = db.GetTable<BangGia>().FirstOrDefault(x => x.Id == et.Id);
                if (obj == null) return false;
                obj.GiaNgayThuong = et.GiaNgayThuong;
                obj.GiaCuoiTuan = et.GiaCuoiTuan;
                obj.GiaNgayLe = et.GiaNgayLe;
                obj.GioBatDau = et.GioBatDau;
                obj.GioKetThuc = et.GioKetThuc;
                obj.PhutBlock = et.PhutBlock;
                obj.PhutTiep = et.PhutTiep;
                obj.GiaPhuThu = et.GiaPhuThu;
                obj.TienCoc = et.TienCoc;
                obj.TrangThai = et.TrangThai;
                db.SubmitChanges();
                return true;
            }
            catch { return false; }
        }

        public bool Xoa(int id)
        {
            try
            {
                var obj = db.GetTable<BangGia>().FirstOrDefault(x => x.Id == id);
                if (obj == null) return false;
                db.GetTable<BangGia>().DeleteOnSubmit(obj);
                db.SubmitChanges();
                return true;
            }
            catch { return false; }
        }
    }
}

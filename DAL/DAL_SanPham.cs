using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_SanPham
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_SanPham instance;
        public static DAL_SanPham Instance
        {
            get
            {
                if (instance == null) instance = new DAL_SanPham();
                return instance;
            }
        }

        public List<ET_SanPham> LoadDS()
        {
            return db.GetTable<SanPham>().Select(s => new ET_SanPham
            {
                Id = s.Id,
                MaCode = s.MaCode,
                Ten = s.Ten,
                LoaiSanPham = s.LoaiSanPham,
                IdDonViCoBan = s.IdDonViCoBan,
                DonGia = s.DonGia,
                IdKhuVuc = s.IdKhuVuc,
                MoTa = s.MoTa,
                TrangThai = s.TrangThai,
                HinhAnh = s.HinhAnh,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt,
                CreatedBy = s.CreatedBy,
                IsDeleted = s.IsDeleted
            }).ToList();
        }

        public List<ET_SanPham> GetAll()
        {
            return LoadDS();
        }

        /// <summary>
        /// Insert SanPham + tự động ghi SanPham_Ve nếu loại = 'Ve'
        /// Dùng single DataContext nên tự động nằm trong 1 transaction
        /// </summary>
        public int ThemVaLayId(ET_SanPham et)
        {
            try {
                SanPham obj = new SanPham();
                obj.MaCode = et.MaCode;
                obj.Ten = et.Ten;
                obj.LoaiSanPham = et.LoaiSanPham;
                obj.IdDonViCoBan = et.IdDonViCoBan;
                obj.DonGia = et.DonGia;
                obj.IdKhuVuc = et.IdKhuVuc;
                obj.MoTa = et.MoTa;
                obj.TrangThai = et.TrangThai;
                obj.HinhAnh = et.HinhAnh;
                obj.CreatedAt = DateTime.Now;
                obj.CreatedBy = et.CreatedBy;
                obj.IsDeleted = false;
                db.GetTable<SanPham>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return obj.Id; // IDENTITY populated after SubmitChanges
            } catch { return 0; }
        }

        public bool Them(ET_SanPham et)
        {
            return ThemVaLayId(et) > 0;
        }

        public bool Sua(ET_SanPham et)
        {
            try {
                var obj = db.GetTable<SanPham>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.Ten = et.Ten;
                    obj.LoaiSanPham = et.LoaiSanPham;
                    obj.IdDonViCoBan = et.IdDonViCoBan;
                    obj.DonGia = et.DonGia;
                    obj.IdKhuVuc = et.IdKhuVuc;
                    obj.MoTa = et.MoTa;
                    obj.TrangThai = et.TrangThai;
                    obj.HinhAnh = et.HinhAnh;
                    obj.UpdatedAt = DateTime.Now;
                    obj.IsDeleted = et.IsDeleted;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<SanPham>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    obj.IsDeleted = true; // Soft delete
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_SanPham LayTheoId(int id)
        {
            return db.GetTable<SanPham>().Where(x => x.Id == id).Select(s => new ET_SanPham {
                Id = s.Id,
                MaCode = s.MaCode,
                Ten = s.Ten,
                LoaiSanPham = s.LoaiSanPham,
                IdDonViCoBan = s.IdDonViCoBan,
                DonGia = s.DonGia,
                IdKhuVuc = s.IdKhuVuc,
                MoTa = s.MoTa,
                TrangThai = s.TrangThai,
                HinhAnh = s.HinhAnh,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt,
                CreatedBy = s.CreatedBy,
                IsDeleted = s.IsDeleted
            }).FirstOrDefault();
        }

        public ET_SanPham LayTheoMaCode(string maCode)
        {
            return db.GetTable<SanPham>().Where(x => x.MaCode == maCode && !x.IsDeleted).Select(s => new ET_SanPham
            {
                Id = s.Id,
                MaCode = s.MaCode,
                Ten = s.Ten,
                LoaiSanPham = s.LoaiSanPham,
                IdDonViCoBan = s.IdDonViCoBan,
                DonGia = s.DonGia,
                IdKhuVuc = s.IdKhuVuc,
                MoTa = s.MoTa,
                TrangThai = s.TrangThai,
                HinhAnh = s.HinhAnh,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt,
                CreatedBy = s.CreatedBy,
                IsDeleted = s.IsDeleted
            }).FirstOrDefault();
        }
    }
}

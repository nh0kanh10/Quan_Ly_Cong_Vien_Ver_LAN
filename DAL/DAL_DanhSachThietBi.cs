using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public class DAL_DanhSachThietBi
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_DanhSachThietBi instance;
        public static DAL_DanhSachThietBi Instance
        {
            get
            {
                if (instance == null) instance = new DAL_DanhSachThietBi();
                return instance;
            }
        }

        private ET_DanhSachThietBi MapToET(DanhSachThietBi s)
        {
            return new ET_DanhSachThietBi
            {
                Id = s.Id,
                MaCode = s.MaCode,
                TenThietBi = s.TenThietBi,
                LoaiThietBi = s.LoaiThietBi,
                IdKhuVuc = s.IdKhuVuc,
                MoTa = s.MoTa,
                NgayMua = s.NgayMua,
                GiaTriMua = s.GiaTriMua,
                TrangThai = s.TrangThai,
                ChuKyBaoTriThang = s.ChuKyBaoTriThang,
                TenKhuVuc = s.KhuVuc != null ? s.KhuVuc.TenKhuVuc : ""
            };
        }

        public List<ET_DanhSachThietBi> LoadDS()
        {
            return db.GetTable<DanhSachThietBi>().ToList().Select(s => MapToET(s)).ToList();
        }

        public List<ET_DanhSachThietBi> LoadDSTheoLoai(string loai)
        {
            return db.GetTable<DanhSachThietBi>()
                .Where(s => s.LoaiThietBi == loai)
                .ToList()
                .Select(s => MapToET(s))
                .ToList();
        }

        /// <summary>
        /// Tìm kiếm theo keyword + loại + trạng thái. "Tất cả" = không lọc.
        /// </summary>
        public List<ET_DanhSachThietBi> TimKiem(string keyword, string loai, string trangThai)
        {
            var q = db.GetTable<DanhSachThietBi>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
                q = q.Where(s => s.TenThietBi.Contains(keyword) || s.MaCode.Contains(keyword));

            if (!string.IsNullOrWhiteSpace(loai) && loai != "Tất cả")
                q = q.Where(s => s.LoaiThietBi == loai);

            if (!string.IsNullOrWhiteSpace(trangThai) && trangThai != "Tất cả")
                q = q.Where(s => s.TrangThai == trangThai);

            return q.ToList().Select(s => MapToET(s)).ToList();
        }

        public ET_DanhSachThietBi LayTheoId(int id)
        {
            var s = db.GetTable<DanhSachThietBi>().FirstOrDefault(x => x.Id == id);
            return s != null ? MapToET(s) : null;
        }

        public bool Them(ET_DanhSachThietBi et)
        {
            try
            {
                var obj = new DanhSachThietBi
                {
                    MaCode = string.IsNullOrWhiteSpace(et.MaCode)
                        ? "TB-" + DateTime.Now.ToString("yyyyMMddHHmmss")
                        : et.MaCode,
                    TenThietBi = et.TenThietBi,
                    LoaiThietBi = et.LoaiThietBi,
                    IdKhuVuc = et.IdKhuVuc,
                    MoTa = et.MoTa,
                    NgayMua = et.NgayMua,
                    GiaTriMua = et.GiaTriMua,
                    TrangThai = string.IsNullOrWhiteSpace(et.TrangThai) ? "HoatDong" : et.TrangThai,
                    ChuKyBaoTriThang = et.ChuKyBaoTriThang
                };
                db.GetTable<DanhSachThietBi>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            }
            catch { return false; }
        }

        public bool Sua(ET_DanhSachThietBi et)
        {
            try
            {
                var obj = db.GetTable<DanhSachThietBi>().FirstOrDefault(x => x.Id == et.Id);
                if (obj == null) return false;
                obj.MaCode = et.MaCode;
                obj.TenThietBi = et.TenThietBi;
                obj.LoaiThietBi = et.LoaiThietBi;
                obj.IdKhuVuc = et.IdKhuVuc;
                obj.MoTa = et.MoTa;
                obj.NgayMua = et.NgayMua;
                obj.GiaTriMua = et.GiaTriMua;
                obj.TrangThai = et.TrangThai;
                obj.ChuKyBaoTriThang = et.ChuKyBaoTriThang;
                db.SubmitChanges();
                return true;
            }
            catch { return false; }
        }

        public bool Xoa(int id)
        {
            try
            {
                var obj = db.GetTable<DanhSachThietBi>().FirstOrDefault(x => x.Id == id);
                if (obj == null) return false;
                db.GetTable<DanhSachThietBi>().DeleteOnSubmit(obj);
                db.SubmitChanges();
                return true;
            }
            catch { return false; }
        }

        public bool DoiTrangThai(int id, string trangThai)
        {
            try
            {
                var obj = db.GetTable<DanhSachThietBi>().FirstOrDefault(x => x.Id == id);
                if (obj == null) return false;
                obj.TrangThai = trangThai;
                db.SubmitChanges();
                return true;
            }
            catch { return false; }
        }
    }
}

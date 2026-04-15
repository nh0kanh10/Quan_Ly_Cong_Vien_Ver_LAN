using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_DoanKhach
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_DoanKhach instance;
        public static DAL_DoanKhach Instance
        {
            get
            {
                if (instance == null) instance = new DAL_DoanKhach();
                return instance;
            }
        }

        private ET_DoanKhach MapToET(DoanKhach s)
        {
            // [DROPPED] IdCombo, TenCombo lookup — dùng DoanKhach_DichVu thay thế
            return new ET_DoanKhach
            {
                Id = s.Id,
                MaBooking = s.MaBooking,
                TenDoan = s.TenDoan,
                MaSoThue = s.MaSoThue,
                NguoiDaiDien = s.NguoiDaiDien,
                DienThoaiLienHe = s.DienThoaiLienHe,
                ChietKhau = s.ChietKhau,
                SoLuongKhach = s.SoLuongKhach,
                NgayDen = s.NgayDen,
                NgayDi = s.NgayDi,
                TrangThai = s.TrangThai,
                // [DROPPED] IdCombo, TenCombo — dùng DoanKhach_DichVu thay thế
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt,
                CreatedBy = s.CreatedBy,
                IsDeleted = s.IsDeleted
            };
        }

        public List<ET_DoanKhach> LoadDS()
        {
            return db.GetTable<DoanKhach>()
                .Where(x => !x.IsDeleted)
                .Select(s => MapToET(s))
                .ToList();
        }

        public List<ET_DoanKhach> GetAll()
        {
            return LoadDS();
        }

        public ET_DoanKhach LayTheoId(int id)
        {
            var s = db.GetTable<DoanKhach>().FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            return s != null ? MapToET(s) : null;
        }

        /// <summary>
        /// Tìm đoàn theo MaBooking hoặc SĐT liên hệ (cho quầy POS quét mã)
        /// </summary>
        public ET_DoanKhach LayTheoMaBookingHoacSdt(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return null;
            var s = db.GetTable<DoanKhach>().FirstOrDefault(x => !x.IsDeleted &&
                (x.MaBooking == keyword || x.DienThoaiLienHe == keyword));
            return s != null ? MapToET(s) : null;
        }

        public bool Them(ET_DoanKhach et)
        {
            return ThemVaLayId(et) > 0;
        }

        public int ThemVaLayId(ET_DoanKhach et)
        {
            try {
                DoanKhach obj = new DoanKhach();
                obj.MaBooking = et.MaBooking;
                obj.TenDoan = et.TenDoan;
                obj.MaSoThue = et.MaSoThue;
                obj.NguoiDaiDien = et.NguoiDaiDien;
                obj.DienThoaiLienHe = et.DienThoaiLienHe;
                obj.ChietKhau = et.ChietKhau;
                obj.SoLuongKhach = et.SoLuongKhach;
                obj.NgayDen = et.NgayDen;
                obj.NgayDi = et.NgayDi;
                obj.TrangThai = et.TrangThai ?? AppConstants.TrangThaiDoanKhach.DaDat;
                // [DROPPED] IdCombo — không set nữa
                obj.CreatedAt = et.CreatedAt;
                obj.UpdatedAt = et.UpdatedAt;
                obj.CreatedBy = et.CreatedBy;
                obj.IsDeleted = et.IsDeleted;
                db.GetTable<DoanKhach>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return obj.Id;
            } catch { return 0; }
        }

        public bool Sua(ET_DoanKhach et)
        {
            try {
                var obj = db.GetTable<DoanKhach>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.MaBooking = et.MaBooking;
                    obj.TenDoan = et.TenDoan;
                    obj.MaSoThue = et.MaSoThue;
                    obj.NguoiDaiDien = et.NguoiDaiDien;
                    obj.DienThoaiLienHe = et.DienThoaiLienHe;
                    obj.ChietKhau = et.ChietKhau;
                    obj.SoLuongKhach = et.SoLuongKhach;
                    obj.NgayDen = et.NgayDen;
                    obj.NgayDi = et.NgayDi;
                    obj.TrangThai = et.TrangThai;
                    // [DROPPED] IdCombo
                    obj.UpdatedAt = DateTime.Now;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<DoanKhach>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    obj.IsDeleted = true;
                    obj.UpdatedAt = DateTime.Now;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        /// <summary>
        /// Đánh dấu đoàn đã xuất vé xong
        /// </summary>
        public bool DanhDauDaXuatVe(int idDoan)
        {
            try {
                var obj = db.GetTable<DoanKhach>().FirstOrDefault(x => x.Id == idDoan);
                if (obj != null) {
                    obj.TrangThai = AppConstants.TrangThaiDoanKhach.DaXuatVe;
                    obj.UpdatedAt = DateTime.Now;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }
    }
}

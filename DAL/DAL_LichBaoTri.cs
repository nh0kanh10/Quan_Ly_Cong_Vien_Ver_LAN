using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public class DAL_LichBaoTri
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_LichBaoTri instance;
        public static DAL_LichBaoTri Instance
        {
            get
            {
                if (instance == null) instance = new DAL_LichBaoTri();
                return instance;
            }
        }

        private ET_LichBaoTri MapToET(LichBaoTri s)
        {
            return new ET_LichBaoTri
            {
                Id = s.Id,
                IdThietBi = s.IdThietBi,
                NgayBaoTri = s.NgayBaoTri,
                LoaiBaoTri = s.LoaiBaoTri,
                NoiDung = s.NoiDung,
                ChiPhi = s.ChiPhi,
                IdNhanVienThucHien = s.IdNhanVienThucHien,
                IdPhieuChi = s.IdPhieuChi,
                TrangThai = s.TrangThai,
                TenThietBi = s.DanhSachThietBi != null ? s.DanhSachThietBi.TenThietBi : "",
                TenNhanVien = s.NhanVien != null ? s.NhanVien.HoTen : ""
            };
        }

        public List<ET_LichBaoTri> LoadDS()
        {
            return db.GetTable<LichBaoTri>().ToList().Select(s => MapToET(s)).ToList();
        }

        public List<ET_LichBaoTri> LoadTheoThietBi(int idThietBi)
        {
            return db.GetTable<LichBaoTri>()
                .Where(s => s.IdThietBi == idThietBi)
                .OrderByDescending(s => s.NgayBaoTri)
                .ToList()
                .Select(s => MapToET(s))
                .ToList();
        }

        public bool Them(ET_LichBaoTri et)
        {
            try
            {
                var obj = new LichBaoTri
                {
                    IdThietBi = et.IdThietBi,
                    NgayBaoTri = et.NgayBaoTri,
                    LoaiBaoTri = et.LoaiBaoTri,
                    NoiDung = et.NoiDung,
                    ChiPhi = et.ChiPhi,
                    IdNhanVienThucHien = et.IdNhanVienThucHien,
                    IdPhieuChi = et.IdPhieuChi,
                    TrangThai = string.IsNullOrWhiteSpace(et.TrangThai) ? "KeHoach" : et.TrangThai
                };
                db.GetTable<LichBaoTri>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            }
            catch { return false; }
        }

        public bool Sua(ET_LichBaoTri et)
        {
            try
            {
                var obj = db.GetTable<LichBaoTri>().FirstOrDefault(x => x.Id == et.Id);
                if (obj == null) return false;
                obj.NgayBaoTri = et.NgayBaoTri;
                obj.LoaiBaoTri = et.LoaiBaoTri;
                obj.NoiDung = et.NoiDung;
                obj.ChiPhi = et.ChiPhi;
                obj.IdNhanVienThucHien = et.IdNhanVienThucHien;
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
                var obj = db.GetTable<LichBaoTri>().FirstOrDefault(x => x.Id == id);
                if (obj == null) return false;
                db.GetTable<LichBaoTri>().DeleteOnSubmit(obj);
                db.SubmitChanges();
                return true;
            }
            catch { return false; }
        }

        public bool HoanTat(int id)
        {
            try
            {
                var obj = db.GetTable<LichBaoTri>().FirstOrDefault(x => x.Id == id);
                if (obj == null) return false;
                obj.TrangThai = "HoanTat";
                db.SubmitChanges();
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Trả về các lịch bảo trì đã quá ngày nhưng chưa hoàn tất (dùng cho cảnh báo Dashboard)
        /// </summary>
        public List<ET_LichBaoTri> LayQuaHan()
        {
            return db.GetTable<LichBaoTri>()
                .Where(s => s.TrangThai != "HoanTat" && s.NgayBaoTri < DateTime.Today)
                .OrderBy(s => s.NgayBaoTri)
                .ToList()
                .Select(s => MapToET(s))
                .ToList();
        }
    }
}


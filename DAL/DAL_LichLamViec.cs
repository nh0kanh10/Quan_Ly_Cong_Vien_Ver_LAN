using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public class DAL_LichLamViec
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_LichLamViec instance;
        public static DAL_LichLamViec Instance
        {
            get
            {
                if (instance == null) instance = new DAL_LichLamViec();
                return instance;
            }
        }

        /// <summary>
        /// Load lịch theo tuần (từ thứ 2 đến CN) và ca làm
        /// </summary>
        public List<ET_LichLamViec> LoadTheoTuan(DateTime mondayOfWeek, string caLam)
        {
            DateTime sunday = mondayOfWeek.AddDays(6);
            return db.LichLamViecs
                .Where(x => x.NgayLam >= mondayOfWeek.Date && x.NgayLam <= sunday.Date && x.CaLam == caLam)
                .Select(x => new ET_LichLamViec
                {
                    Id = x.Id,
                    IdNhanVien = x.IdNhanVien,
                    IdKhuVuc = x.IdKhuVuc,
                    NgayLam = x.NgayLam,
                    CaLam = x.CaLam,
                    GioBatDau = x.GioBatDau,
                    GioKetThuc = x.GioKetThuc,
                    TrangThai = x.TrangThai,
                    GhiChu = x.GhiChu,
                    TenNhanVien = x.NhanVien != null ? x.NhanVien.HoTen : "",
                    TenKhuVuc = x.KhuVuc != null ? x.KhuVuc.TenKhuVuc : ""
                }).ToList();
        }

        /// <summary>
        /// Lấy danh sách NV ĐÃ phân vào 1 ô cụ thể (KhuVuc + Ngay + Ca)
        /// </summary>
        public List<ET_LichLamViec> LoadTheoO(int idKhuVuc, DateTime ngay, string caLam)
        {
            return db.LichLamViecs
                .Where(x => x.IdKhuVuc == idKhuVuc && x.NgayLam == ngay.Date && x.CaLam == caLam)
                .Select(x => new ET_LichLamViec
                {
                    Id = x.Id,
                    IdNhanVien = x.IdNhanVien,
                    IdKhuVuc = x.IdKhuVuc,
                    NgayLam = x.NgayLam,
                    CaLam = x.CaLam,
                    TenNhanVien = x.NhanVien != null ? x.NhanVien.HoTen : ""
                }).ToList();
        }

        /// <summary>
        /// Thêm 1 NV vào ca (auto-fill giờ từ ET helper)
        /// </summary>
        public bool Them(int idNhanVien, int idKhuVuc, DateTime ngayLam, string caLam)
        {
            try
            {
                // Check trùng
                bool daTonTai = db.LichLamViecs.Any(x =>
                    x.IdNhanVien == idNhanVien && x.NgayLam == ngayLam.Date && x.CaLam == caLam);
                if (daTonTai) return false;

                LichLamViec obj = new LichLamViec();
                obj.IdNhanVien = idNhanVien;
                obj.IdKhuVuc = idKhuVuc;
                obj.NgayLam = ngayLam.Date;
                obj.CaLam = caLam;
                obj.GioBatDau = ET_LichLamViec.LayGioBatDau(caLam);
                obj.GioKetThuc = ET_LichLamViec.LayGioKetThuc(caLam);
                obj.TrangThai = "DaXepLich";
                db.LichLamViecs.InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Gỡ NV khỏi ca
        /// </summary>
        public bool Xoa(int id)
        {
            try
            {
                var obj = db.LichLamViecs.FirstOrDefault(x => x.Id == id);
                if (obj != null)
                {
                    db.LichLamViecs.DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        /// <summary>
        /// Copy toàn bộ lịch 1 tuần sang tuần kế tiếp (cùng ca)
        /// </summary>
        public int CopyTuan(DateTime mondayNguon, DateTime mondayDich, string caLam)
        {
            int count = 0;
            try
            {
                DateTime sundayNguon = mondayNguon.AddDays(6);
                var lichNguon = db.LichLamViecs
                    .Where(x => x.NgayLam >= mondayNguon.Date && x.NgayLam <= sundayNguon.Date && x.CaLam == caLam)
                    .ToList();

                foreach (var item in lichNguon)
                {
                    int offsetDay = (item.NgayLam - mondayNguon.Date).Days;
                    DateTime ngayMoi = mondayDich.Date.AddDays(offsetDay);

                    bool daTonTai = db.LichLamViecs.Any(x =>
                        x.IdNhanVien == item.IdNhanVien && x.NgayLam == ngayMoi && x.CaLam == caLam);
                    if (daTonTai) continue;

                    LichLamViec copy = new LichLamViec();
                    copy.IdNhanVien = item.IdNhanVien;
                    copy.IdKhuVuc = item.IdKhuVuc;
                    copy.NgayLam = ngayMoi;
                    copy.CaLam = item.CaLam;
                    copy.GioBatDau = item.GioBatDau;
                    copy.GioKetThuc = item.GioKetThuc;
                    copy.TrangThai = "DaXepLich";
                    copy.GhiChu = item.GhiChu;
                    db.LichLamViecs.InsertOnSubmit(copy);
                    count++;
                }
                db.SubmitChanges();
                return count;
            }
            catch { return 0; }
        }

        /// <summary>
        /// Danh sách ID nhân viên ĐÃ có ca trong ngày+ca cụ thể (dùng để lọc NV chưa phân)
        /// </summary>
        public List<int> LayDsIdNVDaPhanTrongNgay(DateTime ngay, string caLam)
        {
            return db.LichLamViecs
                .Where(x => x.NgayLam == ngay.Date && x.CaLam == caLam)
                .Select(x => x.IdNhanVien)
                .Distinct()
                .ToList();
        }
    }
}

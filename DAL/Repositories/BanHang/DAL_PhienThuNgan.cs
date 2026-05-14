using System;
using System.Linq;
using ET.Constants;
using ET.Models.BanHang;

namespace DAL.Repositories.BanHang
{
    public class DAL_PhienThuNgan
    {
        public static DAL_PhienThuNgan Instance { get; } = new DAL_PhienThuNgan();

        #region Truy vấn dữ liệu

        public ET_PhienThuNgan GetPhienDangMo(int idNhanVien)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.PhienThuNgans
                    .Where(p => p.TrangThai == AppConstants.TrangThaiPhienThuNgan.DangMo && p.IdNhanVien == idNhanVien)
                    .Select(p => new ET_PhienThuNgan
                    {
                        Id = p.Id,
                        IdNhanVien = p.IdNhanVien,
                        IdMayBan = p.IdMayBan,
                        IdKhoBan = p.IdKhoBan,
                        ThoiGianMo = p.ThoiGianMo,
                        TienDauCa = p.TienDauCa,
                        TrangThai = p.TrangThai
                    })
                    .FirstOrDefault();
            }
        }

        public bool MayDangCoPhienKhac(string idMayBan, int idNhanVien)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.PhienThuNgans.Any(p => 
                    p.IdMayBan == idMayBan && 
                    p.TrangThai == AppConstants.TrangThaiPhienThuNgan.DangMo && 
                    p.IdNhanVien != idNhanVien);
            }
        }

        public ET_PhienThuNgan LayPhienDaDong(int idPhien)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var p = db.PhienThuNgans.FirstOrDefault(x => x.Id == idPhien);
                if (p == null) return null;

                decimal tongThu = p.TongThuTrongCa ?? 0;
                decimal tienDau = p.TienDauCa;
                decimal tienCuoi = p.TienCuoiCa ?? 0;
                decimal chenhLech = tienCuoi - (tienDau + tongThu);

                return new ET_PhienThuNgan
                {
                    Id = p.Id,
                    IdNhanVien = p.IdNhanVien,
                    IdMayBan = p.IdMayBan,
                    IdKhoBan = p.IdKhoBan,
                    ThoiGianMo = p.ThoiGianMo,
                    ThoiGianDong = p.ThoiGianDong,
                    TienDauCa = p.TienDauCa,
                    TienCuoiCa = tienCuoi,
                    TongThuTrongCa = tongThu,
                    ChenhLech = chenhLech,
                    TrangThai = p.TrangThai
                };
            }
        }

        #endregion

        #region Thêm / Sửa / Xoá

        public int MoPhien(ET_PhienThuNgan phien)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var row = new PhienThuNgan
                {
                    IdNhanVien = phien.IdNhanVien,
                    IdMayBan = phien.IdMayBan,
                    IdKhoBan = phien.IdKhoBan,
                    ThoiGianMo = DateTime.Now,
                    TienDauCa = phien.TienDauCa,
                    TrangThai = AppConstants.TrangThaiPhienThuNgan.DangMo,
                    GhiChu = phien.GhiChu
                };
                db.PhienThuNgans.InsertOnSubmit(row);
                db.SubmitChanges();
                return row.Id;
            }
        }

        public void DongPhien(int idPhien, decimal tienCuoiCa, string ghiChu)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var phien = db.PhienThuNgans.FirstOrDefault(p => p.Id == idPhien);
                if (phien == null) return;

                phien.ThoiGianDong = DateTime.Now;
                phien.TienCuoiCa = tienCuoiCa;
                phien.GhiChu = ghiChu;
                phien.TrangThai = AppConstants.TrangThaiPhienThuNgan.DaDong;

                // Tính tổng thu trong ca từ các hóa đơn thanh toán
                phien.TongThuTrongCa = db.ChungTuTCs
                    .Where(c => c.IdPhienThuNgan == idPhien && c.LoaiChungTu == AppConstants.LoaiChungTuTC.THU_THANHTOAN && c.TrangThai == AppConstants.TrangThaiChungTuTC.DaDuyet)
                    .Select(c => (decimal?)c.SoTien)
                    .Sum() ?? 0m;

                db.SubmitChanges();
            }
        }

        #endregion
    }
}



using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories.DoiTac
{
    public class DAL_TaiKhoan
    {
        #region Khởi tạo (Singleton)
        public static DAL_TaiKhoan Instance { get; } = new DAL_TaiKhoan();
        #endregion

        #region Nghiệp vụ
        public TaiKhoan KiemTraDangNhap(string username)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.TaiKhoans.FirstOrDefault(t => t.TenDangNhap == username);
            }
        }
        
        public ET_ThongTinTaiKhoan LayThongTinChiTiet(int idDoiTac)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var thongTin = db.ThongTins.FirstOrDefault(t => t.Id == idDoiTac);
                var nhanVien = db.NhanViens.FirstOrDefault(x => x.IdDoiTac == idDoiTac);

                if (thongTin == null) return null;

                var ketQua = new ET_ThongTinTaiKhoan
                {
                    IdDoiTac = thongTin.Id,
                    HoTen = thongTin.HoTen,
                    DienThoai = thongTin.DienThoai,
                    MaNhanVien = nhanVien != null ? nhanVien.MaNhanVien : string.Empty,
                    IdVaiTro = nhanVien != null ? (nhanVien.IdVaiTro ?? 0) : 0
                };

                return ketQua;
            }
        }

        public List<string> LayQuyenHan(int idVaiTro)
        {
            using (var db = new DaiNamDBDataContext())
            {
                if (idVaiTro <= 0) return new List<string>();

                return db.PhanQuyens
                         .Where(q => q.IdVaiTro == idVaiTro)
                         .Select(q => q.QuyenHan.MaQuyen)
                         .ToList();
            }
        }
        #endregion
    }

    // Dùng nội bộ DAL để chuyển dữ liệu lên BUS
    public class ET_ThongTinTaiKhoan
    {
        public int IdDoiTac { get; set; }
        public string HoTen { get; set; }
        public string DienThoai { get; set; }
        public string MaNhanVien { get; set; }
        public int IdVaiTro { get; set; }
    }
}



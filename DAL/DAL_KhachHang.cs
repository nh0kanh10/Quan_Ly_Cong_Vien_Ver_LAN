using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_KhachHang
    {
        private static DAL_KhachHang instance;

        public static DAL_KhachHang Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DAL_KhachHang();
                }
                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        public List<ET_KHACHHANG> loadDS()
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext())
            {
                var ds = from kh in db.KhachHangs
                         select new ET_KHACHHANG
                         {
                             MaKhachHang = kh.MaKhachHang,
                             HoTen = kh.HoTen,
                             SoDienThoai = kh.SoDienThoai,
                             Email = kh.Email,                            
                             DiaChi = kh.DiaChi,
                             NgaySinh = kh.NgaySinh,
                             GioiTinh = kh.GioiTinh,  
                             NgayTao = kh.NgayTao
                         };

                return ds.ToList();
            }
        }

        public List<ET_KHACHHANG> TimKhachHang(string keyword)
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext())
            {
                var ds = from kh in db.KhachHangs
                         where kh.HoTen.Contains(keyword)
                            || kh.SoDienThoai.Contains(keyword)
                         select new ET_KHACHHANG
                         {
                             MaKhachHang = kh.MaKhachHang,
                             HoTen = kh.HoTen,
                             SoDienThoai = kh.SoDienThoai,
                             Email = kh.Email,
                             DiaChi = kh.DiaChi,
                             NgaySinh = kh.NgaySinh,
                             GioiTinh = kh.GioiTinh,
                             NgayTao = kh.NgayTao
                         };

                return ds.ToList();
            }
        }
    }
}

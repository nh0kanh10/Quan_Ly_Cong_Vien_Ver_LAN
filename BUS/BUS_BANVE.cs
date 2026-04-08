using DAL;
using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_BANVE
    {
        private static BUS_BANVE instance;
        public static BUS_BANVE Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BUS_BANVE();
                }
                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        public List<LoaiVe> LayDSLoaiVe()
        {
            return DAL_BANVE.Instance.LayDSLoaiVe();
        }

        public List<ET_DichVu> LayDSDichVu()
        {
            return DAL_BANVE.Instance.LayDSDichVu();
        }

        //Giỏ hàng
        public List<ET_GioHang> gioHang = new List<ET_GioHang>();
        public void themGioHang(ET_GioHang et)
        {
            var e = gioHang.FirstOrDefault(x => x.TenSanPham == et.TenSanPham);
            if (e != null)
            {
                e.SoLuong += et.SoLuong;
            }
            else
            {
                gioHang.Add(et);
            }
        }

        public decimal tamTinh()
        {
            return gioHang.Sum(x => x.ThanhTien);
        }

        public void xoaGioHang(string tenSP, string loai)
        {
            var item = gioHang.FirstOrDefault(x => x.TenSanPham == tenSP && x.Loai == loai);

            if (item != null)
            {
                gioHang.Remove(item);
            }
        }
        public void thanhToan(int? maKhachHang, string phuongThuc)
        {
            DAL_BANVE.Instance.thanhToan(gioHang, maKhachHang, phuongThuc);

            // clear giỏ
            gioHang.Clear();
        }

        public decimal tinhTienGiam(KhuyenMai km, decimal tongTien)
        {
            if (km == null) return 0;

            // chưa đủ điều kiện
            if (tongTien < km.DonToiThieu) return 0;

            if (km.LoaiGiamGia == "PhanTram")
            {
                return tongTien * (km.GiaTriGiam / 100);
            }
            else // SoTien
            {
                return km.GiaTriGiam;
            }
        }
    }
}

using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_BANVE
    {
        private static DAL_BANVE instance;

        public static DAL_BANVE Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DAL_BANVE();
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
            using(var db = new QLKVCGTDataContext())
            {
                return db.LoaiVes.Where(x => x.TrangThai == "Hoạt động").ToList();
            }
        }

        public List<ET_DichVu> LayDSDichVu()
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext())
            {
                var ds = from dv in db.DichVus
                         join kv in db.KhuVucs on dv.MaKhuVuc equals kv.MaKhuVuc
                         join dm in db.DanhMucDichVus on dv.MaDanhMuc equals dm.MaDanhMuc
                         where dv.TrangThai == "Hoạt động"
                         select new ET_DichVu
                         {
                             MaDichVu = dv.MaDichVu,
                             MaCode = dv.MaCode,
                             TenDichVu = dv.TenDichVu,
                             MaDanhMuc = dv.MaDanhMuc ?? 0,
                             MaKhuVuc = dv.MaKhuVuc ?? 0,
                             GiaBan = dv.GiaBan,
                             SoLuongTon = dv.SoLuongTon,
                             DonViTinh = dv.DonViTinh,
                             TrangThai = dv.TrangThai,
                             NgayTao = dv.NgayTao,
                             NgayCapNhat = dv.NgayCapNhat ?? DateTime.Now
                         };
                return ds.ToList();
            }
        }



        //thanh toán
        public void thanhToan(List<ET_GioHang> gioHang, int? maKH, string phuongThuc)
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext())
            {
                decimal tamTinh = gioHang.Sum(x => x.ThanhTien);
                decimal giamGia = 0; // lấy từ KM
                decimal thanhToan = tamTinh - giamGia;

                if (thanhToan < 0) thanhToan = 0;

                HoaDon hd = new HoaDon
                {
                    MaCode = "HD" + DateTime.Now.Ticks,
                    MaKhachHang = maKH,
                    MaNhanVien = 1, // tạm
                    NgayLap = DateTime.Now,

                    TongTien = tamTinh,
                    TienGiamGia = giamGia,
                    ThanhToan = thanhToan,

                    PhuongThuc = phuongThuc,
                    TrangThai = "Đã thanh toán",

                    NgayCapNhat = DateTime.Now // 
                };

                db.HoaDons.InsertOnSubmit(hd);
                db.SubmitChanges();

                // 👉 Chi tiết hóa đơn
                foreach (var item in gioHang)
                {
                    if (item.Loai == "Vé")
                    {
                        ChiTietHoaDon_Ve ct = new ChiTietHoaDon_Ve
                        {
                            MaHoaDon = hd.MaHoaDon,
                            MaLoaiVe = item.MaSanPham,
                            TenSanPham = item.TenSanPham,
                            DonGia = item.DonGia,
                            SoLuong = item.SoLuong,
                            ThanhTien = item.ThanhTien,
                            NgayTao = DateTime.Now //  BẮT BUỘC
                        };
                        db.ChiTietHoaDon_Ves.InsertOnSubmit(ct);
                    }
                    else
                    {
                        ChiTietHoaDon_DichVu ct = new ChiTietHoaDon_DichVu
                        {
                            MaHoaDon = hd.MaHoaDon,
                            MaDichVu = item.MaSanPham,
                            TenSanPham = item.TenSanPham,
                            DonGia = item.DonGia,
                            SoLuong = item.SoLuong,
                            ThanhTien = item.ThanhTien,
                            NgayTao = DateTime.Now //  BẮT BUỘC
                        };
                        db.ChiTietHoaDon_DichVus.InsertOnSubmit(ct);
                    }
                }

                db.SubmitChanges();
            }
        }
    }
}

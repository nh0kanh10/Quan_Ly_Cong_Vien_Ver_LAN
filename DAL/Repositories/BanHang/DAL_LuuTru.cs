using System;
using System.Collections.Generic;
using System.Linq;
using ET.Constants;
using ET.DTOs;

namespace DAL.Repositories.BanHang
{
    public class DAL_LuuTru
    {
        public static DAL_LuuTru Instance { get; } = new DAL_LuuTru();

        #region Thông báo đặt phòng

        /// <summary>
        /// Lấy danh sách PhieuDatPhong đang chờ check-in (TrangThai=DatTruoc),
        /// có NgayNhanPhong rơi vào khoảng tuNgay..denNgay.
        /// </summary>
        public List<DTO_DatPhongOnline> LayDatPhongChoPhanCong(DateTime tuNgay, DateTime denNgay)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var query = from pdp in db.PhieuDatPhongs
                            join ct in db.ChiTietDatPhongs on pdp.Id equals ct.IdPhieuDatPhong into ctg
                            from ct in ctg.DefaultIfEmpty()
                            join lp in db.LoaiPhongs on (int?)ct.IdLoaiPhong equals (int?)lp.IdSanPham into lpg
                            from lp in lpg.DefaultIfEmpty()
                            join tt in db.ThongTins on (int?)pdp.IdKhachHang equals (int?)tt.Id into ttg
                            from tt in ttg.DefaultIfEmpty()
                            where pdp.TrangThai == AppConstants.TrangThaiBooking.DatTruoc
                               && pdp.NgayNhanPhong >= tuNgay
                               && pdp.NgayNhanPhong <= denNgay
                            orderby pdp.NgayTao descending
                            select new DTO_DatPhongOnline
                            {
                                IdPhieu       = pdp.Id,
                                MaDatPhong    = pdp.MaDatPhong,
                                TenKhachHang  = tt != null ? tt.HoTen : (pdp.TenNguoiDat ?? "—"),
                                SoDienThoai   = tt != null ? tt.DienThoai : (pdp.SoDienThoai ?? "—"),
                                TenLoaiPhong  = lp != null ? lp.TenLoai : "—",
                                NgayNhanPhong = pdp.NgayNhanPhong,
                                NgayTraPhong  = pdp.NgayTraPhong,
                                TienCoc       = pdp.TienPhongTraTruoc,
                                NgayDat       = pdp.NgayTao
                            };

                return query.ToList();
            }
        }

        #endregion
    }
}



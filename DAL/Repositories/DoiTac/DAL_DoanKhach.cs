using System;
using System.Collections.Generic;
using System.Linq;
using ET.Constants;
using ET.DTOs;

namespace DAL.Repositories.DoiTac
{
    public class DAL_DoanKhach
    {
        public static DAL_DoanKhach Instance { get; } = new DAL_DoanKhach();

        public List<DTO_QuyenLoiDoan> TraCuuQuyenLoiDoan(string maBooking)
        {
            using (var db = new DaiNamDBDataContext())
            {
                // Truy xuất thông qua Đơn Hàng (hoặc Báo Giá nếu được ánh xạ)
                var query = from ql in db.QuyenLoiDoanKhaches
                            join dh in db.DonHangs on ql.IdDonHang equals dh.Id
                            join sp in db.SanPhams on ql.IdSanPham equals sp.Id
                            join tt in db.ThongTins on ql.IdDoanKhach equals tt.Id into ttGroup
                            from tt in ttGroup.DefaultIfEmpty()
                            where (dh.MaDonHang == maBooking || dh.MaDonHang == "BK-" + maBooking) 
                               && ql.TrangThai == AppConstants.TrangThaiQuyenLoi.HopLe
                               && (ql.NgayHetHan == null || ql.NgayHetHan.Value >= DateTime.Now.Date)
                            select new
                            {
                                IdQuyenLoi = ql.Id,
                                IdSanPham = sp.Id,
                                TenSanPham = sp.TenSanPham,
                                LoaiSanPham = sp.LoaiSanPham,
                                SoLuongTong = ql.TongSoLuongMua,
                                NgayHetHan = ql.NgayHetHan,
                                MaBooking = dh.MaDonHang,
                                TenDoanKhach = tt != null ? tt.HoTen : "Đoàn khách"
                            };

                var listRaw = query.ToList();
                var result = new List<DTO_QuyenLoiDoan>();

                foreach (var item in listRaw)
                {
                    int soLuongDaDung = db.LichSuQuetDoans
                                          .Where(ls => ls.IdQuyenLoi == item.IdQuyenLoi)
                                          .Sum(ls => (int?)ls.SoSuatTru) ?? 0;

                    result.Add(new DTO_QuyenLoiDoan
                    {
                        IdQuyenLoi = item.IdQuyenLoi,
                        IdSanPham = item.IdSanPham,
                        TenSanPham = item.TenSanPham,
                        LoaiSanPham = item.LoaiSanPham,
                        SoLuongTong = item.SoLuongTong,
                        SoLuongDaDung = soLuongDaDung,
                        NgayHetHan = item.NgayHetHan,
                        MaBooking = item.MaBooking,
                        TenDoanKhach = item.TenDoanKhach
                    });
                }

                return result;
            }
        }

        public void GhiNhanSuDungQuyenLoi(int idQuyenLoi, int soSuatTru, int idNhanVien, int? idThietBi = null)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var ls = new LichSuQuetDoan
                {
                    IdQuyenLoi = idQuyenLoi,
                    SoSuatTru = soSuatTru,
                    IdNhanVien = idNhanVien,
                    IdThietBi = idThietBi,
                    ThoiGian = DateTime.Now
                };
                db.LichSuQuetDoans.InsertOnSubmit(ls);
                db.SubmitChanges();
            }
        }
    }
}



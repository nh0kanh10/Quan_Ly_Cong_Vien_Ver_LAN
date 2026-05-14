using System;
using System.Collections.Generic;
using System.Linq;
using ET.DTOs;
using ET.Constants;
using ET.Models.DanhMuc;
using ET.Models.BanHang;
using ET.Results;
using DAL;

namespace BUS.Services.BanHang
{
    public class BUS_LuuTru_SoDo
    {
        public static BUS_LuuTru_SoDo Instance { get; } = new BUS_LuuTru_SoDo();

        private BUS_LuuTru_SoDo() { }

        #region Truy vấn dữ liệu

        public ET.DTOs.DTO_PhongLuuTruView LayChiTietPhong(int idPhong)
        {
            using (var db = new DAL.DaiNamDBDataContext())
            {
                return db.Phongs.Where(p => p.Id == idPhong).Select(p => new ET.DTOs.DTO_PhongLuuTruView
                {
                    IdPhong = p.Id,
                    MaPhong = p.MaPhong,
                    TenLoaiPhong = p.LoaiPhong.TenLoai,
                    IdSanPham = p.LoaiPhong.IdSanPham,
                    SoNguoiToiDa = p.LoaiPhong.SoNguoiToiDa,
                    TrangThaiPhong = p.TrangThai
                }).FirstOrDefault();
            }
        }
        
        public OperationResult<List<DTO_PhongLuuTruView>> LayDanhSachSodoPhong(int? idKhuVuc = null)
        {
            try
            {
                using (var db = new DaiNamDBDataContext())
                {
                    // 1. Lấy toàn bộ danh sách phòng làm gốc 
                    var queryPhong = db.Phongs.AsQueryable();
                    if (idKhuVuc.HasValue)
                    {
                        queryPhong = queryPhong.Where(p => p.IdKhuVuc == idKhuVuc.Value);
                    }

                    var danhSachPhong = queryPhong.Select(p => new DTO_PhongLuuTruView
                    {
                        IdPhong = p.Id,
                        MaPhong = p.MaPhong,
                        Tang = p.Tang,
                        TenLoaiPhong = p.LoaiPhong.TenLoai,
                        IdSanPham = p.LoaiPhong.IdSanPham,
                        SoNguoiToiDa = p.LoaiPhong.SoNguoiToiDa,
                        TrangThaiPhong = p.TrangThai
                    }).ToList();

                    // 2. Lấy thông tin Booking của các phòng đang bận hoặc đã đặt trước
                    var thongTinDatPhong = db.ChiTietDatPhongs
                        .Where(ct => ct.TrangThai == AppConstants.TrangThaiChiTietDatPhong.DaCheckIn 
                                  || ct.TrangThai == AppConstants.TrangThaiChiTietDatPhong.ChoDen)
                        .Select(ct => new
                        {
                            ct.Id,
                            ct.IdPhong,
                            ct.IdPhieuDatPhong,
                            ct.PhieuDatPhong.IdKhachHang,
                            TenKhachHang = ct.PhieuDatPhong.KhachHang != null && ct.PhieuDatPhong.KhachHang.ThongTin1 != null ? ct.PhieuDatPhong.KhachHang.ThongTin1.HoTen : (ct.PhieuDatPhong.TenNguoiDat ?? "Khách Lẻ"),
                            SoDienThoai = ct.PhieuDatPhong.KhachHang != null && ct.PhieuDatPhong.KhachHang.ThongTin1 != null ? ct.PhieuDatPhong.KhachHang.ThongTin1.DienThoai : ct.PhieuDatPhong.SoDienThoai,
                            ct.IdChiTietDonHang,
                            ct.NgayCheckIn,
                            ct.NgayCheckOut,
                            IdDonHang = ct.ChiTietDonHang != null ? ct.ChiTietDonHang.IdDonHang : (int?)null,
                            TrangThaiChiTietDatPhong = ct.TrangThai
                        }).ToList();

                    if (thongTinDatPhong.Any())
                    {

                        // 4. Map thông tin Booking ngược lại vào danh sách phòng
                        foreach (var phong in danhSachPhong)
                        {
                            // Lấy booking mới nhất của phòng đó
                            var booking = thongTinDatPhong.OrderByDescending(b => b.Id)
                                                          .FirstOrDefault(b => b.IdPhong == phong.IdPhong);
                            if (booking != null)
                            {
                                phong.IdChiTietDatPhong = booking.Id;
                                phong.IdPhieuDatPhong = booking.IdPhieuDatPhong;
                                phong.IdDonHang = booking.IdDonHang;
                                phong.TenKhachHang = booking.TenKhachHang;
                                phong.SoDienThoai = booking.SoDienThoai;
                                phong.NgayCheckIn = booking.NgayCheckIn;
                                phong.NgayCheckOut = booking.NgayCheckOut;
                                phong.TrangThaiBooking = booking.TrangThaiChiTietDatPhong;
                            }
                        }
                    }

                    return OperationResult<List<DTO_PhongLuuTruView>>.Ok(danhSachPhong);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<List<DTO_PhongLuuTruView>>.Fail(ex.Message);
            }
        }

        #endregion
    }
}



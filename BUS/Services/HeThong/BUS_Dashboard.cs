using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using ET.Constants;
using ET.DTOs;
using ET.Results;

namespace BUS.Services.HeThong
{

    public class BUS_Dashboard
    {
        #region Singleton

        public static BUS_Dashboard Instance { get; } = new BUS_Dashboard();

        #endregion

        #region giữ kết quả 5 phút

        private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(5);

        private DateTime _lastRefreshBanHang;
        private DTO_DashboardBanHang _cachedBanHang;

        private DateTime _lastRefreshLuuTru;
        private DTO_DashboardLuuTru _cachedLuuTru;

        private DateTime _lastRefreshKho;
        private DTO_DashboardKho _cachedKho;

        private DateTime _lastRefreshKhachHang;
        private DTO_DashboardKhachHang _cachedKhachHang;

        // Gọi khi user bấm "Làm mới".
        public void XoaCache()
        {
            _cachedBanHang = null;
            _cachedLuuTru = null;
            _cachedKho = null;
            _cachedKhachHang = null;
        }

        #endregion

        #region Bán hàng

        /// <summary>
        /// KPI bán hàng: đơn hàng hôm nay, doanh thu hôm nay, doanh thu tháng, đơn chờ thanh toán.
        /// Phạm vi mặc định: từ đầu tháng.
        /// </summary>
        public OperationResult<DTO_DashboardBanHang> LayThongKeBanHang()
        {
            if (_cachedBanHang != null && DateTime.Now - _lastRefreshBanHang < CacheDuration)
                return OperationResult<DTO_DashboardBanHang>.Ok(_cachedBanHang);

            try
            {
                using (var db = new DaiNamDBDataContext())
                {
                    var homNay = DateTime.Today;
                    var dauThang = new DateTime(homNay.Year, homNay.Month, 1);

                    var donHangs = db.DonHangs.Where(d => d.NgayTao >= dauThang).ToList();

                    var dto = new DTO_DashboardBanHang
                    {
                        SoDonHangHomNay = donHangs.Count(d => d.NgayTao >= homNay
                            && d.TrangThai == AppConstants.TrangThaiDonHang.DaThanhToan),
                        DoanhThuHomNay = donHangs
                            .Where(d => d.NgayTao >= homNay && d.TrangThai == AppConstants.TrangThaiDonHang.DaThanhToan)
                            .Sum(d => d.TongTienHang),
                        DoanhThuThang = donHangs
                            .Where(d => d.TrangThai == AppConstants.TrangThaiDonHang.DaThanhToan)
                            .Sum(d => d.TongTienHang),
                        SoDonChoThanhToan = donHangs
                            .Count(d => d.TrangThai == AppConstants.TrangThaiDonHang.ChoThanhToan)
                    };

                    _cachedBanHang = dto;
                    _lastRefreshBanHang = DateTime.Now;
                    return OperationResult<DTO_DashboardBanHang>.Ok(dto);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<DTO_DashboardBanHang>.Fail("ERR_DASHBOARD_BAN_HANG: " + ex.Message);
            }
        }

        #endregion

        #region Lưu trú

        // Tổng phòng, trống, đang ở, bảo trì.
        public OperationResult<DTO_DashboardLuuTru> LayThongKeLuuTru()
        {
            if (_cachedLuuTru != null && DateTime.Now - _lastRefreshLuuTru < CacheDuration)
                return OperationResult<DTO_DashboardLuuTru>.Ok(_cachedLuuTru);

            try
            {
                using (var db = new DaiNamDBDataContext())
                {
                    var phongs = db.Phongs.ToList();

                    var dto = new DTO_DashboardLuuTru
                    {
                        TongPhong = phongs.Count,
                        PhongTrong = phongs.Count(p => p.TrangThai == AppConstants.TrangThaiPhong.Trong),
                        PhongDangO = phongs.Count(p => p.TrangThai == AppConstants.TrangThaiPhong.DangO),
                        PhongBaoTri = phongs.Count(p => p.TrangThai == AppConstants.TrangThaiPhong.BaoTri)
                    };

                    _cachedLuuTru = dto;
                    _lastRefreshLuuTru = DateTime.Now;
                    return OperationResult<DTO_DashboardLuuTru>.Ok(dto);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<DTO_DashboardLuuTru>.Fail("ERR_DASHBOARD_LUU_TRU: " + ex.Message);
            }
        }

        #endregion

        #region Kho

        /// <summary>
        /// Tổng SP, sắp hết hàng, hết hàng hoàn toàn, phiếu chờ duyệt.
        /// Tồn kho = SUM(SoCai nhập) - SUM(SoCai xuất) trên SP.
        /// So sánh với MucTonToiThieu.MucCanhBao.
        /// </summary>
        public OperationResult<DTO_DashboardKho> LayThongKeKho()
        {
            if (_cachedKho != null && DateTime.Now - _lastRefreshKho < CacheDuration)
                return OperationResult<DTO_DashboardKho>.Ok(_cachedKho);

            try
            {
                using (var db = new DaiNamDBDataContext())
                {
                    int tongSP = db.SanPhams.Count(sp => sp.DaXoa == false);

                    // Tính tồn kho per SP: nhập (IdKhoNhap > 0) - xuất (IdKhoXuat > 0)
                    // SoCai: IdKhoNhap = kho nhận, IdKhoXuat = kho xuất
                    // Tồn kho = SUM where IdKhoNhap = kho - SUM where IdKhoXuat = kho
                    var nhapTheoSP = db.ChiTietChungTus
                        .Where(ct => ct.ChungTu.TrangThai == AppConstants.TrangThaiChungTuKho.DaDuyet
                                  && !ct.KhoHang1.LaKhoAo)
                        .GroupBy(ct => ct.IdSanPham)
                        .Select(g => new { IdSanPham = g.Key, Tong = g.Sum(x => x.SoLuong) })
                        .ToDictionary(x => x.IdSanPham, x => x.Tong);

                    var xuatTheoSP = db.ChiTietChungTus
                        .Where(ct => ct.ChungTu.TrangThai == AppConstants.TrangThaiChungTuKho.DaDuyet
                                  && !ct.KhoHang.LaKhoAo)
                        .GroupBy(ct => ct.IdSanPham)
                        .Select(g => new { IdSanPham = g.Key, Tong = g.Sum(x => x.SoLuong) })
                        .ToDictionary(x => x.IdSanPham, x => x.Tong);

                    var allIdSanPham = nhapTheoSP.Keys.Union(xuatTheoSP.Keys).ToList();
                    var tonKho = allIdSanPham.Select(id => new
                    {
                        IdSanPham = id,
                        TonHienTai = (nhapTheoSP.ContainsKey(id) ? nhapTheoSP[id] : 0m)
                                   - (xuatTheoSP.ContainsKey(id) ? xuatTheoSP[id] : 0m)
                    }).ToList();

                    // Lấy mức cảnh báo
                    var mucCanhBao = db.MucTonToiThieus
                        .Where(m => m.TrangThai)
                        .GroupBy(m => m.IdSanPham)
                        .Select(g => new { IdSanPham = g.Key, MucMin = g.Min(m => m.MucCanhBao) })
                        .ToDictionary(x => x.IdSanPham, x => x.MucMin);

                    int sapHet = 0;
                    int hetHoan = 0;

                    foreach (var sp in tonKho)
                    {
                        if (sp.TonHienTai <= 0)
                        {
                            hetHoan++;
                        }
                        else if (mucCanhBao.ContainsKey(sp.IdSanPham) && sp.TonHienTai <= mucCanhBao[sp.IdSanPham])
                        {
                            sapHet++;
                        }
                    }

                    int phieuChoDuyet = db.ChungTus
                        .Count(ct => ct.TrangThai == AppConstants.TrangThaiChungTuKho.ChoDuyet
                                  || ct.TrangThai == AppConstants.TrangThaiChungTuKho.ChoDuyet2);

                    var dto = new DTO_DashboardKho
                    {
                        TongSanPham = tongSP,
                        SapHetHang = sapHet,
                        HetHangHoanToan = hetHoan,
                        PhieuChoDuyet = phieuChoDuyet
                    };

                    _cachedKho = dto;
                    _lastRefreshKho = DateTime.Now;
                    return OperationResult<DTO_DashboardKho>.Ok(dto);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<DTO_DashboardKho>.Fail("ERR_DASHBOARD_KHO: " + ex.Message);
            }
        }

        #endregion

        #region Khách hàng

        // Tổng khách hàng, khách mới trong tháng.
        public OperationResult<DTO_DashboardKhachHang> LayThongKeKhachHang()
        {
            if (_cachedKhachHang != null && DateTime.Now - _lastRefreshKhachHang < CacheDuration)
                return OperationResult<DTO_DashboardKhachHang>.Ok(_cachedKhachHang);

            try
            {
                using (var db = new DaiNamDBDataContext())
                {
                    var dauThang = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

                    // KhachHang FK -> ThongTin (IdDoiTac = ThongTin.Id). NgayTao nằm ở ThongTin.
                    var khachHangs = db.KhachHangs.ToList();

                    var dto = new DTO_DashboardKhachHang
                    {
                        TongKhachHang = khachHangs.Count,
                        KhachMoiThang = db.ThongTins
                            .Where(t => t.LoaiDoiTac == "KhachHang" && t.NgayTao >= dauThang && t.DaXoa == false)
                            .Count()
                    };

                    _cachedKhachHang = dto;
                    _lastRefreshKhachHang = DateTime.Now;
                    return OperationResult<DTO_DashboardKhachHang>.Ok(dto);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<DTO_DashboardKhachHang>.Fail("ERR_DASHBOARD_KHACH_HANG: " + ex.Message);
            }
        }

        #endregion
    }
}



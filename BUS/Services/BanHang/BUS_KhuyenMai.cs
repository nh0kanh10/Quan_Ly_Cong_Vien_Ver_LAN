using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Repositories.BanHang;
using ET.Constants;
using ET.DTOs;
using ET.Results;

namespace BUS.Services.BanHang
{
    /// Nghiệp vụ khuyến mãi: CRUD + tính giảm giá best-deal cho POS.
    public class BUS_KhuyenMai
    {
        #region Khởi tạo (Singleton)

        public static BUS_KhuyenMai Instance { get; } = new BUS_KhuyenMai();

        #endregion

        #region Truy vấn dữ liệu

        public OperationResult LayDanhSach()
        {
            var list = DAL_KhuyenMai.Instance.LayTatCa();
            return OperationResult.Ok(list);
        }

        #endregion

        #region Thêm / Sửa / Xoá

        public OperationResult ThemKhuyenMai(DTO_KhuyenMaiPOS dto, int nguoiTao)
        {
            var loi = ValidateDuLieu(dto);
            if (loi != null) return OperationResult.Fail(loi);

            if (TrungMa(dto.MaKhuyenMai, 0))
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_KM_TRUNG_MA);

            var id = DAL_KhuyenMai.Instance.ThemKhuyenMai(dto, nguoiTao);
            return OperationResult.Ok(id);
        }

        public OperationResult CapNhatKhuyenMai(DTO_KhuyenMaiPOS dto)
        {
            var loi = ValidateDuLieu(dto);
            if (loi != null) return OperationResult.Fail(loi);

            if (TrungMa(dto.MaKhuyenMai, dto.Id))
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_KM_TRUNG_MA);

            DAL_KhuyenMai.Instance.CapNhatKhuyenMai(dto);
            return OperationResult.Ok();
        }

        public OperationResult XoaKhuyenMai(int id)
        {
            DAL_KhuyenMai.Instance.XoaMem(id);
            return OperationResult.Ok();
        }

        public OperationResult LuuDieuKien(int idKhuyenMai, List<DTO_DieuKienKM> danhSach)
        {
            DAL_KhuyenMai.Instance.LuuDieuKien(idKhuyenMai, danhSach ?? new List<DTO_DieuKienKM>());
            return OperationResult.Ok();
        }

        #endregion

        #region Nghiệp vụ đặc thù

        public OperationResult ApDungMa(string maKM, decimal tongTienHang, string hangThanhVien)
        {
            if (string.IsNullOrWhiteSpace(maKM))
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_KM_EMPTY);

            var km = DAL_KhuyenMai.Instance.TimTheoMa(maKM);
            if (km == null)
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_KM_NOT_FOUND);

            if (km.SoLanToiDa.HasValue && km.SoLanDaDung >= km.SoLanToiDa.Value)
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_KM_HET_LUOT);

            if (tongTienHang < km.DonToiThieu)
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_KM_DON_TOI_THIEU);

            if (!KiemTraDieuKien(km, hangThanhVien))
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_KM_DIEU_KIEN);

            km.SoTienGiamThucTe = TinhGiaTriGiam(km, tongTienHang);
            return OperationResult.Ok(km);
        }

        /// <summary>
        /// Tìm tổ hợp KM tốt nhất cho đơn hàng (auto-promotion).
        ///
        /// Thuật toán best-deal(nào tốt thì lấy):
        ///   1. Lọc các KM đang active và thỏa điều kiện.
        ///   2. Tách thành 2 nhóm: stackable (CoChongCheo=true) và exclusive (CoChongCheo=false).
        ///   3. Stackable luôn được cộng dồn với nhau.
        ///   4. Với mỗi exclusive, tính: exclusive.Giam + tổng stackable.
        ///   5. Chọn tổ hợp có tổng giảm cao nhất — lợi nhất cho khách.
        /// </summary>
        public List<DTO_KhuyenMaiPOS> TimToHopBestDeal(decimal tongTienHang, string hangThanhVien)
        {
            var hopLe = DAL_KhuyenMai.Instance.LayDanhSachActive()
                .Where(km => (!km.SoLanToiDa.HasValue || km.SoLanDaDung < km.SoLanToiDa.Value)
                          && tongTienHang >= km.DonToiThieu
                          && KiemTraDieuKien(km, hangThanhVien))
                .ToList();

            if (hopLe.Count == 0) return new List<DTO_KhuyenMaiPOS>();

            var stackable = hopLe.Where(k => k.CoChongCheo).ToList();
            var exclusive = hopLe.Where(k => !k.CoChongCheo).ToList();

            foreach (var km in hopLe)
                km.SoTienGiamThucTe = TinhGiaTriGiam(km, tongTienHang);

            decimal giamStackable = stackable.Sum(k => k.SoTienGiamThucTe);

            // So sánh: chỉ stackable (Có Chồng Chéo = true) vs mỗi tổ hợp exclusive (Có Chồng Chéo = false) + stackable
            decimal bestTong = giamStackable;
            DTO_KhuyenMaiPOS bestExclusive = null;

            foreach (var ex in exclusive)
            {
                decimal tong = ex.SoTienGiamThucTe + giamStackable;
                if (tong > bestTong)
                {
                    bestTong = tong;
                    bestExclusive = ex;
                }
            }

            var ketQua = new List<DTO_KhuyenMaiPOS>(stackable);
            if (bestExclusive != null) ketQua.Add(bestExclusive);
            return ketQua;
        }

        public DTO_KhuyenMaiPOS GetPromotionHint(decimal tongTienHang, string hangThanhVien)
        {
            return DAL_KhuyenMai.Instance.LayDanhSachActive()
                .Where(km => (!km.SoLanToiDa.HasValue || km.SoLanDaDung < km.SoLanToiDa.Value)
                          && km.DonToiThieu > tongTienHang
                          && KiemTraDieuKien(km, hangThanhVien))
                .OrderBy(km => km.DonToiThieu - tongTienHang)
                .FirstOrDefault();
        }

        public decimal TinhGiaTriGiam(DTO_KhuyenMaiPOS km, decimal tongTien)
        {
            decimal giam = 0;
            switch (km.LoaiGiamGia)
            {
                case AppConstants.LoaiGiamGia.PhanTram:
                    giam = (tongTien * km.GiaTriGiam / 100);
                    break;
                case AppConstants.LoaiGiamGia.SoTien:
                    giam = km.GiaTriGiam;
                    break;
            }

            // Làm tròn về số nguyên
            giam = Math.Floor(giam);

            // Loại bỏ các khoản giảm giá "rác" cực nhỏ (ví dụ 4đ, 5đ) do sai số tỉ lệ hoặc dữ liệu test
            if (giam < 10) giam = 0;

            return Math.Min(giam, tongTien);
        }

        #endregion

        #region Hàm hỗ trợ (private)

        private bool KiemTraDieuKien(DTO_KhuyenMaiPOS km, string hangThanhVien)
        {
            if (km.DieuKiens == null || km.DieuKiens.Count == 0) return true;

            // Tất cả điều kiện phải thỏa mãn and 
            foreach (var dk in km.DieuKiens)
            {
                if (dk.LoaiDieuKien == AppConstants.LoaiDieuKienKM.HangThanhVien)
                {
                    if (string.IsNullOrEmpty(hangThanhVien)) return false;
                    if (dk.PhepSo == AppConstants.PhepSoKM.BangNhau && dk.GiaTriDieuKien != hangThanhVien) return false;
                    if (dk.PhepSo == AppConstants.PhepSoKM.IN && !dk.GiaTriDieuKien.Split(',').Contains(hangThanhVien)) return false;
                }
                else
                {
                    // Loại điều kiện chưa được cài đặt → từ chối cho an toàn, tránh lọt KM không hợp lệ
                    return false;
                }
            }
            return true;
        }

        private string ValidateDuLieu(DTO_KhuyenMaiPOS dto)
        {
            if (string.IsNullOrWhiteSpace(dto.MaKhuyenMai))  return AppConstants.ErrorMessages.ERR_KM_MA_RONG;
            if (string.IsNullOrWhiteSpace(dto.TenKhuyenMai)) return AppConstants.ErrorMessages.ERR_KM_TEN_RONG;
            if (dto.GiaTriGiam <= 0)                          return AppConstants.ErrorMessages.ERR_KM_GIA_TRI_SAI;

            if (dto.LoaiGiamGia == AppConstants.LoaiGiamGia.PhanTram && dto.GiaTriGiam > 100)
                return AppConstants.ErrorMessages.ERR_KM_PHANTRAM_VUOT;

            if (dto.NgayBatDau >= dto.NgayKetThuc)
                return AppConstants.ErrorMessages.ERR_KM_NGAY_SAI;

            return null;
        }

        private bool TrungMa(string ma, int idHienTai)
        {
            var list = DAL_KhuyenMai.Instance.LayTatCa();
            return list.Any(k => k.MaKhuyenMai == ma && k.Id != idHienTai);
        }

        #endregion
    }
}



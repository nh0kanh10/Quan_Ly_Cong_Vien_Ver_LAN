using ET.DTOs;
using ET.Results;
using ET.Constants;
using DAL.Repositories.BanHang;
using System;
using System.Linq;

namespace BUS.Services.BanHang
{
    public class BUS_HoanHang
    {
        public static BUS_HoanHang Instance { get; } = new BUS_HoanHang();

        /// Lấy Đơn Hàng Hoàn với Kiểm tra
        public DTO_DonHangHoan LayDonHangHoan(string maDonHang)
        {
            if (string.IsNullOrWhiteSpace(maDonHang)) return null;
            return DAL_HoanHang.Instance.LayDonHangHoan(maDonHang);
        }

        /// Thực thi Hoàn Hàng với Kiểm tra 
        public OperationResult ThucHienHoanHang(DTO_HoanHangRequest request)
        {
            if (request == null || request.IdDonHang <= 0 || request.IdNguoiDuyet <= 0 || request.IdKhoMacDinh <= 0)
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_HOAN_INVALID_REQUEST);

            if (request.ChiTietHoan == null || !request.ChiTietHoan.Any(x => x.SoLuongMuonHoan > 0))
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_HOAN_NO_ITEMS);

            // Load cấu hình giới hạn hoàn trả (giờ)
            decimal gioVe = BUS_CauHinh.Instance.LayGiaTriDecimal(AppConstants.ConfigKeys.HOAN_GIOI_HAN_GIO_VE, 0);
            decimal gioHangHoa = BUS_CauHinh.Instance.LayGiaTriDecimal(AppConstants.ConfigKeys.HOAN_GIOI_HAN_GIO_HANGHOA, 168);
            double soGioDaQua = (DateTime.Now - request.NgayTaoDonHang).TotalHours;

            foreach (var item in request.ChiTietHoan.Where(x => x.SoLuongMuonHoan > 0))
            {
                if (item.SoLuongMuonHoan > (item.SoLuongMua - item.SoLuongDaHoan))
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_REFUND_OVER_QTY);
                
                if (string.IsNullOrWhiteSpace(item.LyDoHoan))
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_HOAN_REASON_REQUIRED);

                // Giới hạn thời gian hoàn theo loại sản phẩm
                bool laVe = item.LoaiSanPham == AppConstants.LoaiSanPham.VeVaoKhu 
                         || item.LoaiSanPham == AppConstants.LoaiSanPham.VeTroChoi;

                if (laVe)
                {
                    // Vé: mặc định 0 = cấm hoàn. Nếu > 0 thì check giờ.
                    if (gioVe <= 0)
                        return OperationResult.Fail(AppConstants.ErrorMessages.ERR_HOAN_VE_KHONG_DUOC_HOAN);
                    if (soGioDaQua > (double)gioVe)
                        return OperationResult.Fail(AppConstants.ErrorMessages.ERR_HOAN_QUA_HAN);
                }
                else
                {
                    // Hàng hóa / Dịch vụ: check giờ (168h = 7 ngày mặc định)
                    if (gioHangHoa > 0 && soGioDaQua > (double)gioHangHoa)
                        return OperationResult.Fail(AppConstants.ErrorMessages.ERR_HOAN_QUA_HAN);
                }
            }

            return DAL_HoanHang.Instance.ThucHienHoanHang(request);
        }
    }
}



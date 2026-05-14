using ET.Constants;
using System;
using System.Collections.Generic;
using ET.Models.Kho;
using DAL.Repositories.Kho;
using ET.Results;

namespace BUS.Services.Kho
{
    public class BUS_TamGiuTonKho
    {
        #region Khởi tạo
        
        public static BUS_TamGiuTonKho Instance { get; } = new BUS_TamGiuTonKho();
        
        #endregion

        #region Truy vấn dữ liệu

        /// <summary>
        /// Truy vấn tổng số lượng đang bị người khác xí mất của một mặt hàng cụ thể.
        /// </summary>
        /// <param name="idKho">Kho cần xác minh</param>
        /// <param name="idSanPham">Sản phẩm cần kiểm tra</param>
        /// <returns>Dung lượng bị giam giữ</returns>
        public decimal GetTongSoLuongDangGiu(int idKho, int idSanPham)
        {
            return DAL_TamGiuTonKho.Instance.GetTongSoLuongDangGiu(idKho, idSanPham);
        }

        #endregion

        #region Thêm / Sửa / Xóa

        /// <summary>
        /// Thực thi lệnh chiếm giữ một lượng hàng cho tới khi kết thúc giao dịch đặt hàng qua ví RFID.
        /// </summary>
        /// <param name="et">Thông tin lệnh khoá giữ lượng</param>
        /// <returns>Sự thành công đi kèm hay thông báo lỗi từ kho</returns>
        public OperationResult LuuThongTinGiuKho(ET_TamGiuTonKho et)
        {
            if ((et.IdDonHangNhap ?? 0) <= 0)
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_GIU_THIEU_DONHANG);

            if (et.IdSanPham <= 0 || et.IdKho <= 0)
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_GIU_THIEU_SANPHAM);

            if (et.SoLuongGiu <= 0)
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_GIU_SO_LUONG_AM);

            if (et.HetHan <= DateTime.Now)
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_GIU_THOIGIAN_SAI);

            try
            {
                DAL_TamGiuTonKho.Instance.LuuThongTinGiuKho(et);
                return OperationResult.Ok("MSG_GIU_LUU_OK");
            }
            catch (Exception ex)
            {
                return OperationResult.Fail("ERR_SYSTEM_FAIL|" + ex.Message);
            }
        }

        /// <summary>
        /// Hủy chiếm dụng hàng hoá do quá lâu khách không thanh toán hoặc quay xe bỏ đi.
        /// </summary>
        /// <param name="idDonHangNhap">Mã đơn hàng nhập nguồn tham chiếu</param>
        /// <returns>Giao dịch có loại bỏ giữ thành công hay không</returns>
        public OperationResult XaTonKho(int idDonHangNhap)
        {
            try
            {
                DAL_TamGiuTonKho.Instance.XaTonKho(idDonHangNhap);
                return OperationResult.Ok("MSG_GIU_XOA_OK");
            }
            catch (Exception ex)
            {
                return OperationResult.Fail("ERR_SYSTEM_FAIL|" + ex.Message);
            }
        }

        #endregion

        #region Nghiệp vụ đặc thù

        /// <summary>
        /// Công thức cực quan trọng cho máy cà thẻ POS.
        /// Tồn Khả Dụng = Tồn Kho Hiện Tại (trong sổ Cái) - Tổng Lượng Đang Bị Giữ.
        /// Dùng để nhân viên quầy báo cho khách biết kho còn thực tế bán nổi hay không.
        /// </summary>
        /// <param name="idKho">Mã khu vực xuất phát</param>
        /// <param name="idSanPham">Sản phẩm được mua</param>
        /// <param name="idLoHang">Mã đợt nhập (nếu có)</param>
        /// <returns>Con số sạch sẽ để bán mang về</returns>
        public decimal GetTonKhoKhaDung(int idKho, int idSanPham, int? idLoHang = null)
        {
            decimal tonThucTe = BUS_SoCai.Instance.GetTonKhoHienTai(idKho, idSanPham, idLoHang);
            decimal dangGiu = GetTongSoLuongDangGiu(idKho, idSanPham);

            return tonThucTe - dangGiu;
        }

        #endregion
    }
}



using ET.Constants;
using System;
using System.Collections.Generic;
using ET.Models.Kho;
using DAL.Repositories.Kho;
using ET.Results;

namespace BUS.Services.Kho
{
    public class BUS_LoHang
    {
        #region Khởi tạo
        
        public static BUS_LoHang Instance { get; } = new BUS_LoHang();
        
        #endregion

        #region Truy vấn dữ liệu

        /// <summary>
        /// Lấy toàn bộ các lô của một loại sản phẩm cho vào danh sách hiển thị.
        /// </summary>
        /// <param name="idSanPham">Mã sản phẩm kiểm tra</param>
        /// <param name="chiLayLoHoatDong">Mặc định lấy tất cả</param>
        /// <returns>Danh sách chứa kết quả phù hợp</returns>
        public List<ET_LoHang> GetDanhSachBySanPham(int idSanPham, bool chiLayLoHoatDong = false)
        {
            return DAL_LoHang.Instance.GetDanhSachBySanPham(idSanPham, chiLayLoHoatDong);
        }

        /// Lấy danh sách lô sắp hết hạn trong N ngày tới.
        public List<ET_LoHang> GetLoSapHetHan(int soNgay)
        {
            return DAL_LoHang.Instance.GetLoSapHetHan(soNgay);
        }

        #endregion

        #region Thêm / Sửa / Xóa

        /// <summary>
        /// Xác thực thông tin thời gian hợp lý trước khi nạp xuống kho dữ liệu để lưu vào hệ thống.
        /// </summary>
        /// <param name="et">Dữ liệu lô hàng sinh ra từ giao diện</param>
        /// <returns>Sự thành công kèm thông báo hoặc đối tượng báo lỗi</returns>
        public OperationResult LuuLoHang(ET_LoHang et)
        {
            if (string.IsNullOrWhiteSpace(et.MaLoHang))
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_LOHANG_MA_RONG);

            if (et.IdSanPham <= 0)
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_LOHANG_THIEU_SANPHAM);

            if (et.NgaySanXuat > DateTime.Now)
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_LOHANG_NSX_TUONGLAI);

            if (et.NgayHetHan.HasValue && et.NgaySanXuat.HasValue && et.NgayHetHan <= et.NgaySanXuat)
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_LOHANG_HSD_NHO_HON_NSX);

            try
            {
                DAL_LoHang.Instance.LuuLoHang(et);
                return OperationResult.Ok("MSG_LOHANG_LUU_OK");
            }
            catch (Exception ex)
            {
                return OperationResult.Fail("ERR_SYSTEM_FAIL|" + ex.Message);
            }
        }

        #endregion

        #region Nghiệp vụ đặc thù

        /// <summary>
        /// Kiểm tra nhanh xem lô hàng này đã quá hạn sử dụng hay chưa.
        /// Thường gọi hàm này khi người dùng xuất hàng từ trong kho.
        /// </summary>
        /// <param name="idLoHang">Mã nhóm lô cần đánh giá</param>
        /// <returns>Trạng thái đúng nếu thời gian hiện hành vượt ngưỡng hạn</returns>
        public bool KiemTraHetHan(int idLoHang)
        {
            var lo = DAL_LoHang.Instance.GetChiTiet(idLoHang);
            if (lo == null || !lo.NgayHetHan.HasValue) return false;

            return DateTime.Now.Date > lo.NgayHetHan.Value;
        }

        #endregion
    }
}



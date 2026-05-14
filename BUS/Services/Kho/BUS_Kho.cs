using System;
using System.Collections.Generic;
using System.Linq;
using ET.Constants;
using ET.Models.Kho;
using DAL.Repositories.Kho;
using ET.Results;

namespace BUS.Services.Kho
{
    public class BUS_Kho
    {
        #region Khởi tạo
        
        public static BUS_Kho Instance { get; } = new BUS_Kho();
        
        #endregion

        #region Truy vấn dữ liệu

        public List<ET_KhoHang> GetAllKho(string langCode = "vi-VN")
        {
            return DAL_Kho.Instance.GetAllKho(langCode);
        }

        // Chỉ lấy kho đang hoạt động (dùng cho dropdown chọn kho trong ucTaoPhieu, POS...)
        public List<ET_KhoHang> GetKhoHoatDong(string langCode = "vi-VN")
        {
            return DAL_Kho.Instance.GetAllKho(langCode)
                .Where(k => k.TrangThai == AppConstants.TrangThaiKho.HoatDong && !k.LaKhoAo)
                .ToList();
        }

        public List<ET_MucTonToiThieu> GetMucTonToiThieu(int idSanPham)
        {
            return DAL_Kho.Instance.GetMucTonToiThieu(idSanPham);
        }

        public ET_KhoHang GetById(int id)
        {
            return DAL_Kho.Instance.GetById(id);
        }

        #endregion

        #region Thêm / Sửa / Xoá

        public OperationResult CapNhatKho(ET_KhoHang kho)
        {
            if (string.IsNullOrWhiteSpace(kho.MaKho))
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_KHO_MA_RONG);
                
            if (string.IsNullOrWhiteSpace(kho.TenKho))
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_KHO_TEN_RONG);

            try
            {
                DAL_Kho.Instance.CapNhatKho(kho);
                return OperationResult.Ok("MSG_KHO_LUU_OK");
            }
            catch (Exception ex)
            {
                // Báo lỗi nếu trùng mã kho nhân viên tự gõ
                if (ex.Message.Contains("UNIQUE") || ex.Message.Contains("IX_"))
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_KHO_TRUNG_MA);
                
                return OperationResult.Fail("ERR_SYSTEM_FAIL|" + ex.Message);
            }
        }

        /// <summary>
        /// Xoá kho. Nếu kho đã lưu dữ liệu mua bán trong sổ, thì chỉ được chuyển trạng thái sang "Ngưng hoạt động".
        /// Xoá cứng (dòng lện delete) sẽ làm mất toàn bộ lịch sử bán hàng liên quan.
        /// </summary>
        public OperationResult XoaKho(int idKho)
        {
            if (DAL_Kho.Instance.KhoDaPhatSinhGiaoDich(idKho))
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_KHO_DA_CO_GIAO_DICH);

            try
            {
                var kho = DAL_Kho.Instance.GetById(idKho);
                if (kho == null)
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_KHO_KHONG_XOA_AO);

                if (kho.LaKhoAo)
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_KHO_KHONG_XOA_AO);

                kho.TrangThai = AppConstants.TrangThaiKho.NgungHoatDong;
                DAL_Kho.Instance.CapNhatKho(kho);
                return OperationResult.Ok("MSG_KHO_XOA_OK");
            }
            catch (Exception ex)
            {
                return OperationResult.Fail("ERR_SYSTEM_FAIL|" + ex.Message);
            }
        }

        public OperationResult CapNhatMucTon(int idSanPham, int idKho, decimal mucCanhBao)
        {
            if (mucCanhBao < 0)
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_KHO_MUC_TON_AM);

            try
            {
                DAL_Kho.Instance.CapNhatMucTon(idSanPham, idKho, mucCanhBao);
                return OperationResult.Ok("MSG_KHO_MUC_TON_OK");
            }
            catch (Exception ex)
            {
                return OperationResult.Fail("ERR_SYSTEM_FAIL|" + ex.Message);
            }
        }

        #endregion

        #region Nghiệp vụ đặc thù 
        #endregion
    }
}



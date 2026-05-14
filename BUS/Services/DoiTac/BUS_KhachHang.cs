using ET.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Repositories.DoiTac;
using ET.DTOs;
using ET.Models.DoiTac;
using ET.Results;

namespace BUS.Services.DoiTac
{
    public class BUS_KhachHang
    {
        #region Khởi tạo (Singleton)

        public static BUS_KhachHang Instance { get; } = new BUS_KhachHang();

        private readonly DAL_KhachHang _dal = DAL_KhachHang.Instance;

        #endregion

        #region Truy vấn dữ liệu

        public List<DTO_KhachHangChiTiet> LayDanhSach(string tuKhoa = null)
        {
            return _dal.LayDanhSach(tuKhoa);
        }

        public DTO_KhachHangChiTiet LayChiTiet(int idDoiTac)
        {
            return _dal.LayChiTiet(idDoiTac);
        }

        public OperationResult TimKhachHang(string tuKhoa, string langCode = "vi-VN")
        {
            if (string.IsNullOrWhiteSpace(tuKhoa))
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_CUST_EMPTY_KEYWORD);

            try
            {
                var kh = _dal.TimKhachHang(tuKhoa, langCode);
                if (kh == null)
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_CUST_NOT_FOUND);

                return OperationResult.Ok(kh);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("BUS_KhachHang.TimKhachHang: " + ex.Message);
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_SYSTEM_FAIL);
            }
        }

        public OperationResult TimTheoRFID(string maThe, string langCode = "vi-VN")
        {
            if (string.IsNullOrWhiteSpace(maThe))
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_CUST_EMPTY_KEYWORD);

            try
            {
                var kh = _dal.TimTheoRFID(maThe, langCode);
                if (kh == null)
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_CUST_RFID_NOT_FOUND);

                return OperationResult.Ok(kh);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("BUS_KhachHang.TimTheoRFID: " + ex.Message);
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_SYSTEM_FAIL);
            }
        }

        #endregion

        #region Thêm / Sửa / Xoá

        public OperationResult ThemMoi(DTO_KhachHangChiTiet dto)
        {
            var ketQua = KiemTraBatBuoc(dto);
            if (!ketQua.Success) return ketQua;

            if (_dal.KiemTraTrungSDT(dto.DienThoai))
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_KH_SDT_TRUNG);

            if (!string.IsNullOrWhiteSpace(dto.Cccd) && _dal.KiemTraTrungCCCD(dto.Cccd))
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_KH_CCCD_TRUNG);

            try
            {
                dto.MaKhachHang = _dal.TaoMaKhachHang();
                int newId = _dal.Them(dto);
                return OperationResult.Ok(newId, "MSG_KH_LUU_OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("BUS_KhachHang.ThemMoi: " + ex.Message);
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_SYSTEM_FAIL);
            }
        }

        public OperationResult CapNhat(DTO_KhachHangChiTiet dto)
        {
            var ketQua = KiemTraBatBuoc(dto);
            if (!ketQua.Success) return ketQua;

            if (_dal.KiemTraTrungSDT(dto.DienThoai, dto.IdDoiTac))
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_KH_SDT_TRUNG);

            if (!string.IsNullOrWhiteSpace(dto.Cccd) && _dal.KiemTraTrungCCCD(dto.Cccd, dto.IdDoiTac))
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_KH_CCCD_TRUNG);

            try
            {
                _dal.CapNhat(dto);
                return OperationResult.Ok("MSG_KH_LUU_OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("BUS_KhachHang.CapNhat: " + ex.Message);
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_SYSTEM_FAIL);
            }
        }

        public OperationResult XoaMem(int idDoiTac)
        {
            try
            {
                _dal.XoaMem(idDoiTac);
                return OperationResult.Ok("MSG_KH_XOA_OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("BUS_KhachHang.XoaMem: " + ex.Message);
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_SYSTEM_FAIL);
            }
        }

        #endregion

        #region Nghiệp vụ đặc thù

        private OperationResult KiemTraBatBuoc(DTO_KhachHangChiTiet dto)
        {
            if (string.IsNullOrWhiteSpace(dto.HoTen))
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_KH_TEN_RONG);

            if (string.IsNullOrWhiteSpace(dto.DienThoai))
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_KH_SDT_RONG);

            return OperationResult.Ok();
        }

        #endregion
    }
}



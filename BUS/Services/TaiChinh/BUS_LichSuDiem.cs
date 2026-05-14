using ET.Constants;
using System;
using System.Collections.Generic;
using DAL.Repositories.TaiChinh;
using ET.Models.TaiChinh;
using ET.Results;

namespace BUS.Services.TaiChinh
{
    public class BUS_LichSuDiem
    {
        #region Khởi tạo (Singleton)

        public static BUS_LichSuDiem Instance { get; } = new BUS_LichSuDiem();

        private readonly DAL_LichSuDiem _dal = DAL_LichSuDiem.Instance;

        #endregion

        #region Truy vấn dữ liệu

        public List<ET_LichSuDiem> LayLichSu(int idKhachHang)
        {
            return _dal.LayLichSu(idKhachHang);
        }

        public int LaySoDiem(int idKhachHang)
        {
            return _dal.LaySoDiem(idKhachHang);
        }

        #endregion

        #region Nghiệp vụ đặc thù


        /// Tính SoDuSauGD = SoDuHienTai + soDiem.
        /// DAL chỉ INSERT dòng mới, không biết tính SoDuSauGD.
        public OperationResult CongDiem(int idKhachHang, int soDiem, int? idDonHang, string moTa)
        {
            if (soDiem <= 0)
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_DIEM_SO_AM);

            try
            {
                int soDuHienTai = _dal.LaySoDiem(idKhachHang);
                int soDuSauGD = soDuHienTai + soDiem;

                _dal.ThemGiaoDich(idKhachHang, "Cong", soDiem, soDuSauGD, idDonHang, moTa);
                return OperationResult.Ok("MSG_DIEM_CONG_OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("BUS_LichSuDiem.CongDiem: " + ex.Message);
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_SYSTEM_FAIL);
            }
        }

        /// Trừ điểm (đổi quà, trừ promotion...).
        /// Kiểm tra: SoDuHienTai >= soDiem. Nếu không đủ -> từ chối.
        public OperationResult TruDiem(int idKhachHang, int soDiem, string moTa)
        {
            if (soDiem <= 0)
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_DIEM_SO_AM);

            try
            {
                int soDuHienTai = _dal.LaySoDiem(idKhachHang);
                if (soDuHienTai < soDiem)
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_DIEM_KHONG_DU);

                int soDuSauGD = soDuHienTai - soDiem;
                _dal.ThemGiaoDich(idKhachHang, "Tru", -soDiem, soDuSauGD, null, moTa);
                return OperationResult.Ok("MSG_DIEM_TRU_OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("BUS_LichSuDiem.TruDiem: " + ex.Message);
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_SYSTEM_FAIL);
            }
        }

        #endregion
    }
}



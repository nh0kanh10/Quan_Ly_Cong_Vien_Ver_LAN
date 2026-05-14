using System;
using DAL.Repositories.BanHang;
using ET.Models.BanHang;
using ET.Results;

namespace BUS.Services.BanHang
{
    public class BUS_PhienThuNgan
    {
        #region Khởi tạo (hoặc cấu trúc Singleton)
        
        public static BUS_PhienThuNgan Instance { get; } = new BUS_PhienThuNgan();

        #endregion

        #region Truy vấn dữ liệu

        /// <summary>
        /// Lấy phiên thu ngân hiện tại đang mở của nhân viên
        /// </summary>
        public OperationResult LayPhienDangMo(int idNhanVien)
        {
            try
            {
                var data = DAL_PhienThuNgan.Instance.GetPhienDangMo(idNhanVien);
                if (data == null)
                    return OperationResult.Fail(ET.Constants.AppConstants.ErrorMessages.ERR_POS_NO_OPEN_SESSION);

                return OperationResult.Ok(data);
            }
            catch (Exception ex)
            {
                return OperationResult.Fail(ex.Message);
            }
        }

        #endregion

        #region Nghiệp vụ đặc thù

        /// Mở phiên thu ngân mới, kiểm tra xem máy POS đã bị ai chiếm dụng chưa
        public OperationResult MoPhienMoi(ET_PhienThuNgan phien)
        {
            try
            {
                // Kiểm tra xem máy này có đang bị nhân viên khác mở mà chưa đóng không
                bool biChiemDung = DAL_PhienThuNgan.Instance.MayDangCoPhienKhac(phien.IdMayBan, phien.IdNhanVien);
                if (biChiemDung)
                    return OperationResult.Fail(ET.Constants.AppConstants.ErrorMessages.ERR_POS_MACHINE_IN_USE);

                // Kiểm tra xem nhân viên này đã có phiên nào chưa đóng không
                var phienHienTai = DAL_PhienThuNgan.Instance.GetPhienDangMo(phien.IdNhanVien);
                if (phienHienTai != null)
                    return OperationResult.Fail(ET.Constants.AppConstants.ErrorMessages.ERR_POS_ALREADY_OPENED_SESSION);

                int newId = DAL_PhienThuNgan.Instance.MoPhien(phien);
                phien.Id = newId;

                return OperationResult.Ok(phien, "MSG_POS_SESSION_OPENED");
            }
            catch (Exception ex)
            {
                return OperationResult.Fail(ex.Message);
            }
        }

        public OperationResult DongPhien(int idPhien, decimal tienCuoiCaThucTeCuaNhanVienDong, string ghiChu)
        {
            try
            {
                if (idPhien <= 0)
                    return OperationResult.Fail(ET.Constants.AppConstants.ErrorMessages.ERR_POS_INVALID_SESSION_ID);

                DAL_PhienThuNgan.Instance.DongPhien(idPhien, tienCuoiCaThucTeCuaNhanVienDong, ghiChu);

                // Đọc lại phiên đã đóng để lấy TongThuTrongCa + tính ChenhLech
                var phienDaDong = DAL_PhienThuNgan.Instance.LayPhienDaDong(idPhien);
                return OperationResult.Ok(phienDaDong, "MSG_POS_SESSION_CLOSED");
            }
            catch (Exception ex)
            {
                return OperationResult.Fail(ex.Message);
            }
        }

        #endregion
    }
}



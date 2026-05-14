using ET.Constants;
using System;
using System.Security.Cryptography;
using System.Text;
using DAL.Repositories.DoiTac;
using ET.Results;
using ET.DTOs;

namespace BUS.Services.HeThong
{
    public class BUS_Auth
    {
        #region Khởi tạo (Singleton)
        public static BUS_Auth Instance { get; } = new BUS_Auth();

        private readonly DAL_TaiKhoan _dal = DAL_TaiKhoan.Instance;
        #endregion

        #region Nghiệp vụ đăng nhập

        /// Băm chuỗi mật khẩu thô bằng SHA256 
        public string HashPassword(string rawPassword)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawPassword));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2")); 
                }
                return builder.ToString();
            }
        }

        /// Xử lý logic đăng nhập, xác thực DB và nạp SessionManager.
        public OperationResult DangNhap(string username, string rawPassword)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(rawPassword))
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_AUTH_EMPTY);

            try
            {
                var taiKhoan = _dal.KiemTraDangNhap(username);
                if (taiKhoan == null)
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_AUTH_WRONG_USER_PASS);

                string hashInput = HashPassword(rawPassword);

                if (taiKhoan.MatKhauHash != hashInput && taiKhoan.MatKhauHash != rawPassword)
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_AUTH_WRONG_USER_PASS);

                var thongTin = _dal.LayThongTinChiTiet(taiKhoan.IdDoiTac);
                if (thongTin == null)
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_AUTH_PROFILE_MISSING);

                var dsQuyen = _dal.LayQuyenHan(thongTin.IdVaiTro);

                var sessionDto = new DTO_Session
                {
                    IdDoiTac = taiKhoan.IdDoiTac,
                    MaDoiTac = thongTin.MaNhanVien,
                    HoTen = thongTin.HoTen,
                    LoaiTaiKhoan = taiKhoan.LoaiTaiKhoan,
                    DanhSachQuyen = dsQuyen
                };

                return OperationResult.Ok(sessionDto);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("BUS_Auth.DangNhap Error: " + ex.Message);
                return OperationResult.Fail($"{AppConstants.ErrorMessages.ERR_SYSTEM_FAIL}\nChi tiết: {ex.Message}");
            }
        }
        #endregion
    }
}



using ET.Constants;
using System;
using System.Linq;
using DAL;
using DAL.Repositories.TaiChinh;
using ET.Results;

namespace BUS.Services.TaiChinh
{
    public class BUS_TheRFID
    {
        #region Khởi tạo (Singleton)

        public static BUS_TheRFID Instance { get; } = new BUS_TheRFID();

        private readonly DAL_TheRFID _dal = DAL_TheRFID.Instance;

        #endregion

        #region Nghiệp vụ đặc thù

        /// <summary>
        /// Chuyển trạng thái thẻ RFID (Kích hoạt, Khóa, Trả, Mở khóa).
        /// Trước khi UPDATE, phải kiểm tra bảng HeThong.LuongTrangThai:
        ///   SELECT 1 FROM LuongTrangThai
        ///   WHERE ThucThe = 'TheRFID'
        ///     AND TuTrangThai = trạng thái hiện tại
        ///     AND DenTrangThai = trạng thái đích
        /// Nếu không tồn tại dòng nào suy ra luồng không hợp lệ dẫn đến từ chối.
        /// </summary>
        /// <param name="idThe">Id thẻ cần chuyển</param>
        /// <param name="trangThaiDich">Trạng thái muốn chuyển tới (DangDung, DaKhoa, DaTra)</param>
        public OperationResult ChuyenTrangThai(int idThe, string trangThaiDich)
        {
            try
            {
                string trangThaiHienTai = _dal.LayTrangThaiHienTai(idThe);
                if (string.IsNullOrEmpty(trangThaiHienTai))
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_RFID_KHONG_TIM_THAY);

                if (trangThaiHienTai == trangThaiDich)
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_RFID_TRANG_THAI_TRUNG);

                if (!KiemTraLuongHopLe(trangThaiHienTai, trangThaiDich))
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_RFID_LUONG_KHONG_HOP_LE);

                _dal.ChuyenTrangThai(idThe, trangThaiDich);

                if (trangThaiDich == "DaKhoa")
                    return OperationResult.Ok("MSG_RFID_KHOA_OK");

                if (trangThaiDich == "DangDung")
                    return OperationResult.Ok("MSG_RFID_KICH_HOAT_OK");

                return OperationResult.Ok("MSG_RFID_CHUYEN_OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("BUS_TheRFID.ChuyenTrangThai: " + ex.Message);
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_SYSTEM_FAIL);
            }
        }

        /// Kiểm tra trong bảng HeThong.LuongTrangThai xem có đường đi hợp lệ không.
        /// Bảng này là "bản đồ" các bước chuyển trạng thái được phép.
        /// Ví dụ: ChuaKichHoat -> DangDung (OK), ChuaKichHoat -> DaKhoa (KHÔNG cho phép).
        private bool KiemTraLuongHopLe(string tuTrangThai, string denTrangThai)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.LuongTrangThais.Any(lt =>
                    lt.ThucThe == "TheRFID" &&
                    lt.TuTrangThai == tuTrangThai &&
                    lt.DenTrangThai == denTrangThai);
            }
        }

        /// <summary>
        /// Tìm thẻ đang hoạt động của khách -> chuyển sang DaKhoa.
        /// </summary>
        /// <param name="idKhachHang">Id khách hàng</param>
        /// <param name="idNguoiThucHien">Id nhân viên thực hiện (reserved cho audit log khi DB có bảng NhatKy)</param>
        public OperationResult KhoaTheTheoKhach(int idKhachHang, int idNguoiThucHien)
        {
            var dsThe = _dal.LayTheTheoKhach(idKhachHang);
            if (dsThe == null || dsThe.Count == 0)
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_RFID_KHONG_TIM_THAY);

            var theActive = dsThe.Find(t => t.TrangThai == ET.Constants.AppConstants.TrangThaiThe.DangDung);
            if (theActive == null)
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_RFID_KHONG_TIM_THAY);

            return ChuyenTrangThai(theActive.Id, ET.Constants.AppConstants.TrangThaiThe.DaKhoa);
        }

        /// <summary>
        /// Tìm thẻ bị khóa của khách -> chuyển sang DangDung.
        /// </summary>
        /// <param name="idKhachHang">Id khách hàng</param>
        /// <param name="idNguoiThucHien">Id nhân viên thực hiện</param>
        public OperationResult MoKhoaTheTheoKhach(int idKhachHang, int idNguoiThucHien)
        {
            var dsThe = _dal.LayTheTheoKhach(idKhachHang);
            if (dsThe == null || dsThe.Count == 0)
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_RFID_KHONG_TIM_THAY);

            var theKhoa = dsThe.Find(t => t.TrangThai == ET.Constants.AppConstants.TrangThaiThe.DaKhoa);
            if (theKhoa == null)
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_RFID_KHONG_TIM_THAY);

            return ChuyenTrangThai(theKhoa.Id, ET.Constants.AppConstants.TrangThaiThe.DangDung);
        }

        #endregion
    }
}



using ET.Constants;
using System;
using DAL.Repositories.TaiChinh;
using DAL.Repositories.HeThong;
using ET.Results;

namespace BUS.Services.TaiChinh
{
    public class BUS_ViDienTu
    {
        #region Khởi tạo (Singleton)

        public static BUS_ViDienTu Instance { get; } = new BUS_ViDienTu();

        private readonly DAL_ViDienTu _dal = DAL_ViDienTu.Instance;

        #endregion

        #region Nghiệp vụ đặc thù

        // Tra cứu thông tin ví dựa vào mã thẻ RFID
        public OperationResult TraCuuViTheoMaThe(string maThe)
        {
            if (string.IsNullOrWhiteSpace(maThe)) return OperationResult.Fail(AppConstants.ErrorMessages.ERR_RFID_MA_RONG);

            try
            {
                var the = DAL_TheRFID.Instance.TraCuuTheoMaThe(maThe);
                if (the == null || !the.IdViDienTu.HasValue) 
                    return OperationResult.Fail("Thẻ chưa được kích hoạt hoặc chưa liên kết ví!");

                var vi = _dal.LayViTheoKhach(the.IdKhachHang.Value);
                if (vi == null) return OperationResult.Fail(AppConstants.ErrorMessages.ERR_VI_CHUA_CO);

                var dict = new System.Collections.Generic.Dictionary<string, object>
                {
                    { "IdKhachHang", vi.IdKhachHang },
                    { "IdVi", vi.Id },
                    { "SoDuVi", _dal.LaySoDu(vi.Id) }
                };
                return OperationResult.Ok(dict);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("BUS_ViDienTu.TraCuuViTheoMaThe: " + ex.Message);
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_SYSTEM_FAIL);
            }
        }
        // Nạp tiền vào ví khách hàng.
        // B1: Kiểm tra số tiền hợp lệ (phải lớn hơn 0).
        // B2: Kiểm tra khách đã có ví (nếu chưa -> tạo ví mới tự động).
        // B3: Gọi DAL nạp tiền (INSERT SoCaiVi + ChungTuTC trong 1 Transaction).
        public OperationResult NapTien(int idKhachHang, decimal soTien, string phuongThuc, int idNguoiTao)
        {
            if (soTien <= 0)
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_VI_SO_TIEN_AM);

            try
            {
                var vi = _dal.LayViTheoKhach(idKhachHang);
                if (vi == null)
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_VI_CHUA_CO);

                _dal.NapTien(vi.Id, soTien, phuongThuc, idNguoiTao);
                return OperationResult.Ok("MSG_NAP_TIEN_OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("BUS_ViDienTu.NapTien: " + ex.Message);
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_SYSTEM_FAIL);
            }
        }


        // Cấp ví + thẻ RFID cho khách mới.
        // B1: Tạo ví điện tử (INSERT ViDienTu).
        // B2: Gắn thẻ RFID, link tới ví vừa tạo (INSERT TheRFID).
        // B3: Phát sinh chứng từ thu cọc thẻ (đọc mức cọc từ HeThong.CauHinh key RFID_TIEN_COC).
        public OperationResult CapViVaThe(int idKhachHang, string maThe, int idNguoiTao)
        {
            if (string.IsNullOrWhiteSpace(maThe))
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_RFID_MA_RONG);

            if (DAL_TheRFID.Instance.KiemTraTrungMaThe(maThe))
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_RFID_MA_TRUNG);

            var viHienTai = _dal.LayViTheoKhach(idKhachHang);
            if (viHienTai != null)
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_VI_DA_CO);

            try
            {
                string maVi = "VI" + idKhachHang.ToString("D5");
                int idVi = _dal.TaoVi(idKhachHang, maVi);

                decimal tienCoc = DAL_CauHinh.Instance.LayGiaTriDecimal("RFID_TIEN_COC", 50000);
                DAL_TheRFID.Instance.GanTheMoi(maThe, idKhachHang, idVi, tienCoc);

                return OperationResult.Ok("MSG_RFID_KICH_HOAT_OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("BUS_ViDienTu.CapViVaThe: " + ex.Message);
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_SYSTEM_FAIL);
            }
        }

        #endregion

        #region Truy vấn dữ liệu

        public System.Collections.Generic.List<ET.Models.TaiChinh.ET_LichSuGiaoDich> LayLichSuGiaoDich(int idKhachHang)
        {
            return _dal.LayLichSuVi(idKhachHang);
        }

        #endregion
    }
}



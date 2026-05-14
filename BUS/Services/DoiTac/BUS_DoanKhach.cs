using System;
using System.Collections.Generic;
using System.Linq;
using ET.Constants;
using ET.DTOs;
using ET.Results;

namespace BUS.Services.DoiTac
{
    public class BUS_DoanKhach
    {
        public static BUS_DoanKhach Instance { get; } = new BUS_DoanKhach();

        public OperationResult CheckBookingValid(string maBooking)
        {
            try
            {
                var dsQuyenLoi = DAL.Repositories.DoiTac.DAL_DoanKhach.Instance.TraCuuQuyenLoiDoan(maBooking);

                if (dsQuyenLoi == null || !dsQuyenLoi.Any())
                {
                    return OperationResult.Fail($"Không tìm thấy thông tin quyền lợi cho mã Booking: {maBooking}");
                }

                var listConLai = dsQuyenLoi.Where(q => q.SoLuongConLai > 0).ToList();

                if (!listConLai.Any())
                {
                    return OperationResult.Fail($"Booking {maBooking} đã sử dụng hết toàn bộ quyền lợi.");
                }

                return OperationResult.Ok(listConLai);
            }
            catch (Exception ex)
            {
                return OperationResult.Fail(ex.Message);
            }
        }

        public OperationResult KhauTruQuota(int idQuyenLoi, int soLuongTru, int idNhanVien, int? idThietBi = null)
        {
            try
            {
                DAL.Repositories.DoiTac.DAL_DoanKhach.Instance.GhiNhanSuDungQuyenLoi(idQuyenLoi, soLuongTru, idNhanVien, idThietBi);
                return OperationResult.Ok("Đã ghi nhận trừ quota thành công");
            }
            catch (Exception ex)
            {
                return OperationResult.Fail(ex.Message);
            }
        }
    }
}



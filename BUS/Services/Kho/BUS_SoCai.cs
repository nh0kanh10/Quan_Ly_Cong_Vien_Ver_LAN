using System;
using System.Collections.Generic;
using ET.Models.Kho;
using DAL.Repositories.Kho;

namespace BUS.Services.Kho
{
    public class BUS_SoCai
    {
        #region Khởi tạo
        
        public static BUS_SoCai Instance { get; } = new BUS_SoCai();
        
        #endregion

        #region Truy vấn dữ liệu

        /// <summary>
        /// Lấy lịch sử thẻ kho của một sản phẩm trong một khoảng thời gian.
        /// Thẻ kho không được phép sửa đổi nên kết quả trả trực tiếp từ cơ sở dữ liệu lên.
        /// </summary>
        /// <param name="idSanPham">Mã sản phẩm kiểm tra</param>
        /// <param name="tuNgay">Mốc bắt đầu</param>
        /// <param name="denNgay">Mốc kết thúc</param>
        /// <param name="idKhoDi">Tìm riêng kho cung cấp ra (tuỳ chọn)</param>
        /// <param name="idKhoDen">Tìm riêng kho nhận vào (tuỳ chọn)</param>
        /// <returns>Danh sách giao dịch trên sổ cái</returns>
        public List<ET_SoCai> GetLichSuGiaoDich(int idSanPham, DateTime tuNgay, DateTime denNgay, int? idKhoDi = null, int? idKhoDen = null)
        {
            // Bắt lỗi đơn giản: Tìm ngày ngược
            if (tuNgay > denNgay)
            {
                // Thay vì ném lỗi, tự động đảo ngày giùm sinh viên cho nó mượt
                var tam = tuNgay;
                tuNgay = denNgay;
                denNgay = tam;
            }

            return DAL_SoCai.Instance.GetLichSuGiaoDich(idSanPham, tuNgay, denNgay, idKhoDi, idKhoDen);
        }

        /// <summary>
        /// Lấy tổng tổn kho hiện hành tính đến thời điểm gọi hàm bằng phép trừ xuất lượng từ nhập lượng.
        /// </summary>
        /// <param name="idKho">Mã kho kiểm tra</param>
        /// <param name="idSanPham">Mã sản phẩm kiểm tra</param>
        /// <param name="idLoHang">Lọc thêm tham số lô hàng nếu có</param>
        /// <returns>Con số tồn thực tế hiện tại</returns>
        public decimal GetTonKhoHienTai(int idKho, int idSanPham, int? idLoHang = null)
        {
            return DAL_SoCai.Instance.GetTonKhoHienTai(idKho, idSanPham, idLoHang);
        }

        // Báo cáo tồn kho tổng hợp tất cả kho thật, có thể filter theo IdKho.
        public List<ET_TonKho> GetBaoCaoTonKho(int? idKho = null)
        {
            return DAL_SoCai.Instance.GetBaoCaoTonKho(idKho);
        }

        // Lấy danh sách SP tồn dưới mức cảnh báo.
        public List<ET_TonKho> GetCanhBaoTonToiThieu()
        {
            return DAL_SoCai.Instance.GetCanhBaoTonToiThieu();
        }

        #endregion


    }
}



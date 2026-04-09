using DAL;
using ET;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BUS
{
    public class BUS_TheKho
    {
        private static BUS_TheKho instance;
        public static BUS_TheKho Instance
        {
            get
            {
                if (instance == null) instance = new BUS_TheKho();
                return instance;
            }
        }

        public List<ET_TheKho_View> XemTheKho(int idKho, int idSanPham, DateTime? tuNgay, DateTime? denNgay)
        {
            // 1. Pull FULL history for this product in this warehouse to calculate rolling stock correctly
            var rawData = DAL_TheKho.Instance.LayTheoSanPham(idKho, idSanPham)
                                    .OrderBy(x => x.ThoiGianGiaoDich).ToList(); // Phải SORT ASC từ đầu thời gian

            // Lấy danh sách nhân viên để dịch tên
            var dsNhanVien = DAL_NhanVien.Instance.LoadDS();

            var result = new List<ET_TheKho_View>();
            int currentStock = 0;

            foreach (var item in rawData)
            {
                // Dịch mã nhân viên
                var nv = dsNhanVien.FirstOrDefault(n => n.Id == item.CreatedBy);
                string tenNv = nv != null ? nv.HoTen : "System";

                // Format Loai Giao Dich
                string moTaGD = item.LoaiGiaoDich;
                string chungTu = "";

                if (item.LoaiGiaoDich == "NHAP_NCC") 
                { moTaGD = "Nhập Nhà cung cấp"; chungTu = "NK-" + item.IdThamChieu; }
                else if (item.LoaiGiaoDich == "XUAT_TIEUHAO") 
                { moTaGD = "Xuất Tiêu Hao"; chungTu = "XK-" + item.IdThamChieu; }
                else if (item.LoaiGiaoDich == "XUAT_BAN_POS") 
                { moTaGD = "Bán POS"; chungTu = "DH-" + item.IdThamChieu; }
                else if (item.LoaiGiaoDich == "KIEM_KHO") 
                { moTaGD = "Kiểm Kê Kho"; chungTu = "KK-" + item.IdThamChieu; }

                // Tính toán Tồn Theo Mạch Nước (Rolling Stock)
                int tonDau = currentStock;
                currentStock += item.SoLuongThayDoi;
                int tonCuoi = currentStock;

                var view = new ET_TheKho_View
                {
                    Id = item.Id,
                    ThoiGian = item.ThoiGianGiaoDich,
                    SoChungTu = chungTu,
                    LoaiGiaoDich = moTaGD,
                    TonDauKy = tonDau,
                    Nhap = item.SoLuongThayDoi > 0 ? item.SoLuongThayDoi : 0,
                    Xuat = item.SoLuongThayDoi < 0 ? Math.Abs(item.SoLuongThayDoi) : 0,
                    TonCuoiKy = tonCuoi,
                    DonGiaVatTu = item.DonGiaVatTu ?? 0,
                    ThanhTien = Math.Abs(item.SoLuongThayDoi) * (item.DonGiaVatTu ?? 0),
                    GhiChu = item.GhiChu,
                    NguoiTao = tenNv
                };

                result.Add(view);
            }

            // 2. Window Filtering: Cắt lấy đoạn giao dịch nằm trong khoảng TuNgay -> DenNgay
            var finalView = result.AsQueryable();

            if (tuNgay.HasValue)
            {
                finalView = finalView.Where(x => x.ThoiGian >= tuNgay.Value.Date);
            }
            if (denNgay.HasValue)
            {
                var endOfDay = denNgay.Value.Date.AddDays(1).AddSeconds(-1);
                finalView = finalView.Where(x => x.ThoiGian <= endOfDay);
            }

            // Lật ngược lại DESC để Kế toán xem phiếu mới nhất ở trên cùng
            return finalView.OrderByDescending(x => x.ThoiGian).ToList();
        }
    }
}

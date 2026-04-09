using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using ET;

namespace BUS
{
    public class BUS_PhieuNhapKho
    {
        private static BUS_PhieuNhapKho instance;
        public static BUS_PhieuNhapKho Instance
        {
            get { return instance ?? (instance = new BUS_PhieuNhapKho()); }
        }

        public List<ET_PhieuNhapKho> LoadDS() { return DAL_PhieuNhapKho.Instance.LoadDS(); }
        public bool Them(ET_PhieuNhapKho et) { return DAL_PhieuNhapKho.Instance.Them(et); }
        public bool Sua(ET_PhieuNhapKho et) { return DAL_PhieuNhapKho.Instance.Sua(et); }
        public bool Xoa(int id) { return DAL_PhieuNhapKho.Instance.Xoa(id); }
        public ET_PhieuNhapKho LayTheoId(int id) { return DAL_PhieuNhapKho.Instance.LayTheoId(id); }

        /// <summary>
        /// Tạo Phiếu Nhập Kho hoàn chỉnh (Master + Details) trong 1 Transaction.
        /// Tự động: Tính TongTien, sinh PhieuChi, cập nhật TonKho, ghi TheKho.
        /// </summary>
        public int TaoPhieu(ET_PhieuNhapKho master, List<ET_ChiTietNhapKho> details)
        {
            // 1. Calculate TongTien from details
            decimal tongTien = 0;
            foreach (var d in details)
                tongTien += d.SoLuong * d.DonGiaNhap;
            master.TongTien = tongTien;

            // 2. Auto-generate SoChungTu if empty
            if (string.IsNullOrEmpty(master.SoChungTu))
                master.SoChungTu = "NK-" + DateTime.Now.ToString("yyyyMMdd-HHmmss");

            master.CreatedAt = DateTime.Now;
            int userId = 0;
            if (ET.SessionManager.CurrentUser != null)
                userId = ET.SessionManager.CurrentUser.Id;
            master.CreatedBy = userId;

            // 3. Auto-generate PhieuChi for accounting if total > 0
            if (tongTien > 0)
            {
                var phieuChi = new ET_PhieuChi
                {
                    MaCode = "PC-NK-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    SoTien = tongTien,
                    LyDo = "Chi tiền nhập kho - " + master.SoChungTu,
                    ThoiGian = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    CreatedBy = userId
                };
                DAL_PhieuChi.Instance.Them(phieuChi);
            }

            // 4. Atomic insert Master + Details
            int newId = DAL_PhieuNhapKho.Instance.ThemHoanChinh(master, details);
            if (newId <= 0) return -1;

            // 5. Cập nhật TonKho + ghi TheKho cho từng dòng chi tiết
            CapNhatTonKho(master.IdKho, details, newId, userId, "NHAP_KHO");

            return newId;
        }

        /// <summary>
        /// Cập nhật bảng TonKho (tăng số lượng) và ghi TheKho (sổ cái kho).
        /// </summary>
        private void CapNhatTonKho(int idKho, List<ET_ChiTietNhapKho> details, int idPhieu, int userId, string loaiGD)
        {
            var allTonKho = DAL_TonKho.Instance.LoadDS().Where(x => x.IdKho == idKho).ToList();

            foreach (var d in details)
            {
                var ton = allTonKho.FirstOrDefault(x => x.IdSanPham == d.IdSanPham);
                int tonCuoi;

                int soLuongBase = (int)(d.SoLuong * (d.TyLeQuyDoi > 0 ? d.TyLeQuyDoi : 1));
                decimal donGiaBase = d.DonGiaNhap / (d.TyLeQuyDoi > 0 ? d.TyLeQuyDoi : 1);

                if (ton != null)
                {
                    // Cộng thêm vào tồn hiện tại
                    ton.SoLuong += soLuongBase;
                    tonCuoi = ton.SoLuong;
                    DAL_TonKho.Instance.Sua(ton);
                }
                else
                {
                    // Chưa có record TonKho -> Tạo mới
                    var newTon = new ET_TonKho
                    {
                        IdKho = idKho,
                        IdSanPham = d.IdSanPham,
                        SoLuong = soLuongBase
                    };
                    tonCuoi = soLuongBase;
                    DAL_TonKho.Instance.Them(newTon);
                }

                // Ghi sổ cái kho (TheKho)
                var theKho = new ET_TheKho
                {
                    IdKho = idKho,
                    IdSanPham = d.IdSanPham,
                    LoaiGiaoDich = loaiGD,
                    SoLuongThayDoi = soLuongBase,
                    TonCuoi = tonCuoi,
                    DonGiaVatTu = donGiaBase,
                    IdThamChieu = idPhieu,
                    ThoiGianGiaoDich = DateTime.Now,
                    CreatedBy = userId,
                    GhiChu = "Nhập kho Phiếu #" + idPhieu
                };
                DAL_TheKho.Instance.Them(theKho);
            }
        }

        public string LaySoChungTuTiepTheo()
        {
            return "NK-" + DateTime.Now.ToString("yyyyMMdd-HHmmss");
        }
    }
}

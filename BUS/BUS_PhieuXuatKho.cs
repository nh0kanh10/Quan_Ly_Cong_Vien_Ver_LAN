using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using ET;

namespace BUS
{
    public class BUS_PhieuXuatKho
    {
        private static BUS_PhieuXuatKho instance;
        public static BUS_PhieuXuatKho Instance
        {
            get { return instance ?? (instance = new BUS_PhieuXuatKho()); }
        }

        public List<ET_PhieuXuatKho> LoadDS() { return DAL_PhieuXuatKho.Instance.LoadDS(); }
        public bool Them(ET_PhieuXuatKho et) { return DAL_PhieuXuatKho.Instance.Them(et); }
        public bool Sua(ET_PhieuXuatKho et) { return DAL_PhieuXuatKho.Instance.Sua(et); }
        public bool Xoa(int id) { return DAL_PhieuXuatKho.Instance.Xoa(id); }
        public ET_PhieuXuatKho LayTheoId(int id) { return DAL_PhieuXuatKho.Instance.LayTheoId(id); }

        /// <summary>
        /// Tạo Phiếu Xuất Kho hoàn chỉnh (Master + Details) trong 1 Transaction.
        /// Tự động: Trừ TonKho, ghi TheKho. Xuất kho KHÔNG sinh Phiếu Chi.
        /// </summary>
        public int TaoPhieu(ET_PhieuXuatKho master, List<ET_ChiTietXuatKho> details)
        {
            master.CreatedAt = DateTime.Now;
            int userId = 0;
            if (ET.SessionManager.CurrentUser != null)
                userId = ET.SessionManager.CurrentUser.Id;
            master.CreatedBy = userId;

            // Atomic insert Master + Details
            int newId = DAL_PhieuXuatKho.Instance.ThemHoanChinh(master, details);
            if (newId <= 0) return -1;

            // Cập nhật TonKho + ghi TheKho cho từng dòng chi tiết
            TruTonKho(master.IdKhoXuat, details, newId, userId, "XUAT_KHO");

            return newId;
        }

        /// <summary>
        /// Cập nhật bảng TonKho (giảm số lượng) và ghi TheKho (sổ cái kho).
        /// </summary>
        private void TruTonKho(int idKho, List<ET_ChiTietXuatKho> details, int idPhieu, int userId, string loaiGD)
        {
            var allTonKho = DAL_TonKho.Instance.LoadDS().Where(x => x.IdKho == idKho).ToList();

            foreach (var d in details)
            {
                var ton = allTonKho.FirstOrDefault(x => x.IdSanPham == d.IdSanPham);
                int tonCuoi;

                int soLuongBase = (int)(d.SoLuong * (d.TyLeQuyDoi > 0 ? d.TyLeQuyDoi : 1));
                decimal donGiaBase = (d.DonGiaXuat ?? 0) / (d.TyLeQuyDoi > 0 ? d.TyLeQuyDoi : 1);

                if (ton != null)
                {
                    // Trừ đi khỏi tồn hiện tại
                    ton.SoLuong -= soLuongBase;
                    tonCuoi = ton.SoLuong;
                    DAL_TonKho.Instance.Sua(ton);
                }
                else
                {
                    // Chưa có record TonKho -> Tạo mới với số âm (cảnh báo)
                    var newTon = new ET_TonKho
                    {
                        IdKho = idKho,
                        IdSanPham = d.IdSanPham,
                        SoLuong = -soLuongBase
                    };
                    tonCuoi = -soLuongBase;
                    DAL_TonKho.Instance.Them(newTon);
                }

                // Ghi sổ cái kho (TheKho)
                var theKho = new ET_TheKho
                {
                    IdKho = idKho,
                    IdSanPham = d.IdSanPham,
                    LoaiGiaoDich = loaiGD,
                    SoLuongThayDoi = -soLuongBase,
                    TonCuoi = tonCuoi,
                    DonGiaVatTu = donGiaBase,
                    IdThamChieu = idPhieu,
                    ThoiGianGiaoDich = DateTime.Now,
                    CreatedBy = userId,
                    GhiChu = "Xuất kho Phiếu #" + idPhieu
                };
                DAL_TheKho.Instance.Them(theKho);
            }
        }

        public string LaySoChungTuTiepTheo()
        {
            return "XK-" + DateTime.Now.ToString("yyyyMMdd-HHmmss");
        }
    }
}

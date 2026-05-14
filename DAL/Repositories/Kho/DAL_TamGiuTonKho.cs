using System;
using System.Collections.Generic;
using System.Linq;
using ET.Models.Kho;

namespace DAL.Repositories.Kho
{
    public class DAL_TamGiuTonKho
    {
        public static DAL_TamGiuTonKho Instance { get; } = new DAL_TamGiuTonKho();

        #region Truy vấn dữ liệu

        /// <summary>
        /// Tổng số lượng hàng đang bị khoá tạm thời (đơn chưa thanh toán xong).
        /// Dùng để tính tồn kho khả dụng = tồn thực - tổng giữ.
        /// </summary>
        /// <param name="idKho">Kho cần kiểm tra</param>
        /// <param name="idSanPham">Sản phẩm cần kiểm tra</param>
        /// <returns>Tổng khối lượng bị chiếm giữ</returns>
        public decimal GetTongSoLuongDangGiu(int idKho, int idSanPham)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.TamGiuTonKhos
                    .Where(t => t.IdKho == idKho
                             && t.IdSanPham == idSanPham
                             && t.HetHan > DateTime.Now)
                    .Sum(t => (decimal?)t.SoLuongGiu) ?? 0;
            }
        }

        /// <summary>
        /// Danh sách giữ hàng của một đơn hàng cụ thể.
        /// </summary>
        public List<ET_TamGiuTonKho> GetDanhSachGiuByDonHang(int idDonHangNhap)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.TamGiuTonKhos
                    .Where(t => t.IdDonHangNhap == idDonHangNhap)
                    .Select(t => new ET_TamGiuTonKho
                    {
                        Id = t.Id,
                        IdDonHangNhap = t.IdDonHangNhap,
                        IdSanPham = t.IdSanPham,
                        IdLoHang = t.IdLoHang,
                        IdKho = t.IdKho,
                        SoLuongGiu = t.SoLuongGiu,
                        GiuTu = t.GiuTu,
                        HetHan = t.HetHan
                    }).ToList();
            }
        }

        #endregion

        #region Thêm / Xóa

        /// <summary>
        /// Tạm giữ một phần tồn kho cho đơn hàng đang xử lý.
        /// </summary>
        public void LuuThongTinGiuKho(ET_TamGiuTonKho et)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var record = new TamGiuTonKho
                {
                    IdDonHangNhap = et.IdDonHangNhap,
                    IdSanPham = et.IdSanPham,
                    IdLoHang = et.IdLoHang,
                    IdKho = et.IdKho,
                    SoLuongGiu = et.SoLuongGiu,
                    GiuTu = DateTime.Now,
                    HetHan = et.HetHan
                };

                db.TamGiuTonKhos.InsertOnSubmit(record);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// Xả toàn bộ giữ hàng khi đơn hàng bị huỷ.
        /// </summary>
        public void XaTonKho(int idDonHangNhap)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var dsGiu = db.TamGiuTonKhos.Where(t => t.IdDonHangNhap == idDonHangNhap);
                db.TamGiuTonKhos.DeleteAllOnSubmit(dsGiu);
                db.SubmitChanges();
            }
        }

        #endregion
    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using ET.Constants;
using ET.Models.Kho;

namespace DAL.Repositories.Kho
{
    public class DAL_LoHang
    {
        public static DAL_LoHang Instance { get; } = new DAL_LoHang();

        #region Truy vấn dữ liệu

        /// <summary>
        /// Lấy danh sách lô hàng của một sản phẩm.
        /// Dùng khi chọn lô hàng xuất kho hoặc xem lịch sử nhập.
        /// </summary>
        /// <param name="idSanPham">Mã định danh sản phẩm cần truy vấn</param>
        /// <param name="chiConHang">Nếu true chỉ lấy lô có TrangThai = 'ConHang'</param>
        /// <returns>Danh sách ET_LoHang sắp xếp theo ngày tạo</returns>
        public List<ET_LoHang> GetDanhSachBySanPham(int idSanPham, bool chiConHang = false)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var query = db.LoHangs.Where(l => l.IdSanPham == idSanPham);

                if (chiConHang)
                    query = query.Where(l => l.TrangThai == AppConstants.TrangThaiLoHang.ConHang);

                return query.Select(l => new ET_LoHang
                {
                    Id = l.Id,
                    MaLoHang = l.MaLoHang,
                    IdSanPham = l.IdSanPham,
                    IdNhaCungCap = l.IdNhaCungCap,
                    NgaySanXuat = l.NgaySanXuat,
                    NgayHetHan = l.NgayHetHan,
                    SoLuongNhap = l.SoLuongNhap,
                    IdChungTuNhap = l.IdChungTuNhap,
                    TrangThai = l.TrangThai,
                    GhiChu = l.GhiChu,
                    NgayTao = l.NgayTao
                }).ToList();
            }
        }

        /// <summary>
        /// Lấy chi tiết thông tin của một lô hàng dựa theo Id.
        /// </summary>
        /// <param name="idLoHang">Mã định danh của lô hàng cần tra cứu</param>
        /// <returns>Đối tượng ET_LoHang, hoặc null nếu không tìm thấy</returns>
        public ET_LoHang GetChiTiet(int idLoHang)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var lo = db.LoHangs.FirstOrDefault(l => l.Id == idLoHang);
                if (lo == null) return null;

                return new ET_LoHang
                {
                    Id = lo.Id,
                    MaLoHang = lo.MaLoHang,
                    IdSanPham = lo.IdSanPham,
                    IdNhaCungCap = lo.IdNhaCungCap,
                    NgaySanXuat = lo.NgaySanXuat,
                    NgayHetHan = lo.NgayHetHan,
                    SoLuongNhap = lo.SoLuongNhap,
                    IdChungTuNhap = lo.IdChungTuNhap,
                    TrangThai = lo.TrangThai,
                    GhiChu = lo.GhiChu,
                    NgayTao = lo.NgayTao
                };
            }
        }

        // Lấy danh sách lô hàng sắp hết hạn trong N ngày tới.
        // Tương đương View Kho.V_CanhBaoHetHan.
        public List<ET_LoHang> GetLoSapHetHan(int soNgay)
        {
            using (var db = new DaiNamDBDataContext())
            {
                DateTime nguong = DateTime.Now.AddDays(soNgay);

                return (from lh in db.LoHangs
                        join sp in db.SanPhams on lh.IdSanPham equals sp.Id
                        where lh.NgayHetHan != null
                              && lh.NgayHetHan <= nguong
                              && lh.TrangThai == AppConstants.TrangThaiLoHang.ConHang
                        orderby lh.NgayHetHan ascending
                        select new ET_LoHang
                        {
                            Id = lh.Id,
                            MaLoHang = lh.MaLoHang,
                            IdSanPham = lh.IdSanPham,
                            TenSanPham = sp.TenSanPham,
                            NgayHetHan = lh.NgayHetHan,
                            SoLuongNhap = lh.SoLuongNhap,
                            TrangThai = lh.TrangThai,
                            GhiChu = lh.GhiChu
                        }).ToList();
            }
        }

        #endregion

        #region Thêm / Sửa

        /// <summary>
        /// Thêm mới (Id=0) hoặc cập nhật lô hàng đã có.
        /// Khi nhập hàng mới từ NCC sẽ tạo lô mới kèm IdChungTuNhap.
        /// </summary>
        /// <param name="et">Thông tin lô hàng cần lưu (Id=0 -> Insert, Id>0 -> Update)</param>
        public void LuuLoHang(ET_LoHang et)
        {
            using (var db = new DaiNamDBDataContext())
            {
                if (et.Id == 0)
                {
                    var loMoi = new LoHang
                    {
                        MaLoHang = et.MaLoHang,
                        IdSanPham = et.IdSanPham,
                        IdNhaCungCap = et.IdNhaCungCap,
                        NgaySanXuat = et.NgaySanXuat,
                        NgayHetHan = et.NgayHetHan,
                        SoLuongNhap = et.SoLuongNhap,
                        IdChungTuNhap = et.IdChungTuNhap,
                        TrangThai = et.TrangThai ?? "ConHang",
                        GhiChu = et.GhiChu
                    };
                    db.LoHangs.InsertOnSubmit(loMoi);
                }
                else
                {
                    var loDb = db.LoHangs.FirstOrDefault(l => l.Id == et.Id);
                    if (loDb != null)
                    {
                        loDb.MaLoHang = et.MaLoHang;
                        loDb.NgaySanXuat = et.NgaySanXuat;
                        loDb.NgayHetHan = et.NgayHetHan;
                        loDb.SoLuongNhap = et.SoLuongNhap;
                        loDb.TrangThai = et.TrangThai;
                        loDb.GhiChu = et.GhiChu;
                    }
                }
                db.SubmitChanges();
            }
        }

        #endregion
    }
}



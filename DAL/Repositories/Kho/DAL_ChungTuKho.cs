using System;
using System.Collections.Generic;
using System.Linq;
using ET.Constants;
using ET.Models.Kho;

namespace DAL.Repositories.Kho
{
    public class DAL_ChungTuKho
    {
        public static DAL_ChungTuKho Instance { get; } = new DAL_ChungTuKho();

        #region Truy vấn dữ liệu

        /// <summary>
        /// Lấy danh sách các chứng từ trong một khoảng thời gian, lọc theo loại và kho (nếu có).
        /// V3: filter kho dựa trên ChiTietChungTu thay vì header ChungTu.
        /// </summary>
        public List<ET_ChungTuKho> GetDanhSach(string loaiChungTu, DateTime tuNgay, DateTime denNgay, int? idKho = null)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var query = db.ChungTus.Where(c => c.NgayTao >= tuNgay.Date && c.NgayTao <= denNgay.Date.AddDays(1).AddTicks(-1));

                if (!string.IsNullOrEmpty(loaiChungTu))
                    query = query.Where(c => c.LoaiChungTu == loaiChungTu);

                // V3: filter kho qua ChiTietChungTu (kho đã chuyển xuống dòng chi tiết)
                if (idKho.HasValue)
                    query = query.Where(c => c.ChiTietChungTus.Any(ct => ct.IdKhoXuat == idKho.Value || ct.IdKhoNhap == idKho.Value));

                return query.OrderByDescending(c => c.NgayTao).Select(c => new ET_ChungTuKho
                {
                    Id = c.Id,
                    MaChungTu = c.MaChungTu,
                    LoaiChungTu = c.LoaiChungTu,
                    IdDoiTac = c.IdDoiTac,
                    NgayChungTu = c.NgayChungTu,
                    TrangThai = c.TrangThai,
                    GhiChu = c.GhiChu,
                    IdNguoiTao = c.IdNguoiTao,
                    NgayTao = c.NgayTao
                }).ToList();
            }
        }

        /// <summary>
        /// Lấy chi tiết 1 chứng từ kèm toàn bộ dòng hàng hóa.
        /// V3: map thêm IdKhoXuat/IdKhoNhap từ ChiTietChungTu.
        /// </summary>
        public ET_ChungTuKho GetChiTiet(int idChungTu)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var master = db.ChungTus.FirstOrDefault(c => c.Id == idChungTu);
                if (master == null) return null;

                var et = new ET_ChungTuKho
                {
                    Id = master.Id,
                    MaChungTu = master.MaChungTu,
                    LoaiChungTu = master.LoaiChungTu,
                    IdDoiTac = master.IdDoiTac,
                    IdDonHang = master.IdDonHang,
                    IdBaoTri = master.IdBaoTri,
                    IdChungTuGoc = master.IdChungTuGoc,
                    NgayChungTu = master.NgayChungTu,
                    LyDo = master.LyDo,
                    TrangThai = master.TrangThai,
                    GhiChu = master.GhiChu,
                    NgayTao = master.NgayTao,
                    IdNguoiTao = master.IdNguoiTao,
                    IdNguoiDuyet1 = master.IdNguoiDuyet1,
                    NgayDuyet1 = master.NgayDuyet1
                };

                // V3: map IdKhoXuat/IdKhoNhap từ từng dòng chi tiết
                et.ChiTiets = (from ct in db.ChiTietChungTus
                               join sp in db.SanPhams on ct.IdSanPham equals sp.Id
                               join dvt in db.DonViTinhs on sp.IdDonViTinh equals dvt.Id
                               join kx in db.KhoHangs on ct.IdKhoXuat equals kx.Id
                               join kn in db.KhoHangs on ct.IdKhoNhap equals kn.Id
                               where ct.IdChungTu == idChungTu
                               select new ET_ChiTietChungTu
                               {
                                   Id = ct.Id,
                                   IdChungTu = ct.IdChungTu,
                                   IdSanPham = ct.IdSanPham,
                                   MaSanPham = sp.MaSanPham,
                                   TenSanPham = sp.TenSanPham,
                                   TenDonViTinh = dvt.TenDonVi,
                                   IdLoHang = ct.IdLoHang,
                                   IdKhoXuat = ct.IdKhoXuat,
                                   IdKhoNhap = ct.IdKhoNhap,
                                   TenKhoXuat = kx.TenKho,
                                   TenKhoNhap = kn.TenKho,
                                   SoLuong = ct.SoLuong,
                                   DonGia = ct.DonGia,
                                   GhiChu = ct.GhiChu
                               }).ToList();

                return et;
            }
        }

        #endregion

        #region Thêm / Sửa / Xoá

        /// <summary>
        /// Tạo mới hoặc chỉnh sửa phiếu kho (header + nhiều dòng chi tiết).
        /// V3: IdKhoXuat/IdKhoNhap nằm ở từng dòng chi tiết, không còn ở header.
        /// </summary>
        public void LuuChungTu(ET_ChungTuKho et)
        {
            using (var db = new DaiNamDBDataContext())
            {
                ChungTu dbMaster;

                if (et.Id == 0)
                {
                    dbMaster = new ChungTu
                    {
                        MaChungTu = et.MaChungTu,
                        LoaiChungTu = et.LoaiChungTu,
                        IdDoiTac = et.IdDoiTac,
                        IdDonHang = et.IdDonHang,
                        IdBaoTri = et.IdBaoTri,
                        NgayChungTu = et.NgayChungTu,
                        LyDo = et.LyDo,
                        TrangThai = et.TrangThai ?? AppConstants.TrangThaiChungTuKho.Moi,
                        IdNguoiTao = et.IdNguoiTao,
                        GhiChu = et.GhiChu,
                        NgayTao = DateTime.Now
                    };
                    db.ChungTus.InsertOnSubmit(dbMaster);
                }
                else
                {
                    dbMaster = db.ChungTus.FirstOrDefault(c => c.Id == et.Id);
                    if (dbMaster == null)
                        throw new InvalidOperationException("Không tìm thấy chứng từ trong CSDL.");

                    if (dbMaster.TrangThai == AppConstants.TrangThaiChungTuKho.DaDuyet || dbMaster.TrangThai == AppConstants.TrangThaiChungTuKho.DaHuy)
                        throw new InvalidOperationException($"Chứng từ đang ở trạng thái '{dbMaster.TrangThai}', không được phép chỉnh sửa!");

                    dbMaster.IdDoiTac = et.IdDoiTac;
                    dbMaster.NgayChungTu = et.NgayChungTu;
                    dbMaster.LyDo = et.LyDo;
                    dbMaster.GhiChu = et.GhiChu;

                    // Xóa chi tiết cũ rồi insert lại
                    var oldDetails = db.ChiTietChungTus.Where(d => d.IdChungTu == et.Id);
                    db.ChiTietChungTus.DeleteAllOnSubmit(oldDetails);
                }

                // V3: mỗi dòng chi tiết mang IdKhoXuat/IdKhoNhap riêng
                foreach (var chiTiet in et.ChiTiets)
                {
                    var dbDetail = new ChiTietChungTu
                    {
                        IdSanPham = chiTiet.IdSanPham,
                        IdLoHang = chiTiet.IdLoHang,
                        IdKhoXuat = chiTiet.IdKhoXuat,
                        IdKhoNhap = chiTiet.IdKhoNhap,
                        SoLuong = chiTiet.SoLuong,
                        DonGia = chiTiet.DonGia,
                        GhiChu = chiTiet.GhiChu
                    };
                    dbMaster.ChiTietChungTus.Add(dbDetail);
                }

                db.SubmitChanges();
            }
        }

        /// <summary>
        /// Xóa phiếu kho (chỉ được xóa khi chưa duyệt).
        /// </summary>
        public void XoaChungTu(int idChungTu)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var dbMaster = db.ChungTus.FirstOrDefault(c => c.Id == idChungTu);
                if (dbMaster == null) return;

                if (dbMaster.TrangThai == AppConstants.TrangThaiChungTuKho.DaDuyet)
                    throw new InvalidOperationException("Không thể xóa chứng từ đã duyệt!");

                var chiTiets = db.ChiTietChungTus.Where(d => d.IdChungTu == idChungTu);
                db.ChiTietChungTus.DeleteAllOnSubmit(chiTiets);
                db.ChungTus.DeleteOnSubmit(dbMaster);
                db.SubmitChanges();
            }
        }

        #endregion

        #region Duyệt Phiếu

        /// <summary>
        /// Duyệt phiếu kho: chuyển TrangThai → DaDuyet.
        /// V3: Không còn ghi SoCai — tồn kho được tính trực tiếp từ ChiTietChungTu qua index.
        /// </summary>
        /// <param name="idChungTu">Id phiếu cần duyệt</param>
        /// <param name="idNguoiDuyet">Nhân viên thực hiện duyệt</param>
        public void DuyetChungTu(int idChungTu, int idNguoiDuyet)
        {
            using (var db = new DaiNamDBDataContext())
            {
                // chỉ lấy phiếu khi TrangThai còn đang là 'Moi'.
                // nếu người khác duyệt trước, query trả null → throw ngay.
                var dbMaster = db.ChungTus.FirstOrDefault(c =>
                    c.Id == idChungTu &&
                    c.TrangThai == AppConstants.TrangThaiChungTuKho.Moi);

                if (dbMaster == null)
                    throw new InvalidOperationException("Phiếu không tồn tại hoặc đã được duyệt/hủy bởi người khác. Vui lòng tải lại danh sách.");

                dbMaster.TrangThai    = AppConstants.TrangThaiChungTuKho.DaDuyet;
                dbMaster.IdNguoiDuyet1 = idNguoiDuyet;
                dbMaster.NgayDuyet1   = DateTime.Now;

                db.SubmitChanges();
            }
        }

        public void DoiTrangThai(int idChungTu, string trangThaiMoi, int idNguoiCapNhat, string lyDo = null)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var ct = db.ChungTus.FirstOrDefault(c => c.Id == idChungTu);
                if (ct == null)
                    throw new InvalidOperationException("Chứng từ không tồn tại.");

                if (ct.TrangThai == AppConstants.TrangThaiChungTuKho.DaDuyet)
                    throw new InvalidOperationException("Không thể đổi trạng thái phiếu đã duyệt.");

                ct.TrangThai = trangThaiMoi;
                ct.GhiChu = string.IsNullOrEmpty(lyDo) ? ct.GhiChu : $"{ct.GhiChu} | {lyDo}";
                db.SubmitChanges();
            }
        }
        // Đọc chi tiết kiểm kê từ bảng Kho.ChiTietKiemKe (bảng riêng, tách khỏi ChiTietChungTu).
        public List<ET.Models.Kho.ET_ChiTietKiemKe> GetChiTietKiemKe(int idChungTu)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.ChiTietKiemKes
                    .Where(kk => kk.IdChungTu == idChungTu)
                    .Select(kk => new ET.Models.Kho.ET_ChiTietKiemKe
                    {
                        Id             = kk.Id,
                        IdChungTu      = kk.IdChungTu,
                        IdKho          = kk.IdKho,
                        IdSanPham      = kk.IdSanPham,
                        IdLoHang       = kk.IdLoHang,
                        TenSanPham     = kk.SanPham != null ? kk.SanPham.TenSanPham : "",
                        MaSanPham      = kk.SanPham != null ? kk.SanPham.MaSanPham  : "",
                        TenKho         = kk.KhoHang != null ? kk.KhoHang.TenKho     : "",
                        SoLuongHeThong = kk.SoLuongHeThong,
                        SoLuongThucTe  = kk.SoLuongThucTe,
                        LyDoChenhLech  = kk.LyDoChenhLech,
                        GhiChu         = kk.GhiChu
                    })
                    .ToList();
            }
        }
        // Thêm các dòng ChiTietChungTu điều chỉnh chênh lệch kiểm kê vào phiếu.
        public void ThemChiTietDieuChinh(int idChungTu, List<ET.Models.Kho.ET_ChiTietChungTu> dsChiTiet)
        {
            if (dsChiTiet == null || !dsChiTiet.Any()) return;

            using (var db = new DaiNamDBDataContext())
            {
                var ct = db.ChungTus.FirstOrDefault(c => c.Id == idChungTu);
                if (ct == null) throw new InvalidOperationException("Chứng từ kiểm kê không tồn tại.");
                if (ct.TrangThai == AppConstants.TrangThaiChungTuKho.DaDuyet)
                    throw new InvalidOperationException("Phiếu đã duyệt.");

                foreach (var item in dsChiTiet)
                {
                    db.ChiTietChungTus.InsertOnSubmit(new ChiTietChungTu
                    {
                        IdChungTu = idChungTu,
                        IdSanPham = item.IdSanPham,
                        IdLoHang  = item.IdLoHang,
                        IdKhoXuat = item.IdKhoXuat,
                        IdKhoNhap = item.IdKhoNhap,
                        SoLuong   = item.SoLuong,
                        DonGia    = item.DonGia,
                        GhiChu    = item.GhiChu
                    });
                }
                db.SubmitChanges();
            }
        }

        #endregion
    }
}



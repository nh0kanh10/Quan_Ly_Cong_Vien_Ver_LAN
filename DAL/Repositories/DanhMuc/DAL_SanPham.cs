using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using ET.Constants;

namespace DAL.Repositories.DanhMuc
{
    public class DAL_SanPham
    {
        public static DAL_SanPham Instance { get; } = new DAL_SanPham();

        #region Truy vấn dữ liệu
        /// <summary>
        /// Lấy danh sách sản phẩm kèm dịch thuật đa ngôn ngữ.
        /// </summary>
        /// <param name="loaiSPFilter">Bộ lọc theo loại sản phẩm (null = tất cả)</param>
        /// <param name="langCode">Mã ngôn ngữ (vi-VN, en-US, zh-CN, ja-JP)</param>
        /// <returns>Danh sách sản phẩm đã được dịch thuật</returns>
        public List<SanPham> LayDanhSach(string loaiSPFilter = null, string langCode = "vi-VN")
        {
            using (var db = new DaiNamDBDataContext())
            {
                var opts = new DataLoadOptions();
                opts.LoadWith<SanPham>(sp => sp.DonViTinh);
                db.LoadOptions = opts;

                var query = db.SanPhams.Where(sp => sp.DaXoa == false);

                if (!string.IsNullOrEmpty(loaiSPFilter))
                    query = query.Where(sp => sp.LoaiSanPham == loaiSPFilter);

                if (langCode == "vi-VN")
                {
                    return query.OrderByDescending(sp => sp.NgayTao).ToList();
                }
                var list = (from sp in query
                            join bd in db.BanDiches
                            on new { Id = sp.Id, Loai = AppConstants.ThucTheDich.SanPham, Cot = AppConstants.TruongDich.TenSanPham, NgonNgu = langCode }
                            equals new { Id = bd.IdThucThe, Loai = bd.LoaiThucThe, Cot = bd.TruongDich, NgonNgu = bd.NgonNgu } into dich
                            from d in dich.DefaultIfEmpty()
                            orderby sp.NgayTao descending
                            select new { SP = sp, TenDich = d != null ? d.NoiDung : sp.TenSanPham }).ToList();

                // Nạp đè dữ liệu dịch thẳng vào thực thể LINQ
                foreach (var item in list)
                {
                    item.SP.TenSanPham = item.TenDich;
                }

                return list.Select(x => x.SP).ToList();
            }
        }

        /// <summary>
        /// Nạp 1 sản phẩm kèm toàn bộ bảng con trong 1 truy vấn duy nhất.
        /// Dùng DataLoadOptions để tải trước dữ liệu, tránh lỗi truy vấn lặp N+1.
        /// Gộp 7 bảng: SanPham + BangGia + BangGia_ThueTheoGio
        ///   + Ve_QuyenTruyCap + SanPham_Ve + MonAn + DonViTinh + Menu_POs.
        /// </summary>
        /// <param name="idSanPham">Khoá chính bảng SanPham</param>
        /// <returns>null nếu không tìm thấy hoặc đã bị xoá mềm</returns>
        public SanPham LayChiTiet(int idSanPham)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var opts = new DataLoadOptions();
                opts.LoadWith<SanPham>(sp => sp.BangGias);
                opts.LoadWith<SanPham>(sp => sp.Ve_QuyenTruyCaps);
                opts.LoadWith<SanPham>(sp => sp.SanPham_Ve);
                opts.LoadWith<SanPham>(sp => sp.MonAn);
                opts.LoadWith<SanPham>(sp => sp.DonViTinh);
                opts.LoadWith<SanPham>(sp => sp.Menu_POs);
                opts.LoadWith<BangGia>(bg => bg.BangGia_ThueTheoGio);
                db.LoadOptions = opts;

                return db.SanPhams.SingleOrDefault(sp => sp.Id == idSanPham && sp.DaXoa == false);
            }
        }

        public List<QuyDoiDonVi> LayQuyDoiTheoSanPham(int idSanPham)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.QuyDoiDonVis
                    .Where(q => q.IdSanPham == idSanPham && q.ConHoatDong)
                    .ToList();
            }
        }

        /// <summary>
        /// BOM (định mức nguyên liệu) = công thức pha chế / chế biến.
        /// Ví dụ: 1 Ly Cafe Sữa cần 20g Cafe + 30ml Sữa đặc.
        /// </summary>
        /// <param name="idThanhPham">Id sản phẩm thành phẩm (món bán ra)</param>
        /// <returns>Danh sách nguyên liệu kèm định lượng</returns>
        public List<DinhMucNguyenLieu> LayDinhMuc(int idThanhPham)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.DinhMucNguyenLieus
                    .Where(d => d.IdThanhPham == idThanhPham)
                    .ToList();
            }
        }

        #endregion

        #region Dữ liệu tra cứu (đổ vào ComboBox / SearchLookUp)

        /// <summary>
        /// Lấy danh sách Đơn Vị Tính kèm ngôn ngữ.
        /// Cách làm đa ngôn ngữ Dữ Liệu (Data Localization):
        /// Bước 1: Nạp danh sách gốc bằng Tiếng Việt từ Database.
        /// Bước 2: Nếu tham số là vi-VN, trả về luôn.
        /// Bước 3: Nếu là ngoại ngữ, lấy các bản dịch tương ứng từ bảng BanDich nạp vào 1 Dictionary (kiểu cấu trúc từ điển Key-Value).
        /// Bước 4: Chạy vòng lặp qua danh sách gốc, đè chữ đã dịch vào thuộc tính TenDonVi rồi trả về UI.
        /// </summary>
        /// <param name="langCode">Mã Locale (Ví dụ: "vi-VN", "en-US", "zh-CN") lấy từ SessionManager</param>
        /// <returns>Danh sách Entity Đơn Vị Tính có thuộc tính TenDonVi đã dịch</returns>
        public List<DonViTinh> LayDonViTinh(string langCode = "vi-VN")
        {
            using (var db = new DaiNamDBDataContext())
            {
                var query = db.DonViTinhs.OrderBy(d => d.TenDonVi).ToList();

                if (langCode == "vi-VN") return query;

                var dicQuery = db.BanDiches
                                 .Where(b => b.NgonNgu == langCode && b.LoaiThucThe == AppConstants.ThucTheDich.DonViTinh && b.TruongDich == AppConstants.TruongDich.TenDonVi)
                                 .ToDictionary(b => b.IdThucThe, b => b.NoiDung);

                foreach (var item in query)
                {
                    if (dicQuery.TryGetValue(item.Id, out var translated))
                        item.TenDonVi = translated;
                }

                return query;
            }
        }

        public List<DiemBanHang_PO> LayDiemBanPOS()
        {
            using (var db = new DaiNamDBDataContext())
                return db.DiemBanHang_POs.ToList();
        }

        public List<TroChoi> LayTroChoi()
        {
            using (var db = new DaiNamDBDataContext())
                return db.TroChois.Where(t => t.DaXoa == false).ToList();
        }

        public List<NhaHang> LayNhaHang()
        {
            using (var db = new DaiNamDBDataContext())
                return db.NhaHangs.ToList();
        }

        /// <summary>
        /// Lấy danh sách Thuế VAT kèm ngôn ngữ.
        /// Dùng thuật toán Dictionary trét chữ (In-Memory overlay) giống hệt hàm LayDonViTinh.
        /// Tách riêng query BanDich.
        /// </summary>
        /// <param name="langCode">Mã ngôn ngữ truyền vào để tra quy tắc dịch tại bảng BanDich</param>
        /// <returns>Danh sách thuế VAT cấu hình</returns>
        public List<CauHinhThue> LayCauHinhThue(string langCode = "vi-VN")
        {
            using (var db = new DaiNamDBDataContext())
            {
                var query = db.CauHinhThues.ToList();

                if (langCode == "vi-VN") return query;

                var dicQuery = db.BanDiches
                                 .Where(b => b.NgonNgu == langCode && b.LoaiThucThe == AppConstants.ThucTheDich.CauHinhThue && b.TruongDich == AppConstants.TruongDich.TenThue)
                                 .ToDictionary(b => b.IdThucThe, b => b.NoiDung);

                foreach (var item in query)
                {
                    if (dicQuery.TryGetValue(item.Id, out var translated))
                        item.TenThue = translated;
                }

                return query;
            }
        }

        /// <summary>
        /// Chỉ lấy SP có cờ LaVatTu = true (nguyên liệu thô, vật tư tiêu hao).
        /// Dùng cho cửa sổ BOM để tránh nhét Vé hay Dịch vụ vào làm nguyên liệu.
        /// </summary>
        /// <returns>Danh sách vật tư chưa bị xoá, sắp theo tên</returns>
        public List<SanPham> LayVatTuKho()
        {
            using (var db = new DaiNamDBDataContext())
            {
                var opts = new DataLoadOptions();
                opts.LoadWith<SanPham>(sp => sp.DonViTinh);
                db.LoadOptions = opts;

                return db.SanPhams
                    .Where(sp => sp.LaVatTu && sp.DaXoa == false)
                    .OrderBy(sp => sp.TenSanPham)
                    .ToList();
            }
        }

        #endregion

        #region Kiểm tra ràng buộc

        public bool KiemTraTrungMa(string maCode, int? excludeId = null)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var query = db.SanPhams.Where(sp => sp.MaSanPham == maCode);
                if (excludeId.HasValue)
                    query = query.Where(sp => sp.Id != excludeId.Value);
                return query.Any();
            }
        }

        /// <summary>
        /// Chặn xoá mềm nếu SP đang dính chứng từ chưa hoàn tất.
        /// Quét 2 bảng: ChiTietDonHang (đơn chưa huỷ/chưa xong)
        ///   + ChiTietChungTu (phiếu kho chưa duyệt/chưa huỷ).
        /// </summary>
        /// <param name="idSanPham">Khoá chính bảng SanPham</param>
        /// <returns>true = CÓ ràng buộc, KHÔNG được xoá</returns>
        public bool CoDonHangHoacChungTuTreo(int idSanPham)
        {
            using (var db = new DaiNamDBDataContext())
            {
                bool coDonHang = db.ChiTietDonHangs
                    .Any(ct => ct.IdSanPham == idSanPham
                        && ct.DonHang.TrangThai != AppConstants.TrangThaiDonHang.DaHuy
                        && ct.DonHang.TrangThai != AppConstants.TrangThaiDonHang.DaThanhToan);

                bool coChungTu = db.ChiTietChungTus
                    .Any(ct => ct.IdSanPham == idSanPham
                        && ct.ChungTu.TrangThai != AppConstants.TrangThaiChungTuKho.DaDuyet
                        && ct.ChungTu.TrangThai != AppConstants.TrangThaiChungTuKho.DaHuy);

                return coDonHang || coChungTu;
            }
        }

        public bool CoTonKho(int idSanPham)
        {
            using (var db = new DaiNamDBDataContext())
            {
                // tính tồn từ ChiTietChungTu (phiếu DaDuyet, kho thật LaKhoAo=false)
                decimal nhap = db.ChiTietChungTus
                    .Where(ct => ct.IdSanPham == idSanPham
                              && ct.ChungTu.TrangThai == ET.Constants.AppConstants.TrangThaiChungTuKho.DaDuyet
                              && !ct.KhoHang1.LaKhoAo)
                    .Sum(ct => (decimal?)ct.SoLuong) ?? 0m;

                decimal xuat = db.ChiTietChungTus
                    .Where(ct => ct.IdSanPham == idSanPham
                              && ct.ChungTu.TrangThai == ET.Constants.AppConstants.TrangThaiChungTuKho.DaDuyet
                              && !ct.KhoHang.LaKhoAo)
                    .Sum(ct => (decimal?)ct.SoLuong) ?? 0m;

                return (nhap - xuat) > 0;
            }
        }

        #endregion

        #region Thêm / Sửa / Xoá

        /// <summary>
        /// Thêm mới sản phẩm đa hình.
        /// Đối tượng truyền vào đã gắn sẵn các bảng con
        /// (BangGias, SanPham_Ve, Ve_QuyenTruyCaps, MonAn...).
        /// SubmitChanges() tự bọc giao dịch, lưu tất cả hoặc huỷ sạch.
        /// </summary>
        /// <param name="entity">SanPham đã gắn đầy đủ bảng con</param>
        /// <returns>Id vừa được sinh tự động</returns>
        public int ThemMoi(SanPham entity, List<QuyDoiDonVi> dsQuyDoi, DaiNamDBDataContext sharedDb = null)
        {
            bool ownsDb = sharedDb == null;
            var db = sharedDb ?? new DaiNamDBDataContext();
            try
            {
                if (ownsDb) db.Connection.Open();
                using (var tx = ownsDb ? db.Connection.BeginTransaction() : null)
                {
                    if (ownsDb) db.Transaction = tx;
                    try
                    {
                        db.SanPhams.InsertOnSubmit(entity);
                        
                        if (dsQuyDoi != null && dsQuyDoi.Count > 0)
                        {
                            foreach (var q in dsQuyDoi)
                            {
                                q.IdSanPham = entity.Id;
                                db.QuyDoiDonVis.InsertOnSubmit(q);
                            }
                        }

                        if (ownsDb)
                        {
                            db.SubmitChanges();
                            tx.Commit();
                        }
                        return entity.Id;
                    }
                    catch
                    {
                        if (ownsDb) tx.Rollback();
                        throw;
                    }
                }
            }
            finally
            {
                if (ownsDb) db.Dispose();
            }
        }

        /// <summary>
        /// Cập nhật sản phẩm và toàn bộ bảng con liên quan.
        /// LINQ to SQL không hỗ trợ gắn lại đối tượng từ ngữ cảnh khác,
        /// nên phải: lấy bản gốc từ DB -> sao chép giá trị mới lên -> lưu.
        ///
        /// Chiến lược bảng con: Xoá hết dòng cũ, chèn lại dòng mới.
        /// Lý do: Người dùng có thể xoá/thêm/sửa nhiều dòng BangGia cùng lúc,
        /// so sánh từng dòng để gộp quá phức tạp mà không lợi gì thêm.
        ///
        /// Thứ tự xoá: bảng con trước, bảng cha sau (tránh vi phạm khoá ngoại).
        /// B1: Xoá BangGia_ThueTheoGio (con của BangGia)
        /// B2: Xoá BangGia
        /// B3: Chèn lại BangGia mới (kèm ThueTheoGio nếu SP cho thuê)
        /// B4: Cập nhật/tạo SanPham_Ve + thay thế Ve_QuyenTruyCap
        /// B5: Cập nhật/tạo MonAn + thay thế DinhMucNguyenLieu
        /// </summary>
        /// <param name="entityMoi">Đối tượng SanPham chứa giá trị mới</param>
        /// <param name="dsBangGia">Danh sách bảng giá mới thay thế</param>
        /// <param name="dsQuyenVe">Danh sách quyền quẹt vé mới (null nếu không phải Vé)</param>
        /// <param name="cauHinhVe">Cấu hình vé (null nếu không phải Vé)</param>
        /// <param name="cauHinhMonAn">Cấu hình món ăn (null nếu không phải F&B)</param>
        /// <param name="dsDinhMuc">Danh sách định mức BOM (null nếu không phải F&B)</param>
        public void CapNhat(SanPham entityMoi,
            List<BangGia> dsBangGia,
            List<Ve_QuyenTruyCap> dsQuyenVe,
            SanPham_Ve cauHinhVe,
            MonAn cauHinhMonAn,
            List<DinhMucNguyenLieu> dsDinhMuc,
            List<QuyDoiDonVi> dsQuyDoi,
            DaiNamDBDataContext sharedDb = null)
        {
            bool ownsDb = sharedDb == null;
            var db = sharedDb ?? new DaiNamDBDataContext();
            try
            {
                if (ownsDb) db.Connection.Open();
                using (var tx = ownsDb ? db.Connection.BeginTransaction() : null)
                {
                    if (ownsDb) db.Transaction = tx;
                    try
                    {
                        var opts = new DataLoadOptions();
                        opts.LoadWith<SanPham>(sp => sp.BangGias);
                        opts.LoadWith<SanPham>(sp => sp.Ve_QuyenTruyCaps);
                        opts.LoadWith<SanPham>(sp => sp.SanPham_Ve);
                        opts.LoadWith<SanPham>(sp => sp.MonAn);
                        opts.LoadWith<BangGia>(bg => bg.BangGia_ThueTheoGio);
                        db.LoadOptions = opts;

                        var goc = db.SanPhams.Single(sp => sp.Id == entityMoi.Id);

                        // Cập nhật bảng gốc (LoaiSanPham bị bỏ qua — không cho đổi loại)
                        goc.MaSanPham = entityMoi.MaSanPham;
                        goc.TenSanPham = entityMoi.TenSanPham;
                        goc.IdDonViTinh = entityMoi.IdDonViTinh;
                        goc.DonGia = entityMoi.DonGia;
                        goc.AnhDaiDien = entityMoi.AnhDaiDien;
                        goc.LaVatTu = entityMoi.LaVatTu;
                        goc.CanQuanLyLo = entityMoi.CanQuanLyLo;
                        goc.TrangThai = entityMoi.TrangThai;

                        // B1-B2: Xoá bảng con trước bảng cha
                        // Giải quyết triệt để lỗi Trigger "Thời gian chồng chéo": 
                        // Do LINQ to SQL luôn chạy INSERT trước DELETE nên trigger bắt lỗi trùng.
                        // Ta buộc nó DELETE trước rồi SubmitChanges() ngay.
                        var oldThueGio = goc.BangGias
                            .Where(bg => bg.BangGia_ThueTheoGio != null)
                            .Select(bg => bg.BangGia_ThueTheoGio)
                            .ToList();
                        db.BangGia_ThueTheoGios.DeleteAllOnSubmit(oldThueGio);
                        db.BangGias.DeleteAllOnSubmit(goc.BangGias);

                        if (cauHinhVe != null)
                        {
                            db.Ve_QuyenTruyCaps.DeleteAllOnSubmit(goc.Ve_QuyenTruyCaps);
                        }

                        if (cauHinhMonAn != null)
                        {
                            var oldBom = db.DinhMucNguyenLieus
                                .Where(d => d.IdThanhPham == goc.Id)
                                .ToList();
                            db.DinhMucNguyenLieus.DeleteAllOnSubmit(oldBom);
                        }

                        // Xoá bản ghi cũ khỏi DB ngay lập tức
                        db.SubmitChanges();

                        // B3: Chèn bảng giá mới
                        foreach (var bg in dsBangGia)
                        {
                            bg.IdSanPham = goc.Id;
                            db.BangGias.InsertOnSubmit(bg);
                        }

                        // B4: Cấu hình Vé (chỉ chạy nếu SP thuộc nhóm Vé)
                        if (cauHinhVe != null)
                        {
                            if (goc.SanPham_Ve != null)
                            {
                                goc.SanPham_Ve.LoaiVe = cauHinhVe.LoaiVe;
                                goc.SanPham_Ve.DoiTuongVe = cauHinhVe.DoiTuongVe;
                                goc.SanPham_Ve.CanTaoToken = cauHinhVe.CanTaoToken;
                            }
                            else
                            {
                                cauHinhVe.IdSanPham = goc.Id;
                                db.SanPham_Ves.InsertOnSubmit(cauHinhVe);
                            }

                            foreach (var qv in dsQuyenVe)
                            {
                                qv.IdSanPhamVe = goc.Id;
                                db.Ve_QuyenTruyCaps.InsertOnSubmit(qv);
                            }
                        }

                        // B5: Cấu hình Món ăn (chỉ chạy nếu SP thuộc nhóm F&B)
                        if (cauHinhMonAn != null)
                        {
                            if (goc.MonAn != null)
                            {
                                goc.MonAn.IdNhaHang = cauHinhMonAn.IdNhaHang;
                                goc.MonAn.CoDiUng = cauHinhMonAn.CoDiUng;
                                goc.MonAn.PhanLoai = cauHinhMonAn.PhanLoai;
                                goc.MonAn.MoTaNgan = cauHinhMonAn.MoTaNgan;
                                goc.MonAn.AnHienMenu = cauHinhMonAn.AnHienMenu;
                            }
                            else
                            {
                                cauHinhMonAn.IdSanPham = goc.Id;
                                db.MonAns.InsertOnSubmit(cauHinhMonAn);
                            }

                            foreach (var dm in dsDinhMuc)
                            {
                                dm.IdThanhPham = goc.Id;
                                db.DinhMucNguyenLieus.InsertOnSubmit(dm);
                            }
                        }

                        // B6: Quy đổi ĐVT — xoá cũ theo SP, chèn lại mới
                        var oldQuyDoi = db.QuyDoiDonVis.Where(q => q.IdSanPham == goc.Id).ToList();
                        db.QuyDoiDonVis.DeleteAllOnSubmit(oldQuyDoi);
                        db.SubmitChanges();

                        if (dsQuyDoi != null && dsQuyDoi.Count > 0)
                        {
                            foreach (var q in dsQuyDoi)
                            {
                                q.Id = 0;
                                q.IdSanPham = goc.Id;
                                db.QuyDoiDonVis.InsertOnSubmit(q);
                            }
                        }

                        // Lưu các bản ghi mới
                        if (ownsDb)
                        {
                            db.SubmitChanges();
                            tx.Commit();
                        }
                    }
                    catch
                    {
                        if (ownsDb) tx.Rollback();
                        throw;
                    }
                }
            }
            finally
            {
                if (ownsDb) db.Dispose();
            }
        }

        /// <summary>
        /// Xoá mềm: giữ lại dữ liệu lịch sử đơn hàng, chỉ ẩn khỏi danh sách.
        /// Tầng BUS phải kiểm tra tồn kho + đơn treo TRƯỚC khi gọi hàm này.
        /// </summary>
        /// <param name="idSanPham">Khoá chính bảng SanPham</param>
        public void XoaMem(int idSanPham)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var sp = db.SanPhams.Single(s => s.Id == idSanPham);
                sp.DaXoa = true;
                db.SubmitChanges();
            }
        }

        #endregion
    }
}



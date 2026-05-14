using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ET.Constants;
using ET.DTOs;
using ET.Models.BanHang;
using ET.Models.DanhMuc;

namespace DAL.Repositories.BanHang
{
    public class DAL_ThueDo
    {
        public static DAL_ThueDo Instance { get; } = new DAL_ThueDo();

        #region Truy vấn dữ liệu

        /// <summary>
        /// Lấy danh sách nguồn cho thuê tại Điểm Bán hiện tại (Phễu lọc 2 lớp: Quyền Bán Hàng + Khu Vực).
        /// Hỗ trợ cả Tài sản không định danh (Phao) và Tài sản định danh (Tủ, Xe, Chòi).
        /// </summary>
        public List<DTO_NguonChoThueView> LayDanhSachNguonChoThue(int idDiemBan, string langCode = "vi-VN")
        {
            using (var db = new DaiNamDBDataContext())
            {
                var now = DateTime.Now.Date;
                var result = new List<DTO_NguonChoThueView>();

                // Lấy thông tin POS để biết đang đứng ở Khu Vực nào
                var pos = db.DiemBanHang_POs.FirstOrDefault(p => p.Id == idDiemBan);
                if (pos == null) return result;
                int idKhuVucPos = pos.IdKhuVuc;

                //  1: Lấy danh sách IdSanPham được phép kinh doanh tại POS này
                var allowedProductIds = db.Menu_POs.Where(m => m.IdDiemBan == idDiemBan).Select(m => m.IdSanPham).ToList();
                if (!allowedProductIds.Any()) return result;

                //  Truy vấn chung thông tin Giá & Tên của các Sản Phẩm được phép bán 
                var products = (from sp in db.SanPhams
                                where allowedProductIds.Contains(sp.Id)
                                      && sp.TrangThai == AppConstants.TrangThaiSanPham.DangBan
                                      && !sp.DaXoa
                                      
                                join bgTmp in db.BangGias.Where(b => b.HieuLucTu <= now && (b.HieuLucDen == null || b.HieuLucDen >= now))
                                on sp.Id equals bgTmp.IdSanPham into bgGroup
                                from bg in bgGroup.DefaultIfEmpty()

                                join tgTmp in db.BangGia_ThueTheoGios on bg.Id equals tgTmp.IdBangGia into tgGroup
                                from tg in tgGroup.DefaultIfEmpty()

                                join bdTmp in db.BanDiches on new { Id = sp.Id, Loai = AppConstants.ThucTheDich.SanPham, Cot = AppConstants.TruongDich.TenSanPham, NgonNgu = langCode }
                                equals new { Id = bdTmp.IdThucThe, Loai = bdTmp.LoaiThucThe, Cot = bdTmp.TruongDich, NgonNgu = bdTmp.NgonNgu } into dichGroup
                                from bd in dichGroup.DefaultIfEmpty()

                                select new
                                {
                                    sp.Id,
                                    sp.MaSanPham,
                                    TenSanPham = (langCode != "vi-VN" && bd != null) ? bd.NoiDung : sp.TenSanPham,
                                    TienThue = bg != null ? bg.GiaBan : sp.DonGia ?? 0m,
                                    TienCoc = tg != null ? (tg.TienCoc ?? 0m) : 0m,
                                    sp.LoaiSanPham
                                }).ToList();

                // 1. Phao bơi / Khăn tắm (KHÔNG định danh) - Dựa hoàn toàn vào Menu_POS, không check vị trí vật lý
                var nonSerialized = products.Where(p => p.LoaiSanPham == AppConstants.LoaiSanPham.DoChoThue).ToList();
                foreach (var p in nonSerialized)
                {
                    result.Add(new DTO_NguonChoThueView
                    {
                        IdSanPham = p.Id,
                        IdTaiSanChoThue = null,
                        MaHienThi = p.MaSanPham,
                        TenHienThi = p.TenSanPham,
                        TienThue = p.TienThue,
                        TienCoc = p.TienCoc,
                        IsDinhDanh = false,
                        TrangThai = AppConstants.TrangThaiTaiSan.SanSang
                    });
                }

                // 2.  2: Tài sản định danh (Phải Check Vị Trí Vật Lý = IdKhuVuc của máy POS)
                var serializedIds = products.Where(p => p.LoaiSanPham != AppConstants.LoaiSanPham.DoChoThue).Select(p => p.Id).ToList();
                if (serializedIds.Any())
                {
                    // 2a. Tủ Đồ (Cố định -> Check IdKhuVuc)
                    var tuDos = (from ts in db.TaiSanChoThues
                                 join t in db.TuDos on ts.Id equals t.IdTaiSan
                                 where t.IdKhuVuc == idKhuVucPos
                                       && ts.TrangThai == AppConstants.TrangThaiTaiSan.SanSang
                                       && !ts.DaXoa
                                       && ts.IdSanPham.HasValue && serializedIds.Contains(ts.IdSanPham.Value)
                                 select new { ts.Id, ts.MaVachThietBi, ts.TenTaiSan, IdSanPham = ts.IdSanPham.Value }).ToList();

                    // 2b. Chòi Nghỉ Mát (Cố định -> Check IdKhuVuc)
                    var chois = (from ts in db.TaiSanChoThues
                                 join c in db.ChoiNghiMats on ts.Id equals c.IdTaiSan
                                 where c.IdKhuVuc == idKhuVucPos
                                       && ts.TrangThai == AppConstants.TrangThaiTaiSan.SanSang
                                       && !ts.DaXoa
                                       && ts.IdSanPham.HasValue && serializedIds.Contains(ts.IdSanPham.Value)
                                 select new { ts.Id, ts.MaVachThietBi, ts.TenTaiSan, IdSanPham = ts.IdSanPham.Value }).ToList();

                    // 2c. Phương Tiện Di Chuyển (Lưu động -> Check IdKhuVucHienTai)
                    var xeCos = (from ts in db.TaiSanChoThues
                                 join x in db.PhuongTienDiChuyens on ts.Id equals x.IdTaiSan
                                 where x.IdKhuVucHienTai == idKhuVucPos
                                       && ts.TrangThai == AppConstants.TrangThaiTaiSan.SanSang
                                       && !ts.DaXoa
                                       && ts.IdSanPham.HasValue && serializedIds.Contains(ts.IdSanPham.Value)
                                 select new { ts.Id, ts.MaVachThietBi, ts.TenTaiSan, IdSanPham = ts.IdSanPham.Value }).ToList();

                    var allAssets = tuDos.Concat(chois).Concat(xeCos).ToList();

                    foreach (var asset in allAssets)
                    {
                        var prodInfo = products.FirstOrDefault(p => p.Id == asset.IdSanPham);
                        if (prodInfo != null)
                        {
                            result.Add(new DTO_NguonChoThueView
                            {
                                IdSanPham = prodInfo.Id,
                                IdTaiSanChoThue = asset.Id,
                                MaHienThi = asset.MaVachThietBi,
                                TenHienThi = string.IsNullOrEmpty(asset.TenTaiSan) ? prodInfo.TenSanPham : $"{prodInfo.TenSanPham} ({asset.TenTaiSan})",
                                TienThue = prodInfo.TienThue,
                                TienCoc = prodInfo.TienCoc,
                                IsDinhDanh = true,
                                TrangThai = AppConstants.TrangThaiTaiSan.SanSang
                            });
                        }
                    }
                }

                return result.OrderBy(r => r.IsDinhDanh).ThenBy(r => r.TenHienThi).ToList();
            }
        }

        /// <summary>
        /// Lấy danh sách phiên thuê chưa trả (TrangThai = DangThue).
        /// Dùng cho grid giám sát nhúng ở ucGiaoDo và ucNhanTra.
        /// <param name="tuNgay">Ngày bắt đầu lấy dữ liệu.</param>
        /// <param name="denNgay">Ngày kết thúc lấy dữ liệu.</param>
        /// <returns>Danh sách phiên thuê chưa trả.</returns>
        /// </summary>
        public List<DTO_PhienChuaTraView> LayDanhSachChuaTra(DateTime tuNgay, DateTime denNgay)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return (from td in db.ThueDoChiTiets
                        join ct in db.ChiTietDonHangs on td.IdChiTietDonHang equals ct.Id
                        join dh in db.DonHangs on ct.IdDonHang equals dh.Id
                        join sp in db.SanPhams on ct.IdSanPham equals sp.Id

                        join khTmp in db.KhachHangs on dh.IdKhachHang equals khTmp.IdDoiTac into khGroup
                        from kh in khGroup.DefaultIfEmpty()
                        join ttTmp in db.ThongTins on kh.IdDoiTac equals ttTmp.Id into ttGroup
                        from tt in ttGroup.DefaultIfEmpty()

                        where td.TrangThai == AppConstants.TrangThaiThueDo.DangThue
                              && td.ThoiGianThue >= tuNgay && td.ThoiGianThue <= denNgay

                        orderby td.ThoiGianThue descending
                        select new DTO_PhienChuaTraView
                        {
                            IdThueDoChiTiet = td.Id,
                            IdSanPham = sp.Id,
                            TenSanPham = sp.TenSanPham,
                            SoLuong = td.SoLuong,
                            ThoiGianThue = td.ThoiGianThue,
                            MaDonHang = dh.MaDonHang,
                            TenKhachHang = tt != null ? tt.HoTen : null
                        }).ToList();
            }
        }

        /// Tìm phiên thuê theo mã đơn hàng (biên lai giấy).
        /// Khách vãng lai đưa mã DH-xxx -> NV nhập -> tìm phiên thuê.
        public List<ET_ThueDoChiTiet> LayTheoMaDon(string maDonHang)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return (from td in db.ThueDoChiTiets
                        join ct in db.ChiTietDonHangs on td.IdChiTietDonHang equals ct.Id
                        join dh in db.DonHangs on ct.IdDonHang equals dh.Id
                        join sp in db.SanPhams on ct.IdSanPham equals sp.Id
                        where dh.MaDonHang == maDonHang
                              && td.TrangThai == AppConstants.TrangThaiThueDo.DangThue
                        select new ET_ThueDoChiTiet
                        {
                            Id = td.Id,
                            IdChiTietDonHang = td.IdChiTietDonHang,
                            IdTaiSanChoThue = td.IdTaiSanChoThue,
                            SoLuong = td.SoLuong,
                            ThoiGianThue = td.ThoiGianThue,
                            TrangThaiCoc = td.TrangThaiCoc,
                            TienCoc = td.TienCoc,
                            TrangThai = td.TrangThai,
                            IdSanPham = sp.Id,
                            TenSanPham = sp.TenSanPham,
                            MaDonHang = dh.MaDonHang
                        }).ToList();
            }
        }

        /// Tìm phiên thuê theo khách hàng (quẹt RFID -> IdKhachHang -> tìm đồ đang thuê).
        public List<ET_ThueDoChiTiet> LayTheoKhachHang(int idKhachHang)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return (from td in db.ThueDoChiTiets
                        join ct in db.ChiTietDonHangs on td.IdChiTietDonHang equals ct.Id
                        join dh in db.DonHangs on ct.IdDonHang equals dh.Id
                        join sp in db.SanPhams on ct.IdSanPham equals sp.Id
                        where dh.IdKhachHang == idKhachHang
                              && td.TrangThai == AppConstants.TrangThaiThueDo.DangThue
                        select new ET_ThueDoChiTiet
                        {
                            Id = td.Id,
                            IdChiTietDonHang = td.IdChiTietDonHang,
                            IdTaiSanChoThue = td.IdTaiSanChoThue,
                            SoLuong = td.SoLuong,
                            ThoiGianThue = td.ThoiGianThue,
                            TrangThaiCoc = td.TrangThaiCoc,
                            TienCoc = td.TienCoc,
                            TrangThai = td.TrangThai,
                            IdSanPham = sp.Id,
                            TenSanPham = sp.TenSanPham,
                            MaDonHang = dh.MaDonHang
                        }).ToList();
            }
        }

        #endregion

        #region Nghiệp vụ đặc thù

        /// <summary>
        /// Giao dịch giao đồ thuê: tạo DonHang + ChiTietDonHang + ThueDoChiTiet + ChungTuTC thu cọc.
        /// Nếu thanh toán RFID thì trừ ví. Nếu có tài sản vật lý thì đổi trạng thái sang DangThue.
        /// Toàn bộ chạy trong 1 transaction.
        /// <param name="req">Yêu cầu giao đồ thuê.</param>
        /// <returns>Mã đơn hàng vừa tạo (biên lai)</returns>
        /// </summary>
        public string GiaoDo(DTO_RentalCheckoutRequest req)
        {
            using (var db = new DaiNamDBDataContext())
            {
                db.Connection.Open();
                using (var tx = db.Connection.BeginTransaction())
                {
                    db.Transaction = tx;
                    try
                    {
                        string maDon = $"DT-{DateTime.Now:yyMMdd-HHmm}-{Guid.NewGuid().ToString().Substring(0, 4).ToUpper()}";

                        // Tính tổng
                        decimal tongThue = req.GioThue.Sum(x => x.TongThue);
                        decimal tongCoc = req.GioThue.Sum(x => x.TongCoc);
                        decimal tongTT = tongThue + tongCoc;

                        // 1. Tạo đơn hàng
                        var donHang = new DonHang
                        {
                            MaDonHang = maDon,
                            IdKhachHang = req.IdKhachHang,
                            IdNhanVien = req.IdNhanVien,
                            // Gắn phiên thu ngân để đơn thuê xuất hiện trong báo cáo ca làm việc
                            IdPhienThuNgan = req.IdPhienThuNgan > 0 ? (int?)req.IdPhienThuNgan : null,
                            NguonBan = AppConstants.NguonBan.TrucTiep,
                            TongTienHang = tongThue,
                            TongGiamGia = 0m,
                            TienThueVAT = 0m,
                            // Tiền cọc KHÔNG nhét vào TienPhiDichVu — sai ngữ nghĩa kế toán.
                            // Tiền cọc được theo dõi qua ChungTuTC loại THU_COC và ThueDoChiTiet.TienCoc.
                            TienPhiDichVu = 0m,
                            TongThanhToan = tongTT,
                            TrangThai = AppConstants.TrangThaiDonHang.DaThanhToan,
                            GhiChu = "Thuê đồ",
                            NgayTao = DateTime.Now
                        };
                        db.DonHangs.InsertOnSubmit(donHang);
                        db.SubmitChanges();

                        // 2. Tạo chi tiết + phiên thuê
                        foreach (var item in req.GioThue)
                        {
                            var chiTiet = new ChiTietDonHang
                            {
                                IdDonHang = donHang.Id,
                                IdSanPham = item.IdSanPham,
                                SoLuong = item.SoLuong,
                                DonGiaThucTe = item.TienThue
                            };
                            db.ChiTietDonHangs.InsertOnSubmit(chiTiet);
                            db.SubmitChanges();

                            // Tách dòng (Split-Insert): Đảm bảo mỗi món đồ sinh ra 1 dòng ThueDoChiTiet độc lập
                            // Giúp chức năng Trả Đồ (Batch Return) có thể trả từng phần dễ dàng.
                            for (int i = 0; i < item.SoLuong; i++)
                            {
                                var thueDo = new ThueDoChiTiet
                                {
                                    IdChiTietDonHang = chiTiet.Id,
                                    IdTaiSanChoThue = item.IdTaiSanChoThue,
                                    SoLuong = 1, // luon bằng 1
                                    ThoiGianThue = DateTime.Now,
                                    TrangThaiCoc = AppConstants.TrangThaiCoc.DaCoc,
                                    TienCoc = item.TienCoc, // Tiền cọc của 1 đơn vị
                                    PhiPhatSinh = 0m,
                                    TrangThai = AppConstants.TrangThaiThueDo.DangThue
                                };
                                db.ThueDoChiTiets.InsertOnSubmit(thueDo);
                            }

                            // Nếu có tài sản vật lý -> kiểm tra lại trạng thái BÊN TRONG transaction
                            // để chặn race condition: 2 NV cùng giao 1 chiếc xe trong 50ms
                            if (item.IdTaiSanChoThue.HasValue)
                            {
                                var ts = db.TaiSanChoThues.FirstOrDefault(x => x.Id == item.IdTaiSanChoThue.Value);
                                if (ts == null)
                                    throw new InvalidOperationException(AppConstants.ErrorMessages.ERR_RENTAL_TAISAN_KHONG_TIM_THAY);

                                // Check lại trong transaction — nếu NV khác vừa đổi sang DangThue thì rollback ngay
                                if (ts.TrangThai != AppConstants.TrangThaiTaiSan.SanSang)
                                    throw new InvalidOperationException(AppConstants.ErrorMessages.ERR_RENTAL_TAISAN_DANG_THUE);

                                ts.TrangThai = AppConstants.TrangThaiTaiSan.DangThue;
                            }
                        }

                        // 3. Tạo chứng từ thu cọc
                        var chungTu = new ChungTuTC
                        {
                            MaChungTu = $"PT-COC-{maDon}",
                            LoaiChungTu = AppConstants.LoaiChungTuTC.THU_COC,
                            IdDonHang = donHang.Id,
                            MaGiaoDichClient = Guid.NewGuid(),
                            SoTien = tongCoc,
                            PhuongThuc = req.PhuongThucTT,
                            TrangThai = AppConstants.TrangThaiChungTuTC.DaDuyet,
                            IdNguoiTao = req.IdNhanVien,
                            NgayChungTu = DateTime.Now,
                            NgayTao = DateTime.Now
                        };
                        db.ChungTuTCs.InsertOnSubmit(chungTu);

                        // 4. Nếu RFID -> trừ ví
                        if (req.PhuongThucTT == AppConstants.PhuongThucTT.ViRFID && req.IdViDienTu.HasValue)
                        {
                            var socaiVi = new SoCaiVi
                            {
                                IdVi = req.IdViDienTu.Value,
                                LoaiPhep = AppConstants.LoaiPhepVi.Tru,
                                SoTien = tongTT,
                                MoTa = $"Thuê đồ {maDon}",
                                NguoiTao = req.IdNhanVien,
                                NgayTao = DateTime.Now
                            };
                            db.SoCaiVis.InsertOnSubmit(socaiVi);
                        }

                        db.SubmitChanges();
                        tx.Commit();
                        return maDon;
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Giao dịch nhận trả đồ: cập nhật TrangThai, ThoiGianTra, tạo chứng từ hoàn cọc.
        /// Nếu mất đồ -> TrangThai = MatDo, TrangThaiCoc = KhongHoan, ghi PhiPhatSinh.
        /// Nếu trả đồ -> TrangThai = DaTra, TrangThaiCoc = DaHoan, hoàn cọc.
        /// </summary>
        /// <param name="idPhienThuNgan">Phiên thu ngân đang thực hiện hoàn cọc — ghi vào IdPhienTra để đối soát giao ca chéo.</param>
        public void TraDo(List<DTO_ThuHoiRequest> dsTraDo, string maDonHang, int idNhanVien, string phuongThucHoan, int idDiemBan, int idPhienThuNgan)
        {
            using (var db = new DaiNamDBDataContext())
            {
                db.Connection.Open();
                using (var tx = db.Connection.BeginTransaction())
                {
                    db.Transaction = tx;
                    try
                    {
                        // Xác định Khu Vực đang thực hiện thu hồi
                        var pos = db.DiemBanHang_POs.FirstOrDefault(p => p.Id == idDiemBan);
                        if (pos == null) throw new InvalidOperationException("Máy POS không tồn tại!");
                        int idKhuVucThuHoi = pos.IdKhuVuc;

                        // Tìm đơn hàng gốc
                        var donHang = db.DonHangs.FirstOrDefault(d => d.MaDonHang == maDonHang);
                        if (donHang == null) throw new InvalidOperationException(AppConstants.ErrorMessages.ERR_RETURN_DONHANG_KHONG_TIM_THAY);

                        decimal tongHoanCoc = 0m;
                        decimal tongTienPhat = 0m;
                        
                        // Chốt mốc thời gian duy nhất để đảm bảo tất cả sản phẩm trả cùng 1 thời điểm
                        DateTime thoiGianHienTai = DateTime.Now;

                        foreach (var item in dsTraDo)
                        {
                            var cacDongGoc = (from td in db.ThueDoChiTiets
                                              join ct in db.ChiTietDonHangs on td.IdChiTietDonHang equals ct.Id
                                              where ct.IdDonHang == donHang.Id
                                                    && ct.IdSanPham == item.IdSanPham
                                                    && td.TrangThai == AppConstants.TrangThaiThueDo.DangThue
                                              select new { ThueDo = td, ChiTiet = ct }).ToList();

                            if (item.SoLuongTra + item.SoLuongMat > cacDongGoc.Count)
                            {
                                throw new InvalidOperationException($"Lỗi: Số lượng thu hồi [{item.TenSanPham}] vượt số lượng đang thuê!");
                            }

                            int index = 0;
                            decimal phatMat1Mon = item.SoLuongMat > 0 ? (item.TienPhat / item.SoLuongMat) : 0m;

                            // Lấy bảng giá để tính lố giờ nội bộ
                            var bangGia = db.BangGias.FirstOrDefault(b => b.IdSanPham == item.IdSanPham && b.HieuLucTu <= thoiGianHienTai.Date && (b.HieuLucDen == null || b.HieuLucDen >= thoiGianHienTai.Date));
                            var bgThemGio = bangGia != null ? db.BangGia_ThueTheoGios.FirstOrDefault(t => t.IdBangGia == bangGia.Id) : null;

                            // 1. Xử lý Trả Bình Thường
                            for (int i = 0; i < item.SoLuongTra; i++)
                            {
                                var phienThue = cacDongGoc[index].ThueDo;
                                var chiTietGoc = cacDongGoc[index].ChiTiet;
                                
                                phienThue.ThoiGianTra = thoiGianHienTai;
                                phienThue.TrangThai = AppConstants.TrangThaiThueDo.DaTra;
                                phienThue.TrangThaiCoc = AppConstants.TrangThaiCoc.DaHoan;
                                // Ghi nhận ca nào hoàn cọc — dùng để đối soát giao ca chéo
                                phienThue.IdPhienTra = idPhienThuNgan > 0 ? (int?)idPhienThuNgan : null;

                                // Tiền cọc luôn được ghi nhận hoàn trả 100%
                                tongHoanCoc += phienThue.TienCoc;

                                //  Tính Phạt Lố Giờ 
                                decimal phiLoGio = 0m;
                                if (bgThemGio != null && bgThemGio.PhutBlock.HasValue && bgThemGio.PhutTiep.HasValue && bgThemGio.GiaPhuThu.HasValue && bgThemGio.PhutTiep.Value > 0)
                                {
                                    int tongPhut = (int)Math.Ceiling((thoiGianHienTai - phienThue.ThoiGianThue).TotalMinutes);
                                    if (tongPhut > bgThemGio.PhutBlock.Value)
                                    {
                                        int phutVuot = tongPhut - bgThemGio.PhutBlock.Value;
                                        int soBlockVuot = (int)Math.Ceiling((double)phutVuot / bgThemGio.PhutTiep.Value);
                                        phiLoGio = soBlockVuot * bgThemGio.GiaPhuThu.Value;
                                    }
                                }

                                if (phiLoGio > 0)
                                {
                                    tongTienPhat += phiLoGio;
                                    phienThue.PhiPhatSinh += phiLoGio;
                                }

                                if (phienThue.IdTaiSanChoThue.HasValue)
                                {
                                    var ts = db.TaiSanChoThues.FirstOrDefault(x => x.Id == phienThue.IdTaiSanChoThue.Value);
                                    if (ts != null)
                                    {
                                        ts.TrangThai = AppConstants.TrangThaiTaiSan.SanSang;
                                        var xe = db.PhuongTienDiChuyens.FirstOrDefault(x => x.IdTaiSan == ts.Id);
                                        if (xe != null)
                                        {
                                            xe.IdKhuVucHienTai = idKhuVucThuHoi;
                                        }
                                        else
                                        {
                                            var tu = db.TuDos.FirstOrDefault(t => t.IdTaiSan == ts.Id);
                                            if (tu != null && tu.IdKhuVuc != idKhuVucThuHoi)
                                                throw new InvalidOperationException($"LỖI: Tủ '{ts.TenTaiSan}' là tài sản cố định. Không thể trả tại khu vực này!");

                                            var choi = db.ChoiNghiMats.FirstOrDefault(c => c.IdTaiSan == ts.Id);
                                            if (choi != null && choi.IdKhuVuc != idKhuVucThuHoi)
                                                throw new InvalidOperationException($"LỖI: Chòi '{ts.TenTaiSan}' là tài sản cố định. Không thể trả tại khu vực này!");
                                        }
                                    }
                                }
                                index++;
                            }

                            // 2. Xử lý Báo Mất / Hỏng
                            for (int i = 0; i < item.SoLuongMat; i++)
                            {
                                var phienThue = cacDongGoc[index].ThueDo;
                                phienThue.ThoiGianTra = thoiGianHienTai;
                                phienThue.TrangThai = AppConstants.TrangThaiThueDo.MatDo;
                                phienThue.TrangThaiCoc = AppConstants.TrangThaiCoc.KhongHoan;
                                // Ghi nhận ca nào xử lý mất đồ
                                phienThue.IdPhienTra = idPhienThuNgan > 0 ? (int?)idPhienThuNgan : null;

                                tongHoanCoc += phienThue.TienCoc;

                                // Khách đã đền tiền món đồ (phatMat1Mon) thì không tính thêm tiền phạt lố giờ 
                                decimal tongPhatHienTai = phatMat1Mon;
                                phienThue.PhiPhatSinh = tongPhatHienTai;
                                tongTienPhat += tongPhatHienTai;

                                if (phienThue.IdTaiSanChoThue.HasValue)
                                {
                                    var ts = db.TaiSanChoThues.FirstOrDefault(x => x.Id == phienThue.IdTaiSanChoThue.Value);
                                    if (ts != null) ts.TrangThai = AppConstants.TrangThaiTaiSan.HuHong;
                                }
                                index++;
                            }
                        }

                        // 3. Tách bạch luồng tiền kế toán: 1 Lệnh Chi (Hoàn Cọc) và 1 Lệnh Thu (Hóa Đơn Phụ Thu)
                        
                        // Lệnh Chi Hoàn Cọc
                        if (tongHoanCoc > 0)
                        {
                            var chungTuHoan = new ChungTuTC
                            {
                                MaChungTu = $"PC-COC-{maDonHang}",
                                LoaiChungTu = AppConstants.LoaiChungTuTC.HOAN_COC,
                                IdDonHang = donHang.Id,
                                MaGiaoDichClient = Guid.NewGuid(),
                                SoTien = tongHoanCoc,
                                PhuongThuc = phuongThucHoan,
                                TrangThai = AppConstants.TrangThaiChungTuTC.DaDuyet,
                                IdNguoiTao = idNhanVien,
                                NgayChungTu = DateTime.Now,
                                NgayTao = DateTime.Now
                            };
                            db.ChungTuTCs.InsertOnSubmit(chungTuHoan);
                        }

                        // Lệnh Thu Tiền Phạt (Surcharge Invoice)
                        if (tongTienPhat > 0)
                        {
                            var chungTuPhat = new ChungTuTC
                            {
                                MaChungTu = $"PT-PHAT-{maDonHang}",
                                LoaiChungTu = AppConstants.LoaiChungTuTC.THU_PHAT,
                                IdDonHang = donHang.Id,
                                MaGiaoDichClient = Guid.NewGuid(),
                                SoTien = tongTienPhat,
                                PhuongThuc = phuongThucHoan,
                                TrangThai = AppConstants.TrangThaiChungTuTC.DaDuyet,
                                IdNguoiTao = idNhanVien,
                                NgayChungTu = DateTime.Now,
                                NgayTao = DateTime.Now
                            };
                            db.ChungTuTCs.InsertOnSubmit(chungTuPhat);
                        }

                        db.SubmitChanges();
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        #endregion
    }
}



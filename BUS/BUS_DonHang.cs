using System;
using ET;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace BUS
{
    public class BUS_DonHang
    {
        #region Khởi tạo & Cấu hình Singleton dành cho TDD (Test-Driven Development)
        private readonly IDonHangGateway _donHangGateway;
        private readonly IChiTietDonHangGateway _chiTietGateway;
        private readonly IPhieuThuGateway _phieuThuGateway;
        private readonly ISanPhamGateway _sanPhamGateway;
        private readonly IComboGateway _comboGateway;
        private readonly IComboChiTietGateway _comboChiTietGateway;
        private readonly ISanPhamVeGateway _sanPhamVeGateway;
        private readonly IViDienTuGateway _viGateway;
        private readonly IGiaoDichViGateway _giaoDichGateway;
        private readonly IKhachHangGateway _khachHangGateway;
        
        private static BUS_DonHang instance;
        
        public static BUS_DonHang Instance
        {
            get
            {
                if (instance == null) instance = new BUS_DonHang();
                return instance;
            }
        }
        
        public BUS_DonHang() : this(new DefaultDonHangGateway(), new DefaultChiTietDonHangGateway(), new DefaultPhieuThuGateway(),
                                    new DefaultSanPhamGateway(), new DefaultComboGateway(), new DefaultComboChiTietGateway(), 
                                    new DefaultSanPhamVeGateway(), new DefaultViDienTuGateway(), new DefaultGiaoDichViGateway(), 
                                    new DefaultKhachHangGateway()) { }

        public BUS_DonHang(IDonHangGateway donHangGateway, IChiTietDonHangGateway chiTietGateway, IPhieuThuGateway phieuThuGateway,
                           ISanPhamGateway sanPhamGateway, IComboGateway comboGateway, IComboChiTietGateway comboChiTietGateway,
                           ISanPhamVeGateway sanPhamVeGateway, IViDienTuGateway viGateway, IGiaoDichViGateway giaoDichGateway,
                           IKhachHangGateway khachHangGateway)
        {
            _donHangGateway = donHangGateway;
            _chiTietGateway = chiTietGateway;
            _phieuThuGateway = phieuThuGateway;
            _sanPhamGateway = sanPhamGateway;
            _comboGateway = comboGateway;
            _comboChiTietGateway = comboChiTietGateway;
            _sanPhamVeGateway = sanPhamVeGateway;
            _viGateway = viGateway;
            _giaoDichGateway = giaoDichGateway;
            _khachHangGateway = khachHangGateway;
        }
        #endregion

        #region Cơ bản (CRUD)
        public List<ET_DonHang> LoadDS()
        {
            return _donHangGateway.LoadDS();
        }

        public ET_DonHang GetById(int id)
        {
            return _donHangGateway.LayTheoId(id);
        }

        public ET_DonHang GetByMaCode(string maCode)
        {
            return _donHangGateway.LayTheoMaCode(maCode);
        }

        public OperationResult ThemDonHang(ET_DonHang et)
        {
            if (et == null) return OperationResult.Failed("Dữ liệu đơn hàng không hợp lệ.");
            bool success = _donHangGateway.Them(et);
            return success ? OperationResult.Success() : OperationResult.Failed("Không thể thêm đơn hàng.");
        }
        #endregion

        #region Dịch vụ & Xử lý nghiệp vụ
        /// <summary>
        /// Chuỗi quy trình tạo mới Đơn Hàng tổng lực, bao quát qua các tiến trình vệ tinh:
        /// - Chốt mã hóa đơn (Master Bill) vào hệ quản trị.
        /// - Xả danh sách chi tiết món hoặc dịch vụ trong hóa đơn xuống theo liên kết khóa ngoại.
        /// - Phát mẫu vé cổng hoặc dịch vụ công viên giải trí qua mã điện tử nếu thuộc tính mua là vé QR.
        /// - Lập chứng từ phiếu thu để bàn giao cuối ngày.
        /// - Tự động đối chiếu quy trình phân tách định lượng nguyên vật liệu.
        /// </summary>
        public OperationResult<int> ThemDonHangVaChiTiet(ET_DonHang donHang, List<ET_ChiTietDonHang> chiTietList, ET_PhieuThu phieuThu, int idKhoXuLy = 1)
        {
            if (donHang == null || chiTietList == null || chiTietList.Count == 0)
                return OperationResult<int>.Failed("Dữ liệu đơn hàng không hợp lệ.");

            if (chiTietList.Any(x => x == null))
                return OperationResult<int>.Failed("Chi tiết đơn hàng chứa dữ liệu rỗng.");

            // Bước 1: Hệ số an toàn: Rào chắn ngăn dữ liệu chi tiết có tính phí nhưng lại thiếu liên kết với tài sản (Sản phẩm hoặc Combo).
            // Đề phòng trường hợp các hàm ngoại vi đẩy dòng dữ liệu ảo không định danh.
            if (chiTietList.Any(x => !x.IdSanPham.HasValue && !x.IdCombo.HasValue && x.DonGiaGoc > 0))
                return OperationResult<int>.Failed("Chi tiết đơn hàng có giá nhưng không gắn sản phẩm/combo.");

            // Bước 1.5: Tái giám định tài chính (Nguyên tắc: KHÔNG TIN TƯỞNG CILENT - CHỐNG HACK GIÁ)
            // Kéo toàn bộ bảng chỉ định thiết yếu ném vào không gian nhớ Cache hệ thống.
            var allSanPhamCache = _sanPhamGateway.LoadDS().ToDictionary(x => x.Id, x => x);
            var allComboCache = _comboGateway.LoadDS().ToDictionary(x => x.Id, x => x);
            
            decimal calcTongTien = 0;
            foreach (var item in chiTietList)
            {
                if (item.SoLuong <= 0) return OperationResult<int>.Failed("Cảnh báo an ninh: Số lượng sản phẩm âm hoặc bằng 0.");
                
                // Móc giá chuẩn Cố định đắp ngược lại vào chi tiết (Cấm không cho client gửi giá láo)
                if (item.IdSanPham.HasValue && allSanPhamCache.ContainsKey(item.IdSanPham.Value))
                {
                    item.DonGiaGoc = allSanPhamCache[item.IdSanPham.Value].DonGia;
                }
                else if (item.IdCombo.HasValue && allComboCache.ContainsKey(item.IdCombo.Value))
                {
                    item.DonGiaGoc = allComboCache[item.IdCombo.Value].Gia;
                }
                calcTongTien += (item.DonGiaGoc * item.SoLuong);
            }
            
            // Ép buộc lại tổng tiền hóa đơn chống chỉnh sửa
            donHang.TongTien = calcTongTien;
            if (donHang.TienGiamGia < 0 || donHang.TienGiamGia > donHang.TongTien)
                return OperationResult<int>.Failed("Cảnh báo an ninh: Khấu hao giảm giá vượt định mức hoặc âm.");

            var allComboChiTietCache = _comboChiTietGateway.LoadDS();
            
            // Xây dựng bộ tự điển mapping chuyên án định tính vé điện tử để chuẩn bị phục vụ việc phát sinh mã QR sau này
            var allVeInfoCache = new Dictionary<int, ET_SanPham_Ve>();
            foreach (var sp in allSanPhamCache.Values.Where(x => x.LoaiSanPham == "Ve"))
            {
                var veInfo = _sanPhamVeGateway.LayTheoIdSanPham(sp.Id);
                if (veInfo != null) allVeInfoCache[sp.Id] = veInfo;
            }

            using (var scope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
            {
                // Bước 3: Đăng ký mã đơn hàng gốc để móc nối các bảng phụ thuộc.
                int donHangId = _donHangGateway.ThemVaLayId(donHang);
                if (donHangId <= 0) return OperationResult<int>.Failed("Không thể tạo đơn hàng.");

                foreach (var item in chiTietList)
                {
                    // Bước 4: Khởi tao chi tiết đơn hàng
                    item.IdDonHang = donHangId;
                    if (!_chiTietGateway.Them(item)) return OperationResult<int>.Failed("Không thể lưu chi tiết đơn hàng.");

                    if (item.IdSanPham.HasValue)
                    {
                        // Bước 5: Tra bảng SanPham_Ve: Nếu mặt hàng mua trùng khớp thông số sinh mã -> Xả luồng vé QR.
                        if (allVeInfoCache.TryGetValue(item.IdSanPham.Value, out var veInfo))
                        {
                            int tongLuot = veInfo.SoLuotQuyDoi * item.SoLuong;
                            if (!BUS_VeDienTu.Instance.GenerateTicket(item.Id, tongLuot, item.IdSanPham, donHang.CreatedBy.GetValueOrDefault(1)))
                                return OperationResult<int>.Failed("Không thể tạo vé điện tử.");
                        }
                    }
                    else if (item.IdCombo.HasValue)
                    {
                        // Bước 6: Trường hợp mặt hàng là gói Combo: phân hạch combo thành các mặt hàng con tương ứng, và sản xuất theo cấp số nhân số lượng tổng
                        var comboDetails = allComboChiTietCache.Where(x => x.IdCombo == item.IdCombo.Value).ToList();
                        foreach (var cd in comboDetails)
                        {
                            if (allVeInfoCache.TryGetValue(cd.IdSanPham, out var veInfo))
                            {
                                int tongLuot = veInfo.SoLuotQuyDoi * cd.SoLuong * item.SoLuong;
                                if (!BUS_VeDienTu.Instance.GenerateTicket(item.Id, tongLuot, cd.IdSanPham, donHang.CreatedBy.GetValueOrDefault(1)))
                                    return OperationResult<int>.Failed("Không thể tạo vé điện tử từ Combo.");
                            }
                        }
                    }
                }

                if (phieuThu != null)
                {
                    // Bước 7: Móc nối hồ sơ phiếu thu cho hóa đơn để báo cáo doanh thu ca làm việc.
                    phieuThu.IdDonHang = donHangId;
                    if (!_phieuThuGateway.Them(phieuThu)) return OperationResult<int>.Failed("Không thể tạo phiếu thu.");
                }

                // Bước 8: Tự động phân rã nguyên vật liệu trực tiếp (Định mức tiêu hao) và trừ số dư tồn vào sổ thẻ kho thực tế.
                if (!BUS_KhoHang.Instance.WriteLedgerTuDong(donHangId, chiTietList, donHang.CreatedBy.GetValueOrDefault(1), idKhoXuLy))
                    return OperationResult<int>.Failed("Lỗi tính toán kho: Không thể trừ kho.");

                // Bước 9: Cơ chế chăm sóc khách hàng: Tự động cộng dồn doanh số mua sắm và thăng hạng cấp bậc thành viên.
                if (donHang.IdKhachHang.HasValue && donHang.IdKhachHang.Value > 0 && donHang.TrangThai == AppConstants.TrangThaiDonHang.DaThanhToan)
                {
                    _khachHangGateway.CapNhatChiTieuVaHang(donHang.IdKhachHang.Value, donHang.TongTien - donHang.TienGiamGia);
                }

                scope.Complete();
                return OperationResult<int>.Success(donHangId);
            }
        }

        /// <summary>
        /// Chuỗi quy trình cực kỳ nghiêm ngặt nhằm thanh toán qua dòng tiền số (Thẻ ví chạm RFID) do không có đối chứng tiền thật ngoài vòng vật lý.
        /// Sử dụng cô lập chuỗi mức ReadCommitted kèm cấu trúc theo dõi biến thời gian sửa đổi gần nhất - ngăn ngừa trục lợi rút ví đa luồng thiết bị.
        /// Các bước cốt lõi: Soi quỹ -> Chốt bill -> xả dữ liệu mua sắm -> Phát mã vạch -> Cắm điểm tín dụng âm vào ví.
        /// </summary>
        public OperationResult<int> ThanhToanBangVi(ET_DonHang donHang, List<ET_ChiTietDonHang> chiTietList, int idKhachHang, int createdBy, int idKhoXuLy = 1)
        {
            if (donHang == null || chiTietList == null || chiTietList.Count == 0)
                return OperationResult<int>.Failed("Dữ liệu đơn hàng không hợp lệ.");

            // Bước 2: Tải khối dữ liệu lên trước để tăng tốc và Tái Giám Định Giá Chống Hack
            var allSanPhamCache = _sanPhamGateway.LoadDS().ToDictionary(x => x.Id, x => x);
            var allComboCache = _comboGateway.LoadDS().ToDictionary(x => x.Id, x => x);
            var allComboChiTietCache = _comboChiTietGateway.LoadDS();
            var allVeInfoCache = new Dictionary<int, ET_SanPham_Ve>();
            foreach (var sp in allSanPhamCache.Values.Where(x => x.LoaiSanPham == "Ve"))
            {
                var veInfo = _sanPhamVeGateway.LayTheoIdSanPham(sp.Id);
                if (veInfo != null) allVeInfoCache[sp.Id] = veInfo;
            }

            // Bước 3: Tái tính toán tài chính (Server-side Recalculation)
            decimal calcTongTien = 0;
            foreach (var item in chiTietList)
            {
                if (item.SoLuong <= 0) return OperationResult<int>.Failed("Cảnh báo an ninh: Số lượng sản phẩm âm hoặc bằng 0.");
                
                if (item.IdSanPham.HasValue && allSanPhamCache.ContainsKey(item.IdSanPham.Value))
                    item.DonGiaGoc = allSanPhamCache[item.IdSanPham.Value].DonGia;
                else if (item.IdCombo.HasValue && allComboCache.ContainsKey(item.IdCombo.Value))
                    item.DonGiaGoc = allComboCache[item.IdCombo.Value].Gia;
                    
                calcTongTien += (item.DonGiaGoc * item.SoLuong);
            }
            donHang.TongTien = calcTongTien;
            if (donHang.TienGiamGia < 0 || donHang.TienGiamGia > donHang.TongTien)
                return OperationResult<int>.Failed("Cảnh báo an ninh: Mức giảm trừ biến dạng.");

            decimal soTienThucThu = donHang.TongTien - donHang.TienGiamGia;
            if (soTienThucThu <= 0) return OperationResult<int>.Failed("Số tiền thanh toán thực phải lớn hơn 0.");

            // Bước 4: Thẩm tra cấu trúc sổ giao dịch ảo của thành viên đang kết nối trên chip RFID hiện tại.
            var vi = _viGateway.LayTheoKhachHang(idKhachHang);
            if (vi == null) return OperationResult<int>.Failed("Khách hàng chưa có ví điện tử liên kết.");
            if (vi.SoDuKhaDung < soTienThucThu)
                return OperationResult<int>.Failed(string.Format("Số dư ví không đủ. Cần: {0:N0}đ, Có: {1:N0}đ", soTienThucThu, vi.SoDuKhaDung));
            foreach (var sp in allSanPhamCache.Values.Where(x => x.LoaiSanPham == "Ve"))
            {
                var veInfo = _sanPhamVeGateway.LayTheoIdSanPham(sp.Id);
                if (veInfo != null) allVeInfoCache[sp.Id] = veInfo;
            }

            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled))
                {
                    // Bước 5: Mở thủ tục niêm yết đơn hàng mới vào kho lưu trữ nhằm đánh dầu mốc dữ liệu
                    donHang.CreatedBy = createdBy; donHang.CreatedAt = DateTime.Now;
                    int donHangId = _donHangGateway.ThemVaLayId(donHang);
                    if (donHangId <= 0) return OperationResult<int>.Failed("Không thể tạo đơn hàng.");

                    // Bước 6: Móc nối và tiến hành rải dữ liệu hàng tiêu dùng chi tiết 
                    foreach (var item in chiTietList)
                    {
                        item.IdDonHang = donHangId;
                        if (!_chiTietGateway.Them(item)) return OperationResult<int>.Failed("Không thể lưu chi tiết đơn hàng.");

                        // Phân nhánh A: Dành cho phân loại mặt hàng đơn lẻ. Rà soát danh sách bộ đếm vé để cấp mã điện tử
                        if (item.IdSanPham.HasValue)
                        {
                            if (allVeInfoCache.TryGetValue(item.IdSanPham.Value, out var veInfo))
                            {
                                int tongLuot = veInfo.SoLuotQuyDoi * item.SoLuong;
                                if (!BUS_VeDienTu.Instance.GenerateTicket(item.Id, tongLuot, item.IdSanPham, createdBy))
                                    return OperationResult<int>.Failed("Không thể tạo vé điện tử.");
                            }
                        }
                        // Phân nhánh B: Bóc tách gói đóng hộp Combo. Trích xuất mảng nguyên vật liệu ẩn trong khoáng để định kỳ phát vé phụ
                        else if (item.IdCombo.HasValue)
                        {
                            var comboDetails = allComboChiTietCache.Where(x => x.IdCombo == item.IdCombo.Value).ToList();
                            foreach (var cd in comboDetails)
                            {
                                if (allVeInfoCache.TryGetValue(cd.IdSanPham, out var veInfo))
                                {
                                    int tongLuot = veInfo.SoLuotQuyDoi * cd.SoLuong * item.SoLuong;
                                    if (!BUS_VeDienTu.Instance.GenerateTicket(item.Id, tongLuot, cd.IdSanPham, createdBy))
                                        return OperationResult<int>.Failed("Không thể tạo vé điện tử từ Combo.");
                                }
                            }
                        }
                    }

                    // Bước 7: Thắt chặt tài chính - Trực tiếp kéo âm tiền trên quỹ thanh toán ảo của khách. 
                    // Chú ý: Nếu ghi đè xảy ra đồng thời 2 luồng truy xuất ở hai trạm RFID khác nhau thì hệ thống Transaction bắt mã ngoại lệ DBConcurrencyException tức thời.
                    vi.SoDuKhaDung -= soTienThucThu;
                    _viGateway.Sua(vi);

                    // Bước 8: Khai sinh vạch chứng từ đối lưu - sinh biên lai dòng giao dịch cho tài khoản ví điện tử báo cáo số thu chi
                    var gd = new ET_GiaoDichVi
                    {
                        MaCode = "GD-" + DateTime.Now.ToString("yyMMddHHmmss") + "-" + Guid.NewGuid().ToString().Substring(0, 4).ToUpper(),
                        IdVi = vi.Id, IdDonHangLienQuan = donHangId, LoaiGiaoDich = AppConstants.LoaiGiaoDichVi.ThanhToanDichVu,
                        SoTien = soTienThucThu, ThoiGian = DateTime.Now, CreatedAt = DateTime.Now, CreatedBy = createdBy
                    };
                    int giaoDichId = _giaoDichGateway.ThemVaLayId(gd);
                    if (giaoDichId <= 0) return OperationResult<int>.Failed("Không thể ghi nhật ký giao dịch ví.");

                    // Bước 9: Móc nối hồ sơ phiếu thu kép giúp kế toán khớp sổ giữa doanh thu Hàng Trạm - Hàng Kế Toán - Giao Dịch Số Điện Tử
                    var phieuThu = new ET_PhieuThu
                    {
                        MaCode = "PT-RFID-" + DateTime.Now.ToString("yyMMddHHmmss") + "-" + Guid.NewGuid().ToString().Substring(0, 4).ToUpper(),
                        IdDonHang = donHangId,
                        IdGiaoDichVi = giaoDichId,
                        SoTien = soTienThucThu,
                        PhuongThuc = AppConstants.PhuongThucThanhToan.ViRfid,
                        ThoiGian = DateTime.Now,
                        CreatedAt = DateTime.Now,
                        CreatedBy = createdBy
                    };
                    if (!_phieuThuGateway.Them(phieuThu)) return OperationResult<int>.Failed("Không thể tạo phiếu thu.");

                    // Bước 10: Thiết lập lệnh cập nhật quản lý định lượng theo thời gian thực tại các kho bãi hậu phẩm.
                    if (!BUS_KhoHang.Instance.WriteLedgerTuDong(donHangId, chiTietList, createdBy, idKhoXuLy))
                        return OperationResult<int>.Failed("Lỗi tính toán kho: Không thể trừ kho.");

                    // Bước 11: Quy trình khách hàng ưu tiên: Ghi nhận mức tiêu dùng để đổi điểm và đẩy định giới phân hạng (Vàng, Bạc, Đồng)
                    if (donHang.IdKhachHang.HasValue && donHang.IdKhachHang.Value > 0)
                    {
                        _khachHangGateway.CapNhatChiTieuVaHang(donHang.IdKhachHang.Value, soTienThucThu);
                    }

                    scope.Complete();
                    return OperationResult<int>.Success(donHangId);
                }
            }
            catch (System.Data.DBConcurrencyException) { return OperationResult<int>.Failed("Ví vừa bị thay đổi bởi giao dịch khác. Vui lòng thử lại."); }
            catch (TransactionAbortedException ex) { return OperationResult<int>.Failed("Giao dịch bị hủy: " + (ex.InnerException?.Message ?? ex.Message)); }
        }

        public OperationResult SuaDonHang(ET_DonHang et)
        {
            if (et == null) return OperationResult.Failed("Dữ liệu đơn hàng không hợp lệ.");
            bool success = _donHangGateway.Sua(et);
            return success ? OperationResult.Success() : OperationResult.Failed("Không thể cập nhật đơn hàng.");
        }

        public OperationResult XoaDonHang(int id)
        {
            bool success = _donHangGateway.Xoa(id);
            return success ? OperationResult.Success() : OperationResult.Failed("Không thể xóa đơn hàng.");
        }
        #endregion
    }
}

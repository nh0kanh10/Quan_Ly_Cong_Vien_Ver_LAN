using System;
using System.Collections.Generic;
using System.Linq;
using ET;
using DAL;

namespace BUS
{
    public class BUS_DatBan
    {
        #region Khởi tạo & Cấu hình Singleton (TDD Injection)
        private readonly IDatBanGateway _datBanGateway;
        private readonly IBanAnGateway _banAnGateway;
        private readonly IChiTietDatBanGateway _chiTietDatBanGateway;
        private readonly IDonHangGateway _donHangGateway;
        private readonly IChiTietDonHangGateway _chiTietDonHangGateway;
        private readonly IPhieuThuGateway _phieuThuGateway;
        private readonly IDoanKhachDichVuGateway _doanKhachDichVuGateway;
        private readonly IBUS_GiaoDichVi _giaoDichViService;
        private readonly IBUS_DoanKhach _doanKhachService;
        
        private static BUS_DatBan instance;
        public static BUS_DatBan Instance
        {
            get
            {
                if (instance == null) instance = new BUS_DatBan();
                return instance;
            }
        }

        public BUS_DatBan() : this(new DefaultDatBanGateway(), new DefaultBanAnGateway(), new DefaultChiTietDatBanGateway(), 
                                   new DefaultDonHangGateway(), new DefaultChiTietDonHangGateway(), new DefaultPhieuThuGateway(), 
                                   new DefaultDoanKhachDichVuGateway(), BUS_GiaoDichVi.Instance, BUS_DoanKhach.Instance) { }

        public BUS_DatBan(IDatBanGateway datBanGateway, IBanAnGateway banAnGateway, IChiTietDatBanGateway chiTietDatBanGateway,
                          IDonHangGateway donHangGateway, IChiTietDonHangGateway chiTietDonHangGateway, IPhieuThuGateway phieuThuGateway,
                          IDoanKhachDichVuGateway doanKhachDichVuGateway, IBUS_GiaoDichVi giaoDichViService, IBUS_DoanKhach doanKhachService)
        {
            _datBanGateway = datBanGateway;
            _banAnGateway = banAnGateway;
            _chiTietDatBanGateway = chiTietDatBanGateway;
            _donHangGateway = donHangGateway;
            _chiTietDonHangGateway = chiTietDonHangGateway;
            _phieuThuGateway = phieuThuGateway;
            _doanKhachDichVuGateway = doanKhachDichVuGateway;
            _giaoDichViService = giaoDichViService;
            _doanKhachService = doanKhachService;
        }
        #endregion

        #region Thao Tác Dữ Liệu Cơ Bản
        public List<ET_DatBan> LoadDS()
        {
            return _datBanGateway.LoadDS();
        }

        public ET_DatBan LayTheoId(int id)
        {
            return _datBanGateway.LayTheoId(id);
        }
        #endregion

        #region Quy Trình Phục Vụ Tại Chỗ & Đặt Bàn Khách Sạn

        /// <summary>
        /// Khởi tạo phiên phục vụ thực tế (Check-in tức thời) ngay trên luồng kinh doanh.
        /// Chuyển bàn thành Đang Sử Dụng và gài sẵn một khung hóa đơn rỗng.
        /// Áp dụng Transaction để không rơi rớt dữ liệu nếu nghẽn mạng.
        /// </summary>
        public OperationResult<int> MoBan(int idNhaHang, List<int> dsBanAnId, int soKhach)
        {
            if (dsBanAnId == null || dsBanAnId.Count == 0)
                return OperationResult<int>.Failed("Cảnh báo: Yêu cầu chọn ít nhất 1 bàn ăn.");

            try
            {
                using (var ts = new System.Transactions.TransactionScope())
                {
                    // Bước 1: Khởi tạo hồ sơ hóa đơn tổng
                    var donHang = new ET_DonHang
                    {
                        MaCode = "NH-" + DateTime.Now.ToString("yyMMddHHmmss") + "-" + Guid.NewGuid().ToString().Substring(0, 4).ToUpper(),
                        ThoiGian = DateTime.Now,
                        TongTien = 0,
                        TrangThai = AppConstants.TrangThaiDonHang.DangXuLy,
                        GhiChu = string.Format("Nhà hàng - Phục vụ {0} bàn - {1} khách", dsBanAnId.Count, soKhach),
                        CreatedAt = DateTime.Now,
                        CreatedBy = SessionManager.CurrentUser != null ? (int?)SessionManager.CurrentUser.Id : null
                    };
                    int idDonHang = _donHangGateway.ThemVaLayId(donHang);
                    if (idDonHang <= 0)
                        return OperationResult<int>.Failed("Lỗi hạ tầng: Không thể khởi tạo hóa đơn tổng.");

                    // Bước 2: Khởi tạo liên kết chi tiết hóa đơn nháp
                    var ctdhNH = new ET_ChiTietDonHang
                    {
                        IdDonHang = idDonHang,
                        IdSanPham = null,
                        SoLuong = 1,
                        DonGiaGoc = 0,
                        TienGiamGiaDong = 0,
                        DonGiaThucTe = 0
                    };
                    int idCtdh = _chiTietDonHangGateway.ThemVaLayId(ctdhNH);
                    if (idCtdh <= 0)
                        return OperationResult<int>.Failed("Lỗi hạ tầng: Không thể khởi tạo chi tiết hóa đơn.");

                    // Bước 3: Áp dụng tiến trình đăng ký bàn ăn với thông số thực khách
                    var datBan = new ET_DatBan
                    {
                        IdChiTietDonHang = idCtdh,
                        IdNhaHang = idNhaHang,
                        ThoiGianDat = DateTime.Now,
                        ThoiGianDenDuKien = DateTime.Now,
                        SoLuongKhach = soKhach,
                        TrangThai = "DaNhan"
                    };
                    int idDatBan = _datBanGateway.ThemVaLayId(datBan);
                    if (idDatBan <= 0)
                        return OperationResult<int>.Failed("Lỗi hệ thống: Tạo phân luồng đặt bàn thất bại.");

                    // Bước 4: Khóa an toàn tình trạng bàn trên sơ đồ vật lý (Ngăn chặn va chạm Overbooking)
                    foreach (int idBan in dsBanAnId)
                    {
                        _chiTietDatBanGateway.Them(new ET_ChiTietDatBan
                        {
                            IdDatBan = idDatBan,
                            IdBanAn = idBan
                        });

                        var ban = _banAnGateway.LayTheoId(idBan);
                        if (ban != null)
                        {
                            // BUG FIX: CHỐNG OVERBOOKING (KHÁCH KHÁC ĐANG NGỒI HOẶC ĐÃ ĐẶT)
                            if (ban.TrangThai != AppConstants.TrangThaiBanAn.Trong) return OperationResult<int>.Failed(string.Format("Lỗi an ninh: Bàn số ID {0} không còn trống (Trạng thái: {1})", ban.Id, ban.TrangThai));

                            ban.TrangThai = AppConstants.TrangThaiBanAn.DangSuDung;
                            _banAnGateway.Sua(ban);
                        }
                    }

                    ts.Complete();
                    return OperationResult<int>.Success(idDonHang);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<int>.Failed("Phát sinh lỗi ngoại lệ trong luồng mở bàn: " + ex.Message);
            }
        }

        /// <summary>
        /// Xử lý hợp đồng đặt cọc giữ chỗ (Reservation).
        /// Áp dụng cơ chế an toàn TransactionScope cho luồng dữ liệu kép: Bảng Đặt Bàn và Tiền Ví Khách Hàng.
        /// </summary>
        public OperationResult<int> DatBanTruoc(int idNhaHang, List<int> dsBanAnId, ET_DatBan info, string maRfid = "")
        {
            if (dsBanAnId == null || dsBanAnId.Count == 0)
                return OperationResult<int>.Failed("Cảnh báo: Yêu cầu chọn ít nhất 1 bàn ăn để giữ chỗ.");

            try
            {
                using (var ts = new System.Transactions.TransactionScope())
                {
                    // Bước 1: Phát hành hóa đơn chờ định danh liên kết với quy trình đặt chỗ
                    var donHang = new ET_DonHang
                    {
                        MaCode = "RSV-" + DateTime.Now.ToString("yyMMddHHmmss") + "-" + Guid.NewGuid().ToString().Substring(0, 4).ToUpper(),
                        ThoiGian = DateTime.Now,
                        TongTien = info.TienCoc,
                        TrangThai = AppConstants.TrangThaiDonHang.ChoThanhToan,
                        GhiChu = string.Format("Khách Tự Đặt Bàn - {0} - {1} {2}", info.TenNguoiDat, info.SoDienThoai, (!string.IsNullOrEmpty(maRfid) ? "[CỌC RFID]" : "")),
                        CreatedAt = DateTime.Now,
                        CreatedBy = SessionManager.CurrentUser != null ? (int?)SessionManager.CurrentUser.Id : null
                    };
                    int idDonHang = _donHangGateway.ThemVaLayId(donHang);
                    if (idDonHang <= 0)
                        return OperationResult<int>.Failed("Lỗi hạ tầng: Không thể phân bổ hóa đơn đặt chỗ gốc (Master Bill).");

                    // Bước 0: Yêu cầu đóng băng tiền cọc vào tài khoản lưu trữ (Bảo lãnh RFID)
                    if (!string.IsNullOrEmpty(maRfid) && info.TienCoc > 0)
                    {
                        var rfidResult = _giaoDichViService.DongBangTienVi(maRfid, info.TienCoc, idDonHang);
                        if (!rfidResult.IsSuccess)
                            return OperationResult<int>.Failed("Tiến trình đóng băng tiền cọc (RFID) không thành công: " + rfidResult.ErrorMessage);
                        
                        // Cấu hình mã định danh để phục vụ đối soát Hoàn Tiền / Khấu Trừ sau này
                        var viResult = _giaoDichViService.TraCuuViTheoRFID(maRfid);
                        if (viResult.IsSuccess && viResult.Data != null)
                        {
                            info.IdKhachHang = viResult.Data.IdKhachHang;
                        }
                    }

                    // Bước 2: Thiết lập chi tiết sơ khai
                    var ctdh = new ET_ChiTietDonHang
                    {
                        IdDonHang = idDonHang,
                        SoLuong = 1,
                        DonGiaGoc = 0,
                        DonGiaThucTe = 0
                    };
                    int idCtdh = _chiTietDonHangGateway.ThemVaLayId(ctdh);

                    // Bước 3: Lập phiếu ủy nhiệm giữ chỗ tích hợp
                    info.IdChiTietDonHang = idCtdh;
                    info.IdNhaHang = idNhaHang;
                    info.TrangThai = AppConstants.TrangThaiBanAn.DaDat;

                    int idDatBan = _datBanGateway.ThemVaLayId(info);

                    // Bước 4: Thiết lập cờ Đã Đặt trên phương diện vật lý
                    foreach (int idBan in dsBanAnId)
                    {
                        _chiTietDatBanGateway.Them(new ET_ChiTietDatBan { IdDatBan = idDatBan, IdBanAn = idBan });
                        var ban = _banAnGateway.LayTheoId(idBan);
                        if (ban != null)
                        {
                            // BUG FIX: CHỐNG OVERBOOKING 
                            if (ban.TrangThai != AppConstants.TrangThaiBanAn.Trong) return OperationResult<int>.Failed(string.Format("Lỗi an ninh: Bàn số ID {0} không còn trống (Trạng thái: {1})", ban.Id, ban.TrangThai));

                            ban.TrangThai = AppConstants.TrangThaiBanAn.DaDat;
                            _banAnGateway.Sua(ban);
                        }
                    }

                    ts.Complete(); // XÁC NHẬN. Dừng rò rỉ hoàn tiền thủ công. Nếu Exception tự động Rollback!
                    return OperationResult<int>.Success(idDatBan);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<int>.Failed("Ngoại lệ phát sinh khi tiến hành đặt chỗ: " + ex.Message);
            }
        }

        public OperationResult NhanBan(int idDatBan)
        {
            try
            {
                using (var ts = new System.Transactions.TransactionScope())
                {
                    var datBan = _datBanGateway.LayTheoId(idDatBan);
                    if (datBan == null) return OperationResult.Failed("Phiếu đặt bàn không tồn tại.");
                    if (datBan.TrangThai != AppConstants.TrangThaiBanAn.DaDat) return OperationResult.Failed("Trạng thái không hợp lệ để nhận.");

                    // Bước 1: Kích hoạt tình trạng hợp đồng phiếu giữ chỗ sang thực tế đi vào quy trình phục vụ dùng bữa (Chuyển Thành Đã Nhận)
                    datBan.TrangThai = "DaNhan";
                    _datBanGateway.Sua(datBan);

                    // Bước 2: Kéo mảng thông tin đơn hàng ảo chờ rỗng từ hệ thống hóa đơn ra mở mạng lưới ghi nhận order ẩm thực
                    var ctdh = _chiTietDonHangGateway.LayTheoId(datBan.IdChiTietDonHang);
                    if (ctdh != null)
                    {
                        var donHang = _donHangGateway.LayTheoId(ctdh.IdDonHang);
                        if (donHang != null)
                        {
                            donHang.TrangThai = AppConstants.TrangThaiDonHang.DangXuLy;
                            _donHangGateway.Sua(donHang);
                        }
                    }

                    // Bước 3: Đổi cờ báo hiệu mô phỏng trên sơ đồ thực tiễn nhà hàng về trạng thái Đang Phục Vụ để tránh xung đột lượt bố trí mới chặn va chạm
                    var dsCTDB = _chiTietDatBanGateway.LoadDS().Where(x => x.IdDatBan == idDatBan).ToList();
                    foreach (var ct in dsCTDB)
                    {
                        var ban = _banAnGateway.LayTheoId(ct.IdBanAn);
                        if (ban != null)
                        {
                            ban.TrangThai = AppConstants.TrangThaiBanAn.DangSuDung;
                            _banAnGateway.Sua(ban);
                        }
                    }
                    
                    ts.Complete();
                    return OperationResult.Success();
                }
            }
            catch (Exception ex) { return OperationResult.Failed("Lỗi nhận bàn: " + ex.Message); }
        }

        public OperationResult HuyDatBan(int idDatBan)
        {
            try
            {
                using (var ts = new System.Transactions.TransactionScope())
                {
                    var datBan = _datBanGateway.LayTheoId(idDatBan);
                    if (datBan == null) return OperationResult.Failed("Phiếu đặt bàn không tồn tại.");

                    var ctdh = _chiTietDonHangGateway.LayTheoId(datBan.IdChiTietDonHang);
                    int? idDonHang = ctdh != null ? (int?)ctdh.IdDonHang : null;

                    // Bước 1: GIẢI TỎA TRẢ LẠI TIỀN CỌC NẾU KHÁCH CÓ THANH TOÁN BẢO LÃNH VÍ
                    if (datBan.TienCoc > 0 && datBan.IdKhachHang.HasValue)
                    {
                        var hoanResult = _giaoDichViService.GiaiToaTienCoc(datBan.IdKhachHang.Value, datBan.TienCoc, idDonHang);
                        if (!hoanResult.IsSuccess)
                            return OperationResult.Failed("Hoàn cọc thất bại: " + hoanResult.ErrorMessage);
                    }

                    // Bước 2: Giải trừ hợp đồng đặt chỗ từ Đã Đặt quay sang bị Hủy bỏ hoàn toàn
                    datBan.TrangThai = AppConstants.TrangThaiBanAn.DaHuy;
                    _datBanGateway.Sua(datBan);

                    // Bước 3: Hủy hệ quy chiếu đơn hàng nháp tĩnh do không có hóa đơn thu chi phát sinh hoàn thiện thành quả
                    ctdh = _chiTietDonHangGateway.LayTheoId(datBan.IdChiTietDonHang);
                    if (ctdh != null)
                    {
                        var donHang = _donHangGateway.LayTheoId(ctdh.IdDonHang);
                        if (donHang != null && donHang.TrangThai != AppConstants.TrangThaiDonHang.DaThanhToan)
                        {
                            donHang.TrangThai = AppConstants.TrangThaiDonHang.DaHuy;
                            _donHangGateway.Sua(donHang);
                        }
                    }

                    // Bước 4: Giải phóng lại các bàn vật lý đang bị neo móc nối về trạng thái ban đầu để quầy thu ngân báo bán lại cho người khác
                    var dsCTDB = _chiTietDatBanGateway.LoadDS().Where(x => x.IdDatBan == idDatBan).ToList();
                    foreach (var ct in dsCTDB)
                    {
                        var ban = _banAnGateway.LayTheoId(ct.IdBanAn);
                        if (ban != null)
                        {
                            ban.TrangThai = AppConstants.TrangThaiBanAn.Trong;
                            _banAnGateway.Sua(ban);
                        }
                    }

                    ts.Complete();
                    return OperationResult.Success();
                }
            }
            catch (Exception ex) { return OperationResult.Failed("Lỗi hủy bàn: " + ex.Message); }
        }

        /// <summary>
        /// Xử lý hoán chuyển thực khách từ bàn ăn này sang bàn ăn khác hoặc dồn chung bàn.
        /// Áp dụng cơ chế an toàn TransactionScope để tránh mất món ăn nếu xảy ra sự cố đột ngột.
        /// </summary>
        public OperationResult GhepBan(int idDatBanGoc, int idDatBanBiGhep)
        {
            try
            {
                using (var ts = new System.Transactions.TransactionScope())
                {
                    var dbGoc = _datBanGateway.LayTheoId(idDatBanGoc);
                    var dbBiGhep = _datBanGateway.LayTheoId(idDatBanBiGhep);

                    if (dbGoc == null || dbBiGhep == null) return OperationResult.Failed("Hệ thống từ chối: Bàn không tồn tại.");

                    var ctdhGoc = _chiTietDonHangGateway.LayTheoId(dbGoc.IdChiTietDonHang);
                    var ctdhBiGhep = _chiTietDonHangGateway.LayTheoId(dbBiGhep.IdChiTietDonHang);
                    if (ctdhGoc == null || ctdhBiGhep == null) return OperationResult.Failed("Hệ thống từ chối: Lỗi dữ liệu liên kết đơn hàng.");

                    int idDonHangGoc = ctdhGoc.IdDonHang;
                    int idDonHangBiGhep = ctdhBiGhep.IdDonHang;

                    // Bước 1: Gỡ liên kết của bàn bị ghép (bàn 2) vì khách sẽ di chuyển sang bàn mới
                    var dsCTDB_BiGhep = _chiTietDatBanGateway.LoadDS().Where(x => x.IdDatBan == idDatBanBiGhep).ToList();
                    foreach (var ctdb in dsCTDB_BiGhep)
                    {
                        _chiTietDatBanGateway.Xoa(ctdb.Id);

                        var ban = _banAnGateway.LayTheoId(ctdb.IdBanAn);
                        if (ban != null)
                        {
                            ban.TrangThai = AppConstants.TrangThaiBanAn.Trong;
                            _banAnGateway.Sua(ban);
                        }
                    }

                    // Bước 2: Hoán chuyển món ăn đã tích lũy từ hóa đơn cũ sang hóa đơn mới
                    var dsCTDH_BiGhep = _chiTietDonHangGateway.LoadByDonHang(idDonHangBiGhep)
                        .Where(x => x.IdSanPham != null || x.IdCombo != null).ToList();

                    foreach (var ctdh in dsCTDH_BiGhep)
                    {
                        ctdh.IdDonHang = idDonHangGoc;
                        _chiTietDonHangGateway.Sua(ctdh);
                    }

                    // Bước 3: Hoàn tiền Ví Điện Tử cho cái bàn cũ bị hủy do đã ghép sang nơi khác
                    // BUG FIX: Chống bốc hơi tiền cọc khách hàng khi hợp nhất bill!
                    if (dbBiGhep.TienCoc > 0 && dbBiGhep.IdKhachHang.HasValue)
                    {
                        var refundResult = _giaoDichViService.GiaiToaTienCoc(dbBiGhep.IdKhachHang.Value, dbBiGhep.TienCoc, idDonHangBiGhep);
                        if (!refundResult.IsSuccess) return OperationResult.Failed("Cảnh báo thất thoát: Không thể hoàn cọc ví RFID cho bàn cũ bị ghép.");
                        dbBiGhep.TienCoc = 0; // Đặt về 0 để đóng quy trình cũ
                    }

                    // Bước 4: Đóng băng tiến trình tại hóa đơn cũ
                    dbBiGhep.TrangThai = AppConstants.TrangThaiBanAn.DaHuy;
                    _datBanGateway.Sua(dbBiGhep);

                    var dhBiGhep = _donHangGateway.LayTheoId(idDonHangBiGhep);
                    if (dhBiGhep != null)
                    {
                        dhBiGhep.TrangThai = AppConstants.TrangThaiDonHang.DaHuy;
                        dhBiGhep.GhiChu = (dhBiGhep.GhiChu ?? "") + " [ĐÃ GHÉP sang hóa đơn: " + idDonHangGoc + "]";
                        _donHangGateway.Sua(dhBiGhep);
                    }

                    CapNhatTongTien(idDonHangGoc);

                    ts.Complete();
                    return OperationResult.Success();
                }
            }
            catch (Exception ex)
            {
                return OperationResult.Failed("Phát sinh lỗi ngoại lệ trong luồng ghép bàn: " + ex.Message);
            }
        }
        #endregion

        #region Quản Lý Định Hình Thực Đơn Hóa Đơn
        /// <summary>
        /// Kích hoạt cơ chế nhúng thêm sản phẩm vào danh mục các món ẩm thực mà khách hàng đang sử dụng trên hệ thống hóa đơn lưu trữ.
        /// </summary>
        public OperationResult ThemMon(int idDonHang, int idSanPham, int soLuong, decimal donGia)
        {
            if (idDonHang <= 0 || idSanPham <= 0 || soLuong <= 0)
                return OperationResult.Failed("Dữ liệu không hợp lệ.");

            try
            {
                using (var ts = new System.Transactions.TransactionScope())
                {
                    // Bước 1: Quy trình tạo dòng kê khai chi tiết mới thuộc phạm vi hóa đơn hiện hành
                    var ct = new ET_ChiTietDonHang
                    {
                        IdDonHang = idDonHang,
                        IdSanPham = idSanPham,
                        SoLuong = soLuong,
                        DonGiaGoc = donGia,
                        TienGiamGiaDong = 0,
                        DonGiaThucTe = donGia
                    };
                    bool ok = _chiTietDonHangGateway.Them(ct);
                    if (!ok) return OperationResult.Failed("Không thể thêm món.");

                    CapNhatTongTien(idDonHang);
                    
                    ts.Complete();
                    return OperationResult.Success();
                }
            }
            catch (Exception ex)
            {
                return OperationResult.Failed("Lỗi thêm món: " + ex.Message);
            }
        }

        public OperationResult ThemPhuThu(int idDonHang, string lyDo, decimal soTien)
        {
            if (idDonHang <= 0 || soTien <= 0)
                return OperationResult.Failed("Dữ liệu không hợp lệ.");

            try
            {
                using (var ts = new System.Transactions.TransactionScope())
                {
                    var ct = new ET_ChiTietDonHang
                    {
                        IdDonHang = idDonHang,
                        IdSanPham = null,
                        IdCombo = null,
                        SoLuong = 1,
                        DonGiaGoc = soTien,
                        TienGiamGiaDong = 0,
                        DonGiaThucTe = soTien
                    };
                    bool ok = _chiTietDonHangGateway.Them(ct);
                    if (!ok) return OperationResult.Failed("Không thể thêm phụ thu.");

                    CapNhatTongTien(idDonHang);
                    
                    ts.Complete();
                    return OperationResult.Success();
                }
            }
            catch (Exception ex)
            {
                return OperationResult.Failed("Lỗi phụ thu: " + ex.Message);
            }
        }

        public OperationResult XoaMon(int idChiTiet, int idDonHang)
        {
            try
            {
                using (var ts = new System.Transactions.TransactionScope())
                {
                    bool ok = _chiTietDonHangGateway.Xoa(idChiTiet);
                    if (!ok) return OperationResult.Failed("Không thể xóa món.");
                    
                    CapNhatTongTien(idDonHang);
                    
                    ts.Complete();
                    return OperationResult.Success();
                }
            }
            catch (Exception ex)
            {
                return OperationResult.Failed("Lỗi xóa món: " + ex.Message);
            }
        }
        #endregion

        #region Đối Soát Thanh Toán & Trả Bảng
        
        /// <summary>
        /// Xử lý tính cước, ghi nhận phiếu thu và giải phóng tài nguyên không gian (bàn ăn).
        /// Áp dụng cơ chế an toàn TransactionScope báo lỗi nếu thiếu đồng bộ.
        /// </summary>
        public OperationResult ThanhToan(int idDonHang, List<int> dsBanAnId, string phuongThucTT = "TienMat")
        {
            try
            {
                using (var ts = new System.Transactions.TransactionScope())
                {
                    var donHang = _donHangGateway.LayTheoId(idDonHang);
                    if (donHang == null) return OperationResult.Failed("Cảnh báo: Dữ liệu đơn hàng không hợp lệ hoặc đã bị dọn dẹp.");

                    // Bước 1: Khóa trạng thái đơn hàng thành hoàn tất (Đã Thanh Toán) để chốt vòng lặp đối soát doanh thu.
                    donHang.TrangThai = AppConstants.TrangThaiDonHang.DaThanhToan;
                    _donHangGateway.Sua(donHang);

                    // Bước 2: Thiết lập trạng thái Hoàn Tất cho mọi hợp đồng đặt bàn nội suy
                    var tatcaCTDH = _chiTietDonHangGateway.LoadByDonHang(idDonHang);
                    var ctdhIds = tatcaCTDH.Select(x => x.Id).ToList();
                    var dsDatBan = _datBanGateway.LoadDS()
                        .Where(x => ctdhIds.Contains(x.IdChiTietDonHang) && x.TrangThai == "DaNhan")
                        .ToList();
                    foreach (var db in dsDatBan)
                    {
                        db.TrangThai = "HoanTat";
                        _datBanGateway.Sua(db);

                        // Tiêu hao cọc thật sự vào hóa đơn
                        if (db.TienCoc > 0 && db.IdKhachHang.HasValue)
                        {
                            var thuResult = _giaoDichViService.KhauTruTienDongBang(db.IdKhachHang.Value, db.TienCoc, idDonHang);
                            if (!thuResult.IsSuccess) return OperationResult.Failed("Lỗi thanh khoản cọc: " + thuResult.ErrorMessage);
                        }
                    }

                    // Bước 3: Toán hạng khấu trừ tiền cọc - Nếu đã cọc quá mệnh giá hóa đơn thì không thu thêm mặt tiền.
                    decimal tongCoc = dsDatBan.Sum(x => x.TienCoc);
                    decimal tienCanThu = donHang.TongTien - tongCoc;
                    if (tienCanThu < 0) tienCanThu = 0;

                    // Bước 4: Sinh phiếu thu cho phòng kế toán đối kiểm
                    var phieuThu = new ET_PhieuThu
                    {
                        MaCode = "PT-NH-" + DateTime.Now.ToString("yyMMddHHmmss") + "-" + Guid.NewGuid().ToString().Substring(0, 4).ToUpper(),
                        IdDonHang = idDonHang,
                        SoTien = tienCanThu,
                        PhuongThuc = phuongThucTT,
                        ThoiGian = DateTime.Now,
                        CreatedAt = DateTime.Now,
                        CreatedBy = SessionManager.CurrentUser != null ? (int?)SessionManager.CurrentUser.Id : null
                    };
                    _phieuThuGateway.Them(phieuThu);

                    // Bước 5: Phóng thích các bàn về tình trạng trống rỗng chờ khách kế tiếp
                    foreach (int idBan in dsBanAnId)
                    {
                        var ban = _banAnGateway.LayTheoId(idBan);
                        if (ban != null)
                        {
                            ban.TrangThai = AppConstants.TrangThaiBanAn.Trong;
                            _banAnGateway.Sua(ban);
                        }
                    }

                    ts.Complete();
                    return OperationResult.Success();
                }
            }
            catch (Exception ex)
            {
                return OperationResult.Failed("Ngoại lệ phát sinh khi tất toán bàn ăn: " + ex.Message);
            }
        }

        public OperationResult TraBan(List<int> dsBanAnId)
        {
            try
            {
                foreach (int idBan in dsBanAnId)
                {
                    var ban = _banAnGateway.LayTheoId(idBan);
                    if (ban != null)
                    {
                        ban.TrangThai = "Trong";
                        _banAnGateway.Sua(ban);
                    }
                }
                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                return OperationResult.Failed("Lỗi trả bàn: " + ex.Message);
            }
        }
        #endregion

        #region Tiện ích hỗ trợ (Helpers)
        private void CapNhatTongTien(int idDonHang)
        {
            var chiTiets = _chiTietDonHangGateway.LoadByDonHang(idDonHang);
            decimal tong = 0;
            foreach (var ct in chiTiets)
            {
                tong += ct.DonGiaThucTe * ct.SoLuong;
            }

            var donHang = _donHangGateway.LayTheoId(idDonHang);
            if (donHang != null)
            {
                donHang.TongTien = tong;
                _donHangGateway.Sua(donHang);
            }
        }

        public List<ET_ChiTietDonHang> LayBill(int idDonHang)
        {
            return _chiTietDonHangGateway.LoadByDonHang(idDonHang);
        }

        public ET_DatBan TimDatBanActiveTheoBan(int idBanAn)
        {
            var chiTiets = _chiTietDatBanGateway.LoadDS()
                .Where(x => x.IdBanAn == idBanAn).ToList();

            foreach (var ct in chiTiets)
            {
                var datBan = _datBanGateway.LayTheoId(ct.IdDatBan);
                if (datBan != null && datBan.TrangThai == "DaNhan")
                    return datBan;
            }

            // Kế hoạch dự phòng: rà soát xem có trạng thái bàn nào đã giữ hay không nếu bàn thường không có kết quả
            foreach (var ct in chiTiets)
            {
                var datBan = _datBanGateway.LayTheoId(ct.IdDatBan);
                if (datBan != null && datBan.TrangThai == AppConstants.TrangThaiBanAn.DaDat)
                    return datBan;
            }
            return null;
        }

        public ET_DonHang LayDonHang(int idDonHang)
        {
            return _donHangGateway.LayTheoId(idDonHang);
        }

        public ET_ChiTietDonHang LayCtdh(int idCtdh)
        {
            return _chiTietDonHangGateway.LayTheoId(idCtdh);
        }

        public List<ET_ChiTietDonHang> LayCtdhTheoDonHang(int idDonHang)
        {
            return _chiTietDonHangGateway.LoadByDonHang(idDonHang);
        }

        public OperationResult HuyDonHang(int idDonHang)
        {
            try
            {
                var dh = _donHangGateway.LayTheoId(idDonHang);
                if (dh == null) return OperationResult.Failed("Đơn hàng không tồn tại.");
                dh.TrangThai = AppConstants.TrangThaiDonHang.DaHuy;
                return _donHangGateway.Sua(dh)
                    ? OperationResult.Success()
                    : OperationResult.Failed("Không thể hủy đơn hàng.");
            }
            catch (Exception ex) { return OperationResult.Failed("Lỗi hủy đơn: " + ex.Message); }
        }

        public decimal TinhTongCoc(int idDonHang)
        {
            var tatcaCTDH = _chiTietDonHangGateway.LoadByDonHang(idDonHang);
            var ctdhIds = tatcaCTDH.Select(x => x.Id).ToList();
            return _datBanGateway.LoadDS()
                .Where(x => ctdhIds.Contains(x.IdChiTietDonHang))
                .Sum(x => x.TienCoc);
        }
        #endregion

        #region Đặt bàn cho Đoàn khách (Tập thể)
        public OperationResult<int> DatBanTruocForDoan(int idNhaHang, List<int> dsBanAnId, ET_DatBan info, int idDoan, string maRfid = "")
        {
            var result = DatBanTruoc(idNhaHang, dsBanAnId, info, maRfid);
            if (!result.IsSuccess) return result;

            try
            {
                var existingQuota = _doanKhachService.LayQuotaTheoLoai(idDoan, AppConstants.LoaiDichVuDoan.AnUong);
                bool allotmentAlreadyPaid = (existingQuota != null);

                var dichVu = new ET_DoanKhach_DichVu
                {
                    IdDoan = idDoan,
                    LoaiDichVu = AppConstants.LoaiDichVuDoan.AnUong,
                    SoLuong = info.SoLuongKhach,
                    DonGia = allotmentAlreadyPaid ? 0 : info.TienCoc,
                    NgaySuDung = info.ThoiGianDenDuKien,
                    GhiChu = $"Đặt bàn NH - {dsBanAnId.Count} bàn, {info.SoLuongKhach} khách ({info.ThoiGianDenDuKien:dd/MM HH:mm})"
                };
                _doanKhachDichVuGateway.Them(dichVu);

                var datBan = _datBanGateway.LayTheoId(result.Data);
                if (datBan != null)
                {
                    var ctdh = _chiTietDonHangGateway.LayTheoId(datBan.IdChiTietDonHang);
                    if (ctdh != null)
                    {
                        var dh = _donHangGateway.LayTheoId(ctdh.IdDonHang);
                        if (dh != null)
                        {
                            dh.IdDoan = idDoan;
                            _donHangGateway.Sua(dh);
                        }
                    }
                }
            }
            catch { }

            return result;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using ET;

namespace BUS
{
    public class BUS_Phong
    {
        private readonly IDoanKhachGateway _doanKhachGateway;
        private readonly IGiaoDichViGateway _giaoDichGateway;
        private readonly IPhieuChiGateway _phieuChiGateway;
        private readonly IKhachHangGateway _khachHangGateway;
        private readonly IPhongGateway _phongGateway;
        private readonly IChiTietDonHangGateway _chiTietGateway;
        private readonly IKhuVucGateway _khuVucGateway;
        private readonly ISanPhamGateway _sanPhamGateway;
        private readonly IDatPhongChiTietGateway _datPhongGateway;
        private readonly IDonHangGateway _donHangGateway;
        private readonly IChiTietDatPhongGateway _chiTietDatPhongGateway;
        private readonly IDoanKhachDichVuGateway _doanKhachDichVuGateway;
        private readonly IViDienTuGateway _viGateway;
        private readonly IPhieuThuGateway _phieuThuGateway;

        #region Khởi tạo & Cấu hình Singleton
        private static BUS_Phong instance;
        public static BUS_Phong Instance
        {
            get
            {
                if (instance == null) instance = new BUS_Phong();
                return instance;
            }
        }

        public BUS_Phong() : this(new DefaultDoanKhachGateway(), new DefaultGiaoDichViGateway(), new DefaultPhieuChiGateway(),
                                  new DefaultKhachHangGateway(), new DefaultPhongGateway(), new DefaultChiTietDonHangGateway(),
                                  new DefaultKhuVucGateway(), new DefaultSanPhamGateway(), new DefaultDatPhongChiTietGateway(),
                                  new DefaultDonHangGateway(), new DefaultChiTietDatPhongGateway(), new DefaultDoanKhachDichVuGateway(),
                                  new DefaultViDienTuGateway(), new DefaultPhieuThuGateway()) { }

        public BUS_Phong(IDoanKhachGateway doanKhachGateway, IGiaoDichViGateway giaoDichGateway, IPhieuChiGateway phieuChiGateway,
                         IKhachHangGateway khachHangGateway, IPhongGateway phongGateway, IChiTietDonHangGateway chiTietGateway,
                         IKhuVucGateway khuVucGateway, ISanPhamGateway sanPhamGateway, IDatPhongChiTietGateway datPhongGateway,
                         IDonHangGateway donHangGateway, IChiTietDatPhongGateway chiTietDatPhongGateway, IDoanKhachDichVuGateway doanKhachDichVuGateway,
                         IViDienTuGateway viGateway, IPhieuThuGateway phieuThuGateway)
        {
            _doanKhachGateway = doanKhachGateway;
            _giaoDichGateway = giaoDichGateway;
            _phieuChiGateway = phieuChiGateway;
            _khachHangGateway = khachHangGateway;
            _phongGateway = phongGateway;
            _chiTietGateway = chiTietGateway;
            _khuVucGateway = khuVucGateway;
            _sanPhamGateway = sanPhamGateway;
            _datPhongGateway = datPhongGateway;
            _donHangGateway = donHangGateway;
            _chiTietDatPhongGateway = chiTietDatPhongGateway;
            _doanKhachDichVuGateway = doanKhachDichVuGateway;
            _viGateway = viGateway;
            _phieuThuGateway = phieuThuGateway;
        }
        #endregion

        #region Cơ bản (CRUD)

        public List<ET_Phong> LoadDS()
        {
            return _phongGateway.LoadDS();
        }

        public ET_Phong LayTheoId(int id)
        {
            return _phongGateway.LayTheoId(id);
        }

        public bool Them(ET_Phong et)
        {
            return _phongGateway.Them(et);
        }

        public bool Sua(ET_Phong et)
        {
            return _phongGateway.Sua(et);
        }

        public bool Xoa(int id)
        {
            return _phongGateway.Xoa(id);
        }

        public ET_DatPhongChiTiet LayThongTinDatPhong(int idDatPhong)
        {
            return _datPhongGateway.LayTheoId(idDatPhong);
        }

        public List<int> GetBusyRoomIds(DateTime start, DateTime end)
        {
            return _phongGateway.GetBusyRoomIds(start, end, 1);
        }
        #endregion

        #region Nghiệp vụ Thuê Phòng Khách Sạn (CheckIn - Nhận phòng)
        /// <summary>
        /// Mở phòng cho khách sử dụng lấy ngay (Tự động đặt giờ trả phòng vào trưa ngày hôm sau)
        /// </summary>
        public bool CheckIn(int idPhong, int? idKhachHang, decimal soTien, string phuongThuc)
        {
            DateTime defaultCheckOut = DateTime.Now.Date.AddDays(1).AddHours(12);
            return CheckIn(idPhong, idKhachHang, soTien, phuongThuc, 1, defaultCheckOut);
        }

        public bool CheckIn(int idPhong, int? idKhachHang, decimal soTien, string phuongThuc, int idNhanVien, DateTime ngayTraDuKien)
        {

            using (var ts = new System.Transactions.TransactionScope())
            {
                try
                {
                    // Bước 1: Khóa kiểm tra phòng để chắc chắn phòng không bị người khác chốt trước vài giây
                    var busyIds = GetBusyRoomIds(DateTime.Now, ngayTraDuKien);
                    if (busyIds.Contains(idPhong)) return false;

                    // Bước 2: Tạo mới hóa đơn để lưu chứng từ tiền khách đóng
                    ET_DonHang dh = new ET_DonHang
                    {
                        MaCode = "DH-KS-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + Guid.NewGuid().ToString().Substring(0, 4).ToUpper(),
                        IdKhachHang = idKhachHang,
                        ThoiGian = DateTime.Now,
                        TongTien = soTien,
                        TienGiamGia = 0,
                        TrangThai = "DaThanhToan",
                        CreatedAt = DateTime.Now,
                        CreatedBy = idNhanVien
                    };
                    int idDonHang = _donHangGateway.ThemVaLayId(dh);
                    if (idDonHang <= 0) return false;

                    // Bước 3: Thu tiền từ khách hàng
                    if (phuongThuc == "ViRFID")
                    {
                        // Giảm số dư trong thẻ ví của khách
                        if (idKhachHang == null) return false;
                        var vi = _viGateway.LayTheoKhachHang(idKhachHang.Value);
                        if (vi == null || vi.SoDuKhaDung < soTien) return false;

                        vi.SoDuKhaDung -= soTien;
                        if (!_viGateway.Sua(vi)) return false;

                        // Tạo nhật ký trừ tiền ví
                        ET_GiaoDichVi gd = new ET_GiaoDichVi
                        {
                            MaCode = "GD" + DateTime.Now.Ticks.ToString().Substring(10),
                            IdVi = vi.Id,
                            LoaiGiaoDich = "ThanhToanDichVu",
                            SoTien = soTien,
                            IdDonHangLienQuan = idDonHang,
                            ThoiGian = DateTime.Now,
                            CreatedAt = DateTime.Now,
                            CreatedBy = idNhanVien
                        };
                        _giaoDichGateway.Them(gd);
                    }
                    else
                    {
                        // Nếu khách đưa tiền mặt/chuyển khoản thì lưu Phiếu thu thường
                        ET_PhieuThu pt = new ET_PhieuThu
                        {
                            MaCode = "PT" + DateTime.Now.Ticks.ToString().Substring(10),
                            IdDonHang = idDonHang,
                            SoTien = soTien,
                            PhuongThuc = phuongThuc,
                            ThoiGian = DateTime.Now,
                            CreatedAt = DateTime.Now,
                            CreatedBy = idNhanVien
                        };
                        _phieuThuGateway.Them(pt);
                    }

                    // Bước 4: Chuyển đổi trạng thái phòng thành Đang Sử Dụng trên sơ đồ
                    var phong = _phongGateway.LayTheoId(idPhong);
                    if (phong == null) return false;
                    phong.TrangThai = "DangSuDung";
                    phong.UpdatedAt = DateTime.Now;
                    _phongGateway.Sua(phong);

                    // Bước 5: Bổ sung dòng thông tin phòng vào hóa đơn (giống như thêm món ăn)
                    ET_ChiTietDonHang ctdhKS = new ET_ChiTietDonHang
                    {
                        IdDonHang = idDonHang,
                        IdSanPham = phong.IdSanPham,
                        SoLuong = 1,
                        DonGiaGoc = soTien,
                        TienGiamGiaDong = 0,
                        DonGiaThucTe = soTien
                    };
                    int idCtdh = _chiTietGateway.ThemVaLayId(ctdhKS);
                    if (idCtdh <= 0) return false;

                    // Bước 6: Mở dữ liệu hệ thống ghi rõ ngày nhận, ngày trả dự kiến của khách này
                    ET_DatPhongChiTiet dpct = new ET_DatPhongChiTiet
                    {
                        IdChiTietDonHang = idCtdh,
                        NgayNhan = DateTime.Now,
                        NgayTra = ngayTraDuKien,
                        TrangThai = "DaNhan"
                    };
                    int idDatPhong = _datPhongGateway.ThemVaLayId(dpct);
                    if (idDatPhong <= 0) return false;

                    ET_ChiTietDatPhong ctdp = new ET_ChiTietDatPhong
                    {
                        IdDatPhongChiTiet = idDatPhong,
                        IdPhong = idPhong,
                        DonGiaThucTe = TinhGiaPhong(idPhong, DateTime.Now, ngayTraDuKien)
                    };
                    _chiTietDatPhongGateway.ThemVaLayId(ctdp);

                    ts.Complete();
                    return true;
                }
                catch { return false; }
            }
        }
        #endregion

        #region Nghiệp vụ Đặt Giữ Chỗ (Reservation)
        public bool ReserveRoom(int idPhong, int idKhachHang, DateTime ngayNhan, DateTime ngayTra, decimal tienCoc, int idNhanVien)
        {
            using (var ts = new System.Transactions.TransactionScope())
            {
                try
                {
                    // Bước 1: Kiểm tra chống đặt phòng trùng lịch (như phòng đã có người thuê)
                    var busyIds = GetBusyRoomIds(ngayNhan, ngayTra);
                    if (busyIds.Contains(idPhong)) return false;

                    // Bước 2: Khởi tạo hóa đơn tổng trước để theo dõi mọi khoản phát sinh sau này
                    ET_DonHang dh = new ET_DonHang
                    {
                        MaCode = "DH-COC-" + DateTime.Now.ToString("yyyyMMdd") + "-" + Guid.NewGuid().ToString().Substring(0, 4),
                        IdKhachHang = idKhachHang,
                        ThoiGian = DateTime.Now,
                        TongTien = tienCoc,
                        TrangThai = tienCoc > 0 ? "DaDatCoc" : "ChoThanhToan",
                        CreatedAt = DateTime.Now,
                        CreatedBy = idNhanVien
                    };
                    int idDonHangLienQuan = _donHangGateway.ThemVaLayId(dh);

                    // Bước 3: Tạo phiếu đóng cọc lấy tiền giữ chỗ từ khách
                    if (tienCoc > 0)
                    {
                        ET_PhieuThu pt = new ET_PhieuThu
                        {
                            MaCode = "PTC" + DateTime.Now.Ticks.ToString().Substring(10),
                            IdDonHang = idDonHangLienQuan,
                            SoTien = tienCoc,
                            PhuongThuc = "TienMat",
                            ThoiGian = DateTime.Now,
                            CreatedAt = DateTime.Now,
                            CreatedBy = idNhanVien
                        };
                        _phieuThuGateway.Them(pt);
                    }

                    var phong = _phongGateway.LayTheoId(idPhong);

                    // Bước 4: Tạo thông tin dòng lấy phòng, giá tiền bằng 0 vì chờ trả phòng mới tính thực tế
                    ET_ChiTietDonHang ctdh = new ET_ChiTietDonHang
                    {
                        IdDonHang = idDonHangLienQuan,
                        IdSanPham = phong.IdSanPham,
                        SoLuong = 1,
                        DonGiaGoc = 0, 
                        TienGiamGiaDong = 0,
                        DonGiaThucTe = 0
                    };
                    int idCtdh = _chiTietGateway.ThemVaLayId(ctdh);

                    // Bước 5: Mở dữ liệu lịch sử đặt trước
                    ET_DatPhongChiTiet dpct = new ET_DatPhongChiTiet
                    {
                        IdChiTietDonHang = idCtdh,
                        NgayNhan = ngayNhan,
                        NgayTra = ngayTra,
                        TrangThai = "DaDat"
                    };
                    int idDatPhong = _datPhongGateway.ThemVaLayId(dpct);

                    ET_ChiTietDatPhong ctdp = new ET_ChiTietDatPhong
                    {
                        IdDatPhongChiTiet = idDatPhong,
                        IdPhong = idPhong,
                        DonGiaThucTe = 0
                    };
                    _chiTietDatPhongGateway.ThemVaLayId(ctdp);

                    // Bước 6: Chuyển cờ giữ chỗ chặn khách vãng lai nếu hôm nay là ngày khách lên
                    if (ngayNhan.Date <= DateTime.Now.Date)
                    {
                        phong.TrangThai = "DaDat";
                        _phongGateway.Sua(phong);
                    }

                    ts.Complete();
                    return true;
                }
                catch { return false; }
            }
        }

        #endregion

        #region Tiện ích khách sạn & Chăm sóc phòng (Phụ thu / Minibar)
        /// <summary>
        /// Thêm số tiền tiêu dùng ngoài lề (Minibar, dọn dẹp thêm) gọi là phụ thu vào hóa đơn gốc.
        /// </summary>
        public OperationResult AddSurcharge(int idPhong, decimal soTien, string lyDo)
        {
            var dpct = GetDatPhongChiTietByPhong(idPhong);
            if (dpct == null || !dpct.IdChiTietDonHang.HasValue || dpct.IdChiTietDonHang.Value <= 0)
                return OperationResult.Failed("Không tìm thấy đơn hàng của phòng này!");

            var ctdh = _chiTietGateway.LayTheoId(dpct.IdChiTietDonHang.Value);
            if (ctdh == null)
                return OperationResult.Failed("Không tìm thấy chi tiết đơn hàng gốc!");

            var ctPhuThu = new ET_ChiTietDonHang
            {
                IdDonHang = ctdh.IdDonHang,
                SoLuong = 1,
                DonGiaGoc = soTien,
                TienGiamGiaDong = 0,
                DonGiaThucTe = soTien
            };

            if (_chiTietGateway.Them(ctPhuThu))
            {
                // Update lại tổng tiền của Đơn Hàng gốc khi có phát sinh
                var donHangGoc = _donHangGateway.LayTheoId(ctdh.IdDonHang);
                if (donHangGoc != null)
                {
                    donHangGoc.TongTien += soTien;
                    _donHangGateway.Sua(donHangGoc);
                }
                return OperationResult.Success($"Phụ thu {lyDo}: {soTien:N0} đ đã thêm thành công!");
            }
            
            return OperationResult.Failed("Lỗi khi thêm phụ thu vào đơn hàng.");
        }

        #endregion

        #region Nghiệp vụ Khách Đoàn (Nhiều phòng)
        public OperationResult ReserveGroup(List<int> idPhongs, ET_DoanKhach masterDoan, DateTime ngayNhan, DateTime ngayTra, decimal tienCoc, string phuongThucCoc, int idNhanVien)
        {
            using (var ts = new System.Transactions.TransactionScope())
            {
                try
                {
                    // Bước 1: Duyệt danh sách các phòng chọn có đang bị trùng với khách đoàn khác không
                    var busyIds = GetBusyRoomIds(ngayNhan, ngayTra);
                    if (busyIds.Any(id => idPhongs.Contains(id)))
                        return OperationResult.Failed("Có phòng đã bị đặt trong khoảng thời gian này.");

                    // Bước 2: Tạo nhóm chung (Đoàn Khách) để làm cầu nối gộp tiền các phòng vào
                    masterDoan.CreatedAt = DateTime.Now;
                    masterDoan.CreatedBy = idNhanVien;
                    int idDoan = _doanKhachGateway.ThemVaLayId(masterDoan);
                    if (idDoan <= 0) return OperationResult.Failed("Lỗi tạo thông tin Trưởng đoàn.");

                    // Bước 3: Chỉ khởi tạo MỘT hóa đơn tổng thể của Đoàn. Không tạo hóa đơn rời từng phòng
                    ET_DonHang dhMaster = new ET_DonHang
                    {
                        MaCode = "DH-GCOC-" + DateTime.Now.ToString("yyMMddHHmmss") + "-" + Guid.NewGuid().ToString().Substring(0, 4).ToUpper(),
                        IdDoan = idDoan,
                        ThoiGian = DateTime.Now,
                        TongTien = tienCoc,
                        TrangThai = (tienCoc > 0) ? "DaDatCoc" : "ChoThanhToan",
                        CreatedAt = DateTime.Now,
                        CreatedBy = idNhanVien
                    };
                    int masterDonHangId = _donHangGateway.ThemVaLayId(dhMaster);

                    // Bước 4: Tạo Phiếu thu nhập quỹ tiền cọc (Nếu đưa tiền)
                    if (tienCoc > 0)
                    {
                        ET_PhieuThu pt = new ET_PhieuThu
                        {
                            MaCode = "PTGC" + DateTime.Now.Ticks.ToString().Substring(10),
                            IdDonHang = masterDonHangId,
                            SoTien = tienCoc,
                            PhuongThuc = phuongThucCoc,
                            ThoiGian = DateTime.Now,
                            CreatedAt = DateTime.Now,
                            CreatedBy = idNhanVien
                        };
                        _phieuThuGateway.Them(pt);
                    }

                    // Bước 5: Thêm từng dòng chi tiết của mỗi phòng gắn kết chung hóa đơn
                    foreach (var idPhong in idPhongs)
                    {
                        var phong = _phongGateway.LayTheoId(idPhong);

                        ET_ChiTietDonHang ctdh = new ET_ChiTietDonHang
                        {
                            IdDonHang = masterDonHangId,
                            IdSanPham = phong.IdSanPham,
                            SoLuong = 1,
                            DonGiaGoc = 0,
                            TienGiamGiaDong = 0,
                            DonGiaThucTe = 0
                        };
                        int idCtdh = _chiTietGateway.ThemVaLayId(ctdh);

                        // Bước 6: Mở dòng phân loại trạng thái đặt giữ chỗ riêng cho phòng đó
                        ET_DatPhongChiTiet dpct = new ET_DatPhongChiTiet
                        {
                            IdChiTietDonHang = idCtdh,
                            NgayNhan = ngayNhan,
                            NgayTra = ngayTra,
                            TrangThai = "DaDat"
                        };
                        int idDatPhong = _datPhongGateway.ThemVaLayId(dpct);

                        ET_ChiTietDatPhong ctdp = new ET_ChiTietDatPhong
                        {
                            IdDatPhongChiTiet = idDatPhong,
                            IdPhong = idPhong,
                            DonGiaThucTe = 0
                        };
                        _chiTietDatPhongGateway.ThemVaLayId(ctdp);
                        
                        // Bước 7: Bảo vệ phòng không cho mướn nếu ngày bắt đầu ở lại là hôm nay
                        if (ngayNhan.Date <= DateTime.Now.Date)
                        {
                            phong.TrangThai = "DaDat";
                            _phongGateway.Sua(phong);
                        }
                    }

                    ts.Complete();
                    return OperationResult.Success("Tạo đặt trước cho Đoàn thành công!");
                }
                catch (Exception ex)
                {
                    return OperationResult.Failed("Lỗi đặt đoàn: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Đặt phòng đoàn LIÊN KẾT với đoàn đã tồn tại (từ frmDoanKhach).
        /// Không tạo ET_DoanKhach mới — chỉ tạo DonHang + DatPhongChiTiet + DoanKhach_DichVu.
        /// Anti-Double-Counting: nếu đoàn đã có allotment "Phong" -> DonGia = 0 (tránh tính trùng).
        /// </summary>
        public OperationResult ReserveGroupForExistingDoan(List<int> idPhongs, int idDoanDaTonTai, 
            DateTime ngayNhan, DateTime ngayTra, decimal tienCoc, string phuongThucCoc, int idNhanVien)
        {
            using (var ts = new System.Transactions.TransactionScope())
            {
                try
                {
                    // Bước 1: Validate đoàn tồn tại
                    var doan = _doanKhachGateway.LayTheoId(idDoanDaTonTai);
                    if (doan == null)
                        return OperationResult.Failed("Không tìm thấy đoàn Id=" + idDoanDaTonTai);

                    // Bước 2: Validate phòng trùng
                    var busyIds = GetBusyRoomIds(ngayNhan, ngayTra);
                    if (busyIds.Any(id => idPhongs.Contains(id)))
                        return OperationResult.Failed("Có phòng đã bị đặt trong khoảng thời gian này.");

                    // Bước 3: Kiểm tra xem đoàn này đã được khai báo dịch vụ Lấy Phòng Khách Sạn trước đó chưa để tránh tính trùng tiền
                    var existingPhongQuota = BUS_DoanKhach.Instance.LayQuotaTheoLoai(idDoanDaTonTai, AppConstants.LoaiDichVuDoan.Phong);
                    bool allotmentAlreadyPaid = (existingPhongQuota != null);

                    // Bước 4: Tạo 1 ĐƠN HÀNG TRUNG TÂM (MASTER BILL / FOLIO)
                    ET_DonHang dhMaster = new ET_DonHang
                    {
                        MaCode = "DH-GCOC-" + DateTime.Now.ToString("yyMMddHHmmss") + "-" + Guid.NewGuid().ToString().Substring(0, 4).ToUpper(),
                        IdDoan = idDoanDaTonTai,
                        ThoiGian = DateTime.Now,
                        TongTien = tienCoc,
                        TrangThai = (tienCoc > 0) ? "DaDatCoc" : "ChoThanhToan",
                        CreatedAt = DateTime.Now,
                        CreatedBy = idNhanVien
                    };
                    int masterDonHangId = _donHangGateway.ThemVaLayId(dhMaster);

                    // Bước 5: Phiếu Thu (Cọc)
                    if (tienCoc > 0)
                    {
                        ET_PhieuThu pt = new ET_PhieuThu
                        {
                            MaCode = "PTGC" + DateTime.Now.Ticks.ToString().Substring(10),
                            IdDonHang = masterDonHangId,
                            SoTien = tienCoc,
                            PhuongThuc = phuongThucCoc,
                            ThoiGian = DateTime.Now,
                            CreatedAt = DateTime.Now,
                            CreatedBy = idNhanVien
                        };
                        _phieuThuGateway.Them(pt);
                    }

                    // Bước 6: Tạo DatPhongChiTiet + DoanKhach_DichVu cho mỗi phòng
                    foreach (var idPhong in idPhongs)
                    {
                        var phong = _phongGateway.LayTheoId(idPhong);
                        decimal giaPhong = TinhGiaPhong(idPhong, ngayNhan, ngayTra);

                        ET_ChiTietDonHang ctdh = new ET_ChiTietDonHang
                        {
                            IdDonHang = masterDonHangId,
                            IdSanPham = phong.IdSanPham,
                            SoLuong = 1,
                            DonGiaGoc = giaPhong,
                            TienGiamGiaDong = 0,
                            DonGiaThucTe = giaPhong
                        };
                        int idCtdh = _chiTietGateway.ThemVaLayId(ctdh);

                        ET_DatPhongChiTiet dpct = new ET_DatPhongChiTiet
                        {
                            IdChiTietDonHang = idCtdh,
                            NgayNhan = ngayNhan,
                            NgayTra = ngayTra,
                            TrangThai = "DaDat"
                        };
                        int idDatPhong = _datPhongGateway.ThemVaLayId(dpct);

                        ET_ChiTietDatPhong ctdp = new ET_ChiTietDatPhong
                        {
                            IdDatPhongChiTiet = idDatPhong,
                            IdPhong = idPhong,
                            DonGiaThucTe = giaPhong
                        };
                        _chiTietDatPhongGateway.ThemVaLayId(ctdp);

                        // Khóa phòng nếu check-in hôm nay
                        if (ngayNhan.Date <= DateTime.Now.Date)
                        {
                            phong.TrangThai = "DaDat";
                            _phongGateway.Sua(phong);
                        }

                        // Bước 7: Khởi tạo thông tin gói dịch vụ phòng để nhân viên Lễ Tân thấy số lượng và tình trạng phòng của đoàn
                        // Nếu trước đó đoàn đã báo số lượng và chốt tiền rồi thì giá của phân bổ lần này tính là 0 đồng.
                        var dichVu = new ET_DoanKhach_DichVu
                        {
                            IdDoan = idDoanDaTonTai,
                            LoaiDichVu = AppConstants.LoaiDichVuDoan.Phong,
                            IdSanPham = phong.IdSanPham,
                            IdChiTietDonHang = idCtdh,
                            SoLuong = 1,
                            DonGia = allotmentAlreadyPaid ? 0 : giaPhong,
                            NgaySuDung = ngayNhan,
                            GhiChu = $"Phòng {phong.TenPhong} ({ngayNhan:dd/MM} -> {ngayTra:dd/MM})"
                        };
                        _doanKhachDichVuGateway.Them(dichVu);
                    }

                    ts.Complete();
                    return OperationResult.Success($"Đã liên kết {idPhongs.Count} phòng vào đoàn \"{doan.TenDoan}\" thành công!");
                }
                catch (Exception ex)
                {
                    return OperationResult.Failed("Lỗi đặt đoàn: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Mở phòng cho khách hàng Vãng Lai (Khách đã đặt giữ chỗ trước đó)
        /// Không thu thêm tiền, mặc định rỗng chênh lệch
        /// </summary>
        public bool CheckInFromReservation(int idPhong, int idDatPhongChiTiet)
        {
            return CheckInFromReservation(idPhong, idDatPhongChiTiet, 0, "TienMat", 1);
        }

        /// <summary>
        /// Mở phòng cho khách hàng Đã Đặt Trước (Có xử lý thu tiền nếu khách đóng thêm hay ở ghép)
        /// </summary>
        public bool CheckInFromReservation(int idPhong, int idDatPhongChiTiet, decimal soTienThem, string phuongThuc, int idNhanVien)
        {
            using (var ts = new System.Transactions.TransactionScope())
            {
                try
                {
                    // Bước 1: Kiểm tra lại tờ phiếu giữ chỗ có đúng là đang ở trạng thái chờ khách tới nhận không
                    var dpct = _datPhongGateway.LayTheoId(idDatPhongChiTiet);
                    if (dpct == null || dpct.TrangThai != "DaDat") return false;

                    // Bước 2: Nếu có phát sinh thu thêm tiền tại quầy lúc đưa chìa khóa phòng
                    if (soTienThem > 0)
                    {
                        int idDonHangLienQuan = 0;
                        // Phân nhánh A: Tìm hóa đơn tổng lúc khách đặt cọc hồi xưa để cộng tiền vào cho gom chung
                        if (dpct.IdChiTietDonHang != null)
                        {
                            var ctdh = _chiTietGateway.LayTheoId(dpct.IdChiTietDonHang.Value);
                            if (ctdh != null)
                            {
                                idDonHangLienQuan = ctdh.IdDonHang;
                                var dh = _donHangGateway.LayTheoId(idDonHangLienQuan);
                                if (dh != null)
                                {
                                    dh.TongTien += soTienThem;
                                    _donHangGateway.Sua(dh);
                                }
                            }
                        }
                        // Phân nhánh B: Nếu lỗi dữ liệu không tìm ra hóa đơn cũ thì tạo luôn 1 hóa đơn khẩn cấp mới
                        else
                        {
                            ET_DonHang dhNew = new ET_DonHang
                            {
                                MaCode = "DH-KS-" + DateTime.Now.ToString("yyyyMMdd") + "-" + Guid.NewGuid().ToString().Substring(0,4),
                                ThoiGian = DateTime.Now,
                                TongTien = soTienThem,
                                TrangThai = "DaThanhToan",
                                CreatedAt = DateTime.Now,
                                CreatedBy = idNhanVien
                            };
                            idDonHangLienQuan = _donHangGateway.ThemVaLayId(dhNew);
                        }

                        // Bước 3: Trừ tiền qua ví điện tử hoặc in phiếu nhận tiền mặt
                        if (phuongThuc == "ViRFID")
                        {
                            var dh = _donHangGateway.LayTheoId(idDonHangLienQuan);
                            if (dh.IdKhachHang == null) return false;

                            var vi = _viGateway.LayTheoKhachHang(dh.IdKhachHang.Value);
                            if (vi == null || vi.SoDuKhaDung < soTienThem) return false;
                            vi.SoDuKhaDung -= soTienThem;
                            _viGateway.Sua(vi);

                            ET_GiaoDichVi gd = new ET_GiaoDichVi
                            {
                                MaCode = "GD" + DateTime.Now.Ticks.ToString().Substring(10),
                                IdVi = vi.Id, LoaiGiaoDich = "ThanhToanDichVu", SoTien = soTienThem,
                                IdDonHangLienQuan = idDonHangLienQuan, ThoiGian = DateTime.Now, CreatedAt = DateTime.Now, CreatedBy = idNhanVien
                            };
                            _giaoDichGateway.Them(gd);
                        }
                        else
                        {
                            ET_PhieuThu pt = new ET_PhieuThu
                            {
                                MaCode = "PT" + DateTime.Now.Ticks.ToString().Substring(10),
                                IdDonHang = idDonHangLienQuan, SoTien = soTienThem, PhuongThuc = phuongThuc,
                                ThoiGian = DateTime.Now, CreatedAt = DateTime.Now, CreatedBy = idNhanVien
                            };
                            _phieuThuGateway.Them(pt);
                        }
                    }

                    // Bước 4: Chấp nhận giao chìa khóa cho khách. Đổi trạng thái nhận phòng
                    dpct.TrangThai = "DaNhan";
                    dpct.NgayNhan = DateTime.Now; 
                    _datPhongGateway.Sua(dpct);

                    // Bước 5: Chốt phòng trên sơ đồ khách sạn thành Đang Sử Dụng để tránh người khác lấy nhầm
                    var phong = _phongGateway.LayTheoId(idPhong);
                    phong.TrangThai = "DangSuDung";
                    _phongGateway.Sua(phong);

                    ts.Complete();
                    return true;
                }
                catch { return false; }
            }
        }

        /// <summary>
        /// Mở phòng cho toàn bộ hành khách trong 1 đoàn cùng một lúc
        /// </summary>
        public OperationResult CheckInGroup(List<int> idDatPhongChiTiets, int idNhanVien)
        {
            using (var ts = new System.Transactions.TransactionScope())
            {
                try
                {
                    // Bước 1: Quét ngang qua danh sách mã đặt phòng được giao
                    foreach (var idDpct in idDatPhongChiTiets)
                    {
                        // Bước 2: Chấp nhận giao phòng cho những phiếu hợp lệ đang chờ nhận
                        var dpct = _datPhongGateway.LayTheoId(idDpct);
                        if (dpct != null && dpct.TrangThai == "DaDat")
                        {
                            dpct.TrangThai = "DaNhan";
                            dpct.NgayNhan = DateTime.Now;
                            _datPhongGateway.Sua(dpct);

                            // Bước 3: Tìm đúng phòng thực tế và đổi trạng thái để tránh bị bán nhầm cho khách khác
                            var ctdp = _chiTietDatPhongGateway.LoadDS().FirstOrDefault(x => x.IdDatPhongChiTiet == dpct.Id);
                            if (ctdp != null)
                            {
                                var phong = _phongGateway.LayTheoId(ctdp.IdPhong);
                                if (phong != null)
                                {
                                    phong.TrangThai = "DangSuDung";
                                    _phongGateway.Sua(phong);
                                }
                            }
                        }
                    }
                    ts.Complete();
                    return OperationResult.Success("Nhận phòng (Check-in) cho quy mô Đoàn thành công!");
                }
                catch (Exception ex) { return OperationResult.Failed(ex.Message); }
            }
        }
        #endregion

        #region Quy trình Trả Phòng Khách Sạn (CheckOut)
        // Hệ thống sẽ xử lý rẽ nhánh Trả Phòng: Tính toán thử đúng tiền không -> Đồng ý thu tiền -> Kết thúc đóng phòng.

        /// <summary>
        /// Bước 1: Tính toán thử xem tổng hóa đơn nếu khách lấy trả phòng bây giờ sẽ là bao nhiêu
        /// (Chỉ để cho thu ngân xem trước, chưa lưu xuống Database thật)
        /// </summary>
        public ET_CheckOutInfo CalculateCheckOut(int idPhong)
        {
            var allDP = _datPhongGateway.LoadDS();
            var allCTDP = _chiTietDatPhongGateway.LoadDS();

            // Tìm ra tờ phiếu ghi nhận khách đang sử dụng ở phòng này
            var booking = (from ct in allCTDP
                          join dp in allDP on ct.IdDatPhongChiTiet equals dp.Id
                          where ct.IdPhong == idPhong && dp.TrangThai == "DaNhan"
                          orderby dp.NgayNhan descending
                          select new { ct, dp }).FirstOrDefault();

            if (booking == null) return null;

            var phong = _phongGateway.LayTheoId(idPhong);
            DateTime now = DateTime.Now;
            
            // Lấy giá trị cơ bản từ máy tính toán giá phòng
            decimal giaGoc = TinhGiaPhong(idPhong, booking.dp.NgayNhan, booking.dp.NgayTra);
            
            // Xử lý tiền phạt phòng cho trường hợp quá giờ trả phòng chuẩn
            decimal phuThu = TinhPhuThuTreGio(idPhong, booking.dp.NgayTra, now);

            // Kiểm tra lại danh sách các khoản khách đã đóng tiền cọc
            decimal daThanhToan = 0;
            if (booking.dp.IdChiTietDonHang.HasValue)
            {
                var ctdh = _chiTietGateway.LayTheoId(booking.dp.IdChiTietDonHang.Value);
                if (ctdh != null)
                {
                    var dh = _donHangGateway.LayTheoId(ctdh.IdDonHang);
                    if (dh != null) daThanhToan = dh.TongTien;
                }
            }

            return new ET_CheckOutInfo
            {
                IdPhong = idPhong,
                TenPhong = phong?.TenPhong ?? "",
                IdDatPhongChiTiet = booking.dp.Id,
                IdChiTietDatPhong = booking.ct.Id,
                NgayNhan = booking.dp.NgayNhan,
                NgayTraDuKien = booking.dp.NgayTra,
                NgayTraThucTe = now,
                TienPhongGoc = giaGoc,
                SoGioTre = Math.Max(0, (now - booking.dp.NgayTra).TotalHours),
                PhuThuTreGio = phuThu,
                DaThanhToan = daThanhToan
            };
        }

        /// <summary>
        /// Tính tiền phạt trường hợp ra quá giờ báo cáo.
        /// Chế tài: <= 2 giờ: 30% | 2-4 giờ: 50% | Nhiều hơn: Phạt chịu nguyên 1 ngày đủ.
        /// </summary>
        public decimal TinhPhuThuTreGio(int idPhong, DateTime ngayTraDuKien, DateTime ngayTraThucTe)
        {
            if (ngayTraThucTe <= ngayTraDuKien) return 0;

            double soGioTre = (ngayTraThucTe - ngayTraDuKien).TotalHours;

            // Lấy giá từ bảng giá phù hợp
            var phong = _phongGateway.LayTheoId(idPhong);
            if (phong == null || !phong.IdSanPham.HasValue) return 0;
            decimal giaTheoNgay = BUS.BUS_BangGia.Instance.GetDynamicPrice(phong.IdSanPham.Value, ngayTraThucTe);

            if (soGioTre <= 2)
                return Math.Round(giaTheoNgay * 0.30m, 0);
            else if (soGioTre <= 4)
                return Math.Round(giaTheoNgay * 0.50m, 0);
            else
                return giaTheoNgay; // Lố > 4h = tính thêm 1 ngày full
        }

        /// <summary>
        /// Bước 2: Xác nhận khách rời khỏi phòng. Hàm lưu toàn bộ lịch sử thu/chi, 
        /// thu nốt phần tiền còn thiếu và cho người tạp vụ quyền tiếp quản dọn dẹp.
        /// </summary>
        public OperationResult ConfirmCheckOut(int idPhong, decimal soTienThuThem, string phuongThuc, int idNhanVien)
        {
            using (var ts = new System.Transactions.TransactionScope())
            {
                try
                {
                    var allDP = _datPhongGateway.LoadDS();
                    var allCTDP = _chiTietDatPhongGateway.LoadDS();

                    // Bước 1: Tra cứu hồ sơ thuê phòng đang xài của khách hiện hành
                    var booking = (from ct in allCTDP
                                  join dp in allDP on ct.IdDatPhongChiTiet equals dp.Id
                                  where ct.IdPhong == idPhong && dp.TrangThai == "DaNhan"
                                  orderby dp.NgayNhan descending
                                  select new { ct, dp }).FirstOrDefault();

                    if (booking == null)
                        return OperationResult.Failed("Không tìm thấy booking đang sử dụng cho phòng này.");

                    // Bước 2: Nếu chưa đóng đủ tiền -> Mở thủ tục đóng nốt tiền chênh lệch (quá giờ, ăn minibar...)
                    if (soTienThuThem > 0)
                    {
                        int idDH = 0;
                        if (booking.dp.IdChiTietDonHang.HasValue)
                        {
                            var ctdh = _chiTietGateway.LayTheoId(booking.dp.IdChiTietDonHang.Value);
                            idDH = ctdh?.IdDonHang ?? 0;
                        }

                        // Mồi thêm một hóa đơn nhỏ đính kèm nếu Database bị lỗi từ ngày trước gộp lại
                        if (idDH <= 0)
                        {
                            ET_DonHang dhNew = new ET_DonHang
                            {
                                MaCode = "DH-CO-FIX-" + DateTime.Now.ToString("yyMMdd") + "-" + Guid.NewGuid().ToString().Substring(0, 4),
                                ThoiGian = DateTime.Now,
                                TongTien = 0,
                                TrangThai = "DaThanhToan",
                                CreatedAt = DateTime.Now,
                                CreatedBy = idNhanVien
                            };
                            idDH = _donHangGateway.ThemVaLayId(dhNew);
                        }

                        // Bước 3: Thu hồi tiền trong thẻ RFID nếu xài thẻ
                        if (phuongThuc == AppConstants.PhuongThucThanhToan.ViRfid)
                        {
                            var dh = _donHangGateway.LayTheoId(idDH);
                            if (dh?.IdKhachHang == null)
                                return OperationResult.Failed("Chưa xác định khách hàng, không thể trừ ví RFID.");

                            var vi = _viGateway.LayTheoKhachHang(dh.IdKhachHang.Value);
                            if (vi == null || vi.SoDuKhaDung < soTienThuThem)
                                return OperationResult.Failed("Số dư ví điện tử không đủ.");

                            vi.SoDuKhaDung -= soTienThuThem;
                            _viGateway.Sua(vi);

                            _giaoDichGateway.Them(new ET_GiaoDichVi
                            {
                                MaCode = "GD-CO-" + DateTime.Now.ToString("yyMMddHHmmss") + "-" + Guid.NewGuid().ToString().Substring(0, 4).ToUpper(),
                                IdVi = vi.Id,
                                LoaiGiaoDich = AppConstants.LoaiGiaoDichVi.ThanhToanDichVu,
                                SoTien = soTienThuThem,
                                IdDonHangLienQuan = idDH,
                                ThoiGian = DateTime.Now,
                                CreatedAt = DateTime.Now,
                                CreatedBy = idNhanVien
                            });
                        }
                        else
                        {
                            _phieuThuGateway.Them(new ET_PhieuThu
                            {
                                MaCode = "PT-CO-" + DateTime.Now.ToString("yyMMddHHmmss") + "-" + Guid.NewGuid().ToString().Substring(0, 4).ToUpper(),
                                IdDonHang = idDH,
                                SoTien = soTienThuThem,
                                PhuongThuc = phuongThuc,
                                ThoiGian = DateTime.Now,
                                CreatedAt = DateTime.Now,
                                CreatedBy = idNhanVien
                            });
                        }

                        // Cố định tăng tổng tiền của ĐH
                        var dhUpdate = _donHangGateway.LayTheoId(idDH);
                        if (dhUpdate != null)
                        {
                            dhUpdate.TongTien += soTienThuThem;
                            _donHangGateway.Sua(dhUpdate);
                        }
                    }

                    // 2. Đóng booking
                    decimal tongTienSauCung = TinhGiaPhong(idPhong, booking.dp.NgayNhan, DateTime.Now);
                    var dpct = booking.dp;
                    dpct.TrangThai = "HoanTat";
                    dpct.NgayTra = DateTime.Now;
                    _datPhongGateway.Sua(dpct);

                    var ctRecord = booking.ct;
                    ctRecord.DonGiaThucTe = tongTienSauCung;
                    _chiTietDatPhongGateway.Sua(ctRecord);

                    // 3. Chuyển trạng thái phòng
                    var phong = _phongGateway.LayTheoId(idPhong);
                    phong.TrangThai = "DonDep";
                    phong.UpdatedAt = DateTime.Now;
                    _phongGateway.Sua(phong);

                    ts.Complete();
                    return OperationResult.Success();
                }
                catch (System.Data.DBConcurrencyException)
                {
                    return OperationResult.Failed("Ví đã bị thay đổi bởi terminal khác. Vui lòng thử lại.");
                }
                catch (Exception ex)
                {
                    return OperationResult.Failed("Lỗi checkout: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Trả phòng phiên bản giao diện thiết kế cũ (cố định không cần thu tiền chênh lệch hay phát sinh do xài đồ minibar)
        /// </summary>
        public bool CheckOut(int idPhong)
        {
            return ConfirmCheckOut(idPhong, 0, "TienMat", 1).IsSuccess;
        }

        public bool CheckOut(int idPhong, int idNhanVien)
        {
            return ConfirmCheckOut(idPhong, 0, "TienMat", idNhanVien).IsSuccess;
        }

        public bool FinishCleaning(int idPhong)
        {
            var phong = _phongGateway.LayTheoId(idPhong);
            if (phong == null) return false;
            phong.TrangThai = "Trong";
            phong.UpdatedAt = DateTime.Now;
            return _phongGateway.Sua(phong);
        }

        /// <summary>
        /// Tìm DatPhongChiTiet đang active (DangSuDung/DaNhan) của 1 phòng cụ thể.
        /// Dùng cho Phụ thu / Dịch vụ phòng.
        /// </summary>
        public ET_DatPhongChiTiet GetDatPhongChiTietByPhong(int idPhong)
        {
            var allCTDP = _chiTietDatPhongGateway.LoadDS();
            var allDP = _datPhongGateway.LoadDS();

            var booking = (from ct in allCTDP
                          join dp in allDP on ct.IdDatPhongChiTiet equals dp.Id
                          where ct.IdPhong == idPhong && dp.TrangThai == "DaNhan"
                          orderby dp.NgayNhan descending
                          select dp).FirstOrDefault();

            return booking;
        }
        public decimal TinhGiaPhong(int idPhong)
        {
            return TinhGiaPhong(idPhong, DateTime.Now, DateTime.Now.AddDays(1).Date.AddHours(12));
        }

        public decimal TinhGiaPhong(int idPhong, DateTime checkIn, DateTime checkOut)
        {
            try
            {
                var phong = _phongGateway.LayTheoId(idPhong);
                // Flat pricing engine: tự chọn giá Thường/CuốiTuần/Lễ
                if (phong == null || !phong.IdSanPham.HasValue) return 0;
                decimal giaTheoNgay = BUS.BUS_BangGia.Instance.GetDynamicPrice(phong.IdSanPham.Value, checkIn);
                decimal giaNghiTrua = Math.Round(giaTheoNgay * 0.5m, 0); // Nghỉ trưa = 50% giá ngày

                int maxGioNghiTrua = 4; 
                TimeSpan duration = checkOut - checkIn;
                double totalHours = duration.TotalHours;

                if (totalHours > 0 && totalHours <= maxGioNghiTrua)
                {
                    return giaNghiTrua;
                }
                else
                {
                    int soNgay = (int)Math.Ceiling(duration.TotalDays);
                    if (soNgay <= 0) soNgay = 1;

                    return giaTheoNgay * soNgay;
                }
            }
            catch { return 0; }
        }

        /// <summary>
        /// Quy trình Hủy đặt phòng khách sạn và tiến hành làm thủ tục HOÀN CỌC cho khách
        /// Bước 1: Tìm xem ban đầu lúc cọc khách đưa tiền mặt hay quẹt thẻ ví
        /// Bước 2: Nếu đóng bằng Thẻ Ví -> Cộng tiền lại vào thẻ khách.
        /// Bước 3: Nếu đưa tiền Thu Ngân -> Tạo tờ Phiếu Chi (Chi xuất quỹ) để giao tiền lại cho khách
        /// </summary>
        public OperationResult CancelReservation(int idDatPhongChiTiet, int idNhanVien = 1)
        {
            var dpct = _datPhongGateway.LayTheoId(idDatPhongChiTiet);
            if (dpct == null || dpct.TrangThai != "DaDat")
                return OperationResult.Failed("Không tìm thấy đặt phòng hoặc đã bị hủy.");

            using (var ts = new System.Transactions.TransactionScope())
            {
                try
                {
                    decimal tienHoan = 0;

                    // B1: Tìm đơn hàng qua ChiTietDonHang
                    if (dpct.IdChiTietDonHang.HasValue)
                    {
                        var ctdh = _chiTietGateway.LayTheoId(dpct.IdChiTietDonHang.Value);
                        var dh = ctdh != null ? _donHangGateway.LayTheoId(ctdh.IdDonHang) : null;
                        if (dh != null)
                        {
                            tienHoan = dh.TongTien; // Toàn bộ tiền cọc

                            // B2: Xác định cọc bằng gì: Tìm GiaoDichVi có IdDonHangLienQuan
                            var gdVi = _giaoDichGateway.LoadDS()
                                .FirstOrDefault(x => x.IdDonHangLienQuan == dh.Id);

                            if (gdVi != null) // Cọc bằng Ví RFID
                            {
                                var vi = _viGateway.LayTheoId(gdVi.IdVi);
                                if (vi != null && tienHoan > 0)
                                {
                                    vi.SoDuKhaDung += tienHoan;
                                    _viGateway.Sua(vi);

                                    var gdHoan = new ET_GiaoDichVi
                                    {
                                        MaCode = "GD-REF-" + DateTime.Now.Ticks.ToString().Substring(10),
                                        IdVi = vi.Id,
                                        LoaiGiaoDich = AppConstants.LoaiGiaoDichVi.HoanCoc,
                                        SoTien = tienHoan,
                                        IdDonHangLienQuan = dh.Id,
                                        ThoiGian = DateTime.Now,
                                        CreatedAt = DateTime.Now,
                                        CreatedBy = idNhanVien
                                    };
                                    _giaoDichGateway.Them(gdHoan);
                                }
                            }
                            else if (tienHoan > 0) // Cọc bằng Tiền Mặt -> Sinh Phiếu Chi
                            {
                                var pc = new ET_PhieuChi
                                {
                                    MaCode = "PC-HUY-" + DateTime.Now.Ticks.ToString().Substring(10),
                                    SoTien = tienHoan,
                                    LyDo = "Hoàn tiền cọc hủy phòng (DatPhong #" + idDatPhongChiTiet + ")",
                                    ThoiGian = DateTime.Now,
                                    CreatedAt = DateTime.Now,
                                    CreatedBy = idNhanVien
                                };
                                _phieuChiGateway.Them(pc);
                            }

                            // Cập nhật trạng thái đơn hàng
                            dh.TrangThai = "DaHuy";
                            _donHangGateway.Sua(dh);
                        }
                    }

                    // Bước 4: Đánh dấu phiếu hủy thành công
                    dpct.TrangThai = "DaHuy";
                    _datPhongGateway.Sua(dpct);

                    // Bước 5: Giải phóng trạng thái phòng trên sơ đồ nếu phòng đang bị khóa bởi phiếu này
                    var ctDatPhong = _chiTietDatPhongGateway.LoadDS().FirstOrDefault(x => x.IdDatPhongChiTiet == idDatPhongChiTiet);
                    if (ctDatPhong != null)
                    {
                        var phong = _phongGateway.LayTheoId(ctDatPhong.IdPhong);
                        if (phong != null && phong.TrangThai == "DaDat")
                        {
                            phong.TrangThai = "Trong"; 
                            phong.UpdatedAt = DateTime.Now;
                            _phongGateway.Sua(phong);
                        }
                    }

                    ts.Complete();

                    if (tienHoan > 0)
                        return OperationResult.Success(string.Format("Hủy thành công. Đã hoàn {0:N0}đ cho khách.", tienHoan));
                    return OperationResult.Success("Hủy đặt phòng thành công.");
                }
                catch (Exception ex)
                {
                    return OperationResult.Failed("Lỗi hủy đặt: " + ex.Message);
                }
            }
        }

        public List<ET_RoomMapItem> GetRoomMapData()
        {
            var allPhong = _phongGateway.LoadDS();
            var allDatPhong = _datPhongGateway.LoadDS();
            var allCTDP = _chiTietDatPhongGateway.LoadDS();
            var allDonHang = _donHangGateway.LoadDS();
            var allKhach = _khachHangGateway.LoadDS();

            var allSanPham = _sanPhamGateway.LoadDS().ToDictionary(x => x.Id, x => x);
            var allKhuVuc = _khuVucGateway.LoadDS().ToDictionary(x => x.Id, x => x);

            var result = new List<ET_RoomMapItem>();

            foreach (var p in allPhong)
            {
                var currentCTDP = (from ct in allCTDP
                                 join dp in allDatPhong on ct.IdDatPhongChiTiet equals dp.Id
                                 where ct.IdPhong == p.Id && dp.TrangThai == "DaNhan"
                                 orderby dp.NgayNhan descending
                                 select new { ct, dp }).FirstOrDefault();

                var nextCTDP = (from ct in allCTDP
                                 join dp in allDatPhong on ct.IdDatPhongChiTiet equals dp.Id
                                 where ct.IdPhong == p.Id && dp.TrangThai == "DaDat" && dp.NgayNhan.Date >= DateTime.Now.Date
                                 orderby dp.NgayNhan ascending
                                 select dp).FirstOrDefault();

                string khachHienTai = "";
                string sdtKhach = "";
                if (currentCTDP != null)
                {
                    var dh = allDonHang.FirstOrDefault(x =>
                    {
                        if (currentCTDP.dp.IdChiTietDonHang == null) return false;
                        var ctdh = _chiTietGateway.LayTheoId(currentCTDP.dp.IdChiTietDonHang.Value);
                        return ctdh != null && x.Id == ctdh.IdDonHang;
                    });
                    if (dh != null && dh.IdKhachHang != null)
                    {
                        var kh = allKhach.FirstOrDefault(x => x.Id == dh.IdKhachHang);
                        khachHienTai = kh?.HoTen ?? "Khách vãng lai";
                        sdtKhach = kh?.DienThoai ?? "";
                    }
                    else
                    {
                        khachHienTai = "Khách vãng lai";
                    }
                }

                // lấy cục db lên
                var sp = (p.IdSanPham.HasValue && allSanPham.ContainsKey(p.IdSanPham.Value)) ? allSanPham[p.IdSanPham.Value] : null;
                var kv = (sp?.IdKhuVuc != null && allKhuVuc.ContainsKey((int)sp.IdKhuVuc)) ? allKhuVuc[(int)sp.IdKhuVuc] : null;

                result.Add(new ET_RoomMapItem
                {
                    Id = p.Id,
                    TenPhong = p.TenPhong,
                    TrangThai = p.TrangThai, 
                    KhachHienTai = khachHienTai,
                    SdtKhach = sdtKhach,
                    NgayCheckIn = currentCTDP?.dp.NgayNhan,
                    NgayTraDuKien = currentCTDP?.dp.NgayTra,
                    IdDatPhongTiepTheo = nextCTDP?.Id,
                    NgayNhanTiepTheo = nextCTDP?.NgayNhan,
                    IdSanPham = p.IdSanPham ?? 0,
                    TenLoaiPhong = sp?.Ten ?? "Standard",
                    MaKhuVuc = kv?.MaCode ?? "KV00",
                    TenKhuVuc = kv?.TenKhuVuc ?? "Chưa rõ",
                    DonGia = sp?.DonGia ?? 0,
                    SucChua = p.SucChua ?? 0,
                    //  Không gọi TinhGiaPhong nhiều lần trong loop.
                    // Chỉ tính khi lễ tân CLICK vào thẻ phòng .
                    TongTienTamTinh = 0
                });
            }

            return result;
        }
        #endregion

    }
}

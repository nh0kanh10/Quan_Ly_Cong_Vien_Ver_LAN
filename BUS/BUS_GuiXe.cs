using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using ET;

namespace BUS
{
    public class BUS_GuiXe
    {
        private readonly ILuotVaoRaBaiXeGateway _luotGateway;
        private readonly ITheRfidGateway _theRfidGateway;
        private readonly IViDienTuGateway _viGateway;
        private readonly IDonHangGateway _donHangGateway;
        private readonly IChiTietDonHangGateway _chiTietDonHangGateway;
        private readonly IVeDoXeChiTietGateway _veDoXeGateway;
        private readonly IGiaoDichViGateway _giaoDichGateway;
        private readonly IPhieuThuGateway _phieuThuGateway;
        private readonly IGiaGuiXeGateway _giaGuiXeGateway;

        private static BUS_GuiXe instance;
        public static BUS_GuiXe Instance
        {
            get
            {
                if (instance == null) instance = new BUS_GuiXe();
                return instance;
            }
        }

        public BUS_GuiXe() : this(new DefaultLuotVaoRaBaiXeGateway(), new DefaultTheRfidGateway(), new DefaultViDienTuGateway(),
                                  new DefaultDonHangGateway(), new DefaultChiTietDonHangGateway(), new DefaultVeDoXeChiTietGateway(),
                                  new DefaultGiaoDichViGateway(), new DefaultPhieuThuGateway(), new DefaultGiaGuiXeGateway()) { }

        public BUS_GuiXe(ILuotVaoRaBaiXeGateway luotGateway, ITheRfidGateway theRfidGateway, IViDienTuGateway viGateway,
                         IDonHangGateway donHangGateway, IChiTietDonHangGateway chiTietDonHangGateway, IVeDoXeChiTietGateway veDoXeGateway,
                         IGiaoDichViGateway giaoDichGateway, IPhieuThuGateway phieuThuGateway, IGiaGuiXeGateway giaGuiXeGateway)
        {
            _luotGateway = luotGateway;
            _theRfidGateway = theRfidGateway;
            _viGateway = viGateway;
            _donHangGateway = donHangGateway;
            _chiTietDonHangGateway = chiTietDonHangGateway;
            _veDoXeGateway = veDoXeGateway;
            _giaoDichGateway = giaoDichGateway;
            _phieuThuGateway = phieuThuGateway;
            _giaGuiXeGateway = giaGuiXeGateway;
        }

        #region Nhận Xe Vào Bãi

        /// <summary>
        /// Quy trình tiếp nhận phương tiện vào bãi đỗ xe.
        /// Bao gồm lưu trữ biển số, phân loại xe, liên kết thẻ RFID và lưu trữ hình ảnh minh chứng.
        /// </summary>
        public OperationResult NhanXe(string bienSo, string loaiXe, string maRfid = null, string anhBienSo = null)
        {
            if (string.IsNullOrWhiteSpace(bienSo))
                return OperationResult.Failed("Vui lòng nhập biển số xe.");

            // Bước 1: Kiểm tra tính hợp lệ - Ngăn chặn cấp phát phiếu lưu trú cho phương tiện đang ở trạng thái gửi trong bãi
            var dangGui = _luotGateway.TimTheoBienSo(bienSo.Trim());
            if (dangGui.Any(x => x.BienSo.Equals(bienSo.Trim(), StringComparison.OrdinalIgnoreCase) && x.TrangThai == AppConstants.TrangThaiGuiXe.DangGui))
                return OperationResult.Failed(string.Format("Xe mang biển số {0} hiện đang được lưu trữ trong bãi.", bienSo));

            try
            {
                // Bước 2: Khởi tạo hồ sơ quản lý lượt vào bãi
                var luot = new ET_LuotVaoRaBaiXe
                {
                    BienSo = bienSo.Trim().ToUpper(),
                    LoaiXe = loaiXe ?? AppConstants.LoaiXe.XeMay,
                    MaRfid = maRfid,
                    AnhBienSo = anhBienSo,
                    ThoiGianVao = DateTime.Now,
                    TrangThai = AppConstants.TrangThaiGuiXe.DangGui
                };
                int idLuot = _luotGateway.ThemVaLayId(luot);
                if (idLuot <= 0)
                    return OperationResult.Failed("Lỗi hệ thống: Không thể ghi nhận dữ liệu lượt xe vào bãi.");

                return OperationResult.Success(string.Format("Nhận xe thành công! Biển số: {0} | Loại phương tiện: {1}", bienSo.ToUpper(), GetTenLoaiXe(loaiXe)));
            }
            catch (Exception ex)
            {
                return OperationResult.Failed("Lỗi ngoại lệ khi nhận xe: " + ex.Message);
            }
        }
        #endregion

        #region Xử Lý Giao Xe Cho Khách (Trả Xe)

        /// <summary>
        /// Quy trình bàn giao phương tiện cho khách.
        /// Xử lý tạo đơn hàng thanh toán phí đỗ xe, thao tác đối soát thẻ RFID và chuyển trạng thái vé gửi.
        /// </summary>
        public OperationResult TraXe(int idLuotVaoRa, string phuongThuc, int idNhanVien = 1)
        {
            var luot = _luotGateway.LayTheoId(idLuotVaoRa);
            if (luot == null)
                return OperationResult.Failed("Lỗi dữ liệu: Không tìm thấy thông tin lượt gửi xe.");
            if (luot.TrangThai != AppConstants.TrangThaiGuiXe.DangGui)
                return OperationResult.Failed("Lỗi nghiệp vụ: Xe này đã được xuất bãi trước đó.");

            DateTime now = DateTime.Now;
            decimal tienGuiXe = TinhTienGuiXe(luot.LoaiXe, luot.ThoiGianVao, now);

            using (var ts = new System.Transactions.TransactionScope())
            {
                try
                {
                    // Bước 1: Khởi tạo Hóa đơn thanh toán dịch vụ đỗ xe trên hệ thống chung
                var dh = new ET_DonHang
                {
                    MaCode = "DX-" + DateTime.Now.ToString("yyMMddHHmmss") + Guid.NewGuid().ToString().Substring(0, 3).ToUpper(),
                    ThoiGian = now,
                    TongTien = tienGuiXe,
                    TienGiamGia = 0,
                    TrangThai = AppConstants.TrangThaiDonHang.DaThanhToan,
                    CreatedAt = now,
                    CreatedBy = idNhanVien
                };

                // Bước 2: Liên kết định danh khách hàng nếu vé được tạo bởi Thẻ RFID nội bộ
                if (!string.IsNullOrEmpty(luot.MaRfid))
                {
                    var theRfid = _theRfidGateway.LoadDS().FirstOrDefault(x => x.MaRfid == luot.MaRfid);
                    if (theRfid != null)
                    {
                        var vi = _viGateway.LayTheoId(theRfid.IdVi);
                        if (vi != null) dh.IdKhachHang = vi.IdKhachHang;
                    }
                }

                int idDonHang = _donHangGateway.ThemVaLayId(dh);
                if (idDonHang <= 0)
                    return OperationResult.Failed("Lỗi hệ thống: Không thể khởi tạo đơn hàng thanh toán.");

                // Bước 3: Ghi nhận chi tiết hóa đơn cước phí đỗ xe
                var ctdh = new ET_ChiTietDonHang
                {
                    IdDonHang = idDonHang,
                    SoLuong = 1,
                    DonGiaGoc = tienGuiXe,
                    TienGiamGiaDong = 0,
                    DonGiaThucTe = tienGuiXe
                };
                int idCtdh = _chiTietDonHangGateway.ThemVaLayId(ctdh);

                // Bước 4: Lưu trữ liên kết tra soát giữa Đơn Hàng và Lịch Sử Bãi Đỗ Xe (VeDoXeChiTiet)
                if (idCtdh > 0)
                {
                    _veDoXeGateway.Them(new ET_VeDoXeChiTiet
                    {
                        IdChiTietDonHang = idCtdh,
                        IdLuotVaoRa = idLuotVaoRa,
                        TienPhaiTra = tienGuiXe
                    });
                }

                // Bước 5: Tiến hành thủ tục đối soát thanh toán và thu ngân
                if (phuongThuc == AppConstants.PhuongThucThanhToan.ViRfid && !string.IsNullOrEmpty(luot.MaRfid))
                {
                    // Lập quy trình trừ cước thông qua số dư khả dụng lưu trên Ví Điện Tử (RFID)
                    var theRfid = _theRfidGateway.LoadDS().FirstOrDefault(x => x.MaRfid == luot.MaRfid);
                    if (theRfid != null)
                    {
                        var vi = _viGateway.LayTheoId(theRfid.IdVi);
                        if (vi != null && vi.SoDuKhaDung >= tienGuiXe)
                        {
                            vi.SoDuKhaDung -= tienGuiXe;
                            _viGateway.Sua(vi);

                            _giaoDichGateway.Them(new ET_GiaoDichVi
                            {
                                MaCode = "GD-XE-" + DateTime.Now.Ticks.ToString().Substring(10),
                                IdVi = vi.Id,
                                LoaiGiaoDich = AppConstants.LoaiGiaoDichVi.ThanhToanDichVu,
                                SoTien = tienGuiXe,
                                IdDonHangLienQuan = idDonHang,
                                ThoiGian = now,
                                CreatedAt = now,
                                CreatedBy = idNhanVien
                            });
                        }
                        else
                        {
                            return OperationResult.Failed("Số dư khả dụng trong ví RFID không đủ để chi trả cước phí. Vui lòng sử dụng phương thức thanh toán khác.");
                        }
                    }
                    else
                    {
                        return OperationResult.Failed("Thẻ RFID không tồn tại hoặc đã bị vô hiệu hóa trong hệ thống.");
                    }
                }
                else
                {
                    // Ghi nhận dòng doanh thu truyền thống cho phương thức Tiền Mặt hoặc Chuyển Khoản Ngân Hàng
                    _phieuThuGateway.Them(new ET_PhieuThu
                    {
                        MaCode = "PT-XE-" + DateTime.Now.Ticks.ToString().Substring(10),
                        IdDonHang = idDonHang,
                        SoTien = tienGuiXe,
                        PhuongThuc = phuongThuc ?? AppConstants.PhuongThucThanhToan.TienMat,
                        ThoiGian = now,
                        CreatedAt = now,
                        CreatedBy = idNhanVien
                    });
                }

                // Bước 6: Hoàn tất chuyển đổi trạng thái lượt lưu trú, đưa phương tiện xuất bãi chính thức
                luot.ThoiGianRa = now;
                luot.TrangThai = AppConstants.TrangThaiGuiXe.DaTra;
                _luotGateway.Sua(luot);

                ts.Complete(); // XÁC NHẬN GIAO DỊCH THÀNH CÔNG TẠI ĐÂY LÀ CHUẨN

                return OperationResult.Success(string.Format("Hoàn tất trả xe. Biển số: {0} | Thanh toán: {1:N0}đ", luot.BienSo, tienGuiXe));
            }
            catch (Exception ex)
            {
                return OperationResult.Failed("Lỗi ngoại lệ trong quá trình trả xe: " + ex.Message);
            }
        }
        }
        #endregion

        #region Xử Lý Phí Lưu Trú Cơ Sở

        /// <summary>
        /// Thuật toán xử lý cước phí giữ xe theo khung thời gian và phân loại phương tiện.
        /// Áp dụng cơ chế định giá theo block 12 tiếng liên tục và phụ phí lưu lượng qua đêm (22:00 - 06:00).
        /// </summary>
        public decimal TinhTienGuiXe(string loaiXe, DateTime thoiGianVao, DateTime thoiGianRa)
        {
            var gia = _giaGuiXeGateway.LayTheoLoaiXe(loaiXe);
            if (gia == null) return 10000; // Thông số an toàn mặc định để tránh gián đoạn dịch vụ
            
            double totalHours = (thoiGianRa - thoiGianVao).TotalHours;
            if (totalHours <= 0) totalHours = 0.1;
            
            // Xử lý cước cơ sở cho Block tối đa 12 giờ lưu trú khai báo đầu tiên
            decimal tongTien = gia.GiaBanNgay;

            // Mở rộng hóa đơn dựa trên số Block (12 tiếng) kế tiếp
            if (totalHours > 12)
            {
                int blocksExtra = (int)Math.Ceiling((totalHours - 12) / 12.0);
                tongTien += blocksExtra * gia.GiaBanNgay;
            }

            // Áp dụng Phụ Thu Cước Đêm nếu thời gian lưu trú vượt qua ranh giới ca trực
            int soDemLuuTru = DemSoDem(thoiGianVao, thoiGianRa);
            if (soDemLuuTru > 0)
            {
                tongTien += soDemLuuTru * gia.GiaQuaDem;
            }

            return tongTien;
        }

        /// <summary>
        /// Khảo sát hàm thời gian kiểm tra giới tuyến lưu trú qua đêm khắt khe (22:00 đến 06:00 ngày liền kề).
        /// Trả về số lượng đêm lưu trú để truy thu chính xác.
        /// </summary>
        private int DemSoDem(DateTime vao, DateTime ra)
        {
            int soDem = 0;
            // Duyệt từng ngày hiện hữu để quét sự tương tác biên
            for (DateTime t = vao.Date; t <= ra.Date; t = t.AddDays(1))
            {
                DateTime demBatDau = t.AddHours(22);
                DateTime demKetThuc = t.AddDays(1).AddHours(6);
                if (vao < demKetThuc && ra > demBatDau)
                    soDem++;
            }
            return soDem;
        }

        #region Tra Cứu Sự Kiện Lịch Sử & Thống Kê Dữ Liệu

        public List<ET_LuotVaoRaBaiXe> LoadDangGui()
        {
            return _luotGateway.LoadDangGui();
        }

        public List<ET_LuotVaoRaBaiXe> TimXeTheoBienSo(string bienSo)
        {
            if (string.IsNullOrWhiteSpace(bienSo)) return LoadDangGui();
            return _luotGateway.TimTheoBienSo(bienSo.Trim());
        }

        public ET_LuotVaoRaBaiXe LayTheoId(int id)
        {
            return _luotGateway.LayTheoId(id);
        }

        public List<ET_GiaGuiXe> LoadBangGia()
        {
            return _giaGuiXeGateway.LoadDS();
        }

        /// <summary>
        /// Báo cáo truy vấn tổng quan số lượng các loại phương tiện đang đỗ hành hành nội bộ (Chức năng dành cho Dashboard Giám sát).
        /// </summary>
        public Dictionary<string, int> DemXeTheoLoai()
        {
            return _luotGateway.DemXeDangGuiTheoLoai();
        }

        public int DemDaTraHomNay()
        {
            return _luotGateway.DemDaTraHomNay();
        }
        #endregion

        #region Tiện Ích Trợ Giúp Ký Tự (Helpers)

        public static string GetTenLoaiXe(string loaiXe)
        {
            switch (loaiXe)
            {
                case AppConstants.LoaiXe.XeDap: return "Xe đạp";
                case AppConstants.LoaiXe.XeMay: return "Xe máy";
                case AppConstants.LoaiXe.OTo: return "Ô tô";
                case AppConstants.LoaiXe.XeDien: return "Xe điện";
                default: return loaiXe ?? "Xe máy";
            }
        }
        #endregion
    }
}

#endregion

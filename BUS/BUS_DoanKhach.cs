using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace BUS
{
    public class BUS_DoanKhach : IBaseBUS<ET_DoanKhach>, IBUS_DoanKhach
    {
        private readonly IDoanKhachGateway _doanKhachGateway;
        private readonly IDoanKhachDichVuGateway _doanKhachDichVuGateway;
        private readonly IDonHangGateway _donHangGateway;
        private readonly IPhieuThuGateway _phieuThuGateway;
        private readonly IChiTietDonHangGateway _chiTietDonHangGateway;
        private readonly IPhieuChiGateway _phieuChiGateway;

        #region Khởi tạo & Singleton
        private static BUS_DoanKhach instance;
        public static BUS_DoanKhach Instance
        {
            get
            {
                if (instance == null) instance = new BUS_DoanKhach();
                return instance;
            }
        }

        public BUS_DoanKhach() : this(new DefaultDoanKhachGateway(), new DefaultDoanKhachDichVuGateway(), 
                                      new DefaultDonHangGateway(), new DefaultPhieuThuGateway(), new DefaultChiTietDonHangGateway(), new DefaultPhieuChiGateway()) { }

        public BUS_DoanKhach(IDoanKhachGateway doanKhachGateway, IDoanKhachDichVuGateway doanKhachDichVuGateway, 
                             IDonHangGateway donHangGateway, IPhieuThuGateway phieuThuGateway, IChiTietDonHangGateway chiTietDonHangGateway, IPhieuChiGateway phieuChiGateway)
        {
            _doanKhachGateway = doanKhachGateway;
            _doanKhachDichVuGateway = doanKhachDichVuGateway;
            _donHangGateway = donHangGateway;
            _phieuThuGateway = phieuThuGateway;
            _chiTietDonHangGateway = chiTietDonHangGateway;
            _phieuChiGateway = phieuChiGateway;
        }
        #endregion

        #region Quản lý thông tin chung (CRUD)
        public List<ET_DoanKhach> LoadDS()
        {
            return _doanKhachGateway.LoadDS();
        }

        public ResponseResult Them(ET_DoanKhach et)
        {
            if (string.IsNullOrWhiteSpace(et.TenDoan))
                return new ResponseResult { IsSuccess = false, ErrorMessage = "Tên đoàn không được rỗng!" };

            if (et.ChietKhau < 0 || et.ChietKhau > 100)
                return new ResponseResult { IsSuccess = false, ErrorMessage = "Chiết khấu phải từ 0 đến 100%!" };

            if (!string.IsNullOrWhiteSpace(et.DienThoaiLienHe) &&
                !System.Text.RegularExpressions.Regex.IsMatch(et.DienThoaiLienHe.Trim(), @"^\d{10,11}$"))
                return new ResponseResult { IsSuccess = false, ErrorMessage = "SĐT phải gồm 10-11 chữ số!" };

            if (string.IsNullOrWhiteSpace(et.MaBooking))
            {
                et.MaBooking = "BK-" + DateTime.Now.ToString("yyMMdd") + "-" + (new Random().Next(1000, 9999));
            }

            et.CreatedAt = DateTime.Now;
            if (_doanKhachGateway.Them(et))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi thêm đoàn khách." };
        }

        public ResponseResult Sua(ET_DoanKhach et)
        {
            if (et.Id <= 0)
                return new ResponseResult { IsSuccess = false, ErrorMessage = "ID không hợp lệ!" };

            if (string.IsNullOrWhiteSpace(et.TenDoan))
                return new ResponseResult { IsSuccess = false, ErrorMessage = "Tên đoàn không được rỗng!" };

            if (et.ChietKhau < 0 || et.ChietKhau > 100)
                return new ResponseResult { IsSuccess = false, ErrorMessage = "Chiết khấu phải từ 0 đến 100%!" };

            et.UpdatedAt = DateTime.Now;
            if (_doanKhachGateway.Sua(et))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi cập nhật đoàn khách." };
        }

        public ResponseResult Xoa(int id)
        {
            if (_doanKhachGateway.Xoa(id))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi xóa. Đoàn có thể đang có khách hàng liên kết!" };
        }

        public ET_DoanKhach GetById(int id)
        {
            return _doanKhachGateway.LayTheoId(id);
        }

        public ET_DoanKhach GetByBookingCode(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return null;
            return _doanKhachGateway.LayTheoMaBookingHoacSdt(keyword.Trim());
        }

        public OperationResult DanhDauDaXuatVe(int idDoan)
        {
            bool ok = _doanKhachGateway.DanhDauDaXuatVe(idDoan);
            return ok ? OperationResult.Success() : OperationResult.Failed("Không thể cập nhật trạng thái đoàn.");
        }

        public List<ET_DoanKhach> TimKiem(string kw, string filter)
        {
            return LoadDS();
        }
        #endregion

        #region Quản lý dịch vụ phân loại riêng theo nhóm
        public List<ET_DoanKhach_DichVu> LayDichVuTheoDoan(int idDoan)
        {
            return _doanKhachDichVuGateway.LayTheoDoan(idDoan);
        }

        public ResponseResult ThemDichVu(ET_DoanKhach_DichVu dv)
        {
            if (dv.IdDoan <= 0)
                return new ResponseResult { IsSuccess = false, ErrorMessage = "Chưa chọn đoàn!" };
            if (dv.SoLuong <= 0)
                return new ResponseResult { IsSuccess = false, ErrorMessage = "Số lượng phải > 0!" };

            dv.TrangThai = dv.TrangThai ?? AppConstants.TrangThaiDichVuDoan.ChuaXuLy;
            int newId = _doanKhachDichVuGateway.Them(dv);
            if (newId > 0)
                return new ResponseResult { IsSuccess = true, ErrorMessage = newId.ToString() };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi thêm dịch vụ." };
        }

        public ResponseResult XoaDichVu(int id)
        {
            if (_doanKhachDichVuGateway.Xoa(id))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi xóa dịch vụ." };
        }

        public ResponseResult SuaDichVu(ET_DoanKhach_DichVu dv)
        {
            if (_doanKhachDichVuGateway.Sua(dv))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi cập nhật dịch vụ." };
        }

        // Tổng hợp mức cước dịch vụ phát sinh chưa tính chiết khấu nội bộ
        public decimal TinhTongDichVu(int idDoan)
        {
            var ds = LayDichVuTheoDoan(idDoan);
            return ds.Where(x => x.TrangThai != AppConstants.TrangThaiDichVuDoan.DaHuy).Sum(x => x.ThanhTien);
        }

        // Tiến hành chốt dịch vụ nằm ở trạng thái chờ kích hoạt hoặc đợi xử lý để rà soát tổng tiền
        public OperationResult XuatHoaDon(int idDoan, string phuongThuc, int idNhanVien)
        {
            var doan = GetById(idDoan);
            if (doan == null) return OperationResult.Failed("Không tìm thấy đoàn.");

            var dichVuChuaChot = LayDichVuTheoDoan(idDoan)
                .Where(x => x.TrangThai == AppConstants.TrangThaiDichVuDoan.ChuaXuLy
                         || x.TrangThai == AppConstants.TrangThaiDichVuDoan.DaDatCho)
                .ToList();

            if (dichVuChuaChot.Count == 0)
                return OperationResult.Failed("Không có dịch vụ nào cần xuất hóa đơn.");

            decimal tongTruocCK = dichVuChuaChot.Sum(x => x.ThanhTien);
            decimal giamGia = tongTruocCK * doan.ChietKhau / 100m;
            decimal tongSauCK = tongTruocCK - giamGia;

            // Tạo đơn hàng
            var dh = new ET_DonHang
            {
                MaCode = "DH-DOAN-" + DateTime.Now.ToString("yyMMddHHmmss") + "-" + Guid.NewGuid().ToString().Substring(0, 4).ToUpper(),
                IdDoan = idDoan,
                ThoiGian = DateTime.Now,
                TongTien = tongSauCK,
                TrangThai = AppConstants.TrangThaiDonHang.DaThanhToan,
                CreatedAt = DateTime.Now,
                CreatedBy = idNhanVien
            };
            int idDonHang = _donHangGateway.ThemVaLayId(dh);
            if (idDonHang <= 0) return OperationResult.Failed("Lỗi tạo đơn hàng.");

            // Tạo phiếu thu
            var pt = new ET_PhieuThu
            {
                MaCode = "PT-DOAN-" + DateTime.Now.Ticks.ToString().Substring(10),
                IdDonHang = idDonHang,
                SoTien = tongSauCK,
                PhuongThuc = phuongThuc,
                ThoiGian = DateTime.Now,
                CreatedAt = DateTime.Now,
                CreatedBy = idNhanVien
            };
            _phieuThuGateway.Them(pt);

            // Tạo ChiTietDonHang cho từng dịch vụ + cập nhật trạng thái
            foreach (var dv in dichVuChuaChot)
            {
                var ctdh = new ET_ChiTietDonHang
                {
                    IdDonHang = idDonHang,
                    IdSanPham = dv.IdSanPham,
                    IdCombo = dv.IdCombo,
                    SoLuong = dv.SoLuong,
                    DonGiaGoc = dv.DonGia,
                    TienGiamGiaDong = dv.DonGia * doan.ChietKhau / 100m,
                    DonGiaThucTe = dv.DonGia * (1 - doan.ChietKhau / 100m)
                };
                int idCtdh = _chiTietDonHangGateway.ThemVaLayId(ctdh);

                // Cập nhật truy vết tài chính
                dv.IdChiTietDonHang = idCtdh;
                dv.TrangThai = AppConstants.TrangThaiDichVuDoan.DaThanhToan;
                _doanKhachDichVuGateway.Sua(dv);
            }

            return OperationResult.Success($"Xuất hóa đơn thành công!\nMã ĐH: {dh.MaCode}\nTổng: {tongSauCK:N0} VNĐ (đã CK {doan.ChietKhau}%)");
        }
        #endregion

        #region Trạm dịch vụ phân bổ toàn cục và Đối soát thực tế (Fulfillment)
        // Cung cấp số lượng mở lệnh dịch vụ dư thừa phân bổ hỗ trợ cho hệ thống cửa kiểm soát điện tử, máy tính tiền ăn và lễ tân
        public List<ET_DoanKhach_DichVu> LayQuotaConLai(int idDoan)
        {
            return _doanKhachDichVuGateway.LayQuotaConLai(idDoan);
        }

        // Lọc số lượng dịch vụ khả dụng bằng phương thức phân nhánh chuyên mục (Ví dụ: cổng từ lọc tìm luồng mở mã vé)
        public ET_DoanKhach_DichVu LayQuotaTheoLoai(int idDoan, string loaiDichVu)
        {
            return LayQuotaConLai(idDoan)
                .FirstOrDefault(x => x.LoaiDichVu == loaiDichVu);
        }

        // Trừ dịch vụ đã cấp phát theo thực tế sử dụng tại cơ sở phân luồng kiểm soát qua thao tác hợp lệ
        public OperationResult KhauTruQuota(int idDichVuDoan, int soLuongDung)
        {
            if (soLuongDung <= 0)
                return OperationResult.Failed("Số lượng phải > 0.");

            bool ok = _doanKhachDichVuGateway.KhauTruQuota(idDichVuDoan, soLuongDung);
            if (!ok)
                return OperationResult.Failed("Không thể khấu trừ. Có thể đã hết quota hoặc dữ liệu không hợp lệ.");
            return OperationResult.Success();
        }

        // Cốt lõi xác định quyền lợi bảo lãnh thành viên theo thời điểm biểu chuẩn hiện hành tại điểm trạm
        public OperationResult CheckBookingValid(ET_DoanKhach doan)
        {
            if (doan == null)
                return OperationResult.Failed("Không tìm thấy đoàn.");

            if (doan.TrangThai == AppConstants.TrangThaiDoanKhach.DaHuy)
                return OperationResult.Failed("Đoàn đã bị hủy.");

            if (doan.TrangThai == AppConstants.TrangThaiDoanKhach.DaHoanTat)
                return OperationResult.Failed("Đoàn đã chốt sổ, không thể phục vụ thêm.");

            if (doan.TrangThai == AppConstants.TrangThaiDoanKhach.HetHan)
                return OperationResult.Failed("Booking đã hết hạn.");

            if (doan.NgayDen.HasValue && DateTime.Today < doan.NgayDen.Value)
                return OperationResult.Failed($"Đoàn chưa đến ngày phục vụ (Ngày đến: {doan.NgayDen.Value:dd/MM/yyyy}).");

            if (doan.NgayDi.HasValue && DateTime.Today > doan.NgayDi.Value)
                return OperationResult.Failed($"Đoàn đã quá ngày phục vụ (Ngày đi: {doan.NgayDi.Value:dd/MM/yyyy}).");

            return OperationResult.Success();
        }

        /// <summary>
        /// Nghiệp vụ hoàn trả dịch vụ đã mua dư (Hỗ trợ theo dõi soát lỗi tự động)
        /// 1. Giảm trừ số lượng thực trên hạn mức đang có.
        /// 2. Ghi nhận chi tiết định lượng đảo ngược với trạng thái hủy để hệ thống khóa lưu sổ.
        /// 3. Kích hoạt phát hành phiếu thu chi ra mặt quầy giao dịch.
        /// </summary>
        public OperationResult RutBotDichVu(int idDichVuDoan, int soRut, string lyDo, int idNhanVien)
        {
            if (soRut <= 0)
                return OperationResult.Failed("Số lượng rút phải > 0.");

            // Kéo dữ liệu từ tầng căn bản nhất nhằm hạn chế tình trạng mảng dữ liệu ngầm không liên kết kịp
            var dvGoc = _doanKhachDichVuGateway.LayTheoId(idDichVuDoan);
            if (dvGoc == null)
                return OperationResult.Failed("Không tìm thấy dịch vụ gốc.");

            if (dvGoc.SoLuongConLai < soRut)
                return OperationResult.Failed($"Số lượng rút ({soRut}) vượt quá số còn lại ({dvGoc.SoLuongConLai}).");

            // Bước 1: Trừ đi cấu trúc tài sản trên nhóm gốc của dữ liệu 
            dvGoc.SoLuong -= soRut;
            if (dvGoc.SoLuong <= 0)
            {
                dvGoc.TrangThai = AppConstants.TrangThaiDichVuDoan.DaHuy;
                dvGoc.SoLuong = 0;
            }
            dvGoc.GhiChu = (dvGoc.GhiChu ?? "") +
                $" | Rút -{soRut} lúc {DateTime.Now:HH:mm}" +
                (string.IsNullOrEmpty(lyDo) ? "" : $" ({lyDo})");
            _doanKhachDichVuGateway.Sua(dvGoc);

            // Bước 2: Thiết lập thông số ảo dạng chỉ lưu (biến âm) cho bảng sao lưu chứng từ tránh sập hệ thống kiểm thử
            var dvAudit = new ET_DoanKhach_DichVu
            {
                IdDoan = dvGoc.IdDoan,
                LoaiDichVu = dvGoc.LoaiDichVu,
                IdCombo = dvGoc.IdCombo,
                IdSanPham = dvGoc.IdSanPham,
                SoLuong = -soRut,  // Số hạng nghịch đảo chuyên đối chiếu kiểm toán
                DonGia = dvGoc.DonGia,
                NgaySuDung = dvGoc.NgaySuDung,
                GhiChu = $"[HOÀN/HỦY] Rút {soRut} từ DV #{idDichVuDoan} - {lyDo} - NV:{idNhanVien} lúc {DateTime.Now:dd/MM HH:mm}",
                TrangThai = AppConstants.TrangThaiDichVuDoan.DaHuy
            };
            _doanKhachDichVuGateway.Them(dvAudit);

            // Bước 3: Đẩy phiếu thanh toán in sẵn ra màn hình theo cấu trúc phân rã bù tiền trực tiếp

            decimal tienHoan = dvGoc.DonGia * soRut;
            if (tienHoan > 0)
            {
                string tenDV = !string.IsNullOrEmpty(dvGoc.TenDichVu) ? dvGoc.TenDichVu
                             : !string.IsNullOrEmpty(dvGoc.TenCombo) ? dvGoc.TenCombo
                             : "DV #" + idDichVuDoan;

                var phieuChi = new ET.ET_PhieuChi
                {
                    MaCode = "PC-" + DateTime.Now.ToString("yyMMdd-HHmmss"),
                    SoTien = tienHoan,
                    LyDo = $"Hoàn tiền đoàn - Rút {soRut}x [{tenDV}]" +
                           (string.IsNullOrEmpty(lyDo) ? "" : $" - {lyDo}"),
                    ThoiGian = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    CreatedBy = idNhanVien
                };
                _phieuChiGateway.Them(phieuChi);
            }

            return OperationResult.Success($"Đã rút {soRut} dịch vụ. Hoàn tiền: {tienHoan:N0} VNĐ");
        }

        // Đóng băng toàn bộ hoạt động cấp quyền xuất dịch vụ mới khi kết sổ kiểm toán hoàn tất kỳ thu hồi
        public OperationResult ChotDoan(int idDoan)
        {
            var doan = GetById(idDoan);
            if (doan == null) return OperationResult.Failed("Không tìm thấy đoàn.");

            doan.TrangThai = AppConstants.TrangThaiDoanKhach.DaHoanTat;
            doan.UpdatedAt = DateTime.Now;
            if (_doanKhachGateway.Sua(doan))
                return OperationResult.Success("Đã chốt đoàn thành công.");
            return OperationResult.Failed("Lỗi khi chốt đoàn.");
        }
        #endregion
    }
}


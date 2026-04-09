using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using ET;

namespace BUS
{
    public class BUS_VeDienTu
    {
        private static BUS_VeDienTu instance;
        public static BUS_VeDienTu Instance
        {
            get
            {
                if (instance == null) instance = new BUS_VeDienTu();
                return instance;
            }
        }

        #region Truy xuất Dữ liệu Cơ bản

        public List<ET_VeDienTu> LoadDS()
        {
            return DAL_VeDienTu.Instance.LoadDS();
        }

        public ET_VeDienTu LayTheoId(Guid id)
        {
            return DAL_VeDienTu.Instance.LayTheoId(id);
        }
        #endregion

        #region Quy Trình Soát Vé Bằng Mã Nhận Diện (Barcode/QR/RFID)

        /// <summary>
        /// Quy trình Soát vé (Gate Keeper) xác thực tại các cổng kiểm soát.
        /// Ưu tiên tốc độ kiểm tra cao nhất nhưng vẫn đảm bảo chặt chẽ các điều kiện khu vực, trò chơi và thời hạn.
        /// Trả về mã kết quả:
        /// 0: Thành công (Vé hợp lệ, đã trừ lượt thành công)
        /// 1: Sai vị trí (Hành khách đang dùng vé của khu vực khác)
        /// 2: Khước từ (Vé đã hết hạn sử dụng hoặc hết sạch số lượt)
        /// 3: Đặc tả trống (Không tìm thấy mã vé trong hệ thống)
        /// 4: Nhầm trò chơi (Đúng phân khu nhưng sai loại máy trò chơi)
        /// </summary>
        public int CheckTicket(string maCode, int? idKhuVucTramGac, out ET_VeDienTu veInfo, int? idThietBi = null)
        {
            veInfo = null;

            // Giai đoạn 1: Phân luồng ưu tiên xử lý cho Khách Đoàn (Dự án bán sỉ)
            // Khách đoàn sử dụng mã Booking tổng để qua cửa thông qua hệ thống Quota
            if (!string.IsNullOrEmpty(maCode) && maCode.StartsWith("BK-", StringComparison.OrdinalIgnoreCase))
            {
                _lastBookingResult = CheckBookingQuota(maCode);
                return _lastBookingResult.ResultCode;
            }

            // Giai đoạn 2: Xử lý định danh Vé Lẻ cá nhân
            try
            {
                veInfo = DAL_VeDienTu.Instance.LayTheoMaCode(maCode); 
                if (veInfo == null) return 3;

                // Bước 1: Khảo sát trạng thái cờ logic (Ví dụ: Bị nhân viên tổng đài hủy)
                if (veInfo.TrangThai == "DaHuy" || veInfo.TrangThai == "HetHan") return 2;

                // Bước 2: Truy vết tuổi thọ của Vé Dịch Vụ
                // Bắt buộc đối soát với thời gian xuất hóa đơn gốc để triệt tiêu việc tái sử dụng vé cũ ngày hôm trước.
                var ctdh = DAL_ChiTietDonHang.Instance.LayTheoId(veInfo.IdChiTietDonHang);
                if (ctdh != null)
                {
                    var donHang = DAL_DonHang.Instance.LayTheoId(ctdh.IdDonHang);
                    if (donHang != null && donHang.ThoiGian.Date < DateTime.Today)
                    {
                        veInfo.TrangThai = AppConstants.TrangThaiVeDienTu.HetHan;
                        DAL_VeDienTu.Instance.Sua(veInfo);
                        return 2;
                    }
                }

                if (idKhuVucTramGac != null && veInfo.IdSanPham.HasValue)
                {
                    var sanPham = DAL_SanPham.Instance.LayTheoId(veInfo.IdSanPham.Value);
                    if (sanPham != null && sanPham.IdKhuVuc != idKhuVucTramGac)
                    {
                        return 1;
                    }

                    // Bước 4: Đối chiếu chéo thiết bị trò chơi cụ thể đối với loại vé chơi trò chơi cá biệt
                    // Một máy chơi trò chơi có thể dùng chung cấu hình cho nhiều vé khác nhau (Người lớn/Trẻ em)
                    if (idThietBi.HasValue)
                    {
                        var spVe = DAL_SanPham_Ve.Instance.LayTheoIdSanPham(veInfo.IdSanPham.Value);
                        if (spVe == null || spVe.IdThietBi != idThietBi.Value)
                        {
                            return 4;
                        }
                    }
                }

                if (veInfo.SoLuotConLai <= 0) 
                {
                    if (veInfo.TrangThai != AppConstants.TrangThaiVeDienTu.DaSuDung)
                    {
                        veInfo.TrangThai = AppConstants.TrangThaiVeDienTu.DaSuDung;
                        DAL_VeDienTu.Instance.Sua(veInfo);
                    }
                    return 2;
                }

                veInfo.SoLuotConLai--;
                veInfo.ThoiGianQuet = DateTime.Now;
                
                if (veInfo.SoLuotConLai <= 0)
                    veInfo.TrangThai = AppConstants.TrangThaiVeDienTu.DaSuDung;
                else
                    veInfo.TrangThai = AppConstants.TrangThaiVeDienTu.DangSuDung; 

                if (DAL_VeDienTu.Instance.Sua(veInfo))
                    return 0;
                return 3;
            }
            catch { return 3; }
        }

        #endregion

        #region Kế toán Lượt Khách Đoàn (Booking Quota)

        private BookingCheckResult _lastBookingResult;
        public BookingCheckResult LastBookingResult => _lastBookingResult;

        /// <summary>
        /// Phân tích và khấu trừ hạn mức (Quota) của khách đoàn khi đi qua cổng quét.
        /// Trả về mã phản hồi: 0 = Giao dịch thành công, 5 = Đoàn đã sử dụng hết hạn mức, 6 = Hồ sơ đoàn sai lệch, 3 = Mã đoàn phi chuẩn.
        /// </summary>
        private BookingCheckResult CheckBookingQuota(string maBooking)
        {
            var doan = BUS_DoanKhach.Instance.GetByBookingCode(maBooking);
            if (doan == null)
                return new BookingCheckResult(3, "Không tìm thấy đoàn.", null, null);

            var checkValid = BUS_DoanKhach.Instance.CheckBookingValid(doan);
            if (!checkValid.IsSuccess)
                return new BookingCheckResult(6, checkValid.ErrorMessage, doan, null);

            // Ưu tiên tìm quota loại "Ve" trước
            var quotaVe = BUS_DoanKhach.Instance.LayQuotaTheoLoai(doan.Id, AppConstants.LoaiDichVuDoan.Ve);

            // Fallback: Combo bao gồm cả vé vào cổng + ăn uống + lưu trú
            if (quotaVe == null)
                quotaVe = BUS_DoanKhach.Instance.LayQuotaTheoLoai(doan.Id, AppConstants.LoaiDichVuDoan.Combo);

            if (quotaVe == null)
                return new BookingCheckResult(5, "Đoàn không có dịch vụ vé hoặc Combo, hoặc đã hết lượt.", doan, null);

            var kqTru = BUS_DoanKhach.Instance.KhauTruQuota(quotaVe.Id, 1);
            if (!kqTru.IsSuccess)
                return new BookingCheckResult(5, $"Hết quota vé! ({quotaVe.SoLuongDaDung}/{quotaVe.SoLuong})", doan, quotaVe);

            // Reload sau khi trừ
            quotaVe.SoLuongDaDung++;
            return new BookingCheckResult(0,
                $"ĐOÀN: {doan.TenDoan}\nLượt {quotaVe.SoLuongDaDung}/{quotaVe.SoLuong}",
                doan, quotaVe);
        }

        #endregion

        #region Sản Xuất & Liên Kết Vé

        /// <summary>
        /// Khởi tạo và cấp phép phát hành một Vé Điện Tử mới gắn với Đơn Hàng.
        /// Quy trình này bảo toàn thuộc tính Giao dịch (Transaction) từ bên ngoài để đảm bảo không lọt vé giả nếu giao dịch lỗi.
        /// </summary>
        public bool GenerateTicket(int idChiTietDonHang, int soLuotQuyDoi, int? idSanPham = null, int? createdBy = null)
        {
            if (!idSanPham.HasValue) 
                throw new ArgumentException("IdSanPham không được bỏ trống.");

            ET_VeDienTu ve = new ET_VeDienTu
            {
                Id = Guid.NewGuid(),
                MaCode = "TK" + DateTime.Now.ToString("yyMMdd") + Guid.NewGuid().ToString().Substring(0, 5).ToUpper(),
                IdChiTietDonHang = idChiTietDonHang,
                IdSanPham = idSanPham,       // Denormalization: O(1) gate check
                SoLuotConLai = soLuotQuyDoi,
                TrangThai = AppConstants.TrangThaiVeDienTu.ChuaSuDung,
                CreatedAt = DateTime.Now,    // Audit trail
                CreatedBy = createdBy        // Audit trail
            };

            return DAL_VeDienTu.Instance.Them(ve);
            // Nếu INSERT fail -> return false -> BUS_DonHang ROLLBACK toàn bộ Transaction
        }

        /// <summary>
        /// Soạn và thu thập danh sách Vé thuộc về một hóa đơn cụ thể.
        /// Sử dụng kỹ thuật truy xuất theo cụm (Batch Fetching) để tối ưu thời gian tốc độ truy vấn lên giao diện.
        /// </summary>
        public List<TicketDisplayItem> LayVeTheoDonHang(int idDonHang)
        {
            // Query 1: Lấy ChiTietDonHang của đơn hàng này (targeted WHERE, not full table scan)
            var chiTietList = DAL_ChiTietDonHang.Instance.LoadByDonHang(idDonHang);
            if (chiTietList.Count == 0) return new List<TicketDisplayItem>();

            var chiTietIds = chiTietList.Select(x => x.Id).ToHashSet();

            // Query 2: Lấy tất cả VeDienTu có IdChiTietDonHang thuộc đơn này
            var allVe = DAL_VeDienTu.Instance.LoadDS()
                .Where(v => chiTietIds.Contains(v.IdChiTietDonHang)).ToList();
            if (allVe.Count == 0) return new List<TicketDisplayItem>();

            // Query 3: Pre-fetch tên sản phẩm (Dictionary O(1) lookup)
            var spIds = allVe.Where(v => v.IdSanPham.HasValue).Select(v => v.IdSanPham.Value).Distinct().ToList();
            var spDict = new Dictionary<int, string>();
            foreach (var id in spIds)
            {
                var sp = DAL_SanPham.Instance.LayTheoId(id);
                if (sp != null) spDict[id] = sp.Ten;
            }

            // Build display list
            return allVe.Select(v => new TicketDisplayItem
            {
                IdVe = v.Id,
                MaCode = v.MaCode,
                TenDichVu = v.IdSanPham.HasValue && spDict.ContainsKey(v.IdSanPham.Value)
                    ? spDict[v.IdSanPham.Value]
                    : "Dịch vụ #" + v.IdChiTietDonHang,
                SoLuotConLai = v.SoLuotConLai,
                TrangThai = v.TrangThai,
                IdSanPham = v.IdSanPham
            }).ToList();
        }
        #endregion
    }

    /// <summary>
    /// Đối tượng Dữ liệu Tùy biến (ViewModel) để trung chuyển thông tin Vé lên giao diện máy quét thiết bị.
    /// </summary>
    public class TicketDisplayItem
    {
        public Guid IdVe { get; set; }
        public string MaCode { get; set; }
        public string TenDichVu { get; set; }
        public int SoLuotConLai { get; set; }
        public string TrangThai { get; set; }
        public int? IdSanPham { get; set; }
    }

    /// <summary>
    /// Kết quả kiểm tra vé đoàn tại Gate — chứa thông tin hiển thị cho UI
    /// </summary>
    public class BookingCheckResult
    {
        public int ResultCode { get; set; }      // 0=OK, 5=hết quota, 6=booking invalid, 3=not found
        public string Message { get; set; }
        public ET_DoanKhach Doan { get; set; }
        public ET_DoanKhach_DichVu QuotaVe { get; set; }

        public BookingCheckResult(int code, string msg, ET_DoanKhach doan, ET_DoanKhach_DichVu quota)
        {
            ResultCode = code;
            Message = msg;
            Doan = doan;
            QuotaVe = quota;
        }
    }
}

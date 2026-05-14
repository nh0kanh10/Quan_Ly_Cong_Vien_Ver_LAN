using System;
using System.Linq;
using DAL.Repositories.BanHang;
using ET.Constants;
using ET.DTOs;
using ET.Models.BanHang;
using ET.Results;

namespace BUS.Services.BanHang
{
    public class BUS_POS
    {
        #region Khởi tạo (hoặc cấu trúc Singleton)
        
        public static BUS_POS Instance { get; } = new BUS_POS();

        #endregion

        #region Truy vấn dữ liệu

        /// Lấy danh sách sản phẩm và bảng giá đang được phép bán trên POS
        public OperationResult LayDanhSachSanPhamPOS(int? idDiemBan, string langCode = "vi-VN")
        {
            try
            {
                var data = DAL_POS.Instance.LayDanhSachSanPhamPOS(idDiemBan, langCode);
                return OperationResult.Ok(data);
            }
            catch (Exception ex)
            {
                return OperationResult.Fail(ex.Message);
            }
        }

        /// Quẹt thẻ RFID để lấy thông tin khách hàng và id ví
        public OperationResult TraCuuTheRFID(string maThe)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(maThe))
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_POS_RFID_EMPTY);

                var data = DAL_POS.Instance.TraCuuTheRFID(maThe);
                if (data == null)
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_POS_RFID_NOT_FOUND_OR_INACTIVE);

                return OperationResult.Ok(data);
            }
            catch (Exception ex)
            {
                return OperationResult.Fail(ex.Message);
            }
        }

        // Lấy danh sách đơn vị bán theo sản phẩm (Lon, Thùng, Lốc...).
        public System.Collections.Generic.List<ET_DonViBanPOS> LayDonViBanTheoSanPham(int idSanPham, decimal donGiaGoc, string langCode = "vi-VN")
        {
            try
            {
                return DAL_POS.Instance.LayDonViBanTheoSanPham(idSanPham, donGiaGoc, langCode)
                    ?? new System.Collections.Generic.List<ET_DonViBanPOS>();
            }
            catch
            {
                return new System.Collections.Generic.List<ET_DonViBanPOS>();
            }
        }

        // Lấy tồn kho hiện tại của 1 sản phẩm tại 1 kho .
        public decimal GetTonKhoHienTai(int idKho, int idSanPham)
        {
            return DAL.Repositories.Kho.DAL_SoCai.Instance.GetTonKhoHienTai(idKho, idSanPham);
        }

        public OperationResult LayDanhSachDonHangTheoPhien(int idPhien)
        {
            try
            {
                var data = DAL_POS.Instance.LayDanhSachDonHangTheoPhien(idPhien);
                return OperationResult.Ok(data);
            }
            catch (Exception ex)
            {
                return OperationResult.Fail(ex.Message);
            }
        }

        #endregion

        #region Combo POS

        public OperationResult LayDanhSachComboPOS()
        {
            try
            {
                var data = DAL_POS.Instance.LayDanhSachComboPOS();
                return OperationResult.Ok(data);
            }
            catch (Exception ex)
            {
                return OperationResult.Fail(ex.Message);
            }
        }

        public OperationResult LayChiTietComboPOS(int idCombo)
        {
            try
            {
                var data = DAL_POS.Instance.LayChiTietComboPOS(idCombo);
                return OperationResult.Ok(data);
            }
            catch (Exception ex)
            {
                return OperationResult.Fail(ex.Message);
            }
        }

        #endregion

        #region Nghiệp vụ đặc thù

        /// <summary>
        /// Sinh mã đơn hàng duy nhất cho POS.
        /// Dùng timestamp + 6 ký tự GUID để đảm bảo không trùng dù nhiều máy POS gọi cùng millisecond.
        /// GUI phải gọi hàm này thay vì tự sinh mã để tránh vi phạm ràng buộc UNIQUE của DB.
        /// </summary>
        public string SinhMaDonHang()
        {
            string suffix = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();
            return $"DH{DateTime.Now:yyMMddHHmmssfff}-{suffix}";
        }

        /// <summary>
        /// Xử lý nguyên luồng nghiệp vụ thanh toán Đơn hàng POS
        /// </summary>
        /// <param name="cart">Dữ liệu Session giỏ hàng hiện tại</param>
        /// <param name="idKhoXuatBan">Kho gắn với điểm bán để trừ tồn kho nếu có vật tư</param>
        public OperationResult ThanhToanDonHang(ET_CartSession cart, int idKhoXuatBan)
        {
            try
            {
                // 1. Kiểm tra giỏ hàng rỗng
                if (cart.DanhSachDong == null || !cart.DanhSachDong.Any())
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_POS_CART_EMPTY);

                // 2. Kiểm tra bắt buộc phải có phiên thu ngân đang mở
                if (cart.IdPhienThuNgan <= 0)
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_POS_NO_OPEN_SESSION);

                // 3. Kiểm tra số tiền khách đưa đã đủ thanh toán chưa
                decimal tongDaTra = cart.DanhSachThanhToan?.Sum(p => p.SoTien) ?? 0;
                if (tongDaTra < cart.TongThanhToan)
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_POS_PAYMENT_INSUFFICIENT);

                // 4. Kiểm tra số dư ví RFID (nếu có thanh toán bằng vòng tay)
                if (cart.DanhSachThanhToan != null)
                {
                    foreach (var pay in cart.DanhSachThanhToan.Where(p => p.PhuongThuc == AppConstants.PhuongThucTT.ViRFID))
                    {
                        if (pay.IdViDienTu == null || pay.IdViDienTu.Value <= 0)
                            return OperationResult.Fail(AppConstants.ErrorMessages.ERR_POS_RFID_PAYMENT_NO_ID);

                        decimal soDuHienTai = DAL_POS.Instance.LaySoDuVi(pay.IdViDienTu.Value);
                        if (soDuHienTai < pay.SoTien)
                            return OperationResult.Fail(AppConstants.ErrorMessages.ERR_POS_RFID_INSUFFICIENT_BALANCE);
                    }
                    
                    // 4.5 Kiểm tra số dư Điểm Tích Lũy 
                    if (cart.IdKhachHang.HasValue)
                    {
                        var payDiem = cart.DanhSachThanhToan.FirstOrDefault(p => p.PhuongThuc == AppConstants.PhuongThucTT.DiemTichLuy);
                        if (payDiem != null && payDiem.DiemQuyDoi.HasValue && payDiem.DiemQuyDoi.Value > 0)
                        {
                            var kh = DAL.Repositories.DoiTac.DAL_KhachHang.Instance.LayThongTinKhachHang(cart.IdKhachHang.Value);
                            if (kh == null || kh.DiemTichLuy < payDiem.DiemQuyDoi.Value)
                            {
                                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_POS_INSUFFICIENT_POINTS); 
                            }
                        }
                    }
                }

                decimal tongTienDuocGiam = 0m;

                // Tính tổng tiền các món được phép giảm giá (trừ F&B) trực tiếp từ cart
                foreach (var item in cart.DanhSachDong)
                {
                    // Nếu DonGiaGoc chưa được set , dùng DonGiaThucTe làm fallback
                    if (item.DonGiaGoc == 0 && item.HeSoQuyDoi == 1m)
                        item.DonGiaGoc = item.DonGiaThucTe;

                    if (ProductTypeHelper.IsDiscountable(item.LoaiSanPham))
                        tongTienDuocGiam += item.DonGiaGoc * item.SoLuong;
                }

                // 4.6 Kiểm tra tồn kho vật tư trước khi chốt giao dịch
                if (idKhoXuatBan > 0)
                {
                    foreach (var item in cart.DanhSachDong.Where(x => x.LaVatTu))
                    {
                        decimal soLuongCanXuat = item.SoLuong * item.HeSoQuyDoi;
                        decimal tonHienTai = GetTonKhoHienTai(idKhoXuatBan, item.IdSanPham);
                        if (tonHienTai < soLuongCanXuat)
                            return OperationResult.Fail(
                                $"{AppConstants.ErrorMessages.ERR_POS_OUT_OF_STOCK}|{item.TenSanPham}");
                    }
                }

                // Rải tiền giảm giá
                if (cart.TongGiamGia > 0 && tongTienDuocGiam > 0)
                {
                    decimal tienDaRai = 0m;
                    var dsDuocGiam = cart.DanhSachDong.Where(x => ProductTypeHelper.IsDiscountable(x.LoaiSanPham)).ToList();
                    
                    for (int i = 0; i < dsDuocGiam.Count; i++)
                    {
                        var item = dsDuocGiam[i];
                        decimal giamDong = 0;
                        
                        if (i == dsDuocGiam.Count - 1)
                        {
                            giamDong = cart.TongGiamGia - tienDaRai;
                        }
                        else
                        {
                            giamDong = Math.Round(cart.TongGiamGia * ((item.DonGiaGoc * item.SoLuong) / tongTienDuocGiam), 0);
                            tienDaRai += giamDong;
                        }
                        
                        item.TienGiamGiaDong = giamDong;
                        if (item.SoLuong > 0)
                        {
                            item.DonGiaThucTe = item.DonGiaGoc - (giamDong / item.SoLuong);
                        }
                    }
                }

                // 5. Gọi DAL xuống chốt giao dịch 
                int idDonHang = DAL_POS.Instance.Checkout(cart, idKhoXuatBan);

                if (idDonHang > 0)
                {
                    // 6. Ghi lịch sử Khuyến mãi (nếu có)
                    if (cart.DanhSachKMApDung != null && cart.DanhSachKMApDung.Any())
                    {
                        foreach (var km in cart.DanhSachKMApDung)
                        {
                            DAL.Repositories.BanHang.DAL_KhuyenMai.Instance.GhiLichSu(
                                km.Id, idDonHang, cart.IdKhachHang, km.SoTienGiamThucTe);
                        }
                    }

                    // 7. Lấy mã vạch vé điện tử nếu có
                    var maVachVe = DAL_POS.Instance.LayDanhSachMaVachVe(idDonHang);

                    var resultDto = new DTO_CheckoutResult
                    {
                        ThanhCong = true,
                        IdDonHang = idDonHang,
                        MaDonHang = cart.MaDonHang,
                        TienThua = tongDaTra - cart.TongThanhToan,
                        TongTienHang = cart.TongTienHang,
                        SoTienGiam = cart.SoTienGiamKM,
                        TongThanhToan = cart.TongThanhToan,
                        TienThanhToanBangDiem = cart.DanhSachThanhToan?.Where(p => p.PhuongThuc == AppConstants.PhuongThucTT.DiemTichLuy).Sum(p => p.SoTien) ?? 0,
                        DanhSachMaVachVe = maVachVe,
                        DanhSachMon = cart.DanhSachDong.Select(x => new DTO_CheckoutItem 
                        {
                            TenMon = x.TenSanPham,
                            SoLuong = x.SoLuong,
                            DonGiaGoc = x.DonGiaGoc,
                            TienGiamGiaDong = x.TienGiamGiaDong,
                            DonGiaThucTe = x.DonGiaThucTe,
                            ThanhTien = x.ThanhTien
                        }).ToList(),
                        Message = "MSG_POS_CHECKOUT_SUCCESS"
                    };

                    return OperationResult.Ok(resultDto, "MSG_POS_CHECKOUT_SUCCESS");
                }

                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_POS_CHECKOUT_FAILED_UNKNOWN);
            }
            catch (Exception ex)
            {
                return OperationResult.Fail(ex.Message);
            }
        }

        #endregion

        #region Gộp giảm giá (Chiết khấu hạng + KM + Điểm)

        /// <summary>
        /// Gộp 3 nguồn giảm giá -> lấy MAX (không cộng dồn mặc định).
        /// Gọi bởi ucPOS mỗi khi giỏ hàng hoặc KH thay đổi.
        /// </summary>
        /// <param name="hangThanhVien">Mã hạng KH (null = khách vãng lai)</param>
        /// <param name="tongTienDuocGiam">Tổng tiền các dòng KHÔNG phải F&B</param>
        /// <param name="kmManual">KM đang áp (null = chưa quét mã)</param>
        public DTO_KetQuaGiamGia TinhTongGiamGia(string hangThanhVien, decimal tongTienDuocGiam, decimal tongTienHang, DTO_KhuyenMaiPOS kmManual)
        {
            var kq = new DTO_KetQuaGiamGia();

            // Nguồn 1: Chiết khấu hạng (BUS_ChietKhau)
            decimal phanTram;
            kq.ChietKhauHang = BUS_ChietKhau.Instance.TinhChietKhau(hangThanhVien, tongTienDuocGiam, out phanTram);
            kq.PhanTramCKHang = phanTram;

            // Nguồn 2: Khuyến mãi
            var toHopKM = new System.Collections.Generic.List<DTO_KhuyenMaiPOS>();

            if (kmManual != null)
            {
                // Nếu khách nhập mã manual, chỉ dùng mã manual
                kmManual.SoTienGiamThucTe = BUS_KhuyenMai.Instance.TinhGiaTriGiam(kmManual, tongTienDuocGiam);
                toHopKM.Add(kmManual);
            }
            else
            {
                // Tự động tìm Best Deal
                toHopKM = BUS_KhuyenMai.Instance.TimToHopBestDeal(tongTienHang, hangThanhVien);
                // Giới hạn giảm giá của từng KM dựa theo tongTienDuocGiam
                foreach (var km in toHopKM)
                {
                    km.SoTienGiamThucTe = BUS_KhuyenMai.Instance.TinhGiaTriGiam(km, tongTienDuocGiam);
                }
            }

            kq.GiamKhuyenMai = toHopKM.Sum(k => k.SoTienGiamThucTe);
            kq.DanhSachKMApDung = toHopKM;
            
            bool coExclusive = toHopKM.Any(k => !k.CoChongCheo);
            
            if (coExclusive)
            {
                // Exclusive sẽ cạnh tranh với CK Hạng. 
                // Stackable được cộng dồn.
                decimal giamStackable = toHopKM.Where(k => k.CoChongCheo).Sum(k => k.SoTienGiamThucTe);
                var exclusive = toHopKM.FirstOrDefault(k => !k.CoChongCheo);
                decimal giamExclusive = exclusive != null ? exclusive.SoTienGiamThucTe : 0;

                if (kq.ChietKhauHang >= giamExclusive)
                {
                    kq.TongGiamGia = kq.ChietKhauHang + giamStackable;
                    kq.MoTa = kq.ChietKhauHang > 0 ? $"CK hạng {phanTram}% + {toHopKM.Count(k => k.CoChongCheo)} KM" : "";
                    
                    // Xóa thằng Exclusive ra khỏi danh sách áp dụng vì CK Hạng ngon hơn
                    if (exclusive != null) kq.DanhSachKMApDung.Remove(exclusive);
                }
                else
                {
                    kq.TongGiamGia = giamExclusive + giamStackable;
                    kq.MoTa = $"Áp dụng {kq.DanhSachKMApDung.Count} KM";
                    kq.ChietKhauHang = 0; // Hủy CK Hạng
                }
            }
            else
            {
                // Toàn là Stackable -> Cộng dồn hết
                kq.TongGiamGia = kq.ChietKhauHang + kq.GiamKhuyenMai;
                kq.MoTa = kq.ChietKhauHang > 0 ? $"CK hạng {phanTram}% + {kq.DanhSachKMApDung.Count} KM" : (kq.DanhSachKMApDung.Count > 0 ? $"Áp dụng {kq.DanhSachKMApDung.Count} KM" : "");
            }

            return kq;
        }

        #endregion
    }
}



using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ET;

namespace BUS
{
    public class BUS_ThueDo
    {
        private readonly IThueDoChiTietGateway _thueDoGateway;
        private readonly IDonHangGateway _donHangGateway;
        private readonly IViDienTuGateway _viGateway;
        private readonly IGiaoDichViGateway _giaoDichGateway;
        private readonly IPhieuThuGateway _phieuThuGateway;
        private readonly IPhieuChiGateway _phieuChiGateway;
        private readonly IChiTietDonHangGateway _ctdhGateway;

        private static BUS_ThueDo instance;
        public static BUS_ThueDo Instance => instance ?? (instance = new BUS_ThueDo());

        public BUS_ThueDo() : this(new DefaultThueDoChiTietGateway(), new DefaultDonHangGateway(), new DefaultViDienTuGateway(),
                                   new DefaultGiaoDichViGateway(), new DefaultPhieuThuGateway(), new DefaultPhieuChiGateway(), new DefaultChiTietDonHangGateway()) { }

        public BUS_ThueDo(IThueDoChiTietGateway tdGw, IDonHangGateway dhGw, IViDienTuGateway viGw,
                          IGiaoDichViGateway gdGw, IPhieuThuGateway ptGw, IPhieuChiGateway pcGw, IChiTietDonHangGateway ctdhGw)
        {
            _thueDoGateway = tdGw;
            _donHangGateway = dhGw;
            _viGateway = viGw;
            _giaoDichGateway = gdGw;
            _phieuThuGateway = ptGw;
            _phieuChiGateway = pcGw;
            _ctdhGateway = ctdhGw;
        }

        public List<ET_ThueDoChiTiet> LoadDS() => _thueDoGateway.LoadDS();

        public OperationResult RentMultipleItems(ET_DonHang dh, List<ET.RentalCartItem> cart, string phuongThuc, int idNhanVien)
        {
            if (cart == null || cart.Count == 0) return OperationResult.Failed("Giỏ hàng trống.");
            
            decimal tongThue = cart.Sum(x => x.TongThue);
            decimal tongCoc = cart.Sum(x => x.TongCoc);
            decimal tongCong = tongThue + tongCoc;

            try
            {
                using (var ts = new System.Transactions.TransactionScope())
                {
                    int idDonHang = _donHangGateway.ThemVaLayId(dh);
                    if (idDonHang <= 0) throw new Exception("Không thể tạo đơn hàng gốc.");

                    int? idGiaoDichCocChung = null;

                    // 1. THANH TOÁN (Trừ tiền 1 lần duy nhất cho toàn bộ giỏ)
                    if (phuongThuc == AppConstants.PhuongThucThanhToan.ViRfid && dh.IdKhachHang != null)
                    {
                        var vi = _viGateway.LayTheoKhachHang(dh.IdKhachHang.Value);
                        if (vi == null || vi.SoDuKhaDung < tongCong) throw new Exception("Số dư ví không đủ.");

                        vi.SoDuKhaDung -= tongThue; 
                        vi.SoDuKhaDung -= tongCoc;  
                        vi.SoDuDongBang += tongCoc;
                        
                        if (!_viGateway.Sua(vi)) throw new Exception("Lỗi cập nhật số dư ví.");

                        if (tongThue > 0)
                        {
                            var gdThue = new ET_GiaoDichVi { MaCode = "GD-RENT-" + DateTime.Now.Ticks.ToString().Substring(10), IdVi = vi.Id, LoaiGiaoDich = AppConstants.LoaiGiaoDichVi.ThanhToanDichVu, SoTien = tongThue, IdDonHangLienQuan = idDonHang, ThoiGian = DateTime.Now, CreatedAt = DateTime.Now, CreatedBy = idNhanVien };
                            if (_giaoDichGateway.ThemVaLayId(gdThue) <= 0) throw new Exception("Lỗi ghi log thanh toán.");
                        }
                        
                        if (tongCoc > 0)
                        {
                            var gdCoc = new ET_GiaoDichVi { MaCode = "GD-DEP-" + DateTime.Now.Ticks.ToString().Substring(10), IdVi = vi.Id, LoaiGiaoDich = AppConstants.LoaiGiaoDichVi.ThuCoc, SoTien = tongCoc, IdDonHangLienQuan = idDonHang, ThoiGian = DateTime.Now, CreatedAt = DateTime.Now, CreatedBy = idNhanVien };
                            idGiaoDichCocChung = _giaoDichGateway.ThemVaLayId(gdCoc);
                            if (idGiaoDichCocChung <= 0) throw new Exception("Lỗi ghi log cọc.");
                        }
                    }
                    else // Tiền mặt
                    {
                        var pt = new ET_PhieuThu { MaCode = "PT-RENT-" + DateTime.Now.Ticks.ToString().Substring(10), IdDonHang = idDonHang, SoTien = tongCong, PhuongThuc = phuongThuc, ThoiGian = DateTime.Now, CreatedAt = DateTime.Now, CreatedBy = idNhanVien };
                        if (!_phieuThuGateway.Them(pt)) throw new Exception("Không tạo được Phiếu Thu.");
                    }

                    // 2. GHI NHẬN CHI TIẾT
                    foreach (var item in cart)
                    {
                        for (int i = 0; i < item.SoLuong; i++)
                        {
                            decimal tienThue1Mon = item.TongThue / item.SoLuong;
                            decimal tienCoc1Mon = item.TongCoc / item.SoLuong;

                            // [VÁ BUG UNIVERSAL LINE ITEM]: Bắt buộc tạo ChiTietDonHang trước!
                            var ctdh = new ET_ChiTietDonHang
                            {
                                IdDonHang = idDonHang,
                                IdSanPham = item.IdSanPham, // Thuê đồ vẫn cần link về Sản phẩm danh mục gốc
                                SoLuong = 1,
                                DonGiaGoc = tienThue1Mon,
                                TienGiamGiaDong = 0,
                                DonGiaThucTe = tienThue1Mon
                            };
                            int idCtdh = _ctdhGateway.ThemVaLayId(ctdh);
                            if (idCtdh <= 0) throw new Exception("Tạo Line Item (Chi Tiết CTDH) thất bại.");

                            var td = new ET_ThueDoChiTiet
                            {
                                IdChiTietDonHang = idCtdh, // Gắn ID vừa tạo
                                IdSanPham = item.IdSanPham,
                                SoLuong = 1,
                                ThoiGianBatDau = DateTime.Now, 
                                SoTienCoc = tienCoc1Mon, 
                                TrangThaiCoc = "ChuaHoan",
                                IdGiaoDichCoc = idGiaoDichCocChung,
                                TienThueDaThu = tienThue1Mon
                            };
                            if (!_thueDoGateway.Them(td)) throw new Exception("Lưu chi tiết thuê thất bại.");
                        }
                    }

                    ts.Complete();
                    return OperationResult.Success();
                }
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message); 
            }
        }

        public OperationResult ReturnItem(int idThueDo, bool coPhat, decimal tienPhat, int idNhanVien)
        {
            try
            {
                using (var ts = new System.Transactions.TransactionScope())
                {
                    var td = _thueDoGateway.LayTheoId(idThueDo);
                    if (td == null) return OperationResult.Failed("Không tìm thấy dữ liệu thuê đồ.");
                    if (td.TrangThaiCoc != AppConstants.TrangThaiCoc.ChuaHoan) return OperationResult.Failed("Món này đã được hoàn cọc rồi.");

                    td.ThoiGianKetThuc = DateTime.Now;
                    decimal tienHoanVeVi = coPhat ? Math.Max(0, td.SoTienCoc - tienPhat) : td.SoTienCoc;
                    decimal tienPhatVuotCoc = coPhat ? Math.Max(0, tienPhat - td.SoTienCoc) : 0;

                    if (td.IdGiaoDichCoc != null) // RFID
                    {
                        var gdCoc = _giaoDichGateway.LayTheoId(td.IdGiaoDichCoc.Value);
                        var vi = _viGateway.LayTheoId(gdCoc.IdVi);

                        vi.SoDuDongBang -= td.SoTienCoc;
                        vi.SoDuKhaDung += tienHoanVeVi;

                        _viGateway.Sua(vi);

                        // [VÁ BUG KẾ TOÁN]: Tiền trả về ví LUÔN LUÔN là Hoàn Cọc.
                        if (tienHoanVeVi > 0)
                        {
                            var gdHoan = new ET_GiaoDichVi
                            {
                                MaCode = "GD-REF-" + DateTime.Now.Ticks.ToString().Substring(10),
                                IdVi = vi.Id,
                                LoaiGiaoDich = AppConstants.LoaiGiaoDichVi.HoanCoc, // Cố định là Hoàn cọc
                                SoTien = tienHoanVeVi,
                                ThoiGian = DateTime.Now, CreatedAt = DateTime.Now, CreatedBy = idNhanVien
                            };
                            td.IdGiaoDichHoanCoc = _giaoDichGateway.ThemVaLayId(gdHoan);
                        }
                    }
                    else // Tiền mặt
                    {
                        if (tienHoanVeVi > 0)
                        {
                            ET_PhieuChi pc = new ET_PhieuChi
                            {
                                MaCode = "PC-REF-" + DateTime.Now.Ticks.ToString().Substring(10),
                                SoTien = tienHoanVeVi,
                                LyDo = coPhat ? "Hoàn cọc (Đã trừ lố giờ/phạt)" : "Hoàn full cọc",
                                ThoiGian = DateTime.Now, CreatedAt = DateTime.Now, CreatedBy = idNhanVien
                            };
                            _phieuChiGateway.Them(pc);
                        }
                    }

                    // [VÁ BUG THỤT KÉT]: Tự động tạo Phiếu Thu cho phần tiền phạt vượt cọc
                    if (tienPhatVuotCoc > 0)
                    {
                        ET_PhieuThu ptPhat = new ET_PhieuThu
                        {
                            MaCode = "PT-PEN-" + DateTime.Now.Ticks.ToString().Substring(10),
                            // ThueDoChiTiet giờ lên qua CTDH nên không có IdDonHang trực tiếp.
                            // Phần phạt thu thêm được gắn vào DonHang qua CTDH của giao dịch cộc (IdGiaoDichCoc).
                            IdDonHang = td.IdGiaoDichCoc.HasValue
                                ? _giaoDichGateway.LayTheoId(td.IdGiaoDichCoc.Value)?.IdDonHangLienQuan ?? 0
                                : 0,
                            SoTien = tienPhatVuotCoc,
                            PhuongThuc = AppConstants.PhuongThucThanhToan.TienMat,
                            ThoiGian = DateTime.Now, CreatedAt = DateTime.Now, CreatedBy = idNhanVien
                        };
                        _phieuThuGateway.Them(ptPhat);
                    }

                    td.TrangThaiCoc = coPhat ? "DaPhat" : "DaHoan";
                    _thueDoGateway.Sua(td);
                    ts.Complete();

                    if (tienPhatVuotCoc > 0)
                        return OperationResult.Success(string.Format("Hoàn cọc thành công.\n⚠️ PHẠT VƯỢT CỌC: Cần thu thêm {0:N0}đ TIỀN MẶT.\n(Hệ thống đã tự động tạo Phiếu Thu)", tienPhatVuotCoc));

                    return OperationResult.Success();
                }
            }
            catch (System.Data.DBConcurrencyException)
            {
                return OperationResult.Failed("Ví vừa bị thay đổi bởi giao dịch khác.");
            }
            catch (Exception ex)
            {
                return OperationResult.Failed("Lỗi hoàn cọc: " + ex.Message);
            }
        }
    }
}

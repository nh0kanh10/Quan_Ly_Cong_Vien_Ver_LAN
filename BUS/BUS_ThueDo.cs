using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DAL;
using ET;

namespace BUS
{
    public class BUS_ThueDo
    {
        private static BUS_ThueDo instance;
        public static BUS_ThueDo Instance => instance ?? (instance = new BUS_ThueDo());

        public List<ET_ThueDoChiTiet> LoadDS() => DAL_ThueDoChiTiet.Instance.LoadDS();

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
                    int idDonHang = DAL_DonHang.Instance.ThemVaLayId(dh);
                    if (idDonHang <= 0) throw new Exception("Không thể tạo đơn hàng gốc.");

                    int? idGiaoDichCocChung = null;

                    // 1. THANH TOÁN (Trừ tiền 1 lần duy nhất cho toàn bộ giỏ)
                    if (phuongThuc == AppConstants.PhuongThucThanhToan.ViRfid && dh.IdKhachHang != null)
                    {
                        var vi = DAL_ViDienTu.Instance.LayTheoKhachHang(dh.IdKhachHang.Value);
                        if (vi == null || vi.SoDuKhaDung < tongCong) throw new Exception("Số dư ví không đủ.");

                        vi.SoDuKhaDung -= tongThue; 
                        vi.SoDuKhaDung -= tongCoc;  
                        vi.SoDuDongBang += tongCoc;
                        
                        if (!DAL_ViDienTu.Instance.Sua(vi)) throw new Exception("Lỗi cập nhật số dư ví.");

                        if (tongThue > 0)
                        {
                            var gdThue = new ET_GiaoDichVi { MaCode = "GD-RENT-" + DateTime.Now.Ticks.ToString().Substring(10), IdVi = vi.Id, LoaiGiaoDich = AppConstants.LoaiGiaoDichVi.ThanhToanDichVu, SoTien = tongThue, IdDonHangLienQuan = idDonHang, ThoiGian = DateTime.Now, CreatedAt = DateTime.Now, CreatedBy = idNhanVien };
                            if (DAL_GiaoDichVi.Instance.ThemVaLayId(gdThue) <= 0) throw new Exception("Lỗi ghi log thanh toán.");
                        }
                        
                        if (tongCoc > 0)
                        {
                            var gdCoc = new ET_GiaoDichVi { MaCode = "GD-DEP-" + DateTime.Now.Ticks.ToString().Substring(10), IdVi = vi.Id, LoaiGiaoDich = AppConstants.LoaiGiaoDichVi.ThuCoc, SoTien = tongCoc, IdDonHangLienQuan = idDonHang, ThoiGian = DateTime.Now, CreatedAt = DateTime.Now, CreatedBy = idNhanVien };
                            idGiaoDichCocChung = DAL_GiaoDichVi.Instance.ThemVaLayId(gdCoc);
                            if (idGiaoDichCocChung <= 0) throw new Exception("Lỗi ghi log cọc.");
                        }
                    }
                    else // Tiền mặt
                    {
                        var pt = new ET_PhieuThu { MaCode = "PT-RENT-" + DateTime.Now.Ticks.ToString().Substring(10), IdDonHang = idDonHang, SoTien = tongCong, PhuongThuc = phuongThuc, ThoiGian = DateTime.Now, CreatedAt = DateTime.Now, CreatedBy = idNhanVien };
                        if (!DAL_PhieuThu.Instance.Them(pt)) throw new Exception("Không tạo được Phiếu Thu.");
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
                            int idCtdh = DAL_ChiTietDonHang.Instance.ThemVaLayId(ctdh);
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
                            if (!DAL_ThueDoChiTiet.Instance.Them(td)) throw new Exception("Lưu chi tiết thuê thất bại.");
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
                    var td = DAL_ThueDoChiTiet.Instance.LayTheoId(idThueDo);
                    if (td == null) return OperationResult.Failed("Không tìm thấy dữ liệu thuê đồ.");
                    if (td.TrangThaiCoc != AppConstants.TrangThaiCoc.ChuaHoan) return OperationResult.Failed("Món này đã được hoàn cọc rồi.");

                    td.ThoiGianKetThuc = DateTime.Now;
                    decimal tienHoanVeVi = coPhat ? Math.Max(0, td.SoTienCoc - tienPhat) : td.SoTienCoc;
                    decimal tienPhatVuotCoc = coPhat ? Math.Max(0, tienPhat - td.SoTienCoc) : 0;

                    if (td.IdGiaoDichCoc != null) // RFID
                    {
                        var gdCoc = DAL_GiaoDichVi.Instance.LayTheoId(td.IdGiaoDichCoc.Value);
                        var vi = DAL_ViDienTu.Instance.LayTheoId(gdCoc.IdVi);

                        vi.SoDuDongBang -= td.SoTienCoc;
                        vi.SoDuKhaDung += tienHoanVeVi;

                        DAL_ViDienTu.Instance.Sua(vi);

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
                            td.IdGiaoDichHoanCoc = DAL_GiaoDichVi.Instance.ThemVaLayId(gdHoan);
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
                            DAL_PhieuChi.Instance.Them(pc);
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
                                ? DAL_GiaoDichVi.Instance.LayTheoId(td.IdGiaoDichCoc.Value)?.IdDonHangLienQuan ?? 0
                                : 0,
                            SoTien = tienPhatVuotCoc,
                            PhuongThuc = AppConstants.PhuongThucThanhToan.TienMat,
                            ThoiGian = DateTime.Now, CreatedAt = DateTime.Now, CreatedBy = idNhanVien
                        };
                        DAL_PhieuThu.Instance.Them(ptPhat);
                    }

                    td.TrangThaiCoc = coPhat ? "DaPhat" : "DaHoan";
                    DAL_ThueDoChiTiet.Instance.Sua(td);
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

using ET.DTOs;
using ET.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories.BanHang
{
    public class DAL_HoanHang
    {
        public static DAL_HoanHang Instance { get; } = new DAL_HoanHang();

        /// Lấy chi tiết đơn hàng để load lên giao diện hoàn hàng
        public DTO_DonHangHoan LayDonHangHoan(string maDonHang)
        {
            if (string.IsNullOrWhiteSpace(maDonHang)) return null;

            using (var db = new DaiNamDBDataContext())
            {
                var dh = db.DonHangs.FirstOrDefault(d => d.MaDonHang == maDonHang && d.TrangThai != AppConstants.TrangThaiDonHang.DaHuy);
                if (dh == null) return null;

                var result = new DTO_DonHangHoan
                {
                    IdDonHang = dh.Id,
                    MaDonHang = dh.MaDonHang,
                    IdKhachHang = dh.IdKhachHang,
                    NgayTao = dh.NgayTao,
                    TongThanhToan = dh.TongThanhToan,
                    DanhSachChiTiet = new List<DTO_ChiTietDonHangHoan>(),
                    DanhSachThanhToan = new List<DTO_PhuongThucThanhToanDaDung>()
                };

                // Load danh sách chi tiết có thể hoàn
                var chiTiet = (from ct in db.ChiTietDonHangs
                              join sp in db.SanPhams on ct.IdSanPham equals sp.Id
                              where ct.IdDonHang == dh.Id
                              let daHoan = db.LichSuHoanHangs.Where(lsh => lsh.IdChiTietDonHang == ct.Id).Sum(lsh => (decimal?)lsh.SoLuongHoan) ?? 0
                              where ct.SoLuong > daHoan
                              select new DTO_ChiTietDonHangHoan
                              {
                                  IdChiTietDonHang = ct.Id,
                                  IdSanPham = ct.IdSanPham,
                                  TenSanPham = sp.TenSanPham,
                                  LoaiSanPham = sp.LoaiSanPham,
                                  SoLuongMua = ct.SoLuong,
                                  SoLuongDaHoan = daHoan,
                                  DonGiaThucTe = ct.DonGiaThucTe
                              });

                result.DanhSachChiTiet = chiTiet.ToList();

                // Load các phương thức thanh toán của đơn này
                var ptPay = from ct in db.ChungTuTCs
                            join pay in db.ChiTietThanhToans on ct.Id equals pay.IdChungTu
                            where ct.IdDonHang == dh.Id && ct.LoaiChungTu == AppConstants.LoaiChungTuTC.THU_THANHTOAN && ct.TrangThai != AppConstants.TrangThaiChungTuTC.DaHuy
                            select new DTO_PhuongThucThanhToanDaDung
                            {
                                PhuongThuc = pay.PhuongThuc,
                                SoTien = pay.SoTien,
                                IdViDienTu = (pay.PhuongThuc == AppConstants.PhuongThucTT.ViRFID) ? (db.SoCaiVis.FirstOrDefault(v => v.IdChungTu == ct.Id) != null ? db.SoCaiVis.FirstOrDefault(v => v.IdChungTu == ct.Id).IdVi : (int?)null) : null
                            };
                
                result.DanhSachThanhToan = ptPay.ToList();

                return result;
            }
        }

        /// Thực thi Hoàn Hàng bằng SQL Transaction. 
        /// Đảm bảo tính nhất quán qua các bảng: ChiTietDonHang, LichSuHoanHang, Kho, TaiChinh, TheRFID, KhachHang.
        public ET.Results.OperationResult ThucHienHoanHang(DTO_HoanHangRequest request)
        {
            if (request == null || request.ChiTietHoan == null || !request.ChiTietHoan.Any(x => x.SoLuongMuonHoan > 0))
                return ET.Results.OperationResult.Fail("NO_ITEM_TO_REFUND");

            using (var db = new DaiNamDBDataContext())
            {
                db.Connection.Open();
                using (var transaction = db.Connection.BeginTransaction())
                {
                    db.Transaction = transaction;
                    try
                    {
                        var donHang = db.DonHangs.FirstOrDefault(x => x.Id == request.IdDonHang);
                        if (donHang == null) return ET.Results.OperationResult.Fail("ORDER_NOT_FOUND");

                        decimal tongTienHoan = 0;
                        decimal tongTienTruDiem = 0;

                        // Tỉ lệ hoàn thực tế:
                        decimal tyLeThucTra = 1m;
                        if (donHang.TongTienHang > 0)
                        {
                            tyLeThucTra = (donHang.TongTienHang - donHang.TongGiamGia) / donHang.TongTienHang;
                        }

                        var listLichSuHoanRAM = new List<LichSuHoanHang>();

                        // 1. duyệt từng dòng chi tiết
                        foreach (var item in request.ChiTietHoan.Where(x => x.SoLuongMuonHoan > 0))
                        {
                            var ct = db.ChiTietDonHangs.FirstOrDefault(x => x.Id == item.IdChiTietDonHang);
                            if (ct == null) throw new Exception($"NOT_FOUND_CHK:{item.IdChiTietDonHang}");

                            // Tính SoLuongDaHoan từ LichSuHoanHang (3NF)
                            decimal daHoan = db.LichSuHoanHangs
                                .Where(lsh => lsh.IdChiTietDonHang == ct.Id)
                                .Sum(lsh => (decimal?)lsh.SoLuongHoan) ?? 0;

                            if (daHoan + item.SoLuongMuonHoan > ct.SoLuong)
                            {
                                throw new Exception($"OVER_REFUND_LIMIT:{ct.Id}");
                            }

                            // 1.2 Ghi log Lịch sử hoàn
                            var lsHoan = new LichSuHoanHang
                            {
                                IdChiTietDonHang = ct.Id,
                                SoLuongHoan = item.SoLuongMuonHoan,
                                LyDo = string.IsNullOrWhiteSpace(item.LyDoHoan) ? "Khách trả lại" : item.LyDoHoan,
                                IdNguoiDuyet = request.IdNguoiDuyet,
                                ThoiGian = DateTime.Now
                            };
                            db.LichSuHoanHangs.InsertOnSubmit(lsHoan);
                            listLichSuHoanRAM.Add(lsHoan);

                            // Tính toán số tiền hoàn ĐÚNG GIÁ THỰC TẾ (DonGiaThucTe x Tỷ Lệ Thực Trả)
                            decimal donGiaSauGiam = ct.DonGiaThucTe * tyLeThucTra;
                            decimal tienHoanDongNay = item.SoLuongMuonHoan * donGiaSauGiam;
                            tongTienHoan += tienHoanDongNay;

                            // 1.3 Kho bãi (Không lưu Kho nếu là AnUong)
                            if (item.LoaiSanPham != AppConstants.LoaiSanPham.AnUong && 
                                item.LoaiSanPham != AppConstants.LoaiSanPham.DoUong && 
                                item.LoaiSanPham != AppConstants.LoaiSanPham.DatChoThuAn)
                            {
                                if (item.LoaiSanPham == AppConstants.LoaiSanPham.HangHoa || 
                                    item.LoaiSanPham == AppConstants.LoaiSanPham.TuDo)
                                {
                                    // Tạo ChiTietChungTu nhập trả kho (không còn SoCai)
                                    // Cần có phiếu kho KHACH_TRA — tạo nếu chưa có trong transaction này
                                    var ctKho = new ChiTietChungTu
                                    {
                                        IdChungTu = request.IdChungTuHoanKho,  // phiếu KHACH_TRA được tạo trước khi gọi hàm này
                                        IdSanPham = ct.IdSanPham,
                                        IdLoHang = ct.IdLoHang,
                                        IdKhoXuat = request.IdKhoKhachAo,   // từ KHO_KHACH (ảo)
                                        IdKhoNhap = request.IdKhoMacDinh,   // về kho thật
                                        SoLuong = item.SoLuongMuonHoan,
                                        GhiChu = "Hoàn hàng"
                                    };
                                    db.ChiTietChungTus.InsertOnSubmit(ctKho);
                                }
                            }

                            // 1.4 Hủy vé chưa quét
                            if (item.LoaiSanPham == AppConstants.LoaiSanPham.VeVaoKhu || item.LoaiSanPham == AppConstants.LoaiSanPham.VeTroChoi)
                            {
                                // Tìm các vé điện tử phát sinh từ dòng này
                                var danhSachVe = db.VeDienTus.Where(v => v.IdChiTietDonHang == ct.Id && v.TrangThai == "ChuaSuDung").Take((int)item.SoLuongMuonHoan).ToList();
                                if (danhSachVe.Count < (int)item.SoLuongMuonHoan)
                                {
                                    throw new Exception($"PARTIAL_TICKET_SCANNED:{ct.Id}");
                                }

                                foreach (var ve in danhSachVe)
                                {
                                    ve.TrangThai = "DaHuy";
                                }
                            }

                            // Cộng dồn để tính điểm cần thu hồi
                            tongTienTruDiem += tienHoanDongNay;
                        }

                        // Submit trước để Lấy Id Lịch sử Hoàn (Chưa Commit)
                        db.SubmitChanges();

                        // 2. TÀI CHÍNH - Lập Phiếu Chi Hoàn Tiến
                        var chungTuTC = new ChungTuTC
                        {
                            MaChungTu = "PC-" + donHang.MaDonHang + "-" + DateTime.Now.ToString("HHmmss"),
                            LoaiChungTu = AppConstants.LoaiChungTuTC.HOAN_TIEN,
                            IdDonHang = donHang.Id,
                            IdPhienThuNgan = donHang.IdPhienThuNgan,
                            MaGiaoDichClient = Guid.NewGuid(),
                            SoTien = tongTienHoan,
                            TrangThai = AppConstants.TrangThaiChungTuTC.DaDuyet,
                            IdNguoiTao = request.IdNguoiDuyet
                        };
                        db.ChungTuTCs.InsertOnSubmit(chungTuTC);
                        db.SubmitChanges(); 

                        // 3. Phân bổ phương thức hoàn tiền theo lúc khách trả 
                        // Lấy các Chi Tiết Thanh Toán Gốc
                        var listOriginalPayment = (from ct in db.ChungTuTCs
                                                   join pay in db.ChiTietThanhToans on ct.Id equals pay.IdChungTu
                                                   where ct.IdDonHang == donHang.Id && ct.LoaiChungTu == AppConstants.LoaiChungTuTC.THU_THANHTOAN && ct.TrangThai != AppConstants.TrangThaiChungTuTC.DaHuy
                                                   select new { pay, ct.Id }).ToList();

                        decimal soTienTrachNhiem = tongTienHoan;

                        // Ưu tiên hoàn vào Ví RFID trước
                        foreach (var ori in listOriginalPayment.Where(p => p.pay.PhuongThuc == AppConstants.PhuongThucTT.ViRFID).ToList())
                        {
                            if (soTienTrachNhiem <= 0) break;
                            
                            // Lấy chi tiết ví
                            var socaiGoc = db.SoCaiVis.FirstOrDefault(v => v.IdChungTu == ori.Id && v.SoTien == ori.pay.SoTien && v.LoaiPhep == AppConstants.LoaiPhepVi.Tru);
                            
                            decimal tienTraVaoVi = Math.Min(ori.pay.SoTien, soTienTrachNhiem);
                            soTienTrachNhiem -= tienTraVaoVi;

                            var ctPay = new ChiTietThanhToan
                            {
                                IdChungTu = chungTuTC.Id,
                                PhuongThuc = AppConstants.PhuongThucTT.ViRFID,
                                SoTien = tienTraVaoVi,
                                GhiChu = "Hoàn lại vào Ví"
                            };
                            db.ChiTietThanhToans.InsertOnSubmit(ctPay);

                            if (socaiGoc != null)
                            {
                                var socaiHoanVi = new SoCaiVi
                                {
                                    IdVi = socaiGoc.IdVi,
                                    LoaiPhep = AppConstants.LoaiPhepVi.Nap,
                                    SoTien = tienTraVaoVi,
                                    IdChungTu = chungTuTC.Id,
                                    MoTa = "Hoàn tiền hóa đơn " + donHang.MaDonHang,
                                    NguoiTao = request.IdNguoiDuyet
                                };
                                db.SoCaiVis.InsertOnSubmit(socaiHoanVi);
                            }
                        }

                        // Ưu tiên 2: Hoàn Chuyển Khoản / MoMo
                        foreach (var ori in listOriginalPayment.Where(p => p.pay.PhuongThuc == AppConstants.PhuongThucTT.ChuyenKhoan || p.pay.PhuongThuc == AppConstants.PhuongThucTT.MoMo).ToList())
                        {
                            if (soTienTrachNhiem <= 0) break;

                            decimal tienTra = Math.Min(ori.pay.SoTien, soTienTrachNhiem);
                            soTienTrachNhiem -= tienTra;

                            db.ChiTietThanhToans.InsertOnSubmit(new ChiTietThanhToan
                            {
                                IdChungTu = chungTuTC.Id,
                                PhuongThuc = ori.pay.PhuongThuc,
                                SoTien = tienTra,
                                GhiChu = "Hoàn qua " + ori.pay.PhuongThuc
                            });
                        }

                        // Cuối cùng: Mở két Hoàn Tiền Mặt phần còn thiếu
                        if (soTienTrachNhiem > 0)
                        {
                            db.ChiTietThanhToans.InsertOnSubmit(new ChiTietThanhToan
                            {
                                IdChungTu = chungTuTC.Id,
                                PhuongThuc = AppConstants.PhuongThucTT.TienMat,
                                SoTien = soTienTrachNhiem,
                                GhiChu = "Mở két chi hoàn Tiền Mặt"
                            });
                        }

                        // 4. KHR & ĐIỂM THƯỞNG: Thu hồi điểm tương ứng với giá trị hàng hoàn lại
                        if (donHang.IdKhachHang.HasValue && tongTienTruDiem > 0)
                        {
                            var configDIEM = db.CauHinhs.FirstOrDefault(c => c.Khoa == AppConstants.ConfigKeys.DIEM_QUY_DOI);
                            decimal tyLe = 1000m;
                            if (configDIEM != null && decimal.TryParse(configDIEM.GiaTri, out var val) && val > 0) tyLe = val;
                            
                            int diemThuHoi = (int)(tongTienTruDiem / tyLe);
                            
                            if (diemThuHoi > 0)
                            {
                                // Tính điểm hiện có từ LichSuDiem (3NF)
                                int diemHienTai = db.LichSuDiems
                                    .Where(ld => ld.IdKhachHang == donHang.IdKhachHang.Value)
                                    .Sum(ld => (int?)ld.SoDiem) ?? 0;

                                var lsDiem = new LichSuDiem
                                {
                                    IdKhachHang = donHang.IdKhachHang.Value,
                                    LoaiGiaoDich = AppConstants.LoaiGiaoDichDiem.TruDiem,
                                    SoDiem = -diemThuHoi,
                                    SoDuSauGD = diemHienTai - diemThuHoi,
                                    IdDonHang = donHang.Id,
                                    MoTa = "Thu hồi điểm do hoàn hàng",
                                    NgayTao = DateTime.Now
                                };
                                db.LichSuDiems.InsertOnSubmit(lsDiem);
                            }
                        }

                        db.SubmitChanges();
                        foreach(var h in listLichSuHoanRAM)
                        {
                            h.IdChungTuHoan = chungTuTC.Id;
                        }
                        db.SubmitChanges();

                        // Kiểm tra nếu TOÀN BỘ chi tiết đã hoàn -> cập nhật header
                        // Kiểm tra toàn bộ đã hoàn bằng SUM(LichSuHoanHang) — 3NF
                        bool tatCaDaHoan = db.ChiTietDonHangs
                            .Where(c => c.IdDonHang == donHang.Id)
                            .All(c => db.LichSuHoanHangs.Where(lsh => lsh.IdChiTietDonHang == c.Id).Sum(lsh => (decimal?)lsh.SoLuongHoan) >= c.SoLuong);
                        if (tatCaDaHoan)
                        {
                            donHang.TrangThai = "DaHoan";
                        }

                        db.SubmitChanges();
                        transaction.Commit();

                        return ET.Results.OperationResult.Ok("HOAN_HANG_SUCCESS");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return ET.Results.OperationResult.Fail("TRANSACTION_FAILED: " + ex.Message);
                    }
                }
            }
        }
    }
}



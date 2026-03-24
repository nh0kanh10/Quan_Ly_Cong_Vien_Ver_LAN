using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using ET;

namespace BUS
{
    public class BUS_KhoHang : IBaseBUS<ET_KhoHang>
    {
        private readonly IKhoHangGateway _khoHangGateway;
        private readonly ITonKhoGateway _tonKhoGateway;
        private readonly ITheKhoGateway _theKhoGateway;
        private readonly ISanPhamGateway _sanPhamGateway;
        private readonly IDonViTinhGateway _donViTinhGateway;
        private readonly IPhieuNhapKhoGateway _phieuNhapGateway;
        private readonly INhaCungCapGateway _nhaCungCapGateway;
        private readonly IPhieuXuatKhoGateway _phieuXuatGateway;
        private readonly IComboChiTietGateway _comboChiTietGateway;

        private static BUS_KhoHang instance;
        public static BUS_KhoHang Instance
        {
            get
            {
                if (instance == null) instance = new BUS_KhoHang();
                return instance;
            }
        }

        public BUS_KhoHang() : this(new DefaultKhoHangGateway(), new DefaultTonKhoGateway(), new DefaultTheKhoGateway(),
                                    new DefaultSanPhamGateway(), new DefaultDonViTinhGateway(), new DefaultPhieuNhapKhoGateway(),
                                    new DefaultNhaCungCapGateway(), new DefaultPhieuXuatKhoGateway(), new DefaultComboChiTietGateway()) { }

        public BUS_KhoHang(IKhoHangGateway khoHangGateway, ITonKhoGateway tonKhoGateway, ITheKhoGateway theKhoGateway,
                           ISanPhamGateway sanPhamGateway, IDonViTinhGateway donViTinhGateway, IPhieuNhapKhoGateway phieuNhapGateway,
                           INhaCungCapGateway nhaCungCapGateway, IPhieuXuatKhoGateway phieuXuatGateway, IComboChiTietGateway comboChiTietGateway)
        {
            _khoHangGateway = khoHangGateway;
            _tonKhoGateway = tonKhoGateway;
            _theKhoGateway = theKhoGateway;
            _sanPhamGateway = sanPhamGateway;
            _donViTinhGateway = donViTinhGateway;
            _phieuNhapGateway = phieuNhapGateway;
            _nhaCungCapGateway = nhaCungCapGateway;
            _phieuXuatGateway = phieuXuatGateway;
            _comboChiTietGateway = comboChiTietGateway;
        }

        public List<ET_KhoHang> LoadDS()
        {
            return _khoHangGateway.LoadDS().Where(x => x.IsDeleted == false).ToList();
        }

        public List<ET_KhoHang> TimKiem(string keyword, string filter)
        {
            var p = LoadDS();
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.ToLower();
                p = p.Where(x => 
                    (x.TenKho != null && x.TenKho.ToLower().Contains(keyword)) ||
                    (x.LoaiKho != null && x.LoaiKho.ToLower().Contains(keyword))).ToList();
            }
            return p;
        }

        public ET_KhoHang GetById(int id)
        {
            return _khoHangGateway.LayTheoId(id);
        }

        public ResponseResult Them(ET_KhoHang entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.IsDeleted = false;
            
            if (_khoHangGateway.Them(entity))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi thêm kho hàng!" };
        }

        public ResponseResult Sua(ET_KhoHang entity)
        {
            if (_khoHangGateway.Sua(entity))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi cập nhật kho hàng!" };
        }

        public ResponseResult Xoa(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                if (_khoHangGateway.Sua(entity))
                    return new ResponseResult { IsSuccess = true };
            }
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi xóa kho hàng!" };
        }

        #region Các nghiệp vụ thống kê phiếu, thẻ kho, số liệu nhập xuất hiển thị cho thu ngân

        /// <summary>
        /// Lấy danh sách chi tiết tất cả các mặt hàng đang có trong kho (kèm theo số lượng hiện tại và đơn giá)
        /// Thường dùng để hiển thị lên bảng tính cho nhân viên theo dõi hàng ngày.
        /// </summary>
        public List<ET_TonKho_View> GetTonKhoChiTiet(int idKho = 1)
        {
            var tonKho = _tonKhoGateway.LoadDS().Where(x => x.IdKho == idKho).ToList();
            var theKho = _theKhoGateway.LoadDS().Where(x => x.IdKho == idKho && (x.LoaiGiaoDich == "NHAP_NCC" || x.LoaiGiaoDich == "NHAP_KHO")).ToList();
            // Lấy tất cả Sản Phẩm "Vật Lý" để luôn hiển thị dù chưa nhập kho (chưa có trong bảng TonKho)
            var sanPham = _sanPhamGateway.LoadDS()
                .Where(x => !x.IsDeleted && 
                           (x.LoaiSanPham == AppConstants.LoaiSanPham.AnUong || 
                            x.LoaiSanPham == AppConstants.LoaiSanPham.DoLuuNiem))
                .ToList(); 
            var donViTinh = _donViTinhGateway.LoadDS();
            
            return sanPham.Select(s => {
                var t = tonKho.FirstOrDefault(tk => tk.IdSanPham == s.Id);
                var lastNhap = theKho.Where(x => x.IdSanPham == s.Id).OrderByDescending(x => x.Id).FirstOrDefault();
                decimal cost = lastNhap != null ? (decimal)lastNhap.DonGiaVatTu : (s.DonGia * 0.5m);

                return new ET_TonKho_View
                {
                    Id = t != null ? t.Id : 0,
                    IdSanPham = s.Id,
                    MaSanPham = s.MaCode ?? "N/A",
                    TenSanPham = s.Ten,
                    LoaiSanPham = s.LoaiSanPham,
                    DonViTinh = donViTinh.FirstOrDefault(d => d.Id == s.IdDonViCoBan)?.Ten ?? "N/A",
                    SoLuong = t != null ? t.SoLuong : 0,
                    NguongCanhBao = 5,
                    DonGia = cost
                };
            }).OrderBy(x => x.LoaiSanPham).ThenBy(x => x.TenSanPham).ToList();
        }

        /// <summary>
        /// Báo cáo tổng thể tình hình sức khỏe của kho hàng dành cho Quản lý xem (Thường hiện ở Màn hình chính)
        /// Tính toán ra 3 con số quan trọng: Những đồ sắp hết, Những đồ bị âm kho, và Tổng số tiền vốn đang nằm trong kho.
        /// </summary>
        public ET_DashboardKho GetDashboardMetrics(int idKho = 1)
        {
            var tonKho = _tonKhoGateway.LoadDS().Where(x => x.IdKho == idKho).ToList();
            var sanPhamPhy = _sanPhamGateway.LoadDS()
                .Where(x => !x.IsDeleted && 
                           (x.LoaiSanPham == AppConstants.LoaiSanPham.AnUong || 
                            x.LoaiSanPham == AppConstants.LoaiSanPham.DoLuuNiem))
                .ToList();
            
            int sapHet = 0;
            int amKho = 0;
            decimal tongVon = 0;

            var theKho = _theKhoGateway.LoadDS().Where(x => x.IdKho == idKho && (x.LoaiGiaoDich == "NHAP_NCC" || x.LoaiGiaoDich == "NHAP_KHO")).ToList();

            foreach (var sp in sanPhamPhy)
            {
                var t = tonKho.FirstOrDefault(x => x.IdSanPham == sp.Id);
                int qty = t != null ? t.SoLuong : 0;
                
                if (qty <= 5 && qty >= 0) sapHet++;
                if (qty < 0) amKho++;

                var lastNhap = theKho.Where(x => x.IdSanPham == sp.Id).OrderByDescending(x => x.Id).FirstOrDefault();
                decimal cost = lastNhap != null ? (decimal)lastNhap.DonGiaVatTu : (sp.DonGia * 0.5m);

                if (qty > 0) tongVon += qty * cost;
            }

            return new ET_DashboardKho 
            { 
                SapHet = sapHet, 
                AmKho = amKho, 
                TongVon = (double)tongVon
            };
        }

        public object GetLichSuNhap(int idKho = 1, DateTime? tuNgay = null, DateTime? denNgay = null)
        {
            var phieuNhap = _phieuNhapGateway.LoadDS().Where(x => x.IdKho == idKho).ToList();
            if (tuNgay != null) phieuNhap = phieuNhap.Where(x => x.NgayNhap >= tuNgay.Value.Date).ToList();
            if (denNgay != null) phieuNhap = phieuNhap.Where(x => x.NgayNhap <= denNgay.Value.Date.AddDays(1).AddSeconds(-1)).ToList();

            var nhaCungCap = _nhaCungCapGateway.LoadDS();

            return phieuNhap.Select(p => new
            {
                p.Id,
                p.SoChungTu,
                NgayNhap = p.NgayNhap,
                NhaCungCap = nhaCungCap.FirstOrDefault(n => n.Id == p.IdNhaCungCap)?.Ten ?? "N/A",
                TongTien = p.TongTien
            }).OrderByDescending(x => x.NgayNhap).ToList();
        }

        public object GetLichSuXuat(int idKho = 1, DateTime? tuNgay = null, DateTime? denNgay = null)
        {
            var phieuXuat = _phieuXuatGateway.LoadDS().Where(x => x.IdKhoXuat == idKho).ToList();
            if (tuNgay != null) phieuXuat = phieuXuat.Where(x => x.NgayXuat >= tuNgay.Value.Date).ToList();
            if (denNgay != null) phieuXuat = phieuXuat.Where(x => x.NgayXuat <= denNgay.Value.Date.AddDays(1).AddSeconds(-1)).ToList();

            return phieuXuat.Select(p => new
            {
                p.Id,
                SoChungTu = "PX" + p.Id.ToString("D5"),
                NgayXuat = p.NgayXuat,
                TrangThai = "Hoàn Tất",
                LyDoXuat = p.LyDo,
                NguoiNhan = p.IdKhoNhan != null ? "Chuyển Kho" : "Xuất Tiêu Hao"
            }).OrderByDescending(x => x.NgayXuat).ToList();
        }

        /// <summary>
        /// Quy trình Tự động cập nhật lượng giảm trên sổ sách khi mới bán xong hóa đơn hàng
        /// </summary>
        public bool WriteLedgerTuDong(int donHangId, List<ET_ChiTietDonHang> chiTietList, int createdBy, int idKhoXuLy = 1)
        {
            try
            {

                // Bước 1: Thu thập toàn bộ danh sách đồ uống/quà lưu niệm và set/combo mà khách đã mua
                var listIdSP = chiTietList.Where(x => x.IdSanPham.HasValue).Select(x => x.IdSanPham.Value).ToList();
                var listIdCombo = chiTietList.Where(x => x.IdCombo.HasValue).Select(x => x.IdCombo.Value).ToList();

                var dictSanPham = _sanPhamGateway.LoadDS().Where(x => listIdSP.Contains(x.Id)).ToDictionary(x => x.Id, x => x);
                var allComboDetails = _comboChiTietGateway.LoadDS().Where(x => listIdCombo.Contains(x.IdCombo)).ToList();

                var listTheKhoToInsert = new List<ET_TheKho>();

                // Bước 2: Tạm nháp và tính toán ra danh sách số lượng bị trừ đi cho các món đồ
                foreach (var item in chiTietList)
                {
                    if (item.IdSanPham.HasValue && dictSanPham.ContainsKey(item.IdSanPham.Value))
                    {
                        var sp = dictSanPham[item.IdSanPham.Value];
                        if (sp.LoaiSanPham == AppConstants.LoaiSanPham.AnUong || sp.LoaiSanPham == AppConstants.LoaiSanPham.DoLuuNiem)
                        {
                            listTheKhoToInsert.Add(CreateLedgerEntry(idKhoXuLy, sp.Id, -item.SoLuong, donHangId, createdBy, "POS Auto-Deduct"));
                        }
                    }
                    else if (item.IdCombo.HasValue)
                    {
                        var comboDetails = allComboDetails.Where(x => x.IdCombo == item.IdCombo.Value).ToList();
                        foreach (var cd in comboDetails)
                        {
                            int qtyToDeduct = cd.SoLuong * item.SoLuong;
                            var sp = _sanPhamGateway.LayTheoId(cd.IdSanPham);
                            if (sp != null && (sp.LoaiSanPham == AppConstants.LoaiSanPham.AnUong || sp.LoaiSanPham == AppConstants.LoaiSanPham.DoLuuNiem)) 
                            {
                                listTheKhoToInsert.Add(CreateLedgerEntry(idKhoXuLy, cd.IdSanPham, -qtyToDeduct, donHangId, createdBy, "POS BOM-Deduct (Combo #" + item.IdCombo + ")"));
                            }
                        }
                    }
                }

                // Bước 3: Đồng loạt lưu lại lịch sử xuất kho tự động cho tất cả các món đồ trên
                foreach (var tk in listTheKhoToInsert)
                {
                    _theKhoGateway.Them(tk); 
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void DongBoTonKhoTrucTiep()
        {
            // Cầu nối để giao diện bấm đồng bộ chốt số lại lượng tồn kho sau khi tính toán
            _tonKhoGateway.DongBoTonKhoTrucTiepTuTheKho();
        }

        #region Nghiệp vụ kiểm kê kho (So sánh chênh lệch phần mềm và kho thực tế)
        /// <summary>
        /// Hoàn tất kiểm kê: Ghi nhận sự điều chỉnh bù/trừ cho các sản phẩm chênh lệch, rồi đồng bộ lại.
        /// </summary>
        public bool HoanTatKiemKe(int idKho, List<(int IdSanPham, int ChenhLech, string GhiChu)> items, int userId)
        {
            try
            {
                using (var ts = new System.Transactions.TransactionScope())
                {
                    foreach (var item in items)
                    {
                        if (item.ChenhLech == 0) continue;

                        var theKho = new ET_TheKho
                        {
                            IdKho = idKho,
                            IdSanPham = item.IdSanPham,
                            LoaiGiaoDich = "KIEM_KE",
                            SoLuongThayDoi = item.ChenhLech, // Dương = dư, Âm = mất
                            TonCuoi = null,
                            DonGiaVatTu = null,
                            IdThamChieu = null,
                            ThoiGianGiaoDich = DateTime.Now,
                            CreatedBy = userId,
                            GhiChu = "Kiểm kê kho - " + (string.IsNullOrEmpty(item.GhiChu) ? "Điều chỉnh tự động" : item.GhiChu)
                        };

                        _theKhoGateway.Them(theKho);
                    }

                    // Đồng bộ tồn kho từ sổ thẻ kho
                    DongBoTonKhoTrucTiep();
                    ts.Complete();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        // Hàm hỗ trợ viết thu gọn việc tạo Thẻ Kho Mới
        private ET_TheKho CreateLedgerEntry(int idKho, int idSP, int qty, int idRef, int userId, string note)
        {
            return new ET_TheKho
            {
                IdKho = idKho, IdSanPham = idSP, LoaiGiaoDich = "XUAT_POS",
                SoLuongThayDoi = qty, TonCuoi = null, DonGiaVatTu = null,
                IdThamChieu = idRef, ThoiGianGiaoDich = DateTime.Now,
                CreatedBy = userId, GhiChu = note + " DonHang #" + idRef
            };
        }
    }
}
 
#endregion

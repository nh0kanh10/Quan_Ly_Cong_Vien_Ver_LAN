using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using BUS;
using DAL;
using ET;
using FontAwesome.Sharp;

namespace GUI
{
    public partial class frmThueDo : Form, IBaseForm
    {
        // === State ===
        private List<RentalCartItem> _cart = new List<RentalCartItem>();
        private ET_KhachHang _selectedKH = null;
        private ET_ViDienTu _selectedVi = null;
        private int _currentIdKhuVuc = -1;
        private List<ET_SanPham> _allRentalProducts = new List<ET_SanPham>();
        private Dictionary<int, decimal> _tienCocCache = new Dictionary<int, decimal>();

        // Return mode state
        private ET_KhachHang _returnKH = null;
        private List<ET_ThueDoChiTiet> _dangThueList = new List<ET_ThueDoChiTiet>();

        public frmThueDo()
        {
            InitializeComponent();
            ApplyStyles();
            LoadTramChoThue();
            SetupGridColumns();
            RefreshCartDisplay();

            InitIcons();
            ApplyStyles();
            ApplyPermissions();
        }

        private void frmThueDo_Load(object sender, EventArgs e)
        {
            txtRfidGiao.Focus();
        }

        public void ApplyPermissions()
        {
            // Standard RBAC gating for Dai Nam POS
            // The VIEW_POS permission is already checked in Form1, but we can add granular control here
            bool canManage = true; // Default for now, can be linked to BUS_QuyenHan
            btnTraDu.Enabled = canManage;
            btnBaoHong.Enabled = canManage;
        }

        public void ApplyStyles()
        {
            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleDevExpressGrid(gridControlSanPham);
            ThemeManager.StyleDevExpressGrid(gridControlGioHang);
            ThemeManager.StyleDevExpressGrid(gridControlDangThue);
        }

        public void InitIcons()
        {
            btnThanhToanRfid.Image = IconHelper.GetBitmap(IconChar.Bolt, Color.White, 18);
            btnThanhToanTienMat.Image = IconHelper.GetBitmap(IconChar.MoneyBillWave, Color.White, 18);
            btnHuyGiao.Image = IconHelper.GetBitmap(IconChar.Xmark, Color.White, 16);
            btnTraDu.Image = IconHelper.GetBitmap(IconChar.Check, Color.White, 18);
            btnBaoHong.Image = IconHelper.GetBitmap(IconChar.TriangleExclamation, Color.White, 18);
        }

        public void LoadData()
        {
            LoadSanPhamThue();
        }

        // ============================================================
        // SETUP
        // ============================================================

        private void LoadTramChoThue()
        {
            try
            {
                var khuVucs = BUS_KhuVuc.Instance.LoadDS();
                cboTramChoThue.Items.Clear();
                cboTramChoThue.Items.Add(new ComboItem { Text = "-- Chọn trạm cho thuê --", Value = -1 });
                foreach (var kv in khuVucs)
                {
                    cboTramChoThue.Items.Add(new ComboItem { Text = "📍 " + kv.TenKhuVuc, Value = kv.Id });
                }
                cboTramChoThue.DisplayMember = "Text";
                cboTramChoThue.SelectedIndex = 0;
            }
            catch { }
        }

        private void SetupGridColumns()
        {
            // Product grid columns
            gridViewSanPham.Columns.Clear();
            gridViewSanPham.PopulateColumns();

            // Cart grid columns
            gridViewGioHang.Columns.Clear();
            gridViewGioHang.PopulateColumns();

            // Return grid columns
            gridViewDangThue.Columns.Clear();
            gridViewDangThue.PopulateColumns();
        }

        // ============================================================
        // STATION SELECTOR (Context-Aware Filtering)
        // ============================================================

        private void cboTramChoThue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTramChoThue.SelectedItem is ComboItem item)
            {
                _currentIdKhuVuc = item.Value;
                LoadSanPhamThue();
                ClearCart();
            }
        }

        private void LoadSanPhamThue()
        {
            try
            {
                // Load all rental products for selected KhuVuc
                var allSP = BUS_SanPham.Instance.LoadDS()
                    .Where(x => x.LoaiSanPham == AppConstants.LoaiSanPham.Thue
                             && x.TrangThai == AppConstants.TrangThaiSanPham.DangBan
                             && !x.IsDeleted)
                    .ToList();

                if (_currentIdKhuVuc > 0)
                    allSP = allSP.Where(x => x.IdKhuVuc == _currentIdKhuVuc).ToList();

                _allRentalProducts = allSP;

                // Pre-load TienCoc from BangGia for each product
                _tienCocCache.Clear();
                foreach (var sp in allSP)
                {
                    _tienCocCache[sp.Id] = BUS_BangGia.Instance.GetTienCoc(sp.Id);
                }

                // Bind to grid with display-friendly columns
                var displayData = allSP.Select(sp => new
                {
                    sp.Id,
                    sp.MaCode,
                    Ten = sp.Ten,
                    TienThue = BUS_BangGia.Instance.GetDynamicPrice(sp.Id, DateTime.Now),
                    TienCoc = _tienCocCache[sp.Id],
                    sp.MoTa
                }).ToList();

                gridControlSanPham.DataSource = displayData;
                gridViewSanPham.PopulateColumns();

                // Style columns
                if (gridViewSanPham.Columns.Count > 0)
                {
                    gridViewSanPham.Columns["Id"].Visible = false;
                    if (gridViewSanPham.Columns["MaCode"] != null) gridViewSanPham.Columns["MaCode"].Width = 120;
                    if (gridViewSanPham.Columns["Ten"] != null) { gridViewSanPham.Columns["Ten"].Caption = "Tên sản phẩm"; gridViewSanPham.Columns["Ten"].Width = 200; }
                    if (gridViewSanPham.Columns["TienThue"] != null) { gridViewSanPham.Columns["TienThue"].Caption = "Tiền thuê"; gridViewSanPham.Columns["TienThue"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric; gridViewSanPham.Columns["TienThue"].DisplayFormat.FormatString = "N0"; }
                    if (gridViewSanPham.Columns["TienCoc"] != null) { gridViewSanPham.Columns["TienCoc"].Caption = "Tiền cọc"; gridViewSanPham.Columns["TienCoc"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric; gridViewSanPham.Columns["TienCoc"].DisplayFormat.FormatString = "N0"; }
                    if (gridViewSanPham.Columns["MoTa"] != null) gridViewSanPham.Columns["MoTa"].Visible = false;
                }

                gridViewSanPham.BestFitColumns();
            }
            catch (Exception ex)
            {
                TDCMessageBox.Show("Lỗi load sản phẩm: " + ex.Message);
            }
        }

        // ============================================================
        // PRODUCT SEARCH
        // ============================================================

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadSanPhamThue();
                return;
            }

            var filtered = _allRentalProducts
                .Where(x => x.Ten.ToLower().Contains(keyword) || x.MaCode.ToLower().Contains(keyword))
                .Select(sp => new
                {
                    sp.Id, sp.MaCode, Ten = sp.Ten,
                    // [Senior FIX]: Luôn lấy từ Engine + Cache để đồng bộ
                    TienThue = BUS_BangGia.Instance.GetDynamicPrice(sp.Id, DateTime.Now),
                    TienCoc = _tienCocCache.ContainsKey(sp.Id) ? _tienCocCache[sp.Id] : BUS_BangGia.Instance.GetTienCoc(sp.Id),
                    sp.MoTa
                }).ToList();

            gridControlSanPham.DataSource = filtered;
        }

        // ============================================================
        // ADD TO CART (Double-click product)
        // ============================================================

        private void gridViewSanPham_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewSanPham.FocusedRowHandle < 0) return;

            int idSP = (int)gridViewSanPham.GetRowCellValue(gridViewSanPham.FocusedRowHandle, "Id");
            var sp = _allRentalProducts.FirstOrDefault(x => x.Id == idSP);
            if (sp == null) return;

            // [Senior FIX]: Lấy giá chuẩn thời điểm này
            decimal currentTienThue = BUS_BangGia.Instance.GetDynamicPrice(idSP, DateTime.Now);
            decimal currentTienCoc = BUS_BangGia.Instance.GetTienCoc(idSP);

            var existing = _cart.FirstOrDefault(x => x.IdSanPham == idSP);
            if (existing != null)
            {
                existing.SoLuong++;
                // Cập nhật lại giá nếu có thay đổi (vd sang giờ cao điểm)
                existing.TienThue = currentTienThue;
                existing.TienCoc = currentTienCoc;
            }
            else
            {
                _cart.Add(new RentalCartItem
                {
                    IdSanPham = idSP,
                    TenSanPham = sp.Ten,
                    TienThue = currentTienThue,
                    TienCoc = currentTienCoc,
                    SoLuong = 1
                });
            }

            RefreshCartDisplay();
        }

        private void RefreshCartDisplay()
        {
            gridControlGioHang.DataSource = null;
            gridControlGioHang.DataSource = _cart.Select(x => new
            {
                x.IdSanPham,
                x.TenSanPham,
                x.SoLuong,
                x.TienThue,
                x.TienCoc,
                TongThue = x.TongThue,
                TongCoc = x.TongCoc
            }).ToList();

            gridViewGioHang.PopulateColumns();
            if (gridViewGioHang.Columns.Count > 0)
            {
                if (gridViewGioHang.Columns["IdSanPham"] != null) gridViewGioHang.Columns["IdSanPham"].Visible = false;
                if (gridViewGioHang.Columns["TenSanPham"] != null) gridViewGioHang.Columns["TenSanPham"].Caption = "Tên";
                foreach (var col in new[] { "TienThue", "TienCoc", "TongThue", "TongCoc" })
                {
                    if (gridViewGioHang.Columns[col] != null)
                    {
                        gridViewGioHang.Columns[col].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        gridViewGioHang.Columns[col].DisplayFormat.FormatString = "N0";
                    }
                }
                if (gridViewGioHang.Columns["TienCoc"] != null) gridViewGioHang.Columns["TienCoc"].Caption = "Cọc/1";
                if (gridViewGioHang.Columns["TongCoc"] != null) gridViewGioHang.Columns["TongCoc"].Caption = "Tổng cọc";
            }

            decimal tongThue = _cart.Sum(x => x.TongThue);
            decimal tongCoc = _cart.Sum(x => x.TongCoc);

            lblTongThue.Text = $"Tiền thuê: {tongThue:N0}đ";
            lblTongCoc.Text = $"Tiền cọc: {tongCoc:N0}đ";
            lblTongCong.Text = $"TỔNG: {(tongThue + tongCoc):N0}đ";
        }

        // ============================================================
        // RFID SCANNER (Giao Đồ mode)
        // ============================================================

        private void txtRfidGiao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LookupCustomerByRfid(txtRfidGiao.Text.Trim(), false);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void LookupCustomerByRfid(string maRfid, bool isReturnMode)
        {
            if (string.IsNullOrEmpty(maRfid)) return;

            try
            {
                // Chain: TheRFID -> ViDienTu (via IdVi) -> KhachHang
                var the = DAL_TheRFID.Instance.LayTheoId(maRfid);
                if (the == null)
                {
                    TDCMessageBox.Show("Không tìm thấy thẻ RFID!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var vi = DAL_ViDienTu.Instance.LayTheoId(the.IdVi);
                if (vi == null)
                {
                    TDCMessageBox.Show("Thẻ chưa liên kết ví điện tử!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var kh = DAL_KhachHang.Instance.LayTheoId(vi.IdKhachHang);
                if (kh == null)
                {
                    TDCMessageBox.Show("Ví chưa liên kết khách hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!isReturnMode)
                {
                    _selectedKH = kh;
                    _selectedVi = vi;
                    lblTenKH.Text = $"Khách: {kh.HoTen}";
                    lblSoDuVi.Text = $"Số dư ví: {vi.SoDuKhaDung:N0}đ";
                    lblSoDuVi.ForeColor = vi.SoDuKhaDung > 0 ? ThemeManager.SuccessColor : ThemeManager.DangerColor;
                }
                else
                {
                    _returnKH = kh;
                    lblTenKHTra.Text = $"Khách: {kh.HoTen}";
                    lblSoDuViTra.Text = $"Số dư ví: {vi.SoDuKhaDung:N0}đ";

                    // Load items currently being rented by this customer
                    LoadDangThue(kh.Id);
                }
            }
            catch (Exception ex)
            {
                TDCMessageBox.Show("Lỗi tìm khách: " + ex.Message);
            }
        }

        // ============================================================
        // PAYMENT (Giao Đồ mode)
        // ============================================================

        private void btnThanhToanRfid_Click(object sender, EventArgs e)
        {
            XuLyThanhToanGiaoDo(AppConstants.PhuongThucThanhToan.ViRfid);
        }

        private void btnThanhToanTienMat_Click(object sender, EventArgs e)
        {
            XuLyThanhToanGiaoDo(AppConstants.PhuongThucThanhToan.TienMat);
        }

        private void XuLyThanhToanGiaoDo(string phuongThuc)
        {
            if (_cart.Count == 0)
            {
                TDCMessageBox.Show("Giỏ hàng trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (phuongThuc == AppConstants.PhuongThucThanhToan.ViRfid && (_selectedKH == null || _selectedVi == null))
            {
                TDCMessageBox.Show("Vui lòng quẹt thẻ RFID khách hàng trước!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal tongThue = _cart.Sum(x => x.TongThue);
            decimal tongCoc = _cart.Sum(x => x.TongCoc);
            decimal tongCong = tongThue + tongCoc;

            if (phuongThuc == AppConstants.PhuongThucThanhToan.ViRfid && _selectedVi != null && _selectedVi.SoDuKhaDung < tongCong)
            {
                TDCMessageBox.Show($"Số dư ví không đủ!\nCần: {tongCong:N0}đ\nCó: {_selectedVi.SoDuKhaDung:N0}đ",
                    "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string phuongThucTxt = phuongThuc == AppConstants.PhuongThucThanhToan.ViRfid ? "trừ ví" : "thu TIỀN MẶT";
            string tenKH = _selectedKH != null ? _selectedKH.HoTen : "Khách vãng lai";

            var confirm = TDCMessageBox.Show(
                $"Xác nhận thuê đồ cho {tenKH}?\n\n" +
                $"Tiền thuê: {tongThue:N0}đ\nTiền cọc: {tongCoc:N0}đ\nTổng {phuongThucTxt}: {tongCong:N0}đ",
                "Xác nhận thanh toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            if (ET.SessionManager.CurrentUser == null || ET.SessionManager.CurrentUser.Id <= 0)
            {
                TDCMessageBox.Show("Phiên làm việc không hợp lệ! Vui lòng khởi động lại.", "Lỗi hệ thống");
                Application.Restart(); Environment.Exit(0); return;
            }

            int idNhanVien = ET.SessionManager.CurrentUser.Id;

            // [BỌC THÉP ARCHITECTURE]: Chỉ tạo Object trên RAM, KHÔNG GỌI DAL!
            var dh = new ET_DonHang
            {
                MaCode = "DH-THUE-" + DateTime.Now.ToString("yyMMddHHmmss") + "-" + Guid.NewGuid().ToString().Substring(0, 4).ToUpper(),
                IdKhachHang = _selectedKH?.Id,
                ThoiGian = DateTime.Now,
                TongTien = tongCong,
                TrangThai = AppConstants.TrangThaiDonHang.DaThanhToan,
                CreatedAt = DateTime.Now,
                CreatedBy = idNhanVien
            };

            // [GỌI ĐỘNG CƠ BUS]: Ném toàn bộ Data xuống BUS để nó chạy Transaction
            var result = BUS_ThueDo.Instance.RentMultipleItems(dh, _cart.ToList(), phuongThuc, idNhanVien);

            if (result.IsSuccess)
            {
                TDCMessageBox.Show($"✅ Thuê đồ thành công! Phương thức: {phuongThuc}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearCart();
                ClearCustomerGiao();
            }
            else
            {
                TDCMessageBox.Show("❌ " + result.ErrorMessage, "Lỗi Thanh Toán", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuyGiao_Click(object sender, EventArgs e)
        {
            if (_cart.Count > 0)
            {
                var confirm = TDCMessageBox.Show("Hủy giỏ hàng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes) return;
            }
            ClearCart();
            ClearCustomerGiao();
        }

        private void ClearCart()
        {
            _cart.Clear();
            RefreshCartDisplay();
        }

        private void ClearCustomerGiao()
        {
            _selectedKH = null;
            _selectedVi = null;
            lblTenKH.Text = "Khách: ---";
            lblSoDuVi.Text = "Số dư ví: ---";
            lblSoDuVi.ForeColor = ThemeManager.SuccessColor;
            txtRfidGiao.Clear(); // Scanner UX: Clear old code for next scan
            txtRfidGiao.Focus();
        }

        private void ClearCustomerTra()
        {
            _returnKH = null;
            lblTenKHTra.Text = "Khách: ---";
            lblSoDuViTra.Text = "Số dư ví: ---";
            gridControlDangThue.DataSource = null;
            txtRfidTra.Clear(); // Scanner UX: Clear old code for next scan
            txtRfidTra.Focus();
        }

        // ============================================================
        // TAB 2: NHẬN TRẢ
        // ============================================================

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabNhanTra)
            {
                txtRfidTra.Focus();
            }
            else
            {
                txtRfidGiao.Focus();
            }
        }

        private void txtRfidTra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LookupCustomerByRfid(txtRfidTra.Text.Trim(), true);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        // [CVE-THUE-1 FIX]: Quét Mã Vạch Biên Lai cho khách vãng lai (không có thẻ RFID)
        private void txtMaDonHang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string maCode = txtMaDonHang.Text.Trim();
                if (string.IsNullOrEmpty(maCode)) return;

                LoadDangThueByMaDonHang(maCode);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// [CVE-THUE-1 FIX]: Tìm đơn hàng bằng MaCode trên biên lai giấy.
        /// Flow: Quét mã vạch DH-THUE-xxx -> Query DAL_DonHang.LayTheoMaCode -> Lôi ThueDoChiTiet ra.
        /// </summary>
        private void LoadDangThueByMaDonHang(string maCode)
        {
            try
            {
                var dh = BUS_DonHang.Instance.GetByMaCode(maCode);
                if (dh == null)
                {
                    TDCMessageBox.Show("Không tìm thấy đơn hàng với mã: " + maCode, "Không tìm thấy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dh.IdKhachHang.HasValue)
                {
                    var kh = DAL_KhachHang.Instance.LayTheoId(dh.IdKhachHang.Value);
                    _returnKH = kh;
                    lblTenKHTra.Text = $"Khách: {kh?.HoTen ?? "Khách vãng lai"}";
                    lblSoDuViTra.Text = "";
                }
                else
                {
                    _returnKH = null;
                    lblTenKHTra.Text = "Khách: Khách vãng lai (Biên lai: " + maCode + ")";
                    lblSoDuViTra.Text = "Hoàn cọc bằng TIỀN MẶT";
                    lblSoDuViTra.ForeColor = ThemeManager.WarningColor;
                }

                // Targeted query: Lấy CTDH của DonHang này, rồi lấy ThueDoChiTiet theo CTDH
                var ctdhOfDH = DAL_ChiTietDonHang.Instance.LoadByDonHang(dh.Id);
                var ctdhIds = ctdhOfDH.Select(x => x.Id).ToList();
                _dangThueList = ctdhIds.SelectMany(id => DAL_ThueDoChiTiet.Instance.LoadByCTDH(id))
                    .Where(x => x.TrangThaiCoc == AppConstants.TrangThaiCoc.ChuaHoan).ToList();

                BindDangThueGrid();

                if (_dangThueList.Count == 0)
                    TDCMessageBox.Show("Đơn hàng này không có đồ đang thuê (hoặc đã hoàn cọc hết).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                TDCMessageBox.Show("Lỗi tìm đơn hàng: " + ex.Message);
            }
        }

        private void LoadDangThue(int idKhachHang)
        {
            try
            {
                var donHangIds = DAL.DAL_DonHang.Instance.LoadDS()
                    .Where(x => x.IdKhachHang == idKhachHang)
                    .Select(x => x.Id)
                    .ToList();
                
                // Targeted batch query: Lấy tất cả CTDH của các DonHang, sau đó truy vấn ThueDoChiTiet ChuaHoan
                var allCtdh = donHangIds.SelectMany(dhId => DAL.DAL_ChiTietDonHang.Instance.LoadByDonHang(dhId)).ToList();
                var allCtdhIds = allCtdh.Select(x => x.Id).ToList();
                _dangThueList = DAL.DAL_ThueDoChiTiet.Instance.LoadChuaHoanByCTDHs(allCtdhIds);

                BindDangThueGrid();

                if (_dangThueList.Count == 0)
                    TDCMessageBox.Show("Khách này không có đồ đang thuê.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                TDCMessageBox.Show("Lỗi tải danh sách thuê: " + ex.Message);
            }
        }

        /// <summary>
        /// Shared: Bind _dangThueList vào gridControlDangThue.
        /// Extracted từ LoadDangThue + LoadDangThueByMaDonHang để DRY.
        /// </summary>
        private void BindDangThueGrid()
        {
            var allSP = BUS_SanPham.Instance.LoadDS();
            var displayData = _dangThueList.Select(td =>
            {
                var sp = allSP.FirstOrDefault(x => x.Id == td.IdSanPham);
                return new
                {
                    td.Id,
                    TenSanPham = sp?.Ten ?? "---",
                    td.SoLuong,
                    td.SoTienCoc,
                    ThoiGianThue = td.ThoiGianBatDau.ToString("dd/MM HH:mm"),
                    TrangThai = td.TrangThaiCoc
                };
            }).ToList();

            gridControlDangThue.DataSource = displayData;
            gridViewDangThue.PopulateColumns();

            if (gridViewDangThue.Columns.Count > 0)
            {
                if (gridViewDangThue.Columns["Id"] != null) gridViewDangThue.Columns["Id"].Visible = false;
                if (gridViewDangThue.Columns["TenSanPham"] != null) gridViewDangThue.Columns["TenSanPham"].Caption = "Tên đồ thuê";
                if (gridViewDangThue.Columns["SoTienCoc"] != null)
                {
                    gridViewDangThue.Columns["SoTienCoc"].Caption = "Tiền cọc";
                    gridViewDangThue.Columns["SoTienCoc"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gridViewDangThue.Columns["SoTienCoc"].DisplayFormat.FormatString = "N0";
                }
                if (gridViewDangThue.Columns["ThoiGianThue"] != null) gridViewDangThue.Columns["ThoiGianThue"].Caption = "Thời gian thuê";
                if (gridViewDangThue.Columns["TrangThai"] != null) gridViewDangThue.Columns["TrangThai"].Visible = false;
            }
        }

        // ============================================================
        // RETURN: Full refund
        // ============================================================

        private void btnTraDu_Click(object sender, EventArgs e)
        {
            if (gridViewDangThue.FocusedRowHandle < 0)
            {
                TDCMessageBox.Show("Chọn một món đồ để trả!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idThueDo = (int)gridViewDangThue.GetRowCellValue(gridViewDangThue.FocusedRowHandle, "Id");
            var td = DAL_ThueDoChiTiet.Instance.LayTheoId(idThueDo);
            if (td == null) return;

            string tenSP = gridViewDangThue.GetRowCellValue(gridViewDangThue.FocusedRowHandle, "TenSanPham")?.ToString() ?? "";
            decimal coc = td.SoTienCoc;

            // Tính tổng thời gian
            DateTime thoiGianTra = DateTime.Now;
            TimeSpan duration = thoiGianTra - td.ThoiGianBatDau;
            int tongPhut = (int)Math.Ceiling(duration.TotalMinutes);
            if (tongPhut < 0) tongPhut = 0;

            // 1. TÍNH TỔNG TIỀN THUÊ THỰC TẾ
            decimal tongTienThueThucTe = BUS_BangGia.Instance.TinhTienThueTheoPhut(td.IdSanPham, td.ThoiGianBatDau, tongPhut);

            // 2. [VÁ BUG KẾ TOÁN]: Tính phần chênh lệch lố giờ
            // YÊU CẦU: Bác PHẢI có cột TienThueDaThu trong bảng ThueDoChiTiet
            decimal tienThueDaThu = td.TienThueDaThu; 
            decimal tienPhatSinhLoGio = Math.Max(0, tongTienThueThucTe - tienThueDaThu);

            // 3. TIỀN HOÀN = Cọc - Tiền Lố Giờ
            decimal tienHoan = Math.Max(0, coc - tienPhatSinhLoGio);

            string msg = string.Format(
                "XÁC NHẬN TRẢ ĐỒ [{0}]\n\n" +
                "- Bắt đầu: {1:HH:mm}\n" +
                "- Kết thúc: {2:HH:mm}\n" +
                "- Tổng thời gian: {3} phút\n" +
                "------------------------------\n" +
                "- Tổng tiền thuê: {4:N0}đ\n" +
                "- Đã trả trước: {5:N0}đ\n" +
                "- Phát sinh thêm: {6:N0}đ\n" +
                "------------------------------\n" +
                "=> SỐ TIỀN HOÀN CỌC: {7:N0}đ",
                tenSP, td.ThoiGianBatDau, thoiGianTra, tongPhut, tongTienThueThucTe, tienThueDaThu, tienPhatSinhLoGio, tienHoan);

            if (TDCMessageBox.Show(msg, "Tính tiền Dai Nam", MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes) 
                return;

            if (ET.SessionManager.CurrentUser == null || ET.SessionManager.CurrentUser.Id <= 0)
            {
                TDCMessageBox.Show("Phiên làm việc không hợp lệ! Vui lòng khởi động lại ứng dụng để đăng nhập.", "Lỗi hệ thống");
                Application.Restart(); Environment.Exit(0); return;
            }

            int idNV = ET.SessionManager.CurrentUser.Id;
            
            // GỌI HÀM BUS TRẢ ĐỒ VỚI SỐ TIỀN LỐ GIỜ
            var result = BUS_ThueDo.Instance.ReturnItem(idThueDo, tienPhatSinhLoGio > 0, tienPhatSinhLoGio, idNV);

            if (result.IsSuccess)
            {
                TDCMessageBox.Show("✅ Trả đồ thành công! Đã hoàn tiền.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearCustomerTra(); 
            }
            else
            {
                TDCMessageBox.Show("❌ " + result.ErrorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // RETURN: With penalty
        // ============================================================

        private void btnBaoHong_Click(object sender, EventArgs e)
        {
            if (gridViewDangThue.FocusedRowHandle < 0)
            {
                TDCMessageBox.Show("Chọn một món đồ để báo hỏng/mất!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idThueDo = (int)gridViewDangThue.GetRowCellValue(gridViewDangThue.FocusedRowHandle, "Id");
            var td = DAL_ThueDoChiTiet.Instance.LayTheoId(idThueDo);
            if (td == null) return;

            string tenSP = gridViewDangThue.GetRowCellValue(gridViewDangThue.FocusedRowHandle, "TenSanPham")?.ToString() ?? "";
            decimal coc = td.SoTienCoc;

            // 1. TÍNH TIỀN LỐ GIỜ (Y chang nút Trả đủ)
            DateTime thoiGianTra = DateTime.Now;
            TimeSpan duration = thoiGianTra - td.ThoiGianBatDau;
            int tongPhut = (int)Math.Ceiling(duration.TotalMinutes);
            if (tongPhut < 0) tongPhut = 0;

            decimal tongTienThueThucTe = BUS_BangGia.Instance.TinhTienThueTheoPhut(td.IdSanPham, td.ThoiGianBatDau, tongPhut);

            decimal tienThueDaThu = td.TienThueDaThu; 
            decimal tienPhatSinhLoGio = Math.Max(0, tongTienThueThucTe - tienThueDaThu);

            // 2. NHẬP TIỀN ĐỀN ĐỒ (PHẠT HƯ HỎNG)
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                $"Nhập số tiền ĐỀN ĐỒ cho [{tenSP}]:\n(Chưa bao gồm {tienPhatSinhLoGio:N0}đ phí lố giờ)",
                "Báo hỏng / Mất đồ", coc.ToString("0"));

            if (string.IsNullOrWhiteSpace(input)) return;

            decimal tienPhatHuHong;
            if (!decimal.TryParse(input, out tienPhatHuHong) || tienPhatHuHong < 0)
            {
                TDCMessageBox.Show("Số tiền đền không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. TỔNG CHI PHÍ = Lố Giờ + Đền Đồ
            decimal tongChiPhiPhatSinh = tienPhatSinhLoGio + tienPhatHuHong;
            decimal tienHoan = Math.Max(0, coc - tongChiPhiPhatSinh);
            string warnText = tongChiPhiPhatSinh > coc 
                ? $"\n\n⚠️ VƯỢT CỌC: Cần thu thêm TIỀN MẶT {(tongChiPhiPhatSinh - coc):N0}đ!" : "";

            string msg = string.Format(
                "XÁC NHẬN BÁO HỎNG & TRẢ ĐỒ [{0}]\n\n" +
                "- Tiền cọc đã thu: {1:N0}đ\n" +
                "------------------------------\n" +
                "- Phí lố giờ: {2:N0}đ\n" +
                "- Phí đền đồ: {3:N0}đ\n" +
                "- Tổng chi phí trừ cọc: {4:N0}đ\n" +
                "------------------------------\n" +
                "=> SỐ TIỀN HOÀN LẠI: {5:N0}đ{6}",
                tenSP, coc, tienPhatSinhLoGio, tienPhatHuHong, tongChiPhiPhatSinh, tienHoan, warnText);

            if (TDCMessageBox.Show(msg, "Xác nhận báo hỏng", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) 
                return;

            if (ET.SessionManager.CurrentUser == null || ET.SessionManager.CurrentUser.Id <= 0)
            {
                TDCMessageBox.Show("Phiên làm việc không hợp lệ! Vui lòng khởi động lại ứng dụng để đăng nhập.", "Lỗi hệ thống");
                Application.Restart(); Environment.Exit(0); return;
            }

            int idNV = ET.SessionManager.CurrentUser.Id;
            
            // GỬI TỔNG CHI PHÍ XUỐNG HÀM BUS (Code BUS hiện tại đã thiết kế xử lý trừ chung một cục Phạt)
            var result = BUS_ThueDo.Instance.ReturnItem(idThueDo, tongChiPhiPhatSinh > 0, tongChiPhiPhatSinh, idNV);

            if (result.IsSuccess)
            {
                string successMsg = "✅ Đã xử lý báo hỏng thành công.";
                if (!string.IsNullOrEmpty(result.ErrorMessage)) successMsg += "\n\n" + result.ErrorMessage; 
                TDCMessageBox.Show(successMsg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearCustomerTra(); 
            }
            else
            {
                TDCMessageBox.Show("❌ " + result.ErrorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool IsFuzzyMatchTienCoc(string input)
        {
            if (string.IsNullOrEmpty(input)) return false;
            string normalized = input.ToLower().Trim().Replace(" ", "");
            // Match phrases often used by staff: tiencoc, tiencọc, deposit, datcoc
            string[] patterns = { "tiencoc", "tiencọc", "deposit", "datcoc", "tiềncọc" };
            return patterns.Any(p => normalized.Contains(p));
        }

       
    }

    // ============================================================
    // HELPER CLASSES
    // ============================================================

    public class ComboItem
    {
        public string Text { get; set; }
        public int Value { get; set; }
        public override string ToString() => Text;
    }
}


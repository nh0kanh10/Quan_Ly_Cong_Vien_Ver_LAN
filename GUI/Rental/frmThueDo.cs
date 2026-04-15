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
        #region Khai báo biến toàn cục
        private List<RentalCartItem> _cart = new List<RentalCartItem>();
        private ET_KhachHang _selectedKH = null;
        private ET_ViDienTu _selectedVi = null;
        private int _currentIdKhuVuc = -1;
        private List<ET_SanPham> _allRentalProducts = new List<ET_SanPham>();

        // Cache tiền cọc theo SP (load 1 lần thay vì query lại mỗi lúc hiển thị)
        private Dictionary<int, decimal> _tienCocCache = new Dictionary<int, decimal>();

        // State cho tab Nhận trả
        private ET_KhachHang _returnKH = null;
        private List<ET_ThueDoChiTiet> _dangThueList = new List<ET_ThueDoChiTiet>();
        #endregion

        #region Khởi tạo Form
        public frmThueDo()
        {
            InitializeComponent();
            ApplyStyles();
            LoadTramChoThue();
            SetupGridColumns();
            RefreshCartDisplay();

            gridControlSanPham.BringToFront();
            gridControlDangThue.BringToFront();

            InitIcons();
            ApplyStyles();
            ApplyPermissions();
        }

        private void frmThueDo_Load(object sender, EventArgs e)
        {
            dtpTuNgay.DateTime = DateTime.Now.Date;
            dtpDenNgay.DateTime = DateTime.Now.Date;
            BtnXemChuaTra_Click(null, null);

            gridViewChuaTra.PopupMenuShowing += GridViewChuaTra_PopupMenuShowing;

            txtRfidGiao.Focus();
        }
        #endregion

        #region Phân quyền và giao diện
        public void ApplyPermissions()
        {
            bool canManage = true; 
            btnXacNhanTra.Enabled = canManage;
        }

        public void ApplyStyles()
        {
            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleDevExpressGrid(gridControlSanPham);
            ThemeManager.StyleDevExpressGrid(gridControlGioHang);
            ThemeManager.StyleDevExpressGrid(gridControlDangThue);

            lblTongThue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular);
            lblTongThue.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);

            lblTongCoc.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            lblTongCoc.ForeColor = System.Drawing.Color.FromArgb(245, 158, 11);

            lblTongCong.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            lblTongCong.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
        }

        public void InitIcons()
        {
            btnThanhToanRfid.Image = IconHelper.GetBitmap(IconChar.Bolt, Color.White, 18);
            btnThanhToanTienMat.Image = IconHelper.GetBitmap(IconChar.MoneyBillWave, Color.White, 18);
            btnHuyGiao.Image = IconHelper.GetBitmap(IconChar.Xmark, Color.White, 16);
            btnXacNhanTra.Image = IconHelper.GetBitmap(IconChar.CheckDouble, Color.White, 18);
        }

        public void LoadData()
        {
            LoadSanPhamThue();
        }
        #endregion

        #region Chọn trạm cho thuê (đổi trạm sẽ xóa giỏ hàng)
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
            gridViewSanPham.Columns.Clear();
            gridViewSanPham.PopulateColumns();

            gridViewGioHang.Columns.Clear();
            gridViewGioHang.PopulateColumns();

            gridViewDangThue.Columns.Clear();
            gridViewDangThue.PopulateColumns();
        }

        // Đổi trạm → xóa giỏ hàng (tránh lẫn SP giữa các trạm)
        private void cboTramChoThue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTramChoThue.SelectedItem is ComboItem item)
            {
                _currentIdKhuVuc = item.Value;
                LoadSanPhamThue();
                ClearCart();
            }
        }
        #endregion

        #region Load danh sách sản phẩm cho thuê theo trạm
        // Giá thuê lấy theo bảng giá THỜI ĐIỂM hiện tại (ngày thường / cuối tuần / giờ cao điểm)
        // Tiền cọc KHÔNG đổi theo ngày — cố định trong bảng giá
        private void LoadSanPhamThue()
        {
            try
            {
                var allSP = BUS_SanPham.Instance.LoadDS()
                    .Where(x => x.LoaiSanPham == AppConstants.LoaiSanPham.Thue
                             && x.TrangThai == AppConstants.TrangThaiSanPham.DangBan
                             && !x.IsDeleted)
                    .ToList();

                if (_currentIdKhuVuc > 0)
                    allSP = allSP.Where(x => x.IdKhuVuc == _currentIdKhuVuc).ToList();

                _allRentalProducts = allSP;

                _tienCocCache.Clear();
                foreach (var sp in allSP)
                {
                    _tienCocCache[sp.Id] = BUS_BangGia.Instance.GetTienCoc(sp.Id);
                }

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
                    TienThue = BUS_BangGia.Instance.GetDynamicPrice(sp.Id, DateTime.Now),
                    TienCoc = _tienCocCache.ContainsKey(sp.Id) ? _tienCocCache[sp.Id] : BUS_BangGia.Instance.GetTienCoc(sp.Id),
                    sp.MoTa
                }).ToList();

            gridControlSanPham.DataSource = filtered;
        }
        #endregion

        #region Thêm sản phẩm vào giỏ thuê (nhấp đúp)
        private void gridViewSanPham_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewSanPham.FocusedRowHandle < 0) return;

            int idSP = (int)gridViewSanPham.GetRowCellValue(gridViewSanPham.FocusedRowHandle, "Id");
            var sp = _allRentalProducts.FirstOrDefault(x => x.Id == idSP);
            if (sp == null) return;

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
        #endregion

        #region Quẹt thẻ RFID nhận diện khách hàng (dùng chung cho Giao đồ và Trả đồ)
        // Chuỗi nhận diện: ThẻRFID → ViĐiệnTử → KháchHàng
        // Nếu bất kỳ mắt xích nào đứt → báo lỗi cụ thể cho nhân viên biết điểm nào hỏng
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

                    LoadDangThue(kh.Id);
                }
            }
            catch (Exception ex)
            {
                TDCMessageBox.Show("Lỗi tìm khách: " + ex.Message);
            }
        }
        #endregion

        #region Thanh toán cho thuê (RFID trừ ví / Tiền mặt thu tay)
        // Tổng thanh toán = TiềnThuê (KHÔNG hoàn) + TiềnCọc (SẼ hoàn khi trả đủ)
        // Mã biên lai tự sinh "DT..." → copy vào clipboard → khách giữ để trả đồ
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

            var dh = new ET_DonHang
            {
                MaCode = "DT" + DateTime.Now.ToString("yyMMddHHmmss") + Guid.NewGuid().ToString().Substring(0, 4).ToUpper(),
                IdKhachHang = _selectedKH?.Id,
                ThoiGian = DateTime.Now,
                TongTien = tongThue, 
                TrangThai = AppConstants.TrangThaiDonHang.DaThanhToan,
                CreatedAt = DateTime.Now,
                CreatedBy = idNhanVien
            };

            int idKhoXuLy = ET.SessionManager.CurrentIdKhuVuc ?? 1;
            var result = BUS_ThueDo.Instance.RentMultipleItems(dh, _cart.ToList(), phuongThuc, idNhanVien, idKhoXuLy);

            if (result.IsSuccess)
            {
                System.Windows.Forms.Clipboard.SetText(dh.MaCode);

                TDCMessageBox.Show(
                    $"Thuê đồ thành công!\n\n" +
                    $"MÃ TRẢ ĐỒ: {dh.MaCode}\n" +
                    $"(Mã này đã được copy sẵn, hãy dán/in để khách dùng khi trả đồ)\n\n" +
                    $"Phương thức: {phuongThuc}", 
                    "Thanh Toán Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearCart();
                ClearCustomerGiao();
            }
            else
            {
                TDCMessageBox.Show("[Lỗi hệ thống]" + result.ErrorMessage, "Lỗi Thanh Toán", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            txtRfidGiao.Clear(); 
            txtRfidGiao.Focus();
        }

        private void ClearCustomerTra()
        {
            _returnKH = null;
            lblTenKHTra.Text = "Khách: ---";
            lblSoDuViTra.Text = "Số dư ví: ---";
            gridControlDangThue.DataSource = null;
            txtRfidTra.Clear(); 
            txtRfidTra.Focus();
        }
        #endregion

        #region Tab Nhận trả — Tìm đồ đang thuê (quẹt RFID hoặc quét mã biên lai)
        // Hai luồng trả đồ:
        //   (1) Khách RFID: quẹt thẻ → tìm TẤT CẢ đơn thuê chưa hoàn cọc của khách
        //   (2) Khách vãng lai: quét mã biên lai DT-xxx → tìm theo đơn hàng cụ thể

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

        // Quét Mã Vạch Biên Lai cho khách vãng lai (không có thẻ RFID)
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

        // Tìm đơn hàng bằng MaCode trên biên lai giấy → load chi tiết thuê chưa hoàn cọc
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

                // Lấy CTDH → ThueDoChiTiet chưa hoàn cọc
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

        // Tìm TẤT CẢ đồ đang thuê của 1 khách hàng (qua RFID)
        private void LoadDangThue(int idKhachHang)
        {
            try
            {
                var donHangIds = DAL.DAL_DonHang.Instance.LoadDS()
                    .Where(x => x.IdKhachHang == idKhachHang)
                    .Select(x => x.Id)
                    .ToList();
                
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

        // Gộp các phiên thuê cùng SP thành 1 dòng (hiện tổng SL đang thuê)
        // Cột "Khách Trả (SL)" và "Báo Mất (SL)" cho nhân viên nhập
        private void BindDangThueGrid()
        {
            var allSP = BUS_SanPham.Instance.LoadDS();
            var groupedData = _dangThueList
                .GroupBy(x => x.IdSanPham)
                .Select(g => new ET.ET_ThuHoiView
                {
                    IdSanPham = g.Key,
                    TenSanPham = allSP.FirstOrDefault(sp => sp.Id == g.Key)?.Ten ?? "---",
                    SoLuongDaThue = g.Count(), 
                    SoLuongChuaTra = g.Count(),
                    TraLanNay = 0,
                    BaoMat = 0
                }).ToList();

            gridControlDangThue.DataSource = groupedData;
            gridViewDangThue.PopulateColumns();

            if (gridViewDangThue.Columns.Count > 0)
            {
                if (gridViewDangThue.Columns["IdSanPham"] != null) gridViewDangThue.Columns["IdSanPham"].Visible = false;
                
                if (gridViewDangThue.Columns["TenSanPham"] != null) 
                {
                    gridViewDangThue.Columns["TenSanPham"].Caption = "Tên đồ thuê";
                    gridViewDangThue.Columns["TenSanPham"].OptionsColumn.AllowEdit = false;
                }
                
                if (gridViewDangThue.Columns["SoLuongDaThue"] != null) gridViewDangThue.Columns["SoLuongDaThue"].Visible = false;
                
                if (gridViewDangThue.Columns["SoLuongChuaTra"] != null)
                {
                    gridViewDangThue.Columns["SoLuongChuaTra"].Caption = "SL Đang Thuê";
                    gridViewDangThue.Columns["SoLuongChuaTra"].OptionsColumn.AllowEdit = false;
                }

                DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinEditTra = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
                spinEditTra.MinValue = 0;
                spinEditTra.MaxValue = 999;
                spinEditTra.IsFloatValue = false;
                gridControlDangThue.RepositoryItems.Add(spinEditTra);
                
                DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinEditMat = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
                spinEditMat.MinValue = 0;
                spinEditMat.MaxValue = 999;
                spinEditMat.IsFloatValue = false;
                gridControlDangThue.RepositoryItems.Add(spinEditMat);

                if (gridViewDangThue.Columns["TraLanNay"] != null)
                {
                    gridViewDangThue.Columns["TraLanNay"].Caption = "Khách Trả (SL)";
                    gridViewDangThue.Columns["TraLanNay"].ColumnEdit = spinEditTra;
                    gridViewDangThue.Columns["TraLanNay"].AppearanceCell.BackColor = System.Drawing.Color.FromArgb(240, 253, 244);
                }

                if (gridViewDangThue.Columns["BaoMat"] != null)
                {
                    gridViewDangThue.Columns["BaoMat"].Caption = "Báo Hỏng/Mất (SL)";
                    gridViewDangThue.Columns["BaoMat"].ColumnEdit = spinEditMat;
                    gridViewDangThue.Columns["BaoMat"].AppearanceCell.BackColor = System.Drawing.Color.FromArgb(254, 242, 242);
                }
                
                gridViewDangThue.OptionsBehavior.Editable = true;
            }
        }
        #endregion

        #region Xác nhận trả đồ (F12 trả đủ / nhập tay / báo mất → popup tính phạt)
        // F12 = "Trả hết" tự điền SL trả = SL đang thuê cho tất cả dòng
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F12)
            {
                btnTraDu_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnTraDu_Click(object sender, EventArgs e)
        {
            var dataSource = gridControlDangThue.DataSource as List<ET.ET_ThuHoiView>;
            if (dataSource == null) return;

            foreach (var item in dataSource)
            {
                item.TraLanNay = item.SoLuongChuaTra;
                item.BaoMat = 0;
            }
            gridControlDangThue.RefreshDataSource();
        }

        // === XỬ LÝ HOÀN CỌC VÀ PHẠT MẤT ĐỒ ===
        // Trả đủ: hoàn 100% cọc (vào ví nếu RFID, tiền mặt nếu vãng lai)
        // Báo mất: mở popup nhập tiền phạt → cọc hoàn = tổng cọc - phạt
        // Nếu nhân viên bỏ trống popup phạt → hủy toàn bộ thao tác (an toàn)
        private void btnXacNhanTra_Click(object sender, EventArgs e)
        {
            if (ET.SessionManager.CurrentUser == null || ET.SessionManager.CurrentUser.Id <= 0)
            {
                TDCMessageBox.Show("Phiên làm việc không hợp lệ! Vui lòng khởi động lại ứng dụng để đăng nhập.", "Lỗi hệ thống");
                Application.Restart(); Environment.Exit(0); return;
            }

            int idNV = ET.SessionManager.CurrentUser.Id;

            gridViewDangThue.PostEditor();
            gridViewDangThue.UpdateCurrentRow();
            
            var dataSource = gridControlDangThue.DataSource as List<ET.ET_ThuHoiView>;
            if (dataSource == null) return;

            var dsXuLy = dataSource.Where(x => x.TraLanNay > 0 || x.BaoMat > 0).ToList();
            if (dsXuLy.Count == 0)
            {
                TDCMessageBox.Show("Vui lòng nhập số lượng [Trả] hoặc [Báo Mất] ít nhất 1 món đồ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra: SL trả + SL mất không được vượt quá SL đang thuê
            bool coLoi = false;
            string thongBaoLoi = "";

            foreach (var item in dsXuLy)
            {
                if (item.TraLanNay + item.BaoMat > item.SoLuongChuaTra)
                {
                    coLoi = true;
                    thongBaoLoi += $"- [{item.TenSanPham}]: Số lượng trả/mất ({item.TraLanNay + item.BaoMat}) vượt quá số đang thuê ({item.SoLuongChuaTra})\n";
                }
            }

            if (coLoi)
            {
                TDCMessageBox.Show($"Lỗi số lượng:\n{thongBaoLoi}", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // === POPUP NHẬP TIỀN PHẠT cho từng SP bị mất ===
            // Mặc định = tiền cọc của món mất. Nhân viên có thể điều chỉnh cao hơn hoặc thấp hơn.
            // Nếu hủy popup → hủy toàn bộ thao tác trả (đảm bảo không mất tiền nhầm)
            var phiDenBuTheoSP = new Dictionary<int, decimal>();
            
            foreach (var item in dsXuLy)
            {
                if (item.BaoMat > 0)
                {
                    var cacRecordCuaSanPham = _dangThueList.Where(x => x.IdSanPham == item.IdSanPham).ToList();
                    decimal tongTienLoGioMoPhong = 0;
                    decimal tongTienCocCuaBaoMat = 0;
                    
                    // Tính thời gian thuê thực tế và phí lố giờ cho các món bị mất
                    for (int i = item.TraLanNay; i < item.TraLanNay + item.BaoMat && i < cacRecordCuaSanPham.Count; ++i)
                    {
                        var td = cacRecordCuaSanPham[i];
                        TimeSpan duration = DateTime.Now - td.ThoiGianBatDau;
                        int tongPhut = Math.Max(0, (int)Math.Ceiling(duration.TotalMinutes));
                        decimal tongTienThueThucTe = BUS_BangGia.Instance.TinhTienThueTheoPhut(td.IdSanPham, td.ThoiGianBatDau, tongPhut);
                        tongTienLoGioMoPhong += Math.Max(0, tongTienThueThucTe - td.TienThueDaThu);
                        tongTienCocCuaBaoMat += td.SoTienCoc;
                    }

                    string input = Microsoft.VisualBasic.Interaction.InputBox(
                        $"Khách báo mất {item.BaoMat} cái [{item.TenSanPham}].\n\n" +
                        $"Tổng tiền cọc của {item.BaoMat} món này: {tongTienCocCuaBaoMat:N0}đ\n" +
                        $"(Chưa tính {tongTienLoGioMoPhong:N0}đ phí lố giờ chung).\n\n" +
                        $"Vui lòng nhập TỔNG SỐ TIỀN ĐỀN BÙ (Phạt) cho {item.BaoMat} món này:",
                        "Báo hỏng / Mất đồ",
                        tongTienCocCuaBaoMat.ToString("0"));

                    if (!string.IsNullOrWhiteSpace(input) && decimal.TryParse(input, out decimal tongTienPhatHuHong) && tongTienPhatHuHong >= 0)
                    {
                        phiDenBuTheoSP[item.IdSanPham] = tongTienPhatHuHong;
                    }
                    else
                    {
                        TDCMessageBox.Show($"Lệnh xác nhận bị huỷ vì chưa cung cấp tiền phạt cho thao tác Báo mất {item.TenSanPham}.", "Huỷ thao tác", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            // Gửi toàn bộ xuống BUS thực thi Batch (hoàn cọc + tính phạt + cập nhật trạng thái)
            int idKhoXuLy = ET.SessionManager.CurrentIdKhuVuc ?? 1;
            var result = BUS_ThueDo.Instance.XacNhanThuHoiDoBatch(_dangThueList, dsXuLy, phiDenBuTheoSP, idNV, idKhoXuLy);

            if (!result.IsSuccess)
            {
                TDCMessageBox.Show(result.ErrorMessage, "Lỗi Thu Hồi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TDCMessageBox.Show(result.ErrorMessage, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            // Tự động load lại theo biên lai hoặc khách
            if (!string.IsNullOrEmpty(txtMaDonHang.Text))
            {
                LoadDangThueByMaDonHang(txtMaDonHang.Text);
            }
            else if (_returnKH != null)
            {
                LoadDangThue(_returnKH.Id);
            }
            else
            {
                _dangThueList.Clear();
                BindDangThueGrid();
            }
        }
        #endregion

        #region Giám sát phiên thuê chưa trả (lọc theo ngày, nhấp đúp để thu hồi)
        // Hiển thị tất cả phiên thuê chưa hoàn cọc trong khoảng ngày
        // Nhấp đúp 1 dòng → tự đẩy mã đơn sang panel Nhận Trả để tất toán ngay
        private bool IsFuzzyMatchTienCoc(string input)
        {
            if (string.IsNullOrEmpty(input)) return false;
            string normalized = input.ToLower().Trim().Replace(" ", "");
            string[] patterns = { "tiencoc", "tiencọc", "deposit", "datcoc", "tiềncọc" };
            return patterns.Any(p => normalized.Contains(p));
        }

        public void BtnXemChuaTra_Click(object sender, EventArgs e)
        {
            try
            {
                var list = BUS_ThueDo.Instance.GetDanhSachDonChuaTra(dtpTuNgay.DateTime, dtpDenNgay.DateTime);
                gridControlChuaTra.DataSource = list;
                gridViewChuaTra.PopulateColumns();
                
                if (gridViewChuaTra.Columns["IdThueDo"] != null) gridViewChuaTra.Columns["IdThueDo"].Visible = false;
                if (gridViewChuaTra.Columns["MaCode"] != null) gridViewChuaTra.Columns["MaCode"].Visible = false;
                if (gridViewChuaTra.Columns["TenKhachHang"] != null) gridViewChuaTra.Columns["TenKhachHang"].Visible = false;
                
                if (gridViewChuaTra.Columns["HeaderNhom"] != null) 
                {
                    gridViewChuaTra.Columns["HeaderNhom"].Caption = "Thông tin đơn (Nhấp đúp dòng con để check out)";
                    gridViewChuaTra.Columns["HeaderNhom"].GroupIndex = 0;
                }
                if (gridViewChuaTra.Columns["TenSanPham"] != null) gridViewChuaTra.Columns["TenSanPham"].Caption = "Mặt hàng";
                if (gridViewChuaTra.Columns["SoLuong"] != null) gridViewChuaTra.Columns["SoLuong"].Caption = "SL";
                if (gridViewChuaTra.Columns["ThoiGianThue"] != null) 
                {
                    gridViewChuaTra.Columns["ThoiGianThue"].Caption = "Lấy ra lúc";
                    gridViewChuaTra.Columns["ThoiGianThue"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    gridViewChuaTra.Columns["ThoiGianThue"].DisplayFormat.FormatString = "dd/MM HH:mm";
                }
                
                gridViewChuaTra.CollapseAllGroups();
                ThemeManager.StyleDevExpressGrid(gridControlChuaTra);
                
                int distinctOrders = list.Select(x => x.MaCode).Distinct().Count();
                int itemsLeft = list.Sum(x => x.SoLuong);
                lbThongBao.Text = distinctOrders > 0 ? $" Đang có {distinctOrders} phiên chờ ({itemsLeft} món chưa về)" : " Đã thu hồi sạch khu vui chơi!";
                lbThongBao.ForeColor = distinctOrders > 0 ? System.Drawing.Color.FromArgb(220, 38, 38) : System.Drawing.Color.FromArgb(16, 185, 129);
            }
            catch (Exception ex)
            {
                TDCMessageBox.Show("Lỗi tải danh sách chưa trả: " + ex.Message);
            }
        }

        public void GridViewChuaTra_DoubleClick(object sender, EventArgs e)
        {
            var rowHandle = gridViewChuaTra.FocusedRowHandle;
            if (rowHandle < 0 && gridViewChuaTra.IsGroupRow(rowHandle))
            {
                return;
            }
            
            var row = gridViewChuaTra.GetFocusedRow() as ET.ET_DanhSachChuaTraView;
            if (row != null && !string.IsNullOrEmpty(row.MaCode))
            {
                this.txtMaDonHang.Text = row.MaCode;
                LoadDangThueByMaDonHang(row.MaCode);
            }
        }

        private void GridViewChuaTra_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
            {
                var rowHandle = e.HitInfo.RowHandle;
                if (rowHandle >= 0 && !gridViewChuaTra.IsGroupRow(rowHandle))
                {
                    DevExpress.Utils.Menu.DXMenuItem item = new DevExpress.Utils.Menu.DXMenuItem("Trả thông tin đơn này");
                    item.Click += (s, args) =>
                    {
                        var row = gridViewChuaTra.GetRow(rowHandle) as ET.ET_DanhSachChuaTraView;
                        if (row != null && !string.IsNullOrEmpty(row.MaCode))
                        {
                            this.txtMaDonHang.Text = row.MaCode;
                            LoadDangThueByMaDonHang(row.MaCode);
                        }
                    };
                    e.Menu.Items.Add(item);
                }
            }
        }
        #endregion
    }

    #region Lớp phụ trợ (ComboItem cho dropdown trạm)
    public class ComboItem
    {
        public string Text { get; set; }
        public int Value { get; set; }
        public override string ToString() => Text;
    }
    #endregion
}

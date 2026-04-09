using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BUS;
using ET;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;

namespace GUI
{
    public partial class frmDoanKhach : Form,IBaseForm
    {
        private List<ET_DoanKhach> _dsDoan;
        private ET_DoanKhach _selectedDoan;
        private List<ET_DoanKhach_DichVu> _dsDichVu;

        public frmDoanKhach()
        {
            InitializeComponent();
        }

        private void frmDoanKhach_Load(object sender, EventArgs e)
        {
            ThemeManager.ApplyTheme(this);
            LoadComboTrangThai();
            ClearForm();
            LoadDanhSachDoan();
        }

        private void GridViewDoan_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle < 0) return;
            _selectedDoan = gridViewDoan.GetRow(e.RowHandle) as ET_DoanKhach;
            if (_selectedDoan != null)
            {
                FillForm(_selectedDoan);
                LoadDichVuDoan(_selectedDoan.Id);
            }
        }

        // ══════════════════════════════════════════════════════════════
        //  IBASEFORM IMPLEMENTATION
        // ══════════════════════════════════════════════════════════════

        public void LoadData()
        {
            LoadDanhSachDoan();
        }

        public void InitIcons()
        {
            // Icons are configured in Designer or via properties
        }

        public void ApplyStyles()
        {
            ThemeManager.ApplyTheme(this);
        }

        public void ApplyPermissions()
        {
            // TODO: Hide/Disable buttons based on user permissions
        }

        // 
        //  LOAD DATA
        // 

        private void LoadDanhSachDoan()
        {
            _dsDoan = BUS_DoanKhach.Instance.LoadDS();
            gridControlDoan.DataSource = _dsDoan;

            var v = gridViewDoan;
            v.OptionsBehavior.Editable = false;
            v.OptionsFind.AlwaysVisible = true;

            string[] hide = { "Id", "IsDeleted", "CreatedBy", "CreatedAt", "UpdatedAt", "IdCombo", "TenCombo", "IsBookingValid", "MaSoThue" };
            foreach (string col in hide)
                if (v.Columns[col] != null) v.Columns[col].Visible = false;

            if (v.Columns["MaBooking"] != null) v.Columns["MaBooking"].Caption = "Mã Booking";
            if (v.Columns["TenDoan"] != null) v.Columns["TenDoan"].Caption = "Tên Đoàn";
            if (v.Columns["NguoiDaiDien"] != null) v.Columns["NguoiDaiDien"].Caption = "Người Đại Diện";
            if (v.Columns["DienThoaiLienHe"] != null) v.Columns["DienThoaiLienHe"].Caption = "SĐT";
            if (v.Columns["ChietKhau"] != null) { v.Columns["ChietKhau"].Caption = "CK %"; v.Columns["ChietKhau"].DisplayFormat.FormatString = "N1"; }
            if (v.Columns["SoLuongKhach"] != null) v.Columns["SoLuongKhach"].Caption = "Số Khách";
            if (v.Columns["NgayDen"] != null) { v.Columns["NgayDen"].Caption = "Ngày Đến"; v.Columns["NgayDen"].DisplayFormat.FormatString = "dd/MM/yyyy"; }
            if (v.Columns["NgayDi"] != null) { v.Columns["NgayDi"].Caption = "Ngày Đi"; v.Columns["NgayDi"].DisplayFormat.FormatString = "dd/MM/yyyy"; }
            if (v.Columns["TrangThai"] != null) v.Columns["TrangThai"].Caption = "Trạng Thái";

            v.BestFitColumns();
        }

        private void LoadComboTrangThai()
        {
            cboTrangThai.Properties.Items.Clear();
            cboTrangThai.Properties.Items.Add(AppConstants.TrangThaiDoanKhach.DaDat);
            cboTrangThai.Properties.Items.Add(AppConstants.TrangThaiDoanKhach.DangPhucVu);
            cboTrangThai.Properties.Items.Add(AppConstants.TrangThaiDoanKhach.DaXuatVe);
            cboTrangThai.Properties.Items.Add(AppConstants.TrangThaiDoanKhach.HetHan);
            cboTrangThai.Properties.Items.Add(AppConstants.TrangThaiDoanKhach.DaHuy);
            cboTrangThai.SelectedIndex = 0;
        }

        // ══════════════════════════════════════════════════════════════
        //  GRID SELECTION -> LOAD FORM + DỊCH VỤ
        // ══════════════════════════════════════════════════════════════

        private void gridViewDoan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0) { ClearForm(); return; }

            _selectedDoan = gridViewDoan.GetRow(e.FocusedRowHandle) as ET_DoanKhach;
            if (_selectedDoan == null) { ClearForm(); return; }

            FillForm(_selectedDoan);
            LoadDichVuDoan(_selectedDoan.Id);
        }

        private void FillForm(ET_DoanKhach doan)
        {
            txtTenDoan.Text = doan.TenDoan;
            txtNguoiDaiDien.Text = doan.NguoiDaiDien;
            txtSdt.Text = doan.DienThoaiLienHe;
            txtMaSoThue.Text = doan.MaSoThue;
            txtMaBooking.Text = doan.MaBooking ?? "";
            spnChietKhau.Value = doan.ChietKhau;
            spnSoLuongKhach.Value = doan.SoLuongKhach > 0 ? doan.SoLuongKhach : 1;
            dtpNgayDen.DateTime = doan.NgayDen ?? DateTime.Today;
            dtpNgayDi.DateTime = doan.NgayDi ?? DateTime.Today.AddDays(1);
            cboTrangThai.EditValue = doan.TrangThai ?? AppConstants.TrangThaiDoanKhach.DaDat;

            grpDichVu.Enabled = true;

            // BẮT BỆNH 3: Lỗ hổng Logic Kế toán (State Violation)
            CheckFormState();
        }

        private void CheckFormState()
        {
            bool isReadOnly = _selectedDoan != null && 
                              (_selectedDoan.TrangThai == AppConstants.TrangThaiDoanKhach.DaHoanTat ||
                               _selectedDoan.TrangThai == AppConstants.TrangThaiDoanKhach.DaXuatVe);

            // Khóa control nhập liệu
            txtTenDoan.ReadOnly = isReadOnly;
            txtNguoiDaiDien.ReadOnly = isReadOnly;
            txtSdt.ReadOnly = isReadOnly;
            txtMaSoThue.ReadOnly = isReadOnly;
            spnChietKhau.ReadOnly = isReadOnly;
            spnSoLuongKhach.ReadOnly = isReadOnly;
            dtpNgayDen.ReadOnly = isReadOnly;
            dtpNgayDi.ReadOnly = isReadOnly;
            cboTrangThai.ReadOnly = isReadOnly;

            // Khóa chức năng Sửa/Xóa gốc
            btnSua.Enabled = !isReadOnly;
            btnXoa.Enabled = !isReadOnly;

            // Khóa chèn ép dịch vụ
            pnlDichVuButtons.Enabled = !isReadOnly;
            btnXuatHoaDon.Enabled = !isReadOnly;
        }

        private void ClearForm()
        {
            _selectedDoan = null;
            txtTenDoan.Text = "";
            txtNguoiDaiDien.Text = "";
            txtSdt.Text = "";
            txtMaSoThue.Text = "";
            txtMaBooking.Text = "";
            spnSoLuongKhach.Value = 0;
            spnChietKhau.Value = 0;
            dtpNgayDen.EditValue = DateTime.Now;
            dtpNgayDi.EditValue = DateTime.Now.AddDays(1);
            if (cboTrangThai.Properties.Items.Count > 0)
                cboTrangThai.SelectedIndex = 0;

            _selectedDoan = null;
            gridControlDichVu.DataSource = null;
            grpDichVu.Enabled = false;
            lblTongSauCK.Text = "0 đ";
            
            CheckFormState();
        }

        // ══════════════════════════════════════════════════════════════
        //  DỊCH VỤ PANEL (PHẢI)
        // ══════════════════════════════════════════════════════════════

        private void LoadDichVuDoan(int idDoan)
        {
            _dsDichVu = BUS_DoanKhach.Instance.LayDichVuTheoDoan(idDoan);
            gridControlDichVu.DataSource = _dsDichVu;

            var v = gridViewDichVu;
            v.OptionsBehavior.Editable = false;

            string[] hide = { "Id", "IdDoan", "IdCombo", "IdSanPham", "IdThamChieu", "IdChiTietDonHang", "LoaiDichVu" };
            foreach (string col in hide)
                if (v.Columns[col] != null) v.Columns[col].Visible = false;

            if (v.Columns["TenLoaiDichVu"] != null) { v.Columns["TenLoaiDichVu"].Caption = "Loại"; v.Columns["TenLoaiDichVu"].VisibleIndex = 0; }
            if (v.Columns["TenDichVu"] != null) { v.Columns["TenDichVu"].Caption = "Tên DV/SP"; v.Columns["TenDichVu"].VisibleIndex = 1; }
            if (v.Columns["TenCombo"] != null) { v.Columns["TenCombo"].Caption = "Combo"; v.Columns["TenCombo"].VisibleIndex = 2; }
            if (v.Columns["SoLuong"] != null) { v.Columns["SoLuong"].Caption = "SL"; v.Columns["SoLuong"].VisibleIndex = 3; }
            if (v.Columns["DonGia"] != null) { v.Columns["DonGia"].Caption = "Đơn giá"; v.Columns["DonGia"].DisplayFormat.FormatString = "N0"; v.Columns["DonGia"].VisibleIndex = 4; }
            if (v.Columns["ThanhTien"] != null) { v.Columns["ThanhTien"].Caption = "Thành tiền"; v.Columns["ThanhTien"].DisplayFormat.FormatString = "N0"; v.Columns["ThanhTien"].VisibleIndex = 5; }
            if (v.Columns["SoLuongDaDung"] != null) { v.Columns["SoLuongDaDung"].Caption = "Đã Dùng"; v.Columns["SoLuongDaDung"].VisibleIndex = 6; }
            if (v.Columns["SoLuongConLai"] != null) { v.Columns["SoLuongConLai"].Caption = "Còn Lại"; v.Columns["SoLuongConLai"].VisibleIndex = 7; }
            if (v.Columns["TenTrangThai"] != null) { v.Columns["TenTrangThai"].Caption = "Trạng thái"; v.Columns["TenTrangThai"].VisibleIndex = 8; }
            if (v.Columns["NgaySuDung"] != null) { v.Columns["NgaySuDung"].Caption = "Ngày SD"; v.Columns["NgaySuDung"].DisplayFormat.FormatString = "dd/MM/yyyy"; v.Columns["NgaySuDung"].VisibleIndex = 9; }
            if (v.Columns["GhiChu"] != null) { v.Columns["GhiChu"].Caption = "Ghi chú"; v.Columns["GhiChu"].VisibleIndex = 10; }
            if (v.Columns["TrangThai"] != null) v.Columns["TrangThai"].Visible = false;

            v.BestFitColumns();
            UpdateTotals();
        }

        private void UpdateTotals()
        {
            if (_selectedDoan == null || _dsDichVu == null) return;

            decimal tongTruocCK = _dsDichVu
                .Where(x => x.TrangThai != AppConstants.TrangThaiDichVuDoan.DaHuy)
                .Sum(x => x.ThanhTien);
            decimal tongSauCK = tongTruocCK * (1 - _selectedDoan.ChietKhau / 100m);

            lblTongTruocCK.Text = tongTruocCK.ToString("N0") + " đ";
            lblTongSauCK.Text = tongSauCK.ToString("N0") + " đ";
        }

        // ══════════════════════════════════════════════════════════════
        //  CRUD ĐOÀN
        // ══════════════════════════════════════════════════════════════

        private ET_DoanKhach ThuThapDuLieu()
        {
            return new ET_DoanKhach
            {
                TenDoan = txtTenDoan.Text.Trim(),
                NguoiDaiDien = txtNguoiDaiDien.Text.Trim(),
                DienThoaiLienHe = txtSdt.Text.Trim(),
                MaSoThue = txtMaSoThue.Text.Trim(),
                MaBooking = txtMaBooking.Text.Trim(),
                ChietKhau = spnChietKhau.Value,
                SoLuongKhach = (int)spnSoLuongKhach.Value,
                NgayDen = dtpNgayDen.DateTime.Date,
                NgayDi = dtpNgayDi.DateTime.Date,
                TrangThai = cboTrangThai.EditValue?.ToString() ?? AppConstants.TrangThaiDoanKhach.DaDat,
                CreatedBy = SessionManager.CurrentUser?.Id
            };
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTenDoan.Text = string.Empty;
            txtNguoiDaiDien.Text = string.Empty;
            txtSdt.Text = string.Empty;
            txtMaSoThue.Text = string.Empty;
            txtMaBooking.Text = string.Empty;
            spnSoLuongKhach.Value = 1;
            spnChietKhau.Value = 0;
            dtpNgayDen.EditValue = DateTime.Now;
            dtpNgayDi.EditValue = DateTime.Now.AddDays(1);
            if (cboTrangThai.Properties.Items.Count > 0)
                cboTrangThai.SelectedIndex = 0;
            
            _selectedDoan = null;
            gridControlDichVu.DataSource = null;
            grpDichVu.Enabled = false;
            lblTongTruocCK.Text = "0 đ";
            lblTongSauCK.Text = "0 đ";
            txtTenDoan.Focus();

            CheckFormState();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            var et = ThuThapDuLieu();

            // Auto-gen MaBooking nếu trống
            if (string.IsNullOrWhiteSpace(et.MaBooking))
                et.MaBooking = "BK-" + DateTime.Now.ToString("yyMMdd") + "-" + Guid.NewGuid().ToString().Substring(0, 4).ToUpper();

            var kq = BUS_DoanKhach.Instance.Them(et);
            if (kq.IsSuccess)
            {
                TDCMessageBox.Show("Thêm đoàn thành công!", "Thông báo");
                LoadDanhSachDoan();
            }
            else
                TDCMessageBox.Show(kq.ErrorMessage, "Lỗi");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_selectedDoan == null) { TDCMessageBox.Show("Chưa chọn đoàn!"); return; }
            var et = ThuThapDuLieu();
            et.Id = _selectedDoan.Id;
            et.MaBooking = _selectedDoan.MaBooking; // Giữ nguyên mã booking

            var kq = BUS_DoanKhach.Instance.Sua(et);
            if (kq.IsSuccess)
            {
                TDCMessageBox.Show("Cập nhật thành công!", "Thông báo");
                LoadDanhSachDoan();
            }
            else
                TDCMessageBox.Show(kq.ErrorMessage, "Lỗi");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_selectedDoan == null) { TDCMessageBox.Show("Chưa chọn đoàn!"); return; }
            if (TDCMessageBox.Show($"Xóa đoàn \"{_selectedDoan.TenDoan}\"?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            var kq = BUS_DoanKhach.Instance.Xoa(_selectedDoan.Id);
            if (kq.IsSuccess)
            {
                TDCMessageBox.Show("Đã xóa!", "Thông báo");
                LoadDanhSachDoan();
                ClearForm();
            }
            else
                TDCMessageBox.Show(kq.ErrorMessage, "Lỗi");
        }

        // ══════════════════════════════════════════════════════════════
        //  THÊM DỊCH VỤ
        // ══════════════════════════════════════════════════════════════

        private void btnThemCombo_Click(object sender, EventArgs e)
        {
            if (_selectedDoan == null) return;

            // Load danh sách Combo
            var combos = BUS_Combo.Instance.LoadDS();
            if (combos == null || combos.Count == 0) { TDCMessageBox.Show("Chưa có Combo nào trong hệ thống."); return; }

            using (var dlg = new frmChonDichVuDoanDialog("Combo", combos.Select(c => new DichVuItem { Id = c.Id, Ten = c.Ten, DonGia = c.Gia, IsCombo = true }).ToList()))
            {
                ThemeManager.ShowAsPopup(dlg);
                if (dlg.SelectedItem == null) return;

                var dv = new ET_DoanKhach_DichVu
                {
                    IdDoan = _selectedDoan.Id,
                    LoaiDichVu = AppConstants.LoaiDichVuDoan.Combo,
                    IdCombo = dlg.SelectedItem.Id,
                    SoLuong = dlg.SoLuong,
                    DonGia = dlg.SelectedItem.DonGia,
                    NgaySuDung = dlg.NgaySuDung,
                    GhiChu = dlg.GhiChu
                };
                var kq = BUS_DoanKhach.Instance.ThemDichVu(dv);
                if (kq.IsSuccess) LoadDichVuDoan(_selectedDoan.Id);
                else TDCMessageBox.Show(kq.ErrorMessage, "Lỗi");
            }
        }

        private void btnThemPhong_Click(object sender, EventArgs e)
        {
            if (_selectedDoan == null) return;

            // Load danh sách loại phòng (SanPham loại LuuTru)
            var phongList = BUS_SanPham.Instance.LoadDS()
                .Where(sp => sp.LoaiSanPham == AppConstants.LoaiSanPham.LuuTru && sp.TrangThai == AppConstants.TrangThaiSanPham.DangBan)
                .ToList();
            if (phongList.Count == 0) { TDCMessageBox.Show("Chưa có sản phẩm Lưu trú."); return; }

            using (var dlg = new frmChonDichVuDoanDialog("Phòng (Room Allotment)", phongList.Select(sp => new DichVuItem { Id = sp.Id, Ten = sp.Ten, DonGia = sp.DonGia }).ToList()))
            {
                ThemeManager.ShowAsPopup(dlg);
                if (dlg.SelectedItem == null) return;

                var dv = new ET_DoanKhach_DichVu
                {
                    IdDoan = _selectedDoan.Id,
                    LoaiDichVu = AppConstants.LoaiDichVuDoan.Phong,
                    IdSanPham = dlg.SelectedItem.Id,
                    SoLuong = dlg.SoLuong,
                    DonGia = dlg.SelectedItem.DonGia,
                    NgaySuDung = dlg.NgaySuDung,
                    GhiChu = dlg.GhiChu
                };
                var kq = BUS_DoanKhach.Instance.ThemDichVu(dv);
                if (kq.IsSuccess) LoadDichVuDoan(_selectedDoan.Id);
                else TDCMessageBox.Show(kq.ErrorMessage, "Lỗi");
            }
        }

        private void btnThemBanAn_Click(object sender, EventArgs e)
        {
            if (_selectedDoan == null) return;

            // Load nhà hàng (SanPham loại AnUong hoặc custom)
            var anUongList = BUS_SanPham.Instance.LoadDS()
                .Where(sp => sp.LoaiSanPham == AppConstants.LoaiSanPham.AnUong && sp.TrangThai == AppConstants.TrangThaiSanPham.DangBan)
                .ToList();
            if (anUongList.Count == 0) { TDCMessageBox.Show("Chưa có sản phẩm Ăn uống."); return; }

            using (var dlg = new frmChonDichVuDoanDialog("Bàn Ăn / Set Menu", anUongList.Select(sp => new DichVuItem { Id = sp.Id, Ten = sp.Ten, DonGia = sp.DonGia }).ToList()))
            {
                dlg.ShowPaxField = true;
                dlg.DefaultPax = _selectedDoan.SoLuongKhach;
                ThemeManager.ShowAsPopup(dlg);
                if (dlg.SelectedItem == null) return;

                var dv = new ET_DoanKhach_DichVu
                {
                    IdDoan = _selectedDoan.Id,
                    LoaiDichVu = AppConstants.LoaiDichVuDoan.BanAn,
                    IdSanPham = dlg.SelectedItem.Id,
                    SoLuong = dlg.SoLuong,
                    DonGia = dlg.SelectedItem.DonGia,
                    NgaySuDung = dlg.NgaySuDung,
                    GhiChu = dlg.GhiChu
                };
                var kq = BUS_DoanKhach.Instance.ThemDichVu(dv);
                if (kq.IsSuccess) LoadDichVuDoan(_selectedDoan.Id);
                else TDCMessageBox.Show(kq.ErrorMessage, "Lỗi");
            }
        }

        private void btnXoaDichVu_Click(object sender, EventArgs e)
        {
            if (_selectedDoan == null) return;
            int idx = gridViewDichVu.FocusedRowHandle;
            if (idx < 0) { TDCMessageBox.Show("Chưa chọn dịch vụ cần xóa!"); return; }

            var dv = gridViewDichVu.GetRow(idx) as ET_DoanKhach_DichVu;
            if (dv == null) return;

            if (dv.IdChiTietDonHang.HasValue)
            {
                TDCMessageBox.Show("Dịch vụ này đã được chốt hóa đơn, không thể xóa!", "Không hợp lệ");
                return;
            }

            if (TDCMessageBox.Show("Hủy dịch vụ này?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            var kq = BUS_DoanKhach.Instance.XoaDichVu(dv.Id);
            if (kq.IsSuccess) LoadDichVuDoan(_selectedDoan.Id);
            else TDCMessageBox.Show(kq.ErrorMessage, "Lỗi");
        }

        // ══════════════════════════════════════════════════════════════
        //  XUẤT HÓA ĐƠN / THU TIỀN
        // ══════════════════════════════════════════════════════════════

        private void btnXuatHoaDon_Click(object sender, EventArgs e)
        {
            if (_selectedDoan == null) return;

            if (TDCMessageBox.Show(
                $"Xuất hóa đơn cho đoàn \"{_selectedDoan.TenDoan}\"?\n\nTất cả dịch vụ chưa chốt sẽ được ghi nhận.",
                "Xác nhận xuất hóa đơn",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            int idNv = SessionManager.CurrentUser?.Id ?? 0;
            var result = BUS_DoanKhach.Instance.XuatHoaDon(_selectedDoan.Id, AppConstants.PhuongThucThanhToan.TienMat, idNv);

            if (result.IsSuccess)
            {
                TDCMessageBox.Show(result.ErrorMessage ?? "Xuất hóa đơn thành công!", "Hoàn tất");
                LoadDichVuDoan(_selectedDoan.Id);
            }
            else
            {
                TDCMessageBox.Show(result.ErrorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ══════════════════════════════════════════════════════════════
        //  COPY MÃ BOOKING -> CLIPBOARD
        // ══════════════════════════════════════════════════════════════

        private void btnCopyMaBooking_Click(object sender, EventArgs e)
        {
            string ma = txtMaBooking.Text.Trim();
            if (string.IsNullOrEmpty(ma))
            {
                TDCMessageBox.Show("Chưa có mã Booking để sao chép!", "Thông báo");
                return;
            }

            Clipboard.SetText(ma);

            // Visual feedback: đổi text nút tạm thời
            btnCopyMaBooking.Text = "✓";
            var timer = new Timer { Interval = 1500 };
            timer.Tick += (s, ev) => { btnCopyMaBooking.Text = "📋"; timer.Stop(); timer.Dispose(); };
            timer.Start();
        }
    }
}

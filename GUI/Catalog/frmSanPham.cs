using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BUS;
using ET;
using FontAwesome.Sharp;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;

namespace GUI
{
    public partial class frmSanPham : Form, IBaseForm
    {
        // ── State Machine ──
        private enum FormState { Browse, AddNew, Edit }
        private FormState _state = FormState.Browse;
        private ET_SanPham _current;

        public frmSanPham()
        {
            InitializeComponent();
            InitIcons();
            WireEvents();
            ApplyStyles();
            ApplyPermissions();
            
            // Xử lý docking grid để không bị trống phần dưới cùng
            gridQuyDoi.BringToFront();
            gridBangGia.BringToFront();
        }

        public void InitIcons()
        {
            btnThemMoi.Image = IconHelper.GetBitmap(IconChar.Plus, Color.FromArgb(30, 64, 175), 16);
            btnLuuTab1.Image = IconHelper.GetBitmap(IconChar.FloppyDisk, Color.White, 16);
            btnXoaSP.Image = IconHelper.GetBitmap(IconChar.TrashCan, Color.White, 16);
            btnThemQuyDoi.Image = IconHelper.GetBitmap(IconChar.Plus, Color.FromArgb(30, 64, 175), 14);
            btnXoaQuyDoi.Image = IconHelper.GetBitmap(IconChar.TrashCan, Color.FromArgb(244, 63, 94), 14);
            btnThemGia.Image = IconHelper.GetBitmap(IconChar.Plus, Color.FromArgb(30, 64, 175), 14);
            btnXoaGia.Image = IconHelper.GetBitmap(IconChar.TrashCan, Color.FromArgb(244, 63, 94), 14);
        }

        // ════════════════════════════════════════════════════
        //  INIT & LOAD
        // ════════════════════════════════════════════════════
        private void frmSanPham_Load(object sender, EventArgs e)
        {
            InitCombos();
            AddGuideLabels();
            LoadMasterGrid();
            SetState(FormState.Browse);
        }

        private void AddGuideLabels()
        {
            // Tab 2 guide
            var lblGuideQD = new Label
            {
                Text = "💡 Chọn đơn vị lớn, nhập hệ số quy đổi (bao nhiêu ĐVT nhỏ = 1 ĐVT lớn), giá bán riêng nếu có -> bấm [+ Thêm]",
                AutoSize = false, Dock = DockStyle.Bottom, Height = 22,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Italic),
                ForeColor = Color.FromArgb(100, 116, 139),
                TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(15, 0, 0, 0)
            };
            pnlQuyDoiInput.Controls.Add(lblGuideQD);
            pnlQuyDoiInput.Height = 75;

            // Tab 3 guide — flat pricing
            var lblGuideBG = new Label
            {
                Text = "💡 Nhập giá Ngày Thường, Cuối tuần, Ngày Lễ. Nếu sản phẩm thuê: bổ sung Tiền Cọc + cấu hình Block -> bấm [+ Thêm]",
                AutoSize = false, Dock = DockStyle.Bottom, Height = 22,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Italic),
                ForeColor = Color.FromArgb(100, 116, 139),
                TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(15, 0, 0, 0)
            };
            pnlBangGiaInput.Controls.Add(lblGuideBG);
            pnlBangGiaInput.Height = 115;
        }

        private void WireEvents()
        {
            this.Load += frmSanPham_Load;

            // Master grid selection
            gridViewMaster.FocusedRowChanged += OnMasterFocusChanged;

            // Search
            txtTimKiem.TextChanged += (s, e) => gridViewMaster.ApplyFindFilter(txtTimKiem.Text.Trim());

            // Buttons
            btnThemMoi.Click += BtnThemMoi_Click;
            btnLuuTab1.Click += BtnLuu_Click;
            btnXoaSP.Click += BtnXoaSP_Click;

            // Tab 2 actions
            btnThemQuyDoi.Click += BtnThemQuyDoi_Click;
            btnXoaQuyDoi.Click += BtnXoaQuyDoi_Click;

            // Tab 3 actions (flat pricing)
            btnThemGia.Click += BtnThemGia_Click;
            btnSuaGia.Click += BtnSuaGia_Click;
            btnLamMoiGia.Click += BtnLamMoiGia_Click;
            btnXoaGia.Click += BtnXoaGia_Click;
            gridViewBangGia.FocusedRowChanged += GridViewBangGia_FocusedRowChanged;

            // Dynamic UI: show/hide Vé panel
            cboLoaiSP.SelectedIndexChanged += (s, e) =>
            {
                pnlVeExtensions.Visible = (cboLoaiSP.Text == AppConstants.LoaiSanPham.Ve);
            };
        }

        private void InitCombos()
        {
            // Loại SP
            cboLoaiSP.Items.Clear();
            cboLoaiSP.Items.AddRange(new object[] {
                AppConstants.LoaiSanPham.AnUong, AppConstants.LoaiSanPham.Ve,
                AppConstants.LoaiSanPham.Thue, AppConstants.LoaiSanPham.DoLuuNiem,
                AppConstants.LoaiSanPham.GuiXe, AppConstants.LoaiSanPham.DichVu,
                AppConstants.LoaiSanPham.Combo, AppConstants.LoaiSanPham.LuuTru,
                AppConstants.LoaiSanPham.Khac
            });

            // Trạng thái
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.AddRange(new object[] {
                AppConstants.TrangThaiSanPham.DangBan, AppConstants.TrangThaiSanPham.TamNgung,
                AppConstants.TrangThaiSanPham.NgungBan, AppConstants.TrangThaiSanPham.HetHang
            });

            // ĐVT
            var dsDVT = BUS_DonViTinh.Instance.LoadDS();
            slkDonVi.Properties.DataSource = dsDVT;
            slkDonVi.Properties.DisplayMember = "Ten";
            slkDonVi.Properties.ValueMember = "Id";
            ThemeManager.StyleSearchLookUpEdit(slkDonVi, new[] { "Ten" }, new[] { "ĐVT" });

            // Khu Vực
            var dsKV = BUS_KhuVuc.Instance.LoadDS();
            slkKhuVuc.Properties.DataSource = dsKV;
            slkKhuVuc.Properties.DisplayMember = "TenKhuVuc";
            slkKhuVuc.Properties.ValueMember = "Id";
            ThemeManager.StyleSearchLookUpEdit(slkKhuVuc, new[] { "TenKhuVuc" }, new[] { "Khu vực" });

            // Trò chơi (cho tab Vé)
            var dsTroChoi = BUS_DanhSachThietBi.Instance.LoadDSTheoLoai("TroChoi");
            slkTroChoi.Properties.DataSource = dsTroChoi;
            slkTroChoi.Properties.DisplayMember = "TenThietBi";
            slkTroChoi.Properties.ValueMember = "Id";
            ThemeManager.StyleSearchLookUpEdit(slkTroChoi, new[] { "TenThietBi" }, new[] { "Trò chơi" });

            // === Tab 2 Input: ĐVT Lớn ===
            var dsDVT2 = BUS_DonViTinh.Instance.LoadDS();
            cboQD_DVTLon.DataSource = dsDVT2;
            cboQD_DVTLon.DisplayMember = "Ten";
            cboQD_DVTLon.ValueMember = "Id";

            // === Tab 3: Flat pricing — không cần combo LoaiGia ===
            cboBG_TrangThai.Items.Clear();
            cboBG_TrangThai.Items.AddRange(new object[] { "HoạtĐộng", "TạmNgưng" });
            cboBG_TrangThai.SelectedIndex = 0;
        }

        public void ApplyStyles()
        {
            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleDevExpressGrid(gridMaster);

            // Đơn thuốc 4: Giữ màu Primary cho các nút chính thay vì Ghost pattern
            btnThemMoi.FillColor = Color.FromArgb(30, 64, 175);
            btnThemMoi.ForeColor = Color.White;
            btnThemMoi.BorderThickness = 0;
            btnThemMoi.HoverState.FillColor = Color.FromArgb(23, 37, 84);

            btnLuuTab1.FillColor = Color.FromArgb(30, 64, 175);
            btnLuuTab1.ForeColor = Color.White;
            btnLuuTab1.BorderThickness = 0;
            btnLuuTab1.HoverState.FillColor = Color.FromArgb(23, 37, 84);
            
            btnXoaSP.FillColor = Color.Transparent; // Đặt trên nền đen của Header nên Transparent cho hợp
            btnXoaSP.ForeColor = Color.FromArgb(244, 63, 94);
            btnXoaSP.HoverState.FillColor = Color.FromArgb(15, 23, 42); // Hover tối nhạt trên header
        }

        public void ApplyPermissions()
        {
            var tk = SessionManager.CurrentUser;
            if (tk == null) return;

            if (!BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "VIEW_PRICE"))
            {
                this.Enabled = false;
                return;
            }

            bool canManage = BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "MANAGE_PRICE");
            btnThemMoi.Enabled = canManage;
            btnLuuTab1.Enabled = canManage;
            btnXoaSP.Enabled = canManage;
        }

        // ════════════════════════════════════════════════════
        //  MASTER GRID
        // ════════════════════════════════════════════════════
        public void LoadData() => LoadMasterGrid();

        private void LoadMasterGrid()
        {
            gridMaster.DataSource = BUS_SanPham.Instance.LoadDS();
            gridViewMaster.PopulateColumns();
            FormatMasterGrid();
        }

        private void FormatMasterGrid()
        {
            var v = gridViewMaster;
            string[] hide = { "Id", "IdDonViCoBan", "HinhAnh", "CreatedAt", "UpdatedAt", "CreatedBy", "IdKhuVuc", "_veInfo", "IsDeleted", "MoTa" };
            foreach (var c in hide) if (v.Columns[c] != null) v.Columns[c].Visible = false;

            if (v.Columns["MaCode"] != null) v.Columns["MaCode"].Caption = "Mã";
            if (v.Columns["Ten"] != null) { v.Columns["Ten"].Caption = "Tên sản phẩm"; v.Columns["Ten"].Width = 200; }
            if (v.Columns["DonGia"] != null)
            {
                v.Columns["DonGia"].Caption = "Giá gốc";
                v.Columns["DonGia"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                v.Columns["DonGia"].DisplayFormat.FormatString = "#,##0";
            }
            if (v.Columns["TrangThai"] != null) v.Columns["TrangThai"].Caption = "TT";
            if (v.Columns["LoaiSanPham"] != null)
            {
                v.Columns["LoaiSanPham"].Caption = "Loại";
                v.Columns["LoaiSanPham"].GroupIndex = 0;
            }
            v.ExpandAllGroups();
            v.OptionsView.ColumnAutoWidth = true;
            v.OptionsView.ShowAutoFilterRow = true;
        }

        // ════════════════════════════════════════════════════
        //  STATE MACHINE
        // ════════════════════════════════════════════════════
        private void SetState(FormState state)
        {
            _state = state;
            switch (state)
            {
                case FormState.Browse:
                    pnlDetail.Visible = (_current != null);
                    lblNoSelection.Visible = (_current == null);
                    tabQuyDoi.Enabled = (_current != null);
                    tabBangGia.Enabled = (_current != null);
                    btnLuuTab1.Text = "💾  LƯU CẤU HÌNH";
                    btnXoaSP.Visible = (_current != null);
                    break;

                case FormState.AddNew:
                    pnlDetail.Visible = true;
                    lblNoSelection.Visible = false;
                    tabQuyDoi.Enabled = false;
                    tabBangGia.Enabled = false;
                    tabDetails.SelectedTab = tabThongTin;
                    btnLuuTab1.Text = "✨  TẠO MỚI";
                    btnXoaSP.Visible = false;
                    lblTenSP.Text = "SẢN PHẨM MỚI";
                    lblMaCode.Text = "Chưa lưu — Nhập thông tin Tab 1 rồi bấm [TẠO MỚI]";
                    ClearTab1();
                    break;

                case FormState.Edit:
                    pnlDetail.Visible = true;
                    lblNoSelection.Visible = false;
                    tabQuyDoi.Enabled = true;
                    tabBangGia.Enabled = true;
                    btnLuuTab1.Text = "💾  LƯU CẤU HÌNH";
                    btnXoaSP.Visible = true;
                    break;
            }
        }

        // ════════════════════════════════════════════════════
        //  MASTER -> DETAIL BINDING
        // ════════════════════════════════════════════════════
        private void OnMasterFocusChanged(object s, FocusedRowChangedEventArgs e)
        {
            if (gridViewMaster.FocusedRowHandle < 0) return;
            _current = gridViewMaster.GetFocusedRow() as ET_SanPham;
            if (_current == null) return;

            ShowProduct(_current);
            SetState(FormState.Edit);
        }

        private void ShowProduct(ET_SanPham sp)
        {
            // Header
            lblTenSP.Text = sp.Ten ?? "";
            lblMaCode.Text = $"{sp.MaCode} • {sp.TrangThai}";

            // Tab 1
            cboLoaiSP.Text = sp.LoaiSanPham;
            txtTen.Text = sp.Ten;
            txtDonGia.Text = sp.DonGia.ToString("N0");
            slkDonVi.EditValue = sp.IdDonViCoBan;
            slkKhuVuc.EditValue = sp.IdKhuVuc;
            cboTrangThai.Text = sp.TrangThai;
            txtMoTa.Text = sp.MoTa;

            // Dynamic Vé panel
            bool isVe = sp.LoaiSanPham == AppConstants.LoaiSanPham.Ve;
            pnlVeExtensions.Visible = isVe;
            if (isVe)
            {
                var veInfo = BUS_SanPham.Instance.LayVeInfo(sp.Id);
                if (veInfo != null)
                {
                    slkTroChoi.EditValue = veInfo.IdThietBi;
                    spnSoLuot.EditValue = veInfo.SoLuotQuyDoi;
                }
                else
                {
                    slkTroChoi.EditValue = null;
                    spnSoLuot.EditValue = 1;
                }
            }

            // Tab 2
            LoadQuyDoi(sp.Id);

            // Tab 3 — Flat Pricing
            LoadBangGia(sp.Id);
        }

        private void ClearTab1()
        {
            cboLoaiSP.SelectedIndex = -1;
            txtTen.Clear();
            txtDonGia.Text = "0";
            slkDonVi.EditValue = null;
            slkKhuVuc.EditValue = null;
            cboTrangThai.SelectedIndex = 0;
            txtMoTa.Clear();
            pnlVeExtensions.Visible = false;
            slkTroChoi.EditValue = null;
            spnSoLuot.EditValue = 1;
            if (txtTen.CanFocus) txtTen.Focus();
        }

        // ════════════════════════════════════════════════════
        //  TAB 1: LƯU / TẠO MỚI
        // ════════════════════════════════════════════════════
        private void BtnThemMoi_Click(object s, EventArgs e)
        {
            _current = null;
            SetState(FormState.AddNew);
        }

        private void BtnLuu_Click(object s, EventArgs e)
        {
            if (!ValidateTab1()) return;
            var et = GetEntityFromUI();

            if (_state == FormState.AddNew)
            {
                et.Id = 0;
                var res = BUS_SanPham.Instance.Them(et);
                if (res.IsSuccess)
                {
                    TDCMessageBox.Show("Tạo sản phẩm thành công!\nTab Quy Đổi & Bảng Giá đã mở khóa.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadMasterGrid();
                    gridViewMaster.MoveLast();
                    _current = gridViewMaster.GetFocusedRow() as ET_SanPham;
                    if (_current != null) ShowProduct(_current);
                    SetState(FormState.Edit);
                }
                else
                {
                    TDCMessageBox.Show(res.ErrorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                var res = BUS_SanPham.Instance.Sua(et);
                if (res.IsSuccess)
                {
                    TDCMessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadMasterGrid();
                    for (int i = 0; i < gridViewMaster.DataRowCount; i++)
                    {
                        var row = gridViewMaster.GetRow(i) as ET_SanPham;
                        if (row != null && row.Id == et.Id)
                        {
                            gridViewMaster.FocusedRowHandle = i;
                            break;
                        }
                    }
                }
                else
                {
                    TDCMessageBox.Show(res.ErrorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnXoaSP_Click(object s, EventArgs e)
        {
            if (_current == null) return;
            if (TDCMessageBox.Show("Xóa sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                var res = BUS_SanPham.Instance.Xoa(_current.Id);
                if (res.IsSuccess)
                {
                    _current = null;
                    LoadMasterGrid();
                    SetState(FormState.Browse);
                }
                else TDCMessageBox.Show(res.ErrorMessage, "Lỗi");
            }
        }

        private ET_SanPham GetEntityFromUI()
        {
            decimal gia = 0;
            decimal.TryParse(txtDonGia.Text.Replace(".", "").Replace(",", ""), out gia);

            var et = new ET_SanPham
            {
                Id = _current?.Id ?? 0,
                MaCode = _current?.MaCode ?? "",
                Ten = txtTen.Text.Trim(),
                DonGia = gia,
                IdDonViCoBan = Convert.ToInt32(slkDonVi.EditValue),
                LoaiSanPham = cboLoaiSP.Text,
                MoTa = txtMoTa.Text.Trim(),
                TrangThai = cboTrangThai.Text,
                IdKhuVuc = slkKhuVuc.EditValue != null ? (int?)Convert.ToInt32(slkKhuVuc.EditValue) : null
            };

            if (et.LoaiSanPham == AppConstants.LoaiSanPham.Ve)
            {
                et._veInfo = new ET_SanPham_Ve
                {
                    IdSanPham = et.Id,
                    IdThietBi = slkTroChoi.EditValue != null ? (int?)Convert.ToInt32(slkTroChoi.EditValue) : null,
                    SoLuotQuyDoi = Convert.ToInt32(spnSoLuot.EditValue),
                    CanTaoToken = true
                };
            }

            return et;
        }

        private bool ValidateTab1()
        {
            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                TDCMessageBox.Show("Vui lòng nhập tên sản phẩm!", "Thông báo"); txtTen.Focus(); return false;
            }
            if (slkDonVi.EditValue == null)
            {
                TDCMessageBox.Show("Vui lòng chọn đơn vị tính!", "Thông báo"); return false;
            }
            if (string.IsNullOrWhiteSpace(cboLoaiSP.Text))
            {
                TDCMessageBox.Show("Vui lòng chọn loại sản phẩm!", "Thông báo"); return false;
            }
            if (string.IsNullOrWhiteSpace(cboTrangThai.Text))
            {
                TDCMessageBox.Show("Vui lòng chọn trạng thái!", "Thông báo"); return false;
            }
            if (cboLoaiSP.Text == AppConstants.LoaiSanPham.Ve)
            {
                if (Convert.ToInt32(spnSoLuot.EditValue) <= 0)
                {
                    TDCMessageBox.Show("Số lượt quẹt/vé phải > 0!", "Thông báo"); return false;
                }
            }
            return true;
        }

        // ════════════════════════════════════════════════════
        //  TAB 2: QUY ĐỔI ĐVT
        // ════════════════════════════════════════════════════
        private List<ET_QuyDoiDonVi> _listQuyDoi = new List<ET_QuyDoiDonVi>();

        private void LoadQuyDoi(int idSP)
        {
            _listQuyDoi = BUS_SanPham.Instance.LayQuyDoiTheoSP(idSP);
            gridQuyDoi.DataSource = _listQuyDoi;
            gridViewQuyDoi.PopulateColumns();
            FormatQuyDoiGrid();
        }

        private void FormatQuyDoiGrid()
        {
            var v = gridViewQuyDoi;
            string[] hide = { "Id", "IdSanPham", "LaDonViCoBan", "CreatedAt" };
            foreach (var c in hide) if (v.Columns[c] != null) v.Columns[c].Visible = false;

            var repDVT = new RepositoryItemSearchLookUpEdit();
            repDVT.DataSource = BUS_DonViTinh.Instance.LoadDS();
            repDVT.DisplayMember = "Ten";
            repDVT.ValueMember = "Id";
            repDVT.NullText = "";

            if (v.Columns["IdDonViLon"] != null) { v.Columns["IdDonViLon"].Caption = "Đơn Vị Lớn"; v.Columns["IdDonViLon"].ColumnEdit = repDVT; }
            if (v.Columns["IdDonViNho"] != null) { v.Columns["IdDonViNho"].Caption = "Đơn Vị Nhỏ (Gốc)"; v.Columns["IdDonViNho"].ColumnEdit = repDVT; }
            if (v.Columns["TyLeQuyDoi"] != null) { v.Columns["TyLeQuyDoi"].Caption = "Hệ Số"; v.Columns["TyLeQuyDoi"].DisplayFormat.FormatString = "N0"; }
            if (v.Columns["GiaBanRieng"] != null) { v.Columns["GiaBanRieng"].Caption = "Giá Bán Riêng"; v.Columns["GiaBanRieng"].DisplayFormat.FormatString = "#,##0"; }

            v.OptionsBehavior.Editable = false;
            v.OptionsView.ColumnAutoWidth = true;
        }

        private void BtnThemQuyDoi_Click(object s, EventArgs e)
        {
            if (_current == null) return;
            if (cboQD_DVTLon.SelectedValue == null) { TDCMessageBox.Show("Vui lòng chọn đơn vị lớn!", "Thông báo"); return; }
            decimal heSo = 1;
            decimal.TryParse(txtQD_HeSo.Text.Replace(",", ""), out heSo);
            if (heSo <= 0) { TDCMessageBox.Show("Hệ số phải > 0!", "Thông báo"); return; }
            decimal? giaRieng = null;
            if (!string.IsNullOrWhiteSpace(txtQD_GiaRieng.Text))
            {
                decimal gr;
                if (decimal.TryParse(txtQD_GiaRieng.Text.Replace(".", "").Replace(",", ""), out gr)) giaRieng = gr;
            }
            var et = new ET_QuyDoiDonVi
            {
                IdSanPham = _current.Id,
                IdDonViNho = _current.IdDonViCoBan,
                IdDonViLon = Convert.ToInt32(cboQD_DVTLon.SelectedValue),
                TyLeQuyDoi = heSo,
                GiaBanRieng = giaRieng,
                LaDonViCoBan = false,
                CreatedAt = DateTime.Now
            };
            var res = BUS_SanPham.Instance.LuuQuyDoi(et);
            if (res.IsSuccess) { LoadQuyDoi(_current.Id); txtQD_HeSo.Text = "1"; txtQD_GiaRieng.Text = ""; }
            else TDCMessageBox.Show(res.ErrorMessage, "Lỗi");
        }

        private void BtnXoaQuyDoi_Click(object s, EventArgs e)
        {
            if (_current == null || gridViewQuyDoi.FocusedRowHandle < 0) return;
            var row = gridViewQuyDoi.GetFocusedRow() as ET_QuyDoiDonVi;
            if (row == null) return;
            if (TDCMessageBox.Show("Xóa dòng quy đổi này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var res = BUS_SanPham.Instance.XoaQuyDoi(row.Id);
                if (res.IsSuccess) LoadQuyDoi(_current.Id);
                else TDCMessageBox.Show(res.ErrorMessage, "Lỗi");
            }
        }

        // ════════════════════════════════════════════════════
        //  TAB 3: BẢNG GIÁ (FLAT PRICING MATRIX)
        // ════════════════════════════════════════════════════
        private List<ET_BangGia> _listBangGia = new List<ET_BangGia>();
        private ET_BangGia _currentGia = null;

        private void LoadBangGia(int idSP)
        {
            _listBangGia = BUS_BangGia.Instance.LayGiaTheoSP(idSP);
            gridBangGia.DataSource = _listBangGia;
            gridViewBangGia.PopulateColumns();
            FormatBangGiaGrid();
        }

        private void FormatBangGiaGrid()
        {
            var v = gridViewBangGia;

            // Hide internal columns
            string[] hide = { "Id", "IdSanPham", "CreatedAt", "CreatedBy" };
            foreach (var c in hide) if (v.Columns[c] != null) v.Columns[c].Visible = false;

            // ── Format price columns ──
            string[] priceCols = { "GiaNgayThuong", "GiaCuoiTuan", "GiaNgayLe", "TienCoc", "GiaPhuThu" };
            string[] priceLabels = { "Giá N.Thường", "Giá Cuối Tuần", "Giá Ngày Lễ", "Tiền Cọc", "Phụ thu lố" };
            for (int i = 0; i < priceCols.Length; i++)
            {
                if (v.Columns[priceCols[i]] != null)
                {
                    v.Columns[priceCols[i]].Caption = priceLabels[i];
                    v.Columns[priceCols[i]].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    v.Columns[priceCols[i]].DisplayFormat.FormatString = "#,##0";
                    v.Columns[priceCols[i]].Width = 110;
                }
            }

            // TimeSpan columns
            if (v.Columns["GioBatDau"] != null) { v.Columns["GioBatDau"].Caption = "Từ giờ"; v.Columns["GioBatDau"].DisplayFormat.FormatString = @"hh\:mm"; }
            if (v.Columns["GioKetThuc"] != null) { v.Columns["GioKetThuc"].Caption = "Đến giờ"; v.Columns["GioKetThuc"].DisplayFormat.FormatString = @"hh\:mm"; }

            // Block columns
            if (v.Columns["PhutBlock"] != null) { v.Columns["PhutBlock"].Caption = "Block (phút)"; v.Columns["PhutBlock"].Width = 80; }
            if (v.Columns["PhutTiep"] != null) { v.Columns["PhutTiep"].Caption = "Lố (phút)"; v.Columns["PhutTiep"].Width = 70; }

            // Computed & status
            if (v.Columns["LoaiGia"] != null) { v.Columns["LoaiGia"].Caption = "Loại"; v.Columns["LoaiGia"].Width = 80; }
            if (v.Columns["TrangThai"] != null) { v.Columns["TrangThai"].Caption = "TT"; v.Columns["TrangThai"].Width = 70; }

            // Column order: LoaiGia -> GiaNgayThuong -> GiaCuoiTuan -> GiaNgayLe -> TienCoc -> Block -> Lố -> PhuThu -> Giờ -> TT
            int pos = 0;
            string[] order = { "LoaiGia", "GiaNgayThuong", "GiaCuoiTuan", "GiaNgayLe", "TienCoc",
                               "PhutBlock", "PhutTiep", "GiaPhuThu", "GioBatDau", "GioKetThuc", "TrangThai" };
            foreach (var col in order)
            {
                if (v.Columns[col] != null) v.Columns[col].VisibleIndex = pos++;
            }

            v.OptionsBehavior.Editable = false;
            v.OptionsView.ColumnAutoWidth = false;
            v.BestFitColumns();
        }

        private void BtnThemGia_Click(object s, EventArgs e)
        {
            if (_current == null) return;

            // Parse prices from input panel
            decimal giaThuong = ParseCurrency(txtBG_GiaThuong.Text);
            decimal giaCuoiTuan = ParseCurrency(txtBG_GiaCuoiTuan.Text);
            decimal giaNgayLe = ParseCurrency(txtBG_GiaNgayLe.Text);
            decimal? tienCoc = string.IsNullOrWhiteSpace(txtBG_TienCoc.Text) ? (decimal?)null : ParseCurrency(txtBG_TienCoc.Text);
            
            // Parse new fields
            int? phutTiep = string.IsNullOrWhiteSpace(txtBG_PhutTiep.Text) ? (int?)null : Convert.ToInt32(ParseCurrency(txtBG_PhutTiep.Text));
            decimal? giaPhuThu = string.IsNullOrWhiteSpace(txtBG_GiaPhuThu.Text) ? (decimal?)null : ParseCurrency(txtBG_GiaPhuThu.Text);

            if (giaThuong <= 0)
            {
                TDCMessageBox.Show("Giá Ngày Thường phải > 0!", "Thông báo");
                txtBG_GiaThuong.Focus();
                return;
            }

            // Auto-fill if empty
            if (giaCuoiTuan <= 0) giaCuoiTuan = Math.Round(giaThuong * 1.2m, 0);
            if (giaNgayLe <= 0) giaNgayLe = Math.Round(giaThuong * 1.5m, 0);

            var et = new ET_BangGia
            {
                IdSanPham = _current.Id,
                GiaNgayThuong = giaThuong,
                GiaCuoiTuan = giaCuoiTuan,
                GiaNgayLe = giaNgayLe,
                TienCoc = tienCoc,
                PhutTiep = phutTiep,
                GiaPhuThu = giaPhuThu,
                TrangThai = cboBG_TrangThai.Text
            };

            var res = BUS_BangGia.Instance.ThemGia(et);
            if (res.IsSuccess)
            {
                LoadBangGia(_current.Id);
                BtnLamMoiGia_Click(null, null);
            }
            else TDCMessageBox.Show(res.ErrorMessage, "Lỗi");
        }

        private void GridViewBangGia_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridViewBangGia.FocusedRowHandle < 0) return;
            
            _currentGia = gridViewBangGia.GetFocusedRow() as ET_BangGia;
            if (_currentGia == null) return;

            txtBG_GiaThuong.Text = _currentGia.GiaNgayThuong.ToString("N0");
            txtBG_GiaCuoiTuan.Text = _currentGia.GiaCuoiTuan.ToString("N0");
            txtBG_GiaNgayLe.Text = _currentGia.GiaNgayLe.ToString("N0");
            txtBG_TienCoc.Text = _currentGia.TienCoc?.ToString("N0") ?? "";
            txtBG_PhutTiep.Text = _currentGia.PhutTiep?.ToString() ?? "";
            txtBG_GiaPhuThu.Text = _currentGia.GiaPhuThu?.ToString("N0") ?? "";
            cboBG_TrangThai.Text = _currentGia.TrangThai;

            // Đổi màu nút để người dùng biết đang ở chế độ sửa
            btnThemGia.Enabled = false;
        }

        private void BtnSuaGia_Click(object s, EventArgs e)
        {
            if (_currentGia == null)
            {
                TDCMessageBox.Show("Vui lòng chọn dòng cần sửa!", "Thông báo");
                return;
            }

            decimal giaThuong = ParseCurrency(txtBG_GiaThuong.Text);
            decimal giaCuoiTuan = ParseCurrency(txtBG_GiaCuoiTuan.Text);
            decimal giaNgayLe = ParseCurrency(txtBG_GiaNgayLe.Text);
            decimal? tienCoc = string.IsNullOrWhiteSpace(txtBG_TienCoc.Text) ? (decimal?)null : ParseCurrency(txtBG_TienCoc.Text);
            
            int? phutTiep = string.IsNullOrWhiteSpace(txtBG_PhutTiep.Text) ? (int?)null : Convert.ToInt32(ParseCurrency(txtBG_PhutTiep.Text));
            decimal? giaPhuThu = string.IsNullOrWhiteSpace(txtBG_GiaPhuThu.Text) ? (decimal?)null : ParseCurrency(txtBG_GiaPhuThu.Text);

            if (giaThuong <= 0)
            {
                TDCMessageBox.Show("Giá Ngày Thường phải > 0!", "Thông báo");
                txtBG_GiaThuong.Focus();
                return;
            }

            // Auto-fill if empty
            if (giaCuoiTuan <= 0) giaCuoiTuan = Math.Round(giaThuong * 1.2m, 0);
            if (giaNgayLe <= 0) giaNgayLe = Math.Round(giaThuong * 1.5m, 0);

            _currentGia.GiaNgayThuong = giaThuong;
            _currentGia.GiaCuoiTuan = giaCuoiTuan;
            _currentGia.GiaNgayLe = giaNgayLe;
            _currentGia.TienCoc = tienCoc;
            _currentGia.PhutTiep = phutTiep;
            _currentGia.GiaPhuThu = giaPhuThu;
            _currentGia.TrangThai = cboBG_TrangThai.Text;

            var res = BUS_BangGia.Instance.SuaGia(_currentGia);
            if (res.IsSuccess)
            {
                LoadBangGia(_current.Id);
                BtnLamMoiGia_Click(null, null);
                TDCMessageBox.Show("Cập nhật giá thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else TDCMessageBox.Show(res.ErrorMessage, "Lỗi");
        }

        private void BtnLamMoiGia_Click(object s, EventArgs e)
        {
            _currentGia = null;
            txtBG_GiaThuong.Text = "0";
            txtBG_GiaCuoiTuan.Text = "";
            txtBG_GiaNgayLe.Text = "";
            txtBG_TienCoc.Text = "";
            txtBG_PhutTiep.Text = "";
            txtBG_GiaPhuThu.Text = "";
            
            // Xóa select trong grid để tránh bị bind lại
            gridViewBangGia.FocusedRowHandle = DevExpress.XtraGrid.GridControl.InvalidRowHandle;

            btnThemGia.Enabled = true;
        }

        private void BtnXoaGia_Click(object s, EventArgs e)
        {
            if (_current == null || gridViewBangGia.FocusedRowHandle < 0) return;
            var row = gridViewBangGia.GetFocusedRow() as ET_BangGia;
            if (row == null) return;
            if (TDCMessageBox.Show("Xóa mức giá này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var res = BUS_BangGia.Instance.XoaGia(row.Id);
                if (res.IsSuccess) LoadBangGia(_current.Id);
                else TDCMessageBox.Show(res.ErrorMessage, "Lỗi");
            }
        }

        // ── Utility ──
        private decimal ParseCurrency(string text)
        {
            decimal val = 0;
            decimal.TryParse(text.Replace(".", "").Replace(",", ""), out val);
            return val;
        }
    }
}

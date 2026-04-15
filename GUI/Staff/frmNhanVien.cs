using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BUS;
using ET;
using FontAwesome.Sharp;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace GUI
{
    public partial class frmNhanVien : Form, IBaseForm
    {
        // ── Core ──────────────────────────────────────────────────────────────
        private IBaseBUS<ET_NhanVien> _bus;
        private ET_NhanVien _currentEntity;

        // ── Header value labels (runtime-created) ─────────────────────────────
        private Label _hdrChucVu, _hdrKhoi, _hdrHopDong, _hdrNhomCV;
        private Label _hdrLuong, _hdrQuanLy, _hdrSDT, _hdrEmail;

        // ── Sub-data lists ────────────────────────────────────────────────────
        private List<ET_ChungChiNhanVien> _dsChungChi = new List<ET_ChungChiNhanVien>();
        private List<ET_KyLuat>           _dsKyLuat   = new List<ET_KyLuat>();

        // ── Dropdown constants ────────────────────────────────────────────────
        private static readonly string[] LoaiHopDongList  = { "FullTime", "PartTime", "TheoMua", "Intern" };
        private static readonly string[] NhomCongViecList = { "ThuongThuong", "NangNhocNguyHiem", "DacBietNguyHiem" };

        // ── Constructor ───────────────────────────────────────────────────────
        public frmNhanVien()
        {
            InitializeComponent();
            _bus = BUS_NhanVien.Instance;

            InitIcons();
            ApplyStyles();
            ApplyPermissions();
        }

        // ─────────────────────────────────────────────────────────────────────
        // IBaseForm interface
        // ─────────────────────────────────────────────────────────────────────

        public void ApplyPermissions()
        {
            var tk = ET.SessionManager.CurrentUser;
            if (tk == null) return;

            if (!BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "VIEW_STAFF"))
            {
                this.Enabled = false;
                return;
            }

            bool canManage = BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "MANAGE_STAFF");
            btnThem.Enabled  = canManage;
            btnSua.Enabled   = canManage;
            btnXoa.Enabled   = canManage;
        }

        public void ApplyStyles()
        {
            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleDevExpressGrid(gridControl);
        }

        public void InitIcons()
        {
            btnThem.Image   = IconHelper.GetBitmap(IconChar.Plus,         Color.White, 16);
            btnSua.Image    = IconHelper.GetBitmap(IconChar.PenToSquare,  Color.White, 16);
            btnXoa.Image    = IconHelper.GetBitmap(IconChar.TrashCan,     Color.White, 16);
            btnLamMoi.Image = IconHelper.GetBitmap(IconChar.ArrowsRotate, Color.White, 16);
        }

        // ─────────────────────────────────────────────────────────────────────
        // Load
        // ─────────────────────────────────────────────────────────────────────

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            SetupHeaderLayout();
            SetupHoSoLayout();
            LoadComboBoxes();
            LoadData();
        }

        private void SetupHeaderLayout()
        {
            pnlNVHeader.Controls.Clear();

            // Avatar (left)
            picAvatar.Location = new Point(15, 15);
            picAvatar.Size     = new Size(90, 108);
            pnlNVHeader.Controls.Add(picAvatar);

            btnChonAnh.Location = new Point(15, 126);
            btnChonAnh.Size     = new Size(90, 20);
            pnlNVHeader.Controls.Add(btnChonAnh);

            // Name row
            lblTenNV.Location  = new Point(120, 12);
            lblTenNV.AutoSize  = true;
            pnlNVHeader.Controls.Add(lblTenNV);

            lblMaNV.Location = new Point(120, 38);
            lblMaNV.AutoSize = true;
            pnlNVHeader.Controls.Add(lblMaNV);

            lblTrangThaiNV.Location = new Point(780, 16);
            lblTrangThaiNV.Anchor   = AnchorStyles.Top | AnchorStyles.Right;
            pnlNVHeader.Controls.Add(lblTrangThaiNV);

            // Info lines - reuse existing Designer labels as structured "Label: value | Label: value"
            lblChucVuInfo.Location  = new Point(120, 62);
            lblChucVuInfo.AutoSize  = true;
            lblChucVuInfo.Font      = new Font("Segoe UI Semibold", 9f);
            lblChucVuInfo.ForeColor = Color.FromArgb(51, 65, 85);
            pnlNVHeader.Controls.Add(lblChucVuInfo);

            lblHopDongInfo.Location  = new Point(120, 82);
            lblHopDongInfo.AutoSize  = true;
            lblHopDongInfo.Font      = new Font("Segoe UI Semibold", 9f);
            lblHopDongInfo.ForeColor = Color.FromArgb(51, 65, 85);
            pnlNVHeader.Controls.Add(lblHopDongInfo);

            lblLuongInfo.Location  = new Point(120, 102);
            lblLuongInfo.AutoSize  = true;
            lblLuongInfo.Font      = new Font("Segoe UI Semibold", 9f);
            lblLuongInfo.ForeColor = Color.FromArgb(51, 65, 85);
            pnlNVHeader.Controls.Add(lblLuongInfo);

            lblSDTEmailInfo.Location  = new Point(120, 122);
            lblSDTEmailInfo.AutoSize  = true;
            lblSDTEmailInfo.Font      = new Font("Segoe UI", 9f);
            lblSDTEmailInfo.ForeColor = Color.FromArgb(100, 116, 139);
            pnlNVHeader.Controls.Add(lblSDTEmailInfo);

            // Buttons move into header
            pnlToolbar.Controls.Remove(btnSua);
            pnlToolbar.Controls.Remove(btnXoa);
            btnSua.Location = new Point(780, 120);
            btnSua.Size     = new Size(80, 30);
            btnSua.Anchor   = AnchorStyles.Top | AnchorStyles.Right;
            btnXoa.Location = new Point(866, 120);
            btnXoa.Size     = new Size(70, 30);
            btnXoa.Anchor   = AnchorStyles.Top | AnchorStyles.Right;
            pnlNVHeader.Controls.Add(btnSua);
            pnlNVHeader.Controls.Add(btnXoa);

            pnlNVHeader.Size = new Size(pnlNVHeader.Width, 160);
        }

        // Populates tblHoSo cells (must run at Load, not designer, to stay VS-safe)
        private void SetupHoSoLayout()
        {
            tblHoSo.Controls.Clear();
            tblHoSo.RowStyles.Clear();

            // Helper: add a section header spanning both columns
            Action<string, Label> addHeader = (text, lbl) =>
            {
                lbl.Text      = text;
                lbl.AutoSize  = false;
                lbl.Dock      = DockStyle.Fill;
                lbl.Font      = new Font("Segoe UI Semibold", 8.5f, FontStyle.Bold);
                lbl.ForeColor = Color.FromArgb(100, 116, 139);
                lbl.BackColor = Color.FromArgb(19, 26, 37);
                lbl.Padding   = new Padding(6, 8, 0, 4);
                lbl.Height    = 30;
                tblHoSo.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                tblHoSo.Controls.Add(lbl);
                tblHoSo.SetColumnSpan(lbl, 2);
            };

            // Helper: add label + control pair
            Action<string, Label, Control> addRow = (text, lbl, ctrl) =>
            {
                lbl.Text      = text;
                lbl.AutoSize  = false;
                lbl.Dock      = DockStyle.Fill;
                lbl.Font      = new Font("Segoe UI Semibold", 8.5f);
                lbl.ForeColor = Color.FromArgb(156, 160, 167);
                lbl.TextAlign = ContentAlignment.MiddleLeft;
                lbl.Height    = 38;
                ctrl.Dock     = DockStyle.Fill;
                ctrl.Margin   = new Padding(0, 4, 0, 4);
                tblHoSo.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                tblHoSo.Controls.Add(lbl);
                tblHoSo.Controls.Add(ctrl);
            };

            // ── Cá nhân ──────────────────────────────────────────────────────
            addHeader("THÔNG TIN CÁ NHÂN", lblGrpCaNhan);
            addRow("Mã code:",       lblMaCode,    txtMaCode);
            addRow("Họ và tên:",     lblHoTen,     txtHoTen);
            addRow("Giới tính:",     lblGioiTinh,  cboGioiTinh);
            addRow("Ngày sinh:",     lblNgaySinh,  dtpNgaySinh);
            addRow("CCCD:",          lblCCCD,      txtCCCD);
            addRow("Số điện thoại:", lblSDT,       txtSDT);
            addRow("Email:",         lblEmail,     txtEmail);
            addRow("Địa chỉ:",       lblDiaChi,    txtDiaChi);

            // ── Công tác ─────────────────────────────────────────────────────
            addHeader("THÔNG TIN CÔNG TÁC", lblGrpCongTac);
            addRow("Chức vụ:",         lblChucVulbl,    slkChucVu);
            addRow("Người quản lý:",   lblNguoiQuanLy,  slkNguoiQuanLy);
            addRow("Ngày vào làm:",    lblNgayVaoLam,   dtpNgayVaoLam);
            addRow("Trạng thái:",      lblTrangThai,    cboTrangThai);
            addRow("Loại hợp đồng:",   lblLoaiHopDong,  cboLoaiHopDong);
            addRow("Nhóm công việc:",  lblNhomCongViec, cboNhomCongViec);

            // ── Lương ────────────────────────────────────────────────────────
            addHeader("THÔNG TIN LƯƠNG", lblGrpLuong);
            addRow("Lương cơ bản:",   lblLuongCoBan,   txtLuongCoBan);
            addRow("Lương theo giờ:", lblLuongTheoGio, txtLuongTheoGio);

            // Ngày phép banner — span 2 cols
            lblNgayPhep.Text      = "Số ngày phép năm sẽ tính khi lưu";
            lblNgayPhep.AutoSize  = false;
            lblNgayPhep.Dock      = DockStyle.Fill;
            lblNgayPhep.Font      = new Font("Segoe UI Semibold", 9f);
            lblNgayPhep.ForeColor = Color.FromArgb(6, 95, 70);
            lblNgayPhep.BackColor = Color.FromArgb(236, 253, 245);
            lblNgayPhep.Padding   = new Padding(8, 6, 8, 6);
            lblNgayPhep.Height    = 40;
            tblHoSo.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tblHoSo.Controls.Add(lblNgayPhep);
            tblHoSo.SetColumnSpan(lblNgayPhep, 2);

            // ── Tài khoản ─────────────────────────────────────────────────────
            addHeader("TÀI KHOẢN HỆ THỐNG", lblGrpTaiKhoan);
            addRow("Tên đăng nhập:", lblUsername,    txtUsername);
            addRow("Mật khẩu:",      lblPassword,    txtPassword);
            addRow("Vai trò:",       lblAccountRole, slkAccountRole);

            // ── Ghi chú ───────────────────────────────────────────────────────
            addHeader("GHI CHÚ", lblGrpGhiChu);
            txtGhiChu.Dock = DockStyle.Fill;
            txtGhiChu.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtGhiChu.MinimumSize = new Size(0, 80);
            tblHoSo.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tblHoSo.Controls.Add(txtGhiChu);
            tblHoSo.SetColumnSpan(txtGhiChu, 2);

            // Wire events that couldn't be in Designer (lambda refs)
            cboLoaiHopDong.SelectedIndexChanged += cboLoaiHopDong_SelectedIndexChanged;
            slkChucVu.EditValueChanged          += slkChucVu_EditValueChanged;
            slkAccountRole.EditValueChanged     += slkAccountRole_EditValueChanged;

            // Setup KPI cards for Tab 3
            SetupKpiCards();
        }

        private void SetupKpiCards()
        {
            Action<Guna.UI2.WinForms.Guna2Panel, Label, Label, Label, Color, string> setup =
                (pnl, title, so, note, color, titleText) =>
                {
                    pnl.Dock        = DockStyle.Left;
                    pnl.Width       = 280;
                    pnl.FillColor   = color;
                    pnl.BorderRadius = 8;
                    pnl.Padding     = new Padding(16, 12, 16, 8);
                    pnl.Margin      = new Padding(0, 0, 10, 0);

                    title.Text      = titleText;
                    title.Font      = new Font("Segoe UI Semibold", 8.5f, FontStyle.Bold);
                    title.ForeColor = Color.White;
                    title.Dock      = DockStyle.Top;
                    title.Height    = 22;

                    so.Text         = "--";
                    so.Font         = new Font("Segoe UI Semibold", 22f, FontStyle.Bold);
                    so.ForeColor    = Color.White;
                    so.Dock         = DockStyle.Top;
                    so.Height       = 38;

                    note.Text       = "ngay";
                    note.Font       = new Font("Segoe UI Semibold", 8f);
                    note.ForeColor  = Color.FromArgb(200, 255, 255, 255);
                    note.Dock       = DockStyle.Fill;

                    pnl.Controls.Clear();
                    pnl.Controls.Add(note);
                    pnl.Controls.Add(so);
                    pnl.Controls.Add(title);
                };

            setup(pnlPhepTong,   lblPhepTongTitle,   lblPhepTongSo,   lblPhepTongNote,
                  Color.FromArgb(37, 99, 235),  "TONG PHEP NAM");
            setup(pnlPhepDaDung, lblPhepDaDungTitle, lblPhepDaDungSo, lblPhepDaDungNote,
                  Color.FromArgb(245, 158, 11), "DA SU DUNG");
            setup(pnlPhepConLai, lblPhepConLaiTitle, lblPhepConLaiSo, lblPhepConLaiNote,
                  Color.FromArgb(5, 150, 105),  "CON LAI");
        }

        // ─────────────────────────────────────────────────────────────────────
        // LoadData
        // ─────────────────────────────────────────────────────────────────────

        private void LoadComboBoxes()
        {
            cboGioiTinh.DataSource  = new List<string> { "Nam", "Nu", "Khac" };
            cboTrangThai.DataSource = new List<string> { "ThuViec", "Dang lam viec", "Tam nghi", "Nghi viec" };

            var dsQuanLy = BUS_NhanVien.Instance.LoadDS();
            slkNguoiQuanLy.Properties.DataSource    = dsQuanLy;
            slkNguoiQuanLy.Properties.DisplayMember = "HoTen";
            slkNguoiQuanLy.Properties.ValueMember   = "Id";
            ThemeManager.StyleSearchLookUpEdit(slkNguoiQuanLy, new[] { "HoTen" }, new[] { "Nguoi quan ly" });

            var listRoles = BUS_VaiTro.Instance.LoadDS()
                .Select(x => new { x.Id, Ten = x.TenVaiTro }).ToList();

            slkChucVu.Properties.DataSource    = listRoles;
            slkChucVu.Properties.DisplayMember = "Ten";
            slkChucVu.Properties.ValueMember   = "Id";
            ThemeManager.StyleSearchLookUpEdit(slkChucVu, new[] { "Ten" }, new[] { "Chuc vu" });

            slkAccountRole.Properties.DataSource    = listRoles;
            slkAccountRole.Properties.DisplayMember = "Ten";
            slkAccountRole.Properties.ValueMember   = "Id";
            ThemeManager.StyleSearchLookUpEdit(slkAccountRole, new[] { "Ten" }, new[] { "Vai tro he thong" });

            cboLoaiHopDong.DataSource  = LoaiHopDongList;
            cboNhomCongViec.DataSource = NhomCongViecList;

            int curYear = DateTime.Today.Year;
            for (int yr = curYear; yr >= curYear - 4; yr--)
                cboNamTaiChinh.Items.Add(yr.ToString());
            if (cboNamTaiChinh.Items.Count > 0) cboNamTaiChinh.SelectedIndex = 0;
        }

        public void LoadData()
        {
            if (_bus == null) return;
            var all = _bus.LoadDS();

            if (cboLocKhoi.SelectedIndex == 1)
                all = all.Where(x => x.LoaiKhoi == "VanHanh").ToList();
            else if (cboLocKhoi.SelectedIndex == 2)
                all = all.Where(x => x.LoaiKhoi == "HanhChinh").ToList();

            gridControl.DataSource = new System.ComponentModel.BindingList<ET_NhanVien>(all);
            gridView.PopulateColumns();
            FormatGrid();
        }



        private void FormatGrid()
        {
            var view = gridView;

            // Whitelist: chỉ giữ 5 cột
            var keepVisible = new[] { "MaCode", "HoTen", "TenVaiTro", "LoaiKhoi", "TrangThai" };
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in view.Columns)
                col.Visible = keepVisible.Contains(col.FieldName);

            if (view.Columns["MaCode"]    != null) { view.Columns["MaCode"].Caption    = "Mã NV";      view.Columns["MaCode"].VisibleIndex    = 0; view.Columns["MaCode"].Width    = 70;  }
            if (view.Columns["HoTen"]     != null) { view.Columns["HoTen"].Caption     = "Họ tên";     view.Columns["HoTen"].VisibleIndex     = 1; view.Columns["HoTen"].Width     = 140; }
            if (view.Columns["TenVaiTro"] != null) { view.Columns["TenVaiTro"].Caption = "Chức vụ";    view.Columns["TenVaiTro"].VisibleIndex = 2; view.Columns["TenVaiTro"].Width = 100; }
            if (view.Columns["LoaiKhoi"]  != null) { view.Columns["LoaiKhoi"].Caption  = "Khối";       view.Columns["LoaiKhoi"].VisibleIndex  = 3; view.Columns["LoaiKhoi"].Width  = 80;  }
            if (view.Columns["TrangThai"] != null) { view.Columns["TrangThai"].Caption = "Trạng thái"; view.Columns["TrangThai"].VisibleIndex = 4; view.Columns["TrangThai"].Width = 100; }

            gridView.RowCellStyle += new RowCellStyleEventHandler(this.gridView_RowCellStyle);
            view.OptionsView.ColumnAutoWidth = true;
        }

        // ─────────────────────────────────────────────────────────────────────
        // Focused row
        // ─────────────────────────────────────────────────────────────────────

        private void OnFocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var view = sender as DevExpress.XtraGrid.Views.Base.ColumnView;
            if (view == null || view.FocusedRowHandle < 0) return;
            
            _currentEntity = view.GetFocusedRow() as ET_NhanVien;
            if (_currentEntity == null) return;

            lblNoSelection.Visible    = false;
            pnlNVHeader.Visible       = true;
            tabControlDetails.Visible = true;

            ShowEntityToUI(_currentEntity);
            LoadSubTabs(_currentEntity.Id);
        }

        private void ShowEntityToUI(ET_NhanVien row)
        {
            // Set lookup EditValues FIRST
            slkChucVu.EditValue          = row.IdVaiTro;
            slkNguoiQuanLy.EditValue     = row.IdNguoiQuanLy;
            slkAccountRole.EditValue     = row.IdVaiTro;

            // Header: populate value labels
            string tenChucVu = slkChucVu.Text?.Trim() ?? "";
            if (string.IsNullOrEmpty(tenChucVu) || tenChucVu.Contains("null")) tenChucVu = "—";

            lblTenNV.Text   = row.HoTen ?? "—";
            lblMaNV.Text    = row.MaCode ?? "—";
            _hdrChucVu.Text  = tenChucVu;
            _hdrKhoi.Text    = row.LoaiKhoi ?? "—";
            _hdrHopDong.Text = row.LoaiHopDong ?? "—";
            _hdrNhomCV.Text  = row.NhomCongViec ?? "—";
            _hdrLuong.Text   = row.LuongCoBan.HasValue ? row.LuongCoBan.Value.ToString("N0") + " VNĐ" : "—";
            _hdrQuanLy.Text  = row.TenNguoiQuanLy ?? "—";
            _hdrSDT.Text     = row.DienThoai ?? "—";
            _hdrEmail.Text   = row.Email ?? "—";

            // Status badge
            switch (row.TrangThai)
            {
                case "Dang lam viec":
                    lblTrangThaiNV.BackColor = Color.FromArgb(220, 252, 231);
                    lblTrangThaiNV.ForeColor = Color.FromArgb(22, 101, 52);
                    lblTrangThaiNV.Text      = "Đang làm việc";
                    break;
                case "Nghi viec":
                    lblTrangThaiNV.BackColor = Color.FromArgb(254, 202, 202);
                    lblTrangThaiNV.ForeColor = Color.FromArgb(153, 27, 27);
                    lblTrangThaiNV.Text      = "Đã nghỉ";
                    break;
                case "Tam nghi":
                    lblTrangThaiNV.BackColor = Color.FromArgb(254, 243, 199);
                    lblTrangThaiNV.ForeColor = Color.FromArgb(146, 64, 14);
                    lblTrangThaiNV.Text      = "Tạm nghỉ";
                    break;
                default:
                    lblTrangThaiNV.BackColor = Color.FromArgb(219, 234, 254);
                    lblTrangThaiNV.ForeColor = Color.FromArgb(29, 78, 216);
                    lblTrangThaiNV.Text      = row.TrangThai ?? "—";
                    break;
            }

            // Tab 1 fields
            txtMaCode.Text               = row.MaCode;
            txtHoTen.Text                = row.HoTen;
            cboGioiTinh.Text             = row.GioiTinh;
            dtpNgaySinh.DateTime         = row.NgaySinh ?? DateTime.Today;
            txtCCCD.Text                 = row.Cccd;
            txtSDT.Text                  = row.DienThoai;
            txtEmail.Text                = row.Email;
            txtDiaChi.Text               = row.DiaChi;
            dtpNgayVaoLam.DateTime       = row.NgayVaoLam ?? DateTime.Today;
            cboTrangThai.Text            = row.TrangThai;
            cboLoaiHopDong.SelectedItem  = row.LoaiHopDong  ?? "FullTime";
            cboNhomCongViec.SelectedItem = row.NhomCongViec ?? "ThuongThuong";
            txtLuongCoBan.Text           = row.LuongCoBan?.ToString("N0")   ?? "";
            txtLuongTheoGio.Text         = row.LuongTheoGio?.ToString("N0") ?? "";
            RefreshNgayPhepLabel(row);
            RefreshLuongFields();

            txtUsername.Text = row.TenDangNhap ?? row.MaCode;
            txtPassword.Text = row.MatKhau;

            if (!string.IsNullOrEmpty(row.HinhAnh))
            {
                picAvatar.ImageLocation = row.HinhAnh;
                picAvatar.Tag           = row.HinhAnh;
            }
            else
            {
                picAvatar.Image = null;
                picAvatar.Tag   = null;
            }
        }

        private void RefreshNgayPhepLabel(ET_NhanVien row)
        {
            if (lblNgayPhep == null || row == null) return;
            int base_  = row.NhomCongViec == "DacBietNguyHiem"  ? 16
                       : row.NhomCongViec == "NangNhocNguyHiem" ? 14 : 12;
            int years  = row.NgayVaoLam.HasValue
                       ? (int)((DateTime.Today - row.NgayVaoLam.Value).TotalDays / 365) : 0;
            int bonus  = years / 5;
            lblNgayPhep.Text = string.Format("Phép năm {0}: {1} (cơ bản) + {2} (thâm niên {3} năm) = {4} ngày", DateTime.Today.Year, base_, bonus, years, base_ + bonus);
        }

        private void RefreshLuongFields()
        {
            bool isFullTime = cboLoaiHopDong.SelectedItem?.ToString() == "FullTime"
                           || cboLoaiHopDong.SelectedItem?.ToString() == "TheoMua";
            txtLuongCoBan.Enabled   = isFullTime;
            txtLuongTheoGio.Enabled = !isFullTime;
        }

        // ─────────────────────────────────────────────────────────────────────
        // Sub tab loaders
        // ─────────────────────────────────────────────────────────────────────

        private void LoadSubTabs(int idNhanVien)
        {
            // Cham cong & Lich lam viec
            LoadChamCongData(idNhanVien);
            // Chung chi
            var dsChungChi = BUS_NhanVien.Instance.LoadChungChi(idNhanVien);
            gridChungChi.DataSource = new System.ComponentModel.BindingList<ET_ChungChiNhanVien>(dsChungChi);
            gridViewChungChi.PopulateColumns();
            FormatGridChungChi();

            // Ky luat
            var dsKyLuat = BUS_NhanVien.Instance.LoadKyLuat(idNhanVien);
            gridKyLuat.DataSource = new System.ComponentModel.BindingList<ET_KyLuat>(dsKyLuat);
            gridViewKyLuat.PopulateColumns();
            FormatGridKyLuat();

            // Don xin nghi
            var dsDonXinNghi = BUS_NhanVien.Instance.LoadDonXinNghi(idNhanVien);
            gridDonXinNghi.DataSource = new System.ComponentModel.BindingList<ET_DonXinNghi>(dsDonXinNghi);
            gridViewDonXinNghi.PopulateColumns();
            FormatGridDonXinNghi();

            // Tai nan lao dong
            var dsTaiNan = BUS_NhanVien.Instance.LoadTaiNanLaoDong(idNhanVien);
            gridTaiNanLaoDong.DataSource = new System.ComponentModel.BindingList<ET_TaiNanLaoDong>(dsTaiNan);
            gridViewTaiNanLaoDong.PopulateColumns();
            FormatGridTaiNanLaoDong();
        }

        private void LoadChamCongData(int idNhanVien)
        {
            try
            {
                // GetKhuVucHienTai returns the current zone assignment for this employee
                var kv = BUS_LichLamViec.Instance.GetKhuVucHienTai(idNhanVien);
                if (kv.IdKhuVuc.HasValue)
                {
                    // Show schedule for the employee's current zone this week
                    var monday = BUS_LichLamViec.LayThu2CuaTuan(DateTime.Today);
                    var dsLich = BUS_LichLamViec.Instance.LoadTheoTuan(monday, 0); // all Ca
                    var filtered = dsLich.FindAll(x => x.IdNhanVien == idNhanVien);
                    gridLichLamViec.DataSource = new System.ComponentModel.BindingList<ET_LichLamViec>(filtered);
                    gridViewLichLamViec.PopulateColumns();
                    string[] hideLLV = { "Id", "IdNhanVien" };
                    foreach (var c in hideLLV)
                        if (gridViewLichLamViec.Columns[c] != null)
                            gridViewLichLamViec.Columns[c].Visible = false;
                    gridViewLichLamViec.OptionsView.ColumnAutoWidth = true;
                }
                else
                {
                    gridLichLamViec.DataSource = null;
                }
            }
            catch { /* graceful fallback */ }

            gridChamCong.DataSource = null;
        }

        // ─────────────────────────────────────────────────────────────────────
        // Grid formatters
        // ─────────────────────────────────────────────────────────────────────

        private void FormatGridChungChi()
        {
            var v = gridViewChungChi;
            foreach (var c in new[] { "IdNhanVien", "HinhAnhFile" })
                if (v.Columns[c] != null) v.Columns[c].Visible = false;
            if (v.Columns["LoaiChungChi"] != null) v.Columns["LoaiChungChi"].Caption = "Loai chung chi";
            if (v.Columns["SoChungChi"]   != null) v.Columns["SoChungChi"].Caption   = "So CC";
            if (v.Columns["NgayCap"]      != null) v.Columns["NgayCap"].Caption      = "Ngay cap";
            if (v.Columns["NgayHetHan"]   != null) v.Columns["NgayHetHan"].Caption   = "Het han";
            if (v.Columns["TrangThai"]    != null) v.Columns["TrangThai"].Caption    = "Tinh trang";
            if (v.Columns["NhaCap"]       != null) v.Columns["NhaCap"].Caption       = "Noi cap";
            v.OptionsView.ColumnAutoWidth = true;
        }

        private void FormatGridKyLuat()
        {
            var v = gridViewKyLuat;
            foreach (var c in new[] { "Id", "IdNhanVien", "IdNguoiQuyetDinh" })
                if (v.Columns[c] != null) v.Columns[c].Visible = false;
            if (v.Columns["NgayApDung"]        != null) v.Columns["NgayApDung"].Caption        = "Ngay ap dung";
            if (v.Columns["HinhThuc"]          != null) v.Columns["HinhThuc"].Caption          = "Hinh thuc";
            if (v.Columns["SoTienTru"]         != null) v.Columns["SoTienTru"].Caption         = "Tien tru";
            if (v.Columns["SoNgayDinhChi"]     != null) v.Columns["SoNgayDinhChi"].Caption     = "Ngay dinh chi";
            if (v.Columns["TenNguoiQuyetDinh"] != null) v.Columns["TenNguoiQuyetDinh"].Caption = "Nguoi quyet dinh";
            if (v.Columns["NgayHetHieuLuc"]    != null) v.Columns["NgayHetHieuLuc"].Caption    = "Het hieu luc";
            v.OptionsView.ColumnAutoWidth = true;
        }

        private void FormatGridDonXinNghi()
        {
            var v = gridViewDonXinNghi;
            foreach (var c in new[] { "Id", "IdNhanVien" })
                if (v.Columns[c] != null) v.Columns[c].Visible = false;
            if (v.Columns["LoaiNghi"]       != null) v.Columns["LoaiNghi"].Caption       = "Loai nghi";
            if (v.Columns["NgayBatDau"]     != null) v.Columns["NgayBatDau"].Caption     = "Bat dau";
            if (v.Columns["NgayKetThuc"]    != null) v.Columns["NgayKetThuc"].Caption    = "Ket thuc";
            if (v.Columns["LyDo"]           != null) v.Columns["LyDo"].Caption           = "Ly do";
            if (v.Columns["TrangThai"]      != null) v.Columns["TrangThai"].Caption      = "Tinh trang";
            if (v.Columns["TiLeLuongHuong"] != null) v.Columns["TiLeLuongHuong"].Caption = "Ti le huong (%)";
            if (v.Columns["NguonChiTra"]    != null) v.Columns["NguonChiTra"].Caption    = "Nguon chi tra";
            v.OptionsView.ColumnAutoWidth = true;
        }

        private void FormatGridTaiNanLaoDong()
        {
            var v = gridViewTaiNanLaoDong;
            foreach (var c in new[] { "Id", "IdNhanVien" })
                if (v.Columns[c] != null) v.Columns[c].Visible = false;
            if (v.Columns["NgayTaiNan"] != null) v.Columns["NgayTaiNan"].Caption = "Ngay dien ra";
            if (v.Columns["LoaiTaiNan"] != null) v.Columns["LoaiTaiNan"].Caption = "Loai tai nan";
            if (v.Columns["MucDo"]      != null) v.Columns["MucDo"].Caption      = "Muc do";
            if (v.Columns["TrangThai"]  != null) v.Columns["TrangThai"].Caption  = "Tinh trang";
            if (v.Columns["MoTa"]       != null) v.Columns["MoTa"].Caption       = "Mo ta / Hau qua";
            v.OptionsView.ColumnAutoWidth = true;
        }

        // ─────────────────────────────────────────────────────────────────────
        // Row cell style handlers
        // ─────────────────────────────────────────────────────────────────────

        private void gridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column?.FieldName != "TrangThai") return;
            switch (e.CellValue?.ToString())
            {
                case "ThuViec":       e.Appearance.BackColor = Color.FromArgb(254, 243, 199); break;
                case "Dang lam viec": e.Appearance.BackColor = Color.FromArgb(220, 252, 231); break;
                case "Tam nghi":      e.Appearance.BackColor = Color.FromArgb(255, 237, 213); break;
                case "Nghi viec":     e.Appearance.BackColor = Color.FromArgb(254, 202, 202); break;
            }
        }

        private void gridViewChungChi_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column?.FieldName != "TrangThai") return;
            var val = e.CellValue?.ToString();
            if (val == "HetHan")
            {
                e.Appearance.BackColor = Color.FromArgb(254, 202, 202);
                e.Appearance.ForeColor = Color.FromArgb(153, 27, 27);
                e.Appearance.Font      = new Font("Segoe UI", 9f, FontStyle.Bold);
            }
            else if (val == "SapHetHan")
            {
                e.Appearance.BackColor = Color.FromArgb(254, 243, 199);
                e.Appearance.ForeColor = Color.FromArgb(146, 64, 14);
            }
            else
            {
                e.Appearance.BackColor = Color.FromArgb(220, 252, 231);
                e.Appearance.ForeColor = Color.FromArgb(22, 101, 52);
            }
        }

        private void gridViewKyLuat_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column?.FieldName != "HinhThuc") return;
            var val = e.CellValue?.ToString();
            if      (val == "SaThai")          e.Appearance.BackColor = Color.FromArgb(254, 202, 202);
            else if (val == "DinhChiCoLuong")  e.Appearance.BackColor = Color.FromArgb(254, 243, 199);
            else if (val == "TruLuong")        e.Appearance.BackColor = Color.FromArgb(255, 237, 213);
        }

        // ─────────────────────────────────────────────────────────────────────
        // Get entity from UI / ClearUI / Validate
        // ─────────────────────────────────────────────────────────────────────

        private ET_NhanVien GetEntityFromUI()
        {
            decimal.TryParse(txtLuongCoBan.Text.Replace(",", "").Replace(".", ""),   out decimal luongCB);
            decimal.TryParse(txtLuongTheoGio.Text.Replace(",", "").Replace(".", ""), out decimal luongGio);

            return new ET_NhanVien
            {
                Id            = _currentEntity?.Id ?? 0,
                MaCode        = txtMaCode.Text.Trim(),
                HoTen         = txtHoTen.Text.Trim(),
                GioiTinh      = cboGioiTinh.Text,
                NgaySinh      = dtpNgaySinh.DateTime.Date,
                Cccd          = txtCCCD.Text.Trim(),
                DienThoai     = txtSDT.Text.Trim(),
                Email         = txtEmail.Text.Trim(),
                DiaChi        = txtDiaChi.Text.Trim(),
                IdVaiTro      = Convert.ToInt32(slkChucVu.EditValue),
                IdNguoiQuanLy = slkNguoiQuanLy.EditValue != null
                                    ? (int?)Convert.ToInt32(slkNguoiQuanLy.EditValue) : null,
                NgayVaoLam    = dtpNgayVaoLam.DateTime.Date,
                TrangThai     = cboTrangThai.Text,
                MatKhau       = txtPassword.Text.Trim(),
                HinhAnh       = picAvatar.Tag?.ToString(),
                LoaiHopDong   = cboLoaiHopDong.SelectedItem?.ToString()  ?? "FullTime",
                NhomCongViec  = cboNhomCongViec.SelectedItem?.ToString() ?? "ThuongThuong",
                LuongCoBan    = luongCB   > 0 ? luongCB   : (decimal?)null,
                LuongTheoGio  = luongGio  > 0 ? luongGio  : (decimal?)null,
            };
        }

        private void ClearUI()
        {
            txtMaCode.Clear(); txtHoTen.Clear(); txtCCCD.Clear();
            txtSDT.Clear();    txtEmail.Clear(); txtDiaChi.Clear();
            txtUsername.Clear(); txtPassword.Clear();
            txtLuongCoBan.Clear(); txtLuongTheoGio.Clear();
            if (cboGioiTinh.Items.Count  > 0) cboGioiTinh.SelectedIndex  = 0;
            if (cboTrangThai.Items.Count > 0) cboTrangThai.SelectedIndex = 1;
            dtpNgaySinh.DateTime   = DateTime.Today;
            dtpNgayVaoLam.DateTime = DateTime.Today;
            slkChucVu.EditValue    = null;
            slkNguoiQuanLy.EditValue = null;
            slkAccountRole.EditValue = null;
            picAvatar.Image = null; picAvatar.Tag = null;
            cboLoaiHopDong.SelectedIndex  = 0;
            cboNhomCongViec.SelectedIndex = 0;
            lblNgayPhep.Text = "So ngay phep nam se tinh khi luu";

            gridChungChi.DataSource      = null;
            gridKyLuat.DataSource        = null;
            gridDonXinNghi.DataSource    = null;
            gridTaiNanLaoDong.DataSource = null;
            gridLichLamViec.DataSource   = null;
            gridChamCong.DataSource      = null;

            lblPhepTongSo.Text   = "--";
            lblPhepDaDungSo.Text = "--";
            lblPhepConLaiSo.Text = "--";
            lblNghiBuInfo.Text   = "NGHI BU TICH LUY TU OT: -- ngay";
            lblTenNV.Text        = "Chon nhan vien";
            lblMaNV.Text         = "--";
            lblChucVuInfo.Text   = "Chuc vu: --    |    Khoi: --";
            lblHopDongInfo.Text  = "Hop dong: --    |    Nhom CV: --";
            lblLuongInfo.Text    = "Luong CB: --    |    Quan ly: --";
            lblSDTEmailInfo.Text = "SDT: --    |    Email: --";
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            { TDCMessageBox.Show("Vui long nhap ho ten nhan vien!", "Thong bao"); txtHoTen.Focus(); return false; }

            if (txtHoTen.Text.Trim().Length < 2)
            { TDCMessageBox.Show("Ho ten phai co it nhat 2 ky tu!", "Thong bao"); txtHoTen.Focus(); return false; }

            int tuoi = DateTime.Today.Year - dtpNgaySinh.DateTime.Year;
            if (tuoi < 15 || tuoi > 65)
            { TDCMessageBox.Show($"Tuoi nhan vien phai tu 15 den 65! (Hien tai: {tuoi} tuoi)", "Thong bao"); return false; }

            if (!string.IsNullOrWhiteSpace(txtCCCD.Text) &&
                !System.Text.RegularExpressions.Regex.IsMatch(txtCCCD.Text.Trim(), @"^\d{12}$"))
            { TDCMessageBox.Show("CCCD phai gom dung 12 chu so!", "Thong bao"); txtCCCD.Focus(); return false; }

            if (string.IsNullOrWhiteSpace(txtSDT.Text))
            { TDCMessageBox.Show("Vui long nhap so dien thoai!", "Thong bao"); txtSDT.Focus(); return false; }

            if (!System.Text.RegularExpressions.Regex.IsMatch(txtSDT.Text.Trim(), @"^\d{10}$"))
            { TDCMessageBox.Show("So dien thoai phai gom dung 10 chu so!", "Thong bao"); txtSDT.Focus(); return false; }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text) &&
                !System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            { TDCMessageBox.Show("Dinh dang Email khong hop le!", "Thong bao"); txtEmail.Focus(); return false; }

            if (slkChucVu.EditValue == null)
            { TDCMessageBox.Show("Vui long chon chuc vu!", "Thong bao"); return false; }

            if (dtpNgayVaoLam.DateTime.Date > DateTime.Today)
            { TDCMessageBox.Show("Ngay vao lam khong the la ngay tuong lai!", "Thong bao"); return false; }

            if (_currentEntity == null && !string.IsNullOrEmpty(txtPassword.Text) && txtPassword.Text.Trim().Length < 4)
            { TDCMessageBox.Show("Mat khau phai co it nhat 4 ky tu!", "Thong bao"); txtPassword.Focus(); return false; }

            return true;
        }

        // ─────────────────────────────────────────────────────────────────────
        // Event handlers — toolbar
        // ─────────────────────────────────────────────────────────────────────

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            HandleResult(_bus.Them(GetEntityFromUI()), "Them moi thanh cong!");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_currentEntity == null) return;
            if (!ValidateInput()) return;
            HandleResult(_bus.Sua(GetEntityFromUI()), "Cap nhat thanh cong!");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_currentEntity == null) return;
            if (TDCMessageBox.Show("Ban co chac chan muon xoa?", "Xac nhan", MessageBoxButtons.YesNo) == DialogResult.Yes)
                HandleResult(_bus.Xoa(_currentEntity.Id), "Xoa thanh cong!");
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            LoadData();
            gridView.ClearFindFilter();
            _currentEntity   = null;
            ClearUI();
            lblNoSelection.Visible    = true;
            pnlNVHeader.Visible       = false;
            tabControlDetails.Visible = false;
        }

        private void HandleResult(ResponseResult res, string successMsg)
        {
            if (res.IsSuccess)
            {
                TDCMessageBox.Show(successMsg, "Thong bao");
                btnLamMoi_Click(null, null);
            }
            else TDCMessageBox.Show(res.ErrorMessage, "Loi");
        }

        // ─────────────────────────────────────────────────────────────────────
        // Event handlers — search / grid
        // ─────────────────────────────────────────────────────────────────────

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
            => gridView.ApplyFindFilter(txtTimKiem.Text.Trim());

        private void cboLocKhoi_SelectedIndexChanged(object sender, EventArgs e) => LoadData();

        // ─────────────────────────────────────────────────────────────────────
        // Event handlers — tabs
        // ─────────────────────────────────────────────────────────────────────

        private void tabControlDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_currentEntity == null) return;
            if (tabControlDetails.SelectedTab?.Name == "tabChamCong")
            {
                Cursor.Current = Cursors.WaitCursor;
                LoadChamCongData(_currentEntity.Id);
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnMoLichLamViec_Click(object sender, EventArgs e)
        {
            var frm = new frmLichLamViec();
            frm.Show();
        }

        private void cboNamTaiChinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_currentEntity == null) return;
            var dsDonXinNghi = BUS_NhanVien.Instance.LoadDonXinNghi(_currentEntity.Id);
            gridDonXinNghi.DataSource = new System.ComponentModel.BindingList<ET_DonXinNghi>(dsDonXinNghi);
            gridViewDonXinNghi.PopulateColumns();
            FormatGridDonXinNghi();
        }

        private void cboLoaiHopDong_SelectedIndexChanged(object sender, EventArgs e)
            => RefreshLuongFields();

        private void slkChucVu_EditValueChanged(object sender, EventArgs e)
        {
            if (slkAccountRole.EditValue != slkChucVu.EditValue)
                slkAccountRole.EditValue = slkChucVu.EditValue;
        }

        private void slkAccountRole_EditValueChanged(object sender, EventArgs e)
        {
            if (slkChucVu.EditValue != slkAccountRole.EditValue)
                slkChucVu.EditValue = slkAccountRole.EditValue;
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog { Filter = "Images|*.jpg;*.png;*.bmp" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    picAvatar.ImageLocation = ofd.FileName;
                    picAvatar.Tag           = ofd.FileName;
                }
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // Event handlers — sub-tab CRUD
        // ─────────────────────────────────────────────────────────────────────

        private void BtnThemCC_Click(object sender, EventArgs e)
        {
            if (_currentEntity == null)
            { TDCMessageBox.Show("Chon nhan vien truoc!", "Thong bao"); return; }
            using (var dlg = new frmChungChiDialog(_currentEntity.Id))
                if (dlg.ShowDialog() == DialogResult.OK)
                    LoadSubTabs(_currentEntity.Id);
        }

        private void BtnXoaCC_Click(object sender, EventArgs e)
        {
            var row = gridViewChungChi.GetFocusedRow() as ET_ChungChiNhanVien;
            if (row == null) return;
            if (TDCMessageBox.Show("Xoa chung chi nay?", "Xac nhan", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var res = BUS_NhanVien.Instance.XoaChungChi(row.Id);
                if (res.IsSuccess) LoadSubTabs(_currentEntity.Id);
                else TDCMessageBox.Show(res.ErrorMessage, "Loi");
            }
        }

        private void BtnThemKL_Click(object sender, EventArgs e)
        {
            if (_currentEntity == null)
            { TDCMessageBox.Show("Chon nhan vien truoc!", "Thong bao"); return; }
            using (var dlg = new frmKyLuatDialog(_currentEntity.Id))
                if (dlg.ShowDialog() == DialogResult.OK)
                    LoadSubTabs(_currentEntity.Id);
        }

        private void BtnXoaKL_Click(object sender, EventArgs e)
        {
            var row = gridViewKyLuat.GetFocusedRow() as ET_KyLuat;
            if (row == null) return;
            if (TDCMessageBox.Show("Xoa ban ghi ky luat nay?", "Xac nhan", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var res = BUS_NhanVien.Instance.XoaKyLuat(row.Id);
                if (res.IsSuccess) LoadSubTabs(_currentEntity.Id);
                else TDCMessageBox.Show(res.ErrorMessage, "Loi");
            }
        }

        private void BtnThemDXN_Click(object sender, EventArgs e)
        {
            if (_currentEntity == null)
            { TDCMessageBox.Show("Chon nhan vien truoc!", "Thong bao"); return; }
            using (var dlg = new frmDonXinNghiDialog(_currentEntity.Id))
                if (dlg.ShowDialog() == DialogResult.OK)
                    LoadSubTabs(_currentEntity.Id);
        }

        private void BtnXoaDXN_Click(object sender, EventArgs e)
        {
            var row = gridViewDonXinNghi.GetFocusedRow() as ET_DonXinNghi;
            if (row == null) return;
            if (TDCMessageBox.Show("Xoa don xin nghi nay?", "Xac nhan", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var res = BUS_NhanVien.Instance.XoaDonXinNghi(row.Id);
                if (res.IsSuccess) LoadSubTabs(_currentEntity.Id);
                else TDCMessageBox.Show(res.ErrorMessage, "Loi");
            }
        }

        private void BtnThemTN_Click(object sender, EventArgs e)
        {
            if (_currentEntity == null)
            { TDCMessageBox.Show("Chon nhan vien truoc!", "Thong bao"); return; }
            using (var dlg = new frmTaiNanLaoDongDialog(_currentEntity.Id))
                if (dlg.ShowDialog() == DialogResult.OK)
                    LoadSubTabs(_currentEntity.Id);
        }

        private void BtnXoaTN_Click(object sender, EventArgs e)
        {
            var row = gridViewTaiNanLaoDong.GetFocusedRow() as ET_TaiNanLaoDong;
            if (row == null) return;
            if (TDCMessageBox.Show("Xoa ghi nhan tai nan nay?", "Xac nhan", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var res = BUS_NhanVien.Instance.XoaTaiNanLaoDong(row.Id);
                if (res.IsSuccess) LoadSubTabs(_currentEntity.Id);
                else TDCMessageBox.Show(res.ErrorMessage, "Loi");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BUS;
using ET;
using FontAwesome.Sharp;
using DevExpress.XtraGrid.Views.Base;

namespace GUI
{
    public partial class frmCombo : Form, IBaseForm
    {
        private IBaseBUS<ET_Combo> _bus;
        private ET_Combo _currentEntity;
        private BindingList<ComboItemDisplay> _roItems = new BindingList<ComboItemDisplay>();
        private List<ET_SanPham> _allSanPham;

        public frmCombo()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            _bus = BUS_Combo.Instance;
            pnlRoInfo.Paint += PnlRoInfo_Paint;

            InitIcons();
            ApplyStyles();
            ApplyPermissions();
        }

        // ===== DTO for basket grid =====
        private class ComboItemDisplay
        {
            [Browsable(false)]
            public int IdSanPham { get; set; }

            [DisplayName("Sản phẩm")]
            [ReadOnly(true)]
            public string TenSanPham { get; set; }

            [DisplayName("Đơn giá")]
            [ReadOnly(true)]
            public decimal DonGia { get; set; }

            [DisplayName("SL")]
            public int SoLuong { get; set; }

            [DisplayName("Tỷ lệ %")]
            public decimal TyLePhanBo { get; set; }

            [DisplayName("Thành tiền")]
            [ReadOnly(true)]
            public decimal ThanhTien => DonGia * SoLuong;
        }

        // ===== IBaseForm =====
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
            btnThem.Enabled = canManage;
            btnSua.Enabled = canManage;
            btnXoa.Enabled = canManage;
            btnLuuRo.Enabled = canManage;
            btnChiaDeu.Enabled = canManage;
            btnThemVaoRo.Enabled = canManage;
        }

        public void ApplyStyles()
        {
            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleDevExpressGrid(gridControl);
            ThemeManager.StyleDevExpressGrid(gridKhoSanPham);
            ThemeManager.StyleDevExpressGrid(gridRoCombo);
            
            // Bật lại chức năng Edit (bị ThemeManager khoá mặc định) để gõ SL/Tỷ Lệ và nhấn nút Xóa
            gridViewRo.OptionsBehavior.Editable = true;
            
            // UX Polish: Nút Lưu Rổ là CTA chính, phải dùng màu Solid (Khác với các nút Outline)
            btnLuuRo.FillColor = ThemeManager.PrimaryColor;
            btnLuuRo.ForeColor = Color.White;
            btnLuuRo.BorderThickness = 0;
            btnLuuRo.HoverState.FillColor = ThemeManager.SidebarHoverColor;
            btnLuuRo.HoverState.ForeColor = Color.White;

            // Visual link between grid products and the storage bar chart below
            gridViewRo.RowCellStyle += GridViewRo_RowCellStyle;
        }

        public void InitIcons()
        {
            btnThem.Image = IconHelper.GetBitmap(IconChar.Plus, ThemeManager.AccentColor, 14);
            btnSua.Image = IconHelper.GetBitmap(IconChar.PenToSquare, ThemeManager.PrimaryColor, 14);
            btnXoa.Image = IconHelper.GetBitmap(IconChar.TrashCan, ThemeManager.DangerColor, 14);
            btnLamMoi.Image = IconHelper.GetBitmap(IconChar.ArrowsRotate, ThemeManager.SecondaryColor, 14);
            btnThemVaoRo.Image = IconHelper.GetBitmap(IconChar.ArrowRight, ThemeManager.PrimaryColor, 14);
            btnLuuRo.Image = IconHelper.GetBitmap(IconChar.FloppyDisk, Color.White, 14);

            // Nâng cấp UX 4: Search Control Clear Buttons
            txtTimKiem.IconLeft = IconHelper.GetBitmap(IconChar.MagnifyingGlass, Color.Gray, 12);
            txtTimKiem.IconRight = IconHelper.GetBitmap(IconChar.Xmark, Color.Gray, 12);
            txtTimKiem.IconRightCursor = Cursors.Hand;
            txtTimKiem.IconRightClick += (s, ev) => { txtTimKiem.Clear(); txtTimKiem.Focus(); };

            txtTimKiemKho.IconLeft = IconHelper.GetBitmap(IconChar.MagnifyingGlass, Color.Gray, 12);
            txtTimKiemKho.IconRight = IconHelper.GetBitmap(IconChar.Xmark, Color.Gray, 12);
            txtTimKiemKho.IconRightCursor = Cursors.Hand;
            txtTimKiemKho.IconRightClick += (s, ev) => { txtTimKiemKho.Clear(); txtTimKiemKho.Focus(); };
        }

        // ===== LOAD =====
        private void frmCombo_Load(object sender, EventArgs e)
        {
            InitCboTrangThai();
            LoadData();
            LoadKhoSanPham();
            _roItems = new BindingList<ComboItemDisplay>();
            BindRoGrid();
            txtMaCode.Text = GenerateMaCode();
        }

        private void InitCboTrangThai()
        {
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add(AppConstants.TrangThaiCombo.BanNhap);
            cboTrangThai.Items.Add(AppConstants.TrangThaiCombo.KichHoat);
            cboTrangThai.Items.Add(AppConstants.TrangThaiCombo.NgungApDung);
            cboTrangThai.SelectedIndex = 0;
        }

        public void LoadData()
        {
            if (_bus == null) return;
            gridControl.DataSource = new BindingList<ET_Combo>(_bus.LoadDS());
            gridView.PopulateColumns();
            FormatComboListGrid();
        }

        private void FormatComboListGrid()
        {
            if (gridView.Columns.Count == 0) return;
            gridView.BestFitColumns();
        }

        private void LoadKhoSanPham()
        {
            _allSanPham = BUS_SanPham.Instance.LoadDS()
                .Where(x => x.TrangThai == AppConstants.TrangThaiSanPham.DangBan && x.LoaiSanPham != AppConstants.LoaiSanPham.Combo)
                .ToList();

            gridKhoSanPham.DataSource = new BindingList<ET_SanPham>(_allSanPham);
            gridViewKho.PopulateColumns();
            FormatKhoGrid();
        }

        private void FormatKhoGrid()
        {
            if (gridViewKho.Columns.Count == 0) return;

            // Kho only loads DangBan → TrangThai column is redundant
            if (gridViewKho.Columns["TrangThai"] != null)
                gridViewKho.Columns["TrangThai"].Visible = false;

            if (gridViewKho.Columns["DonGia"] != null)
            {
                gridViewKho.Columns["DonGia"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridViewKho.Columns["DonGia"].DisplayFormat.FormatString = "N0";
            }
            gridViewKho.BestFitColumns();
        }

        private void BindRoGrid()
        {
            gridRoCombo.DataSource = _roItems;
            gridViewRo.PopulateColumns();
            FormatRoGrid();
        }

        private void FormatRoGrid()
        {
            if (gridViewRo.Columns.Count == 0) return;

            var readOnlyColor = Color.FromArgb(241, 245, 249); 

            if (gridViewRo.Columns["TenSanPham"] != null)
            {
                gridViewRo.Columns["TenSanPham"].AppearanceCell.BackColor = readOnlyColor;
                gridViewRo.Columns["TenSanPham"].OptionsColumn.AllowEdit = false;
            }

            // DisplayFormat (not possible via attributes)
            if (gridViewRo.Columns["DonGia"] != null)
            {
                gridViewRo.Columns["DonGia"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridViewRo.Columns["DonGia"].DisplayFormat.FormatString = "N0";
                gridViewRo.Columns["DonGia"].AppearanceCell.BackColor = readOnlyColor;
                gridViewRo.Columns["DonGia"].OptionsColumn.AllowEdit = false;
            }
            if (gridViewRo.Columns["TyLePhanBo"] != null)
            {
                gridViewRo.Columns["TyLePhanBo"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridViewRo.Columns["TyLePhanBo"].DisplayFormat.FormatString = "N2";
                gridViewRo.Columns["TyLePhanBo"].AppearanceCell.BackColor = Color.White; 
            }
            if (gridViewRo.Columns["SoLuong"] != null)
            {
                gridViewRo.Columns["SoLuong"].AppearanceCell.BackColor = Color.White; 
            }
            if (gridViewRo.Columns["ThanhTien"] != null)
            {
                gridViewRo.Columns["ThanhTien"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridViewRo.Columns["ThanhTien"].DisplayFormat.FormatString = "N0";
                gridViewRo.Columns["ThanhTien"].AppearanceCell.BackColor = readOnlyColor;
                gridViewRo.Columns["ThanhTien"].OptionsColumn.AllowEdit = false;
            }

            // Delete button column — "đường lui" cho user
            if (gridViewRo.Columns.ColumnByFieldName("colXoa") == null)
            {
                var colXoa = gridViewRo.Columns.AddField("colXoa");
                colXoa.Caption = "";
                colXoa.UnboundType = DevExpress.Data.UnboundColumnType.Object;
                colXoa.Visible = true;
                colXoa.VisibleIndex = gridViewRo.Columns.Count;
                colXoa.Width = 35;
                colXoa.MaxWidth = 35;
                colXoa.OptionsColumn.AllowEdit = true;
                colXoa.AppearanceCell.BackColor = Color.White; 

                var btnRepo = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
                btnRepo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
                btnRepo.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete;
                btnRepo.ButtonClick += (s, args) => XoaKhoiRo();
                gridRoCombo.RepositoryItems.Add(btnRepo);
                colXoa.ColumnEdit = btnRepo;
            }

            gridViewRo.BestFitColumns();
        }

        private void GridViewRo_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "TenSanPham")
            {
                int dataSourceIndex = gridViewRo.GetDataSourceRowIndex(e.RowHandle);
                if (dataSourceIndex >= 0)
                {
                    e.Appearance.ForeColor = GetThemeColor(dataSourceIndex);
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
            }
        }

        // ===== COMBO LIST EVENTS =====
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            gridView.ApplyFindFilter(txtTimKiem.Text.Trim());
        }

        private void OnFocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (gridView.FocusedRowHandle >= 0)
            {
                _currentEntity = gridView.GetFocusedRow() as ET_Combo;
                if (_currentEntity != null)
                {
                    ShowEntityToUI(_currentEntity);
                    LoadRoCombo(_currentEntity.Id);
                }
            }
        }

        private void ShowEntityToUI(ET_Combo row)
        {
            txtMaCode.Text = row.MaCode;
            txtTen.Text = row.Ten;
            txtGia.Text = row.Gia.ToString("0");
            txtMoTa.Text = row.MoTa;
            cboTrangThai.Text = row.TrangThai;
        }

        private ET_Combo GetEntityFromUI()
        {
            return new ET_Combo
            {
                Id = _currentEntity?.Id ?? 0,
                MaCode = txtMaCode.Text.Trim(),
                Ten = txtTen.Text.Trim(),
                Gia = decimal.TryParse(txtGia.Text.Replace(".", "").Replace(",", ""), out decimal g) ? g : 0,
                MoTa = txtMoTa.Text.Trim(),
                TrangThai = cboTrangThai.Text
            };
        }

        private void ClearUI()
        {
            txtMaCode.Text = GenerateMaCode();
            txtTen.Clear();
            txtGia.Clear();
            txtMoTa.Clear();
            cboTrangThai.SelectedIndex = 0;
            _currentEntity = null;
            _roItems.Clear();
            UpdateTongInfo();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                TDCMessageBox.Show("Vui lòng nhập tên combo!", "Thông báo");
                txtTen.Focus();
                return false;
            }
            if (!decimal.TryParse(txtGia.Text.Replace(".", "").Replace(",", ""), out decimal gia) || gia < 0)
            {
                TDCMessageBox.Show("Giá combo phải là số >= 0!", "Thông báo");
                txtGia.Focus();
                return false;
            }
            return true;
        }

        private string GenerateMaCode()
        {
            var all = _bus.LoadDS();
            int maxNum = 0;
            foreach (var c in all)
            {
                if (c.MaCode != null && c.MaCode.StartsWith("CB-"))
                {
                    if (int.TryParse(c.MaCode.Substring(3), out int num) && num > maxNum)
                        maxNum = num;
                }
            }
            return $"CB-{(maxNum + 1):D3}";
        }

        // ===== KHO SP EVENTS =====
        private void TxtTimKiemKho_TextChanged(object sender, EventArgs e)
        {
            gridViewKho.ApplyFindFilter(txtTimKiemKho.Text.Trim());
        }

        private void GridViewKho_DoubleClick(object sender, EventArgs e)
        {
            ThemSPVaoRo();
        }

        private void BtnThemVaoRo_Click(object sender, EventArgs e)
        {
            ThemSPVaoRo();
        }

        private void ThemSPVaoRo()
        {
            if (gridViewKho.FocusedRowHandle < 0) return;
            var sp = gridViewKho.GetFocusedRow() as ET_SanPham;
            if (sp == null) return;

            var existing = _roItems.FirstOrDefault(x => x.IdSanPham == sp.Id);
            if (existing != null)
            {
                existing.SoLuong++;
                gridViewRo.RefreshData();
            }
            else
            {
                _roItems.Add(new ComboItemDisplay
                {
                    IdSanPham = sp.Id,
                    TenSanPham = sp.Ten,
                    DonGia = sp.DonGia,
                    SoLuong = 1,
                    TyLePhanBo = 0
                });
                FormatRoGrid();
            }

            UpdateTongInfo();
        }

        // ===== RO COMBO EVENTS =====
        private void LoadRoCombo(int idCombo)
        {
            var chiTiets = BUS_Combo.Instance.LayChiTiet(idCombo);
            _roItems.Clear();

            foreach (var ct in chiTiets)
            {
                var sp = _allSanPham?.FirstOrDefault(x => x.Id == ct.IdSanPham);
                _roItems.Add(new ComboItemDisplay
                {
                    IdSanPham = ct.IdSanPham,
                    TenSanPham = sp?.Ten ?? $"SP#{ct.IdSanPham}",
                    DonGia = sp?.DonGia ?? 0,
                    SoLuong = ct.SoLuong,
                    TyLePhanBo = ct.TyLePhanBo
                });
            }

            BindRoGrid();
            UpdateTongInfo();
        }

        private void GridViewRo_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            gridViewRo.PostEditor();
            gridViewRo.UpdateCurrentRow();
            UpdateTongInfo();
        }

        private void GridViewRo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && gridViewRo.FocusedRowHandle >= 0)
            {
                XoaKhoiRo();
            }
        }

        private void XoaKhoiRo()
        {
            if (gridViewRo.FocusedRowHandle < 0) return;
            _roItems.RemoveAt(gridViewRo.FocusedRowHandle);
            UpdateTongInfo();
        }

        // ===== CHIA ĐỀU =====
        private void BtnChiaDeu_Click(object sender, EventArgs e)
        {
            if (_roItems.Count == 0)
            {
                TDCMessageBox.Show("Rổ combo đang trống!", "Thông báo");
                return;
            }

            int count = _roItems.Count;
            decimal phanDeu = Math.Floor(10000m / count) / 100m;

            for (int i = 0; i < count - 1; i++)
            {
                _roItems[i].TyLePhanBo = phanDeu;
            }
            _roItems[count - 1].TyLePhanBo = 100m - (phanDeu * (count - 1));

            gridViewRo.RefreshData();
            UpdateTongInfo();
        }

        private void UpdateTongInfo()
        {
            decimal tongTyLe = _roItems.Sum(x => x.TyLePhanBo);
            decimal tongGiaGoc = _roItems.Sum(x => x.ThanhTien);

            lblTongTyLe.Text = $"Tổng phân bổ: {tongTyLe:N2}% / 100%";
            lblTongGiaGoc.Text = $"Tổng giá gốc: {tongGiaGoc:N0} VNĐ";

            if (tongTyLe == 100m)
            {
                lblTongTyLe.ForeColor = Color.FromArgb(16, 185, 129);
                btnLuuRo.FillColor = ThemeManager.PrimaryColor;
            }
            else
            {
                if (tongTyLe > 100m)
                {
                    lblTongTyLe.ForeColor = Color.FromArgb(239, 68, 68);
                    btnLuuRo.FillColor = Color.FromArgb(239, 68, 68);
                }
                else
                {
                    lblTongTyLe.ForeColor = Color.FromArgb(245, 158, 11);
                    btnLuuRo.FillColor = Color.FromArgb(245, 158, 11);
                }
            }
            
            // Forgiving UI: Always enabled to allow Drafts
            btnLuuRo.Enabled = true;
                
            pnlRoInfo.Invalidate();
        }

        private Color GetThemeColor(int index)
        {
            Color[] baseColors = new Color[] {
                Color.FromArgb(59, 130, 246), // Blue
                Color.FromArgb(16, 185, 129), // Green
                Color.FromArgb(245, 158, 11), // Orange
                Color.FromArgb(239, 68, 68),  // Red
                Color.FromArgb(168, 85, 247), // Purple
                Color.FromArgb(20, 184, 166)  // Teal
            };

            if (index < baseColors.Length)
                return baseColors[index];

            // Thuật toán sinh màu thông minh (Golden Ratio hue shift) cho > 100 items
            double hue = (index * 137.508) % 360; 
            return ColorFromHSV(hue, 0.65, 0.85); 
        }

        private Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0) return Color.FromArgb(255, v, t, p);
            else if (hi == 1) return Color.FromArgb(255, q, v, p);
            else if (hi == 2) return Color.FromArgb(255, p, v, t);
            else if (hi == 3) return Color.FromArgb(255, p, q, v);
            else if (hi == 4) return Color.FromArgb(255, t, p, v);
            else return Color.FromArgb(255, v, p, q);
        }

        // ===== GDI+ CHART (DELIGHTER FEATURE) =====
        private void PnlRoInfo_Paint(object sender, PaintEventArgs e)
        {
            if (_roItems == null || _roItems.Count == 0) return;

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Define bar area
            int barX = 10;
            int barY = 5;
            int barHeight = 15;
            int barWidth = pnlRoInfo.Width - 20;

            Rectangle rect = new Rectangle(barX, barY, barWidth, barHeight);
            
            // Draw background
            using (Brush bgBrush = new SolidBrush(Color.FromArgb(226, 232, 240)))
            {
                g.FillRectangle(bgBrush, rect);
            }

            decimal tongTyLe = _roItems.Sum(x => x.TyLePhanBo);
            if (tongTyLe <= 0) return;

            float currentX = barX;
            for (int i = 0; i < _roItems.Count; i++)
            {
                var item = _roItems[i];
                if (item.TyLePhanBo <= 0) continue;

                decimal safeTyLe = item.TyLePhanBo;
                if (safeTyLe > 100) safeTyLe = 100m; // limit single visual width
                
                float width = (float)(safeTyLe / 100m) * barWidth;
                
                if (currentX + width > barX + barWidth)
                    width = barX + barWidth - currentX; // cap to right bounds

                if (width <= 0) break;

                RectangleF segmentRect = new RectangleF(currentX, barY, width, barHeight);
                Color color = GetThemeColor(i);

                using (Brush brush = new SolidBrush(color))
                {
                    g.FillRectangle(brush, segmentRect);
                }
                
                // Draw white border separator if not the last segment
                if (i < _roItems.Count - 1)
                {
                    g.DrawLine(Pens.White, currentX + width, barY, currentX + width, barY + barHeight);
                }

                currentX += width;
            }
        }

        // ===== LƯU RỔ =====
        private void BtnLuuRo_Click(object sender, EventArgs e)
        {
            if (_currentEntity == null)
            {
                TDCMessageBox.Show("Vui lòng chọn 1 Combo ở danh sách bên trái trước!", "Thông báo");
                return;
            }

            // AUTO-DOWNGRADE LOGIC: Save as Draft
            decimal tongTyLe = _roItems.Sum(x => x.TyLePhanBo);
            if (tongTyLe != 100m)
            {
                var dr = TDCMessageBox.Show(
                    $"Tổng phân bổ hiện tại là {tongTyLe:N2}%, chưa đạt 100%.\nHệ thống sẽ lưu Combo này dưới dạng [Bản Nháp] để tránh xuất bán nhầm.\n\nBạn có muốn lưu không?", 
                    "Cảnh báo Bản Nháp", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Warning);

                if (dr == DialogResult.No) return;

                // Tự động chặn: Ép về bản nháp
                cboTrangThai.Text = AppConstants.TrangThaiCombo.BanNhap;
                _currentEntity.TrangThai = AppConstants.TrangThaiCombo.BanNhap;
                if (_currentEntity.Id > 0)
                {
                    _bus.Sua(_currentEntity);
                    gridView.RefreshData();
                }
            }

            var items = _roItems.Select(x => new ET_ComboChiTiet
            {
                IdCombo = _currentEntity.Id,
                IdSanPham = x.IdSanPham,
                SoLuong = x.SoLuong,
                TyLePhanBo = x.TyLePhanBo
            }).ToList();

            var result = BUS_Combo.Instance.LuuChiTiet(_currentEntity.Id, items);
            if (result.IsSuccess)
            {
                TDCMessageBox.Show("Lưu rổ combo thành công!", "Thành công");
                LoadRoCombo(_currentEntity.Id);
            }
            else
            {
                TDCMessageBox.Show(result.ErrorMessage, "Lỗi");
            }
        }

        // ===== CRUD COMBO =====
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            var et = GetEntityFromUI();
            et.MaCode = GenerateMaCode();
            et.CreatedAt = DateTime.Now;
            var res = _bus.Them(et);
            HandleResult(res, "Thêm combo mới thành công!");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_currentEntity == null) return;
            if (!ValidateInput()) return;
            var et = GetEntityFromUI();
            et.UpdatedAt = DateTime.Now;
            var res = _bus.Sua(et);
            HandleResult(res, "Cập nhật combo thành công!");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_currentEntity == null) return;
            if (TDCMessageBox.Show("Bạn có chắc chắn muốn xóa combo này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var res = _bus.Xoa(_currentEntity.Id);
                HandleResult(res, "Xóa combo thành công!");
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            txtTimKiemKho.Clear();
            gridView.ClearFindFilter();
            gridViewKho.ClearFindFilter();
            LoadData();
            ClearUI();
        }

        private void HandleResult(ResponseResult res, string successMsg)
        {
            if (res.IsSuccess)
            {
                TDCMessageBox.Show(successMsg, "Thông báo");
                LoadData();
                ClearUI();
            }
            else TDCMessageBox.Show(res.ErrorMessage, "Lỗi");
        }
    }
}

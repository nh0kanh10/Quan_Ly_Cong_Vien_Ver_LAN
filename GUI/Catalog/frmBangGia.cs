using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BUS;
using DAL;
using ET;
using FontAwesome.Sharp;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace GUI
{
    /// <summary>
    /// Flat Pricing Editor — Quản lý bảng giá theo schema mới:
    /// GiaNgayThuong / GiaCuoiTuan / GiaNgayLe / TienCoc / PhutBlock / GiaPhuThu
    /// </summary>
    public partial class frmBangGia : Form, IBaseForm
    {
        private List<ET_BangGia> _dsBangGia = new List<ET_BangGia>();
        private ET_BangGia _currentBangGia;
        private int? _selectedIdSanPham;

        public frmBangGia()
        {
            InitializeComponent();
            InitIcons();
            ApplyStyles();
            ApplyPermissions();
            WireEvents();
        }

        // =================== IBaseForm ===================
        public void ApplyPermissions()
        {
            var tk = ET.SessionManager.CurrentUser;
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
        }

        public void ApplyStyles()
        {
            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleDevExpressGrid(gridControlBangGia);
            gridViewBangGia.OptionsView.ShowGroupPanel = false;
            gridViewBangGia.OptionsView.RowAutoHeight = true;
            gridViewBangGia.RowHeight = 32;
        }

        public void InitIcons()
        {
            btnThem.Image = IconHelper.GetBitmap(IconChar.Plus, Color.White, 18);
            btnSua.Image = IconHelper.GetBitmap(IconChar.PenToSquare, Color.White, 18);
            btnXoa.Image = IconHelper.GetBitmap(IconChar.TrashCan, Color.White, 18);
        }

        public void LoadData()
        {
            LoadSanPham();
        }

        // =================== INIT ===================
        private void WireEvents()
        {
            this.Load += (s, e) => LoadData();
            slkSanPham.EditValueChanged += SlkSanPham_EditValueChanged;
            gridViewBangGia.FocusedRowChanged += GridViewBangGia_FocusedRowChanged;
            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
        }

        // =================== LOAD DATA ===================
        private void LoadSanPham()
        {
            var ds = BUS_SanPham.Instance.LoadDS()
                .Select(x => new { x.Id, Display = $"{x.Ten} [{x.LoaiSanPham}]", x.LoaiSanPham })
                .OrderBy(x => x.Display)
                .ToList();
            slkSanPham.Properties.DataSource = ds;
            slkSanPham.Properties.DisplayMember = "Display";
            slkSanPham.Properties.ValueMember = "Id";

            ThemeManager.StyleDevExpressGrid(slkSanPham.Properties.View.GridControl);
            slkSanPham.Properties.Appearance.BackColor = Color.White;
            slkSanPham.Properties.Appearance.ForeColor = ThemeManager.TextPrimaryColor;
            slkSanPham.Properties.Appearance.Font = ThemeManager.GetFont(10f);
            slkSanPham.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            slkSanPham.Properties.Appearance.BorderColor = ThemeManager.BorderColor;
            slkSanPham.Properties.AppearanceFocused.BorderColor = ThemeManager.PrimaryColor;
            slkSanPham.Properties.AutoHeight = false;
            slkSanPham.Height = 36;
        }

        private void LoadBangGiaTheoSanPham(int idSanPham)
        {
            _selectedIdSanPham = idSanPham;
            _dsBangGia = DAL_BangGia.Instance.LayTheoSanPham(idSanPham);

            gridViewBangGia.OptionsBehavior.AutoPopulateColumns = false;

            if (gridViewBangGia.Columns.Count == 0)
            {
                FormatGridBangGia();
            }

            gridControlBangGia.DataSource = new System.ComponentModel.BindingList<ET_BangGia>(_dsBangGia);
        }

        private void FormatGridBangGia()
        {
            string numFmt = "#,##0";

            var colGiaThuong = gridViewBangGia.Columns.AddVisible("GiaNgayThuong", "Giá N.Thường");
            colGiaThuong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colGiaThuong.DisplayFormat.FormatString = numFmt;
            colGiaThuong.VisibleIndex = 0;

            var colGiaCuoiTuan = gridViewBangGia.Columns.AddVisible("GiaCuoiTuan", "Giá Cuối Tuần");
            colGiaCuoiTuan.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colGiaCuoiTuan.DisplayFormat.FormatString = numFmt;
            colGiaCuoiTuan.VisibleIndex = 1;

            var colGiaLe = gridViewBangGia.Columns.AddVisible("GiaNgayLe", "Giá Ngày Lễ");
            colGiaLe.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colGiaLe.DisplayFormat.FormatString = numFmt;
            colGiaLe.VisibleIndex = 2;

            var colTienCoc = gridViewBangGia.Columns.AddVisible("TienCoc", "Tiền Cọc");
            colTienCoc.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colTienCoc.DisplayFormat.FormatString = numFmt;
            colTienCoc.VisibleIndex = 3;

            var colPhutBlock = gridViewBangGia.Columns.AddVisible("PhutBlock", "Block (phút)");
            colPhutBlock.VisibleIndex = 4;

            var colPhuThu = gridViewBangGia.Columns.AddVisible("GiaPhuThu", "Phụ thu lố");
            colPhuThu.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhuThu.DisplayFormat.FormatString = numFmt;
            colPhuThu.VisibleIndex = 5;

            var colLoai = gridViewBangGia.Columns.AddVisible("LoaiGia", "Loại");
            colLoai.VisibleIndex = 6;
            colLoai.OptionsColumn.AllowEdit = false;

            var colTT = gridViewBangGia.Columns.AddVisible("TrangThai", "TT");
            colTT.VisibleIndex = 7;

            gridViewBangGia.OptionsView.ColumnAutoWidth = true;
            gridViewBangGia.BestFitColumns();
        }

        // =================== EVENTS ===================
        private void SlkSanPham_EditValueChanged(object sender, EventArgs e)
        {
            if (slkSanPham.EditValue != null && int.TryParse(slkSanPham.EditValue.ToString(), out int idSP))
            {
                _selectedIdSanPham = idSP;
                LoadBangGiaTheoSanPham(idSP);
            }
        }

        private void GridViewBangGia_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                _currentBangGia = gridViewBangGia.GetRow(e.FocusedRowHandle) as ET_BangGia;
                if (_currentBangGia != null)
                {
                    PopulateFields(_currentBangGia);
                }
            }
        }

        private void PopulateFields(ET_BangGia bg)
        {
            txtGiaTien.Text = bg.GiaNgayThuong.ToString("#,##0");
        }

        private void ClearFields()
        {
            txtGiaTien.Text = "";
            _currentBangGia = null;
        }

        // =================== CRUD BẢNG GIÁ (FLAT) ===================
        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (!_selectedIdSanPham.HasValue)
            {
                TDCMessageBox.Show("Vui lòng chọn sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtGiaTien.Text.Replace(",", "").Replace(".", ""), out decimal gia))
            {
                TDCMessageBox.Show("Giá tiền không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var et = new ET_BangGia
            {
                IdSanPham = _selectedIdSanPham.Value,
                GiaNgayThuong = gia,
                GiaCuoiTuan = Math.Round(gia * 1.2m, 0),
                GiaNgayLe = Math.Round(gia * 1.5m, 0),
                TrangThai = "HoạtĐộng",
                CreatedBy = ET.SessionManager.CurrentUser?.Id > 0 ? ET.SessionManager.CurrentUser.Id : (int?)null
            };

            if (DAL_BangGia.Instance.Them(et))
            {
                LoadBangGiaTheoSanPham(_selectedIdSanPham.Value);
                ClearFields();
            }
            else
                TDCMessageBox.Show("Thêm thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (_currentBangGia == null)
            {
                TDCMessageBox.Show("Vui lòng chọn dòng cần sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtGiaTien.Text.Replace(",", "").Replace(".", ""), out decimal gia))
            {
                TDCMessageBox.Show("Giá tiền không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _currentBangGia.GiaNgayThuong = gia;

            if (DAL_BangGia.Instance.Sua(_currentBangGia))
                LoadBangGiaTheoSanPham(_selectedIdSanPham.Value);
            else
                TDCMessageBox.Show("Sửa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (_currentBangGia == null) return;
            if (TDCMessageBox.Show($"Xóa dòng giá {_currentBangGia.GiaNgayThuong:N0}đ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (DAL_BangGia.Instance.Xoa(_currentBangGia.Id))
                {
                    LoadBangGiaTheoSanPham(_selectedIdSanPham.Value);
                    ClearFields();
                }
            }
        }
    }
}

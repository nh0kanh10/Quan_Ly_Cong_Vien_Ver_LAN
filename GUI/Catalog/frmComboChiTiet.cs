using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BUS;
using ET;

namespace GUI
{
    public partial class frmComboChiTiet : Form, IBaseForm
    {
        private ET_Combo _combo;

        public frmComboChiTiet(ET_Combo combo)
        {
            InitializeComponent();
            _combo = combo;
            
            InitIcons();
            ApplyStyles();
            ApplyPermissions();
            
            lblTitle.Text = "CẤU HÌNH COMBO: " + _combo.Ten.ToUpper();
            
            LoadData();
        }

        public void ApplyPermissions()
        {
            var tk = ET.SessionManager.CurrentUser;
            if (tk == null) return;

            // This is a management sub-form, gate with MANAGE_COMBO
            if (!BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "MANAGE_COMBO"))
            {
                this.Enabled = false;
                return;
            }
        }

        public void ApplyStyles()
        {
            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleDevExpressGrid(gridData);
        }

        public void InitIcons()
        {
            btnThem.Image = IconHelper.GetBitmap(FontAwesome.Sharp.IconChar.Plus, Color.White, 14);
            btnXoa.Image = IconHelper.GetBitmap(FontAwesome.Sharp.IconChar.Trash, Color.White, 14);
        }

        public void LoadData()
        {
            LoadSanPham();
            LoadGrid();
        }

        private void gridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) BtnXoa_Click(null, null);
        }

        private void LoadSanPham()
        {
            var ds = BUS_SanPham.Instance.LoadDS().Where(x => x.TrangThai == AppConstants.TrangThaiSanPham.DangBan).ToList();
            cboSanPham.DataSource = ds;
            cboSanPham.DisplayMember = "Ten";
            cboSanPham.ValueMember = "Id";
        }

        private void LoadGrid()
        {
            var data = BUS_Combo.Instance.LayChiTiet(_combo.Id);
            
            // Join with SanPham for display Name
            var sanPhams = BUS_SanPham.Instance.LoadDS();
            var displayData = data.Select(x => new
            {
                x.Id,
                x.IdSanPham,
                TenSanPham = sanPhams.FirstOrDefault(s => s.Id == x.IdSanPham)?.Ten ?? "N/A",
                x.SoLuong,
                x.TyLePhanBo
            }).ToList();

            gridData.DataSource = displayData;
            gridView.PopulateColumns();

            if (gridView.Columns.Count > 0)
            {
                if (gridView.Columns["Id"] != null) gridView.Columns["Id"].Visible = false;
                if (gridView.Columns["IdSanPham"] != null) gridView.Columns["IdSanPham"].Visible = false;
                
                if (gridView.Columns["TenSanPham"] != null) 
                { 
                    gridView.Columns["TenSanPham"].Caption = "Sản Phẩm"; 
                    gridView.Columns["TenSanPham"].Width = 250; 
                }
                if (gridView.Columns["SoLuong"] != null) 
                { 
                    gridView.Columns["SoLuong"].Caption = "Số Lượng"; 
                    gridView.Columns["SoLuong"].Width = 100; 
                }
                if (gridView.Columns["TyLePhanBo"] != null)
                {
                    gridView.Columns["TyLePhanBo"].Caption = "Tỷ lệ (%)";
                    gridView.Columns["TyLePhanBo"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gridView.Columns["TyLePhanBo"].DisplayFormat.FormatString = "N2";
                    gridView.Columns["TyLePhanBo"].Width = 100;
                }
            }

            // Update Info label
            decimal currentTotal = data.Sum(x => x.TyLePhanBo);
            lblTongTyLe.Text = string.Format("Tổng tỷ lệ phân bổ: {0}% / 100%", currentTotal.ToString("N2"));
            if (currentTotal == 100)
            {
                lblTongTyLe.ForeColor = Color.FromArgb(16, 185, 129); // Green
            }
            else if (currentTotal > 100)
            {
                lblTongTyLe.ForeColor = Color.FromArgb(239, 68, 68); // Red
            }
            else
            {
                lblTongTyLe.ForeColor = Color.FromArgb(245, 158, 11); // Orange
            }
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (cboSanPham.SelectedValue == null) return;
            
            int idSanPham = (int)cboSanPham.SelectedValue;
            int soLuong = (int)spnSoLuong.Value;
            decimal tyLe = spnTyLe.Value;

            if (tyLe <= 0)
            {
                TDCMessageBox.Show("Tỷ lệ phân bổ phải > 0%", "Cảnh báo");
                return;
            }

            var result = BUS_Combo.Instance.ThemChiTiet(_combo.Id, idSanPham, soLuong, tyLe);
            if (result.IsSuccess)
            {
                // reset input
                spnSoLuong.Value = 1;
                spnTyLe.Value = 0;
                LoadGrid();
            }
            else
            {
                TDCMessageBox.Show(result.ErrorMessage, "Lỗi");
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (gridView.FocusedRowHandle < 0) return;

            int idCT = Convert.ToInt32(gridView.GetRowCellValue(gridView.FocusedRowHandle, "Id"));
            
            if (TDCMessageBox.Show("Xóa chi tiết này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var result = BUS_Combo.Instance.XoaChiTiet(idCT);
                if (result.IsSuccess) LoadGrid();
                else TDCMessageBox.Show(result.ErrorMessage, "Lỗi");
            }
        }
    }
}



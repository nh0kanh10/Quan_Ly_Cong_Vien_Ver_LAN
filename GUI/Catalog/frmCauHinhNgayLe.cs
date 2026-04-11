using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ET;
using BUS;

namespace GUI.Catalog
{
    public partial class frmCauHinhNgayLe : Form, IBaseForm
    {
        private List<ET_CauHinhNgayLe> _list = new List<ET_CauHinhNgayLe>();
        private ET_CauHinhNgayLe _current = null;

        public frmCauHinhNgayLe()
        {
            InitializeComponent();
            ApplyStyles();
            ApplyPermissions();
            InitIcons();
            
            this.Load += FrmCauHinhNgayLe_Load;
        }

        private void FrmCauHinhNgayLe_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void ApplyPermissions() { }

        public void ApplyStyles()
        {
            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleDevExpressGrid(gridControl);
        }

        public void InitIcons() { }

        public void LoadData()
        {
            _list = BUS_CauHinhNgayLe.Instance.LoadDS();
            gridControl.DataSource = _list;
            gridView.PopulateColumns();

            if (gridView.Columns["Id"] != null) gridView.Columns["Id"].Visible = false;
            if (gridView.Columns["TenNgayLe"] != null) { gridView.Columns["TenNgayLe"].Caption = "Tên Lễ/Sự kiện"; gridView.Columns["TenNgayLe"].Width = 150; }
            if (gridView.Columns["NgayBatDau"] != null) { gridView.Columns["NgayBatDau"].Caption = "Bắt đầu"; gridView.Columns["NgayBatDau"].DisplayFormat.FormatString = "dd/MM/yyyy"; }
            if (gridView.Columns["NgayKetThuc"] != null) { gridView.Columns["NgayKetThuc"].Caption = "Kết thúc"; gridView.Columns["NgayKetThuc"].DisplayFormat.FormatString = "dd/MM/yyyy"; }
            if (gridView.Columns["MoTa"] != null) gridView.Columns["MoTa"].Caption = "Mô tả";

            gridView.BestFitColumns();
            BtnLamMoi_Click(null, null);
        }

        private void GridControl_Click(object sender, EventArgs e)
        {
            if (gridView.GetFocusedRow() is ET_CauHinhNgayLe row)
            {
                _current = row;
                txtTen.Text = row.TenNgayLe;
                dtpBatDau.Value = row.NgayBatDau;
                dtpKetThuc.Value = row.NgayKetThuc;
                txtMoTa.Text = row.MoTa;

                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            _current = null;
            txtTen.Clear();
            txtMoTa.Clear();
            dtpBatDau.Value = DateTime.Now;
            dtpKetThuc.Value = DateTime.Now;

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                TDCMessageBox.Show("Vui lòng nhập tên ngày lễ/sự kiện!", "Thông báo");
                txtTen.Focus();
                return;
            }

            if (dtpBatDau.Value.Date > dtpKetThuc.Value.Date)
            {
                TDCMessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var et = new ET_CauHinhNgayLe
            {
                TenNgayLe = txtTen.Text.Trim(),
                NgayBatDau = dtpBatDau.Value.Date,
                NgayKetThuc = dtpKetThuc.Value.Date,
                MoTa = txtMoTa.Text.Trim()
            };

            if (BUS_CauHinhNgayLe.Instance.Them(et))
            {
                LoadData();
            }
            else
            {
                TDCMessageBox.Show("Thêm thất bại!", "Thông báo");
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (_current == null) return;

            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                TDCMessageBox.Show("Vui lòng nhập tên ngày lễ/sự kiện!", "Thông báo");
                return;
            }

            if (dtpBatDau.Value.Date > dtpKetThuc.Value.Date)
            {
                TDCMessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _current.TenNgayLe = txtTen.Text.Trim();
            _current.NgayBatDau = dtpBatDau.Value.Date;
            _current.NgayKetThuc = dtpKetThuc.Value.Date;
            _current.MoTa = txtMoTa.Text.Trim();

            if (BUS_CauHinhNgayLe.Instance.Sua(_current))
            {
                LoadData();
            }
            else
            {
                TDCMessageBox.Show("Cập nhật thất bại!", "Thông báo");
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (_current == null) return;

            if (TDCMessageBox.Show($"Bạn có chắc chắn muốn xóa sự kiện '{_current.TenNgayLe}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (BUS_CauHinhNgayLe.Instance.Xoa(_current.Id))
                {
                    LoadData();
                }
                else
                {
                    TDCMessageBox.Show("Xóa thất bại! Ngày lễ này có thể đang được cấu hình ở bảng giá.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}

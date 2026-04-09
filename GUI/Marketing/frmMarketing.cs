using System;
using System.Drawing;
using System.Windows.Forms;
using ET;
using BUS;
using System.Collections.Generic;
using FontAwesome.Sharp;

namespace GUI
{
    public partial class frmMarketing : Form, IBaseForm
    {
        public frmMarketing()
        {
            InitializeComponent();
            if (ThemeManager.IsInDesignMode(this)) return;
            InitIcons();
            ApplyStyles();
            ApplyPermissions();
            
        }

        private void dtpNgayBatDauSK_ValueChanged(object sender, EventArgs e)
        {
            if (dtpNgayKetThucSK.DateTime < dtpNgayBatDauSK.DateTime) 
                dtpNgayKetThucSK.DateTime = dtpNgayBatDauSK.DateTime;
        }

        private void dtpNgayBatDauKM_ValueChanged(object sender, EventArgs e)
        {
            if (dtpNgayKetThucKM.DateTime < dtpNgayBatDauKM.DateTime) 
                dtpNgayKetThucKM.DateTime = dtpNgayBatDauKM.DateTime;
        }

        public void ApplyPermissions()
        {
            var tk = ET.SessionManager.CurrentUser;
            if (tk == null) return;

            if (!BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "VIEW_PROMOTION"))
            {
                this.Enabled = false;
                return;
            }

            bool canManage = BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "MANAGE_PROMOTION");
            
            // Events
            btnThemSuKien.Enabled = canManage;
            btnSuaSuKien.Enabled = canManage;
            btnXoaSuKien.Enabled = canManage;

            // Promotions
            btnThemKhuyenMai.Enabled = canManage;
            btnSuaKhuyenMai.Enabled = canManage;
            btnXoaKhuyenMai.Enabled = canManage;
        }

        public void ApplyStyles()
        {
            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleDevExpressGrid(gridSuKien);
            ThemeManager.StyleDevExpressGrid(gridKhuyenMai);
        }

        public void InitIcons()
        {
            btnThemSuKien.Image = IconHelper.GetBitmap(IconChar.PlusCircle, Color.White, 20);
            btnSuaSuKien.Image = IconHelper.GetBitmap(IconChar.Edit, Color.White, 20);
            btnXoaSuKien.Image = IconHelper.GetBitmap(IconChar.Trash, Color.White, 20);
            btnLamMoiSuKien.Image = IconHelper.GetBitmap(IconChar.Rotate, Color.White, 20);

            btnThemKhuyenMai.Image = IconHelper.GetBitmap(IconChar.PlusCircle, Color.White, 20);
            btnSuaKhuyenMai.Image = IconHelper.GetBitmap(IconChar.Edit, Color.White, 20);
            btnXoaKhuyenMai.Image = IconHelper.GetBitmap(IconChar.Trash, Color.White, 20);
            btnLamMoiKhuyenMai.Image = IconHelper.GetBitmap(IconChar.Rotate, Color.White, 20);
        }

        public void LoadData()
        {
            LoadDataSuKien();
            LoadDataKhuyenMai();
        }

        private void frmMarketing_Load(object sender, EventArgs e)
        {
            LoadDataSuKien();
            LoadDataKhuyenMai();
        }

        #region "SỰ KIỆN"
        private void LoadDataSuKien()
        {
            var data = BUS_SuKien.Instance.LoadDS();
            
            gridSuKien.ForceInitialize();
            gridSuKien.DataSource = data;
            gridViewSuKien.PopulateColumns();

            // Load Combobox in KhuyenMai Tab
            var optList = new List<ET_SuKien>();
            optList.Add(new ET_SuKien { Id = 0, TenSuKien = "[Không áp dụng]" });
            if (data != null) optList.AddRange(data);
            
            slkSuKien.Properties.DataSource = optList;
            slkSuKien.Properties.DisplayMember = "TenSuKien";
            slkSuKien.Properties.ValueMember = "Id";
            ThemeManager.StyleSearchLookUpEdit(slkSuKien, new[] { "TenSuKien" }, new[] { "Tên Sự Kiện" });

            var view = gridViewSuKien;
            if (view.Columns.Count > 0)
            {
                if (view.Columns["Id"] != null) view.Columns["Id"].Visible = false;
                if (view.Columns["MaCode"] != null) view.Columns["MaCode"].Caption = "Mã Sự Kiện";
                if (view.Columns["TenSuKien"] != null) view.Columns["TenSuKien"].Caption = "Tên Sự Kiện";
                if (view.Columns["MoTa"] != null) view.Columns["MoTa"].Caption = "Mô Tả";
                if (view.Columns["NgayBatDau"] != null)
                {
                    view.Columns["NgayBatDau"].Caption = "Bắt Đầu";
                    view.Columns["NgayBatDau"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    view.Columns["NgayBatDau"].DisplayFormat.FormatString = "dd/MM/yyyy";
                }
                if (view.Columns["NgayKetThuc"] != null)
                {
                    view.Columns["NgayKetThuc"].Caption = "Kết Thúc";
                    view.Columns["NgayKetThuc"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    view.Columns["NgayKetThuc"].DisplayFormat.FormatString = "dd/MM/yyyy";
                }
                if (view.Columns["TrangThai"] != null) view.Columns["TrangThai"].Caption = "Trạng Thái";
                
                // Hide helper/alias columns
                string[] hidden = { "CreatedAt", "CreatedBy", "IsDeleted", "MaSuKien" };
                foreach (var col in hidden) if (view.Columns[col] != null) view.Columns[col].Visible = false;
            }
        }

        private void GridViewSuKien_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridViewSuKien.FocusedRowHandle >= 0)
            {
                var row = gridViewSuKien.GetFocusedRow() as ET_SuKien;
                if (row == null) return;

                txtMaSuKien.Text = row.Id.ToString();
                txtTenSuKien.Text = row.TenSuKien ?? "";
                txtMoTa.Text = row.MoTa ?? "";
                dtpNgayBatDauSK.DateTime = row.NgayBatDau;
                dtpNgayKetThucSK.DateTime = row.NgayKetThuc;
                cboTrangThaiSK.Text = row.TrangThai ?? "Sắp diễn ra";
            }
        }

        private void ResetSuKien()
        {
            txtMaSuKien.Clear();
            txtTenSuKien.Clear();
            txtMoTa.Clear();
            dtpNgayBatDauSK.DateTime = DateTime.Today;
            dtpNgayKetThucSK.DateTime = DateTime.Today.AddDays(7);
            cboTrangThaiSK.SelectedIndex = 0; // "Sắp diễn ra"
        }

        private void BtnLamMoiSuKien_Click(object sender, EventArgs e)
        {
            ResetSuKien();
            LoadDataSuKien();
        }

        private void BtnThemSuKien_Click(object sender, EventArgs e)
        {
            var et = new ET_SuKien
            {
                TenSuKien = txtTenSuKien.Text.Trim(),
                MoTa = txtMoTa.Text.Trim(),
                NgayBatDau = dtpNgayBatDauSK.DateTime,
                NgayKetThuc = dtpNgayKetThucSK.DateTime,
                TrangThai = cboTrangThaiSK.Text
            };

            if (string.IsNullOrEmpty(et.TenSuKien))
            {
                TDCMessageBox.Show("Tên sự kiện không được để trống!", "Lỗi");
                return;
            }

            var res = BUS_SuKien.Instance.ThemSuKien(et);
            if (res.IsSuccess)
            {
                TDCMessageBox.Show("Thêm sự kiện thành công!", "Thông báo");
                BtnLamMoiSuKien_Click(null, null);
            }
            else
            {
                TDCMessageBox.Show(res.ErrorMessage, "Lỗi");
            }
        }

        private void BtnSuaSuKien_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSuKien.Text)) return;
            var et = new ET_SuKien
            {
                Id = int.Parse(txtMaSuKien.Text),
                TenSuKien = txtTenSuKien.Text.Trim(),
                MoTa = txtMoTa.Text.Trim(),
                NgayBatDau = dtpNgayBatDauSK.DateTime,
                NgayKetThuc = dtpNgayKetThucSK.DateTime,
                TrangThai = cboTrangThaiSK.Text
            };

            var res = BUS_SuKien.Instance.SuaSuKien(et);
            if (res.IsSuccess)
            {
                TDCMessageBox.Show("Cập nhật sự kiện thành công!", "Thông báo");
                BtnLamMoiSuKien_Click(null, null);
                LoadDataKhuyenMai(); 
            }
            else
            {
                TDCMessageBox.Show(res.ErrorMessage, "Lỗi");
            }
        }

        private void BtnXoaSuKien_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSuKien.Text)) return;
            if (TDCMessageBox.Show("Bạn có chắc muốn xóa sự kiện này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var res = BUS_SuKien.Instance.XoaSuKien(int.Parse(txtMaSuKien.Text));
                if (res.IsSuccess)
                {
                    TDCMessageBox.Show("Xóa thành công!", "Thông báo");
                    BtnLamMoiSuKien_Click(null, null);
                }
                else
                {
                    TDCMessageBox.Show(res.ErrorMessage, "Lỗi");
                }
            }
        }
        #endregion

        #region "KHUYẾN MÃI"
        private void LoadDataKhuyenMai()
        {
            gridKhuyenMai.ForceInitialize();
            gridKhuyenMai.DataSource = BUS_KhuyenMai.Instance.LoadDS();
            gridViewKhuyenMai.PopulateColumns();

            var view = gridViewKhuyenMai;
            if (view.Columns.Count > 0)
            {
                if (view.Columns["Id"] != null) view.Columns["Id"].Visible = false;
                if (view.Columns["MaCode"] != null) view.Columns["MaCode"].Caption = "Mã KM / Code";
                if (view.Columns["TenKhuyenMai"] != null) view.Columns["TenKhuyenMai"].Caption = "Tên Khuyến Mãi";
                if (view.Columns["LoaiGiamGia"] != null) view.Columns["LoaiGiamGia"].Caption = "Loại";
                if (view.Columns["GiaTriGiam"] != null)
                {
                    view.Columns["GiaTriGiam"].Caption = "Giá Trị";
                    view.Columns["GiaTriGiam"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    view.Columns["GiaTriGiam"].DisplayFormat.FormatString = "N0";
                }
                if (view.Columns["DonToiThieu"] != null)
                {
                    view.Columns["DonToiThieu"].Caption = "Đơn Tối Thiểu";
                    view.Columns["DonToiThieu"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    view.Columns["DonToiThieu"].DisplayFormat.FormatString = "N0";
                }
                if (view.Columns["NgayBatDau"] != null)
                {
                    view.Columns["NgayBatDau"].Caption = "Bắt Đầu";
                    view.Columns["NgayBatDau"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    view.Columns["NgayBatDau"].DisplayFormat.FormatString = "dd/MM/yyyy";
                }
                if (view.Columns["NgayKetThuc"] != null)
                {
                    view.Columns["NgayKetThuc"].Caption = "Kết Thúc";
                    view.Columns["NgayKetThuc"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    view.Columns["NgayKetThuc"].DisplayFormat.FormatString = "dd/MM/yyyy";
                }
                if (view.Columns["TrangThai"] != null) view.Columns["TrangThai"].Caption = "Hoạt Động";
                
                // Hide helper/alias columns
                string[] hidden = { "IdSuKien", "MaSuKien", "MaKhuyenMai", "CreatedAt", "CreatedBy", "IsDeleted" };
                foreach (var col in hidden) if (view.Columns[col] != null) view.Columns[col].Visible = false;
            }
        }

        private void GridViewKhuyenMai_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridViewKhuyenMai.FocusedRowHandle >= 0)
            {
                var row = gridViewKhuyenMai.GetFocusedRow() as ET_KhuyenMai;
                if (row == null) return;

                txtMaKhuyenMai.Text = row.Id.ToString();
                txtTenKhuyenMai.Text = row.TenKhuyenMai ?? "";
                cboLoaiGiamGia.Text = row.LoaiGiamGia ?? "PhanTram";
                
                txtGiaTriGiam.Text = row.GiaTriGiam.ToString("N0");
                txtDonToiThieu.Text = row.DonToiThieu.HasValue ? row.DonToiThieu.Value.ToString("N0") : "0";
                
                dtpNgayBatDauKM.DateTime = row.NgayBatDau;
                dtpNgayKetThucKM.DateTime = row.NgayKetThuc;
                
                slkSuKien.EditValue = row.IdSuKien ?? 0;
                chkTrangThaiKM.Checked = row.TrangThai;
            }
        }

        private void ResetKhuyenMai()
        {
            txtMaKhuyenMai.Clear();
            txtTenKhuyenMai.Clear();
            cboLoaiGiamGia.SelectedIndex = 0; // PhanTram
            txtGiaTriGiam.Text = "0";
            txtDonToiThieu.Text = "0";
            dtpNgayBatDauKM.DateTime = DateTime.Today;
            dtpNgayKetThucKM.DateTime = DateTime.Today.AddDays(7);
            slkSuKien.EditValue = 0;
            chkTrangThaiKM.Checked = true;
        }

        private void BtnLamMoiKhuyenMai_Click(object sender, EventArgs e)
        {
            ResetKhuyenMai();
            LoadDataKhuyenMai();
        }

        private void BtnThemKhuyenMai_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtGiaTriGiam.Text, out decimal giaTriGiam) || 
                !decimal.TryParse(txtDonToiThieu.Text, out decimal donToiThieu))
            {
                TDCMessageBox.Show("Giá trị giảm và Đơn tối thiểu phải là số hợp lệ!", "Lỗi");
                return;
            }

            var et = new ET_KhuyenMai
            {
                TenKhuyenMai = txtTenKhuyenMai.Text.Trim(),
                LoaiGiamGia = cboLoaiGiamGia.Text,
                GiaTriGiam = giaTriGiam,
                DonToiThieu = donToiThieu,
                NgayBatDau = dtpNgayBatDauKM.DateTime,
                NgayKetThuc = dtpNgayKetThucKM.DateTime,
                TrangThai = chkTrangThaiKM.Checked,
                IdSuKien = Convert.ToInt32(slkSuKien.EditValue)
            };

            if (string.IsNullOrEmpty(et.TenKhuyenMai))
            {
                TDCMessageBox.Show("Tên khuyến mãi không được để trống!", "Lỗi");
                return;
            }

            var res = BUS_KhuyenMai.Instance.ThemKhuyenMai(et);
            if (res.IsSuccess)
            {
                TDCMessageBox.Show("Thêm khuyến mãi thành công!", "Thông báo");
                BtnLamMoiKhuyenMai_Click(null, null);
            }
            else
            {
                TDCMessageBox.Show(res.ErrorMessage, "Lỗi");
            }
        }

        private void BtnSuaKhuyenMai_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKhuyenMai.Text)) return;
            if (!decimal.TryParse(txtGiaTriGiam.Text, out decimal giaTriGiam) || 
                !decimal.TryParse(txtDonToiThieu.Text, out decimal donToiThieu))
            {
                TDCMessageBox.Show("Giá trị giảm và Đơn tối thiểu phải là số hợp lệ!", "Lỗi");
                return;
            }

            var et = new ET_KhuyenMai
            {
                Id = int.Parse(txtMaKhuyenMai.Text),
                TenKhuyenMai = txtTenKhuyenMai.Text.Trim(),
                LoaiGiamGia = cboLoaiGiamGia.Text,
                GiaTriGiam = giaTriGiam,
                DonToiThieu = donToiThieu,
                NgayBatDau = dtpNgayBatDauKM.DateTime,
                NgayKetThuc = dtpNgayKetThucKM.DateTime,
                TrangThai = chkTrangThaiKM.Checked,
                IdSuKien = Convert.ToInt32(slkSuKien.EditValue)
            };

            var res = BUS_KhuyenMai.Instance.SuaKhuyenMai(et);
            if (res.IsSuccess)
            {
                TDCMessageBox.Show("Cập nhật khuyến mãi thành công!", "Thông báo");
                BtnLamMoiKhuyenMai_Click(null, null);
            }
            else
            {
                TDCMessageBox.Show(res.ErrorMessage, "Lỗi");
            }
        }

        private void BtnXoaKhuyenMai_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKhuyenMai.Text)) return;
            if (TDCMessageBox.Show("Bạn có chắc muốn xóa khuyến mãi này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var res = BUS_KhuyenMai.Instance.XoaKhuyenMai(int.Parse(txtMaKhuyenMai.Text));
                if (res.IsSuccess)
                {
                    TDCMessageBox.Show("Xóa thành công!", "Thông báo");
                    BtnLamMoiKhuyenMai_Click(null, null);
                }
                else
                {
                    TDCMessageBox.Show(res.ErrorMessage, "Lỗi");
                }
            }
        }
        #endregion
    }
}




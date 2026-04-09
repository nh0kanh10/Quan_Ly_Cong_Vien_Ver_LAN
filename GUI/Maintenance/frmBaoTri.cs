using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BUS;
using ET;
using FontAwesome.Sharp;

namespace GUI
{
    public partial class frmBaoTri : Form, IBaseForm
    {
        private ET_DanhSachThietBi _currentTB;

        public frmBaoTri()
        {
            InitializeComponent();
            InitIcons();
            ApplyStyles();
            ApplyPermissions();
        }

        public void ApplyPermissions() { }

        public void ApplyStyles()
        {
            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleDevExpressGrid(gridThietBi);
            ThemeManager.StyleDevExpressGrid(gridBaoTri);
        }

        public void InitIcons()
        {
            btnThemTB.Image = IconHelper.GetBitmap(IconChar.Plus, Color.White, 16);
            btnSuaTB.Image = IconHelper.GetBitmap(IconChar.PenToSquare, Color.White, 16);
            btnXoaTB.Image = IconHelper.GetBitmap(IconChar.TrashCan, Color.White, 16);
            btnLamMoi.Image = IconHelper.GetBitmap(IconChar.ArrowsRotate, Color.White, 16);
            btnThemBT.Image = IconHelper.GetBitmap(IconChar.Wrench, Color.White, 16);
            btnHoanTatBT.Image = IconHelper.GetBitmap(IconChar.CheckCircle, Color.White, 16);
            btnXoaBT.Image = IconHelper.GetBitmap(IconChar.TrashCan, Color.White, 16);
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            LoadThietBi();
        }

        private void cboLoaiThietBi_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadThietBi();
        }

        private void cboTrangThaiLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadThietBi();
        }

        private void frmBaoTri_Load(object sender, EventArgs e)
        {
            InitCombos();
            LoadThietBi();
        }

        #region COMBOS
        private void InitCombos()
        {
            // Loai thiet bi filter
            cboLoaiThietBi.Items.Clear();
            cboLoaiThietBi.Items.AddRange(new object[] {
                "Tất cả", "Trò chơi", "Máy tạo sóng", "Xe điện", "Kiosk",
                "Bàn ăn", "Ngựa đua", "Phương tiện đua", "Khác"
            });
            cboLoaiThietBi.SelectedIndex = 0;

            // Trang thai filter
            cboTrangThaiLoc.Items.Clear();
            cboTrangThaiLoc.Items.AddRange(new object[] {
                "Tất cả", "Hoạt động", "Đang bảo trì", "Tạm đóng", "Hỏng", "Thanh lý"
            });
            cboTrangThaiLoc.SelectedIndex = 0;

            // Form fields - Loai thiet bi
            cboLoaiTB.Items.Clear();
            cboLoaiTB.Items.AddRange(new object[] {
                "TroChoi", "TaoSong", "XeDien", "Kiosk", "BanAn", "NguaDua", "PhuongTienDua", "Khac"
            });

            // Form fields - Trang thai
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.AddRange(new object[] {
                "HoatDong", "BaoTri", "TamDong", "Hong", "ThanhLy"
            });

            // Khu vuc
            var dsKhuVuc = BUS_KhuVuc.Instance.LoadDS();
            slkKhuVuc.Properties.DataSource = dsKhuVuc;
            slkKhuVuc.Properties.DisplayMember = "TenKhuVuc";
            slkKhuVuc.Properties.ValueMember = "Id";
            ThemeManager.StyleSearchLookUpEdit(slkKhuVuc, new[] { "TenKhuVuc" }, new[] { "Khu vực" });
        }

        #endregion

        #region LOAD DATA
        public void LoadData() => LoadThietBi();

        private void LoadThietBi()
        {
            string keyword = txtTimKiem.Text.Trim();
            string loai = GetLoaiThietBiCode(cboLoaiThietBi.Text);
            string trangThai = GetTrangThaiCode(cboTrangThaiLoc.Text);

            var ds = BUS_DanhSachThietBi.Instance.TimKiem(keyword, loai, trangThai);
            gridThietBi.DataSource = ds;
            gridViewThietBi.PopulateColumns();
            FormatGridThietBi();
        }

        private void LoadLichBaoTri(int idThietBi)
        {
            var ds = BUS_LichBaoTri.Instance.LoadTheoThietBi(idThietBi);
            gridBaoTri.DataSource = ds;
            gridViewBaoTri.PopulateColumns();
            FormatGridBaoTri();
        }

        #endregion

        #region FORMAT GRIDS
        private void FormatGridThietBi()
        {
            var v = gridViewThietBi;
            string[] hidden = { "Id", "IdKhuVuc", "MoTa", "ChuKyBaoTriThang" };
            foreach (var c in hidden) if (v.Columns[c] != null) v.Columns[c].Visible = false;

            if (v.Columns["MaCode"] != null) v.Columns["MaCode"].Caption = "Mã";
            if (v.Columns["TenThietBi"] != null) v.Columns["TenThietBi"].Caption = "Tên thiết bị";
            if (v.Columns["TenLoaiThietBi"] != null) v.Columns["TenLoaiThietBi"].Caption = "Loại";
            if (v.Columns["LoaiThietBi"] != null) v.Columns["LoaiThietBi"].Visible = false;
            if (v.Columns["TenKhuVuc"] != null) v.Columns["TenKhuVuc"].Caption = "Khu vực";
            if (v.Columns["TenTrangThai"] != null) v.Columns["TenTrangThai"].Caption = "Trạng thái";
            if (v.Columns["TrangThai"] != null) v.Columns["TrangThai"].Visible = false;
            if (v.Columns["NgayMua"] != null)
            {
                v.Columns["NgayMua"].Caption = "Ngày mua";
                v.Columns["NgayMua"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                v.Columns["NgayMua"].DisplayFormat.FormatString = "dd/MM/yyyy";
            }
            if (v.Columns["GiaTriMua"] != null)
            {
                v.Columns["GiaTriMua"].Caption = "Giá trị";
                v.Columns["GiaTriMua"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                v.Columns["GiaTriMua"].DisplayFormat.FormatString = "n0";
            }
            v.OptionsView.ColumnAutoWidth = true;
        }

        private void FormatGridBaoTri()
        {
            var v = gridViewBaoTri;
            string[] hidden = { "Id", "IdThietBi", "IdNhanVienThucHien", "IdPhieuChi", "TenThietBi" };
            foreach (var c in hidden) if (v.Columns[c] != null) v.Columns[c].Visible = false;

            if (v.Columns["NgayBaoTri"] != null)
            {
                v.Columns["NgayBaoTri"].Caption = "Ngày";
                v.Columns["NgayBaoTri"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                v.Columns["NgayBaoTri"].DisplayFormat.FormatString = "dd/MM/yyyy";
            }
            if (v.Columns["TenLoaiBaoTri"] != null) v.Columns["TenLoaiBaoTri"].Caption = "Loại BT";
            if (v.Columns["LoaiBaoTri"] != null) v.Columns["LoaiBaoTri"].Visible = false;
            if (v.Columns["NoiDung"] != null) v.Columns["NoiDung"].Caption = "Nội dung";
            if (v.Columns["ChiPhi"] != null)
            {
                v.Columns["ChiPhi"].Caption = "Chi phí";
                v.Columns["ChiPhi"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                v.Columns["ChiPhi"].DisplayFormat.FormatString = "n0";
            }
            if (v.Columns["TenTrangThai"] != null) v.Columns["TenTrangThai"].Caption = "Trạng thái";
            if (v.Columns["TrangThai"] != null) v.Columns["TrangThai"].Visible = false;
            if (v.Columns["TenNhanVien"] != null) v.Columns["TenNhanVien"].Caption = "Nhân viên";
            v.OptionsView.ColumnAutoWidth = true;
        }

        #endregion

        #region UI <-> ENTITY
        private void gridViewThietBi_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridViewThietBi.FocusedRowHandle >= 0)
            {
                _currentTB = gridViewThietBi.GetFocusedRow() as ET_DanhSachThietBi;
                if (_currentTB != null)
                {
                    ShowTBToUI(_currentTB);
                    LoadLichBaoTri(_currentTB.Id);
                }
            }
        }

        private void ShowTBToUI(ET_DanhSachThietBi tb)
        {
            txtTenThietBi.Text = tb.TenThietBi;
            txtMaCode.Text = tb.MaCode;
            cboLoaiTB.Text = tb.LoaiThietBi;
            slkKhuVuc.EditValue = tb.IdKhuVuc;
            cboTrangThai.Text = tb.TrangThai;
            txtMoTa.Text = tb.MoTa;
            if (tb.NgayMua.HasValue)
            {
                dtpNgayMua.DateTime = tb.NgayMua.Value;
                // dtpNgayMua.Checked = true;
            }
            else dtpNgayMua.EditValue = null;

            txtGiaTri.Text = tb.GiaTriMua.HasValue ? tb.GiaTriMua.Value.ToString("n0") : "";
            spnChuKy.Value = tb.ChuKyBaoTriThang ?? 0;
        }

        private ET_DanhSachThietBi GetTBFromUI()
        {
            if (string.IsNullOrWhiteSpace(txtTenThietBi.Text))
            {
                TDCMessageBox.Show("Vui lòng nhập tên thiết bị!", "Thông báo");
                return null;
            }
            if (string.IsNullOrWhiteSpace(cboLoaiTB.Text))
            {
                TDCMessageBox.Show("Vui lòng chọn loại thiết bị!", "Thông báo");
                return null;
            }

            decimal giaTri = 0;
            decimal.TryParse(txtGiaTri.Text.Replace(".", "").Replace(",", ""), out giaTri);

            return new ET_DanhSachThietBi
            {
                Id = _currentTB?.Id ?? 0,
                MaCode = txtMaCode.Text.Trim(),
                TenThietBi = txtTenThietBi.Text.Trim(),
                LoaiThietBi = cboLoaiTB.Text,
                IdKhuVuc = slkKhuVuc.EditValue != null ? (int?)Convert.ToInt32(slkKhuVuc.EditValue) : null,
                MoTa = txtMoTa.Text.Trim(),
                TrangThai = cboTrangThai.Text,
                NgayMua = dtpNgayMua.EditValue != null ? (DateTime?)dtpNgayMua.DateTime.Date : null,
                GiaTriMua = giaTri > 0 ? (decimal?)giaTri : null,
                ChuKyBaoTriThang = (int)spnChuKy.Value > 0 ? (int?)spnChuKy.Value : null
            };
        }

        private void ClearUI()
        {
            txtTenThietBi.Clear();
            txtMaCode.Clear();
            cboLoaiTB.SelectedIndex = -1;
            slkKhuVuc.EditValue = null;
            cboTrangThai.SelectedIndex = -1;
            txtMoTa.Clear();
            dtpNgayMua.EditValue = null;
            txtGiaTri.Clear();
            spnChuKy.Value = 0;
            _currentTB = null;
            gridBaoTri.DataSource = null;
        }

        #endregion

        #region CRUD THIET BI
        private void btnThemTB_Click(object sender, EventArgs e)
        {
            var et = GetTBFromUI();
            if (et == null) return;
            et.Id = 0;
            if (BUS_DanhSachThietBi.Instance.Them(et))
            {
                TDCMessageBox.Show("Thêm thiết bị thành công!", "Thông báo");
                LoadThietBi();
                ClearUI();
            }
            else TDCMessageBox.Show("Lỗi khi thêm thiết bị.", "Lỗi");
        }

        private void btnSuaTB_Click(object sender, EventArgs e)
        {
            if (_currentTB == null) { TDCMessageBox.Show("Chọn thiết bị cần sửa!"); return; }
            var et = GetTBFromUI();
            if (et == null) return;
            if (BUS_DanhSachThietBi.Instance.Sua(et))
            {
                TDCMessageBox.Show("Cập nhật thành công!", "Thông báo");
                LoadThietBi();
            }
            else TDCMessageBox.Show("Lỗi khi cập nhật.", "Lỗi");
        }

        private void btnXoaTB_Click(object sender, EventArgs e)
        {
            if (_currentTB == null) return;
            if (TDCMessageBox.Show("Chắc chắn xóa thiết bị này?", "Xóa", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (BUS_DanhSachThietBi.Instance.Xoa(_currentTB.Id))
                {
                    TDCMessageBox.Show("Xóa thành công!");
                    LoadThietBi();
                    ClearUI();
                }
                else TDCMessageBox.Show("Lỗi khi xóa.", "Lỗi");
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            cboLoaiThietBi.SelectedIndex = 0;
            cboTrangThaiLoc.SelectedIndex = 0;
            ClearUI();
            LoadThietBi();
        }

        #endregion

        #region LICH BAO TRI
        private void btnThemBT_Click(object sender, EventArgs e)
        {
            if (_currentTB == null) { TDCMessageBox.Show("Chọn thiết bị trước!"); return; }

            using (var dlg = new frmThemBaoTri(_currentTB.Id, _currentTB.TenThietBi))
            {
                ThemeManager.ShowAsPopup(dlg);
                if (dlg.DialogResult == DialogResult.OK)
                    LoadLichBaoTri(_currentTB.Id);
            }
        }

        private void btnHoanTatBT_Click(object sender, EventArgs e)
        {
            if (gridViewBaoTri.FocusedRowHandle < 0) return;
            var bt = gridViewBaoTri.GetFocusedRow() as ET_LichBaoTri;
            if (bt == null || bt.TrangThai == "HoanTat") return;

            if (BUS_LichBaoTri.Instance.HoanTat(bt.Id))
            {
                TDCMessageBox.Show("Bảo trì đã hoàn tất!");
                LoadLichBaoTri(_currentTB.Id);
            }
        }

        private void btnXoaBT_Click(object sender, EventArgs e)
        {
            if (gridViewBaoTri.FocusedRowHandle < 0) return;
            var bt = gridViewBaoTri.GetFocusedRow() as ET_LichBaoTri;
            if (bt == null) return;

            if (TDCMessageBox.Show("Xóa lịch bảo trì?", "Xóa", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (BUS_LichBaoTri.Instance.Xoa(bt.Id))
                    LoadLichBaoTri(_currentTB.Id);
            }
        }

        #endregion

        #region HELPERS
        private string GetLoaiThietBiCode(string display)
        {
            switch (display)
            {
                case "Trò chơi": return "TroChoi";
                case "Máy tạo sóng": return "TaoSong";
                case "Xe điện": return "XeDien";
                case "Kiosk": return "Kiosk";
                case "Bàn ăn": return "BanAn";
                case "Ngựa đua": return "NguaDua";
                case "Phương tiện đua": return "PhuongTienDua";
                case "Khác": return "Khac";
                default: return "Tất cả";
            }
        }

        private string GetTrangThaiCode(string display)
        {
            switch (display)
            {
                case "Hoạt động": return "HoatDong";
                case "Đang bảo trì": return "BaoTri";
                case "Tạm đóng": return "TamDong";
                case "Hỏng": return "Hong";
                case "Thanh lý": return "ThanhLy";
                default: return "Tất cả";
            }
        }
        #endregion
    }
}



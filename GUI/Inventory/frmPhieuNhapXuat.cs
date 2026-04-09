using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BUS;
using ET;
using FontAwesome.Sharp;

namespace GUI
{
    public partial class frmPhieuNhapXuat : Form, IBaseForm
    {
        private bool _isNhapMode = true;

        public frmPhieuNhapXuat()
        {
            InitializeComponent();
            if (ThemeManager.IsInDesignMode(this)) return;

            InitIcons();
            ApplyStyles();
            ApplyPermissions();
            LoadData();
        }

        public void ApplyPermissions()
        {
            var tk = ET.SessionManager.CurrentUser;
            if (tk == null) return;
            bool canEdit = BUS.BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "EDIT_INVENTORY");
            btnThem.Enabled = canEdit;
            btnSua.Enabled = canEdit;
            btnXoa.Enabled = canEdit;
        }

        public void ApplyStyles()
        {
            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleDevExpressGrid(gridMaster);
            ThemeManager.StyleDevExpressGrid(gridDetail);
        }

        public void InitIcons()
        {
            btnThem.Image = IconHelper.GetBitmap(IconChar.Plus, Color.White, 20);
            btnSua.Image = IconHelper.GetBitmap(IconChar.Edit, Color.White, 20);
            btnXoa.Image = IconHelper.GetBitmap(IconChar.Trash, Color.White, 20);
            btnLamMoi.Image = IconHelper.GetBitmap(IconChar.Sync, Color.White, 20);
            btnToggleMode.Image = IconHelper.GetBitmap(IconChar.ExchangeAlt, Color.White, 20);
        }

        public void LoadData()
        {
            if (_isNhapMode)
            {
                lblTitle.Text = "QUẢN LÝ PHIẾU NHẬP KHO";
                gbMaster.Text = "Danh sách Phiếu Nhập";
                gridMaster.DataSource = BUS_PhieuNhapKho.Instance.LoadDS();
                gridViewMaster.PopulateColumns();
                FormatMasterGridNhap();
            }
            else
            {
                lblTitle.Text = "QUẢN LÝ PHIẾU XUẤT KHO";
                gbMaster.Text = "Danh sách Phiếu Xuất";
                gridMaster.DataSource = BUS_PhieuXuatKho.Instance.LoadDS();
                gridViewMaster.PopulateColumns();
                FormatMasterGridXuat();
            }
            gridDetail.DataSource = null;
            gbDetail.Text = "Chi tiết phiếu (chọn phiếu ở trên)";
        }

        private void FormatMasterGridNhap()
        {
            var v = gridViewMaster;
            if (v.Columns["Id"] != null) v.Columns["Id"].Caption = "Mã";
            if (v.Columns["SoChungTu"] != null) v.Columns["SoChungTu"].Caption = "Số chứng từ";
            if (v.Columns["NgayNhap"] != null) { v.Columns["NgayNhap"].Caption = "Ngày nhập"; v.Columns["NgayNhap"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime; v.Columns["NgayNhap"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"; }
            if (v.Columns["TongTien"] != null) { v.Columns["TongTien"].Caption = "Tổng tiền"; v.Columns["TongTien"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric; v.Columns["TongTien"].DisplayFormat.FormatString = "n0"; v.Columns["TongTien"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far; }
            string[] hide = { "IdKho", "IdNhaCungCap", "IdPhieuChi", "CreatedAt", "CreatedBy" };
            foreach (var c in hide) if (v.Columns[c] != null) v.Columns[c].Visible = false;
            v.OptionsView.ColumnAutoWidth = true;
        }

        private void FormatMasterGridXuat()
        {
            var v = gridViewMaster;
            if (v.Columns["Id"] != null) v.Columns["Id"].Caption = "Mã";
            if (v.Columns["LyDo"] != null) v.Columns["LyDo"].Caption = "Lý do xuất";
            if (v.Columns["NgayXuat"] != null) { v.Columns["NgayXuat"].Caption = "Ngày xuất"; v.Columns["NgayXuat"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime; v.Columns["NgayXuat"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"; }
            string[] hide = { "IdKhoXuat", "IdKhoNhan", "IdDonHangLienQuan", "CreatedAt", "CreatedBy" };
            foreach (var c in hide) if (v.Columns[c] != null) v.Columns[c].Visible = false;
            v.OptionsView.ColumnAutoWidth = true;
        }

        private void gridViewMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridViewMaster.FocusedRowHandle >= 0)
            {
                int id = 0;
                if (_isNhapMode)
                {
                    var row = gridViewMaster.GetFocusedRow() as ET_PhieuNhapKho;
                    if (row != null) id = row.Id;

                    var details = BUS_ChiTietNhapKho.Instance.LoadDS()
                        .Where(x => x.IdPhieuNhap == id).ToList();
                    gridDetail.DataSource = details;
                    gbDetail.Text = "Chi tiết Phiếu Nhập #" + id + " (" + details.Count + " dòng)";
                    FormatDetailGridNhap();
                }
                else
                {
                    var row = gridViewMaster.GetFocusedRow() as ET_PhieuXuatKho;
                    if (row != null) id = row.Id;

                    var details = BUS_ChiTietXuatKho.Instance.LoadDS()
                        .Where(x => x.IdPhieuXuat == id).ToList();
                    gridDetail.DataSource = details;
                    gbDetail.Text = "Chi tiết Phiếu Xuất #" + id + " (" + details.Count + " dòng)";
                    FormatDetailGridXuat();
                }
            }
        }

        private void FormatDetailGridNhap()
        {
            var v = gridViewDetail;
            v.PopulateColumns();
            if (v.Columns["Id"] != null) v.Columns["Id"].Visible = false;
            if (v.Columns["IdPhieuNhap"] != null) v.Columns["IdPhieuNhap"].Visible = false;
            if (v.Columns["IdSanPham"] != null) v.Columns["IdSanPham"].Caption = "Mã SP";
            if (v.Columns["SoLuong"] != null) { v.Columns["SoLuong"].Caption = "Số lượng"; v.Columns["SoLuong"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far; }
            if (v.Columns["DonGiaNhap"] != null) { v.Columns["DonGiaNhap"].Caption = "Đơn giá nhập"; v.Columns["DonGiaNhap"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric; v.Columns["DonGiaNhap"].DisplayFormat.FormatString = "n0"; v.Columns["DonGiaNhap"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far; }
            
            if (v.Columns["ThanhTien"] == null)
            {
                var col = v.Columns.AddVisible("ThanhTien", "Thành tiền");
                col.UnboundDataType = typeof(decimal);
                col.UnboundExpression = "[SoLuong] * [DonGiaNhap]";
                col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                col.DisplayFormat.FormatString = "n0";
                col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                col.VisibleIndex = v.Columns.Count;
            }
            v.OptionsView.ColumnAutoWidth = true;
        }

        private void FormatDetailGridXuat()
        {
            var v = gridViewDetail;
            v.PopulateColumns();
            if (v.Columns["Id"] != null) v.Columns["Id"].Visible = false;
            if (v.Columns["IdPhieuXuat"] != null) v.Columns["IdPhieuXuat"].Visible = false;
            if (v.Columns["IdSanPham"] != null) v.Columns["IdSanPham"].Caption = "Mã SP";
            if (v.Columns["SoLuong"] != null) { v.Columns["SoLuong"].Caption = "Số lượng"; v.Columns["SoLuong"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far; }
            if (v.Columns["DonGiaXuat"] != null) { v.Columns["DonGiaXuat"].Caption = "Đơn giá xuất"; v.Columns["DonGiaXuat"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric; v.Columns["DonGiaXuat"].DisplayFormat.FormatString = "n0"; v.Columns["DonGiaXuat"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far; }
            
            if (v.Columns["ThanhTien"] == null)
            {
                var col = v.Columns.AddVisible("ThanhTien", "Thành tiền");
                col.UnboundDataType = typeof(decimal);
                col.UnboundExpression = "[SoLuong] * [DonGiaXuat]";
                col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                col.DisplayFormat.FormatString = "n0";
                col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                col.VisibleIndex = v.Columns.Count;
            }
            v.OptionsView.ColumnAutoWidth = true;
        }

        private void btnToggleMode_Click(object sender, EventArgs e)
        {
            _isNhapMode = !_isNhapMode;
            btnToggleMode.Text = _isNhapMode ? "Sang Chế Độ Xuất" : "Sang Chế Độ Nhập";
            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (var frm = new frmTaoPhieuKho(_isNhapMode))
            {
                if (ThemeManager.ShowAsPopup(frm) == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (gridViewMaster.FocusedRowHandle < 0) return;

            if (TDCMessageBox.Show("Bạn có chắc chắn muốn XÓA phiếu này?\nHành động này KHÔNG thể hoàn tác!",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                bool ok;
                if (_isNhapMode)
                {
                    var row = gridViewMaster.GetFocusedRow() as ET_PhieuNhapKho;
                    if (row == null) return;
                    ok = BUS_PhieuNhapKho.Instance.Xoa(row.Id);
                }
                else
                {
                    var row = gridViewMaster.GetFocusedRow() as ET_PhieuXuatKho;
                    if (row == null) return;
                    ok = BUS_PhieuXuatKho.Instance.Xoa(row.Id);
                }

                if (ok) { TDCMessageBox.Show("Đã xóa phiếu thành công!", "Thông báo"); LoadData(); }
                else TDCMessageBox.Show("Xóa thất bại!", "Lỗi");
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}


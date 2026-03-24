using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using BUS;
using FontAwesome.Sharp;
using ET;
using System.Collections.Generic;

namespace GUI
{
    public partial class frmKhoHang : Form, IBaseForm, AI.IAIFormContext
    {
        public string AIContextName => "frmKhoHang";
        public string AIContextDescription =>
            "Form Kho Hàng: quản lý tồn kho, nhập/xuất kho, kiểm kê, đồng bộ tồn kho. " +
            "Người dùng có thể hỏi về số lượng tồn, sản phẩm sắp hết, giá trị kho, so sánh các kho.";

        public frmKhoHang()
        {
            InitializeComponent();
            InitIcons();
            ApplyStyles();
            ApplyPermissions();

            LoadComboKho();
            InitializeContextMenu();
        }

        private ContextMenuStrip _ctxMenuTonKho;

        private void InitializeContextMenu()
        {
            _ctxMenuTonKho = new ContextMenuStrip();
            var btnXemTheKho = _ctxMenuTonKho.Items.Add("Xem Thẻ Kho");
            // btnXemTheKho.Image = Properties.Resources.icons8_list_16; 
            
            gridTonKho.ContextMenuStrip = _ctxMenuTonKho;
            gridViewTonKho.FocusedRowChanged += GridViewTonKho_FocusedRowChanged;
            gridViewTonKho.RowClick += GridViewTonKho_RowClick;
            gridViewTonKho.DoubleClick += GridViewTonKho_DoubleClick;

            this.splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
        }

        private void GridViewTonKho_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (this.splitContainerControl1.PanelVisibility == DevExpress.XtraEditors.SplitPanelVisibility.Panel1)
            {
                this.splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
                LoadEmbeddedTheKho();
            }
        }

        private void GridViewTonKho_DoubleClick(object sender, EventArgs e)
        {
            var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null) return;

            var pt = view.GridControl.PointToClient(Control.MousePosition);
            var hitInfo = view.CalcHitInfo(pt);

            if (hitInfo.InRowCell && hitInfo.Column.FieldName == "DonViTinh")
            {
                var rowParam = view.GetRowCellValue(hitInfo.RowHandle, "IdSanPham");
                if (rowParam == null || string.IsNullOrEmpty(rowParam.ToString())) return; 

                int idSanPham = Convert.ToInt32(rowParam);
                string currentUnit = view.GetRowCellValue(hitInfo.RowHandle, "DonViTinh")?.ToString();
                decimal currentQty = Convert.ToDecimal(view.GetRowCellValue(hitInfo.RowHandle, "SoLuong") ?? 0);
                decimal currentPrice = Convert.ToDecimal(view.GetRowCellValue(hitInfo.RowHandle, "DonGia") ?? 0);

                var allUoM = DAL.DAL_QuyDoiDonVi.Instance.LoadDS().Where(x => x.IdSanPham == idSanPham).ToList();
                if (allUoM.Count == 0) return; // Không có quy đổi nào

                var allUnits = DAL.DAL_DonViTinh.Instance.LoadDS();
                var sp = DAL.DAL_SanPham.Instance.LayTheoId(idSanPham);
                if (sp == null) return;

                var baseDvt = allUnits.FirstOrDefault(u => u.Id == sp.IdDonViCoBan);
                if (baseDvt == null) return;

                // Đẩy Đơn vị Gốc (Base) và N các Đơn vị Quy đổi vào 1 Vòng lặp List
                var cycleList = new List<Tuple<int, string, decimal>>();
                cycleList.Add(Tuple.Create(baseDvt.Id, baseDvt.Ten, 1m));

                foreach (var u in allUoM)
                {
                    // Giả định đơn vị Lớn quy ra N đơn vị Nhỏ
                    var donViLon = allUnits.FirstOrDefault(d => d.Id == u.IdDonViLon);
                    if (donViLon != null && !cycleList.Any(c => c.Item1 == donViLon.Id))
                    {
                        cycleList.Add(Tuple.Create(donViLon.Id, donViLon.Ten, (decimal)u.TyLeQuyDoi));
                    }
                }

                if (cycleList.Count <= 1) return; // Tránh lỗi chia 0 hoặc không có gì xoay

                // Xác định Index đang hiển thị
                int currentIndex = cycleList.FindIndex(c => c.Item2.Equals(currentUnit, StringComparison.OrdinalIgnoreCase));
                if (currentIndex < 0) currentIndex = 0; // Trễ nếu tên lệch

                // Lấy Tỷ lệ cũ để quy ngược về BaseQty
                decimal currentRate = cycleList[currentIndex].Item3;
                if (currentRate == 0) currentRate = 1;
                decimal baseQty = currentQty * currentRate;  // Tính ngược về số lượng Base
                decimal basePrice = currentPrice / currentRate; // Tính ngược về giá Base

                // Mỗi lần Double-Click, sẽ dịch chuyển index lên 1 bậc xoay vòng
                int nextIndex = (currentIndex + 1) % cycleList.Count;
                var nextUnit = cycleList[nextIndex];
                decimal nextRate = nextUnit.Item3;
                if (nextRate == 0) nextRate = 1;

                // Tạm thời Disable EditMode để thay đổi giá trị UI
                bool wasEditable = view.OptionsBehavior.Editable;
                view.OptionsBehavior.Editable = true;

                view.SetRowCellValue(hitInfo.RowHandle, "DonViTinh", nextUnit.Item2);
                view.SetRowCellValue(hitInfo.RowHandle, "SoLuong", Math.Round(baseQty / nextRate, 2));
                view.SetRowCellValue(hitInfo.RowHandle, "DonGia", basePrice * nextRate);

                // Cập nhật lại cột Thành tiền
                decimal newQty = Convert.ToDecimal(view.GetRowCellValue(hitInfo.RowHandle, "SoLuong") ?? 0);
                decimal newPrice = Convert.ToDecimal(view.GetRowCellValue(hitInfo.RowHandle, "DonGia") ?? 0);
                view.SetRowCellValue(hitInfo.RowHandle, "ThanhTien", newQty * newPrice);

                view.OptionsBehavior.Editable = wasEditable;
            }
        }

        private GUI.Inventory.frmTheKho currentEmbeddedTheKho = null;

        private void GridViewTonKho_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (this.splitContainerControl1.PanelVisibility == DevExpress.XtraEditors.SplitPanelVisibility.Both)
            {
                LoadEmbeddedTheKho();
            }
        }

        private void LoadEmbeddedTheKho()
        {
            if (cboKho.SelectedValue == null) return;
            var view = gridViewTonKho;
            if (view.FocusedRowHandle >= 0)
            {
                int idKho = Convert.ToInt32(cboKho.SelectedValue);
                var rowParam = view.GetRowCellValue(view.FocusedRowHandle, "IdSanPham");
                if (rowParam == null || string.IsNullOrEmpty(rowParam.ToString())) return; 
                
                int idSanPham = Convert.ToInt32(rowParam);
                string tenSanPham = view.GetRowCellValue(view.FocusedRowHandle, "TenSanPham")?.ToString() ?? "";
                string maSanPham = view.GetRowCellValue(view.FocusedRowHandle, "MaSanPham")?.ToString() ?? "";
                string dvt = view.GetRowCellValue(view.FocusedRowHandle, "DonViTinh")?.ToString() ?? "";
                string tenKho = cboKho.Text;

                if (currentEmbeddedTheKho == null)
                {
                    currentEmbeddedTheKho = new GUI.Inventory.frmTheKho(idKho, idSanPham, tenKho, tenSanPham, maSanPham, dvt);
                    currentEmbeddedTheKho.TopLevel = false;
                    currentEmbeddedTheKho.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    currentEmbeddedTheKho.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.splitContainerControl1.Panel2.Controls.Add(currentEmbeddedTheKho);
                    currentEmbeddedTheKho.HideHeadersAndShow();
                }
                else
                {
                    currentEmbeddedTheKho.UpdateData(idKho, idSanPham, tenKho, tenSanPham, maSanPham, dvt);
                }
            }
        }

        private void frmKhoHang_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        public void ApplyPermissions()
        {
            var tk = ET.SessionManager.CurrentUser;
            if (tk == null) return;

            if (!BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "VIEW_INVENTORY"))
            {
                this.Enabled = false;
                return;
            }

            bool canManage = BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "MANAGE_INVENTORY");
            btnKeToanNhaCungCap.Enabled = canManage;
            btnNhapKho.Enabled = canManage;
            btnXuatKho.Enabled = canManage;
            btnKiemKe.Enabled = canManage;
        }

        public void ApplyStyles()
        {
            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleDevExpressGrid(gridTonKho);
        }

        public void InitIcons()
        {
            btnKeToanNhaCungCap.Image = IconHelper.GetBitmap(IconChar.Truck, Color.White, 14);
            btnNhapKho.Image = IconHelper.GetBitmap(IconChar.Download, Color.White, 16);
            btnXuatKho.Image = IconHelper.GetBitmap(IconChar.Upload, Color.White, 16);
            btnKiemKe.Image = IconHelper.GetBitmap(IconChar.ClipboardCheck, Color.White, 16);
        }

        private void LoadComboKho()
        {
            cboKho.DisplayMember = "TenKho";
            cboKho.ValueMember = "Id";
            cboKho.DataSource = BUS_KhoHang.Instance.LoadDS();
            if (cboKho.Items.Count > 0) cboKho.SelectedIndex = 0;
        }

        public void LoadData()
        {
            if (cboKho.SelectedValue == null) return;
            int idKho = Convert.ToInt32(cboKho.SelectedValue);

            // 1. Tải danh sách tồn
            var dsTonKho = BUS_KhoHang.Instance.GetTonKhoChiTiet(idKho);
            gridTonKho.DataSource = dsTonKho;
            gridViewTonKho.PopulateColumns();
            FormatGridTonKho();

            // 2. Tính toán Dashboard Cards
            var metrics = BUS_KhoHang.Instance.GetDashboardMetrics(idKho);

            lblSapHetValue.Text = metrics.SapHet + " Loại";
            lblTongVonValue.Text = metrics.TongVon.ToString("N0") + " đ";
            lblCanhBaoValue.Text = metrics.AmKho + " Loại";
        }

        private void FormatGridTonKho()
        {
            var v = gridViewTonKho;
            
            v.OptionsView.ShowGroupPanel = false;
            
            if (v.Columns["Id"] != null) v.Columns["Id"].Visible = false;
            if (v.Columns["IdSanPham"] != null) v.Columns["IdSanPham"].Visible = false;
            if (v.Columns["MaSanPham"] != null) { v.Columns["MaSanPham"].Caption = "Mã SP"; v.Columns["MaSanPham"].Width = 100; }
            if (v.Columns["TenSanPham"] != null) { v.Columns["TenSanPham"].Caption = "Tên Sản Phẩm"; v.Columns["TenSanPham"].Width = 200; }
            if (v.Columns["LoaiSanPham"] != null) { v.Columns["LoaiSanPham"].Caption = "Loại"; v.Columns["LoaiSanPham"].Width = 100; }
            if (v.Columns["DonViTinh"] != null) { v.Columns["DonViTinh"].Caption = "ĐVT"; v.Columns["DonViTinh"].Width = 80; }
            if (v.Columns["SoLuong"] != null)
            {
                v.Columns["SoLuong"].Caption = "SL Tồn Kho";
                v.Columns["SoLuong"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                v.Columns["SoLuong"].DisplayFormat.FormatString = "n0";
                v.Columns["SoLuong"].Width = 120;
            }
            
            if (v.Columns["DonGia"] != null)
            {
                v.Columns["DonGia"].Caption = "Đơn Giá Vốn (VNĐ)";
                v.Columns["DonGia"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                v.Columns["DonGia"].DisplayFormat.FormatString = "n0";
                v.Columns["DonGia"].Width = 100;
            }

            if (v.Columns["ThanhTien"] != null)
            {
                v.Columns["ThanhTien"].Caption = "Thành Tiền (VNĐ)";
                v.Columns["ThanhTien"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                v.Columns["ThanhTien"].DisplayFormat.FormatString = "n0";
                v.Columns["ThanhTien"].Width = 150;
            }

            if (v.Columns["NguongCanhBao"] != null) v.Columns["NguongCanhBao"].Visible = false;

            v.RowCellStyle -= GridViewTonKho_RowCellStyle;
            v.RowCellStyle += GridViewTonKho_RowCellStyle;
        }

        private void GridViewTonKho_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (e.RowHandle >= 0 && e.Column.FieldName == "SoLuong")
            {
                int soLuong = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "SoLuong") ?? 0);
                int canhBao = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "NguongCanhBao") ?? 5);

                if (soLuong == 0)
                {
                    // Hết hàng: Chữ đỏ in đậm
                    e.Appearance.ForeColor = Color.Crimson;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
                else if (soLuong <= canhBao)
                {
                    // Sắp hết: Chữ cam in đậm
                    e.Appearance.ForeColor = Color.DarkOrange; 
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
            }
        }

        private void btnKeToanNhaCungCap_Click(object sender, EventArgs e)
        {
            frmNhaCungCap f = new frmNhaCungCap();
            ThemeManager.ShowAsPopup(f);
        }

        // --- HỆ THỐNG HOTKEYS & ACTIONS ---
        private void btnNhapKho_Click(object sender, EventArgs e)
        {
            frmTaoPhieuKho f = new frmTaoPhieuKho(true);
            ThemeManager.ShowAsPopup(f);
            LoadData(); // Tải lại lưới tồn kho sau khi nhập
        }

        private void btnXuatKho_Click(object sender, EventArgs e)
        {
            frmTaoPhieuKho f = new frmTaoPhieuKho(false);
            ThemeManager.ShowAsPopup(f);
            LoadData(); // Tải lại lưới sau khi xuất
        }

        private void btnKiemKe_Click(object sender, EventArgs e)
        {
            frmKiemKho f = new frmKiemKho();
            ThemeManager.ShowAsPopup(f);
            LoadData(); // Load lại data sau khi đóng form kiểm kê
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            gridViewTonKho.ApplyFindFilter(txtSearch.Text);
        }

        private void btnDongBo_Click(object sender, EventArgs e)
        {
            // Sync is expensive, only run on explicit user click
            BUS_KhoHang.Instance.DongBoTonKhoTrucTiep();
            LoadData(); 
            TDCMessageBox.Show("Đã đồng bộ lại Tồn Kho hiện tại với Sổ Thẻ Kho (Ledger)!", "Thành công");
        }

        private void frmKhoHang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2 && btnNhapKho.Enabled)
            {
                btnNhapKho.PerformClick();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F3 && btnXuatKho.Enabled)
            {
                btnXuatKho.PerformClick();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F4 && btnKiemKe.Enabled)
            {
                btnKiemKe.PerformClick();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F5)
            {
                btnDongBo.PerformClick();
                e.Handled = true;
            }
        }
    }
}



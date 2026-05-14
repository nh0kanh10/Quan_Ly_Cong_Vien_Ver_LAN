using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using ET.Constants;
using GUI.Infrastructure;
using DevExpress.Utils.Menu;
using System.Collections.Generic;
using BUS.Services.HeThong;
using System.Linq;

namespace GUI.Modules.Kho
{
    public partial class ucTaoPhieu : DevExpress.XtraEditors.XtraUserControl
    {
        private BindingList<TempHangHoa> _lstNguon = new BindingList<TempHangHoa>();
        private BindingList<TempHangHoa> _lstChiTiet = new BindingList<TempHangHoa>();
        private readonly BUS_NhatKy _nhatKy = BUS_NhatKy.Instance;
        private int _currentChungTuId = 0;
        private bool _isViewMode = false;

        public bool IsDirty { get; private set; }

        public ucTaoPhieu()
        {
            InitializeComponent();
            KhoiTao();
        }

        private void KhoiTao()
        {
            gridNguon.DataSource = _lstNguon;
            gridChiTiet.DataSource = _lstChiTiet;

            gridViewChiTiet.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            
            gridViewChiTiet.CustomDrawEmptyForeground += gridViewChiTiet_CustomDrawEmptyForeground;
            
            gridViewChiTiet.PopupMenuShowing += GridViewChiTiet_PopupMenuShowing;

            TaoCotGrid();
            LoadDanhMuc();

            ThucHienDichNgonNgu();

            IsDirty = false;
            _currentChungTuId = 0;
        }

        public void XemChungTu(int idChungTu)
        {
            var ct = BUS.Services.Kho.BUS_ChungTuKho.Instance.GetChiTietChungTu(idChungTu);
            if (ct == null) return;

            _isViewMode = true; 
            _currentChungTuId = idChungTu;

            txtMaPhieu.Text = ct.MaChungTu;
            cboLoaiCT.EditValue = ct.LoaiChungTu;
            dtNgayPhieu.DateTime = ct.NgayChungTu;
            slkKhoNhap.EditValue = ct.ChiTiets?.FirstOrDefault()?.IdKhoNhap;
            slkKhoXuat.EditValue = ct.ChiTiets?.FirstOrDefault()?.IdKhoXuat;
            slkDoiTac.EditValue = ct.IdDoiTac;
            txtGhiChu.Text = ct.GhiChu;

            if (lblTrangThai != null)
            {
                string trangThaiText = LanguageManager.GetString(ct.TrangThai) ?? ct.TrangThai;
                lblTrangThai.Text = trangThaiText;
                lblTrangThai.Visible = true;

                if (ct.TrangThai == AppConstants.TrangThaiChungTuKho.DaDuyet)
                    lblTrangThai.Appearance.ForeColor = System.Drawing.Color.FromArgb(76, 175, 80);
                else if (ct.TrangThai == AppConstants.TrangThaiChungTuKho.DaHuy)
                    lblTrangThai.Appearance.ForeColor = System.Drawing.Color.FromArgb(244, 67, 54);
                else
                    lblTrangThai.Appearance.ForeColor = System.Drawing.Color.FromArgb(255, 193, 7);
            }

            _lstChiTiet.Clear();
            foreach (var item in ct.ChiTiets)
            {
                _lstChiTiet.Add(new TempHangHoa
                {
                    IdSanPham  = item.IdSanPham,
                    MaHang     = item.MaSanPham,
                    TenHang    = item.TenSanPham,
                    DVT        = item.TenDonViTinh,
                    SoLuong    = item.SoLuong,
                    DonGia     = item.DonGia ?? 0,
                    TonMay     = item.SoLuongHeThong ?? 0,
                    DemThucTe  = item.SoLuongThucTe  ?? 0,
                    HeSoQuyDoi = 1m,
                    IdKhoXuat  = item.IdKhoXuat,
                    IdKhoNhap  = item.IdKhoNhap
                });
            }

            bool isLocked = ct.TrangThai == AppConstants.TrangThaiChungTuKho.DaDuyet
                         || ct.TrangThai == AppConstants.TrangThaiChungTuKho.DaHuy;

            txtMaPhieu.ReadOnly = true;
            cboLoaiCT.ReadOnly = true;
            dtNgayPhieu.ReadOnly = true;
            slkKhoNhap.ReadOnly = true;
            slkKhoXuat.ReadOnly = true;
            slkDoiTac.ReadOnly = true;
            txtGhiChu.ReadOnly = isLocked;
            
            gridViewChiTiet.OptionsBehavior.Editable = !isLocked;
            gridViewChiTiet.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            
            btnLuu.Visible = !isLocked;
            btnHuy.Text = LanguageManager.GetString("BTN_DONG") ?? "Đóng";
            
            var colXoa = gridViewChiTiet.Columns["Xoa"];
            if (colXoa != null) colXoa.Visible = !isLocked;
            
            IsDirty = false;
            _isViewMode = false;
        }

        private void gridViewChiTiet_CustomDrawEmptyForeground(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
        {
            string d = LanguageManager.GetString("TXT_BAMTHEMDONG") ?? "Bấm chuột vào vùng trắng này (để thêm sản phẩm...";
            using (var f = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Italic))
            {
                var r = new System.Drawing.Rectangle(e.Bounds.Left + 10, e.Bounds.Top + 10, e.Bounds.Width - 10, e.Bounds.Height - 10);
                e.Graphics.DrawString(d, f, System.Drawing.Brushes.Gray, r);
            }
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            if (IsDirty && !UIHelper.XacNhanHuy()) return;
            _lstChiTiet.Clear();
            IsDirty = false;
            _currentChungTuId = 0;
        }

        private void BtnInPhieu_Click(object sender, EventArgs e)
        {
            if (_currentChungTuId <= 0)
            {
                UIHelper.ThongBao(LanguageManager.GetString("MSG_KHO_CHUA_LUU_KHONG_IN") ?? "Vui lòng lưu phiếu trước khi in (hoặc mở lại phiếu từ lịch sử).");
                return;
            }
            try
            {
                var rpt = new rptPhieuKho();
                rpt.NapDuLieu(_currentChungTuId);
                using (var tool = new DevExpress.XtraReports.UI.ReportPrintTool(rpt))
                {
                    tool.ShowRibbonPreviewDialog();
                }
            }
            catch (Exception ex)
            {
                UIHelper.Loi(ex.Message);
            }
        }

        public void ThucHienDichNgonNgu()
        {
            lblLoaiCT.Text = LanguageManager.GetString("LBL_LOAIPHIEU");
            lblMaPhieu.Text = LanguageManager.GetString("LBL_MAPHIEU");
            lblNgay.Text = LanguageManager.GetString("LBL_NGAYLAP");
            lblKhoXuat.Text = LanguageManager.GetString("LBL_KHOXUAT");
            lblKhoNhap.Text = LanguageManager.GetString("LBL_KHONHAP");
            lblDoiTac.Text = LanguageManager.GetString("LBL_DOITAC");
            lblGhiChu.Text = LanguageManager.GetString("LBL_GHICHU");
            
            btnLuu.Text = LanguageManager.GetString("BTN_LUU");
            btnHuy.Text = LanguageManager.GetString("BTN_HUY");
            if (btnInPhieu != null) btnInPhieu.Text = LanguageManager.GetString("BTN_INPHIEU") ?? "In phiếu (F4)";
            
            grpNguon.Text = LanguageManager.GetString("GRP_KHO_NGUON");
            grpChiTiet.Text = LanguageManager.GetString("GRP_KHO_CHITIET");

            if (gridViewChiTiet.Columns["MaHang"] != null) gridViewChiTiet.Columns["MaHang"].Caption = LanguageManager.GetString("COL_MAHANG") ?? "Mã SP";
            if (gridViewChiTiet.Columns["TenHang"] != null) gridViewChiTiet.Columns["TenHang"].Caption = LanguageManager.GetString("COL_TENHANG") ?? "Tên Sản Phẩm";
            if (gridViewChiTiet.Columns["DVT"] != null) gridViewChiTiet.Columns["DVT"].Caption = LanguageManager.GetString("COL_DVT") ?? "ĐVT";
            if (gridViewChiTiet.Columns["SoLuong"] != null) gridViewChiTiet.Columns["SoLuong"].Caption = LanguageManager.GetString("COL_SOLUONG") ?? "Số Lượng";
            if (gridViewChiTiet.Columns["DonGia"] != null) gridViewChiTiet.Columns["DonGia"].Caption = LanguageManager.GetString("COL_DONGIA") ?? "Đơn Giá";
            if (gridViewChiTiet.Columns["ThanhTien"] != null) gridViewChiTiet.Columns["ThanhTien"].Caption = LanguageManager.GetString("COL_THANHTIEN") ?? "Thành Tiền";
            if (gridViewChiTiet.Columns["TonMay"] != null) gridViewChiTiet.Columns["TonMay"].Caption = LanguageManager.GetString("COL_TONMAY") ?? "Tồn Máy";
            if (gridViewChiTiet.Columns["DemThucTe"] != null) gridViewChiTiet.Columns["DemThucTe"].Caption = LanguageManager.GetString("COL_DEMTHUCTE") ?? "Đếm Thực Tế";
            if (gridViewChiTiet.Columns["Lech"] != null) gridViewChiTiet.Columns["Lech"].Caption = LanguageManager.GetString("COL_LECH") ?? "Lệch";

            var dsKho = BUS.Services.Kho.BUS_Kho.Instance.GetKhoHoatDong(GUI.Infrastructure.SessionManager.CurrentLanguage);
            slkKhoXuat.Properties.DataSource = dsKho;
            slkKhoXuat.Properties.NullText = LanguageManager.GetString("COL_KHOXUAT") ?? "-- Chọn Kho Xuất --";
            if (slkKhoXuat.Properties.View.Columns["TenKho"] != null) slkKhoXuat.Properties.View.Columns["TenKho"].Caption = LanguageManager.GetString("COL_TEN") ?? "Tên";

            slkKhoNhap.Properties.DataSource = dsKho;
            slkKhoNhap.Properties.NullText = LanguageManager.GetString("COL_KHONHAP") ?? "-- Chọn Kho Nhập --";
            if (slkKhoNhap.Properties.View.Columns["TenKho"] != null) slkKhoNhap.Properties.View.Columns["TenKho"].Caption = LanguageManager.GetString("COL_TEN") ?? "Tên";

            slkDoiTac.Properties.NullText = LanguageManager.GetString("LBL_DOITAC") ?? "-- Chọn Đối Tác --";
            if (slkDoiTac.Properties.View.Columns["HoTen"] != null) slkDoiTac.Properties.View.Columns["HoTen"].Caption = LanguageManager.GetString("COL_TEN") ?? "Tên";

            var repSanPham = gridViewChiTiet.Columns["MaHang"]?.ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit;
            if (repSanPham != null)
            {
                var allProducts = BUS.Services.DanhMuc.BUS_SanPham.Instance.LayDanhSach(null, GUI.Infrastructure.SessionManager.CurrentLanguage);
                repSanPham.DataSource = allProducts.Where(p => p.LaVatTu).ToList();
                repSanPham.NullText = LanguageManager.GetString("COL_CHONSP") ?? "-- Chọn SP --";
                if (repSanPham.View.Columns["MaSanPham"] != null) repSanPham.View.Columns["MaSanPham"].Caption = LanguageManager.GetString("COL_MA");
                if (repSanPham.View.Columns["TenSanPham"] != null) repSanPham.View.Columns["TenSanPham"].Caption = LanguageManager.GetString("COL_TEN");
            }

            cboLoaiCT.Properties.Items.Clear();
            var dsLoaiCT = BUS.Services.HeThong.BUS_TuDien.Instance.LayDanhSachNhom("LOAI_CHUNG_TU_KHO");
            foreach (var item in dsLoaiCT)
            {
                if (!item.ConHoatDong) continue;
                string displayName = LanguageManager.GetString(item.Ma) ?? item.NhanHienThi;
                cboLoaiCT.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(displayName, item.Ma, -1));
            }
            if (cboLoaiCT.Properties.Items.Count > 0 && cboLoaiCT.EditValue == null)
                cboLoaiCT.SelectedIndex = 0;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F2)
            {
                btnLuu.PerformClick();
                return true;
            }
            if (keyData == Keys.Escape)
            {
                if (IsDirty && !UIHelper.XacNhanHuy()) return true;
                _lstChiTiet.Clear();
                IsDirty = false;
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void MarkDirty() { IsDirty = true; }

        private void control_EditValueChanged(object sender, EventArgs e) { MarkDirty(); }

        private void cboLoaiCT_EditValueChanged(object sender, EventArgs e)
        {
            if (splitContainerControl == null || gridViewChiTiet == null) return;
            string loai = cboLoaiCT.EditValue?.ToString() ?? "";
            bool isAdmin = SessionManager.TenVaiTro == "Admin" || SessionManager.CoQuyen("QUAN_LY_KHO");

            if (!string.IsNullOrEmpty(loai) && !_isViewMode)
                txtMaPhieu.Text = BUS.Services.Kho.BUS_ChungTuKho.Instance.SinhMaChungTu(loai);

            foreach (GridColumn c in gridViewChiTiet.Columns) c.Visible = false;
            var colMa = gridViewChiTiet.Columns["MaHang"];
            var colTen = gridViewChiTiet.Columns["TenHang"];
            var colDVT = gridViewChiTiet.Columns["DVT"];
            if (colMa != null) colMa.Visible = true;
            if (colTen != null) colTen.Visible = true;
            if (colDVT != null) colDVT.Visible = true;

            lblKhoXuat.Visible = true;  slkKhoXuat.Visible = true;
            lblKhoNhap.Visible = false; slkKhoNhap.Visible = false;
            lblDoiTac.Visible = false;  slkDoiTac.Visible = false;
            gridViewChiTiet.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            splitContainerControl.PanelVisibility = SplitPanelVisibility.Panel2;

            //  Mọi phiếu phải có KhoXuat + KhoNhap
            // Kho ảo (NCC, Khách, Hủy...) được auto-set khi lưu theo loại phiếu
            // User chỉ chọn kho thật — kho ảo ẩn đi
            switch (loai)
            {
                case AppConstants.LoaiChungTuKho.NHAP_MUA:
                    // NCC (ảo) -> Kho thật. User chọn Kho Nhập + Đối tác NCC
                    lblKhoXuat.Visible = false; slkKhoXuat.Visible = false;
                    lblKhoNhap.Visible = true;  slkKhoNhap.Visible = true;
                    lblDoiTac.Visible = true;   slkDoiTac.Visible = true;
                    lblKhoNhap.Text = LanguageManager.GetString("LBL_KHONHAP") ?? "Kho Nhập:";
                    lblDoiTac.Text = LanguageManager.GetString("LBL_NCC") ?? "Nhà Cung Cấp:";
                    ShowPriceColumns();
                    break;

                case AppConstants.LoaiChungTuKho.XUAT_BAN:
                    // Kho thật -> Khách (ảo). User chọn Kho Xuất
                    lblKhoXuat.Text = LanguageManager.GetString("LBL_KHOXUAT") ?? "Kho Xuất:";
                    ShowPriceColumns();
                    break;

                case AppConstants.LoaiChungTuKho.TRA_NCC:
                    // Kho thật -> NCC (ảo). User chọn Kho Xuất + Đối tác NCC
                    lblKhoXuat.Text = LanguageManager.GetString("LBL_KHOXUAT") ?? "Kho Xuất:";
                    lblDoiTac.Visible = true; slkDoiTac.Visible = true;
                    lblDoiTac.Text = LanguageManager.GetString("LBL_NCC") ?? "Nhà Cung Cấp:";
                    ShowPriceColumns();
                    break;

                case AppConstants.LoaiChungTuKho.KHACH_TRA:
                    // Khách (ảo) -> Kho thật. User chọn Kho Nhập + Đối tác Khách
                    lblKhoXuat.Visible = false; slkKhoXuat.Visible = false;
                    lblKhoNhap.Visible = true;  slkKhoNhap.Visible = true;
                    lblDoiTac.Visible = true;   slkDoiTac.Visible = true;
                    lblKhoNhap.Text = LanguageManager.GetString("LBL_KHONHAP") ?? "Kho Nhập:";
                    lblDoiTac.Text = LanguageManager.GetString("LBL_DOITAC") ?? "Đối Tác:";
                    ShowPriceColumns();
                    break;

                case AppConstants.LoaiChungTuKho.CHUYEN_KHO:
                    // Kho thật A → Kho thật B. User chọn header KhoXuat + KhoNhap cho toàn phiếu.
                    // Grid chọn hàng từ panel nguồn (tồn kho hiện tại của KhoXuat)
                    splitContainerControl.PanelVisibility = SplitPanelVisibility.Both;
                    lblKhoXuat.Visible = true; slkKhoXuat.Visible = true;
                    lblKhoXuat.Text = LanguageManager.GetString("LBL_KHOXUAT") ?? "Kho Xuất:";
                    lblKhoNhap.Visible = true; slkKhoNhap.Visible = true;
                    lblKhoNhap.Text = LanguageManager.GetString("LBL_KHONHAP") ?? "Kho Nhập:";
                    var colSLCK = gridViewChiTiet.Columns["SoLuong"];
                    if (colSLCK != null) colSLCK.Visible = true;
                    gridViewChiTiet.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                    break;

                case AppConstants.LoaiChungTuKho.HUY_HONG:
                    // Kho thật -> KHO_HUY (ảo). User chọn Kho Xuất
                    lblKhoXuat.Text = LanguageManager.GetString("LBL_KHOXUAT") ?? "Kho Xuất:";
                    var colSLHH = gridViewChiTiet.Columns["SoLuong"];
                    if (colSLHH != null) colSLHH.Visible = true;
                    break;

                case AppConstants.LoaiChungTuKho.XUAT_BAOTRI:
                case AppConstants.LoaiChungTuKho.XUAT_SANXUAT:
                    lblKhoXuat.Text = LanguageManager.GetString("LBL_KHOXUAT") ?? "Kho Xuất:";
                    var colSLBT = gridViewChiTiet.Columns["SoLuong"];
                    if (colSLBT != null) colSLBT.Visible = true;
                    break;

                case AppConstants.LoaiChungTuKho.KIEM_KE:
                    lblKhoXuat.Text = LanguageManager.GetString("LBL_KHO") ?? "Kho Kiểm:";
                    if (isAdmin) {
                        var colTon = gridViewChiTiet.Columns["TonMay"];
                        var colLech = gridViewChiTiet.Columns["Lech"];
                        if (colTon != null) colTon.Visible = true;
                        if (colLech != null) colLech.Visible = true;
                    }
                    var colDem = gridViewChiTiet.Columns["DemThucTe"];
                    if (colDem != null) colDem.Visible = true;
                    break;

                default:
                    lblKhoXuat.Text = LanguageManager.GetString("LBL_KHO") ?? "Kho:";
                    var colSLD = gridViewChiTiet.Columns["SoLuong"];
                    if (colSLD != null) colSLD.Visible = true;
                    break;
            }
            ReorderColumns();
            MarkDirty();
        }

        private void ShowPriceColumns()
        {
            var colSL = gridViewChiTiet.Columns["SoLuong"];
            var colGia = gridViewChiTiet.Columns["DonGia"];
            var colTien = gridViewChiTiet.Columns["ThanhTien"];
            if (colSL != null) colSL.Visible = true;
            if (colGia != null) colGia.Visible = true;
            if (colTien != null) colTien.Visible = true;
        }

        private void ReorderColumns()
        {
            int idx = 0;
            if (gridViewChiTiet.Columns["MaHang"] != null && gridViewChiTiet.Columns["MaHang"].Visible)
                gridViewChiTiet.Columns["MaHang"].VisibleIndex = idx++;
            if (gridViewChiTiet.Columns["TenHang"] != null && gridViewChiTiet.Columns["TenHang"].Visible)
                gridViewChiTiet.Columns["TenHang"].VisibleIndex = idx++;
            if (gridViewChiTiet.Columns["DVT"] != null && gridViewChiTiet.Columns["DVT"].Visible)
                gridViewChiTiet.Columns["DVT"].VisibleIndex = idx++;
            if (gridViewChiTiet.Columns["TonMay"] != null && gridViewChiTiet.Columns["TonMay"].Visible)
                gridViewChiTiet.Columns["TonMay"].VisibleIndex = idx++;
            if (gridViewChiTiet.Columns["DemThucTe"] != null && gridViewChiTiet.Columns["DemThucTe"].Visible)
                gridViewChiTiet.Columns["DemThucTe"].VisibleIndex = idx++;
            if (gridViewChiTiet.Columns["Lech"] != null && gridViewChiTiet.Columns["Lech"].Visible)
                gridViewChiTiet.Columns["Lech"].VisibleIndex = idx++;
            if (gridViewChiTiet.Columns["SoLuong"] != null && gridViewChiTiet.Columns["SoLuong"].Visible)
                gridViewChiTiet.Columns["SoLuong"].VisibleIndex = idx++;
            if (gridViewChiTiet.Columns["DonGia"] != null && gridViewChiTiet.Columns["DonGia"].Visible)
                gridViewChiTiet.Columns["DonGia"].VisibleIndex = idx++;
            if (gridViewChiTiet.Columns["ThanhTien"] != null && gridViewChiTiet.Columns["ThanhTien"].Visible)
                gridViewChiTiet.Columns["ThanhTien"].VisibleIndex = idx++;
        }

        private void gridViewChiTiet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            MarkDirty();
        }

        private void GridViewChiTiet_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                DXMenuItem itemDel = new DXMenuItem(LanguageManager.GetString("MNU_XOADONG") ?? "Xóa dòng", (s, args) => {
                    var row = gridViewChiTiet.GetFocusedRow() as TempHangHoa;
                    if (row != null) {
                        _lstChiTiet.Remove(row);
                        if (cboLoaiCT.EditValue?.ToString() == AppConstants.LoaiChungTuKho.CHUYEN_KHO) _lstNguon.Add(row);
                        MarkDirty();
                    }
                });
                e.Menu.Items.Add(itemDel);
            }
        }

        private void gridNguon_DoubleClick(object sender, EventArgs e)
        {
            if (cboLoaiCT.EditValue?.ToString() != AppConstants.LoaiChungTuKho.CHUYEN_KHO) return;
            var row = gridViewNguon.GetFocusedRow() as TempHangHoa;
            if (row != null)
            {
                _lstNguon.Remove(row);
                _lstChiTiet.Add(row);
                MarkDirty();
            }
        }

        private void gridChiTiet_DoubleClick(object sender, EventArgs e)
        {
            if (cboLoaiCT.EditValue?.ToString() != AppConstants.LoaiChungTuKho.CHUYEN_KHO) return;
            var row = gridViewChiTiet.GetFocusedRow() as TempHangHoa;
            if (row != null)
            {
                _lstChiTiet.Remove(row);
                _lstNguon.Add(row);
                MarkDirty();
                
            }
        }

        private void LoadDanhMuc()
        {
            cboLoaiCT.Properties.Items.Clear();
            var dsLoaiPhieu = BUS.Services.HeThong.BUS_TuDien.Instance.LayDanhSachNhom("LOAI_CHUNG_TU_KHO");
            foreach(var item in dsLoaiPhieu) {
                cboLoaiCT.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LanguageManager.GetString(item.Ma) ?? item.NhanHienThi, item.Ma));
            }

            var dsKho = BUS.Services.Kho.BUS_Kho.Instance.GetKhoHoatDong(GUI.Infrastructure.SessionManager.CurrentLanguage);
            slkKhoXuat.Properties.DataSource = dsKho;
            slkKhoXuat.Properties.DisplayMember = "TenKho";
            slkKhoXuat.Properties.ValueMember = "Id";
            slkKhoXuat.Properties.NullText = LanguageManager.GetString("NULL_KHOXUAT") ?? "-- Chọn --";
            slkKhoXuat.Properties.PopulateViewColumns();
            if(slkKhoXuat.Properties.View.Columns.Count > 0) {
                foreach (GridColumn col in slkKhoXuat.Properties.View.Columns) col.Visible = false;
                if(slkKhoXuat.Properties.View.Columns["TenKho"] != null) slkKhoXuat.Properties.View.Columns["TenKho"].Visible = true;
            }

            slkKhoNhap.Properties.DataSource = dsKho;
            slkKhoNhap.Properties.DisplayMember = "TenKho";
            slkKhoNhap.Properties.ValueMember = "Id";
            slkKhoNhap.Properties.NullText = LanguageManager.GetString("NULL_KHONHAP") ?? "-- Chọn --";
            slkKhoNhap.Properties.PopulateViewColumns();
            if(slkKhoNhap.Properties.View.Columns.Count > 0) {
                foreach (GridColumn col in slkKhoNhap.Properties.View.Columns) col.Visible = false;
                if(slkKhoNhap.Properties.View.Columns["TenKho"] != null) slkKhoNhap.Properties.View.Columns["TenKho"].Visible = true;
            }

            var dsDoiTac = BUS.Services.DoiTac.BUS_DoiTac.Instance.GetAllDoiTac();
            slkDoiTac.Properties.DataSource = dsDoiTac;
            slkDoiTac.Properties.DisplayMember = "HoTen";
            slkDoiTac.Properties.ValueMember = "Id";
            slkDoiTac.Properties.NullText = LanguageManager.GetString("NULL_DOITAC") ?? "-- Chọn --";
            slkDoiTac.Properties.PopulateViewColumns();
            if(slkDoiTac.Properties.View.Columns.Count > 0) {
                foreach (GridColumn col in slkDoiTac.Properties.View.Columns) col.Visible = false;
                if(slkDoiTac.Properties.View.Columns["HoTen"] != null) slkDoiTac.Properties.View.Columns["HoTen"].Visible = true;
            }

            cboLoaiCT.SelectedIndex = 0;
        }

        private void SlkKhoXuat_EditValueChanged(object sender, EventArgs e)
        {
            if (cboLoaiCT.EditValue?.ToString() == AppConstants.LoaiChungTuKho.KIEM_KE && slkKhoXuat.EditValue != null)
            {
                if (int.TryParse(slkKhoXuat.EditValue.ToString(), out int idKho) && idKho > 0)
                {
                    if (_lstChiTiet.Count > 0)
                    {
                        var xacNhan = DevExpress.XtraEditors.XtraMessageBox.Show(
                            LanguageManager.GetString("MSG_XAC_NHAN_DOI_KHO_KIEM_KE") ?? "Đổi kho sẽ xóa trắng danh sách sản phẩm hiện tại để tải lại danh mục tồn kho. Bạn có chắc chắn?",
                            LanguageManager.GetString("TITLE_XAC_NHAN") ?? "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (xacNhan != DialogResult.Yes) return;
                    }

                    _lstChiTiet.Clear();
                    var tonKhos = BUS.Services.Kho.BUS_SoCai.Instance.GetBaoCaoTonKho(idKho);
                    foreach (var ton in tonKhos)
                    {
                        _lstChiTiet.Add(new TempHangHoa
                        {
                            IdSanPham = ton.IdSanPham,
                            MaHang = ton.MaSanPham,
                            TenHang = ton.TenSanPham,
                            DVT = ton.DVT,
                            TonMay = ton.TonHienTai, 
                            DemThucTe = 0
                        });
                    }
                    gridViewChiTiet.RefreshData();
                }
            }
            MarkDirty();
        }

        private void TaoCotGrid()
        {
            gridViewNguon.Columns.Clear();
            gridViewChiTiet.Columns.Clear();

            var repSanPham = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            var allProducts = BUS.Services.DanhMuc.BUS_SanPham.Instance.LayDanhSach(null, GUI.Infrastructure.SessionManager.CurrentLanguage);
            repSanPham.DataSource = allProducts.Where(p => p.LaVatTu).ToList();
            repSanPham.DisplayMember = "MaSanPham"; 
            repSanPham.ValueMember = "MaSanPham";
            repSanPham.NullText = "-- Chọn SP --";
            repSanPham.View.Columns.Add(new GridColumn { FieldName = "MaSanPham", Caption = "Mã SP", Visible = true });
            repSanPham.View.Columns.Add(new GridColumn { FieldName = "TenSanPham", Caption = "Tên SP", Visible = true });
            repSanPham.EditValueChanged += repSanPham_EditValueChanged;

            var repSoLuong = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            repSoLuong.Mask.EditMask = "N2";
            repSoLuong.Mask.UseMaskAsDisplayFormat = true;
            repSoLuong.MinValue = 0;
          
            var repGia = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            repGia.Mask.EditMask = "N0";
            repGia.Mask.UseMaskAsDisplayFormat = true;
            repGia.MinValue = 0;

            var dsKhoGrid = BUS.Services.Kho.BUS_Kho.Instance.GetKhoHoatDong(GUI.Infrastructure.SessionManager.CurrentLanguage);
            var repKho = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            repKho.DataSource     = dsKhoGrid;
            repKho.DisplayMember  = "TenKho";
            repKho.ValueMember    = "Id";
            repKho.NullText       = "-- Chọn Kho --";
            repKho.View.Columns.Add(new GridColumn { FieldName = "TenKho", Caption = "Tên Kho", Visible = true, Width = 150 });

            var colKhoXuat = new GridColumn
            {
                Name = "KhoXuat", FieldName = "IdKhoXuat",
                Caption = LanguageManager.GetString("COL_KHOXUAT") ?? "Kho Xuất",
                Visible = false, Width = 130, ColumnEdit = repKho
            };
            var colKhoNhap = new GridColumn
            {
                Name = "KhoNhap", FieldName = "IdKhoNhap",
                Caption = LanguageManager.GetString("COL_KHONHAP") ?? "Kho Nhập",
                Visible = false, Width = 130, ColumnEdit = repKho
            };

            var colMa   = new GridColumn { Name = "MaHang",   FieldName = "MaHang",   Caption = LanguageManager.GetString("COL_MAHANG")   ?? "Mã SP",        Visible = true, ColumnEdit = repSanPham };
            var colTen  = new GridColumn { Name = "TenHang",  FieldName = "TenHang",  Caption = LanguageManager.GetString("COL_TENHANG")  ?? "Tên Sản Phẩm", Visible = true, OptionsColumn = { AllowEdit = false } };
            var colDVT  = new GridColumn { Name = "DVT",      FieldName = "DVT",      Caption = LanguageManager.GetString("COL_DVT")      ?? "ĐVT",          Visible = true };
            var colSL   = new GridColumn { Name = "SoLuong",  FieldName = "SoLuong",  Caption = LanguageManager.GetString("COL_SOLUONG")  ?? "Số Lượng",     Visible = true, ColumnEdit = repSoLuong };
            var colGia  = new GridColumn { Name = "DonGia",   FieldName = "DonGia",   Caption = LanguageManager.GetString("COL_DONGIA")   ?? "Đơn Giá",      Visible = true, ColumnEdit = repGia };
            var colTien = new GridColumn { Name = "ThanhTien",FieldName = "ThanhTien",Caption = LanguageManager.GetString("COL_THANHTIEN")??"Thành Tiền",    Visible = true, OptionsColumn = { AllowEdit = false }, ColumnEdit = repGia };
            var colTon  = new GridColumn { Name = "TonMay",   FieldName = "TonMay",   Caption = LanguageManager.GetString("COL_TONMAY")   ?? "Tồn Máy",      Visible = false, OptionsColumn = { AllowEdit = false }, ColumnEdit = repSoLuong };
            var colDem  = new GridColumn { Name = "DemThucTe",FieldName = "DemThucTe",Caption = LanguageManager.GetString("COL_DEMTHUCTE")??"Đếm Thực Tế",  Visible = false, ColumnEdit = repSoLuong };
            var colLech = new GridColumn { Name = "Lech",     FieldName = "Lech",     Caption = LanguageManager.GetString("COL_LECH")     ?? "Lệch",         Visible = false, OptionsColumn = { AllowEdit = false }, ColumnEdit = repSoLuong };

            var repXoa = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            repXoa.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            repXoa.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete;
            repXoa.ButtonClick += (s, ev) => { gridViewChiTiet.DeleteRow(gridViewChiTiet.FocusedRowHandle); };
            var colXoa = new GridColumn { Name = "Xoa", Caption = LanguageManager.GetString("BTN_XOA") ?? "Xóa", Visible = true, ColumnEdit = repXoa, Width = 40, Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right };

            gridViewChiTiet.Columns.AddRange(new GridColumn[] { colMa, colTen, colDVT, colKhoXuat, colKhoNhap, colSL, colGia, colTien, colTon, colDem, colLech, colXoa });

            int i = 0;
            colMa.VisibleIndex       = i++;
            colTen.VisibleIndex      = i++;
            colDVT.VisibleIndex      = i++;
            colKhoXuat.VisibleIndex  = i++;
            colKhoNhap.VisibleIndex  = i++;
            colSL.VisibleIndex       = i++;
            colGia.VisibleIndex      = i++;
            colTien.VisibleIndex     = i++;
            colTon.VisibleIndex      = i++;
            colDem.VisibleIndex      = i++;
            colLech.VisibleIndex     = i++;
            colXoa.VisibleIndex      = i++;
            
            gridViewChiTiet.CustomRowCellEditForEditing -= GridViewChiTiet_CustomRowCellEditForEditing;
            gridViewChiTiet.CustomRowCellEditForEditing += GridViewChiTiet_CustomRowCellEditForEditing;
            gridViewChiTiet.CellValueChanged -= GridViewChiTiet_CellValueChanged;
            gridViewChiTiet.CellValueChanged += GridViewChiTiet_CellValueChanged;
        }

        private void GridViewChiTiet_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "DVT")
            {
                int.TryParse(gridViewChiTiet.GetRowCellValue(e.RowHandle, "IdSanPham")?.ToString(), out int idSP);
                if (idSP > 0)
                {
                    var dsDonVi = BUS.Services.BanHang.BUS_POS.Instance.LayDonViBanTheoSanPham(idSP, 0, SessionManager.CurrentLanguage) ?? new List<ET.Models.BanHang.ET_DonViBanPOS>();
                    var repDonVi = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
                    repDonVi.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
                    foreach (var dv in dsDonVi)
                    {
                        repDonVi.Items.Add(dv.TenDonVi);
                    }
                    e.RepositoryItem = repDonVi;
                }
            }
        }

        private void GridViewChiTiet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "DVT")
            {
                var item = gridViewChiTiet.GetRow(e.RowHandle) as TempHangHoa;
                string tenDonVi = e.Value?.ToString() ?? "";
                if (item != null && item.IdSanPham > 0 && !string.IsNullOrEmpty(tenDonVi))
                {
                    var dsDonVi = BUS.Services.BanHang.BUS_POS.Instance.LayDonViBanTheoSanPham(item.IdSanPham, 0, SessionManager.CurrentLanguage);
                    var dv = dsDonVi?.FirstOrDefault(x => x.TenDonVi == tenDonVi);
                    if (dv != null)
                    {
                        item.HeSoQuyDoi = dv.TyLeQuyDoi;
                    }
                }
            }
        }

        private void repSanPham_EditValueChanged(object sender, EventArgs e)
        {
            var lk = sender as DevExpress.XtraEditors.SearchLookUpEdit;
            if (lk != null && lk.EditValue != null)
            {
                var sp = lk.Properties.View.GetFocusedRow() as ET.Models.DanhMuc.ET_SanPham;
                if (sp != null)
                {
                    int row = gridViewChiTiet.FocusedRowHandle;
                    var item = gridViewChiTiet.GetRow(row) as TempHangHoa;
                    if (item != null)
                    {
                        item.IdSanPham = sp.Id;
                        item.HeSoQuyDoi = 1m;
                        item.MaHang = sp.MaSanPham;
                        item.TenHang = sp.TenSanPham;
                        item.DVT = sp.TenDonViTinh ?? "";
                        gridViewChiTiet.RefreshRow(row);
                    }
                }
            }
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (cboLoaiCT.EditValue == null || string.IsNullOrEmpty(cboLoaiCT.EditValue.ToString())) return;
            string loai = cboLoaiCT.EditValue.ToString();

            // Validate: user phải chọn kho thật (kho ảo auto-set)
            if (loai == AppConstants.LoaiChungTuKho.NHAP_MUA || loai == AppConstants.LoaiChungTuKho.KHACH_TRA)
            {
                // Phiếu nhập: user chọn Kho Nhập (kho thật), Kho Xuất auto = kho ảo
                if (slkKhoNhap.EditValue == null) {
                    XtraMessageBox.Show(LanguageManager.GetString(AppConstants.ErrorMessages.ERR_CHONKHO) ?? "Vui lòng chọn Kho Nhập!", LanguageManager.GetString("TITLE_LOI") ?? "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                }
                
                if (slkDoiTac.Visible && slkDoiTac.EditValue == null) {
                    XtraMessageBox.Show(LanguageManager.GetString("ERR_CHONDOITAC") ?? "Vui lòng chọn đối tác!", LanguageManager.GetString("TITLE_LOI") ?? "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                }
            }
            else if (loai == AppConstants.LoaiChungTuKho.TRA_NCC)
            {
                if (slkKhoXuat.EditValue == null) {
                    XtraMessageBox.Show(LanguageManager.GetString(AppConstants.ErrorMessages.ERR_CHONKHO) ?? "Vui lòng chọn Kho Xuất!", LanguageManager.GetString("TITLE_LOI") ?? "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                }
                if (slkDoiTac.Visible && slkDoiTac.EditValue == null) {
                    XtraMessageBox.Show(LanguageManager.GetString("ERR_CHONDOITAC") ?? "Vui lòng chọn đối tác!", LanguageManager.GetString("TITLE_LOI") ?? "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                }
            }
            else if (loai == AppConstants.LoaiChungTuKho.CHUYEN_KHO)
            {
                if (slkKhoXuat.EditValue == null || slkKhoNhap.EditValue == null) {
                    XtraMessageBox.Show(LanguageManager.GetString(AppConstants.ErrorMessages.ERR_CHONKHO) ?? "Vui lòng chọn cả Kho Xuất và Kho Nhập!", LanguageManager.GetString("TITLE_LOI") ?? "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                }
                // so sánh bằng int để tránh nhầm lẫn khi EditValue là kiểu object
                if ((int)slkKhoXuat.EditValue == (int)slkKhoNhap.EditValue)
                {
                    XtraMessageBox.Show(LanguageManager.GetString("ERR_KHO_TRUNG") ?? "Kho xuất và kho nhập không được trùng nhau!", LanguageManager.GetString("TITLE_LOI") ?? "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                }
            }
            else
            {
                // Phiếu xuất / hủy / kiểm kê: user chọn Kho Xuất (kho thật)
                if (slkKhoXuat.EditValue == null) {
                    XtraMessageBox.Show(LanguageManager.GetString(AppConstants.ErrorMessages.ERR_CHONKHO) ?? "Vui lòng chọn Kho thao tác!", LanguageManager.GetString("TITLE_LOI") ?? "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                }
            }

            gridViewChiTiet.PostEditor();
            gridViewChiTiet.UpdateCurrentRow();

            if (_lstChiTiet.Count == 0) {
                XtraMessageBox.Show(LanguageManager.GetString(AppConstants.ErrorMessages.ERR_CHITIETRONG) ?? "Chưa có chi tiết hàng hóa!", LanguageManager.GetString("TITLE_LOI") ?? "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }

            DateTime ngayPhieu = dtNgayPhieu.DateTime;
            if (dtNgayPhieu.EditValue == null || ngayPhieu == DateTime.MinValue || ngayPhieu.Year < 1900)
            {
                XtraMessageBox.Show(LanguageManager.GetString(AppConstants.ErrorMessages.ERR_CHONNGAY) ?? "Vui lòng chọn ngày lập phiếu hợp lệ!", LanguageManager.GetString("TITLE_LOI") ?? "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            // cảnh báo nếu ngày phiếu vượt quá ngày mai (có thể nhập nhầm năm tương lai)
            if (ngayPhieu.Date > DateTime.Today.AddDays(1))
            {
                var kq = XtraMessageBox.Show(
                    LanguageManager.GetString("WARN_NGAY_TUONG_LAI") ?? "Ngày phiếu đang ở tương lai. Bạn có chắc chắn?",
                    LanguageManager.GetString("TITLE_CANHBAO") ?? "Cảnh báo",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (kq != DialogResult.Yes) return;
            }

            // Kho ảo được BUS tự tìm theo MaKho (KHO_NCC, KHO_KHACH, KHO_HUY...)
            int? idKhoXuat = slkKhoXuat.Visible && slkKhoXuat.EditValue != null ? (int)slkKhoXuat.EditValue : (int?)null;
            int? idKhoNhap = slkKhoNhap.Visible && slkKhoNhap.EditValue != null ? (int)slkKhoNhap.EditValue : (int?)null;

            var ct = new ET.Models.Kho.ET_ChungTuKho {
                MaChungTu = txtMaPhieu.Text,
                LoaiChungTu = loai,
                IdDoiTac = slkDoiTac.Visible && slkDoiTac.EditValue != null ? (int)slkDoiTac.EditValue : (int?)null,
                GhiChu = txtGhiChu.Text,
                NgayChungTu = ngayPhieu,
                NgayTao = DateTime.Now,
                TrangThai = ET.Constants.AppConstants.TrangThaiChungTuKho.Moi,
                IdNguoiTao = SessionManager.IdDoiTac
            };

            var details = new List<ET.Models.Kho.ET_ChiTietChungTu>();
            foreach (var item in _lstChiTiet)
            {
                if (item.IdSanPham <= 0) continue;
                decimal heSo = item.HeSoQuyDoi > 0 ? item.HeSoQuyDoi : 1m;

                // Nếu phiếu 1-kho (không phải CHUYEN_KHO per-row), auto-fill từ header
                int khoXuat = item.IdKhoXuat > 0 ? item.IdKhoXuat : (idKhoXuat ?? 0);
                int khoNhap = item.IdKhoNhap > 0 ? item.IdKhoNhap : (idKhoNhap ?? 0);

                details.Add(new ET.Models.Kho.ET_ChiTietChungTu
                {
                    IdSanPham = item.IdSanPham,
                    IdKhoXuat = khoXuat,
                    IdKhoNhap = khoNhap,
                    SoLuong   = (loai == AppConstants.LoaiChungTuKho.KIEM_KE ? item.DemThucTe : item.SoLuong) * heSo,
                    DonGia    = item.DonGia / heSo,
                    GhiChu    = null
                });
            }
            if (details.Count == 0) {
                XtraMessageBox.Show(LanguageManager.GetString(AppConstants.ErrorMessages.ERR_CHITIETRONG) ?? "Chưa có chi tiết hàng hóa (hoặc hàng hóa rỗng)!", LanguageManager.GetString("TITLE_LOI") ?? "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            // kiểm kê: nếu tất cả số đếm thực tế = 0, có thể user chưa kiểm mà lưu vội
            if (loai == AppConstants.LoaiChungTuKho.KIEM_KE && _lstChiTiet.All(x => x.DemThucTe == 0))
            {
                var xacNhan = XtraMessageBox.Show(
                    LanguageManager.GetString("WARN_KIEMKE_TOAN_KHONG") ?? "Tất cả số đếm thực tế đang = 0. Nếu lưu, toàn bộ tồn kho sẽ bị điều chỉnh về 0. Bạn có chắc chắn?",
                    LanguageManager.GetString("TITLE_CANHBAO") ?? "Cảnh báo",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (xacNhan != DialogResult.Yes) return;
            }
            ct.ChiTiets = details;

            try {
                var result = BUS.Services.Kho.BUS_ChungTuKho.Instance.LuuChungTu(ct);
                if (result.Success)
                {
                    XtraMessageBox.Show(LanguageManager.GetString(result.Message) ?? "Lưu phiếu thành công!", LanguageManager.GetString("TITLE_THONGBAO") ?? "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    _nhatKy.GhiLog("ChungTuKho", result.Data != null ? (int)result.Data : 0, "TaoPhieu",
                        SessionManager.IdDoiTac, null, ct.LoaiChungTu + " | " + ct.MaChungTu);
                    
                    _lstChiTiet.Clear();
                    IsDirty = false;
                    _currentChungTuId = 0;
                    txtMaPhieu.Text = BUS.Services.Kho.BUS_ChungTuKho.Instance.SinhMaChungTu(cboLoaiCT.EditValue.ToString());
                }
                else
                {
                    string errorMsg = result.Message;
                    if (errorMsg.Contains("|"))
                    {
                        var parts = errorMsg.Split('|');
                        string translated = LanguageManager.GetString(parts[0]);
                        errorMsg = (translated ?? parts[0]) + "\nChi tiết: " + parts[1];
                    }
                    else
                    {
                        errorMsg = LanguageManager.GetString(result.Message) ?? result.Message;
                    }
                    XtraMessageBox.Show(errorMsg, LanguageManager.GetString("TITLE_LOI") ?? "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            } catch (Exception ex) {
                XtraMessageBox.Show((LanguageManager.GetString(AppConstants.ErrorMessages.MSG_LUUTHATBAI) ?? "Lưu thất bại!") + "\n" + ex.Message, LanguageManager.GetString("TITLE_LOI") ?? "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public class TempHangHoa : INotifyPropertyChanged
        {
            public int IdSanPham { get; set; }
            public string MaHang { get; set; }
            public string TenHang { get; set; }
            public string DVT { get; set; }
            public decimal HeSoQuyDoi { get; set; } = 1m;
            public int IdKhoXuat { get; set; }
            public int IdKhoNhap { get; set; }
            public string TenKhoXuat { get; set; }
            public string TenKhoNhap { get; set; }

            private decimal _soLuong = 1;
            public decimal SoLuong { get => _soLuong; set { _soLuong = value; TinhTien(); OnPropertyChanged(nameof(SoLuong)); } }

            private decimal _donGia = 0;
            public decimal DonGia { get => _donGia; set { _donGia = value; TinhTien(); OnPropertyChanged(nameof(DonGia)); } }

            private decimal _thanhTien = 0;
            public decimal ThanhTien { get => _thanhTien; private set { _thanhTien = value; OnPropertyChanged(nameof(ThanhTien)); } }

            private decimal _tonMay = 0;
            public decimal TonMay { get => _tonMay; set { _tonMay = value; TinhLech(); OnPropertyChanged(nameof(TonMay)); } }

            private decimal _demThucTe = 0;
            public decimal DemThucTe { get => _demThucTe; set { _demThucTe = value; TinhLech(); OnPropertyChanged(nameof(DemThucTe)); } }

            private decimal _lech = 0;
            public decimal Lech { get => _lech; private set { _lech = value; OnPropertyChanged(nameof(Lech)); } }

            private void TinhTien() { ThanhTien = SoLuong * DonGia; }
            private void TinhLech() { Lech = DemThucTe - TonMay; }

            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}

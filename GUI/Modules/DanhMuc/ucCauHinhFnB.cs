using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BUS.Services.DanhMuc;
using ET.Models.DanhMuc;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using GUI.Infrastructure;

namespace GUI.Modules.DanhMuc
{
    /// <summary>
    /// Panel cấu hình dành riêng cho sản phẩm loại ĐỒ ĂN / ĐỒ UỐNG.
    /// </summary>
    public partial class ucCauHinhFnB : UserControl
    {
        private readonly BUS_SanPham _bus = BUS_SanPham.Instance;
        private BindingList<ET_DinhMucNguyenLieu> _dsDinhMuc = new BindingList<ET_DinhMucNguyenLieu>();
        private List<ET_SanPham> _dsVatTu;
        private HashSet<object> _dongDaSua = new HashSet<object>();

        #region Khởi tạo và tải dữ liệu

        public ucCauHinhFnB()
        {
            InitializeComponent();
            CauHinh();
            ThucHienDichNgonNgu();
        }

        public void ThucHienDichNgonNgu()
        {
            lblTitle.Text = LanguageManager.GetString("LBL_TITLE_FNB") ?? "Dành riêng cho: ĐỒ ĂN & THỨC UỐNG";
            lblNhaHang.Text = LanguageManager.GetString("LBL_NHA_HANG") ?? "Bếp / Bar phụ trách:";
            lblPhanLoai.Text = LanguageManager.GetString("LBL_PHAN_LOAI_FNB") ?? "Phân loại thêm:";
            chkDiUng.Text = LanguageManager.GetString("CHK_DI_UNG") ?? "Món ăn chứa thành phần dễ bề dị ứng";
            lblGridTitle.Text = LanguageManager.GetString("LBL_GRID_TITLE_FNB") ?? "Định mức nguyên liệu (R&D BOM):";

            if (gvBOM.Columns["IdNguyenLieu"] != null) gvBOM.Columns["IdNguyenLieu"].Caption = LanguageManager.GetString("COL_NGUYEN_LIEU") ?? "Vật tư / Nguyên liệu";
            if (gvBOM.Columns["TenDonVi"] != null) gvBOM.Columns["TenDonVi"].Caption = LanguageManager.GetString("COL_DVT") ?? "ĐVT Gốc";
            if (gvBOM.Columns["SoLuong"] != null) gvBOM.Columns["SoLuong"].Caption = LanguageManager.GetString("COL_SO_LUONG") ?? "Số lượng";

            foreach (Control ctrl in gcBOM.Parent.Controls) if (ctrl is SimpleButton btn) btn.Text = LanguageManager.GetString("BTN_THEM_NGUYEN_LIEU") ?? "Thêm nguyên liệu";

            // Dịch danh sách gợi ý Phân loại món
            cboPhanLoai.Properties.Items.Clear();
            cboPhanLoai.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LanguageManager.GetString("FNB_CAT_MONCHINH") ?? "Món chính", "MONCHINH", -1));
            cboPhanLoai.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LanguageManager.GetString("FNB_CAT_TRANGMIENG") ?? "Tráng miệng", "TRANGMIENG", -1));
            cboPhanLoai.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LanguageManager.GetString("FNB_CAT_KHAIVI") ?? "Khai vị", "KHAIVI", -1));
            cboPhanLoai.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LanguageManager.GetString("FNB_CAT_NUOC") ?? "Nước uống", "NUOC", -1));
            cboPhanLoai.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LanguageManager.GetString("FNB_CAT_ANVAT") ?? "Ăn vặt", "ANVAT", -1));
            cboPhanLoai.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LanguageManager.GetString("FNB_CAT_CHAY") ?? "Đồ chay", "CHAY", -1));
            cboPhanLoai.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LanguageManager.GetString("FNB_CAT_DONGCHAI") ?? "Đồ uống đóng chai", "DONGCHAI", -1));
            cboPhanLoai.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LanguageManager.GetString("FNB_CAT_TIENLOI") ?? "Đồ ăn tiện lợi", "TIENLOI", -1));
        }

        private void CauHinh()
        {
            // Danh sách nhà hàng / bếp
            var dsNhaHang = _bus.LayNhaHang();
            slkNhaHang.Properties.DataSource = dsNhaHang;
            slkNhaHang.Properties.DisplayMember = "TenNhaHang";
            slkNhaHang.Properties.ValueMember = "Id";

            // Grid BOM
            AppStyle.StyleGrid(gvBOM);

            // Cột IdNguyenLieu
            _dsVatTu = _bus.LayVatTuKho();
            repoSlkNL.DataSource = _dsVatTu;
            repoSlkNL.DisplayMember = "TenSanPham";
            repoSlkNL.ValueMember = "Id";

            gcBOM.DataSource = _dsDinhMuc;

        }

        /// <summary>
        /// Nạp dữ liệu khi chỉnh sửa sản phẩm đã có.
        /// </summary>
        public void NapDuLieu(ET_SanPham sp)
        {
            if (sp.MonAn != null)
            {
                slkNhaHang.EditValue = sp.MonAn.IdNhaHang;
                chkDiUng.Checked = sp.MonAn.CoDiUng;
                cboPhanLoai.EditValue = sp.MonAn.PhanLoai;
            }

            _dsDinhMuc = new BindingList<ET_DinhMucNguyenLieu>(_bus.LayDinhMuc(sp.Id));
            
            // Map tên ĐVT gốc cho các nguyên liệu đã lưu
            if (_dsVatTu != null)
            {
                foreach (var item in _dsDinhMuc)
                {
                    var spInfo = _dsVatTu.FirstOrDefault(v => v.Id == item.IdNguyenLieu);
                    if (spInfo != null) item.TenDonVi = spInfo.TenDonViTinh;
                }
            }

            gcBOM.DataSource = _dsDinhMuc;
            SetVatTuMode(sp.LaVatTu);
        }

        /// Khóa UI cấu hình định mức nếu đây là Vật tư (Quản lý tồn kho trực tiếp)
        public void SetVatTuMode(bool isVatTu)
        {
            gcBOM.Enabled = !isVatTu;
            foreach (Control ctrl in gcBOM.Parent.Controls) 
            {
                if (ctrl is SimpleButton btn && btn.Name == "btnThemNguyenLieu") 
                {
                    btn.Enabled = !isVatTu;
                }
            }
        }

        #endregion

        #region Thu thập dữ liệu (form cha gọi khi Lưu)

        public ET_MonAn LayMonAn()
        {
            int idNH = int.TryParse(slkNhaHang.EditValue?.ToString(), out int val) ? val : 0;
            
            if (idNH <= 0)
            {
                var ds = _bus.LayNhaHang();
                if (ds != null && ds.Count > 0)
                {
                    idNH = ds[0].Id;
                }
            }

            return new ET_MonAn
            {
                IdNhaHang = idNH,
                CoDiUng = chkDiUng.Checked,
                PhanLoai = cboPhanLoai.EditValue?.ToString().Trim(),
            };
        }

        public List<ET_DinhMucNguyenLieu> LayDinhMuc()
        {
            return _dsDinhMuc.ToList();
        }

        #endregion

        #region Hàm hỗ trợ Grid (duplicate từ frmSanPham_Detail — tiện ích cục bộ)

        private void gvBOM_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            e.Valid = true;
        }

        private void gvBOM_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void gvBOM_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;

            var obj = gv.GetRow(e.RowHandle);
            if (obj != null) _dongDaSua.Add(obj);

            if (e.Column.FieldName == "IdNguyenLieu")
            {
                if (e.Value != null && e.Value != DBNull.Value)
                {
                    if (int.TryParse(e.Value.ToString(), out int idNl))
                    {
                        var spInfo = _dsVatTu?.FirstOrDefault(v => v.Id == idNl);
                        if (spInfo != null)
                        {
                            gv.SetRowCellValue(e.RowHandle, "TenDonVi", spInfo.TenDonViTinh);
                        }
                    }
                }
            }

            gv.PostEditor();
            gv.UpdateCurrentRow();
        }

        private void gvBOM_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;

            var obj = gv.GetRow(e.RowHandle);
            if (obj != null && _dongDaSua.Contains(obj))
            {
                e.Appearance.BackColor = Color.FromArgb(255, 255, 230); 
                e.Appearance.Options.UseBackColor = true;
            }
        }



        private void RepBtn_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvBOM.FocusedRowHandle < 0) return;
            gvBOM.DeleteRow(gvBOM.FocusedRowHandle);
        }

        private void BtnThemNguyenLieu_Click(object sender, EventArgs e)
        {
            var bl = gcBOM.DataSource as BindingList<ET_DinhMucNguyenLieu>;
            if (bl == null) return;
            bl.Add(new ET_DinhMucNguyenLieu());
            gvBOM.FocusedRowHandle = gvBOM.RowCount - 1;
            gvBOM.FocusedColumn = gvBOM.VisibleColumns[0];
            gvBOM.ShowEditor();
        }

        #endregion
    }
}

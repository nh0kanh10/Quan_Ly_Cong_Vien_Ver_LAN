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
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using ET.Constants;
using GUI.Infrastructure;

namespace GUI.Modules.DanhMuc
{
    /// <summary>
    /// Panel cấu hình dành riêng cho sản phẩm loại VÉ.
    /// </summary>
    public partial class ucCauHinhVe : UserControl
    {
        private readonly BUS_SanPham _bus = BUS_SanPham.Instance;
        private BindingList<ET_Ve_QuyenTruyCap> _dsQuyenCong = new BindingList<ET_Ve_QuyenTruyCap>();
        private HashSet<object> _dongDaSua = new HashSet<object>();

        #region Khởi tạo và tải dữ liệu

        public ucCauHinhVe()
        {
            InitializeComponent();
            CauHinh();
            ThucHienDichNgonNgu();
        }

        public void ThucHienDichNgonNgu()
        {
            lblTitle.Text = LanguageManager.GetString("LBL_TITLE_VE") ?? "Dành riêng cho: LOẠI VÉ";
            lblLoaiVe.Text = LanguageManager.GetString("LBL_LOAI_VE") ?? "Phân loại vé:";
            lblDoiTuong.Text = LanguageManager.GetString("LBL_DOI_TUONG_VE") ?? "Đối tượng vé:";
            chkTaoToken.Text = LanguageManager.GetString("CHK_TAO_TOKEN") ?? "Cần tạo token (vé điện tử / mã QR)";
            lblGridTitle.Text = LanguageManager.GetString("LBL_GRID_TITLE_VE") ?? "Danh sách cổng quẹt / khu vực được phép vào:";

            if (colIdKhuVuc != null) colIdKhuVuc.Caption = LanguageManager.GetString("COL_KHU_VUC") ?? "Khu vực";
            if (colIdTroChoi != null) colIdTroChoi.Caption = LanguageManager.GetString("COL_TRO_CHOI") ?? "Cổng / Trò chơi";
            if (colSoLuotChoPhep != null) colSoLuotChoPhep.Caption = LanguageManager.GetString("COL_SO_LUOT") ?? "Số lượt cho phép";
            if (colGhiChu != null) colGhiChu.Caption = LanguageManager.GetString("COL_GHI_CHU") ?? "Ghi chú";

            if (btnThemCong != null) btnThemCong.Text = LanguageManager.GetString("BTN_THEM_CONG") ?? "Thêm cổng";
        }

        private class CboItem
        {
            public string Value { get; set; }
            public string Text { get; set; }
            public override string ToString() => Text;
        }

        private void CauHinh()
        {
            // Loại vé 
            cboLoaiVe.Properties.Items.Clear();
            var dsLoaiVe = BUS.Services.HeThong.BUS_TuDien.Instance.LayDanhSachNhom("VE_LOAI");
            foreach (var item in dsLoaiVe)
                cboLoaiVe.Properties.Items.Add(new CboItem { Value = item.Ma, Text = item.NhanHienThi });
            
            cboLoaiVe.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            if (cboLoaiVe.Properties.Items.Count > 0) cboLoaiVe.SelectedIndex = 0;

            // Đối tượng vé
            cboDoiTuong.Properties.Items.Clear();
            var dsDoiTuong = BUS.Services.HeThong.BUS_TuDien.Instance.LayDanhSachNhom("VE_DOI_TUONG");
            foreach (var item in dsDoiTuong)
                cboDoiTuong.Properties.Items.Add(new CboItem { Value = item.Ma, Text = item.NhanHienThi });

            cboDoiTuong.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            if (cboDoiTuong.Properties.Items.Count > 0) cboDoiTuong.SelectedIndex = 0;

            // Grid quyền quẹt cổng
            AppStyle.StyleGrid(gvQuyenCong);

            // Gán DataSource cho RepositoryItemSearchLookUpEdit
            var dsKhuVuc = BUS.Services.DanhMuc.BUS_KhuVuc.Instance.LayDanhSach(SessionManager.CurrentLanguage);
            repoSlkKV.DataSource = dsKhuVuc;
            repoSlkKV.DisplayMember = "TenKhuVuc";
            repoSlkKV.ValueMember = "Id";

            var dsTroChoi = _bus.LayTroChoi();
            repoSlkTC.DataSource = dsTroChoi;
            repoSlkTC.DisplayMember = "TenTroChoi";
            repoSlkTC.ValueMember = "Id";

            gcQuyenCong.DataSource = _dsQuyenCong;

        }

        /// Nạp dữ liệu khi chỉnh sửa sản phẩm đã có.
        public void NapDuLieu(ET_SanPham sp)
        {
            if (sp.SanPham_Ve != null)
            {
                cboLoaiVe.SelectedItem = cboLoaiVe.Properties.Items.OfType<CboItem>()
                    .FirstOrDefault(x => x.Value == sp.SanPham_Ve.LoaiVe);
                cboDoiTuong.SelectedItem = cboDoiTuong.Properties.Items.OfType<CboItem>()
                    .FirstOrDefault(x => x.Value == sp.SanPham_Ve.DoiTuongVe);
                chkTaoToken.Checked = sp.SanPham_Ve.CanTaoToken;
            }

            _dsQuyenCong = new BindingList<ET_Ve_QuyenTruyCap>(sp.Ve_QuyenTruyCaps.ToList());
            gcQuyenCong.DataSource = _dsQuyenCong;
        }

        #endregion

        #region Lấy dữ liệu (form cha gọi khi Lưu)

        public ET_SanPham_Ve LaySanPhamVe()
        {
            return new ET_SanPham_Ve
            {
                LoaiVe = (cboLoaiVe.SelectedItem as CboItem)?.Value,
                DoiTuongVe = (cboDoiTuong.SelectedItem as CboItem)?.Value,
                CanTaoToken = chkTaoToken.Checked,
            };
        }

        public List<ET_Ve_QuyenTruyCap> LayQuyenTruyCap()
        {
            return _dsQuyenCong.ToList();
        }

        #endregion

        #region Hàm hỗ trợ Grid



        private void Gv_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            e.Valid = true;
        }

        private void Gv_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        private void Gv_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (sender is GridView gv)
            {
                var obj = gv.GetRow(e.RowHandle);
                if (obj != null) _dongDaSua.Add(obj);

                gv.PostEditor();
                gv.UpdateCurrentRow();
            }
        }

        private void Gv_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (sender is GridView gv)
            {
                var obj = gv.GetRow(e.RowHandle);
                if (obj != null && _dongDaSua.Contains(obj))
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 255, 230); // Vàng nhạt
                    e.Appearance.Options.UseBackColor = true;
                }
            }
        }



        private void RepoBtn_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (sender is ButtonEdit btnEdit && btnEdit.Parent is GridControl gc && gc.MainView is GridView gv)
            {
                if (gv.FocusedRowHandle < 0) return;
                gv.DeleteRow(gv.FocusedRowHandle);
            }
            else if (gvQuyenCong.FocusedRowHandle >= 0)
            {
                gvQuyenCong.DeleteRow(gvQuyenCong.FocusedRowHandle);
            }
        }

        private void BtnThemDong_Click(object sender, EventArgs e)
        {
            var bl = gcQuyenCong.DataSource as BindingList<ET_Ve_QuyenTruyCap>;
            if (bl == null) return;

            //  Vé lẻ chỉ được có tối đa 1 cổng/khu vực
            var loaiVeCbo = cboLoaiVe.SelectedItem as CboItem;
            if (loaiVeCbo?.Value == AppConstants.LoaiVe.VeLe && bl.Count >= 1)
            {
                XtraMessageBox.Show("Sản phẩm thuộc phân loại 'Vé Lẻ' chỉ được phép cấu hình tối đa 1 khu vực/trò chơi.", 
                    "Hạn chế", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bl.Add(new ET_Ve_QuyenTruyCap { SoLuotChoPhep = 1 });
            gvQuyenCong.FocusedRowHandle = gvQuyenCong.RowCount - 1;
            gvQuyenCong.FocusedColumn = gvQuyenCong.VisibleColumns[0];
            gvQuyenCong.ShowEditor();
        }

        #endregion
    }
}

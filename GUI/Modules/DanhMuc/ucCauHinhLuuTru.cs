using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using ET.Models.DanhMuc;
using ET.Models.VanHanh;
using BUS.Services.DanhMuc;
using GUI.Infrastructure;

namespace GUI.Modules.DanhMuc
{
    public partial class ucCauHinhLuuTru : XtraUserControl
    {
        private BindingList<ET_VatTuPhongMacDinh> _dsVatTu = new BindingList<ET_VatTuPhongMacDinh>();

        public ucCauHinhLuuTru()
        {
            InitializeComponent();
            SetupGrid();
            TaiNgonNgu();
        }

        private void TaiNgonNgu()
        {
            lblTitle.Text = LanguageManager.GetString("LBL_TITLE_LUU_TRU") ?? lblTitle.Text;
            lblNguoiLon.Text = LanguageManager.GetString("LBL_NGUOI_LON") ?? lblNguoiLon.Text;
            lblTreEm.Text = LanguageManager.GetString("LBL_TRE_EM") ?? lblTreEm.Text;
            lblDienTich.Text = LanguageManager.GetString("LBL_DIEN_TICH") ?? lblDienTich.Text;
            lblTienNghi.Text = LanguageManager.GetString("LBL_TIEN_NGHI") ?? lblTienNghi.Text;
            lblGridTitle.Text = LanguageManager.GetString("LBL_GRID_VAT_TU") ?? lblGridTitle.Text;
            btnThemDong.Text = LanguageManager.GetString("BTN_THEM_VAT_TU") ?? btnThemDong.Text;
            colIdSanPham.Caption = LanguageManager.GetString("COL_VAT_TU") ?? colIdSanPham.Caption;
            colSoLuong.Caption = LanguageManager.GetString("COL_SO_LUONG_SETUP") ?? colSoLuong.Caption;
        }

        #region Khởi tạo và tải dữ liệu

        private void SetupGrid()
        {
            gcVatTu.DataSource = _dsVatTu;

            // Load ComboBox Vật Tư (Chỉ lấy các sản phẩm có LaVatTu = true)
            repoSlkSanPham.DataSource = BUS_SanPham.Instance.LayVatTuKho();
            repoSlkSanPham.DisplayMember = "TenSanPham";
            repoSlkSanPham.ValueMember = "Id";
            repoSlkSanPham.View.Columns.Clear();
            repoSlkSanPham.View.Columns.AddVisible("MaSanPham", LanguageManager.GetString("COL_MASANPHAM") ?? "Mã vật tư");
            repoSlkSanPham.View.Columns.AddVisible("TenSanPham", LanguageManager.GetString("COL_TENSANPHAM") ?? "Tên vật tư");
            repoSlkSanPham.View.Columns["MaSanPham"].Width = 100;
            repoSlkSanPham.View.Columns["TenSanPham"].Width = 300;

            // Gắn icon Delete cho nút Xóa
            repoBtnXoa.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete;
        }

        // Nạp dữ liệu cũ khi ở chế độ Chỉnh Sửa
        public void LoadData(ET_LoaiPhong lp, List<ET_VatTuPhongMacDinh> dsVatTu)
        {
            if (lp != null)
            {
                spinNguoiLon.Value = lp.SoNguoiToiDa.HasValue ? (decimal)lp.SoNguoiToiDa.Value : 2;
                spinTreEm.Value = lp.SoTreEmToiDa.HasValue ? (decimal)lp.SoTreEmToiDa.Value : 1;
                txtDienTich.Text = lp.DienTich.HasValue ? lp.DienTich.Value.ToString("0.##") : "";
                txtTienNghi.Text = lp.TienNghi;
            }

            _dsVatTu.Clear();
            if (dsVatTu != null)
            {
                foreach (var item in dsVatTu)
                {
                    _dsVatTu.Add(item);
                }
            }
        }

        public void ClearData()
        {
            spinNguoiLon.Value = 2;
            spinTreEm.Value = 1;
            txtDienTich.Text = "";
            txtTienNghi.Text = "";
            _dsVatTu.Clear();
        }

        #endregion

        #region Xử lý sự kiện

        private void BtnThemVatTu_Click(object sender, EventArgs e)
        {
            _dsVatTu.Add(new ET_VatTuPhongMacDinh
            {
                SoLuong = 1
            });
            gvVatTu.MoveLast();
            gvVatTu.FocusedColumn = colIdSanPham;
            gvVatTu.ShowEditor();
        }

        private void RepBtnXoa_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var row = gvVatTu.GetFocusedRow() as ET_VatTuPhongMacDinh;
            if (row != null)
            {
                _dsVatTu.Remove(row);
            }
        }

        #endregion

        #region Hàm lấy dữ liệu cho tầng trên

        public ET_LoaiPhong LayDuLieuLoaiPhong()
        {
            decimal dienTich = 0;
            decimal.TryParse(txtDienTich.Text, out dienTich);

            return new ET_LoaiPhong
            {
                SoNguoiToiDa = (int)spinNguoiLon.Value,
                SoTreEmToiDa = (int)spinTreEm.Value,
                DienTich = dienTich > 0 ? dienTich : (decimal?)null,
                TienNghi = txtTienNghi.Text,
                MoTa = "", 
                TenLoai = "KhachSan", 
                ConHoatDong = true
            };
        }

        public List<ET_VatTuPhongMacDinh> LayDuLieuVatTu()
        {
            // Lọc ra những dòng có chọn vật tư
            return _dsVatTu.Where(x => x.IdSanPham > 0).ToList();
        }

        #endregion
    }
}

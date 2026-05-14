using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ET.Models.DanhMuc;
using DAL.Repositories.DanhMuc;
using BUS.Services.DanhMuc;
using ET.Constants;
using GUI.Infrastructure;

namespace GUI.Modules.DanhMuc
{
    public partial class ucCauHinhChoThue : XtraUserControl
    {
        private BindingList<ET_TaiSanChoThue> _dsTaiSan = new BindingList<ET_TaiSanChoThue>();
        private int _currentIdSanPham = 0;
        private bool _daLoad = false;

        public ucCauHinhChoThue()
        {
            InitializeComponent();
            SetupGrid();
            TaiNgonNgu();
        }

        private void TaiNgonNgu()
        {
            lblTitle.Text = LanguageManager.GetString("LBL_TITLE_CHO_THUE") ?? lblTitle.Text;
            lblQuetMa.Text = LanguageManager.GetString("LBL_QUET_MA_NHAP_KHO") ?? lblQuetMa.Text;
            lblGridTitle.Text = LanguageManager.GetString("LBL_GRID_TAI_SAN") ?? lblGridTitle.Text;         
            colMaVach.Caption = LanguageManager.GetString("COL_MA_VACH") ?? colMaVach.Caption;
            colTenTaiSan.Caption = LanguageManager.GetString("COL_TEN_NHAN_DIEN") ?? colTenTaiSan.Caption;
            colKhuVuc.Caption = LanguageManager.GetString("COL_VI_TRI_BAI") ?? colKhuVuc.Caption;
            colTrangThai.Caption = LanguageManager.GetString("COL_TRANG_THAI") ?? colTrangThai.Caption;
        }

        #region Khởi tạo và tải dữ liệu

        private void SetupGrid()
        {
            gcTaiSan.DataSource = _dsTaiSan;

            // Load Khu vực (TuDo / ChoiNghiMat cần idKhuVuc)
            repoSlkKhuVuc.DataSource = BUS_KhuVuc.Instance.LayDanhSach();
            repoSlkKhuVuc.DisplayMember = "TenKhuVuc";
            repoSlkKhuVuc.ValueMember = "Id";

            // Cấu hình hiển thị cột cho repoSlkKhuVuc
            repoSlkKhuVuc.View.Columns.Clear();
            repoSlkKhuVuc.View.Columns.AddVisible("MaKhuVuc", LanguageManager.GetString("COL_MAKHUVUC") ?? "Mã khu vực");
            repoSlkKhuVuc.View.Columns.AddVisible("TenKhuVuc", LanguageManager.GetString("COL_TENKHUVUC") ?? "Tên khu vực");
            repoSlkKhuVuc.View.Columns["MaKhuVuc"].Width = 100;
            repoSlkKhuVuc.View.Columns["TenKhuVuc"].Width = 300;

            // Gắn icon
            repoBtnXoa.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete;
        }

        // Reset cờ Load để khi User chuyển qua Tab này, lưới sẽ được lấy lại data mới
        public void ResetLoadState(int idSanPham)
        {
            _currentIdSanPham = idSanPham;
            _daLoad = false;
            _dsTaiSan.Clear();
            txtMaVach.Text = "";
        }

        private void ucCauHinhChoThue_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible && !_daLoad)
            {
                LoadDanhSachTaiSan();
                _daLoad = true;
            }
        }

        private void LoadDanhSachTaiSan()
        {
            _dsTaiSan.Clear();
            if (_currentIdSanPham > 0)
            {
                var dbData = DAL_TaiSanChoThue.Instance.LayTheoSanPham(_currentIdSanPham);
                foreach (var item in dbData)
                {
                    _dsTaiSan.Add(item);
                }
            }
        }

        #endregion

        #region Xử lý sự kiện

        private void TxtMaVach_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string barcode = txtMaVach.Text.Trim();
                if (string.IsNullOrEmpty(barcode)) return;

                // 1. Chống trùng mã vạch trên UI
                if (_dsTaiSan.Any(x => x.MaVachThietBi.Equals(barcode, StringComparison.OrdinalIgnoreCase)))
                {
                    UIHelper.CanhBao("Mã vạch này đã có trong lưới!");
                    txtMaVach.SelectAll();
                    return;
                }

                // 2. Chống trùng mã vạch dưới DB (Tài sản khác đang dùng)
                var existing = DAL_TaiSanChoThue.Instance.LayTheoBarcode(barcode);
                if (existing != null && existing.IdSanPham != _currentIdSanPham)
                {
                    UIHelper.CanhBao($"Mã vạch này đang được dùng cho sản phẩm: {existing.TenSanPham}");
                    txtMaVach.SelectAll();
                    return;
                }

                // 3. Thêm vào lưới
                _dsTaiSan.Add(new ET_TaiSanChoThue
                {
                    Id = 0,
                    MaVachThietBi = barcode,
                    TenTaiSan = "Tài sản " + barcode, // Tên mặc định, User có thể sửa trên grid
                    TrangThai = AppConstants.TrangThaiTaiSan.SanSang
                });

                txtMaVach.Text = "";
                txtMaVach.Focus();
                gvTaiSan.MoveLast();
            }
        }

        private void RepBtnXoa_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var row = gvTaiSan.GetFocusedRow() as ET_TaiSanChoThue;
            if (row != null)
            {
                if (row.Id > 0 && row.TrangThai != AppConstants.TrangThaiTaiSan.SanSang)
                {
                    UIHelper.CanhBao("Tài sản đang được thuê hoặc bảo trì. Không thể xóa!");
                    return;
                }
                _dsTaiSan.Remove(row);
            }
        }

        #endregion

        #region Hàm lấy dữ liệu cho tầng trên

        public List<ET_TaiSanChoThue> LayDuLieuTaiSan()
        {
            return _dsTaiSan.ToList();
        }

        #endregion
    }
}

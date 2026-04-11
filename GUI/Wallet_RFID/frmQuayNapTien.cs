using System;
using System.Drawing;
using System.Windows.Forms;
using BUS;
using ET;
using FontAwesome.Sharp;

namespace GUI
{
    public partial class frmQuayNapTien : Form, IBaseForm
    {
        private ET_ViDienTu _currentVi = null;
        private string _currentRfid = null;
        

        public frmQuayNapTien(string maRfid = null)
        {
            InitializeComponent();
            
            InitIcons();
            ApplyStyles();
            ApplyPermissions();

            SetFormState(false);
            
            if (!string.IsNullOrEmpty(maRfid))
            {
                this.Shown += (s, e) =>
                {
                    txtMaRfid.Text = maRfid;
                    BtnTraCuu_Click(null, null);
                };
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaRfid.Clear();
            SetFormState(false);
            _currentVi = null;
            _currentRfid = null;
        }

        private void btnNap50k_Click(object sender, EventArgs e) => spnSoTien.Value = 50000;
        private void btnNap100k_Click(object sender, EventArgs e) => spnSoTien.Value = 100000;
        private void btnNap200k_Click(object sender, EventArgs e) => spnSoTien.Value = 200000;
        private void btnNap500k_Click(object sender, EventArgs e) => spnSoTien.Value = 500000;
        private void btnNap1M_Click(object sender, EventArgs e) => spnSoTien.Value = 1000000;

        private void txtMaRfid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnTraCuu_Click(null, null);
                e.SuppressKeyPress = true;
            }
        }

        public void ApplyPermissions()
        {
            var tk = ET.SessionManager.CurrentUser;
            if (tk == null) return;

            if (!BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "VIEW_POS"))
            {
                this.Enabled = false;
                return;
            }

            // Gating specific Top-up actions if needed
            bool canManage = BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "MANAGE_POS");
            btnNapTien.Enabled = canManage;
        }

        public void ApplyStyles()
        {
            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleDevExpressGrid(gridLichSu);
        }

        public void InitIcons()
        {
            btnTraCuu.Image = IconHelper.GetBitmap(IconChar.Search, Color.White, 18);
            btnNapTien.Image = IconHelper.GetBitmap(IconChar.PlusCircle, Color.White, 22);
            btnLamMoi.Image = IconHelper.GetBitmap(IconChar.Sync, Color.White, 18);
        }

        private void frmQuayNapTien_Load(object sender, EventArgs e)
        {
            

        }

        public void LoadData()
        {
            if (_currentVi != null) LoadLichSuGiaoDich();
        }


        /// <summary>
        /// Enable/disable nạp tiền section khi chưa/đã tra cứu.
        /// </summary>
        /// <param name="hasWallet"></param>
        private void SetFormState(bool hasWallet)
        {
            gbNapTien.Enabled = hasWallet;
            btnNapTien.Enabled = hasWallet;

            if (!hasWallet)
            {
                lblTenKhachValue.Text = "---";
                lblSoDuValue.Text = "0 đ";
                gridLichSu.DataSource = null;
            }
        }

        private void BtnTraCuu_Click(object sender, EventArgs e)
        {
            string maRfid = txtMaRfid.Text.Trim();
            if (string.IsNullOrEmpty(maRfid))
            {
                TDCMessageBox.Show("Vui lòng nhập mã RFID!", "Thông báo");
                txtMaRfid.Focus();
                return;
            }

            var result = BUS_GiaoDichVi.Instance.TraCuuViTheoRFID(maRfid);
            if (!result.IsSuccess)
            {
                TDCMessageBox.Show(result.ErrorMessage, "Lỗi tra cứu");
                SetFormState(false);
                return;
            }

            _currentVi = result.Data;
            _currentRfid = maRfid;

            // Hiển thị thông tin ví
            string tenKH = BUS_GiaoDichVi.Instance.LayTenKhachHang(_currentVi.IdKhachHang);
            lblTenKhachValue.Text = tenKH;
            lblSoDuValue.Text = _currentVi.SoDuKhaDung.ToString("N0") + " đ";

            // Load lịch sử giao dịch
            LoadLichSuGiaoDich();

            SetFormState(true);
        }

        private void BtnNapTien_Click(object sender, EventArgs e)
        {
            if (_currentVi == null || string.IsNullOrEmpty(_currentRfid))
            {
                TDCMessageBox.Show("Vui lòng tra cứu thẻ RFID trước!", "Thông báo");
                return;
            }

            decimal soTien = spnSoTien.Value;
            if (soTien <= 0)
            {
                TDCMessageBox.Show("Số tiền nạp phải lớn hơn 0!", "Thông báo");
                return;
            }

            string phuongThuc = cboPhuongThuc.Text;

            if (string.IsNullOrEmpty(phuongThuc))
            {
                TDCMessageBox.Show("Vui lòng chọn phương thức thanh toán!", "Thông báo");
                return;
            }

            if (phuongThuc == "Tiền mặt") phuongThuc = AppConstants.PhuongThucThanhToan.TienMat;
            if (phuongThuc == "Ví RFID") phuongThuc = AppConstants.PhuongThucThanhToan.ViRfid;
            if (phuongThuc == "Chuyển khoản") phuongThuc = AppConstants.PhuongThucThanhToan.ChuyenKhoan;

            // Xác nhận
            string msg = string.Format("Xác nhận nạp {0} đ vào thẻ {1}?\nPhương thức: {2}",
                soTien.ToString("N0"), _currentRfid, phuongThuc);

            if (TDCMessageBox.Show(msg, "Xác nhận nạp tiền", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            var result = BUS_GiaoDichVi.Instance.NapTien(_currentRfid, soTien, phuongThuc);
            if (result.IsSuccess)
            {
                TDCMessageBox.Show("Nạp tiền thành công! ✅", "Thông báo");

                // Refresh thông tin ví
                BtnTraCuu_Click(null, null);
            }
            else
            {
                TDCMessageBox.Show(result.ErrorMessage, "Lỗi nạp tiền");
            }
        }

        private void LoadLichSuGiaoDich()
        {
            if (_currentVi == null) return;

            var lichSu = BUS_GiaoDichVi.Instance.LayLichSuGiaoDich(_currentVi.Id);
            gridLichSu.DataSource = lichSu;
            gridViewLichSu.PopulateColumns();

            // Format grid columns
            var view = gridViewLichSu;
            if (view.Columns.Count > 0)
            {
                if (view.Columns["Id"] != null) view.Columns["Id"].Visible = false;
                if (view.Columns["IdVi"] != null) view.Columns["IdVi"].Visible = false;
                if (view.Columns["IdDonHangLienQuan"] != null) view.Columns["IdDonHangLienQuan"].Visible = false;
                if (view.Columns["ParentTransactionId"] != null) view.Columns["ParentTransactionId"].Visible = false;
                if (view.Columns["HashSignature"] != null) view.Columns["HashSignature"].Visible = false;
                if (view.Columns["CreatedAt"] != null) view.Columns["CreatedAt"].Visible = false;
                if (view.Columns["CreatedBy"] != null) view.Columns["CreatedBy"].Visible = false;

                if (view.Columns["MaCode"] != null) view.Columns["MaCode"].Caption = "Mã GD";
                if (view.Columns["LoaiGiaoDich"] != null) view.Columns["LoaiGiaoDich"].Caption = "Loại";
                if (view.Columns["SoTien"] != null)
                {
                    view.Columns["SoTien"].Caption = "Số Tiền";
                    view.Columns["SoTien"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    view.Columns["SoTien"].DisplayFormat.FormatString = "N0";
                }
                if (view.Columns["ThoiGian"] != null)
                {
                    view.Columns["ThoiGian"].Caption = "Thời Gian";
                    view.Columns["ThoiGian"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    view.Columns["ThoiGian"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
                }

                view.BestFitColumns();
            }
        }
    }
}



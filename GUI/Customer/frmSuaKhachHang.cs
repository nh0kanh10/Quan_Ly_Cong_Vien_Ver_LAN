using BUS;
using System;
using System.Windows.Forms;
using ET;

namespace GUI
{
    public partial class frmSuaKhachHang : Form
    {
        private readonly ET_KhachHang _existing;
        private bool _isAdd;

        /// <summary>
        /// After save, contains the MaCode of the saved customer (for re-selection).
        /// </summary>
        public string SavedMaCode { get; private set; }

        /// <param name="existing">null = Thêm mới, not null = Sửa</param>
        public frmSuaKhachHang(ET_KhachHang existing)
        {
            InitializeComponent();
            SetupDateEdit();
            _existing = existing;
            _isAdd = (existing == null);

            if (_isAdd)
            {
                this.Text = "Thêm Khách Hàng Mới";
                txtMaKH.Text = BUS_KhachHang.Instance.LayMaCodeTiepTheo();
            }
            else
            {
                this.Text = "Sửa Thông Tin Khách Hàng";
                FillForm(existing);
            }

            btnLuu.Click += BtnLuu_Click;
            btnHuy.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
        }

        private void SetupDateEdit()
        {
            this.dtpNgaySinh.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10f);
            this.dtpNgaySinh.Properties.Appearance.Options.UseFont = true;
            this.dtpNgaySinh.Properties.AutoHeight = false; // Allow manual height of 36
            this.dtpNgaySinh.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dtpNgaySinh.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpNgaySinh.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dtpNgaySinh.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpNgaySinh.Properties.MaskSettings.Set("mask", "dd/MM/yyyy");
        }

        private void FillForm(ET_KhachHang kh)
        {
            txtMaKH.Text = kh.MaCode;
            txtHoTen.Text = kh.HoTen;
            txtSDT.Text = kh.DienThoai;
            txtEmail.Text = kh.Email;
            txtDiaChi.Text = kh.DiaChi;

            if (!string.IsNullOrEmpty(kh.GioiTinh))
            {
                int idx = cboGioiTinh.Items.IndexOf(kh.GioiTinh);
                if (idx >= 0) cboGioiTinh.SelectedIndex = idx;
            }

            if (kh.NgaySinh.HasValue)
                dtpNgaySinh.DateTime = kh.NgaySinh.Value;

            if (!string.IsNullOrEmpty(kh.LoaiKhach))
            {
                int idx = cboLoaiKhach.Items.IndexOf(kh.LoaiKhach);
                if (idx >= 0) cboLoaiKhach.SelectedIndex = idx;
            }
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            var et = _isAdd ? new ET_KhachHang() : _existing;

            et.MaCode = txtMaKH.Text.Trim();
            et.HoTen = txtHoTen.Text.Trim();
            et.GioiTinh = cboGioiTinh.SelectedItem?.ToString() ?? "Nam";
            et.NgaySinh = dtpNgaySinh.DateTime;
            et.DienThoai = txtSDT.Text.Trim();
            et.Email = txtEmail.Text.Trim();
            et.DiaChi = txtDiaChi.Text.Trim();
            et.LoaiKhach = cboLoaiKhach.SelectedItem?.ToString() ?? AppConstants.LoaiKhachHang.CaNhan;

            // Validate
            string err = BUS_KhachHang.Instance.ValidateKhachHang(et, _isAdd);
            if (!string.IsNullOrEmpty(err))
            {
                TDCMessageBox.Show(err, "Lỗi dữ liệu");
                return;
            }

            // Save
            ResponseResult result;
            if (_isAdd)
            {
                result = BUS_KhachHang.Instance.Them(et);
            }
            else
            {
                result = BUS_KhachHang.Instance.Sua(et);
            }

            if (result.IsSuccess)
            {
                SavedMaCode = et.MaCode;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                TDCMessageBox.Show(result.ErrorMessage, "Lỗi lưu");
            }
        }
    }
}

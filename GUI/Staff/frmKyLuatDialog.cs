using System;
using System.Drawing;
using System.Windows.Forms;
using BUS;
using ET;

namespace GUI
{
    public partial class frmKyLuatDialog : Form
    {
        private readonly int _idNhanVien;

        private static readonly string[] HinhThucList =
        {
            AppConstants.HinhThucKyLuat.CanhCao, 
            AppConstants.HinhThucKyLuat.TruLuong, 
            AppConstants.HinhThucKyLuat.DinhChiCoLuong, 
            AppConstants.HinhThucKyLuat.DinhChiKhongLuong, 
            AppConstants.HinhThucKyLuat.SaThai
        };

        public frmKyLuatDialog(int idNhanVien)
        {
            _idNhanVien = idNhanVien;
            InitializeComponent();
            cboHinhThuc.Items.AddRange(HinhThucList);
            cboHinhThuc.SelectedIndex = 0;
        }

        private void cboHinhThuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ht = cboHinhThuc.SelectedItem?.ToString() ?? "";
            numTienTru.Enabled     = (ht == AppConstants.HinhThucKyLuat.TruLuong);
            numNgayDinhChi.Enabled = (ht == AppConstants.HinhThucKyLuat.DinhChiCoLuong || ht == AppConstants.HinhThucKyLuat.DinhChiKhongLuong);
            pnlCanhBao.Visible     = (ht == AppConstants.HinhThucKyLuat.SaThai);

            if (!numTienTru.Enabled)     numTienTru.Value     = 0;
            if (!numNgayDinhChi.Enabled) numNgayDinhChi.Value = 0;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMoTa.Text))
            {
                MessageBox.Show("Vui lòng nhập lý do kỷ luật!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMoTa.Focus();
                return;
            }

            var currentUser = ET.SessionManager.CurrentUser;
            var et = new ET_KyLuat
            {
                IdNhanVien       = _idNhanVien,
                NgayApDung       = dtpNgayApDung.DateTime.Date,
                NgayHetHieuLuc   = dtpHetHieuLuc.DateTime.Date,
                HinhThuc         = cboHinhThuc.SelectedItem?.ToString(),
                SoTienTru        = numTienTru.Value,
                SoNgayDinhChi    = (int)numNgayDinhChi.Value,
                MoTa             = txtMoTa.Text.Trim(),
                IdNguoiQuyetDinh = currentUser?.Id ?? 0
            };

            var res = BUS_NhanVien.Instance.ThemKyLuat(et);
            if (res.IsSuccess) { DialogResult = DialogResult.OK; Close(); }
            else MessageBox.Show(res.ErrorMessage, "Lỗi validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

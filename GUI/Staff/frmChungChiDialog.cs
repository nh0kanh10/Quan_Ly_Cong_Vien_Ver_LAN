using System;
using System.Drawing;
using System.Windows.Forms;
using BUS;
using ET;

namespace GUI
{
    public partial class frmChungChiDialog : Form
    {
        private readonly int _idNhanVien;

        private static readonly string[] LoaiList =
        {
            AppConstants.LoaiChungChi.CuuHoBoiLoi, 
            AppConstants.LoaiChungChi.SoCuuYTe_CPR, 
            AppConstants.LoaiChungChi.VanHanhThietBiCoKhi,
            AppConstants.LoaiChungChi.ChamSocDongVatHoangDa, 
            AppConstants.LoaiChungChi.LaiXeNangHang, 
            AppConstants.LoaiChungChi.AnToanDien, 
            AppConstants.LoaiChungChi.Khac
        };

        public frmChungChiDialog(int idNhanVien)
        {
            _idNhanVien = idNhanVien;
            InitializeComponent();
            cboLoaiChungChi.Items.AddRange(LoaiList);
            cboLoaiChungChi.SelectedIndex = 0;
            dtpNgayHetHan.DateTime = DateTime.Today.AddYears(2);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (dtpNgayHetHan.DateTime.Date < dtpNgayCap.DateTime.Date)
            { MessageBox.Show("Ngày hết hạn phải sau ngày cấp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            var et = new ET_ChungChiNhanVien
            {
                IdNhanVien   = _idNhanVien,
                LoaiChungChi = cboLoaiChungChi.SelectedItem?.ToString(),
                SoChungChi   = txtSoChungChi.Text.Trim(),
                NhaCap       = txtNhaCap.Text.Trim(),
                NgayCap      = dtpNgayCap.DateTime.Date,
                NgayHetHan   = dtpNgayHetHan.DateTime.Date
            };

            var res = BUS_NhanVien.Instance.ThemChungChi(et);
            if (res.IsSuccess) { DialogResult = DialogResult.OK; Close(); }
            else MessageBox.Show(res.ErrorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

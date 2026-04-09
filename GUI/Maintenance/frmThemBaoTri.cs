using System;
using System.Windows.Forms;
using BUS;
using ET;

namespace GUI
{
    public partial class frmThemBaoTri : Form
    {
        private int _idThietBi;

        public frmThemBaoTri(int idThietBi, string tenThietBi)
        {
            InitializeComponent();
            _idThietBi = idThietBi;
            lblThietBi.Text = "Thiết bị: " + tenThietBi;
            ThemeManager.ApplyTheme(this);

            cboLoai.Items.Clear();
            cboLoai.Items.AddRange(new object[] { "DieuDo", "SuaChua", "ThayThe", "ThanhLy" });
            cboLoai.SelectedIndex = 0;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNoiDung.Text))
            {
                TDCMessageBox.Show("Vui lòng nhập nội dung bảo trì!", "Thông báo");
                return;
            }

            decimal chiPhi = 0;
            decimal.TryParse(txtChiPhi.Text.Replace(".", "").Replace(",", ""), out chiPhi);

            var et = new ET_LichBaoTri
            {
                IdThietBi = _idThietBi,
                NgayBaoTri = dtpNgay.DateTime.Date,
                LoaiBaoTri = cboLoai.Text,
                NoiDung = txtNoiDung.Text.Trim(),
                ChiPhi = chiPhi,
                TrangThai = "KeHoach"
            };

            if (BUS_LichBaoTri.Instance.Them(et))
            {
                TDCMessageBox.Show("Thêm lịch bảo trì thành công!", "Thông báo");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else TDCMessageBox.Show("Lỗi khi thêm.", "Lỗi");
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}


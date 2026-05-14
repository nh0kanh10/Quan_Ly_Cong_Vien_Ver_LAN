using System;
using System.Windows.Forms;
using BUS.Services.TaiChinh;
using ET.Constants;

namespace GUI.Modules.TaiChinh
{
    public partial class frmNapViRFID : DevExpress.XtraEditors.XtraForm
    {
        private int _idKhachHang = 0;
        private int _idNguoiTao;

        public frmNapViRFID(string maTheGoiY = "", int idNguoiTao = 0)
        {
            InitializeComponent();
            _idNguoiTao = idNguoiTao;
            
            // Tải phương thức thanh toán
            cboPhuongThuc.Properties.Items.Add("Tiền mặt");
            cboPhuongThuc.Properties.Items.Add("Chuyển khoản");
            cboPhuongThuc.SelectedIndex = 0;

            if (!string.IsNullOrEmpty(maTheGoiY))
            {
                txtMaThe.Text = maTheGoiY;
                TraCuuThe(maTheGoiY);
            }
        }

        private void btnQuet_Click(object sender, EventArgs e)
        {
            string maThe = txtMaThe.Text.Trim();
            if (string.IsNullOrEmpty(maThe))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Vui lòng nhập mã thẻ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            TraCuuThe(maThe);
        }

        private void TraCuuThe(string maThe)
        {
            var res = BUS_ViDienTu.Instance.TraCuuViTheoMaThe(maThe);
            if (res.Success)
            {
                var data = res.Data as System.Collections.Generic.Dictionary<string, object>;
                _idKhachHang = (int)data["IdKhachHang"];
                decimal soDu = (decimal)data["SoDuVi"];
                lblSoDu.Text = $"Số dư hiện tại: {soDu:N0} đ";
                lblSoDu.ForeColor = System.Drawing.Color.Green;
                btnNapTien.Enabled = true;
            }
            else
            {
                _idKhachHang = 0;
                lblSoDu.Text = res.Message;
                lblSoDu.ForeColor = System.Drawing.Color.Red;
                btnNapTien.Enabled = false;
            }
        }

        private void btnNapTien_Click(object sender, EventArgs e)
        {
            if (_idKhachHang <= 0) return;

            decimal soTien = spinSoTien.Value;
            if (soTien <= 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Số tiền nạp phải lớn hơn 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string phuongThuc = cboPhuongThuc.Text;
            var res = BUS_ViDienTu.Instance.NapTien(_idKhachHang, soTien, phuongThuc, _idNguoiTao);

            if (res.Success)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Nạp tiền thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(res.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

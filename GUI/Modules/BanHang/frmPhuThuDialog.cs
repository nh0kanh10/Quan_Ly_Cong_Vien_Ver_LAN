using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ET.Constants;
using GUI.Infrastructure;

namespace GUI.Modules.BanHang
{
    public partial class frmPhuThuDialog : XtraForm
    {
        public decimal SoTien => decimal.TryParse(txtSoTien.Text, out decimal tien) ? tien : 0;
        public string GhiChu => txtGhiChu.Text.Trim();

        public frmPhuThuDialog()
        {
            InitializeComponent();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (SoTien <= 0)
            {
                string msgTien = LanguageManager.GetString("ERR_TIEN_PHU_THU_INVALID") ?? "Vui lòng nhập số tiền phụ thu hợp lệ (>0)!";
                string msgLoi = LanguageManager.GetString("TITLE_ERROR") ?? "Lỗi";
                XtraMessageBox.Show(msgTien, msgLoi, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoTien.Focus();
                return;
            }
            if (string.IsNullOrEmpty(GhiChu))
            {
                string msgGhiChu = LanguageManager.GetString("ERR_GHI_CHU_INVALID") ?? "Vui lòng nhập ghi chú (VD: Làm vỡ ly, giặt ủi...)!";
                string msgLoi = LanguageManager.GetString("TITLE_ERROR") ?? "Lỗi";
                XtraMessageBox.Show(msgGhiChu, msgLoi, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGhiChu.Focus();
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F2)
            {
                btnLuu.PerformClick();
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                btnHuy.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}

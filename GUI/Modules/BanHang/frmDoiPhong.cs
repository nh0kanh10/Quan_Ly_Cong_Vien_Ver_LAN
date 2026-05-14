using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GUI.Infrastructure;

namespace GUI.Modules.BanHang
{
    public partial class frmDoiPhong : XtraForm
    {
        public int IdPhongMoi { get; private set; }
        public string LyDo { get; private set; }

        public frmDoiPhong(List<ET.DTOs.DTO_PhongLuuTruView> danhSachPhongTrong)
        {
            InitializeComponent();
            cboPhongMoi.Properties.DataSource = danhSachPhongTrong;
        }

        private void frmDoiPhong_Load(object sender, EventArgs e)
        {
            AppStyle.StyleForm(this);
            AppStyle.StyleBtnPrimary(btnXacNhan);
            AppStyle.StyleBtnSecondary(btnHuy);

            this.Text = LanguageManager.GetString("TITLE_DOI_PHONG") ?? "Đổi Phòng";
            layoutPhongMoi.Text = LanguageManager.GetString("LBL_PHONG_MOI") ?? "Chọn phòng mới";
            layoutLyDo.Text = LanguageManager.GetString("LBL_LY_DO") ?? "Lý do đổi";
            btnXacNhan.Text = LanguageManager.GetString("BTN_XAC_NHAN") ?? "Xác nhận";
            btnHuy.Text = LanguageManager.GetString("BTN_HUY_BO") ?? "Hủy bỏ";
        }

        private void BtnXacNhan_Click(object sender, EventArgs e)
        {
            if (cboPhongMoi.EditValue != null && int.TryParse(cboPhongMoi.EditValue.ToString(), out int idPhong))
            {
                IdPhongMoi = idPhong;
                LyDo = txtLyDo.Text.Trim();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

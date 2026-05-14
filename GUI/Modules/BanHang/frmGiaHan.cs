using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GUI.Infrastructure;

namespace GUI.Modules.BanHang
{
    public partial class frmGiaHan : XtraForm
    {
        public DateTime NgayCheckOutMoi { get; private set; }

        public frmGiaHan(DateTime ngayCheckOutHienTai)
        {
            InitializeComponent();
            dtpNgayMoi.DateTime = ngayCheckOutHienTai.AddDays(1);
            dtpNgayMoi.Properties.MinValue = ngayCheckOutHienTai.AddDays(1);
        }

        private void frmGiaHan_Load(object sender, EventArgs e)
        {
            AppStyle.StyleForm(this);
            AppStyle.StyleBtnPrimary(btnXacNhan);
            AppStyle.StyleBtnSecondary(btnHuy);

            this.Text = LanguageManager.GetString("TITLE_GIA_HAN") ?? "Gia Hạn Phòng";
            layoutNgayMoi.Text = LanguageManager.GetString("LBL_NGAY_TRA_MOI") ?? "Ngày trả mới";
            btnXacNhan.Text = LanguageManager.GetString("BTN_XAC_NHAN") ?? "Xác nhận";
            btnHuy.Text = LanguageManager.GetString("BTN_HUY_BO") ?? "Hủy bỏ";
        }

        private void BtnXacNhan_Click(object sender, EventArgs e)
        {
            NgayCheckOutMoi = dtpNgayMoi.DateTime;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

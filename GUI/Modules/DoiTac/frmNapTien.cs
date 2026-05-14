using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GUI.Infrastructure;
using ET.Constants;

namespace GUI.Modules.DoiTac
{
    public partial class frmNapTien : XtraForm
    {
        public decimal SoTien { get; private set; }
        public string PhuongThuc { get; private set; }

        private readonly string[] _dsPhuongThuc = new[]
        {
            AppConstants.PhuongThucTT.TienMat,
            AppConstants.PhuongThucTT.ChuyenKhoan,
            AppConstants.PhuongThucTT.QR,
            AppConstants.PhuongThucTT.MoMo,
            AppConstants.PhuongThucTT.TheNganHang
        };

        public frmNapTien()
        {
            InitializeComponent();
            ApplyStyle();
            DichNgonNgu();
            NapComboBox();
            AppStyle.FixEditorForeColor(this);
        }


        private void ApplyStyle()
        {
            lblTitle.ForeColor = AppStyle.Teal;
            AppStyle.StyleBtnPrimary(btnXacNhan);
            AppStyle.StyleBtnOutline(btnHuy, AppStyle.Navy);
        }

        private void DichNgonNgu()
        {
            Text = LanguageManager.GetString("FRM_NAP_TIEN_TITLE") ?? "Nạp tiền ví";
            lblTitle.Text = LanguageManager.GetString("FRM_NAP_TIEN_HEADER") ?? "NẠP TIỀN VÍ ĐIỆN TỬ";
            lblSoTien.Text = LanguageManager.GetString("LBL_NAP_SOTIEN") ?? "Số tiền nạp (*)";
            lblPhuongThuc.Text = LanguageManager.GetString("LBL_NAP_PHUONGTHUC") ?? "Hình thức thanh toán (*)";
            btnXacNhan.Text = "  " + (LanguageManager.GetString("BTN_NAP_XACNHAN") ?? "Xác nhận nạp tiền");
            btnHuy.Text = LanguageManager.GetString("BTN_HUY_CHUNG") ?? "Hủy";
        }

        private void NapComboBox()
        {
            cboPhuongThuc.Properties.Items.Clear();
            foreach (var pt in _dsPhuongThuc)
            {
                cboPhuongThuc.Properties.Items.Add(LanguageManager.GetString("PTTT_" + pt) ?? DichPhuongThuc(pt));
            }
            cboPhuongThuc.SelectedIndex = 0;
        }

        private string DichPhuongThuc(string ma)
        {
            switch (ma)
            {
                case AppConstants.PhuongThucTT.TienMat: return "Tiền mặt";
                case AppConstants.PhuongThucTT.ChuyenKhoan: return "Chuyển khoản";
                case AppConstants.PhuongThucTT.QR: return "QR Code";
                case AppConstants.PhuongThucTT.MoMo: return "Ví MoMo";
                case AppConstants.PhuongThucTT.TheNganHang: return "Thẻ ngân hàng";
                default: return ma;
            }
        }

        private void BtnXacNhan_Click(object sender, EventArgs e)
        {
            if (numSoTien.Value <= 0)
            {
                UIHelper.Loi(LanguageManager.GetString(AppConstants.ErrorMessages.ERR_VI_SO_TIEN_AM) ?? "Số tiền phải lớn hơn 0!");
                numSoTien.Focus();
                return;
            }

            SoTien = numSoTien.Value;
            PhuongThuc = cboPhuongThuc.SelectedIndex >= 0
                ? _dsPhuongThuc[cboPhuongThuc.SelectedIndex]
                : AppConstants.PhuongThucTT.TienMat;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}

using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace GUI.Modules.BanHang
{
    public partial class ucVeDienTu : XtraUserControl
    {
        public ucVeDienTu()
        {
            InitializeComponent();
        }

        public ucVeDienTu(string tenVe, string maVach) : this()
        {
            grpCard.Text = tenVe;
            lblMa.Text = maVach;
            qrCode.Text = maVach;
        }
    }
}

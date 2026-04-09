using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmGhepBanDialog : Form
    {
        public bool IsSuccess { get; private set; }
        public int IdBanGoc { get; private set; }
        public int IdBanBiGhep { get; private set; }

        private int _id1, _id2;
        private string _maBan1, _maBan2;

        public frmGhepBanDialog(int id1, string maBan1, int id2, string maBan2)
        {
            InitializeComponent();
            this.Text = "Giữ lại Bill của bàn nào?";

            _id1 = id1;
            _id2 = id2;
            _maBan1 = maBan1;
            _maBan2 = maBan2;
        }

        private void frmGhepBanDialog_Load(object sender, EventArgs e)
        {
            if (ThemeManager.IsInDesignMode(this)) return;
            ThemeManager.ApplyTheme(this);

            rdoBan1.Text = "Bàn " + _maBan1;
            rdoBan2.Text = "Bàn " + _maBan2;
            rdoBan1.Checked = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (rdoBan1.Checked)
            {
                IdBanGoc = _id1;
                IdBanBiGhep = _id2;
            }
            else
            {
                IdBanGoc = _id2;
                IdBanBiGhep = _id1;
            }
            IsSuccess = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            IsSuccess = false;
            this.Close();
        }
    }
}

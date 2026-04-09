using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmThanhToanHinhThuc : Form
    {
        public bool IsSuccess { get; private set; }
        public string PhuongThuc { get; private set; }

        public frmThanhToanHinhThuc()
        {
            InitializeComponent();
            this.Text = "Chọn hình thức thanh toán";
        }

        private void frmThanhToanHinhThuc_Load(object sender, EventArgs e)
        {
            if (ThemeManager.IsInDesignMode(this)) return;
            ThemeManager.ApplyTheme(this);
            cboPhuongThuc.SelectedIndex = 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cboPhuongThuc.SelectedIndex < 0) return;
            IsSuccess = true;
            // Ánh xạ ra chuỗi hằng số (TienMat, ViRFID, ChuyenKhoan)
            if (cboPhuongThuc.SelectedIndex == 0) PhuongThuc = "TienMat";
            else if (cboPhuongThuc.SelectedIndex == 1) PhuongThuc = "ViRFID";
            else if (cboPhuongThuc.SelectedIndex == 2) PhuongThuc = "ChuyenKhoan";
            else PhuongThuc = "TienMat";
            
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            IsSuccess = false;
            this.Close();
        }
    }
}

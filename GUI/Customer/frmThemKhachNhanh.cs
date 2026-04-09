using System;
using System.Windows.Forms;
using BUS;
using ET;

namespace GUI
{
    public partial class frmThemKhachNhanh : Form
    {
        public bool IsSuccess { get; private set; }
        public ET_KhachHang ThongTinKhachMoi { get; private set; }

        public frmThemKhachNhanh()
        {
            InitializeComponent();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên khách hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (txtTen.Text.Trim().Length < 2)
            {
                MessageBox.Show("Họ tên phải có ít nhất 2 ký tự.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var ds = BUS_KhachHang.Instance.LoadDS();

            string sdt = txtSdt.Text.Trim();
            if (!string.IsNullOrEmpty(sdt))
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(sdt, @"^\d{10}$"))
                {
                    MessageBox.Show("Số điện thoại không hợp lệ (phải gồm 10 chữ số).", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (System.Linq.Enumerable.Any(ds, x => x.DienThoai == sdt))
                {
                    MessageBox.Show("Số điện thoại này đã tồn tại trong hệ thống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            string cccd = txtCccd.Text.Trim();
            if (string.IsNullOrEmpty(cccd))
            {
                MessageBox.Show("Vui lòng nhập CCCD/CMND!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (!System.Text.RegularExpressions.Regex.IsMatch(cccd, @"^(\d{9}|\d{12})$"))
            {
                MessageBox.Show("CCCD/CMND không hợp lệ (phải là 9 hoặc 12 số).", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (System.Linq.Enumerable.Any(ds, x => x.CmndCccd == cccd))
            {
                MessageBox.Show("CCCD/CMND này đã tồn tại trong hệ thống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var kh = new ET_KhachHang 
            { 
                HoTen = txtTen.Text.Trim(), 
                DienThoai = sdt,
                CmndCccd = cccd
            };
            
            var result = BUS_KhachHang.Instance.Them(kh);
            
            if (result.IsSuccess)
            {
                IsSuccess = true;
                ThongTinKhachMoi = kh;
                this.Close();
            }
            else
            {
                MessageBox.Show(result.ErrorMessage ?? "Thêm thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

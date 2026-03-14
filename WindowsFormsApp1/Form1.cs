using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        // Biến lưu trữ form con đang mở
        private Form activeForm;
        private Button currentButton;
        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close(); // Đóng form cũ
            }

            ActivateButton(btnSender); // Đổi màu nút được chọn
            activeForm = childForm;

            // Cấu hình form con
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            // Thêm vào panel
            this.pnlDesktop.Controls.Add(childForm);
            this.pnlDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

            // Đổi tiêu đề phía trên
            lblTitle.Text = childForm.Text.ToUpper();
        }

        // Hàm đổi màu nút menu khi được chọn
        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton(); // Trả lại màu cũ cho nút trước đó
                    currentButton = (Button)btnSender;
                    
                    // Màu khi được chọn (Sáng hơn màu nền sidebar một chút)
                    currentButton.BackColor = Color.FromArgb(51, 50, 97); 
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold);
                    
                    // Thanh tiêu đề giữ màu Teal hiện đại
                    pnlTitleBar.BackColor = Color.FromArgb(0, 150, 136); 
                    pnlLogo.BackColor = Color.FromArgb(39, 39, 58);
                }
            }
        }
        // Hàm trả lại màu mặc định cho nút
        private void DisableButton()
        {
            foreach (Control previousBtn in pnlMenu.Controls)
            {
                if (previousBtn is Button)
                {
                    // Trả về màu nền Dark Blue của Sidebar
                    previousBtn.BackColor = Color.FromArgb(31, 30, 68); 
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 12F);
                }
            }
        }
        public Form1()
        {
            InitializeComponent();
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnSach_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmQuanLyVe(), sender);
        }

        private void btnLinhVuc_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmKhuVuc(), sender);
        }
<<<<<<< Updated upstream
=======

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmTroChoi(), sender);
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmQuanLyTaiKhoan(), sender);
        }

        private void HideAllMenuButtons()
        {
            btnSach.Visible = false;
            btnLinhVuc.Visible = false;
            btnKhachHang.Visible = false;
            btnTaiKhoan.Visible = false;
            btnDangXuat.Visible = false;
        }

        private void menuDanhMuc_Click(object sender, EventArgs e)
        {
            HideAllMenuButtons();
            btnSach.Visible = true;
            btnLinhVuc.Visible = true;
            btnKhachHang.Visible = true;
            lblTitle.Text = "DANH MỤC QUẢN LÝ";
        }

        private void menuBaoCao_Click(object sender, EventArgs e)
        {
            HideAllMenuButtons();
            lblTitle.Text = "BÁO CÁO THỐNG KÊ";
        }

        private void menuHeThong_Click(object sender, EventArgs e)
        {
            HideAllMenuButtons();
            btnTaiKhoan.Visible = true;
            btnDangXuat.Visible = true;
            lblTitle.Text = "THIẾT LẬP HỆ THỐNG";
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có muốn đăng xuất không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                if (this.Owner != null)
                {
                    this.Owner.Show(); // Hiện lại form Login
                    this.Tag = null;   // Xóa session
                    this.Close();      // Đóng form hiện tại
                }
            }
        }

        // Đảm bảo tắt hẳn ứng dụng khi bấm nút X đỏ của Form1
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.UserClosing && this.Owner != null && this.Owner.Visible == false)
            {
                Application.Exit();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (this.Tag is ET_TaiKhoan tk)
            {
                lblUserInfo.Text = $"Chào: {tk.TenDangNhap}";
                lblUserRole.Text = $"Quyền: {tk.VaiTro}";

                // Mặc định hiển thị Danh mục khi mới vào
                menuDanhMuc_Click(null, null);

                if (tk.VaiTro != "Admin")
                {
                    menuHeThong.Visible = false;
                }
            }
        }

>>>>>>> Stashed changes
    }
}

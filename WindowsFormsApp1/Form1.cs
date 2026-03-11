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
                    currentButton.BackColor = Color.DarkTurquoise; // Màu xanh nổi bật
                    currentButton.ForeColor = Color.Black;
                    currentButton.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    pnlTitleBar.BackColor = Color.DarkTurquoise; // Đồng bộ màu thanh tiêu đề
                    pnlLogo.BackColor = Color.DarkTurquoise;
                }
            }
        }
        // Hàm trả lại màu mặc định cho nút
        private void DisableButton()
        {
            foreach (Control previousBtn in pnlMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.MediumTurquoise;
                    previousBtn.ForeColor = Color.Black;
                    previousBtn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
    }
}

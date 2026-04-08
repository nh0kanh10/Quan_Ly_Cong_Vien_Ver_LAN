    using DAL;
    using QRCoder;
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
        public partial class frmThanhToan : Form
        {

            public string PhuongThuc { get; set; }
            public decimal soTien;
            public frmThanhToan()
            {
                InitializeComponent();
                picLogo.Parent = guna2PictureBox1; // logo nằm trong QR

                picLogo.BackColor = Color.Transparent;

                picLogo.Size = new Size(30, 30); // chỉnh size logo

                
            }

            private void frmThanhToan_Load(object sender, EventArgs e)
            {
                rdoTienMat.Checked = true; // Mặc định chọn tiền mặt
                txtTongTien.Text = soTien.ToString("N0") + " VNĐ";
            }
        private void TaoQR(string loai)
        {
            decimal tien = soTien;

            if (tien <= 0)
            {
                MessageBox.Show("Số tiền không hợp lệ!");
                return;
            }
           
            string url = "";

            if (loai == "CK")
            {
                url = $"https://img.vietqr.io/image/VCB-123456789-compact2.png?amount={tien}&addInfo=ThanhToan";
            }
            else if (loai == "MOMO")
            {
               
                url = $"https://img.vietqr.io/image/MB-123456789-compact2.png?amount={tien}&addInfo=MOMO";

                picLogo.Image = Properties.Resources.MOMO_Logo_App_6262c3743a290ef02396a24ea2b66c35;
            }
            else if (loai == "ZALO")
            {
                url = $"https://img.vietqr.io/image/TPB-123456789-compact2.png?amount={tien}&addInfo=ZALOPAY";
                picLogo.Image = Properties.Resources.Icon_of_Zalo_svg;
            }
            else if (loai == "SHOPEE")
            {
                url = $"https://img.vietqr.io/image/ACB-123456789-compact2.png?amount={tien}&addInfo=SHOPEEPAY";
                picLogo.Image = Properties.Resources.images;
            }

            try
            {
                if (string.IsNullOrEmpty(url))
                {
                    guna2PictureBox1.Image = null;
                    guna2PictureBox1.Visible = false;
                }
                else
                {
                    guna2PictureBox1.Visible = true;
                    guna2PictureBox1.Load(url);
                    int offsetY = -10; // chỉnh số này

                    picLogo.Location = new Point(
                        (guna2PictureBox1.Width - picLogo.Width) / 2,
                        (guna2PictureBox1.Height - picLogo.Height) / 2 + offsetY);
                }
            }
            catch
            {
                MessageBox.Show("Không tải được mã QR (lỗi mạng)");
            }
        }

        private void rdoTienMat_CheckedChanged(object sender, EventArgs e)
            {
                if (rdoTienMat.Checked)
                {
                    guna2PictureBox1.Image = null;
                    guna2PictureBox1.Visible = false;
                }
            }

            private void rdoCK_CheckedChanged(object sender, EventArgs e)
            {
                if (rdoCK.Checked)
                {
                    TaoQR("CK");
                }
            }

            private void rdoMomo_CheckedChanged(object sender, EventArgs e)
            {
                if (rdoMomo.Checked)
                {
                    TaoQR("MOMO");
                }
            }

            private void rdoZalo_CheckedChanged(object sender, EventArgs e)
            {
                if (rdoZalo.Checked)
                {
                    TaoQR("ZALO");
                }
            }

            private void rdoShopee_CheckedChanged(object sender, EventArgs e)
            {
                if (rdoShopee.Checked)
                {
                    TaoQR("SHOPEE");
                }
            }

            private void btnXacNhan_Click(object sender, EventArgs e)
            {
                if (!rdoTienMat.Checked && !rdoCK.Checked &&
                !rdoMomo.Checked && !rdoZalo.Checked && !rdoShopee.Checked)
                {
                    MessageBox.Show("Vui lòng chọn phương thức thanh toán!");
                    return;
                }

                if (rdoTienMat.Checked) PhuongThuc = "TienMat";
                else if (rdoCK.Checked) PhuongThuc = "ChuyenKhoan";
                else if (rdoMomo.Checked) PhuongThuc = "Momo";
                else if (rdoZalo.Checked) PhuongThuc = "ZaloPay";
                else if (rdoShopee.Checked) PhuongThuc = "ShopeePay";

                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        private void picLogo_Click(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show($"Bạn có chắc chắn muốn thoát không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                this.Close();

            }
        }
    }
    }

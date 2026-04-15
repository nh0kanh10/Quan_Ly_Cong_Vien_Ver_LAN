using System;
using FontAwesome.Sharp;

using System.Drawing;
using System.Windows.Forms;
using BUS;
using ET;

namespace GUI
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            InitIcons();
            ThemeManager.ApplyTheme(this);

            lblTitle.Font = new Font("Segoe UI", 24f, FontStyle.Bold);
            lblBranding.Font = new Font("Segoe UI", 16f, FontStyle.Bold);
            
            Color bkgColor = Color.FromArgb(239, 219, 203); 
            pnlSideLogo.BackColor = bkgColor;
            pnlSideLogo.FillColor = bkgColor; 
            picLogo.BackColor = bkgColor;
            lblBranding.BackColor = bkgColor;
            lblBranding.ForeColor = Color.FromArgb(30, 41, 59); 
            lblTitle.ForeColor = ThemeManager.PrimaryColor;

            picLogo.Image = IconHelper.LoadImage("TDC.png");
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void InitIcons()
        {
            btnDangNhap.Image = IconHelper.GetBitmap(IconChar.RightToBracket, Color.White, 20);
            btnThoat.Image = IconHelper.GetBitmap(IconChar.PowerOff, Color.White, 20);
            iconUser.Image = IconHelper.GetBitmap(IconChar.User, Color.FromArgb(31, 30, 68), 20);
            iconPass.Image = IconHelper.GetBitmap(IconChar.Lock, Color.FromArgb(31, 30, 68), 20);
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            try 
            {
                string user = txtUser.Text.Trim();
                string pass = txtPass.Text; 

                ET_NhanVien tk = BUS_NhanVien.Instance.DangNhap(user, pass);
                if (tk != null)
                {
                    SessionManager.CurrentUser = tk;
                    var (idKhuVuc, tenKhuVuc) = BUS_LichLamViec.Instance.GetKhuVucHienTai(tk.Id);
                    SessionManager.CurrentIdKhuVuc = idKhuVuc;
                    SessionManager.CurrentTenKhuVuc = !string.IsNullOrEmpty(tenKhuVuc) ? tenKhuVuc : "Trụ Sở Chính";

                    try { DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Đăng nhập", "Đang tải hệ thống, vui lòng chờ..."); } catch { }
                    
                    Form1 main = new Form1();
                    main.Tag = tk; 
                    main.Owner = this; 

                    main.Opacity = 0; 
                    main.Show();
                    
                    Application.DoEvents();

                    this.Hide();
                    main.Opacity = 1;
                    
                    try { DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm(); } catch { }
                }
                else
                {
                    TDCMessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                TDCMessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible)
            {
                txtUser.Text = "";
                txtPass.Text = "";
                txtUser.Focus();
            }
        }
    }
}


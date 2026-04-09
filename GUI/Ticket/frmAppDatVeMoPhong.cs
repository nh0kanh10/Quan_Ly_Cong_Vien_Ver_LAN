using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using FontAwesome.Sharp;
using ET;
using BUS;

namespace GUI
{
    public partial class frmAppDatVeMoPhong : Form, IBaseForm
    {
        public frmAppDatVeMoPhong()
        {
            InitializeComponent();

            InitIcons();
            ApplyStyles();
            ApplyPermissions();

            cboRole.Items.AddRange(new object[] { "Khách vãng lai (Guest)", "Hội viên (Member)", "Trưởng đoàn (Group Leader)" });
            cboRole.SelectedIndex = 0;

            this.Load += (s, e) => LoadData();
            cboRole.SelectedIndexChanged += (s, e) => LoadData();
        }

        public void ApplyPermissions()
        {
            var tk = ET.SessionManager.CurrentUser;
            if (tk == null) return;

            if (!BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "VIEW_TICKET_SIMULATION"))
            {
                this.Enabled = false;
                return;
            }

            // Chỉ quản lý mới được đổi vai trò mô phỏng (ví dụ vậy)
            cboRole.Enabled = BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "MANAGE_TICKET_SIMULATION");
        }

        public void ApplyStyles()
        {
            ThemeManager.ApplyTheme(this);
        }

        public void InitIcons()
        {
            // Simulation UI uses dynamic icons in cards
        }

        public void LoadData()
        {
            BindUseCases();
        }

        private void BindUseCases()
        {
            pnlFeatures.Controls.Clear();
            var role = cboRole.SelectedIndex;

            if (role == 0) // Guest
            {
                AddFeatureCard(IconChar.TicketAlt, "Đặt vé cơ bản", "Chọn loại vé, số lượng và đặt vé tham quan Đại Nam.", ThemeManager.PrimaryColor);
                AddFeatureCard(IconChar.MoneyBillWave, "Thanh toán tiền mặt / chuyển khoản", "Hỗ trợ thanh toán bằng tiền mặt tại quầy hoặc QR chuyển khoản.", ThemeManager.SuccessColor);
                AddFeatureCard(IconChar.Search, "Tra cứu mã đơn hàng", "Nhập mã đơn để xem trạng thái, chi tiết vé đã đặt.", ThemeManager.WarningColor);
            }
            else if (role == 1) // Member
            {
                AddFeatureCard(IconChar.TicketAlt, "Đặt vé + Áp ưu đãi", "Hội viên được hưởng giá ưu đãi và áp dụng mã khuyến mãi.", ThemeManager.PrimaryColor);
                AddFeatureCard(IconChar.CreditCard, "Thanh toán bằng ví RFID", "Quẹt thẻ RFID để trừ tiền từ ví điện tử cá nhân.", ThemeManager.SuccessColor);
                AddFeatureCard(IconChar.History, "Xem lịch sử đơn và số dư", "Tra cứu toàn bộ đơn hàng đã đặt, số dư ví còn lại.", ThemeManager.WarningColor);
                AddFeatureCard(IconChar.Gift, "Ưu đãi riêng hội viên", "Nhận voucher sinh nhật, combo giảm giá đặc biệt.", Color.FromArgb(139, 92, 246));
            }
            else // Group Leader
            {
                AddFeatureCard(IconChar.Users, "Đặt đoàn nhiều vé", "Đặt trọn gói cho cả đoàn, tự động tính giá theo số lượng.", ThemeManager.PrimaryColor);
                AddFeatureCard(IconChar.Tags, "Gán ưu đãi theo đoàn", "Áp mã khuyến mãi đoàn, giảm giá theo bậc số lượng.", ThemeManager.SuccessColor);
                AddFeatureCard(IconChar.ListAlt, "Theo dõi danh sách thành viên đoàn", "Quản lý danh sách, check-in từng thành viên trong đoàn.", ThemeManager.WarningColor);
                AddFeatureCard(IconChar.FileInvoiceDollar, "Xuất hóa đơn tổng hợp", "In hóa đơn VAT cho toàn bộ đoàn khách.", Color.FromArgb(139, 92, 246));
            }
        }

        private void AddFeatureCard(IconChar icon, string title, string desc, Color accentColor)
        {
            Guna2Panel card = new Guna2Panel();
            card.Size = new Size(pnlFeatures.Width - 40, 80);
            card.BorderRadius = 12;
            card.FillColor = Color.FromArgb(248, 250, 252);
            card.BorderColor = Color.FromArgb(226, 232, 240);
            card.BorderThickness = 1;
            card.Margin = new Padding(5, 5, 5, 5);

            // Icon
            PictureBox pb = new PictureBox();
            pb.Image = IconHelper.GetBitmap(icon, accentColor, 28);
            pb.Size = new Size(45, 45);
            pb.SizeMode = PictureBoxSizeMode.CenterImage;
            pb.BackColor = Color.Transparent;
            pb.Location = new Point(15, 18);
            card.Controls.Add(pb);

            // Title
            Label lblT = new Label();
            lblT.Text = title;
            lblT.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblT.ForeColor = ThemeManager.TextPrimaryColor;
            lblT.AutoSize = true;
            lblT.Location = new Point(70, 15);
            lblT.BackColor = Color.Transparent;
            card.Controls.Add(lblT);

            // Description
            Label lblD = new Label();
            lblD.Text = desc;
            lblD.Font = new Font("Segoe UI", 9F);
            lblD.ForeColor = ThemeManager.TextSecondaryColor;
            lblD.AutoSize = true;
            lblD.Location = new Point(70, 42);
            lblD.BackColor = Color.Transparent;
            card.Controls.Add(lblD);

            // Accent bar
            Guna2Panel bar = new Guna2Panel();
            bar.Size = new Size(4, 50);
            bar.FillColor = accentColor;
            bar.BorderRadius = 2;
            bar.Location = new Point(4, 15);
            card.Controls.Add(bar);

            pnlFeatures.Controls.Add(card);
        }
    }
}



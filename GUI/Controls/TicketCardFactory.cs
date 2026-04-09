using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using Guna.UI2.WinForms;

namespace GUI
{
    /// <summary>
    /// Factory tạo và style Ticket Card dùng cho frmPhatVe.
    /// Tách riêng khỏi Designer để dễ bảo trì, tái sử dụng.
    /// </summary>
    public static class TicketCardFactory
    {
        // ════════════════════════════════════════
        // CARD COLOR PALETTE
        // ════════════════════════════════════════

        // Chờ in (Amber)
        public static readonly Color CardAmberFill = Color.FromArgb(255, 251, 235);
        public static readonly Color CardAmberBorder = Color.FromArgb(252, 211, 77);
        public static readonly Color BadgeAmberFill = Color.FromArgb(254, 243, 199);
        public static readonly Color BadgeAmberText = Color.FromArgb(146, 64, 14);

        // Đã nạp RFID (Blue)
        public static readonly Color CardBlueFill = Color.FromArgb(239, 246, 255);
        public static readonly Color CardBlueBorder = Color.FromArgb(147, 197, 253);
        public static readonly Color BadgeBlueFill = Color.FromArgb(219, 234, 254);
        public static readonly Color BadgeBlueText = Color.FromArgb(30, 64, 175);

        // Hoàn tất (Green)
        public static readonly Color CardGreenFill = Color.FromArgb(236, 253, 245);
        public static readonly Color CardGreenBorder = Color.FromArgb(52, 211, 153);
        public static readonly Color BadgeGreenFill = Color.FromArgb(209, 250, 229);
        public static readonly Color BadgeGreenText = Color.FromArgb(6, 95, 70);

        private static readonly Color SeparatorColor = Color.FromArgb(203, 213, 225);

        // ════════════════════════════════════════
        // CARD DIMENSIONS
        // ════════════════════════════════════════

        public const int CardWidth = 200;
        public const int CardHeight = 220;
        private const int CardRadius = 15;
        private const int CardMargin = 10;
        private const int IconSize = 48;
        public const int IconRenderSize = 36;
        private const int BadgeWidth = 160;
        private const int BadgeHeight = 32;

        // ════════════════════════════════════════
        // PRINT FONTS & DIMENSIONS
        // ════════════════════════════════════════

        public static readonly Font PrintTitleFont = new Font("Segoe UI", 16, FontStyle.Bold);
        public static readonly Font PrintSubFont = new Font("Segoe UI", 10);
        public static readonly Font PrintNameFont = new Font("Segoe UI", 11, FontStyle.Bold);
        public static readonly Font PrintCodeFont = new Font("Consolas", 14, FontStyle.Bold);
        public static readonly Font PrintLuotFont = new Font("Segoe UI", 9);
        public static readonly Font PrintFooterFont = new Font("Segoe UI", 8, FontStyle.Italic);
        public static readonly Font ReprintTitleFont = new Font("Segoe UI", 14, FontStyle.Bold);
        public static readonly Font ReprintNameFont = new Font("Segoe UI", 13, FontStyle.Bold);
        public static readonly Font ReprintCodeFont = new Font("Consolas", 16, FontStyle.Bold);
        public static readonly Font ReprintInfoFont = new Font("Segoe UI", 10);
        public const int PrintCardW = 250;
        public const int PrintCardH = 150;

        // ════════════════════════════════════════
        // CARD CREATION
        // ════════════════════════════════════════

        /// <summary>
        /// Tạo khung Ticket Card (layout thuần, KHÔNG gán data).
        /// Bên trong có các child control đặt Name sẵn:
        /// "pbIcon", "lblTen", "lblCode", "lblLuot", "badge"
        /// -> Gọi BindData() bên ngoài để gán text/image.
        /// </summary>
        public static Guna2Panel CreateCardFrame(bool isRfid)
        {
            var card = new Guna2Panel();
            card.Size = new Size(CardWidth, CardHeight);
            card.BorderRadius = CardRadius;
            card.Margin = new Padding(CardMargin);
            card.Cursor = Cursors.Hand;
            card.ShadowDecoration.Enabled = true;
            card.ShadowDecoration.Depth = 4;
            card.ShadowDecoration.Color = Color.FromArgb(30, 0, 0, 0);
            card.BorderThickness = 2;
            card.FillColor = isRfid ? CardBlueFill : CardAmberFill;
            card.BorderColor = isRfid ? CardBlueBorder : CardAmberBorder;

            // pbIcon
            var pbIcon = new PictureBox();
            pbIcon.Name = "pbIcon";
            pbIcon.Size = new Size(IconSize, IconSize);
            pbIcon.Location = new Point((CardWidth - IconSize) / 2, 10);
            pbIcon.SizeMode = PictureBoxSizeMode.CenterImage;
            pbIcon.BackColor = Color.Transparent;
            card.Controls.Add(pbIcon);

            // lblTen
            var lblTen = new Label();
            lblTen.Name = "lblTen";
            lblTen.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblTen.ForeColor = ThemeManager.TextPrimaryColor;
            lblTen.BackColor = Color.Transparent;
            lblTen.Location = new Point(10, 62);
            lblTen.Size = new Size(CardWidth - 20, 36);
            lblTen.TextAlign = ContentAlignment.TopCenter;
            card.Controls.Add(lblTen);

            // separator
            var sep = new Label();
            sep.Size = new Size(CardWidth - 40, 1);
            sep.Location = new Point(20, 100);
            sep.BackColor = SeparatorColor;
            card.Controls.Add(sep);

            // lblCode
            var lblCode = new Label();
            lblCode.Name = "lblCode";
            lblCode.Font = new Font("Consolas", 12, FontStyle.Bold);
            lblCode.ForeColor = isRfid ? ThemeManager.PrimaryColor : ThemeManager.WarningColor;
            lblCode.BackColor = Color.Transparent;
            lblCode.Location = new Point(10, 108);
            lblCode.Size = new Size(CardWidth - 20, 25);
            lblCode.TextAlign = ContentAlignment.MiddleCenter;
            card.Controls.Add(lblCode);

            // lblLuot
            var lblLuot = new Label();
            lblLuot.Name = "lblLuot";
            lblLuot.Font = new Font("Segoe UI", 9);
            lblLuot.ForeColor = ThemeManager.TextSecondaryColor;
            lblLuot.BackColor = Color.Transparent;
            lblLuot.Location = new Point(10, 135);
            lblLuot.Size = new Size(CardWidth - 20, 20);
            lblLuot.TextAlign = ContentAlignment.MiddleCenter;
            card.Controls.Add(lblLuot);

            // badge
            var badge = new Guna2Button();
            badge.Name = "badge";
            badge.Size = new Size(BadgeWidth, BadgeHeight);
            badge.Location = new Point((CardWidth - BadgeWidth) / 2, 165);
            badge.BorderRadius = 8;
            badge.Animated = false;
            badge.Cursor = Cursors.Default;
            badge.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            badge.Text = isRfid ? "📿 Đã nạp RFID" : "🔴 Chờ in";
            badge.FillColor = isRfid ? BadgeBlueFill : BadgeAmberFill;
            badge.ForeColor = isRfid ? BadgeBlueText : BadgeAmberText;
            card.Controls.Add(badge);

            return card;
        }

        // ════════════════════════════════════════
        // CARD STYLE HELPERS
        // ════════════════════════════════════════

        /// <summary>
        /// Chuyển card sang trạng thái Hoàn tất (🟢 Emerald)
        /// </summary>
        public static void StyleCompleted(Guna2Panel card)
        {
            card.FillColor = CardGreenFill;
            card.BorderColor = CardGreenBorder;
            card.BorderThickness = 2;

            foreach (Control c in card.Controls)
            {
                if (c is Guna2Button badge && c.Name == "badge")
                {
                    badge.Text = "✅ Hoàn tất";
                    badge.FillColor = BadgeGreenFill;
                    badge.ForeColor = BadgeGreenText;
                }
            }
        }

        /// <summary>
        /// Viền highlight khi được chọn (cho In Lại)
        /// </summary>
        public static void StyleSelected(Guna2Panel card)
        {
            card.BorderColor = ThemeManager.PrimaryColor;
            card.BorderThickness = 3;
        }

        /// <summary>
        /// Bỏ viền highlight, trả về trạng thái mặc định
        /// </summary>
        public static void StyleDeselected(Guna2Panel card, bool isRfid)
        {
            card.BorderColor = isRfid ? CardBlueBorder : CardAmberBorder;
            card.BorderThickness = 2;
        }

        // ════════════════════════════════════════
        // ICON MAPPING
        // ════════════════════════════════════════

        /// <summary>
        /// Map tên dịch vụ -> FontAwesome icon phù hợp
        /// </summary>
        public static IconChar GetServiceIcon(string tenDV)
        {
            if (string.IsNullOrEmpty(tenDV)) return IconChar.Ticket;
            string lower = tenDV.ToLower();
            if (lower.Contains("cổng") || lower.Contains("vào")) return IconChar.DoorOpen;
            if (lower.Contains("đu quay") || lower.Contains("quay")) return IconChar.Sync;
            if (lower.Contains("tàu") || lower.Contains("lửa")) return IconChar.Train;
            if (lower.Contains("nước") || lower.Contains("biển") || lower.Contains("hồ")) return IconChar.Water;
            if (lower.Contains("trượt")) return IconChar.Snowboarding;
            if (lower.Contains("xiếc") || lower.Contains("show")) return IconChar.TheaterMasks;
            if (lower.Contains("zoo") || lower.Contains("thú")) return IconChar.Paw;
            return IconChar.Ticket;
        }
    }
}

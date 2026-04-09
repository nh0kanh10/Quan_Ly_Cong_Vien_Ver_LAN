using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ET;
using FontAwesome.Sharp;

namespace GUI.Customer
{
    public partial class ucCustomerInfo : UserControl
    {
        public ucCustomerInfo()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            
            // Set up labels to hold the icons natively to prevent misalignment
            SetupIcons();
            
            // Custom paint for smooth corners (no Region jaggedness)
            AttachPaint();
        }

        // ══════════════════════════════════════════════════════════════
        //  ICONS — Embedded into Labels
        // ══════════════════════════════════════════════════════════════

        private void SetupIcons()
        {
            Color solidIcon = Color.FromArgb(99, 132, 190);
            Color ghostIcon = Color.FromArgb(100, 116, 139);

            // Left card (Solid)
            SpawnIcon(cardSolid, lblMaKHTitle, IconChar.IdBadge, solidIcon);
            SpawnIcon(cardSolid, lblSdtTitle, IconChar.Phone, solidIcon);
            SpawnIcon(cardSolid, lblEmailTitle, IconChar.Envelope, solidIcon);
            SpawnIcon(cardSolid, lblGioiTinhTitle, IconChar.VenusMars, solidIcon);
            SpawnIcon(cardSolid, lblNgaySinhTitle, IconChar.CalendarDays, solidIcon);
            SpawnIcon(cardSolid, lblCccdTitle, IconChar.AddressCard, solidIcon);

            // Right card (Ghost)
            SpawnIcon(cardGhost, lblDiaChiTitle, IconChar.LocationDot, ghostIcon);
            SpawnIcon(cardGhost, lblNgayDKTitle, IconChar.ClipboardCheck, ghostIcon);
            SpawnIcon(cardGhost, lblGhiChuTitle, IconChar.NoteSticky, ghostIcon);
        }

        private void SpawnIcon(Panel card, Label titleLabel, IconChar icon, Color color)
        {
            if (titleLabel == null) return;

            var pb = new PictureBox();
            pb.Image = IconHelper.GetBitmap(icon, color, 14);
            pb.Size = new Size(16, 16);
            pb.SizeMode = PictureBoxSizeMode.CenterImage;
            
            // Align precisely 20px to the left of the title, and vertically centered with the title text
            int x = titleLabel.Left - 22;
            int y = titleLabel.Top + (titleLabel.Height - pb.Height) / 2;
            
            pb.Location = new Point(x, y);
            pb.BackColor = Color.Transparent;
            
            card.Controls.Add(pb);
            pb.BringToFront();
        }

        // ══════════════════════════════════════════════════════════════
        //  PAINT — Smooth anti-aliased corners & border
        // ══════════════════════════════════════════════════════════════

        private void AttachPaint()
        {
            // Remove regions entirely to prevent aliasing/jagged edges
            cardSolid.Region = null;
            cardGhost.Region = null;

            cardSolid.Paint += (s, e) => PaintSmoothBackground(e.Graphics, cardSolid, false);
            cardGhost.Paint += (s, e) => PaintSmoothBackground(e.Graphics, cardGhost, true);
        }

        private void PaintSmoothBackground(Graphics g, Panel card, bool drawBorder)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int radius = 8;
            Rectangle rect = new Rectangle(0, 0, card.Width - 1, card.Height - 1);
            Color parentBg = this.BackColor; // Color.FromArgb(248, 250, 252)

            // Draw inverse corners to blend flawlessly with parent background
            // This prevents the labels inside from losing their transparent backcolor
            using (var p = new GraphicsPath())
            {
                // Top-Left
                p.AddLine(0, radius, 0, 0); p.AddLine(0, 0, radius, 0);
                p.AddArc(0, 0, radius * 2, radius * 2, 270, -90); p.CloseFigure();
                // Top-Right
                p.StartFigure();
                p.AddLine(rect.Right - radius, 0, rect.Right, 0); p.AddLine(rect.Right, 0, rect.Right, radius);
                p.AddArc(rect.Right - radius * 2, 0, radius * 2, radius * 2, 0, -90); p.CloseFigure();
                // Bottom-Left
                p.StartFigure();
                p.AddLine(0, rect.Bottom - radius, 0, rect.Bottom); p.AddLine(0, rect.Bottom, radius, rect.Bottom);
                p.AddArc(0, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90); p.CloseFigure();
                // Bottom-Right
                p.StartFigure();
                p.AddLine(rect.Right - radius, rect.Bottom, rect.Right, rect.Bottom); p.AddLine(rect.Right, rect.Bottom, rect.Right, rect.Bottom - radius);
                p.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, -90); p.CloseFigure();

                using (var brush = new SolidBrush(parentBg))
                    g.FillPath(brush, p);
            }

            // Draw ghost border if needed
            if (drawBorder)
            {
                using (var path = RoundedRect(rect, radius))
                using (var pen = new Pen(Color.FromArgb(203, 213, 225), 1.5f))
                {
                    g.DrawPath(pen, path);
                }
            }
        }

        private GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            var path = new GraphicsPath();
            int d = radius * 2;
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        // ══════════════════════════════════════════════════════════════
        //  PUBLIC API — Data binding
        // ══════════════════════════════════════════════════════════════

        public void LoadData(ET_KhachHang kh)
        {
            if (kh == null) { ClearAll(); return; }

            valMaKH.Text     = kh.MaCode ?? "—";
            valSdt.Text      = kh.DienThoai ?? "—";
            valEmail.Text    = kh.Email ?? "—";
            valGioiTinh.Text = kh.GioiTinh ?? "—";
            valNgaySinh.Text = kh.NgaySinh.HasValue ? kh.NgaySinh.Value.ToString("dd/MM/yyyy") : "—";
            valCccd.Text     = kh.CmndCccd ?? "—";
            valDiaChi.Text   = !string.IsNullOrEmpty(kh.DiaChi) ? kh.DiaChi : "—";
            valNgayDK.Text   = kh.NgayDangKy.ToString("dd/MM/yyyy HH:mm");
            valGhiChu.Text   = !string.IsNullOrEmpty(kh.GhiChu) ? kh.GhiChu : "Không có ghi chú";
        }

        private void ClearAll()
        {
            valMaKH.Text = valSdt.Text = valEmail.Text = "—";
            valGioiTinh.Text = valNgaySinh.Text = valCccd.Text = "—";
            valDiaChi.Text = valNgayDK.Text = valGhiChu.Text = "—";
        }
    }
}

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GUI
{
    
    public class PremiumMenuRenderer : ToolStripProfessionalRenderer
    {
        // Dynamic proxy — all colors from ThemeManager
        static Color MenuBgStart     => ThemeManager.SidebarLogoColor;
        static Color MenuBgEnd       => ThemeManager.SidebarColor;
        static Color HoverBg         => ThemeManager.SidebarHoverColor;
        static Color ActiveBg        => ThemeManager.ShellAccent;
        static Color ActiveIndicator => ThemeManager.ShellAccent;
        static Color TextNormal      => ThemeManager.ShellTextMuted;
        static Color TextHover       => ThemeManager.ShellTextBright;
        static Color SeparatorColor  => ThemeManager.ShellSeparator;

        private string _activeMenuName = "";

        public PremiumMenuRenderer() : base(new PremiumColorTable()) { }

       
        public void SetActiveMenu(string menuName)
        {
            _activeMenuName = menuName ?? "";
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            if (e.ToolStrip is MenuStrip)
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    e.AffectedBounds, MenuBgStart, MenuBgEnd, LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(brush, e.AffectedBounds);
                }

                using (Pen pen = new Pen(SeparatorColor, 1))
                {
                    e.Graphics.DrawLine(pen, 0, e.AffectedBounds.Height - 1,
                        e.AffectedBounds.Width, e.AffectedBounds.Height - 1);
                }
            }
            else
            {
                base.OnRenderToolStripBackground(e);
            }
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (!(e.ToolStrip is MenuStrip))
            {
                base.OnRenderMenuItemBackground(e);
                return;
            }

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle bounds = new Rectangle(2, 3, e.Item.Width - 4, e.Item.Height - 6);
            bool isActive = !string.IsNullOrEmpty(_activeMenuName) &&
                            e.Item.Name == _activeMenuName;
            bool isHovered = e.Item.Selected || e.Item.Pressed;

            if (isActive)
            {
                using (GraphicsPath path = CreateRoundedRect(bounds, 6))
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(40, ActiveBg)))
                {
                    g.FillPath(brush, path);
                }

                Rectangle indicator = new Rectangle(
                    bounds.X + 8, e.Item.Height - 4,
                    bounds.Width - 16, 3);
                using (GraphicsPath indicatorPath = CreateRoundedRect(indicator, 2))
                using (SolidBrush indicatorBrush = new SolidBrush(ActiveIndicator))
                {
                    g.FillPath(indicatorBrush, indicatorPath);
                }

                e.Item.ForeColor = TextHover;
            }
            else if (isHovered)
            {
                using (GraphicsPath path = CreateRoundedRect(bounds, 6))
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(50, HoverBg)))
                {
                    g.FillPath(brush, path);
                }

                using (GraphicsPath path = CreateRoundedRect(bounds, 6))
                using (Pen pen = new Pen(Color.FromArgb(40, 130, 180, 230), 1))
                {
                    g.DrawPath(pen, path);
                }

                e.Item.ForeColor = TextHover;
            }
            else
            {
                e.Item.ForeColor = TextNormal;
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            if (e.ToolStrip is MenuStrip)
            {
                e.TextFont = new Font("Segoe UI Semibold", 10.5f, FontStyle.Bold);
                e.TextFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
            }
            base.OnRenderItemText(e);
        }

        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
        {
            if (e.ToolStrip is MenuStrip && e.Image != null)
            {
                Rectangle imgRect = new Rectangle(
                    e.ImageRectangle.X - 2,
                    e.ImageRectangle.Y + 1,
                    e.ImageRectangle.Width,
                    e.ImageRectangle.Height);
                e.Graphics.DrawImage(e.Image, imgRect);
                return;
            }
            base.OnRenderItemImage(e);
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            if (e.ToolStrip is MenuStrip) return; 
            base.OnRenderToolStripBorder(e);
        }

        private GraphicsPath CreateRoundedRect(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }

  
    public class PremiumColorTable : ProfessionalColorTable
    {
        public override Color MenuStripGradientBegin => ThemeManager.SidebarLogoColor;
        public override Color MenuStripGradientEnd => ThemeManager.SidebarColor;
        public override Color MenuItemSelected => Color.Transparent;
        public override Color MenuItemSelectedGradientBegin => Color.Transparent;
        public override Color MenuItemSelectedGradientEnd => Color.Transparent;
        public override Color MenuItemPressedGradientBegin => Color.Transparent;
        public override Color MenuItemPressedGradientEnd => Color.Transparent;
        public override Color MenuBorder => ThemeManager.ShellSeparator;
        public override Color MenuItemBorder => Color.Transparent;
        public override Color ImageMarginGradientBegin => ThemeManager.SidebarColor;
        public override Color ImageMarginGradientEnd => ThemeManager.SidebarColor;
    }
}

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace GUI
{
    
    public class SidebarNavItem : Control
    {
        private IconChar _iconChar = IconChar.None;
        private bool _isActive;
        private Color _accentColor = Color.Empty; // Empty = use ThemeManager.ShellAccent

        private float _hoverProg;
        private float _accentProg;
        private bool _isHovered;
        private readonly Timer _anim;

        // Dynamic proxy — đọc từ ThemeManager mỗi lần paint
        static Color BgBase    => ThemeManager.SidebarColor;
        static Color BgHover   => ThemeManager.SidebarHoverColor;
        static Color TxtNorm   => ThemeManager.ShellTextMuted;
        static Color TxtHover  => Color.FromArgb(241, 245, 249);

        const int BarW = 4, IcoSz = 22, IcoPad = 18, TxtPad = 50;

        public SidebarNavItem()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint
                   | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            Height = 50; Cursor = Cursors.Hand;
            Font = new Font("Segoe UI Semibold", 10.5f);
            BackColor = BgBase;

            _anim = new Timer { Interval = 16 };
            _anim.Tick += (s, e) =>
            {
                bool d = false; float sp = 0.12f;
                d |= Step(ref _hoverProg, _isHovered ? 1f : 0f, sp);
                d |= Step(ref _accentProg, _isActive ? 1f : 0f, sp * 1.5f);
                if (d) Invalidate(); else _anim.Stop();
            };
        }

        public IconChar IconChar { get { return _iconChar; } set { _iconChar = value; Invalidate(); } }
        public Color AccentColor
        {
            get { return _accentColor == Color.Empty ? ThemeManager.ShellAccent : _accentColor; }
            set { _accentColor = value; Invalidate(); }
        }
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; _anim.Start(); Invalidate(); }
        }

        protected override void OnMouseEnter(EventArgs e) { _isHovered = true;  _anim.Start(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { _isHovered = false; _anim.Start(); base.OnMouseLeave(e); }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            var r = ClientRectangle;

            if (r.Width < 5 || r.Height < 5) return;

            using (var br = new SolidBrush(BgBase))
                g.FillRectangle(br, r);

            if (_isActive)
            {
                Color leftTint = Color.FromArgb(45, _accentColor);
                Color rightTint = Color.FromArgb(12, _accentColor);
                using (var br = new LinearGradientBrush(r, leftTint, rightTint, LinearGradientMode.Horizontal))
                    g.FillRectangle(br, r);

                // Subtle bottom highlight line
                using (var pen = new Pen(Color.FromArgb(20, _accentColor), 1))
                    g.DrawLine(pen, 0, r.Height - 1, r.Width, r.Height - 1);
            }
            // ── Hover state: lighter fill band ──
            else if (_hoverProg > 0.01f)
            {
                int alpha = (int)(35 * _hoverProg);
                using (var br = new SolidBrush(Color.FromArgb(alpha, 148, 163, 184)))
                    g.FillRectangle(br, BarW + 2, 0, r.Width - BarW - 2, r.Height);
            }

            // ── 3. LEFT ACCENT BAR (animated) ──
            if (_accentProg > 0.01f)
            {
                int barH = (int)(r.Height * 0.65f * _accentProg);
                if (barH >= 2)
                {
                    int barY = (r.Height - barH) / 2;
                    var barRect = new Rectangle(0, barY, BarW, barH);

                    // Gradient accent bar
                    using (var br = new LinearGradientBrush(
                        new Rectangle(0, barY, BarW, barH),
                        Color.FromArgb((int)(200 * _accentProg), _accentColor),
                        Color.FromArgb((int)(255 * _accentProg), _accentColor),
                        LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(br, barRect);
                    }

                    // Glow effect next to bar
                    if (barH >= 3)
                    {
                        using (var br = new LinearGradientBrush(
                            new Rectangle(BarW, barY, 20, barH),
                            Color.FromArgb((int)(30 * _accentProg), _accentColor),
                            Color.FromArgb(0, _accentColor),
                            LinearGradientMode.Horizontal))
                        {
                            g.FillRectangle(br, BarW, barY, 20, barH);
                        }
                    }
                }
            }

            // ── 4. Icon ──
            Color iconClr = _isActive ? Color.White : Blend(TxtNorm, TxtHover, _hoverProg);
            if (_iconChar != IconChar.None)
            {
                var bmp = IconHelper.GetBitmap(_iconChar, iconClr, IcoSz);
                if (bmp != null)
                    g.DrawImage(bmp, IcoPad, (r.Height - IcoSz) / 2, IcoSz, IcoSz);
            }

            // ── 5. Text ──
            Color txtClr = _isActive ? Color.White : Blend(TxtNorm, TxtHover, _hoverProg);
            var font = _isActive
                ? new Font("Segoe UI", 10.5f, FontStyle.Bold)
                : new Font("Segoe UI Semibold", 10.5f);

            var txtRect = new Rectangle(TxtPad, 0, r.Width - TxtPad - 8, r.Height);
            using (var br = new SolidBrush(txtClr))
            {
                var sf = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center,
                    Trimming = StringTrimming.EllipsisCharacter
                };
                g.DrawString(Text, font, br, txtRect, sf);
            }

            // ── 6. Top separator line (subtle) ──
            using (var pen = new Pen(Color.FromArgb(15, 255, 255, 255)))
                g.DrawLine(pen, BarW + 10, 0, r.Width - 10, 0);
        }

        static bool Step(ref float v, float t, float s)
        {
            if (Math.Abs(v - t) < 0.01f) { v = t; return false; }
            v = v < t ? Math.Min(v + s, t) : Math.Max(v - s, t);
            return true;
        }

        static Color Blend(Color a, Color b, float t)
        {
            t = Math.Max(0, Math.Min(1, t));
            return Color.FromArgb(
                (int)(a.A + (b.A - a.A) * t), (int)(a.R + (b.R - a.R) * t),
                (int)(a.G + (b.G - a.G) * t), (int)(a.B + (b.B - a.B) * t));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) { _anim?.Stop(); _anim?.Dispose(); }
            base.Dispose(disposing);
        }
    }
}

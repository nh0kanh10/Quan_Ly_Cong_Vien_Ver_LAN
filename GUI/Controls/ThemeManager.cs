using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using FontAwesome.Sharp;
using System.Drawing.Drawing2D;
using System;
using System.Collections.Generic;
using DevExpress.XtraGrid;

namespace GUI
{
    public static class ThemeManager
    {
        // ══════════════════════════════════════════════════════════════
        //  THEME ENGINE — Multi-theme qua ThemePalette
        //  Tất cả static properties đọc từ _current palette.
        //  Caller cũ (50+ files) KHÔNG cần sửa gì.
        // ══════════════════════════════════════════════════════════════

        private static ThemePalette _current = ThemePalette.CreateSlateClassic();

        // ── Theme Management API ──
        private static readonly Dictionary<string, Func<ThemePalette>> _registry
            = new Dictionary<string, Func<ThemePalette>>
            {
                { "SlateClassic",       ThemePalette.CreateSlateClassic },
                { "ImperialModernity",  ThemePalette.CreateImperialModernity },
            };

        /// <summary>
        /// Đổi theme theo tên. Không tự refresh forms — gọi RefreshAllOpenForms() riêng.
        /// </summary>
        public static void SetTheme(string themeName)
        {
            if (_registry.TryGetValue(themeName, out var factory))
                _current = factory();
        }

        public static string GetCurrentThemeName() => _current.Name;

        public static List<(string Name, string DisplayName)> GetAvailableThemes()
        {
            var list = new List<(string, string)>();
            foreach (var kvp in _registry)
                list.Add((kvp.Key, kvp.Value().DisplayName));
            return list;
        }

        /// <summary>
        /// Anti-Flicker refresh: SuspendLayout -> ApplyTheme -> ResumeLayout trên tất cả forms.
        /// Form1 (shell) xử lý riêng qua RefreshShellTheme().
        /// </summary>
        public static void RefreshAllOpenForms()
        {
            var forms = new List<Form>();
            foreach (Form f in Application.OpenForms)
            {
                if (f != null && !f.IsDisposed && !IsInDesignMode(f))
                    forms.Add(f);
            }

            foreach (Form form in forms)
            {
                form.SuspendLayout();
                form.BackColor = BackgroundColor;
                ApplyStyleToControls(form.Controls);
                form.ResumeLayout(true);
                form.Refresh();
            }
        }

        // ══════════════════════════════════════════════════════════════
        //  PROXY PROPERTIES — Đọc từ _current palette
        //  Giữ nguyên tên cũ -> ZERO breaking changes
        // ══════════════════════════════════════════════════════════════

        // [1] Chữ chính & Tiêu đề
        public static Color TextPrimaryColor   => _current.TextPrimary;
        public static Color TextSecondaryColor  => _current.TextSecondary;

        // [2] Nền tổng thể
        public static Color BackgroundColor     => _current.Background;
        public static Color PanelColor          => _current.Panel;

        // [3] Bề mặt / Khung
        public static Color SurfaceColor        => _current.Surface;

        // [4] Viền
        public static Color BorderColor         => _current.Border;

        // [5] Accent
        public static Color AccentColor         => _current.Accent;

        // ── Semantic aliases ──
        public static Color PrimaryColor        => _current.Primary;
        public static Color PrimaryContainerColor => _current.PrimaryContainer;
        public static Color SecondaryColor      => _current.Secondary;
        public static Color SecondaryContainerColor => _current.SecondaryContainer;
        public static Color SuccessColor        => _current.Success;
        public static Color DangerColor         => _current.Danger;
        public static Color WarningColor        => _current.Warning;

        // ── Shell / Navigation (Dark zone) ──
        public static Color SidebarColor        => _current.ShellDark;
        public static Color SidebarHoverColor   => _current.ShellMedium;
        public static Color SidebarLogoColor    => _current.ShellDarkest;
        public static Color TitleBarColor       => _current.ShellDark;
        public static Color ShellTextMuted      => _current.ShellTextMuted;
        public static Color ShellTextBright     => _current.ShellTextBright;
        public static Color ShellAccent         => _current.ShellAccent;
        public static Color ShellSeparator      => _current.ShellSeparator;

        // ── Grid ──
        public static Color GridHeaderColor       => _current.GridHeader;
        public static Color GridSelectionColor    => _current.GridSelection;
        public static Color GridSelectionForeColor => _current.GridSelectionFore;

        public static Color GradientInactiveCaption => _current.GradientInactive;
        public static Color GradientActiveCaption   => _current.GradientActive;

        // ── Dashboard Cards ──
        public static Color CardBlue1   => _current.CardBlue1;
        public static Color CardBlue2   => _current.CardBlue2;
        public static Color CardGreen1  => _current.CardGreen1;
        public static Color CardGreen2  => _current.CardGreen2;
        public static Color CardOrange1 => _current.CardOrange1;
        public static Color CardOrange2 => _current.CardOrange2;
        public static Color CardViolet1 => _current.CardViolet1;
        public static Color CardViolet2 => _current.CardViolet2;

        public static int BorderRadius = 4;


        // ── Font ──
        public static string PrimaryFontFamily = "Segoe UI Semibold";
        public static string SecondaryFontFamily = "Segoe UI Semibold";


        // -- GHOST BUTTON (OUTLINE) --
        public static void ApplyGhostStyle(Guna.UI2.WinForms.Guna2Button btn, Color themeColor)
        {
            if (btn == null) return;
            btn.FillColor = Color.White;
            btn.ForeColor = themeColor;
            btn.BorderColor = themeColor;
            btn.BorderThickness = 1;
            btn.HoverState.FillColor = themeColor;
            btn.HoverState.ForeColor = Color.White;

            if (btn.Image != null)
            {
                btn.Image = RecolorImage(btn.Image, themeColor);
                btn.HoverState.Image = RecolorImage(btn.Image, Color.White); // Trắng lại khi hover
            }
        }

        private static Image RecolorImage(Image img, Color targetColor)
        {
            if (img == null) return null;
            Bitmap bmp = new Bitmap(img);
            
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color p = bmp.GetPixel(x, y);
                    if (p.A > 0) // Chỉ xử lý các pixel không trong suốt
                    {
                        // Nhuộm pixel bằng cách multiply giá trị gốc với TargetColor
                        int r = (int)((p.R / 255f) * targetColor.R);
                        int g = (int)((p.G / 255f) * targetColor.G);
                        int b = (int)((p.B / 255f) * targetColor.B);
                        
                        bmp.SetPixel(x, y, Color.FromArgb(p.A, r, g, b));
                    }
                }
            }
            return bmp;
        }

        public static bool IsInDesignMode(Control control = null)
        {
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return true;
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.Contains("devenv")) return true;
            
            if (control != null)
            {
                while (control != null)
                {
                    if (control.Site != null && control.Site.DesignMode) return true;
                    control = control.Parent;
                }
            }
            return false;
        }

        public static Font GetFont(float size, FontStyle style = FontStyle.Regular)
        {
            return new Font("Segoe UI Semibold", size, style);
        }



        public static DialogResult ShowAsPopup(Form frm)
        {
            if (frm == null) return DialogResult.Cancel;
            
            // Standardize form basic properties for popup
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowInTaskbar = false;

            // Smart Header Detection
            Control headerPanel = null;
            foreach (Control c in frm.Controls)
            {
                if (c.Name == "pnlCustomPopupTitleBar" || c.Name == "pnlHeader" || c.Name == "pnlTitle")
                {
                    headerPanel = c;
                    break;
                }
            }

            if (headerPanel == null)
            {
                Guna2Panel pnlTitle = new Guna2Panel();
                pnlTitle.Name = "pnlCustomPopupTitleBar";
                pnlTitle.Dock = DockStyle.Top;
                pnlTitle.Height = 40;
                pnlTitle.FillColor = SidebarColor;
                
                Label lbl = new Label();
                lbl.Text = string.IsNullOrEmpty(frm.Text) ? "Quản lý dữ liệu" : frm.Text;
                lbl.ForeColor = Color.White;
                lbl.BackColor = Color.Transparent;
                lbl.Font = GetFont(11f, FontStyle.Bold);
                lbl.Location = new Point(15, 8);
                lbl.AutoSize = true;
                pnlTitle.Controls.Add(lbl);
                
                frm.Controls.Add(pnlTitle);
                headerPanel = pnlTitle;
                frm.Height += 40;
            }

            // Ensure headerPanel is Top and high z-index
            headerPanel.Dock = DockStyle.Top;
            headerPanel.SendToBack();

            // Add/Ensure Close Button
            Guna2ControlBox cbClose = null;
            foreach (Control c in headerPanel.Controls)
                if (c is Guna2ControlBox) { cbClose = (Guna2ControlBox)c; break; }

            if (cbClose == null)
            {
                cbClose = new Guna2ControlBox();
                cbClose.Name = "cbClose";
                cbClose.Dock = DockStyle.Right;
                cbClose.Width = 45;
                cbClose.FillColor = SidebarColor;
                cbClose.BackColor = SidebarColor;
                cbClose.IconColor = Color.White;
                cbClose.PressedColor = Color.FromArgb(239, 68, 68);
                cbClose.HoverState.FillColor = Color.FromArgb(239, 68, 68);
                cbClose.HoverState.IconColor = Color.White;
                headerPanel.Controls.Add(cbClose);
            }
            cbClose.BringToFront();

            // Drag Control setup
            Guna.UI2.WinForms.Guna2DragControl dragControl = new Guna.UI2.WinForms.Guna2DragControl();
            dragControl.TargetControl = headerPanel;
            dragControl.ContainerControl = frm;
            
            Guna.UI2.WinForms.Guna2BorderlessForm borderless = new Guna.UI2.WinForms.Guna2BorderlessForm();
            borderless.ContainerControl = frm;
            borderless.BorderRadius = 4;
            borderless.HasFormShadow = false;
            
            frm.Paint += (s, e) =>
            {
                e.Graphics.DrawRectangle(new Pen(SidebarColor, 2), 1, 1, frm.Width - 2, frm.Height - 2);
            };


            return frm.ShowDialog();
        }

        public static void ApplyTheme(Form form)
        {
            if (form == null || IsInDesignMode(form)) return;
            form.BackColor = BackgroundColor;
            ApplyStyleToControls(form.Controls);
        }
        /// <summary>
        /// Áp dụng theme bằng cách duyệt các control từ mdi thuộc cha
        /// </summary>
        /// <param name="controls"></param>
        /// <param name="parentIsDark"></param>
        private static void ApplyStyleToControls(Control.ControlCollection controls, bool parentIsDark = false)
        {
            if (controls == null) return;
            var list = new System.Collections.Generic.List<Control>();
            foreach (Control c in controls) list.Add(c);
            
            foreach (Control c in list)
            {
                if (c == null) continue;
                if (c is Guna2Button gunaBtn)
                {
                    StyleGunaButton(gunaBtn);
                }
                else if (c is Guna2Panel gunaPnl)
                {
                    if (gunaPnl.FillColor == Color.White || gunaPnl.FillColor == Color.Transparent || gunaPnl.FillColor == SystemColors.Control)
                    {
                        if (gunaPnl.Dock != DockStyle.Left && gunaPnl.Dock != DockStyle.Right)
                        {
                            gunaPnl.FillColor = Color.White;
                        }
                    }
                    gunaPnl.BorderColor = BorderColor; 
                    
                    bool isDarkPanel = parentIsDark;
                    if (gunaPnl.FillColor != Color.Transparent && gunaPnl.FillColor.A > 0)
                    {
                        isDarkPanel = gunaPnl.FillColor.R < 100 && gunaPnl.FillColor.G < 100 && gunaPnl.FillColor.B < 100;
                    }
                    ApplyStyleToControls(gunaPnl.Controls, isDarkPanel);
                }
                else if (c is DevExpress.XtraEditors.SplitContainerControl split)
                {
                    split.Appearance.BackColor = BackgroundColor; 
                    split.Appearance.Options.UseBackColor = true;
                    // Bỏ ép style Flat để dùng skin mặc định (WXI) hiện đại hơn
                    split.LookAndFeel.UseDefaultLookAndFeel = true;
                    
                    ApplyStyleToControls(split.Panel1.Controls, parentIsDark);
                    ApplyStyleToControls(split.Panel2.Controls, parentIsDark);
                }
                else if (c is DevExpress.XtraEditors.BaseEdit edit)
                {
                    edit.Properties.Appearance.Font = GetFont(10f); // Match Guna Font
                    
                    // Disable AutoHeight to force uniform 36px height
                    edit.Properties.AutoHeight = false; 
                    edit.Height = 36; 
                    
                    // We let the WXI Skin handle the border radius and rendering instead of wrapping, preserving the beautiful vector arrows!
                }
                else if (c is Panel pnl)
                {
                    if (pnl.BackColor != Color.Transparent && pnl.Dock == DockStyle.None)
                        pnl.BackColor = PanelColor;
                    
                    bool isDarkPanel = parentIsDark;
                    if (pnl.BackColor != Color.Transparent && pnl.BackColor.A > 0)
                    {
                        isDarkPanel = pnl.BackColor.R < 100 && pnl.BackColor.G < 100 && pnl.BackColor.B < 100;
                    }
                    ApplyStyleToControls(pnl.Controls, isDarkPanel);
                }
                else if (c is Guna.UI2.WinForms.Guna2GroupBox gunaGb)
                {
                    gunaGb.BackColor = Color.Transparent;
                    gunaGb.FillColor = Color.White;
                    gunaGb.BorderColor = BorderColor;
                    gunaGb.BorderRadius = 0;
                    gunaGb.CustomBorderColor = BorderColor;
                    gunaGb.ForeColor = PrimaryColor;
                    gunaGb.Font = GetFont(9.5f, FontStyle.Bold);
                    gunaGb.TextOffset = new Point(0, 5);
                    ApplyStyleToControls(gunaGb.Controls);
                }
                else if (c is DataGridView dgv)
                {
                    StyleDataGridView(dgv);
                }
                else if (c is IconButton btn)
                {
                    StyleIconButton(btn);
                }
                else if (c is Button normalBtn)
                {
                    StyleButton(normalBtn);
                }
                else if (c is TextBox txt)
                {
                    txt.BorderStyle = BorderStyle.FixedSingle;
                    txt.Font = GetFont(10f);
                    if (txt.ReadOnly || !txt.Enabled)
                    {
                        txt.BackColor = BorderColor;     // Slate-300
                        txt.ForeColor = TextSecondaryColor;
                    }
                    else
                    {
                        txt.BackColor = Color.White;
                        txt.ForeColor = TextPrimaryColor; // Slate-700
                    }
                }
                else if (c is RichTextBox rtxt)
                {
                    rtxt.BorderStyle = BorderStyle.None;
                    rtxt.Font = GetFont(10f);
                    rtxt.ForeColor = TextPrimaryColor;
                    rtxt.BackColor = Color.White;
                }
                else if (c is TDCTextBox tdcTxt)
                {
                    if (tdcTxt.ReadOnly)
                    {
                        tdcTxt.BackColor = BorderColor;  // Slate-300
                    }
                    else
                    {
                        tdcTxt.BackColor = Color.White;
                    }
                    ApplyStyleToControls(tdcTxt.Controls);
                }
                else if (c is Guna2TextBox gunaTxt)
                {
                    gunaTxt.BorderRadius = BorderRadius;
                    gunaTxt.BorderThickness = 1;
                    gunaTxt.BorderColor = BorderColor;     // Slate-300
                    gunaTxt.FocusedState.BorderColor = PrimaryColor;
                    gunaTxt.Font = GetFont(9f);
                    gunaTxt.ForeColor = TextPrimaryColor;  // Slate-700
                    gunaTxt.FillColor = Color.White;
                    gunaTxt.DisabledState.ForeColor = TextSecondaryColor;
                    gunaTxt.DisabledState.FillColor = BorderColor;
                }
                else if (c is Guna.UI2.WinForms.Guna2ComboBox gunaCbo)
                {
                    gunaCbo.BorderRadius = BorderRadius;
                    gunaCbo.BorderThickness = 1;
                    gunaCbo.BorderColor = BorderColor;     // Slate-300
                    gunaCbo.FocusedState.BorderColor = PrimaryColor;
                    gunaCbo.Font = GetFont(9f);
                    gunaCbo.ForeColor = TextPrimaryColor;  // Slate-700
                    gunaCbo.BackColor = Color.White;
                    gunaCbo.FillColor = Color.White;
                    gunaCbo.ItemHeight = 28;
                    gunaCbo.DropDownStyle = ComboBoxStyle.DropDownList;
                }
                else if (c is Guna2TabControl gunaTab)
                {
                    StyleGunaTabControl(gunaTab);
                    foreach (TabPage tp in gunaTab.TabPages)
                    {
                        tp.BackColor = Color.White;
                        ApplyStyleToControls(tp.Controls);
                    }
                }
                else if (c is Guna2NumericUpDown gunaNud)
                {
                    StyleGunaNumericUpDown(gunaNud);
                }
                else if (c is NumericUpDown nud)
                {
                    StyleNumericUpDown(nud);
                }
                else if (c is ComboBox cb)
                {
                    cb.FlatStyle = FlatStyle.Flat;
                    cb.Font = GetFont(11f);
                    cb.ForeColor = TextPrimaryColor;
                    if (!cb.Enabled)
                    {
                        cb.BackColor = BorderColor;  // Slate-300
                    }
                }
                else if (c is Label lbl)
                {
                    lbl.ForeColor = parentIsDark ? Color.White : TextPrimaryColor;
                    if (lbl.Font != null)
                    {
                        lbl.Font = GetFont(9f, lbl.Font.Style);
                    }
                }
                else if (c is Guna2DateTimePicker gunaDtp)
                {
                    StyleGunaDateTimePicker(gunaDtp);
                }
                else if (c is DateTimePicker dtp)
                {
                    dtp.Font = GetFont(10f);
                    dtp.MinDate = new DateTime(1753, 1, 1);
                    dtp.Format = DateTimePickerFormat.Custom;
                    dtp.CustomFormat = "dd/MM/yyyy";
                    if (!dtp.Enabled)
                    {
                        dtp.BackColor = BorderColor;       // Slate-300
                        dtp.ForeColor = TextSecondaryColor; // Nhạt
                    }
                    else
                    {
                        dtp.BackColor = Color.White;
                        dtp.ForeColor = TextPrimaryColor;   // Slate-700
                    }
                }
                else if (c is CheckBox chk)
                {
                    chk.Font = GetFont(11f);
                    chk.ForeColor = TextPrimaryColor;
                }
                else if (c is TDCCheckBox tdcChk)
                {
                    tdcChk.Font = GetFont(10f);
                    tdcChk.ForeColor = TextPrimaryColor;
                }
                else if (c is GridControl grid)
                {
                   StyleDevExpressGrid(grid);
                }
                else
                {
                    if (c.HasChildren)
                        ApplyStyleToControls(c.Controls);
                }
                
            }
        }

        public static void StyleDataGridView(DataGridView dgv)
        {
            ApplyGridViewStyle(dgv);
        }

        private static void StyleIconButton(IconButton btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.ForeColor = Color.White;
            btn.Font = GetFont(10f, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.IconColor = Color.White;
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.ImageAlign = ContentAlignment.MiddleCenter;
            btn.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn.AutoEllipsis = false;
            btn.Padding = new Padding(0);
            btn.Margin = new Padding(0);
            
            string text = btn.Text.ToLower();
            if (btn.Height > 40) btn.IconSize = 26;
            else btn.IconSize = 22;
            
            if (text.Contains("xóa"))
            {
                btn.BackColor = DangerColor;
            }
            else if (text.Contains("thêm") || text.Contains("mới") || text.Contains("tạo"))
            {
                btn.BackColor = AccentColor;
                // Nếu Accent = Vàng thẫm, chữ trắng vẫn ổn
            }
            else if (text.Contains("làm mới") || text.Contains("thoát") || text.Contains("hủy"))
            {
                btn.BackColor = SecondaryColor;
            }
            else
            {
                btn.BackColor = PrimaryColor;
            }
        }

        private static void StyleButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.ForeColor = Color.White;
            btn.Font = GetFont(10f, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.BackColor = PrimaryColor;
        }

        private static void DrawRoundedCorners(Control btn, Graphics g)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                int radius = BorderRadius;
                path.AddArc(0, 0, radius, radius, 180, 90);
                path.AddArc(btn.Width - radius, 0, radius, radius, 270, 90);
                path.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90);
                path.AddArc(0, btn.Height - radius, radius, radius, 90, 90);
                path.CloseFigure();
                btn.Region = new Region(path);
            }
        }



        private static void StyleGunaButton(Guna2Button btn)
        {
            // Bỏ qua Menu Gốc (Navigation & Sidebar của Form1)
            var form = btn.FindForm();
            if (form != null && (form.Name == "Form1" || form.Name == "frmMain")) return;
            if (btn.Name.StartsWith("btnNav") || btn.Name.StartsWith("btnToggle") || btn.Name.StartsWith("menu")) return;

            btn.BorderRadius = BorderRadius;
            btn.Font = GetFont(10f, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.Animated = true;

            // ── THIẾT QUÂN LUẬT: Chỉ 2 loại nút ──
            // Mặc định: Ghost Slate-700 (viền xám đậm, chữ xám đậm, nền trắng)
            // Cảnh báo: Ghost Đỏ (viền đỏ, chữ đỏ, nền trắng)
            string text = btn.Text.ToLower();
            Color targetColor = PrimaryColor; // Mặc định bảng chung
            
            // Xoá -> Đỏ
            if (text.Contains("xóa") || text.Contains("hủy") || text.Contains("đóng") || text.Contains("trừ"))
            {
                targetColor = DangerColor; 
            }
            // Thêm / Mới -> Vàng
            else if (text.Contains("thêm") || text.Contains("mới") || text.Contains("tạo"))
            {
                targetColor = AccentColor;
            }

            ApplyGhostStyle(btn, targetColor);
        }

        public static void StyleGunaNumericUpDown(Guna2NumericUpDown nud)
        {
            nud.BorderRadius = BorderRadius;
            nud.Font = GetFont(10f);
            nud.ForeColor = TextPrimaryColor;     // Slate-700 (CẤM dùng Black)
            nud.BorderThickness = 1;
            nud.BorderColor = BorderColor;         // Slate-300
            nud.Height = 36;
            
            nud.UpDownButtonFillColor = BackgroundColor; // Slate-50
            nud.UpDownButtonForeColor = TextPrimaryColor;
            
            nud.FillColor = Color.White;
            nud.ForeColor = TextPrimaryColor;      // Slate-700 (CẤM dùng Black)
            nud.FocusedState.BorderColor = PrimaryColor;
        }

        public static void StyleNumericUpDown(NumericUpDown nud)
        {
            nud.Font = GetFont(10f);
            nud.ForeColor = TextPrimaryColor;
            nud.BackColor = Color.White;
        }

        public static void StyleGunaDateTimePicker(Guna2DateTimePicker dtp)
        {
            dtp.BorderRadius = BorderRadius;
            dtp.Font = GetFont(10f);
            dtp.ForeColor = TextPrimaryColor; // Dùng màu tối cho chữ để dễ đọc
            dtp.FillColor = Color.White;
            dtp.BorderThickness = 1;
            dtp.BorderColor = Color.FromArgb(226, 232, 240);
            
            // Set standard height for modern look
            dtp.Height = 36;
            
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = "dd/MM/yyyy";
            
            // Màu sắc nút chọn ngày
            dtp.CheckedState.FillColor = GradientActiveCaption;
            dtp.CheckedState.ForeColor = TextPrimaryColor;
            
            // Hover state
            dtp.HoverState.BorderColor = GradientInactiveCaption;
        }
        public static void StyleGunaTabControl(Guna2TabControl tab)
        {
            tab.TabButtonIdleState.FillColor = SidebarColor;
            tab.TabButtonIdleState.Font = GetFont(10, FontStyle.Bold);
            tab.TabButtonIdleState.ForeColor = BorderColor;  // Slate-300
            tab.TabButtonIdleState.InnerColor = SidebarColor;

            tab.TabButtonSelectedState.FillColor = SidebarHoverColor;
            tab.TabButtonSelectedState.Font = GetFont(10, FontStyle.Bold);
            tab.TabButtonSelectedState.ForeColor = Color.White;
            tab.TabButtonSelectedState.InnerColor = Color.White; // Thanh Active trắng

            tab.TabButtonHoverState.FillColor = SidebarHoverColor;
            tab.TabButtonHoverState.Font = GetFont(10, FontStyle.Bold);
            tab.TabButtonHoverState.ForeColor = Color.White;
            tab.TabButtonHoverState.InnerColor = SidebarHoverColor;

            tab.TabMenuBackColor = SidebarColor;
        }

        public static void ApplyGridViewStyle(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AllowUserToResizeRows = false;
            dgv.RowHeadersVisible = false;

            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = GridHeaderColor;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = GetFont(10f, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 45;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(0);

            dgv.DefaultCellStyle.SelectionBackColor = GridSelectionColor;
            dgv.DefaultCellStyle.SelectionForeColor = GridSelectionForeColor;
            dgv.DefaultCellStyle.Font = GetFont(10f);
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgv.RowTemplate.Height = 35;

            dgv.DataBindingComplete += (s, e) =>
            {
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (col.ValueType == typeof(DateTime) || col.ValueType == typeof(DateTime?))
                    {
                        col.DefaultCellStyle.Format = "dd/MM/yyyy";
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            };
            
            dgv.AlternatingRowsDefaultCellStyle.BackColor = BackgroundColor; // Slate-50
        }
        public static void StyleDevExpressGrid(DevExpress.XtraGrid.GridControl grid)
        {
            if (grid == null) return;
            var view = grid.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null) return;

            // ══════════════════════════════════════════════════════════
            //  PREMIUM FLAT GRID — 5 MÀU DUY NHẤT
            // ══════════════════════════════════════════════════════════

            // -- General Options --
            view.OptionsView.ShowGroupPanel = false;
            view.OptionsBehavior.Editable = false;
            view.OptionsView.ShowIndicator = false;
            view.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.True;
            view.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.True; // Bật kẻ dọc dễ nhìn theo ý sếp
            view.OptionsView.EnableAppearanceEvenRow = true;

            view.RowHeight = 38;
            view.ColumnPanelRowHeight = 42;

            view.OptionsView.ColumnAutoWidth = true;
            view.OptionsSelection.EnableAppearanceFocusedCell = false;
            view.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;

            view.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            view.Appearance.ViewCaption.ForeColor = TextSecondaryColor;

            // -- HEADER: Slate-700 nền, chữ trắng --
            view.Appearance.HeaderPanel.BackColor = GridHeaderColor;  // Slate-700
            view.Appearance.HeaderPanel.ForeColor = Color.White;
            view.Appearance.HeaderPanel.Font = GetFont(10f, FontStyle.Bold);
            view.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            view.Appearance.HeaderPanel.Options.UseBackColor = true;
            view.Appearance.HeaderPanel.Options.UseForeColor = true;
            view.Appearance.HeaderPanel.Options.UseFont = true;
            view.Appearance.HeaderPanel.Options.UseTextOptions = true;

            // -- Đường kẻ ngang: Slate-300 siêu mờ --
            view.Appearance.HorzLine.BackColor = BorderColor;  // Slate-300
            view.Appearance.HorzLine.Options.UseBackColor = true;
            view.Appearance.VertLine.BackColor = BorderColor;
            view.Appearance.VertLine.Options.UseBackColor = true;

            // Custom draw Header phẳng hoàn toàn
            view.CustomDrawColumnHeader += (s, e) =>
            {
                if (e.Column == null) return;
                e.Cache.FillRectangle(new SolidBrush(GridHeaderColor), e.Bounds);
                e.Appearance.DrawString(e.Cache, e.Info.Caption, e.Info.CaptionRect);

                foreach (DevExpress.Utils.Drawing.DrawElementInfo element in e.Info.InnerElements)
                {
                    if (!element.Visible) continue;
                    DevExpress.Utils.Drawing.ObjectPainter.DrawObject(e.Cache, element.ElementPainter, element.ElementInfo);
                }

                // Phân cách cột bằng Slate-300 mờ
                using (Pen pen = new Pen(Color.FromArgb(60, 203, 213, 225)))
                {
                    e.Graphics.DrawLine(pen, e.Bounds.Right - 1, e.Bounds.Top + 8, e.Bounds.Right - 1, e.Bounds.Bottom - 8);
                }
                e.Handled = true;
            };

            // -- ROW: Nền trắng, chữ Slate-700 --
            view.Appearance.Row.Font = GetFont(10f);
            view.Appearance.Row.ForeColor = TextPrimaryColor;  // Slate-700
            view.Appearance.Row.BackColor = Color.White;
            view.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            view.Appearance.Row.Options.UseFont = true;
            view.Appearance.Row.Options.UseForeColor = true;
            view.Appearance.Row.Options.UseTextOptions = true;

            // -- EVEN ROW: Slate-50 (Phân biệt rất nhẹ) --
            view.Appearance.EvenRow.BackColor = BackgroundColor;  // Slate-50
            view.Appearance.EvenRow.ForeColor = TextPrimaryColor;
            view.Appearance.EvenRow.Options.UseBackColor = true;
            view.Appearance.EvenRow.Options.UseForeColor = true;

            // -- SELECTION: Slate-50 nền + Slate-700 chữ (tinh tế, không lóa) --
            view.Appearance.FocusedRow.BackColor = GridSelectionColor;
            view.Appearance.FocusedRow.ForeColor = GridSelectionForeColor;
            view.Appearance.FocusedRow.Font = GetFont(10f, FontStyle.Bold);
            view.Appearance.FocusedRow.Options.UseBackColor = true;
            view.Appearance.FocusedRow.Options.UseForeColor = true;
            view.Appearance.FocusedRow.Options.UseFont = true;

            view.Appearance.HideSelectionRow.BackColor = GridSelectionColor;
            view.Appearance.HideSelectionRow.ForeColor = GridSelectionForeColor;
            view.Appearance.HideSelectionRow.Options.UseBackColor = true;

            // -- Find Panel --
            view.OptionsFind.ShowCloseButton = false;
            view.OptionsFind.ShowFindButton = false;
            view.OptionsFind.ShowClearButton = false;

            // -- Filter Panel: Slate-50 --
            view.Appearance.FilterPanel.BackColor = BackgroundColor;
            view.Appearance.FilterPanel.Options.UseBackColor = true;
            view.Appearance.FilterPanel.ForeColor = TextPrimaryColor;
            view.Appearance.FilterPanel.Options.UseForeColor = true;
            view.Appearance.FilterPanel.Font = GetFont(10f);
            view.Appearance.FilterPanel.Options.UseFont = true;

            view.Appearance.FilterCloseButton.BackColor = BackgroundColor;
            view.Appearance.FilterCloseButton.Options.UseBackColor = true;

            grid.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True;

            CustomizeFindPanel(grid);
        }

        public static void CustomizeFindPanel(DevExpress.XtraGrid.GridControl grid)
        {
            grid.ForceInitialize();
            foreach (Control c in grid.Controls)
            {
                if (c.GetType().Name == "FindControl")
                {
                    c.BackColor = Color.White;
                    ApplyFindControlStyle(c);
                }
            }
        }

        private static void ApplyFindControlStyle(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                if (child.GetType().Name == "LayoutControl")
                {
                    child.BackColor = Color.White;
                }
                else if (child is DevExpress.XtraEditors.TextEdit txt)
                {
                    txt.BackColor = Color.White;
                    txt.ForeColor = TextPrimaryColor;
                    txt.Font = GetFont(11f);
                    txt.Properties.AutoHeight = false;
                    txt.Height = 36;
                    txt.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
                    txt.Properties.Appearance.BorderColor = BorderColor; // Slate-300
                    txt.Properties.AppearanceFocused.BorderColor = PrimaryColor;
                    
                    if (txt is DevExpress.XtraEditors.MRUEdit mru)
                    {
                        mru.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
                        mru.Properties.Buttons.Clear();
                    }
                }
                else if (child is DevExpress.XtraEditors.SimpleButton btn)
                {
                    // Ẩn luôn các nút Find/Clear thô kệch
                    btn.Visible = false;
                }
                
                if (child.HasChildren)
                {
                    ApplyFindControlStyle(child);
                }
            }
        }
        public static void StyleSearchLookUpEdit(DevExpress.XtraEditors.SearchLookUpEdit slk, string[] visibleColumns = null, string[] captions = null)
        {
            if (slk == null) return;
            slk.Properties.Appearance.Font = GetFont(10f);
            slk.Properties.AutoHeight = false;
            slk.Height = 36;

            slk.Properties.NullText = "";
            slk.Properties.NullValuePrompt = "--- Chọn ---";
            
            var view = slk.Properties.PopupView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null) return;

            // Cấu hình chung cho Toolbar tìm kiếm thông qua View nội bộ
            view.OptionsFind.AlwaysVisible = true;
            view.OptionsFind.ShowClearButton = false;
            view.OptionsFind.ShowFindButton = false;
            slk.Properties.ShowAddNewButton = false;
            
            // Đợi khi popup mở ra để tinh chỉnh GridControl bên trong
            slk.Popup += (s, e) => {
                var popupControl = (slk as DevExpress.Utils.Win.IPopupControl).PopupWindow;
                if (popupControl != null)
                {
                    // Tùy biến Grid bên trong popup
                    StyleDevExpressGrid(view.GridControl);
                    view.OptionsView.ShowIndicator = false; // Ẩn cột thừa bên trái
                    
                    // Tùy biến Find Panel
                    foreach (Control c in popupControl.Controls)
                    {
                        if (c.GetType().Name.Contains("FindControl") || c.HasChildren)
                            ApplyFindControlStyle(c);
                    }
                }

                if (visibleColumns != null)
                {
                    for (int i = 0; i < view.Columns.Count; i++)
                    {
                        var col = view.Columns[i];
                        bool found = false;
                        for (int j = 0; j < visibleColumns.Length; j++)
                        {
                            if (col.FieldName == visibleColumns[j])
                            {
                                found = true;
                                if (captions != null && j < captions.Length) col.Caption = captions[j];
                                col.VisibleIndex = j;
                                break;
                            }
                        }
                        col.Visible = found;
                    }
                }
            };
        }
    }
}









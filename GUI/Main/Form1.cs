using ET;
using BUS;
using GUI.AI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace GUI
{
    public partial class Form1 : Form
    {
        // ══════════════════════════════════════════════════════════════
        //  SINGLETON + CORE STATE
        // ══════════════════════════════════════════════════════════════
        public static Form1 Instance { get; private set; }
        public Panel PnlDesktop { get { return pnlDesktop; } }
        private Form activeForm;
        private readonly Dictionary<Type, Form> _formCache = new Dictionary<Type, Form>();

        // ── AI Chatbox ──
        private AIChatPanel _chatPanel;
        private Guna.UI2.WinForms.Guna2CircleButton _btnAIChatBubble;
        private readonly BUS_QuyenHan _permissionBus = BUS_QuyenHan.Instance;

        // Track active category + sub-item
        private int _activeCatIndex = -1;
        private Guna.UI2.WinForms.Guna2Button _activeSubBtn;
        private readonly Dictionary<int, string> _lastSubPerCat = new Dictionary<int, string>();

        // Window drag support (borderless form)
        private bool _dragging;
        private Point _dragStart;

        // Double-buffering for flicker-free rendering
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, bool wParam, int lParam);
        private const int WM_SETREDRAW = 11;
        private void SuspendDrawing(Control c) { SendMessage(c.Handle, WM_SETREDRAW, false, 0); }
        private void ResumeDrawing(Control c) { SendMessage(c.Handle, WM_SETREDRAW, true, 0); c.Refresh(); }

        // ══════════════════════════════════════════════════════════════
        //  PERMISSION HELPERS
        // ══════════════════════════════════════════════════════════════
        private bool Can(string maQuyen)
        {
            var tk = SessionManager.CurrentUser;
            if (tk == null) return false;
            return _permissionBus.HasPermission(tk.IdVaiTro, maQuyen);
        }

        private bool CanAny(params string[] keys)
        {
            if (keys == null || keys.Length == 0) return false;
            return keys.Any(k => Can(k));
        }

        // ══════════════════════════════════════════════════════════════
        //  COLOR PALETTE — Proxy từ ThemeManager (dynamic theme)
        // ══════════════════════════════════════════════════════════════
        static Color NavDark     => ThemeManager.SidebarLogoColor;
        static Color SubDark     => ThemeManager.SidebarColor;
        static Color TxtInactive => ThemeManager.ShellTextMuted;
        static Color TxtActive   => ThemeManager.ShellTextBright;
        static Color AccentBlue  => ThemeManager.ShellAccent;
        static Color PillActive  => ThemeManager.SidebarHoverColor;
        static readonly Color DotCached   = Color.FromArgb(74, 222, 128);  // Green-400 (universal)

        // ══════════════════════════════════════════════════════════════
        //  MENU DEFINITION — SINGLE SOURCE OF TRUTH
        // ══════════════════════════════════════════════════════════════
        private struct MenuDef
        {
            public string Text;
            public IconChar Icon;
            public string[] Permissions; // CanAny — null = always visible
            public Type FormType;        // null = special action (logout, exit, dashboard)
            public Action SpecialAction; // For logout, exit, etc.
        }

        private struct CategoryDef
        {
            public string Text;
            public IconChar Icon;
            public string[] Permissions; // CanAny to see this category
            public MenuDef[] Items;
        }

        private CategoryDef[] _categories;

        private void BuildMenuDefinitions()
        {
            _categories = new CategoryDef[]
            {
                // ── TIỀN SẢNH ──
                new CategoryDef
                {
                    Text = "TIỀN SẢNH",
                    Icon = IconChar.CashRegister,
                    Permissions = new[] { "VIEW_POS", "VIEW_DONHANG", "VIEW_RFID_TOPUP", "VIEW_ACCESS_CONTROL", "VIEW_HOTEL" },
                    Items = new MenuDef[]
                    {
                        new MenuDef { Text = "Bán Hàng",       Icon = IconChar.CartArrowDown, Permissions = new[] { "VIEW_POS" },              FormType = typeof(frmBanHang) },
                        new MenuDef { Text = "Vé Đoàn",        Icon = IconChar.TicketAlt,     Permissions = new[] { "VIEW_POS" },              FormType = null,
                            SpecialAction = () => { using (var f = new POS.frmXuatVeDoan()) { ThemeManager.ShowAsPopup(f); } }
                        },
                        new MenuDef { Text = "Soát Vé",        Icon = IconChar.Barcode,       Permissions = new[] { "VIEW_ACCESS_CONTROL" },   FormType = typeof(frmKiemSoatVe) },
                        new MenuDef { Text = "Đặt Phòng",      Icon = IconChar.Hotel,         Permissions = new[] { "VIEW_HOTEL" },            FormType = typeof(frmDatPhong) },
                        new MenuDef { Text = "Đặt Bàn",        Icon = IconChar.Chair,         Permissions = new[] { "VIEW_HOTEL" },            FormType = typeof(frmDatBan) },
                        new MenuDef { Text = "Cho Thuê",       Icon = IconChar.Retweet,       Permissions = new[] { "VIEW_POS" },              FormType = typeof(frmThueDo) },
                        new MenuDef { Text = "Lễ Tân Đoàn",    Icon = IconChar.PeopleArrows,  Permissions = new[] { "VIEW_POS" },              FormType = typeof(frmQuayVe_LeTan) },
                        new MenuDef { Text = "Giữ Xe",         Icon = IconChar.Car,           Permissions = new[] { "VIEW_POS" },              FormType = typeof(frmGuiXe) },
                        new MenuDef { Text = "Nạp Tiền",       Icon = IconChar.Wallet,        Permissions = new[] { "VIEW_RFID_TOPUP" },       FormType = null,
                            SpecialAction = () => { using (var f = new frmQuayNapTien()) { ThemeManager.ShowAsPopup(f); } }
                        },
                    }
                },
                // ── QUẢN TRỊ ──
                new CategoryDef
                {
                    Text = "QUẢN TRỊ",
                    Icon = IconChar.Database,
                    Permissions = new[] { "VIEW_PRICE", "VIEW_INVENTORY", "VIEW_REGION", "VIEW_CUSTOMER", "VIEW_RFID", "VIEW_WALLET" },
                    Items = new MenuDef[]
                    {
                        new MenuDef { Text = "Sản Phẩm",      Icon = IconChar.BoxOpen,        Permissions = new[] { "VIEW_PRICE" },     FormType = typeof(frmSanPham) },
                        new MenuDef { Text = "Combo",          Icon = IconChar.Cubes,          Permissions = new[] { "VIEW_PRICE" },     FormType = typeof(frmCombo) },
                        new MenuDef { Text = "Khu Vực",        Icon = IconChar.MapMarkerAlt,   Permissions = new[] { "VIEW_REGION" },    FormType = typeof(frmKhuVuc) },
                        new MenuDef { Text = "Khách Hàng",     Icon = IconChar.UserGroup,      Permissions = new[] { "VIEW_CUSTOMER" },  FormType = typeof(frmKhachHang) },
                        new MenuDef { Text = "Đoàn Khách",    Icon = IconChar.PeopleGroup,    Permissions = new[] { "VIEW_CUSTOMER" },  FormType = typeof(frmDoanKhach) },
                        new MenuDef { Text = "Khuyến Mãi",     Icon = IconChar.Tags,           Permissions = new[] { "VIEW_PROMOTION" }, FormType = typeof(frmKhuyenMai) },
                    }
                },
                // ── VẬN HÀNH ──
                new CategoryDef
                {
                    Text = "VẬN HÀNH",
                    Icon = IconChar.Tools,
                    Permissions = new[] { "VIEW_STAFF", "VIEW_REGION", "VIEW_INVENTORY" },
                    Items = new MenuDef[]
                    {
                        new MenuDef { Text = "Nhân Viên",      Icon = IconChar.IdCard,               Permissions = new[] { "VIEW_STAFF" },     FormType = typeof(frmNhanVien) },
                        new MenuDef { Text = "Lịch Làm Việc",  Icon = IconChar.CalendarCheck,        Permissions = new[] { "VIEW_STAFF" },     FormType = typeof(frmLichLamViec) },
                        new MenuDef { Text = "Kho Hàng",       Icon = IconChar.Warehouse,            Permissions = new[] { "VIEW_INVENTORY" }, FormType = typeof(frmKhoHang) },
                        new MenuDef { Text = "Nhập Xuất Kho",  Icon = IconChar.FileInvoice,          Permissions = new[] { "VIEW_INVENTORY" }, FormType = typeof(frmPhieuNhapXuat) },
                        new MenuDef { Text = "Sự Cố",          Icon = IconChar.ExclamationTriangle,   Permissions = new[] { "VIEW_STAFF" },     FormType = typeof(frmSuCo) },
                        new MenuDef { Text = "Bảo Trì",        Icon = IconChar.Wrench,               Permissions = new[] { "VIEW_STAFF" },     FormType = typeof(frmBaoTri) },
                        new MenuDef { Text = "Trò Chơi",       Icon = IconChar.Gamepad,              Permissions = new[] { "VIEW_REGION" },    FormType = typeof(frmTroChoi) },
                        new MenuDef { Text = "Khu Thú",        Icon = IconChar.Paw,                  Permissions = new[] { "VIEW_REGION" },    FormType = typeof(frmKhuVucThu) },
                        new MenuDef { Text = "Khu Biển",       Icon = IconChar.Water,                Permissions = new[] { "VIEW_REGION" },    FormType = typeof(frmKhuVucBien) },
                        new MenuDef { Text = "Nhà Hàng",       Icon = IconChar.Utensils,             Permissions = new[] { "VIEW_REGION" },    FormType = typeof(frmNhaHang) },
                        new MenuDef { Text = "Động Vật",       Icon = IconChar.Hippo,                Permissions = new[] { "VIEW_REGION" },    FormType = typeof(frmDongVat) },
                        new MenuDef { Text = "Nước Biển",      Icon = IconChar.Tint,                 Permissions = new[] { "VIEW_REGION" },    FormType = typeof(frmChatLuongNuoc) },
                    }
                },
                // ── BÁO CÁO ──
                new CategoryDef
                {
                    Text = "BÁO CÁO",
                    Icon = IconChar.ChartBar,
                    Permissions = new[] { "VIEW_REPORT", "VIEW_DONHANG", "VIEW_LEDGER" },
                    Items = new MenuDef[]
                    {
                        new MenuDef { Text = "Dashboard",       Icon = IconChar.ChartPie,            Permissions = new[] { "VIEW_REPORT" },   FormType = typeof(frmDashboard) },
                        new MenuDef { Text = "Báo Cáo DT",     Icon = IconChar.ChartBar,            Permissions = new[] { "VIEW_REPORT" },   FormType = typeof(frmBaoCao) },
                        new MenuDef { Text = "Tra Cứu Đơn",    Icon = IconChar.Receipt,             Permissions = new[] { "VIEW_DONHANG" },  FormType = typeof(frmDonHang) },
                        new MenuDef { Text = "Phiếu Thu/Chi",   Icon = IconChar.FileInvoiceDollar,   Permissions = new[] { "VIEW_LEDGER" },   FormType = typeof(frmPhieuThuChi) },
                    }
                },
                // ── HỆ THỐNG ──
                new CategoryDef
                {
                    Text = "HỆ THỐNG",
                    Icon = IconChar.Gears,
                    Permissions = null, // Always visible
                    Items = new MenuDef[]
                    {
                        new MenuDef { Text = "Vai Trò",        Icon = IconChar.UsersCog,       Permissions = new[] { "MANAGE_USER" },  FormType = typeof(frmVaiTro) },
                        new MenuDef { Text = "Phân Quyền",     Icon = IconChar.ShieldHalved,   Permissions = new[] { "MANAGE_USER" },  FormType = typeof(frmPhanQuyen) },
                        new MenuDef { Text = "Marketing",      Icon = IconChar.Bullhorn,       Permissions = new[] { "VIEW_PROMOTION" }, FormType = typeof(frmMarketing) },
                        new MenuDef { Text = "Mô Phỏng Vé",    Icon = IconChar.TicketAlt,      Permissions = new[] { "VIEW_TICKET_SIMULATION" }, FormType = typeof(frmAppDatVeMoPhong) },
                    }
                },
            };
        }

        // ══════════════════════════════════════════════════════════════
        //  CONSTRUCTOR
        // ══════════════════════════════════════════════════════════════
        public Form1()
        {
            Instance = this;
            InitializeComponent();

            this.DoubleBuffered = true;
            typeof(Panel).GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
               ?.SetValue(pnlDesktop, true, null);

            BuildMenuDefinitions();
            StyleMainBar();
            SetupWindowControls();
            SetupSettingsGear();
            SetupClock();
            SetupDrag();

            // Load logo image from pic/ folder
            var logoImg = IconHelper.LoadImage("logo_tdc.png");
            if (logoImg != null)
                picLogo.Image = logoImg;
            else
                picLogo.Image = IconHelper.GetBitmap(IconChar.Building, Color.White, 32);

            // ── Seamless transition: shadow between nav and content ──
            pnlDesktop.Paint += (s, ev) =>
            {
                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    new Rectangle(0, 0, pnlDesktop.Width, 6),
                    Color.FromArgb(40, 0, 0, 0),
                    Color.FromArgb(0, 0, 0, 0),
                    System.Drawing.Drawing2D.LinearGradientMode.Vertical))
                {
                    ev.Graphics.FillRectangle(brush, 0, 0, pnlDesktop.Width, 6);
                }
            };

            // Sub-bar bottom separator
            pnlSubBar.CustomBorderColor = Color.FromArgb(20, 255, 255, 255);
            pnlSubBar.CustomBorderThickness = new Padding(0, 0, 0, 1);

            // ── AI Chatbox Setup ──
            SetupAIChatbox();
        }

        // ══════════════════════════════════════════════════════════════
        //  WINDOW CONTROLS (Min / Max / Close) — Borderless form
        // ══════════════════════════════════════════════════════════════
        private void SetupWindowControls()
        {
            // Minimize
            btnWindowMin.Image = IconHelper.GetBitmap(IconChar.WindowMinimize, TxtInactive, 14);
            btnWindowMin.Click += (s, e) => this.WindowState = FormWindowState.Minimized;

            // Maximize / Restore
            btnWindowMax.Image = IconHelper.GetBitmap(IconChar.WindowMaximize, TxtInactive, 14);
            btnWindowMax.Click += (s, e) =>
            {
                if (this.WindowState == FormWindowState.Maximized)
                {
                    this.WindowState = FormWindowState.Normal;
                    btnWindowMax.Image = IconHelper.GetBitmap(IconChar.WindowMaximize, TxtInactive, 14);
                }
                else
                {
                    this.WindowState = FormWindowState.Maximized;
                    btnWindowMax.Image = IconHelper.GetBitmap(IconChar.WindowRestore, TxtInactive, 14);
                }
            };

            // Close
            btnWindowClose.Image = IconHelper.GetBitmap(IconChar.Xmark, TxtInactive, 14);
            btnWindowClose.Click += (s, e) =>
            {
                if (TDCMessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    Environment.Exit(0);
            };
        }

        // ══════════════════════════════════════════════════════════════
        //  SETTINGS GEAR — Context menu for Logout, About, etc.
        // ══════════════════════════════════════════════════════════════
        private ContextMenuStrip _settingsMenu;

        private void SetupSettingsGear()
        {
            btnSettings.Image = IconHelper.GetBitmap(IconChar.Gear, TxtInactive, 18);
            RebuildSettingsMenu();

            btnSettings.Click += (s, e) =>
            {
                _settingsMenu.Show(btnSettings, new Point(0, btnSettings.Height));
            };
        }

        private void RebuildSettingsMenu()
        {
            _settingsMenu = new ContextMenuStrip();
            _settingsMenu.BackColor = SubDark;
            _settingsMenu.ForeColor = TxtActive;
            _settingsMenu.Font = new Font("Segoe UI", 9.5f);
            _settingsMenu.Renderer = new DarkMenuRenderer();
            _settingsMenu.ShowImageMargin = true;

            // ── Theme Switcher SubMenu ──
            var miTheme = new ToolStripMenuItem("🎨 Giao Diện", null);
            miTheme.DropDown.BackColor = SubDark;
            miTheme.DropDown.ForeColor = TxtActive;
            miTheme.DropDown.Renderer = new DarkMenuRenderer();

            string currentTheme = ThemeManager.GetCurrentThemeName();
            foreach (var (name, displayName) in ThemeManager.GetAvailableThemes())
            {
                string prefix = (name == currentTheme) ? "✔ " : "   ";
                var mi = new ToolStripMenuItem(prefix + displayName);
                mi.Tag = name;
                string themeName = name; // Capture for closure
                mi.Click += (s, e) =>
                {
                    if (ThemeManager.GetCurrentThemeName() == themeName) return;

                    ThemeManager.SetTheme(themeName);
                    Properties.Settings.Default.CurrentTheme = themeName;
                    Properties.Settings.Default.Save();

                    // Anti-Flicker: freeze -> repaint -> snap
                    SuspendDrawing(this);
                    RefreshShellTheme();
                    RefreshChildForms();
                    ResumeDrawing(this);
                };
                miTheme.DropDownItems.Add(mi);
            }

            // Logout
            var miLogout = new ToolStripMenuItem("Đăng Xuất", IconHelper.GetBitmap(IconChar.DoorOpen, TxtInactive, 16));
            miLogout.Click += (s, e) =>
            {
                if (TDCMessageBox.Show("Bạn có muốn đăng xuất không?", "Thông báo",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (this.Owner != null) { this.Owner.Show(); this.Tag = null; this.Close(); }
                }
            };

            // Exit
            var miExit = new ToolStripMenuItem("Thoát", IconHelper.GetBitmap(IconChar.Xmark, TxtInactive, 16));
            miExit.Click += (s, e) =>
            {
                if (TDCMessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    Environment.Exit(0);
            };

            _settingsMenu.Items.Add(miTheme);
            _settingsMenu.Items.Add(new ToolStripSeparator());
            _settingsMenu.Items.Add(miLogout);
            _settingsMenu.Items.Add(new ToolStripSeparator());
            _settingsMenu.Items.Add(miExit);
        }

        // ══════════════════════════════════════════════════════════════
        //  DRAG SUPPORT (Borderless form — drag via pnlMainBar)
        // ══════════════════════════════════════════════════════════════
        private void SetupDrag()
        {
            // Allow dragging from main bar and logo area
            pnlMainBar.MouseDown += DragStart;
            pnlMainBar.MouseMove += DragMove;
            pnlMainBar.MouseUp   += DragEnd;

            pnlLogoArea.MouseDown += DragStart;
            pnlLogoArea.MouseMove += DragMove;
            pnlLogoArea.MouseUp   += DragEnd;

            picLogo.MouseDown += DragStart;
            picLogo.MouseMove += DragMove;
            picLogo.MouseUp   += DragEnd;

            // Double-click title bar to maximize/restore
            pnlMainBar.DoubleClick += (s, e) => btnWindowMax.PerformClick();
            pnlLogoArea.DoubleClick += (s, e) => btnWindowMax.PerformClick();
        }

        private void DragStart(object s, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && this.WindowState != FormWindowState.Maximized)
            {
                _dragging = true;
                _dragStart = e.Location;
            }
        }

        private void DragMove(object s, MouseEventArgs e)
        {
            if (_dragging)
            {
                this.Left += e.X - _dragStart.X;
                this.Top  += e.Y - _dragStart.Y;
            }
        }

        private void DragEnd(object s, MouseEventArgs e)
        {
            _dragging = false;
        }

        // ══════════════════════════════════════════════════════════════
        //  MAIN BAR STYLING
        // ══════════════════════════════════════════════════════════════
        private Guna.UI2.WinForms.Guna2Button[] _catButtons;

        private void StyleMainBar()
        {
            _catButtons = new[] { btnCat1, btnCat2, btnCat3, btnCat4, btnCat5 };

            for (int i = 0; i < _catButtons.Length && i < _categories.Length; i++)
            {
                var btn = _catButtons[i];
                var cat = _categories[i];

                btn.Text = cat.Text;
                btn.Image = IconHelper.GetBitmap(cat.Icon, TxtInactive, 18);
                btn.Tag = i; // Store category index

                // Hover state
                btn.HoverState.FillColor = Color.FromArgb(30, 255, 255, 255);
                btn.HoverState.ForeColor = TxtActive;

                int catIndex = i; // Capture for closure
                btn.Click += (s, e) => SwitchCategory(catIndex);
            }

            // Separator line between main bar and sub bar
            pnlMainBar.CustomBorderColor = Color.FromArgb(30, 255, 255, 255);
            pnlMainBar.CustomBorderThickness = new Padding(0, 0, 0, 1);
        }

        // ══════════════════════════════════════════════════════════════
        //  CATEGORY SWITCHING
        // ══════════════════════════════════════════════════════════════
        private void SwitchCategory(int catIndex)
        {
            if (catIndex < 0 || catIndex >= _categories.Length) return;

            _activeCatIndex = catIndex;

            // ── Update main bar button states ──
            for (int i = 0; i < _catButtons.Length; i++)
            {
                bool isActive = (i == catIndex);
                _catButtons[i].ForeColor = isActive ? TxtActive : TxtInactive;
                _catButtons[i].Font = new Font("Segoe UI Semibold", 10f, isActive ? FontStyle.Bold : FontStyle.Regular);
                _catButtons[i].Image = IconHelper.GetBitmap(_categories[i].Icon, isActive ? TxtActive : TxtInactive, 18);

                // Active indicator: bottom border
                _catButtons[i].CustomBorderColor = isActive ? AccentBlue : Color.Transparent;
                _catButtons[i].CustomBorderThickness = isActive ? new Padding(0, 0, 0, 3) : new Padding(0);
            }

            // ── Build sub-bar for this category ──
            BuildSubBar(catIndex);
        }

        // ══════════════════════════════════════════════════════════════
        //  SUB-BAR BUILDING (Direct layout — no overflow, all visible)
        // ══════════════════════════════════════════════════════════════
        private void BuildSubBar(int catIndex)
        {
            flowSubNav.SuspendLayout();
            flowSubNav.Controls.Clear();

            var cat = _categories[catIndex];
            string lastActiveName = null;
            _lastSubPerCat.TryGetValue(catIndex, out lastActiveName);

            Guna.UI2.WinForms.Guna2Button lastActiveBtn = null;

            foreach (var item in cat.Items)
            {
                if (item.Permissions != null && !CanAny(item.Permissions))
                    continue;

                var btn = CreateSubButton(item, catIndex);
                flowSubNav.Controls.Add(btn);

                if (lastActiveName != null && item.Text == lastActiveName)
                    lastActiveBtn = btn;

                // Show dot if form is cached
                if (item.FormType != null && _formCache.ContainsKey(item.FormType) && !_formCache[item.FormType].IsDisposed)
                    btn.Text = item.Text + " •";
            }

            flowSubNav.ResumeLayout(true);

            // Auto-restore last active sub-item for this category
            if (lastActiveBtn != null)
            {
                lastActiveBtn.PerformClick();
            }
        }

        private Guna.UI2.WinForms.Guna2Button CreateSubButton(MenuDef item, int catIndex)
        {
            var btn = new Guna.UI2.WinForms.Guna2Button();
            btn.Text = item.Text;
            btn.Name = "sub_" + item.Text.Replace(" ", "");
            btn.Image = IconHelper.GetBitmap(item.Icon, TxtInactive, 16);
            btn.ImageSize = new Size(16, 16);
            btn.ImageAlign = HorizontalAlignment.Left;
            btn.ImageOffset = new Point(6, 0);
            btn.TextAlign = HorizontalAlignment.Left;
            btn.TextOffset = new Point(8, 0);
            btn.Font = new Font("Segoe UI", 9.5f);
            btn.ForeColor = TxtInactive;
            btn.FillColor = Color.Transparent;
            btn.BorderRadius = 4;
            btn.Cursor = Cursors.Hand;
            btn.Animated = true;
            btn.AutoSize = false;
            btn.Margin = new Padding(2, 3, 2, 3);
            btn.Padding = new Padding(0);

            // Measure text width for auto-sizing
            int textWidth = TextRenderer.MeasureText(item.Text, btn.Font).Width;
            btn.Size = new Size(textWidth + 46, 30); // 16 icon + 8+6 padding + text + margin

            // Hover
            btn.HoverState.FillColor = Color.FromArgb(20, 255, 255, 255);
            btn.HoverState.ForeColor = TxtActive;

            // Store menu definition in Tag
            btn.Tag = item;

            btn.Click += (s, e) =>
            {
                var def = (MenuDef)((Guna.UI2.WinForms.Guna2Button)s).Tag;

                if (def.SpecialAction != null)
                {
                    def.SpecialAction();
                    return;
                }

                if (def.FormType != null)
                {
                    SetActiveSubButton((Guna.UI2.WinForms.Guna2Button)s);
                    _lastSubPerCat[catIndex] = def.Text;
                    OpenChildForm(def.FormType, def.Text);
                }
            };

            return btn;
        }

        // ══════════════════════════════════════════════════════════════
        //  ACTIVE SUB-BUTTON STYLING
        // ══════════════════════════════════════════════════════════════
        private void SetActiveSubButton(Guna.UI2.WinForms.Guna2Button activeBtn)
        {
            // Reset all sub-buttons
            foreach (Control c in flowSubNav.Controls)
            {
                if (c is Guna.UI2.WinForms.Guna2Button btn)
                {
                    var def = (MenuDef)btn.Tag;
                    bool isCached = def.FormType != null && _formCache.ContainsKey(def.FormType) && !_formCache[def.FormType].IsDisposed;

                    btn.FillColor = Color.Transparent;
                    btn.ForeColor = TxtInactive;
                    btn.Font = new Font("Segoe UI", 9.5f);
                    btn.Image = IconHelper.GetBitmap(def.Icon, TxtInactive, 16);
                    btn.Text = isCached ? def.Text + " •" : def.Text;
                }
            }

            // Highlight active
            if (activeBtn != null)
            {
                var activeDef = (MenuDef)activeBtn.Tag;
                activeBtn.FillColor = PillActive;
                activeBtn.ForeColor = TxtActive;
                activeBtn.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                activeBtn.Image = IconHelper.GetBitmap(activeDef.Icon, TxtActive, 16);
                activeBtn.Text = activeDef.Text;

                _activeSubBtn = activeBtn;
            }
        }

        // ══════════════════════════════════════════════════════════════
        //  OPEN CHILD FORM (Refactored — single method for all forms)
        // ══════════════════════════════════════════════════════════════
        private void OpenChildForm(Type formType, string title)
        {
            var prevForm = activeForm;

            try { DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Hệ Thống", "Đang tải dữ liệu..."); } catch { }
            SuspendDrawing(this);

            if (!_formCache.TryGetValue(formType, out Form cached) || cached.IsDisposed)
            {
                cached = (Form)Activator.CreateInstance(formType);
                cached.TopLevel = false;
                cached.FormBorderStyle = FormBorderStyle.None;
                cached.Dock = DockStyle.Fill;
                cached.BackColor = Color.FromArgb(248, 250, 252); // Match pnlDesktop Slate-50 for seamless look
                cached.AutoScroll = true;
                cached.MinimumSize = new Size(0, 0);
                typeof(Form).GetProperty("DoubleBuffered",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                   ?.SetValue(cached, true, null);
                pnlDesktop.Controls.Add(cached);
                cached.Tag = this.Tag;
                pnlDesktop.AutoScroll = true;
                _formCache[formType] = cached;

                // IBaseForm lifecycle — apply theme, permissions, and load data
                if (cached is IBaseForm baseForm)
                {
                    baseForm.ApplyStyles();
                    baseForm.ApplyPermissions();
                    baseForm.InitIcons();
                    baseForm.LoadData();
                }
            }

            activeForm = cached;
            cached.BringToFront();
            cached.Show();

            if (prevForm != null && prevForm != cached) prevForm.Hide();

            // ── AI Context Switching: "Đổi Não" ──
            if (_chatPanel != null)
            {
                if (cached is IAIFormContext aiCtx)
                    _chatPanel.SwitchContext(aiCtx.AIContextName, aiCtx.AIContextDescription);
                else
                    _chatPanel.SwitchContext("navigation", null);

                // Đảm bảo chat bubble luôn nổi trên cùng
                if (_btnAIChatBubble != null) _btnAIChatBubble.BringToFront();
                if (_chatPanel.Visible) _chatPanel.BringToFront();
            }

            ResumeDrawing(this);
            Application.DoEvents();
            try { DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm(); } catch { }

            // Update status bar with context info
            lblStatus.Text = "  📂 " + title;
        }

        // ══════════════════════════════════════════════════════════════
        //  FORM LOAD
        // ══════════════════════════════════════════════════════════════
        private void Form1_Load(object sender, EventArgs e)
        {
            // APPLY THEME TO MAIN SHELL ON STARTUP
            RefreshShellTheme();

            var tk = SessionManager.CurrentUser;
            if (tk != null)
            {
                // User name + icon in top bar
                lblUserName.Text = tk.TenDangNhap;
                lblUserName.Image = IconHelper.GetBitmap(IconChar.UserCircle, TxtInactive, 20);
                lblUserName.ImageAlign = ContentAlignment.MiddleLeft;
                lblUserName.TextAlign = ContentAlignment.MiddleCenter;

                // Status bar — show role + connection info
                var role = BUS_VaiTro.Instance.LayTheoId(tk.IdVaiTro);
                string roleName = role?.TenVaiTro ?? tk.IdVaiTro.ToString();
                lblStatus.Text = string.Format("  ● Kết nối LAN: OK  │  Vai trò: {0}", roleName);

                // Hide categories user has no permission for
                for (int i = 0; i < _catButtons.Length && i < _categories.Length; i++)
                {
                    var cat = _categories[i];
                    _catButtons[i].Visible = (cat.Permissions == null) || CanAny(cat.Permissions);
                }

                // Auto-select first visible category
                for (int i = 0; i < _catButtons.Length; i++)
                {
                    if (_catButtons[i].Visible)
                    {
                        SwitchCategory(i);
                        break;
                    }
                }
            }
        }

        // ══════════════════════════════════════════════════════════════
        //  STATUS BAR CLOCK
        // ══════════════════════════════════════════════════════════════
        private Timer _clockTimer;
        private void SetupClock()
        {
            _clockTimer = new Timer();
            _clockTimer.Interval = 30000; // Update every 30 seconds
            _clockTimer.Tick += (s, e) =>
            {
                lblClock.Text = DateTime.Now.ToString("HH:mm  dd/MM");
            };
            _clockTimer.Start();
            lblClock.Text = DateTime.Now.ToString("HH:mm  dd/MM");
        }

        // ══════════════════════════════════════════════════════════════
        //  KEYBOARD SHORTCUTS
        // ══════════════════════════════════════════════════════════════
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Ctrl+1..5 to switch categories
            if ((keyData & Keys.Control) == Keys.Control)
            {
                int catIndex = -1;
                if ((keyData & Keys.D1) == Keys.D1) catIndex = 0;
                else if ((keyData & Keys.D2) == Keys.D2) catIndex = 1;
                else if ((keyData & Keys.D3) == Keys.D3) catIndex = 2;
                else if ((keyData & Keys.D4) == Keys.D4) catIndex = 3;
                else if ((keyData & Keys.D5) == Keys.D5) catIndex = 4;

                if (catIndex >= 0 && catIndex < _catButtons.Length && _catButtons[catIndex].Visible)
                {
                    SwitchCategory(catIndex);
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        // ══════════════════════════════════════════════════════════════
        //  FORM CLOSING
        // ══════════════════════════════════════════════════════════════
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _clockTimer?.Stop();
            _clockTimer?.Dispose();
            if (e.CloseReason == CloseReason.UserClosing && this.Owner != null && !this.Owner.Visible)
            {
                Environment.Exit(0);
            }
        }

        // ══════════════════════════════════════════════════════════════
        //  LIVE THEME SWITCHING — Refresh shell + cached child forms
        // ══════════════════════════════════════════════════════════════

        /// <summary>
        /// Re-paint tất cả shell panels (nav bar, sub bar, status bar) theo theme mới.
        /// </summary>
        private void RefreshShellTheme()
        {
            // Main bar
            pnlMainBar.BackColor = NavDark;
            pnlMainBar.FillColor = NavDark;
            pnlMainBar.CustomBorderColor = Color.FromArgb(30, 255, 255, 255);
            pnlMainBar.CustomBorderThickness = new Padding(0, 0, 0, 1);

            // Sub bar
            pnlSubBar.BackColor = SubDark;
            pnlSubBar.FillColor = SubDark;
            pnlSubBar.CustomBorderColor = Color.FromArgb(20, 255, 255, 255);
            pnlSubBar.CustomBorderThickness = new Padding(0, 0, 0, 1);

            // Status bar
            pnlStatusBar.BackColor = NavDark;
            pnlStatusBar.FillColor = NavDark;
            lblStatus.ForeColor = TxtInactive;
            lblClock.ForeColor = TxtInactive;

            // Desktop
            pnlDesktop.BackColor = ThemeManager.BackgroundColor;
            this.BackColor = ThemeManager.BackgroundColor;

            // User area
            lblUserName.ForeColor = TxtInactive;

            // Window control icons
            btnWindowMin.Image = IconHelper.GetBitmap(IconChar.WindowMinimize, TxtInactive, 14);
            btnWindowMax.Image = IconHelper.GetBitmap(
                this.WindowState == FormWindowState.Maximized ? IconChar.WindowRestore : IconChar.WindowMaximize,
                TxtInactive, 14);
            btnWindowClose.Image = IconHelper.GetBitmap(IconChar.Xmark, TxtInactive, 14);

            // Settings gear
            btnSettings.Image = IconHelper.GetBitmap(IconChar.Gear, TxtInactive, 18);

            // Category buttons
            for (int i = 0; i < _catButtons.Length && i < _categories.Length; i++)
            {
                var btn = _catButtons[i];
                bool isActive = (i == _activeCatIndex);
                btn.ForeColor = isActive ? TxtActive : TxtInactive;
                btn.Image = IconHelper.GetBitmap(_categories[i].Icon, isActive ? TxtActive : TxtInactive, 18);
                btn.HoverState.FillColor = Color.FromArgb(30, 255, 255, 255);
                btn.HoverState.ForeColor = TxtActive;
                btn.CustomBorderColor = isActive ? AccentBlue : Color.Transparent;
            }

            // Rebuild settings menu with new theme colors
            RebuildSettingsMenu();

            // Refresh active sub-bar
            if (_activeCatIndex >= 0)
                BuildSubBar(_activeCatIndex);
        }

        /// <summary>
        /// Re-theme tất cả child forms đang cache trong _formCache.
        /// </summary>
        private void RefreshChildForms()
        {
            foreach (var kvp in _formCache)
            {
                var form = kvp.Value;
                if (form != null && !form.IsDisposed)
                {
                    form.SuspendLayout();
                    form.BackColor = ThemeManager.BackgroundColor;
                    ThemeManager.ApplyTheme(form);
                    form.ResumeLayout(true);
                }
            }
        }

        // ══════════════════════════════════════════════════════════════
        //  AI CHATBOX — Floating Chat Bubble + Panel
        // ══════════════════════════════════════════════════════════════

        private void SetupAIChatbox()
        {
            // ── Chat Bubble Button (góc phải dưới pnlDesktop) ──
            _btnAIChatBubble = new Guna.UI2.WinForms.Guna2CircleButton();
            _btnAIChatBubble.Size = new Size(52, 52);
            _btnAIChatBubble.FillColor = Color.FromArgb(212, 175, 55); // Gold
            _btnAIChatBubble.HoverState.FillColor = Color.FromArgb(245, 208, 80);
            _btnAIChatBubble.Image = IconHelper.GetBitmap(IconChar.Robot, Color.White, 24);
            _btnAIChatBubble.Cursor = Cursors.Hand;
            _btnAIChatBubble.Animated = true;
            _btnAIChatBubble.UseTransparentBackground = true;
            _btnAIChatBubble.ShadowDecoration.Enabled = true;
            _btnAIChatBubble.ShadowDecoration.Color = Color.FromArgb(100, 212, 175, 55);
            _btnAIChatBubble.ShadowDecoration.Depth = 8;

            pnlDesktop.Controls.Add(_btnAIChatBubble);
            _btnAIChatBubble.BringToFront();

            // ── Chat Panel ──
            _chatPanel = new AIChatPanel();
            _chatPanel.Size = new Size(400, 540);
            _chatPanel.Visible = false;
            _chatPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            pnlDesktop.Controls.Add(_chatPanel);

            // ── Position trên resize ──
            pnlDesktop.Resize += (s, e) => PositionChatControls();
            PositionChatControls();

            // ── Toggle show/hide ──
            _btnAIChatBubble.Click += (s, e) =>
            {
                _chatPanel.Visible = !_chatPanel.Visible;
                if (_chatPanel.Visible)
                {
                    _chatPanel.BringToFront();
                    _chatPanel.Focus();
                }
            };

            // Khởi tạo context mặc định = Navigation
            _chatPanel.SwitchContext("navigation", null);

            // ── Lắng nghe lệnh thao tác UI từ AI ──
            _chatPanel.OnUICommandRequested += (cmd, args) =>
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => DispatchUICommand(cmd, args)));
                }
                else
                {
                    DispatchUICommand(cmd, args);
                }
            };
        }

        private void DispatchUICommand(string cmd, Dictionary<string, object> args)
        {
            if (activeForm is GUI.AI.IAICommandHandler handler)
            {
                handler.ExecuteAICommand(cmd, args);
            }
        }

        private void PositionChatControls()
        {
            if (_btnAIChatBubble == null) return;
            int margin = 20;
            _btnAIChatBubble.Location = new Point(
                pnlDesktop.Width - _btnAIChatBubble.Width - margin,
                pnlDesktop.Height - _btnAIChatBubble.Height - margin);

            if (_chatPanel != null)
            {
                _chatPanel.Location = new Point(
                    pnlDesktop.Width - _chatPanel.Width - margin,
                    pnlDesktop.Height - _chatPanel.Height - _btnAIChatBubble.Height - margin - 8);
            }
        }

        /// <summary>
        /// Bridge method cho AI Agent gọi — mở form theo tên string.
        /// AI gửi tên form (vd: "frmKhoHang") -> method này tìm trong _categories -> mở form.
        /// </summary>
        public void NavigateToFormByAI(string formName)
        {
            if (string.IsNullOrEmpty(formName)) return;

            // Tìm form trong menu definitions
            for (int catIdx = 0; catIdx < _categories.Length; catIdx++)
            {
                var cat = _categories[catIdx];
                foreach (var item in cat.Items)
                {
                    if (item.FormType != null && item.FormType.Name.Equals(formName, StringComparison.OrdinalIgnoreCase))
                    {
                        // Chuyển category nếu cần
                        if (_activeCatIndex != catIdx)
                            SwitchCategory(catIdx);

                        // Mở form
                        OpenChildForm(item.FormType, item.Text);

                        // Highlight sub-button tương ứng
                        foreach (Control c in flowSubNav.Controls)
                        {
                            if (c is Guna.UI2.WinForms.Guna2Button btn && btn.Tag is MenuDef md && md.FormType == item.FormType)
                            {
                                SetActiveSubButton(btn);
                                _lastSubPerCat[catIdx] = item.Text;
                                break;
                            }
                        }
                        return;
                    }
                }
            }
        }
    }

    // ══════════════════════════════════════════════════════════════
    //  DARK MENU RENDERER — for gear settings dropdown
    // ══════════════════════════════════════════════════════════════
    internal class DarkMenuRenderer : ToolStripProfessionalRenderer
    {
        public DarkMenuRenderer() : base(new DarkColorTable()) { }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            var rc = new Rectangle(Point.Empty, e.Item.Size);
            using (var br = new SolidBrush(e.Item.Selected ? ThemeManager.SidebarHoverColor : ThemeManager.SidebarColor))
                e.Graphics.FillRectangle(br, rc);
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = Color.FromArgb(226, 232, 240);
            base.OnRenderItemText(e);
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            using (var pen = new Pen(ThemeManager.SidebarHoverColor))
                e.Graphics.DrawRectangle(pen, 0, 0, e.AffectedBounds.Width - 1, e.AffectedBounds.Height - 1);
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            using (var br = new SolidBrush(ThemeManager.SidebarColor))
                e.Graphics.FillRectangle(br, e.AffectedBounds);
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            int y = e.Item.ContentRectangle.Height / 2;
            using (var pen = new Pen(ThemeManager.SidebarHoverColor))
                e.Graphics.DrawLine(pen, 4, y, e.Item.Width - 4, y);
        }
    }

    internal class DarkColorTable : ProfessionalColorTable
    {
        public override Color MenuBorder => ThemeManager.SidebarHoverColor;
        public override Color MenuItemBorder => ThemeManager.SidebarHoverColor;
        public override Color MenuItemSelected => ThemeManager.SidebarHoverColor;
        public override Color MenuStripGradientBegin => ThemeManager.SidebarColor;
        public override Color MenuStripGradientEnd => ThemeManager.SidebarColor;
        public override Color MenuItemSelectedGradientBegin => ThemeManager.SidebarHoverColor;
        public override Color MenuItemSelectedGradientEnd => ThemeManager.SidebarHoverColor;
        public override Color MenuItemPressedGradientBegin => ThemeManager.SidebarHoverColor;
        public override Color MenuItemPressedGradientEnd => ThemeManager.SidebarHoverColor;
        public override Color ToolStripDropDownBackground => ThemeManager.SidebarColor;
        public override Color ImageMarginGradientBegin => ThemeManager.SidebarColor;
        public override Color ImageMarginGradientMiddle => ThemeManager.SidebarColor;
        public override Color ImageMarginGradientEnd => ThemeManager.SidebarColor;
        public override Color SeparatorDark => ThemeManager.SidebarHoverColor;
        public override Color SeparatorLight => ThemeManager.SidebarHoverColor;
    }
}

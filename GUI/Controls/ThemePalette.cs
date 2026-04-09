using System.Drawing;

namespace GUI
{
    /// <summary>
    /// Chứa toàn bộ color tokens cho 1 theme.
    /// Mỗi instance = 1 bộ màu hoàn chỉnh.
    /// </summary>
    public class ThemePalette
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }

        // ══════════════════════════════════════════════════════════════
        //  CONTENT AREA — Vùng nội dung chính (forms, grids, inputs)
        // ══════════════════════════════════════════════════════════════
        public Color TextPrimary { get; set; }
        public Color TextSecondary { get; set; }
        public Color Background { get; set; }      // App canvas (surface)
        public Color Panel { get; set; }            // = Background for most themes
        public Color Surface { get; set; }          // Cards, inputs (surface-container-lowest)
        public Color SurfaceHigh { get; set; }      // Inset panels (surface-container-high)
        public Color Border { get; set; }           // outline-variant
        public Color Accent { get; set; }           // Highlight color

        // ══════════════════════════════════════════════════════════════
        //  SEMANTIC — Ý nghĩa cố định (hành động, trạng thái)
        // ══════════════════════════════════════════════════════════════
        public Color Primary { get; set; }          // Brand + CTA chính
        public Color PrimaryContainer { get; set; } // Hover/Gradient variant
        public Color Secondary { get; set; }        // Brand phụ
        public Color SecondaryContainer { get; set; }// Highlight badge
        public Color Success { get; set; }
        public Color Danger { get; set; }
        public Color Warning { get; set; }

        // ══════════════════════════════════════════════════════════════
        //  SHELL / NAVIGATION — Vùng tối (sidebar, nav bar, status)
        // ══════════════════════════════════════════════════════════════
        public Color ShellDarkest { get; set; }     // NavDark, SidebarLogo, StatusBar
        public Color ShellDark { get; set; }        // SubBar, SidebarBg, TitleBar
        public Color ShellMedium { get; set; }      // Hover, Pill active, GridHeader
        public Color ShellTextMuted { get; set; }   // Inactive text/icon on dark bg
        public Color ShellTextBright { get; set; }  // Active text on dark bg
        public Color ShellAccent { get; set; }      // Active indicator (Blue-400 / Gold)
        public Color ShellSeparator { get; set; }   // Separator lines on dark bg

        // ══════════════════════════════════════════════════════════════
        //  GRID
        // ══════════════════════════════════════════════════════════════
        public Color GridHeader { get; set; }
        public Color GridSelection { get; set; }
        public Color GridSelectionFore { get; set; }
        public Color GradientInactive { get; set; }
        public Color GradientActive { get; set; }

        // ══════════════════════════════════════════════════════════════
        //  DASHBOARD CARDS
        // ══════════════════════════════════════════════════════════════
        public Color CardBlue1 { get; set; }
        public Color CardBlue2 { get; set; }
        public Color CardGreen1 { get; set; }
        public Color CardGreen2 { get; set; }
        public Color CardOrange1 { get; set; }
        public Color CardOrange2 { get; set; }
        public Color CardViolet1 { get; set; }
        public Color CardViolet2 { get; set; }

        // ══════════════════════════════════════════════════════════════
        //  FACTORY: Slate Classic — Bộ màu hiện tại (giữ nguyên 100%)
        // ══════════════════════════════════════════════════════════════
        public static ThemePalette CreateSlateClassic()
        {
            return new ThemePalette
            {
                Name = "SlateClassic",
                DisplayName = "Xám Thanh Lịch",

                // Content
                TextPrimary        = Color.FromArgb(51, 65, 85),      // Slate-700
                TextSecondary      = Color.FromArgb(100, 116, 139),   // Slate-500
                Background         = Color.FromArgb(248, 250, 252),   // Slate-50
                Panel              = Color.FromArgb(248, 250, 252),
                Surface            = Color.White,
                SurfaceHigh        = Color.FromArgb(232, 232, 232),
                Border             = Color.FromArgb(203, 213, 225),   // Slate-300
                Accent             = Color.FromArgb(16, 185, 129),    // Emerald-500

                // Semantic
                Primary            = Color.FromArgb(51, 65, 85),      // Slate-700
                PrimaryContainer   = Color.FromArgb(71, 85, 105),     // Slate-600
                Secondary          = Color.FromArgb(100, 116, 139),   // Slate-500
                SecondaryContainer = Color.FromArgb(148, 163, 184),   // Slate-400
                Success            = Color.FromArgb(16, 185, 129),
                Danger             = Color.FromArgb(185, 28, 28),
                Warning            = Color.FromArgb(180, 83, 9),

                // Shell
                ShellDarkest       = Color.FromArgb(15, 23, 42),      // Slate-900
                ShellDark          = Color.FromArgb(30, 41, 59),      // Slate-800
                ShellMedium        = Color.FromArgb(51, 65, 85),      // Slate-700
                ShellTextMuted     = Color.FromArgb(148, 163, 184),   // Slate-400
                ShellTextBright    = Color.White,
                ShellAccent        = Color.FromArgb(96, 165, 250),    // Blue-400
                ShellSeparator     = Color.FromArgb(60, 75, 100),

                // Grid
                GridHeader         = Color.FromArgb(51, 65, 85),
                GridSelection      = Color.FromArgb(248, 250, 252),
                GridSelectionFore  = Color.FromArgb(51, 65, 85),
                GradientInactive   = Color.FromArgb(248, 250, 252),
                GradientActive     = Color.FromArgb(203, 213, 225),

                // Dashboard Cards
                CardBlue1          = Color.FromArgb(30, 41, 59),
                CardBlue2          = Color.FromArgb(51, 65, 85),
                CardGreen1         = Color.FromArgb(20, 83, 45),
                CardGreen2         = Color.FromArgb(16, 185, 129),
                CardOrange1        = Color.FromArgb(124, 45, 18),
                CardOrange2        = Color.FromArgb(234, 88, 12),
                CardViolet1        = Color.FromArgb(51, 65, 85),
                CardViolet2        = Color.FromArgb(100, 116, 139),
            };
        }

        // ══════════════════════════════════════════════════════════════
        //  FACTORY: Imperial Modernity — Đỏ Hoàng Gia Đại Nam
        //  Tuân thủ Design System Document: "The Digital Sanctuary"
        //  Palette: Vietnamese royalty + contemporary minimalist
        // ══════════════════════════════════════════════════════════════
        public static ThemePalette CreateImperialModernity()
        {
            return new ThemePalette
            {
                Name = "ImperialModernity",
                DisplayName = "Đỏ Hoàng Gia",

                // Content — Không nhợt nhạt, nền kem ấm mượt mà
                TextPrimary        = Color.FromArgb(43, 30, 30),      // Nâu đen thẫm, rất nét
                TextSecondary      = Color.FromArgb(115, 95, 95),     // Nâu xám
                Background         = Color.FromArgb(250, 248, 246),   // Kem nhạt rất sang (#FAF8F6)
                Panel              = Color.FromArgb(250, 248, 246),
                Surface            = Color.White,                     // Nổi bật card trắng
                SurfaceHigh        = Color.FromArgb(240, 235, 232),   // Xám kem 
                Border             = Color.FromArgb(224, 212, 210),   // Viền rất êm
                Accent             = Color.FromArgb(212, 175, 55),    // Royal Gold 

                // Semantic
                Primary            = Color.FromArgb(128, 21, 21),     // Đỏ tươi hơn, bớt bầm
                PrimaryContainer   = Color.FromArgb(163, 27, 27),     
                Secondary          = Color.FromArgb(153, 122, 0),     // Vàng đồng (#997a00)
                SecondaryContainer = Color.FromArgb(245, 214, 115),   
                Success            = Color.FromArgb(22, 163, 74),    
                Danger             = Color.FromArgb(220, 38, 38),     
                Warning            = Color.FromArgb(217, 119, 6),      

                // Shell — Khắc phục "phờ phạc": Trả lại sức nặng cho thanh Top Bar
                // Thanh top cần màu sâu, đậm, tương phản cực gắt với content bên dưới
                ShellDarkest       = Color.FromArgb(61, 14, 14),      // Đỏ mận quyền lực (#3D0E0E) - MainBar
                ShellDark          = Color.FromArgb(46, 11, 11),      // Đỏ đô thẫm - SubBar 
                ShellMedium        = Color.FromArgb(89, 23, 23),      // Đỏ sáng khi Hover
                ShellTextMuted     = Color.FromArgb(224, 196, 196),   // Chữ trắng ánh hồng (dịu mắt trên nền tối)
                ShellTextBright    = Color.White,                     // Chữ trắng tinh khi active
                ShellAccent        = Color.FromArgb(242, 197, 85),    // Sọc Vàng Gold nổi bật (#F2C555)
                ShellSeparator     = Color.FromArgb(87, 28, 28),      // Line phân cách mượt 

                // Grid — Header nhạt, trang nhã, không đè nén nội dung
                GridHeader         = Color.FromArgb(61, 14, 14),      // Cho Header Grid trùng đỏ thẫm
                GridSelection      = Color.FromArgb(247, 232, 230),   // Kem ánh hồng rất nhạt
                GridSelectionFore  = Color.FromArgb(128, 21, 21),     // Chữ đỏ
                GradientInactive   = Color.FromArgb(250, 248, 246),
                GradientActive     = Color.FromArgb(224, 212, 210),

                // Dashboard Cards 
                CardBlue1          = Color.FromArgb(153, 122, 0),     // Vàng Gold thẫm
                CardBlue2          = Color.FromArgb(242, 197, 85),    // Vàng Gold sáng
                CardGreen1         = Color.FromArgb(20, 83, 45),      
                CardGreen2         = Color.FromArgb(16, 185, 129),
                CardOrange1        = Color.FromArgb(128, 21, 21),     // Đỏ Primary
                CardOrange2        = Color.FromArgb(180, 28, 28),
                CardViolet1        = Color.FromArgb(84, 46, 68),      // Tím mận
                CardViolet2        = Color.FromArgb(135, 75, 110),
            };
        }
    }
}

using System.Drawing;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.Utils.Svg;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;

namespace GUI.Infrastructure
{
    // Quản lý theme + màu sắc + font toàn app.
    // Gọi AppStyle.Init() trong Program.cs TRƯỚC Application.Run().
    public static class AppStyle
    {
        #region Bảng màu Đại Nam — tham chiếu module Khách Hàng

        public static readonly Color Navy = Color.FromArgb(26, 35, 50);
        public static readonly Color NavyLight = Color.FromArgb(36, 48, 66);
        public static readonly Color Teal = Color.FromArgb(37, 64, 121);     
        public static readonly Color TealLight = Color.FromArgb(38, 166, 154);
        public static readonly Color Coral = Color.FromArgb(239, 108, 87);
        public static readonly Color Danger = Color.FromArgb(239, 83, 80);
        public static readonly Color Success = Color.FromArgb(67, 160, 71);
        public static readonly Color Amber = Color.FromArgb(255, 160, 0);
        public static readonly Color Gold = Color.FromArgb(201, 169, 70);
        public static readonly Color BgMain = Color.FromArgb(244, 245, 247);
        public static readonly Color BgCard = Color.White;
        public static readonly Color Border = Color.FromArgb(222, 226, 230);
        public static readonly Color GridHeader = Color.FromArgb(52, 63, 82);
        public static readonly Color TextPrimary = Color.FromArgb(33, 33, 33);
        public static readonly Color TextMuted = Color.FromArgb(117, 117, 117);

        #endregion

        #region Font chữ mặc định

        public static readonly Font FontDefault = new Font("Segoe UI", 9.5f);
        public static readonly Font FontBold = new Font("Segoe UI Semibold", 9.5f);
        public static readonly Font FontHeader = new Font("Segoe UI Semibold", 11f);
        public static readonly Font FontTitle = new Font("Segoe UI Semibold", 14f);

        #endregion

        #region Khởi tạo theme

        /// <summary>
        /// Gọi TRƯỚC Application.Run(). WXI skin thuần.
        /// </summary>
        public static void Init()
        {
            WindowsFormsSettings.DefaultFont = FontDefault;
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);
        }

        /// <summary>
        /// Đệ quy set ForeColor đen cho TẤT CẢ DevExpress editors trong container.
        /// DevExpress TextEdit không kế thừa ForeColor từ parent như WinForms.
        /// Phải set Properties.Appearance.ForeColor trực tiếp.
        /// Gọi trong mỗi form Load sau StyleForm().
        /// </summary>
        public static void FixEditorForeColor(System.Windows.Forms.Control parent)
        {
            foreach (System.Windows.Forms.Control ctrl in parent.Controls)
            {
                if (ctrl is BaseEdit edit)
                {
                    edit.Properties.Appearance.ForeColor = TextPrimary;
                    edit.Properties.Appearance.Options.UseForeColor = true;
                }

                if (ctrl.HasChildren)
                    FixEditorForeColor(ctrl);
            }
        }

        #endregion

        #region Style grid

        /// <summary>
        /// Grid header tối, dòng xen kẽ, row 32px.
        /// </summary>
        public static void StyleGrid(GridView view)
        {
            if (view == null) return;

            // Header: để WXI skin mặc định, chỉ đổi font đậm
            view.Appearance.HeaderPanel.Font = FontBold;
            view.Appearance.HeaderPanel.Options.UseFont = true;

            view.Appearance.OddRow.BackColor = Color.FromArgb(248, 249, 250);
            view.Appearance.OddRow.Options.UseBackColor = true;
            view.OptionsView.EnableAppearanceOddRow = true;

            view.Appearance.FocusedRow.BackColor = Color.FromArgb(224, 242, 239);
            view.Appearance.FocusedRow.ForeColor = TextPrimary;
            view.Appearance.FocusedRow.Options.UseBackColor = true;
            view.Appearance.FocusedRow.Options.UseForeColor = true;

            view.OptionsView.ShowGroupPanel = false;
            view.OptionsView.ShowIndicator = false;
            view.OptionsView.ColumnAutoWidth = true;
            view.OptionsView.RowAutoHeight = true;
            view.RowHeight = 32;
        }

        #endregion

        #region Style button — GIỮ skin WXI cho bo tròn + hover

        /// <summary>
        /// Nút chính: Teal nền, trắng chữ.
        /// GIỮ UseDefaultLookAndFeel = true để skin WXI vẽ bo tròn + hover effect.
        /// Chỉ bật Options.UseXxx để đè màu lên skin.
        /// </summary>
        public static void StyleBtnPrimary(SimpleButton btn)
        {
            if (btn == null) return;
            
            btn.ShowFocusRectangle = DefaultBoolean.False;
            btn.AllowGlyphSkinning = DefaultBoolean.True; 

            btn.Appearance.BackColor = Teal;
            btn.Appearance.BorderColor = Teal;
            btn.Appearance.ForeColor = Color.White;
            btn.Appearance.Font = FontBold;
            btn.Appearance.Options.UseBackColor = true;
            btn.Appearance.Options.UseBorderColor = true;
            btn.Appearance.Options.UseForeColor = true;
            btn.Appearance.Options.UseFont = true;
            btn.ImageOptions.SvgImageSize = new Size(25, 25);
            
            // Hiệu ứng Hover làm tối màu đi
            btn.AppearanceHovered.BackColor = NavyLight;
            btn.AppearanceHovered.Options.UseBackColor = true;
        }

        public static void StyleBtnDanger(SimpleButton btn)
        {
            if (btn == null) return;

            btn.ShowFocusRectangle = DefaultBoolean.False;
            btn.AllowGlyphSkinning = DefaultBoolean.True; // Ép icon thành màu trắng

            btn.Appearance.BackColor = Danger;
            btn.Appearance.BorderColor = Danger;
            btn.Appearance.ForeColor = Color.White;
            btn.Appearance.Font = FontBold;
            btn.Appearance.Options.UseBackColor = true;
            btn.Appearance.Options.UseBorderColor = true;
            btn.Appearance.Options.UseForeColor = true;
            btn.Appearance.Options.UseFont = true;
            btn.ImageOptions.SvgImageSize = new Size(25, 25);
            
            btn.AppearanceHovered.BackColor = Color.FromArgb(200, 50, 50);
            btn.AppearanceHovered.Options.UseBackColor = true;
        }

        public static void StyleBtnWarning(SimpleButton btn)
        {
            if (btn == null) return;
            btn.ShowFocusRectangle = DefaultBoolean.False;
            btn.AllowGlyphSkinning = DefaultBoolean.True; // Ép DevExpress tẩy toàn bộ icon sang màu của ForeColor (Trắng)

            btn.Appearance.BackColor = Coral;
            btn.Appearance.ForeColor = Color.White;
            btn.Appearance.BorderColor = Coral;
            btn.Appearance.Font = FontBold;
            
            btn.Appearance.Options.UseBackColor = true;
            btn.Appearance.Options.UseForeColor = true;
            btn.Appearance.Options.UseBorderColor = true;
            btn.Appearance.Options.UseFont = true;
            btn.ImageOptions.SvgImageSize = new Size(25, 25);

            btn.AppearanceHovered.BackColor = Color.FromArgb(220, 90, 70); // Cam sậm hơn lúc hover
            btn.AppearanceHovered.Options.UseBackColor = true;
        }

        /// <summary>
        /// Nút phụ: nền trắng, viền xám, chữ Navy.
        /// </summary>
        public static void StyleBtnSecondary(SimpleButton btn)
        {
            if (btn == null) return;
            btn.Appearance.BackColor = BgCard;
            btn.Appearance.ForeColor = Navy;
            btn.Appearance.Font = FontBold;
            btn.Appearance.BorderColor = Border;
            btn.Appearance.Options.UseBackColor = true;
            btn.Appearance.Options.UseForeColor = true;
            btn.Appearance.Options.UseFont = true;
            btn.Appearance.Options.UseBorderColor = true;
            btn.ImageOptions.SvgImageSize = new Size(25, 25);
        }

        /// <summary>
        /// Nút Ghost/Outline: giữ WXI skin (bo tròn), nền trắng, chữ + viền cùng màu accent.
        /// </summary>
        public static void StyleBtnOutline(SimpleButton btn, Color accentColor)
        {
            if (btn == null) return;
            btn.Appearance.BackColor = Color.White;
            btn.Appearance.ForeColor = accentColor;
            btn.Appearance.Font = FontBold;
            btn.Appearance.BorderColor = accentColor;
            btn.Appearance.Options.UseBackColor = true;
            btn.Appearance.Options.UseFont = true;
            btn.Appearance.Options.UseBorderColor = true;
            btn.ImageOptions.SvgImageSize = new Size(25, 25);
        }


        public static void AutoStyleButton(params SimpleButton[] buttons)
        {
            foreach (var btn in buttons)
            {
                if (btn == null) continue;
                string text = btn.Text?.ToLower() ?? "";

                if (text.Contains("hủy") || text.Contains("xóa") || text.Contains("đóng") || text.Contains("trừ"))
                {
                    StyleBtnDanger(btn); // Đỏ Solid
                }
                else if (text.Contains("thanh toán") || text.Contains("thêm") || text.Contains("lưu") || text.Contains("áp dụng"))
                {
                    StyleBtnPrimary(btn); // Teal Solid (Màu chính)
                }
                else if (text.Contains("hoàn trả") || text.Contains("làm mới") || text.Contains("xóa giỏ"))
                {
                    StyleBtnSecondary(btn); 
                }
                else
                {
                    StyleBtnPrimary(btn); 
                }

                try 
                {
                    btn.Appearance.Font = FontHeader; 
                    btn.Appearance.Options.UseFont = true;
                } 
                catch {}
            }
        }

        #endregion

        #region Style form + panel

        /// <summary>
        /// Nền form sáng.
        /// </summary>
        public static void StyleForm(XtraForm frm)
        {
            if (frm == null) return;
            frm.BackColor = BgMain;
            frm.ForeColor = TextPrimary;
            frm.Font = FontDefault;
            frm.IconOptions.ShowIcon = false;
        }

        public static void StyleForm(XtraUserControl ctrl)
        {
            if (ctrl == null) return;
            ctrl.BackColor = BgMain;
            ctrl.ForeColor = TextPrimary;
            ctrl.Font = FontDefault;
        }

        /// <summary>
        /// Banner tiêu đề Navy tối + chữ trắng.
        /// </summary>
        public static void StyleBanner(System.Windows.Forms.Panel pnl, LabelControl lbl)
        {
            if (pnl == null) return;
            pnl.BackColor = Navy;

            if (lbl == null) return;
            lbl.Appearance.ForeColor = Color.White;
            lbl.Appearance.Font = FontTitle;
            lbl.Appearance.Options.UseForeColor = true;
            lbl.Appearance.Options.UseFont = true;
        }

        /// <summary>
        /// Banner tiêu đề (overload cho DevExpress PanelControl).
        /// </summary>
        public static void StyleBanner(PanelControl pnl, LabelControl lbl)
        {
            if (pnl == null) return;
            
            // Tắt LookAndFeel mặc định (WXI Skin) để cho phép hiển thị BackColor
            pnl.LookAndFeel.UseDefaultLookAndFeel = false;
            pnl.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            pnl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            
            pnl.Appearance.BackColor = Navy;
            pnl.Appearance.Options.UseBackColor = true;

            if (lbl == null) return;
            lbl.Appearance.ForeColor = Color.White;
            lbl.Appearance.Font = FontTitle;
            lbl.Appearance.Options.UseForeColor = true;
            lbl.Appearance.Options.UseFont = true;
        }

        /// <summary>
        /// Tab control: active = Teal, inactive = xám.
        /// </summary>
        public static void StyleTabControl(DevExpress.XtraTab.XtraTabControl tab)
        {
            if (tab == null) return;
            tab.AppearancePage.Header.Font = FontBold;
            tab.AppearancePage.Header.ForeColor = TextMuted;
            tab.AppearancePage.Header.Options.UseFont = true;
            tab.AppearancePage.Header.Options.UseForeColor = true;
            tab.AppearancePage.HeaderActive.Font = FontBold;
            tab.AppearancePage.HeaderActive.ForeColor = Teal;
            tab.AppearancePage.HeaderActive.Options.UseFont = true;
            tab.AppearancePage.HeaderActive.Options.UseForeColor = true;
        }

        /// <summary>
        /// Style LayoutControl: hiện label rõ ràng, chữ đậm teal cho (*).
        /// Gọi sau InitializeComponent.
        /// </summary>
        public static void StyleLayoutControl(LayoutControl lc)
        {
            if (lc == null) return;

            // Đảm bảo label item hiện chữ đen, font đậm
            foreach (BaseLayoutItem item in lc.Items)
            {
                if (item is LayoutControlItem lci && lci.TextVisible)
                {
                    lci.AppearanceItemCaption.ForeColor = TextPrimary;
                    lci.AppearanceItemCaption.Font = FontBold;
                    lci.AppearanceItemCaption.Options.UseForeColor = true;
                    lci.AppearanceItemCaption.Options.UseFont = true;
                }
            }
        }

        /// <summary>
        /// Nút lọc active: Teal.
        /// </summary>
        public static void StyleFilterBtnActive(SimpleButton btn)
        {
            if (btn == null) return;
            btn.Appearance.BackColor = Teal;
            btn.Appearance.ForeColor = Color.White;
            btn.Appearance.Font = FontBold;
            btn.Appearance.Options.UseBackColor = true;
            btn.Appearance.Options.UseForeColor = true;
            btn.Appearance.Options.UseFont = true;
        }

        /// <summary>
        /// Nút lọc inactive: nền trắng viền xám.
        /// </summary>
        public static void StyleFilterBtnInactive(SimpleButton btn)
        {
            if (btn == null) return;
            btn.Appearance.BackColor = BgCard;
            btn.Appearance.ForeColor = TextPrimary;
            btn.Appearance.Font = FontDefault;
            btn.Appearance.BorderColor = Border;
            btn.Appearance.Options.UseBackColor = true;
            btn.Appearance.Options.UseForeColor = true;
            btn.Appearance.Options.UseFont = true;
            btn.Appearance.Options.UseBorderColor = true;
        }

        /// <summary>
        /// Footer/status bar: nền NavyLight chữ trắng.
        /// </summary>
        public static void StyleStatusBar(System.Windows.Forms.Panel pnl, LabelControl lbl)
        {
            if (pnl == null) return;
            pnl.BackColor = NavyLight;

            if (lbl == null) return;
            lbl.Appearance.ForeColor = Color.LightGray;
            lbl.Appearance.Options.UseForeColor = true;
        }

        #endregion
    }
}

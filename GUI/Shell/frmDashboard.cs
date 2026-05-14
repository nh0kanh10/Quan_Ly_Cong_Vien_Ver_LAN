using System;
using System.Drawing;
using System.Windows.Forms;
using BUS.Services.HeThong;
using DevExpress.XtraEditors;
using ET.DTOs;
using GUI.Infrastructure;

namespace GUI.Shell
{
    /// <summary>
    /// Dashboard thông minh — hiển thị KPI card dựa theo quyền hạn user.
    /// Dùng CoQuyen("DASHBOARD_xxx") thống nhất, Admin thấy tất cả.
    /// </summary>
    public partial class frmDashboard : XtraUserControl
    {
        public frmDashboard()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ApplyLocalization();
            LoadDashboard();
        }

        #region Localization

        private void ApplyLocalization()
        {
            lblWelcome.Text = string.Format(
                LanguageManager.GetString("DASHBOARD_TITLE") ?? "Xin chào, {0}!",
                SessionManager.HoTen ?? "Admin");
            lblStats.Text = string.Format(
                LanguageManager.GetString("DASHBOARD_SUBTITLE") ?? "Dữ liệu từ đầu tháng {0}/{1}",
                DateTime.Today.Month, DateTime.Today.Year);
            btnLamMoi.Text = LanguageManager.GetString("BTN_LAM_MOI") ?? "Làm mới";
        }

        #endregion

        #region Load KPI Cards theo quyền

        private void LoadDashboard()
        {
            flowCards.Controls.Clear();
            flowCards.SuspendLayout();

            // Admin hoặc chưa cấu hình quyền DASHBOARD → hiển thị tất cả.
            // CoQuyen đã bypass cho Admin (TenVaiTro == "Admin").
            // Fallback: nếu LoaiTaiKhoan/TenVaiTro khác chính xác "Admin" → dùng danh sách quyền.
            System.Diagnostics.Debug.WriteLine(
                $"[Dashboard] TenVaiTro='{SessionManager.TenVaiTro}', CoQuyen(DASHBOARD)={SessionManager.CoQuyen("DASHBOARD")}");

            bool isAdmin = SessionManager.CoQuyen("DASHBOARD_TOAN_BO")
                        || SessionManager.CoQuyen("DASHBOARD"); // fallback: quyền DASHBOARD chung đã seed
            bool hasAny = false;

            // Hàm check quyền: nếu admin → true, nếu có quyền cụ thể → true
            bool Allowed(string maQuyen) => isAdmin || SessionManager.CoQuyen(maQuyen);

            //  BÁN HÀNG 
            if (Allowed("DASHBOARD_BAN_HANG"))
            {
                hasAny = true;
                AddSectionHeader(LanguageManager.GetString("DASHBOARD_SEC_BAN_HANG") ?? "💰 BÁN HÀNG");
                var res = BUS_Dashboard.Instance.LayThongKeBanHang();
                if (res.Success && res.Data != null)
                {
                    var d = res.Data;
                    AddCard(LanguageManager.GetString("KPI_DON_HOM_NAY") ?? "Đơn hôm nay",
                        d.SoDonHangHomNay.ToString("N0"), AppStyle.Teal);
                    AddCard(LanguageManager.GetString("KPI_DOANH_THU_HOM_NAY") ?? "Doanh thu hôm nay",
                        FormatTien(d.DoanhThuHomNay), AppStyle.TealLight);
                    AddCard(LanguageManager.GetString("KPI_DOANH_THU_THANG") ?? "Doanh thu tháng",
                        FormatTien(d.DoanhThuThang), AppStyle.Success);
                    AddCard(LanguageManager.GetString("KPI_DON_CHO") ?? "Đơn chờ thanh toán",
                        d.SoDonChoThanhToan.ToString("N0"),
                        d.SoDonChoThanhToan > 0 ? AppStyle.Amber : AppStyle.TealLight);
                }
                else AddErrorCard(res.ErrorMessage);
            }

            //  LƯU TRÚ 
            if (Allowed("DASHBOARD_LUU_TRU"))
            {
                hasAny = true;
                AddSectionHeader(LanguageManager.GetString("DASHBOARD_SEC_LUU_TRU") ?? "LƯU TRÚ");
                var res = BUS_Dashboard.Instance.LayThongKeLuuTru();
                if (res.Success && res.Data != null)
                {
                    var d = res.Data;
                    AddCard(LanguageManager.GetString("KPI_TONG_PHONG") ?? "Tổng phòng",
                        d.TongPhong.ToString(), AppStyle.Teal);
                    AddCard(LanguageManager.GetString("KPI_PHONG_TRONG") ?? "Phòng trống",
                        d.PhongTrong.ToString(), AppStyle.Success);
                    AddCard(LanguageManager.GetString("KPI_PHONG_DANG_O") ?? "Đang sử dụng",
                        d.PhongDangO.ToString(), AppStyle.Coral);
                    AddCard(LanguageManager.GetString("KPI_PHONG_BAO_TRI") ?? "Bảo trì",
                        d.PhongBaoTri.ToString(),
                        d.PhongBaoTri > 0 ? AppStyle.Danger : AppStyle.TealLight);
                }
                else AddErrorCard(res.ErrorMessage);
            }

            //  KHO 
            if (Allowed("DASHBOARD_KHO"))
            {
                hasAny = true;
                AddSectionHeader(LanguageManager.GetString("DASHBOARD_SEC_KHO") ?? "KHO HÀNG");
                var res = BUS_Dashboard.Instance.LayThongKeKho();
                if (res.Success && res.Data != null)
                {
                    var d = res.Data;
                    AddCard(LanguageManager.GetString("KPI_TONG_SP") ?? "Tổng sản phẩm",
                        d.TongSanPham.ToString("N0"), AppStyle.Teal);
                    AddCard(LanguageManager.GetString("KPI_SAP_HET") ?? "Sắp hết hàng",
                        d.SapHetHang.ToString(),
                        d.SapHetHang > 0 ? AppStyle.Amber : AppStyle.Success);
                    AddCard(LanguageManager.GetString("KPI_HET_HANG") ?? "Hết hàng",
                        d.HetHangHoanToan.ToString(),
                        d.HetHangHoanToan > 0 ? AppStyle.Danger : AppStyle.Success);
                    AddCard(LanguageManager.GetString("KPI_PHIEU_CHO") ?? "Phiếu chờ duyệt",
                        d.PhieuChoDuyet.ToString(),
                        d.PhieuChoDuyet > 0 ? AppStyle.Coral : AppStyle.TealLight);
                }
                else AddErrorCard(res.ErrorMessage);
            }

            //  KHÁCH HÀNG 
            if (Allowed("DASHBOARD_KHACH_HANG"))
            {
                hasAny = true;
                AddSectionHeader(LanguageManager.GetString("DASHBOARD_SEC_KHACH_HANG") ?? "KHÁCH HÀNG");
                var res = BUS_Dashboard.Instance.LayThongKeKhachHang();
                if (res.Success && res.Data != null)
                {
                    var d = res.Data;
                    AddCard(LanguageManager.GetString("KPI_TONG_KH") ?? "Tổng khách hàng",
                        d.TongKhachHang.ToString("N0"), AppStyle.Teal);
                    AddCard(LanguageManager.GetString("KPI_KH_MOI") ?? "Khách mới tháng này",
                        d.KhachMoiThang.ToString("N0"), AppStyle.TealLight);
                }
                else AddErrorCard(res.ErrorMessage);
            }

            // Nếu user không có quyền dashboard nào
            if (!hasAny)
            {
                AddSectionHeader(LanguageManager.GetString("DASHBOARD_NO_PERMISSION")
                    ?? "Chào mừng! Bạn chưa được phân quyền xem Dashboard.");
            }

            flowCards.ResumeLayout(true);
        }

        #endregion

        #region UI Card Builder

        /// <summary>
        /// Tạo 1 KPI card: số lớn + label nhỏ bên dưới, viền trái màu accent.
        /// </summary>
        private void AddCard(string label, string value, Color accentColor)
        {
            var card = new Panel
            {
                Width = 220,
                Height = 100,
                Margin = new Padding(8),
                BackColor = Color.White,
                Padding = new Padding(12, 10, 12, 10)
            };

            // Viền trái accent
            var stripe = new Panel
            {
                Width = 4,
                Dock = DockStyle.Left,
                BackColor = accentColor
            };

            var lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 22F, FontStyle.Bold),
                ForeColor = accentColor,
                AutoSize = true,
                Location = new Point(16, 10)
            };

            var lblLabel = new Label
            {
                Text = label,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(100, 100, 100),
                AutoSize = true,
                Location = new Point(16, 60),
                MaximumSize = new Size(190, 0)
            };

            card.Controls.Add(lblLabel);
            card.Controls.Add(lblValue);
            card.Controls.Add(stripe);

            // Rounded corners + shadow nhẹ
            card.Paint += (s, e) =>
            {
                var rect = card.ClientRectangle;
                rect.Inflate(-1, -1);
                using (var pen = new Pen(AppStyle.Border, 1))
                    e.Graphics.DrawRectangle(pen, rect);
            };

            flowCards.Controls.Add(card);
        }

        /// <summary>
        /// Header phân nhóm (BÁN HÀNG, KHO, LƯU TRÚ...).
        /// Full-width label chiếm hết dòng.
        /// </summary>
        private void AddSectionHeader(string text)
        {
            var lbl = new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = AppStyle.Navy,
                AutoSize = false,
                Height = 36,
                Width = flowCards.Width - 40,
                Padding = new Padding(8, 10, 0, 0),
                Margin = new Padding(8, 12, 8, 0)
            };
            flowCards.SetFlowBreak(lbl, true);
            flowCards.Controls.Add(lbl);
        }

        // Card lỗi khi query thất bại.
        private void AddErrorCard(string message)
        {
            AddCard(LanguageManager.GetString(message) ?? message, "--", AppStyle.Danger);
        }

        #endregion

        #region Helpers

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            BUS_Dashboard.Instance.XoaCache();
            LoadDashboard();
        }

        private static string FormatTien(decimal amount)
        {
            if (amount >= 1_000_000_000) return (amount / 1_000_000_000).ToString("N1") + " tỷ";
            if (amount >= 1_000_000) return (amount / 1_000_000).ToString("N1") + " tr";
            return amount.ToString("N0") + "đ";
        }

        #endregion
    }
}

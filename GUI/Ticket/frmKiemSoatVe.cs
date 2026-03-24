using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using BUS;
using DAL;
using ET;
using Guna.UI2.WinForms;
using FontAwesome.Sharp;

namespace GUI
{
    public partial class frmKiemSoatVe : Form, IBaseForm
    {
        // === State ===
        private int _countPass = 0;
        private int _countFail = 0;
        private int _flashStep = 0;
        private Color _flashColor = Color.Empty;
        private CameraScanner _cameraScanner;

        // Dark Theme Constants
        private static readonly Color BG_DARK = Color.FromArgb(18, 18, 24);
        private static readonly Color CARD_DARK = Color.FromArgb(30, 32, 42);
        private static readonly Color CARD_BORDER = Color.FromArgb(55, 60, 80);
        private static readonly Color TEXT_DIM = Color.FromArgb(120, 130, 155);
        private static readonly Color TEXT_BRIGHT = Color.FromArgb(230, 235, 245);
        private static readonly Color GREEN_PASS = Color.FromArgb(34, 197, 94);
        private static readonly Color RED_FAIL = Color.FromArgb(239, 68, 68);
        private static readonly Color AMBER_WARN = Color.FromArgb(245, 158, 11);
        private static readonly Color BLUE_INFO = Color.FromArgb(59, 130, 246);

        public frmKiemSoatVe()
        {
            InitializeComponent();
            if (ThemeManager.IsInDesignMode(this)) return;

            InitIcons();
            ApplyStyles();
            ApplyPermissions();
            LoadKhuVuc();
            UpdateCounter();
            ResetToIdle();

            // Wire events
            cboKhuVuc.SelectedIndexChanged += cboKhuVuc_SelectedIndexChanged;

            this.ActiveControl = txtScanInput;
        }

        public void ApplyPermissions() { }

        public void ApplyStyles()
        {
            this.BackColor = BG_DARK;

            // Header bar
            pnlHeader.BackColor = Color.FromArgb(24, 26, 36);
            lblGateTitle.ForeColor = TEXT_BRIGHT;
            lblGateTitle.Font = new Font("Segoe UI", 13f, FontStyle.Bold);

            // KhuVuc selector
            cboKhuVuc.BackColor = Color.FromArgb(40, 44, 58);
            cboKhuVuc.ForeColor = Color.White;
            cboKhuVuc.Font = new Font("Segoe UI", 11f, FontStyle.Bold);

            // Main feedback panel
            pnlFeedback.FillColor = CARD_DARK;
            pnlFeedback.BorderColor = CARD_BORDER;
            pnlFeedback.BorderThickness = 2;

            // Ticket info card (right side)
            pnlTicketInfo.FillColor = CARD_DARK;
            pnlTicketInfo.BorderColor = CARD_BORDER;
            pnlTicketInfo.BorderThickness = 1;

            // Counter bar
            lblCounter.BackColor = Color.FromArgb(24, 26, 36);
            lblCounter.ForeColor = TEXT_DIM;
            lblCounter.Font = new Font("Segoe UI", 10f, FontStyle.Bold);

            // History list
            lstHistory.BackColor = Color.FromArgb(22, 24, 32);
            lstHistory.ForeColor = TEXT_DIM;
            lstHistory.Font = new Font("Consolas", 9.5f);

            // Scanner textbox (invisible to user — captures keyboard input)
            txtScanner.FillColor = BG_DARK;
            txtScanner.ForeColor = BG_DARK;
            txtScanner.Font = new Font("Segoe UI", 1f);
        }

        public void InitIcons()
        {
            picStatus.Image = IconHelper.GetBitmap(IconChar.Barcode, TEXT_DIM, 100);
            picGateIcon.Image = IconHelper.GetBitmap(IconChar.ShieldHalved, BLUE_INFO, 28);
        }

        public void LoadData() { }

        private void LoadKhuVuc()
        {
            var khuVucList = BUS_KhuVuc.Instance.LoadDS();
            cboKhuVuc.DisplayMember = "TenKhuVuc";
            cboKhuVuc.ValueMember = "Id";
            cboKhuVuc.DataSource = khuVucList;
            if (khuVucList.Count > 0)
                cboKhuVuc.SelectedIndex = 0;

            LoadTroChoi();
        }

        private void cboKhuVuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTroChoi();
        }

        /// <summary>
        /// Load danh sách trò chơi (SanPham có vé) thuộc khu vực đang chọn.
        /// Item đầu = "-- Cổng chung (tất cả trò) --" -> check khu vực only.
        /// </summary>
        private void LoadTroChoi()
        {
            int? idKhuVuc = cboKhuVuc.SelectedValue as int?;
            if (!idKhuVuc.HasValue) return;

            var troChoiList = BUS_DanhSachThietBi.Instance.LoadDSTheoLoai("TroChoi")
                .Where(tc => tc.IdKhuVuc == idKhuVuc.Value)
                .ToList();

            // Insert "Cổng chung" option at top
            var displayList = new List<object>();
            displayList.Add(new { Id = (int?)null, Ten = "-- Cổng chung (tất cả trò) --" });

            // Binding trick: use anonymous type list -> need DataTable or concrete list
            cboTroChoi.Items.Clear();
            cboTroChoi.Items.Add("-- Cổng chung (tất cả trò) --");
            foreach (var tc in troChoiList)
                cboTroChoi.Items.Add(tc);

            cboTroChoi.DisplayMember = "TenThietBi";
            cboTroChoi.SelectedIndex = 0;
        }

        /// <summary>
        /// Lấy IdSanPham trò chơi đang chọn (null nếu chọn "Cổng chung")
        /// </summary>
        private int? GetSelectedTroChoiId()
        {
            if (cboTroChoi.SelectedItem is ET_DanhSachThietBi tc)
                return tc.Id;
            return null; // Cổng chung -> check khu vực only
        }

        // ============================================================
        // SCANNER INPUT
        // ============================================================

        private void txtScanner_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string code = txtScanner.Text.Trim();
                if (!string.IsNullOrEmpty(code))
                    ProcessScan(code);
                txtScanner.Clear();
                txtScanner.Focus();
            }
        }

        /// <summary>
        /// [DEMO MODE]: Ô nhập mã vé nhìn thấy được — gõ mã + Enter để quét.
        /// </summary>
        private void txtScanInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string code = txtScanInput.Text.Trim();
                if (!string.IsNullOrEmpty(code))
                    ProcessScan(code);
                txtScanInput.Clear();
                txtScanInput.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        // ============================================================
        // CORE: PROCESS SCAN
        // ============================================================

        private void ProcessScan(string code)
        {
            ET_VeDienTu ve;

            int? currentAreaId = null;
            if (cboKhuVuc.SelectedValue is int selectedId)
                currentAreaId = selectedId;

            int? idTroChoi = GetSelectedTroChoiId();

            int result = BUS_VeDienTu.Instance.CheckTicket(code, currentAreaId, out ve, idTroChoi);

            // Tên trò chơi đang gác cổng (để hiện trên feedback)
            string tenKhu = (cboKhuVuc.SelectedItem as ET_KhuVuc)?.TenKhuVuc ?? "---";
            string tenTroChoi = (cboTroChoi.SelectedItem is ET_DanhSachThietBi tc) ? tc.TenThietBi : tenKhu;

            // Resolve ticket details for info card
            string tenDV = "", maVe = code, trangThai = "", luotConLai = "";
            DateTime? ngayMua = null;

            if (ve != null)
            {
                maVe = ve.MaCode ?? code;
                trangThai = ve.TrangThai;
                luotConLai = ve.SoLuotConLai.ToString();
                ngayMua = ve.CreatedAt;

                if (ve.IdSanPham.HasValue)
                {
                    var sp = DAL_SanPham.Instance.LayTheoId(ve.IdSanPham.Value);
                    if (sp != null) tenDV = sp.Ten;
                }
            }

            switch (result)
            {
                case 0: // PASS
                    _countPass++;
                    // Check if this was a booking scan
                    var bk = BUS_VeDienTu.Instance.LastBookingResult;
                    if (bk != null && bk.Doan != null)
                    {
                        string quotaText = bk.QuotaVe != null
                            ? $"{bk.QuotaVe.SoLuongDaDung}/{bk.QuotaVe.SoLuong}"
                            : "?";
                        ShowFeedback(GREEN_PASS, IconChar.CheckCircle, "MỜI VÀO", $"🏷️ ĐOÀN: {bk.Doan.TenDoan}");
                        ShowTicketInfo(bk.Doan.MaBooking, bk.Doan.TenDoan, tenKhu, quotaText, null, GREEN_PASS, "HỢP LỆ");
                        AddToHistory(code, $"✅ ĐOÀN {quotaText}", GREEN_PASS);
                    }
                    else
                    {
                        ShowFeedback(GREEN_PASS, IconChar.CheckCircle, "MỜI VÀO", tenDV);
                        ShowTicketInfo(maVe, tenDV, tenKhu, luotConLai, ngayMua, GREEN_PASS, "HỢP LỆ");
                        AddToHistory(code, "✅ HỢP LỆ", GREEN_PASS);
                    }
                    FlashBorder(GREEN_PASS);
                    break;

                case 1: // SAI KHU VỰC
                    _countFail++;
                    ShowFeedback(RED_FAIL, IconChar.MapMarkerAlt, "SAI KHU VỰC", "Vé không thuộc trạm: " + tenKhu);
                    ShowTicketInfo(maVe, tenDV, tenKhu, luotConLai, ngayMua, AMBER_WARN, "SAI KHU");
                    AddToHistory(code, "⛔ SAI KHU", AMBER_WARN);
                    FlashBorder(RED_FAIL);
                    break;

                case 2: // HẾT LƯỢT
                    _countFail++;
                    string detail = !string.IsNullOrEmpty(tenDV) ? tenDV + " — hết lượt" : "Vé hết hạn";
                    ShowFeedback(RED_FAIL, IconChar.TimesCircle, "TỪ CHỐI", detail);
                    ShowTicketInfo(maVe, tenDV, tenKhu, "0", ngayMua, RED_FAIL, "HẾT LƯỢT");
                    AddToHistory(code, "🚫 HẾT LƯỢT", RED_FAIL);
                    FlashBorder(RED_FAIL);
                    break;

                case 3: // KHÔNG TÌM THẤY
                    _countFail++;
                    ShowFeedback(Color.FromArgb(80, 80, 95), IconChar.QuestionCircle, "KHÔNG TÌM THẤY", "Mã: " + code);
                    ClearTicketInfo();
                    AddToHistory(code, "❓ SAI MÃ", TEXT_DIM);
                    break;

                case 4: // SAI TRÒ CHƠI
                    _countFail++;
                    ShowFeedback(AMBER_WARN, IconChar.ExclamationTriangle, "SAI TRÒ CHƠI",
                        "Vé: " + tenDV + " — Trạm: " + tenTroChoi);
                    ShowTicketInfo(maVe, tenDV, tenKhu, luotConLai, ngayMua, AMBER_WARN, "SAI TRÒ");
                    AddToHistory(code, "⚠️ SAI TRÒ", AMBER_WARN);
                    FlashBorder(AMBER_WARN);
                    break;

                case 5: // ĐOÀN: HẾT QUOTA
                    _countFail++;
                    var bkFail5 = BUS_VeDienTu.Instance.LastBookingResult;
                    string doanName5 = bkFail5?.Doan?.TenDoan ?? code;
                    ShowFeedback(RED_FAIL, IconChar.Ban, "HẾT LƯỢT ĐOÀN", bkFail5?.Message ?? "Quota vé đã hết");
                    ShowTicketInfo(code, doanName5, tenKhu, "0", null, RED_FAIL, "HẾT QUOTA");
                    AddToHistory(code, "🚫 ĐOÀN HẾT VÉ", RED_FAIL);
                    FlashBorder(RED_FAIL);
                    break;

                case 6: // ĐOÀN: BOOKING INVALID
                    _countFail++;
                    var bkFail6 = BUS_VeDienTu.Instance.LastBookingResult;
                    ShowFeedback(AMBER_WARN, IconChar.ExclamationTriangle, "BOOKING LỖI", bkFail6?.Message ?? "Booking không hợp lệ");
                    ClearTicketInfo();
                    AddToHistory(code, "⚠️ BK LỖI", AMBER_WARN);
                    FlashBorder(AMBER_WARN);
                    break;
            }
            UpdateCounter();
        }

        // ============================================================
        // FEEDBACK DISPLAY (Left panel — big icon + text)
        // ============================================================

        private void ShowFeedback(Color bgColor, IconChar icon, string title, string subTitle)
        {
            pnlFeedback.FillColor = bgColor;
            pnlFeedback.BorderColor = bgColor;
            lblStatusTitle.Text = title;
            lblStatusTitle.ForeColor = Color.White;
            lblStatusTitle.Font = new Font("Segoe UI", 28f, FontStyle.Bold);
            lblStatusSub.Text = subTitle;
            lblStatusSub.ForeColor = Color.FromArgb(220, 225, 235);
            picStatus.Image = IconHelper.GetBitmap(icon, Color.White, 100);

            timerReset.Stop();
            timerReset.Start();
        }

        private void ResetToIdle()
        {
            pnlFeedback.FillColor = CARD_DARK;
            pnlFeedback.BorderColor = CARD_BORDER;
            lblStatusTitle.Text = "SẴN SÀNG";
            lblStatusTitle.ForeColor = TEXT_DIM;
            lblStatusTitle.Font = new Font("Segoe UI", 28f, FontStyle.Bold);
            lblStatusSub.Text = "Quét mã Barcode / QR trên vé";
            lblStatusSub.ForeColor = TEXT_DIM;
            picStatus.Image = IconHelper.GetBitmap(IconChar.Barcode, TEXT_DIM, 100);
        }

        private void timerReset_Tick(object sender, EventArgs e)
        {
            ResetToIdle();
            timerReset.Stop();
        }

        // ============================================================
        // TICKET INFO CARD (Right panel — ticket details)
        // ============================================================

        private void ShowTicketInfo(string maVe, string tenDV, string tenKhu, string luotConLai, DateTime? ngayMua, Color accentColor, string verdict)
        {
            pnlTicketInfo.Visible = true;
            pnlTicketInfo.BorderColor = accentColor;

            lblTicketMaVe.Text = maVe;
            lblTicketMaVe.ForeColor = TEXT_BRIGHT;

            lblTicketTenDV.Text = !string.IsNullOrEmpty(tenDV) ? tenDV : "---";
            lblTicketTenDV.ForeColor = TEXT_BRIGHT;

            lblTicketKhuVuc.Text = "📍 " + tenKhu;
            lblTicketKhuVuc.ForeColor = TEXT_DIM;

            lblTicketLuot.Text = "🎫 Lượt còn: " + luotConLai;
            lblTicketLuot.ForeColor = accentColor;

            lblTicketNgayMua.Text = ngayMua.HasValue ? "📅 " + ngayMua.Value.ToString("dd/MM/yyyy HH:mm") : "";
            lblTicketNgayMua.ForeColor = TEXT_DIM;

            lblTicketVerdict.Text = verdict;
            lblTicketVerdict.ForeColor = Color.White;
            lblTicketVerdict.BackColor = accentColor;
        }

        private void ClearTicketInfo()
        {
            lblTicketMaVe.Text = "---";
            lblTicketTenDV.Text = "";
            lblTicketKhuVuc.Text = "";
            lblTicketLuot.Text = "";
            lblTicketNgayMua.Text = "";
            lblTicketVerdict.Text = "N/A";
            lblTicketVerdict.BackColor = Color.FromArgb(60, 65, 80);
            lblTicketVerdict.ForeColor = TEXT_DIM;
            pnlTicketInfo.BorderColor = CARD_BORDER;
        }

        // ============================================================
        // FLASH BORDER ANIMATION (visual gate feedback)
        // ============================================================

        private void FlashBorder(Color color)
        {
            _flashColor = color;
            _flashStep = 0;
            timerFlash.Stop();
            timerFlash.Start();
        }

        private void timerFlash_Tick(object sender, EventArgs e)
        {
            _flashStep++;
            if (_flashStep % 2 == 0)
                this.BackColor = BG_DARK;
            else
            {
                // Solid blend: mix flash color with BG_DARK (no alpha — WinForms doesn't support transparent BackColor)
                int r = (BG_DARK.R + _flashColor.R) / 2;
                int g = (BG_DARK.G + _flashColor.G) / 2;
                int b = (BG_DARK.B + _flashColor.B) / 2;
                this.BackColor = Color.FromArgb(r, g, b);
            }

            if (_flashStep >= 4)
            {
                this.BackColor = BG_DARK;
                timerFlash.Stop();
            }
        }

        // ============================================================
        // COUNTER & HISTORY
        // ============================================================

        private void UpdateCounter()
        {
            lblCounter.Text = string.Format("   ✅ Hợp lệ: {0}     ❌ Từ chối: {1}     📊 Tổng quét: {2}", _countPass, _countFail, _countPass + _countFail);
        }

        private void AddToHistory(string code, string status, Color color)
        {
            ListViewItem item = new ListViewItem(DateTime.Now.ToString("HH:mm:ss"));
            item.SubItems.Add(code);
            item.SubItems.Add(status);
            item.ForeColor = color;
            lstHistory.Items.Insert(0, item);

            if (lstHistory.Items.Count > 50) lstHistory.Items.RemoveAt(50);
        }

        // ============================================================
        // CAMERA SCANNER (Webcam barcode/QR decode)
        // ============================================================

        private void btnToggleCamera_Click(object sender, EventArgs e)
        {
            if (_cameraScanner != null && _cameraScanner.IsRunning)
            {
                // Tắt camera
                _cameraScanner.Stop();
                picCamera.Visible = false;
                btnToggleCamera.Text = "\uD83D\uDCF7 Bật Cam";
                btnToggleCamera.FillColor = BLUE_INFO;
            }
            else
            {
                // Bật camera
                if (_cameraScanner == null)
                {
                    _cameraScanner = new CameraScanner();
                    _cameraScanner.OnBarcodeDetected += CameraScanner_OnBarcodeDetected;
                    _cameraScanner.OnError += (err) => TDCMessageBox.Show(err, "Camera");
                }

                picCamera.Visible = true;
                if (_cameraScanner.Start(picCamera))
                {
                    btnToggleCamera.Text = "\u23F9 Tắt Cam";
                    btnToggleCamera.FillColor = Color.FromArgb(220, 38, 38); // Red
                }
            }
        }

        private void CameraScanner_OnBarcodeDetected(string code)
        {
            // Gọi thẳng vào hàm xử lý quét vé hiện có (reuse 100%)
            ProcessScan(code);
            // Beep nhẹ cho feedback âm thanh
            try { System.Media.SystemSounds.Beep.Play(); } catch { }
        }

        /// <summary>
        /// [PHƯƠNG ÁN B]: Quét từ ảnh file — không cần camera vật lý.
        /// </summary>
        private void btnScanFile_Click(object sender, EventArgs e)
        {
            EnsureScanner();
            _cameraScanner.ScanFromFile();
        }

        private void EnsureScanner()
        {
            if (_cameraScanner == null)
            {
                _cameraScanner = new CameraScanner();
                _cameraScanner.OnBarcodeDetected += CameraScanner_OnBarcodeDetected;
                _cameraScanner.OnError += (err) => TDCMessageBox.Show(err, "Quét ảnh");
            }
        }
    }
}


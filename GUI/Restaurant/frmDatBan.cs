using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BUS;
using ET;
using DevExpress.XtraGrid.Views.Tile;
using FontAwesome.Sharp;

namespace GUI
{
    public partial class frmDatBan : Form
    {
        // === Menu Cache (Zero-Latency) ===
        private List<ET_SanPham> _menuCache;

        // State
        private List<ET_BanAn> _banList = new List<ET_BanAn>();
        private List<int> _selectedBanIds = new List<int>();
        private int _currentDonHangId = 0;
        private int _currentNhaHangId = 0;
        private DateTime? _thoiGianMoBan = null;

        private List<TableDisplayItem> _tableDisplayList = new List<TableDisplayItem>();

        // DTO for TileView binding
        private class TableDisplayItem
        {
            public int Id { get; set; }
            public string MaBan { get; set; }
            public string SucChuaText { get; set; }
            public string TrangThaiText { get; set; }
            public string TrangThai { get; set; }
            public Image Icon { get; set; }
            public string GhiChuCoc { get; set; } // Cọc
        }

        // Pre-rendered icons
        private Image _iconTrong;
        private Image _iconDangDung;
        private Image _iconDaDat;

        private class BillDisplayItem
        {
            public int Id { get; set; }
            public string TenMon { get; set; }
            public int SoLuong { get; set; }
            public decimal DonGia { get; set; }
            public decimal ThanhTien { get; set; }
        }

        // Trạng thái bàn
        private static readonly Dictionary<string, string> StatusLabel = new Dictionary<string, string>
        {
            { AppConstants.TrangThaiPhong.Trong,      "TRỐNG"     },
            { AppConstants.TrangThaiPhong.DangSuDung, "ĐANG DÙNG" },
            { AppConstants.TrangThaiBanAn.DaDat,      "ĐÃ ĐẶT"    }
        };

        private static readonly Dictionary<string, Color> StatusBgColor = new Dictionary<string, Color>
        {
            { AppConstants.TrangThaiPhong.Trong,      Color.FromArgb(248, 250, 252) }, // Slate-50
            { AppConstants.TrangThaiPhong.DangSuDung, Color.FromArgb(254, 243, 199) }, // Amber-100 (warm)
            { AppConstants.TrangThaiBanAn.DaDat,      Color.FromArgb(219, 234, 254) }  // Blue-100
        };

        private static readonly Dictionary<string, Color> StatusFgColor = new Dictionary<string, Color>
        {
            { AppConstants.TrangThaiPhong.Trong,      Color.FromArgb(100, 116, 139) }, 
            { AppConstants.TrangThaiPhong.DangSuDung, Color.FromArgb(180, 83, 9)    },  
            { AppConstants.TrangThaiBanAn.DaDat,      Color.FromArgb(30, 64, 175)   }  
        };
        public frmDatBan()
        {
            InitializeComponent();
            if (ThemeManager.IsInDesignMode(this)) return;

            ThemeManager.ApplyTheme(this);
            BuildLegend();

            // Pre-render table icons
            _iconTrong = IconChar.Chair.ToBitmap(Color.FromArgb(148, 163, 184), 28);
            _iconDangDung = IconChar.Utensils.ToBitmap(Color.FromArgb(180, 83, 9), 28);
            _iconDaDat = IconChar.Clock.ToBitmap(Color.FromArgb(30, 64, 175), 28);

            // Default: all buttons disabled until table selected
            UpdateButtonState(null);
        }

        // ==========================================
        // PHÍM TẮT
        // ==========================================
        private void frmDatBan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3) { btnGoiMon_Click(sender, e); e.Handled = true; }
            else if (e.KeyCode == Keys.F9) { btnThanhToan_Click(sender, e); e.Handled = true; }
        }

        // ==========================================
        // LOAD
        // ==========================================
        private void frmDatBan_Load(object sender, EventArgs e)
        {
            _menuCache = BUS_SanPham.Instance.LoadDS()
                .Where(x => x.LoaiSanPham == AppConstants.LoaiSanPham.AnUong
                         && x.TrangThai == AppConstants.TrangThaiSanPham.DangBan)
                .ToList();

            LoadNhaHang();
            timerClock.Start();
        }

        private void LoadNhaHang()
        {
            var ds = BUS_NhaHang.Instance.LoadDS();
            cboNhaHang.Items.Clear();
            cboNhaHang.DisplayMember = "TenNhaHang";
            cboNhaHang.ValueMember = "Id";
            cboNhaHang.DataSource = ds;

            if (ds.Count > 0) cboNhaHang.SelectedIndex = 0;
        }

        private void cboNhaHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = cboNhaHang.SelectedItem as ET_NhaHang;
            if (selected == null) return;

            _currentNhaHangId = selected.Id;
            _selectedBanIds.Clear();
            _currentDonHangId = 0;
            _thoiGianMoBan = null;
            LoadTableMap();
            ClearBill();
            UpdateContext(null, null);
        }

        // ==========================================
        // SƠ ĐỒ BÀN
        // ==========================================
        private void LoadTableMap()
        {
            _banList = BUS_BanAn.Instance.LoadTheoNhaHang(_currentNhaHangId);

            int trong = 0, dangDung = 0;
            _tableDisplayList = new List<TableDisplayItem>();

            foreach (var ban in _banList)
            {
                string status = ban.TrangThai ?? AppConstants.TrangThaiPhong.Trong;
                string label = status;
                string ghiChuCoc = "";

                if (!StatusLabel.TryGetValue(status, out label)) label = status;

                Image icon = _iconTrong;
                if (status == AppConstants.TrangThaiPhong.DangSuDung) icon = _iconDangDung;
                else if (status == AppConstants.TrangThaiBanAn.DaDat)
                {
                    icon = _iconDaDat;
                    var datBan = BUS_DatBan.Instance.TimDatBanActiveTheoBan(ban.Id);
                    if (datBan != null)
                    {
                        if (datBan.ThoiGianDenDuKien != DateTime.MinValue && datBan.ThoiGianDenDuKien.Year > 2000)
                        {
                            if (DateTime.Now > datBan.ThoiGianDenDuKien.AddMinutes(30))
                                label = "⚠ QUÁ GIỜ!";
                            else
                                label = "Đến: " + datBan.ThoiGianDenDuKien.ToString("HH:mm");
                        }
                        if (datBan.TienCoc > 0)
                            ghiChuCoc = "🔒 Cọc: " + datBan.TienCoc.ToString("N0");
                    }
                }

                _tableDisplayList.Add(new TableDisplayItem
                {
                    Id = ban.Id,
                    MaBan = ban.MaBan,
                    SucChuaText = string.Format("{0} người", ban.SucChua),
                    TrangThaiText = label,
                    TrangThai = status,
                    Icon = icon,
                    GhiChuCoc = ghiChuCoc
                });

                if (status == AppConstants.TrangThaiPhong.Trong) trong++;
                else if (status == AppConstants.TrangThaiPhong.DangSuDung) dangDung++;
            }

            gridTableMap.DataSource = _tableDisplayList;

            lblStats.Text = string.Format("Trống: {0}  |  Đang dùng: {1}  |  Tổng: {2}",
                trong, dangDung, _banList.Count);
        }

        private void tileViewTable_ItemCustomize(object sender, TileViewItemCustomizeEventArgs e)
        {
            var view = sender as TileView;
            if (view == null) return;

            int rowHandle = e.Item.RowHandle;
            if (!view.IsValidRowHandle(rowHandle)) return;

            string trangThai = Convert.ToString(view.GetRowCellValue(rowHandle, "TrangThai"));
            int idBan = Convert.ToInt32(view.GetRowCellValue(rowHandle, "Id"));

            Color bg;
            if (!StatusBgColor.TryGetValue(trangThai, out bg)) bg = Color.WhiteSmoke;
            Color fg;
            if (!StatusFgColor.TryGetValue(trangThai, out fg)) fg = Color.Gray;

            bool isSelected = _selectedBanIds.Contains(idBan);

            if (isSelected)
            {
                // Selected: Emerald accent border, light emerald bg
                e.Item.AppearanceItem.Normal.BackColor = Color.FromArgb(209, 250, 229); // Emerald-100
                e.Item.AppearanceItem.Normal.BorderColor = Color.FromArgb(16, 185, 129); // Emerald-500
            }
            else
            {
                e.Item.AppearanceItem.Normal.BackColor = bg;
                e.Item.AppearanceItem.Normal.BorderColor = Color.FromArgb(203, 213, 225); // Slate-300 border consistent
            }

            // Tô đỏ bàn ĐÃ ĐẶT quá giờ hẹn
            if (trangThai == AppConstants.TrangThaiBanAn.DaDat)
            {
                string trangThaiText = Convert.ToString(view.GetRowCellValue(rowHandle, "TrangThaiText"));
                if (trangThaiText != null && trangThaiText.Contains("QUÁ GIỜ"))
                {
                    e.Item.AppearanceItem.Normal.BackColor = Color.FromArgb(254, 226, 226); // Red-100
                    e.Item.AppearanceItem.Normal.BorderColor = Color.FromArgb(239, 68, 68); // Red-500
                }
            }

            for (int i = 0; i < e.Item.Elements.Count; i++)
            {
                var el = e.Item.Elements[i];
                if (i == 0) // MaBan header
                {
                    el.Appearance.Normal.ForeColor = isSelected
                        ? Color.FromArgb(6, 95, 70)   // Emerald-800
                        : Color.FromArgb(51, 65, 85);  // Slate-700
                    el.Appearance.Normal.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
                }
                else if (i == 1) // SucChua
                {
                    el.Appearance.Normal.ForeColor = Color.FromArgb(100, 116, 139); // Slate-500
                }
                else if (i == 2) // TrangThai
                {
                    el.Appearance.Normal.ForeColor = isSelected
                        ? Color.FromArgb(6, 95, 70)
                        : fg;
                }
                else if (i == 3) // GhiChuCoc
                {
                    el.Appearance.Normal.ForeColor = Color.FromArgb(234, 179, 8); // Yellow-500
                    el.Appearance.Normal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
            }
        }

        // ==========================================
        // CLICK BÀN: Single = preview, Double = chọn/bỏ chọn
        // ==========================================
        private void tileViewTable_ItemClick(object sender, TileViewItemClickEventArgs e)
        {
            // Single click = CHỈ XEM THÔNG TIN (preview), KHÔNG select
            var view = sender as TileView;
            if (view == null) return;

            int rowHandle = e.Item.RowHandle;
            string trangThai = Convert.ToString(view.GetRowCellValue(rowHandle, "TrangThai"));
            string maBan = Convert.ToString(view.GetRowCellValue(rowHandle, "MaBan"));
            int idBan = Convert.ToInt32(view.GetRowCellValue(rowHandle, "Id"));

            // Preview context header (không thay đổi selection)
            if (trangThai == AppConstants.TrangThaiPhong.DangSuDung)
            {
                // Nếu bàn đang dùng -> load bill tạm để xem
                var datBan = BUS_DatBan.Instance.TimDatBanActiveTheoBan(idBan);
                if (datBan != null && datBan.IdChiTietDonHang > 0)
                {
                    var ctdh = BUS_DatBan.Instance.LayCtdh(datBan.IdChiTietDonHang);
                    if (ctdh != null)
                    {
                        int previewDonHang = ctdh.IdDonHang;
                        var dh = BUS_DatBan.Instance.LayDonHang(previewDonHang);

                        // Nếu chưa select bàn này -> chỉ preview, không gán _currentDonHangId
                        if (!_selectedBanIds.Contains(idBan))
                        {
                            UpdateContext(maBan, trangThai);
                            if (dh != null) _thoiGianMoBan = dh.ThoiGian;
                        }
                    }
                }
            }
            else
            {
                UpdateContext(maBan, trangThai);
                // SingleClick bàn Đang Dùng-> auto-select để enable Gọi Món nhanh
                if (trangThai == AppConstants.TrangThaiPhong.DangSuDung && !_selectedBanIds.Contains(idBan))
                {
                    _selectedBanIds.Clear();
                    _selectedBanIds.Add(idBan);
                    var datBanSC = BUS_DatBan.Instance.TimDatBanActiveTheoBan(idBan);
                    if (datBanSC != null && datBanSC.IdChiTietDonHang > 0)
                    {
                        var ctdhSC = BUS_DatBan.Instance.LayCtdh(datBanSC.IdChiTietDonHang);
                        if (ctdhSC != null) _currentDonHangId = ctdhSC.IdDonHang;
                        var dhSC = BUS_DatBan.Instance.LayDonHang(_currentDonHangId);
                        _thoiGianMoBan = dhSC?.ThoiGian;
                        LoadBill();
                    }
                    tileViewTable.LayoutChanged();
                }
            }
        }

        private void tileViewTable_ItemDoubleClick(object sender, TileViewItemClickEventArgs e)
        {
            // Double click = CHỌN / BỎ CHỌN bàn
            var view = sender as TileView;
            if (view == null) return;

            int rowHandle = e.Item.RowHandle;
            int idBan = Convert.ToInt32(view.GetRowCellValue(rowHandle, "Id"));
            string trangThai = Convert.ToString(view.GetRowCellValue(rowHandle, "TrangThai"));
            string maBan = Convert.ToString(view.GetRowCellValue(rowHandle, "MaBan"));

            if (trangThai == AppConstants.TrangThaiPhong.Trong)
            {
                // Toggle chọn/bỏ chọn bàn trống (hỗ trợ ghép bàn)
                if (_selectedBanIds.Contains(idBan))
                {
                    _selectedBanIds.Remove(idBan);
                    if (_selectedBanIds.Count == 0)
                    {
                        _currentDonHangId = 0;
                        _thoiGianMoBan = null;
                        ClearBill();
                        UpdateContext(null, null);
                    }
                }
                else
                {
                    // Nếu đang có bàn DangSuDung selected -> clear trước
                    var banDangDung = _selectedBanIds.Where(id =>
                    {
                        var b = _banList.FirstOrDefault(x => x.Id == id);
                        return b != null && b.TrangThai == AppConstants.TrangThaiPhong.DangSuDung;
                    }).ToList();
                    foreach (var id in banDangDung) _selectedBanIds.Remove(id);

                    _selectedBanIds.Add(idBan);
                    _currentDonHangId = 0;
                    _thoiGianMoBan = null;
                    ClearBill();
                    UpdateContext(maBan, trangThai);
                }
            }
            else if (trangThai == AppConstants.TrangThaiPhong.DangSuDung)
            {
                if (_selectedBanIds.Contains(idBan))
                {
                    // Bỏ chọn nếu double click vào bàn đang chọn
                    _selectedBanIds.Remove(idBan);
                    _currentDonHangId = 0;
                    _thoiGianMoBan = null;
                    ClearBill();
                    UpdateContext(null, null);
                }
                else 
                {
                    // Bàn đang dùng -> exclusive select (load bill)
                    _selectedBanIds.Clear();
                    _selectedBanIds.Add(idBan);

                    var datBan = BUS_DatBan.Instance.TimDatBanActiveTheoBan(idBan);
                    if (datBan != null && datBan.IdChiTietDonHang > 0)
                    {
                        var ctdh = BUS_DatBan.Instance.LayCtdh(datBan.IdChiTietDonHang);
                        if (ctdh != null) _currentDonHangId = ctdh.IdDonHang;

                        var dh = BUS_DatBan.Instance.LayDonHang(_currentDonHangId);
                        _thoiGianMoBan = dh?.ThoiGian;

                        LoadBill();
                        UpdateContext(maBan, trangThai);
                    }
                }
            }

            tileViewTable.LayoutChanged();
        }

        // ==========================================
        // CONTEXT BINDING — Header cột phải
        // ==========================================
        private void UpdateContext(string maBan, string trangThai)
        {
            if (string.IsNullOrEmpty(maBan))
            {
                lblBanInfo.Text = "Chưa chọn bàn";
                lblTrangThaiBan.Text = "";
                lblBillTitle.Text = "🛒 BILL — Chưa chọn bàn";
                UpdateButtonState(null);
                return;
            }

            string info = string.Format("BÀN {0}", maBan);

            if (trangThai == AppConstants.TrangThaiPhong.DangSuDung && _thoiGianMoBan.HasValue)
            {
                var elapsed = DateTime.Now - _thoiGianMoBan.Value;
                info += string.Format("  |  ⏱ {0}h {1}m", (int)elapsed.TotalHours, elapsed.Minutes);
            }

            lblBanInfo.Text = info;
            lblBillTitle.Text = string.Format("🛒 BILL — Bàn {0}", maBan);

            if (trangThai == AppConstants.TrangThaiPhong.DangSuDung)
            {
                lblTrangThaiBan.Text = "● ĐANG PHỤC VỤ";
                lblTrangThaiBan.ForeColor = Color.FromArgb(252, 165, 165);
            }
            else
            {
                lblTrangThaiBan.Text = "● TRỐNG — Sẵn sàng";
                lblTrangThaiBan.ForeColor = Color.FromArgb(134, 239, 172);
            }

            UpdateButtonState(trangThai);
        }

        // ==========================================
        // CONTEXT-AWARE UI — Enable/Disable nút theo trạng thái bàn
        // ==========================================
        private void UpdateButtonState(string trangThai)
        {
            bool isTrong = (trangThai == AppConstants.TrangThaiPhong.Trong);
            bool isDangDung = (trangThai == AppConstants.TrangThaiPhong.DangSuDung);
            bool hasSelection = !string.IsNullOrEmpty(trangThai);

            // Bàn TRỐNG: chỉ bật MỞ BÀN
            btnMoBan.Enabled = isTrong;

            // Bàn ĐANG DÙNG: bật các nút thao tác
            btnGoiMon.Enabled = isDangDung;
            btnDoiBan.Enabled = isDangDung;
            btnPhuThu.Enabled = isDangDung;
            btnTraBan.Enabled = isDangDung;
            btnThanhToan.Enabled = isDangDung;
            btnInTamTinh.Enabled = isDangDung;

            // LÀM MỚI luôn bật
            btnLamMoi.Enabled = true;
        }

        // Timer tick — cập nhật thời gian ngồi
        private void timerClock_Tick(object sender, EventArgs e)
        {
            if (_thoiGianMoBan.HasValue && _selectedBanIds.Count > 0)
            {
                // Update context label mà không gọi lại DB
                string currentText = lblBanInfo.Text;
                int pipeIdx = currentText.IndexOf("|  ⏱");
                if (pipeIdx > 0)
                {
                    string banPart = currentText.Substring(0, pipeIdx);
                    var elapsed = DateTime.Now - _thoiGianMoBan.Value;
                    lblBanInfo.Text = string.Format("{0}|  ⏱ {1}h {2}m", banPart, (int)elapsed.TotalHours, elapsed.Minutes);
                }
            }

            // Cảnh báo bàn ĐÃ ĐẶT quá giờ hẹn (trigger re-render mỗi 60s)
            if (DateTime.Now.Second == 0)
            {
                bool hasOverdue = _tableDisplayList.Any(t => t.TrangThai == AppConstants.TrangThaiBanAn.DaDat);
                if (hasOverdue) tileViewTable.LayoutChanged();
            }
        }

        // ==========================================
        // BILL
        // ==========================================
        private decimal _tongTienBill = 0;

        private void LoadBill()
        {
            if (_currentDonHangId <= 0) { ClearBill(); return; }

            var chiTiets = BUS_DatBan.Instance.LayBill(_currentDonHangId);

            var displayList = new List<BillDisplayItem>();
            decimal tong = 0;
            foreach (var ct in chiTiets)
            {
                string tenMon = "Phụ thu";
                if (ct.IdSanPham.HasValue && ct.IdSanPham.Value > 0)
                {
                    var sp = _menuCache.FirstOrDefault(x => x.Id == ct.IdSanPham.Value);
                    if (sp != null) tenMon = sp.Ten;
                    else tenMon = "SP #" + ct.IdSanPham.Value;
                }

                decimal thanhTien = ct.DonGiaThucTe * ct.SoLuong;
                tong += thanhTien;

                displayList.Add(new BillDisplayItem
                {
                    Id = ct.Id,
                    TenMon = tenMon,
                    SoLuong = ct.SoLuong,
                    DonGia = ct.DonGiaThucTe,
                    ThanhTien = thanhTien
                });
            }

            gridBill.DataSource = displayList;
            gridViewBill.PopulateColumns();
            gridViewBill.OptionsBehavior.Editable = true;

            SetupDeleteColumn();

            if (gridViewBill.Columns["Id"] != null) gridViewBill.Columns["Id"].Visible = false;
            
            if (gridViewBill.Columns["DeleteBtn"] != null)
                gridViewBill.Columns["DeleteBtn"].VisibleIndex = 0;

            if (gridViewBill.Columns["TenMon"] != null) { 
                gridViewBill.Columns["TenMon"].Caption = "Món"; 
                gridViewBill.Columns["TenMon"].OptionsColumn.AllowEdit = false; 
                gridViewBill.Columns["TenMon"].VisibleIndex = 1;
            }
            if (gridViewBill.Columns["SoLuong"] != null) { 
                gridViewBill.Columns["SoLuong"].Caption = "SL"; 
                gridViewBill.Columns["SoLuong"].Width = 40; 
                gridViewBill.Columns["SoLuong"].OptionsColumn.AllowEdit = false; 
                gridViewBill.Columns["SoLuong"].VisibleIndex = 2;
            }
            if (gridViewBill.Columns["DonGia"] != null) { 
                gridViewBill.Columns["DonGia"].Caption = "Đơn giá"; 
                gridViewBill.Columns["DonGia"].DisplayFormat.FormatString = "N0"; 
                gridViewBill.Columns["DonGia"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric; 
                gridViewBill.Columns["DonGia"].OptionsColumn.AllowEdit = false; 
                gridViewBill.Columns["DonGia"].VisibleIndex = 3;
            }
            if (gridViewBill.Columns["ThanhTien"] != null) { 
                gridViewBill.Columns["ThanhTien"].Caption = "Thành tiền"; 
                gridViewBill.Columns["ThanhTien"].DisplayFormat.FormatString = "N0"; 
                gridViewBill.Columns["ThanhTien"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric; 
                gridViewBill.Columns["ThanhTien"].OptionsColumn.AllowEdit = false; 
                gridViewBill.Columns["ThanhTien"].VisibleIndex = 4;
            }

            gridViewBill.OptionsView.ColumnAutoWidth = true;

            _tongTienBill = tong;
            UpdateTotalDisplay(tong);
            lblBillTitle.Text = string.Format("🛒 CHI TIẾT BILL ({0} món)", chiTiets.Count);
        }

        private void UpdateTotalDisplay(decimal tong)
        {
            lblTongCong.Text = string.Format("TỔNG CỘNG:                                                        {0:N0} đ", tong);
            lblChietKhau.Text = string.Format("CHIẾT KHẤU:                                                               0 đ");
            lblThanhTien.Text = string.Format("THÀNH TIỀN:   {0:N0} VNĐ", tong);
        }

        private void ClearBill()
        {
            gridBill.DataSource = null;
            _tongTienBill = 0;
            UpdateTotalDisplay(0);
            lblBillTitle.Text = "🛒 BILL — Chưa chọn bàn";
        }

        // ==========================================
        // LEGEND
        // ==========================================
        private void BuildLegend()
        {
            pnlLegend.Controls.Clear();
            foreach (var kvp in StatusLabel)
            {
                Color accent;
                if (!StatusFgColor.TryGetValue(kvp.Key, out accent)) accent = Color.Gray;

                var dot = new Label();
                dot.Text = "●";
                dot.Font = new Font("Segoe UI", 12f);
                dot.ForeColor = accent;
                dot.AutoSize = true;
                dot.Margin = new Padding(8, 5, 0, 0);

                var lbl = new Label();
                lbl.Text = kvp.Value;
                lbl.Font = new Font("Segoe UI", 9f);
                lbl.ForeColor = Color.FromArgb(100, 116, 139);
                lbl.AutoSize = true;
                lbl.Margin = new Padding(0, 8, 5, 0);

                pnlLegend.Controls.Add(dot);
                pnlLegend.Controls.Add(lbl);
            }
        }

        // ==========================================
        // NÚT CHỨC NĂNG — Command Center
        // ==========================================

        // ── GọI MÓN (F3) — Popup Menu (Batch mode) ──
        private void btnGoiMon_Click(object sender, EventArgs e)
        {
            if (_currentDonHangId <= 0)
            {
                TDCMessageBox.Show("Vui lòng MỞ BÀN trước khi gọi món!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var popup = new frmMenuPopup(_menuCache))
            {
                ThemeManager.ShowAsPopup(popup);
                if (popup.DialogResult == DialogResult.OK && popup.SelectedItems.Count > 0)
                {
                    bool anyAdded = false;
                    foreach (var item in popup.SelectedItems)
                    {
                        var r = BUS_DatBan.Instance.ThemMon(_currentDonHangId, item.ProductId, item.Quantity, item.Price);
                        if (r.IsSuccess) anyAdded = true;
                    }
                    if (anyAdded) LoadBill();
                }
            }
        }

        // ── XÓA MÓN (icon thùng rác trong grid) ──
        private void SetupDeleteColumn()
        {
            // Remove existing delete column if any
            var existingCol = gridViewBill.Columns.ColumnByFieldName("DeleteBtn");
            if (existingCol != null) return; // Already set up

            var colDel = new DevExpress.XtraGrid.Columns.GridColumn();
            colDel.FieldName = "DeleteBtn";
            colDel.Caption = "";
            colDel.Width = 35;
            colDel.MaxWidth = 35;
            colDel.MinWidth = 35;
            colDel.UnboundType = DevExpress.Data.UnboundColumnType.String;
            colDel.OptionsColumn.AllowEdit = true; // MUST be true to click button
            colDel.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            colDel.VisibleIndex = 0;

            var btnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            btnEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            btnEdit.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            btnEdit.Buttons[0].Caption = "X";
            btnEdit.Buttons[0].Appearance.ForeColor = Color.FromArgb(239, 68, 68);
            btnEdit.Buttons[0].Appearance.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            btnEdit.ButtonClick += (s, ev) =>
            {
                var row = gridViewBill.GetFocusedRow() as BillDisplayItem;
                if (row == null) return;

                if (TDCMessageBox.Show(
                    string.Format("Hủy món \"{0}\"?", row.TenMon),
                    "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var r = BUS_DatBan.Instance.XoaMon(row.Id, _currentDonHangId);
                    if (r.IsSuccess) LoadBill();
                    else TDCMessageBox.Show(r.ErrorMessage, "Lỗi");
                }
            };

            colDel.ColumnEdit = btnEdit;
            gridBill.RepositoryItems.Add(btnEdit);
            gridViewBill.Columns.Add(colDel);
            colDel.VisibleIndex = 0; // Explicitly enforce index AFTER adding to column list
        }

        // ── HỦY MÓN ──
        private void btnHuyMon_Click(object sender, EventArgs e)
        {
            if (_currentDonHangId <= 0 || gridViewBill.FocusedRowHandle < 0)
            {
                TDCMessageBox.Show("Chọn dòng món cần hủy trên bill!", "Thông báo");
                return;
            }

            var row = gridViewBill.GetFocusedRow() as BillDisplayItem;
            if (row == null) return;

            if (TDCMessageBox.Show(
                string.Format("Hủy món \"{0}\"?", row.TenMon),
                "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var r = BUS_DatBan.Instance.XoaMon(row.Id, _currentDonHangId);
                if (r.IsSuccess) LoadBill();
                else TDCMessageBox.Show(r.ErrorMessage, "Lỗi");
            }
        }

        // ── MỞ BÀN ──
        private void btnMoBan_Click(object sender, EventArgs e)
        {
            if (_selectedBanIds.Count == 0)
            {
                TDCMessageBox.Show("Double-click vào bàn TRỐNG để chọn trước!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra tất cả bàn đã chọn phải TRỐNG
            var banKhongTrong = _selectedBanIds.Where(id =>
            {
                var b = _banList.FirstOrDefault(x => x.Id == id);
                return b != null && b.TrangThai != AppConstants.TrangThaiPhong.Trong;
            }).ToList();

            if (banKhongTrong.Count > 0)
            {
                TDCMessageBox.Show("Có bàn ĐANG SỬ DỤNG trong danh sách!\nChỉ được mở bàn TRỐNG.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string soKhachStr = Microsoft.VisualBasic.Interaction.InputBox(
                "Nhập số lượng khách:", "Mở bàn", "4");
            int soKhach;
            if (!int.TryParse(soKhachStr, out soKhach) || soKhach <= 0) return;

            var r = BUS_DatBan.Instance.MoBan(_currentNhaHangId, _selectedBanIds, soKhach);
            if (r.IsSuccess)
            {
                _currentDonHangId = r.Data;
                _thoiGianMoBan = DateTime.Now;
                TDCMessageBox.Show("Mở bàn thành công!\nBấm GỌI MÓN (F3) để thêm món.", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTableMap();
                LoadBill();

                // Update context
                if (_selectedBanIds.Count > 0)
                {
                    var ban = _banList.FirstOrDefault(b => b.Id == _selectedBanIds[0]);
                    if (ban != null) UpdateContext(ban.MaBan, AppConstants.TrangThaiPhong.DangSuDung);
                }
            }
            else
            {
                TDCMessageBox.Show(r.ErrorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── ĐỔI BÀN ──
        private void btnDoiBan_Click(object sender, EventArgs e)
        {
            if (_currentDonHangId <= 0 || _selectedBanIds.Count == 0)
            {
                TDCMessageBox.Show("Chọn bàn ĐANG DÙNG trước!", "Thông báo");
                return;
            }

            // Lấy danh sách bàn trống
            var banTrong = _banList.Where(b => b.TrangThai == AppConstants.TrangThaiPhong.Trong).ToList();
            if (banTrong.Count == 0)
            {
                TDCMessageBox.Show("Không còn bàn trống để đổi!", "Thông báo");
                return;
            }

            string dsBan = string.Join(", ", banTrong.Select(b => b.MaBan));
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                string.Format("Bàn trống: {0}\n\nNhập MÃ BÀN muốn đổi sang:", dsBan),
                "Đổi bàn", banTrong[0].MaBan);

            if (string.IsNullOrWhiteSpace(input)) return;

            var banMoi = banTrong.FirstOrDefault(b => b.MaBan.Equals(input.Trim(), StringComparison.OrdinalIgnoreCase));
            if (banMoi == null)
            {
                TDCMessageBox.Show("Mã bàn không hợp lệ hoặc bàn đang sử dụng!", "Lỗi");
                return;
            }

            // Đổi bàn: trạng thái bàn cũ -> Trống, bàn mới -> Đang dùng
            int idBanCu = _selectedBanIds[0];
            BUS_BanAn.Instance.CapNhatTrangThai(idBanCu, AppConstants.TrangThaiPhong.Trong);
            BUS_BanAn.Instance.CapNhatTrangThai(banMoi.Id, AppConstants.TrangThaiPhong.DangSuDung);

            _selectedBanIds.Clear();
            _selectedBanIds.Add(banMoi.Id);
            LoadTableMap();
            UpdateContext(banMoi.MaBan, AppConstants.TrangThaiPhong.DangSuDung);
            TDCMessageBox.Show(string.Format("Đã đổi sang bàn {0}!", banMoi.MaBan), "Thành công");
        }

        // ── ĐẶT BÀN TRƯỚC ──
        private void btnDatBanTruoc_Click(object sender, EventArgs e)
        {
            if (_selectedBanIds.Count == 0 || _currentNhaHangId <= 0)
            {
                TDCMessageBox.Show("Chọn (các) bàn trống cần đặt trước!", "Cảnh báo");
                return;
            }

            var banKhongTrong = _selectedBanIds.Where(id =>
            {
                var b = _banList.FirstOrDefault(x => x.Id == id);
                return b != null && b.TrangThai != AppConstants.TrangThaiPhong.Trong;
            }).ToList();

            if (banKhongTrong.Count > 0)
            {
                TDCMessageBox.Show("Chỉ được chọn bàn TRỐNG để đặt trước!", "Lỗi");
                return;
            }

            string maBans = string.Join(", ", _selectedBanIds.Select(id => _banList.FirstOrDefault(b => b.Id == id)?.MaBan));
            using (var frm = new frmDatBanTruocDialog(_currentNhaHangId, new List<int>(_selectedBanIds), maBans))
            {
                ThemeManager.ShowAsPopup(frm);
                if (frm.IsSuccess)
                {
                    _selectedBanIds.Clear();
                    LoadTableMap();
                }
            }
        }

        // ── NHẬN BÀN ĐaÃ ĐẶT ──
        private void btnNhanBan_Click(object sender, EventArgs e)
        {
            if (_selectedBanIds.Count == 0) return;
            int idBan = _selectedBanIds[0];
            var ban = _banList.FirstOrDefault(b => b.Id == idBan);
            if (ban == null || ban.TrangThai != AppConstants.TrangThaiBanAn.DaDat)
            {  
                TDCMessageBox.Show("Vui lòng chọn 1 bàn màu Xanh (Đã Đặt)!", "Lỗi");
                return;
            }

            var datBan = BUS_DatBan.Instance.TimDatBanActiveTheoBan(idBan);
            if (datBan == null) return;

            var r = BUS_DatBan.Instance.NhanBan(datBan.Id);
            if (r.IsSuccess)
            {
                TDCMessageBox.Show("Nhận bàn thành công! Đã tạo Master Bill.", "Thành công");
                _selectedBanIds.Clear();
                LoadTableMap();
            }
            else TDCMessageBox.Show(r.ErrorMessage, "Lỗi");
        }

        // ── GHÉP BÀN ──
        private void btnGhepBan_Click(object sender, EventArgs e)
        {
            if (_selectedBanIds.Count != 2)
            {
                TDCMessageBox.Show("Vui lòng giữ [CTRL] và click chọn chính xác 2 bàn Đang Phục Vụ để ghép!", "Hướng dẫn");
                return;
            }

            foreach(var id in _selectedBanIds)
            {
                var b = _banList.FirstOrDefault(x => x.Id == id);
                if (b == null || b.TrangThai != AppConstants.TrangThaiPhong.DangSuDung)
                {
                    TDCMessageBox.Show("Cả 2 bàn phải đang có khách (Màu Cam) mới có thể ghép!", "Lỗi");
                    return;
                }
            }

            int id1 = _selectedBanIds[0];
            int id2 = _selectedBanIds[1];
            string maBan1 = _banList.First(x => x.Id == id1).MaBan;
            string maBan2 = _banList.First(x => x.Id == id2).MaBan;

            int idGoc = 0;
            int idBiGhep = 0;

            using (var f = new frmGhepBanDialog(id1, maBan1, id2, maBan2))
            {
                ThemeManager.ShowAsPopup(f);
                if (!f.IsSuccess) return; 
                
                idGoc = f.IdBanGoc;
                idBiGhep = f.IdBanBiGhep;
            }

            var dbGoc = BUS_DatBan.Instance.TimDatBanActiveTheoBan(idGoc);
            var dbBiGhep = BUS_DatBan.Instance.TimDatBanActiveTheoBan(idBiGhep);
            if (dbGoc == null || dbBiGhep == null) return;

            string maBanBiGhep = _banList.First(x => x.Id == idBiGhep).MaBan;
            string maBanGoc = _banList.First(x => x.Id == idGoc).MaBan;

            var r = BUS_DatBan.Instance.GhepBan(dbGoc.Id, dbBiGhep.Id);
            if (r.IsSuccess)
            {
                TDCMessageBox.Show(string.Format("Ghép bàn thành công! (Bàn {0} đã được dọn sang Trống)", maBanBiGhep), "OK");
                _selectedBanIds.Clear();
                LoadTableMap();
            }
            else 
            {
                TDCMessageBox.Show(r.ErrorMessage, "Lỗi ghép bàn");
            }
        }

        // ── THANH TOÁN (F9) ──
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (_currentDonHangId <= 0)
            {
                TDCMessageBox.Show("Click vào bàn đỏ (đang dùng) trước!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var donHang = BUS_DatBan.Instance.LayDonHang(_currentDonHangId);
            if (donHang == null) return;

            // Tra tiền cọc đã đặt trước (nếu có)
            decimal tongCoc = BUS_DatBan.Instance.TinhTongCoc(_currentDonHangId);
            decimal tienCanThu = donHang.TongTien - tongCoc;
            if (tienCanThu < 0) tienCanThu = 0;

            string confirmMsg = string.Format("Xác nhận thanh toán?\n\nTổng tiền: {0:N0} đ", donHang.TongTien);
            if (tongCoc > 0)
                confirmMsg += string.Format("\nCọc đã đặt: -{0:N0} đ\n-> CẦN THU: {1:N0} đ", tongCoc, tienCanThu);
            
            var result = TDCMessageBox.Show(confirmMsg,
                "Thanh toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string phuongThuc = "TienMat";
                using (var f = new frmThanhToanHinhThuc())
                {
                    ThemeManager.ShowAsPopup(f);
                    if (!f.IsSuccess) return; 
                    phuongThuc = f.PhuongThuc;
                }

                var r = BUS_DatBan.Instance.ThanhToan(_currentDonHangId, _selectedBanIds, phuongThuc);
                if (r.IsSuccess)
                {
                    // === RECEIPT PREVIEW ===
                    var chiTiets = BUS_DatBan.Instance.LayBill(_currentDonHangId);
                    string receipt = "═════ HOÁ ĐƠN NHÀ HÀNG ═════\n\n";
                    foreach (var ct in chiTiets)
                    {
                        string tenMon = "Phụ thu";
                        if (ct.IdSanPham.HasValue && ct.IdSanPham.Value > 0)
                        {
                            var sp = _menuCache.FirstOrDefault(x => x.Id == ct.IdSanPham.Value);
                            if (sp != null) tenMon = sp.Ten;
                        }
                        receipt += string.Format("{0}  x{1}  {2:N0}đ\n", tenMon, ct.SoLuong, ct.DonGiaThucTe * ct.SoLuong);
                    }
                    receipt += "────────────────────\n";
                    receipt += string.Format("Tổng:          {0:N0} đ\n", donHang.TongTien);
                    if (tongCoc > 0)
                        receipt += string.Format("Cọc đã đặt:    -{0:N0} đ\n", tongCoc);
                    receipt += string.Format("THỰC THU:       {0:N0} VNĐ\n", tienCanThu);
                    receipt += string.Format("Phương thức:    {0}\n", phuongThuc);
                    receipt += "═══════════════════════";
                    TDCMessageBox.Show(receipt, "🧾 THANH TOÁN THÀNH CÔNG", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _currentDonHangId = 0;
                    _thoiGianMoBan = null;
                    _selectedBanIds.Clear();
                    ClearBill();
                    LoadTableMap();
                    UpdateContext(null, null);
                }
                else
                {
                    TDCMessageBox.Show(r.ErrorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ── PHỤ THU ──
        private void btnPhuThu_Click(object sender, EventArgs e)
        {
            if (_currentDonHangId <= 0)
            {
                TDCMessageBox.Show("Click vào bàn đỏ (đang dùng) trước!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string lyDo = Microsoft.VisualBasic.Interaction.InputBox(
                "Lý do phụ thu:", "Phụ thu / Phạt", "Vỡ ly");
            if (string.IsNullOrWhiteSpace(lyDo)) return;

            string soTienStr = Microsoft.VisualBasic.Interaction.InputBox(
                "Số tiền:", "Phụ thu / Phạt", "50000");
            decimal soTien;
            if (!decimal.TryParse(soTienStr, out soTien) || soTien <= 0) return;

            var r = BUS_DatBan.Instance.ThemPhuThu(_currentDonHangId, lyDo, soTien);
            if (r.IsSuccess)
                LoadBill();
            else
                TDCMessageBox.Show(r.ErrorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // ── TRẢ BÀN ──
        private void btnTraBan_Click(object sender, EventArgs e)
        {
            if (_selectedBanIds.Count == 0)
            {
                TDCMessageBox.Show("Chọn bàn cần trả!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra nếu bàn có bill chưa thanh toán
            if (_currentDonHangId > 0)
            {
                var donHang = BUS_DatBan.Instance.LayDonHang(_currentDonHangId);
                if (donHang != null && donHang.TongTien > 0)
                {
                    var confirm = TDCMessageBox.Show(
                        string.Format("⚠️ Bàn này CÓ BILL CHƯA THANH TOÁN!\n\n" +
                            "Tổng tiền: {0:N0} đ\n\n" +
                            "Trả bàn sẽ HỦY đơn hàng.\nBạn có chắc?", donHang.TongTien),
                        "Xác nhận trả bàn",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (confirm != DialogResult.Yes) return;

                    BUS_DatBan.Instance.HuyDonHang(_currentDonHangId);
                }
            }
            else
            {
                foreach (int idBan in _selectedBanIds)
                {
                    var datBan = BUS_DatBan.Instance.TimDatBanActiveTheoBan(idBan);
                    if (datBan != null && datBan.IdChiTietDonHang > 0)
                    {
                        var ctdh = BUS_DatBan.Instance.LayCtdh(datBan.IdChiTietDonHang);
                        var dh = ctdh != null ? BUS_DatBan.Instance.LayDonHang(ctdh.IdDonHang) : null;
                        if (dh != null && dh.TongTien > 0)
                        {
                            var confirm = TDCMessageBox.Show(
                                string.Format("⚠️ Bàn có đơn hàng {0:N0} đ CHƯA thanh toán!\n\n" +
                                    "Trả bàn sẽ HỦY đơn hàng.\nBạn có chắc?", dh.TongTien),
                                "Xác nhận",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (confirm != DialogResult.Yes) return;

                            BUS_DatBan.Instance.HuyDonHang(dh.Id);
                        }
                    }
                }
            }

            var r = BUS_DatBan.Instance.TraBan(_selectedBanIds);
            if (r.IsSuccess)
            {
                _currentDonHangId = 0;
                _thoiGianMoBan = null;
                _selectedBanIds.Clear();
                ClearBill();
                LoadTableMap();
                UpdateContext(null, null);
            }
            else
            {
                TDCMessageBox.Show(r.ErrorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── IN TẠM TÍNH ──
        private void btnInTamTinh_Click(object sender, EventArgs e)
        {
            if (_currentDonHangId <= 0)
            {
                TDCMessageBox.Show("Chưa có bill để in!", "Thông báo");
                return;
            }

            // Hiển thị preview dạng text
            var chiTiets = BUS_DatBan.Instance.LayBill(_currentDonHangId);
            string preview = "═══════ TẠM TÍNH ═══════\n\n";

            foreach (var ct in chiTiets)
            {
                string tenMon = "Phụ thu";
                if (ct.IdSanPham.HasValue && ct.IdSanPham.Value > 0)
                {
                    var sp = _menuCache.FirstOrDefault(x => x.Id == ct.IdSanPham.Value);
                    if (sp != null) tenMon = sp.Ten;
                }
                preview += string.Format("{0}  x{1}  {2:N0}đ\n", tenMon, ct.SoLuong, ct.DonGiaThucTe * ct.SoLuong);
            }

            preview += string.Format("\n────────────────────\nTỔNG: {0:N0} VNĐ\n═══════════════════════", _tongTienBill);

            TDCMessageBox.Show(preview, "🖨 Tạm Tính", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ── LÀM MỚI ──
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            _selectedBanIds.Clear();
            _currentDonHangId = 0;
            _thoiGianMoBan = null;
            ClearBill();
            LoadTableMap();
            UpdateContext(null, null);
        }
    }
}

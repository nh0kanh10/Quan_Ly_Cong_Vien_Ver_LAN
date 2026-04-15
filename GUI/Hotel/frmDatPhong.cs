using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BUS;
using ET;
using Guna.UI2.WinForms;
using FontAwesome.Sharp;

namespace GUI
{
    public partial class frmDatPhong : Form, IBaseForm
    {
        // Sử dụng struct thay vì Tuple để tương thích mọi phiên bản C# và dễ đọc hơn
        private struct RoomStatusInfo
        {
            public string Label;
            public Color Base;
            public Color Accent;
            public RoomStatusInfo(string l, Color b, Color a) { Label = l; Base = b; Accent = a; }
        }

        private List<ET_RoomMapItem> _allRooms = new List<ET_RoomMapItem>();
        private string _filterArea = "Tất cả";
        private HashSet<string> _filterStatuses = new HashSet<string>(); // empty = show all
        private ET_RoomMapItem _selectedRoom = null;
        private List<ET_RoomMapItem> _selectedGroupRooms = new List<ET_RoomMapItem>(); // Cho chế độ Khách đoàn

        private static readonly Dictionary<string, RoomStatusInfo> StatusMap
            = new Dictionary<string, RoomStatusInfo>
        {
            { ET.AppConstants.TrangThaiPhong.Trong,      new RoomStatusInfo("TRỐNG",     Color.White, Color.FromArgb(74, 137, 115)) },   // Muted Sage
            { ET.AppConstants.TrangThaiPhong.DangSuDung, new RoomStatusInfo("ĐANG Ở",    Color.White, Color.FromArgb(180, 83, 83)) },    // Muted Rose
            { ET.AppConstants.TrangThaiPhong.DaDat,      new RoomStatusInfo("ĐÃ ĐẶT",   Color.White, Color.FromArgb(180, 130, 60)) },   // Muted Amber
            { ET.AppConstants.TrangThaiPhong.DonDep,     new RoomStatusInfo("CHỜ DỌN",   Color.White, Color.FromArgb(170, 150, 60)) },   // Muted Gold
            { ET.AppConstants.TrangThaiPhong.BaoTri,     new RoomStatusInfo("BẢO TRÌ",   Color.White, Color.FromArgb(100, 116, 139)) },  // Slate-500
            { "TamKhoa",    new RoomStatusInfo("KHÓA",      Color.White, Color.FromArgb(71, 85, 105)) },    // Slate-600
        };

        private static readonly Dictionary<string, IconChar> StatusIcons = new Dictionary<string, IconChar>
        {
            { ET.AppConstants.TrangThaiPhong.Trong,      IconChar.Home },
            { ET.AppConstants.TrangThaiPhong.DangSuDung, IconChar.UserLock },
            { ET.AppConstants.TrangThaiPhong.DaDat,      IconChar.CalendarCheck },
            { ET.AppConstants.TrangThaiPhong.DonDep,     IconChar.Broom },
            { ET.AppConstants.TrangThaiPhong.BaoTri,     IconChar.Tools },
            { "TamKhoa",    IconChar.Lock },
        };

        public frmDatPhong()
        {
            InitializeComponent();
            if (ThemeManager.IsInDesignMode(this)) return;

            InitIcons();
            ApplyStyles();
            ApplyPermissions();
            LoadData();
        }

        public void ApplyPermissions()
        {
            // RBAC logic
        }

        public void ApplyStyles()
        {
            ThemeManager.ApplyTheme(this);
        }

        public void InitIcons()
        {
            // Icons được tạo động trong BuildAreaFilters và BuildStatusFilters
        }

        // 
        // LOAD DATA & BUILD FILTERS
        // 

        public void LoadData()
        {
            _allRooms = BUS_Phong.Instance.GetRoomMapData();
            BuildAreaFilters();
            BuildStatusFilters();
            ApplyFilters();
        }

        private void BuildAreaFilters()
        {
            pnlAreaFilters.Controls.Clear();

            // Lấy danh sách khu vực duy nhất từ dữ liệu
            var areas = _allRooms.Select(r => r.TenKhuVuc ?? "Chưa rõ").Distinct().OrderBy(x => x).ToList();

            // Nút "Tất cả"
            var btnAll = CreateFilterButton("Tất cả", _filterArea == "Tất cả");
            btnAll.Click += (s, e) =>
            {
                _filterArea = "Tất cả";
                HighlightAreaButton(btnAll);
                ApplyFilters();
            };
            pnlAreaFilters.Controls.Add(btnAll);

            foreach (string area in areas)
            {
                var btn = CreateFilterButton(area, _filterArea == area);
                string capturedArea = area; // Closure capture
                btn.Click += (s, e) =>
                {
                    _filterArea = capturedArea;
                    HighlightAreaButton(btn);
                    ApplyFilters();
                };
                pnlAreaFilters.Controls.Add(btn);
            }
        }

        private void BuildStatusFilters()
        {
            pnlStatusFilters.Controls.Clear();

            // Đếm số phòng theo trạng thái
            var statusCounts = _allRooms.GroupBy(r => r.TrangThai)
                .ToDictionary(g => g.Key, g => g.Count());

            foreach (var kvp in StatusMap)
            {
                string status = kvp.Key;
                string label = kvp.Value.Label;
                Color accent = kvp.Value.Accent;
                int count = statusCounts.ContainsKey(status) ? statusCounts[status] : 0;

                var badge = new Guna2Button();
                badge.Text = string.Format("{0} ({1})", label, count);
                badge.Size = new Size(120, 30);
                badge.BorderRadius = 4;
                badge.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
                badge.Margin = new Padding(3);
                badge.Cursor = Cursors.Hand;

                // Ghost style: outline + colored text
                bool isActive = _filterStatuses.Count == 0 || _filterStatuses.Contains(status);
                badge.FillColor = Color.White;
                badge.BorderThickness = isActive ? 2 : 1;
                badge.BorderColor = isActive ? accent : Color.FromArgb(203, 213, 225); // Slate-300
                badge.ForeColor = isActive ? accent : Color.FromArgb(148, 163, 184); // Slate-400

                string capturedStatus = status;
                badge.Click += (s, e) =>
                {
                    ToggleStatusFilter(capturedStatus);
                    ApplyFilters();
                };

                pnlStatusFilters.Controls.Add(badge);
            }
        }

        private void ToggleStatusFilter(string status)
        {
            if (_filterStatuses.Count == 0)
            {
                // Đang hiện tất cả -> ẩn chỉ trạng thái này
                foreach (var key in StatusMap.Keys)
                    _filterStatuses.Add(key);
                _filterStatuses.Remove(status);
            }
            else if (_filterStatuses.Contains(status))
            {
                _filterStatuses.Remove(status);
                // Nếu đã bỏ hết -> quay lại hiện tất cả (filterStatuses rỗng)
            }
            else
            {
                _filterStatuses.Add(status);
                if (_filterStatuses.Count == StatusMap.Count)
                    _filterStatuses.Clear(); // Tất cả bật = hiện tất cả
            }

            // Fix Flicker: Cập nhật màu badge tại chỗ, không rebuild từ đầu
            RefreshStatusBadgeColors();
        }

        private void RefreshStatusBadgeColors()
        {
            int i = 0;
            foreach (var kvp in StatusMap)
            {
                if (i >= pnlStatusFilters.Controls.Count) break;
                if (pnlStatusFilters.Controls[i] is Guna.UI2.WinForms.Guna2Button badge)
                {
                    bool isActive = _filterStatuses.Count == 0 || !_filterStatuses.Contains(kvp.Key);
                    badge.FillColor = Color.White;
                    badge.BorderThickness = isActive ? 2 : 1;
                    badge.BorderColor = isActive ? kvp.Value.Accent : Color.FromArgb(203, 213, 225);
                    badge.ForeColor = isActive ? kvp.Value.Accent : Color.FromArgb(148, 163, 184);
                }
                i++;
            }
        }

        // 
        // APPLY FILTERS & RENDER
        // 

        private void ApplyFilters()
        {
            string keyword = txtTimKiem.Text.Trim().ToLower();
            var filtered = _allRooms.AsEnumerable();

            // Lọc theo khu vực
            if (_filterArea != "Tất cả")
                filtered = filtered.Where(r => r.TenKhuVuc == _filterArea);

            // Lọc theo trạng thái (nếu có filter)
            if (_filterStatuses.Count > 0)
                filtered = filtered.Where(r => !_filterStatuses.Contains(r.TrangThai));

            // Lọc theo từ khóa tìm kiếm
            if (!string.IsNullOrEmpty(keyword))
            {
                filtered = filtered.Where(r =>
                    (r.TenPhong != null && r.TenPhong.ToLower().Contains(keyword)) ||
                    (r.KhachHienTai != null && r.KhachHienTai.ToLower().Contains(keyword)) ||
                    (r.SdtKhach != null && r.SdtKhach.Contains(keyword)));
            }

            RenderRoomCards(filtered.ToList());
            UpdateStats();
        }

        private void RenderRoomCards(List<ET_RoomMapItem> rooms)
        {
            pnlFloorMap.SuspendLayout();

            // Fix GDI+ leak: Dispose old cards before clearing to free memory
            foreach (Control ctrl in pnlFloorMap.Controls)
                ctrl.Dispose();
            pnlFloorMap.Controls.Clear();

            foreach (var room in rooms)
                pnlFloorMap.Controls.Add(CreateRoomCard(room));

            pnlFloorMap.ResumeLayout(true);
        }

        // 
        // STATISTICS
        // 

        private void UpdateStats()
        {
            int total = _allRooms.Count;
            int occupied = _allRooms.Count(r => r.TrangThai == ET.AppConstants.TrangThaiPhong.DangSuDung);
            int reserved = _allRooms.Count(r => r.TrangThai == ET.AppConstants.TrangThaiPhong.DaDat);
            int available = _allRooms.Count(r => r.TrangThai == ET.AppConstants.TrangThaiPhong.Trong);

            double occupancy = total > 0 ? (double)(occupied + reserved) / total * 100 : 0;
            decimal revenue = _allRooms.Where(r => r.TrangThai == ET.AppConstants.TrangThaiPhong.DangSuDung).Sum(r => r.TongTienTamTinh);

            lblStats.Text = string.Format("Công suất: {0:F0}% ({1}/{2})  |  Doanh thu tạm tính: {3}",
                occupancy, occupied, total, revenue.ToString("N0") + "đ");
        }

        // 
        // ROOM CARD CREATION
        // 

        private Control CreateRoomCard(ET_RoomMapItem room)
        {
            RoomStatusInfo info = StatusMap.ContainsKey(room.TrangThai) ? StatusMap[room.TrangThai]
                : new RoomStatusInfo("???", Color.FromArgb(248, 250, 252), Color.FromArgb(148, 163, 184));
            var icon = StatusIcons.ContainsKey(room.TrangThai) ? StatusIcons[room.TrangThai] : IconChar.QuestionCircle;

            //  FLAT CARD — No shadow, minimal border 
            Guna2Panel card = new Guna2Panel();
            card.Size = new Size(170, 110);
            card.BorderRadius = 6;
            card.BorderThickness = 1;
            card.Margin = new Padding(6);
            card.Cursor = Cursors.Hand;
            card.ShadowDecoration.Enabled = false; // NO SHADOW
            card.FillColor = Color.White;
            card.BorderColor = Color.FromArgb(226, 232, 240); // Slate-200
            card.Tag = room;

            // Nếu đang trong chế độ Khách Đoàn và phòng này đã được chọn
            bool isSelectedInGroup = _selectedGroupRooms.Any(x => x.Id == room.Id);
            if (tgGroupMode.Checked && isSelectedInGroup)
            {
                card.BorderColor = Color.FromArgb(16, 185, 129); // Xanh lá
                card.BorderThickness = 2;
                card.FillColor = Color.FromArgb(240, 253, 244); // Xanh lá nhạt
            }

            // Click handlers
            card.Click += (s, e) => HandleCardClick(card, room);
            if (!tgGroupMode.Checked)
            {
                card.ContextMenuStrip = BuildContextMenu(room, info.Accent);
            }

            //  Tùy biến UI cho Chế độ Khách Đoàn 
            // (Đã loại bỏ checkbox để form nhẹ mượt hơn, chỉ dùng Border và FillColor)

            //  Left Accent Stripe (4px color bar) 
            Panel stripe = new Panel();
            stripe.Size = new Size(4, card.Height);
            stripe.Location = new Point(0, 0);
            stripe.BackColor = info.Accent;
            stripe.Dock = DockStyle.Left;
            card.Controls.Add(stripe);

            if (!tgGroupMode.Checked)
            {
                //  Status Icon (small, top-right) 
                PictureBox pbIcon = new PictureBox();
                pbIcon.Image = IconHelper.GetBitmap(icon, info.Accent, 20);
                pbIcon.SizeMode = PictureBoxSizeMode.CenterImage;
                pbIcon.Size = new Size(28, 28);
                pbIcon.Location = new Point(card.Width - 34, 6);
                pbIcon.BackColor = Color.Transparent;
                pbIcon.Click += (s, e) => HandleCardClick(card, room);
                card.Controls.Add(pbIcon);
            }

            //  Room Name (left-aligned, bold) 
            Label lblName = new Label();
            lblName.Text = room.TenPhong;
            lblName.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblName.ForeColor = Color.FromArgb(51, 65, 85); // Slate-700
            lblName.Location = new Point(14, 10);
            lblName.AutoSize = true;
            lblName.BackColor = Color.Transparent;
            lblName.Click += (s, e) => HandleCardClick(card, room);
            card.Controls.Add(lblName);

            //  Status Text (under name) 
            Label lblStatus = new Label();
            lblStatus.Text = info.Label;
            lblStatus.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            lblStatus.ForeColor = info.Accent;
            lblStatus.Location = new Point(14, 35);
            lblStatus.AutoSize = true;
            lblStatus.BackColor = Color.Transparent;
            lblStatus.Click += (s, e) => HandleCardClick(card, room);
            card.Controls.Add(lblStatus);

            //  Extra Info Line (bottom area) 
            string extraText = "";
            Color extraColor = Color.FromArgb(148, 163, 184); // Slate-400

            if (room.TrangThai == ET.AppConstants.TrangThaiPhong.DangSuDung)
            {
                extraText = room.KhachHienTai ?? "";
                extraColor = Color.FromArgb(100, 116, 139); // Slate-500
            }
            else if (room.TrangThai == ET.AppConstants.TrangThaiPhong.Trong && room.DonGia > 0)
            {
                extraText = room.DonGia.ToString("N0") + "đ/đêm";
                extraColor = Color.FromArgb(148, 163, 184); // Slate-400
            }

            if (room.NgayNhanTiepTheo != null)
            {
                DateTime nextCheckIn = (DateTime)room.NgayNhanTiepTheo;
                bool isImminent = (nextCheckIn - DateTime.Now).TotalHours < 4;
                extraText = (isImminent ? "⚠ " : "") + nextCheckIn.ToString("HH:mm dd/MM");
                extraColor = isImminent ? Color.FromArgb(239, 68, 68) : Color.FromArgb(148, 163, 184);

                if (isImminent)
                {
                    card.BorderColor = Color.FromArgb(252, 165, 165); // Red-300
                    card.BorderThickness = 2;
                }
            }

            if (!string.IsNullOrEmpty(extraText))
            {
                Label lblExtra = new Label();
                lblExtra.Text = extraText;
                lblExtra.Font = new Font("Segoe UI", 8);
                lblExtra.ForeColor = extraColor;
                lblExtra.Location = new Point(14, 58);
                lblExtra.Size = new Size(card.Width - 30, 16);
                lblExtra.BackColor = Color.Transparent;
                lblExtra.Click += (s, e) => HandleCardClick(card, room);
                card.Controls.Add(lblExtra);
            }

            //  Price at bottom 
            if (room.DonGia > 0)
            {
                Label lblPrice = new Label();
                lblPrice.Text = room.DonGia.ToString("N0") + "đ";
                lblPrice.Font = new Font("Segoe UI", 8);
                lblPrice.ForeColor = Color.FromArgb(148, 163, 184); // Slate-400
                lblPrice.TextAlign = ContentAlignment.BottomRight;
                lblPrice.Location = new Point(card.Width - 110, card.Height - 22);
                lblPrice.Size = new Size(95, 16);
                lblPrice.BackColor = Color.Transparent;
                lblPrice.Click += (s, e) => HandleCardClick(card, room);
                card.Controls.Add(lblPrice);
            }

            return card;
        }

        // 
        // CONTEXT MENU
        // 

        private ContextMenuStrip BuildContextMenu(ET_RoomMapItem room, Color accent)
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Font = new Font("Segoe UI", 10);

            if (room.TrangThai == ET.AppConstants.TrangThaiPhong.Trong || room.TrangThai == ET.AppConstants.TrangThaiPhong.DaDat)
            {
                var checkInItem = menu.Items.Add(room.TrangThai == ET.AppConstants.TrangThaiPhong.DaDat ? "Nhận phòng (Đã đặt trước)" : "Nhận phòng (Check-in)");
                checkInItem.Image = IconHelper.GetBitmap(IconChar.SignInAlt, accent, 20);
                int? idDatPhong = room.IdDatPhongTiepTheo;
                checkInItem.Click += (s, e) => HandleCheckIn(room.Id, room.TenPhong, idDatPhong);
            }

            if (room.TrangThai == ET.AppConstants.TrangThaiPhong.Trong)
            {
                var reserveItem = menu.Items.Add("Đặt trước (Reserve)");
                reserveItem.Image = IconHelper.GetBitmap(IconChar.CalendarPlus, accent, 20);
                reserveItem.Click += (s, e) => HandleReserve(room.Id, room.TenPhong);
            }

            // [D3]: Hủy đặt phòng (chỉ khi trạng thái DaDat)
            if (room.TrangThai == ET.AppConstants.TrangThaiPhong.DaDat && room.IdDatPhongTiepTheo != null)
            {
                var cancelItem = menu.Items.Add("❌ Hủy đặt phòng");
                cancelItem.Image = IconHelper.GetBitmap(IconChar.TimesCircle, Color.FromArgb(185, 28, 28), 20);
                int idDatPhong = room.IdDatPhongTiepTheo.Value;
                cancelItem.Click += (s, e) =>
                {
                    if (TDCMessageBox.Show($"Xác nhận hủy đặt {room.TenPhong}?\nTiền cọc sẽ được hoàn lại.", "Hủy đặt phòng", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        var result = BUS_Phong.Instance.CancelReservation(idDatPhong, GetCurrentUserId());
                        if (result.IsSuccess)
                        {
                            TDCMessageBox.Show(result.ErrorMessage ?? "Hủy thành công!", "Thông báo");
                            LoadData();
                        }
                        else
                        {
                            TDCMessageBox.Show(result.ErrorMessage ?? "Hủy thất bại!", "Lỗi");
                        }
                    }
                };
            }

            if (room.TrangThai == ET.AppConstants.TrangThaiPhong.DangSuDung)
            {
                var checkOutItem = menu.Items.Add("Trả phòng (Check-out)");
                checkOutItem.Image = IconHelper.GetBitmap(IconChar.SignOutAlt, accent, 20);
                checkOutItem.Click += (s, e) => HandleCheckOut(room.Id, room.TenPhong);

                var addServiceItem = menu.Items.Add("Phụ thu / Dịch vụ");
                addServiceItem.Image = IconHelper.GetBitmap(IconChar.PlusCircle, accent, 20);
                addServiceItem.Click += (s, e) =>
                {
                    string lyDo = Microsoft.VisualBasic.Interaction.InputBox(
                        "Lý do phụ thu (minibar, giặt ủi, room service...):", "Phụ thu / Dịch vụ", "Minibar");
                    if (string.IsNullOrWhiteSpace(lyDo)) return;
                    string soTienStr = Microsoft.VisualBasic.Interaction.InputBox(
                        "Số tiền:", "Phụ thu", "50000");
                    decimal soTien;
                    if (!decimal.TryParse(soTienStr, out soTien) || soTien <= 0) return;

                    var result = BUS_Phong.Instance.AddSurcharge(room.Id, soTien, lyDo);
                    TDCMessageBox.Show(result.ErrorMessage ?? (result.IsSuccess ? "Thành công!" : "Lỗi!"), 
                        result.IsSuccess ? "Thành công" : "Lỗi");
                };
            }

            if (room.TrangThai == ET.AppConstants.TrangThaiPhong.DonDep)
            {
                var finishItem = menu.Items.Add("Hoàn tất dọn dẹp");
                finishItem.Image = IconHelper.GetBitmap(IconChar.CheckCircle, accent, 20);
                finishItem.Click += (s, e) =>
                {
                    if (BUS_Phong.Instance.FinishCleaning(room.Id)) LoadData();
                };

                var maintenanceItem = menu.Items.Add("Báo hỏng (Bảo trì)");
                maintenanceItem.Image = IconHelper.GetBitmap(IconChar.Tools, accent, 20);
                maintenanceItem.Click += (s, e) =>
                {
                    if (TDCMessageBox.Show($"Chuyển phòng {room.TenPhong} sang Bảo trì?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        var phong = BUS_Phong.Instance.LayTheoId(room.Id);
                        if (phong != null)
                        {
                            phong.TrangThai = ET.AppConstants.TrangThaiPhong.BaoTri;
                            phong.UpdatedAt = DateTime.Now;
                            BUS_Phong.Instance.Sua(phong);
                            LoadData();
                        }
                    }
                };
            }

            if (room.TrangThai == ET.AppConstants.TrangThaiPhong.BaoTri)
            {
                var fixedItem = menu.Items.Add("Đã sửa xong -> Trống");
                fixedItem.Image = IconHelper.GetBitmap(IconChar.CheckCircle, Color.Green, 20);
                fixedItem.Click += (s, e) =>
                {
                    if (BUS_Phong.Instance.FinishCleaning(room.Id)) LoadData();
                };
            }

            return menu;
        }

        // 
        // SIDEBAR DETAIL
        // 

        private void ShowRoomDetail(ET_RoomMapItem room)
        {
            _selectedRoom = room;
            lblSidebarHint.Visible = false;

            RoomStatusInfo info = StatusMap.ContainsKey(room.TrangThai) ? StatusMap[room.TrangThai]
                : new RoomStatusInfo("???", Color.White, Color.FromArgb(148, 163, 184));

            lblSidebarTitle.Text = room.TenPhong;
            lblSidebarTitle.ForeColor = Color.FromArgb(30, 41, 59); // Slate-800 always
            lblSidebarTitle.Visible = true;

            lblSidebarLoaiPhong.Text = "Loại: " + (room.TenLoaiPhong ?? "Standard");
            lblSidebarLoaiPhong.Visible = true;

            lblSidebarKhuVuc.Text = "Khu vực: " + (room.TenKhuVuc ?? "Chưa rõ");
            lblSidebarKhuVuc.Visible = true;

            lblSidebarTrangThai.Text = "● " + info.Label;
            lblSidebarTrangThai.ForeColor = info.Accent;
            lblSidebarTrangThai.Visible = true;

            // Divider line
            lblSidebarDivider.Visible = true;

            lblSidebarSucChua.Text = "Sức chứa: " + room.SucChua + " người";
            lblSidebarSucChua.Visible = true;

            lblSidebarDonGia.Text = "Đơn giá: " + room.DonGia.ToString("N0") + "đ/đêm";
            lblSidebarDonGia.Visible = true;

            // Thông tin khách (chỉ hiện khi đang sử dụng)
            if (room.TrangThai == ET.AppConstants.TrangThaiPhong.DangSuDung)
            {
                lblSidebarKhach.Text = "Khách: " + (room.KhachHienTai ?? "Khách vãng lai");
                lblSidebarKhach.Visible = true;

                lblSidebarSdt.Text = "SĐT: " + (string.IsNullOrEmpty(room.SdtKhach) ? "—" : room.SdtKhach);
                lblSidebarSdt.Visible = true;

                lblSidebarGioVao.Text = "Check-in: " + (room.NgayCheckIn?.ToString("HH:mm dd/MM") ?? "—");
                lblSidebarGioVao.Visible = true;

                lblSidebarGioRa.Text = "Dự kiến trả: " + (room.NgayTraDuKien?.ToString("HH:mm dd/MM") ?? "—");
                lblSidebarGioRa.Visible = true;
                var checkoutInfo = BUS_Phong.Instance.CalculateCheckOut(room.Id);
                decimal tamTinh = 0;
                if (checkoutInfo != null) {
                    tamTinh = Math.Max(0, checkoutInfo.TienPhongGoc + checkoutInfo.PhuThuTreGio - checkoutInfo.DaThanhToan);
                }

                lblSidebarTongTien.Text = "Tạm tính: " + tamTinh.ToString("N0") + " VNĐ";
                lblSidebarTongTien.Visible = true;
            }
            else
            {
                lblSidebarKhach.Visible = false;
                lblSidebarSdt.Visible = false;
                lblSidebarGioVao.Visible = false;
                lblSidebarGioRa.Visible = false;
                lblSidebarTongTien.Visible = false;
            }

            // Quick Action Buttons
            BuildSidebarActions(room, info.Accent);
        }

        private void BuildSidebarActions(ET_RoomMapItem room, Color accent)
        {
            pnlSidebarActions.Controls.Clear();
            pnlSidebarActions.Visible = true;

            if (room.TrangThai == ET.AppConstants.TrangThaiPhong.Trong || room.TrangThai == ET.AppConstants.TrangThaiPhong.DaDat)
            {
                string checkInLabel = room.TrangThai == ET.AppConstants.TrangThaiPhong.DaDat ? "Nhận phòng (Đã đặt)" : "Nhận phòng";
                int? idDatPhong = room.IdDatPhongTiepTheo;
                var btnCheckIn = CreateSidebarActionButton(checkInLabel, IconChar.SignInAlt, Color.FromArgb(74, 137, 115));
                btnCheckIn.Click += (s, e) => HandleCheckIn(room.Id, room.TenPhong, idDatPhong);
                pnlSidebarActions.Controls.Add(btnCheckIn);
            }

            if (room.TrangThai == ET.AppConstants.TrangThaiPhong.Trong)
            {
                var btnReserve = CreateSidebarActionButton("Đặt trước", IconChar.CalendarPlus, Color.FromArgb(180, 130, 60));
                btnReserve.Click += (s, e) => HandleReserve(room.Id, room.TenPhong);
                pnlSidebarActions.Controls.Add(btnReserve);
            }

            if (room.TrangThai == ET.AppConstants.TrangThaiPhong.DaDat && room.IdDatPhongTiepTheo != null)
            {
                int idDatPhong = room.IdDatPhongTiepTheo.Value;
                var btnCancel = CreateSidebarActionButton("Hủy đặt phòng", IconChar.TimesCircle, Color.FromArgb(180, 83, 83));
                btnCancel.Click += (s, e) =>
                {
                    if (TDCMessageBox.Show("Xác nhận hủy đặt " + room.TenPhong + "?\nTiền cọc sẽ được hoàn lại.", "Hủy đặt phòng", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        var result = BUS_Phong.Instance.CancelReservation(idDatPhong, GetCurrentUserId());
                        TDCMessageBox.Show(result.ErrorMessage ?? (result.IsSuccess ? "Hủy thành công!" : "Hủy thất bại!"), "Thông báo");
                        if (result.IsSuccess) LoadData();
                    }
                };
                pnlSidebarActions.Controls.Add(btnCancel);
            }

            if (room.TrangThai == ET.AppConstants.TrangThaiPhong.DangSuDung)
            {
                var btnCheckOut = CreateSidebarActionButton("Trả phòng", IconChar.SignOutAlt, Color.FromArgb(180, 83, 83));
                btnCheckOut.Click += (s, e) => HandleCheckOut(room.Id, room.TenPhong);
                pnlSidebarActions.Controls.Add(btnCheckOut);
                
                var btnInBill = CreateSidebarActionButton("In Folio (Demo)", IconChar.Print, Color.FromArgb(100, 116, 139));
                btnInBill.Click += (s, e) => {
                    var checkoutInfo = BUS_Phong.Instance.CalculateCheckOut(room.Id);
                    if (checkoutInfo != null)
                    {
                        decimal tongTien = Math.Max(0, checkoutInfo.TienPhongGoc + checkoutInfo.PhuThuTreGio - checkoutInfo.DaThanhToan);
                        string msg = string.Format(" ĐẠI NAM HOTEL - MASTER FOLIO \n" +
                                     "Phòng: {0}\n" +
                                     "Tiền phòng gốc: {1:N0} đ\n" +
                                     "Phạt lố giờ: {2:N0} đ\n" +
                                     "Đã đặt cọc: {3:N0} đ\n" +
                                     "------------------------------------\n" +
                                     "TỔNG THANH TOÁN (GỒM VAT): {4:N0} đ\n" +
                                     "",
                                     room.TenPhong, checkoutInfo.TienPhongGoc, checkoutInfo.PhuThuTreGio, checkoutInfo.DaThanhToan, tongTien);
                        TDCMessageBox.Show(msg, "In Hóa Đơn Trả Phòng");
                    }
                };
                pnlSidebarActions.Controls.Add(btnInBill);
            }

            if (room.TrangThai == ET.AppConstants.TrangThaiPhong.DonDep)
            {
                var btnFinish = CreateSidebarActionButton("Hoàn tất dọn dẹp", IconChar.CheckCircle, Color.FromArgb(74, 137, 115));
                btnFinish.Click += (s, e) => { if (BUS_Phong.Instance.FinishCleaning(room.Id)) LoadData(); };
                pnlSidebarActions.Controls.Add(btnFinish);
            }

            if (room.TrangThai == ET.AppConstants.TrangThaiPhong.BaoTri)
            {
                var btnFixed = CreateSidebarActionButton("Đã sửa xong", IconChar.CheckCircle, Color.FromArgb(74, 137, 115));
                btnFixed.Click += (s, e) => { if (BUS_Phong.Instance.FinishCleaning(room.Id)) LoadData(); };
                pnlSidebarActions.Controls.Add(btnFixed);
            }
        }

        private Guna2Button CreateSidebarActionButton(string text, IconChar icon, Color accentColor)
        {
            var btn = new Guna2Button();
            btn.Text = text;
            btn.Size = new Size(210, 36);
            btn.BorderRadius = 4;
            btn.BorderThickness = 1;
            btn.BorderColor = accentColor;
            btn.FillColor = Color.White;
            btn.ForeColor = accentColor;
            btn.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            btn.Image = IconHelper.GetBitmap(icon, accentColor, 16);
            btn.ImageAlign = HorizontalAlignment.Left;
            btn.TextAlign = HorizontalAlignment.Left;
            btn.Margin = new Padding(0, 3, 0, 3);
            btn.Cursor = Cursors.Hand;
            return btn;
        }

        // 
        // SEARCH EVENT
        // 

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        // 
        // HELPER: FILTER BUTTONS
        // 

        private Guna2Button CreateFilterButton(string text, bool isActive)
        {
            var btn = new Guna2Button();
            btn.Text = text;
            btn.Size = new Size(Math.Max(text.Length * 10 + 30, 90), 30);
            btn.BorderRadius = 4;
            btn.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            btn.Margin = new Padding(3);
            btn.Cursor = Cursors.Hand;

            if (isActive)
            {
                btn.FillColor = Color.FromArgb(51, 65, 85); // Slate-700
                btn.ForeColor = Color.White;
                btn.BorderThickness = 0;
            }
            else
            {
                btn.FillColor = Color.White;
                btn.ForeColor = Color.FromArgb(71, 85, 105); // Slate-600
                btn.BorderThickness = 1;
                btn.BorderColor = Color.FromArgb(203, 213, 225); // Slate-300
            }
            return btn;
        }

        private void HighlightAreaButton(Guna2Button activeBtn)
        {
            foreach (Control c in pnlAreaFilters.Controls)
            {
                if (c is Guna2Button b)
                {
                    b.FillColor = Color.White;
                    b.ForeColor = Color.FromArgb(71, 85, 105);
                    b.BorderThickness = 1;
                    b.BorderColor = Color.FromArgb(203, 213, 225);
                }
            }
            activeBtn.FillColor = Color.FromArgb(51, 65, 85);
            activeBtn.ForeColor = Color.White;
            activeBtn.BorderThickness = 0;
        }

        // 
        // HANDLERS: CHECK-IN / RESERVE / CHECK-OUT
        // 

        private int GetCurrentUserId()
        {
            if (this.Tag is ET_NhanVien tk) return tk.Id;
            var mainForm = ET.SessionManager.CurrentUser;
            if (mainForm != null) return mainForm.Id;
            return 1; // Fallback admin
        }

        private void HandleCheckIn(int idPhong, string tenPhong, int? idDatPhong = null)
        {
            using (var dial = new frmBookingDialog(idPhong, tenPhong, GetCurrentUserId(), idDatPhong))
            {
                ThemeManager.ShowAsPopup(dial);
                if (dial.IsSuccess)
                {
                    LoadData();
                }
            }
        }

        private void HandleReserve(int idPhong, string tenPhong)
        {
            using (var dial = new frmReserveDialog(idPhong, tenPhong, GetCurrentUserId()))
            {
                ThemeManager.ShowAsPopup(dial);
                if (dial.IsSuccess)
                {
                    LoadData();
                }
            }
        }

        private void HandleCheckOut(int idPhong, string tenPhong)
        {
            var info = BUS_Phong.Instance.CalculateCheckOut(idPhong);
            if (info == null)
            {
                TDCMessageBox.Show("Không tìm thấy thông tin phòng đang sử dụng!", "Lỗi");
                return;
            }

            decimal tongPhaiThu = Math.Max(0, info.TienPhongGoc + info.PhuThuTreGio - info.DaThanhToan);

            // [D1]: Build thông tin chi tiết hơn
            string thongTin = $"Xác nhận trả phòng {tenPhong}?";
            thongTin += $"\n\n🕑 Check-in: {info.NgayNhan:HH:mm dd/MM}";
            thongTin += $"\n🕒 Check-out: {DateTime.Now:HH:mm dd/MM}";
            thongTin += $"\n\n💰 Tiền phòng gốc: {info.TienPhongGoc:N0} VNĐ";
            if (info.PhuThuTreGio > 0)
                thongTin += $"\n⏰ Phụ thu trễ ({info.SoGioTre:F1}h): +{info.PhuThuTreGio:N0} VNĐ";
            thongTin += $"\n✅ Đã thanh toán: -{info.DaThanhToan:N0} VNĐ";
            if (tongPhaiThu > 0)
                thongTin += $"\n\n➡️ Cần thu thêm: {tongPhaiThu:N0} VNĐ";
            else
                thongTin += "\n\n✔️ Không cần thu thêm.";

            if (TDCMessageBox.Show(thongTin, "Check-out", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            // [D1]: Chọn phương thức thanh toán (nếu cần thu thêm)
            string phuongThuc = "TienMat";
            if (tongPhaiThu > 0)
            {
                var ptResult = TDCMessageBox.Show(
                    $"Thu {tongPhaiThu:N0} VNĐ bằng phương thức nào?\n\n• YES = Tiền mặt / Chuyển khoản\n• NO = Ví RFID",
                    "Phương thức thanh toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                phuongThuc = ptResult == DialogResult.Yes ? "TienMat" : AppConstants.PhuongThucThanhToan.ViRfid;
            }

            var result = BUS_Phong.Instance.ConfirmCheckOut(idPhong, tongPhaiThu, phuongThuc, GetCurrentUserId());

            if (result.IsSuccess)
            {
                TDCMessageBox.Show("Trả phòng thành công!", "Thông báo");
                LoadData();
            }
            else
            {
                TDCMessageBox.Show(result.ErrorMessage ?? "Lỗi khi trả phòng.", "Lỗi");
            }
        }
        // 
        // GROUP MODE LOGIC & EVENTS
        // 

        private void HandleCardClick(Guna2Panel card, ET_RoomMapItem room)
        {
            if (tgGroupMode.Checked)
            {
                if (room.TrangThai != ET.AppConstants.TrangThaiPhong.Trong && room.TrangThai != ET.AppConstants.TrangThaiPhong.DaDat)
                {
                    string msg = string.Format("Phòng {0} đang ở trạng thái {1}. Chỉ được chọn phòng TRỐNG để đặt trước, hoặc phòng ĐÃ ĐẶT để nhận phòng đoàn.", room.TenPhong, room.TrangThai);
                    TDCMessageBox.Show(msg, "Lỗi chọn phòng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }

                // Toggle selection logic in Group Mode (MƯỢT/KHÔNG RE-RENDER)
                var existing = _selectedGroupRooms.FirstOrDefault(x => x.Id == room.Id);
                if (existing != null)
                {
                    _selectedGroupRooms.Remove(existing);
                    // Cập nhật giao diện trực tiếp trên thẻ đang click (KHÔNG load lại toàn bộ danh sách)
                    card.BorderColor = Color.FromArgb(226, 232, 240); // Slate-200
                    card.BorderThickness = 1;
                    card.FillColor = Color.White;
                }
                else
                {
                    _selectedGroupRooms.Add(room);
                    // Cập nhật giao diện trực tiếp trên thẻ đang click
                    card.BorderColor = Color.FromArgb(16, 185, 129); // Xanh lá
                    card.BorderThickness = 2;
                    card.FillColor = Color.FromArgb(240, 253, 244); // Xanh lá nhạt
                }
                
                UpdateGroupBottomBar();
            }
            else
            {
                // Normal mode
                ShowRoomDetail(room);
            }
        }

        private void tgGroupMode_CheckedChanged(object sender, EventArgs e)
        {
            _selectedGroupRooms.Clear();
            _selectedRoom = null;
            pnlSidebar.Visible = !tgGroupMode.Checked; // Ẩn sidebar khi vô Group Mode
            pnlBottomAction.Visible = tgGroupMode.Checked; // Hiện BottomBar
            splitMain.Collapsed = tgGroupMode.Checked; // DevExpress v21.2 SplitContainerControl 
            
            // Khi thoát Group Mode, reset sidebar về trạng thái hint
            if (!tgGroupMode.Checked)
            {
                lblSidebarHint.Visible = true;
                lblSidebarHint.Text = "← Click vào một phòng để xem chi tiết";
                lblSidebarTitle.Text = "";
                lblSidebarTitle.Visible = false;
                lblSidebarLoaiPhong.Text = "";
                lblSidebarTrangThai.Text = "";
                lblSidebarDonGia.Text = "";
            }
            
            UpdateGroupBottomBar();
            ApplyFilters(); // Re-render to show/hide checkboxes
        }

        private void UpdateGroupBottomBar()
        {
            if (!tgGroupMode.Checked) return;
            
            lblSelectedCount.Text = $" ĐÃ CHỌN {_selectedGroupRooms.Count} PHÒNG ({string.Join(", ", _selectedGroupRooms.Select(x => x.TenPhong))})";
            decimal total = _selectedGroupRooms.Sum(x => x.DonGia);
            lblTempTotal.Text = $"|  Tổng tiền tạm: {total:N0} đ/đêm";
        }

        private void btnCancelSelection_Click(object sender, EventArgs e)
        {
            _selectedGroupRooms.Clear();
            UpdateGroupBottomBar();
            ApplyFilters();
        }

        private void btnReserveGroup_Click(object sender, EventArgs e)
        {
            if (_selectedGroupRooms.Count == 0)
            {
                TDCMessageBox.Show("Vui lòng chọn ít nhất 1 phòng để Đặt Cọc Đoàn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // Chỉ chọn những phòng đang TRỐNG để Đặt Trước
            if (_selectedGroupRooms.Any(r => r.TrangThai != ET.AppConstants.TrangThaiPhong.Trong)) {
                TDCMessageBox.Show("Đặt trước Nhóm chỉ áp dụng cho nhóm các phòng đang [TRỐNG].\nVui lòng bỏ chọn các phòng đang trạng thái khác.", "Sai trạng thái", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var dial = new frmDatPhongDoanDialog(_selectedGroupRooms, GetCurrentUserId()))
            {
                ThemeManager.ShowAsPopup(dial);
                if (dial.IsSuccess)
                {
                    tgGroupMode.Checked = false; // Thoát chế độ đoàn sau khi hoàn tất
                    LoadData(); // Phục hồi lưới
                }
            }
        }

        private void btnCheckInGroup_Click(object sender, EventArgs e)
        {
            if (_selectedGroupRooms.Count == 0)
            {
                TDCMessageBox.Show("Vui lòng chọn ít nhất 1 phòng để Nhận Phòng Đoàn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // Lọc ra các phòng đang Đã Đặt có chứa thông tin DatCho
            var dpcts = _selectedGroupRooms
                .Where(r => r.TrangThai == ET.AppConstants.TrangThaiPhong.DaDat && r.IdDatPhongTiepTheo.HasValue)
                .Select(r => r.IdDatPhongTiepTheo.Value)
                .ToList();
                
            if (dpcts.Count == 0)
            {
                TDCMessageBox.Show("Chỉ có thể Nhận Phòng (Check-in) cho các Phòng đang ở trạng thái [ĐÃ ĐẶT].\nVui lòng bỏ chọn các phòng trống hoặc đã có khách.", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (MessageBox.Show($"Xác nhận Check-in cho {dpcts.Count} phòng đã được đặt trước dành cho đoàn?", "Xác nhận Nhận Phòng", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var result = BUS_Phong.Instance.CheckInGroup(dpcts, GetCurrentUserId());
                if (result.IsSuccess)
                {
                    TDCMessageBox.Show("Thành công! Toàn bộ phòng Đoàn đã được bàn giao (Check-in).", "Hoàn tất");
                    tgGroupMode.Checked = false; // Thoát chế độ đoàn sau khi hoàn tất
                    LoadData(); // Phục hồi lưới
                }
                else
                {
                    TDCMessageBox.Show(result.ErrorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}




using BUS;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ET;

namespace GUI
{
    public partial class frmKhachHang : Form, IBaseForm, AI.IAIFormContext, AI.IAICommandHandler
    {
        // ── AI Context ──
        public string AIContextName => "frmKhachHang";
        public string AIContextDescription =>
            "BẠN ĐANG TƯ VẤN Ở MÀN HÌNH KHÁCH HÀNG.\n" +
            "1. TÌM KIẾM: Hỏi chung chung ưu tiên `get_customers`.\n" +
            "2. TỰ ĐỘNG CHỌN (Proactive Select): Nếu hỏi 1 người cụ thể hoặc kêu chọn -> Lấy SĐT -> GỌI LUÔN `ui_select_customer` để đổ hồ sơ người đó lên UI.\n" +
            "3. LỊCH SỬ GIAO DỊCH VÍ: Dùng `get_customer_transactions` để xem chi tiết tiền ra/vào của 1 khách.\n" +
            "4. NẠP TIỀN (Thao tác): Nếu người dùng yêu cầu Nạp Tiền cho 1 khách -> Dùng SĐT của khách -> GỌI NGAY `ui_open_recharge_modal` để hệ thống tự bật bảng nạp tiền lên.\n" +
            "5. SẮP XẾP LƯỚI (Thao tác): Nếu người dùng kêu 'sắp xếp theo...' -> GỌI NGAY `ui_sort_customers` (cột: TongChiTieu, DiemTichLuy, HoTen) để lưới dữ liệu tự đổi.";

        public void ExecuteAICommand(string commandName, Dictionary<string, object> args)
        {
            if (commandName == "ui_select_customer")
            {
                if (args.TryGetValue("sdt", out var sdtObj))
                {
                    string sdt = sdtObj?.ToString();
                    if (!string.IsNullOrEmpty(sdt))
                    {
                        for (int i = 0; i < gridViewKhachHang.RowCount; i++)
                        {
                            var rowId = gridViewKhachHang.GetRowCellValue(i, "DienThoai")?.ToString();
                            if (rowId == sdt)
                            {
                                gridViewKhachHang.FocusedRowHandle = i;
                                break;
                            }
                        }
                    }
                }
            }
            else if (commandName == "ui_open_recharge_modal")
            {
                if (args.TryGetValue("sdt", out var sdtObj))
                {
                    string sdt = sdtObj?.ToString();
                    if (!string.IsNullOrEmpty(sdt))
                    {
                        // 1. Tự động chọn khách hàng
                        ExecuteAICommand("ui_select_customer", args);

                        // 2. Mở bảng nạp tiền sau khi UI đã chọn xong
                        this.BeginInvoke(new Action(() => {
                           BtnNapTien_Click(null, null);
                        }));
                    }
                }
            }
            else if (commandName == "ui_sort_customers")
            {
                if (args.TryGetValue("sort_column", out var colObj) && args.TryGetValue("sort_direction", out var dirObj))
                {
                    string col = colObj?.ToString();
                    string dir = dirObj?.ToString();

                    this.BeginInvoke(new Action(() => {
                        if (gridViewKhachHang.Columns[col] != null)
                        {
                            gridViewKhachHang.ClearSorting();
                            gridViewKhachHang.Columns[col].SortOrder = 
                                dir.Equals("ASC", StringComparison.OrdinalIgnoreCase) 
                                ? DevExpress.Data.ColumnSortOrder.Ascending 
                                : DevExpress.Data.ColumnSortOrder.Descending;
                        }
                    }));
                }
            }
        }

        private ET_KhachHang _selectedKH;
        private ET_ViDienTu _selectedVi;
        private ET_TheRFID _selectedThe;
        private Customer.ucCustomerInfo _ucInfo;

        public frmKhachHang()
        {
            InitializeComponent();
            gridViewDiem.CustomColumnDisplayText += gridViewDiem_CustomColumnDisplayText;
            gridViewDiem.RowCellStyle += gridViewDiem_RowCellStyle;
        }

        private void gridViewDiem_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "SoDiem" && e.Value != null)
            {
                if (int.TryParse(e.Value.ToString(), out int soDiem))
                {
                    var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                    string loaiGD = view.GetListSourceRowCellValue(e.ListSourceRowIndex, "LoaiGiaoDich")?.ToString() ?? "";
                    
                    if (loaiGD == ET.AppConstants.LoaiGiaoDichDiem.TichLuy || (loaiGD == ET.AppConstants.LoaiGiaoDichDiem.DieuChinh && soDiem > 0))
                        e.DisplayText = "+ " + soDiem.ToString("N0") + " pts";
                    else if (soDiem < 0)
                        e.DisplayText = "- " + Math.Abs(soDiem).ToString("N0") + " pts";
                    else
                        e.DisplayText = soDiem.ToString("N0") + " pts";
                }
            }
        }

        private void gridViewDiem_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "SoDiem")
            {
                var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                string loaiGD = view.GetRowCellValue(e.RowHandle, "LoaiGiaoDich")?.ToString() ?? "";
                
                int soDiem = 0;
                var cellVal = view.GetRowCellValue(e.RowHandle, "SoDiem");
                if (cellVal != null) int.TryParse(cellVal.ToString(), out soDiem);
                
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far; 

                if (loaiGD == ET.AppConstants.LoaiGiaoDichDiem.DieuChinh)
                {
                    e.Appearance.ForeColor = Color.FromArgb(217, 119, 6); 
                }
                else if (loaiGD == ET.AppConstants.LoaiGiaoDichDiem.TichLuy || soDiem > 0)
                {
                    e.Appearance.ForeColor = Color.FromArgb(5, 150, 105); 
                }
                else
                {
                    e.Appearance.ForeColor = Color.FromArgb(220, 38, 38); 
                }
            }
        }

       

        public void ApplyStyles()
        {
            ThemeManager.ApplyTheme(this);

            // Override: KPI numbers use monospaced font
            lblSoDu.Font = new Font("Cascadia Code", 20f, FontStyle.Bold);
            lblSoDu.ForeColor = ThemeManager.PrimaryColor;
            lblDiemTichLuy.Font = new Font("Cascadia Code", 20f, FontStyle.Bold);
            lblDiemTichLuy.ForeColor = Color.FromArgb(212, 175, 55); // Gold cho điểm phụ
            lblTongChiTieu.Font = new Font("Cascadia Code", 20f, FontStyle.Bold);
            lblTongChiTieu.ForeColor = ThemeManager.PrimaryColor; // Primary cho tiền (Main KPI)
            lblTenKH.Font = new Font("Segoe UI Semibold", 18f, FontStyle.Bold);
            // Override: Top bar stays white after theme
            pnlTopBar.FillColor = Color.White;
            pnlCustomerHeader.FillColor = Color.White;

            // Override: Keep tabDetails in dark theme (do not let ThemeManager reset it)
            tabDetails.TabMenuBackColor = Color.FromArgb(33, 42, 57);
            
            tabDetails.TabButtonIdleState.FillColor = Color.FromArgb(33, 42, 57);
            tabDetails.TabButtonIdleState.ForeColor = Color.FromArgb(156, 160, 167);
            tabDetails.TabButtonIdleState.InnerColor = Color.FromArgb(33, 42, 57);

            tabDetails.TabButtonSelectedState.FillColor = Color.FromArgb(29, 37, 49);
            tabDetails.TabButtonSelectedState.ForeColor = Color.White;
            tabDetails.TabButtonSelectedState.InnerColor = Color.FromArgb(76, 132, 255);

            tabDetails.TabButtonHoverState.FillColor = Color.FromArgb(40, 52, 70);
            tabDetails.TabButtonHoverState.ForeColor = Color.White;
            tabDetails.TabButtonHoverState.InnerColor = Color.FromArgb(40, 52, 70);

            tabGiaoDich.BackColor = Color.FromArgb(29, 37, 49);
            tabDiem.BackColor = Color.FromArgb(29, 37, 49);
            tabSuCo.BackColor = Color.FromArgb(29, 37, 49);

            // Keep CTA buttons colored (theme turns them ghost)

            btnKhoaThe.BorderColor = System.Drawing.Color.FromArgb(220, 38, 38);
            btnKhoaThe.BorderThickness = 1;
            btnKhoaThe.FillColor = System.Drawing.Color.Transparent;
            btnKhoaThe.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            btnKhoaThe.HoverState.FillColor = System.Drawing.Color.FromArgb(220, 38, 38);
            btnKhoaThe.HoverState.ForeColor = System.Drawing.Color.White;

            btnNapTien.FillColor = Color.FromArgb(5, 150, 105);
            btnNapTien.ForeColor = Color.White;
            btnNapTien.BorderColor = Color.FromArgb(5, 150, 105);
            btnNapTien.HoverState.FillColor = Color.FromArgb(4, 120, 87);
            btnNapTien.HoverState.ForeColor = Color.White;

            btnThemKhachMoi.FillColor = Color.FromArgb(30, 64, 175);
            btnThemKhachMoi.ForeColor = Color.White;
            btnXoaKH.FillColor = Color.FromArgb(239, 68, 68);
            btnXoaKH.ForeColor = Color.White;

            if (btnSuaDiem != null)
            {
                btnSuaDiem.BorderColor = Color.FromArgb(217, 119, 6);
                btnSuaDiem.BorderThickness = 1;
                btnSuaDiem.FillColor = Color.Transparent;
                btnSuaDiem.ForeColor = Color.FromArgb(217, 119, 6);
                btnSuaDiem.HoverState.FillColor = Color.FromArgb(217, 119, 6);
                btnSuaDiem.HoverState.ForeColor = Color.White;
            }
        }

        public void ApplyPermissions()
        {
            var tk = SessionManager.CurrentUser;
            if (tk == null) return;
            bool canManage = BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "MANAGE_CUSTOMER");
            btnThemKhachMoi.Enabled = canManage;
            btnXoaKH.Enabled = canManage;
            btnNapTien.Enabled = canManage;
            btnKhoaThe.Enabled = canManage;
            btnCapVi.Enabled = canManage;
        }

        public void InitIcons() { }

        public void LoadData()
        {
            CreateInfoPanel();
            LoadDanhSachKH();
            // Show all controls from start — user will select a customer to populate data
        }

        private void CreateInfoPanel()
        {
            _ucInfo = new Customer.ucCustomerInfo();
            _ucInfo.Dock = DockStyle.Top;
            _ucInfo.Margin = new Padding(0, 5, 0, 5);
            pnlDetail.Controls.Add(_ucInfo);
            // Z-order: insert after pnlKpiStrip (index 2) so it renders between Header and KPI
            // pnlDetail order: tabDetails(0), pnlActions(1), pnlKpiStrip(2), pnlCustomerHeader(3), lblNoSelection(4)
            // Insert at index 3 -> between KPI and Header
            pnlDetail.Controls.SetChildIndex(_ucInfo, 3);
        }

        // 
        //  MASTER GRID — Danh sách khách hàng (Lazy: chỉ load info cơ bản)
        // 

        private void LoadDanhSachKH(string keyword = "")
        {
            var ds = string.IsNullOrWhiteSpace(keyword)
                ? BUS_KhachHang.Instance.LoadDS()
                : BUS_KhachHang.Instance.TimKiem(keyword);

            gridKhachHang.DataSource = ds;
            gridViewKhachHang.PopulateColumns();

            // Configure columns
            var v = gridViewKhachHang;
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in v.Columns)
                col.Visible = false;

            ShowColumn(v, "MaCode", "Mã KH", 85);
            ShowColumn(v, "HoTen", "Họ Tên", 160);
            ShowColumn(v, "DienThoai", "SĐT", 100);
            ShowColumn(v, "LoaiKhach", "Phân hạng", 80);

            if (v.Columns["MaCode"] != null) v.Columns["MaCode"].VisibleIndex = 0;
            if (v.Columns["HoTen"] != null) v.Columns["HoTen"].VisibleIndex = 1;
            if (v.Columns["DienThoai"] != null) v.Columns["DienThoai"].VisibleIndex = 2;
            if (v.Columns["LoaiKhach"] != null) v.Columns["LoaiKhach"].VisibleIndex = 3;

            v.OptionsView.ColumnAutoWidth = true;
        }

        private void ShowColumn(DevExpress.XtraGrid.Views.Grid.GridView view, string fieldName, string caption, int width)
        {
            var col = view.Columns[fieldName];
            if (col == null) return;
            col.Visible = true;
            col.Caption = caption;
            col.Width = width;
            col.OptionsColumn.AllowEdit = false;
        }

        // ══════════════════════════════════════════════════════════════
        // HELPERS CHO TYPOGRAPHY
        // ══════════════════════════════════════════════════════════════
        private void AddUnitLabel(Label targetLabel, string unitText)
        {
            var existing = targetLabel.Parent.Controls.OfType<Label>().FirstOrDefault(c => c.Name == targetLabel.Name + "_Unit");
            if (existing != null) targetLabel.Parent.Controls.Remove(existing);

            Label lblUnit = new Label();
            lblUnit.Name = targetLabel.Name + "_Unit";
            lblUnit.Text = unitText;
            lblUnit.AutoSize = true;
            lblUnit.Font = new Font(targetLabel.Font.FontFamily, targetLabel.Font.Size * 0.5f, FontStyle.Regular);
            lblUnit.ForeColor = Color.DimGray;
            lblUnit.BackColor = Color.Transparent;
            
            targetLabel.Parent.Controls.Add(lblUnit);
            lblUnit.BringToFront();
            
            targetLabel.SizeChanged -= TargetLabel_SizeChanged;
            targetLabel.SizeChanged += TargetLabel_SizeChanged;
            
            TargetLabel_SizeChanged(targetLabel, EventArgs.Empty);
        }

        private void RemoveUnitLabel(Label targetLabel)
        {
            var existing = targetLabel.Parent.Controls.OfType<Label>().FirstOrDefault(c => c.Name == targetLabel.Name + "_Unit");
            if (existing != null) targetLabel.Parent.Controls.Remove(existing);
        }

        private void TargetLabel_SizeChanged(object sender, EventArgs e)
        {
            Label target = sender as Label;
            var lblUnit = target.Parent.Controls.OfType<Label>().FirstOrDefault(c => c.Name == target.Name + "_Unit");
            if (lblUnit != null)
            {
                lblUnit.Left = target.Right - 5; // Cắt bớt padding thừa của WinForms Label
                lblUnit.Top = target.Bottom - lblUnit.PreferredHeight - (int)(target.Font.Size * 0.1f);
            }
        }

        // ══════════════════════════════════════════════════════════════
        // TABS SU CO
        // ══════════════════════════════════════════════════════════════

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            LoadDanhSachKH(txtTimKiem.Text.Trim());
        }

        private void gridViewKhachHang_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            OnCustomerSelected();
        }

        private bool IsPositiveTransaction(string loaiGD)
        {
            return loaiGD == ET.AppConstants.LoaiGiaoDichVi.NapTien ||
                   loaiGD == ET.AppConstants.LoaiGiaoDichVi.HoanCoc ||
                   loaiGD == ET.AppConstants.LoaiGiaoDichVi.HoanTien ||
                   loaiGD == ET.AppConstants.LoaiGiaoDichVi.DieuChinhTang;
        }

        private void gridViewGiaoDich_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "SoTien" && e.Value != null)
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal soTien))
                {
                    var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                    string loaiGD = view.GetListSourceRowCellValue(e.ListSourceRowIndex, "LoaiGiaoDich")?.ToString() ?? "";
                    string nhom = view.GetListSourceRowCellValue(e.ListSourceRowIndex, "NhomGiaoDich")?.ToString() ?? "";

                    if (nhom == "Giao dịch thường")
                    {
                        if (loaiGD.Contains("hoàn tiền") || loaiGD.Contains("Hoàn tiền"))
                        {
                            e.DisplayText = "+ " + soTien.ToString("N0") + " đ";
                        }
                        else if (loaiGD.Contains("Hủy") || loaiGD.Contains("hủy"))
                        {
                            e.DisplayText = soTien.ToString("N0") + " đ";
                        }
                        else
                        {
                            e.DisplayText = "- " + soTien.ToString("N0") + " đ";
                        }
                    }
                    else
                    {
                        if (IsPositiveTransaction(loaiGD))
                            e.DisplayText = "+ " + soTien.ToString("N0") + " đ";
                        else
                            e.DisplayText = "- " + soTien.ToString("N0") + " đ";
                    }
                }
            }
        }

        private void gridViewGiaoDich_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "SoTien")
            {
                var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                string loaiGD = view.GetRowCellValue(e.RowHandle, "LoaiGiaoDich")?.ToString() ?? "";
                string nhom = view.GetRowCellValue(e.RowHandle, "NhomGiaoDich")?.ToString() ?? "";
                
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far; // Sạn 2: Ép canh phải tuyệt đối

                if (nhom == "Giao dịch thường")
                {
                    if (loaiGD.Contains("hoàn tiền") || loaiGD.Contains("Hoàn tiền"))
                    {
                        e.Appearance.ForeColor = Color.FromArgb(5, 150, 105); 
                    }
                    else if (loaiGD.Contains("Hủy") || loaiGD.Contains("hủy"))
                    {
                        e.Appearance.ForeColor = Color.FromArgb(245, 158, 11); 
                    }
                    else if (loaiGD.Contains("nợ") || loaiGD.Contains("Nợ"))
                    {
                        e.Appearance.ForeColor = Color.FromArgb(37, 99, 235); 
                    }
                    else
                    {
                        e.Appearance.ForeColor = Color.FromArgb(220, 38, 38); 
                    }
                }
                else
                {
                    if (IsPositiveTransaction(loaiGD))
                    {
                        e.Appearance.ForeColor = Color.FromArgb(5, 150, 105);
                    }
                    else
                    {
                        e.Appearance.ForeColor = Color.FromArgb(220, 38, 38);
                    }
                }
            }
        }


        // ══════════════════════════════════════════════════════════════
        //  DETAIL LOADING — Lazy: chỉ khi chọn 1 khách hàng
        // ══════════════════════════════════════════════════════════════

        private void OnCustomerSelected()
        {
            var row = gridViewKhachHang.GetFocusedRow() as ET_KhachHang;
            if (row == null)
            {
                SetDetailVisible(false);
                return;
            }

            _selectedKH = BUS_KhachHang.Instance.GetById(row.Id);
            if (_selectedKH == null) { SetDetailVisible(false); return; }

            SetDetailVisible(true);
            LoadCustomerHeader();
            LoadKpiStrip();
            LoadTabGiaoDich();
            LoadTabDiem();
            LoadTabSuCo();
        }

        private void SetDetailVisible(bool visible)
        {
            pnlCustomerHeader.Visible = visible;
            if (_ucInfo != null) _ucInfo.Visible = visible;
            pnlKpiStrip.Visible = visible;
            pnlActions.Visible = visible;
            tabDetails.Visible = visible;
            btnSuaKH.Visible = visible;
            btnXoaKH.Visible = visible;
            lblNoSelection.Visible = !visible;

            if (visible)
                lblNoSelection.SendToBack();
            else
                lblNoSelection.BringToFront();
        }

        private void LoadCustomerHeader()
        {
            lblTenKH.Text = _selectedKH.HoTen ?? "—";

            if (!string.IsNullOrEmpty(_selectedKH.LoaiKhach))
            {
                lblLoaiKhach.Text = _selectedKH.LoaiKhach;
                lblLoaiKhach.Visible = true;

                switch (_selectedKH.LoaiKhach)
                {
                    case AppConstants.LoaiKhachHang.VVIP:
                        lblLoaiKhach.BackColor = Color.FromArgb(180, 83, 9);
                        break;
                    case AppConstants.LoaiKhachHang.Vip:
                        lblLoaiKhach.BackColor = Color.FromArgb(124, 58, 237);
                        break;
                    default:
                        lblLoaiKhach.BackColor = Color.FromArgb(71, 85, 105);
                        break;
                }
                lblLoaiKhach.ForeColor = Color.White;
                lblLoaiKhach.Location = new Point(lblTenKH.Right + 12, lblTenKH.Top + 6);
            }
            else
            {
                lblLoaiKhach.Visible = false;
            }

            // Load full customer info into UC
            if (_ucInfo != null) _ucInfo.LoadData(_selectedKH);
        }

        private void LoadKpiStrip()
        {
            // Wallet
            _selectedVi = BUS_KhachHang.Instance.LayViTheoKhachHang(_selectedKH.Id);
            _selectedThe = null;

            if (_selectedVi != null)
            {
                lblSoDu.Text = _selectedVi.SoDuKhaDung.ToString("N0");
                AddUnitLabel(lblSoDu, "đ");
                
                lblSoDu.Font = new Font("Cascadia Code", 20f, FontStyle.Bold);
                lblSoDu.ForeColor = ThemeManager.PrimaryColor;
                btnNapTien.Enabled = true;
                btnCapVi.Enabled = false;

                // RFID Card
                _selectedThe = BUS_KhachHang.Instance.LayTheRfidTheoVi(_selectedVi.Id);
                if (_selectedThe != null)
                {
                    lblMaRfid.Text = _selectedThe.MaRfid;
                    bool isActive = _selectedThe.TrangThai == AppConstants.TrangThaiTheRfid.Active;
                    lblTrangThaiThe.Text = isActive ? AppConstants.Display.WalletActive : _selectedThe.TrangThai.ToUpper();
                    lblTrangThaiThe.ForeColor = isActive ? Color.FromArgb(5, 150, 105) : Color.FromArgb(220, 38, 38);
                    btnKhoaThe.Enabled = true;
                    btnKhoaThe.Text = isActive ? "KHÓA THẺ" : "MỞ KHÓA";
                }
                else
                {
                    lblTrangThaiThe.Text = AppConstants.Display.NoRfid;
                    lblTrangThaiThe.ForeColor = Color.FromArgb(148, 163, 184);
                    lblMaRfid.Text = "";
                    btnKhoaThe.Enabled = false;
                }
            }
            else
            {
                lblSoDu.Text = AppConstants.Display.NoWallet;
                RemoveUnitLabel(lblSoDu);
                lblSoDu.Font = new Font("Segoe UI Semibold", 10f);
                lblSoDu.ForeColor = Color.FromArgb(148, 163, 184);
                lblTrangThaiThe.Text = "—";
                lblMaRfid.Text = "";
                btnNapTien.Enabled = false;
                btnKhoaThe.Enabled = false;
                btnCapVi.Enabled = true;
            }

            // Points and Spending
            lblDiemTichLuy.Text = _selectedKH.DiemTichLuy.ToString("N0") + " pts";
            lblTongChiTieu.Text = _selectedKH.TongChiTieu.ToString("N0") + "đ";
        }

        private class LsGiaoDichViewModel
        {
            public DateTime ThoiGian { get; set; }
            public string NhomGiaoDich { get; set; }
            public string LoaiGiaoDich { get; set; }
            public decimal SoTien { get; set; }
            public string MaCode { get; set; }
        }

        private void LoadTabGiaoDich()
        {
            if (_selectedKH == null)
            {
                gridGiaoDich.DataSource = null;
                return;
            }

            var listCombined = new List<LsGiaoDichViewModel>();

            if (_selectedVi != null)
            {
                var dsVi = BUS_KhachHang.Instance.LayLichSuGiaoDich(_selectedVi.Id);
                foreach (var viGiaoDich in dsVi)
                {
                    listCombined.Add(new LsGiaoDichViewModel
                    {
                        ThoiGian = viGiaoDich.ThoiGian,
                        NhomGiaoDich = "Giao dịch ví",
                        LoaiGiaoDich = viGiaoDich.LoaiGiaoDich,
                        SoTien = viGiaoDich.SoTien,
                        MaCode = viGiaoDich.MaCode
                    });
                }
            }

            var dsDonHang = BUS_DonHang.Instance.LoadDS().Where(x => x.IdKhachHang == _selectedKH.Id).ToList();
            foreach (var dh in dsDonHang)
            {
                listCombined.Add(new LsGiaoDichViewModel
                {
                    ThoiGian = dh.ThoiGian,
                    NhomGiaoDich = "Giao dịch thường",
                    LoaiGiaoDich = "Hóa đơn - " + dh.TenTrangThai,
                    SoTien = dh.TongTien - dh.TienGiamGia,
                    MaCode = dh.MaCode
                });
            }

            listCombined = listCombined.OrderByDescending(x => x.ThoiGian).ToList();

            gridGiaoDich.DataSource = listCombined;
            gridViewGiaoDich.PopulateColumns();

            var v = gridViewGiaoDich;
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in v.Columns)
                col.Visible = false;

            ShowColumn(v, "NhomGiaoDich", "Nhóm GD", 100);
            ShowColumn(v, "ThoiGian", "Thời Gian", 140);
            ShowColumn(v, "LoaiGiaoDich", "Loại GD", 130);
            ShowColumn(v, "SoTien", "Số Tiền", 130);
            ShowColumn(v, "MaCode", "Mã GD", 150);

            if (v.Columns["NhomGiaoDich"] != null)
            {
                v.Columns["NhomGiaoDich"].GroupIndex = 0;
            }

            if (v.Columns["SoTien"] != null)
            {
                v.Columns["SoTien"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                v.Columns["SoTien"].DisplayFormat.FormatString = "N0";
                v.Columns["SoTien"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                v.Columns["SoTien"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            }
            if (v.Columns["ThoiGian"] != null)
            {
                v.Columns["ThoiGian"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                v.Columns["ThoiGian"].DisplayFormat.FormatString = "dd/MM/yy HH:mm";
            }
            
            v.OptionsView.ShowGroupPanel = false;
            v.ExpandAllGroups();
            v.OptionsView.ColumnAutoWidth = true;
        }

        private void LoadTabDiem()
        {
            if (_selectedKH == null) return;
            
            lblDiemTichLuy.Text = _selectedKH.DiemTichLuy.ToString("N0");
            AddUnitLabel(lblDiemTichLuy, "pts");

            var ds = BUS_KhachHang.Instance.LayLichSuDiem(_selectedKH.Id);
            gridDiem.DataSource = ds;
            gridViewDiem.PopulateColumns();

            var v = gridViewDiem;
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in v.Columns)
                col.Visible = false;

            ShowColumn(v, "ThoiGian", "Thời Gian", 130);
            ShowColumn(v, "LoaiGiaoDich", "Loại", 90);
            ShowColumn(v, "SoDiem", "Khoản", 80);
            ShowColumn(v, "SoDuTruoc", "Trước GD", 80);
            ShowColumn(v, "SoDuSau", "Sau GD", 80);
            ShowColumn(v, "LyDo", "Lý Do", 150);
            ShowColumn(v, "TenNhanVien", "Người Xử Lý", 120);

            if (v.Columns["ThoiGian"] != null) v.Columns["ThoiGian"].VisibleIndex = 0;
            if (v.Columns["LoaiGiaoDich"] != null) v.Columns["LoaiGiaoDich"].VisibleIndex = 1;
            if (v.Columns["SoDiem"] != null) v.Columns["SoDiem"].VisibleIndex = 2;
            if (v.Columns["SoDuTruoc"] != null) v.Columns["SoDuTruoc"].VisibleIndex = 3;
            if (v.Columns["SoDuSau"] != null) v.Columns["SoDuSau"].VisibleIndex = 4;
            if (v.Columns["LyDo"] != null) v.Columns["LyDo"].VisibleIndex = 5;
            if (v.Columns["TenNhanVien"] != null) v.Columns["TenNhanVien"].VisibleIndex = 6;

            if (v.Columns["ThoiGian"] != null)
            {
                v.Columns["ThoiGian"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                v.Columns["ThoiGian"].DisplayFormat.FormatString = "dd/MM/yy HH:mm";
            }
            v.OptionsView.ColumnAutoWidth = true;
        }

        private void LoadTabSuCo()
        {
            var ds = BUS_KhachHang.Instance.LaySuCoTheoKhachHang(_selectedKH.Id);
            gridSuCo.DataSource = ds;
            gridViewSuCo.PopulateColumns();

            var v = gridViewSuCo;
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in v.Columns)
                col.Visible = false;

            ShowColumn(v, "ThoiGian", "Thời Gian", 120);
            ShowColumn(v, "LoaiSuCo", "Loại", 100);
            ShowColumn(v, "MucDo", "Mức Độ", 80);
            ShowColumn(v, "MoTa", "Mô Tả", 350);
            ShowColumn(v, "TenNhanVienXuLy", "NV Xử Lý", 150);

            if (v.Columns["ThoiGian"] != null) 
            {
                v.Columns["ThoiGian"].MaxWidth = 120; 
                v.Columns["ThoiGian"].MinWidth = 120;
            }

            if (v.Columns["ThoiGian"] != null)
            {
                v.Columns["ThoiGian"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                v.Columns["ThoiGian"].DisplayFormat.FormatString = "dd/MM/yy HH:mm";
            }
            v.OptionsView.ColumnAutoWidth = true;
        }

        // ══════════════════════════════════════════════════════════════
        //  CRUD — THÊM / SỬA / XÓA
        // ══════════════════════════════════════════════════════════════

        private void BtnThemKhachMoi_Click(object sender, EventArgs e)
        {
            using (var frm = new frmSuaKhachHang(null))
            {
                if (ThemeManager.ShowAsPopup(frm) == DialogResult.OK)
                {
                    LoadDanhSachKH(txtTimKiem.Text.Trim());
                    // Focus the new row
                    SelectRowByMaCode(frm.SavedMaCode);
                }
            }
        }

        private void BtnSuaKH_Click(object sender, EventArgs e)
        {
            if (_selectedKH == null) return;
            using (var frm = new frmSuaKhachHang(_selectedKH))
            {
                if (ThemeManager.ShowAsPopup(frm) == DialogResult.OK)
                {
                    LoadDanhSachKH(txtTimKiem.Text.Trim());
                    SelectRowById(_selectedKH.Id);
                }
            }
        }

        private void BtnXoaKH_Click(object sender, EventArgs e)
        {
            if (_selectedKH == null) return;

            // Step 1: Show detailed warning
            var checkResult = BUS_KhachHang.Instance.KiemTraTruocKhiXoa(_selectedKH.Id);
            if (!checkResult.IsSuccess)
            {
                TDCMessageBox.Show(checkResult.ErrorMessage, "Lỗi");
                return;
            }

            if (TDCMessageBox.Show(checkResult.Data, "⚠️ XÁC NHẬN XÓA KHÁCH HÀNG",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            // Step 2: Execute smart delete
            var result = BUS_KhachHang.Instance.XoaThongMinh(_selectedKH.Id);
            if (result.IsSuccess)
            {
                TDCMessageBox.Show("Đã xóa khách hàng thành công.", "Hoàn tất");
                _selectedKH = null;
                SetDetailVisible(false);
                LoadDanhSachKH(txtTimKiem.Text.Trim());
            }
            else
            {
                TDCMessageBox.Show(result.ErrorMessage, "Lỗi xóa");
            }
        }

        // 
        //   Nạp tiền / Khóa thẻ / Cấp ví
        // 

        private void BtnNapTien_Click(object sender, EventArgs e)
        {
            if (_selectedKH == null || _selectedVi == null) return;

            var theRfid = _selectedThe;
            if (theRfid == null || theRfid.TrangThai != AppConstants.TrangThaiTheRfid.Active)
            {
                TDCMessageBox.Show("Thẻ RFID chưa được gắn hoặc đang bị khóa.\nKhông thể nạp tiền.", "Yêu cầu");
                return;
            }

            using (var frm = new frmQuayNapTien(theRfid.MaRfid))
            {
                ThemeManager.ShowAsPopup(frm);
            }

            OnCustomerSelected();
        }

        private void BtnKhoaThe_Click(object sender, EventArgs e)
        {
            if (_selectedThe == null) return;

            bool isActive = _selectedThe.TrangThai == AppConstants.TrangThaiTheRfid.Active;
            string action = isActive ? "KHÓA" : "MỞ KHÓA";
            string newStatus = isActive ? AppConstants.TrangThaiTheRfid.Locked : AppConstants.TrangThaiTheRfid.Active;

            string msg = string.Format("{0} thẻ RFID [{1}]?\nKhách hàng: {2}",
                action, _selectedThe.MaRfid, _selectedKH.HoTen);

            if (isActive && _selectedVi != null && _selectedVi.SoDuKhaDung > 0)
            {
                msg += string.Format("\n\n Ví còn {0:N0} VNĐ — sau khi khóa thẻ, khách sẽ không thể thanh toán bằng ví.", _selectedVi.SoDuKhaDung);
            }

            if (TDCMessageBox.Show(msg, "XÁC NHẬN " + action + " THẺ",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            var result = BUS_GiaoDichVi.Instance.KhoaMoThe(_selectedThe.MaRfid, newStatus);
            if (result.IsSuccess)
            {
                TDCMessageBox.Show("Đã " + action.ToLower() + " thẻ thành công.", "Hoàn tất");
                LoadKpiStrip();
            }
            else
            {
                TDCMessageBox.Show(result.ErrorMessage, "Lỗi");
            }
        }

        private void BtnCapVi_Click(object sender, EventArgs e)
        {
            if (_selectedKH == null) return;

            string msg = string.Format("Cấp ví điện tử mới cho khách hàng:\n「{0}」 ({1})\n\nSố dư ban đầu: 0 VNĐ",
                _selectedKH.HoTen, _selectedKH.MaCode);

            if (TDCMessageBox.Show(msg, "CẤP VÍ MỚI",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            var result = BUS_KhachHang.Instance.CapViMoi(_selectedKH.Id);
            if (result.IsSuccess)
            {
                TDCMessageBox.Show("Đã cấp ví thành công.\nBạn có thể gắn thẻ RFID và nạp tiền ngay.", "Hoàn tất");
                LoadKpiStrip();
                LoadTabGiaoDich();
            }
            else
            {
                TDCMessageBox.Show(result.ErrorMessage, "Lỗi");
            }
        }

        private void BtnSuaDiem_Click(object sender, EventArgs e)
        {
            if (_selectedKH == null) return;

            using (var frm = new frmDieuChinhDiemDialog(_selectedKH.DiemTichLuy))
            {
                if (ThemeManager.ShowAsPopup(frm) == DialogResult.OK)
                {
                    int createdBy = SessionManager.CurrentUser?.Id ?? 1;
                    var result = BUS_TichDiem.Instance.DieuChinhDiem(_selectedKH.Id, frm.DiemDieuChinh, frm.LyDo, createdBy);

                    if (result.IsSuccess)
                    {
                        TDCMessageBox.Show("Đã điều chỉnh điểm thành công.", "Hoàn tất");
                        // Refresh info and UI
                        _selectedKH = BUS_KhachHang.Instance.GetById(_selectedKH.Id);
                        LoadKpiStrip();
                        LoadTabDiem();
                    }
                    else
                    {
                        TDCMessageBox.Show(result.ErrorMessage, "Lỗi");
                    }
                }
            }
        }

        // 
        //  HELPERS
        // 

        private void SelectRowById(int id)
        {
            for (int i = 0; i < gridViewKhachHang.RowCount; i++)
            {
                var row = gridViewKhachHang.GetRow(i) as ET_KhachHang;
                if (row != null && row.Id == id)
                {
                    gridViewKhachHang.FocusedRowHandle = i;
                    return;
                }
            }
        }

        private void SelectRowByMaCode(string maCode)
        {
            if (string.IsNullOrEmpty(maCode)) return;
            for (int i = 0; i < gridViewKhachHang.RowCount; i++)
            {
                var row = gridViewKhachHang.GetRow(i) as ET_KhachHang;
                if (row != null && row.MaCode == maCode)
                {
                    gridViewKhachHang.FocusedRowHandle = i;
                    return;
                }
            }
        }
    }
}

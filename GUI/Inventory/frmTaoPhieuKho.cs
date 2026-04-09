using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BUS;
using ET;
using FontAwesome.Sharp;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DAL;

namespace GUI
{
    public partial class frmTaoPhieuKho : Form
    {
        private bool _isNhapMode;
        private BindingList<ChiTietRow> _chiTietRows;
        private List<ET_SanPham> _allSanPham;
        private CameraScanner _cameraScanner;

        /// <summary>
        /// DTO nội bộ để bind lên Grid inline-edit
        /// </summary>
        public class ChiTietRow : INotifyPropertyChanged
        {
            public int IdSanPham { get; set; }
            public string TenSanPham { get; set; }

            private int _soLuong;
            public int SoLuong 
            { 
                get { return _soLuong; } 
                set { _soLuong = value; OnPropertyChanged("SoLuong"); OnPropertyChanged("ThanhTien"); }
            }

            private decimal _donGia;
            public decimal DonGia 
            { 
                get { return _donGia; } 
                set { _donGia = value; OnPropertyChanged("DonGia"); OnPropertyChanged("ThanhTien"); }
            }

            public decimal ThanhTien { get { return SoLuong * DonGia; } }

            // LƯU CƠ CHẾ QUY ĐỔI NỘI BỘ
            private string _donVi;
            public string DonVi 
            { 
                get { return _donVi; } 
                set { _donVi = value; OnPropertyChanged("DonVi"); }
            } 
            public int IdDonViHienTai { get; set; }
            public decimal TyLeQuyDoi { get; set; } // So với Base Unit

            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        public frmTaoPhieuKho(bool isNhapMode)
        {
            InitializeComponent();
            _isNhapMode = isNhapMode;
            _chiTietRows = new BindingList<ChiTietRow>();
            _allSanPham = BUS_SanPham.Instance.LoadDS();

            SetupMode();
            SetupGrid();
            SetupDropdowns();
            ApplyTheme();
            
            // Recalculate total after any edit (Dynamic property)
            _chiTietRows.ListChanged += (s, e) => UpdateTongTien();
        }

        private void SetupMode()
        {
            if (_isNhapMode)
            {
                lblFormTitle.Text = "TẠO PHIẾU NHẬP KHO";
                lblMode.Text = "NHẬP KHO";
                lblMode.ForeColor = ThemeManager.SuccessColor; // Emerald
                txtSoChungTu.Text = BUS_PhieuNhapKho.Instance.LaySoChungTuTiepTheo();

                // Show NCC, hide LyDo
                lbl_cboNhaCungCap.Visible = true;
                cboNhaCungCap.Visible = true;
                lbl_txtLyDo.Visible = false;
                txtLyDo.Visible = false;
            }
            else
            {
                lblFormTitle.Text = "TẠO PHIẾU XUẤT KHO";
                lblMode.Text = "XUẤT KHO";
                lblMode.ForeColor = ThemeManager.DangerColor;  // Red muted
                txtSoChungTu.Text = BUS_PhieuXuatKho.Instance.LaySoChungTuTiepTheo();

                // Hide NCC, show LyDo
                lbl_cboNhaCungCap.Visible = false;
                cboNhaCungCap.Visible = false;
                lbl_txtLyDo.Visible = true;
                txtLyDo.Visible = true;
            }
        }

        private void SetupDropdowns()
        {
            // Kho dropdown
            var dsKho = BUS_KhoHang.Instance.LoadDS();
            cboKho.Properties.DataSource = dsKho;
            cboKho.Properties.DisplayMember = "TenKho";
            cboKho.Properties.ValueMember = "Id";

            // Nhà cung cấp dropdown (only for Nhap mode)
            if (_isNhapMode)
            {
                var dsNCC = BUS_NhaCungCap.Instance.LoadDS();
                cboNhaCungCap.Properties.DataSource = dsNCC;
                cboNhaCungCap.Properties.DisplayMember = "Ten";
                cboNhaCungCap.Properties.ValueMember = "Id";
            }
        }

        private void SetupGrid()
        {
            gridChiTiet.DataSource = _chiTietRows;
            gridViewChiTiet.PopulateColumns();

            // Hide IdSanPham column, show TenSanPham as SearchLookUp
            if (gridViewChiTiet.Columns["IdSanPham"] != null)
                gridViewChiTiet.Columns["IdSanPham"].Visible = false;

            // Setup SearchLookUpEdit for TenSanPham column
            var riSanPham = new RepositoryItemSearchLookUpEdit();
            riSanPham.DataSource = _allSanPham;
            riSanPham.DisplayMember = "Ten";
            riSanPham.ValueMember = "Ten";
            riSanPham.NullText = "[Chọn sản phẩm]";

            var col = riSanPham.View.Columns;
            riSanPham.View.Columns.AddVisible("MaCode", "Mã");
            riSanPham.View.Columns.AddVisible("Ten", "Tên sản phẩm");
            riSanPham.View.Columns.AddVisible("DonGia", "Đơn giá");

            if (gridViewChiTiet.Columns["TenSanPham"] != null)
            {
                gridViewChiTiet.Columns["TenSanPham"].Caption = "Sản phẩm";
                gridViewChiTiet.Columns["TenSanPham"].ColumnEdit = riSanPham;
                gridViewChiTiet.Columns["TenSanPham"].Width = 300;
            }

            // Format other columns
            if (gridViewChiTiet.Columns["SoLuong"] != null)
            {
                gridViewChiTiet.Columns["SoLuong"].Caption = "Số lượng";
                gridViewChiTiet.Columns["SoLuong"].Width = 100;
            }
            if (gridViewChiTiet.Columns["DonGia"] != null)
            {
                gridViewChiTiet.Columns["DonGia"].Caption = _isNhapMode ? "Đơn giá nhập" : "Đơn giá xuất";
                gridViewChiTiet.Columns["DonGia"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridViewChiTiet.Columns["DonGia"].DisplayFormat.FormatString = "n0";
                gridViewChiTiet.Columns["DonGia"].Width = 150;
            }
            if (gridViewChiTiet.Columns["ThanhTien"] != null)
            {
                gridViewChiTiet.Columns["ThanhTien"].Caption = "Thành tiền";
                gridViewChiTiet.Columns["ThanhTien"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridViewChiTiet.Columns["ThanhTien"].DisplayFormat.FormatString = "n0";
                gridViewChiTiet.Columns["ThanhTien"].OptionsColumn.ReadOnly = true;
                gridViewChiTiet.Columns["ThanhTien"].AppearanceCell.ForeColor = Color.FromArgb(231, 76, 60);
                gridViewChiTiet.Columns["ThanhTien"].AppearanceCell.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                gridViewChiTiet.Columns["ThanhTien"].Width = 150;
            }

            if (gridViewChiTiet.Columns["DonVi"] != null)
            {
                gridViewChiTiet.Columns["DonVi"].Caption = "Đơn vị (Click đúp để chuyển)";
                gridViewChiTiet.Columns["DonVi"].OptionsColumn.ReadOnly = true; // Block manual edit
                gridViewChiTiet.Columns["DonVi"].AppearanceCell.BackColor = Color.FromArgb(240, 248, 255); // AliceBlue
                gridViewChiTiet.Columns["DonVi"].AppearanceCell.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
                gridViewChiTiet.Columns["DonVi"].Width = 120;
            }

            gridViewChiTiet.OptionsView.ColumnAutoWidth = true;
            
            // Xử lý Double Click để xoay vòng các đơn vị quy đổi
            gridViewChiTiet.DoubleClick += GridViewChiTiet_DoubleClick;
        }

        private void GridViewChiTiet_DoubleClick(object sender, EventArgs e)
        {
            var pt = gridViewChiTiet.GridControl.PointToClient(Control.MousePosition);
            var info = gridViewChiTiet.CalcHitInfo(pt);

            if (info.InRowCell && info.Column.FieldName == "DonVi")
            {
                var row = gridViewChiTiet.GetRow(info.RowHandle) as ChiTietRow;
                if (row == null || row.IdSanPham <= 0) return;

                // Load all possible units
                var allUoM = DAL_QuyDoiDonVi.Instance.LoadDS().Where(x => x.IdSanPham == row.IdSanPham).ToList();
                if (allUoM.Count == 0)
                {
                    ToolTip tt = new ToolTip();
                    tt.Show("Sản phẩm này không có đơn vị quy đổi nào khác.", gridChiTiet, pt, 2000);
                    return;
                }

                var baseDvt = DAL_DonViTinh.Instance.LayTheoId(_allSanPham.First(x => x.Id == row.IdSanPham).IdDonViCoBan);

                // Build sequence: Base Unit (x1) -> UoM 1 -> UoM 2 -> ...
                var cycleList = new List<Tuple<int, string, decimal>>(); // IdDvt, Tên, Tỉ lệ
                cycleList.Add(Tuple.Create(baseDvt.Id, baseDvt.Ten + " (Base)", 1m));

                foreach (var u in allUoM)
                {
                    var dvt = DAL_DonViTinh.Instance.LayTheoId(u.IdDonViLon);
                    if (dvt != null)
                        cycleList.Add(Tuple.Create(dvt.Id, dvt.Ten + " (x" + u.TyLeQuyDoi + ")", u.TyLeQuyDoi));
                }

                // Find current index in cycle
                int currentIndex = cycleList.FindIndex(x => x.Item1 == row.IdDonViHienTai);
                if (currentIndex < 0) currentIndex = 0;

                // Move to next
                int nextIndex = (currentIndex + 1) % cycleList.Count;
                var nextUnit = cycleList[nextIndex];

                // Check "Giá Bán Riêng" for quick import price auto-calc? Actually import price is user typed, but we can suggest
                // Update Row
                row.IdDonViHienTai = nextUnit.Item1;
                row.TyLeQuyDoi = nextUnit.Item3;
                row.DonVi = nextUnit.Item2;

                // Adjust suggested Price if cycling up (for Nhập kho only to help user)
                if (_isNhapMode && row.DonGia > 0 && currentIndex == 0 && nextUnit.Item3 > 1)
                {
                    row.DonGia = row.DonGia * nextUnit.Item3;
                }
                else if (_isNhapMode && row.DonGia > 0 && nextUnit.Item3 == 1 && cycleList[currentIndex].Item3 > 1)
                {
                    row.DonGia = row.DonGia / cycleList[currentIndex].Item3;
                }

                gridViewChiTiet.RefreshRow(info.RowHandle);
                UpdateTongTien();
            }
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ApplyTheme()
        {
            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleDevExpressGrid(gridChiTiet);

            // Keep header dark
            pnlHeader.FillColor = ThemeManager.SidebarColor;
            pnlHeader.BackColor = ThemeManager.SidebarColor;

            btnThemDong.Image = IconHelper.GetBitmap(IconChar.Plus, Color.White, 14);
            btnXoaDong.Image = IconHelper.GetBitmap(IconChar.Minus, Color.White, 14);
            btnLuuPhieu.Image = IconHelper.GetBitmap(IconChar.Save, Color.White, 18);
        }

        private void BtnThemDong_Click(object sender, EventArgs e)
        {
            _chiTietRows.Add(new ChiTietRow { SoLuong = 1, DonGia = 0 });
            gridViewChiTiet.FocusedRowHandle = _chiTietRows.Count - 1;
        }

        private void BtnXoaDong_Click(object sender, EventArgs e)
        {
            int idx = gridViewChiTiet.FocusedRowHandle;
            if (idx >= 0 && idx < _chiTietRows.Count)
            {
                _chiTietRows.RemoveAt(idx);
                UpdateTongTien();
            }
        }

        private void GridViewChiTiet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "TenSanPham")
            {
                string tenSP = e.Value?.ToString();
                var sp = _allSanPham.FirstOrDefault(x => x.Ten == tenSP);
                if (sp != null)
                {
                    gridViewChiTiet.SetRowCellValue(e.RowHandle, "IdSanPham", sp.Id);
                    gridViewChiTiet.SetRowCellValue(e.RowHandle, "DonGia", sp.DonGia);

                    // Load Base Unit Defaults
                    var dvt = DAL_DonViTinh.Instance.LayTheoId(sp.IdDonViCoBan);
                    gridViewChiTiet.SetRowCellValue(e.RowHandle, "IdDonViHienTai", sp.IdDonViCoBan);
                    gridViewChiTiet.SetRowCellValue(e.RowHandle, "DonVi", dvt?.Ten ?? "Lon/Cái");
                    gridViewChiTiet.SetRowCellValue(e.RowHandle, "TyLeQuyDoi", 1m);
                }
            }

            if (!_isNhapMode && e.Column.FieldName == "SoLuong")
            {
                if (cboKho.EditValue != null && cboKho.EditValue != DBNull.Value)
                {
                    int idKho = Convert.ToInt32(cboKho.EditValue);
                    int idSP = Convert.ToInt32(gridViewChiTiet.GetRowCellValue(e.RowHandle, "IdSanPham"));
                    int soHan = Convert.ToInt32(e.Value);
                    decimal tyLe = Convert.ToDecimal(gridViewChiTiet.GetRowCellValue(e.RowHandle, "TyLeQuyDoi"));
                    
                    int soLuongBase = (int)(soHan * tyLe); // Số lượng cần xuất (tính theo Base Unit)

                    var ton = DAL_TonKho.Instance.LoadDS().FirstOrDefault(x => x.IdKho == idKho && x.IdSanPham == idSP);
                    int currentTon = ton != null ? ton.SoLuong : 0;
                    
                    if (soLuongBase > currentTon)
                    {
                        TDCMessageBox.Show($"Trong kho chỉ còn {currentTon} sản phẩm cơ bản (Base).\nBạn đang yêu cầu xuất {soLuongBase} Base Units (Tức là {soHan} {gridViewChiTiet.GetRowCellValue(e.RowHandle, "DonVi")} ).", "Vượt tồn kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        // Trả về số lượng Max có thể xuất theo đơn vị hiện tại = (currentTon / tyLe) làm tròn xuống
                        int maxChoPhep = (int)(currentTon / tyLe);
                        gridViewChiTiet.SetRowCellValue(e.RowHandle, "SoLuong", maxChoPhep);
                    }
                }
            }

            // Refresh ThanhTien
            gridViewChiTiet.RefreshRow(e.RowHandle);
            UpdateTongTien();
        }

        private void UpdateTongTien()
        {
            decimal total = _chiTietRows.Sum(x => x.ThanhTien);
            lblTongTienValue.Text = total.ToString("N0") + " đ";
        }

        // ==========================================================
        // CAMERA SCANNER MÃ VẠCH (THÔNG MINH & TIỆN QUÉT)
        // ==========================================================

        private void EnsureScanner()
        {
            if (_cameraScanner == null)
            {
                _cameraScanner = new CameraScanner();
                _cameraScanner.OnBarcodeDetected += txtScanner_BarcodeDetected;
                _cameraScanner.OnError += (err) => TDCMessageBox.Show(err, "Camera Kho");
            }
        }

        private void btnToggleCamera_Click(object sender, EventArgs e)
        {
            if (_cameraScanner != null && _cameraScanner.IsRunning)
            {
                _cameraScanner.Stop();
                picCamera.Visible = false;
                btnToggleCamera.Text = "📷 Cam Kho";
                btnToggleCamera.FillColor = ThemeManager.PrimaryColor;
            }
            else
            {
                EnsureScanner();
                picCamera.Visible = true;
                if (_cameraScanner.Start(picCamera))
                {
                    btnToggleCamera.Text = "⏹ Tắt Cam";
                    btnToggleCamera.FillColor = ThemeManager.DangerColor;
                }
            }
        }

        private void txtScanner_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string code = txtScanner.Text.Trim();
                if (!string.IsNullOrEmpty(code))
                {
                    ProcessScan(code);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void txtScanner_BarcodeDetected(string code)
        {
            // Invoke từ luồng camera sang giao diện
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => txtScanner_BarcodeDetected(code)));
                return;
            }

            txtScanner.Text = code;
            ProcessScan(code);
            try { System.Media.SystemSounds.Beep.Play(); } catch { }
        }

        private void ProcessScan(string code)
        {
            var sp = _allSanPham.FirstOrDefault(x => x.MaCode == code);
            if (sp == null)
            {
                TDCMessageBox.Show("Không tìm thấy sản phẩm mã: " + code, "Lỗi Quét Kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtScanner.Clear();
                txtScanner.Focus();
                return;
            }

            if (!_isNhapMode)
            {
                if (cboKho.EditValue == null || cboKho.EditValue == DBNull.Value)
                {
                    TDCMessageBox.Show("Vui lòng chọn Kho xuất trước khi quét sản phẩm!", "Chưa chọn kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtScanner.Clear();
                    txtScanner.Focus();
                    return;
                }

                int idKho = Convert.ToInt32(cboKho.EditValue);
                var ton = DAL_TonKho.Instance.LoadDS().FirstOrDefault(x => x.IdKho == idKho && x.IdSanPham == sp.Id);
                int currentTon = ton != null ? ton.SoLuong : 0;

                var existingItem = _chiTietRows.FirstOrDefault(x => x.IdSanPham == sp.Id);
                int proposedSoLuong = (existingItem != null ? existingItem.SoLuong : 0) + 1;

                if (proposedSoLuong > currentTon)
                {
                    TDCMessageBox.Show($"Trong kho chỉ còn {currentTon} cái '{sp.Ten}'!\nKhông thể xuất thêm.", "Hết hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtScanner.Clear();
                    txtScanner.Focus();
                    return;
                }
            }

            // Gộp số lượng nếu đã tồn tại
            var existing = _chiTietRows.FirstOrDefault(x => x.IdSanPham == sp.Id);
            if (existing != null)
            {
                existing.SoLuong += 1;
            }
            else
            {
                _chiTietRows.Add(new ChiTietRow
                {
                    IdSanPham = sp.Id,
                    TenSanPham = sp.Ten,
                    SoLuong = 1,
                    DonGia = sp.DonGia
                });
            }

            gridChiTiet.RefreshDataSource();
            UpdateTongTien();

            txtScanner.Clear();
            txtScanner.Focus();
        }

        private void BtnLuuPhieu_Click(object sender, EventArgs e)
        {
            // Validate
            if (cboKho.EditValue == null || cboKho.EditValue == DBNull.Value)
            {
                TDCMessageBox.Show("Vui lòng chọn Kho!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_chiTietRows.Count == 0)
            {
                TDCMessageBox.Show("Chưa có sản phẩm nào trong danh sách!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check for rows with no product selected
            var invalidRows = _chiTietRows.Where(x => x.IdSanPham == 0).ToList();
            if (invalidRows.Count > 0)
            {
                TDCMessageBox.Show("Có " + invalidRows.Count + " dòng chưa chọn sản phẩm!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // [K1]: Validate SL > 0
            var zeroQtyRows = _chiTietRows.Where(x => x.SoLuong <= 0).ToList();
            if (zeroQtyRows.Count > 0)
            {
                TDCMessageBox.Show("Có " + zeroQtyRows.Count + " dòng có số lượng ≤ 0!\nVui lòng sửa hoặc xóa dòng đó.", "Số lượng không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // [K2]: Validate DonGia > 0 cho Nhập Kho (kế toán yêu cầu có giá nhập)
            if (_isNhapMode)
            {
                var zeroPriceRows = _chiTietRows.Where(x => x.DonGia <= 0).ToList();
                if (zeroPriceRows.Count > 0)
                {
                    TDCMessageBox.Show("Có " + zeroPriceRows.Count + " dòng có đơn giá = 0!\nPhiếu nhập kho bắt buộc phải có giá nhập.", "Đơn giá không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // [K3]: Check trùng sản phẩm -> gộp
            var duplicateSPs = _chiTietRows.GroupBy(x => x.IdSanPham).Where(g => g.Count() > 1).ToList();
            if (duplicateSPs.Count > 0)
            {
                string tenTrung = string.Join(", ", duplicateSPs.Select(g => g.First().TenSanPham));
                if (TDCMessageBox.Show($"Phát hiện sản phẩm trùng: {tenTrung}\n\nBạn có muốn tự động gộp số lượng?", "Sản phẩm trùng", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (var group in duplicateSPs)
                    {
                        var first = group.First();
                        first.SoLuong = group.Sum(x => x.SoLuong);
                        first.DonGia = group.Max(x => x.DonGia); // Lấy giá cao nhất
                        foreach (var dup in group.Skip(1))
                            _chiTietRows.Remove(dup);
                    }
                    gridChiTiet.RefreshDataSource();
                    UpdateTongTien();
                    TDCMessageBox.Show("Đã gộp xong! Vui lòng kiểm tra lại rồi nhấn Lưu.", "Đã gộp");
                    return;
                }
                else
                {
                    return; // Không gộp thì không cho lưu
                }
            }

            gridViewChiTiet.CloseEditor();
            gridViewChiTiet.UpdateCurrentRow();

            if (!_isNhapMode)
            {
                int idKho = Convert.ToInt32(cboKho.EditValue);
                var allTonKho = DAL_TonKho.Instance.LoadDS().Where(x => x.IdKho == idKho).ToList();
                
                foreach (var r in _chiTietRows)
                {
                    var ton = allTonKho.FirstOrDefault(x => x.IdSanPham == r.IdSanPham);
                    int currentTon = ton != null ? ton.SoLuong : 0;
                    if (r.SoLuong > currentTon)
                    {
                        TDCMessageBox.Show($"Sản phẩm '{r.TenSanPham}' không đủ tồn kho!\nTồn hiện tại: {currentTon} - Bạn muốn xuất: {r.SoLuong}", "Thiếu tồn kho", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            int result;
            if (_isNhapMode)
            {
                var master = new ET_PhieuNhapKho
                {
                    IdKho = Convert.ToInt32(cboKho.EditValue),
                    IdNhaCungCap = cboNhaCungCap.EditValue != null && cboNhaCungCap.EditValue != DBNull.Value
                        ? (int?)Convert.ToInt32(cboNhaCungCap.EditValue) : null,
                    NgayNhap = ((DateTime)dtNgay.EditValue),
                    SoChungTu = txtSoChungTu.Text.Trim()
                };
                var details = _chiTietRows.Select(r => new ET_ChiTietNhapKho
                {
                    IdSanPham = r.IdSanPham,
                    SoLuong = r.SoLuong,              // 100
                    DonGiaNhap = r.DonGia,            // 350.000
                    IdDonViNhap = r.IdDonViHienTai,   // ID của "Thùng"
                    TyLeQuyDoi = r.TyLeQuyDoi         // 24 (Đóng băng lịch sử quy đổi)
                }).ToList();

                result = BUS_PhieuNhapKho.Instance.TaoPhieu(master, details);
            }
            else
            {
                var master = new ET_PhieuXuatKho
                {
                    IdKhoXuat = Convert.ToInt32(cboKho.EditValue),
                    NgayXuat = ((DateTime)dtNgay.EditValue),
                    LyDo = txtLyDo.Text.Trim()
                };
                var details = _chiTietRows.Select(r => new ET_ChiTietXuatKho
                {
                    IdSanPham = r.IdSanPham,
                    SoLuong = r.SoLuong,
                    DonGiaXuat = r.DonGia,
                    IdDonViXuat = r.IdDonViHienTai,
                    TyLeQuyDoi = r.TyLeQuyDoi
                }).ToList();

                result = BUS_PhieuXuatKho.Instance.TaoPhieu(master, details);
            }

            if (result > 0)
            {
                TDCMessageBox.Show((_isNhapMode ? "Nhập kho" : "Xuất kho") + " thành công!\nMã phiếu: #" + result,
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                TDCMessageBox.Show("Lưu phiếu thất bại! Kiểm tra lại dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

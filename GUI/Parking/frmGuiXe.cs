using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using BUS;
using ET;
using FontAwesome.Sharp;

namespace GUI
{
    public partial class frmGuiXe : Form
    {
        //  State 
        private List<ET_LuotVaoRaBaiXe> _dsDangGui = new List<ET_LuotVaoRaBaiXe>();
        private ET_LuotVaoRaBaiXe _selectedLuot = null;
        private string _anhBienSoPath = null;
        private Timer _dashboardTimer;

        public frmGuiXe()
        {
            InitializeComponent();
            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleDevExpressGrid(gridXeDangGui);
            SetupComboboxes();
            SetupButtonIcons();
            StartDashboardTimer();
        }

        // =
        // SETUP
        // =

        private void SetupButtonIcons()
        {
            btnNhanXe.Image = IconHelper.GetBitmap(IconChar.Check, Color.White, 20);
            btnChonAnh.Image = IconHelper.GetBitmap(IconChar.FileImage, Color.White, 16);
            btnChupDienThoai.Image = IconHelper.GetBitmap(IconChar.Camera, Color.White, 16);
            btnXoaAnh.Image = IconHelper.GetBitmap(IconChar.Times, Color.White, 16);
            btnTraXeTienMat.Image = IconHelper.GetBitmap(IconChar.MoneyBill, Color.White, 16);
            btnTraXeChuyenKhoan.Image = IconHelper.GetBitmap(IconChar.CreditCard, Color.White, 16);
            btnTraXeRfid.Image = IconHelper.GetBitmap(IconChar.Wifi, Color.White, 16);
        }

        private void SetupComboboxes()
        {
            var loaiXeList = new Dictionary<string, string> {
                { AppConstants.LoaiXe.XeMay, "🏍️ Xe máy" },
                { AppConstants.LoaiXe.OTo, "🚗 Ô tô" },
                { AppConstants.LoaiXe.XeDap, "🚲 Xe đạp" },
                { AppConstants.LoaiXe.XeDien, "⚡ Xe điện" }
            };
            cboLoaiXe.DataSource = new BindingSource(loaiXeList, null);
            cboLoaiXe.DisplayMember = "Value";
            cboLoaiXe.ValueMember = "Key";
            cboLoaiXe.SelectedValueChanged += cboLoaiXe_SelectedValueChanged;
        }

        private void frmGuiXe_Load(object sender, EventArgs e)
        {
            RefreshDangGui();
            RefreshDashboard();
            UpdateGiaDuKien();
        }

        // =
        // DASHBOARD
        // =

        private void StartDashboardTimer()
        {
            _dashboardTimer = new Timer { Interval = 30000 }; // 30s
            _dashboardTimer.Tick += dashboardTimer_Tick;
            _dashboardTimer.Start();
        }

        private void dashboardTimer_Tick(object sender, EventArgs e)
        {
            RefreshDashboard();
        }

        private void RefreshDashboard()
        {
            try
            {
                var demTheoLoai = BUS_GuiXe.Instance.DemXeTheoLoai();
                int xeMay = demTheoLoai.ContainsKey(AppConstants.LoaiXe.XeMay) ? demTheoLoai[AppConstants.LoaiXe.XeMay] : 0;
                int oTo = demTheoLoai.ContainsKey(AppConstants.LoaiXe.OTo) ? demTheoLoai[AppConstants.LoaiXe.OTo] : 0;
                int xeDap = demTheoLoai.ContainsKey(AppConstants.LoaiXe.XeDap) ? demTheoLoai[AppConstants.LoaiXe.XeDap] : 0;
                int xeDien = demTheoLoai.ContainsKey(AppConstants.LoaiXe.XeDien) ? demTheoLoai[AppConstants.LoaiXe.XeDien] : 0;
                int tongDangGui = xeMay + oTo + xeDap + xeDien;
                int daTraHomNay = BUS_GuiXe.Instance.DemDaTraHomNay();

                lblDashXeMay.Text = string.Format("Xe máy: {0}", xeMay);
                lblDashOTo.Text = string.Format("Ô tô: {0}", oTo);
                lblDashXeDap.Text = string.Format("Xe đạp: {0}", xeDap);
                lblDashXeDien.Text = string.Format("Xe điện: {0}", xeDien);
                lblDashTong.Text = string.Format("Tổng đang gửi: {0} | Đã trả hôm nay: {1}", tongDangGui, daTraHomNay);
            }
            catch { }
        }

        // =
        // TAB NHẬN XE — Event Handlers
        // =

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Ảnh|*.jpg;*.jpeg;*.png;*.bmp";
                ofd.Title = "Chọn ảnh biển số xe";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    LoadAnhBienSo(ofd.FileName);
                }
            }
        }

        private void btnChupDienThoai_Click(object sender, EventArgs e)
        {
            string url = Microsoft.VisualBasic.Interaction.InputBox(
                "Nhập URL IP Webcam của điện thoại:\n(Ví dụ: http://192.168.1.100:8080/shot.jpg)",
                "Chụp qua điện thoại",
                "http://192.168.1.100:8080/shot.jpg");

            if (string.IsNullOrWhiteSpace(url)) return;

            try
            {
                using (var client = new WebClient())
                {
                    string tempPath = Path.Combine(Path.GetTempPath(), "parking_capture_" + DateTime.Now.ToString("HHmmss") + ".jpg");
                    client.DownloadFile(url, tempPath);
                    LoadAnhBienSo(tempPath);
                    TDCMessageBox.Show("Chụp ảnh từ điện thoại thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                TDCMessageBox.Show("Không kết nối được điện thoại.\nKiểm tra:\n- Cùng WiFi?\n- App IP Webcam đang chạy?\n\nLỗi: " + ex.Message,
                    "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXoaAnh_Click(object sender, EventArgs e)
        {
            picBienSo.Image?.Dispose();
            picBienSo.Image = null;
            _anhBienSoPath = null;
        }

        private void cboLoaiXe_SelectedValueChanged(object sender, EventArgs e)
        {
            UpdateGiaDuKien();
        }

        private void btnNhanXe_Click(object sender, EventArgs e)
        {
            string bienSo = txtBienSoNhan.Text.Trim();
            if (string.IsNullOrWhiteSpace(bienSo))
            {
                TDCMessageBox.Show("Vui lòng nhập biển số xe!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBienSoNhan.Focus();
                return;
            }

            string loaiXe = cboLoaiXe.SelectedValue?.ToString() ?? AppConstants.LoaiXe.XeMay;
            string maRfid = string.IsNullOrWhiteSpace(txtRfidNhan.Text) ? ("QR-" + DateTime.Now.ToString("HHmmss")) : txtRfidNhan.Text.Trim();

            var result = BUS_GuiXe.Instance.NhanXe(bienSo, loaiXe, maRfid, _anhBienSoPath);

            if (result.IsSuccess)
            {
                // -- MÔ PHỎNG IN VÉ (IN RA MÃ QR BẰNG ID PHIẾU GIỮ XE) --
                try
                {
                    string finalQrData = string.IsNullOrWhiteSpace(maRfid) || maRfid.StartsWith("QR-") ? "QR-" + result.Data : maRfid;
                    var qrBitmap = BarcodeHelper.GenerateQrCode(finalQrData);
                    string ticketPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Ticket_" + bienSo + ".png");
                    qrBitmap.Save(ticketPath, System.Drawing.Imaging.ImageFormat.Png);
                    TDCMessageBox.Show("Nhận xe thành công!\nĐã 'in' vé (chứa mã QR) ra Desktop:\n" + ticketPath, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    TDCMessageBox.Show("Nhận xe thành công nhưng không tạo được thẻ ảo: " + ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
                ResetFormNhanXe();
            }
            else
            {
                TDCMessageBox.Show(result.ErrorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =
        // TAB TRẢ XE — Event Handlers
        // =

        private void txtTimBienSo_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTimBienSo.Text.Trim();
            if (string.IsNullOrWhiteSpace(keyword))
            {
                RefreshDangGui();
            }
            else
            {
                _dsDangGui = BUS_GuiXe.Instance.TimXeTheoBienSo(keyword);
                gridXeDangGui.DataSource = _dsDangGui;
                FormatGridDangGui();
            }
        }

        private void gridViewXeDangGui_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridViewXeDangGui.GetFocusedRow() is ET_LuotVaoRaBaiXe luot)
            {
                _selectedLuot = luot;
                ShowChiTietTraXe(luot);
                VerifyExitMatch(); // Đối chiếu mỗi khi chuyển xe
            }
        }

        // =
        // MÔ PHỎNG BARIE TRẢ XE (QR & CAMERA)
        // =

        // Biến lưu biển số được OCR đọc lúc xe chạy ra
        private string _bienSoRaCamera = "";

        private void btnChonAnhQR_TraXe_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                ofd.Title = "Chọn ảnh chứa Mã QR/Barcode (Thẻ/Vé)";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string qrData = BarcodeHelper.ReadBarcodeFromFile(ofd.FileName);
                        if (string.IsNullOrWhiteSpace(qrData))
                        {
                            TDCMessageBox.Show("Không đọc được mã vạch/QR từ ảnh này!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        ProcessScannedRfid(qrData);
                    }
                    catch (Exception ex)
                    {
                        TDCMessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnChonAnhCamera_TraXe_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                ofd.Title = "Chọn ảnh Biển Số Lúc Trả Xe (Giả lập Camera)";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    ProcessExitCameraImage(ofd.FileName);
                }
            }
        }

        /// <summary>
        /// Hàm tách rời để xử lý thẻ RFID. Có thể dùng cho đọc file QR hoặc quẹt máy vật lý.
        /// </summary>
        public void ProcessScannedRfid(string rfidData)
        {
            if (string.IsNullOrWhiteSpace(rfidData)) return;

            ET_LuotVaoRaBaiXe match = null;
            if (rfidData.StartsWith("QR-"))
            {
                if (int.TryParse(rfidData.Substring(3), out int parsedId))
                {
                    match = _dsDangGui.FirstOrDefault(x => x.Id == parsedId);
                }
            }
            else
            {
                match = _dsDangGui.FirstOrDefault(x => x.MaRfid == rfidData);
            }
            if (match != null)
            {
                // Chọn xe trên Grid
                int rowHandle = gridViewXeDangGui.LocateByValue("Id", match.Id);
                if (rowHandle >= 0)
                {
                    gridViewXeDangGui.FocusedRowHandle = rowHandle;
                    TDCMessageBox.Show("Đã nhận diện vé giữ xe hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                TDCMessageBox.Show("Thẻ/Vé không hợp lệ hoặc xe không có trong bãi!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Hàm tách rời để xử lý ảnh camera lối ra
        /// </summary>
        public async void ProcessExitCameraImage(string imagePath)
        {
            try
            {
                string rawText = await System.Threading.Tasks.Task.Run(() => OcrHelper.RecognizeText(imagePath));
                string bienSo = OcrHelper.ExtractLicensePlate(rawText);
                
                this.Invoke(new MethodInvoker(delegate {
                    _bienSoRaCamera = string.IsNullOrWhiteSpace(bienSo) ? "[Không đọc được]" : bienSo;
                    VerifyExitMatch();
                    TDCMessageBox.Show("📷 Camera lối ra đọc được biển số: " + _bienSoRaCamera, "Camera", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }));
            }
            catch (Exception ex)
            {
                TDCMessageBox.Show("Lỗi camera lối ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Đối chiếu giữa biển số lúc vào (DB) và biển số xe ra thực tế (Camera)
        /// </summary>
        private void VerifyExitMatch()
        {
            if (_selectedLuot == null || string.IsNullOrEmpty(_bienSoRaCamera)) return;

            if (_selectedLuot.BienSo.Equals(_bienSoRaCamera, StringComparison.OrdinalIgnoreCase))
            {
                lblTraBienSo.ForeColor = Color.Green;
                lblTraBienSo.Text = _selectedLuot.BienSo + " (KHỚP)";
            }
            else
            {
                lblTraBienSo.ForeColor = Color.Red;
                lblTraBienSo.Text = _selectedLuot.BienSo + " (SAI LỆCH)";
            }
        }

        private void btnTraXeTienMat_Click(object sender, EventArgs e)
        {
            TraXe(AppConstants.PhuongThucThanhToan.TienMat);
        }

        private void btnTraXeChuyenKhoan_Click(object sender, EventArgs e)
        {
            TraXe(AppConstants.PhuongThucThanhToan.ChuyenKhoan);
        }

        private void btnTraXeRfid_Click(object sender, EventArgs e)
        {
            TraXe(AppConstants.PhuongThucThanhToan.ViRfid);
        }

        // =
        // HELPER METHODS
        // =

        private void LoadAnhBienSo(string filePath)
        {
            try
            {
                picBienSo.Image?.Dispose();
                picBienSo.Image = Image.FromFile(filePath);
                _anhBienSoPath = filePath;

                string savePath = Path.Combine(Application.StartupPath, "AnhBienSo");
                if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);
                string destFile = Path.Combine(savePath, DateTime.Now.ToString("yyyyMMdd_HHmmss") + Path.GetExtension(filePath));
                if (filePath != destFile) File.Copy(filePath, destFile, true);
                _anhBienSoPath = destFile;

                // Thử OCR tự động đọc biển số (chạy background)
                TryOcrAsync(destFile);
            }
            catch (Exception ex)
            {
                TDCMessageBox.Show("Lỗi load ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Background OCR: dùng Tesseract đọc biển số từ ảnh, tự điền vào txtBienSoNhan.
        /// Chạy ngầm tránh lag UI. Nếu OCR fail -> im lặng, user nhập tay.
        /// </summary>
        private async void TryOcrAsync(string imagePath)
        {
            try
            {
                // Chạy OCR ở thread khác để không đơ giao diện
                string rawText = await System.Threading.Tasks.Task.Run(() => OcrHelper.RecognizeText(imagePath));
                
                if (string.IsNullOrWhiteSpace(rawText)) return;

                string bienSo = OcrHelper.ExtractLicensePlate(rawText);
                
                if (!string.IsNullOrWhiteSpace(bienSo))
                {
                    this.Invoke(new MethodInvoker(delegate {
                        txtBienSoNhan.Text = bienSo;
                        txtBienSoNhan.SelectAll();
                    }));
                }
            }
            catch { /* Silently fail — fallback to manual typing */ }
        }

        private void UpdateGiaDuKien()
        {
            string loaiXe = cboLoaiXe.SelectedValue?.ToString() ?? AppConstants.LoaiXe.XeMay;
            var bangGia = BUS_GuiXe.Instance.LoadBangGia();
            var gia = bangGia.FirstOrDefault(x => x.LoaiXe == loaiXe);
            if (gia != null)
            {
                lblGiaDuKien.Text = string.Format("Giá: {0:N0}đ / lượt | Qua đêm: +{1:N0}đ", gia.GiaBanNgay, gia.GiaQuaDem);
            }
        }

        private void ResetFormNhanXe()
        {
            txtBienSoNhan.Text = "";
            txtRfidNhan.Text = "";
            btnXoaAnh_Click(null, null);
            cboLoaiXe.SelectedIndex = 0;
            RefreshDangGui();
            RefreshDashboard();
            txtBienSoNhan.Focus();
        }

        private void RefreshDangGui()
        {
            _dsDangGui = BUS_GuiXe.Instance.LoadDangGui();
            gridXeDangGui.DataSource = _dsDangGui;
            FormatGridDangGui();
        }

        private void FormatGridDangGui()
        {
            if (gridViewXeDangGui.Columns["Id"] != null) gridViewXeDangGui.Columns["Id"].Visible = false;
            if (gridViewXeDangGui.Columns["MaRfid"] != null) gridViewXeDangGui.Columns["MaRfid"].Visible = false;
            if (gridViewXeDangGui.Columns["AnhBienSo"] != null) gridViewXeDangGui.Columns["AnhBienSo"].Visible = false;
            if (gridViewXeDangGui.Columns["TrangThai"] != null) gridViewXeDangGui.Columns["TrangThai"].Visible = false;
            if (gridViewXeDangGui.Columns["ThoiGianRa"] != null) gridViewXeDangGui.Columns["ThoiGianRa"].Visible = false;
            if (gridViewXeDangGui.Columns["LoaiXe"] != null) gridViewXeDangGui.Columns["LoaiXe"].Visible = false;

            if (gridViewXeDangGui.Columns["BienSo"] != null) gridViewXeDangGui.Columns["BienSo"].Caption = "Biển Số";
            if (gridViewXeDangGui.Columns["TenLoaiXe"] != null) gridViewXeDangGui.Columns["TenLoaiXe"].Caption = "Loại Xe";
            if (gridViewXeDangGui.Columns["ThoiGianVao"] != null)
            {
                gridViewXeDangGui.Columns["ThoiGianVao"].Caption = "Giờ Vào";
                gridViewXeDangGui.Columns["ThoiGianVao"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridViewXeDangGui.Columns["ThoiGianVao"].DisplayFormat.FormatString = "HH:mm dd/MM";
            }
            if (gridViewXeDangGui.Columns["TenTrangThai"] != null) gridViewXeDangGui.Columns["TenTrangThai"].Visible = false;
            if (gridViewXeDangGui.Columns["ThoiGianGuiHienThi"] != null) gridViewXeDangGui.Columns["ThoiGianGuiHienThi"].Caption = "Thời Gian Gửi";
            gridViewXeDangGui.BestFitColumns();
        }

        private void ShowChiTietTraXe(ET_LuotVaoRaBaiXe luot)
        {
            if (luot == null)
            {
                lblTraBienSo.Text = "—";
                lblTraLoaiXe.Text = "—";
                lblTraGioVao.Text = "—";
                lblTraThoiGian.Text = "—";
                lblTraTienPhaiTra.Text = "—";
                picBienSoTra.Image = null;
                return;
            }

            lblTraBienSo.Text = luot.BienSo;
            lblTraLoaiXe.Text = luot.TenLoaiXe;
            lblTraGioVao.Text = luot.ThoiGianVao.ToString("HH:mm dd/MM/yyyy");
            lblTraThoiGian.Text = luot.ThoiGianGuiHienThi;

            decimal tienPhaiTra = BUS_GuiXe.Instance.TinhTienGuiXe(luot.LoaiXe, luot.ThoiGianVao, DateTime.Now);
            lblTraTienPhaiTra.Text = string.Format("{0:N0} đ", tienPhaiTra);

            try
            {
                if (!string.IsNullOrEmpty(luot.AnhBienSo) && File.Exists(luot.AnhBienSo))
                {
                    picBienSoTra.Image?.Dispose();
                    picBienSoTra.Image = Image.FromFile(luot.AnhBienSo);
                }
                else
                {
                    picBienSoTra.Image = null;
                }
            }
            catch { picBienSoTra.Image = null; }
        }

        private void TraXe(string phuongThuc)
        {
            if (_selectedLuot == null)
            {
                TDCMessageBox.Show("Vui lòng chọn xe cần trả từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal tienPhaiTra = BUS_GuiXe.Instance.TinhTienGuiXe(_selectedLuot.LoaiXe, _selectedLuot.ThoiGianVao, DateTime.Now);
            string tenPT = phuongThuc == AppConstants.PhuongThucThanhToan.TienMat ? "Tiền mặt" :
                           phuongThuc == AppConstants.PhuongThucThanhToan.ChuyenKhoan ? "Chuyển khoản" : "Ví RFID";

            var confirm = TDCMessageBox.Show(
                string.Format("Xác nhận trả xe?\n\nBiển số: {0}\nLoại xe: {1}\nThời gian gửi: {2}\nTiền phải trả: {3:N0}đ\nPhương thức: {4}",
                    _selectedLuot.BienSo, _selectedLuot.TenLoaiXe, _selectedLuot.ThoiGianGuiHienThi, tienPhaiTra, tenPT),
                "Xác nhận trả xe", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            var result = BUS_GuiXe.Instance.TraXe(_selectedLuot.Id, phuongThuc);

            if (result.IsSuccess)
            {
                TDCMessageBox.Show("Đã trả xe", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _selectedLuot = null;
                ShowChiTietTraXe(null);
                _bienSoRaCamera = ""; // Reset camera buffer
                RefreshDangGui();
                RefreshDashboard();
            }
            else
            {
                TDCMessageBox.Show(result.ErrorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _dashboardTimer?.Stop();
            _dashboardTimer?.Dispose();
            base.OnFormClosed(e);
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraEditors;
using BUS.Services.BanHang;
using ET.Models.DanhMuc;
using ET.Models.BanHang;
using ET.Constants;
using GUI.Infrastructure;
using ET.Results;

namespace GUI.Modules.VanHanh
{
    public partial class ucKiemSoatCong : XtraUserControl
    {
        private CameraScanner _scanner;
        private int _totalScanned = 0;
        private int _validCount = 0;
        private int _invalidCount = 0;
        private List<ET_LichSuQuet> _historyList = new List<ET_LichSuQuet>();
        private bool _isFlashState = false;
        private Color _originalFeedbackColor = Color.FromArgb(240, 244, 248);

        private readonly Action<object> _onLanguageChanged;

        public ucKiemSoatCong()
        {
            InitializeComponent();
            
            _onLanguageChanged = _ => {
                if (this.IsHandleCreated && !this.IsDisposed)
                {
                    this.Invoke((MethodInvoker)delegate {
                        ApplyLanguage();
                    });
                }
            };
            EventBus.Subscribe("LanguageChanged", _onLanguageChanged);

            this.Load += UcKiemSoatCong_Load;
        }

        private void UcKiemSoatCong_Load(object sender, EventArgs e)
        {
            LoadData();
            ApplyLanguage();
            UpdateCounterLabel();
        }

        private void LoadData()
        {
            try
            {
                // Load Khu Vuc
                var dsKhuVuc = BUS_KiemSoatVe.Instance.LayDanhSachKhuVuc();
                cboKhuVuc.Properties.DataSource = dsKhuVuc;
                cboKhuVuc.Properties.DisplayMember = "TenKhuVuc";
                cboKhuVuc.Properties.ValueMember = "Id";
                cboKhuVuc.Properties.Columns.Clear();
                cboKhuVuc.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKhuVuc", LanguageManager.GetString("KHU_VUC") ?? "Khu Vực"));
                cboKhuVuc.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;

                // Load Tro Choi
                LoadTroChoi(null);

                RefreshHistory();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTroChoi(int? idKhuVuc)
        {
            var dsTroChoi = BUS_KiemSoatVe.Instance.LayDanhSachTroChoi(idKhuVuc);
            cboTroChoi.Properties.DataSource = dsTroChoi;
            cboTroChoi.Properties.DisplayMember = "TenTroChoi";
            cboTroChoi.Properties.ValueMember = "Id";
            cboTroChoi.Properties.Columns.Clear();
            cboTroChoi.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTroChoi", LanguageManager.GetString("TEN_TRO_CHOI") ?? "Tên Trò Chơi"));
            cboTroChoi.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
        }

        private void CboKhuVuc_EditValueChanged(object sender, EventArgs e)
        {
            int? idKhuVuc = cboKhuVuc.EditValue as int?;
            LoadTroChoi(idKhuVuc);
            RefreshHistory();
        }

        private void CboTroChoi_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void TxtScanInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string barcode = txtScanInput.Text.Trim();
                if (!string.IsNullOrWhiteSpace(barcode))
                {
                    ProcessScan(barcode);
                    txtScanInput.Text = "";
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void ProcessScan(string barcode)
        {
            int? idKhuVuc = cboKhuVuc.EditValue as int?;
            int? idTroChoi = cboTroChoi.EditValue as int?;

            int? idThietBi = 1; 

            var result = BUS_KiemSoatVe.Instance.SoatVe(barcode, idKhuVuc, idTroChoi, idThietBi);

            _totalScanned++;

            if (result.Success && result.Data != null)
            {
                var data = result.Data;
                UpdateTicketUI(data);

                if (data.MaKetQua == 0)
                {
                    _validCount++;
                    TriggerSuccessFeedback();
                }
                else
                {
                    _invalidCount++;
                    TriggerFailureFeedback(data.ThongBaoKey);
                }
            }
            else
            {
                _invalidCount++;
                TriggerFailureFeedback(result.Message);
            }

            UpdateCounterLabel();
            RefreshHistory();
        }

        private void UpdateTicketUI(ET_KetQuaSoatVe data)
        {
            if (data.VeInfo != null)
            {
                lblTicketMaVe.Text = data.VeInfo.MaVach;
                lblTicketTenVe.Text = data.VeInfo.TenSanPham;
                lblTicketLoaiVe.Text = $"{data.VeInfo.LoaiVe} ({data.VeInfo.DoiTuongVe})";
                lblTicketLuot.Text = data.VeInfo.SoLuotConLai.ToString();
                lblTicketNgayMua.Text = data.VeInfo.NgayTao.ToString("dd/MM/yyyy");
                lblTicketHetHan.Text = data.VeInfo.NgayHetHan?.ToString("dd/MM/yyyy") ?? "---";
                lblTicketKhach.Text = data.VeInfo.TenKhachHang ?? "---";
                lblTicketRFID.Text = "---";
            }
            else
            {
                lblTicketMaVe.Text = "---";
                lblTicketTenVe.Text = "---";
                lblTicketLoaiVe.Text = "---";
                lblTicketLuot.Text = "---";
                lblTicketNgayMua.Text = "---";
                lblTicketHetHan.Text = "---";
                lblTicketKhach.Text = "---";
                lblTicketRFID.Text = "---";
            }
        }

        private void TriggerSuccessFeedback()
        {
            timerReset.Stop();
            timerFlash.Stop();

            pnlFeedback.Appearance.BackColor = Color.FromArgb(240, 249, 241); 
            lblVerdict.Appearance.BackColor = Color.FromArgb(240, 249, 241);
            lblVerdict.ForeColor = Color.FromArgb(46, 125, 50);
            lblVerdict.Text = LanguageManager.GetString("HOP_LE") ?? "HỢP LỆ";
            lblStatusTitle.Appearance.ForeColor = Color.FromArgb(46, 125, 50);
            lblStatusTitle.Text = LanguageManager.GetString("HOP_LE") ?? "HỢP LỆ — Cho vào";
            lblStatusSub.Appearance.ForeColor = Color.FromArgb(46, 125, 50);
            lblStatusSub.Text = LanguageManager.GetString("THAO_TAC_THANH_CONG") ?? "Thao tác thành công";

            timerReset.Start();
        }

        private void TriggerFailureFeedback(string errorKey)
        {
            timerReset.Stop();
            timerFlash.Stop();

            pnlFeedback.Appearance.BackColor = Color.FromArgb(254, 242, 242); 
            lblVerdict.Appearance.BackColor = Color.FromArgb(254, 242, 242);
            lblVerdict.ForeColor = Color.FromArgb(198, 40, 40);

            string translated = errorKey;
            try
            {
                translated = LanguageManager.GetString(errorKey);
                if (string.IsNullOrEmpty(translated))
                    translated = errorKey;
            }
            catch { }

            lblVerdict.Text = LanguageManager.GetString("TU_CHOI") ?? "TỪ CHỐI";
            lblStatusTitle.Appearance.ForeColor = Color.FromArgb(198, 40, 40);
            lblStatusTitle.Text = LanguageManager.GetString("TU_CHOI") ?? "Từ chối — Vé không hợp lệ";
            lblStatusSub.Appearance.ForeColor = Color.FromArgb(198, 40, 40);
            lblStatusSub.Text = translated;

            timerReset.Start();
        }

        private void TimerReset_Tick(object sender, EventArgs e)
        {
            timerReset.Stop();
            timerFlash.Stop();
            pnlFeedback.Appearance.BackColor = _originalFeedbackColor;
            lblVerdict.Appearance.BackColor = Color.FromArgb(200, 220, 240);
            lblVerdict.ForeColor = Color.FromArgb(30, 60, 114);
            lblVerdict.Text = LanguageManager.GetString("CHO_QUET") ?? "CHỜ QUÉT";
            lblStatusTitle.Appearance.ForeColor = Color.FromArgb(30, 60, 114);
            lblStatusTitle.Text = LanguageManager.GetString("MOI_QUET_VE") ?? "MỜI QUÉT VÉ";
            lblStatusSub.Appearance.ForeColor = Color.FromArgb(71, 85, 105);
            lblStatusSub.Text = "---";
        }

        private void TimerFlash_Tick(object sender, EventArgs e)
        {
            _isFlashState = !_isFlashState;
            if (_isFlashState)
            {
                pnlFeedback.Appearance.BackColor = Color.FromArgb(76, 175, 80); 
            }
            else
            {
                pnlFeedback.Appearance.BackColor = Color.FromArgb(46, 125, 50);
            }
        }

        private void UpdateCounterLabel()
        {
            lblKPI_GuestsValue.Text = _totalScanned.ToString("N0");
            lblKPI_SuccessValue.Text = _validCount.ToString("N0");
            lblKPI_RejectedValue.Text = _invalidCount.ToString("N0");
            
            double percent = _totalScanned > 0 ? ((double)_validCount / _totalScanned) * 100 : 100;
            lblKPI_SuccessSub.Text = $"{percent:N1}%";
            lblKPI_SuccessSub.Visible = true;
        }

        private void RefreshHistory()
        {
            int? idKhuVuc = cboKhuVuc.EditValue as int?;
            _historyList = BUS_KiemSoatVe.Instance.LayLichSu(idKhuVuc);
            gridLichSu.DataSource = _historyList;
            viewLichSu.RefreshData();
        }

        private void BtnToggleCamera_Click(object sender, EventArgs e)
        {
            if (_scanner == null)
            {
                _scanner = new CameraScanner();
                _scanner.OnBarcodeDetected += Scanner_OnBarcodeDetected;
                _scanner.OnError += Scanner_OnError;
            }

            if (_scanner.IsRunning)
            {
                _scanner.Stop();
                btnToggleCamera.Text = "Bat Cam";
                picCamera.Visible = false;
            }
            else
            {
                picCamera.Visible = true;
                if (_scanner.Start(picCamera, 0))
                {
                    btnToggleCamera.Text = "Tat Cam";
                }
                else
                {
                    picCamera.Visible = false;
                }
            }
        }

        private void Scanner_OnBarcodeDetected(string barcode)
        {
            if (!string.IsNullOrWhiteSpace(barcode))
            {
                ProcessScan(barcode);
            }
        }

        private void Scanner_OnError(string error)
        {
            XtraMessageBox.Show(error, "Camera Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void BtnScanFile_Click(object sender, EventArgs e)
        {
            if (_scanner == null)
            {
                _scanner = new CameraScanner();
                _scanner.OnBarcodeDetected += Scanner_OnBarcodeDetected;
                _scanner.OnError += Scanner_OnError;
            }
            _scanner.ScanFromFile();
        }

        private void ApplyLanguage()
        {
            lblTitle.Text = LanguageManager.GetString("CONG_KIEM_SOAT_VE_DAI_NAM") ?? "CỔNG KIỂM SOÁT VÉ ĐẠI NAM";
            txtScanInput.Properties.NullValuePrompt = LanguageManager.GetString("MOI_QUET_VE") ?? "Nhập mã hoặc quét barcode -> Enter...";

            lblKPI_GuestsTitle.Text = LanguageManager.GetString("KHACH_VAO_HOM_NAY") ?? "Khách vào hôm nay";
            lblKPI_SuccessTitle.Text = LanguageManager.GetString("QUET_THANH_CONG") ?? "Quét thành công";
            lblKPI_RejectedTitle.Text = LanguageManager.GetString("BI_TU_CHOI") ?? "Bị từ chối";
            
            btnToggleCamera.Text = LanguageManager.GetString("BTN_CAMERA") ?? "Bật cam";
            
            lblTicketHeaderTitle.Text = LanguageManager.GetString("THONG_TIN_VE") ?? "Thông tin vé";
            
            lblTitle_MaVe.Text = LanguageManager.GetString("MA_VE") ?? "Mã vé:";
            lblTitle_TenVe.Text = LanguageManager.GetString("TEN_VE") ?? "Tên vé:";
            lblTitle_LoaiVe.Text = LanguageManager.GetString("LOAI_VE") ?? "Loại vé:";
            lblTitle_KhuVuc.Text = LanguageManager.GetString("KHU_VUC") ?? "Khu vực:";
            lblTitle_Luot.Text = LanguageManager.GetString("LUOT_CON") ?? "Lượt còn:";
            lblTitle_NgayMua.Text = LanguageManager.GetString("NGAY_TAO") ?? "Ngày tạo:";
            lblTitle_HetHan.Text = LanguageManager.GetString("HET_HAN") ?? "Hết hạn:";
            lblTitle_Khach.Text = LanguageManager.GetString("KHACH_HANG") ?? "Khách hàng:";
            lblTitle_RFID.Text = LanguageManager.GetString("RFID") ?? "RFID:";

            lblTicketMaVe.Text = "---";
            lblTicketTenVe.Text = "---";
            lblTicketLoaiVe.Text = "---";
            lblTicketKhuVuc.Text = "---";
            lblTicketLuot.Text = "---";
            lblTicketNgayMua.Text = "---";
            lblTicketHetHan.Text = "---";
            lblTicketKhach.Text = "---";
            lblTicketRFID.Text = "---";

            colGio.Caption = LanguageManager.GetString("GIO") ?? "Giờ";
            colMaVe.Caption = LanguageManager.GetString("MA_VE") ?? "Mã Vé";
            colKetQua.Caption = LanguageManager.GetString("KET_QUA") ?? "Kết Quả";
            colTenVe.Caption = LanguageManager.GetString("TEN_VE") ?? "Tên Vé";
            colKhuVuc.Caption = LanguageManager.GetString("KHU_VUC") ?? "Khu Vực";
            colLuotConLai.Caption = LanguageManager.GetString("LUOT_CON") ?? "Lượt Còn";
            lblVerdict.Text = LanguageManager.GetString("CHO_QUET") ?? "CHỜ QUÉT";
            lblStatusTitle.Text = LanguageManager.GetString("MOI_QUET_VE") ?? "MỜI QUÉT VÉ";
        }

        private void ViewLichSu_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == colKetQua)
            {
                string val = e.CellValue?.ToString() ?? "";
                bool isSuccess = val.ToLower().Contains("hợp lệ") || val.ToLower().Contains("thành công");

                e.DefaultDraw();

                Color dotColor = isSuccess ? Color.Green : Color.Red;
                using (Brush brush = new SolidBrush(dotColor))
                {
                    e.Cache.FillEllipse(brush, new Rectangle(e.Bounds.X + 5, e.Bounds.Y + (e.Bounds.Height - 10) / 2, 10, 10));
                }

                e.Handled = true;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using ET.Models.BanHang;
using ET.Constants;
using BUS.Services.BanHang;
using GUI.Infrastructure;

namespace GUI.Modules.BanHang
{
    public partial class frmThanhToan : DevExpress.XtraEditors.XtraForm
    {
        private readonly decimal _tongPhaiThu;
        private DataTable _dtThanhToan;
        
        // Dữ liệu điểm
        private int _diemHienCo;
        private int _diemToiDaDuocDung;
        private decimal _tyLeQuyDoi;
        private bool _isManualPaymentEntered = false;

        // Output: danh sách dòng thanh toán sau khi user xác nhận
        public List<ET_PaymentLine> DanhSachThanhToan { get; private set; }

        private bool _hasDiscount;

        public frmThanhToan(decimal tongPhaiThu, int diemTichLuy = 0, bool hasDiscount = false)
        {
            InitializeComponent();
            _tongPhaiThu = tongPhaiThu;
            _diemHienCo = diemTichLuy;
            _hasDiscount = hasDiscount;
            
            ApplyLanguage();
            lblTongPhaiThuValue.Text = tongPhaiThu.ToString("#,##0") + " ₫";
            
            KhoiTaoThongTinDiem();
            KhoiTaoBangThanhToan();
            
            viewThanhToan.CustomColumnDisplayText += ViewThanhToan_CustomColumnDisplayText;
        }

        private void KhoiTaoThongTinDiem()
        {
            if (_diemHienCo <= 0 || _tongPhaiThu <= 0) return;

            // Load cấu hình tỷ lệ quy đổi
            _tyLeQuyDoi = BUS_CauHinh.Instance.LayGiaTriDecimal(AppConstants.ConfigKeys.DIEM_QUY_DOI, 1000m);
            // Load giới hạn phần trăm thanh toán bằng điểm (mặc định 50% đơn hàng)
            decimal tyLePhanTram = BUS_CauHinh.Instance.LayGiaTriDecimal(AppConstants.ConfigKeys.DIEM_CAP_PHAN_TRAM, 50m);
            if (_tyLeQuyDoi <= 0) return;

            // Tính số tiền được phép trừ tối đa (Dựa theo phần trăm giới hạn)
            decimal tienToiDa = _tongPhaiThu * (tyLePhanTram / 100m);
            int diemMaxCanThiet = (int)(tienToiDa / _tyLeQuyDoi);

            _diemToiDaDuocDung = Math.Min(_diemHienCo, diemMaxCanThiet);

            if (_diemToiDaDuocDung > 0)
            {
                pnlDiem.Visible = true;
                if (_hasDiscount)
                {
                    spinDiemDung.Enabled = false;
                    lblDiemCoSan.Text = LanguageManager.GetString("PAY_POINTS_DISABLED_DISCOUNT") ?? "Không được cộng dồn (Đã có KM/VIP)";
                    lblDiemCoSan.ForeColor = Color.Red;
                }
                else
                {
                    lblDiemCoSan.Text = string.Format(
                        LanguageManager.GetString("PAY_POINTS_AVAILABLE") ?? "Có {0} điểm. Dùng:",
                        _diemHienCo.ToString("#,##0"));
                    spinDiemDung.Properties.MaxValue = _diemToiDaDuocDung;
                    spinDiemDung.Properties.MinValue = 0;
                    spinDiemDung.EditValue = 0;
                    CapNhatQuyDoiDiem();
                }
            }
        }

        private void KhoiTaoBangThanhToan()
        {
            _dtThanhToan = new DataTable();
            _dtThanhToan.Columns.Add("PhuongThuc", typeof(string));
            _dtThanhToan.Columns.Add("SoTien", typeof(decimal));
            _dtThanhToan.Columns.Add("GhiChu", typeof(string));

            // mặc định : 1 dòng tiền mặt = tổng
            _dtThanhToan.Rows.Add("TienMat", _tongPhaiThu, DBNull.Value);
            _dtThanhToan.RowChanged += DtThanhToan_RowChanged;
            gridThanhToan.DataSource = _dtThanhToan;
            CapNhatDoiSoat();
        }

        private void DtThanhToan_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (e.Action == DataRowAction.Change || e.Action == DataRowAction.Add)
            {
                _isManualPaymentEntered = true;
            }
        }

        #region Xử lý sự kiện

        private void BtnThemDong_Click(object sender, EventArgs e)
        {
            _dtThanhToan.Rows.Add("TienMat", 0m, DBNull.Value);
            CapNhatDoiSoat();
        }

        private void ViewThanhToan_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "SoTien")
            {
                _isManualPaymentEntered = true;
            }

            // Kiểm tra nếu phương thức = ViRFID thì hiện panel RFID
            if (e.Column.FieldName == "PhuongThuc")
            {
                bool coViRFID = _dtThanhToan.AsEnumerable()
                    .Any(r => r["PhuongThuc"]?.ToString() == AppConstants.PhuongThucTT.ViRFID);
                pnlRFID.Visible = coViRFID;
                _isManualPaymentEntered = true;
            }
            CapNhatDoiSoat();
        }

        private void ViewThanhToan_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "PhuongThuc" && e.Value != null)
            {
                string valueStr = e.Value.ToString();
                if (valueStr == AppConstants.PhuongThucTT.TienMat)
                    e.DisplayText = LanguageManager.GetString("PTTT_TIENMAT") ?? "Tiền mặt";
                else if (valueStr == AppConstants.PhuongThucTT.ChuyenKhoan)
                    e.DisplayText = LanguageManager.GetString("PTTT_CHUYENKHOAN") ?? "Chuyển khoản";
                else if (valueStr == AppConstants.PhuongThucTT.ViRFID)
                    e.DisplayText = LanguageManager.GetString("PTTT_VIRFID") ?? "Ví RFID";
                else if (valueStr == AppConstants.PhuongThucTT.QR)
                    e.DisplayText = LanguageManager.GetString("PTTT_QR") ?? "QR Code";
            }
        }

        private void SpinDiemDung_EditValueChanged(object sender, EventArgs e)
        {
            CapNhatQuyDoiDiem();
            if (!_isManualPaymentEntered && _dtThanhToan.Rows.Count > 0)
            {
                int.TryParse(spinDiemDung.EditValue?.ToString(), out int diemDung);
                decimal tongTienDiem = diemDung * _tyLeQuyDoi;
                decimal conLai = _tongPhaiThu - tongTienDiem;
                if (conLai < 0) conLai = 0;
                
                // Tạm thời tắt cờ để không bị đánh dấu là manual khi code tự update
                _dtThanhToan.RowChanged -= DtThanhToan_RowChanged;
                _dtThanhToan.Rows[0]["SoTien"] = conLai;
                _dtThanhToan.RowChanged += DtThanhToan_RowChanged;
            }
            CapNhatDoiSoat();
        }

        private void BtnVuaDu_Click(object sender, EventArgs e)
        {
            if (_dtThanhToan.Rows.Count == 0) return;
            
            int.TryParse(spinDiemDung.EditValue?.ToString(), out int diemDung);
            decimal tongTienDiem = diemDung * _tyLeQuyDoi;
            
            decimal daTra = _dtThanhToan.AsEnumerable()
                .Skip(1).Sum(r => r.Field<decimal>("SoTien"));
                
            decimal conLai = _tongPhaiThu - tongTienDiem - daTra;
            if (conLai < 0) conLai = 0;
            
            _dtThanhToan.Rows[0]["SoTien"] = conLai;
            _isManualPaymentEntered = true;
            CapNhatDoiSoat();
        }

        private void Btn500k_Click(object sender, EventArgs e)
        {
            if (_dtThanhToan.Rows.Count == 0) return;
            _dtThanhToan.Rows[0]["SoTien"] = 500000m;
            _isManualPaymentEntered = true;
            CapNhatDoiSoat();
        }

        private void Btn1tr_Click(object sender, EventArgs e)
        {
            if (_dtThanhToan.Rows.Count == 0) return;
            _dtThanhToan.Rows[0]["SoTien"] = 1000000m;
            _isManualPaymentEntered = true;
            CapNhatDoiSoat();
        }

        private void BtnXacNhan_Click(object sender, EventArgs e)
        {
            // Validate: không cho số tiền âm
            foreach (DataRow row in _dtThanhToan.Rows)
            {
                decimal soTien = row.Field<decimal>("SoTien");
                if (soTien < 0)
                {
                    XtraMessageBox.Show(
                        LanguageManager.GetString(AppConstants.ErrorMessages.ERR_PAY_NEGATIVE_AMOUNT) ?? "Số tiền thanh toán không được âm!",
                        "POS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            int.TryParse(spinDiemDung.EditValue?.ToString(), out int diemDungXacNhan);
            decimal tongTienDiem = diemDungXacNhan * _tyLeQuyDoi;
            decimal tongTraManual = _dtThanhToan.AsEnumerable().Sum(r => r.Field<decimal>("SoTien"));
            decimal tongTra = tongTraManual + tongTienDiem;

            if (tongTra < _tongPhaiThu)
            {
                XtraMessageBox.Show(
                    LanguageManager.GetString(AppConstants.ErrorMessages.ERR_POS_PAYMENT_INSUFFICIENT) ?? "Số tiền trả chưa đủ!",
                     "POS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tìm IdViDienTu nếu có dòng Vi RFID
            bool coRFID = _dtThanhToan.AsEnumerable()
                .Any(r => r["PhuongThuc"]?.ToString() == AppConstants.PhuongThucTT.ViRFID);
            int? idViDienTu = null;

            if (coRFID)
            {
                string maThe = txtMaThe.Text.Trim();
                if (string.IsNullOrEmpty(maThe))
                {
                    XtraMessageBox.Show(
                        LanguageManager.GetString(AppConstants.ErrorMessages.ERR_POS_RFID_EMPTY) ?? "Vui lòng nhập mã thẻ RFID!",
                        "POS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var rfidResult = BUS_POS.Instance.TraCuuTheRFID(maThe);
                if (!rfidResult.Success)
                {
                    XtraMessageBox.Show(
                        LanguageManager.GetString(rfidResult.Message) ?? rfidResult.Message,
                        "POS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var rfidData = rfidResult.Data;
                var prop = rfidData.GetType().GetProperty("IdViDienTu");
                idViDienTu = prop != null ? (int?)prop.GetValue(rfidData) : null;
            }

            DanhSachThanhToan = new List<ET_PaymentLine>();

            // 1. Thêm dòng dùng điểm (nếu có)
            int.TryParse(spinDiemDung.EditValue?.ToString(), out int diemDung);
            if (diemDung > 0)
            {
                decimal tienTuDiem = diemDung * _tyLeQuyDoi;
                DanhSachThanhToan.Add(new ET_PaymentLine
                {
                    PhuongThuc = AppConstants.PhuongThucTT.DiemTichLuy,
                    SoTien = tienTuDiem,
                    DiemQuyDoi = diemDung,
                    GhiChu = $"Quy đổi {diemDung:#,##0} điểm"
                });
            }

            // 2. Thêm các dòng 
            foreach (DataRow r in _dtThanhToan.Rows)
            {
                decimal soTienDong = r.Field<decimal>("SoTien");
                if (soTienDong <= 0) continue;

                var line = new ET_PaymentLine
                {
                    PhuongThuc = r["PhuongThuc"]?.ToString() ?? AppConstants.PhuongThucTT.TienMat,
                    SoTien = soTienDong,
                    GhiChu = r["GhiChu"] == DBNull.Value ? null : r["GhiChu"]?.ToString()
                };

                if (line.PhuongThuc == AppConstants.PhuongThucTT.ViRFID)
                {
                    line.MaTheRFID = txtMaThe.Text.Trim();
                    line.IdViDienTu = idViDienTu;
                }

                DanhSachThanhToan.Add(line);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion

        #region Hàm hỗ trợ

        private void ApplyLanguage()
        {
            this.Text = LanguageManager.GetString("PAY_TITLE") ?? "Thanh toán";
            lblTieuDe.Text = LanguageManager.GetString("PAY_HEADER") ?? "THANH TOÁN";
            lblTongPhaiThu.Text = LanguageManager.GetString("PAY_TOTAL_DUE") ?? "Tổng phải thu:";

            // Grid
            colPhuongThuc.Caption = LanguageManager.GetString("PAY_COL_METHOD") ?? "Phương thức";
            colSoTien.Caption = LanguageManager.GetString("PAY_COL_AMOUNT") ?? "Số tiền";
            colGhiChu.Caption = LanguageManager.GetString("PAY_COL_NOTE") ?? "Ghi chú";

            btnThemDong.Text = LanguageManager.GetString("PAY_BTN_ADD_LINE") ?? "+ Thêm dòng thanh toán";
            btnVuaDu.Text = LanguageManager.GetString("PAY_BTN_EXACT") ?? "Vừa đủ";

            // Đối soát
            lblDaTra.Text = LanguageManager.GetString("PAY_PAID") ?? "Đã trả:";
            lblConThieu.Text = LanguageManager.GetString("PAY_REMAINING") ?? "Còn thiếu:";
            lblTienThua.Text = LanguageManager.GetString("PAY_CHANGE") ?? "Tiền thừa:";

            // RFID
            lblMaThe.Text = LanguageManager.GetString("PAY_RFID_CODE") ?? "Mã thẻ:";

            // Nút
            btnHuy.Text = LanguageManager.GetString("PAY_BTN_CANCEL") ?? "HỦY (Esc)";
            btnXacNhan.Text = LanguageManager.GetString("PAY_BTN_CONFIRM") ?? "XÁC NHẬN";
        }

        private void CapNhatDoiSoat()
        {
            int.TryParse(spinDiemDung.EditValue?.ToString(), out int diemDung);
            decimal tongTienDiem = diemDung * _tyLeQuyDoi;
            decimal tongTraManual = _dtThanhToan.AsEnumerable().Sum(r => r.Field<decimal>("SoTien"));
            
            decimal tongTra = tongTraManual + tongTienDiem;
            decimal conThieu = Math.Max(0, _tongPhaiThu - tongTra);
            decimal tienThua = Math.Max(0, tongTra - _tongPhaiThu);

            lblDaTraValue.Text = tongTra.ToString("#,##0");
            lblConThieuValue.Text = conThieu.ToString("#,##0");
            lblTienThuaValue.Text = tienThua.ToString("#,##0");

            // tắt nút xác nhận khi chưa đủ
            btnXacNhan.Enabled = (tongTra >= _tongPhaiThu);
        }

        private void CapNhatQuyDoiDiem()
        {
            int.TryParse(spinDiemDung.EditValue?.ToString(), out int diemDung);
            decimal tienTuDiem = diemDung * _tyLeQuyDoi;
            lblQuyDoiDiem.Text = $"= {tienTuDiem:#,##0}₫";
        }

        #endregion
    }
}

using ET.Constants;
using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BUS.Services.BanHang;
using ET.Models.BanHang;
using ET.Results;
using GUI.Infrastructure;
using BUS.Services.HeThong;

namespace GUI.Modules.BanHang
{
    ///   laMo = true  -> Mở phiên mới (nhập MaMay, TienDauCa, Kho)
    ///   laMo = false -> Đóng phiên (nhập TienCuoiCa, hiển thị chênh lệch)
    public partial class frmPhienThuNgan : DevExpress.XtraEditors.XtraForm
    {
        private readonly ET_PhienThuNgan _phienHienTai;
        private readonly bool _laMoPhien;
        private readonly BUS_NhatKy _nhatKy = BUS_NhatKy.Instance;

        // Output: phiên kết quả sau khi mở thành công
        public ET_PhienThuNgan PhienKetQua { get; private set; }

        public frmPhienThuNgan(ET_PhienThuNgan phienHienTai, bool laMoPhien)
        {
            InitializeComponent();
            _phienHienTai = phienHienTai;
            _laMoPhien = laMoPhien;
            ApplyLanguage();
            CauHinhCheDo();
        }

        private void CauHinhCheDo()
        {
            try 
            {
                cboKho.Properties.DataSource = BUS.Services.Kho.BUS_Kho.Instance.GetKhoHoatDong(SessionManager.CurrentLanguage);
                cboKho.Properties.DisplayMember = "TenKho";
                cboKho.Properties.ValueMember = "Id";

                cboMaMay.Properties.DataSource = BUS.Services.BanHang.BUS_DiemBanHang.Instance.GetAllDiemBanHangHoatDong();
                cboMaMay.Properties.DisplayMember = "TenDiemBan";
                cboMaMay.Properties.ValueMember = "MaDiemBan";
                cboMaMay.Properties.NullText = LanguageManager.GetString("SES_SELECT_POS") ?? "-- Chọn quầy POS --";
            }
            catch (Exception ex)
            {
                string errMsg = LanguageManager.GetString("ERR_LOAD_PHIEN_COMBOS") ?? $"Không tải được danh sách kho/POS: {ex.Message}";
                DevExpress.XtraEditors.XtraMessageBox.Show(errMsg, "Lỗi",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Warning);
            }

            if (_laMoPhien)
            {
                //  Mode: Mở phiên 
                lblTieuDe.Text = LanguageManager.GetString("SES_OPEN_TITLE") ?? "MỞ PHIÊN THU NGÂN";
                btnXacNhan.Text = LanguageManager.GetString("SES_BTN_OPEN") ?? "MỞ PHIÊN";

                // Ẩn phần đóng phiên
                lblTienCuoi.Visible = false;
                txtTienCuoiCa.Visible = false;
                lblTongThu.Visible = false;
                lblTongThuValue.Visible = false;
                lblChenhLech.Visible = false;
                lblChenhLechValue.Visible = false;

                this.ClientSize = new System.Drawing.Size(450, 300);
                btnHuy.Location = new System.Drawing.Point(230, 240);
                btnXacNhan.Location = new System.Drawing.Point(330, 240);
            }
            else
            {
                // Đóng phiên 
                lblTieuDe.Text = LanguageManager.GetString("SES_CLOSE_TITLE") ?? "ĐÓNG PHIÊN THU NGÂN";
                btnXacNhan.Text = LanguageManager.GetString("SES_BTN_CLOSE") ?? "CHỐT CA";

                // Disable các field mở phiên
                cboMaMay.Enabled = false;
                txtTienDauCa.Enabled = false;
                cboKho.Enabled = false;

                if (_phienHienTai != null)
                {
                    cboMaMay.EditValue = _phienHienTai.IdMayBan;
                    txtTienDauCa.EditValue = _phienHienTai.TienDauCa;
                }

                // CHỈ hiện ô nhập tiền cuối ca, ẩn tổng thu + chênh lệch
                lblTienCuoi.Visible = true;
                txtTienCuoiCa.Visible = true;
                lblTongThu.Visible = false;
                lblTongThuValue.Visible = false;
                lblChenhLech.Visible = false;
                lblChenhLechValue.Visible = false;
            }
        }

        private void ApplyLanguage()
        {
            this.Text = LanguageManager.GetString("SES_FORM_TITLE") ?? "Phiên thu ngân";
            lblMaMay.Text = LanguageManager.GetString("SES_MACHINE") ?? "Mã máy POS:";
            lblTienDau.Text = LanguageManager.GetString("SES_START_CASH") ?? "Tiền đầu ca:";
            lblKho.Text = LanguageManager.GetString("SES_WAREHOUSE") ?? "Kho bán hàng:";
            lblGhiChu.Text = LanguageManager.GetString("SES_NOTE") ?? "Ghi chú:";
            lblTienCuoi.Text = LanguageManager.GetString("SES_END_CASH") ?? "Tiền cuối ca:";
            lblTongThu.Text = LanguageManager.GetString("SES_TOTAL_REV") ?? "Tổng thu trong ca:";
            lblChenhLech.Text = LanguageManager.GetString("SES_DIFF") ?? "Chênh lệch:";
            btnHuy.Text = LanguageManager.GetString("BTN_HUY") ?? "HỦY";
            cboKho.Properties.NullText = LanguageManager.GetString("SES_SELECT_WH") ?? "-- Chọn kho --";
        }

        private void BtnXacNhan_Click(object sender, EventArgs e)
        {
            if (_laMoPhien)
                XuLyMoPhien();
            else
                XuLyDongPhien();
        }

        private void XuLyMoPhien()
        {
            //  kiểm tra
            string maMay = cboMaMay.EditValue?.ToString().Trim();
            if (string.IsNullOrEmpty(maMay))
            {
                XtraMessageBox.Show(
                    LanguageManager.GetString(AppConstants.ErrorMessages.ERR_POS_MACHINE_REQUIRED) ?? "Vui lòng nhập mã máy POS.",
                    "POS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaMay.Focus();
                return;
            }

            decimal tienDauCa = 0;
            if (txtTienDauCa.EditValue != null)
                decimal.TryParse(txtTienDauCa.EditValue.ToString(), out tienDauCa);

            int idNhanVien = SessionManager.IdDoiTac;
            int? idKho = cboKho.EditValue as int?;
            string ghiChu = txtGhiChu.Text.Trim();

            var phien = new ET_PhienThuNgan
            {
                IdNhanVien = idNhanVien,
                IdMayBan = maMay,
                IdKhoBan = idKho,
                TienDauCa = tienDauCa,
                GhiChu = string.IsNullOrEmpty(ghiChu) ? null : ghiChu
            };

            OperationResult result = BUS_PhienThuNgan.Instance.MoPhienMoi(phien);

            if (result.Success)
            {
                PhienKetQua = result.Data as ET_PhienThuNgan;
                _nhatKy.GhiLog("PhienThuNgan", PhienKetQua?.Id ?? 0, "MoPhien", SessionManager.IdDoiTac, maMay, null);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                XtraMessageBox.Show(
                    LanguageManager.GetString(result.Message) ?? result.Message,
                    "POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XuLyDongPhien()
        {
            if (_phienHienTai == null) return;

            decimal tienCuoiCa = 0;
            if (txtTienCuoiCa.EditValue != null)
                decimal.TryParse(txtTienCuoiCa.EditValue.ToString(), out tienCuoiCa);

            // thu ngân khai tiền cuối ca, hệ thống tự tính chênh lệch
            OperationResult result = BUS_PhienThuNgan.Instance.DongPhien(
                _phienHienTai.Id, tienCuoiCa, txtGhiChu.Text.Trim());

            if (result.Success)
            {
                var phienDaDong = result.Data as ET_PhienThuNgan;
                try {
                    _nhatKy.GhiLog("PhienThuNgan", _phienHienTai.Id, "DongPhien", SessionManager.IdDoiTac, _phienHienTai.IdMayBan, "Tiền cuối ca: " + tienCuoiCa);
                } catch { }
                if (phienDaDong != null)
                {
                    // hiện kết quả SAU khi chốt
                    decimal tongThu = phienDaDong.TongThuTrongCa ?? 0;
                    decimal chenhLech = phienDaDong.ChenhLech ?? 0;

                    lblTongThuValue.Text = tongThu.ToString("#,##0");
                    lblChenhLechValue.Text = chenhLech.ToString("+#,##0;-#,##0;0") + " ₫";

                    if (chenhLech != 0)
                        lblChenhLechValue.Appearance.ForeColor = System.Drawing.Color.FromArgb(200, 40, 40);
                    else
                        lblChenhLechValue.Appearance.ForeColor = System.Drawing.Color.FromArgb(0, 120, 60);

                    lblTongThu.Visible = true;
                    lblTongThuValue.Visible = true;
                    lblChenhLech.Visible = true;
                    lblChenhLechValue.Visible = true;

                    // Đổi nút thành ĐÓNG
                    btnXacNhan.Text = LanguageManager.GetString("BTN_DONG") ?? "ĐÓNG";
                    btnXacNhan.Click -= BtnXacNhan_Click;
                    btnXacNhan.Click += BtnDong_Click;
                    txtTienCuoiCa.Enabled = false;
                }
                else
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            else
            {
                XtraMessageBox.Show(
                    LanguageManager.GetString(result.Message) ?? result.Message,
                    "POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDong_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}

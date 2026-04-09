using System;
using System.Drawing;
using System.Windows.Forms;
using BUS;
using ET;

namespace GUI.POS
{
    public partial class frmXuatVeDoan : Form
    {
        private ET_DoanKhach _doanTimThay;

        public frmXuatVeDoan()
        {
            InitializeComponent();
        }

        private void frmXuatVeDoan_Load(object sender, EventArgs e)
        {
            ThemeManager.ApplyTheme(this);
            pnlBookingInfo.Visible = false;
            txtBookingCode.Focus();
        }

        // ============================================================
        //  SEARCH: Tìm đoàn bằng MaBooking hoặc SĐT trưởng đoàn
        // ============================================================

        private void txtBookingCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; TimDoan(); }
        }

        private void btnTimDoan_Click(object sender, EventArgs e)
        {
            TimDoan();
        }

        private void TimDoan()
        {
            string keyword = txtBookingCode.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                TDCMessageBox.Show("Vui lòng nhập Mã Booking hoặc SĐT trưởng đoàn.", "Thiếu thông tin");
                return;
            }

            var doan = BUS_DoanKhach.Instance.GetByBookingCode(keyword);
            if (doan == null)
            {
                TDCMessageBox.Show("Không tìm thấy đoàn nào khớp với: " + keyword, "Không tìm thấy");
                pnlBookingInfo.Visible = false;
                _doanTimThay = null;
                return;
            }

            _doanTimThay = doan;
            HienThiThongTinDoan(doan);
        }

        // ============================================================
        //  DISPLAY: Hiển thị thông tin booking + trạng thái validation
        // ============================================================

        private void HienThiThongTinDoan(ET_DoanKhach doan)
        {
            pnlBookingInfo.Visible = true;

            lblTenDoan.Text = doan.TenDoan;
            lblNguoiDaiDien.Text = doan.NguoiDaiDien ?? "(Chưa có)";
            lblSdt.Text = doan.DienThoaiLienHe ?? "(Chưa có)";
            lblSoLuongVe.Text = doan.SoLuongKhach + " người";
            lblChietKhau.Text = doan.ChietKhau.ToString("N1") + "%";
            lblNgayDen.Text = doan.NgayDen.HasValue ? doan.NgayDen.Value.ToString("dd/MM/yyyy") : "(Chưa xác định)";
            lblTrangThai.Text = doan.TrangThai ?? "N/A";

            // Hiển thị gói combo nếu có
            if (lblCombo != null)
                lblCombo.Text = !string.IsNullOrEmpty(doan.TenCombo) ? "🎁 " + doan.TenCombo : "(Chưa chọn gói)";

            // Validate booking
            bool isValid = doan.IsBookingValid;
            if (isValid)
            {
                lblValidation.Text = "✅ Booking hợp lệ — sẵn sàng xác nhận đoàn đã đến.";
                lblValidation.ForeColor = Color.FromArgb(16, 185, 129);
                lblTrangThai.ForeColor = Color.FromArgb(16, 185, 129);
                btnXuatVe.Enabled = true;
            }
            else
            {
                string reason = "";
                if (doan.TrangThai == AppConstants.TrangThaiDoanKhach.DaXuatVe)
                    reason = "Đoàn này ĐÃ ĐƯỢC XÁC NHẬN trước đó.";
                else if (doan.TrangThai == AppConstants.TrangThaiDoanKhach.DaHuy)
                    reason = "Booking đã bị HỦY.";
                else if (doan.TrangThai == AppConstants.TrangThaiDoanKhach.HetHan)
                    reason = "Booking đã HẾT HẠN.";
                else if (doan.NgayDen.HasValue && doan.NgayDen.Value.Date > DateTime.Today)
                    reason = string.Format("Chưa đến ngày. Ngày hẹn: {0:dd/MM/yyyy}", doan.NgayDen.Value);
                else
                    reason = "Trạng thái không hợp lệ: " + (doan.TrangThai ?? "null");

                lblValidation.Text = "❌ " + reason;
                lblValidation.ForeColor = Color.FromArgb(239, 68, 68);
                lblTrangThai.ForeColor = Color.FromArgb(239, 68, 68);
                btnXuatVe.Enabled = false;
            }
        }

        // ============================================================
        //  CONFIRM: Xác nhận đoàn đã đến -> đánh dấu DaXuatVe
        // ============================================================

        private void btnXuatVe_Click(object sender, EventArgs e)
        {
            if (_doanTimThay == null) return;

            var confirm = TDCMessageBox.Show(
                string.Format("Xác nhận đoàn \"{0}\" ({1} người) đã đến?\n\nSau khi xác nhận, mã booking sẽ không thể dùng lại.",
                    _doanTimThay.TenDoan, _doanTimThay.SoLuongKhach),
                "Xác nhận đoàn đã đến",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            var result = BUS_DoanKhach.Instance.DanhDauDaXuatVe(_doanTimThay.Id);
            if (result.IsSuccess)
            {
                TDCMessageBox.Show(
                    string.Format("✅ Thành công!\n\nĐoàn: {0}\nSố người: {1}\nChiết khấu: {2:N1}%\n\nĐoàn đã được ghi nhận đến.",
                        _doanTimThay.TenDoan, _doanTimThay.SoLuongKhach, _doanTimThay.ChietKhau),
                    "Hoàn tất");

                // Reset UI
                _doanTimThay = null;
                pnlBookingInfo.Visible = false;
                txtBookingCode.Text = "";
                txtBookingCode.Focus();
            }
            else
            {
                TDCMessageBox.Show("Lỗi: " + result.ErrorMessage, "Thất bại",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BUS;
using ET;

namespace GUI
{
    public partial class frmDatBanTruocDialog : Form
    {
        public bool IsSuccess { get; private set; }
        public ET_DatBan ReservationInfo { get; private set; }

        private List<int> _dsBanAnId;
        private int _idNhaHang;

        // Linked group state
        private int _linkedDoanId = 0;
        private ET_DoanKhach _linkedDoan = null;

        public frmDatBanTruocDialog(int idNhaHang, List<int> dsBanAnId, string tenCacBan)
        {
            InitializeComponent();
            this.Text = "ĐẶT BÀN TRƯỚC (RESERVATION)";

            _idNhaHang = idNhaHang;
            _dsBanAnId = dsBanAnId;

            lblBanDaChon.Text = string.Format("Bàn đã chọn ({0}): {1}", dsBanAnId.Count, tenCacBan);
        }

        private void FrmDatBanTruocDialog_Load(object sender, EventArgs e)
        {
            ThemeManager.ApplyTheme(this);
            dtpGioDen.MinDate = DateTime.Now;
            dtpGioDen.Value = DateTime.Now.AddHours(2);
            LoadKhachHang();
            cboKhachHang.SelectedIndexChanged += cboKhachHang_SelectedIndexChanged;
            rdoTienMat.Checked = true;
            txtMaRFID.Enabled = false;

            txtSearchBooking.KeyDown += (s, ev) =>
            {
                if (ev.KeyCode == Keys.Enter) { ev.SuppressKeyPress = true; TimDoan(); }
            };
        }

        // ══════════════════════════════════════════════════════════════
        //  TÌM ĐOÀN ĐÃ TỒN TẠI -> AUTO-FILL
        // ══════════════════════════════════════════════════════════════

        private void btnTimDoan_Click(object sender, EventArgs e)
        {
            TimDoan();
        }

        private void TimDoan()
        {
            string keyword = txtSearchBooking.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                ResetLinkedDoan();
                return;
            }

            var doan = BUS_DoanKhach.Instance.GetByBookingCode(keyword);
            if (doan == null)
            {
                lblTimKetQua.Text = "❌ Không tìm thấy. Sẽ đặt bàn KHÔNG liên kết đoàn.";
                lblTimKetQua.ForeColor = Color.FromArgb(239, 68, 68);
                ResetLinkedDoan();
                return;
            }

            _linkedDoanId = doan.Id;
            _linkedDoan = doan;

            // Auto-fill số khách từ đoàn
            if (doan.SoLuongKhach > 0)
                numSoKhach.Value = Math.Min(doan.SoLuongKhach, numSoKhach.Maximum);

            lblTimKetQua.Text = $"✅ Liên kết: {doan.TenDoan} ({doan.SoLuongKhach} người, CK {doan.ChietKhau}%)";
            lblTimKetQua.ForeColor = Color.FromArgb(16, 185, 129);
        }

        private void ResetLinkedDoan()
        {
            _linkedDoanId = 0;
            _linkedDoan = null;
            lblTimKetQua.Text = "Bỏ trống -> không liên kết đoàn.";
            lblTimKetQua.ForeColor = Color.Gray;
        }

        // ══════════════════════════════════════════════════════════════
        //  HÌNH THỨC CỌC
        // ══════════════════════════════════════════════════════════════

        private void RdoHinhThuc_CheckedChanged(object sender, EventArgs e)
        {
            txtMaRFID.Enabled = rdoRFID.Checked;
            if (rdoRFID.Checked) txtMaRFID.Focus();
        }

        private void LoadKhachHang()
        {
            var ds = BUS_KhachHang.Instance.LoadDS();
            cboKhachHang.DisplayMember = "HoTen";
            cboKhachHang.ValueMember = "Id";
            cboKhachHang.DataSource = ds;
            cboKhachHang.SelectedIndex = -1;
        }

        private void btnThemKhachNhanh_Click(object sender, EventArgs e)
        {
            using (var frm = new frmThemKhachNhanh())
            {
                frm.ShowDialog();
                if (frm.IsSuccess)
                {
                    LoadKhachHang();
                    cboKhachHang.SelectedIndex = cboKhachHang.Items.Count - 1;
                }
            }
        }

        private void cboKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboKhachHang.SelectedItem != null && cboKhachHang.SelectedItem is ET_KhachHang kh)
            {
                txtSoDienThoai.Text = kh.DienThoai;
            }
            else
            {
                txtSoDienThoai.Text = "";
            }
        }

        private void txtTienCoc_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTienCoc.Text)) return;
            string raw = txtTienCoc.Text.Replace(",", "");
            if (decimal.TryParse(raw, out decimal value))
            {
                txtTienCoc.TextChanged -= txtTienCoc_TextChanged;
                txtTienCoc.Text = value.ToString("N0");
                txtTienCoc.SelectionStart = txtTienCoc.Text.Length;
                txtTienCoc.TextChanged += txtTienCoc_TextChanged;
            }
        }

        // ══════════════════════════════════════════════════════════════
        //  LƯU: PHÂN NHÁNH ĐOÀN vs BÌNH THƯỜNG
        // ══════════════════════════════════════════════════════════════

        private void btnReserve_Click(object sender, EventArgs e)
        {
            if (cboKhachHang.SelectedValue == null)
            {
                TDCMessageBox.Show("Vui lòng chọn khách hàng!", "Cảnh báo");
                return;
            }

            decimal tienCoc = 0;
            if (!string.IsNullOrEmpty(txtTienCoc.Text))
            {
                decimal.TryParse(txtTienCoc.Text.Replace(",", ""), out tienCoc);
            }

            ReservationInfo = new ET_DatBan
            {
                IdKhachHang = (int)cboKhachHang.SelectedValue,
                TenNguoiDat = cboKhachHang.Text,
                SoDienThoai = txtSoDienThoai.Text,
                ThoiGianDat = DateTime.Now,
                ThoiGianDenDuKien = dtpGioDen.Value,
                SoLuongKhach = (int)numSoKhach.Value,
                GhiChu = txtGhiChu.Text,
                TienCoc = tienCoc
            };

            string maRfid = rdoRFID.Checked ? txtMaRFID.Text.Trim() : "";

            if (rdoRFID.Checked && ReservationInfo.TienCoc > 0 && string.IsNullOrEmpty(maRfid))
            {
                TDCMessageBox.Show("Vui lòng quẹt thẻ RFID để đóng cọc!", "Cảnh báo");
                return;
            }

            OperationResult<int> result;

            if (_linkedDoanId > 0)
            {
                // ═══ NHÁNH A: Liên kết đoàn đã tồn tại ═══
                result = BUS_DatBan.Instance.DatBanTruocForDoan(
                    _idNhaHang, _dsBanAnId, ReservationInfo, _linkedDoanId, maRfid);
            }
            else
            {
                // ═══ NHÁNH B: Đặt bàn thường (không liên kết đoàn) ═══
                result = BUS_DatBan.Instance.DatBanTruoc(
                    _idNhaHang, _dsBanAnId, ReservationInfo, maRfid);
            }

            if (result.IsSuccess)
            {
                string msg = _linkedDoanId > 0 
                    ? $"Đặt bàn trước thành công!\n✅ Đã liên kết đoàn: {_linkedDoan.TenDoan}"
                    : "Đặt bàn trước thành công!";
                TDCMessageBox.Show(msg, "Thông báo");
                this.IsSuccess = true;
                this.Close();
            }
            else
            {
                TDCMessageBox.Show(result.ErrorMessage, "Lỗi");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.IsSuccess = false;
            this.Close();
        }

        private void FrmDatBanTruocDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnCancel_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F9)
            {
                btnReserve_Click(sender, e);
            }
        }
    }
}

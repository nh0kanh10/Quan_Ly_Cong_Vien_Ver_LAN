using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BUS;
using ET;

namespace GUI
{
    public partial class frmDatPhongDoanDialog : Form
    {
        private List<ET_RoomMapItem> _selectedRooms;
        private int _currentUserId;

        // Linked group state
        private int _linkedDoanId = 0;
        private ET_DoanKhach _linkedDoan = null;

        public bool IsSuccess { get; private set; }

        public frmDatPhongDoanDialog(List<ET_RoomMapItem> selectedRooms, int currentUserId)
        {
            InitializeComponent();
            this.Text = "THÔNG TIN ĐẶT PHÒNG ĐOÀN (GROUP BOOKING)";

            _selectedRooms = selectedRooms;
            _currentUserId = currentUserId;

            this.Load += FrmDatPhongDoanDialog_Load;
            this.KeyDown += FrmDatPhongDoanDialog_KeyDown;
        }

        private void FrmDatPhongDoanDialog_Load(object sender, EventArgs e)
        {
            ThemeManager.ApplyTheme(this);
            dtpCheckIn.Value = DateTime.Now;
            dtpCheckOut.Value = DateTime.Now.AddDays(1).Date.AddHours(12);

            txtTienCoc.TextChanged += txtTienCoc_TextChanged;
            txtSearchBooking.KeyDown += (s, ev) =>
            {
                if (ev.KeyCode == Keys.Enter) { ev.SuppressKeyPress = true; TimDoan(); }
            };

            LoadRoomGrid();
            CalculateTotal();
        }

        // ══════════════════════════════════════════════════════════════
        //  TÌM ĐOÀN ĐÃ TỒN TẠI -> AUTO-FILL + KHÓA Ô
        // ══════════════════════════════════════════════════════════════

        private void btnTimDoan_Click(object sender, EventArgs e)
        {
            TimDoan();
        }

        private void TimDoan()
        {
            string keyword = txtSearchBooking.Text.Trim();

            // Nếu xóa trắng -> reset về chế độ tạo đoàn mới
            if (string.IsNullOrEmpty(keyword))
            {
                ResetLinkedDoan();
                return;
            }

            var doan = BUS_DoanKhach.Instance.GetByBookingCode(keyword);
            if (doan == null)
            {
                lblTimKetQua.Text = "Không tìm thấy. Sẽ tạo đoàn MỚI khi lưu.";
                lblTimKetQua.ForeColor = Color.FromArgb(239, 68, 68);
                ResetLinkedDoan();
                return;
            }

            // Tìm thấy -> Auto-fill + khóa
            _linkedDoanId = doan.Id;
            _linkedDoan = doan;

            txtTenDoan.Text = doan.TenDoan;
            txtNguoiDaiDien.Text = doan.NguoiDaiDien ?? "";
            txtSdt.Text = doan.DienThoaiLienHe ?? "";
            numChietKhau.Value = doan.ChietKhau;

            // Khóa ô (ReadOnly) vì dữ liệu do Sales quản lý
            txtTenDoan.ReadOnly = true;
            txtNguoiDaiDien.ReadOnly = true;
            txtSdt.ReadOnly = true;
            numChietKhau.Enabled = false;

            lblTimKetQua.Text = $"Liên kết: {doan.TenDoan} ({doan.SoLuongKhach} người, CK {doan.ChietKhau}%)";
            lblTimKetQua.ForeColor = Color.FromArgb(16, 185, 129);

            CalculateTotal();
        }

        private void ResetLinkedDoan()
        {
            _linkedDoanId = 0;
            _linkedDoan = null;

            txtTenDoan.ReadOnly = false;
            txtNguoiDaiDien.ReadOnly = false;
            txtSdt.ReadOnly = false;
            numChietKhau.Enabled = true;

            lblTimKetQua.Text = "Bỏ trống ô trên -> sẽ tạo đoàn MỚI khi lưu.";
            lblTimKetQua.ForeColor = Color.Gray;
        }

        // ══════════════════════════════════════════════════════════════
        //  TIỀN CỌC FORMAT
        // ══════════════════════════════════════════════════════════════

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
        //  ROOM GRID + TÍNH TIỀN
        // ══════════════════════════════════════════════════════════════

        private void LoadRoomGrid()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("ColId", typeof(int));
            dt.Columns.Add("ColPhong", typeof(string));
            dt.Columns.Add("ColLoai", typeof(string));
            dt.Columns.Add("ColDonGia", typeof(string));
            dt.Columns.Add("ColSoDem", typeof(int));
            dt.Columns.Add("ColThanhTien", typeof(string));

            foreach (var r in _selectedRooms)
            {
                int days = (int)Math.Ceiling((dtpCheckOut.Value - dtpCheckIn.Value).TotalDays);
                if (days <= 0) days = 1;

                decimal estimatedPrice = BUS_Phong.Instance.TinhGiaPhong(r.Id, dtpCheckIn.Value, dtpCheckOut.Value);
                
                dt.Rows.Add(r.Id, r.TenPhong, r.TenLoaiPhong, (estimatedPrice / days).ToString("N0"), days, estimatedPrice.ToString("N0"));
            }

            dgvRooms.DataSource = dt;
            
            var view = dgvRooms.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view != null)
            {
                view.Columns["ColId"].Visible = false;
                view.Columns["ColPhong"].Caption = "Phòng";
                view.Columns["ColLoai"].Caption = "Loại phòng";
                view.Columns["ColDonGia"].Caption = "Đơn giá/Đêm";
                view.Columns["ColSoDem"].Caption = "Số đêm";
                view.Columns["ColThanhTien"].Caption = "Thành tiền";
            }
        }

        private void CalculateTotal()
        {
            decimal totalRoom = 0;
            var view = dgvRooms.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view != null)
            {
                for (int i = 0; i < view.RowCount; i++)
                {
                    string thanhTienStr = view.GetRowCellValue(i, "ColThanhTien")?.ToString().Replace(",", "");
                    if (decimal.TryParse(thanhTienStr, out decimal tt))
                    {
                        totalRoom += tt;
                    }
                }
            }
            lblTotalRoom.Text = totalRoom.ToString("N0") + " đ";

            decimal chietKhau = numChietKhau.Value;
            decimal gianGia = totalRoom * chietKhau / 100m;
            lblGiamGia.Text = $"Giảm: -{gianGia:N0} đ";

            decimal final_total = totalRoom - gianGia;
            lblFinalTotal.Text = final_total.ToString("N0") + " đ";
        }

        private void numChietKhau_ValueChanged(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void dtpCheckIn_ValueChanged(object sender, EventArgs e)
        {
            lblNights.Text = $"(Tổng: {(int)Math.Max(1, Math.Ceiling((dtpCheckOut.Value - dtpCheckIn.Value).TotalDays))} đêm)";
            LoadRoomGrid();
            CalculateTotal();
        }

        // ══════════════════════════════════════════════════════════════
        //  LƯU: PHÂN NHÁNH ĐOÀN MỚI vs ĐOÀN ĐÃ TỒN TẠI
        // ══════════════════════════════════════════════════════════════

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenDoan.Text) || string.IsNullOrWhiteSpace(txtNguoiDaiDien.Text))
            {
                TDCMessageBox.Show("Vui lòng điền đủ Tên Đoàn và Đại diện!", "Lỗi nhập liệu");
                return;
            }

            if (dtpCheckIn.Value >= dtpCheckOut.Value)
            {
                TDCMessageBox.Show("Ngày trả phòng phải sau ngày nhận!", "Lỗi logic");
                return;
            }

            decimal tienCoc = 0;
            if (!string.IsNullOrEmpty(txtTienCoc.Text))
            {
                decimal.TryParse(txtTienCoc.Text.Replace(",", ""), out tienCoc);
            }

            string phuongThucCoc = AppConstants.PhuongThucThanhToan.TienMat;
            if (rbChuyenKhoan.Checked) phuongThucCoc = AppConstants.PhuongThucThanhToan.ChuyenKhoan;
            else if (rbViRFID.Checked) phuongThucCoc = AppConstants.PhuongThucThanhToan.ViRfid;

            List<int> roomIds = _selectedRooms.Select(x => x.Id).ToList();
            OperationResult result;

            if (_linkedDoanId > 0)
            {
                // ═══ NHÁNH A: Liên kết đoàn đã tồn tại ═══
                result = BUS_Phong.Instance.ReserveGroupForExistingDoan(
                    roomIds, _linkedDoanId, dtpCheckIn.Value, dtpCheckOut.Value, 
                    tienCoc, phuongThucCoc, _currentUserId);
            }
            else
            {
                // ═══ NHÁNH B: Tạo đoàn mới (flow cũ, walk-in) ═══
                ET_DoanKhach doan = new ET_DoanKhach
                {
                    TenDoan = txtTenDoan.Text,
                    NguoiDaiDien = txtNguoiDaiDien.Text,
                    DienThoaiLienHe = txtSdt.Text,
                    ChietKhau = numChietKhau.Value
                };
                result = BUS_Phong.Instance.ReserveGroup(
                    roomIds, doan, dtpCheckIn.Value, dtpCheckOut.Value, 
                    tienCoc, phuongThucCoc, _currentUserId);
            }

            if (result.IsSuccess)
            {
                TDCMessageBox.Show(result.ErrorMessage ?? "Tạo Đoàn & Đặt Cọc Thành Công!", "Hoàn tất");
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
            this.Close();
        }

        private void FrmDatPhongDoanDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.F9)
            {
                btnSave_Click(null, null);
            }
        }
    }
}

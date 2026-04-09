using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BUS;
using ET;

namespace GUI
{
    public partial class frmReserveDialog : Form
    {
        private int _idPhong;
        private string _tenPhong;
        private int _idNhanVien;

        public bool IsSuccess { get; private set; }

        public frmReserveDialog(int idPhong, string tenPhong, int idNhanVien)
        {
            InitializeComponent();
            _idPhong = idPhong;
            _tenPhong = tenPhong;
            _idNhanVien = idNhanVien;

            this.Text = "ĐẶT TRƯỚC - " + tenPhong.ToUpper();
            lblTitle.Text = "Đặt trước: " + tenPhong;
            ThemeManager.ApplyTheme(this);

            LoadCombos();
            SetDefaultDates();
        }

        private void LoadCombos()
        {
            var dsKhach = BUS_KhachHang.Instance.LoadDS();
            cboKhachHang.DataSource = dsKhach;
            cboKhachHang.DisplayMember = "HoTen";
            cboKhachHang.ValueMember = "Id";
            cboKhachHang.SelectedIndex = -1;
        }

        private void SetDefaultDates()
        {
            // Mặc định: nhận phòng lúc 14:00 hôm nay, trả phòng 12:00 ngày mai
            dtpNgayNhan.DateTime = DateTime.Now.Date.AddHours(14);
            if (dtpNgayNhan.DateTime < DateTime.Now)
                dtpNgayNhan.DateTime = DateTime.Now.AddHours(1);
            dtpNgayTra.DateTime = dtpNgayNhan.DateTime.Date.AddDays(1).AddHours(12);
        }

        private void btnReserve_Click(object sender, EventArgs e)
        {
            // Validate: Phải chọn khách hàng
            if (cboKhachHang.SelectedValue == null)
            {
                TDCMessageBox.Show("Vui lòng chọn khách hàng để đặt trước!", "Cảnh báo");
                cboKhachHang.Focus();
                return;
            }

            // Validate: Ngày trả phải sau ngày nhận
            if (dtpNgayTra.DateTime <= dtpNgayNhan.DateTime)
            {
                TDCMessageBox.Show("Ngày trả phòng phải sau ngày nhận phòng!", "Cảnh báo");
                dtpNgayTra.Focus();
                return;
            }

            // Validate: Ngày nhận phải từ hiện tại trở đi
            if (dtpNgayNhan.DateTime < DateTime.Now.AddMinutes(-5))
            {
                TDCMessageBox.Show("Ngày nhận phòng không được ở trong quá khứ!", "Cảnh báo");
                dtpNgayNhan.Focus();
                return;
            }

            // Check conflict
            var busyIds = BUS_Phong.Instance.GetBusyRoomIds(dtpNgayNhan.DateTime, dtpNgayTra.DateTime);
            if (busyIds.Contains(_idPhong))
            {
                lblConflict.Text = "⚠️ Phòng đã có lịch đặt trùng trong khoảng thời gian này!";
                lblConflict.Visible = true;
                return;
            }

            int idKH = (int)cboKhachHang.SelectedValue;
            decimal tienCoc = txtTienCoc.Value;

            // Cảnh báo nếu cọc = 0
            if (tienCoc == 0)
            {
                if (TDCMessageBox.Show("Đặt phòng không có tiền cọc?\nKhách có thể không đến nhận phòng.", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;
            }

            bool success = BUS_Phong.Instance.ReserveRoom(
                _idPhong,
                idKH,
                dtpNgayNhan.DateTime,
                dtpNgayTra.DateTime,
                tienCoc,
                _idNhanVien
            );

            if (success)
            {
                string msg = string.Format("Đặt trước phòng {0} thành công!", _tenPhong);
                if (tienCoc > 0) msg += string.Format("\nĐã thu cọc: {0:N0} VNĐ", tienCoc);
                TDCMessageBox.Show(msg, "Thành công");
                this.IsSuccess = true;
                this.Close();
            }
            else
            {
                TDCMessageBox.Show("Lỗi: Không thể đặt trước. Phòng có thể đã bị trùng lịch hoặc lỗi hệ thống.", "Lỗi");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThemKhachNhanh_Click(object sender, EventArgs e)
        {
            using (var frm = new Form())
            {
                frm.Text = "Thêm nhanh khách hàng";
                frm.Size = new Size(350, 200);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.FormBorderStyle = FormBorderStyle.FixedDialog;
                frm.MaximizeBox = false; frm.MinimizeBox = false;
                frm.BackColor = Color.White;
                frm.Font = new Font("Segoe UI", 10f);

                var lblTen = new Label { Text = "Họ tên *", Location = new Point(20, 15), AutoSize = true };
                var txtTen = new Guna.UI2.WinForms.Guna2TextBox { Location = new Point(20, 38), Size = new Size(295, 36), BorderRadius = 4, PlaceholderText = "Nhập họ tên khách..." };
                var lblSdt = new Label { Text = "Số điện thoại", Location = new Point(20, 80), AutoSize = true };
                var txtSdt = new Guna.UI2.WinForms.Guna2TextBox { Location = new Point(20, 103), Size = new Size(295, 36), BorderRadius = 4, PlaceholderText = "Nhập SĐT..." };
                var btnLuu = new Guna.UI2.WinForms.Guna2Button
                {
                    Text = "LƯU", Location = new Point(20, 150), Size = new Size(295, 36),
                    BorderRadius = 4, FillColor = Color.FromArgb(74, 137, 115),
                    Font = new Font("Segoe UI", 10f, FontStyle.Bold), ForeColor = Color.White
                };
                frm.Controls.AddRange(new Control[] { lblTen, txtTen, lblSdt, txtSdt, btnLuu });
                btnLuu.Click += (s2, e2) =>
                {
                    if (string.IsNullOrWhiteSpace(txtTen.Text)) { TDCMessageBox.Show("Vui lòng nhập họ tên!", "Cảnh báo"); return; }
                    var kh = new ET_KhachHang { HoTen = txtTen.Text.Trim(), DienThoai = txtSdt.Text.Trim() };
                    var result = BUS_KhachHang.Instance.Them(kh);
                    if (result.IsSuccess)
                    {
                        LoadCombos();
                        var dsKhach = (System.Collections.Generic.List<ET_KhachHang>)cboKhachHang.DataSource;
                        var khMoi = dsKhach.FindLast(x => x.HoTen == kh.HoTen);
                        if (khMoi != null) cboKhachHang.SelectedValue = khMoi.Id;
                        frm.Close();
                    }
                    else { TDCMessageBox.Show(result.ErrorMessage ?? "Thêm thất bại!", "Lỗi"); }
                };
                frm.ShowDialog(this);
            }
        }
    }
}


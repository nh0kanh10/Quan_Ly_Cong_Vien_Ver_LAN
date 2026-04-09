using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BUS;
using ET;

namespace GUI
{
    public partial class frmBookingDialog : Form
    {
        private int _idPhong;
        private string _tenPhong;
        private int _idNhanVien;
        private int? _idDatPhongLienQuan; // Nếu là check-in từ đặt trước

        public bool IsSuccess { get; private set; }

        public frmBookingDialog(int idPhong, string tenPhong, int idNhanVien, int? idDatPhongLienQuan = null)
        {
            InitializeComponent();
            _idPhong = idPhong;
            _tenPhong = tenPhong;
            _idNhanVien = idNhanVien;
            _idDatPhongLienQuan = idDatPhongLienQuan;

            this.Text = "NHẬN PHÒNG - " + tenPhong.ToUpper();
            lblTitle.Text = "Check-in: " + tenPhong;
            ThemeManager.ApplyTheme(this);

            LoadCombos();
            LoadRoomConstraints();

            // [UX-FIX]: Disable radio khi tiền = 0
            txtSoTien.ValueChanged += (s, ev) =>
            {
                bool hasMoney = txtSoTien.Value > 0;
                rbTienMat.Enabled = hasMoney;
                rbRFID.Enabled = hasMoney;
                if (!hasMoney) rbTienMat.Checked = true;
            };
        }

        private void LoadCombos()
        {
            var dsKhach = BUS_KhachHang.Instance.LoadDS();
            slkKhachHang.Properties.DataSource = dsKhach;
            slkKhachHang.Properties.DisplayMember = "HoTen";
            slkKhachHang.Properties.ValueMember = "Id";

            // Format Popup Grid
            ThemeManager.StyleDevExpressGrid(slkKhachHang.Properties.View.GridControl);
            var view = slkKhachHang.Properties.View;
            view.PopulateColumns(dsKhach);

            // Hide all by default, show only the 3 requested columns
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in view.Columns)
            {
                col.Visible = false;
            }

            if (view.Columns["HoTen"] != null)
            {
                view.Columns["HoTen"].Caption = "Khách hàng";
                view.Columns["HoTen"].Visible = true;
                view.Columns["HoTen"].VisibleIndex = 0;
            }

            if (view.Columns["DienThoai"] != null)
            {
                view.Columns["DienThoai"].Caption = "Điện thoại";
                view.Columns["DienThoai"].Visible = true;
                view.Columns["DienThoai"].VisibleIndex = 1;
            }

            if (view.Columns["CmndCccd"] != null)
            {
                view.Columns["CmndCccd"].Caption = "CCCD/CMND";
                view.Columns["CmndCccd"].Visible = true;
                view.Columns["CmndCccd"].VisibleIndex = 2;
            }

            view.BestFitColumns();
            
            slkKhachHang.Properties.Appearance.BackColor = Color.White;
            slkKhachHang.Properties.Appearance.ForeColor = ThemeManager.TextPrimaryColor;
            slkKhachHang.Properties.Appearance.Font = ThemeManager.GetFont(10f);
            slkKhachHang.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            slkKhachHang.Properties.Appearance.BorderColor = ThemeManager.BorderColor;
            slkKhachHang.Properties.AppearanceFocused.BorderColor = ThemeManager.PrimaryColor;
            slkKhachHang.Properties.AutoHeight = false;
            slkKhachHang.Height = 36;
        }

        private void LoadRoomConstraints()
        {
            // Ngày trả mặc định: 12:00 ngày mai
            dtpNgayTra.DateTime = DateTime.Now.AddDays(1).Date.AddHours(12);

            // Kiểm tra có khách tiếp theo không để cảnh báo và hạn chế giờ trả
            var mapData = BUS_Phong.Instance.GetRoomMapData();
            var room = mapData.FirstOrDefault(x => x.Id == _idPhong);

            if (room != null && room.NgayNhanTiepTheo != null)
            {
                lblConstraint.Text = "LƯU Ý: Có khách tiếp theo lúc " + room.NgayNhanTiepTheo.Value.ToString("HH:mm dd/MM");
                lblConstraint.Visible = true;
                lblConstraint.ForeColor = ThemeManager.DangerColor;
                // Tự động đặt giờ trả trước 1 tiếng so với khách tiếp theo
                dtpNgayTra.DateTime = room.NgayNhanTiepTheo.Value.AddHours(-1);
            }
            else
            {
                lblConstraint.Visible = false;
            }

            // Nếu check-in từ đặt trước: load thông tin khách hàng đã đặt
            if (_idDatPhongLienQuan != null)
            {
                lblTitle.Text = "Nhận phòng (Đã đặt trước): " + _tenPhong;

                try
                {
                    // Tìm đặt phòng -> đơn hàng -> khách hàng thông qua BUS
                    var booking = BUS_Phong.Instance.LayThongTinDatPhong(_idDatPhongLienQuan.Value);

                    if (booking?.IdChiTietDonHang != null)
                    {
                        var ctdh = DAL.DAL_ChiTietDonHang.Instance.LayTheoId(booking.IdChiTietDonHang.Value);
                        if (ctdh != null)
                        {
                            var donHang = BUS_DonHang.Instance.GetById(ctdh.IdDonHang);
                            if (donHang?.IdKhachHang != null)
                            {
                                // Tự động chọn khách hàng trong ComboBox
                                slkKhachHang.EditValue = donHang.IdKhachHang.Value;
                            }
                        }
                        // Điền ngày trả theo lịch đặt phòng
                        dtpNgayTra.DateTime = booking.NgayTra;
                    }

                    // Gợi ý tiền theo giá phòng tính từ DB
                    decimal giaDuKien = BUS_Phong.Instance.TinhGiaPhong(_idPhong, DateTime.Now, dtpNgayTra.DateTime);
                    txtSoTien.Value = giaDuKien;
                }
                catch { /* Không crash dialog nếu load thất bại */ }
            }
            else
            {
                // Check-in trực tiếp: gợi ý giá phòng
                try
                {
                    decimal giaDuKien = BUS_Phong.Instance.TinhGiaPhong(_idPhong, DateTime.Now, dtpNgayTra.DateTime);
                    txtSoTien.Value = giaDuKien;
                }
                catch { }
            }

            // Tự động cập nhật giá khi thay đổi giờ trả
            dtpNgayTra.EditValueChanged += (s, e) =>
            {
                try
                {
                    decimal gia = BUS_Phong.Instance.TinhGiaPhong(_idPhong, DateTime.Now, dtpNgayTra.DateTime);
                    txtSoTien.Value = gia;
                    UpdatePriceBreakdown(gia);
                }
                catch { }
            };
            

            // Hiển thị giá phòng ban đầu
            try { UpdatePriceBreakdown(txtSoTien.Value); } catch { }
        }

        // Hiển thị chi tiết giá phòng
        private void UpdatePriceBreakdown(decimal giaHienTai)
        {
            try
            {
                TimeSpan duration = dtpNgayTra.DateTime - DateTime.Now;
                double totalHours = duration.TotalHours;
                string breakdown;

                if (totalHours <= 4)
                    breakdown = string.Format("Nghỉ trưa ({0:F1}h) = {1:N0}đ", totalHours, giaHienTai);
                else
                {
                    int soNgay = (int)Math.Ceiling(duration.TotalDays);
                    if (soNgay <= 0) soNgay = 1;
                    breakdown = string.Format("{0} đêm × {1:N0}đ = {2:N0}đ", soNgay, giaHienTai / soNgay, giaHienTai);
                }

                lblConstraint.Text =  breakdown;
                lblConstraint.Visible = true;
                lblConstraint.ForeColor = ThemeManager.TextSecondaryColor;
            }
            catch { }
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            // Validation: ngày trả phải hợp lệ
            if (dtpNgayTra.DateTime <= DateTime.Now)
            {
                TDCMessageBox.Show("Giờ trả phòng phải sau thời điểm hiện tại!", "Cảnh báo");
                dtpNgayTra.Focus();
                return;
            }

            int? idKH = default;
            if (slkKhachHang.EditValue != null && int.TryParse(slkKhachHang.EditValue.ToString(), out int parsedKH))
            {
                idKH = parsedKH;
            }

            decimal soTien = txtSoTien.Value;
            string pthuc = rbTienMat.Checked ? "TienMat" : "ViRFID";
            DateTime ngayTra = dtpNgayTra.DateTime;

            // Nếu dùng Ví RFID phải có khách hàng
            if (pthuc == "ViRFID" && idKH == null)
            {
                TDCMessageBox.Show("Phải chọn khách hàng để thanh toán qua Ví RFID!", "Cảnh báo");
                slkKhachHang.Focus();
                return;
            }

            bool success = false;
            if (_idDatPhongLienQuan != null)
            {
                // Check-in từ đặt trước
                success = BUS_Phong.Instance.CheckInFromReservation(_idPhong, _idDatPhongLienQuan.Value, soTien, pthuc, _idNhanVien);
            }
            else
            {
                // Check-in trực tiếp
                success = BUS_Phong.Instance.CheckIn(_idPhong, idKH, soTien, pthuc, _idNhanVien, ngayTra);
            }

            if (success)
            {
                TDCMessageBox.Show("Check-in thành công! Chúc khách hàng có kỳ nghỉ vui vẻ.", "Thành công");
                this.IsSuccess = true;
                this.Close();
            }
            else
            {
                TDCMessageBox.Show("Check-in thất bại! Có thể do:\n• Phòng đã bị đặt trong khoảng thời gian này\n• Số dư Ví RFID không đủ\n• Lỗi hệ thống", "Lỗi");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThemKhachNhanh_Click(object sender, EventArgs e)
        {
            using (var frm = new frmThemKhachNhanh())
            {
                frm.ShowDialog(this);
                if (frm.IsSuccess && frm.ThongTinKhachMoi != null)
                {
                    LoadCombos();
                    slkKhachHang.EditValue = frm.ThongTinKhachMoi.Id;
                }
            }
        }
    }
}


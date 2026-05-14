using System;
using System.Windows.Forms;
using System.Linq;
using DevExpress.XtraEditors;
using ET.Constants;
using BUS.Services.BanHang;
using GUI.Infrastructure;
using BUS.Services.HeThong;
using ET.Models.BanHang;

namespace GUI.Modules.BanHang
{
    public partial class frmBookingDialog : XtraForm
    {
        private int _idPhongChon;
        private DevExpress.XtraEditors.TextEdit txtSoNguoi = new DevExpress.XtraEditors.TextEdit();
        private DevExpress.XtraEditors.TextEdit txtGiaPhong = new DevExpress.XtraEditors.TextEdit();
        private DevExpress.XtraEditors.TextEdit txtRFID = new DevExpress.XtraEditors.TextEdit();

        public frmBookingDialog(int idPhong = 0)
        {
            InitializeComponent();
            _idPhongChon = idPhong;
        }

        #region Khởi tạo và tải dữ liệu
        
        private void frmBookingDialog_Load(object sender, EventArgs e)
        {
            AppStyle.StyleForm(this);
            AppStyle.StyleBtnPrimary(btnCheckIn);
            AppStyle.StyleBtnSecondary(btnThoat);

            this.Text = LanguageManager.GetString("TITLE_BOOKING_DIALOG") ?? "Nhận Phòng / Đặt Phòng";
            layoutKhachHang.Text = LanguageManager.GetString("LBL_TEN_KHACH_HANG") ?? "Tên Khách Hàng";
            layoutSDT.Text = LanguageManager.GetString("LBL_SO_DIEN_THOAI") ?? "Số Điện Thoại";
            layoutPhong.Text = LanguageManager.GetString("LBL_PHONG_LUA_CHON") ?? "Phòng Lựa Chọn";
            layoutCheckIn.Text = LanguageManager.GetString("LBL_GIO_CHECK_IN") ?? "Giờ Check-in";
            layoutCheckOut.Text = LanguageManager.GetString("LBL_GIO_CHECK_OUT") ?? "Giờ Check-out";
            layoutTienCoc.Text = LanguageManager.GetString("LBL_TIEN_DAT_COC") ?? "Tiền Đặt Cọc";
            btnCheckIn.Text = LanguageManager.GetString("BTN_NHAN_PHONG") ?? "Nhận Phòng";
            btnThoat.Text = LanguageManager.GetString("BTN_HUY_BO") ?? "Hủy bỏ";
            slkPhong.Properties.NullText = LanguageManager.GetString("TXT_CHON_PHONG") ?? "Chọn phòng...";

            dateCheckIn.EditValue = DateTime.Now;
            dateCheckOut.EditValue = DateTime.Now.AddDays(1).Date.AddHours(12); 

            // Khởi tạo controls động cho Giá Phòng và Số Người
            txtGiaPhong.Properties.ReadOnly = true;
            txtGiaPhong.EditValue = "0 VND";
            txtGiaPhong.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            txtGiaPhong.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);

            txtSoNguoi.Properties.Mask.EditMask = "N0";
            txtSoNguoi.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtSoNguoi.EditValue = 1;

            var lciGiaPhong = new DevExpress.XtraLayout.LayoutControlItem();
            lciGiaPhong.Control = txtGiaPhong;
            lciGiaPhong.Text = LanguageManager.GetString("LBL_GIA_DU_KIEN") ?? "Giá Phòng Dự Kiến";
            Root.AddItem(lciGiaPhong, layoutTienCoc, DevExpress.XtraLayout.Utils.InsertType.Top);

            var lciSoNguoi = new DevExpress.XtraLayout.LayoutControlItem();
            lciSoNguoi.Control = txtSoNguoi;
            lciSoNguoi.Text = LanguageManager.GetString("LBL_SO_NGUOI") ?? "Số Người O";
            Root.AddItem(lciSoNguoi, layoutTienCoc, DevExpress.XtraLayout.Utils.InsertType.Top);

            var lciRFID = new DevExpress.XtraLayout.LayoutControlItem();
            lciRFID.Control = txtRFID;
            lciRFID.Text = LanguageManager.GetString("LBL_THE_RFID") ?? "Mã Thẻ RFID";
            Root.AddItem(lciRFID, layoutTienCoc, DevExpress.XtraLayout.Utils.InsertType.Top);

            slkPhong.EditValueChanged += TinhGiaDuKien_Event;
            dateCheckIn.EditValueChanged += TinhGiaDuKien_Event;
            dateCheckOut.EditValueChanged += TinhGiaDuKien_Event;

            txtSDT.Leave += TxtSDT_Leave;

            LoadDanhSachPhong();
            MoPhienThuNgan();
        }

        private void MoPhienThuNgan()
        {
            if (SessionManager.IdPhienGiaoDich > 0) return;

            var resultPhien = BUS_PhienThuNgan.Instance.LayPhienDangMo(SessionManager.IdDoiTac);
            if (resultPhien.Success && resultPhien.Data != null)
            {
                var phien = resultPhien.Data as ET_PhienThuNgan;
                SessionManager.IdPhienGiaoDich = phien.Id;
                return;
            }

            if (UIHelper.XacNhan(LanguageManager.GetString("ERR_CHUA_MO_PHIEN") ?? "Bạn chưa mở phiên thu ngân. Vui lòng mở phiên trước khi thực hiện đặt phòng."))
            {
                using (var frm = new frmPhienThuNgan(null, true))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        SessionManager.IdPhienGiaoDich = frm.PhienKetQua.Id;
                    }
                    else
                    {
                        this.Close(); 
                    }
                }
            }
            else
            {
                this.Close();
            }
        }

        private void TxtSDT_Leave(object sender, EventArgs e)
        {
            string sdt = txtSDT.Text.Trim();
            if (!string.IsNullOrEmpty(sdt))
            {
                var res = BUS.Services.DoiTac.BUS_DoiTac.Instance.LayChiTietTheoDienThoai(sdt);
                if (res.Success && res.Data != null)
                {
                    if (string.IsNullOrEmpty(txtKhachHang.Text.Trim()))
                    {
                        txtKhachHang.Text = res.Data.HoTen;
                    }
                }
            }
        }

        private void TinhGiaDuKien_Event(object sender, EventArgs e)
        {
            if (slkPhong.EditValue != null && dateCheckIn.EditValue != null && dateCheckOut.EditValue != null)
            {
                int idPhong = Convert.ToInt32(slkPhong.EditValue);
                DateTime checkIn = (DateTime)dateCheckIn.EditValue;
                DateTime checkOut = (DateTime)dateCheckOut.EditValue;
                
                int soNgay = (checkOut.Date - checkIn.Date).Days;
                if (soNgay <= 0) soNgay = 1;

                // Lấy thông tin phòng 
                var roomInfo = BUS_LuuTru_SoDo.Instance.LayChiTietPhong(idPhong);
                if (roomInfo != null && roomInfo.IdSanPham > 0)
                {
                    decimal giaGoc = BUS.Services.DanhMuc.BUS_BangGia.Instance.GetDynamicPrice(roomInfo.IdSanPham, checkIn, out _);
                    decimal tongGia = giaGoc * soNgay;
                    txtGiaPhong.EditValue = $"{tongGia:N0} VND ({soNgay} đêm x {giaGoc:N0})";
                }
                else
                {
                    txtGiaPhong.EditValue = "Chưa cấu hình giá";
                }
            }
        }

        private void LoadDanhSachPhong()
        {
            
            var res = BUS_LuuTru_SoDo.Instance.LayDanhSachSodoPhong(null);
            if (res.Success)
            {
                var phongTrong = res.Data.Where(p => p.TrangThaiPhong == AppConstants.TrangThaiPhong.Trong).ToList();
                slkPhong.Properties.DataSource = phongTrong;
                slkPhong.Properties.DisplayMember = "MaPhong";
                slkPhong.Properties.ValueMember = "IdPhong";

                slkPhong.Properties.PopulateViewColumns();
                foreach (DevExpress.XtraGrid.Columns.GridColumn col in slkPhongView.Columns)
                {
                    col.Visible = false;
                }
                
                if (slkPhongView.Columns["MaPhong"] != null)
                {
                    slkPhongView.Columns["MaPhong"].Visible = true;
                    slkPhongView.Columns["MaPhong"].Caption = LanguageManager.GetString("COL_MA_PHONG") ?? "Mã Phòng";
                }
                if (slkPhongView.Columns["TenLoaiPhong"] != null)
                {
                    slkPhongView.Columns["TenLoaiPhong"].Visible = true;
                    slkPhongView.Columns["TenLoaiPhong"].Caption = LanguageManager.GetString("COL_LOAI_PHONG") ?? "Loại Phòng";
                }

                if (_idPhongChon > 0)
                {
                    slkPhong.EditValue = _idPhongChon;
                    slkPhong.Enabled = false; 
                }
            }
            else
            {
                string[] parts = res.ErrorMessage.Split('|');
                string errMsg = LanguageManager.GetString(parts[0]);
                if (parts.Length > 1)
                {
                    errMsg = string.Format(errMsg, parts.Skip(1).ToArray());
                }
                UIHelper.ThongBaoLoi(errMsg);
            }
        }

        #endregion

        #region Xử lý sự kiện

        private void BtnThoat_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void BtnCheckIn_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            int idPhong = (int)slkPhong.EditValue;
            string tenKhach = txtKhachHang.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            decimal tienCoc = 0;
            decimal.TryParse(spinTienCoc.EditValue?.ToString(), out tienCoc);

            string maRFID = txtRFID.Text.Trim();

            int idNhanVien = GUI.Infrastructure.SessionManager.IdDoiTac > 0 ? GUI.Infrastructure.SessionManager.IdDoiTac : 1;

            var res = BUS_LuuTru_Booking.Instance.DatPhongVaCheckIn(
                tenKhach, sdt, idPhong,
                (DateTime)dateCheckIn.EditValue,
                (DateTime)dateCheckOut.EditValue,
                tienCoc, idNhanVien, true, maRFID,
                SessionManager.IdPhienGiaoDich);

            if (res.Success)
            {
                BUS_NhatKy.Instance.GhiLog("Lưu Trú", idPhong, "Nhận phòng", idNhanVien, null, $"Phòng {idPhong}, Khách {tenKhach}, SĐT {sdt}", "Nhận phòng thành công");

                string msg = LanguageManager.GetString("MSG_NHAN_PHONG_THANH_CONG");
                if (string.IsNullOrWhiteSpace(msg)) msg = "Nhận phòng thành công!";
                UIHelper.ThongBao(msg);
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                string[] parts = res.ErrorMessage.Split('|');
                string errMsg = LanguageManager.GetString(parts[0]);
                if (parts.Length > 1)
                {
                    errMsg = string.Format(errMsg, parts.Skip(1).ToArray());
                }
                UIHelper.ThongBaoLoi(errMsg ?? res.ErrorMessage);
            }
        }

        #endregion

        #region Kiểm tra dữ liệu nhập (Validation)

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtKhachHang.Text))
            {
                string msg = LanguageManager.GetString("ERR_THIEU_TEN_KHACH_HANG");
                if (string.IsNullOrWhiteSpace(msg)) msg = "Vui lòng nhập tên khách hàng!";
                UIHelper.ThongBaoLoi(msg);
                txtKhachHang.Focus();
                return false;
            }

            string sdt = txtSDT.Text.Trim();
            if (string.IsNullOrEmpty(sdt) || sdt.Length < 10)
            {
                string msgSdt = LanguageManager.GetString("ERR_SDT_INVALID") ?? "Số điện thoại không hợp lệ. Vui lòng nhập ít nhất 10 số!";
                UIHelper.ThongBaoLoi(msgSdt);
                txtSDT.Focus();
                return false;
            }

            if (slkPhong.EditValue == null)
            {
                string msg = LanguageManager.GetString("ERR_THIEU_PHONG");
                if (string.IsNullOrWhiteSpace(msg)) msg = "Vui lòng chọn phòng lưu trú!";
                UIHelper.ThongBaoLoi(msg);
                return false;
            }

            int soNguoi = 1;
            int.TryParse(txtSoNguoi.EditValue?.ToString(), out soNguoi);
            if (soNguoi <= 0)
            {
                UIHelper.ThongBaoLoi(LanguageManager.GetString("ERR_SO_NGUOI_KHONG_HOP_LE") ?? "Số người phải lớn hơn 0");
                txtSoNguoi.Focus();
                return false;
            }

            //  số người tối đa của phòng
            int.TryParse(slkPhong.EditValue?.ToString(), out int idPhong);
            var view = slkPhong.Properties.View;
            int rowHandle = view.LocateByValue("IdPhong", idPhong);
            if (rowHandle >= 0)
            {
                int.TryParse(view.GetRowCellValue(rowHandle, "SoNguoiToiDa")?.ToString(), out int maxNguoi);
                if (maxNguoi > 0 && soNguoi > maxNguoi)
                {
                    UIHelper.ThongBaoLoi(string.Format(LanguageManager.GetString("ERR_VUOT_SO_NGUOI_TOI_DA") ?? "Phòng này chỉ cho phép tối đa {0} người!", maxNguoi));
                    return false;
                }
            }

            DateTime checkIn  = (DateTime)dateCheckIn.EditValue;
            DateTime checkOut = (DateTime)dateCheckOut.EditValue;
            if (checkOut <= checkIn)
            {
                UIHelper.ThongBaoLoi(LanguageManager.GetString("ERR_CHECKOUT_TRUOC_CHECKIN") ?? "Giờ Check-out phải sau giờ Check-in!");
                dateCheckOut.Focus();
                return false;
            }

            return true;
        }

        #endregion
    }
}

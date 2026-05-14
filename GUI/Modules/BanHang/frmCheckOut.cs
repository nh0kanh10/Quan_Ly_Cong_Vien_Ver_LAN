using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ET.DTOs;
using BUS.Services.BanHang;
using GUI.Infrastructure;
using System.Linq;
using ET.Constants;
using ET.Models.BanHang;

namespace GUI.Modules.BanHang
{
    public partial class frmCheckOut : XtraForm
    {
        private DTO_PhongLuuTruView _phong;
        private decimal _tienPhong = 0;
        private decimal _tienPhat = 0;

        private decimal _tienGiamKM = 0;
        private int _diemKhaDung = 0;
        private int _diemQuyDoi = 0;
        private decimal _tienGiamDiem = 0;
        private int? _idKhuyenMaiApDung = null;
        private int _idKhachHang = 0;
        private string _hangThanhVien = "";

        public frmCheckOut(DTO_PhongLuuTruView phong)
        {
            InitializeComponent();
            _phong = phong;
        }

        private void frmCheckOut_Load(object sender, EventArgs e)
        {
            AppStyle.StyleForm(this);
            AppStyle.StyleBtnPrimary(btnCheckOut);
            AppStyle.StyleBtnSecondary(btnThoat);

            this.Text = LanguageManager.GetString("TITLE_CHECKOUT_DIALOG") ?? "Trả Phòng / Check-out";
            layoutKhachHang.Text = LanguageManager.GetString("LBL_TEN_KHACH_HANG") ?? "Khách Hàng";
            layoutPhong.Text = LanguageManager.GetString("LBL_PHONG_LUA_CHON") ?? "Phòng";
            layoutThoiGian.Text = LanguageManager.GetString("LBL_GIO_IN_OUT") ?? "Giờ In/Out";
            layoutTienPhong.Text = LanguageManager.GetString("LBL_TIEN_PHONG") ?? "Tiền Phòng";
            layoutTienPhat.Text = LanguageManager.GetString("LBL_TIEN_PHAT") ?? "Tiền Phạt";
            layoutTienCoc.Text = LanguageManager.GetString("LBL_TIEN_DAT_COC") ?? "Tiền Cọc";
            layoutPhuThu.Text = LanguageManager.GetString("LBL_PHU_THU_KHAC") ?? "Phụ Thu Khác";
            layoutGhiChu.Text = LanguageManager.GetString("LBL_GHI_CHU_PT") ?? "Ghi Chú PT";
            layoutTongTien.Text = LanguageManager.GetString("LBL_TONG_THANH_TOAN") ?? "Tổng Thanh Toán";
            btnCheckOut.Text = LanguageManager.GetString("BTN_TRA_PHONG") ?? "Trả Phòng";
            btnThoat.Text = LanguageManager.GetString("BTN_HUY_BO") ?? "Hủy bỏ";

            if (_phong == null)
            {
                UIHelper.ThongBaoLoi(LanguageManager.GetString("ERR_NO_ROOM_DATA") ?? "Không có dữ liệu phòng để Check-out!");
                this.Close();
                return;
            }

            layoutPhuongThuc.Text = LanguageManager.GetString("LBL_PHUONG_THUC") ?? "Phương Thức";
            layoutMaKhuyenMai.Text = LanguageManager.GetString("LBL_KHUYEN_MAI") ?? "Khuyến Mãi";
            btnApDungKM.Text = LanguageManager.GetString("BTN_AP_DUNG") ?? "Áp dụng";
            chkDungDiem.Properties.Caption = LanguageManager.GetString("CHK_DUNG_DIEM") ?? "Sử dụng điểm tích lũy";

            cboPhuongThuc.Properties.Items.Clear();
            cboPhuongThuc.Properties.Items.Add(LanguageManager.GetString("PAY_CASH") ?? "Tiền mặt");
            cboPhuongThuc.Properties.Items.Add(LanguageManager.GetString("PAY_TRANSFER") ?? "Chuyển khoản");
            cboPhuongThuc.Properties.Items.Add(LanguageManager.GetString("PAY_CARD") ?? "Thẻ ngân hàng");
            cboPhuongThuc.Properties.Items.Add(AppConstants.PhuongThucTT.ViRFID);
            cboPhuongThuc.SelectedIndex = 0;
            btnApDungKM.Click += BtnApDungKM_Click;
            chkDungDiem.CheckedChanged += ChkDungDiem_CheckedChanged;

            LoadData();
        }

        private void LoadData()
        {
            // 1. Gán thông tin hiển thị cơ bản
            txtPhong.Text = _phong.MaPhong;
            txtKhachHang.Text = _phong.TenKhachHang;
            
            if (_phong.NgayCheckIn.HasValue)
            {
                txtThoiGian.Text = $"{_phong.NgayCheckIn.Value:dd/MM HH:mm} - {DateTime.Now:dd/MM HH:mm}";
            }

            // 2. Tính toán tiền phòng và tiền phạt lố giờ
            if (_phong.IdChiTietDatPhong.HasValue)
            {
                var resTinhTien = BUS_LuuTru_TinhToan.Instance.TinhTienPhongVaPhatLoGio(
                    _phong.IdChiTietDatPhong.Value, out _tienPhat, out string ghiChuPhat);

                if (resTinhTien.Success)
                {
                    _tienPhong = resTinhTien.Data;
                    spinTienPhong.EditValue = _tienPhong;
                    spinTienPhat.EditValue = _tienPhat;
                    
                    if (_tienPhat > 0 && string.IsNullOrWhiteSpace(txtGhiChuPhuThu.Text))
                    {
                        // Gợi ý ghi chú phạt
                        txtGhiChuPhuThu.Text = ghiChuPhat; 
                    }
                }
                else
                {
                    UIHelper.ThongBaoLoi(resTinhTien.ErrorMessage);
                }
            }

            // 3. Load thông tin Khách Hàng (Hạng thành viên, Điểm) và Tiền Cọc
            var resThongTin = BUS_LuuTru_Booking.Instance.LayThongTinKhachHangCheckOut(_phong.IdChiTietDatPhong ?? 0);
            if (resThongTin.Success)
            {
                _idKhachHang = resThongTin.Data.IdKhachHang;
                _hangThanhVien = resThongTin.Data.HangThanhVien;
                _diemKhaDung = resThongTin.Data.DiemKhaDung;
                spinTienCoc.EditValue = resThongTin.Data.TienCoc;
                
                // Load tiền dịch vụ (vỡ ly, minibar...) vào ô Phụ thu khác
                spinPhuThu.EditValue = resThongTin.Data.TienDichVu;
            }
            else
            {
                string rawMsg = resThongTin.ErrorMessage ?? "ERR_SYSTEM_FAIL";
                if (rawMsg == "ERR_DAT_PHONG_KHONG_TON_TAI")
                {
                    if (UIHelper.XacNhan(LanguageManager.GetString("MSG_RESET_PHONG_TRONG") ?? "Không tìm thấy Booking hoạt động cho phòng này.\nBạn có muốn RESET trạng thái phòng về 'Trống' không?"))

                    {
                        BUS_LuuTru_Booking.Instance.CapNhatTrangThaiPhong(_phong.IdPhong, AppConstants.TrangThaiPhong.Trong);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        return;
                    }
                }
                
                string[] parts = rawMsg.Split('|');
                string errMsg = LanguageManager.GetString(parts[0]);
                if (parts.Length > 1 && !string.IsNullOrEmpty(errMsg) && errMsg.Contains("{0}"))
                {
                    try { errMsg = string.Format(errMsg, parts.Skip(1).ToArray()); } catch { }
                }
                if (errMsg == parts[0]) errMsg = rawMsg;
                UIHelper.ThongBaoLoi(errMsg);
                this.Close();
                return;
            }

            string strDiemKhaDung = LanguageManager.GetString("LBL_DIEM_KHA_DUNG") ?? "Điểm khả dụng:";
            lblDiem.Text = $"{strDiemKhaDung} {_diemKhaDung:N0}";
            
            CheckAutoPromotion();
            TinhTongTien();
        }

        private void CheckAutoPromotion()
        {
            decimal tongTienTruocKM = _tienPhong + _tienPhat;
            var bestKM = BUS_KhuyenMai.Instance.TimToHopBestDeal(tongTienTruocKM, _hangThanhVien).FirstOrDefault();
            if (bestKM != null)
            {
                txtMaKhuyenMai.Text = bestKM.MaKhuyenMai;
                BtnApDungKM_Click(null, null);
                
                // Vạch mặt Bug: Hiển thị rõ mã nào đang gây ra việc giảm tiền
                if (bestKM.SoTienGiamThucTe > 0 && bestKM.SoTienGiamThucTe < 10)
                {
                     DevExpress.XtraEditors.XtraMessageBox.Show($"PHÁT HIỆN KM LẺ: {bestKM.TenKhuyenMai} (Mã: {bestKM.MaKhuyenMai}) đang giảm {bestKM.SoTienGiamThucTe}đ. Đây là lý do gây ra số lẻ!");
                }
            }

            var hint = BUS_KhuyenMai.Instance.GetPromotionHint(tongTienTruocKM, _hangThanhVien);
            if (hint != null)
            {
                decimal canThem = hint.DonToiThieu - tongTienTruocKM;
                string hintGoiY = LanguageManager.GetString("MSG_HINT_KHUYEN_MAI") ?? "Gợi ý: Dùng thêm {0:N0}đ dịch vụ để được giảm {1:N0}{2}";
                string unit = hint.LoaiGiamGia == AppConstants.LoaiGiamGia.PhanTram ? "%" : "đ";
                lblHint.Text = string.Format(hintGoiY, canThem, hint.GiaTriGiam, unit);
                lblHint.Appearance.ForeColor = System.Drawing.Color.Orange;
            }
            else
            {
                lblHint.Text = "";
            }
        }

        private void TinhTongTien()
        {
            decimal phuThuKhac = spinPhuThu.EditValue is decimal d1 ? d1 : 0m;
            decimal tienCoc    = spinTienCoc.EditValue  is decimal d2 ? d2 : 0m;

            decimal tongGiamGia = Math.Max(_tienGiamKM, _tienGiamDiem);

            // Tổng = Tiền phòng + Phạt + Phụ Thu - (Cọc + Giảm giá)
            decimal tong = _tienPhong + _tienPhat + phuThuKhac - tienCoc - tongGiamGia;
            if (tong < 0) tong = 0;

            spinTongTien.EditValue = Math.Floor(tong);

            // Nếu cọc > bill: hiển thị 0 và ghi nhận khoản cần hoàn lại
            if (_tienPhong + _tienPhat + phuThuKhac < tienCoc + tongGiamGia)
            {
                decimal du = (tienCoc + tongGiamGia) - (_tienPhong + _tienPhat + phuThuKhac);
                lblHoanCoc.Text = $"Cọc dư cần hoàn: {du:N0}đ";
                layoutLblHoanCoc.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                layoutLblHoanCoc.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private void BtnApDungKM_Click(object sender, EventArgs e)
        {
            string maKM = txtMaKhuyenMai.Text.Trim();
            if (string.IsNullOrEmpty(maKM))
            {
                _tienGiamKM = 0;
                _idKhuyenMaiApDung = null;
                TinhTongTien();
                return;
            }

            decimal tongTienTruocKM = _tienPhong + _tienPhat;
            var res = BUS_KhuyenMai.Instance.ApDungMa(maKM, tongTienTruocKM, _hangThanhVien);
            if (res.Success)
            {
                var km = res.Data as DTO_KhuyenMaiPOS;
                if (km != null)
                {
                    _tienGiamKM = km.SoTienGiamThucTe;
                    _idKhuyenMaiApDung = km.Id;
                }
                if (sender != null)
                {
                    UIHelper.ThongBao($"Áp dụng khuyến mãi thành công: -{_tienGiamKM:N0}đ"); 
                }
                TinhTongTien();
            }
            else
            {
                _tienGiamKM = 0;
                _idKhuyenMaiApDung = null;
                txtMaKhuyenMai.Text = "";
                UIHelper.ThongBaoLoi(res.ErrorMessage);
                TinhTongTien();
            }
        }

        private void ChkDungDiem_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDungDiem.Checked)
            {
                if (_diemKhaDung <= 0)
                {
                    UIHelper.ThongBaoLoi(LanguageManager.GetString("ERR_KHONG_CO_DIEM") ?? "Khách hàng không có điểm khả dụng!");
                    chkDungDiem.Checked = false;
                    return;
                }

                // Lấy tỷ lệ từ DB config
                decimal tyLeQuyDoi = BUS_CauHinh.Instance
                    .LayGiaTriDecimal(ET.Constants.AppConstants.ConfigKeys.DIEM_QUY_DOI, 1000m);
                _diemQuyDoi = _diemKhaDung;
                _tienGiamDiem = _diemQuyDoi * tyLeQuyDoi;
            }
            else
            {
                _diemQuyDoi = 0;
                _tienGiamDiem = 0;
            }
            TinhTongTien();
        }

        private void SpinPhuThu_EditValueChanged(object sender, EventArgs e)
        {
            TinhTongTien();
        }

        private void BtnThoat_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void BtnCheckOut_Click(object sender, EventArgs e)
        {
            if (!_phong.IdChiTietDatPhong.HasValue) return;

            decimal phuThuKhac = spinPhuThu.EditValue is decimal dPT ? dPT : 0m;
            string ghiChu = txtGhiChuPhuThu.Text.Trim();

            int idNhanVien = GUI.Infrastructure.SessionManager.IdDoiTac > 0 ? GUI.Infrastructure.SessionManager.IdDoiTac : 1;
            // Kiểm tra phiên thu ngân
            if (SessionManager.IdPhienGiaoDich <= 0)
            {
                // tìm phiên đang mở của nhân viên này
                var resPhien = BUS_PhienThuNgan.Instance.LayPhienDangMo(SessionManager.IdDoiTac);
                if (resPhien.Success && resPhien.Data != null)
                {
                    var phien = resPhien.Data as ET_PhienThuNgan;
                    SessionManager.IdPhienGiaoDich = phien.Id;
                }
                else
                {
                    // Yêu cầu mở phiên
                    if (UIHelper.XacNhan(LanguageManager.GetString("ERR_CHUA_MO_PHIEN") ?? "Bạn chưa mở phiên thu ngân. Bạn có muốn mở phiên ngay bây giờ không?"))
                    {
                        using (var frmSes = new frmPhienThuNgan(null, true))
                        {
                            if (frmSes.ShowDialog() == DialogResult.OK)
                            {
                                if (frmSes.PhienKetQua != null)
                                    SessionManager.IdPhienGiaoDich = frmSes.PhienKetQua.Id;
                                else
                                {
                                    UIHelper.ThongBaoLoi("Không thể khởi tạo phiên thu ngân.");
                                    return;
                                }
                            }
                            else return;
                        }
                    }
                    else return;
                }
            }

            int idPhienThuNgan = SessionManager.IdPhienGiaoDich;
            decimal tongTien = spinTongTien.EditValue is decimal dTT ? dTT : 0m;
            string phuongThuc = cboPhuongThuc.Text;
            decimal giamGia = Math.Max(_tienGiamKM, _tienGiamDiem);
            int diemToUse = (_tienGiamDiem >= _tienGiamKM) ? _diemQuyDoi : 0;
            int? kmToUse = (_tienGiamKM > _tienGiamDiem) ? _idKhuyenMaiApDung : null;

            int? idViDienTu = null;
            if (phuongThuc == AppConstants.PhuongThucTT.ViRFID)
            {
                string maThe = XtraInputBox.Show(LanguageManager.GetString("MSG_QUET_THE_RFID") ?? "Vui lòng quẹt thẻ RFID để thanh toán:", LanguageManager.GetString("TITLE_THANH_TOAN_VI") ?? "Thanh toán Ví RFID", "");
                if (string.IsNullOrEmpty(maThe)) return;

                var the = DAL.Repositories.BanHang.DAL_POS.Instance.TraCuuTheRFID(maThe);
                if (the == null)
                {
                    UIHelper.ThongBaoLoi(LanguageManager.GetString("ERR_THE_KHONG_TON_TAI") ?? "Thẻ không tồn tại hoặc đã bị khóa!");
                    return;
                }

                var theObj = the as ET.DTOs.DTO_TheRFID;
                idViDienTu = theObj?.IdViDienTu;

                if (!idViDienTu.HasValue)
                {
                    UIHelper.ThongBaoLoi(LanguageManager.GetString("ERR_THE_CHUA_KICH_HOAT_VI") ?? "Thẻ này chưa được kích hoạt ví điện tử!");
                    return;
                }

                decimal soDu = DAL.Repositories.BanHang.DAL_POS.Instance.LaySoDuVi(idViDienTu.Value);
                if (soDu < tongTien)
                {
                    UIHelper.ThongBaoLoi(string.Format(LanguageManager.GetString("ERR_VI_KHONG_DU_TIEN") ?? "Số dư ví không đủ! (Hiện có: {0:N0})", soDu));
                    return;
                }
            }

            var res = BUS_LuuTru_Booking.Instance.XuLyCheckOut_MotPhong(
                _phong.IdChiTietDatPhong.Value, idNhanVien, idPhienThuNgan, phuThuKhac, ghiChu,
                phuongThuc, giamGia, diemToUse, kmToUse, tongTien, idViDienTu);

            if (res.Success)
            {
                BUS.Services.HeThong.BUS_NhatKy.Instance.GhiLog("Lưu Trú", _phong.IdChiTietDatPhong.Value, "Trả phòng", idNhanVien, $"Phòng {_phong.MaPhong}, Phạt {_tienPhat}", $"Tổng thanh toán: {tongTien}", $"PT: {phuongThuc}. Ghi chú: {ghiChu}");

                UIHelper.ThongBao(LanguageManager.GetString("MSG_TRA_PHONG_THANH_CONG") ?? "Trả phòng thành công!");
                
                try
                {
                    decimal tienCoc = 0; 
                    decimal.TryParse(spinTienCoc.EditValue?.ToString(), out tienCoc);
                    
                    var rpt = new rptHoaDonLuuTru(_phong, _tienPhong, _tienPhat, phuThuKhac, tienCoc);
                    var printTool = new DevExpress.XtraReports.UI.ReportPrintTool(rpt);
                    printTool.ShowPreviewDialog();
                }
                catch (Exception ex)
                {
                    string errPrint = LanguageManager.GetString("ERR_PRINT_INVOICE") ?? "Lỗi in hóa đơn: ";
                    UIHelper.ThongBaoLoi(errPrint + ex.Message);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                string rawMsg = res.ErrorMessage ?? "ERR_SYSTEM_FAIL";
                string[] parts = rawMsg.Split('|');
                string errMsg = LanguageManager.GetString(parts[0]);
                
                if (parts.Length > 1 && !string.IsNullOrEmpty(errMsg) && errMsg.Contains("{0}"))
                {
                    try { errMsg = string.Format(errMsg, parts.Skip(1).ToArray()); } catch { }
                }
                
                if (errMsg == parts[0]) errMsg = rawMsg;

                UIHelper.ThongBaoLoi(errMsg);
            }
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Tile;
using BUS.Services.BanHang;
using ET.Constants;
using System.Linq;
using GUI.Infrastructure;
using BUS.Services.HeThong;
using ET.Models.BanHang;

namespace GUI.Modules.BanHang
{
    public partial class ucLuuTru_Main : XtraUserControl
    {
        public ucLuuTru_Main()
        {
            InitializeComponent();
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            // Lấy toàn bộ phòng trên sơ đồ theo ID Khu Vực hiện tại của nhân viên
            var result = BUS_LuuTru_SoDo.Instance.LayDanhSachSodoPhong(null);
            if (result.Success)
            {
                gridControlPhong.DataSource = result.Data;
            }
            else
            {
                string[] parts = result.ErrorMessage.Split('|');
                string errMsg = LanguageManager.GetString(parts[0]);
                if (parts.Length > 1)
                {
                    errMsg = string.Format(errMsg, parts.Skip(1).ToArray());
                }
                UIHelper.ThongBaoLoi(errMsg);
            }
        }

        private void ucLuuTru_Main_Load(object sender, EventArgs e)
        {
            AppStyle.StyleBtnPrimary(btnActionCheckOut);
            AppStyle.StyleBtnOutline(btnActionMinibar, AppStyle.Coral);
            AppStyle.StyleBtnOutline(btnActionThemDichVu, AppStyle.Gold);
            AppStyle.StyleBtnOutline(btnActionOpenMinibar, AppStyle.TealLight);
            btnLamMoi.Text = LanguageManager.GetString("BTN_LAM_MOI") ?? "Làm mới";
            if (tileViewPhong.Columns["MaPhong"] != null) tileViewPhong.Columns["MaPhong"].Caption = LanguageManager.GetString("COL_MA_PHONG") ?? "Mã Phòng";
            if (tileViewPhong.Columns["TenLoaiPhong"] != null) tileViewPhong.Columns["TenLoaiPhong"].Caption = LanguageManager.GetString("COL_LOAI_PHONG") ?? "Loại Phòng";
            if (tileViewPhong.Columns["TenKhachHang"] != null) tileViewPhong.Columns["TenKhachHang"].Caption = LanguageManager.GetString("COL_KHACH_HANG") ?? "Khách Hàng";

            tileViewPhong.OptionsTiles.ItemSize = new Size(260, 115);
            tileViewPhong.OptionsTiles.ItemPadding = new Padding(0);
            
            tileViewPhong.Appearance.ItemNormal.BackColor = Color.Transparent;
            tileViewPhong.Appearance.ItemNormal.BorderColor = Color.Transparent;
            tileViewPhong.Appearance.ItemFocused.BackColor = Color.Transparent;
            tileViewPhong.Appearance.ItemFocused.BorderColor = Color.Transparent;
            tileViewPhong.Appearance.ItemHovered.BackColor = Color.Transparent;
            tileViewPhong.Appearance.ItemHovered.BorderColor = Color.Transparent;
            
            tileViewPhong.ItemRightClick -= TileViewPhong_ItemRightClick;
            tileViewPhong.ItemRightClick += TileViewPhong_ItemRightClick;
            tileViewPhong.CustomColumnDisplayText -= TileViewPhong_CustomColumnDisplayText;
            tileViewPhong.CustomColumnDisplayText += TileViewPhong_CustomColumnDisplayText;

            LoadData();
            MoPhienThuNgan();
            InitTranslation();

            timerKiemTraDat.Start();
            CapNhatBadgeChuong();
        }

        private void InitTranslation()
        {
            groupKhachHang.Text = LanguageManager.GetString("LBL_SEC_KHACH_HANG");
            lciHoTen.Text = LanguageManager.GetString("LBL_HO_TEN_SIDEBAR");
            lciSDT.Text = LanguageManager.GetString("LBL_SDT_SIDEBAR");

            groupThoiGian.Text = LanguageManager.GetString("LBL_SEC_THOI_GIAN");
            lciCheckIn.Text = LanguageManager.GetString("LBL_CHECKIN_SIDEBAR");
            lciCheckOut.Text = LanguageManager.GetString("LBL_CHECKOUT_SIDEBAR");

            groupTaiChinh.Text = LanguageManager.GetString("LBL_SEC_TAI_CHINH");
            lciTienPhong.Text = LanguageManager.GetString("LBL_TIEN_PHONG_SIDEBAR");
            lciPhuThu.Text = LanguageManager.GetString("LBL_PHU_THU_SIDEBAR");
            lciDaCoc.Text = LanguageManager.GetString("LBL_DA_COC_SIDEBAR");
            lciTongTien.Text = LanguageManager.GetString("LBL_TONG_TIEN_SIDEBAR");

            groupThaoTac.Text = LanguageManager.GetString("LBL_SEC_THAO_TAC");
            btnActionCheckOut.Text = LanguageManager.GetString("BTN_ACTION_CHECKOUT");
            btnActionMinibar.Text = LanguageManager.GetString("BTN_ACTION_PHU_THU");
            btnActionDoiPhong.Text = LanguageManager.GetString("BTN_ACTION_DOI_PHONG");
            btnActionGiaHan.Text = LanguageManager.GetString("BTN_ACTION_GIA_HAN");
            btnActionThemDichVu.Text = LanguageManager.GetString("BTN_ACTION_IN_BILL");
            btnLamMoi.Text = LanguageManager.GetString("BTN_LAMMOI") ?? "Làm mới";
        }

        private void MoPhienThuNgan()
        {
            if (SessionManager.IdPhienGiaoDich > 0) return;

            // Kiểm tra xem nhân viên có phiên đang mở không
            var resultPhien = BUS_PhienThuNgan.Instance.LayPhienDangMo(SessionManager.IdDoiTac);
            if (resultPhien.Success && resultPhien.Data != null)
            {
                var phien = resultPhien.Data as ET_PhienThuNgan;
                SessionManager.IdPhienGiaoDich = phien.Id;
                return;
            }

            // Chưa có phiên -> Mở dialog
            if (UIHelper.XacNhan(LanguageManager.GetString("ERR_CHUA_MO_PHIEN") ?? "Bạn chưa mở phiên thu ngân. Vui lòng mở phiên trước khi thao tác."))
            {
                using (var frm = new frmPhienThuNgan(null, true))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        SessionManager.IdPhienGiaoDich = frm.PhienKetQua.Id;
                    }
                }
            }
        }



        private int _selectedIdPhong = 0;

        private void ResetSidebar()
        {
            lblRoomTitle.Text = LanguageManager.GetString("LBL_CHUA_CHON_PHONG") ?? "Chưa chọn phòng";
            lblStatusBadge.Text = "--";
            lblStatusBadge.Appearance.ForeColor = Color.Gray;

            lblHoTen.Text = "--";
            lblSDT.Text = "--";
            lblCheckIn.Text = "--";
            lblCheckOut.Text = "--";
            lblTienPhong.Text = "--";
            lblPhuThu.Text = "--";
            lblDaCoc.Text = "--";
            lblTongTien.Text = "--";
            lblTongTien.Appearance.ForeColor = SystemColors.ControlText;

            btnActionCheckOut.Enabled = false;
            btnActionMinibar.Enabled = false;
            btnActionThemDichVu.Enabled = false;
            btnActionDoiPhong.Enabled = false;
            btnActionGiaHan.Enabled = false;
            btnActionOpenMinibar.Enabled = false;
        }

        private void TileViewPhong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e) // hiển thị thật nhiều thứ khi được chọn
        {
            var view = sender as TileView;
            if (view == null || e.FocusedRowHandle < 0) { ResetSidebar(); return; }

            var phongInfo = view.GetRow(e.FocusedRowHandle) as ET.DTOs.DTO_PhongLuuTruView;
            if (phongInfo == null) { ResetSidebar(); return; }

            _selectedIdPhong = phongInfo.IdPhong;

            lblRoomTitle.Text = $"{phongInfo.MaPhong} - {phongInfo.TenLoaiPhong}";
            
            string statusKey = "STATUS_" + phongInfo.TrangThaiPhong.ToUpper().Replace("DANGSUDUNG", "DANG_O").Replace("DONDEP", "DON_DEP");
            lblStatusBadge.Text = LanguageManager.GetString(statusKey) ?? phongInfo.TrangThaiPhong;

            if (phongInfo.TrangThaiPhong == AppConstants.TrangThaiPhong.Trong) lblStatusBadge.Appearance.ForeColor = Color.Green;
            else if (phongInfo.TrangThaiPhong == AppConstants.TrangThaiPhong.DangO) lblStatusBadge.Appearance.ForeColor = Color.Red;
            else if (phongInfo.TrangThaiPhong == AppConstants.TrangThaiPhong.ChoDon) lblStatusBadge.Appearance.ForeColor = Color.Orange;
            else lblStatusBadge.Appearance.ForeColor = Color.Gray;

            lblHoTen.Text = string.IsNullOrEmpty(phongInfo.TenKhachHang) ? "--" : phongInfo.TenKhachHang;
            lblSDT.Text = string.IsNullOrEmpty(phongInfo.SoDienThoai) ? "--" : phongInfo.SoDienThoai;

            lblCheckIn.Text = phongInfo.NgayCheckIn.HasValue
                ? phongInfo.NgayCheckIn.Value.ToString("dd/MM/yyyy HH:mm")
                : "--";
            lblCheckOut.Text = phongInfo.NgayCheckOut.HasValue
                ? phongInfo.NgayCheckOut.Value.ToString("dd/MM/yyyy HH:mm")
                : "--";

            bool isDangO = (phongInfo.TrangThaiPhong == AppConstants.TrangThaiPhong.DangO);
            if (isDangO && phongInfo.IdChiTietDatPhong.HasValue)
            {
                decimal tienPhat = 0;
                string ghiChuPhat = "";
                var resTT = BUS_LuuTru_TinhToan.Instance.TinhTienPhongVaPhatLoGio(
                    phongInfo.IdChiTietDatPhong.Value, out tienPhat, out ghiChuPhat);
                if (resTT.Success)
                {
                    lblTienPhong.Text = resTT.Data.ToString("N0") + "đ";
                    lblPhuThu.Text = tienPhat.ToString("N0") + "đ";
                }
                else
                {
                    lblTienPhong.Text = "--";
                    lblPhuThu.Text = "--";
                }

                var resKH = BUS_LuuTru_Booking.Instance.LayThongTinKhachHangCheckOut(phongInfo.IdChiTietDatPhong.Value);
                if (resKH.Success)
                {
                    lblDaCoc.Text = resKH.Data.TienCoc.ToString("N0") + "đ";
                    decimal giaPhong = resTT.Success ? resTT.Data : 0;
                    
                    // Lấy khuyến mãi tự động (nếu có) để hiển thị khớp với Popup Checkout
                    decimal tienGiamKM = 0;
                    var bestKM = BUS.Services.BanHang.BUS_KhuyenMai.Instance.TimToHopBestDeal(giaPhong + tienPhat, resKH.Data.HangThanhVien).FirstOrDefault();
                    if (bestKM != null)
                    {
                        tienGiamKM = bestKM.SoTienGiamThucTe;
                    }

                    decimal tong = giaPhong + tienPhat - resKH.Data.TienCoc - tienGiamKM;
                    if (tong < 0) tong = 0;

                    lblTongTien.Text = tong.ToString("N0") + "đ";
                }
                else
                {
                    lblDaCoc.Text = "--";
                    lblTongTien.Text = "--";
                }
                lblTongTien.Appearance.ForeColor = Color.Red;
            }
            else
            {
                lblTienPhong.Text = "--";
                lblPhuThu.Text = "--";
                lblDaCoc.Text = "--";
                lblTongTien.Text = "--";
                lblTongTien.Appearance.ForeColor = SystemColors.ControlText;
            }

            btnActionCheckOut.Enabled = isDangO;
            btnActionMinibar.Enabled = isDangO;
            btnActionThemDichVu.Enabled = isDangO;
            btnActionDoiPhong.Enabled = isDangO;
            btnActionGiaHan.Enabled = isDangO;
            btnActionOpenMinibar.Enabled = isDangO;
        }

        private void BtnActionCheckOut_Click(object sender, EventArgs e)
        {
            if (_selectedIdPhong <= 0) return;
            var view = gridControlPhong.MainView as TileView;
            var phongInfo = view?.GetRow(view.FocusedRowHandle) as ET.DTOs.DTO_PhongLuuTruView;
            if (phongInfo != null && phongInfo.IdPhong == _selectedIdPhong && phongInfo.TrangThaiPhong == AppConstants.TrangThaiPhong.DangO)
            {
                using (var frm = new frmCheckOut(phongInfo))
                {
                    if (frm.ShowDialog() == DialogResult.OK) LoadData();
                }
            }
        }

        private void BtnActionMinibar_Click(object sender, EventArgs e)
        {
            if (_selectedIdPhong <= 0) return;
            var view = gridControlPhong.MainView as TileView;
            var phongInfo = view?.GetRow(view.FocusedRowHandle) as ET.DTOs.DTO_PhongLuuTruView;
            if (phongInfo == null || !phongInfo.IdChiTietDatPhong.HasValue) return;

            using (var frm = new frmPhuThuDialog())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    int idNV = SessionManager.IdDoiTac > 0 ? SessionManager.IdDoiTac : 1;
                    var res = BUS_LuuTru_Booking.Instance.PhuThuDichVu(_selectedIdPhong, frm.SoTien, frm.GhiChu, idNV);
                    if (res.Success) UIHelper.ThongBao(LanguageManager.GetString("MSG_PHU_THU_THANH_CONG") ?? "Phụ thu thành công!");
                    else UIHelper.ThongBaoLoi(res.ErrorMessage);
                }
            }
        }


        private void BtnActionOpenMinibar_Click(object sender, EventArgs e)
        {
            if (_selectedIdPhong <= 0) return;
            var view = gridControlPhong.MainView as TileView;
            var phongInfo = view?.GetRow(view.FocusedRowHandle) as ET.DTOs.DTO_PhongLuuTruView;
            if (phongInfo == null || !phongInfo.IdChiTietDatPhong.HasValue) return;

            int idNV = GetIdNhanVien();
            using (var frm = new frmMinibar(phongInfo.IdChiTietDatPhong.Value, idNV))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void BtnActionThemDichVu_Click(object sender, EventArgs e)
        {
            if (_selectedIdPhong <= 0) return;
            var view = gridControlPhong.MainView as TileView;
            var phongInfo = view?.GetRow(view.FocusedRowHandle) as ET.DTOs.DTO_PhongLuuTruView;
            if (phongInfo == null || !phongInfo.IdChiTietDatPhong.HasValue) return;

            var resBill = BUS_LuuTru_Booking.Instance.LayChiTietBillPhong(phongInfo.IdChiTietDatPhong.Value);
            if (!resBill.Success || resBill.Data == null || resBill.Data.Count == 0)
            {
                UIHelper.ThongBao(LanguageManager.GetString("MSG_BILL_TRONG") ?? "Chưa có chi phí phát sinh nào.");
                return;
            }

            // Tính thêm tiền phạt lố giờ (nếu có)
            decimal tienPhat = 0;
            string ghiChuPhat = "";
            BUS_LuuTru_TinhToan.Instance.TinhTienPhongVaPhatLoGio(phongInfo.IdChiTietDatPhong.Value, out tienPhat, out ghiChuPhat);

            // Lấy tiền cọc
            decimal tienCoc = 0;
            var resKH = BUS_LuuTru_Booking.Instance.LayThongTinKhachHangCheckOut(phongInfo.IdChiTietDatPhong.Value);
            if (resKH.Success) tienCoc = resKH.Data.TienCoc;

            // Tổng tiến từ bill + phạt
            decimal tongBill = resBill.Data.Sum(x => x.ThanhTien);

            var rpt = new rptHoaDonLuuTru(phongInfo, tongBill, tienPhat, 0, tienCoc, resBill.Data);
            var printTool = new DevExpress.XtraReports.UI.ReportPrintTool(rpt);
            printTool.ShowPreviewDialog();
        }

        private void BtnActionDoiPhong_Click(object sender, EventArgs e)
        {
            if (_selectedIdPhong <= 0) return;
            XuLyDoiPhong(_selectedIdPhong);
        }

        private void BtnActionGiaHan_Click(object sender, EventArgs e)
        {
            if (_selectedIdPhong <= 0) return;
            XuLyGiaHan(_selectedIdPhong);
        }

        private void TileViewPhong_ItemDoubleClick(object sender, TileViewItemClickEventArgs e)
        {
            var view = sender as TileView;
            if (view == null) return;

            int idPhong = Convert.ToInt32(view.GetRowCellValue(e.Item.RowHandle, "IdPhong"));
            string trangThaiBooking = view.GetRowCellValue(e.Item.RowHandle, "TrangThaiBooking")?.ToString();
            string trangThai = view.GetRowCellValue(e.Item.RowHandle, "TrangThaiPhong")?.ToString();

            if (trangThai == AppConstants.TrangThaiPhong.Trong)
            {
                if (trangThaiBooking == AppConstants.TrangThaiChiTietDatPhong.ChoDen)
                {
                    if (DevExpress.XtraEditors.XtraMessageBox.Show(LanguageManager.GetString("MSG_XAC_NHAN_CHECKIN_DAT_TRUOC") ?? "Phòng này đã được đặt trước. Bạn muốn Check-in khách vào ở ngay bây giờ?", LanguageManager.GetString("TITLE_XAC_NHAN_CHECKIN") ?? "Xác nhận Check-in", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        var phongInfo = view.GetRow(e.Item.RowHandle) as ET.DTOs.DTO_PhongLuuTruView;
                        if (phongInfo != null && phongInfo.IdPhieuDatPhong.HasValue)
                        {
                            string maRFID = ""; 
                            int idNhanVien = GetIdNhanVien();
                            if (SessionManager.IdPhienGiaoDich <= 0) MoPhienThuNgan();
                            int idPhien = SessionManager.IdPhienGiaoDich;
                            if (idPhien <= 0) return;
                            var res = BUS_LuuTru_Booking.Instance.XuLyCheckIn(phongInfo.IdPhieuDatPhong.Value, idNhanVien, idPhien, maRFID);
                            if (res.Success) LoadData();
                            else UIHelper.ThongBaoLoi(res.ErrorMessage);
                        }
                    }
                }
                else
                {
                    using (var frm = new frmBookingDialog(idPhong))
                    {
                        if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            LoadData();
                        }
                    }
                }
            }
            else if (trangThai == AppConstants.TrangThaiPhong.DangO)
            {
                var phongInfo = view.GetRow(e.Item.RowHandle) as ET.DTOs.DTO_PhongLuuTruView;
                if (phongInfo != null)
                {
                    using (var frm = new frmCheckOut(phongInfo))
                    {
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            LoadData();
                        }
                    }
                }
            }
            else if (trangThai == AppConstants.TrangThaiPhong.ChoDon)
            {
                if (XtraMessageBox.Show(LanguageManager.GetString("MSG_DON_DEP_ROOM") ?? "Xác nhận đã dọn phòng xong?", LanguageManager.GetString("TITLE_BUONG_PHONG") ?? "Chức năng Buồng phòng", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var resDon = BUS_LuuTru_Booking.Instance.CapNhatTrangThaiPhong(idPhong, AppConstants.TrangThaiPhong.Trong);
                    if (resDon.Success) LoadData();
                    else UIHelper.ThongBaoLoi(resDon.ErrorMessage);
                }
            }
        }

        private void TileViewPhong_ItemRightClick(object sender, TileViewItemClickEventArgs e)
        {
            var view = sender as TileView;
            if (view == null) return;
            
            int idPhong = Convert.ToInt32(view.GetRowCellValue(e.Item.RowHandle, "IdPhong"));
            string trangThaiBooking = view.GetRowCellValue(e.Item.RowHandle, "TrangThaiBooking")?.ToString();
            string trangThai = view.GetRowCellValue(e.Item.RowHandle, "TrangThaiPhong")?.ToString();
            
            var nextStatesPhong = BUS_LuuTru_Booking.Instance.GetNextStates("Phong", trangThai);
            var nextStatesBooking = string.IsNullOrEmpty(trangThaiBooking) ? new System.Collections.Generic.List<string>() : BUS_LuuTru_Booking.Instance.GetNextStates("DatPhong", trangThaiBooking);
            
            // Ẩn tất cả các nút trước
            btnCheckIn.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            btnHuyDat.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            btnMinibar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            btnDoiPhong.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            btnGiaHan.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            btnDonXong.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            btnBaoTri.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            btnSuaXong.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            // Hum đáng suy ngẫm
            if (trangThai == AppConstants.TrangThaiPhong.Trong && trangThaiBooking == AppConstants.TrangThaiChiTietDatPhong.ChoDen)
            {
                btnCheckIn.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                btnCheckIn.Tag = idPhong;

                btnHuyDat.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                btnHuyDat.Tag = idPhong;
            }
            
            // Xử lý các chức năng phòng đang ở (DangO)
            if (trangThai == AppConstants.TrangThaiPhong.DangO)
            {
                btnMinibar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                btnMinibar.Tag = idPhong;

                btnDoiPhong.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                btnDoiPhong.Tag = idPhong;

                btnGiaHan.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                btnGiaHan.Tag = idPhong;
            }
            
            // Dọn xong (ChoDon -> Trong)
            if (nextStatesPhong.Contains(AppConstants.TrangThaiPhong.Trong) && trangThai == AppConstants.TrangThaiPhong.ChoDon)
            {
                btnDonXong.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                btnDonXong.Tag = idPhong;
            }

            // Đưa vào bảo trì (Trong -> BaoTri)
            if (nextStatesPhong.Contains(AppConstants.TrangThaiPhong.BaoTri) && trangThai == AppConstants.TrangThaiPhong.Trong && string.IsNullOrEmpty(trangThaiBooking))
            {
                btnBaoTri.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                btnBaoTri.Tag = idPhong;
            }

            // Hoàn thành bảo trì (BaoTri -> Trong)
            if (nextStatesPhong.Contains(AppConstants.TrangThaiPhong.Trong) && trangThai == AppConstants.TrangThaiPhong.BaoTri)
            {
                btnSuaXong.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                btnSuaXong.Tag = idPhong;
            }

            bool hasVisibleItem = false;
            foreach(DevExpress.XtraBars.LinkPersistInfo link in popupMenu1.LinksPersistInfo)
            {
                if(link.Item.Visibility == DevExpress.XtraBars.BarItemVisibility.Always)
                {
                    hasVisibleItem = true;
                    break;
                }
            }

            if (hasVisibleItem)
            {
                popupMenu1.ShowPopup(Control.MousePosition);
            }
        }


        private void BtnMinibar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var view = tileViewPhong;
            if (view.FocusedRowHandle < 0) return;
            var phongInfo = view.GetRow(view.FocusedRowHandle) as ET.DTOs.DTO_PhongLuuTruView;
            if (phongInfo == null || !phongInfo.IdChiTietDatPhong.HasValue) return;

            int idNV = GetIdNhanVien();
            using (var frm = new frmMinibar(phongInfo.IdChiTietDatPhong.Value, idNV))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private int GetIdNhanVien() => SessionManager.IdDoiTac > 0 ? SessionManager.IdDoiTac : 1;

        private void BtnDonXong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Item.Tag is int idPhong)
            {
                var res = BUS_LuuTru_Booking.Instance.CapNhatTrangThaiPhong(idPhong, AppConstants.TrangThaiPhong.Trong, GetIdNhanVien());
                if (res.Success) LoadData();
                else UIHelper.ThongBaoLoi(res.ErrorMessage);
            }
        }

        private void BtnBaoTri_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Item.Tag is int idPhong)
            {
                var res = BUS_LuuTru_Booking.Instance.CapNhatTrangThaiPhong(idPhong, AppConstants.TrangThaiPhong.BaoTri, GetIdNhanVien());
                if (res.Success) LoadData();
                else UIHelper.ThongBaoLoi(res.ErrorMessage);
            }
        }

        private void BtnSuaXong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Item.Tag is int idPhong)
            {
                var res = BUS_LuuTru_Booking.Instance.CapNhatTrangThaiPhong(idPhong, AppConstants.TrangThaiPhong.Trong, GetIdNhanVien());
                if (res.Success) LoadData();
                else UIHelper.ThongBaoLoi(res.ErrorMessage);
            }
        }

        private void BtnHuyDat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Item.Tag is int idPhong)
            {
                if (DevExpress.XtraEditors.XtraMessageBox.Show(LanguageManager.GetString("MSG_XAC_NHAN_HUY_DAT_PHONG") ?? "Bạn có chắc chắn muốn hủy đặt phòng này không?", LanguageManager.GetString("TITLE_HUY_DAT_PHONG") ?? "Xác nhận Hủy Đặt Phòng", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    // Lấy IdPhieuDatPhong từ view
                    var view = gridControlPhong.MainView as DevExpress.XtraGrid.Views.Tile.TileView;
                    if (view != null)
                    {
                        var phongInfo = view.GetRow(view.FocusedRowHandle) as ET.DTOs.DTO_PhongLuuTruView;
                        if (phongInfo != null && phongInfo.IdPhieuDatPhong.HasValue)
                        {
                            var res = BUS_LuuTru_Booking.Instance.HuyDatPhong(phongInfo.IdPhieuDatPhong.Value, GetIdNhanVien());
                            if (res.Success) LoadData();
                            else UIHelper.ThongBaoLoi(res.ErrorMessage);
                        }
                    }
                }
            }
        }

        private void BtnCheckInDatTruoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Item.Tag is int idPhong)
            {
                if (DevExpress.XtraEditors.XtraMessageBox.Show(LanguageManager.GetString("MSG_XAC_NHAN_CHECKIN_DAT_TRUOC") ?? "Phòng này đã được đặt trước. Bạn muốn Check-in khách vào ở ngay bây giờ?", LanguageManager.GetString("TITLE_XAC_NHAN_CHECKIN") ?? "Xác nhận Check-in", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    var view = gridControlPhong.MainView as DevExpress.XtraGrid.Views.Tile.TileView;
                    if (view != null)
                    {
                        var phongInfo = view.GetRow(view.FocusedRowHandle) as ET.DTOs.DTO_PhongLuuTruView;
                        if (phongInfo != null && phongInfo.IdPhieuDatPhong.HasValue)
                        {
                            string maRFID = "";
                            int idNhanVien = GetIdNhanVien();
                            if (SessionManager.IdPhienGiaoDich <= 0) MoPhienThuNgan();
                            int idPhien = SessionManager.IdPhienGiaoDich;
                            if (idPhien <= 0) return;
                            var res = BUS_LuuTru_Booking.Instance.XuLyCheckIn(phongInfo.IdPhieuDatPhong.Value, idNhanVien, idPhien, maRFID);
                            if (res.Success) LoadData();
                            else UIHelper.ThongBaoLoi(res.ErrorMessage);
                        }
                    }
                }
            }
        }

        private void BtnDoiPhong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Item.Tag is int idPhongCu)
            {
                XuLyDoiPhong(idPhongCu);
            }
        }

        private void XuLyDoiPhong(int idPhongCu)
        {
            var resPhong = BUS_LuuTru_SoDo.Instance.LayDanhSachSodoPhong(null);
            if (!resPhong.Success || resPhong.Data == null) return;

            var dstrung = resPhong.Data.Where(x => x.TrangThaiPhong == AppConstants.TrangThaiPhong.Trong && string.IsNullOrEmpty(x.TrangThaiBooking)).ToList();
            if (dstrung.Count == 0)
            {
                UIHelper.ThongBaoLoi(LanguageManager.GetString("ERR_KHONG_CO_PHONG_TRONG") ?? "Không có phòng trống nào để đổi.");
                return;
            }

            using (var frm = new frmDoiPhong(dstrung))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var res = BUS_LuuTru_Booking.Instance.DoiPhong(idPhongCu, frm.IdPhongMoi, GetIdNhanVien(), frm.LyDo);
                    if (res.Success) LoadData();
                    else UIHelper.ThongBaoLoi(res.ErrorMessage);
                }
            }
        }

        private void BtnGiaHan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Item.Tag is int idPhong)
            {
                XuLyGiaHan(idPhong);
            }
        }

        private void XuLyGiaHan(int idPhong)
        {
            var view = gridControlPhong.MainView as DevExpress.XtraGrid.Views.Tile.TileView;
            var phongInfo = view?.GetRow(view.FocusedRowHandle) as ET.DTOs.DTO_PhongLuuTruView;
            DateTime minDate = DateTime.Now.Date;
            DateTime currentDate = phongInfo != null && phongInfo.NgayCheckOut.HasValue ? phongInfo.NgayCheckOut.Value : DateTime.Now.Date.AddDays(1);
            if (currentDate < minDate) currentDate = minDate; // BIZ-5: Không cho phép chọn ngày quá khứ mặc định

            using (var frm = new frmGiaHan(currentDate))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var res = BUS_LuuTru_Booking.Instance.GiaHanPhong(idPhong, frm.NgayCheckOutMoi, GetIdNhanVien());
                    if (res.Success) LoadData();
                    else UIHelper.ThongBaoLoi(res.ErrorMessage);
                }
            }
        }

        private void TileViewPhong_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "TrangThaiPhong")
            {
                string val = e.Value?.ToString();
                if (string.IsNullOrEmpty(val)) return;
                string statusKey = "STATUS_" + val.ToUpper().Replace("DANGSUDUNG", "DANG_O").Replace("DONDEP", "DON_DEP");
                e.DisplayText = LanguageManager.GetString(statusKey) ?? val;
            }
        }

        // chuông

        private int _lastKnownCount = -1; // -1 = lần đầu load, không báo

        private void CapNhatBadgeChuong()
        {
            var res = BUS_LuuTru_Booking.Instance.LayDatPhongChoPhanCong("TuanNay"); // Lấy 7 ngày tới 
            if (!res.Success) return;

            int soPhieu = res.Data.Count;

            // Cập nhật text badge
            btnChuong.Text = soPhieu > 0
                ? $" Đặt trước ({soPhieu})"
                : " Đặt trước";

            // Màu sắc
            if (soPhieu > 0)
                AppStyle.StyleBtnOutline(btnChuong, Color.FromArgb(230, 81, 0));
            else
                AppStyle.StyleBtnOutline(btnChuong, Color.Gray);

            // Phát hiện đặt: count tăng so với lần kiểm tra trước
            bool coDataMoi = _lastKnownCount >= 0 && soPhieu > _lastKnownCount;
            _lastKnownCount = soPhieu;

            if (coDataMoi)
            {
                // Beep báo hiệu
                System.Media.SystemSounds.Beep.Play();

                // rung nút 3 lần để thu hút chú ý
                FlashBtnChuong(3);
            }
        }

        private async void FlashBtnChuong(int soLan)
        {
            try
            {
                var originalColor = btnChuong.Appearance.BackColor;
                for (int i = 0; i < soLan; i++)
                {
                    btnChuong.Appearance.BackColor = Color.FromArgb(255, 200, 0);
                    btnChuong.Refresh();
                    await System.Threading.Tasks.Task.Delay(250);
                    btnChuong.Appearance.BackColor = Color.FromArgb(230, 81, 0);
                    btnChuong.Refresh();
                    await System.Threading.Tasks.Task.Delay(250);
                }
            }
            catch { }
        }

        private void BtnChuong_Click(object sender, EventArgs e)
        {
            using (var frm = new frmDatPhongNotification())
            {
                frm.ShowDialog(this);
            }
            // Refresh badge sau khi đóng popup (có thể vừa check-in xong)
            CapNhatBadgeChuong();
        }

        private void TimerKiemTraDat_Tick(object sender, EventArgs e)
        {
            CapNhatBadgeChuong();
        }
    }
}

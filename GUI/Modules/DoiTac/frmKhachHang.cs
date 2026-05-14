using System;
using System.Drawing;
using System.Windows.Forms;
using BUS.Services.DoiTac;
using BUS.Services.TaiChinh;
using BUS.Services.HeThong;
using ET.Models.DoiTac;
using DevExpress.XtraEditors;
using GUI.AI;
using GUI.Infrastructure;
using ET.Constants;

namespace GUI.Modules.DoiTac
{
    public partial class frmKhachHang : XtraUserControl, IAIModuleContext, IAICommandHandler
    {
        private readonly BUS_KhachHang _busKH = BUS_KhachHang.Instance;
        private readonly BUS_ViDienTu _busVi = BUS_ViDienTu.Instance;
        private readonly BUS_TheRFID _busThe = BUS_TheRFID.Instance;
        private readonly BUS_LichSuDiem _busDiem = BUS_LichSuDiem.Instance;
        private readonly BUS_NhatKy _nhatKy = BUS_NhatKy.Instance;

        private int _idDangChon;
        private bool _dangThemMoi;
        private bool _isDirty;

        private readonly string[] _dsLoaiKhach = new[] { 
            AppConstants.LoaiKhach.CaNhan, AppConstants.LoaiKhach.Doan, AppConstants.LoaiKhach.DoanhNghiep, 
            AppConstants.LoaiKhach.HocSinhSinhVien, AppConstants.LoaiKhach.NoiBo 
        };
        private readonly string[] _dsHangTV = new[] { 
            AppConstants.HangThanhVien.Thuong, AppConstants.HangThanhVien.Bac, AppConstants.HangThanhVien.Vang, AppConstants.HangThanhVien.KimCuong 
        };

        private readonly Action<object> _onLangChanged;

        public frmKhachHang()
        {
            InitializeComponent();
            _onLangChanged = _ => { DichNgonNgu(); NapComboBox(); TaiDanhSach(); };
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            AppStyle.StyleForm(this);
            AppStyle.StyleBanner(pnlBanner, lblTitle);
            AppStyle.StyleStatusBar(pnlBottom, lblTongKH);
            AppStyle.StyleTabControl(tabChiTiet);
            AppStyle.StyleGrid(gridViewKH);
            AppStyle.StyleGrid(gridViewGD);
            AppStyle.StyleGrid(gridViewDiem);
            AppStyle.StyleBtnPrimary(btnThemMoi);
            AppStyle.StyleBtnSecondary(btnLamMoi);
            AppStyle.StyleBtnPrimary(btnNapTien);
            AppStyle.StyleBtnDanger(btnKhoaThe);
            AppStyle.StyleBtnWarning(btnCapViThe);
            AppStyle.StyleBtnSecondary(btnChinhDiem);
            AppStyle.StyleBtnSecondary(btnSua);
            AppStyle.StyleBtnDanger(btnXoa);
            AppStyle.StyleBtnPrimary(btnLuu);
            AppStyle.StyleBtnOutline(btnHuyForm, AppStyle.Navy);
            StyleMetrics();
            DichNgonNgu();
            NapComboBox();
            TaiDanhSach();
            AnChiTiet();
            CauHinhInputMask();
            CauHinhTracking();
            gridViewKH.RowCellStyle += GridViewKH_RowCellStyle;
            AppStyle.FixEditorForeColor(this);
            EventBus.Subscribe("LanguageChanged", _onLangChanged);
            this.HandleDestroyed += frmKhachHang_HandleDestroyed;
        }

        private void frmKhachHang_HandleDestroyed(object sender, EventArgs e)
        {
            EventBus.Unsubscribe("LanguageChanged", _onLangChanged);
        }

        #region Style helpers

        private void StyleMetrics()
        {
            var fontTitle = new Font("Segoe UI", 7.5f);
            var fontValue = new Font("Segoe UI Semibold", 14f);

            lblSoDuTitle.Font = fontTitle;
            lblSoDuTitle.ForeColor = AppStyle.TextMuted;
            lblSoDuValue.Font = fontValue;
            lblSoDuValue.ForeColor = AppStyle.Teal;

            lblDiemTitle.Font = fontTitle;
            lblDiemTitle.ForeColor = AppStyle.TextMuted;
            lblDiemValue.Font = fontValue;
            lblDiemValue.ForeColor = AppStyle.Success;

            lblTheTitle.Font = fontTitle;
            lblTheTitle.ForeColor = AppStyle.TextMuted;
            lblTheValue.Font = new Font("Segoe UI Semibold", 10f);
            lblTheValue.ForeColor = AppStyle.Navy;

            lblTongChiTitle.Font = fontTitle;
            lblTongChiTitle.ForeColor = AppStyle.TextMuted;
            lblTongChiValue.Font = fontValue;
            lblTongChiValue.ForeColor = AppStyle.Navy;

            lblTenKhach.Font = new Font("Segoe UI Semibold", 14f);
            lblTenKhach.ForeColor = AppStyle.TextPrimary;
            lblBadge.Font = new Font("Segoe UI Semibold", 8f);
        }

        private void CauHinhInputMask()
        {
            txtDienThoai.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            txtDienThoai.Properties.Mask.EditMask = "0000000000d";
            txtDienThoai.Properties.Mask.UseMaskAsDisplayFormat = false;
            txtDienThoai.Properties.MaxLength = 11;

            txtCccd.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            txtCccd.Properties.Mask.EditMask = "000000000000";
            txtCccd.Properties.MaxLength = 12;
        }

        private void CauHinhTracking()
        {
            EventHandler markDirty = (s, e) => { if (pnlFormEdit.Visible) _isDirty = true; };
            txtHoTen.EditValueChanged += markDirty;
            txtDienThoai.EditValueChanged += markDirty;
            txtEmail.EditValueChanged += markDirty;
            txtCccd.EditValueChanged += markDirty;
            txtDiaChi.EditValueChanged += markDirty;
            cboLoaiKhach.EditValueChanged += markDirty;
            cboHangTV.EditValueChanged += markDirty;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F2 && pnlFormEdit.Visible)
            {
                btnLuu_Click(this, EventArgs.Empty);
                return true;
            }
            if (keyData == Keys.Escape && pnlFormEdit.Visible)
            {
                btnHuyForm_Click(this, EventArgs.Empty);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        #region Ngôn ngữ & Combo

        private void DichNgonNgu()
        {
            lblTitle.Text = LanguageManager.GetString("FRM_KH_TITLE") ?? "QUẢN LÝ KHÁCH HÀNG";
            btnThemMoi.Text = "  + " + (LanguageManager.GetString("BTN_THEM") ?? "Thêm Mới");
            btnLamMoi.Text = "  " + (LanguageManager.GetString("BTN_LAM_MOI") ?? "Làm mới");
            txtTimKiem.Properties.NullValuePrompt = LanguageManager.GetString("PROMPT_TIM_KIEM") ?? "Nhập SĐT, Mã thẻ, Tên KH...";

            btnNapTien.Text = "  " + (LanguageManager.GetString("BTN_KH_NAP_TIEN") ?? "Nạp tiền ví");
            btnKhoaThe.Text = "  " + (LanguageManager.GetString("BTN_KH_KHOA_THE") ?? "Khóa thẻ");
            btnCapViThe.Text = "  " + (LanguageManager.GetString("BTN_KH_CAP_VI_THE") ?? "Cấp ví & thẻ");
            btnChinhDiem.Text = "  " + (LanguageManager.GetString("BTN_KH_CHINH_DIEM") ?? "Chỉnh sửa điểm");
            btnSua.Text = LanguageManager.GetString("BTN_SUA") ?? "Sửa";
            btnXoa.Text = LanguageManager.GetString("BTN_XOA") ?? "Xóa";
            btnLuu.Text = "  " + (LanguageManager.GetString("BTN_LUU") ?? "Lưu (F2)");
            btnHuyForm.Text = "  " + (LanguageManager.GetString("BTN_HUY") ?? "Hủy (Esc)");

            tabLichSuGD.Text = LanguageManager.GetString("TAB_KH_LICHSU_GD") ?? "Lịch sử giao dịch";
            tabDiem.Text = LanguageManager.GetString("TAB_KH_DIEM") ?? "Lịch sử điểm";

            lblSoDuTitle.Text = (LanguageManager.GetString("LBL_KH_SODU") ?? "Số dư khả dụng").ToUpper();
            lblDiemTitle.Text = (LanguageManager.GetString("LBL_KH_DIEM") ?? "Điểm tích lũy").ToUpper();
            lblTheTitle.Text = (LanguageManager.GetString("LBL_KH_THE_RFID") ?? "Thẻ RFID").ToUpper();
            lblTongChiTitle.Text = (LanguageManager.GetString("LBL_KH_TONGCHI") ?? "Tổng chi tiêu").ToUpper();

            lblFrmTen.Text = LanguageManager.GetString("LBL_KH_TEN") ?? "Họ tên (*)";
            lblFrmSDT.Text = LanguageManager.GetString("LBL_KH_SDT") ?? "Điện thoại (*)";
            lblFrmEmail.Text = LanguageManager.GetString("LBL_KH_EMAIL") ?? "Email";
            lblFrmCccd.Text = LanguageManager.GetString("LBL_KH_CCCD") ?? "CCCD";
            lblFrmDiaChi.Text = LanguageManager.GetString("LBL_KH_DIACHI") ?? "Địa chỉ";
            lblFrmLoai.Text = LanguageManager.GetString("LBL_KH_LOAI") ?? "Loại khách";
            lblFrmHang.Text = LanguageManager.GetString("LBL_KH_HANG") ?? "Hạng thành viên";

            if (colMaKH != null) colMaKH.Caption = LanguageManager.GetString("COL_KH_MA") ?? "Mã KH";
            if (colHoTen != null) colHoTen.Caption = LanguageManager.GetString("COL_KH_HOTEN") ?? "Họ Tên";
            if (colSDT != null) colSDT.Caption = LanguageManager.GetString("COL_KH_DIENTHOAI") ?? "SĐT";
            if (colLoaiKhach != null) colLoaiKhach.Caption = LanguageManager.GetString("COL_KH_LOAIKHACH") ?? "Loại khách";

            if (_dangThemMoi) lblTenKhach.Text = LanguageManager.GetString("TITLE_THEM_KH") ?? "Thêm khách hàng mới";
            else if (_idDangChon > 0) { /* Refresh logic for existing customer title if needed */ }
        }

        private void NapComboBox()
        {
            cboLoaiKhach.Properties.Items.Clear();
            foreach (var item in _dsLoaiKhach)
            {
                cboLoaiKhach.Properties.Items.Add(LanguageManager.GetString(item) ?? item);
            }
            cboLoaiKhach.SelectedIndex = 0;

            cboHangTV.Properties.Items.Clear();
            foreach (var item in _dsHangTV)
            {
                cboHangTV.Properties.Items.Add(LanguageManager.GetString(item) ?? item);
            }
            cboHangTV.SelectedIndex = 0;
        }

        #endregion

        #region Tải dữ liệu

        private void TaiDanhSach()
        {
            var ds = _busKH.LayDanhSach(txtTimKiem.Text);
            gridKhachHang.DataSource = ds;
            lblTongKH.Text = string.Format("{0} {1}",
                ds.Count,
                LanguageManager.GetString("TXT_KH_KHACH") ?? "Khách hàng");
        }

        private void NapChiTiet(int idDoiTac)
        {
            var dto = _busKH.LayChiTiet(idDoiTac);
            if (dto == null) { AnChiTiet(); return; }

            HienChiTiet();

            lblTenKhach.Text = dto.HoTen;
            string mappedLoaiKhach = dto.LoaiKhach != null ? (LanguageManager.GetString(dto.LoaiKhach) ?? dto.LoaiKhach) : "Cá nhân";
            lblBadge.Text = "  " + mappedLoaiKhach + "  ";
            lblBadge.Appearance.BackColor = AppStyle.TealLight;
            lblBadge.Appearance.ForeColor = Color.White;
            lblBadge.Appearance.Options.UseBackColor = true;
            lblBadge.Appearance.Options.UseForeColor = true;
            lblBadge.Location = new Point(lblTenKhach.Right + 12, lblTenKhach.Top + 4);

            string lbMa = LanguageManager.GetString("LBL_KH_FIELD_MA") ?? "Mã KH";
            string lbSDT = LanguageManager.GetString("LBL_KH_FIELD_SDT") ?? "SĐT";
            string lbEmail = LanguageManager.GetString("LBL_KH_FIELD_EMAIL") ?? "Email";
            string lbCCCD = LanguageManager.GetString("LBL_KH_FIELD_CCCD") ?? "CCCD";
            string lbDiaChi = LanguageManager.GetString("LBL_KH_FIELD_DIACHI") ?? "Địa chỉ";
            string lbNgayTao = LanguageManager.GetString("LBL_KH_FIELD_NGAYTAO") ?? "Ngày ĐK";
            string lbHang = LanguageManager.GetString("LBL_KH_FIELD_HANG") ?? "Hạng TV";

            lblInfoMaKH.Text = string.Format("{0}: {1}", lbMa, dto.MaKhachHang);
            lblInfoSDT.Text = string.Format("{0}: {1}", lbSDT, dto.DienThoai);
            lblInfoEmail.Text = string.Format("{0}: {1}", lbEmail, dto.Email ?? "—");
            lblInfoCCCD.Text = string.Format("{0}: {1}", lbCCCD, dto.Cccd ?? "—");
            lblInfoDiaChi.Text = string.Format("{0}: {1}", lbDiaChi, dto.DiaChi ?? "—");
            lblInfoNgayTao.Text = string.Format("{0}: {1:dd/MM/yyyy}", lbNgayTao, dto.NgayTao);
            string mappedHangTV = dto.HangThanhVien != null ? (LanguageManager.GetString(dto.HangThanhVien) ?? dto.HangThanhVien) : "Thường";
            lblInfoHangTV.Text = string.Format("{0}: {1}", lbHang, mappedHangTV);

            lblSoDuValue.Text = string.Format("{0:N0}đ", dto.SoDuVi);
            lblDiemValue.Text = string.Format("{0:N0} pts", dto.DiemTichLuy);
            lblTongChiValue.Text = string.Format("{0:N0}đ", dto.TongChiTieu);

            string ttThe = dto.TrangThaiThe ?? "N/A";
            string strTrangThaiThe = ttThe;
            
            if (ttThe == AppConstants.TrangThaiThe.DangDung) strTrangThaiThe = LanguageManager.GetString("TXT_THE_DANGDUNG") ?? "Đang dùng";
            else if (ttThe == AppConstants.TrangThaiThe.DaKhoa) strTrangThaiThe = LanguageManager.GetString("TXT_THE_DAKHOA") ?? "Đã khóa";
            else if (ttThe == AppConstants.TrangThaiThe.ChuaKichHoat) strTrangThaiThe = LanguageManager.GetString("TXT_THE_CHUAKICHHOAT") ?? "Chưa kích hoạt";
            else if (ttThe == AppConstants.TrangThaiThe.DaTra) strTrangThaiThe = LanguageManager.GetString("TXT_THE_DATRA") ?? "Đã trả";

            lblTheValue.Text = strTrangThaiThe + (dto.MaTheRFID != null ? " | " + dto.MaTheRFID : "");
            if (ttThe == AppConstants.TrangThaiThe.DangDung) lblTheValue.ForeColor = AppStyle.Success;
            else if (ttThe == AppConstants.TrangThaiThe.DaKhoa) lblTheValue.ForeColor = AppStyle.Danger;
            else lblTheValue.ForeColor = AppStyle.TextMuted;

            bool coThe = dto.MaTheRFID != null;
            bool theDangDung = ttThe == AppConstants.TrangThaiThe.DangDung;
            bool theDaKhoa = ttThe == AppConstants.TrangThaiThe.DaKhoa;

            btnNapTien.Visible = dto.CoViDienTu;
            btnKhoaThe.Visible = coThe && (theDangDung || theDaKhoa);
            
            if (btnKhoaThe.Visible)
            {
                btnKhoaThe.Tag = ttThe;
                if (theDangDung)
                {
                    btnKhoaThe.Text = "  " + (LanguageManager.GetString("BTN_KH_KHOA_THE") ?? "Khóa thẻ");
                    AppStyle.StyleBtnDanger(btnKhoaThe);
                }
                else
                {
                    btnKhoaThe.Text = "  " + (LanguageManager.GetString("BTN_KH_MOKHOA_THE") ?? "Mở khóa thẻ");
                    AppStyle.StyleBtnPrimary(btnKhoaThe);
                }
            }

            btnCapViThe.Visible = !dto.CoViDienTu;
            btnChinhDiem.Visible = true;

            gridLichSuGD.DataSource = _busVi.LayLichSuGiaoDich(idDoiTac);
            gridDiem.DataSource = _busDiem.LayLichSu(idDoiTac);
            DinhDangLuoiLichSu();
        }

        private void DinhDangLuoiLichSu()
        {
            gridViewGD.PopulateColumns();
            gridViewDiem.PopulateColumns();

            if (gridViewGD.Columns["MaGiaoDich"] != null) gridViewGD.Columns["MaGiaoDich"].Caption = LanguageManager.GetString("COL_KH_MAGD") ?? "Mã GD";
            if (gridViewGD.Columns["NhomGiaoDich"] != null) gridViewGD.Columns["NhomGiaoDich"].Caption = LanguageManager.GetString("COL_KH_NHOMGD") ?? "Nhóm GD";
            if (gridViewGD.Columns["LoaiGiaoDich"] != null) gridViewGD.Columns["LoaiGiaoDich"].Caption = LanguageManager.GetString("COL_KH_LOAIGD") ?? "Loại GD";
            if (gridViewGD.Columns["SoTien"] != null)
            {
                gridViewGD.Columns["SoTien"].Caption = LanguageManager.GetString("COL_KH_SOTIEN") ?? "Số Tiền";
                gridViewGD.Columns["SoTien"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            }
            if (gridViewGD.Columns["ThoiGian"] != null)
            {
                gridViewGD.Columns["ThoiGian"].Caption = LanguageManager.GetString("COL_KH_THOIGIAN") ?? "Thời Gian";
                gridViewGD.Columns["ThoiGian"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            }

            if (gridViewDiem.Columns["Id"] != null) { gridViewDiem.Columns["Id"].Visible = false; gridViewDiem.Columns["Id"].OptionsColumn.ShowInCustomizationForm = false; }
            if (gridViewDiem.Columns["IdKhachHang"] != null) { gridViewDiem.Columns["IdKhachHang"].Visible = false; gridViewDiem.Columns["IdKhachHang"].OptionsColumn.ShowInCustomizationForm = false; }
            if (gridViewDiem.Columns["IdDonHang"] != null) { gridViewDiem.Columns["IdDonHang"].Visible = false; gridViewDiem.Columns["IdDonHang"].OptionsColumn.ShowInCustomizationForm = false; }
            if (gridViewDiem.Columns["LoaiGiaoDich"] != null) gridViewDiem.Columns["LoaiGiaoDich"].Caption = LanguageManager.GetString("COL_KH_LOAIGD") ?? "Loại GD";
            if (gridViewDiem.Columns["SoDiem"] != null)
            {
                gridViewDiem.Columns["SoDiem"].Caption = LanguageManager.GetString("COL_KH_SODIEM") ?? "Số Điểm";
                gridViewDiem.Columns["SoDiem"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            }
            if (gridViewDiem.Columns["SoDuSauGD"] != null)
            {
                gridViewDiem.Columns["SoDuSauGD"].Caption = LanguageManager.GetString("COL_KH_SODUSAUGD") ?? "Số Dư Sau GD";
                gridViewDiem.Columns["SoDuSauGD"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridViewDiem.Columns["SoDuSauGD"].DisplayFormat.FormatString = "N0";
            }
            if (gridViewDiem.Columns["MoTa"] != null) gridViewDiem.Columns["MoTa"].Caption = LanguageManager.GetString("COL_KH_MOTA") ?? "Mô Tả";
            if (gridViewDiem.Columns["NgayTao"] != null)
            {
                gridViewDiem.Columns["NgayTao"].Caption = LanguageManager.GetString("COL_KH_THOIGIAN") ?? "Thời Gian";
                gridViewDiem.Columns["NgayTao"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            }
        }


        #endregion

        #region Hiển thị / Ẩn

        private void HienChiTiet()
        {
            pnlHeader.Visible = true;
            grpInfo.Visible = true;
            grpMetrics.Visible = true;
            pnlActions.Visible = true;
            tabChiTiet.Visible = true;
            pnlFormEdit.Visible = false;
        }

        private void AnChiTiet()
        {
            pnlHeader.Visible = false;
            grpInfo.Visible = false;
            grpMetrics.Visible = false;
            pnlActions.Visible = false;
            tabChiTiet.Visible = false;
            pnlFormEdit.Visible = false;
            btnNapTien.Visible = false;
            btnKhoaThe.Visible = false;
            btnCapViThe.Visible = false;
            btnChinhDiem.Visible = false;
        }

        private void XoaForm()
        {
            txtHoTen.Text = string.Empty;
            txtDienThoai.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtCccd.Text = string.Empty;
            txtDiaChi.Text = string.Empty;
            cboLoaiKhach.SelectedIndex = 0;
            cboHangTV.SelectedIndex = 0;
            _isDirty = false;
        }

        #endregion

        #region Sự kiện Grid & Toolbar

        private void GridViewKH_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                var hang = gridViewKH.GetRowCellValue(e.RowHandle, "HangThanhVien")?.ToString();
                if (hang == AppConstants.HangThanhVien.KimCuong)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 248, 220); 
                    if (e.Column.FieldName == "HoTen" || e.Column.FieldName == "LoaiKhach")
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
            }
        }

        private void GridViewGD_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "SoTien" && e.RowHandle >= 0)
            {
                var loaiGD = gridViewGD.GetRowCellValue(e.RowHandle, "LoaiGiaoDich")?.ToString();
                if (loaiGD == AppConstants.LoaiPhepVi.Nap || loaiGD == AppConstants.LoaiPhepVi.Cong) e.Appearance.ForeColor = AppStyle.Success;
                else if (loaiGD == AppConstants.LoaiPhepVi.Tru || loaiGD == AppConstants.LoaiPhepVi.Chi) e.Appearance.ForeColor = AppStyle.Danger;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        }

        private void GridViewKH_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "LoaiKhach" && e.Value != null)
            {
                string val = e.Value.ToString();
                e.DisplayText = LanguageManager.GetString(val) ?? val;
            }
        }

        private void GridViewGD_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "SoTien" && e.ListSourceRowIndex >= 0)
            {
                var loaiGD = gridViewGD.GetListSourceRowCellValue(e.ListSourceRowIndex, "LoaiGiaoDich")?.ToString();
                if (decimal.TryParse(e.Value?.ToString(), out decimal soTien))
                {
                    if (loaiGD == AppConstants.LoaiPhepVi.Nap || loaiGD == AppConstants.LoaiPhepVi.Cong) e.DisplayText = "+" + soTien.ToString("N0");
                    else if (loaiGD == AppConstants.LoaiPhepVi.Tru || loaiGD == AppConstants.LoaiPhepVi.Chi) e.DisplayText = "-" + soTien.ToString("N0");
                }
            }
            else if (e.Column.FieldName == "LoaiGiaoDich" && e.Value != null)
            {
                string val = e.Value.ToString();
                if (val == AppConstants.LoaiPhepVi.Nap) e.DisplayText = LanguageManager.GetString("TXT_GD_NAP") ?? "Nạp";
                else if (val == AppConstants.LoaiPhepVi.Tru) e.DisplayText = LanguageManager.GetString("TXT_GD_TRU") ?? "Trừ";
                else if (val == AppConstants.LoaiPhepVi.Thu) e.DisplayText = LanguageManager.GetString("TXT_GD_THU") ?? "Thu";
                else if (val == AppConstants.LoaiPhepVi.Chi) e.DisplayText = LanguageManager.GetString("TXT_GD_CHI") ?? "Chi";
                else if (val == AppConstants.LoaiPhepVi.Cong) e.DisplayText = LanguageManager.GetString("TXT_GD_CONG") ?? "Cộng";
            }
        }

        private void GridViewDiem_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "SoDiem" && e.RowHandle >= 0)
            {
                var loaiGD = gridViewDiem.GetRowCellValue(e.RowHandle, "LoaiGiaoDich")?.ToString();
                if (loaiGD == AppConstants.LoaiGiaoDichDiem.CongDiem) e.Appearance.ForeColor = AppStyle.Success;
                else if (loaiGD == AppConstants.LoaiGiaoDichDiem.TruDiem) e.Appearance.ForeColor = AppStyle.Danger;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        }

        private void GridViewDiem_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "SoDiem" && e.ListSourceRowIndex >= 0)
            {
                var loaiGD = gridViewDiem.GetListSourceRowCellValue(e.ListSourceRowIndex, "LoaiGiaoDich")?.ToString();
                if (int.TryParse(e.Value?.ToString(), out int soDiem))
                {
                    if (loaiGD == AppConstants.LoaiGiaoDichDiem.CongDiem) e.DisplayText = "+" + Math.Abs(soDiem).ToString("N0");
                    else if (loaiGD == AppConstants.LoaiGiaoDichDiem.TruDiem) e.DisplayText = "-" + Math.Abs(soDiem).ToString("N0");
                }
            }
            else if (e.Column.FieldName == "LoaiGiaoDich" && e.Value != null)
            {
                string val = e.Value.ToString();
                if (val == AppConstants.LoaiGiaoDichDiem.CongDiem) e.DisplayText = LanguageManager.GetString("TXT_DIEM_CONG") ?? "Cộng Điểm";
                else if (val == AppConstants.LoaiGiaoDichDiem.TruDiem) e.DisplayText = LanguageManager.GetString("TXT_DIEM_TRU") ?? "Trừ Điểm";
            }
        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            TaiDanhSach();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Text = string.Empty;
            TaiDanhSach();
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            _dangThemMoi = true;
            _idDangChon = 0;
            XoaForm();
            AnChiTiet();
            pnlHeader.Visible = true;
            lblTenKhach.Text = LanguageManager.GetString("TITLE_THEM_KH") ?? "Thêm khách hàng mới";
            lblBadge.Text = string.Empty;
            pnlFormEdit.Visible = true;
            _isDirty = false;
        }

        private void gridViewKH_FocusedRowChanged(object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridViewKH.FocusedRowHandle < 0) return;

            if (pnlFormEdit.Visible && _isDirty)
            {
                if (!UIHelper.XacNhanHuy()) 
                {
                    gridViewKH.FocusedRowChanged -= gridViewKH_FocusedRowChanged;
                    gridViewKH.FocusedRowHandle = e.PrevFocusedRowHandle;
                    gridViewKH.FocusedRowChanged += gridViewKH_FocusedRowChanged;
                    return;
                }
            }

            object idObj = gridViewKH.GetFocusedRowCellValue("IdDoiTac");
            if (idObj == null) return;

            _idDangChon = (int)idObj;
            _dangThemMoi = false;
            NapChiTiet(_idDangChon);
        }

        #endregion

        #region Sự kiện CRUD

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_idDangChon <= 0) return;
            _dangThemMoi = false;

            var dto = _busKH.LayChiTiet(_idDangChon);
            if (dto == null) return;

            txtHoTen.Text = dto.HoTen;
            txtDienThoai.Text = dto.DienThoai;
            txtEmail.Text = dto.Email;
            txtCccd.Text = dto.Cccd;
            txtDiaChi.Text = dto.DiaChi;

            cboLoaiKhach.SelectedItem = LanguageManager.GetString(dto.LoaiKhach) ?? dto.LoaiKhach;
            cboHangTV.SelectedItem = LanguageManager.GetString(dto.HangThanhVien) ?? dto.HangThanhVien;
            
            pnlFormEdit.Visible = true;
            _isDirty = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_idDangChon <= 0) return;
            if (!UIHelper.XacNhanXoa(lblTenKhach.Text)) return;

            string tenCu = lblTenKhach.Text;
            var kq = _busKH.XoaMem(_idDangChon);
            string msg = LanguageManager.GetString(kq.Message) ?? kq.Message;
            if (kq.Success)
            {
                UIHelper.ThongBao(msg);
                _nhatKy.GhiLog("KhachHang", _idDangChon, "XoaMem",
                    SessionManager.IdDoiTac, tenCu, null);
                _idDangChon = 0;
                TaiDanhSach();
                AnChiTiet();
            }
            else
            {
                UIHelper.Loi(msg);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            var dto = new DTO_KhachHangChiTiet
            {
                HoTen = txtHoTen.Text?.Trim(),
                DienThoai = txtDienThoai.Text?.Trim(),
                Email = txtEmail.Text?.Trim(),
                Cccd = txtCccd.Text?.Trim(),
                DiaChi = txtDiaChi.Text?.Trim(),
                LoaiKhach = cboLoaiKhach.SelectedIndex >= 0 ? _dsLoaiKhach[cboLoaiKhach.SelectedIndex] : AppConstants.LoaiKhach.CaNhan,
                HangThanhVien = cboHangTV.SelectedIndex >= 0 ? _dsHangTV[cboHangTV.SelectedIndex] : AppConstants.HangThanhVien.Thuong
            };

            ET.Results.OperationResult kq;
            bool wasThem = _dangThemMoi;
            if (_dangThemMoi)
                kq = _busKH.ThemMoi(dto);
            else
            {
                dto.IdDoiTac = _idDangChon;
                kq = _busKH.CapNhat(dto);
            }

            string msg = LanguageManager.GetString(kq.Message) ?? kq.Message;
            if (kq.Success)
            {
                UIHelper.ThongBao(msg);
                pnlFormEdit.Visible = false;
                _dangThemMoi = false;
                TaiDanhSach();
                if (kq.Data != null) _idDangChon = (int)kq.Data;
                if (_idDangChon > 0) NapChiTiet(_idDangChon);

                string hanhDong = wasThem ? "Them" : "Sua";
                _nhatKy.GhiLog("KhachHang", _idDangChon, hanhDong,
                    SessionManager.IdDoiTac, null, dto.HoTen + " | " + dto.DienThoai);
                _isDirty = false;
            }
            else
            {
                UIHelper.Loi(msg);
            }
        }

        private void btnHuyForm_Click(object sender, EventArgs e)
        {
            if (_isDirty && !UIHelper.XacNhanHuy()) return;

            pnlFormEdit.Visible = false;
            if (_idDangChon > 0) NapChiTiet(_idDangChon);
            else AnChiTiet();
            _isDirty = false;
        }

        #endregion

        #region Sự kiện nghiệp vụ

        private void btnNapTien_Click(object sender, EventArgs e)
        {
            if (_idDangChon <= 0) return;

            using (var frm = new frmNapTien())
            {
                if (frm.ShowDialog() != DialogResult.OK) return;

                decimal soTien = frm.SoTien;
                string phuongThuc = frm.PhuongThuc;

                var kq = _busVi.NapTien(_idDangChon, soTien, phuongThuc, SessionManager.IdDoiTac);
                string msg = LanguageManager.GetString(kq.Message) ?? kq.Message;
                if (kq.Success)
                {
                    UIHelper.ThongBao(msg);
                    _nhatKy.GhiLog("KhachHang", _idDangChon, "NapTien",
                        SessionManager.IdDoiTac, null, soTien.ToString("N0") + "đ (" + phuongThuc + ")");
                    NapChiTiet(_idDangChon);
                }
                else UIHelper.Loi(msg);
            }
        }

        private void btnKhoaThe_Click(object sender, EventArgs e)
        {
            if (_idDangChon <= 0) return;

            string trangThaiHienTai = btnKhoaThe.Tag as string;
            ET.Results.OperationResult kq;

            if (trangThaiHienTai == AppConstants.TrangThaiThe.DangDung)
            {
                kq = _busThe.KhoaTheTheoKhach(_idDangChon, SessionManager.IdDoiTac);
            }
            else
            {
                kq = _busThe.MoKhoaTheTheoKhach(_idDangChon, SessionManager.IdDoiTac);
            }

            string msg = LanguageManager.GetString(kq.Message) ?? kq.Message;
            if (kq.Success)
            {
                UIHelper.ThongBao(msg);
                string hd = trangThaiHienTai == AppConstants.TrangThaiThe.DangDung ? "KhoaThe" : "MoKhoaThe";
                _nhatKy.GhiLog("TheRFID", _idDangChon, hd,
                    SessionManager.IdDoiTac, trangThaiHienTai, null);
                NapChiTiet(_idDangChon);
            }
            else UIHelper.Loi(msg);
        }

        private void btnCapViThe_Click(object sender, EventArgs e)
        {
            if (_idDangChon <= 0) return;

            string maThe = XtraInputBox.Show(
                LanguageManager.GetString(AppConstants.ErrorMessages.MSG_KH_NHAP_MA_THE) ?? "Nhập mã thẻ RFID:",
                LanguageManager.GetString("BTN_KH_CAP_VI_THE") ?? "Cấp ví & thẻ",
                "");

            if (string.IsNullOrWhiteSpace(maThe)) return;

            var kq = _busVi.CapViVaThe(_idDangChon, maThe, SessionManager.IdDoiTac);
            string msg = LanguageManager.GetString(kq.Message) ?? kq.Message;
            if (kq.Success)
            {
                UIHelper.ThongBao(msg);
                _nhatKy.GhiLog("KhachHang", _idDangChon, "CapViThe",
                    SessionManager.IdDoiTac, null, maThe);
                NapChiTiet(_idDangChon);
            }
            else UIHelper.Loi(msg);
        }

        private void btnChinhDiem_Click(object sender, EventArgs e)
        {
            if (_idDangChon <= 0) return;

            using (var frm = new DevExpress.XtraEditors.XtraForm())
            {
                frm.Text = LanguageManager.GetString("BTN_KH_CHINH_DIEM") ?? "Chỉnh sửa điểm";
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.Size = new Size(350, 220);
                frm.FormBorderStyle = FormBorderStyle.FixedDialog;
                frm.MaximizeBox = false;
                frm.MinimizeBox = false;

                var lblDiem = new DevExpress.XtraEditors.LabelControl() { Text = LanguageManager.GetString(AppConstants.ErrorMessages.MSG_KH_NHAP_DIEM) ?? "Nhập điểm (+) hoặc (-):", Location = new Point(15, 15) };
                var numDiem = new DevExpress.XtraEditors.SpinEdit() { Location = new Point(15, 35), Width = 300 };
                numDiem.Properties.IsFloatValue = false;
                numDiem.Properties.MinValue = -1000000;
                numDiem.Properties.MaxValue = 1000000;
                numDiem.Value = 100;

                var lblLyDo = new DevExpress.XtraEditors.LabelControl() { Text = LanguageManager.GetString(AppConstants.ErrorMessages.MSG_KH_NHAP_LYDO) ?? "Nhập lý do điều chỉnh điểm:", Location = new Point(15, 75) };
                var txtLyDo = new DevExpress.XtraEditors.TextEdit() { Location = new Point(15, 95), Width = 300 };
                txtLyDo.Text = LanguageManager.GetString("TXT_KH_LYDO_MACDINH") ?? "Điều chỉnh cộng/trừ thao tác tay";

                var btnOk = new DevExpress.XtraEditors.SimpleButton() { Text = "OK", Location = new Point(150, 140), Width = 80, DialogResult = DialogResult.OK };
                var btnCancel = new DevExpress.XtraEditors.SimpleButton() { Text = "Cancel", Location = new Point(235, 140), Width = 80, DialogResult = DialogResult.Cancel };

                frm.Controls.AddRange(new System.Windows.Forms.Control[] { lblDiem, numDiem, lblLyDo, txtLyDo, btnOk, btnCancel });
                frm.AcceptButton = btnOk;
                frm.CancelButton = btnCancel;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    int soDiem = (int)numDiem.Value;
                    string moTa = txtLyDo.Text;

                    if (soDiem == 0) return;
                    if (string.IsNullOrWhiteSpace(moTa)) return;

                    ET.Results.OperationResult kq;
                    string moTaFull = string.Format("{0} (NV: {1})", moTa, SessionManager.HoTen);
                    if (soDiem > 0)
                        kq = _busDiem.CongDiem(_idDangChon, soDiem, null, moTaFull);
                    else
                        kq = _busDiem.TruDiem(_idDangChon, Math.Abs(soDiem), moTaFull);

                    string msg = LanguageManager.GetString(kq.Message) ?? kq.Message;
                    if (kq.Success)
                    {
                        UIHelper.ThongBao(msg);
                        _nhatKy.GhiLog("KhachHang", _idDangChon, "ChinhDiem",
                            SessionManager.IdDoiTac, null, string.Format("{0}{1} ({2})", soDiem > 0 ? "+" : "", soDiem, moTa));
                        NapChiTiet(_idDangChon);
                    }
                    else UIHelper.Loi(msg);
                }
            }
        }

        #endregion

        #region AI Integration

        public string AIContextName => "KHACH_HANG_CRM";
        public string AIContextDescription => "Quản lý khách hàng, ví RFID, điểm tích lũy";
        public string[] SuggestedQuestions => new[] { 
            LanguageManager.GetString("AI_SUG_KH_1") ?? "Tìm khách hàng VIP", 
            LanguageManager.GetString("AI_SUG_KH_2") ?? "Ai có điểm tích lũy cao nhất?", 
            LanguageManager.GetString("AI_SUG_KH_3") ?? "Lọc khách hạng Kim Cương" 
        };
        public string[] FilterableColumns => new[] { "HoTen", "MaKhachHang", "DienThoai", "HangThanhVien", "DiemTichLuy", "SoDu" };

        public void ExecuteAICommand(string commandName, System.Collections.Generic.Dictionary<string, object> args)
        {
            Action action = () =>
            {
                switch (commandName)
                {
                    case "ui_filter_grid":
                        if (args.ContainsKey("filter"))
                            gridViewKH.ActiveFilterString = args["filter"].ToString();
                        break;

                    case "ui_select_customer":
                        if (args.ContainsKey("id"))
                        {
                            int id;
                            if (int.TryParse(args["id"].ToString(), out id))
                                ChonKhachHangTheoId(id);
                        }
                        break;

                    case "ui_open_topup":
                        if (_idDangChon > 0)
                            btnNapTien_Click(this, EventArgs.Empty);
                        break;

                    case "ui_open_adjust_points":
                        if (_idDangChon > 0)
                            btnChinhDiem_Click(this, EventArgs.Empty);
                        break;

                    case "ui_open_issue_card":
                        if (_idDangChon > 0)
                            btnCapViThe_Click(this, EventArgs.Empty);
                        break;
                }
            };

            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }

        // Chọn khách hàng trên grid theo IdDoiTac (dùng bởi AI)
        private void ChonKhachHangTheoId(int idDoiTac)
        {
            for (int i = 0; i < gridViewKH.DataRowCount; i++)
            {
                object val = gridViewKH.GetRowCellValue(i, "IdDoiTac");
                if (val != null && (int)val == idDoiTac)
                {
                    gridViewKH.FocusedRowHandle = i;
                    return;
                }
            }
        }

        #endregion
    }
}

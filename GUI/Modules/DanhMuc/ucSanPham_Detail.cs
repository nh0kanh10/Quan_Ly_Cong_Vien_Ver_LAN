using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BUS.Services.DanhMuc;
using ET.Models.DanhMuc;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using ET.Constants;
using DAL.Repositories.DanhMuc;
using GUI.Infrastructure;
using BUS.Services.HeThong;

namespace GUI.Modules.DanhMuc
{
    /// <summary>
    /// Màn hình chi tiết Sản phẩm (Modal Dialog).
    /// 4 Tab: Thông tin chung / Bảng giá / Quy đổi ĐVT / Cấu hình vận hành.
    /// Tab 4 dùng Dynamic Panel: hoán đổi UserControl tuỳ LoaiSanPham.
    /// </summary>
    public partial class ucSanPham_Detail : XtraUserControl
    {
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxValProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider();
        private readonly BUS_SanPham _bus = BUS_SanPham.Instance;
        private readonly BUS_NhatKy _nhatKy = BUS_NhatKy.Instance;

        // null = Thêm mới, có giá trị = Chỉnh sửa
        private int? _idSanPham;
        private ET_SanPham _spHienTai;

        // Dữ liệu tra cứu dùng chung cho các ComboBox
        private List<ET_DonViTinh> _dsDonViTinh;
        private List<ET_DiemBanHang_POS> _dsDiemBan;
        private List<ET_CauHinhThue> _dsCauHinhThue;

        // UserControl động cho Tab 4
        private UserControl _ucCauHinhHienTai;

        // Cờ theo dõi thay đổi để cảnh báo mất dữ liệu (Socratic Gate 2)
        private bool _daThayDoi = false;
        private bool _dangTaiDuLieu = false;


        #region Khởi tạo và tải dữ liệu

        public ucSanPham_Detail(int? idSanPham = null)
        {
            InitializeComponent();
            _idSanPham = idSanPham;
            KhoiPhucNutBamDropDown();
        }

        private void KhoiPhucNutBamDropDown()
        {
            if (cboLoaiSP.Properties.Buttons.Count == 0)
                cboLoaiSP.Properties.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo));
                
            if (cboTrangThai.Properties.Buttons.Count == 0)
                cboTrangThai.Properties.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo));
                
            if (slkDonViTinh.Properties.Buttons.Count == 0)
                slkDonViTinh.Properties.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo));
                
            if (slkThueVAT.Properties.Buttons.Count == 0)
                slkThueVAT.Properties.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo));
                
            if (chkComboDiemBan.Properties.Buttons.Count == 0)
                chkComboDiemBan.Properties.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo));
        }

        private void ucSanPham_Detail_Load(object sender, EventArgs e)
        {
            AppStyle.StyleForm(this);
            AppStyle.StyleTabControl(tabControl);
            CauHinhNutBam();
            AppStyle.StyleLayoutControl(layoutControl);
            TaiDuLieuTraCuu();
            CauHinhGridBangGia();
            CauHinhGridQuyDoi();
            AppStyle.FixEditorForeColor(this);
            TheoDoiThayDoi(this.Controls);
            

            if (_idSanPham.HasValue)
            {
                // Chế độ Sửa: khoá Mã SP và Loại SP
                cboLoaiSP.Properties.ReadOnly = true;
                txtMaSP.Properties.ReadOnly = true;
                TaiDuLieuChinhSua(_idSanPham.Value);
            }
            else
            {
                // Khởi tạo DataSource rỗng cho các Grid ở chế độ Thêm mới
                gcBangGia.DataSource = new BindingList<ET_BangGia>();
                gcQuyDoi.DataSource = new BindingList<ET_QuyDoiDonVi>();
            }

            txtGiaUuTien.Properties.ReadOnly = true;
            txtGiaUuTien.Properties.Appearance.ForeColor = AppStyle.Teal;

            // Xử lý sự kiện riêng cho Ảnh Đại Diện
            if (picAnhDaiDien != null)
            {
                picAnhDaiDien.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
                picAnhDaiDien.Properties.ShowMenu = false; 
                picAnhDaiDien.Cursor = Cursors.Hand;
                
                // Móc sự kiện click
                picAnhDaiDien.Click -= PicAnhDaiDien_Click;
                picAnhDaiDien.Click += PicAnhDaiDien_Click;
            }

            this.HandleDestroyed += ucSanPham_Detail_HandleDestroyed;

            ThucHienDichNgonNgu();
        }

        private void PicAnhDaiDien_Click(object sender, EventArgs e)
        {
            ChonAnhDaiDien();
        }

        public void ThucHienDichNgonNgu()
        {
            // Dịch UI components bằng LanguageManager
            tabThongTin.Text = LanguageManager.GetString("TAB_THONG_TIN");
            tabBangGia.Text = LanguageManager.GetString("TAB_BANG_GIA");
            if (picAnhDaiDien != null) picAnhDaiDien.Properties.NullText = LanguageManager.GetString("TXT_CHON_ANH") ?? "Click chọn ảnh đại diện";
            tabQuyDoi.Text = LanguageManager.GetString("TAB_QUY_DOI");
            tabCauHinh.Text = LanguageManager.GetString("TAB_CAU_HINH");

            lciTenSP.Text = LanguageManager.GetString("LBL_TENSP");
            lciMaSP.Text = LanguageManager.GetString("LBL_MASP");
            lciLoaiSP.Text = LanguageManager.GetString("LBL_LOAISP");
            lciDonViTinh.Text = LanguageManager.GetString("LBL_DVT");
            lciThueVAT.Text = LanguageManager.GetString("LBL_VAT");
            lciTrangThai.Text = LanguageManager.GetString("LBL_TRANG_THAI");
            lciGiaUuTien.Text = LanguageManager.GetString("LBL_GIA_UU_TIEN");
            lciDiemBan.Text = LanguageManager.GetString("LBL_DIEM_BAN");

            chkAllPOS.Text = LanguageManager.GetString("CHK_ALL_POS");
            chkLaVatTu.Text = LanguageManager.GetString("CHK_VAT_TU");
            chkQuanLyLo.Text = LanguageManager.GetString("CHK_LO");

            // Tooltips vật tư & lô
            chkLaVatTu.ToolTipTitle = "Vật Tư Vật Lý";
            chkLaVatTu.ToolTip = LanguageManager.GetString("TOOLTIP_VATTU");
            chkLaVatTu.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            
            chkQuanLyLo.ToolTipTitle = "Quản Lý Lô (HSD)";
            chkQuanLyLo.ToolTip = LanguageManager.GetString("TOOLTIP_QUANLYLO");
            chkQuanLyLo.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;

            //  Tooltips  đơn vị tính và giá ưu tiên
            txtGiaUuTien.ToolTip = LanguageManager.GetString("TOOLTIP_GIAUUTIEN");
            txtGiaUuTien.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            slkDonViTinh.ToolTip = LanguageManager.GetString("TOOLTIP_DONVITINH");
            slkDonViTinh.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;

            btnLuu.Text = LanguageManager.GetString("BTN_LUU");
            btnHuy.Text = LanguageManager.GetString("BTN_HUY");

            CapNhatNgonNguLuoi();
        }

        private void CapNhatNgonNguLuoi()
        {
            if (gvBangGia.Columns["LoaiGia"] != null) gvBangGia.Columns["LoaiGia"].Caption = LanguageManager.GetString("COL_LOAIGIA");
            if (gvBangGia.Columns["HieuLucTu"] != null) gvBangGia.Columns["HieuLucTu"].Caption = LanguageManager.GetString("COL_HIEULUC_TU");
            if (gvBangGia.Columns["HieuLucDen"] != null) gvBangGia.Columns["HieuLucDen"].Caption = LanguageManager.GetString("COL_HIEULUC_DEN");
            if (gvBangGia.Columns["GiaBan"] != null) gvBangGia.Columns["GiaBan"].Caption = LanguageManager.GetString("COL_GIABAN");
            if (gvBangGia.Columns["TienCoc"] != null) gvBangGia.Columns["TienCoc"].Caption = LanguageManager.GetString("COL_TIENCOC");
            if (gvBangGia.Columns["PhutBlock"] != null) gvBangGia.Columns["PhutBlock"].Caption = LanguageManager.GetString("COL_PHUTBLOCK");
            if (gvBangGia.Columns["PhutTiep"] != null) gvBangGia.Columns["PhutTiep"].Caption = LanguageManager.GetString("COL_PHUTTIEP");
            if (gvBangGia.Columns["GiaPhuThu"] != null) gvBangGia.Columns["GiaPhuThu"].Caption = LanguageManager.GetString("COL_GIAPHUTHU");

            if (gvQuyDoi.Columns["IdDonViDich"] != null) gvQuyDoi.Columns["IdDonViDich"].Caption = LanguageManager.GetString("COL_MUC_TIEU_DON_VI");
            if (gvQuyDoi.Columns["TyLeQuyDoi"] != null) gvQuyDoi.Columns["TyLeQuyDoi"].Caption = LanguageManager.GetString("COL_TY_LE_CHUYEN_DOI");
            if (gvQuyDoi.Columns["GiaBan"] != null) gvQuyDoi.Columns["GiaBan"].Caption = LanguageManager.GetString("COL_GIABAN");

            
            // Tìm và dịch nút Thêm dòng trong các lưới
            foreach (Control ctrl in gcBangGia.Parent.Controls) if (ctrl is SimpleButton btn) btn.Text = LanguageManager.GetString("BTN_THEM_DONG");
            foreach (Control ctrl in gcQuyDoi.Parent.Controls) if (ctrl is SimpleButton btn) btn.Text = LanguageManager.GetString("BTN_THEM_DONG");

            // Nạp lại các dropdown để được cập nhật ngôn ngữ nội bộ (ComboBox Nhóm, Thuế, v.v...)
            TaiDuLieuTraCuu();

            var repoLoaiGia = gvBangGia.Columns["LoaiGia"]?.ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox;
            if (repoLoaiGia != null)
            {
                repoLoaiGia.Items.Clear();
                var dsLoaiGia = new[] { AppConstants.LoaiGiaBan.MacDinh, AppConstants.LoaiGiaBan.NgayLe, AppConstants.LoaiGiaBan.KhuyenMai };
                foreach (var lg in dsLoaiGia)
                {
                    var text = LanguageManager.GetString("LOAIGIA_" + lg.ToUpper()) ?? lg;
                    repoLoaiGia.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(text, lg, -1));
                }
            }

            // Lan truyền lệnh dịch xuống Dynamic Panel ở Tab 4
            if (_ucCauHinhHienTai is ucCauHinhVe ucVe) ucVe.ThucHienDichNgonNgu();
            else if (_ucCauHinhHienTai is ucCauHinhFnB ucFnb) ucFnb.ThucHienDichNgonNgu();
        }

        private void TheoDoiThayDoi(Control.ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                if (c is BaseEdit edit)
                {
                    edit.EditValueChanged += control_EditValueChanged;
                }
                if (c.HasChildren) TheoDoiThayDoi(c.Controls);
            }
        }

        private void control_EditValueChanged(object sender, EventArgs e)
        {
            if (!_dangTaiDuLieu) _daThayDoi = true;
        }

        private void ucSanPham_Detail_HandleDestroyed(object sender, EventArgs e)
        {
            if (picAnhDaiDien?.Image != null)
            {
                picAnhDaiDien.Image.Dispose();
                picAnhDaiDien.Image = null;
            }
        }

        private void cboLoaiSP_EditValueChanged(object sender, EventArgs e)
        {
            if (cboLoaiSP.EditValue == null) return;
            
            string selectedType = cboLoaiSP.EditValue.ToString();
            
            if (!_idSanPham.HasValue) // Chỉ áp dụng mớm mã khi tạo mới
            {
                string prefix = ET.Constants.ProductTypeHelper.GetPrefix(selectedType);
                // Thay thế prefix nếu ô Mã rỗng, hoặc đang chứa 1 prefix cũ (VD: VE_, FB_)
                if (string.IsNullOrEmpty(txtMaSP.Text) || (txtMaSP.Text.Length <= 4 && txtMaSP.Text.EndsWith("_")))
                {
                    txtMaSP.Text = prefix;
                    // Đưa con trỏ nháy (Caret) về cuối chuỗi cho user gõ tiếp số (Con đường 2: Vừa tạo sẵn vừa cho nhập đè Mã Vạch)
                    txtMaSP.SelectionStart = txtMaSP.Text.Length;
                }
            }

            bool isVirtual = ET.Constants.ProductTypeHelper.IsVirtualProduct(selectedType);
            bool isForced = ET.Constants.ProductTypeHelper.IsInventoryForced(selectedType);
            bool isForbidden = ET.Constants.ProductTypeHelper.IsInventoryForbidden(selectedType);

            if (isForbidden || isVirtual)
            {
                chkLaVatTu.Checked = false;
                chkLaVatTu.Enabled = false;
                if (isVirtual) 
                {
                    chkQuanLyLo.Checked = false;
                    chkQuanLyLo.Enabled = false;
                }
            }
            else if (isForced)
            {
                chkLaVatTu.Checked = true;
                chkLaVatTu.Enabled = false;
                chkQuanLyLo.Enabled = true;
            }
            else
            {
                chkLaVatTu.Enabled = true; 
                chkQuanLyLo.Enabled = true;
            }
        }

        private void chkLaVatTu_CheckedChanged(object sender, EventArgs e)
        {
            string loai = cboLoaiSP.EditValue?.ToString();
            DoiPanelCauHinh(loai);
        }

       

        private void txtTenSP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenSP.Text))
                dxValProvider.SetError(txtTenSP, LanguageManager.GetString(AppConstants.ErrorMessages.ERR_REQUIRED_TENSP) ?? "Bắt buộc nhập Tên Sản Phẩm!", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical);
            else
                dxValProvider.SetError(txtTenSP, "");
        }

        private void txtMaSP_Leave(object sender, EventArgs e)
        {
            // Bắt chặt lỗi User để nguyên tiền tố (VD: "FB_") mà không nhập thêm
            string maSp = txtMaSP.Text.Trim();
            if (string.IsNullOrWhiteSpace(maSp))
                dxValProvider.SetError(txtMaSP, LanguageManager.GetString(AppConstants.ErrorMessages.ERR_REQUIRED_MASP) ?? "Bắt buộc có Mã Sản Phẩm!", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical);
            else if (maSp.Length <= 4 && maSp.EndsWith("_"))
                dxValProvider.SetError(txtMaSP, LanguageManager.GetString(AppConstants.ErrorMessages.ERR_MASP_CHI_TIENTO) ?? "Mã sản phẩm không được chỉ chứa mỗi tiền tố! Hãy gõ tiếp hoặc quét mã vạch.", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical);
            else
                dxValProvider.SetError(txtMaSP, "");
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F2)
            {
                btnLuu.PerformClick();
                return true;
            }
            if (keyData == Keys.Escape)
            {
                btnHuy.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public bool DaThayDoi { get { return _daThayDoi; } set { _daThayDoi = value; } }

        private void CauHinhNutBam()
        {
            AppStyle.StyleBtnPrimary(btnLuu);
            AppStyle.StyleBtnOutline(btnHuy, AppStyle.Danger);
            pnlFooter.BackColor = AppStyle.BgCard;
        }



        private void ChonAnhDaiDien()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.webp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Sinh thư mục Upload nếu chưa có
                        string uploadsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Uploads", "SanPham");
                        if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
                        
                        // Sinh tên file không trùng lặp
                        string ext = Path.GetExtension(ofd.FileName);
                        string newFileName = Guid.NewGuid().ToString("N") + ext;
                        string destPath = Path.Combine(uploadsFolder, newFileName);
                        
                        File.Copy(ofd.FileName, destPath, true);
                        
                        // Xoá ảnh cũ trong bộ nhớ
                        if (picAnhDaiDien.Image != null) {
                            var oldImg = picAnhDaiDien.Image;
                            picAnhDaiDien.Image = null;
                            oldImg.Dispose();
                        }
                        
                        // Load lại ảnh mới
                        using (var fs = new FileStream(destPath, FileMode.Open, FileAccess.Read))
                        {
                            picAnhDaiDien.Image = Image.FromStream(fs);
                        }
                        
                        if (_spHienTai == null) _spHienTai = new ET_SanPham();
                        _spHienTai.AnhDaiDien = "Uploads\\SanPham\\" + newFileName;
                        
                        _daThayDoi = true;
                    }
                    catch (Exception ex)
                    {
                        UIHelper.Loi("Lỗi khi tải ảnh: " + ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Nạp dữ liệu tra cứu vào tất cả ComboBox / SearchLookUp.
        /// </summary>
        private void TaiDuLieuTraCuu()
        {
            _dsDonViTinh = _bus.LayDonViTinh(SessionManager.CurrentLanguage);
            slkDonViTinh.Properties.DataSource = _dsDonViTinh;
            slkDonViTinh.Properties.DisplayMember = "TenDonVi";
            slkDonViTinh.Properties.ValueMember = "Id";
            slkDonViTinh.Properties.NullText = LanguageManager.GetString("TXT_CHON_DVT");         
            slkDonViTinh.Properties.PopulateViewColumns();
            slkDonViTinh.Properties.View.Columns["Id"].Visible = false;
            slkDonViTinh.Properties.View.Columns["ConHoatDong"].Visible = false;
            slkDonViTinh.Properties.View.Columns["MaDonVi"].Visible = false;
            slkDonViTinh.Properties.View.Columns["TenDonVi"].Caption = LanguageManager.GetString("COL_TEN");
            slkDonViTinh.Properties.View.Columns["TenDonVi"].Width = 150;
            if (gvQuyDoi.Columns["IdDonViDich"] != null && gvQuyDoi.Columns["IdDonViDich"].ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repoSlkDVT)
            {
                repoSlkDVT.DataSource = _dsDonViTinh.Where(d => d.ConHoatDong).ToList();
            }

            _dsCauHinhThue = _bus.LayCauHinhThue(SessionManager.CurrentLanguage);
            slkThueVAT.Properties.DataSource = _dsCauHinhThue;
            slkThueVAT.Properties.DisplayMember = "TenThue";
            slkThueVAT.Properties.ValueMember = "Id";
            slkThueVAT.Properties.NullText = LanguageManager.GetString("TXT_CHON_THUE");

            slkThueVAT.Properties.PopulateViewColumns();
            slkThueVAT.Properties.View.Columns["Id"].Visible = false;
            slkThueVAT.Properties.View.Columns["ApDungChoLoaiSP"].Visible = false;
            slkThueVAT.Properties.View.Columns["MaThue"].Caption = LanguageManager.GetString("COL_MA");
            slkThueVAT.Properties.View.Columns["TenThue"].Caption = LanguageManager.GetString("COL_TEN");
            slkThueVAT.Properties.View.Columns["TyLePhanTram"].Caption = "% VAT";

            _dsDiemBan = _bus.LayDiemBanPOS();
            chkComboDiemBan.Properties.DataSource = _dsDiemBan;
            chkComboDiemBan.Properties.DisplayMember = "TenDiem";
            chkComboDiemBan.Properties.ValueMember = "Id";
            chkComboDiemBan.Properties.NullText = LanguageManager.GetString("TXT_CHON_DIEMBAN");

            // ComboBox Loại SP: nạp vào ImageComboBoxEdit với cặp (Hiển thị, Giá trị)
            cboLoaiSP.Properties.Items.Clear();
            var dsLoaiSP = new[]
            {
                AppConstants.LoaiSanPham.VeVaoKhu,
                AppConstants.LoaiSanPham.VeTroChoi,
                AppConstants.LoaiSanPham.AnUong,
                AppConstants.LoaiSanPham.DoUong,
                AppConstants.LoaiSanPham.HangHoa,
                AppConstants.LoaiSanPham.TuDo,
                AppConstants.LoaiSanPham.DoChoThue,
                AppConstants.LoaiSanPham.ChoiNghiMat,
                AppConstants.LoaiSanPham.LuuTru,
                AppConstants.LoaiSanPham.NguyenLieu,
                AppConstants.LoaiSanPham.GuiXe,
                AppConstants.LoaiSanPham.DatChoThuAn,
                AppConstants.LoaiSanPham.DoUongDongChai,
                AppConstants.LoaiSanPham.AnUongTienLoi,
            };
            foreach (var loai in dsLoaiSP)
            {
                cboLoaiSP.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(
                    LanguageManager.GetString("LOAISP_" + loai.ToUpper()), loai,-1));
            }

            // ComboBox Trạng thái
            cboTrangThai.Properties.Items.Clear();
            var dsTrangThai = new[] 
            { 
                AppConstants.TrangThaiSanPham.DangBan, 
                AppConstants.TrangThaiSanPham.TamNgung, 
                AppConstants.TrangThaiSanPham.NgungBan 
            };
            foreach (var status in dsTrangThai)
            {
                cboTrangThai.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(
                    LanguageManager.GetString("TRANGTHAI_" + status.ToUpper()), 
                    status, 
                    -1));
            }
            cboTrangThai.SelectedIndex = 0;
        }

        private void CauHinhGridBangGia()
        {
            AppStyle.StyleGrid(gvBangGia);
            CauHinhGridChinhSua(gvBangGia);

            // Cột LoaiGia: ComboBox
            var repoLoaiGia = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            var dsLoaiGia = new[] { AppConstants.LoaiGiaBan.MacDinh, AppConstants.LoaiGiaBan.NgayLe, AppConstants.LoaiGiaBan.KhuyenMai };
            foreach (var lg in dsLoaiGia)
            {
                var text = LanguageManager.GetString("LOAIGIA_" + lg.ToUpper()) ?? lg;
                repoLoaiGia.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(text, lg, -1));
            }
            repoLoaiGia.TextEditStyle = TextEditStyles.DisableTextEditor;
            gcBangGia.RepositoryItems.Add(repoLoaiGia);
            if (gvBangGia.Columns["LoaiGia"] != null) gvBangGia.Columns["LoaiGia"].ColumnEdit = repoLoaiGia;

            // Cột ngày: DateEdit
            var repoDate = new RepositoryItemDateEdit();
            repoDate.EditMask = "dd/MM/yyyy";
            repoDate.UseMaskAsDisplayFormat = true;
            gcBangGia.RepositoryItems.Add(repoDate);

            if (gvBangGia.Columns["HieuLucTu"] != null) gvBangGia.Columns["HieuLucTu"].ColumnEdit = repoDate;
            if (gvBangGia.Columns["HieuLucDen"] != null) gvBangGia.Columns["HieuLucDen"].ColumnEdit = repoDate;

            // Cột giá: SpinEdit VNĐ
            var repoGia = new RepositoryItemSpinEdit();
            repoGia.EditFormat.FormatString = "N0";
            repoGia.DisplayFormat.FormatString = "N0";
            repoGia.IsFloatValue = false;
            gcBangGia.RepositoryItems.Add(repoGia);

            if (gvBangGia.Columns["GiaBan"] != null) gvBangGia.Columns["GiaBan"].ColumnEdit = repoGia;

            // Cột phụ thu cho thuê: ẩn mặc định
            if (gvBangGia.Columns["TienCoc"] != null)
            {
                gvBangGia.Columns["TienCoc"].ColumnEdit = repoGia;
                gvBangGia.Columns["TienCoc"].Visible = false;
            }
            gvBangGia.Columns.AddVisible("PhutBlock", LanguageManager.GetString("COL_PHUTBLOCK")).Visible = false;
            gvBangGia.Columns.AddVisible("PhutTiep", LanguageManager.GetString("COL_PHUTTIEP")).Visible = false;
            
            if (gvBangGia.Columns["GiaPhuThu"] != null)
            {
                gvBangGia.Columns["GiaPhuThu"].ColumnEdit = repoGia;
                gvBangGia.Columns["GiaPhuThu"].Visible = false;
            }

            // Cột nút Xoá cuối mỗi dòng
            ThemCotXoa(gcBangGia, gvBangGia);

            gcBangGia.DataSource = new BindingList<ET_BangGia>();

            // Nút Thêm dòng phía dưới grid
            ThemNutThemDong(gcBangGia);
        }

        private void CauHinhGridQuyDoi()
        {
            AppStyle.StyleGrid(gvQuyDoi);
            CauHinhGridChinhSua(gvQuyDoi);

            // Cột ĐVT Đích: SearchLookUp
            var repoSlkDVT = new RepositoryItemSearchLookUpEdit();
            repoSlkDVT.DataSource = _dsDonViTinh.Where(d => d.ConHoatDong).ToList(); 
            repoSlkDVT.DisplayMember = "TenDonVi";
            repoSlkDVT.ValueMember = "Id";
            repoSlkDVT.NullText = LanguageManager.GetString("TXT_CHON_DVT");
            
            // Ẩn bớt cột rác trong Dropdown
            repoSlkDVT.View.Columns.AddField("TenDonVi").Visible = true;
            repoSlkDVT.View.Columns["TenDonVi"].Caption = LanguageManager.GetString("COL_TEN");

            gcQuyDoi.RepositoryItems.Add(repoSlkDVT);
            if (gvQuyDoi.Columns["IdDonViDich"] != null) gvQuyDoi.Columns["IdDonViDich"].ColumnEdit = repoSlkDVT;

            // Cột hệ số: SpinEdit
            var repoHeSo = new RepositoryItemSpinEdit();
            repoHeSo.IsFloatValue = true;
            repoHeSo.EditFormat.FormatString = "0.####";
            repoHeSo.DisplayFormat.FormatString = "0.####";
            gcQuyDoi.RepositoryItems.Add(repoHeSo);
            if (gvQuyDoi.Columns["TyLeQuyDoi"] != null) gvQuyDoi.Columns["TyLeQuyDoi"].ColumnEdit = repoHeSo;

            // Cột giá bán riêng cho đơn vị đích
            var repoGiaBanQD = new RepositoryItemSpinEdit();
            repoGiaBanQD.EditFormat.FormatString = "N0";
            repoGiaBanQD.DisplayFormat.FormatString = "N0";
            repoGiaBanQD.IsFloatValue = false;
            repoGiaBanQD.NullText = "(Tự tính)";
            repoGiaBanQD.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            gcQuyDoi.RepositoryItems.Add(repoGiaBanQD);
            if (gvQuyDoi.Columns["GiaBan"] != null) gvQuyDoi.Columns["GiaBan"].ColumnEdit = repoGiaBanQD;

            // Cột nút xoá  
            ThemCotXoa(gcQuyDoi, gvQuyDoi);

            gcQuyDoi.DataSource = new BindingList<ET_QuyDoiDonVi>();

            // Nút thêm dòng
            ThemNutThemDong(gcQuyDoi);
        }

        public void TaiDuLieuChinhSua(int id)
        {
            _dangTaiDuLieu = true;
            _idSanPham = id;
            _spHienTai = _bus.LayChiTiet(id);
            if (_spHienTai == null)
            {
                UIHelper.Loi("Không tìm thấy sản phẩm.");
                this.Hide();
                return;
            }

            // Mở khóa UI nếu đang ẩn ở chế độ Thêm mới
            SetReadOnlyFormat(cboLoaiSP, true);
            SetReadOnlyFormat(txtMaSP, true);

            // Tải Ảnh Đại Diện
            if (picAnhDaiDien.Image != null)
            {
                var oldImg = picAnhDaiDien.Image;
                picAnhDaiDien.Image = null;
                oldImg.Dispose();
            }

            if (!string.IsNullOrEmpty(_spHienTai.AnhDaiDien))
            {
                string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _spHienTai.AnhDaiDien);
                if (File.Exists(fullPath))
                {
                    using (var fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                    {
                        picAnhDaiDien.Image = Image.FromStream(fs);
                    }
                }
            }

            // Tab 1: Thông tin chung
            txtMaSP.Text = _spHienTai.MaSanPham;
            txtTenSP.Text = _spHienTai.TenSanPham;
            cboLoaiSP.EditValue = _spHienTai.LoaiSanPham;
            slkDonViTinh.EditValue = _spHienTai.IdDonViTinh;
            cboTrangThai.EditValue = _spHienTai.TrangThai;
            chkLaVatTu.Checked = _spHienTai.LaVatTu;
            chkQuanLyLo.Checked = _spHienTai.CanQuanLyLo;

            // Giá ưu tiên: lấy từ dòng BangGia mặc định đầu tiên
            SetReadOnlyFormat(txtGiaUuTien, true);
            txtGiaUuTien.Properties.Appearance.ForeColor = AppStyle.Teal;
            var giaMacDinh = _spHienTai.BangGias.OrderBy(bg => bg.HieuLucTu).FirstOrDefault(bg => bg.LoaiGia == "MacDinh");
            txtGiaUuTien.Text = giaMacDinh != null ? string.Format("{0:N0} VNĐ", giaMacDinh.GiaBan) : LanguageManager.GetString("TXT_CHUA_CO");

            // Tab 2: Bảng giá — BindingList để inline-edit không bị mất dữ liệu
            gcBangGia.DataSource = new BindingList<ET_BangGia>(_spHienTai.BangGias.ToList());

            // Tab 3: Quy đổi ĐVT
            gcQuyDoi.DataSource = new BindingList<ET_QuyDoiDonVi>(_bus.LayQuyDoiTheoSanPham(_spHienTai.Id));

            // Tab 4: Kích hoạt panel động
            DoiPanelCauHinh(_spHienTai.LoaiSanPham);

            _daThayDoi = false;
            _dangTaiDuLieu = false;
        }

        public void ChuanBiThemMoi()
        {
            _dangTaiDuLieu = true;
            _idSanPham = null;
            _spHienTai = new ET_SanPham();
            
            txtMaSP.Text = "";
            txtTenSP.Text = "";
            cboLoaiSP.EditValue = null;
            slkDonViTinh.EditValue = null;
            cboTrangThai.EditValue = AppConstants.TrangThaiSanPham.DangBan;
            chkLaVatTu.Checked = false;
            chkQuanLyLo.Checked = false;
            txtGiaUuTien.Text = LanguageManager.GetString("TXT_CHUA_CO");

            // Reset Ảnh
            if (picAnhDaiDien.Image != null)
            {
                var oldImg = picAnhDaiDien.Image;
                picAnhDaiDien.Image = null;
                oldImg.Dispose();
            }

            gcBangGia.DataSource = new BindingList<ET_BangGia>();
            gcQuyDoi.DataSource = new BindingList<ET_QuyDoiDonVi>();
            DoiPanelCauHinh(null);

            // Xoá toàn bộ báo lỗi đỏ (nếu có) từ phiên trước
            dxValProvider.ClearErrors();

            SetReadOnlyFormat(cboLoaiSP, false);
            SetReadOnlyFormat(txtMaSP, false);
            SetReadOnlyFormat(txtGiaUuTien, true);
            txtGiaUuTien.Properties.Appearance.ForeColor = AppStyle.Teal;

            _daThayDoi = false;
            _dangTaiDuLieu = false;
        }

        private void SetReadOnlyFormat(DevExpress.XtraEditors.BaseEdit control, bool readOnly)
        {
            control.Properties.ReadOnly = readOnly;
            control.Properties.Appearance.BackColor = readOnly ? Color.WhiteSmoke : Color.White;
            if (readOnly && control != txtGiaUuTien) 
                control.Properties.Appearance.ForeColor = Color.DimGray;
            else if (!readOnly)
                control.Properties.Appearance.ForeColor = Color.Black;
        }

        #endregion

        #region Xử lý sự kiện

        /// Khi đổi Loại SP: hoán đổi UserControl ở Tab 4 (Cấu hình vận hành).
        /// Đồng thời bật/tắt cột phụ thu trong Grid Bảng giá cho nhóm Cho Thuê.
        private void cboLoaiSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            string loai = cboLoaiSP.EditValue?.ToString();
            DoiPanelCauHinh(loai);
            BatTatCotChoThue(loai);
        }

        private void chkAllPOS_CheckedChanged(object sender, EventArgs e)
        {
            // Checkbox "Áp dụng toàn hệ thống" -> khoá xám danh sách Điểm bán
            chkComboDiemBan.Enabled = !chkAllPOS.Checked;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            var kq = LuuSanPham();

            // Móc từ điển dịch mã lỗi/Thành công từ tằng BUS bắn lên
            string[] parts = kq.Message?.Split('|') ?? new string[0];
            string msg = parts.Length > 0 ? (LanguageManager.GetString(parts[0]) ?? parts[0]) : kq.Message;

            // Ráp tham số (VD: Tên mã SP bị trùng) vào Message nếu có
            if (parts.Length > 1) 
            {
                try { msg = string.Format(msg, parts.Skip(1).ToArray()); } catch { }
            }

            if (kq.Success)
            {
                UIHelper.ThongBao(msg);
                
                string action = _idSanPham.HasValue ? "Sua" : "ThemMoi";
                int id = kq.Data != null ? (int)kq.Data : (_idSanPham ?? 0);
                string loaiSpStr = cboLoaiSP.EditValue?.ToString();
                string maSp = txtMaSP.Text?.Trim();
                _nhatKy.GhiLog("SanPham", id, action, SessionManager.IdDoiTac, null, maSp + " | " + loaiSpStr);
                
                _daThayDoi = false;
                EventBus.Publish("SanPham_DaLuu", null);
            }
            else
            {
                UIHelper.Loi(msg);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (_daThayDoi)
            {
                if (!UIHelper.XacNhan(LanguageManager.GetString("MSG_DIRTY_CONFIRM") ?? "Bạn có thay đổi chưa lưu. Bạn có chắc chắn muốn hủy bỏ các thay đổi này không?"))
                {
                    return;
                }
            }
            if (_idSanPham.HasValue) TaiDuLieuChinhSua(_idSanPham.Value);
            else ChuanBiThemMoi();
            _daThayDoi = false;
        }

        #endregion

        #region Nghiệp vụ lưu dữ liệu

        /// <summary>
        /// Thu thập dữ liệu từ tất cả Tab, gọi BUS lưu xuống DB.
        /// Luồng: Thu Tab 1 -> Thu Tab 2 -> Thu Tab 3 -> Thu Tab 4 -> Gọi BUS.
        /// </summary>
        private ET.Results.OperationResult LuuSanPham()
        {
            // Bắt buộc đẩy dữ liệu đang nhập dở vào DataSource
            gvBangGia.PostEditor();
            gvBangGia.UpdateCurrentRow();
            gvQuyDoi.PostEditor();
            gvQuyDoi.UpdateCurrentRow();

            // Thu Tab 1: Thông tin chung
            var sp = _spHienTai ?? new ET_SanPham();
            sp.MaSanPham = txtMaSP.Text?.Trim();
            sp.TenSanPham = txtTenSP.Text?.Trim();
            sp.LoaiSanPham = cboLoaiSP.EditValue?.ToString();
            if (int.TryParse(slkDonViTinh.EditValue?.ToString(), out int idDvt))
                sp.IdDonViTinh = idDvt;
            else
                sp.IdDonViTinh = 0;
            sp.TrangThai = cboTrangThai.EditValue?.ToString();
            sp.LaVatTu = chkLaVatTu.Checked;
            sp.CanQuanLyLo = chkQuanLyLo.Checked;

            // Thu Tab 2: Bảng giá
            var blBangGia = gcBangGia.DataSource as BindingList<ET_BangGia>;
            var dsBangGia = blBangGia != null ? blBangGia.ToList() : new List<ET_BangGia>();

            if (sp.TrangThai != AppConstants.TrangThaiSanPham.TamNgung 
                && sp.LoaiSanPham != ET.Constants.AppConstants.LoaiSanPham.NguyenLieu 
                && (dsBangGia.Count == 0 || !dsBangGia.Any(bg => bg.GiaBan >= 0)))
            {
                string msg = GUI.Infrastructure.LanguageManager.GetString("ERR_SP_MISSING_PRICE") 
                    ?? "Sản phẩm chưa có bảng giá để bán! Hệ thống sẽ tự động chuyển trạng thái về 'Tạm ngừng'. Vui lòng bổ sung bảng giá nếu muốn đưa sản phẩm vào kinh doanh.";
                GUI.Infrastructure.UIHelper.CanhBao(msg);

                sp.TrangThai = AppConstants.TrangThaiSanPham.TamNgung;
                cboTrangThai.EditValue = AppConstants.TrangThaiSanPham.TamNgung;
            }

            // Thu Tab 3: Quy đổi ĐVT (BindingList -> List)
            var blQuyDoi = gcQuyDoi.DataSource as BindingList<ET_QuyDoiDonVi>;
            var dsQuyDoi = blQuyDoi != null ? blQuyDoi.ToList() : new List<ET_QuyDoiDonVi>();

            // Thu Tab 4: Cấu hình theo loại
            ET_SanPham_Ve cauHinhVe = null;
            List<ET_Ve_QuyenTruyCap> dsQuyenVe = null;
            ET_MonAn cauHinhMonAn = null;
            List<ET_DinhMucNguyenLieu> dsDinhMuc = null;
            ET_LoaiPhong loaiPhong = null;
            List<ET.Models.VanHanh.ET_VatTuPhongMacDinh> dsVatTu = null;
            List<ET_TaiSanChoThue> dsTaiSan = null;

            if (_ucCauHinhHienTai is ucCauHinhVe ucVe)
            {
                cauHinhVe = ucVe.LaySanPhamVe();
                dsQuyenVe = ucVe.LayQuyenTruyCap();
            }
            else if (_ucCauHinhHienTai is ucCauHinhFnB ucFnb)
            {
                cauHinhMonAn = ucFnb.LayMonAn();
                dsDinhMuc = ucFnb.LayDinhMuc();

                if (!sp.LaVatTu && cauHinhMonAn != null && cauHinhMonAn.IdNhaHang <= 0)
                {
                    string msg = sp.LoaiSanPham == AppConstants.LoaiSanPham.DoUong
                        ? (LanguageManager.GetString("ERR_MISSING_BAR") ?? "Vui lòng chọn Bar / Quầy nước phụ trách!")
                        : (LanguageManager.GetString("ERR_MISSING_KITCHEN") ?? "Vui lòng chọn Bếp phụ trách!");
                    return new ET.Results.OperationResult { Success = false, Message = msg };
                }

                // Bắt lỗi CỨNG nếu F&B chế biến không có định mức nguyên liệu
                if (!sp.LaVatTu && (dsDinhMuc == null || dsDinhMuc.Count == 0))
                {
                    string msg = GUI.Infrastructure.LanguageManager.GetString("ERR_SP_FNB_MISSING_BOM") ?? "BẮT BUỘC KHAI BÁO NGUYÊN LIỆU!\n\nSản phẩm pha chế/chế biến phải có ít nhất 1 nguyên liệu định mức để trừ kho khi bán.\nVui lòng thêm nguyên liệu hoặc chuyển sang loại 'Đồ Uống Đóng Chai / Đồ Ăn Tiện Lợi'.";
                    return new ET.Results.OperationResult { Success = false, Message = msg };
                }
            }
            else if (_ucCauHinhHienTai is ucCauHinhLuuTru ucLuuTru)
            {
                loaiPhong = ucLuuTru.LayDuLieuLoaiPhong();
                dsVatTu = ucLuuTru.LayDuLieuVatTu();
            }
            else if (_ucCauHinhHienTai is ucCauHinhChoThue ucChoThue)
            {
                dsTaiSan = ucChoThue.LayDuLieuTaiSan();
            }

            if (_idSanPham.HasValue)
                return _bus.CapNhat(sp, dsBangGia, dsQuyDoi, dsQuyenVe, cauHinhVe, cauHinhMonAn, dsDinhMuc, loaiPhong, dsVatTu, dsTaiSan);
            else
                return _bus.ThemMoi(sp, dsBangGia, dsQuyDoi, dsQuyenVe, cauHinhVe, cauHinhMonAn, dsDinhMuc, loaiPhong, dsVatTu, dsTaiSan);
        }

        public bool GoiHamLuuTuXa()
        {
            btnLuu_Click(this, EventArgs.Empty);
            return !_daThayDoi;
        }

        #endregion

        #region Hàm hỗ trợ

        /// <summary>
        /// Hoán đổi UserControl trên Tab 4 tuỳ loại sản phẩm.
        /// pnlDynamic.Controls.Clear() -> thêm UC mới -> Dock Fill.
        /// Nếu loại không cần cấu hình riêng, hiển thị nhãn "Không có cấu hình".
        /// </summary>
        /// <param name="loaiSP">Giá trị AppConstants.LoaiSanPham từ AppConstants</param>
        private void DoiPanelCauHinh(string loaiSP)
        {
            pnlDynamic.Controls.Clear();
            _ucCauHinhHienTai = null;

            bool laVe = loaiSP == AppConstants.LoaiSanPham.VeVaoKhu || loaiSP == AppConstants.LoaiSanPham.VeTroChoi;
            bool laFnB = (loaiSP == AppConstants.LoaiSanPham.AnUong || loaiSP == AppConstants.LoaiSanPham.DoUong) && !chkLaVatTu.Checked;
            bool laLuuTru = loaiSP == AppConstants.LoaiSanPham.LuuTru;
            bool laChoThue = loaiSP == AppConstants.LoaiSanPham.TuDo || loaiSP == AppConstants.LoaiSanPham.ChoiNghiMat || loaiSP == AppConstants.LoaiSanPham.DoChoThue;

            if (laVe)
            {
                var uc = new ucCauHinhVe();
                if (_spHienTai != null) uc.NapDuLieu(_spHienTai);
                uc.Dock = DockStyle.Fill;
                pnlDynamic.Controls.Add(uc);
                TheoDoiThayDoi(uc.Controls);
                _ucCauHinhHienTai = uc;
            }
            else if (laFnB)
            {
                var uc = new ucCauHinhFnB();
                if (_spHienTai != null) uc.NapDuLieu(_spHienTai);
                uc.SetVatTuMode(chkLaVatTu.Checked);
                uc.Dock = DockStyle.Fill;
                pnlDynamic.Controls.Add(uc);
                TheoDoiThayDoi(uc.Controls);
                _ucCauHinhHienTai = uc;
            }
            else if (laLuuTru)
            {
                var uc = new ucCauHinhLuuTru();
                if (_spHienTai != null && _idSanPham.HasValue)
                {
                    // Lấy dữ liệu từ DAL
                    var lp = DAL_LoaiPhong.Instance.LayChiTiet(_idSanPham.Value);
                    var vt = DAL.Repositories.VanHanh.DAL_VatTuPhongMacDinh.Instance.LayDanhSach(_idSanPham.Value);
                    uc.LoadData(lp, vt);
                }
                else
                {
                    uc.ClearData();
                }
                uc.Dock = DockStyle.Fill;
                pnlDynamic.Controls.Add(uc);
                TheoDoiThayDoi(uc.Controls);
                _ucCauHinhHienTai = uc;
            }
            else if (laChoThue)
            {
                var uc = new ucCauHinhChoThue();
                if (_idSanPham.HasValue)
                {
                    uc.ResetLoadState(_idSanPham.Value);
                }
                uc.Dock = DockStyle.Fill;
                pnlDynamic.Controls.Add(uc);
                TheoDoiThayDoi(uc.Controls);
                _ucCauHinhHienTai = uc;
            }
            else
            {
                var lbl = new LabelControl
                {
                    Text = LanguageManager.GetString("TXT_KHONG_CAU_HINH") ?? "Sản phẩm này không có cấu hình",
                    Dock = DockStyle.Fill,
                    Appearance = { ForeColor = AppStyle.TextMuted, TextOptions = { HAlignment = DevExpress.Utils.HorzAlignment.Center } }
                };
                pnlDynamic.Controls.Add(lbl);
            }
        }

        /// <summary>
        /// Bật/tắt các cột phụ thu thuê đồ trong Grid Bảng giá.
        /// Chỉ hiện khi loại SP là Cho Thuê (Tủ đồ, Đồ cho, Chòi nghỉ).
        /// </summary>
        private void BatTatCotChoThue(string loaiSP)
        {
            bool laChoThue = loaiSP == AppConstants.LoaiSanPham.TuDo || loaiSP == AppConstants.LoaiSanPham.DoChoThue || loaiSP == AppConstants.LoaiSanPham.ChoiNghiMat;

            var tenCotThue = new[] { "TienCoc", "PhutBlock", "PhutTiep", "GiaPhuThu" };
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in gvBangGia.Columns)
            {
                if (tenCotThue.Contains(col.FieldName))
                    col.Visible = laChoThue;
            }
        }

        private readonly HashSet<object> _dongDaSua = new HashSet<object>();

        /// <summary>
        /// Cấu hình grid cho phép inline edit + tự động lưu giá trị khi rời cell.
        /// Đồng thời highlight các dòng đã sửa (màu vàng nhạt).
        /// PHẢI gọi SAU khi đã thêm cột, TRƯỚC khi gán DataSource.
        /// </summary>
        private void CauHinhGridChinhSua(GridView gv)
        {
            gv.OptionsBehavior.Editable = true;
            gv.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            gv.ValidateRow += GvChinhSua_ValidateRow;
            gv.InvalidRowException += GvChinhSua_InvalidRowException;
            gv.CellValueChanged += GvChinhSua_CellValueChanged;
            gv.RowCellStyle += GvChinhSua_RowCellStyle;
        }

        private void GvChinhSua_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            e.Valid = true;
        }

        private void GvChinhSua_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void GvChinhSua_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (sender is GridView gv)
            {
                var obj = gv.GetRow(e.RowHandle);
                if (obj != null) _dongDaSua.Add(obj);

                _daThayDoi = true;

                gv.PostEditor();
                gv.UpdateCurrentRow();
            }
        }

        private void GvChinhSua_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (sender is GridView gv)
            {
                var obj = gv.GetRow(e.RowHandle);
                if (obj != null && _dongDaSua.Contains(obj))
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 255, 230); 
                    e.Appearance.Options.UseBackColor = true;
                }
            }
        }

        /// <summary>
        /// Thêm cột hành động cuối dòng: nút [Xoá] (icon ).
        /// </summary>
        private void ThemCotXoa(DevExpress.XtraGrid.GridControl gc, GridView gv)
        {
            var repoBtn = new RepositoryItemButtonEdit();
            repoBtn.TextEditStyle = TextEditStyles.HideTextEditor;
            repoBtn.Buttons.Clear();
            repoBtn.Buttons.Add(new EditorButton(ButtonPredefines.Delete));
            
            // Xóa hoàn toàn Lambda ẩn danh
            repoBtn.ButtonClick += RepoBtnXoa_ButtonClick;
            
            gc.RepositoryItems.Add(repoBtn);

            var col = gv.Columns["_colXoa"];
            if (col == null)
            {
                col = gv.Columns.AddVisible("_colXoa", " ");
            }
            col.ColumnEdit = repoBtn;
            col.Width = 40;
            col.MaxWidth = 40;
            col.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            col.OptionsFilter.AllowFilter = false;
            col.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            col.OptionsColumn.AllowEdit = true;
            col.UnboundType = DevExpress.Data.UnboundColumnType.Object;
        }

        private void RepoBtnXoa_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (sender is DevExpress.XtraEditors.ButtonEdit btn && btn.Parent is DevExpress.XtraGrid.GridControl gc && gc.MainView is GridView gv)
            {
                if (gv.FocusedRowHandle >= 0)
                {
                    gv.DeleteRow(gv.FocusedRowHandle);
                    _daThayDoi = true;
                }
            }
        }

        /// <summary>
        /// Thêm nút Thêm dòng bên dưới Grid.
        /// Bấm -> tạo object mới -> add vào BindingList -> grid vẽ dòng mới -> focus vào dòng đó.
        /// </summary>
        private void ThemNutThemDong(DevExpress.XtraGrid.GridControl gc)
        {
            var btn = new SimpleButton();
            btn.Text = LanguageManager.GetString("BTN_THEM_DONG");
            btn.Height = 28;
            btn.Dock = DockStyle.Bottom;
            btn.Appearance.BackColor = AppStyle.Teal;
            btn.Appearance.ForeColor = Color.White;
            btn.Appearance.Options.UseBackColor = true;
            btn.Appearance.Options.UseForeColor = true;
            
            btn.Click += BtnThemDong_Click;

            gc.Parent.Controls.Add(btn);
            btn.BringToFront();
        }

        private void BtnThemDong_Click(object sender, EventArgs e)
        {
            var btn = sender as SimpleButton;
            if (btn?.Parent == null) return;
            
            DevExpress.XtraGrid.GridControl gc = null;
            foreach (Control c in btn.Parent.Controls)
            {
                if (c is DevExpress.XtraGrid.GridControl g) { gc = g; break; }
            }
            if (gc == null || !(gc.MainView is GridView gv)) return;

            if (gc == gcBangGia)
            {
                var bl = gc.DataSource as BindingList<ET_BangGia>;
                if (bl != null)
                {
                    bl.Add(new ET_BangGia
                    {
                        LoaiGia = AppConstants.LoaiGiaBan.MacDinh,
                        HieuLucTu = DateTime.Today,
                        HieuLucDen = DateTime.Today.AddYears(1),
                        TrangThai = AppConstants.TrangThaiHieuLuc.HoatDong
                    });
                }
            }
            else if (gc == gcQuyDoi)
            {
                var bl = gc.DataSource as BindingList<ET_QuyDoiDonVi>;
                if (bl != null)
                {
                    bl.Add(new ET_QuyDoiDonVi());
                }
            }
            
            _daThayDoi = true;
            gv.RefreshData();
            gv.FocusedRowHandle = gv.RowCount - 1;
            if (gv.VisibleColumns.Count > 0)
                gv.FocusedColumn = gv.VisibleColumns[0];
            gv.ShowEditor();
        }

        #endregion
    }
}

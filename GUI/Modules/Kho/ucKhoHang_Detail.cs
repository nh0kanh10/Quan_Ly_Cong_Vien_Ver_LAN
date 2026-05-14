using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BUS.Services.Kho;
using BUS.Services.HeThong;
using ET.Constants;
using ET.Models.Kho;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GUI.Infrastructure;

namespace GUI.Modules.Kho
{
    /// <summary>
    /// Panel chi tiết kho hàng (nhúng bên phải SplitContainer của frmKhoHang).
    /// Nhập/sửa thông tin 1 kho: Mã, Tên, Khu vực, Kho ảo, Tồn âm, Trạng thái.
    /// </summary>
    public partial class ucKhoHang_Detail : XtraUserControl
    {
        private readonly BUS_Kho _bus = BUS_Kho.Instance;
        private readonly BUS_NhatKy _nhatKy = BUS_NhatKy.Instance;
        private int? _idDangSua;
        public bool DaThayDoi { get; set; }

        /// Sự kiện bắn lên cho frmKhoHang biết vừa lưu xong để nạp lại grid.
        public event EventHandler DaLuuXong;

        #region Khởi tạo và tải dữ liệu

        public ucKhoHang_Detail()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gọi sau khi control được add vào form cha.
        /// Dùng để style, nạp dữ liệu tra cứu, và dịch đa ngôn ngữ.
        /// </summary>
        public void KhoiTao()
        {
            AppStyle.StyleBtnPrimary(btnLuu);
            AppStyle.StyleBtnOutline(btnHuy, AppStyle.Danger);
            AppStyle.StyleLayoutControl(layoutControl);
            pnlFooter.BackColor = AppStyle.BgCard;

            NapComboTrangThai();
            NapDuLieuKhuVuc();
            ThucHienDichNgonNgu();
            AppStyle.FixEditorForeColor(this);

        }

        private void control_EditValueChanged(object sender, EventArgs e)
        {
            DaThayDoi = true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F2 && this.Visible)
            {
                btnLuu.PerformClick();
                return true;
            }
            if (keyData == Keys.Escape && this.Visible)
            {
                btnHuy.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void NapComboTrangThai()
        {
            cboTrangThai.Properties.Items.Clear();

            var dsTuDien = BUS.Services.HeThong.BUS_TuDien.Instance.LayDanhSachNhom("KHO_TRANG_THAI");
            if (dsTuDien != null && dsTuDien.Count > 0)
            {
                foreach (var td in dsTuDien.OrderBy(x => x.ThuTu))
                {
                    // Ưu tiên nạp đa ngôn ngữ theo mã từ điển, nếu không có thì lấy chuỗi gốc NhanHienThi.
                    string localizedStr = LanguageManager.GetString("TRANGTHAI_" + td.Ma.ToUpper());
                    string display = localizedStr.StartsWith("TRANGTHAI_") ? td.NhanHienThi : localizedStr;
                    cboTrangThai.Properties.Items.Add(new ImageComboBoxItem(display, td.Ma, -1));
                }
            }
            else
            {
                // Fallback nếu CSDL chưa Insert TuDien
                cboTrangThai.Properties.Items.Add(new ImageComboBoxItem(
                    LanguageManager.GetString("TRANGTHAI_HOATDONG"), AppConstants.TrangThaiKho.HoatDong, -1));
                cboTrangThai.Properties.Items.Add(new ImageComboBoxItem(
                    LanguageManager.GetString("TRANGTHAI_NGUNGHOATDONG"), AppConstants.TrangThaiKho.NgungHoatDong, -1));
            }
            
            cboTrangThai.SelectedIndex = 0;
        }

        private void NapDuLieuKhuVuc()
        {
            try
            {
                var dsKhuVuc = BUS.Services.DanhMuc.BUS_KhuVuc.Instance.LayDanhSach(SessionManager.CurrentLanguage);
                slkKhuVuc.Properties.DataSource = dsKhuVuc;
                slkKhuVuc.Properties.DisplayMember = "TenKhuVuc";
                slkKhuVuc.Properties.ValueMember = "Id";
                slkKhuVuc.Properties.NullText = LanguageManager.GetString("PROMPT_CHON");
                slkKhuVuc.Properties.View.Columns.Clear();
                slkKhuVuc.Properties.View.Columns.AddVisible("MaKhuVuc", LanguageManager.GetString("COL_MA_KHU_VUC"));
                slkKhuVuc.Properties.View.Columns.AddVisible("TenKhuVuc", LanguageManager.GetString("COL_TEN_KHU_VUC"));
                slkKhuVuc.Properties.View.BestFitColumns();
            }
            catch
            {
            }
        }

        #endregion

        #region Xử lý sự kiện

        public void TaiDuLieuChinhSua(ET_KhoHang kho)
        {
            if (kho == null) return;

            _idDangSua = kho.Id;
            txtMaKho.Text = kho.MaKho;
            txtTenKho.Text = kho.TenKho;
            slkKhuVuc.EditValue = kho.IdKhuVuc;
            chkTonAm.Checked = kho.ChoPhepTonAm;
            cboTrangThai.EditValue = kho.TrangThai;

            chkTonAm.Enabled = true;
            slkKhuVuc.Enabled = true;

            DaThayDoi = false;
        }

        public void XoaTrangThemMoi()
        {
            _idDangSua = null;
            txtMaKho.Text = "";
            txtTenKho.Text = "";
            slkKhuVuc.EditValue = null;

            chkTonAm.Checked = false;
            cboTrangThai.EditValue = AppConstants.TrangThaiKho.HoatDong;
            chkTonAm.Enabled = true;
            slkKhuVuc.Enabled = true;
            DaThayDoi = false;
            txtMaKho.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            bool isAdd = (_idDangSua == null || _idDangSua == 0);
            var kho = new ET_KhoHang
            {
                Id = _idDangSua ?? 0,
                MaKho = txtMaKho.Text?.Trim(),
                TenKho = txtTenKho.Text?.Trim(),
                IdKhuVuc = slkKhuVuc.EditValue as int?,
                LaKhoAo = false, 
                ChoPhepTonAm = chkTonAm.Checked,
                TrangThai = cboTrangThai.EditValue?.ToString() ?? AppConstants.TrangThaiKho.HoatDong
            };

            var kq = _bus.CapNhatKho(kho);

            string[] parts = kq.Message?.Split('|') ?? new string[0];
            string msg = parts.Length > 0 ? LanguageManager.GetString(parts[0]) : kq.Message;
            if (parts.Length > 1)
            {
                try { msg = string.Format(msg, parts[1]); } catch { }
            }

            if (kq.Success)
            {
                UIHelper.ThongBao(msg);
                
                string action = isAdd ? "ThemMoi" : "Sua";
                _nhatKy.GhiLog("KhoHang", kho.Id, action, SessionManager.IdDoiTac, null, kho.MaKho + " | " + kho.TenKho);
                
                DaThayDoi = false;
                DaLuuXong?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                UIHelper.Loi(msg);
            }

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (DaThayDoi && !UIHelper.XacNhanHuy()) return;
            this.Visible = false;
            DaThayDoi = false;
        }

        #endregion

        #region Hàm hỗ trợ

        public void ThucHienDichNgonNgu()
        {
            lciTenKho.Text = LanguageManager.GetString("LBL_TEN_KHO");
            lciKhuVuc.Text = LanguageManager.GetString("LBL_KHU_VUC");
            lciTrangThai.Text = LanguageManager.GetString("LBL_TRANG_THAI_KHO");

            chkKhoAo.Visible = false; 
            chkTonAm.Text = LanguageManager.GetString("CHK_TON_AM");
            btnLuu.Text = LanguageManager.GetString("BTN_LUU");
            btnHuy.Text = LanguageManager.GetString("BTN_HUY");

            chkTonAm.ToolTipTitle = LanguageManager.GetString("TOOLTIP_TONAM_TITLE");
            chkTonAm.ToolTip = LanguageManager.GetString("TOOLTIP_TONAM_DESC");
            chkTonAm.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Warning;

            NapComboTrangThai();
        }

        #endregion
    }
}

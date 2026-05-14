using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BUS.Services.BanHang;
using ET.Models.BanHang;
using GUI.Infrastructure;

namespace GUI.Modules.BanHang
{
    // Màn hình tổng "Quản lý Thuê Đồ" gộp 2 tab: Giao Đồ + Nhận Trả.
    // Quản lý Phiên Thu Ngân (Mở/Đóng ca) cho toàn bộ giao dịch thuê đồ.
    public partial class ucQuanLyThueDo : DevExpress.XtraEditors.XtraUserControl
    {
        #region Khởi tạo và tải dữ liệu

        private ET_PhienThuNgan _phienHienTai;
        private ucGiaoDo _ucGiaoDo;
        private ucNhanTra _ucNhanTra;
        private readonly Action<object> _onLanguageChanged;

        public ucQuanLyThueDo()
        {
            InitializeComponent();

            _onLanguageChanged = _ =>
            {
                if (this.IsHandleCreated && !this.IsDisposed)
                    this.Invoke((MethodInvoker)delegate { ApplyLanguage(); });
            };
            EventBus.Subscribe("LanguageChanged", _onLanguageChanged);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;

            AppStyle.StyleForm(this);
            AppStyle.StyleBanner(pnlBanner, lblTitle);
            ApplyLanguage();

            // Mở phiên thu ngân (bắt buộc trước khi thao tác)
            MoPhienThuNgan();

            if (_phienHienTai != null)
            {
                NhungTabCon();
                PhanQuyenTab();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_onLanguageChanged != null)
                    EventBus.Unsubscribe("LanguageChanged", _onLanguageChanged);
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Nhúng 2 UserControl con (ucGiaoDo, ucNhanTra) vào tab tương ứng.
        /// Truyền IdPhienThuNgan xuống cho cả 2 UC để gắn vào giao dịch.
        /// </summary>
        private void NhungTabCon()
        {
            int? idDiemBan = BUS_DiemBanHang.Instance.GetIdDiemBanByMa(_phienHienTai.IdMayBan);

            _ucGiaoDo = new ucGiaoDo();
            _ucGiaoDo.Dock = DockStyle.Fill;
            _ucGiaoDo.IdPhienThuNgan = _phienHienTai.Id;
            _ucGiaoDo.IdDiemBan = idDiemBan ?? 0;
            tabGiaoDo.Controls.Add(_ucGiaoDo);

            _ucNhanTra = new ucNhanTra();
            _ucNhanTra.Dock = DockStyle.Fill;
            _ucNhanTra.IdPhienThuNgan = _phienHienTai.Id;
            _ucNhanTra.IdDiemBan = idDiemBan ?? 0;
            tabNhanTra.Controls.Add(_ucNhanTra);
        }

        // Ẩn tab nếu nhân viên không có quyền tương ứng.
        // Hiển thị nếu có ít nhất 1 trong 2 quyền (menu đã check ở frmMain).
        private void PhanQuyenTab()
        {
            tabGiaoDo.PageVisible = SessionManager.CoQuyen("THUE_DO");
            tabNhanTra.PageVisible = SessionManager.CoQuyen("THUE_DO");
        }

        #endregion

        #region Xử lý sự kiện (Click, SelectedChanged...)

        private void BtnDongPhien_Click(object sender, EventArgs e)
        {
            if (_phienHienTai == null)
            {
                MoPhienThuNgan();
                if (_phienHienTai != null)
                {
                    NhungTabCon();
                    PhanQuyenTab();
                }
            }
            else
            {
                DongPhienThuNgan();
            }
        }

        // Phím tắt: F8 = Đóng phiên thu ngân
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F8)
            {
                DongPhienThuNgan();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        #region Hàm hỗ trợ

        // Kiểm tra nhân viên đã mở ca chưa. Nếu chưa -> mở dialog nhập tiền đầu ca.
        // Kế thừa logic từ ucPOS.MoPhienThuNgan().
        private void MoPhienThuNgan()
        {
            var resultPhien = BUS_PhienThuNgan.Instance.LayPhienDangMo(SessionManager.IdDoiTac);
            if (resultPhien.Success && resultPhien.Data != null)
            {
                _phienHienTai = resultPhien.Data as ET_PhienThuNgan;
                SessionManager.IdPhienGiaoDich = _phienHienTai.Id;
                CapNhatThongTinPhien();
                return;
            }

            // Chưa có phiên -> Mở dialog
            if (UIHelper.XacNhan(LanguageManager.GetString("ERR_CHUA_MO_PHIEN") ?? "Bạn chưa mở phiên thu ngân. Vui lòng mở phiên trước khi thao tác."))
            {
                using (var frm = new frmPhienThuNgan(null, true))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        _phienHienTai = frm.PhienKetQua;
                        SessionManager.IdPhienGiaoDich = _phienHienTai.Id;
                        CapNhatThongTinPhien();
                    }
                }
            }
        }

        // Đóng phiên thu ngân. Nhân viên nhập tiền cuối ca -> Hệ thống so sánh chênh lệch.
        private void DongPhienThuNgan()
        {
            if (_phienHienTai == null) return;

            using (var frm = new frmPhienThuNgan(_phienHienTai, false))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    _phienHienTai = null;
                    SessionManager.IdPhienGiaoDich = 0;
                    CapNhatThongTinPhien();

                    XtraMessageBox.Show(
                        LanguageManager.GetString("MSG_PHIEN_DONG_OK") ?? "Đã đóng phiên thu ngân.",
                        "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // Hiển thị thông tin phiên trên banner: "Ca: 001 | Mở lúc 08:00"
        private void CapNhatThongTinPhien()
        {
            if (_phienHienTai != null)
            {
                lblThongTinPhien.Text = $"Ca #{_phienHienTai.Id} | Mở: {_phienHienTai.ThoiGianMo:HH:mm dd/MM}";
                btnDongPhien.Text = LanguageManager.GetString("POS_BTN_CLOSE_SHIFT") ?? "Đóng Phiên (F8)";
                btnDongPhien.Enabled = true;
            }
            else
            {
                lblThongTinPhien.Text = LanguageManager.GetString("LBL_CHUA_MO_PHIEN") ?? "Chưa mở ca";
                btnDongPhien.Text = LanguageManager.GetString("BTN_MO_PHIEN") ?? "Mở Phiên";
                btnDongPhien.Enabled = true;
            }
        }

        private void ApplyLanguage()
        {
            lblTitle.Text = LanguageManager.GetString("RENTAL_MGR_TITLE") ?? "QUẢN LÝ THUÊ ĐỒ";
            btnDongPhien.Text = LanguageManager.GetString("POS_BTN_CLOSE_SHIFT") ?? "Đóng Phiên (F8)";
            tabGiaoDo.Text = $"  {LanguageManager.GetString("RENTAL_TAB_GIAO") ?? "Giao Đồ (Cho Thuê)"}  ";
            tabNhanTra.Text = $"  {LanguageManager.GetString("RENTAL_TAB_TRA") ?? "Nhận Trả Đồ & Hoàn Cọc"}  ";
        }





        #endregion

        
    }
}

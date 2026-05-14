using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils.Menu;
using ET.Constants;
using GUI.AI;
using GUI.Infrastructure;

namespace GUI.Modules.Kho
{
    public partial class frmTrungTamKho : XtraUserControl, IAIModuleContext
    {
        // Các UC rỗng định nghĩa sẵn
        private ucTaoPhieu _ucTaoPhieu;
        private ucTonKho _ucTonKho;
        private ucLichSu _ucLichSu;
        private ucCanhBao _ucCanhBao;

        public frmTrungTamKho()
        {
            InitializeComponent();
        }

        private void frmTrungTamKho_Load(object sender, EventArgs e)
        {
            try
            {
                btnTaoPhieu.Text = LanguageManager.GetString("BTN_KHO_TAOPHIEU") ?? "TẠO PHIẾU";
                btnTonKho.Text = LanguageManager.GetString("BTN_KHO_TONKHO") ?? "TỒN KHO";
                btnLichSu.Text = LanguageManager.GetString("BTN_KHO_LICHSU") ?? "LỊCH SỬ G.DỊCH";
                btnCanhBao.Text = LanguageManager.GetString("BTN_KHO_CANHBAO") ?? "CẢNH BÁO";
                lblListPhieu.Text = LanguageManager.GetString("LBL_KHO_DSPHIEU") ?? "DS PHIẾU GẦN ĐÂY";

                LoadAlertCounts();

                AppStyle.FixEditorForeColor(this);
                AppStyle.StyleBtnPrimary(btnCanhBao);

                lstPhieuGanDau.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;

                AppStyle.StyleBanner(pnlAlert, lblAlertTon);

                AppStyle.StyleBtnPrimary(btnDuyetPhieu);
                AppStyle.StyleBtnDanger(btnHuyPhieu);

                EventBus.Subscribe("LanguageChanged", _onLanguageChanged);

                SetupListPhieuContextMenu();
                LoadDSPhieuGanDay();
                SetupAlertClickHandlers();
                
                // Mặc định load form tạo phiếu
                LoadUserControl(typeof(ucTaoPhieu));

                this.HandleDestroyed += FrmTrungTamKho_HandleDestroyed;
            }
            catch (Exception ex)
            {
                UIHelper.Loi(ex.Message);
            }
        }

        private void _onLanguageChanged(object obj)
        {
            ThucHienDichNgonNgu();
        }

        public void ThucHienDichNgonNgu()
        {
            btnTaoPhieu.Text = LanguageManager.GetString("MNU_TAOPHIEU");
            btnTonKho.Text = LanguageManager.GetString("MNU_TONKHO");
            btnLichSu.Text = LanguageManager.GetString("MNU_LICHSU");
            btnCanhBao.Text = LanguageManager.GetString("MNU_CANHBAO");
            lblListPhieu.Text = LanguageManager.GetString("LBL_KHO_DSPHIEU") ?? "DS PHIẾU GẦN ĐÂY";

            if (pnlContent.Controls.Count > 0)
            {
                var activeControl = pnlContent.Controls[0];
                var method = activeControl.GetType().GetMethod("ThucHienDichNgonNgu");
                if (method != null) method.Invoke(activeControl, null);
            }
        }

        private void SetupListPhieuContextMenu()
        {
            lstPhieuGanDau.MouseUp += LstPhieuGanDau_MouseUp;
            lstPhieuGanDau.DoubleClick += LstPhieuGanDau_DoubleClick;
        }

        private void FrmTrungTamKho_HandleDestroyed(object sender, EventArgs e)
        {
            EventBus.Unsubscribe("LanguageChanged", _onLanguageChanged);
        }

        private void LstPhieuGanDau_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) 
            {
                var menu = new DXPopupMenu();
                
                DXMenuItem itemDuyet = new DXMenuItem(LanguageManager.GetString("MNU_PHEDUYET") ?? "Phê duyệt ghi sổ", ItemDuyet_Click);
                DXMenuItem itemHuy = new DXMenuItem(LanguageManager.GetString("MNU_HUYPHIEU") ?? "Hủy phiếu", ItemHuy_Click);

                menu.Items.Add(itemDuyet);
                menu.Items.Add(itemHuy);

                DevExpress.Utils.Menu.StandardMenuManager.Default.ShowPopupMenu(menu, lstPhieuGanDau, e.Location);
            }
        }

        private void ItemDuyet_Click(object sender, EventArgs e)
        {
            var item = lstPhieuGanDau.SelectedItem as DevExpress.XtraEditors.Controls.ImageListBoxItem;
            var phieu = item?.Value as ET.Models.Kho.ET_ChungTuKho;
            if (phieu == null)
            {
                UIHelper.ThongBao(LanguageManager.GetString("MSG_CHON_PHIEU_TRUOC") ?? "Vui lòng chọn một phiếu trong danh sách.");
                return;
            }

            var kq = BUS.Services.Kho.BUS_ChungTuKho.Instance.DuyetChungTu(phieu.Id, SessionManager.IdDoiTac);
            string rawMsg = kq.Message;
            string msg = rawMsg;
            if (!string.IsNullOrEmpty(rawMsg))
            {
                var parts = rawMsg.Split('|');
                msg = LanguageManager.GetString(parts[0]) ?? rawMsg;
                if (parts.Length > 1)
                {
                    try {
                        object[] args = new object[parts.Length - 1];
                        Array.Copy(parts, 1, args, 0, parts.Length - 1);
                        msg = string.Format(msg, args);
                    } catch {}
                }
            }
            if (kq.Success)
            {
                UIHelper.ThongBao(msg);
                LoadDSPhieuGanDay();
                LoadAlertCounts();
            }
            else
            {
                UIHelper.Loi(msg);
            }
        }

        private void ItemHuy_Click(object sender, EventArgs e)
        {
            var item = lstPhieuGanDau.SelectedItem as DevExpress.XtraEditors.Controls.ImageListBoxItem;
            var phieu = item?.Value as ET.Models.Kho.ET_ChungTuKho;
            if (phieu == null)
            {
                UIHelper.ThongBao(LanguageManager.GetString("MSG_CHON_PHIEU_TRUOC") ?? "Vui lòng chọn một phiếu trong danh sách.");
                return;
            }

            var xacNhan = XtraMessageBox.Show(
                LanguageManager.GetString("MSG_XAC_NHAN_HUY_PHIEU") ?? "Xác nhận hủy phiếu này?",
                LanguageManager.GetString("TITLE_XAC_NHAN") ?? "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (xacNhan != DialogResult.Yes) return;

            var kq = BUS.Services.Kho.BUS_ChungTuKho.Instance.HuyChungTu(
                phieu.Id,
                SessionManager.IdDoiTac,
                LanguageManager.GetString("LY_DO_HUY_MAC_DINH") ?? "Hủy từ màn hình trung tâm kho");

            string rawMsg = kq.Message;
            string msg = rawMsg;
            if (!string.IsNullOrEmpty(rawMsg))
            {
                var parts = rawMsg.Split('|');
                msg = LanguageManager.GetString(parts[0]) ?? rawMsg;
                if (parts.Length > 1)
                {
                    try {
                        object[] args = new object[parts.Length - 1];
                        Array.Copy(parts, 1, args, 0, parts.Length - 1);
                        msg = string.Format(msg, args);
                    } catch {}
                }
            }
            if (kq.Success)
            {
                UIHelper.ThongBao(msg);
                LoadDSPhieuGanDay();
            }
            else
            {
                UIHelper.Loi(msg);
            }
        }

        private void LstPhieuGanDau_DoubleClick(object sender, EventArgs e)
        {
            var item = lstPhieuGanDau.SelectedItem as DevExpress.XtraEditors.Controls.ImageListBoxItem;
            var phieu = item?.Value as ET.Models.Kho.ET_ChungTuKho;
            if (phieu != null) {
                LoadUserControl(typeof(ucTaoPhieu));
                if (_ucTaoPhieu != null) {
                    _ucTaoPhieu.XemChungTu(phieu.Id);
                }
            }
        }

        private void LoadDSPhieuGanDay()
        {
            try
            {
                var ds = BUS.Services.Kho.BUS_ChungTuKho.Instance.GetDanhSachChungTu(
                    null, DateTime.Now.AddDays(-30), DateTime.Now);
                lstPhieuGanDau.Items.Clear();

                if (lstPhieuGanDau.ImageList == null)
                {
                    var imgList = new DevExpress.Utils.ImageCollection();
                    imgList.AddImage(CreateStatusIcon(Color.FromArgb(76, 175, 80)));  
                    imgList.AddImage(CreateStatusIcon(Color.FromArgb(244, 67, 54)));   
                    imgList.AddImage(CreateStatusIcon(Color.FromArgb(255, 193, 7)));   
                    lstPhieuGanDau.ImageList = imgList;
                }

                foreach (var p in ds.Take(10))
                {
                    int imgIdx = p.TrangThai == AppConstants.TrangThaiChungTuKho.DaDuyet ? 0 :
                                 p.TrangThai == AppConstants.TrangThaiChungTuKho.DaHuy ? 1 : 2;

                    // Loại phiếu + Ngày
                    string loaiTen = LanguageManager.GetString(p.LoaiChungTu) ?? p.LoaiChungTu;
                    string displayText = $"{p.MaChungTu}  |  {loaiTen}  |  {p.NgayTao:dd/MM}";

                    var itemBox = new DevExpress.XtraEditors.Controls.ImageListBoxItem(p, displayText, imgIdx);
                    lstPhieuGanDau.Items.Add(itemBox);
                }

                UpdatePhieuActionButtons();
            }
            catch (Exception ex)
            {
                UIHelper.Loi(ex.Message);
            }
        }

        private void LoadAlertCounts()
        {
            try
            {
                int soLoHSD = BUS.Services.Kho.BUS_LoHang.Instance.GetLoSapHetHan(7).Count;
                int soSPThieu = BUS.Services.Kho.BUS_SoCai.Instance.GetCanhBaoTonToiThieu().Count;

                lblAlertHSD.Text = string.Format(LanguageManager.GetString("TXT_ALERT_HSD") ?? "{0} Lô sắp hết HSD", soLoHSD);
                lblAlertTon.Text = string.Format(LanguageManager.GetString("TXT_ALERT_TON") ?? "{0} Mặt hàng dưới mức tối thiểu", soSPThieu);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("[LoadAlertCounts] " + ex.Message);
            }
        }

        private Image CreateStatusIcon(Color color)
        {
            var bmp = new Bitmap(16, 16);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(color))
                    g.FillEllipse(brush, 2, 2, 12, 12);
            }
            return bmp;
        }

        private void btnTaoPhieu_Click(object sender, EventArgs e)
        {
            LoadUserControl(typeof(ucTaoPhieu));
        }

        private void btnTonKho_Click(object sender, EventArgs e)
        {
            LoadUserControl(typeof(ucTonKho));
        }

        private void btnLichSu_Click(object sender, EventArgs e)
        {
            LoadUserControl(typeof(ucLichSu));
        }

        private void btnCanhBao_Click(object sender, EventArgs e)
        {
            LoadUserControl(typeof(ucCanhBao));
        }

        private void LoadUserControl(Type ucType)
        {
            if (pnlContent.Controls.Count > 0)
            {
                var activeUc = pnlContent.Controls[0];
                if (activeUc is ucTaoPhieu taoPhieu && taoPhieu.IsDirty)
                {
                    string msg = LanguageManager.GetString(AppConstants.ErrorMessages.MSG_WARN_UNSAVED) ?? "Phiếu chưa lưu! Chuyển trang sẽ mất dữ liệu nhập dở. Bạn có tiếp tục?";
                    string title = LanguageManager.GetString("TITLE_CANHBAO") ?? "Cảnh báo";
                    
                    if (XtraMessageBox.Show(msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        return; // Hủy lệnh chuyển trang
                }

                if (activeUc is ucTaoPhieu) _ucTaoPhieu = null;
                else if (activeUc is ucTonKho) _ucTonKho = null;
                else if (activeUc is ucLichSu) _ucLichSu = null;
                else if (activeUc is ucCanhBao) _ucCanhBao = null;

                activeUc.Dispose();
                pnlContent.Controls.Clear();
            }

            XtraUserControl newUc = null;
            if (ucType == typeof(ucTaoPhieu)) { _ucTaoPhieu = new ucTaoPhieu(); newUc = _ucTaoPhieu; }
            else if (ucType == typeof(ucTonKho)) { _ucTonKho = new ucTonKho(); newUc = _ucTonKho; }
            else if (ucType == typeof(ucLichSu)) { _ucLichSu = new ucLichSu(); newUc = _ucLichSu; }
            else if (ucType == typeof(ucCanhBao)) { _ucCanhBao = new ucCanhBao(); newUc = _ucCanhBao; }


            if (newUc != null)
            {
                newUc.Dock = DockStyle.Fill;
                pnlContent.Controls.Add(newUc);
                
                // Cập nhật ngôn ngữ ngay khi Load
                var method = newUc.GetType().GetMethod("ThucHienDichNgonNgu");
                if (method != null) method.Invoke(newUc, null);
            }

            HighlightActiveButton(
                ucType == typeof(ucTaoPhieu) ? btnTaoPhieu :
                ucType == typeof(ucTonKho) ? btnTonKho :
                ucType == typeof(ucLichSu) ? btnLichSu :
                ucType == typeof(ucCanhBao) ? btnCanhBao : null);
        }

        // màu mè 
        private void HighlightActiveButton(SimpleButton active)
        {
            foreach (var btn in new[] { btnTaoPhieu, btnTonKho, btnLichSu, btnCanhBao })
            {
                if (btn == active)
                {
                    btn.Appearance.FontStyleDelta = FontStyle.Bold;
                    btn.Appearance.Options.UseFont = true;
                }
                else
                {
                    btn.Appearance.FontStyleDelta = FontStyle.Regular;
                    btn.Appearance.Options.UseFont = false;
                }
                
                btn.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            }
        }

        // Click vào alert label -> chuyển thẳng sang tab Cảnh báo.
        private void SetupAlertClickHandlers()
        {
            lblAlertHSD.Cursor = Cursors.Hand;
            lblAlertTon.Cursor = Cursors.Hand;
            lblAlertHSD.Click += (s, e) => { LoadUserControl(typeof(ucCanhBao)); };
            lblAlertTon.Click += (s, e) => { LoadUserControl(typeof(ucCanhBao)); };
        }

        // bật tắt nút Duyệt/Hủy theo trạng thái phiếu đang chọn.
        private void UpdatePhieuActionButtons()
        {
            var item = lstPhieuGanDau.SelectedItem as DevExpress.XtraEditors.Controls.ImageListBoxItem;
            var phieu = item?.Value as ET.Models.Kho.ET_ChungTuKho;

            bool isPhieuMoi = phieu != null 
                && phieu.TrangThai != AppConstants.TrangThaiChungTuKho.DaDuyet 
                && phieu.TrangThai != AppConstants.TrangThaiChungTuKho.DaHuy;

            if (btnDuyetPhieu != null) btnDuyetPhieu.Enabled = isPhieuMoi;
            if (btnHuyPhieu != null) btnHuyPhieu.Enabled = isPhieuMoi;
        }

        private void BtnDuyetPhieu_Click(object sender, EventArgs e)
        {
            ItemDuyet_Click(sender, e);
        }

        private void BtnHuyPhieu_Click(object sender, EventArgs e)
        {
            ItemHuy_Click(sender, e);
        }

        private void LstPhieuGanDau_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePhieuActionButtons();
        }

        #region AI Integration

        public string AIContextName => "TRUNG_TAM_KHO";
        public string AIContextDescription => "Trung tâm kho: nhập/xuất, tồn kho, cảnh báo";
        public string[] SuggestedQuestions => new[] { 
            LanguageManager.GetString("AI_SUG_TTKHO_1") ?? "Tồn kho hiện tại", 
            LanguageManager.GetString("AI_SUG_TTKHO_2") ?? "Sản phẩm nào sắp hết?", 
            LanguageManager.GetString("AI_SUG_TTKHO_3") ?? "Hướng dẫn tạo phiếu nhập kho" 
        };
        public string[] FilterableColumns => new[] { "MaKho", "TenKho", "TrangThai" };

        #endregion
    }
}

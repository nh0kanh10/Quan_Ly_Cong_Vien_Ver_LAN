using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BUS.Services.Kho;
using ET.Constants;
using ET.Models.Kho;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using GUI.AI;
using GUI.Infrastructure;
using BUS.Services.HeThong;

namespace GUI.Modules.Kho
{
    /// <summary>
    /// Màn hình CRUD Danh mục Kho Hàng (Split View).
    /// Bên trái: Grid danh sách kho. Bên phải: ucKhoHang_Detail (UC riêng).
    /// Giống cặp frmSanPham + ucSanPham_Detail.
    /// </summary>
    public partial class frmKhoHang : XtraUserControl, IAIModuleContext
    {
        private readonly BUS_Kho _bus = BUS_Kho.Instance;
        private readonly BUS_NhatKy _nhatKy = BUS_NhatKy.Instance;
        private List<ET.Models.HeThong.TuDien> _dsTrangThai;

        private SplitContainerControl _split;
        private ucKhoHang_Detail _ucDetail;

        private readonly Action<object> _onLanguageChanged;

        #region Khởi tạo và tải dữ liệu

        public frmKhoHang()
        {
            InitializeComponent();
            DungSplitView();

            _onLanguageChanged = _ =>
            {
                DichNgonNgu();
                TaiDuLieu();
                _ucDetail.ThucHienDichNgonNgu();
            };

            this.HandleDestroyed += frmKhoHang_HandleDestroyed;
        }

        private void frmKhoHang_HandleDestroyed(object sender, EventArgs e)
        {
            EventBus.Unsubscribe("LanguageChanged", _onLanguageChanged);
        }

        private void frmKhoHang_Load(object sender, EventArgs e)
        {
            try
            {
                AppStyle.StyleForm(this);
                AppStyle.StyleBanner(pnlBanner, lblTitle);
                AppStyle.StyleBtnPrimary(btnThemMoi);
                AppStyle.StyleBtnOutline(btnXoa, AppStyle.Coral);
                AppStyle.StyleBtnOutline(btnLamMoi, AppStyle.Navy);
                pnlToolbar.BackColor = AppStyle.BgCard;

                CauHinhGrid();
                DichNgonNgu();
                TaiDuLieu();

                // UC chi tiết ban đầu ẩn, hiện khi chọn dòng hoặc Thêm mới
                _ucDetail.Visible = false;

                AppStyle.FixEditorForeColor(this);
                EventBus.Subscribe("LanguageChanged", _onLanguageChanged);
            }
            catch (Exception ex)
            {
                UIHelper.Loi(ex.Message);
            }
        }

        /// <summary>
        /// Chia đôi: trái = Grid, phải = ucKhoHang_Detail.
        /// </summary>
        private void DungSplitView()
        {
            _split = new SplitContainerControl();
            _split.Dock = DockStyle.Fill;
            _split.SplitterPosition = 500;
            _split.FixedPanel = SplitFixedPanel.Panel1;

            // Chuyển grid + search vào Panel1
            this.Controls.Remove(gridControl);
            this.Controls.Remove(txtTimKiem);
            _split.Panel1.Controls.Add(gridControl);
            _split.Panel1.Controls.Add(txtTimKiem);

            // UC chi tiết vào Panel2
            _ucDetail = new ucKhoHang_Detail();
            _ucDetail.Dock = DockStyle.Fill;
            _ucDetail.KhoiTao();
            _ucDetail.DaLuuXong += UcDetail_DaLuuXong;
            _split.Panel2.Controls.Add(_ucDetail);

            this.Controls.Add(_split);
            _split.BringToFront();

            gridView.FocusedRowChanged += GridView_FocusedRowChanged;
        }

        private void CauHinhGrid()
        {
            AppStyle.StyleGrid(gridView);
            gridView.Columns.Clear();

            gridView.Columns.AddVisible("MaKho", LanguageManager.GetString("COL_MA_KHO")).Width = 100;
            gridView.Columns.AddVisible("TenKho", LanguageManager.GetString("COL_TEN_KHO")).Width = 200;

            gridView.Columns.AddVisible("TrangThai", LanguageManager.GetString("COL_TRANG_THAI")).Width = 100;

            // Gắn sự kiện dịch văn bản và tô màu cho các cột Enum/Khóa
            gridView.CustomColumnDisplayText -= GridView_CustomColumnDisplayText;
            gridView.CustomColumnDisplayText += GridView_CustomColumnDisplayText;
            gridView.RowCellStyle -= GridView_RowCellStyle;
            gridView.RowCellStyle += GridView_RowCellStyle;
        }

        private void GridView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "TrangThai" && _dsTrangThai != null)
            {
                string status = gridView.GetRowCellValue(e.RowHandle, "TrangThai")?.ToString();
                var td = _dsTrangThai.FirstOrDefault(x => x.Ma == status);
                if (td != null && !string.IsNullOrEmpty(td.MauSac))
                {
                    try
                    {
                        var color = System.Drawing.ColorTranslator.FromHtml(td.MauSac);
                        e.Appearance.ForeColor = color;
                        e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, System.Drawing.FontStyle.Bold);
                        e.Appearance.Options.UseForeColor = true;
                        e.Appearance.Options.UseFont = true;
                    }
                    catch { /* Bỏ qua lỗi format màu */ }
                }
            }
        }

        private void GridView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Value == null) return;
            
            if (e.Column.FieldName == "TrangThai")
            {
                string statusMa = e.Value.ToString();
                string resxKey = "TRANGTHAI_" + statusMa.ToUpper();
                string translatedStr = LanguageManager.GetString(resxKey);

                // Fallback: Nếu không tìm thấy key trong ResX (trả về đúng nguyên key)
                if (translatedStr == resxKey && _dsTrangThai != null)
                {
                    var td = _dsTrangThai.FirstOrDefault(x => x.Ma == statusMa);
                    if (td != null)
                    {
                        translatedStr = td.NhanHienThi; // Fallback lấy tiếng Việt dưới DB lên
                    }
                }
                e.DisplayText = translatedStr;
            }
        }

        private void TaiDuLieu()
        {
            try
            {
                // Nạp từ điển config màu sắc và tiếng Việt gốc
                _dsTrangThai = BUS.Services.HeThong.BUS_TuDien.Instance.LayDanhSachNhom("KHO_TRANG_THAI");

                // Chỉ hiện kho thật (vật lý) — kho ảo do hệ thống quản lý, không hiện cho user
                var dsKho = _bus.GetAllKho(SessionManager.CurrentLanguage)
                    .Where(k => !k.LaKhoAo)
                    .ToList();
                gridControl.DataSource = dsKho;
                gridView.BestFitColumns();
            }
            catch (Exception ex)
            {
                UIHelper.Loi(ex.Message);
            }
        }

        #endregion

        #region Xử lý sự kiện

        private void GridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (_ucDetail != null && _ucDetail.Visible && _ucDetail.DaThayDoi)
            {
                if (!UIHelper.XacNhanHuy())
                {
                    // Revert focus without triggering event recursively
                    gridView.FocusedRowChanged -= GridView_FocusedRowChanged;
                    gridView.FocusedRowHandle = e.PrevFocusedRowHandle;
                    gridView.FocusedRowChanged += GridView_FocusedRowChanged;
                    return;
                }
                _ucDetail.DaThayDoi = false;
            }

            if (gridView.FocusedRowHandle < 0 || gridView.IsGroupRow(e.FocusedRowHandle))
            {
                _ucDetail.Visible = false;
                return;
            }

            var kho = gridView.GetFocusedRow() as ET_KhoHang;
            if (kho != null)
            {
                _ucDetail.Visible = true;
                _ucDetail.TaiDuLieuChinhSua(kho);
            }
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            _ucDetail.Visible = true;
            _ucDetail.XoaTrangThemMoi();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var kho = gridView.GetFocusedRow() as ET_KhoHang;
            if (kho == null) return;
            if (kho.LaKhoAo) { UIHelper.Loi(LanguageManager.GetString(AppConstants.ErrorMessages.ERR_KHO_KHONG_XOA_AO) ?? "Kho hệ thống không được phép xóa."); return; }

            var xacNhan = XtraMessageBox.Show(
                LanguageManager.GetString(AppConstants.ErrorMessages.MSG_XAC_NHAN_XOA) ?? "Xác nhận ngưng hoạt động kho này?",
                LanguageManager.GetString("TITLE_XAC_NHAN") ?? "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (xacNhan != DialogResult.Yes) return;

            var kq = _bus.XoaKho(kho.Id);
            string msg = LanguageManager.GetString(kq.Message?.Split('|')[0]) ?? kq.Message;

            if (kq.Success)
            {
                UIHelper.ThongBao(msg);
                _nhatKy.GhiLog("KhoHang", kho.Id, "Xoa", SessionManager.IdDoiTac, kho.TenKho, null);
                TaiDuLieu();
                _ucDetail.Visible = false;
            }
            else
            {
                UIHelper.Loi(msg);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            TaiDuLieu();
        }

        /// <summary>
        /// UC chi tiết bắn lên sau khi lưu xong -> nạp lại grid.
        /// </summary>
        private void UcDetail_DaLuuXong(object sender, EventArgs e)
        {
            TaiDuLieu();
        }

        #endregion

        #region Hàm hỗ trợ

        private void DichNgonNgu()
        {
            lblTitle.Text = LanguageManager.GetString("FRM_KHO_TITLE");
            btnThemMoi.Text = LanguageManager.GetString("BTN_THEM_MOI");
            btnXoa.Text = LanguageManager.GetString("BTN_XOA");
            btnLamMoi.Text = LanguageManager.GetString("BTN_LAM_MOI");
            txtTimKiem.Properties.NullValuePrompt = LanguageManager.GetString("PROMPT_TIM_KIEM");
            CauHinhGrid();
        }

        #endregion

        #region AI Integration

        public string AIContextName => "DANH_MUC_KHO";
        public string AIContextDescription => "Danh mục kho hàng";
        public string[] SuggestedQuestions => new[] { 
            LanguageManager.GetString("AI_SUG_KHO_1") ?? "Có bao nhiêu kho?", 
            LanguageManager.GetString("AI_SUG_KHO_2") ?? "Kho nào đang hoạt động?" 
        };
        public string[] FilterableColumns => new[] { "MaKho", "TenKho", "TrangThai" };

        #endregion
    }
}

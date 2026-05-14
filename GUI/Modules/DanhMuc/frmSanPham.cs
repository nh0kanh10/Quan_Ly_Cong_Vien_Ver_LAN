using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BUS.Services.DanhMuc;
using ET.Models.DanhMuc;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using ET.Constants;
using GUI.AI;
using GUI.Infrastructure;
using BUS.Services.HeThong;

namespace GUI.Modules.DanhMuc
{
    public partial class frmSanPham : XtraUserControl, IAIModuleContext, IAICommandHandler
    {
        private readonly BUS_SanPham _bus = BUS_SanPham.Instance;
        private readonly BUS_NhatKy _nhatKy = BUS_NhatKy.Instance;
        private string _filterLoaiSP = null;

        private void frmSanPham_Load(object sender, EventArgs e)
        {
            try
            {
                AppStyle.StyleForm(this);
                AppStyle.StyleBanner(pnlBanner, lblTitle);
                AppStyle.StyleBtnPrimary(btnThemMoi);
                AppStyle.StyleBtnOutline(btnLamMoi, AppStyle.Navy);
                AppStyle.StyleStatusBar(pnlBottom, lblTongSP);

                pnlToolbar.BackColor = AppStyle.BgCard;
                pnlFilter.BackColor = AppStyle.BgCard;

                CauHinhBoLoc();
                CauHinhGrid();
                TaiDuLieu();
                AppStyle.FixEditorForeColor(this);
                
                EventBus.Subscribe("SanPham_DaLuu", _onSanPhamDaLuu);
                EventBus.Subscribe("LanguageChanged", _onLanguageChanged);
            }
            catch (Exception ex)
            {
                var title = LanguageManager.GetString(AppConstants.ErrorMessages.MSG_LOI) ?? "Lỗi";
                var msg = LanguageManager.GetString(AppConstants.ErrorMessages.ERR_NAP_MAN_HINH) ?? "Lỗi nạp màn hình:";
                XtraMessageBox.Show($"{msg} {ex.Message}\n\nStack:\n{ex.StackTrace}", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private readonly Action<object> _onSanPhamDaLuu;
        private readonly Action<object> _onLanguageChanged;

        private SplitContainerControl _split;
        private ucSanPham_Detail _ucDetail;
        
        public frmSanPham()
        {
            InitializeComponent();
            SetupSplitView();
            _onSanPhamDaLuu = _ => TaiDuLieu();
            _onLanguageChanged = _ => {
                TaiDuLieu();
                _ucDetail.ThucHienDichNgonNgu();
            };

            this.HandleDestroyed += frmSanPham_HandleDestroyed;
        }

        private void frmSanPham_HandleDestroyed(object sender, EventArgs e)
        {
            EventBus.Unsubscribe("SanPham_DaLuu", _onSanPhamDaLuu);
            EventBus.Unsubscribe("LanguageChanged", _onLanguageChanged);
        }

        private void SetupSplitView()
        {
            _split = new SplitContainerControl();
            _split.Dock = DockStyle.Fill;
            _split.SplitterPosition = 500; 

            this.Controls.Remove(gridControl);
            _split.Panel1.Controls.Add(gridControl);
            
            _ucDetail = new ucSanPham_Detail();
            _ucDetail.Dock = DockStyle.Fill;
            _split.Panel2.Controls.Add(_ucDetail);

            _ucDetail.Visible = false;

            this.Controls.Add(_split);
            _split.BringToFront();

            gridView.BeforeLeaveRow += GridView_BeforeLeaveRow;
            gridView.FocusedRowChanged += GridView_FocusedRowChanged;
        }

        private void GridView_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            if (_ucDetail.Visible && _ucDetail.DaThayDoi)
            {
                var msg = LanguageManager.GetString(AppConstants.ErrorMessages.MSG_UNSAVED) ?? "Dữ liệu chưa được lưu! Bác có muốn lưu lại trước khi chuyển sang món khác không?";
                var title = LanguageManager.GetString("TITLE_UNSAVED") ?? "Cảnh báo mất dữ liệu";
                var result = DevExpress.XtraEditors.XtraMessageBox.Show(msg, title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.Cancel)
                {
                    e.Allow = false; 
                }
                else if (result == DialogResult.Yes)
                {
                    if (!_ucDetail.GoiHamLuuTuXa()) 
                    {
                        e.Allow = false; 
                    }
                }
                else if (result == DialogResult.No)
                {
                    _ucDetail.DaThayDoi = false; 
                }
            }
        }

        private void GridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView.FocusedRowHandle < 0 || gridView.IsGroupRow(e.FocusedRowHandle))
            {
                _ucDetail.Visible = false;
                return;
            }

            object idObj = gridView.GetFocusedRowCellValue("Id");
            if (idObj != null)
            {
                _ucDetail.Visible = true;
                _ucDetail.TaiDuLieuChinhSua((int)idObj);
            }
        }

        /// <summary>
        /// Tạo các nút lọc nhanh theo nhóm LoaiSanPham.
        /// Dùng AppConstants thay vì hardcode tên nhóm.
        /// </summary>
        private void CauHinhBoLoc()
        {
            pnlFilter.Controls.Clear();
            var dsBoLoc = new Dictionary<string, string>
            {
                { LanguageManager.GetString("FILTER_ALL"), null },
                { LanguageManager.GetString("FILTER_VE"), AppConstants.LoaiSanPham.VeVaoKhu },
                { LanguageManager.GetString("FILTER_DOAN"), AppConstants.LoaiSanPham.AnUong },
                { LanguageManager.GetString("FILTER_DOUONG"), AppConstants.LoaiSanPham.DoUong },
                { LanguageManager.GetString("FILTER_CHOTHUE"), AppConstants.LoaiSanPham.TuDo },
                { LanguageManager.GetString("FILTER_LUUTRU"), AppConstants.LoaiSanPham.LuuTru },
                { LanguageManager.GetString("FILTER_VATTU"), AppConstants.LoaiSanPham.NguyenLieu },
            };

            foreach (var item in dsBoLoc)
            {
                var btn = new SimpleButton
                {
                    Text = item.Key,
                    Tag = item.Value,
                    Width = 90,
                    Height = 30
                };
                btn.Click += BtnBoLoc_Click;
                pnlFilter.Controls.Add(btn);
            }

            // Highlight lại nút đang chọn theo _filterLoaiSP
            SimpleButton activeBtn = null;
            foreach (Control ctrl in pnlFilter.Controls)
            {
                if (ctrl is SimpleButton sb && (string)sb.Tag == _filterLoaiSP)
                {
                    activeBtn = sb;
                    break;
                }
            }
            if (activeBtn == null && pnlFilter.Controls.Count > 0)
                activeBtn = pnlFilter.Controls[0] as SimpleButton;

            if (activeBtn != null) 
                HighlightNutLoc(activeBtn);
        }

        /// <summary>
        /// Cấu hình Grid: cột dữ liệu, cột hành động (Sửa/Xoá),
        /// định dạng màu trạng thái.
        /// </summary>
        private void CauHinhGrid()
        {
            AppStyle.StyleGrid(gridView);
            
            gridView.OptionsBehavior.Editable = true;
            gridView.OptionsBehavior.ReadOnly = false;

            //  thực hiện dịch 
            if (gridView.Columns["MaSanPham"] != null) gridView.Columns["MaSanPham"].Caption = LanguageManager.GetString("GRID_COL_MASP");
            if (gridView.Columns["TenSanPham"] != null) gridView.Columns["TenSanPham"].Caption = LanguageManager.GetString("GRID_COL_TENSP");
            if (gridView.Columns["DonGia"] != null) gridView.Columns["DonGia"].Caption = LanguageManager.GetString("COL_GIABAN") ?? "Đơn giá";
            if (gridView.Columns["LoaiSanPham"] != null) gridView.Columns["LoaiSanPham"].Caption = LanguageManager.GetString("GRID_COL_LOAISP");
            if (gridView.Columns["TrangThai"] != null) gridView.Columns["TrangThai"].Caption = LanguageManager.GetString("GRID_COL_TRANG_THAI");
            if (gridView.Columns["Id"] != null) gridView.Columns["Id"].Caption = LanguageManager.GetString("GRID_COL_HANH_DONG");
            
            if (gridControl.RepositoryItems.Contains(repoBtnXoa) && repoBtnXoa.Buttons.Count > 0)
            {
                repoBtnXoa.Buttons[0].Caption = LanguageManager.GetString("BTN_XOA");
            }

            //  Grouping cho Loại Sản Phẩm
            if (gridView.Columns["LoaiSanPham"] != null)
                gridView.Columns["LoaiSanPham"].GroupIndex = 0;

            var colLaVatTu = gridView.Columns["LaVatTu"];
            if (colLaVatTu == null)
            {
                colLaVatTu = new DevExpress.XtraGrid.Columns.GridColumn();
                colLaVatTu.FieldName = "LaVatTu";
                colLaVatTu.Caption = LanguageManager.GetString("GRID_COL_LAVATTU") ?? "Tính chất";
                colLaVatTu.Name = "colLaVatTu";
                gridView.Columns.Add(colLaVatTu);
            }
            colLaVatTu.GroupIndex = 1;
            gridView.GroupFormat = "{1}";

            gridView.ExpandAllGroups();

            // Gắn sự kiện dịch văn bản cho các cột Enum/Khóa
            gridView.CustomColumnDisplayText -= GridView_CustomColumnDisplayText;
            gridView.CustomColumnDisplayText += GridView_CustomColumnDisplayText;
        }

        private void GridView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Value == null) return;
            
            if (e.Column.FieldName == "LoaiSanPham")
            {
                e.DisplayText = LanguageManager.GetString("LOAISP_" + e.Value.ToString().ToUpper());
            }
            else if (e.Column.FieldName == "TrangThai")
            {
                e.DisplayText = LanguageManager.GetString("TRANGTHAI_" + e.Value.ToString().ToUpper());
            }
            else if (e.Column.FieldName == "LaVatTu")
            {
                bool isVatTu = e.Value != null && (bool)e.Value;
                var sp = gridView.GetRow(e.ListSourceRowIndex) as ET_SanPham;
                string loaiSP = sp != null ? sp.LoaiSanPham : "";

                if (loaiSP == AppConstants.LoaiSanPham.DoUong || loaiSP == AppConstants.LoaiSanPham.AnUong)
                {
                    e.DisplayText = isVatTu 
                        ? (loaiSP == AppConstants.LoaiSanPham.DoUong 
                            ? (LanguageManager.GetString("TXT_DOUONG_DONGCHAI") ?? "Đồ uống đóng chai") 
                            : (LanguageManager.GetString("TXT_DOAN_TIENLOI") ?? "Đồ ăn nhanh/tiện lợi"))
                        : (loaiSP == AppConstants.LoaiSanPham.DoUong 
                            ? (LanguageManager.GetString("TXT_DOUONG_PHACHE") ?? "Đồ uống pha chế") 
                            : (LanguageManager.GetString("TXT_DOAN_CHEBIEN") ?? "Đồ ăn chế biến"));
                }
                else
                {
                    e.DisplayText = isVatTu 
                        ? (LanguageManager.GetString("TXT_VATTU_LUUKHO") ?? "Vật tư lưu kho") 
                        : (LanguageManager.GetString("TXT_PHI_VATTU") ?? "Phi vật tư");
                }
            }
        }

        private void TaiDuLieu()
        {
            // Tránh cảnh báo mất dữ liệu khi vừa mới Reload toàn bộ màn hình
            _ucDetail.DaThayDoi = false;

            var ds = _bus.LayDanhSach(_filterLoaiSP, SessionManager.CurrentLanguage);
            gridControl.DataSource = ds;
            gridView.ExpandAllGroups();
            lblTongSP.Text = $"{LanguageManager.GetString("TXT_TONG")} {ds.Count}";
            // Cập nhật lại Grid UI phòng khi chuyển ngôn ngữ
            CauHinhGrid();
            btnThemMoi.Text = LanguageManager.GetString("BTN_THEM");
            btnLamMoi.Text = LanguageManager.GetString("BTN_LAM_MOI");
            this.Text = LanguageManager.GetString("MENU_ITEM_SANPHAM");

            // Nạp lại động các nút lọc đa ngôn ngữ
            // Cập nhật lại chuỗi tìm kiếm
            txtTimKiem.Properties.NullValuePrompt = LanguageManager.GetString("PROMPT_TIM_KIEM");
            CauHinhBoLoc();
        }



        #region Xử lý sự kiện

        private void BtnBoLoc_Click(object sender, EventArgs e)
        {
            var btn = sender as SimpleButton;
            _filterLoaiSP = btn.Tag as string;
            HighlightNutLoc(btn);
            TaiDuLieu();
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            _ucDetail.Visible = true;
            _ucDetail.ChuanBiThemMoi();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            _filterLoaiSP = null;
            HighlightNutLoc(pnlFilter.Controls[0] as SimpleButton);
            TaiDuLieu();
        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text?.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                gridView.ActiveFilterString = "";
                return;
            }

            string safe = keyword.Replace("'", "''").Replace("[", "[[");
            gridView.ActiveFilterString =
                $"[MaSanPham] LIKE '%{safe}%' OR [TenSanPham] LIKE '%{safe}%'";
        }

        private void gridView_DoubleClick(object sender, EventArgs e)
        {
            // Không làm gì cả vì Single-Click row change đã gánh việc mở Form rồi (GridView_FocusedRowChanged)
        }

        /// <summary>
        /// Tô màu cột Trạng thái: DangBan = xanh, TamNgung = vàng, NgungBan = đỏ.
        /// </summary>
        private void gridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName != "TrangThai") return;

            string trangThai = gridView.GetRowCellValue(e.RowHandle, e.Column)?.ToString();
            switch (trangThai)
            {
                case "DangBan":
                    e.Appearance.ForeColor = AppStyle.Success;
                    break;
                case "TamNgung":
                    e.Appearance.ForeColor = AppStyle.Amber;
                    break;
                case "NgungBan":
                    e.Appearance.ForeColor = AppStyle.Danger;
                    break;
                default:
                    e.Appearance.ForeColor = AppStyle.TextMuted;
                    break;
            }
            e.Appearance.Font = AppStyle.FontBold;
        }

        private void RepoBtn_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (gridView.FocusedRowHandle < 0) return;

            int id = (int)gridView.GetFocusedRowCellValue("Id");
            string ten = gridView.GetFocusedRowCellValue("TenSanPham")?.ToString();

            string action = e.Button.Tag?.ToString();

            if (action == "DELETE")
            {
                if (!UIHelper.XacNhanXoa(ten)) return;

                var kq = _bus.XoaMem(id);

                string[] parts = kq.Message?.Split('|') ?? new string[0];
                string msg = parts.Length > 0 ? LanguageManager.GetString(parts[0]) : kq.Message;
                if (parts.Length > 1) 
                {
                    try { msg = string.Format(msg, parts.Skip(1).ToArray()); } catch { }
                }

                if (kq.Success)
                {
                    UIHelper.ThongBao(msg);
                    _nhatKy.GhiLog("SanPham", id, "XoaMem", SessionManager.IdDoiTac, ten, null);
                    TaiDuLieu();
                }
                else
                {
                    UIHelper.Loi(msg);
                }
            }
        }

        #endregion

        #region Hàm hỗ trợ


        private void HighlightNutLoc(SimpleButton nutDuocChon)
        {
            foreach (Control ctrl in pnlFilter.Controls)
            {
                if (ctrl is SimpleButton sb)
                    AppStyle.StyleFilterBtnInactive(sb);
            }

            if (nutDuocChon != null)
                AppStyle.StyleFilterBtnActive(nutDuocChon);
        }

        #endregion

        #region AI Integration

        public string AIContextName => "SAN_PHAM";
        public string AIContextDescription => "Quản lý sản phẩm, dịch vụ, vé khu du lịch Đại Nam";
        public string[] SuggestedQuestions => new[] { 
            LanguageManager.GetString("AI_SUG_SP_1") ?? "Có bao nhiêu sản phẩm?", 
            LanguageManager.GetString("AI_SUG_SP_2") ?? "Lọc vé vào khu giá trên 200k", 
            LanguageManager.GetString("AI_SUG_SP_3") ?? "Sản phẩm nào đang ngừng bán?" 
        };
        public string[] FilterableColumns => new[] { "TenSanPham", "MaSanPham", "LoaiSanPham", "DonGia", "TrangThai" };

        public void ExecuteAICommand(string commandName, System.Collections.Generic.Dictionary<string, object> args)
        {
            if (commandName == "ui_filter_grid" && args.ContainsKey("filter"))
            {
                if (InvokeRequired)
                    Invoke(new Action(() => gridView.ActiveFilterString = args["filter"].ToString()));
                else
                    gridView.ActiveFilterString = args["filter"].ToString();
            }
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BUS.Services.DanhMuc;
using ET.Constants;
using ET.DTOs;
using ET.Models.DanhMuc;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using GUI.AI;
using GUI.Infrastructure;

namespace GUI.Modules.DanhMuc
{
    public partial class frmCombo : XtraUserControl, IAIModuleContext, IAICommandHandler
    {
        private readonly BUS_Combo _bus = BUS_Combo.Instance;
        private ET_Combo _current;
        private BindingList<DTO_ComboChiTietDisplay> _roItems = new BindingList<DTO_ComboChiTietDisplay>();
        private List<ET_SanPham> _allSP;
        private readonly Action<object> _onLanguageChanged;

        public frmCombo()
        {
            InitializeComponent();
            _onLanguageChanged = _ =>
            {
                if (this.IsHandleCreated && !this.IsDisposed)
                    this.Invoke((MethodInvoker)delegate { TaiDuLieu(); });
            };
            EventBus.Subscribe("LanguageChanged", _onLanguageChanged);
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

        #region Khởi tạo và tải dữ liệu

        private void frmCombo_Load(object sender, EventArgs e)
        {
            try
            {
                AppStyle.StyleForm(this);
                AppStyle.StyleBanner(pnlBanner, lblTitle);
                AppStyle.StyleBtnPrimary(btnThemMoi);
                AppStyle.StyleBtnOutline(btnLamMoi, AppStyle.Navy);
                AppStyle.StyleStatusBar(pnlBottom, lblTongCombo);
                AppStyle.StyleGrid(gridViewCombo);
                AppStyle.StyleBtnDanger(btnXoaCombo);
                AppStyle.StyleGrid(gridViewKho);
                AppStyle.StyleGrid(gridViewRo);
                AppStyle.StyleBtnOutline(btnThemVaoRo, AppStyle.Teal);
                AppStyle.StyleBtnOutline(btnChiaDeu, AppStyle.Navy);

                TaiDuLieu();
                TaiKhoSanPham();
                GanRoGrid();
                AppStyle.FixEditorForeColor(this);
            }
            catch (Exception ex)
            {
                UIHelper.Loi(ex.Message);
            }
        }

        private void TaiDuLieu()
        {
            var ds = _bus.LayDanhSach();
            gridCombo.DataSource = ds;
            gridViewCombo.BestFitColumns();
            lblTongCombo.Text = $"Tổng: {ds.Count}";
            lblTitle.Text = LanguageManager.GetString("FRM_COMBO_TITLE") ?? "QUẢN LÝ COMBO SẢN PHẨM";
            btnThemMoi.Text = LanguageManager.GetString("BTN_THEM") ?? "  + Thêm combo";
            btnLamMoi.Text = LanguageManager.GetString("BTN_LAM_MOI") ?? "  Làm mới";
        }

        private void TaiKhoSanPham()
        {
            _allSP = BUS_Combo.Instance.LaySanPhamChonCombo();
            gridKho.DataSource = _allSP;
            FormatGridKho();
        }

        private void FormatGridKho()
        {
            if (gridViewKho.Columns.Count == 0) return;
            var hide = new[] { "Id", "AnhDaiDien", "LaVatTu", "CanQuanLyLo", "TrangThai", "DaXoa", "NgayTao", "NguoiTao",
                "BangGias", "SanPham_Ve", "MonAn", "Ve_QuyenTruyCaps", "DinhMucNguyenLieus", "TenDonViTinh", "IdDonViTinh" };
            foreach (var h in hide)
                if (gridViewKho.Columns[h] != null) gridViewKho.Columns[h].Visible = false;

            if (gridViewKho.Columns["DonGia"] != null)
            {
                gridViewKho.Columns["DonGia"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridViewKho.Columns["DonGia"].DisplayFormat.FormatString = "N0";
            }
            gridViewKho.BestFitColumns();
        }

        private void GanRoGrid()
        {
            gridRo.DataSource = _roItems;
            FormatGridRo();
        }

        private void FormatGridRo()
        {
            if (gridViewRo.Columns.Count == 0) return;
            if (gridViewRo.Columns["IdSanPham"] != null) gridViewRo.Columns["IdSanPham"].Visible = false;

            var readOnly = new[] { "MaSanPham", "TenSanPham", "DonGia", "ThanhTien" };
            foreach (var col in readOnly)
                if (gridViewRo.Columns[col] != null) gridViewRo.Columns[col].OptionsColumn.AllowEdit = false;

            if (gridViewRo.Columns["DonGia"] != null)
            {
                gridViewRo.Columns["DonGia"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridViewRo.Columns["DonGia"].DisplayFormat.FormatString = "N0";
            }
            if (gridViewRo.Columns["ThanhTien"] != null)
            {
                gridViewRo.Columns["ThanhTien"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridViewRo.Columns["ThanhTien"].DisplayFormat.FormatString = "N0";
            }
            if (gridViewRo.Columns["TyLePhanBo"] != null)
            {
                gridViewRo.Columns["TyLePhanBo"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridViewRo.Columns["TyLePhanBo"].DisplayFormat.FormatString = "N2";
            }
            gridViewRo.BestFitColumns();
        }

        #endregion

        #region Xử lý sự kiện

        private void TxtTimKho_EditValueChanged(object sender, EventArgs e)
        {
            gridViewKho.ApplyFindFilter(txtTimKho.Text?.Trim());
        }

        private void gridViewCombo_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (gridViewCombo.FocusedRowHandle < 0) { XoaForm(); return; }
            _current = gridViewCombo.GetFocusedRow() as ET_Combo;
            if (_current == null) return;
            HienThiCombo(_current);
            NapRoCombo(_current.Id);
        }

        private void gridViewCombo_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName != "TrangThai") return;
            string tt = gridViewCombo.GetRowCellValue(e.RowHandle, e.Column)?.ToString();
            if (tt == AppConstants.TrangThaiCombo.HoatDong) e.Appearance.ForeColor = AppStyle.Success;
            else if (tt == AppConstants.TrangThaiCombo.BanNhap) e.Appearance.ForeColor = AppStyle.Amber;
            else if (tt == AppConstants.TrangThaiCombo.NgungApDung) e.Appearance.ForeColor = AppStyle.Danger;
            e.Appearance.Font = AppStyle.FontBold;
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            _current = null;
            XoaForm();
            txtMaCombo.Text = "(Tự sinh)";
            cboTrangThai.SelectedIndex = 0;
            txtTenCombo.Focus();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            TaiDuLieu();
            TaiKhoSanPham();
            XoaForm();
        }

        private void BtnLuuCombo_Click(object sender, EventArgs e)
        {
            var dto = new ET_Combo
            {
                Id = _current?.Id ?? 0,
                TenCombo = txtTenCombo.Text?.Trim(),
                GiaCombo = decimal.TryParse(txtGiaCombo.Text?.Replace(",", "").Replace(".", ""), out decimal g) ? g : 0,
                MoTa = txtMoTa.Text?.Trim(),
                TrangThai = cboTrangThai.Text
            };

            // Ép về Bản Nháp nếu chưa phân bổ 100% tiền
            if (dto.TrangThai == "HoatDong" || dto.TrangThai == "Đang hoạt động")
            {
                decimal tongTyLe = _roItems?.Sum(x => x.TyLePhanBo) ?? 0m;
                if (tongTyLe != 100m)
                {
                    UIHelper.CanhBao(LanguageManager.GetString(AppConstants.ErrorMessages.MSG_COMBO_TYLE_CHUA_DU) ?? "Tổng tỷ lệ phân bổ chưa đạt 100%.\nHệ thống tự động chuyển Combo về 'BanNhap' để đảm bảo an toàn doanh thu.");
                    dto.TrangThai = "BanNhap";
                    cboTrangThai.Text = dto.TrangThai;
                }
            }

            bool laThemMoi = dto.Id == 0;
            string snapshotCu = null;
            if (!laThemMoi && _current != null)
            {
                var monCuDS = _bus.LayChiTiet(_current.Id);
                string monCu = monCuDS.Count > 0
                    ? string.Join(", ", monCuDS.Select(x => $"{x.TenSanPham} x{x.SoLuong}"))
                    : "(trống)";
                snapshotCu = $"Tên: {_current.TenCombo} | Giá: {_current.GiaCombo:N0} | TT: {_current.TrangThai} | Món: {monCu}";
            }

            ET.Results.OperationResult kq;
            if (laThemMoi)
                kq = _bus.ThemMoi(dto);
            else
                kq = _bus.CapNhat(dto);

            string msg = LanguageManager.GetString(kq.Message?.Split('|')[0]) ?? kq.Message;
            if (!kq.Success)
            {
                UIHelper.Loi(msg);
                return;
            }

            int idComboSaved = laThemMoi && kq.Data is int newId ? newId : dto.Id;

            // Lưu luôn danh sách rổ chi tiết
            if (idComboSaved > 0 && _roItems != null)
            {
                var dsChiTiet = _roItems.Select(x => new ET_ComboChiTiet
                {
                    IdSanPham = x.IdSanPham,
                    SoLuong = x.SoLuong,
                    TyLePhanBo = x.TyLePhanBo
                }).ToList();

                var resRo = _bus.LuuChiTiet(idComboSaved, dsChiTiet);
                if (!resRo.Success)
                {
                    UIHelper.Loi(LanguageManager.GetString(resRo.Message?.Split('|')[0]) ?? resRo.Message);
                    return;
                }
            }

            UIHelper.ThongBao(msg);

            string monMoi = _roItems != null && _roItems.Count > 0
                ? string.Join(", ", _roItems.Select(x => $"{x.TenSanPham} x{x.SoLuong}"))
                : "(trống)";
            string snapshotMoi = $"Tên: {dto.TenCombo} | Giá: {dto.GiaCombo:N0} | TT: {dto.TrangThai} | Món: {monMoi}";

            BUS.Services.HeThong.BUS_NhatKy.Instance.GhiLog(
                "Combo",
                idComboSaved,
                laThemMoi ? "Thêm mới Combo" : "Cập nhật Combo",
                SessionManager.IdDoiTac,
                snapshotCu,
                snapshotMoi,
                null
            );

            TaiDuLieu();
            
            if (dto.Id == 0)
            {
                _current = new ET_Combo { Id = idComboSaved };
                _roItems.Clear();
                GanRoGrid();
            }
            else
            {
                NapRoCombo(idComboSaved);
            }
        }

        private void RepoBtnXoaMon_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gridViewRo.FocusedRowHandle < 0) return;
            var row = gridViewRo.GetFocusedRow() as DTO_ComboChiTietDisplay;
            if (row != null)
            {
                _roItems.Remove(row);
                gridViewRo.RefreshData();
                CapNhatTongInfo();
            }
        }

        private void BtnXoaCombo_Click(object sender, EventArgs e)
        {
            if (_current == null) return;
            if (!UIHelper.XacNhanXoa(_current.TenCombo)) return;
            var kq = _bus.XoaMem(_current.Id);
            string msg = LanguageManager.GetString(kq.Message?.Split('|')[0]) ?? kq.Message;
            if (kq.Success) 
            { 
                BUS.Services.HeThong.BUS_NhatKy.Instance.GhiLog("Combo", _current.Id, "Xoá", SessionManager.IdDoiTac, $"Xoá combo: {_current.TenCombo}", null, null);
                
                UIHelper.ThongBao(msg); 
                TaiDuLieu(); 
                XoaForm(); 
            }
            else UIHelper.Loi(msg);
        }

        private void GridViewKho_DoubleClick(object sender, EventArgs e) => ThemSPVaoRo();
        private void BtnThemVaoRo_Click(object sender, EventArgs e) => ThemSPVaoRo();

        private void ThemSPVaoRo()
        {
            if (gridViewKho.FocusedRowHandle < 0) return;
            var sp = gridViewKho.GetFocusedRow() as ET_SanPham;
            if (sp == null) return;

            var existing = _roItems.FirstOrDefault(x => x.IdSanPham == sp.Id);
            if (existing != null)
            {
                existing.SoLuong++;
                gridViewRo.RefreshData();
            }
            else
            {
                _roItems.Add(new DTO_ComboChiTietDisplay
                {
                    IdSanPham = sp.Id,
                    MaSanPham = sp.MaSanPham,
                    TenSanPham = sp.TenSanPham,
                    DonGia = sp.DonGia ?? 0,
                    SoLuong = 1,
                    TyLePhanBo = 0
                });
                FormatGridRo();
            }
            CapNhatTongInfo();
        }

        private void GridViewRo_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            gridViewRo.PostEditor();
            gridViewRo.UpdateCurrentRow();
            CapNhatTongInfo();
        }

        private void BtnChiaDeu_Click(object sender, EventArgs e)
        {
            if (_roItems.Count == 0) return;
            int count = _roItems.Count;
            decimal phanDeu = Math.Floor(10000m / count) / 100m;
            for (int i = 0; i < count - 1; i++)
                _roItems[i].TyLePhanBo = phanDeu;
            _roItems[count - 1].TyLePhanBo = 100m - (phanDeu * (count - 1));
            gridViewRo.RefreshData();
            CapNhatTongInfo();
        }

        #endregion

        #region Hàm hỗ trợ

        private void HienThiCombo(ET_Combo c)
        {
            txtMaCombo.Text = c.MaCombo;
            txtTenCombo.Text = c.TenCombo;
            txtGiaCombo.Text = c.GiaCombo.ToString("N0");
            txtMoTa.Text = c.MoTa;
            cboTrangThai.Text = c.TrangThai;
        }

        private void XoaForm()
        {
            _current = null;
            txtMaCombo.Text = ""; txtTenCombo.Text = ""; txtGiaCombo.Text = ""; txtMoTa.Text = "";
            cboTrangThai.SelectedIndex = 0;
            _roItems.Clear();
            GanRoGrid();
            CapNhatTongInfo();
        }

        private void NapRoCombo(int idCombo)
        {
            var ds = _bus.LayChiTiet(idCombo);
            _roItems = new BindingList<DTO_ComboChiTietDisplay>(ds);
            GanRoGrid();
            CapNhatTongInfo();
        }

        private void CapNhatTongInfo()
        {
            decimal tongTyLe = _roItems.Sum(x => x.TyLePhanBo);
            decimal tongGiaGoc = _roItems.Sum(x => x.ThanhTien);

            lblTongTyLe.Text = $"Phân bổ: {tongTyLe:N2}% / 100%";
            lblTongGiaGoc.Text = $"Giá gốc: {tongGiaGoc:N0}₫";

            if (tongTyLe == 100m) lblTongTyLe.ForeColor = AppStyle.Success;
            else if (tongTyLe > 100m) lblTongTyLe.ForeColor = AppStyle.Danger;
            else lblTongTyLe.ForeColor = AppStyle.Amber;

            pnlBarChart?.Invalidate();
        }

        private static readonly Color[] BarColors = {
            Color.FromArgb(59, 130, 246), Color.FromArgb(16, 185, 129),
            Color.FromArgb(245, 158, 11), Color.FromArgb(239, 68, 68),
            Color.FromArgb(168, 85, 247), Color.FromArgb(20, 184, 166)
        };

        private void PnlBarChart_Paint(object sender, PaintEventArgs e)
        {
            if (_roItems == null || _roItems.Count == 0) return;
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            int bx = 10, by = 3, bh = 18, bw = pnlBarChart.Width - 20;
            using (var bg = new SolidBrush(Color.FromArgb(226, 232, 240)))
                g.FillRectangle(bg, bx, by, bw, bh);

            float cx = bx;
            for (int i = 0; i < _roItems.Count; i++)
            {
                if (_roItems[i].TyLePhanBo <= 0) continue;
                float w = (float)(_roItems[i].TyLePhanBo / 100m) * bw;
                if (cx + w > bx + bw) w = bx + bw - cx;
                if (w <= 0) break;
                using (var br = new SolidBrush(BarColors[i % BarColors.Length]))
                    g.FillRectangle(br, cx, by, w, bh);
                if (i < _roItems.Count - 1) g.DrawLine(Pens.White, cx + w, by, cx + w, by + bh);
                cx += w;
            }
        }

        #endregion

        #region AI Integration

        public string AIContextName => "COMBO";
        public string AIContextDescription => "Quản lý combo gói sản phẩm";
        public string[] SuggestedQuestions => new[] { 
            LanguageManager.GetString("AI_SUG_CB_1") ?? "Liệt kê tất cả combo", 
            LanguageManager.GetString("AI_SUG_CB_2") ?? "Combo nào đang hoạt động?", 
            LanguageManager.GetString("AI_SUG_CB_3") ?? "Chi tiết combo VIP" 
        };
        public string[] FilterableColumns => new[] { "TenCombo", "MaCombo", "GiaCombo", "TrangThai" };

        public void ExecuteAICommand(string commandName, System.Collections.Generic.Dictionary<string, object> args)
        {
            if (commandName == "ui_filter_grid" && args.ContainsKey("filter"))
            {
                if (InvokeRequired)
                    Invoke(new Action(() => gridViewCombo.ActiveFilterString = args["filter"].ToString()));
                else
                    gridViewCombo.ActiveFilterString = args["filter"].ToString();
            }
        }

        #endregion
    }
}

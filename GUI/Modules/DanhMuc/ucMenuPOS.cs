using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using BUS.Services.BanHang;
using ET.Constants;
using ET.DTOs;
using ET.Models.BanHang;
using ET.Models.DanhMuc;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using GUI.AI;
using GUI.Infrastructure;
using DAL.Repositories.BanHang;

namespace GUI.Modules.DanhMuc
{
    public partial class ucMenuPOS : XtraUserControl, IAIModuleContext, IAICommandHandler
    {
        private readonly BUS_MenuPOS _bus = BUS_MenuPOS.Instance;
        private ET_DiemBanHang _current;
        private BindingList<DTO_MenuPOSItem> _menuItems = new BindingList<DTO_MenuPOSItem>();
        private List<ET_SanPham> _allSP;

        private readonly Action<object> _onLanguageChanged;

        public ucMenuPOS()
        {
            InitializeComponent();
            _onLanguageChanged = _ => { InitUX(); TaiDuLieuDiemBan(); GanMenuGrid(); };
            
            this.Load += UcMenuPOS_Load;
            this.HandleDestroyed += UcMenuPOS_HandleDestroyed;
        }

        private void UcMenuPOS_HandleDestroyed(object sender, EventArgs e)
        {
            EventBus.Unsubscribe("LanguageChanged", _onLanguageChanged);
        }

        #region Khởi tạo và tải dữ liệu

        private void UcMenuPOS_Load(object sender, EventArgs e)
        {
            try
            {
                AppStyle.StyleBanner(pnlBanner, lblTitle);
                AppStyle.StyleBtnPrimary(btnThemMoi);
                AppStyle.StyleBtnOutline(btnLuuDiemBan, AppStyle.Navy);
                AppStyle.StyleBtnDanger(btnXoaDiemBan);
                AppStyle.StyleStatusBar(pnlBottom, lblTongDiemBan);
                AppStyle.StyleGrid(gridViewDiemBan);
                AppStyle.StyleGrid(gridViewKho);
                AppStyle.StyleGrid(gridViewMenu);
                AppStyle.StyleBtnOutline(btnThemVaoMenu, AppStyle.Teal);
                AppStyle.StyleBtnPrimary(btnLuuMenu);

                LoadKhuVuc();
                TaiDuLieuDiemBan();
                TaiKhoSanPham();
                GanMenuGrid();
                AppStyle.FixEditorForeColor(this);
                InitUX();
                
                EventBus.Subscribe("LanguageChanged", _onLanguageChanged);
            }
            catch (Exception ex)
            {
                UIHelper.Loi(ex.Message);
            }
        }

        private void GridViewMenu_CustomDrawEmptyForeground(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
        {
            if (_menuItems.Count == 0)
            {
                var format = new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Center, LineAlignment = System.Drawing.StringAlignment.Center };
                e.Graphics.DrawString(LanguageManager.GetString("MSG_MENU_EMPTY") ?? "Chưa có món nào.\nHãy tìm và kéo thả/click từ Kho Sản Phẩm.", new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Italic), System.Drawing.Brushes.Gray, e.Bounds, format);
            }
        }

        private void GridViewDiemBan_CustomDrawEmptyForeground(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
        {
            if (gridViewDiemBan.RowCount == 0)
            {
                var format = new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Center, LineAlignment = System.Drawing.StringAlignment.Center };
                e.Graphics.DrawString(LanguageManager.GetString("MSG_DIEMBAN_EMPTY") ?? "Chưa có Điểm bán nào được thiết lập.", new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Italic), System.Drawing.Brushes.Gray, e.Bounds, format);
            }
        }

        private void LoadKhuVuc()
        {
            var dsKhuVuc = BUS.Services.DanhMuc.BUS_KhuVuc.Instance.LayDanhSach();
            cboKhuVuc.Properties.DataSource = dsKhuVuc;
            cboKhuVuc.Properties.DisplayMember = "TenKhuVuc";
            cboKhuVuc.Properties.ValueMember = "Id";
        }

        private void TaiDuLieuDiemBan()
        {
            var ds = _bus.LayDanhSachDiemBan();
            gridDiemBan.DataSource = ds;
            gridViewDiemBan.BestFitColumns();
            lblTongDiemBan.Text = string.Format(LanguageManager.GetString("LBL_TONG_DIEMBAN") ?? "Tổng: {0}", ds.Count);
        }

        private void TaiKhoSanPham()
        {
            _allSP = _bus.LayKhoSanPham();
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

        private void GanMenuGrid()
        {
            gridMenu.DataSource = _menuItems;
            FormatGridMenu();
        }

        private void FormatGridMenu()
        {
            if (gridViewMenu.Columns.Count == 0) return;
            if (gridViewMenu.Columns["IdSanPham"] != null) gridViewMenu.Columns["IdSanPham"].Visible = false;

            var readOnly = new[] { "MaSanPham", "TenSanPham", "LoaiSanPham", "DonGia" };
            foreach (var col in readOnly)
                if (gridViewMenu.Columns[col] != null) gridViewMenu.Columns[col].OptionsColumn.AllowEdit = false;

            if (gridViewMenu.Columns["DonGia"] != null)
            {
                gridViewMenu.Columns["DonGia"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridViewMenu.Columns["DonGia"].DisplayFormat.FormatString = "N0";
            }
            gridViewMenu.BestFitColumns();
        }

        #endregion

        #region Xử lý sự kiện Điểm Bán

        private void InitUX()
        {
            btnThemMoi.ToolTip = LanguageManager.GetString("TT_THEM_POS") ?? "Tạo mới một cấu hình Điểm bán";
            btnLuuDiemBan.ToolTip = LanguageManager.GetString("TT_LUU_POS") ?? "Lưu lại các thay đổi của Điểm bán hiện tại";
            btnXoaDiemBan.ToolTip = LanguageManager.GetString("TT_XOA_POS") ?? "Xóa Điểm bán đang chọn";
            btnThemVaoMenu.ToolTip = LanguageManager.GetString("TT_THEM_MON") ?? "Đưa sản phẩm đang chọn vào Menu Điểm bán";
            btnLuuMenu.ToolTip = LanguageManager.GetString("TT_LUU_MENU") ?? "Lưu thay đổi danh sách Menu thực đơn";
            
            cboKhuVuc.Properties.NullText = LanguageManager.GetString("MSG_CHON_KHUVUC") ?? "Chọn khu vực...";
        }

        private void GridViewDiemBan_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (gridViewDiemBan.FocusedRowHandle < 0) { XoaForm(); return; }
            _current = gridViewDiemBan.GetFocusedRow() as ET_DiemBanHang;
            if (_current == null) return;
            HienThiDiemBan(_current);
            NapMenuDiemBan(_current.Id);
        }

        private void GridViewDiemBan_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName != "TrangThai") return;
            string tt = gridViewDiemBan.GetRowCellValue(e.RowHandle, e.Column)?.ToString();
            if (tt == AppConstants.TrangThaiHieuLuc.HoatDong) e.Appearance.ForeColor = AppStyle.Success;
            else if (tt == AppConstants.TrangThaiHieuLuc.VoHieuHoa) e.Appearance.ForeColor = AppStyle.Danger;
            e.Appearance.Font = AppStyle.FontBold;
        }

        private void BtnThemMoi_Click(object sender, EventArgs e)
        {
            _current = null;
            XoaForm();
            txtTenDiemBan.Focus();
        }

        private void BtnLuuDiemBan_Click(object sender, EventArgs e)
        {
            var dto = new ET_DiemBanHang
            {
                Id = _current?.Id ?? 0,
                MaDiemBan = txtMaDiemBan.Text?.Trim(),
                TenDiemBan = txtTenDiemBan.Text?.Trim(),
                IdKhuVuc = Convert.ToInt32(cboKhuVuc.EditValue),
                ChoPhepBanVe = chkChoPhepBanVe.Checked,
                ChoPhepBanFNB = chkChoPhepBanFNB.Checked,
                ChoPhepThue = chkChoPhepThue.Checked,
                TrangThai = cboTrangThai.Text
            };

            ET.Results.OperationResult kq;
            if (dto.Id == 0)
                kq = _bus.ThemDiemBan(dto);
            else
                kq = _bus.CapNhatDiemBan(dto);

            string msg = LanguageManager.GetString(kq.Message?.Split('|')[0]) ?? kq.Message;
            if (!kq.Success)
            {
                UIHelper.Loi(msg);
                return;
            }

            UIHelper.ThongBao(msg);
            
            int logId = dto.Id == 0 && kq.Data is int ? (int)kq.Data : dto.Id;
            BUS.Services.HeThong.BUS_NhatKy.Instance.GhiLog("Điểm Bán POS", logId, dto.Id == 0 ? "Thêm mới" : "Cập nhật", SessionManager.IdDoiTac, $"Điểm bán: {dto.TenDiemBan}", $"Khu vực ID: {dto.IdKhuVuc}", $"TT: {dto.TrangThai}");

            TaiDuLieuDiemBan();
            
            if (dto.Id == 0 && kq.Data is int newId)
            {
                _current = new ET_DiemBanHang { Id = newId };
                _menuItems.Clear();
                GanMenuGrid();
            }
        }

        private void BtnXoaDiemBan_Click(object sender, EventArgs e)
        {
            if (_current == null) return;
            if (!UIHelper.XacNhanXoa(_current.TenDiemBan)) return;
            
            var kq = _bus.XoaDiemBan(_current.Id, _current.TenDiemBan);
            string msg = LanguageManager.GetString(kq.Message?.Split('|')[0]) ?? kq.Message;
            
            if (kq.Success) 
            { 
                BUS.Services.HeThong.BUS_NhatKy.Instance.GhiLog("Điểm Bán POS", _current.Id, "Xoá", SessionManager.IdDoiTac, $"Xoá điểm bán: {_current.TenDiemBan}", null, null);
                
                UIHelper.ThongBao(msg); 
                TaiDuLieuDiemBan(); 
                XoaForm(); 
            }
            else UIHelper.Loi(msg);
        }

        #endregion

        #region Xử lý sự kiện Menu

        private void TxtTimKho_EditValueChanged(object sender, EventArgs e)
        {
            gridViewKho.ApplyFindFilter(txtTimKho.Text?.Trim());
        }

        private void GridViewKho_DoubleClick(object sender, EventArgs e) => ThemSPVaoMenu();
        private void BtnThemVaoMenu_Click(object sender, EventArgs e) => ThemSPVaoMenu();

        private void ThemSPVaoMenu()
        {
            if (_current == null || _current.Id == 0)
            {
                UIHelper.CanhBao(LanguageManager.GetString("ERR_CHON_DIEMBAN") ?? "Vui lòng chọn Điểm Bán trước!");
                return;
            }

            if (gridViewKho.FocusedRowHandle < 0) return;
            var sp = gridViewKho.GetFocusedRow() as ET_SanPham;
            if (sp == null) return;

            if (_menuItems.Any(x => x.IdSanPham == sp.Id))
            {
                UIHelper.CanhBao(LanguageManager.GetString("ERR_SP_DA_CO") ?? "Sản phẩm đã tồn tại trong Menu!");
                return;
            }

            int nextOrder = _menuItems.Count > 0 ? _menuItems.Max(x => x.ThuTuHienThi) + 1 : 1;

            _menuItems.Add(new DTO_MenuPOSItem
            {
                IdSanPham = sp.Id,
                MaSanPham = sp.MaSanPham,
                TenSanPham = sp.TenSanPham,
                LoaiSanPham = sp.LoaiSanPham,
                DonGia = sp.DonGia ?? 0,
                ThuTuHienThi = nextOrder,
                ConHoatDong = true
            });
            
            FormatGridMenu();
            CapNhatTongInfo();
        }

        private void RepoBtnXoaMon_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gridViewMenu.FocusedRowHandle < 0) return;
            var row = gridViewMenu.GetFocusedRow() as DTO_MenuPOSItem;
            if (row != null)
            {
                _menuItems.Remove(row);
                gridViewMenu.RefreshData();
                CapNhatTongInfo();
            }
        }

        private void BtnLuuMenu_Click(object sender, EventArgs e)
        {
            if (_current == null || _current.Id == 0) return;
            
            var kq = _bus.LuuMenu(_current.Id, _menuItems.ToList());
            string msg = LanguageManager.GetString(kq.Message?.Split('|')[0]) ?? kq.Message;
            
            if (kq.Success)
            {
                string dsMon = _menuItems.Count > 0 ? string.Join(", ", _menuItems.Select(x => x.TenSanPham)) : "Trống (Đã xoá hết món)";
                BUS.Services.HeThong.BUS_NhatKy.Instance.GhiLog("Điểm Bán POS", _current.Id, "Cập nhật Thực đơn (Menu)", SessionManager.IdDoiTac, $"Điểm bán: {_current.TenDiemBan}", $"Gồm {_menuItems.Count} món: {dsMon}", null);

                UIHelper.ThongBao(msg);
            }
            else
            {
                UIHelper.Loi(msg);
            }
        }

        #endregion

        #region Hàm hỗ trợ

        private void HienThiDiemBan(ET_DiemBanHang d)
        {
            txtMaDiemBan.Text = d.MaDiemBan;
            txtTenDiemBan.Text = d.TenDiemBan;
            cboKhuVuc.EditValue = d.IdKhuVuc;
            chkChoPhepBanVe.Checked = d.ChoPhepBanVe;
            chkChoPhepBanFNB.Checked = d.ChoPhepBanFNB;
            chkChoPhepThue.Checked = d.ChoPhepThue;
            cboTrangThai.Text = d.TrangThai;
        }

        private void XoaForm()
        {
            _current = null;
            txtMaDiemBan.Text = ""; 
            txtTenDiemBan.Text = "";
            cboKhuVuc.EditValue = null;
            chkChoPhepBanVe.Checked = false;
            chkChoPhepBanFNB.Checked = false;
            chkChoPhepThue.Checked = false;
            cboTrangThai.SelectedIndex = 0;
            
            _menuItems.Clear();
            GanMenuGrid();
            CapNhatTongInfo();
        }

        private void NapMenuDiemBan(int idDiemBan)
        {
            var ds = _bus.LayMenuTheoDiemBan(idDiemBan);
            _menuItems = new BindingList<DTO_MenuPOSItem>(ds);
            GanMenuGrid();
            CapNhatTongInfo();
        }

        private void CapNhatTongInfo()
        {
            lblTongMenu.Text = string.Format(LanguageManager.GetString("LBL_TONG_MENU") ?? "Số lượng món: {0}", _menuItems.Count);
        }

        #endregion

        #region AI Integration

        public string AIContextName => "MENU_POS";
        public string AIContextDescription => "Cấu hình Menu sản phẩm cho máy bán hàng POS";
        public string[] SuggestedQuestions => new[] { 
            LanguageManager.GetString("AI_SUG_MP_1") ?? "Có bao nhiêu máy POS?", 
            LanguageManager.GetString("AI_SUG_MP_2") ?? "Máy nào được bán vé?", 
        };
        public string[] FilterableColumns => new[] { "TenDiemBan", "MaDiemBan", "TrangThai" };

        public void ExecuteAICommand(string commandName, Dictionary<string, object> args)
        {
            if (commandName == "ui_filter_grid" && args.ContainsKey("filter"))
            {
                if (InvokeRequired)
                    Invoke(new Action(() => gridViewDiemBan.ActiveFilterString = args["filter"].ToString()));
                else
                    gridViewDiemBan.ActiveFilterString = args["filter"].ToString();
            }
        }

        #endregion
    }
}

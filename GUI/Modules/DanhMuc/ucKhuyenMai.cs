using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BUS.Services.BanHang;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ET.Constants;
using ET.DTOs;
using ET.Results;
using GUI.Infrastructure;

namespace GUI.Modules.DanhMuc
{
    public partial class ucKhuyenMai : XtraUserControl
    {
        private List<DTO_KhuyenMaiPOS> _danhSachKM;
        private DTO_KhuyenMaiPOS _currentKM;
        private readonly Action<object> _onLanguageChanged;

        #region Khởi tạo và tải dữ liệu

        public ucKhuyenMai()
        {
            InitializeComponent();
            _onLanguageChanged = _ => { InitLanguage(); LoadDuLieu(); };
            
            this.Load += ucKhuyenMai_Load;
            this.HandleDestroyed += ucKhuyenMai_HandleDestroyed;
        }

        private void ucKhuyenMai_Load(object sender, EventArgs e)
        {
            AppStyle.StyleForm(this);
            AppStyle.StyleBtnPrimary(btnThemMoi);
            AppStyle.StyleBtnDanger(btnXoa);
            AppStyle.StyleBtnPrimary(btnThemDK);
            AppStyle.StyleBtnDanger(btnXoaDK);

            InitLanguage();
            LoadDuLieu();
            EventBus.Subscribe("LanguageChanged", _onLanguageChanged);
        }

        private void ucKhuyenMai_HandleDestroyed(object sender, EventArgs e)
        {
            EventBus.Unsubscribe("LanguageChanged", _onLanguageChanged);
        }

        private void InitLanguage()
        {
            lblBanner.Text = LanguageManager.GetString("LBL_KM_BANNER");
            
            lblMaKM.Text = LanguageManager.GetString("LBL_KM_MA");
            lblTenKM.Text = LanguageManager.GetString("LBL_KM_TEN");
            lblLoaiGiam.Text = LanguageManager.GetString("LBL_KM_LOAI");
            lblGiaTri.Text = LanguageManager.GetString("LBL_KM_GIA_TRI");
            lblTuNgay.Text = LanguageManager.GetString("LBL_KM_TU_NGAY");
            lblDenNgay.Text = LanguageManager.GetString("LBL_KM_DEN_NGAY");
            lblDonTT.Text = LanguageManager.GetString("LBL_KM_DON_TT");
            lblSoLan.Text = LanguageManager.GetString("LBL_KM_SO_LAN");
            
            chkChongCheo.Properties.Caption = LanguageManager.GetString("LBL_KM_CHONG_CHEO");
            chkTrangThai.Properties.Caption = LanguageManager.GetString("LBL_KM_TRANG_THAI");
            lblHintDK.Text = LanguageManager.GetString("LBL_KM_AND_HINT");
            
            colMaKM.Caption = LanguageManager.GetString("LBL_KM_MA");
            colTenKM.Caption = LanguageManager.GetString("LBL_KM_TEN");
            colLoaiGiam.Caption = LanguageManager.GetString("LBL_KM_LOAI");
            colGiaTri.Caption = LanguageManager.GetString("LBL_KM_GIA_TRI");
            colTrangThai.Caption = LanguageManager.GetString("FLT_KM_ACTIVE");
            
            colLoaiDK.Caption = LanguageManager.GetString("COL_KM_LOAI_DK");
            colPhepSo.Caption = LanguageManager.GetString("COL_KM_PHEP_SO");
            colGiaTriDK.Caption = LanguageManager.GetString("COL_KM_GIA_TRI_DK");

            cboTrangThaiFlt.Properties.Items.Clear();
            cboTrangThaiFlt.Properties.Items.Add(LanguageManager.GetString("FLT_KM_TAT_CA"));
            cboTrangThaiFlt.Properties.Items.Add(LanguageManager.GetString("FLT_KM_ACTIVE"));
            cboTrangThaiFlt.Properties.Items.Add(LanguageManager.GetString("FLT_KM_EXPIRED"));
            cboTrangThaiFlt.SelectedIndex = 0;

            cboLoaiGiam.Properties.Items.Clear();
            cboLoaiGiam.Properties.Items.Add(AppConstants.LoaiGiamGia.PhanTram);
            cboLoaiGiam.Properties.Items.Add(AppConstants.LoaiGiamGia.SoTien);

            repLoaiDK.Items.Clear();
            repLoaiDK.Items.Add(AppConstants.LoaiDieuKienKM.HangThanhVien);
            repPhepSo.Items.Clear();
            repPhepSo.Items.Add(AppConstants.PhepSoKM.BangNhau);
            repPhepSo.Items.Add(AppConstants.PhepSoKM.IN);

            var tip = new ToolTip { ShowAlways = true, AutoPopDelay = 5000, InitialDelay = 500 };
            tip.SetToolTip(txtMaKM,    LanguageManager.GetString("TIP_KM_MA")    ?? "Mã định danh duy nhất của chương trình khuyến mãi");
            tip.SetToolTip(txtTenKM,   LanguageManager.GetString("TIP_KM_TEN")   ?? "Tên mô tả ngắn gọn cho chương trình khuyến mãi");
            tip.SetToolTip(spinGiaTri, LanguageManager.GetString("TIP_KM_GIA_TRI") ?? "Giá trị giảm (% hoặc số tiền cố định)");
            tip.SetToolTip(spinDonTT,  LanguageManager.GetString("TIP_KM_DON_TT") ?? "Tổng tiền tối thiểu của đơn hàng để áp dụng KM");
            tip.SetToolTip(spinSoLan,  LanguageManager.GetString("TIP_KM_SO_LAN") ?? "Bỏ trống = không giới hạn số lần sử dụng");
            tip.SetToolTip(chkChongCheo, LanguageManager.GetString("TIP_KM_CHONG_CHEO") ?? "Cho phép cộng dồn với khuyến mãi khác (Stackable)");
        }

        private void LoadDuLieu()
        {
            var rs = BUS_KhuyenMai.Instance.LayDanhSach();
            if (rs.Success)
            {
                _danhSachKM = rs.Data as List<DTO_KhuyenMaiPOS> ?? new List<DTO_KhuyenMaiPOS>();
                HienThiGrid();
                LamMoiForm();
            }
            else
            {
                UIHelper.Loi(LanguageManager.GetString(rs.Message));
            }
        }

        private void HienThiGrid()
        {
            var lst = _danhSachKM;
            if (cboTrangThaiFlt.SelectedIndex == 1)
                lst = lst.Where(x => x.TrangThai && x.NgayKetThuc >= DateTime.Now).ToList();
            else if (cboTrangThaiFlt.SelectedIndex == 2)
                lst = lst.Where(x => !x.TrangThai || x.NgayKetThuc < DateTime.Now).ToList();

            string keyword = txtTimKiem.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(keyword))
            {
                lst = lst.Where(x => x.MaKhuyenMai.ToLower().Contains(keyword) || 
                                     x.TenKhuyenMai.ToLower().Contains(keyword)).ToList();
            }

            gridKM.DataSource = null;
            gridKM.DataSource = lst;
            gridViewKM.RefreshData();
            gridViewKM.BestFitColumns();
        }

        private void GridViewKM_CustomDrawEmptyForeground(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
        {
            if (gridViewKM.RowCount == 0)
            {
                var format = new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Center, LineAlignment = System.Drawing.StringAlignment.Center };
                e.Graphics.DrawString(LanguageManager.GetString("MSG_KM_GRID_EMPTY") ?? "Chưa có chương trình khuyến mãi nào.", new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Italic), System.Drawing.Brushes.Gray, e.Bounds, format);
            }
        }

        private void GridViewDK_CustomDrawEmptyForeground(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
        {
            if (gridViewDK.RowCount == 0)
            {
                var format = new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Center, LineAlignment = System.Drawing.StringAlignment.Center };
                e.Graphics.DrawString(LanguageManager.GetString("MSG_KM_DK_EMPTY") ?? "Chưa có điều kiện nào.", new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Italic), System.Drawing.Brushes.Gray, e.Bounds, format);
            }
        }

        private void HienThiChiTiet(DTO_KhuyenMaiPOS km)
        {
            if (km == null) return;
            
            txtMaKM.Text = km.MaKhuyenMai;
            txtTenKM.Text = km.TenKhuyenMai;
            cboLoaiGiam.Text = km.LoaiGiamGia;
            spinGiaTri.Value = km.GiaTriGiam;
            dtTuNgay.EditValue = km.NgayBatDau;
            dtDenNgay.EditValue = km.NgayKetThuc;
            spinDonTT.Value = km.DonToiThieu;
            spinSoLan.EditValue = km.SoLanToiDa;
            chkChongCheo.Checked = km.CoChongCheo;
            chkTrangThai.Checked = km.TrangThai;

            gridDK.DataSource = null;
            gridDK.DataSource = km.DieuKiens;
            gridViewDK.RefreshData();
            gridViewDK.BestFitColumns();

            tabDieuKien.Enabled = true;
        }

        private void LamMoiForm()
        {
            _currentKM = null;
            txtMaKM.Text = "";
            txtTenKM.Text = "";
            cboLoaiGiam.SelectedIndex = -1;
            spinGiaTri.Value = 0;
            dtTuNgay.EditValue = DateTime.Now;
            dtDenNgay.EditValue = DateTime.Now.AddMonths(1);
            spinDonTT.Value = 0;
            spinSoLan.EditValue = null;
            chkChongCheo.Checked = false;
            chkTrangThai.Checked = true;

            gridDK.DataSource = null;
            tabDieuKien.Enabled = false;
            txtMaKM.Focus();
        }

        #endregion

        #region Xử lý sự kiện

        private void BtnThemMoi_Click(object sender, EventArgs e)
        {
            LamMoiForm();
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            var dto = new DTO_KhuyenMaiPOS
            {
                Id = _currentKM?.Id ?? 0,
                MaKhuyenMai = txtMaKM.Text.Trim(),
                TenKhuyenMai = txtTenKM.Text.Trim(),
                LoaiGiamGia = cboLoaiGiam.Text,
                GiaTriGiam = spinGiaTri.Value,
                DonToiThieu = spinDonTT.Value,
                NgayBatDau = dtTuNgay.DateTime,
                NgayKetThuc = dtDenNgay.DateTime,
                CoChongCheo = chkChongCheo.Checked,
                TrangThai = chkTrangThai.Checked
            };

            if (spinSoLan.EditValue != null && int.TryParse(spinSoLan.EditValue.ToString(), out int soLan) && soLan > 0)
                dto.SoLanToiDa = soLan;

            OperationResult rs;
            if (dto.Id == 0)
                rs = BUS_KhuyenMai.Instance.ThemKhuyenMai(dto, 1);
            else
                rs = BUS_KhuyenMai.Instance.CapNhatKhuyenMai(dto);

            if (rs.Success)
            {
                int idUser = GUI.Infrastructure.SessionManager.IdDoiTac > 0 ? GUI.Infrastructure.SessionManager.IdDoiTac : 1;
                BUS.Services.HeThong.BUS_NhatKy.Instance.GhiLog("Khuyến Mãi", dto.Id, dto.Id == 0 ? "Thêm mới KM" : "Cập nhật KM", idUser, null, $"Mã: {dto.MaKhuyenMai}, Tên: {dto.TenKhuyenMai}", "Lưu thành công");

                UIHelper.ThongBao(LanguageManager.GetString("MSG_LUU_OK"));
                LoadDuLieu();
            }
            else
            {
                UIHelper.Loi(LanguageManager.GetString(rs.Message));
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (_currentKM == null)
            {
                UIHelper.Loi(LanguageManager.GetString("MSG_KM_CHON_TRUOC"));
                return;
            }

            if (UIHelper.XacNhan(LanguageManager.GetString("MSG_KM_XOA_CONFIRM")))
            {
                var rs = BUS_KhuyenMai.Instance.XoaKhuyenMai(_currentKM.Id);
                if (rs.Success)
                {
                    int idUser = GUI.Infrastructure.SessionManager.IdDoiTac > 0 ? GUI.Infrastructure.SessionManager.IdDoiTac : 1;
                    BUS.Services.HeThong.BUS_NhatKy.Instance.GhiLog("Khuyến Mãi", _currentKM.Id, "Xóa KM", idUser, $"Mã: {_currentKM.MaKhuyenMai}, Tên: {_currentKM.TenKhuyenMai}", null, "Xóa thành công");

                    UIHelper.ThongBao(LanguageManager.GetString("MSG_KM_XOA_OK"));
                    LoadDuLieu();
                }
            }
        }

        private void GridViewKM_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (gridViewKM.FocusedRowHandle < 0) return;
            _currentKM = gridViewKM.GetFocusedRow() as DTO_KhuyenMaiPOS;
            HienThiChiTiet(_currentKM);
        }

        private void GridViewKM_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "TrangThai")
            {
                if (e.CellValue != null && (bool)e.CellValue)
                    e.Appearance.ForeColor = System.Drawing.Color.Green;
                else
                    e.Appearance.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void CboTrangThaiFlt_SelectedIndexChanged(object sender, EventArgs e)
        {
            HienThiGrid();
        }

        private void TxtTimKiem_TextChanged(object sender, EventArgs e)
        {
            HienThiGrid();
        }

        private void BtnThemDK_Click(object sender, EventArgs e)
        {
            if (_currentKM == null) return;
            if (_currentKM.DieuKiens == null) _currentKM.DieuKiens = new List<DTO_DieuKienKM>();
            
            _currentKM.DieuKiens.Add(new DTO_DieuKienKM());
            gridViewDK.RefreshData();
        }

        private void BtnLuuDK_Click(object sender, EventArgs e)
        {
            if (_currentKM == null) return;
            gridViewDK.PostEditor();
            gridViewDK.UpdateCurrentRow();

            var rs = BUS_KhuyenMai.Instance.LuuDieuKien(_currentKM.Id, _currentKM.DieuKiens);
            if (rs.Success)
            {
                UIHelper.ThongBao(LanguageManager.GetString("MSG_LUU_OK"));
            }
            else UIHelper.Loi(LanguageManager.GetString(rs.Message));
        }

        private void BtnXoaDK_Click(object sender, EventArgs e)
        {
            if (_currentKM == null || gridViewDK.FocusedRowHandle < 0) return;
            var dk = gridViewDK.GetFocusedRow() as DTO_DieuKienKM;
            if (dk != null)
            {
                _currentKM.DieuKiens.Remove(dk);
                gridViewDK.RefreshData();
            }
        }

        #endregion
    }
}

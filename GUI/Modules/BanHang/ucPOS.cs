using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraGrid.Views.Grid;
using BUS.Services.BanHang;
using BUS.Services.DoiTac;
using ET.Constants;
using ET.DTOs;
using ET.Models.BanHang;
using ET.Results;
using GUI.AI;
using GUI.Infrastructure;
using BUS.Services.HeThong;

namespace GUI.Modules.BanHang
{
    public partial class ucPOS : DevExpress.XtraEditors.XtraUserControl, IAIModuleContext
    {
        #region Khởi tạo và tải dữ liệu

        // Biến giữ trạng thái phiên
        private ET_PhienThuNgan _phienHienTai;
        private DataTable _dtGioHang;
        private DataTable _dtDanhMuc;
        private readonly BUS_NhatKy _nhatKy = BUS_NhatKy.Instance;

        private CameraScanner _cameraScanner;


        // Khách hàng đang gắn vào đơn (null = khách vãng lai)
        private DTO_KhachHangPOS _khachHangHienTai;

        // Giảm giá: KM manual đang áp + kết quả gộp
        private DTO_KhuyenMaiPOS _kmManual;
        private DTO_KetQuaGiamGia _ketQuaGiamGia;

        private readonly Action<object> _onLanguageChanged;

        public ucPOS()
        {
            InitializeComponent();
            
            _onLanguageChanged = _ => {
                if (this.IsHandleCreated && !this.IsDisposed)
                {
                    this.Invoke((MethodInvoker)delegate {
                        ApplyLanguage();
                        ReloadDynamicData();
                    });
                }
            };
            EventBus.Subscribe("LanguageChanged", _onLanguageChanged);

            KhoiTao();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                MoPhienThuNgan();
                LoadDanhMuc();
                
                txtBarcode.Focus();
                GUI.Infrastructure.AppStyle.AutoStyleButton(btnThanhToan, btnDongPhien, btnHoanTra, btnXoaGio);              
                GUI.Infrastructure.AppStyle.StyleBanner(pnlBanner, lblTitle);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_onLanguageChanged != null)
                {
                    GUI.Infrastructure.EventBus.Unsubscribe("LanguageChanged", _onLanguageChanged);
                }

                if (components != null)
                {
                    components.Dispose();
                }
                
                if (_cameraScanner != null)
                {
                    _cameraScanner.Dispose();
                }
            }
            base.Dispose(disposing);
        }



        #region Camera Scanner
        private void TxtBarcode_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (_cameraScanner == null)
            {
                _cameraScanner = new CameraScanner();
                _cameraScanner.OnBarcodeDetected += CameraScanner_OnBarcodeDetected;
                _cameraScanner.OnError += (err) => DevExpress.XtraEditors.XtraMessageBox.Show(err, "Lỗi Camera");
            }

            var btn = txtBarcode.Properties.Buttons[0];

            if (_cameraScanner.IsRunning)
            {
                _cameraScanner.Stop();
                picCamera.Visible = false;
                btn.Caption = LanguageManager.GetString("POS_BTN_START_CAM") ?? "Bật Cam";
                btn.Appearance.ForeColor = Color.Black;
                btn.Appearance.BackColor = Color.LightGreen;
            }
            else
            {
                picCamera.Visible = true;
                picCamera.BringToFront();
                if (_cameraScanner.Start(picCamera))
                {
                    btn.Caption = LanguageManager.GetString("POS_BTN_STOP_CAM") ?? "Tắt Cam";
                    btn.Appearance.ForeColor = Color.Black;
                    btn.Appearance.BackColor = Color.LightPink;
                }
                else
                {
                    picCamera.Visible = false;
                }
            }
        }

        private void CameraScanner_OnBarcodeDetected(string code)
        {
            txtBarcode.Text = code;
            XuLyBarcode(code);
            txtBarcode.Text = "";
            try { System.Media.SystemSounds.Beep.Play(); } catch { }
        }
        #endregion

        private void KhoiTao()
        {
            CauHinhTileTemplate();
            KhoiTaoGioHang();
            ApplyLanguage();
        }

        private void KhoiTaoGioHang()
        {
            _dtGioHang = new DataTable();
            _dtGioHang.Columns.Add("IdSanPham", typeof(int));
            _dtGioHang.Columns.Add("MaSanPham", typeof(string));
            _dtGioHang.Columns.Add("TenSanPham", typeof(string));
            _dtGioHang.Columns.Add("DonGia", typeof(decimal));
            _dtGioHang.Columns.Add("SoLuong", typeof(decimal));
            _dtGioHang.Columns.Add("ThanhTien", typeof(decimal));
            _dtGioHang.Columns.Add("IdBangGia", typeof(int));
            _dtGioHang.Columns.Add("LaVatTu", typeof(bool));
            _dtGioHang.Columns.Add("LoaiSanPham", typeof(string));
            _dtGioHang.Columns.Add("GhiChu", typeof(string));
            _dtGioHang.Columns.Add("IdCauHinhThue", typeof(int));
            _dtGioHang.Columns.Add("PhanTramThue", typeof(decimal));
            _dtGioHang.Columns.Add("HeSoQuyDoi", typeof(decimal));
            _dtGioHang.Columns.Add("IdDonViTinh", typeof(int));
            _dtGioHang.Columns.Add("TenDonVi", typeof(string));
            _dtGioHang.Columns.Add("IdCombo", typeof(int));
            _dtGioHang.Columns.Add("TenCombo", typeof(string));
            _dtGioHang.Columns.Add("IdQuyenLoiDoan", typeof(int));
            gridGioHang.DataSource = _dtGioHang;
        }

        private void ViewGioHang_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "TenDonVi")
            {
                int.TryParse(viewGioHang.GetRowCellValue(e.RowHandle, "IdSanPham")?.ToString(), out int idSP);
                decimal donGiaGoc = 0;
                
                if (_dtDanhMuc != null)
                {
                    DataRow sp = _dtDanhMuc.AsEnumerable().FirstOrDefault(r => r.Field<int>("Id") == idSP);
                    if (sp != null) donGiaGoc = sp.Field<decimal>("DonGia");
                }

                var dsDonVi = BUS_POS.Instance.LayDonViBanTheoSanPham(idSP, donGiaGoc, SessionManager.CurrentLanguage);
                if (dsDonVi == null) dsDonVi = new List<ET.Models.BanHang.ET_DonViBanPOS>();

                string tenDonViGoc = "Cái";
                if (_dtDanhMuc != null)
                {
                    DataRow sp = _dtDanhMuc.AsEnumerable().FirstOrDefault(r => r.Field<int>("Id") == idSP);
                    if (sp != null && sp.Table.Columns.Contains("TenDonViGoc"))
                    {
                        tenDonViGoc = sp["TenDonViGoc"]?.ToString() ?? "Cái";
                    }
                }
                
                dsDonVi.Insert(0, new ET.Models.BanHang.ET_DonViBanPOS
                {
                    IdDonViDich = 0, TenDonVi = tenDonViGoc, TyLeQuyDoi = 1m, GiaBan = donGiaGoc
                });

                var riCmb = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                riCmb.DataSource = dsDonVi;
                riCmb.DisplayMember = "TenDonVi";
                riCmb.ValueMember = "TenDonVi";
                riCmb.NullText = "";
                riCmb.ShowHeader = true;
                riCmb.ShowFooter = false;
                riCmb.Columns.Clear();
                riCmb.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenDonVi", LanguageManager.GetString("COL_TEN") ?? "Tên ĐVT"));
                riCmb.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GiaBan", LanguageManager.GetString("COL_GIABAN") ?? "Giá Bán") { FormatType = DevExpress.Utils.FormatType.Numeric, FormatString = "N0" });

                e.RepositoryItem = riCmb;
            }
        }



        #endregion


        #region Xử lý sự kiện (Click, SelectedChanged...)

        private void TileViewDanhMuc_ItemClick(object sender, TileViewItemClickEventArgs e)
        {
            // thêm sản phẩm vào giỏ hàng
            if (e.Item == null) return;
            int focusedRow = tileViewDanhMuc.FocusedRowHandle;
            if (focusedRow < 0) return;

            int idSP = Convert.ToInt32(tileViewDanhMuc.GetRowCellValue(focusedRow, "Id"));
            string maSP = tileViewDanhMuc.GetRowCellValue(focusedRow, "MaSanPham")?.ToString() ?? "";
            string tenSP = tileViewDanhMuc.GetRowCellValue(focusedRow, "TenSanPham")?.ToString() ?? "";
            decimal donGia = 0m;
            if (decimal.TryParse(tileViewDanhMuc.GetRowCellValue(focusedRow, "DonGia")?.ToString(), out decimal parsedDonGiaTile))
                donGia = parsedDonGiaTile;
            bool laVatTu = Convert.ToBoolean(tileViewDanhMuc.GetRowCellValue(focusedRow, "LaVatTu"));
            string loaiSP = tileViewDanhMuc.GetRowCellValue(focusedRow, "LoaiSanPham")?.ToString() ?? "";

            if (loaiSP == AppConstants.LoaiSanPham.Combo)
            {
                int idCombo = idSP;
                var res = BUS_POS.Instance.LayChiTietComboPOS(idCombo);
                string tooltipInfo = (LanguageManager.GetString("POS_LBL_COMPONENTS") ?? "Thành phần:") + "\n";
                if (res.Success && res.Data is List<DTO_ComboItemPOS> items)
                {
                    foreach (var it in items)
                    {
                        tooltipInfo += $"- {it.TenSanPham} (x{it.SoLuong})\n";
                    }
                }
                tooltipInfo = tooltipInfo.TrimEnd('\n');

                ThemVaoGioHang(idSP, maSP, tenSP, donGia, laVatTu, loaiSP, null, 0m, 1m, 0, "Combo", 1, null, null, tooltipInfo);
                return;
            }

            object rawThue = tileViewDanhMuc.GetRowCellValue(focusedRow, "IdCauHinhThue");
            int? idThue = rawThue != null && rawThue != DBNull.Value ? Convert.ToInt32(rawThue) : (int?)null;
            decimal phanTramThue = 0m;
            if (decimal.TryParse(tileViewDanhMuc.GetRowCellValue(focusedRow, "PhanTramThue")?.ToString(), out decimal parsedThue))
                phanTramThue = parsedThue;

            decimal heSo = 1m;
            decimal giaThucTe = donGia;
            
            int idDonViGoc = tileViewDanhMuc.GetRowCellValue(focusedRow, "IdDonViGoc") != DBNull.Value ? Convert.ToInt32(tileViewDanhMuc.GetRowCellValue(focusedRow, "IdDonViGoc")) : 0;
            string tenDonVi = tileViewDanhMuc.GetRowCellValue(focusedRow, "TenDonViGoc")?.ToString() ?? "Cái";

            ThemVaoGioHang(idSP, maSP, tenSP, giaThucTe, laVatTu, loaiSP, idThue, phanTramThue, heSo, idDonViGoc, tenDonVi);
        }

        private void TxtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string code = txtBarcode.Text.Trim();
                if (!string.IsNullOrEmpty(code))
                {
                    XuLyBarcode(code);
                    txtBarcode.Text = "";
                }
                e.Handled = true;
            }
        }

        private void TabDanhMuc_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            LocDanhMucTheoTab();
        }

        private void BtnThanhToan_Click(object sender, EventArgs e)
        {
            MoFormThanhToan();
        }

        private void BtnXoaGio_Click(object sender, EventArgs e)
        {
            if (_dtGioHang.Rows.Count == 0) return;

            if (XtraMessageBox.Show(
                LanguageManager.GetString("MSG_POS_CONFIRM_CLEAR_CART") ?? "Bạn có chắc chắn muốn xóa toàn bộ giỏ hàng?",
                "POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            XoaGioHang(true);
        }

        private void BtnDongPhien_Click(object sender, EventArgs e)
        {
            if (_phienHienTai == null)
            {
                XtraMessageBox.Show(
                    LanguageManager.GetString(AppConstants.ErrorMessages.ERR_POS_NO_OPEN_SESSION) ?? "Bạn chưa mở phiên thu ngân nào.",
                    "POS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_dtGioHang != null && _dtGioHang.Rows.Count > 0)
            {
                if (XtraMessageBox.Show(
                    LanguageManager.GetString(AppConstants.ErrorMessages.MSG_POS_CART_NOT_EMPTY) ?? "Giỏ hàng đang có sản phẩm chưa thanh toán. Đóng phiên sẽ hủy toàn bộ giỏ hàng này. Bạn có chắc chắn muốn tiếp tục?",
                    "POS", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    return;
                }
            }

            using (var frm = new frmPhienThuNgan(_phienHienTai, false))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    _phienHienTai = null;
                    CapNhatBanner();
                    XoaGioHang(false);
                }
            }
        }

        private void BtnHoanTra_Click(object sender, EventArgs e)
        {
            if (_phienHienTai == null)
            {
                XtraMessageBox.Show(
                    LanguageManager.GetString(AppConstants.ErrorMessages.ERR_POS_NO_OPEN_SESSION) ?? "Vui lòng mở phiên thu ngân trước.",
                    "POS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var frm = new frmHoanHang(SessionManager.IdDoiTac, _phienHienTai.IdKhoBan ?? 0))
            {
                frm.ShowDialog();
            }
        }

        /// Khi SL thay đổi trên Grid giỏ, tự tính lại Thành tiền. SL = 0 thì xóa dòng.
        private void ViewGioHang_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "SoLuong")
            {
                if (e.Value == null || !decimal.TryParse(e.Value.ToString(), out decimal sl))
                    sl = 0m;
                
                if (sl <= 0)
                {
                    viewGioHang.DeleteRow(e.RowHandle);
                }
                else
                {
                    bool laVatTu = Convert.ToBoolean(viewGioHang.GetRowCellValue(e.RowHandle, "LaVatTu"));
                    if (laVatTu && _phienHienTai != null && _phienHienTai.IdKhoBan.HasValue)
                    {
                        int idSP = Convert.ToInt32(viewGioHang.GetRowCellValue(e.RowHandle, "IdSanPham"));
                        
                        decimal heSo = 1m;
                        if (decimal.TryParse(viewGioHang.GetRowCellValue(e.RowHandle, "HeSoQuyDoi")?.ToString(), out decimal parsedHeSo))
                            heSo = parsedHeSo;
                            
                        decimal tonKhoHienTai = BUS_POS.Instance.GetTonKhoHienTai(_phienHienTai.IdKhoBan.Value, idSP);
                        if (sl * heSo > tonKhoHienTai)
                        {
                            string msg = string.Format(LanguageManager.GetString("ERR_POS_TONKHO_KHONGDU") ?? "Tồn kho không đủ! (Hiện còn: {0:N0})", tonKhoHienTai);
                            XtraMessageBox.Show(msg, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            // Set lại SL tối đa có thể bán
                            viewGioHang.CellValueChanged -= ViewGioHang_CellValueChanged;
                            decimal slMax = tonKhoHienTai / heSo;
                            if (slMax <= 0) viewGioHang.DeleteRow(e.RowHandle);
                            else viewGioHang.SetRowCellValue(e.RowHandle, "SoLuong", slMax);
                            viewGioHang.CellValueChanged += ViewGioHang_CellValueChanged;
                            sl = slMax;
                        }
                    }

                    if (sl > 0)
                    {
                        decimal donGia = 0m;
                        if (decimal.TryParse(viewGioHang.GetRowCellValue(e.RowHandle, "DonGia")?.ToString(), out decimal parsedDonGia))
                            donGia = parsedDonGia;
                            
                        viewGioHang.SetRowCellValue(e.RowHandle, "ThanhTien", sl * donGia);
                    }
                }
                CapNhatTong();
            }
            else if (e.Column.FieldName == "TenDonVi")
            {
                int idSP = Convert.ToInt32(viewGioHang.GetRowCellValue(e.RowHandle, "IdSanPham"));
                string tenDonViMoi = e.Value?.ToString();

                decimal donGiaGoc = 0;
                if (_dtDanhMuc != null)
                {
                    DataRow sp = _dtDanhMuc.AsEnumerable().FirstOrDefault(r => r.Field<int>("Id") == idSP);
                    if (sp != null) donGiaGoc = sp.Field<decimal>("DonGia");
                }

                var dsDonVi = BUS_POS.Instance.LayDonViBanTheoSanPham(idSP, donGiaGoc, SessionManager.CurrentLanguage);
                var donViQuyDoi = dsDonVi.FirstOrDefault(d => string.Equals(d.TenDonVi, tenDonViMoi, StringComparison.OrdinalIgnoreCase));

                decimal heSoMoi = 1m;
                decimal donGiaMoi = donGiaGoc;
                int idDonViTinhMoi = 0;

                if (donViQuyDoi != null)
                {
                    heSoMoi = donViQuyDoi.TyLeQuyDoi;
                    donGiaMoi = donViQuyDoi.GiaBan;
                    idDonViTinhMoi = donViQuyDoi.IdDonViDich;
                }
                else
                {
                    if (_dtDanhMuc != null)
                    {
                        DataRow sp = _dtDanhMuc.AsEnumerable().FirstOrDefault(r => r.Field<int>("Id") == idSP);
                        if (sp != null && sp.Table.Columns.Contains("IdDonViGoc"))
                            idDonViTinhMoi = sp["IdDonViGoc"] != DBNull.Value ? sp.Field<int>("IdDonViGoc") : 0;
                    }
                }

                viewGioHang.SetRowCellValue(e.RowHandle, "HeSoQuyDoi", heSoMoi);
                viewGioHang.SetRowCellValue(e.RowHandle, "DonGia", donGiaMoi);
                viewGioHang.SetRowCellValue(e.RowHandle, "IdDonViTinh", idDonViTinhMoi);

                decimal sl = 0m;
                if (decimal.TryParse(viewGioHang.GetRowCellValue(e.RowHandle, "SoLuong")?.ToString(), out decimal parsedSl))
                    sl = parsedSl;
                    
                viewGioHang.SetRowCellValue(e.RowHandle, "ThanhTien", sl * donGiaMoi);

                DataRow row = viewGioHang.GetDataRow(e.RowHandle);
                if (row != null)
                {
                    row["HeSoQuyDoi"] = heSoMoi;
                    row["DonGia"] = donGiaMoi;
                    row["IdDonViTinh"] = idDonViTinhMoi;
                    row["ThanhTien"] = sl * donGiaMoi;
                }

                CapNhatTong();
            }
        }

        #endregion



        #region Hàm hỗ trợ

        private void ApplyLanguage()
        {
            lblTitle.Text = LanguageManager.GetString("POS_TITLE") ?? "ĐẠI NAM POS";
            CapNhatBanner();

            tabTatCa.Text = LanguageManager.GetString("TAB_ALL") ?? "Tất cả";
            tabVe.Text = LanguageManager.GetString("TAB_TICKET") ?? "Vé";
            tabCombo.Text = LanguageManager.GetString("TAB_COMBO") ?? "Combo";
            tabAnUong.Text = LanguageManager.GetString("TAB_FNB") ?? "F&B";
            tabHangHoa.Text = LanguageManager.GetString("TAB_GOODS") ?? "Hàng hóa/Vật tư";
            txtBarcode.Properties.NullValuePrompt = LanguageManager.GetString("POS_BARCODE_HINT") ?? "Quét mã vạch hoặc gõ tên sản phẩm...";            
            colGH_TenSanPham.Caption = LanguageManager.GetString("POS_COL_PRODUCT") ?? "Sản phẩm";
            colGH_DonGia.Caption = LanguageManager.GetString("COL_DONGIA") ?? "Đơn giá";
            colGH_SoLuong.Caption = LanguageManager.GetString("COL_SOLUONG") ?? "SL";
            colGH_ThanhTien.Caption = LanguageManager.GetString("COL_THANHTIEN") ?? "Thành tiền";
            lblTienHang.Text = LanguageManager.GetString("POS_SUBTOTAL") ?? "Tiền hàng:";
            lblVAT.Text = LanguageManager.GetString("POS_VAT") ?? "Thuế VAT:";
            lblGiamGia.Text = LanguageManager.GetString("POS_DISCOUNT") ?? "Giảm giá:";
            lblTong.Text = LanguageManager.GetString("POS_TOTAL") ?? "TỔNG:";
            btnThanhToan.Text = LanguageManager.GetString("POS_BTN_CHECKOUT") ?? "THANH TOÁN (F2)";
            btnXoaGio.Text = LanguageManager.GetString("POS_BTN_CLEAR") ?? "Xóa giỏ (Esc)";
            btnDongPhien.Text = LanguageManager.GetString("POS_BTN_CLOSE_SESSION") ?? "Đóng phiên (F8)";
            btnXoaKM.Text = LanguageManager.GetString("POS_BTN_REMOVE_KM") ?? "Xóa KM";
            btnHoanTra.Text = LanguageManager.GetString("POS_BTN_REFUND") ?? "Hoàn trả (F9)";
            txtTimKhach.Properties.NullValuePrompt = LanguageManager.GetString("CUST_SEARCH_HINT") ?? "SĐT / Mã KH / Quét RFID...";
            btnTimKhach.Text = LanguageManager.GetString("CUST_BTN_SEARCH") ?? "Tìm";
            btnBoChonKhach.Text = LanguageManager.GetString("CUST_BTN_CLEAR") ?? "X";
        }

        private void CauHinhTileTemplate() // hum hum đáng suy nghĩ, cái đống gì đây
        {
            if (tileViewDanhMuc.Columns["LoaiSanPham_Text"] == null)
            {
                tileViewDanhMuc.Columns.AddVisible("LoaiSanPham_Text");
            }

            string htmlTemplate = tileViewDanhMuc.TileHtmlTemplate.Template;
            if (!string.IsNullOrEmpty(htmlTemplate))
            {
                htmlTemplate = htmlTemplate.Replace(">${LoaiSanPham}<", ">${LoaiSanPham_Text}<");
                tileViewDanhMuc.TileHtmlTemplate.Template = htmlTemplate;
            }
            tileViewDanhMuc.TileTemplate.Clear();
            tileViewDanhMuc.OptionsTiles.ItemSize = new System.Drawing.Size(180, 230);

            tileViewDanhMuc.ItemCustomize += (sender, e) =>
            {
                int rowHandle = e.RowHandle;
                if (rowHandle < 0) return;

                bool laVatTu = Convert.ToBoolean(tileViewDanhMuc.GetRowCellValue(rowHandle, "LaVatTu"));
                if (!laVatTu) return;

                if (_phienHienTai == null || !_phienHienTai.IdKhoBan.HasValue) return;

                int idSP = Convert.ToInt32(tileViewDanhMuc.GetRowCellValue(rowHandle, "Id"));
                decimal tonKho = BUS_POS.Instance.GetTonKhoHienTai(_phienHienTai.IdKhoBan.Value, idSP);

                if (tonKho > 0) return;

                if (e.HtmlElement != null)
                {
                    e.HtmlElement.Style.SetProperty("opacity", "0.5");
                    var badgeEl = e.HtmlElement.FindElementById("badge-het-hang");
                    if (badgeEl != null)
                    {
                        badgeEl.Style.SetProperty("display", "block");
                    }
                }
            };
        }

        // Xử lý nút - + trên cột SL
        private void BtnEditSL_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int rowHandle = viewGioHang.FocusedRowHandle;
            if (rowHandle < 0) return;
            
            decimal slHienTai = 0m;
            if (decimal.TryParse(viewGioHang.GetRowCellValue(rowHandle, "SoLuong")?.ToString(), out decimal parsedSl))
                slHienTai = parsedSl;

            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Minus)
            {
                slHienTai--;
                if (slHienTai <= 0)
                {
                    viewGioHang.DeleteRow(rowHandle);
                    CapNhatTong();
                    return;
                }
            }
            else if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                slHienTai++;
            }

            viewGioHang.SetRowCellValue(rowHandle, "SoLuong", slHienTai);
            
            decimal donGia = 0m;
            if (decimal.TryParse(viewGioHang.GetRowCellValue(rowHandle, "DonGia")?.ToString(), out decimal parsedDonGia))
                donGia = parsedDonGia;
                
            viewGioHang.SetRowCellValue(rowHandle, "ThanhTien", slHienTai * donGia);
            CapNhatTong();
        }

        
        private void RepBtnXoa_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int rowHandle = viewGioHang.FocusedRowHandle;
            if (rowHandle >= 0)
            {
                viewGioHang.DeleteRow(rowHandle);
                CapNhatTong();
            }
        }

        private void ThemVaoGioHang(int idSP, string maSP, string tenSP, decimal donGia, bool laVatTu, string loaiSanPham, int? idCauHinhThue = null, decimal phanTramThue = 0m, decimal heSoQuyDoi = 1m, int idDonViTinh = 0, string tenDonVi = "", int soLuongThem = 1, int? idCombo = null, string tenCombo = null, string ghiChu = "", int? idQuyenLoiDoan = null)
        {
            DataRow existing = null;
            foreach (DataRow r in _dtGioHang.Rows)
            {
                int rIdSP = 0;
                if (r["IdSanPham"] != null && int.TryParse(r["IdSanPham"]?.ToString(), out int parsedIdSP))
                    rIdSP = parsedIdSP;

                int rIdDVT = 0;
                if (r["IdDonViTinh"] != null && int.TryParse(r["IdDonViTinh"]?.ToString(), out int parsedIdDVT))
                    rIdDVT = parsedIdDVT;
                
                int? rIdCombo = null;
                if (r["IdCombo"] != null && r["IdCombo"] != DBNull.Value && int.TryParse(r["IdCombo"]?.ToString(), out int parsedCombo))
                    rIdCombo = parsedCombo;

                int? rIdQuyenLoi = null;
                if (r["IdQuyenLoiDoan"] != null && r["IdQuyenLoiDoan"] != DBNull.Value && int.TryParse(r["IdQuyenLoiDoan"]?.ToString(), out int parsedQL))
                    rIdQuyenLoi = parsedQL;

                if (rIdSP == idSP && rIdDVT == idDonViTinh && rIdCombo == idCombo && rIdQuyenLoi == idQuyenLoiDoan)
                {
                    existing = r;
                    break;
                }
            }

            decimal slCuTheoGoc = 0m;
            if (existing != null)
            {
                decimal exSl = 0m;
                if (decimal.TryParse(existing["SoLuong"]?.ToString(), out decimal parsedExSl))
                    exSl = parsedExSl;
                    
                decimal exHeSo = 1m;
                if (decimal.TryParse(existing["HeSoQuyDoi"]?.ToString(), out decimal parsedExHeSo))
                    exHeSo = parsedExHeSo;
                    
                slCuTheoGoc = exSl * exHeSo;
            }
            decimal slThemTheoGoc = soLuongThem * heSoQuyDoi;

            if (laVatTu && _phienHienTai != null && _phienHienTai.IdKhoBan.HasValue)
            {
                decimal tonKhoHienTai = BUS_POS.Instance.GetTonKhoHienTai(_phienHienTai.IdKhoBan.Value, idSP);
                if (slCuTheoGoc + slThemTheoGoc > tonKhoHienTai)
                {
                    string msg = string.Format(LanguageManager.GetString("ERR_POS_TONKHO_KHONGDU") ?? "Tồn kho không đủ! (Hiện còn: {0:N0})", tonKhoHienTai);
                    XtraMessageBox.Show(msg, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (existing != null)
            {
                decimal slCu = 0m;
                if (decimal.TryParse(existing["SoLuong"]?.ToString(), out decimal parsedSlCu))
                    slCu = parsedSlCu;

                existing["SoLuong"] = slCu + soLuongThem;
                existing["ThanhTien"] = (slCu + soLuongThem) * donGia;
            }
            else
            {
                _dtGioHang.Rows.Add(idSP, maSP, tenSP, donGia, (decimal)soLuongThem, donGia * soLuongThem, DBNull.Value, laVatTu, loaiSanPham, string.IsNullOrEmpty(ghiChu) ? DBNull.Value : (object)ghiChu,
                    idCauHinhThue.HasValue ? (object)idCauHinhThue.Value : DBNull.Value, phanTramThue, heSoQuyDoi, idDonViTinh, tenDonVi,
                    idCombo.HasValue ? (object)idCombo.Value : DBNull.Value, tenCombo ?? (object)DBNull.Value, idQuyenLoiDoan.HasValue ? (object)idQuyenLoiDoan.Value : DBNull.Value);
            }

            CapNhatTong();
        }

        private void XuLyMaBooking(string code)
        {
            var res = BUS.Services.DoiTac.BUS_DoanKhach.Instance.CheckBookingValid(code);
            if (!res.Success)
            {
                XtraMessageBox.Show(res.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dsQuyenLoi = res.Data as List<DTO_QuyenLoiDoan>;
            if (dsQuyenLoi == null || !dsQuyenLoi.Any()) return;

            // Hỏi xác nhận trước khi đưa toàn bộ vào giỏ hàng
            if (XtraMessageBox.Show($"Tìm thấy {dsQuyenLoi.Count} quyền lợi chưa dùng của Booking {code}.\nBạn có muốn đưa vào giỏ hàng với giá 0đ không?", 
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (var ql in dsQuyenLoi)
                {
                    ThemVaoGioHang(ql.IdSanPham, code, ql.TenSanPham, 0m, false, ql.LoaiSanPham, null, 0m, 1m, 0, "Suất", ql.SoLuongConLai, null, null, $"BK: {code}", ql.IdQuyenLoi);
                }
                CapNhatTong();
                gridGioHang.RefreshDataSource();
            }
        }

        private void XuLyBarcode(string code)
        {
            if (code.StartsWith("RF-", StringComparison.OrdinalIgnoreCase))
            {
                string maThe = code.Substring(3);
                var result = BUS_KhachHang.Instance.TimTheoRFID(maThe);
                if (result.Success && result.Data != null)
                {
                    GanKhachHang(result.Data as DTO_KhachHangPOS);
                }
                else
                {
                    XtraMessageBox.Show(
                        LanguageManager.GetString(result.Message) ?? result.Message, "POS");
                }
                return;
            }

            if (code.StartsWith("KM-", StringComparison.OrdinalIgnoreCase))
            {
                XuLyMaKhuyenMai(code);
                return;
            }

            if (code.StartsWith("BK-", StringComparison.OrdinalIgnoreCase))
            {
                XuLyMaBooking(code);
                return;
            }

            if (_dtDanhMuc == null) return;

            // Xử lý cú pháp SL*Mã (ví dụ: 5*VE123)
            int soLuongThem = 1;
            string searchCode = code;
            if (code.Contains("*"))
            {
                var parts = code.Split('*');
                if (parts.Length == 2 && int.TryParse(parts[0], out int sl) && sl > 0)
                {
                    soLuongThem = sl;
                    searchCode = parts[1];
                }
            }

            // 1. Tìm theo MaSanPham (barcode chứa mã SP)
            DataRow sp = _dtDanhMuc.AsEnumerable()
                .FirstOrDefault(r => string.Equals(r["MaSanPham"]?.ToString(), searchCode, StringComparison.OrdinalIgnoreCase));

            // 2. Tìm theo Id (barcode chứa số Id)
            if (sp == null && int.TryParse(searchCode, out int idParsed))
            {
                sp = _dtDanhMuc.AsEnumerable()
                    .FirstOrDefault(r => r.Field<int>("Id") == idParsed);
            }

            // 3.  tìm theo tên sản phẩm (thu ngân gõ tay)
            if (sp == null)
            {
                var ketQua = _dtDanhMuc.AsEnumerable()
                    .Where(r => r["TenSanPham"].ToString().IndexOf(searchCode, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                if (ketQua.Count == 1)
                {
                    sp = ketQua[0];
                }
                else if (ketQua.Count > 1)
                {
                    // Nhiều kết quả -> focus vào tile danh mục để thu ngân chọn
                    string msg = string.Format(
                        LanguageManager.GetString(AppConstants.ErrorMessages.MSG_POS_MULTI_RESULT) ?? "Tìm thấy {0} sản phẩm chứa \"{1}\". Vui lòng chọn trên danh mục.",
                        ketQua.Count, code);
                    XtraMessageBox.Show(msg, "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (sp != null)
            {
                ThemSanPhamTuDataRow(sp, soLuongThem);
            }
            else
            {
                XtraMessageBox.Show(
                    string.Format(LanguageManager.GetString(AppConstants.ErrorMessages.ERR_POS_PRODUCT_NOT_FOUND) ?? "Không tìm thấy sản phẩm: \"{0}\"", code),
                    "POS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ThemSanPhamTuDataRow(DataRow sp, int soLuongThem = 1)
        {
            int idSP = sp.Field<int>("Id");
            string maSP = sp["MaSanPham"].ToString();
            string tenSP = sp["TenSanPham"].ToString();
            decimal donGia = sp.Field<decimal>("DonGia");
            bool laVatTu = sp.Field<bool>("LaVatTu");
            string loaiSP = sp["LoaiSanPham"].ToString();
            int? idThue = sp["IdCauHinhThue"] != DBNull.Value ? sp.Field<int>("IdCauHinhThue") : (int?)null;
            decimal phanTramThue = sp["PhanTramThue"] != DBNull.Value ? sp.Field<decimal>("PhanTramThue") : 0m;
            int idDonViGoc = sp.Table.Columns.Contains("IdDonViGoc") && sp["IdDonViGoc"] != DBNull.Value ? sp.Field<int>("IdDonViGoc") : 0;
            string tenDonVi = sp.Table.Columns.Contains("TenDonViGoc") && sp["TenDonViGoc"] != null ? sp["TenDonViGoc"].ToString() : "Cái";
            
            ThemVaoGioHang(idSP, maSP, tenSP, donGia, laVatTu, loaiSP, idThue, phanTramThue, 1m, idDonViGoc, tenDonVi, soLuongThem);
        }

        /// <summary>
        /// Tính lại tổng tiền hàng, giảm giá và tổng thanh toán.
        /// Gọi BUS_POS.TinhTongGiamGia để gộp 3 nguồn: CK hạng + KM + điểm.
        /// </summary>
        private void CapNhatTong()
        {
            decimal tongHang = _dtGioHang.AsEnumerable()
                .Sum(r => r.Field<decimal>("ThanhTien"));

            // Tính tiền được giảm: chỉ các dòng KHÔNG phải F&B
            decimal tongDuocGiam = _dtGioHang.AsEnumerable()
                .Where(r => ProductTypeHelper.IsDiscountable(r.Field<string>("LoaiSanPham")))
                .Sum(r => r.Field<decimal>("ThanhTien"));

            // Tính VAT từ PhanTramThue của từng dòng sản phẩm
            decimal tongVAT = _dtGioHang.AsEnumerable()
                .Sum(r => r.Field<decimal>("ThanhTien") * (r.Field<decimal>("PhanTramThue") / 100m));
            tongVAT = Math.Round(tongVAT, 0);

            // Gọi BUS_POS gộp giảm giá (CK hạng + KM manual + Auto Promo)
            string hangKH = _khachHangHienTai?.HangThanhVien;
            _ketQuaGiamGia = BUS_POS.Instance.TinhTongGiamGia(hangKH, tongDuocGiam, tongHang, _kmManual);
            decimal tongGiam = _ketQuaGiamGia.TongGiamGia;

            lblTienHangValue.Text = tongHang.ToString("#,##0");
            lblVATValue.Text = tongVAT.ToString("#,##0");
            lblGiamGiaValue.Text = tongGiam > 0 ? $"-{tongGiam:#,##0}" : "0";
            lblTongValue.Text = (tongHang + tongVAT - tongGiam).ToString("#,##0") + " ₫";

            // Mô tả nguồn giảm giá và Gợi ý Up-sale
            string moTa = _ketQuaGiamGia.MoTa ?? "";
            var hint = BUS_KhuyenMai.Instance.GetPromotionHint(tongHang, hangKH);
            if (hint != null && _ketQuaGiamGia.DanhSachKMApDung.Count == 0)
            {
                decimal canThem = hint.DonToiThieu - tongHang;
                string hintGoiY = LanguageManager.GetString("MSG_HINT_KHUYEN_MAI") ?? " Gợi ý: Mua thêm {0:N0}đ để giảm {1:N0}{2}";
                string unit = hint.LoaiGiamGia == AppConstants.LoaiGiamGia.PhanTram ? "%" : "đ";
                string txtHint = string.Format(hintGoiY, canThem, hint.GiaTriGiam, unit);
                moTa = string.IsNullOrEmpty(moTa) ? txtHint : (moTa + "\n" + txtHint);
            }

            lblMoTaGiamGia.Text = moTa;
            lblMoTaGiamGia.Visible = !string.IsNullOrEmpty(moTa);
            btnXoaKM.Visible = _kmManual != null;
        }

        private void XoaGioHang(bool isHuy = true)
        {
            if (isHuy && _dtGioHang.Rows.Count > 0)
                _nhatKy.GhiLog("POS", _phienHienTai?.Id ?? 0, "HuyGioHang", SessionManager.IdDoiTac, null, null);

            _dtGioHang.Rows.Clear();
            _kmManual = null;
            _ketQuaGiamGia = null;
            CapNhatTong();
        }

        //  Mở form thanh toán 
        private void MoFormThanhToan()
        {
            if (_phienHienTai == null)
            {
                XtraMessageBox.Show(
                    LanguageManager.GetString(AppConstants.ErrorMessages.ERR_POS_NO_OPEN_SESSION) ?? "Vui lòng mở phiên thu ngân trước.",
                    "POS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_dtGioHang.Rows.Count == 0)
            {
                XtraMessageBox.Show(
                    LanguageManager.GetString(AppConstants.ErrorMessages.ERR_POS_CART_EMPTY) ?? "Giỏ hàng đang rỗng.",
                    "POS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal tongHang = _dtGioHang.AsEnumerable().Sum(r => r.Field<decimal>("ThanhTien"));
            decimal tongVAT = _dtGioHang.AsEnumerable()
                .Sum(r => r.Field<decimal>("ThanhTien") * (r.Field<decimal>("PhanTramThue") / 100m));
            tongVAT = Math.Round(tongVAT, 0);
            decimal tongGiam = _ketQuaGiamGia?.TongGiamGia ?? 0;
            decimal tongThanhToan = tongHang + tongVAT - tongGiam;

            bool hasDiscount = tongGiam > 0;
            using (var frm = new frmThanhToan(tongThanhToan, _khachHangHienTai?.DiemTichLuy ?? 0, hasDiscount))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var cart = TaoCartSession(frm.DanhSachThanhToan, tongHang, tongGiam, tongThanhToan);
                    int idKhoBan = _phienHienTai.IdKhoBan ?? 0;

                    var result = BUS_POS.Instance.ThanhToanDonHang(cart, idKhoBan);

                    if (result.Success)
                    {
                        var dto = result.Data as DTO_CheckoutResult;
                        
                        using (var frmResult = new frmKetQuaThanhToan(dto))
                        {
                            frmResult.ShowDialog();
                        }

                        HienThiToastThanhCong(dto);
                        
                        _nhatKy.GhiLog("POS", _phienHienTai?.Id ?? 0, "ThanhToan", SessionManager.IdDoiTac, null, cart.MaDonHang);
                        EventBus.Publish("OrderCompleted", dto);
                        
                        XoaGioHang(false);
                        ResetKhachHang();
                        txtBarcode.Focus();
                    }
                    else
                    {
                        XtraMessageBox.Show(
                            LanguageManager.GetString(result.Message) ?? result.Message,
                            "POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private ET_CartSession TaoCartSession(List<ET_PaymentLine> danhSachThanhToan, decimal tongTienHang, decimal tongGiamGia, decimal tongThanhToan)
        {
            var cart = new ET_CartSession
            {
                MaDonHang = BUS_POS.Instance.SinhMaDonHang(),
                IdNhanVien = _phienHienTai.IdNhanVien,
                IdPhienThuNgan = _phienHienTai.Id,
                IdKhachHang = _khachHangHienTai?.IdDoiTac,
                TongGiamGia = tongGiamGia,
                DanhSachKMApDung = _kmManual != null ? new List<ET.DTOs.DTO_KhuyenMaiPOS> { _kmManual } : new List<ET.DTOs.DTO_KhuyenMaiPOS>(),
                SoTienGiamKM = _kmManual?.SoTienGiamThucTe ?? 0,
                DanhSachThanhToan = danhSachThanhToan,
                DanhSachDong = new List<ET_CartItem>()
            };

            foreach (DataRow r in _dtGioHang.Rows)
            {
                decimal cartSl = 0m;
                if (decimal.TryParse(r["SoLuong"]?.ToString(), out decimal parsedCartSl))
                    cartSl = parsedCartSl;
                    
                decimal cartDonGia = 0m;
                if (decimal.TryParse(r["DonGia"]?.ToString(), out decimal parsedCartDonGia))
                    cartDonGia = parsedCartDonGia;
                    
                decimal cartPhanTramThue = 0m;
                if (r["PhanTramThue"] != DBNull.Value && decimal.TryParse(r["PhanTramThue"]?.ToString(), out decimal pt))
                    cartPhanTramThue = pt;
                    
                decimal cartHeSoQuyDoi = 1m;
                if (r["HeSoQuyDoi"] != DBNull.Value && decimal.TryParse(r["HeSoQuyDoi"]?.ToString(), out decimal hs))
                    cartHeSoQuyDoi = hs;
                    
                cart.DanhSachDong.Add(new ET_CartItem
                {
                    IdSanPham = r.Field<int>("IdSanPham"),
                    TenSanPham = r["TenSanPham"]?.ToString(),
                    SoLuong = cartSl,
                    DonGiaThucTe = cartDonGia,
                    IdBangGia = r["IdBangGia"] != DBNull.Value ? r.Field<int>("IdBangGia") : (int?)null,
                    IdCauHinhThue = r["IdCauHinhThue"] != DBNull.Value ? r.Field<int>("IdCauHinhThue") : (int?)null,
                    PhanTramThue = cartPhanTramThue,
                    LaVatTu = Convert.ToBoolean(r["LaVatTu"]),
                    LoaiSanPham = r["LoaiSanPham"]?.ToString(),
                    HeSoQuyDoi = cartHeSoQuyDoi,
                    IdCombo = r["IdCombo"] != DBNull.Value ? r.Field<int>("IdCombo") : (int?)null,
                    TenCombo = r["TenCombo"]?.ToString(),
                    GhiChu = r["GhiChu"]?.ToString(),
                    IdQuyenLoiDoan = r["IdQuyenLoiDoan"] != DBNull.Value ? r.Field<int>("IdQuyenLoiDoan") : (int?)null
                });
            }

            return cart;
        }

        #region Khách hàng 

        private void TxtTimKhach_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnTimKhach_Click(sender, e);
                e.Handled = true;
            }
        }

        private void BtnTimKhach_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKhach.Text.Trim();
            if (string.IsNullOrEmpty(tuKhoa)) return;

            var result = BUS_KhachHang.Instance.TimKhachHang(tuKhoa, SessionManager.CurrentLanguage);
            if (result.Success && result.Data != null)
            {
                GanKhachHang(result.Data as DTO_KhachHangPOS);
            }
            else
            {
                XtraMessageBox.Show(
                    LanguageManager.GetString(result.Message) ?? result.Message,
                    "POS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnBoChonKhach_Click(object sender, EventArgs e)
        {
            ResetKhachHang();
        }

        private void ResetKhachHang()
        {
            _khachHangHienTai = null;
            lblTenKhach.Text = "";
            lblHangKhach.Text = "";
            lblDiemKhach.Text = "";
            lblViKhach.Text = "";
            btnBoChonKhach.Visible = false;
            txtTimKhach.Text = "";
            txtTimKhach.Enabled = true;
            btnTimKhach.Enabled = true;

            CapNhatTong();
            txtBarcode.Focus();
        }

        /// <summary>
        /// Gắn khách hàng vào phiên POS. Hiện thông tin tên, hạng, điểm.
        /// </summary>
        private void GanKhachHang(DTO_KhachHangPOS kh)
        {
            if (kh == null) return;
            _khachHangHienTai = kh;

            lblTenKhach.Text = kh.HoTen ?? kh.MaKhachHang;

            lblHangKhach.Text = kh.TenHang ?? "";

            // Hiển thị điểm
            string diemLabel = LanguageManager.GetString("CUST_LBL_POINTS") ?? "điểm";
            lblDiemKhach.Text = kh.DiemTichLuy > 0
                ? $" {kh.DiemTichLuy:N0} {diemLabel}"
                : "";

            // Hiển thị số dư ví RFID
            lblViKhach.Text = kh.IdViDienTu.HasValue
                ? $" {kh.SoDuVi:N0} đ"
                : "";

            // Style thẻ tên
            lblTenKhach.Appearance.ForeColor = System.Drawing.Color.FromArgb(30, 60, 100);
            lblHangKhach.Text = string.IsNullOrEmpty(kh.TenHang) ? "" : $"🏅 {kh.TenHang}";

            // Khóa ô tìm, hiện nút X
            txtTimKhach.Text = kh.DienThoai ?? kh.MaKhachHang;
            txtTimKhach.Enabled = false;
            btnTimKhach.Enabled = false;
            btnBoChonKhach.Visible = true;

            // Gắn khách xong -> tính lại giảm giá (CK hạng có thể được áp)
            CapNhatTong();
            txtBarcode.Focus();
        }

        #endregion

        #region Khuyến mãi 

        /// Xử lý khi quét mã KM-xxx: gọi BUS_KhuyenMai validate, lưu _kmManual, cập nhật tổng.
        private void XuLyMaKhuyenMai(string maKM)
        {
            decimal tongHang = _dtGioHang.AsEnumerable()
                .Sum(r => r.Field<decimal>("ThanhTien"));
            decimal tongDuocGiam = _dtGioHang.AsEnumerable()
                .Where(r => ProductTypeHelper.IsDiscountable(r.Field<string>("LoaiSanPham")))
                .Sum(r => r.Field<decimal>("ThanhTien"));

            string hangKH = _khachHangHienTai?.HangThanhVien;

            var result = BUS_KhuyenMai.Instance.ApDungMa(maKM, tongDuocGiam, hangKH);
            if (result.Success && result.Data != null)
            {
                _kmManual = result.Data as DTO_KhuyenMaiPOS;
                CapNhatTong();

                string msgOk = string.Format(
                    LanguageManager.GetString(AppConstants.ErrorMessages.MSG_KM_APPLIED) ?? "Đã áp mã {0}: giảm {1:#,##0}₫",
                    _kmManual.MaKhuyenMai, _kmManual.SoTienGiamThucTe);
                XtraMessageBox.Show(msgOk, "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(
                    LanguageManager.GetString(result.Message) ?? result.Message,
                    "POS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// Bấm nút Xóa KM: bỏ mã KM manual, tính lại tổng
        private void BtnXoaKM_Click(object sender, EventArgs e)
        {
            _kmManual = null;
            CapNhatTong();
        }

        #endregion

        //   thông báo sau checkout thành công 
        private void HienThiToastThanhCong(DTO_CheckoutResult dto)
        {
            if (dto == null) return;
            string toastTitle = LanguageManager.GetString(AppConstants.ErrorMessages.MSG_POS_TOAST_TITLE) ?? "Thanh toán thành công";
            string changeLbl = LanguageManager.GetString(AppConstants.ErrorMessages.MSG_POS_TOAST_CHANGE) ?? "Thừa";
            string ticketLbl = LanguageManager.GetString(AppConstants.ErrorMessages.MSG_POS_TOAST_TICKETS) ?? "Vé";

            string msg = $"{dto.MaDonHang}\n{changeLbl}: {dto.TienThua:#,##0}₫";
            if (dto.DanhSachMaVachVe != null && dto.DanhSachMaVachVe.Count > 0)
                msg += $"\n{ticketLbl}: {dto.DanhSachMaVachVe.Count} QR";

            DevExpress.XtraBars.Alerter.AlertControl alert = new DevExpress.XtraBars.Alerter.AlertControl();
            alert.AutoFormDelay = 3000;
            alert.Show(this.FindForm(), toastTitle, msg);
        }

        //  Cập nhật Banner khi phiên thay đổi 
        private void CapNhatBanner()
        {
            string lblCsh = LanguageManager.GetString("POS_LBL_CASHIER") ?? "Thu ngân";
            string lblShf = LanguageManager.GetString("POS_LBL_SHIFT") ?? "Ca";
            string lblSes = LanguageManager.GetString("POS_LBL_SESSION") ?? "Phiên";
            string lblMac = LanguageManager.GetString("POS_LBL_MACHINE") ?? "Máy";

            if (_phienHienTai != null)
            {
                lblThuNgan.Text = $"{lblCsh}: NV#{_phienHienTai.IdNhanVien}";
                lblCa.Text = $"{lblShf}: #{_phienHienTai.Id}"; 
                lblStatus.Text = $"{lblSes} #{_phienHienTai.Id} | {lblMac}: {_phienHienTai.IdMayBan} | {DateTime.Now:dd/MM/yyyy HH:mm}";
            }
            else
            {
                lblThuNgan.Text = $"{lblCsh}: ---";
                lblCa.Text = $"{lblShf}: ---";
                lblStatus.Text = LanguageManager.GetString("POS_NO_SESSION") ?? "Chưa mở phiên thu ngân";
            }
        }

        //  Lọc danh mục theo tab đang chọn 
        private void LocDanhMucTheoTab()
        {
            if (_dtDanhMuc == null) return;
            string selectedTab = tabDanhMuc.SelectedTabPage?.Name;
            DataView dv = _dtDanhMuc.DefaultView;

            if (selectedTab == "tabVe")
                dv.RowFilter = $"LoaiSanPham = '{AppConstants.LoaiSanPham.VeVaoKhu}' OR LoaiSanPham = '{AppConstants.LoaiSanPham.VeTroChoi}'";
            else if (selectedTab == "tabCombo")
                dv.RowFilter = $"LoaiSanPham = '{AppConstants.LoaiSanPham.Combo}'";
            else if (selectedTab == "tabAnUong")
                dv.RowFilter = $"LoaiSanPham = '{AppConstants.LoaiSanPham.AnUong}' OR LoaiSanPham = '{AppConstants.LoaiSanPham.DoUong}'";
            else if (selectedTab == "tabHangHoa")
                dv.RowFilter = $"LoaiSanPham = '{AppConstants.LoaiSanPham.HangHoa}' OR LoaiSanPham = '{AppConstants.LoaiSanPham.TuDo}'";
            else
                dv.RowFilter = ""; 

            gridDanhMuc.DataSource = dv;
        }

        //  Load danh mục sản phẩm từ BUS 
        public void LoadDanhMuc()
        {
            int? idDiemBan = null;
            if (_phienHienTai != null && !string.IsNullOrEmpty(_phienHienTai.IdMayBan))
            {
                idDiemBan = BUS.Services.BanHang.BUS_DiemBanHang.Instance.GetIdDiemBanByMa(_phienHienTai.IdMayBan);
            }

            var result = BUS_POS.Instance.LayDanhSachSanPhamPOS(idDiemBan, SessionManager.CurrentLanguage);
            if (result.Success && result.Data != null)
            {
                _dtDanhMuc = ConvertToDataTable(result.Data);

                // Load Combo và gộp vào Danh mục
                var resCombo = BUS_POS.Instance.LayDanhSachComboPOS();
                if (resCombo.Success && resCombo.Data is List<DTO_ComboPOS> comboList)
                {
                    foreach (var c in comboList)
                    {
                        var row = _dtDanhMuc.NewRow();
                        row["Id"] = c.Id;
                        row["MaSanPham"] = c.MaCombo;
                        row["TenSanPham"] = c.TenCombo;
                        row["DonGia"] = c.GiaCombo;
                        row["LoaiSanPham"] = AppConstants.LoaiSanPham.Combo;
                        row["LaVatTu"] = false;
                        row["LoaiSanPham_Text"] = "Combo";
                        _dtDanhMuc.Rows.Add(row);
                    }
                }

                gridDanhMuc.DataSource = _dtDanhMuc;           
                tileViewDanhMuc.TileTemplate.Clear();
            }
        }

        //  Mở phiên thu ngân khi lần đầu vào POS 
        public void MoPhienThuNgan()
        {
            // Kiểm tra xem nhân viên có phiên đang mở không
            var resultPhien = BUS_PhienThuNgan.Instance.LayPhienDangMo(SessionManager.IdDoiTac);
            if (resultPhien.Success && resultPhien.Data != null)
            {
                _phienHienTai = resultPhien.Data as ET_PhienThuNgan;
                CapNhatBanner();
                return;
            }

            // Chưa có phiên -> Mở dialog
            using (var frm = new frmPhienThuNgan(null, true))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    _phienHienTai = frm.PhienKetQua;
                    CapNhatBanner();
                }
            }
        }

        //  Phím tắt: F2 = Thanh toán, F8 = Đóng phiên, Esc = Xóa giỏ, F4 = Lịch sử, F5 = Nạp Ví 
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F2)
            {
                BtnThanhToan_Click(this, EventArgs.Empty);
                return true;
            }
            if (keyData == Keys.Escape)
            {
                BtnXoaGio_Click(this, EventArgs.Empty);
                return true;
            }
            if (keyData == Keys.F8)
            {
                BtnDongPhien_Click(this, EventArgs.Empty);
                return true;
            }
            if (keyData == Keys.F9)
            {
                BtnHoanTra_Click(this, EventArgs.Empty);
                return true;
            }
            if (keyData == Keys.F4)
            {
                MoLichSuDonHang();
                return true;
            }
            if (keyData == Keys.F5)
            {
                MoNapViRFID();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private DataTable ConvertToDataTable(object data)
        {
            var dt = new DataTable();
            var list = data as System.Collections.IList;
            if (list == null || list.Count == 0)
            {
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("MaSanPham", typeof(string));
                dt.Columns.Add("TenSanPham", typeof(string));
                dt.Columns.Add("LoaiSanPham", typeof(string));
                dt.Columns.Add("DonGia", typeof(decimal));
                dt.Columns.Add("IdBangGia", typeof(int));
                dt.Columns.Add("IdCauHinhThue", typeof(int));
                dt.Columns.Add("PhanTramThue", typeof(decimal));
                dt.Columns.Add("LaVatTu", typeof(bool));
                dt.Columns.Add("AnhDaiDien", typeof(string));
                dt.Columns.Add("IdDonViGoc", typeof(int));
                dt.Columns.Add("TenDonViGoc", typeof(string));
                dt.Columns.Add("LoaiSanPham_Text", typeof(string));
                return dt;
            }

            var props = list[0].GetType().GetProperties();
            foreach (var prop in props)
            {
                var colType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                dt.Columns.Add(prop.Name, colType);
            }
            
            dt.Columns.Add("LoaiSanPham_Text", typeof(string));

            foreach (var item in list)
            {
                var row = dt.NewRow();
                string loaiSpRaw = null;

                foreach (var prop in props)
                {
                    object val = prop.GetValue(item);
                    row[prop.Name] = val ?? DBNull.Value;
                    if (prop.Name == "LoaiSanPham") loaiSpRaw = val?.ToString();
                }

                string badgeKey = "";
                if (loaiSpRaw == AppConstants.LoaiSanPham.AnUong || loaiSpRaw == AppConstants.LoaiSanPham.DoUong) badgeKey = "POS_TAB_FNB"; 
                else if (loaiSpRaw == AppConstants.LoaiSanPham.VeVaoKhu || loaiSpRaw == AppConstants.LoaiSanPham.VeTroChoi) badgeKey = "POS_TAB_TICKET";
                else badgeKey = "POS_TAB_GOODS";
                
                if (loaiSpRaw == "DoUong") row["LoaiSanPham_Text"] = LanguageManager.GetString("POS_TAB_DRINKS") ?? LanguageManager.GetString("POS_TAB_FNB") ?? "Đồ uống";
                else row["LoaiSanPham_Text"] = LanguageManager.GetString(badgeKey) ?? loaiSpRaw;

                dt.Rows.Add(row);
            }

            return dt;
        }

        private void ReloadDynamicData()
        {
            LoadDanhMuc();
            LocDanhMucTheoTab();
        }

        #endregion

        private void MoNapViRFID()
        {
            var frm = new GUI.Modules.TaiChinh.frmNapViRFID("", SessionManager.IdDoiTac);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                // Refresh if the current customer was topped up
                if (_khachHangHienTai != null)
                {
                    var res = BUS.Services.TaiChinh.BUS_ViDienTu.Instance.TraCuuViTheoMaThe(_khachHangHienTai.DienThoai ?? _khachHangHienTai.MaKhachHang);
                    if (res.Success)
                    {
                        var data = res.Data as System.Collections.Generic.Dictionary<string, object>;
                        if (data != null)
                        {
                            _khachHangHienTai.IdViDienTu = (int)data["IdVi"];
                            _khachHangHienTai.SoDuVi = (decimal)data["SoDuVi"];
                            GanKhachHang(_khachHangHienTai);
                        }
                    }
                }
            }
        }

        private void MoLichSuDonHang()
        {
            if (_phienHienTai == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(LanguageManager.GetString("POS_NO_SESSION") ?? "Chưa mở phiên thu ngân!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var frm = new frmLichSuDonHang(_phienHienTai.Id);
            frm.ShowDialog();
        }

        private void btnDongPhien_Click_1(object sender, EventArgs e)
        {
            BtnDongPhien_Click(this, EventArgs.Empty);
        }

        #region AI Integration

        public string AIContextName => "POS_BAN_LE";
        public string AIContextDescription => "Quầy bán hàng POS: quét barcode, giỏ hàng, thanh toán";
        public string[] SuggestedQuestions => new[] { 
            LanguageManager.GetString("AI_SUG_POS_1") ?? "Hướng dẫn thanh toán", 
            LanguageManager.GetString("AI_SUG_POS_2") ?? "Cách quét thẻ RFID khách hàng", 
            LanguageManager.GetString("AI_SUG_POS_3") ?? "Mở phiên thu ngân" 
        };
        public string[] FilterableColumns => new[] { "TenSanPham", "MaSanPham", "DonGia", "LoaiSanPham" };

        #endregion
    }
}

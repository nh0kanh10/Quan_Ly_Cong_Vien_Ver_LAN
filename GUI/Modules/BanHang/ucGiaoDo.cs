using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BUS.Services.BanHang;
using BUS.Services.DoiTac;
using BUS.Services.Kho;
using ET.Constants;
using ET.DTOs;
using GUI.Infrastructure;

namespace GUI.Modules.BanHang
{
    public partial class ucGiaoDo : DevExpress.XtraEditors.XtraUserControl
    {
        #region Khởi tạo và tải dữ liệu

        private List<DTO_NguonChoThueView> _dsNguonChoThue;
        private List<DTO_RentalCartItem> _gioThue = new List<DTO_RentalCartItem>();
        private DTO_KhachHangPOS _khachHang;
        private int? _idViDienTu;
        private readonly Action<object> _onLanguageChanged;

        // Id phiên thu ngân hiện tại, nhận từ ucQuanLyThueDo cha.
        // Gắn vào DTO_RentalCheckoutRequest khi giao đồ.
        public int IdPhienThuNgan { get; set; }
        
        // IdDiemBan nhận từ ucQuanLyThueDo cha để biết đang ở máy POS nào.
        public int IdDiemBan { get; set; }

        public ucGiaoDo()
        {
            InitializeComponent();

            _onLanguageChanged = _ =>
            {
                if (this.IsHandleCreated && !this.IsDisposed)
                {
                    this.Invoke((MethodInvoker)delegate { ApplyLanguage(); });
                }
            };
            EventBus.Subscribe("LanguageChanged", _onLanguageChanged);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                AppStyle.StyleForm(this);
                AppStyle.StyleBanner(pnlBanner, lblTitle);
                AppStyle.StyleGrid(viewSanPham);
                AppStyle.StyleGrid(viewGioThue);
                AppStyle.StyleGrid(viewChuaTra);
                AppStyle.AutoStyleButton(btnRFID, btnTienMat, btnHuy, btnRefresh);
                ApplyLanguage();
                LoadKhoXuat();
                LoadSanPham();
                CapNhatGioThue();
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

        // Load danh sách kho đang hoạt động vào dropdown chọn kho.
        private void LoadKhoXuat()
        {
            var dsKho = BUS_Kho.Instance.GetKhoHoatDong(SessionManager.CurrentLanguage);
            slkKho.Properties.DataSource = dsKho;
            if (dsKho.Count > 0)
                slkKho.EditValue = dsKho[0].Id;
        }

        private void LoadSanPham()
        {
            if (IdDiemBan <= 0) return;

            var result = BUS_ThueDo.Instance.LayDanhSachNguonChoThue(IdDiemBan, SessionManager.CurrentLanguage);
            if (result.Success && result.Data is List<DTO_NguonChoThueView> ds)
            {
                _dsNguonChoThue = ds;
                gridSanPham.DataSource = _dsNguonChoThue;
            }
        }

        #endregion

        #region Xử lý sự kiện (Click, SelectedChanged...)

        private void SlkKho_EditValueChanged(object sender, EventArgs e)
        {
            LoadSanPham();
        }

        private void ViewSanPham_DoubleClick(object sender, EventArgs e)
        {
            int row = viewSanPham.FocusedRowHandle;
            if (row < 0) return;

            var sp = viewSanPham.GetRow(row) as DTO_NguonChoThueView;
            if (sp == null) return;

            // Nếu là tài sản định danh (IsDinhDanh = true) -> add thẳng vào giỏ số lượng 1
            // Nếu không định danh (Phao/Khăn) -> mở hộp thoại nhập số lượng
            int soLuong = 1;
            if (!sp.IsDinhDanh)
            {
                // Sử dụng XtraInputBox để nhập số lượng 
                string input = XtraInputBox.Show(
                    LanguageManager.GetString("RENTAL_ENTER_QUANTITY") ?? "Nhập số lượng:", 
                    LanguageManager.GetString("RENTAL_QUANTITY_TITLE") ?? "Số Lượng", 
                    "1");

                if (string.IsNullOrEmpty(input)) return; //bấm Cancel
                
                if (!int.TryParse(input, out soLuong) || soLuong <= 0)
                {
                    XtraMessageBox.Show(LanguageManager.GetString("ERR_INVALID_QUANTITY") ?? "Số lượng không hợp lệ!", LanguageManager.GetString("TITLE_ERROR") ?? "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            ThemVaoGio(sp.IdSanPham, sp.TenHienThi, sp.TienThue, sp.TienCoc, sp.IdTaiSanChoThue, sp.IsDinhDanh ? sp.MaHienThi : null, soLuong);
        }

        private void TxtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            string code = txtBarcode.Text.Trim();
            if (string.IsNullOrEmpty(code)) return;

            // Thử quét RFID khách trước
            if (code.StartsWith("RF-", StringComparison.OrdinalIgnoreCase))
            {
                string maThe = code.Substring(3);
                var result = BUS_KhachHang.Instance.TimTheoRFID(maThe, SessionManager.CurrentLanguage);
                if (result.Success && result.Data is DTO_KhachHangPOS kh)
                    GanKhachHang(kh);
                else
                    XtraMessageBox.Show(LanguageManager.GetString(result.Message) ?? result.Message, "");

                txtBarcode.Text = "";
                e.Handled = true;
                return;
            }

            // Thử quét barcode tài sản vật lý (đồ lớn)
            var resTaiSan = BUS_ThueDo.Instance.QuetBarcodeTaiSan(code);
            if (resTaiSan.Success && resTaiSan.Data is ET.Models.DanhMuc.ET_TaiSanChoThue taiSan)
            {
                if (_dsNguonChoThue != null)
                {
                    // Lấy từ nguồn cho thuê hiện tại để có Giá và Tên chuẩn
                    var spInfo = _dsNguonChoThue.FirstOrDefault(s => s.IdTaiSanChoThue == taiSan.Id);
                    if (spInfo != null)
                    {
                        ThemVaoGio(
                            spInfo.IdSanPham,
                            spInfo.TenHienThi,
                            spInfo.TienThue,
                            spInfo.TienCoc,
                            spInfo.IdTaiSanChoThue,
                            spInfo.MaHienThi, 1);
                    }
                    else
                    {
                         XtraMessageBox.Show($"Tài sản {code} không được phép thuê tại máy POS này (Sai Khu Vực hoặc chưa Cấp Quyền).", "Cảnh báo");
                    }
                }
                txtBarcode.Text = "";
                e.Handled = true;
                return;
            }

            // Tìm theo mã hiển thị (Phao bơi nhập mã sản phẩm)
            if (_dsNguonChoThue != null)
            {
                var sp = _dsNguonChoThue.FirstOrDefault(r => string.Equals(r.MaHienThi, code, StringComparison.OrdinalIgnoreCase));
                if (sp != null)
                {
                    ThemVaoGio(sp.IdSanPham, sp.TenHienThi, sp.TienThue, sp.TienCoc, sp.IdTaiSanChoThue, sp.IsDinhDanh ? sp.MaHienThi : null, 1);
                }
                else
                {
                    XtraMessageBox.Show(
                        string.Format(LanguageManager.GetString(AppConstants.ErrorMessages.ERR_POS_PRODUCT_NOT_FOUND) ?? "Không tìm thấy: \"{0}\"", code),
                        "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            txtBarcode.Text = "";
            e.Handled = true;
        }

        private void ViewGioThue_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "SoLuong")
            {
                int sl = Convert.ToInt32(e.Value);
                if (sl <= 0)
                {
                    _gioThue.RemoveAt(e.RowHandle);
                }
                else
                {
                    _gioThue[e.RowHandle].SoLuong = sl;
                }
                CapNhatGioThue();
            }
        }

        private void RepBtnXoa_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int row = viewGioThue.FocusedRowHandle;
            if (row >= 0 && row < _gioThue.Count)
            {
                _gioThue.RemoveAt(row);
                CapNhatGioThue();
            }
        }

        private void BtnRFID_Click(object sender, EventArgs e)
        {
            ThanhToan(AppConstants.PhuongThucTT.ViRFID);
        }

        private void BtnTienMat_Click(object sender, EventArgs e)
        {
            ThanhToan(AppConstants.PhuongThucTT.TienMat);
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            _gioThue.Clear();
            CapNhatGioThue();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadChuaTra();
        }

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
            if (result.Success && result.Data is DTO_KhachHangPOS kh)
                GanKhachHang(kh);
            else
                XtraMessageBox.Show(LanguageManager.GetString(result.Message) ?? result.Message, "");
        }

        private void BtnBoChonKhach_Click(object sender, EventArgs e)
        {
            ResetKhachHang();
        }

        #endregion

        #region Hàm hỗ trợ

        private void ThemVaoGio(int idSP, string ten, decimal tienThue, decimal tienCoc, int? idTaiSan, string maVach, int soLuong = 1)
        {
            // Tài sản vật lý -> mỗi cái 1 dòng riêng (SL = 1)
            if (idTaiSan.HasValue)
            {
                if (_gioThue.Any(x => x.IdTaiSanChoThue == idTaiSan.Value))
                {
                    XtraMessageBox.Show(
                        LanguageManager.GetString(AppConstants.ErrorMessages.ERR_RENTAL_TAISAN_DANG_THUE) ?? "Tài sản đã có trong giỏ.",
                        "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                _gioThue.Add(new DTO_RentalCartItem
                {
                    IdSanPham = idSP, TenSanPham = $"{ten} [{maVach}]",
                    SoLuong = 1, TienThue = tienThue, TienCoc = tienCoc,
                    IdTaiSanChoThue = idTaiSan, MaVachTaiSan = maVach
                });
            }
            else
            {
                // SP thường -> gộp SL nếu đã có trong giỏ
                var existing = _gioThue.FirstOrDefault(x => x.IdSanPham == idSP && !x.IdTaiSanChoThue.HasValue);
                if (existing != null)
                {
                    existing.SoLuong += soLuong;
                }
                else
                {
                    _gioThue.Add(new DTO_RentalCartItem
                    {
                        IdSanPham = idSP, TenSanPham = ten,
                        SoLuong = soLuong, TienThue = tienThue, TienCoc = tienCoc
                    });
                }
            }
            CapNhatGioThue();
        }

        private void CapNhatGioThue()
        {
            // Tạo DataTable để bind grid
            var dt = new DataTable();
            dt.Columns.Add("TenSanPham", typeof(string));
            dt.Columns.Add("SoLuong", typeof(int));
            dt.Columns.Add("TienThue", typeof(decimal));
            dt.Columns.Add("TienCoc", typeof(decimal));
            dt.Columns.Add("Tong", typeof(decimal));
            dt.Columns.Add("Xoa", typeof(string));

            foreach (var item in _gioThue)
            {
                dt.Rows.Add(item.TenSanPham, item.SoLuong, item.TienThue, item.TienCoc,
                    item.TongThue + item.TongCoc, "");
            }
            gridGioThue.DataSource = dt;

            decimal tongThue = _gioThue.Sum(x => x.TongThue);
            decimal tongCoc = _gioThue.Sum(x => x.TongCoc);
            lblTienThueValue.Text = tongThue.ToString("#,##0");
            lblTienCocValue.Text = tongCoc.ToString("#,##0");
            lblTongValue.Text = $"{(tongThue + tongCoc):#,##0} ₫";
        }

        // Chốt giao đồ: validate -> gọi BUS -> in biên lai.
        private void ThanhToan(string phuongThuc)
        {
            if (!_gioThue.Any())
            {
                XtraMessageBox.Show(
                    LanguageManager.GetString(AppConstants.ErrorMessages.ERR_RENTAL_CART_EMPTY) ?? "Giỏ thuê trống!",
                    "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (phuongThuc == AppConstants.PhuongThucTT.ViRFID && _khachHang == null)
            {
                XtraMessageBox.Show(
                    LanguageManager.GetString(AppConstants.ErrorMessages.ERR_RENTAL_RFID_REQUIRED) ?? "Vui lòng quẹt RFID khách.",
                    "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string msg = LanguageManager.GetString(AppConstants.ErrorMessages.MSG_RENTAL_CONFIRM) ?? "Xác nhận cho thuê đồ?";
            if (XtraMessageBox.Show(msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            var req = new DTO_RentalCheckoutRequest
            {
                GioThue = _gioThue,
                PhuongThucTT = phuongThuc,
                IdKhachHang = _khachHang?.IdDoiTac,
                IdViDienTu = _idViDienTu,
                IdNhanVien = SessionManager.IdDoiTac,
                IdPhienThuNgan = this.IdPhienThuNgan
            };

            var result = BUS_ThueDo.Instance.XuLyGiaoDo(req);
            if (result.Success)
            {
                string maDon = result.Data?.ToString() ?? "";
                System.Windows.Forms.Clipboard.SetText(maDon);
                
                string copiedMsg = LanguageManager.GetString(AppConstants.ErrorMessages.MSG_COPIED_TO_CLIPBOARD) ?? "Đã sao chép mã biên lai: {0}";
                string successMsg = LanguageManager.GetString(AppConstants.ErrorMessages.MSG_RENTAL_CHECKOUT_SUCCESS) ?? "Giao đồ thành công! Mã biên lai: {0}";
                
                XtraMessageBox.Show(string.Format(successMsg + "\n" + copiedMsg, maDon), "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _gioThue.Clear();
                CapNhatGioThue();
                ResetKhachHang();
                LoadChuaTra();
                txtBarcode.Focus();
            }
            else
            {
                XtraMessageBox.Show(
                    LanguageManager.GetString(result.Message) ?? result.Message,
                    "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadChuaTra()
        {
            var result = BUS_ThueDo.Instance.LayDanhSachChuaTra(DateTime.Today, DateTime.Today.AddDays(1));
            if (result.Success && result.Data is List<DTO_PhienChuaTraView> ds)
            {
                gridChuaTra.DataSource = ds;
            }
        }

        private void GanKhachHang(DTO_KhachHangPOS kh)
        {
            if (kh == null) return;
            _khachHang = kh;
            _idViDienTu = kh.IdViDienTu;
            lblTenKhach.Text = kh.HoTen ?? kh.MaKhachHang;
            lblSoDuVi.Text = kh.SoDuVi > 0 ? $"Ví: {kh.SoDuVi:#,##0}₫" : "";
            txtTimKhach.Enabled = false;
            btnTimKhach.Enabled = false;
            btnBoChonKhach.Visible = true;
            txtBarcode.Focus();
        }

        private void ResetKhachHang()
        {
            _khachHang = null;
            _idViDienTu = null;
            lblTenKhach.Text = "";
            lblSoDuVi.Text = "";
            txtTimKhach.Enabled = true;
            txtTimKhach.Text = "";
            btnTimKhach.Enabled = true;
            btnBoChonKhach.Visible = false;
        }

        private void ApplyLanguage()
        {
            lblTitle.Text = LanguageManager.GetString("RENTAL_TITLE") ?? "CHO THUÊ ĐỒ";
            colSP_TenSanPham.Caption = LanguageManager.GetString("RENTAL_COL_TEN") ?? "Sản phẩm";
            colSP_TienThue.Caption = LanguageManager.GetString("RENTAL_COL_TIENTHUE") ?? "Tiền thuê";
            colSP_TienCoc.Caption = LanguageManager.GetString("RENTAL_COL_TIENCOC") ?? "Tiền cọc";
            colGT_TenSanPham.Caption = LanguageManager.GetString("RENTAL_COL_TEN") ?? "Sản phẩm";
            colGT_SoLuong.Caption = LanguageManager.GetString("COL_SOLUONG") ?? "SL";
            colGT_TienThue.Caption = LanguageManager.GetString("RENTAL_COL_TIENTHUE") ?? "Tiền thuê";
            colGT_TienCoc.Caption = LanguageManager.GetString("RENTAL_COL_TIENCOC") ?? "Cọc";
            colGT_Tong.Caption = LanguageManager.GetString("COL_THANHTIEN") ?? "Tổng";
            lblTienThueLabel.Text = LanguageManager.GetString("RENTAL_COL_TIENTHUE") ?? "Tiền thuê:";
            lblTienCocLabel.Text = LanguageManager.GetString("RENTAL_COL_TIENCOC") ?? "Tiền cọc:";
            lblTongLabel.Text = LanguageManager.GetString("POS_TOTAL") ?? "TỔNG:";
            btnRFID.Text = "RFID";
            btnTienMat.Text = LanguageManager.GetString("POS_BTN_CASH") ?? "Tiền mặt";
            btnHuy.Text = LanguageManager.GetString("POS_BTN_CLEAR") ?? "Hủy";
            btnRefresh.Text = LanguageManager.GetString("BTN_REFRESH") ?? "Làm mới";
            lblGiamSatTitle.Text = LanguageManager.GetString("RENTAL_MONITOR_TITLE") ?? "Đang cho thuê tại trạm";
            txtBarcode.Properties.NullValuePrompt = LanguageManager.GetString("RENTAL_BARCODE_HINT") ?? "Quét barcode hoặc tìm SP...";
            txtTimKhach.Properties.NullValuePrompt = LanguageManager.GetString("CUST_SEARCH_HINT") ?? "SĐT / Quét RFID...";
        }

        #endregion
    }
}

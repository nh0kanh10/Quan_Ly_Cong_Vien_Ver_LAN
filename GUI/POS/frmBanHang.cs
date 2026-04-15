using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BUS;
using ET;
using FontAwesome.Sharp;
using Guna.UI2.WinForms;
using DevExpress.XtraEditors.Repository;
using DAL;

namespace GUI
{
    public partial class frmBanHang : Form, IBaseForm
    {
        #region Khai báo biến toàn cục
        private List<CartItem> _cart = new List<CartItem>();
        private ET_KhachHang _selectedKH = null;

        // _tongTien: tổng tiền GỐC chưa giảm giá
        // _soTienThucThu: số tiền SAU KHI đã trừ chiết khấu/điểm (hiển thị trên label lớn)
        private decimal _tongTien = 0;
        private decimal _soTienThucThu = 0;
        private RepositoryItemButtonEdit btnXoa;
        private CameraScanner _cameraScanner;

        // Lưu thông tin đoàn khách khi quét mã booking "BK-xxx"
        // Nếu != null nghĩa là đang ở chế độ phục vụ đoàn (giá 0đ, trừ quota)
        private ET_DoanKhach _currentBooking = null;
        #endregion

        #region Khởi tạo Form
        public frmBanHang()
        {
            InitializeComponent();

            this.KeyPreview = true;
            this.KeyDown += FrmBanHang_KeyDown;

            InitIcons();
            ApplyStyles();
            InitCboKho();
            ApplyPermissions();
            EnsurePOSScanner();

            LoadData();
            RefreshCartDisplay();

            txtScanner.Focus();
        }

        private void InitCboKho()
        {
            var dsKho = BUS_KhoHang.Instance.LoadDS();
            if (ET.SessionManager.CurrentIdKhuVuc.HasValue)
            {
                dsKho = dsKho.Where(x => x.IdKhuVuc == ET.SessionManager.CurrentIdKhuVuc.Value).ToList();
            }
            // Nếu khu vực này chưa có kho riêng, fallback về Kho Tổng (IdKhuVuc == null)
            if (dsKho.Count == 0) dsKho = BUS_KhoHang.Instance.LoadDS().Where(x => x.IdKhuVuc == null).ToList();

            cboKhoXuLy.DataSource = dsKho;
            cboKhoXuLy.DisplayMember = "TenKho";
            cboKhoXuLy.ValueMember = "Id";
            if (dsKho.Count > 0) cboKhoXuLy.SelectedIndex = 0;
        }
        #endregion

        #region Phím tắt toàn màn hình (F1, F2, F8, F9, F10, F11, Esc)
        private void FrmBanHang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9) { btnThanhToanTienMat_Click(null, null); }
            else if (e.KeyCode == Keys.F10) { btnThanhToanVi_Click(null, null); }
            else if (e.KeyCode == Keys.F11) { btnThanhToanChuyenKhoan_Click(null, null); }
            else if (e.KeyCode == Keys.F1)
            {
                txtTimKiem.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F2)
            {
                txtMaKH.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                btnHuyDon_Click(null, null);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F8)
            {
                txtKhachDua.Focus();
                e.Handled = true;
            }
        }
        #endregion

        #region Lọc sản phẩm theo nhóm (Tab: Tất cả / Vé / F&B / Thuê)
        private void Category_Click(object sender, EventArgs e)
        {
            if (sender is Guna2Button btn)
            {
                string category = "Tất cả";
                if (btn == btnCatVe) category = AppConstants.LoaiSanPham.Ve;
                else if (btn == btnCatFood) category = AppConstants.LoaiSanPham.AnUong;
                else if (btn == btnCatRental) category = AppConstants.LoaiSanPham.Thue;

                foreach (Control c in pnlCategories.Controls)
                {
                    if (c is Guna2Button b)
                    {
                        b.FillColor = ThemeManager.BackgroundColor;
                        b.ForeColor = ThemeManager.TextSecondaryColor;
                        b.BorderThickness = 1;
                        b.BorderColor = ThemeManager.BorderColor;
                    }
                }
                btn.FillColor = ThemeManager.PrimaryColor;
                btn.ForeColor = Color.White;
                btn.BorderThickness = 0;

                LoadSanPham(category);
                txtScanner.Focus();
            }
        }
        #endregion

        #region Quét mã sản phẩm (máy quét / gõ tay / camera)
        // Hỗ trợ cú pháp "SL*Mã", ví dụ: "3*VEM001" = thêm 3 cái VEM001
        // Nếu mã bắt đầu bằng "BK-" → nhận diện đoàn khách → chuyển sang chế độ phục vụ đoàn
        private void txtScanner_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string rawInput = txtScanner.Text.Trim();
                if (string.IsNullOrEmpty(rawInput)) return;

                int qty = 1;
                string code = rawInput;

                // Tách "SL*Mã" thành số lượng + mã sản phẩm
                if (rawInput.Contains("*"))
                {
                    string[] parts = rawInput.Split('*');
                    if (parts.Length == 2 && int.TryParse(parts[0], out int q))
                    {
                        qty = q;
                        code = parts[1].Trim();
                    }
                }

                if (!string.IsNullOrEmpty(code))
                {
                    //  NHÁNH ĐOÀN: quét MaBooking (BK-xxx) -> load quota ăn uống 
                    if (code.StartsWith("BK-", StringComparison.OrdinalIgnoreCase))
                    {
                        ApplyBookingToCart(code);
                        txtScanner.Clear();
                    }
                    //  NHÁNH thường: SP/Combo 
                    else
                    {
                        var sp = BUS_SanPham.Instance.GetByMaCode(code);
                        if (sp != null)
                        {
                            AddToCart(sp, qty);
                            txtScanner.Clear();
                        }
                        else
                        {
                            var combo = BUS_Combo.Instance.GetByMaCode(code);
                            if (combo != null)
                            {
                                if (combo.TrangThai == AppConstants.TrangThaiCombo.BanNhap || combo.TrangThai == AppConstants.TrangThaiCombo.NgungApDung)
                                {
                                    TDCMessageBox.Show($"Combo '{combo.Ten}' đang ở trạng thái {combo.TrangThai}, không được phép xuất bán!", "Cảnh báo truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    txtScanner.Clear();
                                }
                                else
                                {
                                    AddComboToCart(combo, qty);
                                    txtScanner.Clear();
                                }
                            }
                            else
                            {
                                TDCMessageBox.Show("Không tìm thấy SP/Combo: " + code, "Lỗi");
                                txtScanner.Clear();
                            }
                        }
                    }
                }
                e.Handled = true;
            }
        }
        #endregion

        #region Thao tác giỏ hàng (thêm / xóa / sửa số lượng / đổi đơn vị tính)
        private void btnXoaGioHang_Click(object sender, EventArgs e)
        {
            if (_cart.Count > 0 && TDCMessageBox.Show("Xóa toàn bộ giỏ hàng?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _cart.Clear();
                RefreshCartDisplay();
            }
            txtScanner.Focus();
        }

        // Xử lý khi user THAY ĐỔI giá trị ô trên lưới giỏ hàng
        // 2 trường hợp: (1) Sửa cột SoLuong, (2) Đổi cột ĐVT (đơn vị tính)
        private void gridViewGioHang_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "SoLuong")
            {
                int newQty = Convert.ToInt32(e.Value);
                var item = gridViewGioHang.GetRow(e.RowHandle) as CartItem;
                if (item != null)
                {
                    if (newQty <= 0)
                    {
                        _cart.Remove(item);
                    }
                    else
                    {
                        // Kiểm tra kho nếu đây là món vật lý và tăng số lượng vượt tồn kho
                        if (item.IdSanPham > 0)
                        {
                            var sp = BUS_SanPham.Instance.GetById(item.IdSanPham);
                            if (sp != null && IsPhysicalProduct(sp.LoaiSanPham))
                            {
                                int totalStock = DAL_TonKho.Instance.LoadDS().Where(x => x.IdSanPham == sp.Id).Sum(x => x.SoLuong);

                                // currentInCartOtherLines: tổng SL quy đổi về DVT cơ bản của các dòng KHÁC 
                                // trong giỏ cũng chứa SP này (tránh đếm trùng dòng đang sửa)
                                int currentInCartOtherLines = _cart.Where(x => x.IdSanPham == sp.Id && x.IdCombo == 0 && x != item).Sum(x => x.SoLuong * x.TyLeQuyDoi);
                                int proposedTotalBase = currentInCartOtherLines + (newQty * item.TyLeQuyDoi);

                                if (proposedTotalBase > totalStock)
                                {
                                    int maxAllowed = (totalStock - currentInCartOtherLines) / item.TyLeQuyDoi;
                                    item.SoLuong = Math.Max(0, maxAllowed);
                                    TDCMessageBox.Show($"Chỉ còn tồn {totalStock} '{sp.Ten}' trong kho!\nSau khi trừ đi giỏ hàng, bạn được phép nhập thêm tối đa {maxAllowed} {item.TenDVT}.", "Hết Tồn Kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else
                                {
                                    item.SoLuong = newQty;
                                }
                            }
                            else
                            {
                                item.SoLuong = newQty;
                            }
                        }
                        else
                        {
                            item.SoLuong = newQty;
                        }
                    }
                }
                RefreshCartDisplay();
                txtScanner.Focus();
            }
            else if (e.Column.FieldName == "TenDVT")
            {
                //  ĐỔI ĐƠN VỊ TÍNH TRÊN LƯỚI GIỎ HÀNG 
                // VD: Lon → Thùng (1 Thùng = 24 Lon)
                // Giá tự cập nhật theo bảng giá của ĐVT mới
                // Nếu đổi xong mà tồn kho không đủ → tự revert về ĐVT cũ
                string tenDVTMoi = e.Value?.ToString() ?? "";
                var item = gridViewGioHang.GetRow(e.RowHandle) as CartItem;
                if (item == null || item.IdSanPham <= 0) return;

                var sp = BUS_SanPham.Instance.GetById(item.IdSanPham);
                if (sp == null) return;

                var dvtGoc = BUS_DonViTinh.Instance.GetById(sp.IdDonViCoBan);
                bool laChonVeDVTGoc = (dvtGoc != null && dvtGoc.Ten == tenDVTMoi);

                int oldDvt = item.IdDVTHienTai;
                string oldTen = item.TenDVT;
                decimal oldGia = item.DonGia;

                if (laChonVeDVTGoc)
                {
                    // Chọn lại DVT gốc -> giá gốc
                    item.DonGia = item.DonGiaGoc;
                    item.IdDVTHienTai = sp.IdDonViCoBan;
                    item.TenDVT = tenDVTMoi;
                }
                else
                {
                    // Tìm đơn vị trong danh sách quy đổi
                    var quyDoiChon = item.DsQuyDoi?.FirstOrDefault(q =>
                    {
                        var dvt = BUS_DonViTinh.Instance.GetById(q.IdDonViLon);
                        return dvt?.Ten == tenDVTMoi;
                    });

                    if (quyDoiChon != null)
                    {
                        decimal giaUnit = BUS_BangGia.Instance.GetPriceByUnit(sp.Id, quyDoiChon.IdDonViLon, DateTime.Now);
                        item.DonGia = giaUnit;
                        item.IdDVTHienTai = quyDoiChon.IdDonViLon;
                        item.TenDVT = tenDVTMoi;
                    }
                }

                // Kiểm tra lại tồn kho sau khi đổi DVT
                if (IsPhysicalProduct(sp.LoaiSanPham))
                {
                    int totalStock = DAL_TonKho.Instance.LoadDS().Where(x => x.IdSanPham == sp.Id).Sum(x => x.SoLuong);
                    int currentInCartOtherLines = _cart.Where(x => x.IdSanPham == sp.Id && x.IdCombo == 0 && x != item).Sum(x => x.SoLuong * x.TyLeQuyDoi);
                    if (currentInCartOtherLines + (item.SoLuong * item.TyLeQuyDoi) > totalStock)
                    {
                        TDCMessageBox.Show($"Tồn kho không đủ để đáp ứng {item.SoLuong} {item.TenDVT} (tổng quy đổi là {item.SoLuong * item.TyLeQuyDoi} Base Units)!\nVui lòng sửa lại số lượng trước khi đổi đơn vị lớn.", "Hết Tồn Kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        // Revert back
                        item.IdDVTHienTai = oldDvt;
                        item.TenDVT = oldTen;
                        item.DonGia = oldGia;
                    }
                }

                RefreshCartDisplay();
            }
        }

        // Cung cấp ComboBox chọn ĐVT cho từng dòng giỏ hàng 
        // (chỉ SP có quy đổi mới hiện dropdown, SP đơn vị duy nhất thì khóa)
        private void OnCartCustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName != "TenDVT") return;

            var item = gridViewGioHang.GetRow(e.RowHandle) as CartItem;
            if (item == null) return;

            if (item.CoNhieuDVT)
            {
                var combo = new RepositoryItemComboBox();
                combo.DropDownRows = 6;

                var dvtGoc = BUS_DonViTinh.Instance.GetById(
                    BUS_SanPham.Instance.GetById(item.IdSanPham)?.IdDonViCoBan ?? 0);
                if (dvtGoc != null) combo.Items.Add(dvtGoc.Ten);

                foreach (var qd in item.DsQuyDoi)
                {
                    var dvtLon = BUS_DonViTinh.Instance.GetById(qd.IdDonViLon);
                    if (dvtLon != null && !combo.Items.Contains(dvtLon.Ten))
                        combo.Items.Add(dvtLon.Ten);
                }
                e.RepositoryItem = combo;
            }
            else
            {
                var readOnly = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
                readOnly.ReadOnly = true;
                e.RepositoryItem = readOnly;
            }
        }

        private void gridViewGioHang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && gridViewGioHang.FocusedRowHandle >= 0)
            {
                if (TDCMessageBox.Show("Xóa mặt hàng này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _cart.RemoveAt(gridViewGioHang.FocusedRowHandle);
                    RefreshCartDisplay();
                }
            }
        }
        #endregion

        #region Nút thanh toán (Tiền mặt F9 / Ví RFID F10 / Chuyển khoản F11)
        private void btnThanhToanTienMat_Click(object sender, EventArgs e)
        {
            ThanhToan(AppConstants.PhuongThucThanhToan.TienMat);
        }

        private void btnThanhToanVi_Click(object sender, EventArgs e)
        {
            txtKhachDua.Clear();
            lblTienThua.Clear();
            lblTienThua.FillColor = Color.White;

            ThanhToan(AppConstants.PhuongThucThanhToan.ViRfid);
        }

        private void btnThanhToanChuyenKhoan_Click(object sender, EventArgs e)
        {
            txtKhachDua.Clear();
            lblTienThua.Clear();
            lblTienThua.FillColor = Color.White;

            ThanhToan(AppConstants.PhuongThucThanhToan.ChuyenKhoan);
        }
        #endregion

        #region Huỷ đơn hàng (Esc)
        private void btnHuyDon_Click(object sender, EventArgs e)
        {
            if (_cart.Count > 0)
            {
                if (TDCMessageBox.Show("Hủy đơn hàng hiện tại và làm mới khách hàng?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
            }

            _cart.Clear();
            _selectedKH = null;
            _currentBooking = null;
            lblTenKH.Text = "Khách vãng lai";
            lblTenKH.ForeColor = ThemeManager.TextPrimaryColor;
            txtMaKH.Text = "";
            RefreshCartDisplay();
            txtScanner.Focus();
        }
        #endregion

        #region Phân quyền và giao diện
        public void ApplyPermissions()
        {
            var tk = ET.SessionManager.CurrentUser;
            if (tk == null) return;

            if (!BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "VIEW_POS"))
            {
                this.Enabled = false;
                return;
            }

            bool canManage = BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "MANAGE_POS");
            btnHuyDon.Enabled = canManage;
            btnXoaGioHang.Enabled = canManage;
        }

        public void ApplyStyles()
        {
            ThemeManager.ApplyTheme(this);

            lblTongTienLarge.Font = new Font("Cascadia Code", 32f, FontStyle.Bold);
            lblTongTienLarge.ForeColor = ThemeManager.PrimaryColor; 

            gridViewGioHang.OptionsBehavior.Editable = true; 
            gridViewGioHang.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
        }

        public void InitIcons()
        {
            btnTimKH.Image = IconHelper.GetBitmap(IconChar.Search, Color.White, 24);
            btnTimKH.ImageSize = new System.Drawing.Size(24, 24);

            btnXoaGioHang.Image = IconHelper.GetBitmap(IconChar.TrashAlt, Color.White, 24);
            btnXoaGioHang.ImageSize = new System.Drawing.Size(24, 24);

            btnHuyDon.Image = IconHelper.GetBitmap(IconChar.Ban, Color.White, 24);
            btnHuyDon.ImageSize = new System.Drawing.Size(24, 24);

            btnXoa = new RepositoryItemButtonEdit();
            btnXoa.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            btnXoa.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            btnXoa.Buttons[0].Image = IconHelper.GetBitmap(IconChar.TrashAlt, ThemeManager.DangerColor, 24); 
            btnXoa.Buttons[0].Caption = "Xóa";
            btnXoa.ButtonClick += btnXoa_ButtonClick;
        }
        #endregion

        #region Load danh sách sản phẩm (lưới bên trái)
        private void tileViewSanPham_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "DonGia")
            {
                if (e.Value != null && decimal.TryParse(e.Value.ToString(), out decimal price))
                {
                    e.DisplayText = price.ToString("N0") + " đ";
                }
            }
        }

        public void LoadData()
        {
            LoadSanPham("Tất cả");
        }

        // Giá sản phẩm hiển thị theo bảng giá NGÀY HIỆN TẠI (ngày thường / cuối tuần / lễ)
        public void LoadSanPham(string category = "Tất cả")
        {
            string kw = txtTimKiem.Text.Trim();
            var data = BUS_SanPham.Instance.TimKiem(kw, category == "Tất cả" ? "Tất cả" : category)
                .Where(sp => !sp.IsDeleted && sp.TrangThai == AppConstants.TrangThaiSanPham.DangBan).ToList();

            foreach (var sp in data)
            {
                sp.DonGia = BUS_BangGia.Instance.GetDynamicPrice(sp.Id, DateTime.Now);
            }

            gridSanPham.DataSource = data;
        }

        private void tileViewSanPham_ItemDoubleClick(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventArgs e)
        {
            var sp = tileViewSanPham.GetRow(e.Item.RowHandle) as ET_SanPham;
            if (sp != null)
            {
                AddToCart(sp);
            }
        }
        #endregion

        #region Nghiệp vụ thêm sản phẩm / combo vào giỏ hàng
        // IsPhysicalProduct: SP vật lý = Ăn uống + Lưu niệm (CẦN kiểm tra tồn kho)
        // Vé, Dịch vụ, Combo KHÔNG kiểm tra tồn kho
        private bool IsPhysicalProduct(string loaiSP)
        {
            return loaiSP == AppConstants.LoaiSanPham.AnUong ||
                   loaiSP == AppConstants.LoaiSanPham.DoLuuNiem;
        }

        private void AddToCart(ET_SanPham sp, int qty = 1)
        {
            if (IsPhysicalProduct(sp.LoaiSanPham))
            {
                int currentInCart = _cart.Where(x => x.IdSanPham == sp.Id && x.IdCombo == 0).Sum(x => x.SoLuong * x.TyLeQuyDoi);
                int totalStock = DAL_TonKho.Instance.LoadDS().Where(x => x.IdSanPham == sp.Id).Sum(x => x.SoLuong);

                
                if (currentInCart + qty > totalStock)
                {
                    TDCMessageBox.Show($"Sản phẩm '{sp.Ten}' chỉ còn tồn {totalStock} trên toàn hệ thống!\nBan không thể thêm vượt quá mức này.", "Hết Tồn Kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            decimal currentPrice = BUS_BangGia.Instance.GetDynamicPrice(sp.Id, DateTime.Now);

            // Load danh sách quy đổi DVT của SP này (VD: 1 Thùng = 24 Lon)
            var dsQuyDoi = BUS_SanPham.Instance.LayQuyDoiTheoSP(sp.Id);
            var dvtCoBan = BUS_DonViTinh.Instance.GetById(sp.IdDonViCoBan);
            string tenDVTGoc = dvtCoBan?.Ten ?? "ĐVT";

            // Nếu giỏ đã có SP này (cùng DVT cơ bản) → chỉ tăng SL thay vì thêm dòng mới
            var existing = _cart.Find(x => x.IdSanPham == sp.Id && x.IdCombo == 0 && x.IdDVTHienTai == sp.IdDonViCoBan);
            if (existing != null)
            {
                existing.SoLuong += qty;
                existing.DonGia = currentPrice;
            }
            else
            {
                _cart.Add(new CartItem
                {
                    IdSanPham = sp.Id,
                    TenSanPham = sp.Ten,
                    DonGia = currentPrice,
                    DonGiaGoc = currentPrice,
                    SoLuong = qty,
                    LoaiSanPham = sp.LoaiSanPham,
                    TenDVT = tenDVTGoc,
                    IdDVTHienTai = sp.IdDonViCoBan,
                    DsQuyDoi = dsQuyDoi
                });
            }
            FocusCartRow();
        }

        private void AddComboToCart(ET_Combo combo, int qty = 1)
        {
            var existing = _cart.Find(x => x.IdCombo == combo.Id);
            if (existing != null)
            {
                existing.SoLuong += qty;
            }
            else
            {
                _cart.Add(new CartItem
                {
                    IdCombo = combo.Id,
                    TenSanPham = "[COMBO] " + combo.Ten,
                    DonGia = combo.Gia,
                    SoLuong = qty,
                    LoaiSanPham = AppConstants.LoaiSanPham.Combo
                });
            }
            FocusCartRow();
        }

        private void FocusCartRow()
        {
            RefreshCartDisplay();
            gridGioHang.Focus();
            gridViewGioHang.FocusedRowHandle = gridViewGioHang.RowCount - 1;
            if (gridViewGioHang.Columns["SoLuong"] != null)
            {
                gridViewGioHang.FocusedColumn = gridViewGioHang.Columns["SoLuong"];
                gridViewGioHang.ShowEditor();
            }
        }
        #endregion

        #region Cập nhật hiển thị giỏ hàng (lưới + tổng tiền + chiết khấu)
        private void RefreshCartDisplay()
        {
            gridGioHang.DataSource = null;
            gridGioHang.DataSource = _cart.ToList();
            gridViewGioHang.PopulateColumns();

            var v = gridViewGioHang;
            if (v.Columns.Count > 0)
            {
                if (v.Columns["IdSanPham"] != null) v.Columns["IdSanPham"].Visible = false;
                if (v.Columns["IdCombo"] != null) v.Columns["IdCombo"].Visible = false;
                if (v.Columns["IdDichVuDoan"] != null) v.Columns["IdDichVuDoan"].Visible = false;
                if (v.Columns["LoaiSanPham"] != null) v.Columns["LoaiSanPham"].Visible = false;

                if (v.Columns["TenSanPham"] != null)
                {
                    v.Columns["TenSanPham"].Caption = "Tên Sản Phẩm";
                    v.Columns["TenSanPham"].OptionsColumn.AllowEdit = false;
                }
                if (v.Columns["SoLuong"] != null)
                {
                    v.Columns["SoLuong"].Caption = "SL";
                    v.Columns["SoLuong"].Width = 80;
                    v.Columns["SoLuong"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    v.Columns["SoLuong"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    RepositoryItemSpinEdit spinEdit = new RepositoryItemSpinEdit();
                    spinEdit.IsFloatValue = false;
                    spinEdit.MinValue = 0;
                    spinEdit.MaxValue = 999;
                    spinEdit.Buttons[0].Visible = true; 
                    v.Columns["SoLuong"].ColumnEdit = spinEdit;
                }
                if (v.Columns["DonGia"] != null) { v.Columns["DonGia"].Visible = false; }
                if (v.Columns["ThanhTien"] != null)
                {
                    v.Columns["ThanhTien"].Caption = "Thành Tiền";
                    v.Columns["ThanhTien"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    v.Columns["ThanhTien"].DisplayFormat.FormatString = "N0";
                    v.Columns["ThanhTien"].OptionsColumn.AllowEdit = false;
                    v.Columns["ThanhTien"].Width = 100;
                    v.Columns["ThanhTien"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

                    // Hiện dòng Footer Tổng ở dưới cùng Grid
                    v.OptionsView.ShowFooter = true;
                    v.OptionsView.ShowFooter = true;
                    v.Columns["ThanhTien"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                    v.Columns["ThanhTien"].SummaryItem.DisplayFormat = "TỔNG: {0:N0}";
                }

                if (v.Columns["TenDVT"] != null)
                {
                    var colDVT = v.Columns["TenDVT"];
                    colDVT.Caption = "ĐVT";
                    colDVT.Width = 80;
                    colDVT.OptionsColumn.AllowEdit = true;
                    colDVT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    colDVT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    v.CustomRowCellEdit -= OnCartCustomRowCellEdit;
                    v.CustomRowCellEdit += OnCartCustomRowCellEdit;
                }
                if (v.Columns["IdDVTHienTai"] != null) v.Columns["IdDVTHienTai"].Visible = false;
                if (v.Columns["DonGiaGoc"] != null) v.Columns["DonGiaGoc"].Visible = false;
                if (v.Columns["DsQuyDoi"] != null) v.Columns["DsQuyDoi"].Visible = false;
                if (v.Columns["CoNhieuDVT"] != null) v.Columns["CoNhieuDVT"].Visible = false;

                // Thêm cột Xóa nếu chưa có
                if (v.Columns["colXoa"] == null)
                {
                    var colXoa = v.Columns.AddField("colXoa");
                    colXoa.Visible = true;
                    colXoa.Caption = "Xóa";
                    colXoa.ColumnEdit = btnXoa;
                    colXoa.Width = 45;
                    colXoa.OptionsColumn.AllowSize = false;
                    colXoa.OptionsColumn.FixedWidth = true;
                    colXoa.AppearanceHeader.ForeColor = Color.FromArgb(220, 38, 38);
                    colXoa.AppearanceHeader.Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
                    colXoa.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                }

                int idx = 0;
                if (v.Columns["TenSanPham"] != null) v.Columns["TenSanPham"].VisibleIndex = idx++;
                if (v.Columns["TenDVT"] != null) v.Columns["TenDVT"].VisibleIndex = idx++;
                if (v.Columns["SoLuong"] != null) v.Columns["SoLuong"].VisibleIndex = idx++;
                if (v.Columns["ThanhTien"] != null) v.Columns["ThanhTien"].VisibleIndex = idx++;
                if (v.Columns["colXoa"] != null) v.Columns["colXoa"].VisibleIndex = idx++;
            }
            v.OptionsView.ColumnAutoWidth = true;

            _tongTien = _cart.Sum(x => x.ThanhTien);
            // Tính thực thu (sau chiết khấu cao nhất giữa 3 nguồn: VIP / Khuyến mãi / Điểm)
            _soTienThucThu = TinhThucThu(_tongTien);
            lblTongTienLarge.Text = _soTienThucThu.ToString("N0") + " VNĐ";
            lblCartTitle.Text = string.Format("GIỎ HÀNG ({0})", _cart.Count);
            UpdateDiscountPreview();
            txtKhachDua_TextChanged(null, null);
        }
        #endregion

        #region Tìm kiếm khách hàng (nhập mã / SĐT)
        private void BtnTimKH_Click(object sender, EventArgs e)
        {
            string keyword = txtMaKH.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                _selectedKH = null;
                lblTenKH.Text = "Khách vãng lai";
                return;
            }

            var kh = BUS_KhachHang.Instance.GetByMaCodeOrSdt(keyword);

            if (kh != null)
            {
                _selectedKH = kh;
                string tenHien = kh.HoTen ?? "Không tên";

                // Hiển thị chiết khấu VIP
                decimal pctVip = GetVipDiscount(kh.LoaiKhach);
                if (pctVip > 0)
                    tenHien += string.Format("  [{0} -{1}%]", kh.LoaiKhach, (int)(pctVip * 100));

                // Hiển thị điểm tích lũy Loyalty
                if (kh.DiemTichLuy > 0)
                {
                    decimal giaTriDiem = BUS_TichDiem.Instance.TinhGiaTriDiem(kh.DiemTichLuy);
                    tenHien += string.Format("  | {0} điểm (={1:N0}đ)", kh.DiemTichLuy, giaTriDiem);
                }

                lblTenKH.Text = tenHien;
                UpdateDiscountPreview();
            }
            else
            {
                _selectedKH = null;
                lblTenKH.Text = "Không tìm thấy KH";
                UpdateDiscountPreview();
                TDCMessageBox.Show("Không tìm thấy khách hàng!", "Thông báo");
            }
        }
        #endregion

        #region Xử lý thanh toán chính (so sánh 3 nguồn giảm giá → lưu đơn → phát vé)
        //  CƠ CHẾ CHIẾT KHẤU 3 NGUỒN (quan trọng!) 
        // Hệ thống so sánh 3 nguồn giảm giá và chọn cái CÓ LỢI NHẤT cho khách:
        //   (a) Chiết khấu theo hạng khách: VIP=10%, VVIP=25%, HSSV=5%
        //   (b) Khuyến mãi đang chạy (event / campaign)
        //   (c) Điểm tích lũy (quy đổi ra tiền)
        // KHÔNG CỘNG DỒN — chỉ áp dụng 1 nguồn duy nhất.
        // Chiết khấu CHỈ áp cho Vé/Dịch vụ, F&B (ăn uống) KHÔNG BAO GIỜ giảm giá.

        private void ThanhToan(string phuongThuc)
        {
            if (_currentBooking != null)
            {
                ThanhToanDoan(phuongThuc);
                return;
            }

            if (_cart.Count == 0)
            {
                TDCMessageBox.Show("Giỏ hàng trống!", "Thông báo");
                return;
            }

            if (phuongThuc == AppConstants.PhuongThucThanhToan.ViRfid && _selectedKH == null)
            {
                TDCMessageBox.Show("Thanh toán ví RFID bắt buộc phải quét thẻ khách hàng trước!", "Yêu cầu");
                txtMaKH.Focus();
                return;
            }

            decimal tongTienGoc = _tongTien;

            // (a) Chiết khấu theo hạng VIP
            decimal pctVip = _selectedKH != null ? GetVipDiscount(_selectedKH.LoaiKhach) : 0m;

            // (b) Khuyến mãi sự kiện đang chạy
            var kmEvent = BUS_KhuyenMai.Instance.GetBestActivePromotion(tongTienGoc);
            decimal pctEvent = 0m;
            if (kmEvent != null)
            {
                if (kmEvent.LoaiGiamGia == "PhanTram")
                    pctEvent = kmEvent.GiaTriGiam / 100m;
                else
                    pctEvent = tongTienGoc > 0 ? kmEvent.GiaTriGiam / tongTienGoc : 0;
            }

            // Chọn % giảm lớn nhất giữa VIP và Khuyến mãi
            decimal pctApDung = Math.Max(pctVip, pctEvent);

            // tongTienDuocGiam: chỉ tính trên Vé/Dịch vụ (F&B không được giảm)
            decimal tongTienDuocGiam = _cart
                .Where(item => !IsPhysicalProduct(item.LoaiSanPham))
                .Sum(item => item.ThanhTien);
            decimal tienGiamChietKhau = Math.Round(tongTienDuocGiam * pctApDung, 0);

            // (c) Điểm tích lũy — tính số điểm tối đa có thể dùng
            int diemKhaDung = 0;
            decimal tienGiamDiem = 0;
            bool dungDiem = false;

            if (_selectedKH != null && _selectedKH.DiemTichLuy > 0)
            {
                diemKhaDung = BUS_TichDiem.Instance.TinhDiemKhaDung(_selectedKH.DiemTichLuy, tongTienGoc);
                tienGiamDiem = BUS_TichDiem.Instance.TinhGiaTriDiem(diemKhaDung);
            }

            decimal tienGiam;
            string nguonGiam;

            // Nếu điểm lợi hơn chiết khấu → hỏi khách có muốn dùng điểm không
            if (tienGiamDiem > tienGiamChietKhau && diemKhaDung > 0)
            {
                string hoi = string.Format(
                    "Bạn có {0} điểm (={1:N0}đ).\nDùng {2} điểm giảm {3:N0}đ?\n(Lợi hơn chiết khấu {4:N0}đ)",
                    _selectedKH.DiemTichLuy, BUS_TichDiem.Instance.TinhGiaTriDiem(_selectedKH.DiemTichLuy),
                    diemKhaDung, tienGiamDiem, tienGiamChietKhau);

                if (TDCMessageBox.Show(hoi, "DÙNG ĐIỂM TÍCH LŨY?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    dungDiem = true;
                    tienGiam = tienGiamDiem;
                    nguonGiam = string.Format("Điểm ({0}đ)", diemKhaDung);
                }
                else
                {
                    tienGiam = tienGiamChietKhau;
                    nguonGiam = pctEvent >= pctVip
                        ? (kmEvent?.TenKhuyenMai ?? "Sự kiện")
                        : "Khách " + (_selectedKH?.LoaiKhach ?? "");
                }
            }
            else
            {
                tienGiam = tienGiamChietKhau;
                nguonGiam = pctEvent >= pctVip
                    ? (kmEvent?.TenKhuyenMai ?? "Sự kiện")
                    : "Khách " + (_selectedKH?.LoaiKhach ?? "");
            }

            decimal soTienThucThu = tongTienGoc - tienGiam;

            // Build thông tin xác nhận
            string discountInfo = "";
            if (tienGiam > 0)
                discountInfo = string.Format("\n🎁 Giảm giá ({0}): -{1:N0}đ", nguonGiam, tienGiam);

            // POS chỉ bán B2C -> luôn tích điểm cá nhân
            int diemSeCong = 0;
            string loyaltyInfo = "";
            if (_selectedKH != null)
            {
                diemSeCong = BUS_TichDiem.Instance.TinhDiemThuong(soTienThucThu, _selectedKH.LoaiKhach);
                if (diemSeCong > 0)
                    loyaltyInfo = string.Format("\nTích thêm: +{0} điểm", diemSeCong);
            }

            string msg = string.Format(
                "Tổng đơn: {0:N0} VNĐ{1}\n-> Thanh toán thực tế: {2:N0} VNĐ\nPhương thức: {3}\nSố mặt hàng: {4}{5}",
                tongTienGoc, discountInfo, soTienThucThu, phuongThuc, _cart.Count, loyaltyInfo);

            if (TDCMessageBox.Show(msg, "XÁC NHẬN THANH TOÁN", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            int currentUserId = GetCurrentUserId();
            string uniqueSuffix = "-" + Guid.NewGuid().ToString().Substring(0, 4).ToUpper();

            //  POS = B2C only -> IdDoan luôn NULL
            var donHang = new ET_DonHang
            {
                MaCode = "DH-" + DateTime.Now.ToString("yyMMddHHmmss") + uniqueSuffix,
                IdKhachHang = _selectedKH?.Id,
                IdDoan = null,
                TongTien = tongTienGoc,
                TienGiamGia = tienGiam,
                ThoiGian = DateTime.Now,
                TrangThai = AppConstants.TrangThaiDonHang.DaThanhToan,
                NguonBan = "POS",
                CreatedAt = DateTime.Now,
                CreatedBy = currentUserId
            };

            //  RẢI TIỀN GIẢM GIÁ XUỐNG TỪNG DÒNG CHI TIẾT 
            // Nếu dùng chiết khấu %: F&B = 0đ giảm, Vé/DV = tỷ lệ phân bổ theo %
            // Nếu dùng điểm: mọi dòng đều được phân bổ theo tỷ trọng tiền
            var chiTiet = _cart.Select(item =>
            {
                bool isVatLy = IsPhysicalProduct(item.LoaiSanPham);
                decimal giamDong = 0;
                if (!dungDiem && !isVatLy && pctApDung > 0 && tongTienDuocGiam > 0)
                {
                    giamDong = Math.Round(item.ThanhTien * pctApDung, 0);
                }
                else if (dungDiem && tongTienGoc > 0)
                {
                    giamDong = Math.Round(item.ThanhTien * (tienGiam / tongTienGoc), 0);
                }
                decimal donGiaThucTe = item.SoLuong > 0
                    ? Math.Max(0, item.DonGia - Math.Round(giamDong / item.SoLuong, 0))
                    : item.DonGia;

                // Lấy tỷ lệ quy đổi ĐVT để lưu vào DB
                int tyLe = 1;
                if (item.IdDVTHienTai > 0 && item.DsQuyDoi != null)
                {
                    var qd = item.DsQuyDoi.FirstOrDefault(q => q.IdDonViLon == item.IdDVTHienTai);
                    if (qd != null && qd.TyLeQuyDoi > 1) tyLe = (int)qd.TyLeQuyDoi;
                }

                return new ET_ChiTietDonHang
                {
                    IdSanPham = item.IdSanPham > 0 ? item.IdSanPham : (int?)null,
                    IdCombo = item.IdCombo > 0 ? item.IdCombo : (int?)null,
                    SoLuong = item.SoLuong,
                    DonGiaGoc = item.DonGia,
                    TienGiamGiaDong = giamDong,
                    DonGiaThucTe = donGiaThucTe,
                    TyLeQuyDoi = tyLe
                };
            }).ToList();

            OperationResult<int> result;

            int idKhoDeduct = (cboKhoXuLy != null && cboKhoXuLy.SelectedValue != null)
                                ? Convert.ToInt32(cboKhoXuLy.SelectedValue) : 1;

            if (phuongThuc == AppConstants.PhuongThucThanhToan.ViRfid)
            {
                result = BUS_DonHang.Instance.ThanhToanBangVi(donHang, chiTiet, _selectedKH.Id, currentUserId, idKhoDeduct);
            }
            else
            {
                var phieuThu = new ET_PhieuThu
                {
                    MaCode = "PT-" + DateTime.Now.ToString("yyMMddHHmmss") + uniqueSuffix,
                    SoTien = soTienThucThu,
                    PhuongThuc = phuongThuc,
                    ThoiGian = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    CreatedBy = currentUserId
                };
                result = BUS_DonHang.Instance.ThemDonHangVaChiTiet(donHang, chiTiet, phieuThu, idKhoDeduct);
            }

            if (!result.IsSuccess)
            {
                TDCMessageBox.Show(result.ErrorMessage, "Lỗi");
                return;
            }

            int idDonHangMoi = result.Data;

            if (_selectedKH != null)
            {
                // 1. Trừ điểm nếu đã dùng
                if (dungDiem && diemKhaDung > 0)
                {
                    BUS_TichDiem.Instance.TieuDiem(_selectedKH.Id, diemKhaDung, idDonHangMoi,
                        string.Format("Dùng điểm thanh toán {0}", donHang.MaCode), currentUserId);
                }

                // 2. Cộng điểm mới (chỉ tính trên số tiền thực tế khách xuất hầu bao)
                if (diemSeCong > 0)
                {
                    BUS_TichDiem.Instance.CongDiem(_selectedKH.Id, diemSeCong, idDonHangMoi,
                        string.Format("Tích điểm {0}", donHang.MaCode), currentUserId);
                }
            }

            // Nếu đơn có vé → tự mở popup Phát Vé Điện Tử
            var tickets = BUS_VeDienTu.Instance.LayVeTheoDonHang(idDonHangMoi);
            if (tickets.Count > 0)
            {
                using (var frmVe = new frmPhatVe(idDonHangMoi, donHang.MaCode, _selectedKH?.HoTen, phuongThuc))
                {
                    ThemeManager.ShowAsPopup(frmVe);
                }
            }
            else
            {
                string receipt = "═══════ HOÁ ĐƠN ═══════\n";
                receipt += string.Format("Mã HĐ: {0}\n", donHang.MaCode);
                receipt += string.Format("Ngày: {0:dd/MM/yyyy HH:mm}\n\n", DateTime.Now);
                foreach (var item in _cart)
                {
                    string tenDvtDisplay = string.IsNullOrEmpty(item.TenDVT) ? "" : item.TenDVT + " ";
                    receipt += string.Format("{0}  x{1} {2} {3:N0}đ\n", item.TenSanPham, item.SoLuong, tenDvtDisplay, item.ThanhTien);
                }
                receipt += "────────────────────\n";
                receipt += string.Format("Tổng gốc:          {0:N0} đ\n", tongTienGoc);
                if (tienGiam > 0)
                    receipt += string.Format("Giảm ({0}): -{1:N0} đ\n", nguonGiam, tienGiam);
                receipt += string.Format("THỰC THU:           {0:N0} VNĐ\n", soTienThucThu);
                receipt += string.Format("Phương thức:        {0}\n", phuongThuc);
                if (diemSeCong > 0)
                    receipt += string.Format("Tích thêm:       +{0} điểm\n", diemSeCong);
                receipt += "═══════════════════════";
                TDCMessageBox.Show(receipt, "THANH TOÁN THÀNH CÔNG", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Reset
            _cart.Clear();
            _selectedKH = null;
            _currentBooking = null;
            lblTenKH.Text = "Khách vãng lai";
            txtMaKH.Text = "";
            RefreshCartDisplay();
            txtScanner.Focus();
        }
        #endregion

        #region Phục vụ đoàn khách (quét mã booking BK-xxx → giá 0đ → trừ quota)
        // Khi quét mã "BK-xxx": hệ thống tìm đoàn, kiểm tra booking hợp lệ, 
        // load quota ăn uống còn lại, thêm vào giỏ giá 0đ (đã trả theo hợp đồng)
        private void ApplyBookingToCart(string maBooking)
        {
            var doan = BUS_DoanKhach.Instance.GetByBookingCode(maBooking);
            if (doan == null)
            {
                TDCMessageBox.Show("Không tìm thấy đoàn: " + maBooking, "Lỗi");
                return;
            }

            var check = BUS_DoanKhach.Instance.CheckBookingValid(doan);
            if (!check.IsSuccess)
            {
                TDCMessageBox.Show(check.ErrorMessage, "Booking không hợp lệ");
                return;
            }

            // Lấy quota ăn uống còn lại
            var quotaAnUong = BUS_DoanKhach.Instance.LayQuotaConLai(doan.Id)
                .Where(x => x.LoaiDichVu == AppConstants.LoaiDichVuDoan.AnUong
                         || x.LoaiDichVu == AppConstants.LoaiDichVuDoan.DichVu)
                .ToList();

            if (quotaAnUong.Count == 0)
            {
                TDCMessageBox.Show($"Đoàn '{doan.TenDoan}' không có suất ăn hoặc đã dùng hết!", "Hết Quota");
                return;
            }

            _currentBooking = doan;
            lblTenKH.Text = $"🏷️ ĐOÀN: {doan.TenDoan} (CK {doan.ChietKhau}%)";
            lblTenKH.ForeColor = Color.FromArgb(34, 197, 94);

            string info = $"ĐOÀN: {doan.TenDoan}\n";
            info += "Suất ăn còn lại:\n";
            foreach (var q in quotaAnUong)
            {
                string tenDV = !string.IsNullOrEmpty(q.TenDichVu) ? q.TenDichVu
                             : !string.IsNullOrEmpty(q.TenCombo) ? q.TenCombo
                             : "Dịch vụ #" + q.Id;
                info += $"  • {tenDV}: còn {q.SoLuongConLai}/{q.SoLuong}\n";
            }
            info += "\nThêm 1 suất vào giỏ?";

            if (TDCMessageBox.Show(info, "SUẤT ĂN ĐOÀN", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (var q in quotaAnUong)
                {
                    string tenDV = !string.IsNullOrEmpty(q.TenDichVu) ? q.TenDichVu
                                 : !string.IsNullOrEmpty(q.TenCombo) ? q.TenCombo
                                 : "DV Đoàn #" + q.Id;
                    _cart.Add(new CartItem
                    {
                        IdSanPham = q.IdSanPham ?? 0,
                        IdCombo = q.IdCombo ?? 0,
                        IdDichVuDoan = q.Id,
                        TenSanPham = $"[ĐOÀN] {tenDV}",
                        DonGia = 0,  // Zero-dollar: đã trả theo hợp đồng
                        SoLuong = 1,
                        LoaiSanPham = AppConstants.LoaiSanPham.AnUong
                    });
                }
                RefreshCartDisplay();
            }
        }

        // Thanh toán riêng cho đoàn: tạo đơn ghi nợ công ty (tổng = 0đ), trừ quota
        private void ThanhToanDoan(string phuongThuc)
        {
            var doanItems = _cart.Where(x => x.IdDichVuDoan > 0).ToList();
            if (doanItems.Count == 0)
            {
                TDCMessageBox.Show("Giỏ hàng không có mục đoàn nào.", "Lỗi");
                return;
            }

            string confirm = $"Xác nhận phục vụ ĐOÀN: {_currentBooking.TenDoan}\n";
            confirm += $"{doanItems.Count} mục — Tổng: 0 VNĐ (theo hợp đồng)\n";
            confirm += "Trừ quota dịch vụ đoàn?";

            if (TDCMessageBox.Show(confirm, "XÁC NHẬN ĐOÀN", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            int errorCount = 0;
            foreach (var item in doanItems)
            {
                var result = BUS_DoanKhach.Instance.KhauTruQuota(item.IdDichVuDoan, item.SoLuong);
                if (!result.IsSuccess)
                    errorCount++;
            }

            if (errorCount > 0)
            {
                TDCMessageBox.Show($"{errorCount} mục không thể trừ quota (có thể đã hết).", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Tạo ĐH ghi nợ công ty (0đ), mã "DHD..." = Đơn Hàng Đoàn
            int currentUserId = GetCurrentUserId();
            var donHang = new ET_DonHang
            {
                MaCode = "DHD" + DateTime.Now.ToString("yyMMddHHmmss"),
                IdDoan = _currentBooking.Id,
                TongTien = 0,
                TienGiamGia = 0,
                ThoiGian = DateTime.Now,
                TrangThai = AppConstants.TrangThaiDonHang.GhiNoCongTy,
                GhiChu = $"POS phục vụ đoàn {_currentBooking.TenDoan}",
                NguonBan = "POS",
                CreatedAt = DateTime.Now,
                CreatedBy = currentUserId
            };

            var chiTiet = doanItems.Select(item =>
            {
                int tyLe = 1;
                if (item.IdDVTHienTai > 0 && item.DsQuyDoi != null)
                {
                    var qd = item.DsQuyDoi.FirstOrDefault(q => q.IdDonViLon == item.IdDVTHienTai);
                    if (qd != null && qd.TyLeQuyDoi > 1) tyLe = (int)qd.TyLeQuyDoi;
                }

                return new ET_ChiTietDonHang
                {
                    IdSanPham = item.IdSanPham > 0 ? item.IdSanPham : (int?)null,
                    IdCombo = item.IdCombo > 0 ? item.IdCombo : (int?)null,
                    SoLuong = item.SoLuong,
                    DonGiaGoc = 0,
                    TienGiamGiaDong = 0,
                    DonGiaThucTe = 0,
                    TyLeQuyDoi = tyLe
                };
            }).ToList();

            var phieuThu = new ET_PhieuThu
            {
                MaCode = "PT-DOAN-" + DateTime.Now.Ticks.ToString().Substring(10),
                SoTien = 0,
                PhuongThuc = AppConstants.PhuongThucThanhToan.TienMat,
                ThoiGian = DateTime.Now,
                CreatedAt = DateTime.Now,
                CreatedBy = currentUserId
            };

            int idKhoDeduct = (cboKhoXuLy != null && cboKhoXuLy.SelectedValue != null)
                                ? Convert.ToInt32(cboKhoXuLy.SelectedValue) : 1;
            var result2 = BUS_DonHang.Instance.ThemDonHangVaChiTiet(donHang, chiTiet, phieuThu, idKhoDeduct);

            if (result2.IsSuccess)
            {
                TDCMessageBox.Show(
                    $" Phục vụ đoàn thành công!\nĐoàn: {_currentBooking.TenDoan}\nMã HĐ: {donHang.MaCode}\n{doanItems.Count} mục — 0 VNĐ",
                    "HOÀN TẤT", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                TDCMessageBox.Show(result2.ErrorMessage, "Lỗi tạo đơn");
            }

            // Reset
            _cart.Clear();
            _selectedKH = null;
            _currentBooking = null;
            lblTenKH.Text = "Khách vãng lai";
            lblTenKH.ForeColor = ThemeManager.TextPrimaryColor;
            txtMaKH.Text = "";
            RefreshCartDisplay();
            txtScanner.Focus();
        }
        #endregion

        #region Bảng chiết khấu theo hạng khách và tính toán giảm giá
        /// <summary>
        /// Chiết khấu POS chỉ theo RANK CÁ NHÂN.
        /// Doan/DoanhNghiep = 0% tại POS (đoàn B2B đi form riêng frmXuatVeDoan).
        /// </summary>
        private decimal GetVipDiscount(string loaiKhach)
        {
            if (string.IsNullOrEmpty(loaiKhach)) return 0m;
            switch (loaiKhach)
            {
                case AppConstants.LoaiKhachHang.VVIP: return 0.25m;
                case AppConstants.LoaiKhachHang.Vip: return 0.10m;
                case AppConstants.LoaiKhachHang.HocSinhSinhVien: return 0.05m;
                default: return 0m; // CaNhan, NoiBo, (legacy Doan/DoanhNghiep) = 0%
            }
        }

        /// <summary>
        /// Cập nhật dòng hiển thị giảm giá trên POS (dưới tổng tiền).
        /// Hiện "ĐANG CÓ [nguồn] GIẢM X%: -Yđ" hoặc gợi ý "Mua thêm Xđ để kích hoạt KM"
        /// </summary>
        private void UpdateDiscountPreview()
        {
            if (_tongTien <= 0)
            {
                lblTongTienTitle.Text = "";
                return;
            }

            decimal pctVip = _selectedKH != null ? GetVipDiscount(_selectedKH.LoaiKhach) : 0m;
            var kmEvent = BUS_KhuyenMai.Instance.GetBestActivePromotion(_tongTien);
            decimal pctEvent = 0m;
            if (kmEvent != null)
            {
                pctEvent = kmEvent.LoaiGiamGia == "PhanTram"
                    ? kmEvent.GiaTriGiam / 100m
                    : (_tongTien > 0 ? kmEvent.GiaTriGiam / _tongTien : 0);
            }

            decimal pctMax = Math.Max(pctVip, pctEvent);
            //  Chiết khấu chỉ trên Vé/DichVu, không F&B
            decimal tongDuocGiam = _cart
                .Where(item => !IsPhysicalProduct(item.LoaiSanPham))
                .Sum(item => item.ThanhTien);
            decimal tienGiam = Math.Round(tongDuocGiam * pctMax, 0);
            decimal thucThu = _tongTien - tienGiam;

            // So sánh với điểm tích lũy
            int diemKhaDung = _selectedKH != null ? BUS_TichDiem.Instance.TinhDiemKhaDung(_selectedKH.DiemTichLuy, _tongTien) : 0;
            decimal tienGiamDiem = BUS_TichDiem.Instance.TinhGiaTriDiem(diemKhaDung);

            if (tienGiam > 0 || tienGiamDiem > 0)
            {
                string info;
                if (tienGiamDiem > tienGiam && diemKhaDung > 0)
                    info = string.Format("CÓ THỂ DÙNG ĐIỂM VIP: Giảm -{0:N0}đ -> TT: {1:N0}đ",
                        tienGiamDiem, _tongTien - tienGiamDiem);
                else if (tienGiam > 0)
                {
                    if (pctMax == pctVip && pctVip > pctEvent)
                    {
                        info = string.Format(" ĐANG CÓ [Khách {3}] GIẢM {0}%: -{1:N0}đ -> TT: {2:N0}đ",
                            (int)(pctMax * 100), tienGiam, thucThu, _selectedKH.LoaiKhach);
                    }
                    else if (kmEvent != null)
                    {
                        info = string.Format(" ĐANG CÓ [{3}] GIẢM {0}%: -{1:N0}đ -> TT: {2:N0}đ",
                            (int)(pctMax * 100), tienGiam, thucThu, kmEvent.TenKhuyenMai);
                    }
                    else
                    {
                        info = string.Format(" ĐANG CÓ CHƯƠNG TRÌNH GIẢM {0}%: -{1:N0}đ -> TT: {2:N0}đ",
                            (int)(pctMax * 100), tienGiam, thucThu);
                    }
                }
                else
                {
                    info = string.Format("Dùng {0} điểm -> Giảm {1:N0}đ", diemKhaDung, tienGiamDiem);
                }

                lblTongTienTitle.Text = info;
                lblTongTienTitle.ForeColor = ThemeManager.SuccessColor;
            }
            else
            {
                lblTongTienTitle.Text = "";
            }

            //  Gợi ý khuyến mãi nếu mua thêm không quá 100K VND
            var hint = BUS_KhuyenMai.Instance.GetPromotionHint(_tongTien);
            if (hint != null)
            {
                decimal missing = hint.DonToiThieu.GetValueOrDefault(0) - _tongTien;
                if (missing > 0 && missing <= 100000)
                {
                    string hintText = $"Gợi ý: Mua thêm {missing:N0}đ để kích hoạt [{hint.TenKhuyenMai}]";
                    if (string.IsNullOrEmpty(lblTongTienTitle.Text))
                    {
                        lblTongTienTitle.Text = hintText;
                        lblTongTienTitle.ForeColor = ThemeManager.WarningColor;
                    }
                    else
                    {
                        lblTongTienTitle.Text += $" | {hintText}";
                    }
                }
            }
        }

        /// <summary>
        /// Tính số tiền thực thu (sau chiết khấu cao nhất) để hiển thị trên label lớn
        /// </summary>
        private decimal TinhThucThu(decimal tongGoc)
        {
            if (tongGoc <= 0) return 0;
            decimal pctVip = _selectedKH != null ? GetVipDiscount(_selectedKH.LoaiKhach) : 0m;
            var kmEvent = BUS_KhuyenMai.Instance.GetBestActivePromotion(tongGoc);
            decimal pctEvent = 0m;
            if (kmEvent != null)
            {
                pctEvent = kmEvent.LoaiGiamGia == "PhanTram"
                    ? kmEvent.GiaTriGiam / 100m
                    : (tongGoc > 0 ? kmEvent.GiaTriGiam / tongGoc : 0);
            }
            decimal pctMax = Math.Max(pctVip, pctEvent);
            // Chiết khấu chỉ trên Vé/DichVu
            decimal tongDuocGiam = _cart
                .Where(item => !IsPhysicalProduct(item.LoaiSanPham))
                .Sum(item => item.ThanhTien);
            decimal tienGiam = Math.Round(tongDuocGiam * pctMax, 0);
            // So với điểm loyalty
            if (_selectedKH != null && _selectedKH.DiemTichLuy > 0)
            {
                int diemKD = BUS_TichDiem.Instance.TinhDiemKhaDung(_selectedKH.DiemTichLuy, tongGoc);
                decimal tienDiem = BUS_TichDiem.Instance.TinhGiaTriDiem(diemKD);
                if (tienDiem > tienGiam) tienGiam = tienDiem;
            }
            return tongGoc - tienGiam;
        }
        #endregion

        #region Tìm kiếm sản phẩm tức thì (gõ tên lọc ngay)
        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            tileViewSanPham.ApplyFindFilter(txtTimKiem.Text.Trim());
        }
        #endregion

        #region Xoá dòng giỏ hàng (nút thùng rác trên lưới)
        private void btnXoa_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gridViewGioHang.FocusedRowHandle >= 0)
            {
                var item = gridViewGioHang.GetRow(gridViewGioHang.FocusedRowHandle) as CartItem;
                if (item != null)
                {
                    _cart.Remove(item);
                    RefreshCartDisplay();
                }
            }
        }

        private int GetCurrentUserId()
        {
            var tk = ET.SessionManager.CurrentUser;
            if (tk != null) return tk.Id;

            TDCMessageBox.Show("Phiên làm việc không hợp lệ! Vui lòng khởi động lại ứng dụng để đăng nhập.", "Lỗi bảo mật");
            Application.Restart();
            Environment.Exit(0);
            return -1;
        }
        #endregion

        #region Quét mã bằng Camera (webcam nhận diện barcode / QR)
        private void EnsurePOSScanner()
        {
            if (_cameraScanner == null)
            {
                _cameraScanner = new CameraScanner();
                _cameraScanner.OnBarcodeDetected += POS_OnBarcodeDetected;
                _cameraScanner.OnError += (err) => TDCMessageBox.Show(err, "Quét ảnh");
            }
        }

        private void btnToggleCamera_Click(object sender, EventArgs e)
        {
            if (_cameraScanner != null && _cameraScanner.IsRunning)
            {
                _cameraScanner.Stop();
                picCamera.Visible = false;
                btnToggleCamera.Text = "\uD83D\uDCF7 Cam";
                btnToggleCamera.FillColor = ThemeManager.PrimaryColor;
            }
            else
            {
                EnsurePOSScanner();
                picCamera.Visible = true;
                if (_cameraScanner.Start(picCamera))
                {
                    btnToggleCamera.Text = "\u23F9 Tắt";
                    btnToggleCamera.FillColor = ThemeManager.DangerColor;
                }
            }
        }

        private void btnScanFile_Click(object sender, EventArgs e)
        {
            EnsurePOSScanner();
            _cameraScanner.ScanFromFile();
        }

        // Khi camera nhận diện được mã → tự inject vào logic quét như máy quét vật lý
        private void POS_OnBarcodeDetected(string code)
        {
            txtScanner.Text = code;
            var sp = BUS_SanPham.Instance.GetByMaCode(code);
            if (sp != null)
            {
                AddToCart(sp, 1);
                txtScanner.Clear();
                try { System.Media.SystemSounds.Beep.Play(); } catch { }
            }
            else
            {
                var combo = BUS_Combo.Instance.GetByMaCode(code);
                if (combo != null)
                {
                    AddComboToCart(combo, 1);
                    txtScanner.Clear();
                    try { System.Media.SystemSounds.Beep.Play(); } catch { }
                }
                else
                {
                    TDCMessageBox.Show("Camera quét: Không tìm thấy SP/Combo: " + code, "Không tìm thấy");
                    txtScanner.Clear();
                }
            }
        }
        #endregion

        #region Nút tiền nhanh (Quick Cash: 50K / 100K / 200K / 500K / Đưa đủ)
        private void btn50K_Click(object sender, EventArgs e)
        {
            txtKhachDua.Text = "50000";
        }
        private void btn100K_Click(object sender, EventArgs e)
        {
            txtKhachDua.Text = "100000";
        }
        private void btn200K_Click(object sender, EventArgs e)
        {
            txtKhachDua.Text = "200000";
        }
        private void btn500K_Click(object sender, EventArgs e)
        {
            txtKhachDua.Text = "500000";
        }
        private void btnDuaDu_Click(object sender, EventArgs e)
        {
            txtKhachDua.Text = _soTienThucThu.ToString("0");
        }
        #endregion

        #region Tính tiền thừa / thiếu khi khách đưa tiền mặt
        // Tự format số tiền có dấu phẩy (150,000)
        // Xanh = đủ tiền, Đỏ = còn thiếu, nút F9 chỉ bật khi đủ
        private void txtKhachDua_TextChanged(object sender, EventArgs e)
        {
            string rawInput = txtKhachDua.Text.Replace(",", "").Replace(".", "");
            decimal khachDua = 0;
            decimal.TryParse(rawInput, out khachDua);

            if (khachDua > 0)
            {
                // Unhook tạm thời để tránh loop vô hạn khi setText lại
                txtKhachDua.TextChanged -= txtKhachDua_TextChanged;
                txtKhachDua.Text = khachDua.ToString("N0");
                txtKhachDua.SelectionStart = txtKhachDua.Text.Length;
                txtKhachDua.TextChanged += txtKhachDua_TextChanged;
            }

            decimal soCanThu = _soTienThucThu > 0 ? _soTienThucThu : _tongTien;
            decimal tienThua = khachDua - soCanThu;

            if (khachDua >= soCanThu && soCanThu > 0)
            {
                lblTienThua.Text = tienThua.ToString("N0") + " đ";
                lblTienThua.FillColor = Color.White;
                lblTienThua.ForeColor = ThemeManager.SuccessColor;

                btnThanhToanTienMat.Enabled = true;
                btnThanhToanTienMat.FillColor = ThemeManager.SuccessColor;
            }
            else
            {
                lblTienThua.Text = "CÒN THIẾU: " + Math.Abs(tienThua).ToString("N0") + " đ";
                lblTienThua.FillColor = Color.White;
                lblTienThua.ForeColor = ThemeManager.DangerColor;

                btnThanhToanTienMat.Enabled = false;
                btnThanhToanTienMat.FillColor = Color.Silver;
            }

            if (soCanThu == 0)
            {
                lblTienThua.Text = "0 đ";
                lblTienThua.FillColor = Color.White;
                lblTienThua.ForeColor = ThemeManager.TextSecondaryColor;
                btnThanhToanTienMat.Enabled = false;
                btnThanhToanTienMat.FillColor = ThemeManager.BorderColor;
            }
        }

        private void txtKhachDua_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                if (btnThanhToanTienMat.Enabled)
                {
                    btnThanhToanTienMat.PerformClick();
                }
                else
                {
                    TDCMessageBox.Show("Khách chưa đưa đủ tiền!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        #endregion
    }

    #region Model giỏ hàng tạm (CartItem — in-memory, không lưu DB)
    public class CartItem
    {
        public int IdSanPham { get; set; }
        public int IdCombo { get; set; }
        public int IdDichVuDoan { get; set; }  // > 0 = item đoàn, dùng để KhauTruQuota
        public string TenSanPham { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }
        public string LoaiSanPham { get; set; }
        public decimal ThanhTien => DonGia * SoLuong;

        //  ĐVT & Quy đổi 
        /// <summary>Tên ĐVT hiện tại đang bán (VD: "Lon", "Thùng")</summary>
        public string TenDVT { get; set; }
        /// <summary>Id ĐVT đang chọn. 0 = DVT cơ bản.</summary>
        public int IdDVTHienTai { get; set; }
        /// <summary>Giá gốc theo DVT cơ bản (để tính khi đổi ĐVT)</summary>
        public decimal DonGiaGoc { get; set; }
        /// <summary>Danh sách quy đổi của SP này (null = không có)</summary>
        public List<ET_QuyDoiDonVi> DsQuyDoi { get; set; }
        /// <summary>Có nhiều hơn 1 ĐVT để chọn không?</summary>
        public bool CoNhieuDVT => DsQuyDoi != null && DsQuyDoi.Count > 0;

        // TyLeQuyDoi: số đơn vị cơ bản trong 1 đơn vị lớn
        // VD: 1 Thùng = 24 Lon → TyLeQuyDoi = 24
        // Dùng để quy đổi khi kiểm tra tồn kho
        public int TyLeQuyDoi
        {
            get
            {
                if (IdDVTHienTai > 0 && DsQuyDoi != null)
                {
                    var qd = DsQuyDoi.FirstOrDefault(q => q.IdDonViLon == IdDVTHienTai);
                    if (qd != null && qd.TyLeQuyDoi > 1) return (int)qd.TyLeQuyDoi;
                }
                return 1;
            }
        }
    }
    #endregion
}



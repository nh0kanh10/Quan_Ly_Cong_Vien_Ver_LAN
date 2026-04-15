using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BUS;
using ET;
using FontAwesome.Sharp;
using Guna.UI2.WinForms;

namespace GUI
{
    /// <summary>
    /// TRẠM 2: Lễ Tân — Xử lý phát sinh tại chỗ cho đoàn khách.
    /// - Quét MaBooking -> hiện danh sách dịch vụ + quota
    /// - Bơm thêm / Rút bớt dịch vụ
    /// - Thu/Chi tiền chênh lệch
    /// </summary>
    public partial class frmQuayVe_LeTan : Form, IBaseForm
    {
        private ET_DoanKhach _doan;
        private List<ET_DoanKhach_DichVu> _dichVuList;

        public frmQuayVe_LeTan()
        {
            InitializeComponent();
            if (ThemeManager.IsInDesignMode(this)) return;
            InitIcons();
            ApplyStyles();
            ApplyPermissions();
        }

        public void ApplyPermissions() { }

        public void ApplyStyles()
        {
            ThemeManager.ApplyTheme(this);
            lblTitle.Font = new Font(ThemeManager.PrimaryFontFamily, 16f, FontStyle.Bold);
            lblTitle.ForeColor = ThemeManager.TextPrimaryColor;
        }

        public void InitIcons()
        {
            btnTimDoan.Image = IconHelper.GetBitmap(IconChar.Search, Color.White, 20);
        }

        public void LoadData() { }

        // 
        // TÌM ĐOÀN
        // 

        private void btnTimDoan_Click(object sender, EventArgs e)
        {
            string keyword = txtMaBooking.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                TDCMessageBox.Show("Nhập mã booking hoặc tên đoàn.", "Thông báo");
                return;
            }

            _doan = BUS_DoanKhach.Instance.GetByBookingCode(keyword);
            if (_doan == null)
            {
                // Fallback: tìm theo tên
                var all = BUS_DoanKhach.Instance.LoadDS();
                _doan = all.FirstOrDefault(x => x.TenDoan != null
                    && x.TenDoan.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (_doan == null)
            {
                TDCMessageBox.Show("Không tìm thấy đoàn: " + keyword, "Lỗi");
                ClearAll();
                return;
            }

            HienThiDoan();
        }

        private void txtMaBooking_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTimDoan_Click(null, null);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        // 
        // HIỂN THỊ THÔNG TIN ĐOÀN + QUOTA
        // 

        private void HienThiDoan()
        {
            if (_doan == null) return;

            // BẮT BỆNH 1: Profile Card Chuyên Nghiệp
            lblTenDoan.Text = _doan.TenDoan;
            
            string nguoiDD = string.IsNullOrEmpty(_doan.NguoiDaiDien) ? "Khách Lẻ" : _doan.NguoiDaiDien;
            string sdt = string.IsNullOrEmpty(_doan.DienThoaiLienHe) ? "(Không có SĐT)" : _doan.DienThoaiLienHe;
            lblSubText.Text = $"Mã Booking: {_doan.MaBooking}   |   Trưởng đoàn: {nguoiDD} - {sdt}";

            // Badge Trạng thái
            switch (_doan.TrangThai)
            {
                case "DaDat":
                    lblTrangThai.Text = "ĐÃ ĐẶT (ĐANG CHỜ)";
                    lblTrangThai.ForeColor = ThemeManager.PrimaryColor;
                    break;
                case "DangPhucVu":
                    lblTrangThai.Text = "ĐANG HOẠT ĐỘNG";
                    lblTrangThai.ForeColor = ThemeManager.SuccessColor;
                    break;
                case "DaHoanTat":
                    lblTrangThai.Text = "ĐÃ HOÀN TẤT";
                    lblTrangThai.ForeColor = ThemeManager.TextSecondaryColor;
                    break;
                default:
                    lblTrangThai.Text = _doan.TrangThai.ToUpper();
                    lblTrangThai.ForeColor = ThemeManager.DangerColor;
                    break;
            }

            pnlDoanInfo.Visible = true;
            pnlToolbar.Visible = true;

            // Load quota
            RefreshQuota();

            // Khóa UI nếu đoàn đã Đóng/Chốt sổ
            CheckFormState();
        }

        private void CheckFormState()
        {
            if (_doan == null) return;
            
            bool isReadOnly = (_doan.TrangThai == AppConstants.TrangThaiDoanKhach.DaHoanTat || 
                               _doan.TrangThai == AppConstants.TrangThaiDoanKhach.DaXuatVe);

            btnMuaThem.Enabled = !isReadOnly;
            btnHoanHuy.Enabled = !isReadOnly;
            btnKhoaSo.Enabled = !isReadOnly;
        }

        private void RefreshQuota()
        {
            if (_doan == null) return;

            _dichVuList = BUS_DoanKhach.Instance.LayQuotaConLai(_doan.Id);

            // Bind vào DevExpress GridControl
            gcQuota.DataSource = null;
            gcQuota.DataSource = _dichVuList.Select(x => new
            {
                x.Id,
                Loai = x.TenLoaiDichVu,
                DichVu = !string.IsNullOrEmpty(x.TenDichVu) ? x.TenDichVu
                        : !string.IsNullOrEmpty(x.TenCombo) ? x.TenCombo
                        : "DV #" + x.Id,
                SoLuong = x.SoLuong,
                DaDung = x.SoLuongDaDung,
                ConLai = x.SoLuongConLai,
                TrangThai = x.TenTrangThai
            }).ToList();

            // BẮT BỆNH 4: Group By theo Loại
            if (gvQuota.Columns.Count > 0)
            {
                gvQuota.Columns["Id"].Visible = false;
                gvQuota.Columns["Loai"].Caption = "Loại";
                gvQuota.Columns["DichVu"].Caption = "Sản phẩm / Dịch vụ";
                gvQuota.Columns["SoLuong"].Caption = "Gốc";
                gvQuota.Columns["DaDung"].Caption = "Đã Dùng";
                gvQuota.Columns["ConLai"].Caption = "Còn Lại";
                gvQuota.Columns["TrangThai"].Caption = "Trạng Thái";

                gvQuota.Columns["Loai"].GroupIndex = 0; // Gom nhóm
                gvQuota.ExpandAllGroups();
                
                gvQuota.BestFitColumns();
            }

            // Tổng kết
            int tongGoc = _dichVuList.Sum(x => x.SoLuong);
            int tongDung = _dichVuList.Sum(x => x.SoLuongDaDung);
            lblTongQuota.Text = string.Format("Tổng: {0} gốc | {1} đã dùng | {2} còn lại",
                tongGoc, tongDung, tongGoc - tongDung);
            lblTongQuota.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
        }

        // 
        // BƠM THÊM DỊCH VỤ
        // 

        private void btnBomThem_Click(object sender, EventArgs e)
        {
            if (_doan == null) { TDCMessageBox.Show("Chưa chọn đoàn.", "Thông báo"); return; }

            var check = BUS_DoanKhach.Instance.CheckBookingValid(_doan);
            if (!check.IsSuccess)
            {
                TDCMessageBox.Show(check.ErrorMessage, "Không thể thêm");
                return;
            }

            // Hỏi loại dịch vụ qua MessageBox (nhẹ, rõ ràng)
            var result = TDCMessageBox.Show(
                "Chọn loại dịch vụ muốn BƠM THÊM:\n\n" +
                "• [YES] = Vé + Combo (ăn uống, vui chơi)\n" +
                "• [NO]  = Phòng KS + Bàn ăn nhà hàng\n" +
                "• [Cancel] = Hủy",
                "BƠM THÊM DỊCH VỤ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (result == DialogResult.Cancel) return;

            if (result == DialogResult.Yes)
            {
                // Nhóm 1: Combo (bao gồm vé, ăn uống, dịch vụ gộp)
                BomThemCombo();
            }
            else
            {
                // Nhóm 2: Phòng hoặc Bàn ăn
                var r2 = TDCMessageBox.Show(
                    "• [YES] = Thêm PHÒNG khách sạn\n• [NO]  = Thêm BÀN ĂN nhà hàng",
                    "CHỌN LOẠI", MessageBoxButtons.YesNo);
                if (r2 == DialogResult.Yes) BomThemPhong();
                else BomThemBanAn();
            }
        }

        private void BomThemCombo()
        {
            var combos = BUS_Combo.Instance.LoadDS();
            if (combos == null || combos.Count == 0) { TDCMessageBox.Show("Chưa có Combo nào trong hệ thống."); return; }

            using (var dlg = new frmChonDichVuDoanDialog("Combo / Vé / Dịch vụ",
                combos.Select(c => new DichVuItem { Id = c.Id, Ten = c.Ten, DonGia = c.Gia, IsCombo = true }).ToList()))
            {
                dlg.DefaultPax = _doan.SoLuongKhach;
                ThemeManager.ShowAsPopup(dlg);
                if (dlg.SelectedItem == null) return;

                LuuDichVuPhatSinh(
                    AppConstants.LoaiDichVuDoan.Combo,
                    idCombo: dlg.SelectedItem.Id,
                    idSanPham: null,
                    dlg.SoLuong, dlg.SelectedItem.DonGia, dlg.NgaySuDung, dlg.GhiChu);
            }
        }

        private void BomThemPhong()
        {
            var phongList = BUS_SanPham.Instance.LoadDS()
                .Where(sp => sp.LoaiSanPham == AppConstants.LoaiSanPham.LuuTru && sp.TrangThai == AppConstants.TrangThaiSanPham.DangBan)
                .ToList();
            if (phongList.Count == 0) { TDCMessageBox.Show("Chưa có sản phẩm Lưu trú."); return; }

            using (var dlg = new frmChonDichVuDoanDialog("Phòng Khách Sạn",
                phongList.Select(sp => new DichVuItem { Id = sp.Id, Ten = sp.Ten, DonGia = sp.DonGia }).ToList()))
            {
                ThemeManager.ShowAsPopup(dlg);
                if (dlg.SelectedItem == null) return;

                LuuDichVuPhatSinh(
                    AppConstants.LoaiDichVuDoan.Phong,
                    idCombo: null,
                    idSanPham: dlg.SelectedItem.Id,
                    dlg.SoLuong, dlg.SelectedItem.DonGia, dlg.NgaySuDung, dlg.GhiChu);
            }
        }

        private void BomThemBanAn()
        {
            var anUongList = BUS_SanPham.Instance.LoadDS()
                .Where(sp => sp.LoaiSanPham == AppConstants.LoaiSanPham.AnUong && sp.TrangThai == AppConstants.TrangThaiSanPham.DangBan)
                .ToList();
            if (anUongList.Count == 0) { TDCMessageBox.Show("Chưa có sản phẩm Ăn uống."); return; }

            using (var dlg = new frmChonDichVuDoanDialog("Bàn Ăn / Set Menu",
                anUongList.Select(sp => new DichVuItem { Id = sp.Id, Ten = sp.Ten, DonGia = sp.DonGia }).ToList()))
            {
                dlg.ShowPaxField = true;
                dlg.DefaultPax = _doan.SoLuongKhach;
                ThemeManager.ShowAsPopup(dlg);
                if (dlg.SelectedItem == null) return;

                LuuDichVuPhatSinh(
                    AppConstants.LoaiDichVuDoan.BanAn,
                    idCombo: null,
                    idSanPham: dlg.SelectedItem.Id,
                    dlg.SoLuong, dlg.SelectedItem.DonGia, dlg.NgaySuDung, dlg.GhiChu);
            }
        }

        /// <summary>
        /// Ghi dịch vụ phát sinh vào DB, hiện thông báo tiền thu thêm, rồi refresh lưới.
        /// </summary>
        private void LuuDichVuPhatSinh(string loaiDV, int? idCombo, int? idSanPham,
            int soLuong, decimal donGia, DateTime? ngaySuDung, string ghiChu)
        {
            var dv = new ET_DoanKhach_DichVu
            {
                IdDoan = _doan.Id,
                LoaiDichVu = loaiDV,
                IdCombo = idCombo ?? 0,
                IdSanPham = idSanPham ?? 0,
                SoLuong = soLuong,
                DonGia = donGia,
                NgaySuDung = ngaySuDung,
                GhiChu = $"Phát sinh Lễ tân {DateTime.Now:dd/MM HH:mm}" +
                         (string.IsNullOrEmpty(ghiChu) ? "" : " | " + ghiChu)
            };

            var kq = BUS_DoanKhach.Instance.ThemDichVu(dv);
            if (!kq.IsSuccess)
            {
                TDCMessageBox.Show(kq.ErrorMessage, "Lỗi");
                return;
            }

            decimal tienThu = donGia * soLuong;
            string msg = $"Đã thêm {soLuong} dịch vụ vào đoàn [{_doan.TenDoan}]";
            if (tienThu > 0)
                msg += $"\nThu thêm: {tienThu:N0} VNĐ";

            TDCMessageBox.Show(msg, "THÊM THÀNH CÔNG");
            RefreshQuota();
        }

        // 
        // RÚT BỚT DỊCH VỤ
        // 

        private void btnRutBot_Click(object sender, EventArgs e)
        {
            if (_doan == null) { TDCMessageBox.Show("Chưa chọn đoàn.", "Thông báo"); return; }

            if (gvQuota.FocusedRowHandle < 0 || gvQuota.IsGroupRow(gvQuota.FocusedRowHandle))
            {
                TDCMessageBox.Show("Chọn dòng dịch vụ cần rút bớt.", "Thông báo");
                return;
            }

            int idDV = Convert.ToInt32(gvQuota.GetFocusedRowCellValue("Id"));
            var dv = _dichVuList?.FirstOrDefault(x => x.Id == idDV);
            if (dv == null) return;

            if (dv.SoLuongConLai <= 0)
            {
                TDCMessageBox.Show("Dịch vụ này đã dùng hết, không thể rút.", "Lỗi");
                return;
            }

            string tenDV = !string.IsNullOrEmpty(dv.TenDichVu) ? dv.TenDichVu
                         : !string.IsNullOrEmpty(dv.TenCombo) ? dv.TenCombo
                         : "DV #" + dv.Id;

            // Popup: Số lượng rút + Lý do hoàn tiền
            int soRut = 0;
            string lyDo = "";
            using (var popup = new Form())
            {
                popup.Text = "RÚT BỚT — " + tenDV;
                popup.FormBorderStyle = FormBorderStyle.FixedDialog;
                popup.StartPosition = FormStartPosition.CenterParent;
                popup.MaximizeBox = false; popup.MinimizeBox = false;
                popup.ClientSize = new Size(350, 220);
                popup.KeyPreview = true;
                popup.KeyDown += (s2, e2) => { if (e2.KeyCode == Keys.Escape) popup.Close(); };

                var lbl = new Label { Text = $"Rút bao nhiêu [{tenDV}]? (Tối đa: {dv.SoLuongConLai})", AutoSize = true, Location = new Point(20, 15) };
                var spn = new Guna2NumericUpDown
                {
                    Location = new Point(20, 40), Size = new Size(310, 35), BorderRadius = 4,
                    Minimum = 1, Maximum = dv.SoLuongConLai, Value = dv.SoLuongConLai
                };
                var lblLyDo = new Label { Text = "Lý do hoàn / rút:", AutoSize = true, Location = new Point(20, 82) };
                var txtLyDo = new Guna2TextBox
                {
                    Location = new Point(20, 102), Size = new Size(310, 60), BorderRadius = 4,
                    PlaceholderText = "VD: Khách ốm bớt 3 người...",
                    Multiline = true
                };
                var btnOK = new Guna2Button
                {
                    Text = "Xác nhận rút", Size = new Size(150, 35), Location = new Point(20, 175),
                    BorderRadius = 6, FillColor = Color.FromArgb(239, 68, 68), ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10f, FontStyle.Bold)
                };
                var btnHuy = new Guna2Button
                {
                    Text = "Hủy", Size = new Size(100, 35), Location = new Point(180, 175),
                    BorderRadius = 6, FillColor = Color.FromArgb(107, 114, 128), ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10f)
                };
                btnOK.Click += (s2, e2) => { soRut = (int)spn.Value; lyDo = txtLyDo.Text.Trim(); popup.DialogResult = DialogResult.OK; popup.Close(); };
                btnHuy.Click += (s2, e2) => { popup.Close(); };
                popup.Controls.AddRange(new Control[] { lbl, spn, lblLyDo, txtLyDo, btnOK, btnHuy });
                ThemeManager.ApplyTheme(popup);
                popup.ShowDialog(this);
                if (popup.DialogResult != DialogResult.OK || soRut <= 0) return;
            }

            decimal tienHoan = dv.DonGia * soRut;

            if (TDCMessageBox.Show(
                $"Xác nhận RÚT {soRut} [{tenDV}]?\n" +
                $"Giảm: {dv.SoLuong} -> {dv.SoLuong - soRut}\n" +
                (tienHoan > 0 ? $"💸 Hoàn tiền: {tienHoan:N0} VNĐ (tự tạo phiếu chi)\n" : "") +
                (string.IsNullOrEmpty(lyDo) ? "" : $"Lý do: {lyDo}"),
                "XÁC NHẬN RÚT", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            // Gọi BUS xử lý: giảm quota + tạo dòng Âm audit + tạo Phiếu Chi
            // (Chuẩn 3-Layer: GUI -> BUS -> DAL, KHÔNG bypass)
            var kq = BUS_DoanKhach.Instance.RutBotDichVu(
                idDV, soRut, lyDo, SessionManager.CurrentUser?.Id ?? 0);

            if (kq.IsSuccess)
            {
                TDCMessageBox.Show(
                    $"✅ {kq.ErrorMessage}\n" +
                    (string.IsNullOrEmpty(lyDo) ? "" : $"📝 Lý do: {lyDo}"),
                    "RÚT THÀNH CÔNG");
            }
            else
            {
                TDCMessageBox.Show(kq.ErrorMessage, "Lỗi");
                return;
            }

            // Refresh lại lưới quota từ DB mới
            RefreshQuota();
        }

        // 
        // CHỐT ĐOÀN (Chuyển sang DaHoanTat)
        // 

        private void btnChotDoan_Click(object sender, EventArgs e)
        {
            if (_doan == null) { TDCMessageBox.Show("Chưa chọn đoàn.", "Thông báo"); return; }

            if (_doan.TrangThai == AppConstants.TrangThaiDoanKhach.DaHoanTat)
            {
                TDCMessageBox.Show("Đoàn đã chốt sổ rồi.", "Thông báo");
                return;
            }

            // Tổng kết
            string summary = $"CHỐT ĐOÀN: {_doan.TenDoan}\n\n";
            if (_dichVuList != null)
            {
                foreach (var dv in _dichVuList)
                {
                    string tenDV = !string.IsNullOrEmpty(dv.TenDichVu) ? dv.TenDichVu
                                 : !string.IsNullOrEmpty(dv.TenCombo) ? dv.TenCombo
                                 : "DV #" + dv.Id;
                    summary += $"  • {tenDV}: {dv.SoLuongDaDung}/{dv.SoLuong} ({dv.TenTrangThai})\n";
                }
            }
            summary += $"\nTổng tiền hợp đồng: {_dichVuList?.Sum(x => x.ThanhTien):N0} VNĐ";
            summary += "\n\n Sau khi chốt, đoàn sẽ KHÔNG thể phục vụ thêm.\nXác nhận?";

            if (TDCMessageBox.Show(summary, "CHỐT SỔ ĐOÀN", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            var result = BUS_DoanKhach.Instance.ChotDoan(_doan.Id);
            if (result.IsSuccess)
            {
                TDCMessageBox.Show(result.ErrorMessage, "HOÀN TẤT");
                _doan.TrangThai = AppConstants.TrangThaiDoanKhach.DaHoanTat;
                HienThiDoan();
            }
            else
            {
                TDCMessageBox.Show(result.ErrorMessage, "Lỗi");
            }
        }

        // 
        // CLEAR
        // 

        private void ClearAll()
        {
            _doan = null;
            _dichVuList = null;
            pnlDoanInfo.Visible = false;
            pnlToolbar.Visible = false;
            gcQuota.DataSource = null;
            lblTenDoan.Text = "";
            lblSubText.Text = "";
            lblTrangThai.Text = "";
            lblTongQuota.Text = "";
        }
    }
}

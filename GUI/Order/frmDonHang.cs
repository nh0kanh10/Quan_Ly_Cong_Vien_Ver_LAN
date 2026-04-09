using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BUS;
using ET;
using FontAwesome.Sharp;

namespace GUI
{
    public partial class frmDonHang : Form, IBaseForm, AI.IAIFormContext
    {
        // ── AI Context ──
        public string AIContextName => "frmDonHang";
        public string AIContextDescription =>
            "Form Đơn Hàng: tra cứu hoá đơn, lịch sử bán hàng, thống kê doanh thu. " +
            "Người dùng có thể hỏi về đơn hàng theo ngày, trạng thái, tổng doanh thu.";

        public frmDonHang()
        {
            InitializeComponent();

            InitIcons();
            ApplyStyles();
            ApplyPermissions();
            
            cboTrangThai.SelectedIndex = 0;
        }

        private void frmDonHang_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) LoadData();
        }

        public void ApplyPermissions()
        {
            var tk = ET.SessionManager.CurrentUser;
            if (tk == null) return;

            if (!BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "VIEW_DONHANG"))
            {
                this.Enabled = false;
                return;
            }

            bool canManage = BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "MANAGE_DONHANG");
            btnHuyDon.Enabled = canManage;
        }

        public void ApplyStyles()
        {
            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleDevExpressGrid(gridData);
        }

        public void InitIcons()
        {
            // Icons
            btnLoc.Image = IconHelper.GetBitmap(IconChar.Filter, Color.White, 14);
            btnXemChiTiet.Image = IconHelper.GetBitmap(IconChar.Eye, Color.White, 14);
            btnInBill.Image = IconHelper.GetBitmap(IconChar.Print, Color.White, 14);
            btnHuyDon.Image = IconHelper.GetBitmap(IconChar.Ban, Color.White, 14);
        }



        public void LoadData()
        {
            DateTime tuNgay = dtpTuNgay.DateTime.Date;
            DateTime denNgay = dtpDenNgay.DateTime.Date.AddDays(1).AddSeconds(-1);
            string kw = txtTimKiem.Text.Trim().ToLower();
            string trangThai = cboTrangThai.Text;

            var allData = BUS_DonHang.Instance.LoadDS();
            var filtered = allData.Where(x => x.ThoiGian >= tuNgay && x.ThoiGian <= denNgay).ToList();

            if (trangThai != "Tất cả")
            {
                filtered = filtered.Where(x => x.TrangThai == trangThai).ToList();
            }

            if (!string.IsNullOrEmpty(kw))
            {
                filtered = filtered.Where(x => 
                    (x.MaCode != null && x.MaCode.ToLower().Contains(kw)) ||
                    (x.GhiChu != null && x.GhiChu.ToLower().Contains(kw))
                ).ToList();
            }

            // Gắn tên KH
            var khList = BUS_KhachHang.Instance.LoadDS();
            var displayData = filtered.Select(x => new
            {
                x.Id,
                x.MaCode,
                KhachHang = khList.FirstOrDefault(k => k.Id == x.IdKhachHang)?.HoTen ?? "Khách vãng lai",
                x.NguonBan,
                x.ThoiGian,
                x.TongTien,
                x.TienGiamGia,
                x.TrangThai,
                x.TenTrangThai,
                x.GhiChu
            }).OrderByDescending(x => x.ThoiGian).ToList();

            gridData.DataSource = displayData;
            gridView.PopulateColumns();

            if (gridView.Columns.Count > 0)
            {
                if (gridView.Columns["Id"] != null) gridView.Columns["Id"].Visible = false;

                if (gridView.Columns["MaCode"] != null) { gridView.Columns["MaCode"].Caption = "Mã Đơn"; gridView.Columns["MaCode"].Width = 120; }
                if (gridView.Columns["KhachHang"] != null) { gridView.Columns["KhachHang"].Caption = "Khách Hàng"; gridView.Columns["KhachHang"].Width = 150; }
                if (gridView.Columns["ThoiGian"] != null)
                {
                    gridView.Columns["ThoiGian"].Caption = "Thời Gian";
                    gridView.Columns["ThoiGian"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    gridView.Columns["ThoiGian"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
                    gridView.Columns["ThoiGian"].Width = 120;
                }
                if (gridView.Columns["TongTien"] != null)
                {
                    gridView.Columns["TongTien"].Caption = "Tổng Tiền";
                    gridView.Columns["TongTien"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gridView.Columns["TongTien"].DisplayFormat.FormatString = "N0";
                    gridView.Columns["TongTien"].Width = 100;
                }
                if (gridView.Columns["TienGiamGia"] != null)
                {
                    gridView.Columns["TienGiamGia"].Caption = "Giảm Giá";
                    gridView.Columns["TienGiamGia"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gridView.Columns["TienGiamGia"].DisplayFormat.FormatString = "N0";
                    gridView.Columns["TienGiamGia"].Width = 100;
                }
                if (gridView.Columns["TrangThai"] != null) gridView.Columns["TrangThai"].Visible = false;
                if (gridView.Columns["NguonBan"] != null) { gridView.Columns["NguonBan"].Caption = "Nguồn Bán"; gridView.Columns["NguonBan"].Width = 80; }
                if (gridView.Columns["TenTrangThai"] != null) { gridView.Columns["TenTrangThai"].Caption = "Trạng Thái"; gridView.Columns["TenTrangThai"].Width = 100; }
                if (gridView.Columns["GhiChu"] != null) { gridView.Columns["GhiChu"].Caption = "Ghi Chú"; gridView.Columns["GhiChu"].Width = 150; }
            }
        }

        private void BtnXemChiTiet_Click(object sender, EventArgs e)
        {
            if (gridView.FocusedRowHandle < 0) return;
            string maCode = gridView.GetRowCellValue(gridView.FocusedRowHandle, "MaCode").ToString();
            
            frmChiTietHoaDon f = new frmChiTietHoaDon(maCode);
            ThemeManager.ShowAsPopup(f);
        }

        private void BtnInBill_Click(object sender, EventArgs e)
        {
            // Placeholder for Print logic using Crystal Reports
            // To be implemented fully in Reporting Phase if requested
            if (gridView.FocusedRowHandle < 0) return;
            string maCode = gridView.GetRowCellValue(gridView.FocusedRowHandle, "MaCode").ToString();
            TDCMessageBox.Show("Chức năng in bill cho đơn " + maCode + " (Sẽ tích hợp Crystal Report sau)!", "Thông báo");
        }

        private void BtnHuyDon_Click(object sender, EventArgs e)
        {
            if (gridView.FocusedRowHandle < 0) return;

            string maCode = gridView.GetRowCellValue(gridView.FocusedRowHandle, "MaCode").ToString();
            string trangThai = gridView.GetRowCellValue(gridView.FocusedRowHandle, "TrangThai").ToString();

            if (trangThai == AppConstants.TrangThaiDonHang.DaHuy)
            {
                TDCMessageBox.Show("Đơn hàng này đã bị hủy trước đó!", "Cảnh báo");
                return;
            }

            if (TDCMessageBox.Show("Bạn có chắc chắn muốn HỦY đơn hàng " + maCode + "?", "Xác nhận hủy", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int id = Convert.ToInt32(gridView.GetRowCellValue(gridView.FocusedRowHandle, "Id"));
                var dh = BUS_DonHang.Instance.GetById(id);
                dh.TrangThai = AppConstants.TrangThaiDonHang.DaHuy;
                dh.GhiChu = "Đã hủy vào " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                
                var result = BUS_DonHang.Instance.SuaDonHang(dh);
                if (result.IsSuccess)
                {
                    TDCMessageBox.Show("Hủy đơn thành công!", "Thành công");
                    LoadData();
                }
                else
                {
                    TDCMessageBox.Show(result.ErrorMessage, "Lỗi");
                }
            }
        }

        
    }
}





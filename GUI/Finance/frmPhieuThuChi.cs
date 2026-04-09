using BUS;
using DevExpress.XtraGrid;
using DevExpress.XtraVerticalGrid;
using ET;
using FontAwesome.Sharp;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmPhieuThuChi : Form, IBaseForm
    {
        public frmPhieuThuChi()
        {
            InitializeComponent();

            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleDevExpressGrid(gridThu);
            ThemeManager.StyleDevExpressGrid(gridChi);

            InitIcons();
            ApplyPermissions();

            dtpFrom.DateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpTo.DateTime = DateTime.Today;
        }

        public void ApplyPermissions()
        {
            var tk = ET.SessionManager.CurrentUser;
            if (tk == null) return;

            if (!BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "VIEW_LEDGER"))
            {
                this.Enabled = false;
                return;
            }
        }
        
        public void InitIcons()
        {
            btnLoad.Image = IconHelper.GetBitmap(IconChar.Filter, Color.White, 16);
        }

        private void frmPhieuThuChi_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            var from = dtpFrom.DateTime.Date;
            var to = dtpTo.DateTime.Date.AddDays(1).AddSeconds(-1);

            var thu = BUS_PhieuThuChi.Instance.LoadPhieuThu(from, to);
            gridThu.DataSource = new BindingList<ET_PhieuThu>(thu);
            gridViewThu.PopulateColumns();
            FormatGridThu();

            var chi = BUS_PhieuThuChi.Instance.LoadPhieuChi(from, to);
            gridChi.DataSource = new BindingList<ET_PhieuChi>(chi);
            gridViewChi.PopulateColumns();
            FormatGridChi();

            // Summary
            decimal tongThu = thu.Sum(x => x.SoTien);
            decimal tongChi = chi.Sum(x => x.SoTien);
            decimal tonQuy = tongThu - tongChi;

            lblTongThu.Text = string.Format("{0:N0} đ", tongThu);
            lblTongChi.Text = string.Format("{0:N0} đ", tongChi);
            if (lblTonQuy != null) 
            {
                lblTonQuy.Text = string.Format("{0:N0} đ", tonQuy);
                lblTonQuy.ForeColor = tonQuy < 0 ? Color.FromArgb(239, 68, 68) : Color.FromArgb(41, 128, 185);
            }
        }
        public void ApplyStyles()
        {
            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleDevExpressGrid(gridThu);
            ThemeManager.StyleDevExpressGrid(gridChi);

        }
        private void FormatGridThu()
        {
            var view = gridViewThu;
            if (view.Columns["Id"] != null) view.Columns["Id"].Visible = false;
            if (view.Columns["CreatedAt"] != null) view.Columns["CreatedAt"].Visible = false;
            if (view.Columns["CreatedBy"] != null) view.Columns["CreatedBy"].Visible = false;
            if (view.Columns["IdDonHang"] != null) view.Columns["IdDonHang"].Caption = "Mã ĐH";
            if (view.Columns["IdGiaoDichVi"] != null) view.Columns["IdGiaoDichVi"].Caption = "Mã GD Ví";
            if (view.Columns["MaCode"] != null) view.Columns["MaCode"].Caption = "Mã phiếu";
            if (view.Columns["SoTien"] != null)
            {
                view.Columns["SoTien"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                view.Columns["SoTien"].DisplayFormat.FormatString = "N0";
                view.Columns["SoTien"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            }
            view.RowCellStyle -= GridViewThu_RowCellStyle;
            view.RowCellStyle += GridViewThu_RowCellStyle;
            if (view.Columns["PhuongThuc"] != null) view.Columns["PhuongThuc"].Caption = "Phương thức";
            if (view.Columns["MaGiaoDichDoiTac"] != null) view.Columns["MaGiaoDichDoiTac"].Caption = "Mã GD đối tác";
            if (view.Columns["ThoiGian"] != null)
            {
                view.Columns["ThoiGian"].Caption = "Thời gian";
                view.Columns["ThoiGian"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                view.Columns["ThoiGian"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            }
            view.OptionsView.ColumnAutoWidth = true;
        }

        private void FormatGridChi()
        {
            var view = gridViewChi;
            if (view.Columns["Id"] != null) view.Columns["Id"].Visible = false;
            if (view.Columns["CreatedAt"] != null) view.Columns["CreatedAt"].Visible = false;
            if (view.Columns["CreatedBy"] != null) view.Columns["CreatedBy"].Visible = false;
            if (view.Columns["MaCode"] != null) view.Columns["MaCode"].Caption = "Mã phiếu";
            if (view.Columns["SoTien"] != null)
            {
                view.Columns["SoTien"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                view.Columns["SoTien"].DisplayFormat.FormatString = "N0";
                view.Columns["SoTien"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            }
            view.RowCellStyle -= GridViewChi_RowCellStyle;
            view.RowCellStyle += GridViewChi_RowCellStyle;
            if (view.Columns["LyDo"] != null) view.Columns["LyDo"].Caption = "Lý do";
            if (view.Columns["ThoiGian"] != null)
            {
                view.Columns["ThoiGian"].Caption = "Thời gian";
                view.Columns["ThoiGian"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                view.Columns["ThoiGian"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            }
            view.OptionsView.ColumnAutoWidth = true;
        }

        private void GridViewThu_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "SoTien")
            {
                e.Appearance.ForeColor = Color.FromArgb(16, 185, 129);
                e.Appearance.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            }
        }

        private void GridViewChi_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "SoTien")
            {
                e.Appearance.ForeColor = Color.FromArgb(239, 68, 68);
                e.Appearance.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            }
        }

        private void cboKyBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboKyBaoCao.SelectedIndex == -1) return;
            string sel = cboKyBaoCao.SelectedItem.ToString();
            DateTime now = DateTime.Today;

            if (sel == "Hôm nay") { dtpFrom.DateTime = now; dtpTo.DateTime = now; }
            else if (sel == "Tuần này") 
            { 
                 int diff = (7 + (now.DayOfWeek - DayOfWeek.Monday)) % 7;
                 dtpFrom.DateTime = now.AddDays(-1 * diff);
                 dtpTo.DateTime = dtpFrom.DateTime.AddDays(6);
            }
            else if (sel == "Tháng này") { dtpFrom.DateTime = new DateTime(now.Year, now.Month, 1); dtpTo.DateTime = dtpFrom.DateTime.AddMonths(1).AddDays(-1); }
            else if (sel == "Tháng trước") 
            { 
                 var prev = now.AddMonths(-1);
                 dtpFrom.DateTime = new DateTime(prev.Year, prev.Month, 1);
                 dtpTo.DateTime = dtpFrom.DateTime.AddMonths(1).AddDays(-1);
            }
            else if (sel == "Quý này") 
            { 
                 int quarterNumber = (now.Month - 1) / 3 + 1;
                 dtpFrom.DateTime = new DateTime(now.Year, (quarterNumber - 1) * 3 + 1, 1);
                 dtpTo.DateTime = dtpFrom.DateTime.AddMonths(3).AddDays(-1);
            }
            if(sel != "Tùy chọn") LoadData();
        }
    }
}



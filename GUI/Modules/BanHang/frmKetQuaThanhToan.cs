using System;
using System.Data;
using System.Windows.Forms;
using ET.DTOs;
using GUI.Infrastructure;
using DevExpress.XtraEditors;

namespace GUI.Modules.BanHang
{
    public partial class frmKetQuaThanhToan : DevExpress.XtraEditors.XtraForm
    {
        private DTO_CheckoutResult _result;

        public frmKetQuaThanhToan(DTO_CheckoutResult result)
        {
            InitializeComponent();
            _result = result;
        }

        public frmKetQuaThanhToan(string maDonHang, bool isHistory)
        {
            InitializeComponent();
            _result = new DTO_CheckoutResult { MaDonHang = maDonHang, TongThanhToan = 0, TienThua = 0 };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;

            AppStyle.StyleForm(this);
            ApplyLanguage();

            lblMaDon.Text = $"{LanguageManager.GetString("LBL_ORDER_ID") ?? "Mã đơn"}: {_result.MaDonHang}";
            lblTienThuaValue.Text = $"{_result.TienThua:#,##0} ₫";

            if (_result.DanhSachMaVachVe != null && _result.DanhSachMaVachVe.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("MaVach", typeof(string));
                dt.Columns.Add("TenVe", typeof(string));
                foreach (var ve in _result.DanhSachMaVachVe)
                {
                    dt.Rows.Add(ve.MaVach, ve.TenVe);
                }
                gridVe.DataSource = dt;
            }
            else
            {
                pnlMid.Visible = false;
                this.Height -= pnlMid.Height;
                btnInVe.Enabled = false;
            }
        }

        private void ApplyLanguage()
        {
            lblTitle.Text = LanguageManager.GetString("LBL_PAYMENT_SUCCESS") ?? "THANH TOÁN THÀNH CÔNG";
            lblTienThuaText.Text = LanguageManager.GetString("LBL_CHANGE") ?? "Tiền thừa:";
            colMaVach.Caption = LanguageManager.GetString("LBL_TICKET_BARCODES") ?? "Danh sách mã vạch Vé đã phát hành";
            btnInHoaDon.Text = LanguageManager.GetString("BTN_PRINT_INVOICE") ?? "In Hóa Đơn";
            btnInVe.Text = LanguageManager.GetString("BTN_PRINT_TICKET") ?? "In Vé";
            btnDong.Text = LanguageManager.GetString("BTN_CLOSE") ?? "Đóng (Enter)";
        }

        private void BtnInHoaDon_Click(object sender, EventArgs e)
        {
            // Sử dụng DevExpress XtraReport để in hóa đơn K80
            rptHoaDonK80 rpt = new rptHoaDonK80(_result);
            DevExpress.XtraReports.UI.ReportPrintTool printTool = new DevExpress.XtraReports.UI.ReportPrintTool(rpt);
            printTool.ShowRibbonPreviewDialog();
        }

        private void BtnInVe_Click(object sender, EventArgs e)
        {
            if (_result.DanhSachMaVachVe == null || _result.DanhSachMaVachVe.Count == 0) return;

            Form frmPreview = new Form();
            frmPreview.Text = "Demo In Thẻ Vé (QR Code)";
            frmPreview.Size = new System.Drawing.Size(750, 500);
            frmPreview.StartPosition = FormStartPosition.CenterParent;
            frmPreview.FormBorderStyle = FormBorderStyle.FixedDialog;
            frmPreview.MaximizeBox = false;
            frmPreview.MinimizeBox = false;

            FlowLayoutPanel flowPanel = new FlowLayoutPanel();
            flowPanel.Dock = DockStyle.Fill;
            flowPanel.AutoScroll = true;
            flowPanel.Padding = new Padding(10);
            frmPreview.Controls.Add(flowPanel);

            foreach (var ve in _result.DanhSachMaVachVe)
            {
                var ucVe = new ucVeDienTu(ve.TenVe, ve.MaVach);
                flowPanel.Controls.Add(ucVe);
            }

            frmPreview.ShowDialog(this);
        }

        private void BtnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter || keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}

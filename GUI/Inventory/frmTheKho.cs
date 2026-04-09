using System;
using System.Drawing;
using System.Windows.Forms;
using BUS;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace GUI.Inventory
{
    public partial class frmTheKho : Form
    {
        private int _idKho;
        private int _idSanPham;

        public frmTheKho(int idKho, int idSanPham, string tenKho, string tenSp, string maSp, string dvt)
        {
            InitializeComponent();
            _idKho = idKho;
            _idSanPham = idSanPham;
            this.Text = $"Thẻ Kho: {tenSp}"; // Label cho MDI Tab

            // Default: 30 days history
            dtpTuNgay.DateTime = DateTime.Today.AddDays(-30);
            dtpDenNgay.DateTime = DateTime.Today;
        }

        public void HideHeadersAndShow()
        {
           
            this.Show();
        }

        public void UpdateData(int idKho, int idSanPham, string tenKho, string tenSp, string maSp, string dvt)
        {
            _idKho = idKho;
            _idSanPham = idSanPham;
            LoadData();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F10)
            {
                InBaoCao();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void InBaoCao()
        {
            MessageBox.Show("Chức năng In báo cáo (Crystal Reports).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnInBaoCao_Click(object sender, EventArgs e)
        {
            InBaoCao();
        }

        private void frmTheKho_Load(object sender, EventArgs e)
        {
            ThemeManager.ApplyTheme(this);
            LoadData();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            var ds = BUS_TheKho.Instance.XemTheKho(_idKho, _idSanPham, dtpTuNgay.DateTime, dtpDenNgay.DateTime);
            gridTheKho.DataSource = ds;
            gridViewTheKho.PopulateColumns();
            FormatGrid();
        }

        private void FormatGrid()
        {
            var v = gridViewTheKho;
            if (v.Columns["Id"] != null) v.Columns["Id"].Visible = false;

            if (v.Columns["ThoiGian"] != null)
            {
                v.Columns["ThoiGian"].Caption = "Thời Gian";
                v.Columns["ThoiGian"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                v.Columns["ThoiGian"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
                v.Columns["ThoiGian"].Width = 130;
            }

            if (v.Columns["SoChungTu"] != null) { v.Columns["SoChungTu"].Caption = "Số Chứng Từ"; v.Columns["SoChungTu"].Width = 100; }
            if (v.Columns["LoaiGiaoDich"] != null) { v.Columns["LoaiGiaoDich"].Caption = "Loại Diễn Giải"; v.Columns["LoaiGiaoDich"].Width = 150; }
            if (v.Columns["GhiChu"] != null) { v.Columns["GhiChu"].Caption = "Ghi Chú"; v.Columns["GhiChu"].Width = 200; }
            if (v.Columns["NguoiTao"] != null) { v.Columns["NguoiTao"].Caption = "Người Lập"; v.Columns["NguoiTao"].Width = 120; }

            // Group Number Formatting
            string[] numCols = { "TonDauKy", "Nhap", "Xuat", "TonCuoiKy" };
            foreach (var col in numCols)
            {
                if (v.Columns[col] != null)
                {
                    v.Columns[col].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    v.Columns[col].DisplayFormat.FormatString = "n0";
                    v.Columns[col].Width = 80;
                }
            }

            if (v.Columns["TonDauKy"] != null) v.Columns["TonDauKy"].Caption = "Tồn Đầu";
            if (v.Columns["TonCuoiKy"] != null) v.Columns["TonCuoiKy"].Caption = "Tồn Cuối";
            if (v.Columns["Nhap"] != null) v.Columns["Nhap"].Caption = "Nhập";
            if (v.Columns["Xuat"] != null) v.Columns["Xuat"].Caption = "Xuất";

            // Currency formatting
            if (v.Columns["DonGiaVatTu"] != null)
            {
                v.Columns["DonGiaVatTu"].Caption = "Đơn Giá (VNĐ)";
                v.Columns["DonGiaVatTu"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                v.Columns["DonGiaVatTu"].DisplayFormat.FormatString = "n0";
                v.Columns["DonGiaVatTu"].Width = 100;
            }
            if (v.Columns["ThanhTien"] != null)
            {
                v.Columns["ThanhTien"].Caption = "Thành Tiền (VNĐ)";
                v.Columns["ThanhTien"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                v.Columns["ThanhTien"].DisplayFormat.FormatString = "n0";
                v.Columns["ThanhTien"].Width = 120;
            }

            v.RowCellStyle -= GridViewTheKho_RowCellStyle;
            v.RowCellStyle += GridViewTheKho_RowCellStyle;
        }

        private void GridViewTheKho_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                if (e.Column.FieldName == "Nhap")
                {
                    int nhap = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "Nhap") ?? 0);
                    if (nhap > 0)
                    {
                        e.Appearance.ForeColor = Color.MediumSeaGreen;
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    }
                }
                else if (e.Column.FieldName == "Xuat")
                {
                    int xuat = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "Xuat") ?? 0);
                    if (xuat > 0)
                    {
                        e.Appearance.ForeColor = Color.Crimson;
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    }
                }
                else if (e.Column.FieldName == "TonCuoiKy")
                {
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
            }
        }
    }
}

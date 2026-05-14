using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.Linq;
using GUI.Infrastructure;

namespace GUI.Modules.Kho
{
    public partial class ucTonKho : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly Action<object> _onLanguageChanged;

        public ucTonKho()
        {
            InitializeComponent();
            KhoiTao();
            
            _onLanguageChanged = _ =>
            {
                if (this.IsHandleCreated && !this.IsDisposed)
                    this.Invoke((MethodInvoker)delegate { ThucHienDichNgonNgu(); });
            };
            EventBus.Subscribe("LanguageChanged", _onLanguageChanged);
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

        private void KhoiTao()
        {
            lblNhomSP.Visible = false;
            cboNhomSP.Visible = false;

            ThucHienDichNgonNgu();
            viewTonKho.RowStyle += ViewTonKho_RowStyle;
            btnLoc.Click += BtnLoc_Click;
            btnExcel.Click += BtnExcel_Click;
            LoadDuLieu();
        }

        private void BtnExcel_Click(object sender, EventArgs e)
        {
            using (var dlg = new SaveFileDialog())
            {
                dlg.Filter = "Excel Files|*.xlsx";
                dlg.FileName = $"TonKho_{DateTime.Now:yyyyMMdd}";
                dlg.Title = LanguageManager.GetString("CTX_XUAT_EXCEL") ?? "Xuất Excel";

                if (dlg.ShowDialog() != DialogResult.OK) return;

                gridTonKho.ExportToXlsx(dlg.FileName);
                UIHelper.ThongBao($"{LanguageManager.GetString("TITLE_SUCCESS") ?? "Thành công"}: {dlg.FileName}");
            }
        }

        public void ThucHienDichNgonNgu()
        {
            if (viewTonKho.Columns["MaSanPham"] != null) viewTonKho.Columns["MaSanPham"].Caption = LanguageManager.GetString("COL_MAHANG") ?? "Mã SP";
            if (viewTonKho.Columns["TenSanPham"] != null) viewTonKho.Columns["TenSanPham"].Caption = LanguageManager.GetString("COL_TENHANG") ?? "Tên Sản Phẩm";
            if (viewTonKho.Columns["DVT"] != null) viewTonKho.Columns["DVT"].Caption = LanguageManager.GetString("COL_DVT") ?? "ĐVT";
            if (viewTonKho.Columns["TenKho"] != null) viewTonKho.Columns["TenKho"].Caption = LanguageManager.GetString("COL_KHO") ?? "Kho";
            if (viewTonKho.Columns["TonHienTai"] != null) 
            {
                viewTonKho.Columns["TonHienTai"].Caption = LanguageManager.GetString("COL_TONKHO") ?? "Tồn Kho";
                viewTonKho.Columns["TonHienTai"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                viewTonKho.Columns["TonHienTai"].DisplayFormat.FormatString = "#,##0.###";
            }
            if (viewTonKho.Columns["MucCanhBao"] != null)
            {
                viewTonKho.Columns["MucCanhBao"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                viewTonKho.Columns["MucCanhBao"].DisplayFormat.FormatString = "#,##0.###";
            }
            if (viewTonKho.Columns["TrangThai"] != null) viewTonKho.Columns["TrangThai"].Caption = LanguageManager.GetString("COL_TRANGTHAI") ?? "Trạng Thái";

            lblKho.Text = LanguageManager.GetString("LBL_KHO") ?? "Kho:";
            lblNhomSP.Text = LanguageManager.GetString("LBL_NHOMSP") ?? "Nhóm SP:";
            btnLoc.Text = LanguageManager.GetString("BTN_LOC") ?? "LỌC";
            btnExcel.Text = LanguageManager.GetString("BTN_EXCEL") ?? "Xuất Excel";

            // Dịch dropdown hiển thị
            var dsKho = BUS.Services.Kho.BUS_Kho.Instance.GetKhoHoatDong(GUI.Infrastructure.SessionManager.CurrentLanguage);
            slkKho.Properties.DataSource = dsKho;
            slkKho.Properties.DisplayMember = "TenKho";
            slkKho.Properties.ValueMember = "Id";
            slkKho.Properties.NullText = LanguageManager.GetString("COL_KHOXUAT") ?? "-- Chọn --";
            if (slkKho.Properties.View.Columns["TenKho"] != null) slkKho.Properties.View.Columns["TenKho"].Caption = LanguageManager.GetString("COL_TEN") ?? "Tên Kho";
            cboNhomSP.Properties.NullText = LanguageManager.GetString("LBL_NHOMSP") ?? "-- Chọn --";
        }

        private void LoadDuLieu()
        {
            try
            {
                int? idKho = null;
                if (slkKho.EditValue != null && int.TryParse(slkKho.EditValue.ToString(), out int parsedId) && parsedId > 0)
                {
                    idKho = parsedId;
                }
                var data = BUS.Services.Kho.BUS_SoCai.Instance.GetBaoCaoTonKho(idKho);
                gridTonKho.DataSource = data;
            }
            catch (Exception ex)
            {
                UIHelper.Loi(ex.Message);
            }
        }

        private void ViewTonKho_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string status = view.GetRowCellDisplayText(e.RowHandle, view.Columns["TrangThai"]);
                if (status == ET.Constants.AppConstants.TrangThaiTonKho.DuoiMuc)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 235, 238); 
                    e.Appearance.ForeColor = Color.DarkRed;
                }
            }
        }

        private void BtnLoc_Click(object sender, EventArgs e)
        {
            LoadDuLieu();
        }
    }
}

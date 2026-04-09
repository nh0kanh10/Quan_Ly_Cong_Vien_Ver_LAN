using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BUS;
using ET;
using FontAwesome.Sharp;

namespace GUI
{
    public partial class frmViDienTu : Form, IBaseForm
    {
        public frmViDienTu()
        {
            InitializeComponent();

            InitIcons();
            ApplyStyles();
            ApplyPermissions();

            this.Load += (s, e) => LoadData();
            txtSearch.TextChanged += (s, e) => ApplyFilter();
            btnRefresh.Click += (s, e) => { txtSearch.Clear(); LoadData(); };
        }

        public void ApplyPermissions()
        {
            var tk = ET.SessionManager.CurrentUser;
            if (tk == null) return;

            if (!BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "VIEW_WALLET"))
            {
                this.Enabled = false;
                return;
            }
        }

        public void ApplyStyles()
        {
            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleDevExpressGrid(gridControl);
        }

        public void InitIcons()
        {
            btnRefresh.Image = IconHelper.GetBitmap(IconChar.ArrowsRotate, Color.White, 16);
        }

        public void LoadData()
        {
            var data = BUS_ViDienTu.Instance.LoadDS();
            gridControl.DataSource = new BindingList<ET_ViDienTu>(data);
            gridView.PopulateColumns();
            FormatGrid();
        }

        private void FormatGrid()
        {
            var view = gridView;
            if (view.Columns["RowVer"] != null) view.Columns["RowVer"].Visible = false;
            if (view.Columns["Id"] != null) view.Columns["Id"].Caption = "Mã ví";
            if (view.Columns["IdKhachHang"] != null) view.Columns["IdKhachHang"].Caption = "Mã KH";
            if (view.Columns["SoDuKhaDung"] != null)
            {
                view.Columns["SoDuKhaDung"].Caption = "Số dư khả dụng";
                view.Columns["SoDuKhaDung"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                view.Columns["SoDuKhaDung"].DisplayFormat.FormatString = "N0";
            }
            if (view.Columns["SoDuDongBang"] != null)
            {
                view.Columns["SoDuDongBang"].Caption = "Số dư đóng băng";
                view.Columns["SoDuDongBang"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                view.Columns["SoDuDongBang"].DisplayFormat.FormatString = "N0";
            }
            view.OptionsView.ColumnAutoWidth = true;
        }

        private void ApplyFilter()
        {
            var kw = txtSearch.Text.Trim();
            var data = BUS_ViDienTu.Instance.TimKiem(kw, null);
            gridControl.DataSource = new BindingList<ET_ViDienTu>(data);
        }
    }
}



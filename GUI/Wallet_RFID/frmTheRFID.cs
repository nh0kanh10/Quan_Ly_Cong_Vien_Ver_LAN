using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BUS;
using ET;
using FontAwesome.Sharp;
using DevExpress.XtraGrid.Views.Base;

namespace GUI
{
    public partial class frmTheRFID : Form, IBaseForm
    {
        public frmTheRFID()
        {
            InitializeComponent();

            InitIcons();
            ApplyStyles();
            ApplyPermissions();

            this.Load += (s, e) => LoadData();
            txtSearch.TextChanged += (s, e) => LoadData();
            btnLock.Click += (s, e) => UpdateStatus(true);
            btnUnlock.Click += (s, e) => UpdateStatus(false);
            btnRefresh.Click += (s, e) => { txtSearch.Clear(); LoadData(); };
        }

        public void ApplyPermissions()
        {
            var tk = ET.SessionManager.CurrentUser;
            if (tk == null) return;

            if (!BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "VIEW_RFID"))
            {
                this.Enabled = false;
                return;
            }

            bool canManage = BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "MANAGE_RFID");
            btnLock.Enabled = canManage;
            btnUnlock.Enabled = canManage;
        }

        public void ApplyStyles()
        {
            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleDevExpressGrid(gridControl);
        }

        public void InitIcons()
        {
            btnLock.Image = IconHelper.GetBitmap(IconChar.Lock, Color.White, 16);
            btnUnlock.Image = IconHelper.GetBitmap(IconChar.LockOpen, Color.White, 16);
            btnRefresh.Image = IconHelper.GetBitmap(IconChar.ArrowsRotate, Color.White, 16);
        }

        public void LoadData()
        {
            var kw = txtSearch.Text.Trim().ToLowerInvariant();
            var data = BUS_TheRFID.Instance.TimKiem(kw);
            gridControl.DataSource = new BindingList<ET_TheRFID>(data);
            gridView.PopulateColumns();
            FormatGrid();
        }

        private void FormatGrid()
        {
            var view = gridView;
            if (view.Columns["MaRfid"] != null) view.Columns["MaRfid"].Caption = "Mã thẻ RFID";
            if (view.Columns["IdVi"] != null) view.Columns["IdVi"].Caption = "Mã Ví";
            if (view.Columns["TrangThai"] != null) view.Columns["TrangThai"].Visible = false;
            if (view.Columns["TenTrangThai"] != null) view.Columns["TenTrangThai"].Caption = "Trạng thái";
            if (view.Columns["NgayKichHoat"] != null)
            {
                view.Columns["NgayKichHoat"].Caption = "Ngày kích hoạt";
                view.Columns["NgayKichHoat"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                view.Columns["NgayKichHoat"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            }
            if (view.Columns["NgayHuy"] != null)
            {
                view.Columns["NgayHuy"].Caption = "Ngày hủy/khóa";
                view.Columns["NgayHuy"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                view.Columns["NgayHuy"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            }
            view.OptionsView.ColumnAutoWidth = true;
        }

        private void UpdateStatus(bool isLock)
        {
            if (gridView.FocusedRowHandle < 0)
            {
                TDCMessageBox.Show("Vui lòng chọn thẻ RFID cần thao tác!", "Thông báo");
                return;
            }

            var row = gridView.GetFocusedRow() as ET_TheRFID;
            if (row == null) return;

            var result = isLock
                ? BUS_TheRFID.Instance.KhoaThe(row.MaRfid)
                : BUS_TheRFID.Instance.MoThe(row.MaRfid);

            if (!result.IsSuccess)
            {
                TDCMessageBox.Show(result.ErrorMessage, "Lỗi");
                return;
            }

            LoadData();
            TDCMessageBox.Show(isLock ? "Đã khóa thẻ thành công." : "Đã mở khóa thẻ thành công.", "Thông báo");
        }
    }
}



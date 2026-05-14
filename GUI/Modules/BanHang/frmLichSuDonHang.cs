using System;
using System.Windows.Forms;
using ET.Results;
using BUS.Services.BanHang;

namespace GUI.Modules.BanHang
{
    public partial class frmLichSuDonHang : DevExpress.XtraEditors.XtraForm
    {
        private int _idPhienHienTai;

        public frmLichSuDonHang(int idPhienHienTai)
        {
            InitializeComponent();
            _idPhienHienTai = idPhienHienTai;
        }

        private void frmLichSuDonHang_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            var res = BUS_POS.Instance.LayDanhSachDonHangTheoPhien(_idPhienHienTai);
            if (res.Success)
            {
                gridLichSu.DataSource = res.Data;
            }
            else
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(res.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void viewLichSu_DoubleClick(object sender, EventArgs e)
        {
            var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null || view.FocusedRowHandle < 0) return;

            string maDon = view.GetRowCellValue(view.FocusedRowHandle, "MaDonHang")?.ToString();
            if (string.IsNullOrEmpty(maDon)) return;

            var frm = new frmKetQuaThanhToan(maDon, false); // false = không tự động in, cho user tuỳ chọn in lại
            frm.ShowDialog();
        }
    }
}

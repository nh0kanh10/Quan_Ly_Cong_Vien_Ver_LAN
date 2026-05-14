using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BUS.Services.BanHang;
using ET.DTOs;

namespace GUI.Modules.BanHang
{
    /// <summary>
    /// Popup xem danh sách phiếu đặt phòng đang chờ check-in (hôm nay / tuần này).
    /// Mở từ nút chuông trên màn hình sơ đồ phòng.
    /// </summary>
    public partial class frmDatPhongNotification : XtraForm
    {
        #region Khởi tạo và tải dữ liệu

        public frmDatPhongNotification()
        {
            InitializeComponent();
            TaiDuLieu("TuanNay");
        }

        private void TaiDuLieu(string locTheo)
        {
            var mauChon    = Color.FromArgb(33, 150, 243);
            var mauMacDinh = Color.Empty;
            var trangChon  = Color.White;
            var trangBinh  = Color.Empty;

            btnHomNay.Appearance.BackColor  = locTheo == "HomNay" ? mauChon : mauMacDinh;
            btnHomNay.Appearance.ForeColor  = locTheo == "HomNay" ? trangChon : trangBinh;
            
            btnTuanNay.Appearance.BackColor = locTheo == "TuanNay" ? mauChon : mauMacDinh;
            btnTuanNay.Appearance.ForeColor = locTheo == "TuanNay" ? trangChon : trangBinh;

            btnTatCa.Appearance.BackColor   = locTheo == "TatCa" ? mauChon : mauMacDinh;
            btnTatCa.Appearance.ForeColor   = locTheo == "TatCa" ? trangChon : trangBinh;

            var ketQua = BUS_LuuTru_Booking.Instance.LayDatPhongChoPhanCong(locTheo);
            if (ketQua.Success)
            {
                gridDatPhong.DataSource = ketQua.Data;
                this.Text = $"Phiếu đặt phòng chờ nhận — {ketQua.Data.Count} phiếu";
            }
            else
            {
                XtraMessageBox.Show(ketQua.ErrorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Xử lý sự kiện

        private void BtnHomNay_Click(object sender, EventArgs e)
        {
            TaiDuLieu("HomNay");
        }

        private void BtnTuanNay_Click(object sender, EventArgs e)
        {
            TaiDuLieu("TuanNay");
        }

        private void BtnTatCa_Click(object sender, EventArgs e)
        {
            TaiDuLieu("TatCa");
        }

        #endregion

        #region Hàm hỗ trợ

        private void GridViewMain_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            var phieu = gridViewMain.GetRow(e.RowHandle) as DTO_DatPhongOnline;
            if (phieu != null && phieu.NgayNhanPhong.Date == DateTime.Now.Date)
            {
                e.Appearance.BackColor = Color.FromArgb(255, 243, 205);
                e.Appearance.ForeColor = Color.FromArgb(130, 80, 0);
            }
        }

        #endregion
    }
}

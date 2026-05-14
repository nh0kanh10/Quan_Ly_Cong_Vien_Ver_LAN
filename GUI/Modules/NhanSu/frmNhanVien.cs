using System;
using System.Windows.Forms;
using ET.Results;
using ET.Models.DoiTac;
using BUS.Services.DoiTac;

namespace GUI.Modules.NhanSu
{
    public partial class frmNhanVien : DevExpress.XtraEditors.XtraForm
    {
        #region Khởi tạo và tải dữ liệu

        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void FrmNhanVien_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            gridDanhSach.DataSource = BUS_NhanVien.Instance.LayDanhSach();
        }

        #endregion

        #region Xử lý sự kiện

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            var frm = new frmHoSoNhanVien(null);
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                LoadData();
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            MoHoSoNhanVien();
        }

        private void GridViewDanhSach_DoubleClick(object sender, EventArgs e)
        {
            MoHoSoNhanVien();
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (!(gridViewDanhSach.GetFocusedRow() is DTO_NhanVienChiTiet nv)) return;

            string msg = string.Format(GUI.Infrastructure.LanguageManager.GetString("MSG_XAC_NHAN_XOA"), nv.HoTen);
            if (DevExpress.XtraEditors.XtraMessageBox.Show(msg, GUI.Infrastructure.LanguageManager.GetString("LBL_XAC_NHAN"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            OperationResult result = BUS_NhanVien.Instance.XoaMem(nv.IdDoiTac);
            if (result.Success)
                LoadData();
            else
                DevExpress.XtraEditors.XtraMessageBox.Show(GUI.Infrastructure.LanguageManager.GetString(result.Message), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        #region Hàm hỗ trợ

        private void MoHoSoNhanVien()
        {
            if (!(gridViewDanhSach.GetFocusedRow() is DTO_NhanVienChiTiet nv)) return;

            frmHoSoNhanVien frm = new frmHoSoNhanVien(nv.MaNhanVien);
            frm.ShowDialog();
            LoadData();
        }

        #endregion
    }
}

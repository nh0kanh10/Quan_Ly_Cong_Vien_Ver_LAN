using System;
using System.Windows.Forms;
using ET;

namespace GUI
{
    public partial class frmTaiNanLaoDongDialog : Form
    {
        private int _idNhanVien;

        private static readonly string[] LoaiList = 
        {
            AppConstants.LoaiTaiNan.TrongGioLam,
            AppConstants.LoaiTaiNan.NgoaiGioLam,
            AppConstants.LoaiTaiNan.DiLai
        };

        private static readonly string[] MucDoList = 
        {
            AppConstants.MucDoTaiNan.Nhe,
            AppConstants.MucDoTaiNan.TrungBinh,
            AppConstants.MucDoTaiNan.NangNe,
            AppConstants.MucDoTaiNan.TuVong
        };

        public frmTaiNanLaoDongDialog(int idNV)
        {
            InitializeComponent();
            _idNhanVien = idNV;
        }

        private void frmTaiNanLaoDongDialog_Load(object sender, EventArgs e)
        {
            cboLoaiTaiNan.DataSource = LoaiList;
            cboMucDo.DataSource = MucDoList;
            dtpNgayTaiNan.DateTime = DateTime.Today;
            ThemeManager.ApplyTheme(this);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            var TNLD = new ET_TaiNanLaoDong
            {
                IdNhanVien = _idNhanVien,
                NgayTaiNan = dtpNgayTaiNan.DateTime.Date,
                LoaiTaiNan = cboLoaiTaiNan.SelectedItem?.ToString() ?? "",
                MucDo = cboMucDo.SelectedItem?.ToString() ?? "",
                MoTa = txtMoTa.Text.Trim(),
                TrangThai = AppConstants.TrangThaiTaiNan.DangDieuTri
            };

            var res = BUS.BUS_NhanVien.Instance.ThemTaiNanLaoDong(TNLD);
            if (res.IsSuccess)
            {
                TDCMessageBox.Show("Ghi nhận tai nạn thành công!", "Thông báo");
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                TDCMessageBox.Show(res.ErrorMessage, "Lỗi");
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

using System;
using System.Windows.Forms;
using ET;

namespace GUI
{
    public partial class frmDonXinNghiDialog : Form
    {
        private int _idNhanVien;

        private static readonly string[] LoaiList =
        {
            AppConstants.LoaiNghi.PhepNam,
            AppConstants.LoaiNghi.NghiOm,
            AppConstants.LoaiNghi.ThaiSanNu,
            AppConstants.LoaiNghi.ThaiSanNam,
            AppConstants.LoaiNghi.TaiNanLaoDong,
            AppConstants.LoaiNghi.NghiBu,
            AppConstants.LoaiNghi.NghiLe,
            AppConstants.LoaiNghi.DotXuatCoLuong,
            AppConstants.LoaiNghi.NghiKhongLuong
        };

        private static readonly string[] NguonList =
        {
            AppConstants.NguonChiTra.CongTy,
            AppConstants.NguonChiTra.BHXH
        };

        public frmDonXinNghiDialog(int idNV)
        {
            InitializeComponent();
            _idNhanVien = idNV;
        }

        private void frmDonXinNghiDialog_Load(object sender, EventArgs e)
        {
            cboLoaiNghi.DataSource = LoaiList;
            cboNguonChiTra.DataSource = NguonList;
            dtpNgayBatDau.DateTime = DateTime.Today;
            dtpNgayKetThuc.DateTime = DateTime.Today;
            ThemeManager.ApplyTheme(this);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            var don = new ET_DonXinNghi
            {
                IdNhanVien = _idNhanVien,
                LoaiNghi = cboLoaiNghi.SelectedItem?.ToString() ?? "",
                NgayBatDau = dtpNgayBatDau.DateTime.Date,
                NgayKetThuc = dtpNgayKetThuc.DateTime.Date,
                TiLeLuongHuong = numTiLe.Value,
                NguonChiTra = cboNguonChiTra.SelectedItem?.ToString() ?? "",
                LyDo = txtLyDo.Text.Trim(),
                TrangThai = AppConstants.TrangThaiDonNghi.ChoDuyet
            };

            var res = BUS.BUS_NhanVien.Instance.ThemDonXinNghi(don);
            if (res.IsSuccess)
            {
                TDCMessageBox.Show("Thêm đơn xin nghỉ thành công!", "Thông báo");
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

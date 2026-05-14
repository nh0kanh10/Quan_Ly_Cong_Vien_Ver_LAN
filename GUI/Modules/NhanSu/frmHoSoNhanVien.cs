using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ET.Models.DoiTac;
using ET.Results;
using BUS.Services.DoiTac;
using BUS.Services.NhanSu;
using GUI.Infrastructure;

namespace GUI.Modules.NhanSu
{
    public partial class frmHoSoNhanVien : XtraForm
    {
        private string _maNhanVien;
        private DTO_NhanVienChiTiet _nhanVienInfo;
        private bool _isAddMode;

        public frmHoSoNhanVien(string maNhanVien)
        {
            InitializeComponent();
            _maNhanVien = maNhanVien;
            _isAddMode = string.IsNullOrEmpty(maNhanVien);
        }

        #region Khởi tạo và tải dữ liệu

        private void FrmHoSoNhanVien_Load(object sender, EventArgs e)
        {
            if (_isAddMode)
            {
                this.Text = "Thêm Nhân Viên Mới";
                BatCheDoDuLieu(readOnly: false);
            }
            else
            {
                LoadData();
                BatCheDoDuLieu(readOnly: true);
            }
        }

        private void LoadData()
        {
            try
            {
                var result = BUS_NhanVien.Instance.LayChiTiet(_maNhanVien);
                if (result.Success && result.Data != null)
                {
                    _nhanVienInfo = result.Data;
                    BindData();
                }
                else
                {
                    XtraMessageBox.Show(LanguageManager.GetString(result.ErrorMessage), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                var resultLich = BUS.Services.NhanSu.BUS_LichLamViec.Instance.LayDanhSachTheoNhanVien(_maNhanVien);
                if (resultLich.Success)
                    gcLichLamViec.DataSource = resultLich.Data;

                var resultHopDong = BUS.Services.NhanSu.BUS_HopDong.Instance.LayDanhSachTheoNhanVien(_maNhanVien);
                if (resultHopDong.Success)
                    gcHopDong.DataSource = resultHopDong.Data;

                var resultKPI = BUS.Services.NhanSu.BUS_DanhGiaKPI.Instance.LayDanhSachTheoNhanVien(_maNhanVien);
                if (resultKPI.Success)
                    gcKPI.DataSource = resultKPI.Data;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"ERR_LOI_HETHONG|{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindData()
        {
            if (_nhanVienInfo == null) return;

            txtMaNhanVien.Text = _nhanVienInfo.MaNhanVien;
            txtHoTen.Text = _nhanVienInfo.HoTen;
            txtDienThoai.Text = _nhanVienInfo.DienThoai;
            txtPhongBan.Text = _nhanVienInfo.PhongBan;
            txtChucVu.Text = _nhanVienInfo.ChucVu;

            if (_nhanVienInfo.NgayVaoLam.HasValue)
                dtNgayVaoLam.DateTime = _nhanVienInfo.NgayVaoLam.Value;
            else
                dtNgayVaoLam.EditValue = null;

            this.Text = $"Hồ Sơ Nhân Viên - {_nhanVienInfo.HoTen} ({_nhanVienInfo.MaNhanVien})";
        }

        private void BatCheDoDuLieu(bool readOnly)
        {
            txtHoTen.Properties.ReadOnly = readOnly;
            txtDienThoai.Properties.ReadOnly = readOnly;
            txtPhongBan.Properties.ReadOnly = readOnly;
            txtChucVu.Properties.ReadOnly = readOnly;
            dtNgayVaoLam.Properties.ReadOnly = readOnly;
            txtMaNhanVien.Properties.ReadOnly = true;

            btnLuu.Visible = !readOnly;
            btnChinhSua.Visible = readOnly;
            btnHuy.Visible = !readOnly;
        }

        #endregion

        #region Xử lý sự kiện

        private void BtnChinhSua_Click(object sender, EventArgs e)
        {
            BatCheDoDuLieu(readOnly: false);
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            LuuDuLieu();
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            if (_isAddMode)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            else
            {
                BindData();
                BatCheDoDuLieu(readOnly: true);
            }
        }

        #endregion

        #region Hàm hỗ trợ

        private void LuuDuLieu()
        {
            var dto = new DTO_NhanVienChiTiet
            {
                HoTen = txtHoTen.Text.Trim(),
                DienThoai = txtDienThoai.Text.Trim(),
                ChucVu = txtChucVu.Text.Trim(),
                NgayVaoLam = dtNgayVaoLam.EditValue != null ? (DateTime?)dtNgayVaoLam.DateTime : null
            };

            ET.Results.OperationResult result;
            if (_isAddMode)
            {
                result = BUS_NhanVien.Instance.Them(dto);
            }
            else
            {
                dto.IdDoiTac = _nhanVienInfo?.IdDoiTac ?? 0;
                dto.MaNhanVien = _maNhanVien;
                result = BUS_NhanVien.Instance.CapNhat(dto);
            }

            if (result.Success)
            {
                XtraMessageBox.Show(LanguageManager.GetString(result.Message), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                XtraMessageBox.Show(LanguageManager.GetString(result.Message), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}

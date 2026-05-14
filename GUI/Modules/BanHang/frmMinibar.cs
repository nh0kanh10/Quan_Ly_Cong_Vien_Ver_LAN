using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using BUS.Services.BanHang;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using GUI.Infrastructure;

namespace GUI.Modules.BanHang
{
    /// <summary>
    /// Form chọn sản phẩm minibar (đồ ăn tiện lợi, nước đóng chai) để thêm vào bill phòng.
    /// </summary>
    public partial class frmMinibar : XtraForm
    {
        private readonly int _idChiTietDatPhong;
        private readonly int _idNhanVien;
        private DataTable _dtSanPham;

        #region Khởi tạo và tải dữ liệu

        public frmMinibar(int idChiTietDatPhong, int idNhanVien)
        {
            InitializeComponent();
            _idChiTietDatPhong = idChiTietDatPhong;
            _idNhanVien = idNhanVien;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ApplyStyle();
            ApplyLocalization();
            LoadSanPham();
        }

        /// <summary>
        /// Áp dụng AppStyle cho nút bấm.
        /// </summary>
        private void ApplyStyle()
        {
            AppStyle.StyleBtnPrimary(btnXacNhan);
        }

        /// <summary>
        /// Gắn text đa ngôn ngữ cho các label, cột, nút.
        /// </summary>
        private void ApplyLocalization()
        {
            this.Text = LanguageManager.GetString("FRM_MINIBAR_TITLE") ?? "Minibar - Đồ ăn & Thức uống";
            colTenSanPham.Caption = LanguageManager.GetString("COL_TEN_SAN_PHAM") ?? "Sản phẩm";
            colGiaBan.Caption = LanguageManager.GetString("COL_DON_GIA") ?? "Đơn giá";
            colSoLuong.Caption = LanguageManager.GetString("COL_SO_LUONG") ?? "SL";
            colThanhTien.Caption = LanguageManager.GetString("COL_THANH_TIEN") ?? "Thành tiền";
            btnXacNhan.Text = LanguageManager.GetString("BTN_XAC_NHAN") ?? "Xác nhận";
            btnHuy.Text = LanguageManager.GetString("BTN_HUY") ?? "Hủy";
            lciTongCong.Text = LanguageManager.GetString("LBL_TONG_CONG") ?? "Tổng cộng:";
        }

        /// <summary>
        /// Gọi BUS lấy danh sách sản phẩm minibar, nạp vào DataTable để GridControl hiển thị.
        /// Thêm 2 cột tính toán: SoLuongChon (editable) và ThanhTien (auto).
        /// </summary>
        private void LoadSanPham()
        {
            var result = BUS_LuuTru_Minibar.Instance.LayDanhSachSanPhamMinibar();
            if (!result.Success)
            {
                string msg = LanguageManager.GetString(result.ErrorMessage) ?? result.ErrorMessage;
                XtraMessageBox.Show(msg, "Minibar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _dtSanPham = new DataTable();
            _dtSanPham.Columns.Add("IdSanPham", typeof(int));
            _dtSanPham.Columns.Add("TenSanPham", typeof(string));
            _dtSanPham.Columns.Add("GiaBan", typeof(decimal));
            _dtSanPham.Columns.Add("SoLuongChon", typeof(int));
            _dtSanPham.Columns.Add("ThanhTien", typeof(decimal));

            DateTime now = DateTime.Now;
            foreach (var sp in result.Data)
            {
                // Lấy giá hiệu lực hiện tại (HieuLucTu <= now <= HieuLucDen)
                var bangGiaHienTai = sp.BangGias != null
                    ? sp.BangGias.FirstOrDefault(bg => bg.HieuLucTu <= now && bg.HieuLucDen >= now)
                    : null;
                decimal giaBan = bangGiaHienTai?.GiaBan ?? 0;

                _dtSanPham.Rows.Add(sp.Id, sp.TenSanPham, giaBan, 0, 0m);
            }

            gridSanPham.DataSource = _dtSanPham;
            TinhTongCong();
        }

        #endregion

        #region Xử lý sự kiện

        /// <summary>
        /// Khi user thay đổi số lượng → tính lại thành tiền dòng đó + tổng cộng.
        /// </summary>
        private void GridViewSanPham_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName != "SoLuongChon") return;

            var row = gridViewSanPham.GetDataRow(e.RowHandle);
            if (row == null) return;

            // Lấy trực tiếp từ kiểu dữ liệu của DataTable (decimal), không qua ToString()
            int sl       = row["SoLuongChon"] is int i ? i : 0;
            decimal gia  = row["GiaBan"]      is decimal d ? d : 0m;

            row["ThanhTien"] = sl * gia;
            TinhTongCong();
        }

        /// <summary>
        /// Xác nhận: lấy danh sách sản phẩm có SL > 0, gọi BUS thêm vào bill.
        /// </summary>
        private void BtnXacNhan_Click(object sender, EventArgs e)
        {
            var danhSach = new List<MinibarItem>();
            foreach (DataRow row in _dtSanPham.Rows)
            {
                if (!int.TryParse(row["SoLuongChon"].ToString(), out int sl) || sl <= 0) continue;
                if (!int.TryParse(row["IdSanPham"].ToString(), out int idSp)) continue;
                if (!decimal.TryParse(row["GiaBan"].ToString(), out decimal gia)) continue;

                danhSach.Add(new MinibarItem
                {
                    IdSanPham = idSp,
                    SoLuong = sl,
                    DonGia = gia
                });
            }

            if (danhSach.Count == 0)
            {
                string msg = LanguageManager.GetString("ERR_MINIBAR_EMPTY") ?? "Chưa chọn sản phẩm nào!";
                XtraMessageBox.Show(msg, "Minibar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = BUS_LuuTru_Minibar.Instance.ThemMinibarVaoBill(_idChiTietDatPhong, danhSach, _idNhanVien);
            if (result.Success)
            {
                BUS.Services.HeThong.BUS_NhatKy.Instance.GhiLog("Lưu Trú", _idChiTietDatPhong, "Thêm Minibar", _idNhanVien, null, $"Số lượng SP: {danhSach.Count}, Tổng tiền: {lblTongCong.Text}", "Thêm vào bill thành công");

                string msg = LanguageManager.GetString("MSG_MINIBAR_OK") ?? "Đã thêm vào bill thành công!";
                XtraMessageBox.Show(msg, "Minibar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                string msg = LanguageManager.GetString(result.ErrorMessage) ?? result.ErrorMessage;
                XtraMessageBox.Show(msg, "Minibar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion

        #region Hàm hỗ trợ

        /// <summary>
        /// Tính tổng tiền từ tất cả dòng có SL > 0, hiển thị lên label.
        /// </summary>
        private void TinhTongCong()
        {
            decimal tong = 0;
            if (_dtSanPham != null)
            {
                foreach (DataRow row in _dtSanPham.Rows)
                {
                    if (decimal.TryParse(row["ThanhTien"].ToString(), out decimal tt))
                        tong += tt;
                }
            }
            lblTongCong.Text = tong.ToString("N0") + "đ";
        }

        #endregion
    }
}

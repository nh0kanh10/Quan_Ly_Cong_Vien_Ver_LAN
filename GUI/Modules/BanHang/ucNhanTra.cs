using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BUS.Services.BanHang;
using BUS.Services.DoiTac;
using ET.Constants;
using ET.DTOs;
using ET.Models.BanHang;
using GUI.Infrastructure;

namespace GUI.Modules.BanHang
{
    public partial class ucNhanTra : DevExpress.XtraEditors.XtraUserControl
    {
        #region Khởi tạo và tải dữ liệu

        // Phiên thuê đang hiển thị trên grid editable
        private List<ET_ThueDoChiTiet> _phienThue = new List<ET_ThueDoChiTiet>();
        private DataTable _dtTraDo;
        private string _maDonHienTai;
        private readonly Action<object> _onLanguageChanged;


        // Id phiên thu ngân hiện tại (ca trả đồ), nhận từ ucQuanLyThueDo cha.
        // Gắn vào ThueDoChiTiet.IdPhienTra khi xử lý trả đồ — dùng để đối soát giao ca chéo.
        public int IdPhienThuNgan { get; set; }
        // IdDiemBan nhận từ ucQuanLyThueDo cha để biết đang ở máy POS nào.
        public int IdDiemBan { get; set; }

        public ucNhanTra()
        {
            InitializeComponent();

            _onLanguageChanged = _ =>
            {
                if (this.IsHandleCreated && !this.IsDisposed)
                    this.Invoke((MethodInvoker)delegate { ApplyLanguage(); });
            };
            EventBus.Subscribe("LanguageChanged", _onLanguageChanged);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                AppStyle.StyleForm(this);
                AppStyle.StyleBanner(pnlBanner, lblTitle);
                AppStyle.StyleGrid(viewTraDo);
                AppStyle.StyleGrid(viewChuaTra);
                AppStyle.AutoStyleButton(btnXacNhanTra, btnTraHet, btnXem);
                ApplyLanguage();
                LoadChuaTra();
            }
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

        #endregion

        #region Xử lý sự kiện (Click, SelectedChanged...)

        private void TxtMaDon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnTimDon_Click(sender, e);
                e.Handled = true;
            }
        }

        private void BtnTimDon_Click(object sender, EventArgs e)
        {
            string maDon = txtMaDon.Text.Trim();
            if (string.IsNullOrEmpty(maDon)) return;

            var result = BUS_ThueDo.Instance.LayPhienTheoMaDon(maDon);
            if (result.Success && result.Data is List<ET_ThueDoChiTiet> ds)
            {
                _phienThue = ds;
                _maDonHienTai = maDon;
                lblKhachHang.Text = ds.FirstOrDefault()?.TenKhachHang ?? "";
                HienThiGridTraDo();
            }
            else
            {
                XtraMessageBox.Show(
                    LanguageManager.GetString(result.Message) ?? result.Message,
                    "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TxtRFID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnTimRFID_Click(sender, e);
                e.Handled = true;
            }
        }

        private void BtnTimRFID_Click(object sender, EventArgs e)
        {
            string rfid = txtRFID.Text.Trim();
            if (string.IsNullOrEmpty(rfid)) return;

            // Tìm khách qua RFID
            string maThe = rfid.StartsWith("RF-", StringComparison.OrdinalIgnoreCase) ? rfid.Substring(3) : rfid;
            var resKH = BUS_KhachHang.Instance.TimTheoRFID(maThe);
            if (!resKH.Success || !(resKH.Data is DTO_KhachHangPOS kh))
            {
                XtraMessageBox.Show(
                    LanguageManager.GetString(resKH.Message) ?? resKH.Message,
                    "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lblKhachHang.Text = kh.HoTen ?? kh.MaKhachHang;

            // Tìm phiên thuê của khách
            var result = BUS_ThueDo.Instance.LayPhienTheoKhach(kh.IdDoiTac);
            if (result.Success && result.Data is List<ET_ThueDoChiTiet> ds)
            {
                _phienThue = ds;
                _maDonHienTai = ds.FirstOrDefault()?.MaDonHang ?? "";
                txtMaDon.Text = _maDonHienTai;
                HienThiGridTraDo();
            }
            else
            {
                XtraMessageBox.Show(
                    LanguageManager.GetString(result.Message) ?? result.Message,
                    "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ViewTraDo_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "SoLuongMat")
            {
                int slMat = Convert.ToInt32(e.Value);
                if (slMat > 0)
                {
                    // Mở popup nhập tiền phạt thủ công
                    string tenSP = viewTraDo.GetRowCellValue(e.RowHandle, "TenSanPham")?.ToString() ?? "";
                    decimal tienCoc = Convert.ToDecimal(viewTraDo.GetRowCellValue(e.RowHandle, "TienCoc"));

                    using (var frm = new frmPhatMatDo(tenSP, slMat, tienCoc))
                    {
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            _dtTraDo.Rows[e.RowHandle]["TienPhat"] = frm.TienPhat;
                        }
                        else
                        {
                            _dtTraDo.Rows[e.RowHandle]["SoLuongMat"] = 0;
                        }
                    }
                }
                else
                {
                    _dtTraDo.Rows[e.RowHandle]["TienPhat"] = 0m;
                }
            }
            CapNhatTongTra();
        }

        private void BtnXacNhanTra_Click(object sender, EventArgs e)
        {
            XuLyTraDo();
        }

        // F12: Auto điền "Trả SL" = "SL thuê" cho tất cả dòng.
        private void BtnTraHet_Click(object sender, EventArgs e)
        {
            if (_dtTraDo == null) return;
            foreach (DataRow r in _dtTraDo.Rows)
            {
                r["SoLuongTra"] = r["SoLuong"];
                r["SoLuongMat"] = 0;
            }
            viewTraDo.RefreshData();
            CapNhatTongTra();
        }

        private void BtnXem_Click(object sender, EventArgs e)
        {
            LoadChuaTra();
        }

        // Double-click dòng giám sát -> auto điền mã biên lai vào ô tìm kiếm và load phiên.
        private void ViewChuaTra_DoubleClick(object sender, EventArgs e)
        {
            int row = viewChuaTra.FocusedRowHandle;
            if (row < 0) return;

            string maDon = viewChuaTra.GetRowCellValue(row, "MaDonHang")?.ToString();
            if (!string.IsNullOrEmpty(maDon))
            {
                txtMaDon.Text = maDon;
                BtnTimDon_Click(sender, e);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F12)
            {
                BtnTraHet_Click(this, EventArgs.Empty);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        #region Hàm hỗ trợ

        private void HienThiGridTraDo()
        {
            _dtTraDo = new DataTable();
            _dtTraDo.Columns.Add("IdSanPham", typeof(int));
            _dtTraDo.Columns.Add("TenSanPham", typeof(string));
            _dtTraDo.Columns.Add("SoLuong", typeof(int));
            _dtTraDo.Columns.Add("ThoiGianThue", typeof(DateTime));
            _dtTraDo.Columns.Add("TienCoc", typeof(decimal));
            _dtTraDo.Columns.Add("SoLuongTra", typeof(int));
            _dtTraDo.Columns.Add("SoLuongMat", typeof(int));
            _dtTraDo.Columns.Add("TienPhat", typeof(decimal));

            foreach (var p in _phienThue)
            {
                _dtTraDo.Rows.Add(
                    p.IdSanPham, p.TenSanPham, p.SoLuong, p.ThoiGianThue,
                    p.TienCoc, 0, 0, 0m);
            }

            gridTraDo.DataSource = _dtTraDo;
            CapNhatTongTra();
        }

        private void CapNhatTongTra()
        {
            if (_dtTraDo == null) return;
            decimal tongHoan = 0m;
            decimal tongPhat = 0m;

            foreach (DataRow r in _dtTraDo.Rows)
            {
                int slTra = Convert.ToInt32(r["SoLuongTra"]);
                int slMat = Convert.ToInt32(r["SoLuongMat"]);
                decimal coc = Convert.ToDecimal(r["TienCoc"]);
                decimal phat = Convert.ToDecimal(r["TienPhat"]);

                if (slTra > 0) tongHoan += coc; // Hoàn cọc khi trả
                tongPhat += phat;
            }

            lblHoanCocValue.Text = tongHoan.ToString("#,##0");
            lblPhatValue.Text = tongPhat.ToString("#,##0");
        }

        private void XuLyTraDo()
        {
            if (_dtTraDo == null || _dtTraDo.Rows.Count == 0)
            {
                XtraMessageBox.Show(
                    LanguageManager.GetString(AppConstants.ErrorMessages.ERR_RETURN_NO_ITEMS) ?? "Chưa có đồ để trả.",
                    "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo request từ grid
            var dsTraDo = new List<DTO_ThuHoiRequest>();
            foreach (DataRow r in _dtTraDo.Rows)
            {
                int slTra = Convert.ToInt32(r["SoLuongTra"]);
                int slMat = Convert.ToInt32(r["SoLuongMat"]);
                if (slTra == 0 && slMat == 0) continue;

                dsTraDo.Add(new DTO_ThuHoiRequest
                {
                    IdSanPham = Convert.ToInt32(r["IdSanPham"]),
                    TenSanPham = r["TenSanPham"].ToString(),
                    SoLuongDangThue = Convert.ToInt32(r["SoLuong"]),
                    SoLuongTra = slTra,
                    SoLuongMat = slMat,
                    TienPhat = Convert.ToDecimal(r["TienPhat"])
                });
            }

            if (!dsTraDo.Any())
            {
                XtraMessageBox.Show(
                    LanguageManager.GetString(AppConstants.ErrorMessages.ERR_RETURN_NO_ITEMS) ?? "Chưa nhập SL trả hoặc mất.",
                    "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string msg = LanguageManager.GetString(AppConstants.ErrorMessages.MSG_RETURN_CONFIRM) ?? "Xác nhận trả đồ?";
            if (XtraMessageBox.Show(msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            var result = BUS_ThueDo.Instance.XuLyTraDo(dsTraDo, _maDonHienTai, SessionManager.IdDoiTac, AppConstants.PhuongThucTT.TienMat, IdDiemBan, IdPhienThuNgan);
            if (result.Success)
            {
                XtraMessageBox.Show(
                    LanguageManager.GetString(AppConstants.ErrorMessages.MSG_RETURN_SUCCESS) ?? "Trả đồ thành công!",
                    "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _phienThue.Clear();
                _dtTraDo = null;
                gridTraDo.DataSource = null;
                lblKhachHang.Text = "";
                txtMaDon.Text = "";
                txtRFID.Text = "";
                CapNhatTongTra();
                LoadChuaTra();
            }
            else
            {
                XtraMessageBox.Show(
                    LanguageManager.GetString(result.Message) ?? result.Message,
                    "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadChuaTra()
        {
            DateTime tu = dtTuNgay.DateTime.Date;
            DateTime den = dtDenNgay.DateTime.Date.AddDays(1);
            var result = BUS_ThueDo.Instance.LayDanhSachChuaTra(tu, den);
            if (result.Success && result.Data is List<DTO_PhienChuaTraView> ds)
            {
                gridChuaTra.DataSource = ds;
            }
        }

        private void ApplyLanguage()
        {
            lblTitle.Text = LanguageManager.GetString("RETURN_TITLE") ?? "NHẬN TRẢ ĐỒ";
            colTD_TenSanPham.Caption = LanguageManager.GetString("RENTAL_COL_TEN") ?? "Sản phẩm";
            colTD_SoLuongThue.Caption = LanguageManager.GetString("COL_SOLUONG") ?? "SL thuê";
            colTD_ThoiGianThue.Caption = LanguageManager.GetString("RETURN_COL_BATDAU") ?? "Bắt đầu";
            colTD_TienCoc.Caption = LanguageManager.GetString("RENTAL_COL_TIENCOC") ?? "Cọc";
            colTD_SoLuongTra.Caption = LanguageManager.GetString("RETURN_COL_TRASL") ?? "Trả SL";
            colTD_SoLuongMat.Caption = LanguageManager.GetString("RETURN_COL_MAT") ?? "Mất";
            lblHoanCocLabel.Text = LanguageManager.GetString("RETURN_HOANCOC") ?? "Hoàn cọc:";
            lblPhatLabel.Text = LanguageManager.GetString("RETURN_PHAT") ?? "Phạt:";
            btnXacNhanTra.Text = LanguageManager.GetString("RETURN_BTN_CONFIRM") ?? "Xác nhận trả";
            btnTraHet.Text = LanguageManager.GetString("RETURN_BTN_ALL") ?? "Trả hết (F12)";
            btnXem.Text = LanguageManager.GetString("BTN_REFRESH") ?? "Xem";
            lblGiamSatTitle.Text = LanguageManager.GetString("RETURN_MONITOR_TITLE") ?? "Phiên chưa trả";
            txtMaDon.Properties.NullValuePrompt = LanguageManager.GetString("RETURN_INPUT_CODE") ?? "Nhập mã biên lai DT-xxx...";
            txtRFID.Properties.NullValuePrompt = LanguageManager.GetString("RETURN_INPUT_RFID") ?? "Quẹt RFID khách...";
        }

        #endregion
    }
}

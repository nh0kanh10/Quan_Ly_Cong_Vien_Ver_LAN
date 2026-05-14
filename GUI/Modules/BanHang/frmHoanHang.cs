using ET.Constants;
using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ET.DTOs;
using BUS.Services.BanHang;
using System.Linq;
using GUI.Infrastructure;

namespace GUI.Modules.BanHang
{
    public partial class frmHoanHang : XtraForm
    {
        private DTO_DonHangHoan _donHang;
        private readonly int _idNguoiDuyet;
        private readonly int _idKhoMacDinh;

        public frmHoanHang(int idNguoiDuyet, int idKhoMacDinh)
        {
            InitializeComponent();
            _idNguoiDuyet = idNguoiDuyet;
            _idKhoMacDinh = idKhoMacDinh;
            ApplyLanguage();
        }

        private void ApplyLanguage()
        {
            this.Text = LanguageManager.GetString("REFUND_TITLE") ?? "Nghiệp Vụ - Hoàn Trả Giao Dịch Bán Lẻ";
            txtMaDonHang.Properties.NullValuePrompt = LanguageManager.GetString("REFUND_SEARCH_HINT") ?? "Mã Đơn Hàng cần hoàn...";
            btnTim.Text = LanguageManager.GetString("REFUND_BTN_SEARCH") ?? "Tìm Kiếm";
            btnHoan.Text = LanguageManager.GetString("REFUND_BTN_CONFIRM") ?? "XÁC NHẬN HOÀN";
            btnDong.Text = LanguageManager.GetString("REFUND_BTN_CLOSE") ?? "ĐÓNG";

            colTenSP.Caption = LanguageManager.GetString("REFUND_COL_PRODUCT") ?? "Sản Phẩm";
            colLoai.Caption = LanguageManager.GetString("REFUND_COL_TYPE") ?? "Loại";
            colSoLuongMua.Caption = LanguageManager.GetString("REFUND_COL_BOUGHT") ?? "Đã Mua";
            colDaHoan.Caption = LanguageManager.GetString("REFUND_COL_REFUNDED") ?? "Đã Hoàn";
            colGiaThucTe.Caption = LanguageManager.GetString("REFUND_COL_PRICE") ?? "Đơn Giá Trả";
            colSoLuongMuonHoan.Caption = LanguageManager.GetString("REFUND_COL_RETURNQTY") ?? "SL Muốn Hoàn";
            colLyDo.Caption = LanguageManager.GetString("REFUND_COL_REASON") ?? "Lý Do";
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string maDon = txtMaDonHang.Text.Trim();
            if (string.IsNullOrEmpty(maDon))
            {
                XtraMessageBox.Show(LanguageManager.GetString(AppConstants.ErrorMessages.ERR_REFUND_EMPTY_CODE) ?? "Vui lòng nhập mã đơn hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _donHang = BUS_HoanHang.Instance.LayDonHangHoan(maDon);
            if (_donHang == null)
            {
                XtraMessageBox.Show(LanguageManager.GetString(AppConstants.ErrorMessages.ERR_REFUND_NOT_FOUND) ?? "Không tìm thấy đơn hàng hoặc đơn hàng trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            BindData();
        }

        private void BindData()
        {
            string createLabel = LanguageManager.GetString("REFUND_LBL_CREATED") ?? "Ngày tạo";
            string custLabel = LanguageManager.GetString("REFUND_LBL_CUST") ?? "KH";
            string totalLabel = LanguageManager.GetString("REFUND_LBL_TOTAL") ?? "Tổng Thanh Toán";
            
            lblDonHangInfo.Text = $"{createLabel}: {_donHang.NgayTao:dd/MM/yyyy HH:mm} | {custLabel}: {_donHang.IdKhachHang?.ToString() ?? "Khách lẻ"} | {totalLabel}: {_donHang.TongThanhToan:N0}₫";
            gridControl.DataSource = _donHang.DanhSachChiTiet;
            gridView.ValidatingEditor -= GridView_ValidatingEditor;
            gridView.ValidatingEditor += GridView_ValidatingEditor;
            gridView.RefreshData();
        }

        private void GridView_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (gridView.FocusedColumn.FieldName != "SoLuongMuonHoan") return;

            var row = gridView.GetFocusedRow() as DTO_ChiTietDonHangHoan;
            if (row == null) return;

            decimal value;
            if (!decimal.TryParse(e.Value?.ToString(), out value))
            {
                e.Valid = false;
                e.ErrorText = LanguageManager.GetString(AppConstants.ErrorMessages.ERR_REFUND_INVALID_NUMBER) ?? "Vui lòng nhập số hợp lệ!";
                return;
            }

            if (value < 0)
            {
                e.Valid = false;
                e.ErrorText = LanguageManager.GetString(AppConstants.ErrorMessages.ERR_REFUND_NEGATIVE_QTY) ?? "Số lượng không được âm!";
                return;
            }

            decimal conLai = row.SoLuongMua - row.SoLuongDaHoan;
            if (value > conLai)
            {
                e.Valid = false;
                string msg = LanguageManager.GetString(AppConstants.ErrorMessages.ERR_REFUND_OVER_QTY) ?? "Không được vượt quá {0}!";
                e.ErrorText = string.Format(msg, conLai);
            }
        }

        private void btnHoan_Click(object sender, EventArgs e)
        {
            if (_donHang == null || !_donHang.DanhSachChiTiet.Any(x => x.SoLuongMuonHoan > 0))
            {
                XtraMessageBox.Show(LanguageManager.GetString(AppConstants.ErrorMessages.ERR_HOAN_NO_ITEMS) ?? "Vui lòng nhập định mức cần hoàn cho ít nhất 1 sản phẩm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string msgConfirm = LanguageManager.GetString(AppConstants.ErrorMessages.MSG_REFUND_CONFIRM) ?? "Bạn có chắc chắn muốn thực hiện hoàn hàng? Quá trình này không thể phục hồi!";
            string titleConfirm = LanguageManager.GetString("TITLE_REFUND_CONFIRM") ?? "Hoàn Khách Hàng";
            var confirm = XtraMessageBox.Show(msgConfirm, titleConfirm, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            var request = new DTO_HoanHangRequest
            {
                IdDonHang = _donHang.IdDonHang,
                IdNguoiDuyet = _idNguoiDuyet,
                IdKhoMacDinh = _idKhoMacDinh,
                NgayTaoDonHang = _donHang.NgayTao,
                ChiTietHoan = _donHang.DanhSachChiTiet.Where(x => x.SoLuongMuonHoan > 0).ToList()
            };

            var res = BUS_HoanHang.Instance.ThucHienHoanHang(request);
            if (res.Success)
            {
                BUS.Services.HeThong.BUS_NhatKy.Instance.GhiLog("Bán Hàng", _donHang.IdDonHang, "Hoàn hàng", _idNguoiDuyet, $"Tổng đơn gốc: {_donHang.TongThanhToan}", $"Đã hoàn {request.ChiTietHoan.Count} mặt hàng", "Hoàn thành công");

                string msgSuccess = LanguageManager.GetString(AppConstants.ErrorMessages.MSG_REFUND_SUCCESS) ?? "Lập phiếu chi hoàn vé / hoàn hàng THÀNH CÔNG!";
                string titleSuccess = LanguageManager.GetString("TITLE_SUCCESS") ?? "Thành Công!";
                XtraMessageBox.Show(msgSuccess, titleSuccess, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                string msgFail = LanguageManager.GetString(AppConstants.ErrorMessages.MSG_REFUND_FAIL) ?? "Hoàn hàng thất bại:";
                string titleFail = LanguageManager.GetString("TITLE_FAIL") ?? "Lỗi";
                string translatedMsg = LanguageManager.GetString(res.Message) ?? res.Message;
                XtraMessageBox.Show($"{msgFail} {translatedMsg}", titleFail, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

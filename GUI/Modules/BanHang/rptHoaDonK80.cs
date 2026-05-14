using System;
using DevExpress.XtraReports.UI;
using ET.DTOs;

namespace GUI.Modules.BanHang
{
    public partial class rptHoaDonK80 : DevExpress.XtraReports.UI.XtraReport
    {
        public rptHoaDonK80(DTO_CheckoutResult result)
        {
            InitializeComponent();
            ApplyLanguage();
            BindData(result);
        }

        private void ApplyLanguage()
        {
            lblTitle.Text = GUI.Infrastructure.LanguageManager.GetString("RPT_KDL_DAI_NAM") ?? "KDL ĐẠI NAM";
            lblSubTitle.Text = GUI.Infrastructure.LanguageManager.GetString("RPT_PHIEU_THANH_TOAN") ?? "PHIẾU THANH TOÁN";
            lblThanks.Text = GUI.Infrastructure.LanguageManager.GetString("RPT_CAM_ON") ?? "Cảm ơn Quý Khách & Hẹn Gặp Lại!";
            lblL2.Text = GUI.Infrastructure.LanguageManager.GetString("RPT_TIEN_THUA") ?? "Tiền thừa:";
        }

        private void BindData(DTO_CheckoutResult result)
        {
            string maDonText = GUI.Infrastructure.LanguageManager.GetString("RPT_MA_DON") ?? "Mã đơn";
            string ngayText = GUI.Infrastructure.LanguageManager.GetString("RPT_NGAY") ?? "Ngày";
            
            lblMaDon.Text = $"{maDonText}: {result.MaDonHang}";
            lblNgay.Text = $"{ngayText}: {DateTime.Now:dd/MM/yyyy HH:mm}";
            
            string lblTienHang = GUI.Infrastructure.LanguageManager.GetString("RPT_TONG_TIEN_HANG") ?? "Tổng tiền hàng";
            string lblGiamGia = GUI.Infrastructure.LanguageManager.GetString("RPT_GIAM_GIA") ?? "Giảm giá";
            string lblTruDiem = GUI.Infrastructure.LanguageManager.GetString("RPT_TRU_DIEM") ?? "Trừ điểm";
            string lblCanThanhToan = GUI.Infrastructure.LanguageManager.GetString("RPT_CAN_THANH_TOAN") ?? "CẦN THANH TOÁN";

            string labelL1 = $"{lblTienHang}:\r\n";
            string labelTien = $"{result.TongTienHang:#,##0} ₫\r\n";

            if (result.SoTienGiam > 0)
            {
                labelL1 += $"{lblGiamGia}:\r\n";
                labelTien += $"-{result.SoTienGiam:#,##0} ₫\r\n";
            }
            if (result.TienThanhToanBangDiem > 0)
            {
                labelL1 += $"{lblTruDiem}:\r\n";
                labelTien += $"-{result.TienThanhToanBangDiem:#,##0} ₫\r\n";
            }

            labelL1 += $"{lblCanThanhToan}:";
            labelTien += $"{result.TongThanhToan:#,##0} ₫";

            lblL1.Text = labelL1;
            lblTongTien.Text = labelTien;

            lblTienThua.Text = $"{result.TienThua:#,##0} ₫";

            if (result.DanhSachMon != null && result.DanhSachMon.Count > 0)
            {
                this.DataSource = result.DanhSachMon;
            }
        }
    }
}

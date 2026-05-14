using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraReports.UI;
using ET.DTOs;

namespace GUI.Modules.BanHang
{
    public partial class rptHoaDonLuuTru : DevExpress.XtraReports.UI.XtraReport
    {
        //  dùng khi checkout (3 dòng cứng: tiền phòng, phạt, phụ thu)
        public rptHoaDonLuuTru(DTO_PhongLuuTruView phong, decimal tienPhong, decimal tienPhat, decimal phuThu, decimal tienCoc)
        {
            InitializeComponent();
            ApplyLanguage();

            var dsChiPhi = new List<DTO_CheckoutItem>();
            if (tienPhong > 0)
                dsChiPhi.Add(new DTO_CheckoutItem { TenMon = GUI.Infrastructure.LanguageManager.GetString("LBL_TIEN_PHONG") ?? "Tiền phòng", SoLuong = 1, DonGiaGoc = tienPhong, ThanhTien = tienPhong });
            if (tienPhat > 0)
                dsChiPhi.Add(new DTO_CheckoutItem { TenMon = GUI.Infrastructure.LanguageManager.GetString("LBL_TIEN_PHAT") ?? "Phạt lố giờ", SoLuong = 1, DonGiaGoc = tienPhat, ThanhTien = tienPhat });
            if (phuThu > 0)
                dsChiPhi.Add(new DTO_CheckoutItem { TenMon = GUI.Infrastructure.LanguageManager.GetString("LBL_PHU_THU_KHAC") ?? "Phụ thu khác", SoLuong = 1, DonGiaGoc = phuThu, ThanhTien = phuThu });

            decimal tongChiPhi = tienPhong + tienPhat + phuThu;
            BindData(phong, tongChiPhi, tienCoc, dsChiPhi);
        }

        // dùng khi xem bill giữa chừng (hiển thị từng dòng chi tiết thật từ DB)
        public rptHoaDonLuuTru(DTO_PhongLuuTruView phong, decimal tongTienPhong, decimal tienPhat, decimal phuThu, decimal tienCoc, List<DTO_CheckoutItem> danhSachChiTiet)
        {
            InitializeComponent();
            ApplyLanguage();

            // Nếu có phạt lố giờ mà chưa nằm trong danh sách -> thêm vào
            if (tienPhat > 0 && !danhSachChiTiet.Any(x => x.TenMon != null && x.TenMon.Contains("lố giờ")))
            {
                danhSachChiTiet.Add(new DTO_CheckoutItem
                {
                    TenMon = GUI.Infrastructure.LanguageManager.GetString("LBL_TIEN_PHAT") ?? "Phạt lố giờ",
                    SoLuong = 1,
                    DonGiaGoc = tienPhat,
                    ThanhTien = tienPhat
                });
            }

            decimal tongChiPhi = danhSachChiTiet.Sum(x => x.ThanhTien);
            BindData(phong, tongChiPhi, tienCoc, danhSachChiTiet);
        }

        private void ApplyLanguage()
        {
            lblTitle.Text = GUI.Infrastructure.LanguageManager.GetString("RPT_KDL_DAI_NAM") ?? "KDL ĐẠI NAM";
            lblSubTitle.Text = GUI.Infrastructure.LanguageManager.GetString("RPT_PHIEU_THANH_TOAN") ?? "PHIẾU THANH TOÁN";
            lblThanks.Text = GUI.Infrastructure.LanguageManager.GetString("RPT_CAM_ON") ?? "Cảm ơn Quý Khách & Hẹn Gặp Lại!";
            lblL2.Text = GUI.Infrastructure.LanguageManager.GetString("RPT_TIEN_THUA") ?? "Tiền thừa/Trả lại:";
        }

        private void BindData(DTO_PhongLuuTruView phong, decimal tongChiPhi, decimal tienCoc, List<DTO_CheckoutItem> dsChiPhi)
        {
            string ngayText = GUI.Infrastructure.LanguageManager.GetString("RPT_NGAY") ?? "Ngày";
            
            lblMaDon.Text = $"Phòng: {phong.MaPhong} ({phong.TenKhachHang})";
            lblNgay.Text = $"{ngayText}: {DateTime.Now:dd/MM/yyyy HH:mm}";
            
            string lblTongChiPhiText = GUI.Infrastructure.LanguageManager.GetString("LBL_TONG_CHI_PHI") ?? "Tổng chi phí";
            string lblDaThuCoc = GUI.Infrastructure.LanguageManager.GetString("LBL_DA_THU_COC") ?? "Tiền cọc đã thu";
            string lblCanThanhToan = GUI.Infrastructure.LanguageManager.GetString("RPT_CAN_THANH_TOAN") ?? "KHÁCH THANH TOÁN";

            decimal tongThanhToan = tongChiPhi - tienCoc;

            string labelL1 = $"{lblTongChiPhiText}:\r\n";
            string labelTien = $"{tongChiPhi:#,##0} ₫\r\n";

            if (tienCoc > 0)
            {
                labelL1 += $"{lblDaThuCoc}:\r\n";
                labelTien += $"-{tienCoc:#,##0} ₫\r\n";
            }

            labelL1 += $"{lblCanThanhToan}:";
            
            if (tongThanhToan >= 0)
            {
                labelTien += $"{tongThanhToan:#,##0} ₫";
                lblTienThua.Text = "0 ₫";
            }
            else
            {
                labelTien += $"0 ₫";
                lblTienThua.Text = $"{Math.Abs(tongThanhToan):#,##0} ₫ (Thối lại)";
            }

            lblL1.Text = labelL1;
            lblTongTien.Text = labelTien;

            this.DataSource = dsChiPhi;
        }
    }
}

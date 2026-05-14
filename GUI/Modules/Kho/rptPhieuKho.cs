using System;
using System.Collections.Generic;
using System.Data;
using DevExpress.XtraReports.UI;
using ET.Models.Kho;
using GUI.Infrastructure;

namespace GUI.Modules.Kho
{
    public partial class rptPhieuKho : DevExpress.XtraReports.UI.XtraReport
    {


        public rptPhieuKho()
        {
            InitializeComponent();
        }



        /// <summary>
        /// Nạp dữ liệu cho phiếu kho từ BUS layer.
        /// V3: Kho xuất/nhập hiển thị theo từng dòng chi tiết, không còn ở header.
        /// </summary>
        public void NapDuLieu(int idChungTu)
        {
            var ct = BUS.Services.Kho.BUS_ChungTuKho.Instance.GetChiTietChungTu(idChungTu);
            if (ct == null) return;

            string loaiPhieuDisplay = LanguageManager.GetString(ct.LoaiChungTu) ?? ct.LoaiChungTu;
            lblTitle.Text = "PHIẾU KHO — " + loaiPhieuDisplay.ToUpper();
            lblMaPhieu.Text = "Số phiếu: " + ct.MaChungTu;
            lblNgay.Text = "Ngày lập: " + ct.NgayChungTu.ToString("dd/MM/yyyy");

            // V3: Kho không còn ở header — hiển thị tóm tắt từ dòng đầu tiên (nếu có)
            if (ct.ChiTiets.Count > 0)
            {
                var dongDau = ct.ChiTiets[0];
                lblKho.Text = $"Kho Xuất: {dongDau.TenKhoXuat ?? "N/A"}  →  Kho Nhập: {dongDau.TenKhoNhap ?? "N/A"}";
            }
            else
            {
                lblKho.Text = "";
            }

            string tenNguoiLap = BUS.Services.DoiTac.BUS_DoiTac.Instance.GetTenDoiTac(ct.IdNguoiTao);
            lblNguoiLap.Text = "Người lập: " + (tenNguoiLap ?? "N/A");

            var dt = TaoDataTable(ct.ChiTiets);
            DataSource = dt;

            cellSTT.DataBindings.Add("Text", DataSource, "STT");
            cellTenSP.DataBindings.Add("Text", DataSource, "TenSanPham");
            cellDVT.DataBindings.Add("Text", DataSource, "DonViTinh");
            cellSoLuong.DataBindings.Add("Text", DataSource, "SoLuong", "{0:N2}");
            // V3: Thêm cột kho xuất/nhập vào báo cáo nếu phiếu có nhiều kho đích khác nhau
        }

        private DataTable TaoDataTable(List<ET_ChiTietChungTu> chiTiets)
        {
            var dt = new DataTable("ChiTietPhieu");
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("TenSanPham", typeof(string));
            dt.Columns.Add("DonViTinh", typeof(string));
            dt.Columns.Add("SoLuong", typeof(decimal));
            dt.Columns.Add("TenKhoXuat", typeof(string));
            dt.Columns.Add("TenKhoNhap", typeof(string));

            int stt = 1;
            foreach (var item in chiTiets)
            {
                dt.Rows.Add(stt++, item.TenSanPham ?? "", item.TenDonViTinh ?? "", item.SoLuong,
                    item.TenKhoXuat ?? "", item.TenKhoNhap ?? "");
            }
            return dt;
        }
    }
}

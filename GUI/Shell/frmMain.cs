using ET.Constants;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars.FluentDesignSystem;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Docking2010.Views;
using GUI.AI;
using GUI.Infrastructure;
using GUI.Modules.DanhMuc;
using GUI.Modules.Kho;
using GUI.Modules.DoiTac;
using GUI.Modules.HeThong;
using GUI.Modules.BanHang;
using GUI.Modules.BaoCao;
using GUI.Modules.VanHanh;


namespace GUI.Shell
{
    public partial class frmMain : FluentDesignForm
    {
        // Ánh xạ MenuKey sang Type. Thêm module mới chỉ cần thêm 1 dòng.
        // null = chức năng đang phát triển (hiện thông báo khi click).
        private readonly Dictionary<string, Type> _appRouter = new Dictionary<string, Type>
        {
            // Bảng điều khiển
            { "DASHBOARD",             typeof(frmDashboard) },

            // Tiền sảnh và bán hàng
            { "POS_BAN_LE",            typeof(GUI.Modules.BanHang.ucPOS) },  // BanHang.DonHang, DiemBanHang_POS, Menu_POS
            { "MENU_POS",              typeof(GUI.Modules.DanhMuc.ucMenuPOS) },
            { "THUE_DO",               typeof(ucQuanLyThueDo) },  // BanHang.ThueDoChiTiet — 2 Tab: Giao đồ + Nhận trả
            { "POS_NHA_HANG",          null },  // BanHang.DatBan, DatBan_MonAn, LenhBep
            { "LE_TAN_PHONG",          typeof(GUI.Modules.BanHang.ucLuuTru_Main) },  // BanHang.ChiTietDatPhong
            { "KHACH_DOAN_B2B",        null },  // BanHang.BaoGia, QuyenLoiDoanKhach
            { "VI_RFID",               null },  // TaiChinh.TheRFID, ViDienTu, SoCaiVi
            { "KHACH_HANG_CRM",        typeof(frmKhachHang) },  // DoiTac.KhachHang, TaiChinh.LichSuDiem

            // Vận hành khu du lịch
            { "KIEM_SOAT_CONG",        typeof(GUI.Modules.VanHanh.ucKiemSoatCong) },  // BanHang.ChiTietLuotQuet, VanHanh.LichSuQuetVe
            { "TRUONG_DUA",            null },  // VanHanh.DuongDua, GiaiDua, LichThiDau, KetQuaDua
            { "BIEU_DIEN_SHOW",        null },  // VanHanh.LichBieuDien, BanHang.DatChoXemShow
            { "BAI_DO_XE",             null },  // VanHanh.BaiDoXe, LuotVaoRaBaiXe
            { "DONG_VAT",              null },  // DanhMuc.DongVat, ChuongTrai, VanHanh.NhatKyYTe_Thu
            { "SU_CO_THAT_LAC",        null },  // VanHanh.SuCo, ThatLac, CaTrucCuuHo
            { "BAO_TRI",               null },  // VanHanh.BaoTri, ChiTietVatTuBaoTri

            // Kho và F&B
            { "DANH_MUC_KHO",          typeof(frmKhoHang) },
            { "TRUNG_TAM_KHO",         typeof(frmTrungTamKho) },
            { "TON_KHO",               null },  // View: Kho.V_TonKho, V_TonTheoLo, V_CanhBaoHetHan
            { "NHAP_XUAT_KHO",         null },  // Kho.ChungTu, ChiTietChungTu, SoCai
            { "DINH_MUC_BOM",          null },  // DanhMuc.DinhMucNguyenLieu
            { "MAN_HINH_BEP",          null },  // BanHang.LenhBep (KDS - Kitchen Display)

            // Nhân sự
            { "NHAN_VIEN",             typeof(GUI.Modules.NhanSu.frmNhanVien) },  // DoiTac.NhanVien, NhanSu.ChungChiNhanVien
            { "PHAN_CA",               null },  // NhanSu.CaLamMau, LichLamViec
            { "CHAM_CONG",             null },  // NhanSu.BangChamCong, NghiBu, DonXinNghi
            { "BANG_LUONG",            null },  // NhanSu.BangLuong, SoNgayPhepNam

            // Tài chính
            { "PHIEN_THU_NGAN",        null },  // BanHang.PhienThuNgan (Mở/Đóng ca, Chốt két)
            { "BAO_CAO",               typeof(ucBaoCaoDoanhThu) },  // View: BanHang.V_BillChiTiet
            { "CONG_NO",               null },  // BanHang.CongNo
            { "KHUYEN_MAI",            typeof(ucKhuyenMai) },  // BanHang.KhuyenMai, KhuyenMai_DieuKien

            // Thiết lập hệ thống
            { "SAN_PHAM",              typeof(frmSanPham) },
            { "KHU_VUC",               null },  // DanhMuc.KhuVuc, KhuVucBien, KhuVucThu
            { "BANG_GIA",              null },  // DanhMuc.BangGia, BangGia_ThueTheoGio, CauHinhNgayLe
            { "COMBO",                 typeof(frmCombo) },  // DanhMuc.Combo, ComboChiTiet
            { "PHAN_QUYEN",            typeof(frmPhanQuyen) },  // NhanSu.VaiTro, QuyenHan, PhanQuyen
            { "NHAT_KY_HE_THONG",      typeof(frmNhatKyThaoTac) },  // HeThong.NhatKyThaoTac
        };

        // Các đường dẫn SVG icon dạng Material Design 24x24
        static class Icons
        {
            // Group icons (24px)
            public const string Dashboard   = "M3 13h8V3H3v10zm0 8h8v-6H3v6zm10 0h8V11h-8v10zm0-18v6h8V3h-8z";
            public const string FrontOffice  = "M20 4H4v2h16V4zm1 10v-2l-1-5H4l-1 5v2h1v6h10v-6h4v6h2v-6h1zm-9 4H6v-4h6v4z";
            public const string Operations   = "M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm-2 15l-5-5 1.41-1.41L10 14.17l7.59-7.59L19 8l-9 9z";
            public const string Warehouse    = "M20 2H4c-1 0-2 1-2 2v3.01c0 .72.43 1.34 1 1.69V20c0 1.1 1.1 2 2 2h14c.9 0 2-.9 2-2V8.7c.57-.35 1-.97 1-1.69V4c0-1-1-2-2-2zm-5 12H9v-2h6v2zm5-7H4V4h16v3z";
            public const string HR           = "M16 11c1.66 0 2.99-1.34 2.99-3S17.66 5 16 5c-1.66 0-3 1.34-3 3s1.34 3 3 3zm-8 0c1.66 0 2.99-1.34 2.99-3S9.66 5 8 5C6.34 5 5 6.34 5 8s1.34 3 3 3zm0 2c-2.33 0-7 1.17-7 3.5V19h14v-2.5c0-2.33-4.67-3.5-7-3.5zm8 0c-.29 0-.62.02-.97.05 1.16.84 1.97 1.97 1.97 3.45V19h6v-2.5c0-2.33-4.67-3.5-7-3.5z";
            public const string Finance      = "M11.8 10.9c-2.27-.59-3-1.2-3-2.15 0-1.09 1.01-1.85 2.7-1.85 1.78 0 2.44.85 2.5 2.1h2.21c-.07-1.72-1.12-3.3-2.71-3.69V3h-4v2.26c-1.81.4-3.1 1.56-3.1 3.22 0 2.09 1.75 2.97 4.51 3.57 2.5.55 3 1.62 3 2.75 0 1.25-1.17 1.85-2.77 1.85-1.95 0-2.73-1.14-2.81-2.4H6.12c.11 2.37 1.64 3.73 3.68 4.14V21h4v-2.26c1.94-.42 3.2-1.66 3.2-3.4 0-2.4-1.95-3.34-4.2-3.99z";
            public const string Settings     = "M19.43 12.98c.04-.32.07-.64.07-.98 0-.34-.03-.66-.07-.98l2.11-1.65c.19-.15.24-.42.12-.64l-2-3.46c-.12-.22-.39-.3-.61-.22l-2.49 1c-.52-.4-1.08-.73-1.69-.98l-.38-2.65C14.46 2.18 14.25 2 14 2h-4c-.25 0-.46.18-.49.42l-.38 2.65c-.61.25-1.17.59-1.69.98l-2.49-1c-.23-.09-.5 0-.61.22l-2 3.46c-.13.22-.07.49.12.64l2.11 1.65c-.04.32-.07.65-.07.98 0 .33.03.66.07.98l-2.11 1.65c-.19.15-.24.42-.12.64l2 3.46c.12.22.39.3.61.22l2.49-1c.52.4 1.08.73 1.69.98l.38 2.65c.03.24.24.42.49.42h4c.25 0 .46-.18.49-.42l.38-2.65c.61-.25 1.17-.59 1.69-.98l2.49 1c.23.09.5 0 .61-.22l2-3.46c.12-.22.07-.49-.12-.64l-2.11-1.65zM12 15.5c-1.93 0-3.5-1.57-3.5-3.5s1.57-3.5 3.5-3.5 3.5 1.57 3.5 3.5-1.57 3.5-3.5 3.5z";

            // Item icons (small 16px contextual)
            public const string POS          = "M17 2H7c-1.1 0-2 .9-2 2v16c0 1.1.9 2 2 2h10c1.1 0 2-.9 2-2V4c0-1.1-.9-2-2-2zm0 18H7V4h10v16z";
            public const string Restaurant   = "M11 9H9V2H7v7H5V2H3v7c0 2.12 1.66 3.84 3.75 3.97V22h2.5v-9.03C11.34 12.84 13 11.12 13 9V2h-2v7zm5-3v8h2.5v8H21V2c-2.76 0-5 2.24-5 5z";
            public const string Hotel        = "M7 13c1.66 0 3-1.34 3-3S8.66 7 7 7s-3 1.34-3 3 1.34 3 3 3zm12-6h-8v7H3V5H1v15h2v-3h18v3h2v-9c0-2.21-1.79-4-4-4z";
            public const string Group        = "M12 12.75c1.63 0 3.07.39 4.24.9 1.08.48 1.76 1.56 1.76 2.73V18H6v-1.61c0-1.18.68-2.26 1.76-2.73 1.17-.52 2.61-.91 4.24-.91zM4 13c1.1 0 2-.9 2-2s-.9-2-2-2-2 .9-2 2 .9 2 2 2zm1.13 1.1C4.76 14.04 4.39 14 4 14c-.99 0-1.93.21-2.78.58A2.01 2.01 0 000 16.43V18h4.5v-1.61c0-.83.23-1.61.63-2.29zM20 13c1.1 0 2-.9 2-2s-.9-2-2-2-2 .9-2 2 .9 2 2 2zm4 3.43c0-.81-.48-1.53-1.22-1.85A6.95 6.95 0 0020 14c-.39 0-.76.04-1.13.1.4.68.63 1.46.63 2.29V18H24v-1.57zM12 6c1.66 0 3 1.34 3 3s-1.34 3-3 3-3-1.34-3-3 1.34-3 3-3z";
            public const string Wallet       = "M21 18v1c0 1.1-.9 2-2 2H5c-1.11 0-2-.9-2-2V5c0-1.1.89-2 2-2h14c1.1 0 2 .9 2 2v1h-9c-1.11 0-2 .9-2 2v8c0 1.1.89 2 2 2h9zm-9-2h10V8H12v8zm4-2.5c-.83 0-1.5-.67-1.5-1.5s.67-1.5 1.5-1.5 1.5.67 1.5 1.5-.67 1.5-1.5 1.5z";
            public const string Customer     = "M12 12c2.21 0 4-1.79 4-4s-1.79-4-4-4-4 1.79-4 4 1.79 4 4 4zm0 2c-2.67 0-8 1.34-8 4v2h16v-2c0-2.66-5.33-4-8-4z";
            public const string Gate         = "M6 2v20h12V2H6zm10 14H8v-2h8v2zm0-4H8v-2h8v2zm0-4H8V6h8v2z";
            public const string Racing       = "M14.4 6L14 4H5v17h2v-7h5.6l.4 2h7V6z";
            public const string Theater      = "M18 3v2h-2V3H8v2H6V3H4v18h2v-2h2v2h8v-2h2v2h2V3h-2zM8 17H6v-2h2v2zm0-4H6v-2h2v2zm0-4H6V7h2v2zm10 8h-2v-2h2v2zm0-4h-2v-2h2v2zm0-4h-2V7h2v2z";
            public const string Parking      = "M13 3H6v18h4v-6h3c3.31 0 6-2.69 6-6s-2.69-6-6-6zm.2 8H10V7h3.2c1.1 0 2 .9 2 2s-.9 2-2 2z";
            public const string Pets         = "M4.5 9.5m-2.5 0a2.5 2.5 0 1 0 5 0a2.5 2.5 0 1 0-5 0M9 5.5m-2.5 0a2.5 2.5 0 1 0 5 0a2.5 2.5 0 1 0-5 0M15 5.5m-2.5 0a2.5 2.5 0 1 0 5 0a2.5 2.5 0 1 0-5 0M19.5 9.5m-2.5 0a2.5 2.5 0 1 0 5 0a2.5 2.5 0 1 0-5 0M17.34 14.86c-.87-1.02-1.6-1.89-2.48-2.91-.46-.54-1.17-.95-1.86-.95-.69 0-1.39.41-1.86.95-.87 1.02-1.6 1.89-2.48 2.91-1.31 1.31-2.92 2.76-2.62 4.79.29 1.02 1.02 2.03 2.33 2.32.73.15 3.06-.44 5.54-.44h.18c2.48 0 4.81.59 5.54.44 1.31-.29 2.04-1.31 2.33-2.32.31-2.04-1.3-3.49-2.62-4.79z";
            public const string Warning      = "M1 21h22L12 2 1 21zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z";
            public const string Build        = "M22.7 19l-9.1-9.1c.9-2.3.4-5-1.5-6.9-2-2-5-2.4-7.4-1.3L9 6 6 9 1.6 4.7C.4 7.1.9 10.1 2.9 12.1c1.9 1.9 4.6 2.4 6.9 1.5l9.1 9.1c.4.4 1 .4 1.4 0l2.3-2.3c.5-.4.5-1.1.1-1.4z";
            public const string Inventory    = "M20 2H4c-1 0-2 1-2 2v3.01c0 .72.43 1.34 1 1.69V20c0 1.1 1.1 2 2 2h14c.9 0 2-.9 2-2V8.7c.57-.35 1-.97 1-1.69V4c0-1-1-2-2-2zm-5 12H9v-2h6v2zm5-7H4V4h16v3z";
            public const string Transfer     = "M16 17.01V10h-2v7.01h-3L15 21l4-3.99h-3zM9 3L5 6.99h3V14h2V6.99h3L9 3z";
            public const string Recipe       = "M18.06 22.99h1.66c.84 0 1.53-.64 1.63-1.46L23 5.05h-5V1h-1.97v4.05h-4.97l.3 2.34c1.71.47 3.31 1.32 4.27 2.26 1.44 1.42 2.43 2.89 2.43 5.29v8.05zM1 21.99V21h15.03v.99c0 .55-.45 1-1.01 1H2.01c-.56 0-1.01-.45-1.01-1zm15.03-7c0-4-6.03-7-7.97-7-1.68 0-7.06 2.56-7.06 7v2h15.03v-2z";
            public const string Kitchen      = "M8.1 13.34l2.83-2.83L3.91 3.5c-1.56 1.56-1.56 4.09 0 5.66l4.19 4.18zm6.78-1.81c1.53.71 3.68.21 5.27-1.38 1.91-1.91 2.28-4.65.81-6.12-1.46-1.46-4.2-1.1-6.12.81-1.59 1.59-2.09 3.74-1.38 5.27L3.7 19.87l1.41 1.41L12 14.41l6.88 6.88 1.41-1.41L13.41 13l1.47-1.47z";
            public const string Staff        = "M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 3c1.66 0 3 1.34 3 3s-1.34 3-3 3-3-1.34-3-3 1.34-3 3-3zm0 14.2c-2.5 0-4.71-1.28-6-3.22.03-1.99 4-3.08 6-3.08 1.99 0 5.97 1.09 6 3.08-1.29 1.94-3.5 3.22-6 3.22z";
            public const string Calendar     = "M17 12h-5v5h5v-5zM16 1v2H8V1H6v2H5c-1.11 0-1.99.9-1.99 2L3 19c0 1.1.89 2 2 2h14c1.1 0 2-.9 2-2V5c0-1.1-.9-2-2-2h-1V1h-2zm3 18H5V8h14v11z";
            public const string Clock        = "M11.99 2C6.47 2 2 6.48 2 12s4.47 10 9.99 10C17.52 22 22 17.52 22 12S17.52 2 11.99 2zM12 20c-4.42 0-8-3.58-8-8s3.58-8 8-8 8 3.58 8 8-3.58 8-8 8zm.5-13H11v6l5.25 3.15.75-1.23-4.5-2.67V7z";
            public const string Salary       = "M11.8 10.9c-2.27-.59-3-1.2-3-2.15 0-1.09 1.01-1.85 2.7-1.85 1.78 0 2.44.85 2.5 2.1h2.21c-.07-1.72-1.12-3.3-2.71-3.69V3h-4v2.26c-1.81.4-3.1 1.56-3.1 3.22 0 2.09 1.75 2.97 4.51 3.57 2.5.55 3 1.62 3 2.75 0 1.25-1.17 1.85-2.77 1.85-1.95 0-2.73-1.14-2.81-2.4H6.12c.11 2.37 1.64 3.73 3.68 4.14V21h4v-2.26c1.94-.42 3.2-1.66 3.2-3.4 0-2.4-1.95-3.34-4.2-3.99z";
            public const string CashRegister = "M4 6h18V4H2v13H0v3h12v-3H4V6zm20 2h-8v12h8V8zm-2 10h-4v-8h4v8z";
            public const string Report       = "M19 3H5c-1.1 0-2 .9-2 2v14c0 1.1.89 2 2 2h14c1.1 0 2-.9 2-2V5c0-1.1-.9-2-2-2zM9 17H7v-7h2v7zm4 0h-2V7h2v10zm4 0h-2v-4h2v4z";
            public const string Debt         = "M4 10v7h3v-7H4zm6 0v7h3v-7h-3zM2 22h19v-3H2v3zm14-12v7h3v-7h-3zM12 1L2 6v2h19V6L12 1z";
            public const string Discount     = "M12.79 21L3 11.21v2c0 .53.21 1.04.59 1.41l7.79 7.79c.78.78 2.05.78 2.83 0l6.21-6.21c.78-.78.78-2.05 0-2.83L12.79 21z M11.38 17.41c.78.78 2.05.78 2.83 0l6.21-6.21c.78-.78.78-2.05 0-2.83L12.63.58C12.25.21 11.74 0 11.21 0H5C3.9 0 3 .9 3 2v6.21c0 .53.21 1.04.59 1.41l7.79 7.79zM7.25 3a1.25 1.25 0 110 2.5 1.25 1.25 0 010-2.5z";
            public const string Product      = "M12 2l-5.5 9h11L12 2zm0 3.84L13.93 9h-3.87L12 5.84zM17.5 13c-2.49 0-4.5 2.01-4.5 4.5s2.01 4.5 4.5 4.5 4.5-2.01 4.5-4.5-2.01-4.5-4.5-4.5zm0 7c-1.38 0-2.5-1.12-2.5-2.5s1.12-2.5 2.5-2.5 2.5 1.12 2.5 2.5-1.12 2.5-2.5 2.5zM3 21.5h8v-8H3v8zm2-6h4v4H5v-4z";
            public const string Map          = "M20.5 3l-.16.03L15 5.1 9 3 3.36 4.9c-.21.07-.36.25-.36.48V20.5c0 .28.22.5.5.5l.16-.03L9 18.9l6 2.1 5.64-1.9c.21-.07.36-.25.36-.48V3.5c0-.28-.22-.5-.5-.5zM15 19l-6-2.11V5l6 2.11V19z";
            public const string Price        = "M21.41 11.58l-9-9C12.05 2.22 11.55 2 11 2H4c-1.1 0-2 .9-2 2v7c0 .55.22 1.05.59 1.42l9 9c.36.36.86.58 1.41.58.55 0 1.05-.22 1.41-.59l7-7c.37-.36.59-.86.59-1.41 0-.55-.23-1.06-.59-1.42zM5.5 7C4.67 7 4 6.33 4 5.5S4.67 4 5.5 4 7 4.67 7 5.5 6.33 7 5.5 7z";
            public const string Lock         = "M18 8h-1V6c0-2.76-2.24-5-5-5S7 3.24 7 6v2H6c-1.1 0-2 .9-2 2v10c0 1.1.9 2 2 2h12c1.1 0 2-.9 2-2V10c0-1.1-.9-2-2-2zm-6 9c-1.1 0-2-.9-2-2s.9-2 2-2 2 .9 2 2-.9 2-2 2zm3.1-9H8.9V6c0-1.71 1.39-3.1 3.1-3.1 1.71 0 3.1 1.39 3.1 3.1v2z";
        }

        private DevExpress.XtraBars.BarSubItem itemSettings;
        private DevExpress.XtraBars.BarSubItem subLang;
        private DevExpress.XtraBars.BarButtonItem subLogout;

        // AI Chat
        private AIChatPanel _chatPanel;
        private DevExpress.XtraEditors.SimpleButton _btnAI;

        public frmMain()
        {
            InitializeComponent();

            this.tabbedView.DocumentGroupProperties.ShowTabHeader = true;


            // Style sidebar
            // Tổng thể Accordion
            accordionControl.Appearance.AccordionControl.BackColor = AppStyle.Navy;
            accordionControl.OptionsMinimizing.AllowMinimizeMode = DevExpress.Utils.DefaultBoolean.True; // Hamburger collapse

            // GROUP: chữ trắng sáng, font đậm, highlight nhẹ khi hover            
            accordionControl.Appearance.Group.Normal.BackColor = AppStyle.Navy;
            accordionControl.Appearance.Group.Normal.ForeColor = Color.FromArgb(220, 225, 235);
            accordionControl.Appearance.Group.Normal.Font = AppStyle.FontBold;
            accordionControl.Appearance.Group.Hovered.BackColor = AppStyle.NavyLight;
            accordionControl.Appearance.Group.Hovered.ForeColor = Color.White;

            // ITEM: xám nhạt Regular, sáng khi hover, Teal khi active
            accordionControl.Appearance.Item.Normal.BackColor = AppStyle.Navy;
            accordionControl.Appearance.Item.Normal.ForeColor = Color.FromArgb(170, 178, 195);
            accordionControl.Appearance.Item.Normal.Font = AppStyle.FontDefault;
            accordionControl.Appearance.Item.Hovered.BackColor = Color.FromArgb(40, 52, 72);
            accordionControl.Appearance.Item.Hovered.ForeColor = Color.White;
            accordionControl.Appearance.Item.Pressed.BackColor = AppStyle.Teal;
            accordionControl.Appearance.Item.Pressed.ForeColor = Color.White;

            // Gọi hàm xây menu ĐỘNG & Top Bar
            BuildMenu();
            BuildTopBar();
            KhoiTaoAI();

            // Tab theming
            this.tabbedView.AppearancePage.Header.Font = AppStyle.FontBold;
            this.tabbedView.AppearancePage.HeaderActive.ForeColor = AppStyle.Teal;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.fluentDesignFormContainer.Padding = new Padding(0);

            // Lazy Load: Chỉ nạp Dashboard khi form đã hiển thị xong
            OpenModule("DASHBOARD");
        }



        #region Sinh menu động 
        private void BuildMenu()
        {
            accordionControl.Elements.Clear();

            // 1. Bảng điều khiển
            var grpDash = CreateGroup(LanguageManager.GetString("MENU_GRP_DASHBOARD"), Icons.Dashboard);
            AddItemIfAllowed(grpDash, LanguageManager.GetString("MENU_ITEM_DASHBOARD"), "DASHBOARD", Icons.Report);
            AddGroupIfHasItems(grpDash);

            // 2. Tiền sảnh & Bán hàng
            var grpBanHang = CreateGroup(LanguageManager.GetString("MENU_GRP_BANHANG") ?? "Tiền Sảnh & Bán Hàng", Icons.FrontOffice);
            AddItemIfAllowed(grpBanHang, LanguageManager.GetString("MENU_ITEM_POS") ?? "Màn hình POS (Bán lẻ)", "POS_BAN_LE", Icons.POS);
            AddItemIfAllowed(grpBanHang, LanguageManager.GetString("MENU_ITEM_THUE_DO") ?? "Quản lý thuê đồ", "THUE_DO", Icons.Transfer);
            AddItemIfAllowed(grpBanHang, LanguageManager.GetString("MENU_ITEM_LE_TAN_PHONG") ?? "Lễ tân & Lưu trú", "LE_TAN_PHONG", Icons.Hotel);
            AddGroupIfHasItems(grpBanHang);

            // 2.5 Vận hành khu du lịch
            var grpVanHanh = CreateGroup(LanguageManager.GetString("MENU_GRP_VANHANH") ?? "Vận Hành Khu Du Lịch", Icons.Operations);
            grpVanHanh.Expanded = true;
            AddItemIfAllowed(grpVanHanh, LanguageManager.GetString("MENU_ITEM_KIEM_SOAT_CONG") ?? "Kiểm Soát Cổng Vé", "KIEM_SOAT_CONG", Icons.Gate);
            AddGroupIfHasItems(grpVanHanh);

            // 3. Danh mục hệ thống
            var grpDanhMuc = CreateGroup(LanguageManager.GetString("MENU_GRP_DANHMUC"), Icons.Settings);
            grpDanhMuc.Expanded = true;
            AddItemIfAllowed(grpDanhMuc, LanguageManager.GetString("MENU_ITEM_SANPHAM"), "SAN_PHAM", Icons.Product);
            AddItemIfAllowed(grpDanhMuc, LanguageManager.GetString("MENU_ITEM_COMBO") ?? "Quản lý Combo", "COMBO", Icons.Recipe);
            AddItemIfAllowed(grpDanhMuc, LanguageManager.GetString("MENU_ITEM_MENUPOS") ?? "Menu POS", "MENU_POS", Icons.Restaurant);
            AddGroupIfHasItems(grpDanhMuc);

            // 4. Kho & F&B
            var grpKho = CreateGroup(LanguageManager.GetString("MENU_GRP_KHO") ?? "Kho & F&B", Icons.Warehouse);
            AddItemIfAllowed(grpKho, LanguageManager.GetString("MENU_ITEM_DANH_MUC_KHO") ?? "Danh mục Kho", "DANH_MUC_KHO", Icons.Inventory);
            AddItemIfAllowed(grpKho, LanguageManager.GetString("MENU_ITEM_TRUNG_TAM_KHO") ?? "Trung tâm Kho", "TRUNG_TAM_KHO", Icons.Transfer);
            AddGroupIfHasItems(grpKho);

            // 4.5 Nhân sự
            var grpNhanSu = CreateGroup(LanguageManager.GetString("MENU_GRP_NHANSU") ?? "Nhân Sự", Icons.HR);
            AddItemIfAllowed(grpNhanSu, LanguageManager.GetString("MENU_ITEM_NHANVIEN") ?? "Danh bạ nhân viên", "NHAN_VIEN", Icons.Staff);
            AddGroupIfHasItems(grpNhanSu);

            // 5. Quản trị
            var grpQuanTri = CreateGroup(LanguageManager.GetString("MENU_GRP_DOITAC") ?? "Quản Trị", Icons.Group);
            grpQuanTri.Expanded = true;
            AddItemIfAllowed(grpQuanTri, LanguageManager.GetString("MENU_ITEM_KHACHHANG") ?? "Khách Hàng", "KHACH_HANG_CRM", Icons.Customer);
            AddItemIfAllowed(grpQuanTri, LanguageManager.GetString("MENU_ITEM_PHANQUYEN") ?? "Phân Quyền", "PHAN_QUYEN", Icons.Lock);
            AddItemIfAllowed(grpQuanTri, LanguageManager.GetString("MENU_ITEM_NHATKY") ?? "Nhật ký hệ thống", "NHAT_KY_HE_THONG", Icons.Clock);
            AddGroupIfHasItems(grpQuanTri);
            
            // 6. Tài chính & Báo cáo
            var grpTaiChinh = CreateGroup(LanguageManager.GetString("MENU_GRP_TAICHINH") ?? "Tài Chính & Báo Cáo", Icons.Finance);
            AddItemIfAllowed(grpTaiChinh, LanguageManager.GetString("MENU_ITEM_DOANHTHU") ?? "Báo cáo Doanh thu", "BAO_CAO", Icons.Report);
            AddItemIfAllowed(grpTaiChinh, LanguageManager.GetString("MENU_ITEM_KHUYENMAI") ?? "Quản lý Khuyến Mãi", "KHUYEN_MAI", Icons.Discount);
            AddGroupIfHasItems(grpTaiChinh);
        }

        /// <summary>
        /// Chỉ thêm menu item nếu user đang đăng nhập CÓ quyền tương ứng (MaQuyen = menuKey).
        /// </summary>
        private void AddItemIfAllowed(AccordionControlElement group, string text, string menuKey, string svgPath)
        {
            if (SessionManager.CoQuyen(menuKey))
            {
                group.Elements.Add(CreateItem(text, menuKey, svgPath));
            }
        }

        /// <summary>
        /// Chỉ hiển thị group trên sidebar nếu bên trong có ít nhất 1 item (tránh group rỗng).
        /// </summary>
        private void AddGroupIfHasItems(AccordionControlElement group)
        {
            if (group.Elements.Count > 0)
            {
                accordionControl.Elements.Add(group);
            }
        }

        #endregion

        #region Hàm hỗ trợ (Factory)
        private void BuildTopBar()
        {
            var manager = fluentDesignFormControl.Manager;
            if (manager == null)
            {
                manager = new DevExpress.XtraBars.FluentDesignSystem.FluentFormDefaultManager();
                manager.Form = this;
                fluentDesignFormControl.Manager = manager;
            }

            // 1. User Profile (Static Text + Icon)
            var itemUser = new DevExpress.XtraBars.BarStaticItem();
            itemUser.Caption = "ADMIN · Giám Đốc";
            itemUser.ImageOptions.SvgImage = CreateSvgIcon(Icons.Staff);
            itemUser.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            itemUser.ItemAppearance.Normal.Font = AppStyle.FontBold;
            itemUser.ItemAppearance.Normal.ForeColor = AppStyle.Gold;
            itemUser.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;

            // 2. System Settings Dropdown (BarSubItem)
            itemSettings = new DevExpress.XtraBars.BarSubItem();
            itemSettings.Caption = GUI.Infrastructure.LanguageManager.GetString("TOPBAR_SYSTEM");
            itemSettings.ImageOptions.SvgImage = CreateSvgIcon(Icons.Settings);
            itemSettings.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            itemSettings.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;

            // Menu con của Hệ Thống
            subLang = new DevExpress.XtraBars.BarSubItem();
            subLang.Caption = GUI.Infrastructure.LanguageManager.GetString("TOPBAR_LANGUAGE");
            subLang.ImageOptions.SvgImage = CreateSvgIcon(Icons.Map); 

            // Menu các ngôn ngữ
            var langViet = new DevExpress.XtraBars.BarButtonItem { Caption = "🇻🇳 Tiếng Việt" };
            langViet.ItemClick += LangViet_ItemClick;

            var langEng = new DevExpress.XtraBars.BarButtonItem { Caption = "🇬🇧 English" };
            langEng.ItemClick += LangEng_ItemClick;

            var langHoa = new DevExpress.XtraBars.BarButtonItem { Caption = "🇨🇳 中文" };
            langHoa.ItemClick += LangHoa_ItemClick;

            subLang.AddItem(langViet);
            subLang.AddItem(langEng);
            subLang.AddItem(langHoa);

            subLogout = new DevExpress.XtraBars.BarButtonItem();
            subLogout.Caption = GUI.Infrastructure.LanguageManager.GetString("TOPBAR_LOGOUT");
            subLogout.ImageOptions.SvgImage = CreateSvgIcon(Icons.Lock);
            subLogout.ItemClick += SubLogout_ItemClick;

            // 3. Skin Chooser (Giao diện) - Đưa vào Menu Hệ thống
            var itemSkin = new DevExpress.XtraBars.BarSubItem();
            itemSkin.Caption = GUI.Infrastructure.LanguageManager.GetString("TOPBAR_SKIN") ?? "Giao diện (Đổi Skin)";
            itemSkin.ImageOptions.SvgImage = CreateSvgIcon(Icons.Settings); 
            DevExpress.XtraBars.Helpers.SkinHelper.InitSkinPopupMenu(itemSkin);

            // Thêm các menu con vào menu Hệ Thống
            itemSettings.AddItem(subLang);
            itemSettings.AddItem(itemSkin);
            itemSettings.AddItem(subLogout);

            // Đăng ký TẤT CẢ các Item/SubItem/Button vào Manager để khởi tạo đồng bộ
            manager.Items.Add(itemSettings);
            manager.Items.Add(itemUser);
            manager.Items.Add(itemSkin);
            manager.Items.Add(subLang);
            manager.Items.Add(subLogout);
            manager.Items.Add(langViet);
            manager.Items.Add(langEng);
            manager.Items.Add(langHoa);

            // Thêm vào Right của Top Bar thông qua TitleItemLinks 
            fluentDesignFormControl.TitleItemLinks.Add(itemSettings);
            fluentDesignFormControl.TitleItemLinks.Add(itemUser);
        }
        private void ApplyLanguage()
        {
            // Tái cấu trúc lại UI Menu với ngôn ngữ mới
            BuildMenu();
            
            // Dịch động cụm TopBar Setting
            if (itemSettings != null) itemSettings.Caption = GUI.Infrastructure.LanguageManager.GetString("TOPBAR_SYSTEM");
            if (subLang != null) subLang.Caption = GUI.Infrastructure.LanguageManager.GetString("TOPBAR_LANGUAGE");
            if (subLogout != null) subLogout.Caption = GUI.Infrastructure.LanguageManager.GetString("TOPBAR_LOGOUT");

            // Dịch Document Tabs (các màn hình đang mở)
            foreach (DevExpress.XtraBars.Docking2010.Views.BaseDocument doc in tabbedView.Documents)
            {
                if (doc.Control != null && !string.IsNullOrEmpty(doc.Control.Name))
                {
                    doc.Caption = GUI.Infrastructure.LanguageManager.GetString(doc.Control.Name) ?? doc.Control.Name;
                }
            }

            // Broadcast Event ra toàn hệ thống (cho các Tab đang mở thực hiện Dịch)
            EventBus.Publish("LanguageChanged");
        }

        private AccordionControlElement CreateGroup(string text, string svgPath)
        {
            var grp = new AccordionControlElement();
            grp.Text = text;
            grp.Style = ElementStyle.Group;
            if (!string.IsNullOrEmpty(svgPath))
                grp.ImageOptions.SvgImage = CreateSvgIcon(svgPath);
            return grp;
        }

        /// <summary>
        /// Overload KHÔNG có icon (backwards compatible).
        /// </summary>
        private AccordionControlElement CreateItem(string text, string keyTag)
        {
            return CreateItem(text, keyTag, null);
        }

        /// <summary>
        /// Tạo menu item CON với icon SVG riêng (16px).
        /// </summary>
        private AccordionControlElement CreateItem(string text, string keyTag, string svgPath)
        {
            var item = new AccordionControlElement();
            item.Text = text;
            item.Style = ElementStyle.Item;
            item.Tag = keyTag;
            if (!string.IsNullOrEmpty(svgPath))
                item.ImageOptions.SvgImage = CreateSvgIcon(svgPath);
            return item;
        }

        private DevExpress.Utils.Svg.SvgImage CreateSvgIcon(string path)
        {
            try {
                string svg = $"<svg viewBox='0 0 24 24' xmlns='http://www.w3.org/2000/svg'><path fill='#B0BEC5' d='{path}'/></svg>";
                using (var ms = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(svg)))
                {
                    return DevExpress.Utils.Svg.SvgImage.FromStream(ms);
                }
            } catch { return null; }
        }

        // Xử lý click menu sidebar
        private void AccordionControl_ElementClick(object sender, ElementClickEventArgs e)
        {
            if (e.Element.Style == ElementStyle.Item && e.Element.Tag != null)
            {
                OpenModule(e.Element.Tag.ToString());
            }
        }

        private void OpenModule(string menuKey)
        {
            if (menuKey == "SWITCH_LANGUAGE")
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Tính năng thay đổi ngôn ngữ (Đa ngôn ngữ - i18n) sẽ được kích hoạt tại đây. " +
                    "Toàn bộ Data Grid và ComboBox sẽ tự động chuyển đổi theo Resource và bảng TuDien.", 
                    "Chuyển Đổi Ngôn Ngữ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 1. Chức năng không tồn tại hoặc chưa phát triển
            if (!_appRouter.ContainsKey(menuKey) || _appRouter[menuKey] == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    $"Chức năng [{menuKey}] đang được phát triển!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (BaseDocument doc in tabbedView.Documents)
            {
                if (doc.Control is Control c && c.Name == menuKey)
                {
                    tabbedView.Controller.Activate(doc);
                    return;
                }
            }

            try
            {
                Type controlType = _appRouter[menuKey];
                Control moduleControl;

                // Nếu module là Form (VD: frmPhanQuyen) -> Wrap thành UserControl để nhúng vào Tab
                if (typeof(Form).IsAssignableFrom(controlType))
                {
                    Form frm = (Form)Activator.CreateInstance(controlType);
                    frm.TopLevel = false;
                    frm.FormBorderStyle = FormBorderStyle.None;
                    frm.Dock = DockStyle.Fill;

                    var wrapper = new DevExpress.XtraEditors.XtraUserControl();
                    wrapper.Name = menuKey;
                    wrapper.Dock = DockStyle.Fill;
                    wrapper.Controls.Add(frm);
                    frm.Show();
                    moduleControl = wrapper;
                }
                else
                {
                    // Nếu module là UserControl thì dùng trực tiếp
                    moduleControl = (DevExpress.XtraEditors.XtraUserControl)Activator.CreateInstance(controlType);
                    moduleControl.Name = menuKey;
                    ((Control)moduleControl).Dock = DockStyle.Fill;
                }

                DevExpress.XtraBars.Docking2010.Views.BaseDocument doc = tabbedView.AddDocument((Control)moduleControl);          
                doc.Caption = GUI.Infrastructure.LanguageManager.GetString(menuKey) ?? menuKey; 
                tabbedView.Controller.Activate(doc);

                // Đổi ngữ cảnh AI nếu module có implement IAIModuleContext
                CapNhatNguCanhAI((Control)moduleControl);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Tích hợp AI

        // Khởi tạo nút bấm AI và chat panel
        private void KhoiTaoAI()
        {
            _chatPanel = new AIChatPanel();
            _chatPanel.Visible = false;
            _chatPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.Controls.Add(_chatPanel);
            _chatPanel.BringToFront();

            // Nút bubble AI ở góc phải dưới
            _btnAI = new DevExpress.XtraEditors.SimpleButton();
            _btnAI.Text = "AI";
            _btnAI.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            _btnAI.Size = new Size(48, 48);
            _btnAI.Appearance.BackColor = AppStyle.Gold;
            _btnAI.Appearance.ForeColor = Color.FromArgb(30, 30, 46);
            _btnAI.Appearance.BorderColor = Color.Transparent;
            _btnAI.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.Controls.Add(_btnAI);
            _btnAI.BringToFront();

            // Đặt vị trí khi form resize
            this.Resize += FrmMain_Resize;
            DatViTriAI();

            // Toggle hiện/ẩn chat panel
            _btnAI.Click += BtnAI_Click;

            // Lắng nghe lệnh mở module từ AI
            _chatPanel.Service.OnOpenModuleRequested += ChatPanel_OnOpenModuleRequested;

            // Lắng nghe lệnh UI từ AI (lọc grid, chọn dòng...)
            _chatPanel.OnUICommandRequested += ChatPanel_OnUICommandRequested;

            // Tự đổi context khi chuyển tab
            tabbedView.DocumentActivated += TabbedView_DocumentActivated;
        }

        // Đặt vị trí nút AI và chat panel ở góc phải dưới
        private void DatViTriAI()
        {
            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;

            _btnAI.Location = new Point(
                w - _btnAI.Width - 20,
                h - _btnAI.Height - 20);

            _chatPanel.Location = new Point(
                w - _chatPanel.Width - 20,
                h - _chatPanel.Height - _btnAI.Height - 30);
        }

        // Đổi ngữ cảnh AI khi người dùng chuyển module
        private void CapNhatNguCanhAI(Control moduleControl)
        {
            if (_chatPanel == null) return;

            if (moduleControl is IAIModuleContext ctx)
            {
                _chatPanel.SwitchContext(ctx.AIContextName, ctx.AIContextDescription, ctx.SuggestedQuestions);
            }
            else
            {
                _chatPanel.SwitchContext("navigation", null);
            }
        }

        private void ChatPanel_OnUICommandRequested(string cmd, Dictionary<string, object> args)
        {
            if (InvokeRequired)
                Invoke(new Action(() => ChuyenLenhAIDenModule(cmd, args)));
            else
                ChuyenLenhAIDenModule(cmd, args);
        }

        private void ChatPanel_OnOpenModuleRequested(string menuKey, Dictionary<string, object> args)
        {
            if (InvokeRequired)
                Invoke(new Action(() => OpenModule(menuKey)));
            else
                OpenModule(menuKey);
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            DatViTriAI();
        }

        private void BtnAI_Click(object sender, EventArgs e)
        {
            _chatPanel.Visible = !_chatPanel.Visible;
            if (_chatPanel.Visible)
            {
                DatViTriAI();
                _chatPanel.BringToFront();
            }
        }

        private void TabbedView_DocumentActivated(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {
            if (e.Document?.Control != null)
                CapNhatNguCanhAI(e.Document.Control);
        }

        private void LangViet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SessionManager.CurrentLanguage = "vi-VN";
            ApplyLanguage();
        }

        private void LangEng_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SessionManager.CurrentLanguage = "en-US";
            ApplyLanguage();
        }

        private void LangHoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SessionManager.CurrentLanguage = "zh-CN";
            ApplyLanguage();
        }

        private void SubLogout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var confirm = DevExpress.XtraEditors.XtraMessageBox.Show(
                GUI.Infrastructure.LanguageManager.GetString(AppConstants.ErrorMessages.MSG_CONFIRM_LOGOUT) ?? "Bạn có chắc chắn muốn đăng xuất?", 
                GUI.Infrastructure.LanguageManager.GetString("TOPBAR_LOGOUT") ?? "Đăng xuất", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);
                
            if (confirm == DialogResult.Yes)
            {
                SessionManager.DangXuat();
                Application.Restart();
            }
        }

        // Chuyển lệnh AI đến module đang active (VD: lọc grid)
        private void ChuyenLenhAIDenModule(string command, Dictionary<string, object> args)
        {
            var activeDoc = tabbedView.ActiveDocument;
            if (activeDoc?.Control is IAICommandHandler handler)
            {
                handler.ExecuteAICommand(command, args);
            }
        }

        #endregion
    }
}

using System;

namespace ET
{
    public partial class ET_KhachHang
    {
        public int Id { get; set; }
        public string MaCode { get; set; }
        public string HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string Email { get; set; }
        public string DienThoai { get; set; }
        public string CmndCccd { get; set; }
        public string DiaChi { get; set; }
        public string LoaiKhach { get; set; }
        public int DiemTichLuy { get; set; }
        public decimal TongChiTieu { get; set; }
        public int? IdDoan { get; set; }
        public string HinhAnh { get; set; }
        public string GhiChu { get; set; }
        public DateTime NgayDangKy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }

        public ET_KhachHang() { }

        public ET_KhachHang(int id, string maCode, string hoTen, DateTime? ngaySinh, string gioiTinh, string email, string dienThoai, string cmndCccd, string diaChi, string loaiKhach, int diemTichLuy, decimal tongChiTieu, int? idDoan, string hinhAnh, string ghiChu)
        {
            Id = id;
            MaCode = maCode;
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            GioiTinh = gioiTinh;
            Email = email;
            DienThoai = dienThoai;
            CmndCccd = cmndCccd;
            DiaChi = diaChi;
            LoaiKhach = loaiKhach;
            DiemTichLuy = diemTichLuy;
            TongChiTieu = tongChiTieu;
            IdDoan = idDoan;
            HinhAnh = hinhAnh;
            GhiChu = ghiChu;
        }

        public string TenLoaiKhach
        {
            get
            {
                switch (LoaiKhach)
                {
                    case "CaNhan": return "Cá nhân";
                    case "Doan": return "Đoàn khách";
                    case "DoanhNghiep": return "Doanh nghiệp";
                    case "HocSinhSinhVien": return "Học sinh/Sinh viên";
                    case "VIP": return "VIP";
                    case "VVIP": return "VVIP";
                    case "NoiBo": return "Nội bộ";
                    default: return LoaiKhach;
                }
            }
        }
    }
}

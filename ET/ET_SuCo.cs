using System;

namespace ET
{
    public class ET_SuCo
    {
        public int Id { get; set; }
        public int? IdKhachHang { get; set; }
        public int? IdNhanVienXuLy { get; set; }
        public DateTime ThoiGian { get; set; }
        public string MoTa { get; set; }
        public string MucDo { get; set; }
        public string ToaDoGps { get; set; }
        public string LoaiSuCo { get; set; }

        public string TenKhachHang { get; set; } 
        public string TenNhanVienXuLy { get; set; } 

        public string TenLoaiSuCo
        {
            get
            {
                switch (LoaiSuCo)
                {
                    case "Thuong": return "Khu vực thường";
                    case "DuoiNuoc": return "Đuối nước";
                    case "MatTre": return "Lạc mất trẻ";
                    case "DanhNhau": return "Đánh nhau / Cãi vã";
                    case "ThietBi": return "Hỏng thiết bị";
                    case "Khac": return "Khác";
                    default: return LoaiSuCo;
                }
            }
        }

        public string TenMucDo
        {
            get
            {
                switch (MucDo)
                {
                    case "Nhe": return "Nhẹ";
                    case "TrungBinh": return "Trung bình";
                    case "NghiemTrong": return "Nghiêm trọng";
                    case "KhanCap": return "Khẩn cấp";
                    default: return MucDo;
                }
            }
        }
    }
}

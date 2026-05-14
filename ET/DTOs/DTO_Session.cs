using System.Collections.Generic;

namespace ET.DTOs
{
    public class DTO_Session
    {
        public int IdDoiTac { get; set; }
        public string MaDoiTac { get; set; }
        public string HoTen { get; set; }
        public string LoaiTaiKhoan { get; set; }
        public List<string> DanhSachQuyen { get; set; }
    }
}

using System;

namespace ET.Models.DanhMuc
{
    public class ET_ChoiNghiMat : ET_TaiSanChoThue
    {
        public int? IdKhuVuc { get; set; }
        public int? SucChua { get; set; }
        
        public string TenKhuVuc { get; set; }
    }
}

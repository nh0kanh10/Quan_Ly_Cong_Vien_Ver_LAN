using System;

namespace ET
{
    public class ET_ThuHoiView
    {
        public int IdSanPham { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuongDaThue { get; set; }
        public int SoLuongChuaTra { get; set; }
        
        // Editable fields configured in DevExpress Grid
        public int TraLanNay { get; set; }
        public int BaoMat { get; set; }
    }
}

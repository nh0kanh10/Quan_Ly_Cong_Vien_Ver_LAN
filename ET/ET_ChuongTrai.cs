using System;

namespace ET
{
    public partial class ET_ChuongTrai
    {
        public int Id { get; set; }
        public int IdKhuVucThu { get; set; }
        public int IdDongVat { get; set; }
        public string TenChuong { get; set; }
        public int? SucChua { get; set; }
        public string TrangThai { get; set; }
    }
}

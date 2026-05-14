using System;

namespace ET.Models.HeThong
{
    // Bảng HeThong.LichSuTrangThai — Audit trail ai chuyển trạng thái gì lúc nào.
    // Id kiểu BIGINT vì bảng này sẽ rất nhiều dòng theo thời gian.
    public class LichSuTrangThai
    {
        public long Id { get; set; }
        public string ThucThe { get; set; }
        public int IdThucThe { get; set; }
        public string TuTrangThai { get; set; }
        public string DenTrangThai { get; set; }
        public int IdNguoiThucHien { get; set; }
        public DateTime ThoiGian { get; set; }
        public string GhiChu { get; set; }
    }
}

using System;
using System.ComponentModel;

namespace ET
{
    public class ET_CauHinhNgayLe
    {
        public int Id { get; set; }

        [DisplayName("Tên ngày lễ")]
        public string TenNgayLe { get; set; }

        [DisplayName("Từ ngày")]
        public DateTime NgayBatDau { get; set; }

        [DisplayName("Đến ngày")]
        public DateTime NgayKetThuc { get; set; }

        [DisplayName("Mô tả")]
        public string MoTa { get; set; }
    }
}

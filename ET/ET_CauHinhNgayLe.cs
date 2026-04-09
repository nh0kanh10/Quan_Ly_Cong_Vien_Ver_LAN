using System;
using System.ComponentModel;

namespace ET
{
    public class ET_CauHinhNgayLe
    {
        [DisplayName("Ngày")]
        public DateTime Ngay { get; set; }

        [DisplayName("Tên ngày lễ")]
        public string TenNgayLe { get; set; }
    }
}

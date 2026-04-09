using System;
using System.ComponentModel;

namespace ET
{
    public partial class ET_Combo
    {
        [Browsable(false)]
        public int Id { get; set; }

        [DisplayName("Mã")]
        public string MaCode { get; set; }

        [DisplayName("Tên Combo")]
        public string Ten { get; set; }

        [DisplayName("Giá")]
        [Browsable(false)]
        public decimal Gia { get; set; }

        [Browsable(false)]
        public string MoTa { get; set; }

        [DisplayName("T.Thái")]
        public string TrangThai { get; set; }

        [Browsable(false)]
        public DateTime CreatedAt { get; set; }

        [Browsable(false)]
        public DateTime? UpdatedAt { get; set; }

        [Browsable(false)]
        public int? CreatedBy { get; set; }

        [Browsable(false)]
        public bool IsDeleted { get; set; }
    }
}

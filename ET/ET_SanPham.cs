using System;
using System.ComponentModel;

namespace ET
{
    public partial class ET_SanPham
    {
        [Browsable(false)]
        public int Id { get; set; }

        [DisplayName("Mã")]
        public string MaCode { get; set; }

        [DisplayName("Tên SP")]
        public string Ten { get; set; }

        [DisplayName("Loại")]
        public string LoaiSanPham { get; set; }

        [Browsable(false)]
        public int IdDonViCoBan { get; set; }

        [DisplayName("Đơn giá")]
        public decimal DonGia { get; set; }

        [Browsable(false)]
        public int? IdKhuVuc { get; set; }

        [Browsable(false)]
        [DisplayName("Mô tả")]
        public string MoTa { get; set; }

        [DisplayName("Trạng thái")]
        public string TrangThai { get; set; }

        [Browsable(false)]
        public string HinhAnh { get; set; }

        [Browsable(false)]
        public DateTime CreatedAt { get; set; }

        [Browsable(false)]
        public DateTime? UpdatedAt { get; set; }

        [Browsable(false)]
        public int? CreatedBy { get; set; }

        [Browsable(false)]
        public bool IsDeleted { get; set; }

        [Browsable(false)]
        public ET_SanPham_Ve _veInfo { get; set; }
    }
}

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ET
{
    public class ET_BangGia : IDataErrorInfo, ICloneable
    {
        [Browsable(false)]
        public int Id { get; set; }

        [Browsable(false)]
        public int IdSanPham { get; set; }

        [DisplayName("Mức Giá")]
        [DefaultValue(0)]
        public decimal GiaBan { get; set; }

        [DisplayName("Áp dụng cho")]
        public string LoaiGiaApDung { get; set; } = "MacDinh";

        [Browsable(false)]
        public int? IdNgayLe { get; set; }

        [DisplayName("Từ")]
        public TimeSpan GioBatDau { get; set; } = TimeSpan.Zero;

        [DisplayName("Đến")]
        public TimeSpan GioKetThuc { get; set; } = new TimeSpan(23, 59, 0);

        [DisplayName("Block đầu (phút)")]
        [Description("Để trống = bán đứt")]
        public int? PhutBlock { get; set; }

        [DisplayName("Lố mỗi (phút)")]
        [Description("Mỗi X phút sau block đầu thu phụ thu")]
        public int? PhutTiep { get; set; }

        [DisplayName("Phụ thu lố")]
        public decimal? GiaPhuThu { get; set; }

        [DisplayName("Tiền cọc")]
        [Description("Tiền cọc khi thuê. Để trống = không cần cọc")]
        public decimal? TienCoc { get; set; }

        [DisplayName("Loại")]
        [ReadOnly(true)]
        public string LoaiGia => PhutBlock.HasValue ? "Thuê giờ" : "Bán đứt";

        [DisplayName("TT")]
        public string TrangThai { get; set; } = "HoạtĐộng";

        [Browsable(false), ReadOnly(true)]
        public DateTime CreatedAt { get; set; }

        [Browsable(false), ReadOnly(true)]
        public int? CreatedBy { get; set; }

        [Browsable(false)]
        public string Error => null;

        [Browsable(false)]
        public string this[string col]
        {
            get
            {
                if (col == nameof(GiaBan) && GiaBan < 0)
                    return "Giá không được âm";
                if (col == nameof(PhutBlock) && PhutBlock.HasValue && PhutBlock <= 0)
                    return "Phút block phải > 0";
                if (col == nameof(PhutTiep) && PhutTiep.HasValue && PhutTiep <= 0)
                    return "Phút tiếp phải > 0";
                return null;
            }
        }

        public object Clone() => MemberwiseClone();
    }
}

using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DAL
{
    public class DAL_BangGia
    {
            private static DAL_BangGia instance;
            public static DAL_BangGia Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = new DAL_BangGia();
                    }
                    return instance;
                }
                private set
                {
                    instance = value;
                }
            }
        
        public List<ET_BangGia> loadDS()
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext())
            {
                return db.BangGias.Select(b => new ET_BangGia
                {
                    MaBangGia = b.MaBangGia,
                    MaDichVu = b.MaDichVu ?? 0,
                    MaLoaiVe = b.MaLoaiVe ?? 0,
                    GiaBan = b.GiaBan,
                    LoaiNgay = b.LoaiNgay,
                    TrangThai = b.TrangThai,
                    NgayBatDau = b.NgayBatDau,
                    NgayKetThuc = b.NgayKetThuc,
                    NgayTao = b.NgayTao,
                    NgayCapNhat = b.NgayCapNhat ?? DateTime.Now
                }).ToList();
            }
        }
        public int LayMaBangGiaLonNhat()
        {
            using (var db = new QLKVCGTDataContext())
            {
                // Lấy giá trị ID lớn nhất trong bảng TroChoi
                int maxId = db.BangGias.Any() ? db.BangGias.Max(tc => tc.MaBangGia) : 0;
                return maxId + 1; // giá trị tiếp theo
            }
        }
        public bool Them(ET_BangGia bg)
        {
            try
            {
                using (QLKVCGTDataContext db = new QLKVCGTDataContext())
                {
                    BangGia b = new BangGia
                    {
                        MaDichVu = bg.MaDichVu,
                        MaLoaiVe = bg.MaLoaiVe,
                        GiaBan = bg.GiaBan,
                        LoaiNgay = bg.LoaiNgay,
                        TrangThai = bg.TrangThai,
                        NgayBatDau = bg.NgayBatDau,
                        NgayKetThuc = bg.NgayKetThuc,
                        NgayTao = DateTime.Now
                    };

                    db.BangGias.InsertOnSubmit(b);
                    db.SubmitChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()); // 👈 dùng ToString luôn
                return false;
            }
        }
        public bool Xoa(int maBangGia)
        {
            try
            {
                using (QLKVCGTDataContext db = new QLKVCGTDataContext())
                {
                    var bg = db.BangGias.SingleOrDefault(x => x.MaBangGia == maBangGia);
                    if (bg == null) return false;

                    db.BangGias.DeleteOnSubmit(bg);
                    db.SubmitChanges();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool CapNhat(ET_BangGia bg)
        {
            try
            {
                using (QLKVCGTDataContext db = new QLKVCGTDataContext())
                {
                    var ds = db.BangGias.SingleOrDefault(x => x.MaBangGia == bg.MaBangGia);
                    if (ds == null) return false;

                    ds.GiaBan = bg.GiaBan;
                    ds.LoaiNgay = bg.LoaiNgay;
                    ds.TrangThai = bg.TrangThai;
                    ds.NgayBatDau = bg.NgayBatDau;
                    ds.NgayKetThuc = bg.NgayKetThuc;
                    ds.NgayCapNhat = DateTime.Now;

                    db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public int LayMaBangGiaTiepTheo()
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext())
            {
                return db.BangGias.Any() ? db.BangGias.Max(x => x.MaBangGia) + 1 : 1;
            }
        }
    }
}

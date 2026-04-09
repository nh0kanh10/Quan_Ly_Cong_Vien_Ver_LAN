using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_TonKho
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_TonKho instance;
        public static DAL_TonKho Instance
        {
            get
            {
                if (instance == null) instance = new DAL_TonKho();
                return instance;
            }
        }

        public List<ET_TonKho> LoadDS()
        {
            return db.GetTable<TonKho>().Select(s => new ET_TonKho
            {
                Id = s.Id,
                IdKho = s.IdKho,
                IdSanPham = s.IdSanPham,
                SoLuong = s.SoLuong,
                RowVer = s.RowVer == null ? null : s.RowVer.ToArray()
            }).ToList();
        }

        public List<ET_TonKho> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_TonKho et)
        {
            try {
                TonKho obj = new TonKho();
                obj.IdKho = et.IdKho;
                obj.IdSanPham = et.IdSanPham;
                obj.SoLuong = et.SoLuong;
                obj.RowVer = et.RowVer;
                db.GetTable<TonKho>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_TonKho et)
        {
            try {
                var obj = db.GetTable<TonKho>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.IdKho = et.IdKho;
                    obj.IdSanPham = et.IdSanPham;
                    obj.SoLuong = et.SoLuong;
                    obj.RowVer = et.RowVer;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<TonKho>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<TonKho>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_TonKho LayTheoId(int id)
        {
            return db.GetTable<TonKho>().Where(x => x.Id == id).Select(s => new ET_TonKho {
                Id = s.Id,
                IdKho = s.IdKho,
                IdSanPham = s.IdSanPham,
                SoLuong = s.SoLuong,
                RowVer = s.RowVer == null ? null : s.RowVer.ToArray()
            }).FirstOrDefault();
        }

        public void DongBoTonKhoTrucTiepTuTheKho()
        {
            try
            {
                // 1. Tính tổng từ sổ thẻ kho (Log) dùng LINQ-to-SQL
                var expectedStock = db.GetTable<TheKho>()
                    .GroupBy(x => new { x.IdKho, x.IdSanPham })
                    .Select(g => new 
                    { 
                        IdKho = g.Key.IdKho, 
                        IdSanPham = g.Key.IdSanPham, 
                        TongSoLuong = g.Sum(x => x.SoLuongThayDoi) 
                    }).ToList();

                // 2. Lấy danh sách tồn kho hiện tại (Snapshot)
                var currentStock = db.GetTable<TonKho>().ToList();

                foreach (var expected in expectedStock)
                {
                    var tk = currentStock.FirstOrDefault(x => x.IdKho == expected.IdKho && x.IdSanPham == expected.IdSanPham);
                    if (tk != null)
                    {
                        if (tk.SoLuong != expected.TongSoLuong)
                        {
                            tk.SoLuong = expected.TongSoLuong;
                        }
                    }
                    else
                    {
                        var newTk = new TonKho 
                        {
                            IdKho = expected.IdKho,
                            IdSanPham = expected.IdSanPham,
                            SoLuong = expected.TongSoLuong
                        };
                        db.GetTable<TonKho>().InsertOnSubmit(newTk);
                    }
                }

                db.SubmitChanges();
            }
            catch { }
        }
    }
}

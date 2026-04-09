using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public class DAL_KhuVucThu
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_KhuVucThu instance;
        public static DAL_KhuVucThu Instance
        {
            get
            {
                if (instance == null) instance = new DAL_KhuVucThu();
                return instance;
            }
        }

        /// <summary>
        /// Load danh sách KhuVucThu JOIN KhuVuc (parent) để lấy TenKhuVuc, MoTa
        /// </summary>
        public List<ET_KhuVucThu> LoadDS()
        {
            return (from t in db.GetTable<KhuVucThu>()
                    join kv in db.GetTable<KhuVuc>() on t.IdKhuVuc equals kv.Id
                    select new ET_KhuVucThu
                    {
                        Id = t.IdKhuVuc,
                        TenKhuVuc = kv.TenKhuVuc,
                        MoTa = kv.MoTa,
                        MaCode = kv.MaCode
                    }).ToList();
        }

        /// <summary>
        /// Thêm: Tạo KhuVuc (parent) trước, rồi tạo KhuVucThu (child)
        /// </summary>
        public bool Them(ET_KhuVucThu et)
        {
            try
            {
                var khuVuc = new KhuVuc
                {
                    MaCode = et.MaCode,
                    TenKhuVuc = et.TenKhuVuc,
                    MoTa = et.MoTa,
                    TrangThai = "Hoạt động"
                };
                db.GetTable<KhuVuc>().InsertOnSubmit(khuVuc);
                db.SubmitChanges();

                var obj = new KhuVucThu
                {
                    IdKhuVuc = khuVuc.Id  // Weak Entity: PK = FK -> KhuVuc.Id
                };
                db.GetTable<KhuVucThu>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Sửa: Cập nhật KhuVuc (parent) — KhuVucThu không có thuộc tính riêng
        /// </summary>
        public bool Sua(ET_KhuVucThu et)
        {
            try
            {
                var parent = db.GetTable<KhuVuc>().FirstOrDefault(x => x.Id == et.Id);
                if (parent != null)
                {
                    parent.TenKhuVuc = et.TenKhuVuc;
                    parent.MoTa = et.MoTa;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        /// <summary>
        /// Xóa: Xóa KhuVucThu (child) trước, rồi xóa KhuVuc (parent)
        /// </summary>
        public bool Xoa(int id)
        {
            try
            {
                var child = db.GetTable<KhuVucThu>().FirstOrDefault(x => x.IdKhuVuc == id);
                if (child != null)
                {
                    db.GetTable<KhuVucThu>().DeleteOnSubmit(child);
                    db.SubmitChanges();

                    var parent = db.GetTable<KhuVuc>().FirstOrDefault(x => x.Id == id);
                    if (parent != null)
                    {
                        db.GetTable<KhuVuc>().DeleteOnSubmit(parent);
                        db.SubmitChanges();
                    }
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        public List<ET_KhuVucThu> TimKiem(string keyword)
        {
            var ds = LoadDS();
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.ToLower();
                ds = ds.Where(x => (x.TenKhuVuc != null && x.TenKhuVuc.ToLower().Contains(keyword))
                                || (x.MoTa != null && x.MoTa.ToLower().Contains(keyword))).ToList();
            }
            return ds;
        }
    }
}

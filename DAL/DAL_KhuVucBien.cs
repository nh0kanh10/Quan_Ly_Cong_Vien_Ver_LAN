using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public class DAL_KhuVucBien
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_KhuVucBien instance;
        public static DAL_KhuVucBien Instance
        {
            get
            {
                if (instance == null) instance = new DAL_KhuVucBien();
                return instance;
            }
        }

        /// <summary>
        /// Load danh sách KhuVucBien JOIN KhuVuc (parent) để lấy TenKhuVuc, MoTa
        /// </summary>
        public List<ET_KhuVucBien> LoadDS()
        {
            return (from b in db.GetTable<KhuVucBien>()
                    join kv in db.GetTable<KhuVuc>() on b.IdKhuVuc equals kv.Id
                    select new ET_KhuVucBien
                    {
                        Id = b.IdKhuVuc,
                        DoSauToiDa = b.DoSauToiDa,
                        YeuCauPhao = b.YeuCauPhao,
                        TenKhuVuc = kv.TenKhuVuc,
                        MoTa = kv.MoTa,
                        MaCode = kv.MaCode
                    }).ToList();
        }

        /// <summary>
        /// Thêm: Tạo KhuVuc (parent) trước, rồi tạo KhuVucBien (child)
        /// </summary>
        public bool Them(ET_KhuVucBien et)
        {
            try
            {
                // Step 1: Insert parent KhuVuc
                var khuVuc = new KhuVuc
                {
                    MaCode = et.MaCode,
                    TenKhuVuc = et.TenKhuVuc,
                    MoTa = et.MoTa,
                    TrangThai = "Hoạt động"
                };
                db.GetTable<KhuVuc>().InsertOnSubmit(khuVuc);
                db.SubmitChanges();

                // Step 2: Insert child KhuVucBien with same Id
                var obj = new KhuVucBien
                {
                    IdKhuVuc = khuVuc.Id,  // Weak Entity: PK = FK -> KhuVuc.Id
                    DoSauToiDa = et.DoSauToiDa,
                    YeuCauPhao = et.YeuCauPhao
                };
                db.GetTable<KhuVucBien>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Sửa: Cập nhật cả KhuVuc (parent) và KhuVucBien (child)
        /// </summary>
        public bool Sua(ET_KhuVucBien et)
        {
            try
            {
                var parent = db.GetTable<KhuVuc>().FirstOrDefault(x => x.Id == et.Id);
                var child = db.GetTable<KhuVucBien>().FirstOrDefault(x => x.IdKhuVuc == et.Id);
                if (parent != null && child != null)
                {
                    parent.TenKhuVuc = et.TenKhuVuc;
                    parent.MoTa = et.MoTa;
                    child.DoSauToiDa = et.DoSauToiDa;
                    child.YeuCauPhao = et.YeuCauPhao;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        /// <summary>
        /// Xóa: Xóa KhuVucBien (child) trước, rồi xóa KhuVuc (parent)
        /// </summary>
        public bool Xoa(int id)
        {
            try
            {
                var child = db.GetTable<KhuVucBien>().FirstOrDefault(x => x.IdKhuVuc == id);
                if (child != null)
                {
                    db.GetTable<KhuVucBien>().DeleteOnSubmit(child);
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

        public List<ET_KhuVucBien> TimKiem(string keyword)
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

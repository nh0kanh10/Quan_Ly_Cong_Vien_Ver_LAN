using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories.DanhMuc
{
    public class DAL_KhuVuc
    {
        public static DAL_KhuVuc Instance { get; } = new DAL_KhuVuc();

        public List<KhuVuc> LayDanhSach(string langCode = "vi-VN")
        {
            using (var db = new DaiNamDBDataContext())
            {
                var query = db.KhuVucs.Where(k => k.DaXoa == false);
                if (langCode == "vi-VN" || string.IsNullOrEmpty(langCode))
                    return query.ToList();

                var listRaw = (from k in query
                            join bd in db.BanDiches
                            on new { Id = k.Id, Loai = "KhuVuc", Cot = "TenKhuVuc", NgonNgu = langCode }
                            equals new { Id = bd.IdThucThe, Loai = bd.LoaiThucThe, Cot = bd.TruongDich, NgonNgu = bd.NgonNgu } into dich
                            from d in dich.DefaultIfEmpty()
                            select new {
                                Id = k.Id,
                                MaKhuVuc = k.MaKhuVuc,
                                TenKhuVuc = d != null ? d.NoiDung : k.TenKhuVuc,
                                LoaiKhuVuc = k.LoaiKhuVuc,
                                SucChua = k.SucChua,
                                DaXoa = k.DaXoa
                            }).ToList();

                var list = listRaw.Select(k => new KhuVuc {
                    Id = k.Id,
                    MaKhuVuc = k.MaKhuVuc,
                    TenKhuVuc = k.TenKhuVuc,
                    LoaiKhuVuc = k.LoaiKhuVuc,
                    SucChua = k.SucChua,
                    DaXoa = k.DaXoa
                }).ToList();

                return list;

            }
        }
    }
}



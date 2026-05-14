using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories.HeThong
{
    public class DAL_BanDich
    {
        public static DAL_BanDich Instance { get; } = new DAL_BanDich();

        /// <summary>
        /// Lấy toàn bộ từ điển của hệ thống lên .
        /// </summary>
        public List<BanDich> LayTuDienHeThong()
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.BanDiches
                    .Where(b => b.LoaiThucThe == "UI")
                    .ToList();
            }
        }

        /// <summary>
        /// Lấy bản dịch 1 trường cụ thể theo composite key.
        /// </summary>
        public string LayNoiDung(string loaiThucThe, int idThucThe, string ngonNgu, string truongDich)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.BanDiches
                    .Where(b => b.LoaiThucThe == loaiThucThe
                             && b.IdThucThe == idThucThe
                             && b.NgonNgu == ngonNgu
                             && b.TruongDich == truongDich)
                    .Select(b => b.NoiDung)
                    .FirstOrDefault();
            }
        }

        /// <summary>
        /// Lấy nguyên một từ điển bản dịch cho 1 loại thực thể (chống N+1 query).
        /// </summary>
        public Dictionary<int, string> LayTuDienThucThe(string loaiThucThe, string ngonNgu, string truongDich = null)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var query = db.BanDiches.Where(b => b.LoaiThucThe == loaiThucThe && b.NgonNgu == ngonNgu);
                if (!string.IsNullOrEmpty(truongDich))
                    query = query.Where(b => b.TruongDich == truongDich);

                return query.ToDictionary(b => b.IdThucThe, b => b.NoiDung);
            }
        }
    }
}



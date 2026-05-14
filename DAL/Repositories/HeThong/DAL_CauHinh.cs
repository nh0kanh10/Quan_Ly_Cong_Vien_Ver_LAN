using System.Linq;

namespace DAL.Repositories.HeThong
{
    public class DAL_CauHinh
    {
        public static DAL_CauHinh Instance { get; } = new DAL_CauHinh();

        /// Lấy GiaTri theo Khoa. Returns null nếu không tìm thấy.
        public string LayGiaTri(string khoa)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.CauHinhs
                    .Where(c => c.Khoa == khoa)
                    .Select(c => c.GiaTri)
                    .FirstOrDefault();
            }
        }

        /// Lấy GiaTri dạng decimal, fallback 0 nếu lỗi.
        public decimal LayGiaTriDecimal(string khoa, decimal fallback = 0)
        {
            string val = LayGiaTri(khoa);
            return decimal.TryParse(val, out decimal result) ? result : fallback;
        }
    }
}



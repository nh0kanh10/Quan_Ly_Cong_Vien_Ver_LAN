using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    /// <summary>
    /// DAL_TroChoi giờ là wrapper: delegate sang DanhSachThietBi filter LoaiThietBi='TroChoi'.
    /// Giữ lại để các form cũ (frmTroChoi, frmSanPham) không bị break.
    /// </summary>
    public class DAL_TroChoi
    {
        private static DAL_TroChoi instance;
        public static DAL_TroChoi Instance
        {
            get
            {
                if (instance == null) instance = new DAL_TroChoi();
                return instance;
            }
        }

        public List<ET_TroChoi> LoadDS()
        {
            return DAL_DanhSachThietBi.Instance.LoadDSTheoLoai("TroChoi")
                .Select(x => new ET_TroChoi
                {
                    Id = x.Id,
                    MaCode = x.MaCode,
                    TenTroChoi = x.TenThietBi,
                    IdKhuVuc = x.IdKhuVuc,
                    MoTa = x.MoTa,
                    TrangThai = x.TrangThai
                }).ToList();
        }

        public bool Them(ET_TroChoi et)
        {
            return DAL_DanhSachThietBi.Instance.Them(new ET_DanhSachThietBi
            {
                MaCode = et.MaCode,
                TenThietBi = et.TenTroChoi,
                LoaiThietBi = "TroChoi",
                IdKhuVuc = et.IdKhuVuc,
                MoTa = et.MoTa,
                TrangThai = et.TrangThai ?? "HoatDong"
            });
        }

        public bool Sua(ET_TroChoi et)
        {
            return DAL_DanhSachThietBi.Instance.Sua(new ET_DanhSachThietBi
            {
                Id = et.Id,
                MaCode = et.MaCode,
                TenThietBi = et.TenTroChoi,
                LoaiThietBi = "TroChoi",
                IdKhuVuc = et.IdKhuVuc,
                MoTa = et.MoTa,
                TrangThai = et.TrangThai
            });
        }

        public bool Xoa(int id) => DAL_DanhSachThietBi.Instance.Xoa(id);

        public List<ET_TroChoi> TimKiem(string keyword, string idKhuVuc)
        {
            var ds = LoadDS();
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.ToLower();
                ds = ds.Where(x => (x.TenTroChoi != null && x.TenTroChoi.ToLower().Contains(keyword)) ||
                                   (x.MaCode != null && x.MaCode.ToLower().Contains(keyword))).ToList();
            }
            if (!string.IsNullOrEmpty(idKhuVuc) && idKhuVuc != "Tất cả")
            {
                int kId = int.Parse(idKhuVuc);
                ds = ds.Where(x => x.IdKhuVuc == kId).ToList();
            }
            return ds;
        }
    }
}

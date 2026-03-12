using ET;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class DAL_ChiTietCombo
    {
        private static DAL_ChiTietCombo instance;
        public static DAL_ChiTietCombo Instance
        {
            get
            {
                if (instance == null) instance = new DAL_ChiTietCombo();
                return instance;
            }
        }

        /// <summary>
        /// Load danh sách vé con của 1 combo (theo MaLoaiVe của cha)
        /// </summary>
        public List<ComboDetailView> LoadVeConTheoCombo(int maLoaiVeCha)
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext())
            {
                var ds = from ct in db.ChiTietCombos
                         join lv in db.LoaiVes on ct.MaLoaiVeCon equals lv.MaLoaiVe
                         where ct.MaLoaiVeCha == maLoaiVeCha
                         select new ComboDetailView
                         {
                             MaChiTietCombo = ct.MaChiTietCombo,
                             MaLoaiVeCha = ct.MaLoaiVeCha,
                             MaLoaiVeCon = ct.MaLoaiVeCon,
                             TenVeCon = lv.TenLoaiVe,
                             MaCodeVeCon = lv.MaCode,
                             SoLuotChoPhep = ct.SoLuotChoPhep
                         };
                return ds.ToList();
            }
        }

        /// <summary>
        /// Thêm 1 vé con vào combo
        /// </summary>
        public bool ThemVeCon(int maLoaiVeCha, int maLoaiVeCon, int soLuot)
        {
            try
            {
                using (QLKVCGTDataContext db = new QLKVCGTDataContext())
                {
                    // Kiểm tra đã tồn tại chưa
                    bool daTonTai = db.ChiTietCombos.Any(
                        x => x.MaLoaiVeCha == maLoaiVeCha && x.MaLoaiVeCon == maLoaiVeCon);
                    if (daTonTai) return false;

                    ChiTietCombo ct = new ChiTietCombo
                    {
                        MaLoaiVeCha = maLoaiVeCha,
                        MaLoaiVeCon = maLoaiVeCon,
                        SoLuotChoPhep = soLuot,
                        NgayTao = DateTime.Now
                    };
                    db.ChiTietCombos.InsertOnSubmit(ct);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        /// <summary>
        /// Xóa 1 vé con khỏi combo
        /// </summary>
        public bool XoaVeCon(int maChiTietCombo)
        {
            try
            {
                using (QLKVCGTDataContext db = new QLKVCGTDataContext())
                {
                    var ct = db.ChiTietCombos.SingleOrDefault(x => x.MaChiTietCombo == maChiTietCombo);
                    if (ct != null)
                    {
                        db.ChiTietCombos.DeleteOnSubmit(ct);
                        db.SubmitChanges();
                    }
                    return true;
                }
            }
            catch { return false; }
        }

        /// <summary>
        /// Xóa tất cả vé con của 1 combo
        /// </summary>
        public bool XoaTatCaVeCon(int maLoaiVeCha)
        {
            try
            {
                using (QLKVCGTDataContext db = new QLKVCGTDataContext())
                {
                    var ds = db.ChiTietCombos.Where(ct => ct.MaLoaiVeCha == maLoaiVeCha);
                    db.ChiTietCombos.DeleteAllOnSubmit(ds);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        /// <summary>
        /// Load danh sách vé KHÔNG phải combo (để chọn làm vé con)
        /// </summary>
        public List<ET_LoaiVe> LoadDSVeThuong()
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext())
            {
                var ds = from lv in db.LoaiVes
                         where lv.LaCombo == false && lv.TrangThai == "Hoạt động"
                         select new ET_LoaiVe
                         {
                             MaLoaiVe = lv.MaLoaiVe,
                             MaCode = lv.MaCode,
                             TenLoaiVe = lv.TenLoaiVe,
                             GiaVe = lv.GiaVe,
                         };
                return ds.ToList();
            }
        }
    }
}

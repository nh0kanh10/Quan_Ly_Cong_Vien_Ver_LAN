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

    
        public List<ComboDetailView> LoadVeConTheoCombo(int maLoaiVeCha)
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext(ConnectionManager.GetConnectionString()))
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
                             GiaVe = lv.GiaVe,
                             SoLuotChoPhep = ct.SoLuotChoPhep
                         };
                return ds.ToList();
            }
        }

       
        public bool ThemVeCon(int maLoaiVeCha, int maLoaiVeCon, int soLuot)
        {
            try
            {
                using (QLKVCGTDataContext db = new QLKVCGTDataContext(ConnectionManager.GetConnectionString()))
                {
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

        
        public bool XoaVeCon(int maChiTietCombo)
        {
            try
            {
                using (QLKVCGTDataContext db = new QLKVCGTDataContext(ConnectionManager.GetConnectionString()))
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

        
        public bool XoaTatCaVeCon(int maLoaiVeCha)
        {
            try
            {
                using (QLKVCGTDataContext db = new QLKVCGTDataContext(ConnectionManager.GetConnectionString()))
                {
                    var ds = db.ChiTietCombos.Where(ct => ct.MaLoaiVeCha == maLoaiVeCha);
                    db.ChiTietCombos.DeleteAllOnSubmit(ds);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        
        public List<ET_LoaiVe> LoadDSVeThuong()
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext(ConnectionManager.GetConnectionString()))
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

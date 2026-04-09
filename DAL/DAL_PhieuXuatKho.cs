using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_PhieuXuatKho
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_PhieuXuatKho instance;
        public static DAL_PhieuXuatKho Instance
        {
            get
            {
                if (instance == null) instance = new DAL_PhieuXuatKho();
                return instance;
            }
        }

        public List<ET_PhieuXuatKho> LoadDS()
        {
            return db.GetTable<PhieuXuatKho>().Select(s => new ET_PhieuXuatKho
            {
                Id = s.Id,
                IdKhoXuat = s.IdKhoXuat,
                IdKhoNhan = s.IdKhoNhan,
                NgayXuat = s.NgayXuat,
                LyDo = s.LyDo,
                IdDonHangLienQuan = s.IdDonHangLienQuan,
                CreatedAt = s.CreatedAt,
                CreatedBy = s.CreatedBy
            }).ToList();
        }

        public List<ET_PhieuXuatKho> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_PhieuXuatKho et)
        {
            try {
                PhieuXuatKho obj = new PhieuXuatKho();
                obj.IdKhoXuat = et.IdKhoXuat;
                obj.IdKhoNhan = et.IdKhoNhan;
                obj.NgayXuat = et.NgayXuat;
                obj.LyDo = et.LyDo;
                obj.IdDonHangLienQuan = et.IdDonHangLienQuan;
                obj.CreatedAt = et.CreatedAt;
                obj.CreatedBy = et.CreatedBy;
                db.GetTable<PhieuXuatKho>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_PhieuXuatKho et)
        {
            try {
                var obj = db.GetTable<PhieuXuatKho>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.IdKhoXuat = et.IdKhoXuat;
                    obj.IdKhoNhan = et.IdKhoNhan;
                    obj.NgayXuat = et.NgayXuat;
                    obj.LyDo = et.LyDo;
                    obj.IdDonHangLienQuan = et.IdDonHangLienQuan;
                    obj.CreatedAt = et.CreatedAt;
                    obj.CreatedBy = et.CreatedBy;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<PhieuXuatKho>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<PhieuXuatKho>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_PhieuXuatKho LayTheoId(int id)
        {
            return db.GetTable<PhieuXuatKho>().Where(x => x.Id == id).Select(s => new ET_PhieuXuatKho {
                Id = s.Id,
                IdKhoXuat = s.IdKhoXuat,
                IdKhoNhan = s.IdKhoNhan,
                NgayXuat = s.NgayXuat,
                LyDo = s.LyDo,
                IdDonHangLienQuan = s.IdDonHangLienQuan,
                CreatedAt = s.CreatedAt,
                CreatedBy = s.CreatedBy
            }).FirstOrDefault();
        }

        /// <summary>
        /// Atomic: Insert Master + all Details in one transaction.
        /// Returns the new PhieuXuatKho Id, or -1 on failure.
        /// </summary>
        public int ThemHoanChinh(ET_PhieuXuatKho master, List<ET_ChiTietXuatKho> details)
        {
            using (var txDb = new DataQuanLyDaiNamDataContext())
            {
                if (txDb.Connection.State != System.Data.ConnectionState.Open)
                    txDb.Connection.Open();
                using (var tx = txDb.Connection.BeginTransaction())
                {
                    txDb.Transaction = tx;
                    try
                    {
                        var obj = new PhieuXuatKho();
                        obj.IdKhoXuat = master.IdKhoXuat;
                        obj.IdKhoNhan = master.IdKhoNhan;
                        obj.NgayXuat = master.NgayXuat;
                        obj.LyDo = master.LyDo;
                        obj.IdDonHangLienQuan = master.IdDonHangLienQuan;
                        obj.CreatedAt = master.CreatedAt;
                        obj.CreatedBy = master.CreatedBy;
                        txDb.GetTable<PhieuXuatKho>().InsertOnSubmit(obj);
                        txDb.SubmitChanges();

                        int newId = obj.Id;

                        foreach (var d in details)
                        {
                            var ct = new ChiTietXuatKho();
                            ct.IdPhieuXuat = newId;
                            ct.IdSanPham = d.IdSanPham;
                            ct.SoLuong = d.SoLuong;
                            ct.DonGiaXuat = d.DonGiaXuat;
                            ct.IdDonViXuat = d.IdDonViXuat;
                            ct.TyLeQuyDoi = d.TyLeQuyDoi > 0 ? d.TyLeQuyDoi : 1;
                            txDb.GetTable<ChiTietXuatKho>().InsertOnSubmit(ct);
                        }
                        txDb.SubmitChanges();

                        tx.Commit();
                        return newId;
                    }
                    catch
                    {
                        tx.Rollback();
                        return -1;
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_PhieuNhapKho
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_PhieuNhapKho instance;
        public static DAL_PhieuNhapKho Instance
        {
            get
            {
                if (instance == null) instance = new DAL_PhieuNhapKho();
                return instance;
            }
        }

        public List<ET_PhieuNhapKho> LoadDS()
        {
            return db.GetTable<PhieuNhapKho>().Select(s => new ET_PhieuNhapKho
            {
                Id = s.Id,
                IdKho = s.IdKho,
                IdNhaCungCap = s.IdNhaCungCap,
                NgayNhap = s.NgayNhap,
                SoChungTu = s.SoChungTu,
                TongTien = s.TongTien,
                IdPhieuChi = s.IdPhieuChi,
                CreatedAt = s.CreatedAt,
                CreatedBy = s.CreatedBy
            }).ToList();
        }

        public List<ET_PhieuNhapKho> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_PhieuNhapKho et)
        {
            try {
                PhieuNhapKho obj = new PhieuNhapKho();
                obj.IdKho = et.IdKho;
                obj.IdNhaCungCap = et.IdNhaCungCap;
                obj.NgayNhap = et.NgayNhap;
                obj.SoChungTu = et.SoChungTu;
                obj.TongTien = et.TongTien;
                obj.IdPhieuChi = et.IdPhieuChi;
                obj.CreatedAt = et.CreatedAt;
                obj.CreatedBy = et.CreatedBy;
                db.GetTable<PhieuNhapKho>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_PhieuNhapKho et)
        {
            try {
                var obj = db.GetTable<PhieuNhapKho>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.IdKho = et.IdKho;
                    obj.IdNhaCungCap = et.IdNhaCungCap;
                    obj.NgayNhap = et.NgayNhap;
                    obj.SoChungTu = et.SoChungTu;
                    obj.TongTien = et.TongTien;
                    obj.IdPhieuChi = et.IdPhieuChi;
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
                var obj = db.GetTable<PhieuNhapKho>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<PhieuNhapKho>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_PhieuNhapKho LayTheoId(int id)
        {
            return db.GetTable<PhieuNhapKho>().Where(x => x.Id == id).Select(s => new ET_PhieuNhapKho {
                Id = s.Id,
                IdKho = s.IdKho,
                IdNhaCungCap = s.IdNhaCungCap,
                NgayNhap = s.NgayNhap,
                SoChungTu = s.SoChungTu,
                TongTien = s.TongTien,
                IdPhieuChi = s.IdPhieuChi,
                CreatedAt = s.CreatedAt,
                CreatedBy = s.CreatedBy
            }).FirstOrDefault();
        }

        /// <summary>
        /// Atomic: Insert Master + all Details in one transaction.
        /// Returns the new PhieuNhapKho Id, or -1 on failure.
        /// </summary>
        public int ThemHoanChinh(ET_PhieuNhapKho master, List<ET_ChiTietNhapKho> details)
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
                        // 1. Insert Master
                        var obj = new PhieuNhapKho();
                        obj.IdKho = master.IdKho;
                        obj.IdNhaCungCap = master.IdNhaCungCap;
                        obj.NgayNhap = master.NgayNhap;
                        obj.SoChungTu = master.SoChungTu;
                        obj.TongTien = master.TongTien;
                        obj.IdPhieuChi = master.IdPhieuChi;
                        obj.CreatedAt = master.CreatedAt;
                        obj.CreatedBy = master.CreatedBy;
                        txDb.GetTable<PhieuNhapKho>().InsertOnSubmit(obj);
                        txDb.SubmitChanges();

                        int newId = obj.Id; // IDENTITY returns

                        // 2. Insert Details
                        foreach (var d in details)
                        {
                            var ct = new ChiTietNhapKho();
                            ct.IdPhieuNhap = newId;
                            ct.IdSanPham = d.IdSanPham;
                            ct.SoLuong = d.SoLuong;
                            ct.DonGiaNhap = d.DonGiaNhap;
                            ct.IdDonViNhap = d.IdDonViNhap;
                            ct.TyLeQuyDoi = d.TyLeQuyDoi;
                            txDb.GetTable<ChiTietNhapKho>().InsertOnSubmit(ct);
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

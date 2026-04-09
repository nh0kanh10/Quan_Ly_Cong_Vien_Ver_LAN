using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_ViDienTu
    {
        private static DAL_ViDienTu instance;
        private DataQuanLyDaiNamDataContext CreateDb() { return new DataQuanLyDaiNamDataContext(); }
        public static DAL_ViDienTu Instance
        {
            get
            {
                if (instance == null) instance = new DAL_ViDienTu();
                return instance;
            }
        }

        public List<ET_ViDienTu> LoadDS()
        {
            using (var db = CreateDb())
            {
                return db.GetTable<ViDienTu>().Select(s => new ET_ViDienTu
                {
                    Id = s.Id,
                    IdKhachHang = s.IdKhachHang,
                    SoDuKhaDung = s.SoDuKhaDung,
                    SoDuDongBang = s.SoDuDongBang,
                    RowVer = s.RowVer == null ? null : s.RowVer.ToArray()
                }).ToList();
            }
        }

        public List<ET_ViDienTu> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_ViDienTu et)
        {
            if (et == null) return false;
            try {
                using (var db = CreateDb())
                {
                    ViDienTu obj = new ViDienTu();
                    obj.IdKhachHang = et.IdKhachHang;
                    obj.SoDuKhaDung = et.SoDuKhaDung;
                    obj.SoDuDongBang = et.SoDuDongBang;
                    obj.RowVer = et.RowVer;
                    db.GetTable<ViDienTu>().InsertOnSubmit(obj);
                    db.SubmitChanges();
                }
                return true;
            } catch (Exception ex) { DalErrorLogger.Log("DAL_ViDienTu.Them", ex); return false; }
        }

        /// <summary>
        /// [PATCH 1 - CVE-2 FIX]: Real OCC using WHERE RowVer = @original.
        /// Throws System.Data.DBConcurrencyException if another terminal changed the wallet.
        /// </summary>
        public bool Sua(ET_ViDienTu et)
        {
            if (et == null) return false;
            using (var db = CreateDb())
            {
                // Raw SQL to enforce OCC: WHERE RowVer = @original
                int affected = db.ExecuteCommand(
                    "UPDATE ViDienTu SET SoDuKhaDung = {0}, SoDuDongBang = {1}, IdKhachHang = {2} " +
                    "WHERE Id = {3} AND RowVer = {4}",
                    et.SoDuKhaDung, et.SoDuDongBang, et.IdKhachHang,
                    et.Id, et.RowVer
                );

                if (affected == 0)
                    throw new System.Data.DBConcurrencyException(
                        "Ví đã bị thay đổi bởi terminal khác. Vui lòng thử lại.");

                return true;
            }
        }

        public bool Xoa(int id)
        {
            try {
                using (var db = CreateDb())
                {
                    var obj = db.GetTable<ViDienTu>().FirstOrDefault(x => x.Id == id);
                    if (obj != null) {
                        db.GetTable<ViDienTu>().DeleteOnSubmit(obj);
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
            } catch (Exception ex) { DalErrorLogger.Log("DAL_ViDienTu.Xoa", ex); return false; }
        }

        public ET_ViDienTu LayTheoId(int id)
        {
            using (var db = CreateDb())
            {
                return db.GetTable<ViDienTu>().Where(x => x.Id == id).Select(s => new ET_ViDienTu {
                    Id = s.Id,
                    IdKhachHang = s.IdKhachHang,
                    SoDuKhaDung = s.SoDuKhaDung,
                    SoDuDongBang = s.SoDuDongBang,
                    RowVer = s.RowVer == null ? null : s.RowVer.ToArray()
                }).FirstOrDefault();
            }
        }

        /// <summary>
        /// [PATCH 4 - CVE-6 FIX]: O(1) lookup by IdKhachHang instead of LoadDS().FirstOrDefault
        /// </summary>
        public ET_ViDienTu LayTheoKhachHang(int idKhachHang)
        {
            using (var db = CreateDb())
            {
                return db.GetTable<ViDienTu>()
                    .Where(x => x.IdKhachHang == idKhachHang)
                    .Select(s => new ET_ViDienTu {
                        Id = s.Id,
                        IdKhachHang = s.IdKhachHang,
                        SoDuKhaDung = s.SoDuKhaDung,
                        SoDuDongBang = s.SoDuDongBang,
                        RowVer = s.RowVer == null ? null : s.RowVer.ToArray()
                    }).FirstOrDefault();
            }
        }
    }
}

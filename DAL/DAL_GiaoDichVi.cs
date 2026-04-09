using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_GiaoDichVi
    {
        private static DAL_GiaoDichVi instance;
        private DataQuanLyDaiNamDataContext CreateDb() { return new DataQuanLyDaiNamDataContext(); }
        public static DAL_GiaoDichVi Instance
        {
            get
            {
                if (instance == null) instance = new DAL_GiaoDichVi();
                return instance;
            }
        }

        public List<ET_GiaoDichVi> LoadDS()
        {
            using (var db = CreateDb())
            {
                return db.GetTable<GiaoDichVi>().Select(s => new ET_GiaoDichVi
                {
                    Id = s.Id,
                    MaCode = s.MaCode,
                    IdVi = s.IdVi,
                    LoaiGiaoDich = s.LoaiGiaoDich,
                    SoTien = s.SoTien,
                    IdDonHangLienQuan = s.IdDonHangLienQuan,
                    ParentTransactionId = s.ParentTransactionId,
                    ThoiGian = s.ThoiGian,
                    HashSignature = s.HashSignature,
                    CreatedAt = s.CreatedAt,
                    CreatedBy = s.CreatedBy
                }).ToList();
            }
        }

        public List<ET_GiaoDichVi> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_GiaoDichVi et)
        {
            if (et == null) return false;
            try {
                using (var db = CreateDb())
                {
                    GiaoDichVi obj = new GiaoDichVi();
                    obj.MaCode = et.MaCode;
                    obj.IdVi = et.IdVi;
                    obj.LoaiGiaoDich = et.LoaiGiaoDich;
                    obj.SoTien = et.SoTien;
                    obj.IdDonHangLienQuan = et.IdDonHangLienQuan;
                    obj.ParentTransactionId = et.ParentTransactionId;
                    obj.ThoiGian = et.ThoiGian;
                    obj.HashSignature = et.HashSignature;
                    obj.CreatedAt = et.CreatedAt;
                    obj.CreatedBy = et.CreatedBy;
                    db.GetTable<GiaoDichVi>().InsertOnSubmit(obj);
                    db.SubmitChanges();
                }
                return true;
            } catch (Exception ex) { DalErrorLogger.Log("DAL_GiaoDichVi.Them", ex); return false; }
        }

        public int ThemVaLayId(ET_GiaoDichVi et)
        {
            if (et == null) return 0;
            try
            {
                using (var db = CreateDb())
                {
                    GiaoDichVi obj = new GiaoDichVi();
                    obj.MaCode = et.MaCode;
                    obj.IdVi = et.IdVi;
                    obj.LoaiGiaoDich = et.LoaiGiaoDich;
                    obj.SoTien = et.SoTien;
                    obj.IdDonHangLienQuan = et.IdDonHangLienQuan;
                    obj.ParentTransactionId = et.ParentTransactionId;
                    obj.ThoiGian = et.ThoiGian;
                    obj.HashSignature = et.HashSignature;
                    obj.CreatedAt = et.CreatedAt;
                    obj.CreatedBy = et.CreatedBy;
                    db.GetTable<GiaoDichVi>().InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.Id;
                }
            }
            catch (Exception ex)
            {
                DalErrorLogger.Log("DAL_GiaoDichVi.ThemVaLayId", ex);
                return 0;
            }
        }

        public bool Sua(ET_GiaoDichVi et)
        {
            if (et == null) return false;
            try {
                using (var db = CreateDb())
                {
                    var obj = db.GetTable<GiaoDichVi>().FirstOrDefault(x => x.Id == et.Id);
                    if (obj != null) {
                        obj.MaCode = et.MaCode;
                        obj.IdVi = et.IdVi;
                        obj.LoaiGiaoDich = et.LoaiGiaoDich;
                        obj.SoTien = et.SoTien;
                        obj.IdDonHangLienQuan = et.IdDonHangLienQuan;
                        obj.ParentTransactionId = et.ParentTransactionId;
                        obj.ThoiGian = et.ThoiGian;
                        obj.HashSignature = et.HashSignature;
                        obj.CreatedAt = et.CreatedAt;
                        obj.CreatedBy = et.CreatedBy;
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
            } catch (Exception ex) { DalErrorLogger.Log("DAL_GiaoDichVi.Sua", ex); return false; }
        }

        public bool Xoa(int id)
        {
            try {
                using (var db = CreateDb())
                {
                    var obj = db.GetTable<GiaoDichVi>().FirstOrDefault(x => x.Id == id);
                    if (obj != null) {
                        db.GetTable<GiaoDichVi>().DeleteOnSubmit(obj);
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
            } catch (Exception ex) { DalErrorLogger.Log("DAL_GiaoDichVi.Xoa", ex); return false; }
        }

        public ET_GiaoDichVi LayTheoId(int id)
        {
            using (var db = CreateDb())
            {
                return db.GetTable<GiaoDichVi>().Where(x => x.Id == id).Select(s => new ET_GiaoDichVi {
                    Id = s.Id,
                    MaCode = s.MaCode,
                    IdVi = s.IdVi,
                    LoaiGiaoDich = s.LoaiGiaoDich,
                    SoTien = s.SoTien,
                    IdDonHangLienQuan = s.IdDonHangLienQuan,
                    ParentTransactionId = s.ParentTransactionId,
                    ThoiGian = s.ThoiGian,
                    HashSignature = s.HashSignature,
                    CreatedAt = s.CreatedAt,
                    CreatedBy = s.CreatedBy
                }).FirstOrDefault();
            }
        }
    }
}

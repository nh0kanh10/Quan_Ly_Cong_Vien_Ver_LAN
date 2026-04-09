using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_ChatLuongNuoc
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_ChatLuongNuoc instance;
        public static DAL_ChatLuongNuoc Instance
        {
            get
            {
                if (instance == null) instance = new DAL_ChatLuongNuoc();
                return instance;
            }
        }

        public List<ET_ChatLuongNuoc> LoadDS()
        {
            return db.GetTable<ChatLuongNuoc>().Select(s => new ET_ChatLuongNuoc
            {
                Id = s.Id,
                IdKhuVucBien = s.IdKhuVucBien,
                Ngay = s.Ngay,
                DoMan = s.DoMan,
                PH = s.PH,
                NhietDo = s.NhietDo,
                DoTrong = s.DoTrong,
                TrangThaiVeSinh = s.TrangThaiVeSinh
            }).ToList();
        }

        public List<ET_ChatLuongNuoc> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_ChatLuongNuoc et)
        {
            try {
                ChatLuongNuoc obj = new ChatLuongNuoc();
                obj.IdKhuVucBien = et.IdKhuVucBien;
                obj.Ngay = et.Ngay;
                obj.DoMan = et.DoMan;
                obj.PH = et.PH;
                obj.NhietDo = et.NhietDo;
                obj.DoTrong = et.DoTrong;
                obj.TrangThaiVeSinh = et.TrangThaiVeSinh;
                db.GetTable<ChatLuongNuoc>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_ChatLuongNuoc et)
        {
            try {
                var obj = db.GetTable<ChatLuongNuoc>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.IdKhuVucBien = et.IdKhuVucBien;
                    obj.Ngay = et.Ngay;
                    obj.DoMan = et.DoMan;
                    obj.PH = et.PH;
                    obj.NhietDo = et.NhietDo;
                    obj.DoTrong = et.DoTrong;
                    obj.TrangThaiVeSinh = et.TrangThaiVeSinh;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<ChatLuongNuoc>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<ChatLuongNuoc>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_ChatLuongNuoc LayTheoId(int id)
        {
            return db.GetTable<ChatLuongNuoc>().Where(x => x.Id == id).Select(s => new ET_ChatLuongNuoc {
                Id = s.Id,
                IdKhuVucBien = s.IdKhuVucBien,
                Ngay = s.Ngay,
                DoMan = s.DoMan,
                PH = s.PH,
                NhietDo = s.NhietDo,
                DoTrong = s.DoTrong,
                TrangThaiVeSinh = s.TrangThaiVeSinh
            }).FirstOrDefault();
        }
    }
}

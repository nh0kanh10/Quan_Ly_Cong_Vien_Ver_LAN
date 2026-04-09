using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_LuotVaoRaBaiXe
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_LuotVaoRaBaiXe instance;
        public static DAL_LuotVaoRaBaiXe Instance
        {
            get
            {
                if (instance == null) instance = new DAL_LuotVaoRaBaiXe();
                return instance;
            }
        }

        private ET_LuotVaoRaBaiXe MapToET(LuotVaoRaBaiXe s)
        {
            return new ET_LuotVaoRaBaiXe
            {
                Id = s.Id,
                BienSo = s.BienSo,
                LoaiXe = s.LoaiXe,
                MaRfid = s.MaRfid,
                AnhBienSo = s.AnhBienSo,
                ThoiGianVao = s.ThoiGianVao,
                ThoiGianRa = s.ThoiGianRa,
                TrangThai = s.TrangThai
            };
        }

        public List<ET_LuotVaoRaBaiXe> LoadDS()
        {
            return db.GetTable<LuotVaoRaBaiXe>().Select(s => new ET_LuotVaoRaBaiXe
            {
                Id = s.Id,
                BienSo = s.BienSo,
                LoaiXe = s.LoaiXe,
                MaRfid = s.MaRfid,
                AnhBienSo = s.AnhBienSo,
                ThoiGianVao = s.ThoiGianVao,
                ThoiGianRa = s.ThoiGianRa,
                TrangThai = s.TrangThai
            }).ToList();
        }

        public List<ET_LuotVaoRaBaiXe> GetAll()
        {
            return LoadDS();
        }

        /// <summary>
        /// Load danh sách xe đang gửi (chưa trả)
        /// </summary>
        public List<ET_LuotVaoRaBaiXe> LoadDangGui()
        {
            return db.GetTable<LuotVaoRaBaiXe>()
                .Where(x => x.TrangThai == AppConstants.TrangThaiGuiXe.DangGui)
                .OrderByDescending(x => x.ThoiGianVao)
                .Select(s => new ET_LuotVaoRaBaiXe
                {
                    Id = s.Id,
                    BienSo = s.BienSo,
                    LoaiXe = s.LoaiXe,
                    MaRfid = s.MaRfid,
                    AnhBienSo = s.AnhBienSo,
                    ThoiGianVao = s.ThoiGianVao,
                    ThoiGianRa = s.ThoiGianRa,
                    TrangThai = s.TrangThai
                }).ToList();
        }

        /// <summary>
        /// Tìm xe đang gửi theo biển số (partial match)
        /// </summary>
        public List<ET_LuotVaoRaBaiXe> TimTheoBienSo(string bienSo)
        {
            return db.GetTable<LuotVaoRaBaiXe>()
                .Where(x => x.BienSo.Contains(bienSo) && x.TrangThai == AppConstants.TrangThaiGuiXe.DangGui)
                .OrderByDescending(x => x.ThoiGianVao)
                .Select(s => new ET_LuotVaoRaBaiXe
                {
                    Id = s.Id,
                    BienSo = s.BienSo,
                    LoaiXe = s.LoaiXe,
                    MaRfid = s.MaRfid,
                    AnhBienSo = s.AnhBienSo,
                    ThoiGianVao = s.ThoiGianVao,
                    ThoiGianRa = s.ThoiGianRa,
                    TrangThai = s.TrangThai
                }).ToList();
        }

        public int ThemVaLayId(ET_LuotVaoRaBaiXe et)
        {
            try {
                LuotVaoRaBaiXe obj = new LuotVaoRaBaiXe();
                obj.BienSo = et.BienSo;
                obj.LoaiXe = et.LoaiXe;
                obj.MaRfid = et.MaRfid;
                obj.AnhBienSo = et.AnhBienSo;
                obj.ThoiGianVao = et.ThoiGianVao;
                obj.ThoiGianRa = et.ThoiGianRa;
                obj.TrangThai = et.TrangThai;
                db.GetTable<LuotVaoRaBaiXe>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return obj.Id;
            } catch { return -1; }
        }

        public bool Them(ET_LuotVaoRaBaiXe et)
        {
            return ThemVaLayId(et) > 0;
        }

        public bool Sua(ET_LuotVaoRaBaiXe et)
        {
            try {
                var obj = db.GetTable<LuotVaoRaBaiXe>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.BienSo = et.BienSo;
                    obj.LoaiXe = et.LoaiXe;
                    obj.MaRfid = et.MaRfid;
                    obj.AnhBienSo = et.AnhBienSo;
                    obj.ThoiGianVao = et.ThoiGianVao;
                    obj.ThoiGianRa = et.ThoiGianRa;
                    obj.TrangThai = et.TrangThai;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<LuotVaoRaBaiXe>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<LuotVaoRaBaiXe>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_LuotVaoRaBaiXe LayTheoId(int id)
        {
            return db.GetTable<LuotVaoRaBaiXe>().Where(x => x.Id == id).Select(s => new ET_LuotVaoRaBaiXe {
                Id = s.Id,
                BienSo = s.BienSo,
                LoaiXe = s.LoaiXe,
                MaRfid = s.MaRfid,
                AnhBienSo = s.AnhBienSo,
                ThoiGianVao = s.ThoiGianVao,
                ThoiGianRa = s.ThoiGianRa,
                TrangThai = s.TrangThai
            }).FirstOrDefault();
        }

        /// <summary>
        /// Đếm xe đang gửi theo loại (cho dashboard)
        /// </summary>
        public Dictionary<string, int> DemXeDangGuiTheoLoai()
        {
            return db.GetTable<LuotVaoRaBaiXe>()
                .Where(x => x.TrangThai == AppConstants.TrangThaiGuiXe.DangGui)
                .GroupBy(x => x.LoaiXe)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        /// <summary>
        /// Tổng doanh thu giữ xe hôm nay
        /// </summary>
        public int DemDaTraHomNay()
        {
            var today = DateTime.Today;
            return db.GetTable<LuotVaoRaBaiXe>()
                .Count(x => x.TrangThai == AppConstants.TrangThaiGuiXe.DaTra && x.ThoiGianRa >= today);
        }
    }
}

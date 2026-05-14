using System;
using System.Collections.Generic;
using System.Linq;
using ET.Models.TaiChinh;

namespace DAL.Repositories.TaiChinh
{
    public class DAL_TheRFID
    {
        #region Khởi tạo (Singleton)

        public static DAL_TheRFID Instance { get; } = new DAL_TheRFID();

        #endregion

        #region Truy vấn dữ liệu

        public List<ET_TheRFID> LayTheTheoKhach(int idKhachHang)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.TheRFIDs.Where(t => t.IdKhachHang == idKhachHang)
                    .OrderByDescending(t => t.NgayTao)
                    .Select(t => new ET_TheRFID
                    {
                        Id = t.Id,
                        MaThe = t.MaThe,
                        IdKhachHang = t.IdKhachHang,
                        IdViDienTu = t.IdViDienTu,
                        TrangThai = t.TrangThai,
                        NgayKichHoat = t.NgayKichHoat,
                        NgayHetHan = t.NgayHetHan,
                        TienCocThe = t.TienCocThe,
                        NgayTao = t.NgayTao
                    }).ToList();
            }
        }

        public ET_TheRFID TraCuuTheoMaThe(string maThe)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var t = db.TheRFIDs.FirstOrDefault(x => x.MaThe == maThe);
                if (t == null) return null;

                return new ET_TheRFID
                {
                    Id = t.Id,
                    MaThe = t.MaThe,
                    IdKhachHang = t.IdKhachHang,
                    IdViDienTu = t.IdViDienTu,
                    TrangThai = t.TrangThai,
                    NgayKichHoat = t.NgayKichHoat,
                    NgayHetHan = t.NgayHetHan,
                    TienCocThe = t.TienCocThe,
                    NgayTao = t.NgayTao
                };
            }
        }

        #endregion

        #region Thêm / Sửa / Xoá

        public int GanTheMoi(string maThe, int idKhachHang, int? idVi, decimal tienCoc)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var the = new TheRFID
                {
                    MaThe = maThe,
                    IdKhachHang = idKhachHang,
                    IdViDienTu = idVi,
                    TrangThai = ET.Constants.AppConstants.TrangThaiThe.ChuaKichHoat,
                    TienCocThe = tienCoc,
                    NgayTao = DateTime.Now
                };
                db.TheRFIDs.InsertOnSubmit(the);
                db.SubmitChanges();
                return the.Id;
            }
        }

        public void ChuyenTrangThai(int idThe, string trangThaiMoi)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var the = db.TheRFIDs.First(t => t.Id == idThe);
                the.TrangThai = trangThaiMoi;

                if (trangThaiMoi == ET.Constants.AppConstants.TrangThaiThe.DangDung && the.NgayKichHoat == null)
                    the.NgayKichHoat = DateTime.Now;

                db.SubmitChanges();
            }
        }

        #endregion

        #region Nghiệp vụ đặc thù

        public string LayTrangThaiHienTai(int idThe)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.TheRFIDs.Where(t => t.Id == idThe).Select(t => t.TrangThai).FirstOrDefault();
            }
        }

        public bool KiemTraTrungMaThe(string maThe)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.TheRFIDs.Any(t => t.MaThe == maThe);
            }
        }

        #endregion
    }
}



using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories.Kho
{
    public class DAL_Kho
    {
        public static DAL_Kho Instance { get; } = new DAL_Kho();

        #region Truy vấn dữ liệu

        public List<ET.Models.Kho.ET_KhoHang> GetAllKho(string langCode = "vi-VN")
        {
            using (var db = new DaiNamDBDataContext())
            {
                var query = db.KhoHangs.AsQueryable();

                if (langCode == "vi-VN" || string.IsNullOrEmpty(langCode))
                {
                    return query.OrderBy(k => k.MaKho).Select(k => new ET.Models.Kho.ET_KhoHang {
                        Id = k.Id,
                        MaKho = k.MaKho,
                        TenKho = k.TenKho,
                        LaKhoAo = k.LaKhoAo,
                        ChoPhepTonAm = k.ChoPhepTonAm,
                        IdKhuVuc = k.IdKhuVuc,
                        TrangThai = k.TrangThai
                    }).ToList();
                }

                var list = (from k in query
                            join bd in db.BanDiches
                            on new { Id = k.Id, Loai = "KhoHang", Cot = "TenKho", NgonNgu = langCode }
                            equals new { Id = bd.IdThucThe, Loai = bd.LoaiThucThe, Cot = bd.TruongDich, NgonNgu = bd.NgonNgu } into dich
                            from d in dich.DefaultIfEmpty()
                            orderby k.MaKho
                            select new ET.Models.Kho.ET_KhoHang {
                                Id = k.Id,
                                MaKho = k.MaKho,
                                TenKho = d != null ? d.NoiDung : k.TenKho,
                                LaKhoAo = k.LaKhoAo,
                                ChoPhepTonAm = k.ChoPhepTonAm,
                                IdKhuVuc = k.IdKhuVuc,
                                TrangThai = k.TrangThai
                            }).ToList();

                return list;
            }
        }

        /// <summary>
        /// Lấy bảng chi tiết cảnh báo tồn kho của 1 sản phẩm ở tất cả các kho.
        /// Dùng để tô màu đỏ trên màn hình nếu số lượng kho còn ít hơn mức này.
        /// </summary>
        public List<ET.Models.Kho.ET_MucTonToiThieu> GetMucTonToiThieu(int idSanPham)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.MucTonToiThieus
                    .Where(m => m.IdSanPham == idSanPham)
                    .Select(m => new ET.Models.Kho.ET_MucTonToiThieu {
                        Id = m.Id,
                        IdSanPham = m.IdSanPham,
                        IdKho = m.IdKho,
                        MucCanhBao = m.MucCanhBao,
                        SoLuongDatHang = m.SoLuongDatHang,
                        TrangThai = m.TrangThai
                    }).ToList();
            }
        }

        // Cache kho ảo Ids — tránh query liên tục cho cùng 1 mã
        private readonly Dictionary<string, int?> _cacheKhoAo = new Dictionary<string, int?>();

        /// <summary>
        /// Lấy Id kho theo mã kho (dùng để auto-resolve kho ảo như KHO_NCC, KHO_KHACH, KHO_HUY...)
        /// </summary>
        public int? GetIdKhoTheoMa(string maKho)
        {
            if (string.IsNullOrWhiteSpace(maKho)) return null;
            maKho = maKho.Trim().ToUpper();

            if (_cacheKhoAo.ContainsKey(maKho))
                return _cacheKhoAo[maKho];

            using (var db = new DaiNamDBDataContext())
            {
                var kho = db.KhoHangs.FirstOrDefault(k => k.MaKho.Trim().ToUpper() == maKho);
                int? id = kho?.Id;
                _cacheKhoAo[maKho] = id;
                return id;
            }
        }

        public ET.Models.Kho.ET_KhoHang GetById(int id)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var k = db.KhoHangs.FirstOrDefault(x => x.Id == id);
                if (k == null) return null;
                return new ET.Models.Kho.ET_KhoHang
                {
                    Id = k.Id,
                    MaKho = k.MaKho,
                    TenKho = k.TenKho,
                    LaKhoAo = k.LaKhoAo,
                    ChoPhepTonAm = k.ChoPhepTonAm,
                    IdKhuVuc = k.IdKhuVuc,
                    TrangThai = k.TrangThai
                };
            }
        }

        #endregion

        #region Thêm / Sửa / Xoá

        public void CapNhatKho(ET.Models.Kho.ET_KhoHang khoET)
        {
            using (var db = new DaiNamDBDataContext())
            {
                if (khoET.Id == 0)
                {
                    var moi = new KhoHang
                    {
                        MaKho = khoET.MaKho,
                        TenKho = khoET.TenKho,
                        LaKhoAo = khoET.LaKhoAo,
                        ChoPhepTonAm = khoET.ChoPhepTonAm,
                        IdKhuVuc = khoET.IdKhuVuc,
                        TrangThai = khoET.TrangThai
                    };
                    db.KhoHangs.InsertOnSubmit(moi);
                }
                else
                {
                    var khoDb = db.KhoHangs.FirstOrDefault(k => k.Id == khoET.Id);
                    if (khoDb != null)
                    {
                        khoDb.MaKho = khoET.MaKho;
                        khoDb.TenKho = khoET.TenKho;
                        khoDb.LaKhoAo = khoET.LaKhoAo;
                        khoDb.ChoPhepTonAm = khoET.ChoPhepTonAm;
                        khoDb.IdKhuVuc = khoET.IdKhuVuc;
                        khoDb.TrangThai = khoET.TrangThai;
                    }
                }
                db.SubmitChanges();
            }
        }

        public void CapNhatMucTon(int idSanPham, int idKho, decimal mucCanhBao)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var mucDb = db.MucTonToiThieus.FirstOrDefault(m => m.IdSanPham == idSanPham && m.IdKho == idKho);
                if (mucDb != null)
                {
                    mucDb.MucCanhBao = mucCanhBao;
                }
                else
                {
                    var moi = new MucTonToiThieu
                    {
                        IdSanPham = idSanPham,
                        IdKho = idKho,
                        MucCanhBao = mucCanhBao,
                        TrangThai = true
                    };
                    db.MucTonToiThieus.InsertOnSubmit(moi);
                }
                db.SubmitChanges();
            }
        }

        #endregion

        #region Nghiệp vụ đặc thù

        /// <summary>
        /// Kiểm tra kho này đã từng có hàng ra vào chưa.
        /// Chống lỗi nếu nhân viên ấn Xoá kho mà kho đó đang lưu lịch sử bán hàng trong DB.
        /// </summary>
        public bool KhoDaPhatSinhGiaoDich(int idKho)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.ChiTietChungTus.Any(ct => ct.IdKhoXuat == idKho || ct.IdKhoNhap == idKho);
            }
        }

        #endregion
    }
}



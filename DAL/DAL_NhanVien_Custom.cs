using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_NhanVien
    {
        public ET_NhanVien DangNhap(string username, string password)
        {
            var nv = db.GetTable<NhanVien>().FirstOrDefault(x => x.TenDangNhap == username && x.MatKhau == password && !x.IsDeleted);
            if (nv == null) return null;

            return new ET_NhanVien
            {
                Id = nv.Id,
                MaCode = nv.MaCode,
                HoTen = nv.HoTen,
                IdVaiTro = nv.IdVaiTro,
                TenDangNhap = nv.TenDangNhap,
                TrangThai = nv.TrangThai
            };
        }
    }
}

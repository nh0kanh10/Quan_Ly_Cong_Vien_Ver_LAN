using System;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_NhanVien
    {
        public ET_NhanVien DangNhap(string username, string password)
        {
            return db.GetTable<ET_NhanVien>().FirstOrDefault(x => x.TenDangNhap == username && x.MatKhau == password && !x.IsDeleted);
        }
    }
}

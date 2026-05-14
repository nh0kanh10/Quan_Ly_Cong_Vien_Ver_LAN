using System.Collections.Generic;
using System.Linq;
using DAL.Repositories.DanhMuc;
using ET.Models.DanhMuc;

namespace BUS.Services.DanhMuc
{
    public class BUS_KhuVuc
    {
        public static BUS_KhuVuc Instance { get; } = new BUS_KhuVuc();

        private readonly DAL_KhuVuc _dal = DAL_KhuVuc.Instance;

        public List<ET_KhuVuc> LayDanhSach(string langCode = "vi-VN")
        {
            return _dal.LayDanhSach(langCode).Select(k => new ET_KhuVuc
            {
                Id = k.Id,
                MaKhuVuc = k.MaKhuVuc,
                TenKhuVuc = k.TenKhuVuc,
                LoaiKhuVuc = k.LoaiKhuVuc
            }).ToList();
        }
    }
}



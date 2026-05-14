using System.Collections.Generic;
using System.Linq;
using ET.Models.HeThong;

namespace BUS.Services.HeThong
{
    public class BUS_TuDien
    {
        public static BUS_TuDien Instance { get; } = new BUS_TuDien();

        private readonly DAL.Repositories.HeThong.DAL_TuDien _dal = DAL.Repositories.HeThong.DAL_TuDien.Instance;

        public List<TuDien> LayDanhSachNhom(string nhomMa)
        {
            return _dal.LayDanhSachNhom(nhomMa).Select(t => new TuDien
            {
                NhomMa = t.NhomMa,
                Ma = t.Ma,
                NhanHienThi = t.NhanHienThi,
                ThuTu = t.ThuTu,
                MauSac = t.MauSac,
                BieuTuong = t.BieuTuong,
                ConHoatDong = (bool)(t.ConHoatDong)
            }).ToList();
        }
    }
}



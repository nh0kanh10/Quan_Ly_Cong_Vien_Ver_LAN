using System.Collections.Generic;
using DAL;
using ET;

namespace BUS
{
    public interface IDonHangGateway
    {
        List<ET_DonHang> LoadDS();
        ET_DonHang LayTheoId(int id);
        ET_DonHang LayTheoMaCode(string maCode);
        bool Them(ET_DonHang et);
        int ThemVaLayId(ET_DonHang et);
        bool Sua(ET_DonHang et);
        bool Xoa(int id);
    }

    public interface IChiTietDonHangGateway { bool Them(ET_ChiTietDonHang et); }
    public interface IPhieuThuGateway { bool Them(ET_PhieuThu et); List<ET_PhieuThu> LoadDS(); }
    public interface IPhieuChiGateway { List<ET_PhieuChi> LoadDS(); }
    public interface ITheRfidGateway { ET_TheRFID LayTheoId(string maRfid); bool Sua(ET_TheRFID et); List<ET_TheRFID> LoadDS(); }
    public interface IViDienTuGateway { ET_ViDienTu LayTheoId(int id); ET_ViDienTu LayTheoKhachHang(int idKhachHang); bool Sua(ET_ViDienTu et); List<ET_ViDienTu> LoadDS(); bool Them(ET_ViDienTu et); bool Xoa(int id); }
    public interface IGiaoDichViGateway { int ThemVaLayId(ET_GiaoDichVi et); List<ET_GiaoDichVi> LoadDS(); }
    public interface IKhachHangGateway { ET_KhachHang LayTheoId(int id); void CapNhatChiTieuVaHang(int idKhachHang, decimal soTien); }
    public interface ISanPhamGateway { List<ET_SanPham> LoadDS(); }
    public interface IComboGateway { List<ET_Combo> LoadDS(); }
    public interface IComboChiTietGateway { List<ET_ComboChiTiet> LoadDS(); }
    public interface ISanPhamVeGateway { ET_SanPham_Ve LayTheoIdSanPham(int id); }
    public interface IDatBanGateway { ET_DatBan LayTheoId(int id); List<ET_DatBan> LoadDS(); int ThemVaLayId(ET_DatBan et); bool Sua(ET_DatBan et); }
    public interface IBanAnGateway { ET_BanAn LayTheoId(int id); bool Sua(ET_BanAn et); }
    public interface IChiTietDatBanGateway { bool Them(ET_ChiTietDatBan et); List<ET_ChiTietDatBan> LoadDS(); bool Xoa(int id); }
    public interface IDoanKhachDichVuGateway { bool Them(ET_DoanKhach_DichVu et); }
    public interface ILuotVaoRaBaiXeGateway { int ThemVaLayId(ET_LuotVaoRaBaiXe et); bool Sua(ET_LuotVaoRaBaiXe et); ET_LuotVaoRaBaiXe LayTheoId(int id); List<ET_LuotVaoRaBaiXe> TimTheoBienSo(string bienSo); List<ET_LuotVaoRaBaiXe> LoadDangGui(); Dictionary<string, int> DemXeDangGuiTheoLoai(); int DemDaTraHomNay(); }
    public interface IVeDoXeChiTietGateway { bool Them(ET_VeDoXeChiTiet et); }
    public interface IGiaGuiXeGateway { ET_GiaGuiXe LayTheoLoaiXe(string loaiXe); List<ET_GiaGuiXe> LoadDS(); }
    public interface IVeDienTuGateway { int ThemVaLayId(ET_VeDienTu et); bool Sua(ET_VeDienTu et); ET_VeDienTu LayTheoId(int id); List<ET_VeDienTu> LoadDS(); List<ET_VeDienTu> LayTheoDonHang(int idDonHang); ET_VeDienTu LayTheoMaQR(string maQR); }

    internal class DefaultDonHangGateway : IDonHangGateway
    {
        public List<ET_DonHang> LoadDS() => DAL_DonHang.Instance.LoadDS();
        public ET_DonHang LayTheoId(int id) => DAL_DonHang.Instance.LayTheoId(id);
        public ET_DonHang LayTheoMaCode(string maCode) => DAL_DonHang.Instance.LayTheoMaCode(maCode);
        public bool Them(ET_DonHang et) => DAL_DonHang.Instance.Them(et);
        public int ThemVaLayId(ET_DonHang et) => DAL_DonHang.Instance.ThemVaLayId(et);
        public bool Sua(ET_DonHang et) => DAL_DonHang.Instance.Sua(et);
        public bool Xoa(int id) => DAL_DonHang.Instance.Xoa(id);
    }

    internal class DefaultChiTietDonHangGateway : IChiTietDonHangGateway { public bool Them(ET_ChiTietDonHang et) => DAL_ChiTietDonHang.Instance.Them(et); }
    internal class DefaultPhieuThuGateway : IPhieuThuGateway { public bool Them(ET_PhieuThu et) => DAL_PhieuThu.Instance.Them(et); public List<ET_PhieuThu> LoadDS() => DAL_PhieuThu.Instance.LoadDS(); }
    internal class DefaultPhieuChiGateway : IPhieuChiGateway { public List<ET_PhieuChi> LoadDS() => DAL_PhieuChi.Instance.LoadDS(); }
    internal class DefaultTheRfidGateway : ITheRfidGateway { public ET_TheRFID LayTheoId(string maRfid) => DAL_TheRFID.Instance.LayTheoId(maRfid); public bool Sua(ET_TheRFID et) => DAL_TheRFID.Instance.Sua(et); public List<ET_TheRFID> LoadDS() => DAL_TheRFID.Instance.LoadDS(); }
    internal class DefaultViDienTuGateway : IViDienTuGateway
    {
        public ET_ViDienTu LayTheoId(int id) => DAL_ViDienTu.Instance.LayTheoId(id);
        public ET_ViDienTu LayTheoKhachHang(int idKhachHang) => DAL_ViDienTu.Instance.LayTheoKhachHang(idKhachHang);
        public bool Sua(ET_ViDienTu et) => DAL_ViDienTu.Instance.Sua(et);
        public List<ET_ViDienTu> LoadDS() => DAL_ViDienTu.Instance.LoadDS();
        public bool Them(ET_ViDienTu et) => DAL_ViDienTu.Instance.Them(et);
        public bool Xoa(int id) => DAL_ViDienTu.Instance.Xoa(id);
    }
    internal class DefaultGiaoDichViGateway : IGiaoDichViGateway { public int ThemVaLayId(ET_GiaoDichVi et) => DAL_GiaoDichVi.Instance.ThemVaLayId(et); public List<ET_GiaoDichVi> LoadDS() => DAL_GiaoDichVi.Instance.LoadDS(); }
    internal class DefaultKhachHangGateway : IKhachHangGateway { public ET_KhachHang LayTheoId(int id) => DAL_KhachHang.Instance.LayTheoId(id); public void CapNhatChiTieuVaHang(int idKhachHang, decimal soTien) => DAL_KhachHang.Instance.CapNhatChiTieuVaHang(idKhachHang, soTien); }
    internal class DefaultSanPhamGateway : ISanPhamGateway { public List<ET_SanPham> LoadDS() => DAL_SanPham.Instance.LoadDS(); }
    internal class DefaultComboGateway : IComboGateway { public List<ET_Combo> LoadDS() => DAL_Combo.Instance.LoadDS(); }
    internal class DefaultComboChiTietGateway : IComboChiTietGateway { public List<ET_ComboChiTiet> LoadDS() => DAL_ComboChiTiet.Instance.LoadDS(); }
    internal class DefaultSanPhamVeGateway : ISanPhamVeGateway { public ET_SanPham_Ve LayTheoIdSanPham(int id) => DAL_SanPham_Ve.Instance.LayTheoIdSanPham(id); }
    internal class DefaultDatBanGateway : IDatBanGateway { public ET_DatBan LayTheoId(int id) => DAL_DatBan.Instance.LayTheoId(id); public List<ET_DatBan> LoadDS() => DAL_DatBan.Instance.LoadDS(); public int ThemVaLayId(ET_DatBan et) => DAL_DatBan.Instance.ThemVaLayId(et); public bool Sua(ET_DatBan et) => DAL_DatBan.Instance.Sua(et); }
    internal class DefaultBanAnGateway : IBanAnGateway { public ET_BanAn LayTheoId(int id) => DAL_BanAn.Instance.LayTheoId(id); public bool Sua(ET_BanAn et) => DAL_BanAn.Instance.Sua(et); }
    internal class DefaultChiTietDatBanGateway : IChiTietDatBanGateway { public bool Them(ET_ChiTietDatBan et) => DAL_ChiTietDatBan.Instance.Them(et); public List<ET_ChiTietDatBan> LoadDS() => DAL_ChiTietDatBan.Instance.LoadDS(); public bool Xoa(int id) => DAL_ChiTietDatBan.Instance.Xoa(id); }
    internal class DefaultDoanKhachDichVuGateway : IDoanKhachDichVuGateway { public bool Them(ET_DoanKhach_DichVu et) => DAL_DoanKhach_DichVu.Instance.Them(et); }
    internal class DefaultLuotVaoRaBaiXeGateway : ILuotVaoRaBaiXeGateway { public int ThemVaLayId(ET_LuotVaoRaBaiXe et) => DAL_LuotVaoRaBaiXe.Instance.ThemVaLayId(et); public bool Sua(ET_LuotVaoRaBaiXe et) => DAL_LuotVaoRaBaiXe.Instance.Sua(et); public ET_LuotVaoRaBaiXe LayTheoId(int id) => DAL_LuotVaoRaBaiXe.Instance.LayTheoId(id); public List<ET_LuotVaoRaBaiXe> TimTheoBienSo(string bienSo) => DAL_LuotVaoRaBaiXe.Instance.TimTheoBienSo(bienSo); public List<ET_LuotVaoRaBaiXe> LoadDangGui() => DAL_LuotVaoRaBaiXe.Instance.LoadDangGui(); public Dictionary<string, int> DemXeDangGuiTheoLoai() => DAL_LuotVaoRaBaiXe.Instance.DemXeDangGuiTheoLoai(); public int DemDaTraHomNay() => DAL_LuotVaoRaBaiXe.Instance.DemDaTraHomNay(); }
    internal class DefaultVeDoXeChiTietGateway : IVeDoXeChiTietGateway { public bool Them(ET_VeDoXeChiTiet et) => DAL_VeDoXeChiTiet.Instance.Them(et); }
    internal class DefaultGiaGuiXeGateway : IGiaGuiXeGateway { public ET_GiaGuiXe LayTheoLoaiXe(string loaiXe) => DAL_GiaGuiXe.Instance.LayTheoLoaiXe(loaiXe); public List<ET_GiaGuiXe> LoadDS() => DAL_GiaGuiXe.Instance.LoadDS(); }
    internal class DefaultVeDienTuGateway : IVeDienTuGateway { public int ThemVaLayId(ET_VeDienTu et) => DAL_VeDienTu.Instance.ThemVaLayId(et); public bool Sua(ET_VeDienTu et) => DAL_VeDienTu.Instance.Sua(et); public ET_VeDienTu LayTheoId(int id) => DAL_VeDienTu.Instance.LayTheoId(id); public List<ET_VeDienTu> LoadDS() => DAL_VeDienTu.Instance.LoadDS(); public List<ET_VeDienTu> LayTheoDonHang(int idDonHang) => DAL_VeDienTu.Instance.LayTheoDonHang(idDonHang); public ET_VeDienTu LayTheoMaQR(string maQR) => DAL_VeDienTu.Instance.LayTheoMaQR(maQR); }
}

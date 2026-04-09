using System;
using System.Collections.Generic;
using System.Linq;
using ET;
using DAL;

namespace BUS
{
    public class BUS_BanAn
    {
        private static BUS_BanAn instance;
        public static BUS_BanAn Instance
        {
            get
            {
                if (instance == null) instance = new BUS_BanAn();
                return instance;
            }
        }

        #region Thao Tác Truy Xuất Dữ Liệu

        public List<ET_BanAn> LoadDS()
        {
            return DAL_BanAn.Instance.LoadDS();
        }

        public List<ET_BanAn> LoadTheoNhaHang(int idNhaHang)
        {
            return DAL_BanAn.Instance.LoadDS().Where(x => x.IdNhaHang == idNhaHang).ToList();
        }

        public ET_BanAn LayTheoId(int id)
        {
            return DAL_BanAn.Instance.LayTheoId(id);
        }
        #endregion

        #region Quy Trình Cập Nhật Thông Tin Bàn Ăn

        public OperationResult Them(ET_BanAn et)
        {
            if (et == null || string.IsNullOrWhiteSpace(et.MaBan))
                return OperationResult.Failed("Mã bàn không được trống.");
            bool ok = DAL_BanAn.Instance.Them(et);
            return ok ? OperationResult.Success() : OperationResult.Failed("Không thể thêm bàn ăn.");
        }

        public OperationResult Sua(ET_BanAn et)
        {
            if (et == null || et.Id <= 0)
                return OperationResult.Failed("Dữ liệu không hợp lệ.");
            bool ok = DAL_BanAn.Instance.Sua(et);
            return ok ? OperationResult.Success() : OperationResult.Failed("Không thể cập nhật bàn ăn.");
        }

        public OperationResult Xoa(int id)
        {
            bool ok = DAL_BanAn.Instance.Xoa(id);
            return ok ? OperationResult.Success() : OperationResult.Failed("Không thể xóa bàn ăn.");
        }

        public OperationResult CapNhatTrangThai(int idBan, string trangThai)
        {
            var ban = DAL_BanAn.Instance.LayTheoId(idBan);
            if (ban == null) return OperationResult.Failed("Hệ thống từ chối cập nhật: Bàn ăn không tồn tại.");
            ban.TrangThai = trangThai;
            bool ok = DAL_BanAn.Instance.Sua(ban);
            return ok ? OperationResult.Success() : OperationResult.Failed("Lỗi ngoại lệ: Không thể cập nhật trạng thái bàn.");
        }
        #endregion
    }
}

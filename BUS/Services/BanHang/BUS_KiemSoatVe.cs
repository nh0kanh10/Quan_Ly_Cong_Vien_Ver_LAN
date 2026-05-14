using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Repositories.BanHang;
using DAL.Repositories.DanhMuc;
using ET.Constants;
using ET.Models.BanHang;
using ET.Models.DanhMuc;
using ET.Results;
using BUS.Services.DanhMuc;

namespace BUS.Services.BanHang
{
    public class BUS_KiemSoatVe
    {
        #region Khởi tạo (Singleton)

        public static BUS_KiemSoatVe Instance { get; } = new BUS_KiemSoatVe();

        #endregion

        #region Truy vấn dữ liệu

        public List<ET_KhuVuc> LayDanhSachKhuVuc()
        {
            return BUS_KhuVuc.Instance.LayDanhSach();
        }

        public List<ET_TroChoi> LayDanhSachTroChoi(int? idKhuVuc)
        {
            try
            {
                return DAL_KiemSoatVe.Instance.LayDanhSachTroChoi(idKhuVuc);
            }
            catch
            {
                return new List<ET_TroChoi>();
            }
        }

        public List<ET_LichSuQuet> LayLichSu(int? idKhuVuc)
        {
            try
            {
                return DAL_KiemSoatVe.Instance.LayLichSuQuetHomNay(idKhuVuc);
            }
            catch
            {
                return new List<ET_LichSuQuet>();
            }
        }

        #endregion

        #region Nghiệp vụ đặc thù

        // Logic soát vé 6 trạng thái:
        // 0=HopLe (mời vào), 1=SaiKhuVuc, 2=HetLuot, 3=KhongTimThay, 4=SaiTroChoi, 5=HetHan, 6=DaHuy
        public OperationResult<ET_KetQuaSoatVe> SoatVe(string maVach, int? idKhuVuc, int? idTroChoi, int? idThietBi)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(maVach))
                    return OperationResult<ET_KetQuaSoatVe>.Fail("ERR_GATE_MA_RONG");

                var ve = DAL_KiemSoatVe.Instance.TimVeTheoMaVach(maVach.Trim());

                // Mã 3: Không tìm thấy
                if (ve == null)
                {
                    return OperationResult<ET_KetQuaSoatVe>.Ok(new ET_KetQuaSoatVe
                    {
                        MaKetQua = 3,
                        ThongBaoKey = "GATE_KHONG_TIM_THAY"
                    });
                }

                // Mã 6: Đã hủy
                if (ve.TrangThai == AppConstants.TrangThaiVe.DaHuy)
                {
                    return OperationResult<ET_KetQuaSoatVe>.Ok(new ET_KetQuaSoatVe
                    {
                        MaKetQua = 6,
                        VeInfo = ve,
                        ThongBaoKey = "GATE_VE_DA_HUY"
                    });
                }

                // Mã 5: Hết hạn
                if (ve.TrangThai == AppConstants.TrangThaiVe.HetHan ||
                    (ve.NgayHetHan.HasValue && ve.NgayHetHan.Value < DateTime.Today))
                {
                    return OperationResult<ET_KetQuaSoatVe>.Ok(new ET_KetQuaSoatVe
                    {
                        MaKetQua = 5,
                        VeInfo = ve,
                        ThongBaoKey = "GATE_VE_HET_HAN"
                    });
                }

                // Mã 2: Hết lượt
                if (ve.SoLuotConLai <= 0)
                {
                    return OperationResult<ET_KetQuaSoatVe>.Ok(new ET_KetQuaSoatVe
                    {
                        MaKetQua = 2,
                        VeInfo = ve,
                        ThongBaoKey = "GATE_HET_LUOT"
                    });
                }

                // Kiểm tra quyền truy cập cổng (Khu vực / Trò chơi)
                var dsQuyen = DAL_KiemSoatVe.Instance.LayQuyenTruyCap(ve.IdSanPham);

                if (dsQuyen.Any() && idKhuVuc.HasValue)
                {
                    var quyenKhuVuc = dsQuyen.Where(q => q.IdKhuVuc == idKhuVuc.Value).ToList();

                    // Mã 1: Sai khu vực
                    if (!quyenKhuVuc.Any())
                    {
                        DAL_KiemSoatVe.Instance.TruLuotVaGhiLog(ve.Id,
                            dsQuyen.First().Id, idThietBi, AppConstants.KetQuaQuetVe.SaiVe);

                        return OperationResult<ET_KetQuaSoatVe>.Ok(new ET_KetQuaSoatVe
                        {
                            MaKetQua = 1,
                            VeInfo = ve,
                            ThongBaoKey = "GATE_SAI_KHU_VUC"
                        });
                    }

                    // Mã 4: Sai trò chơi (nếu cổng quét yêu cầu trò chơi cụ thể)
                    if (idTroChoi.HasValue)
                    {
                        var quyenTroChoi = quyenKhuVuc.FirstOrDefault(q =>
                            q.IdTroChoi == null || q.IdTroChoi == idTroChoi.Value);

                        if (quyenTroChoi == null)
                        {
                            DAL_KiemSoatVe.Instance.TruLuotVaGhiLog(ve.Id,
                                quyenKhuVuc.First().Id, idThietBi, AppConstants.KetQuaQuetVe.SaiVe);

                            return OperationResult<ET_KetQuaSoatVe>.Ok(new ET_KetQuaSoatVe
                            {
                                MaKetQua = 4,
                                VeInfo = ve,
                                ThongBaoKey = "GATE_SAI_TRO_CHOI"
                            });
                        }
                    }
                }

                // Mã 0: HỢP LỆ -> Trừ lượt + ghi log thành công
                var quyenDung = dsQuyen.FirstOrDefault(q =>
                    (!idKhuVuc.HasValue || q.IdKhuVuc == idKhuVuc.Value) &&
                    (!idTroChoi.HasValue || q.IdTroChoi == null || q.IdTroChoi == idTroChoi.Value))
                    ?? dsQuyen.FirstOrDefault();

                int idQuyen = quyenDung?.Id ?? 0;
                if (idQuyen > 0)
                {
                    DAL_KiemSoatVe.Instance.TruLuotVaGhiLog(ve.Id, idQuyen, idThietBi,
                        AppConstants.KetQuaQuetVe.ThanhCong);
                }

                ve.SoLuotConLai = Math.Max(0, ve.SoLuotConLai - 1);

                return OperationResult<ET_KetQuaSoatVe>.Ok(new ET_KetQuaSoatVe
                {
                    MaKetQua = 0,
                    VeInfo = ve,
                    ThongBaoKey = "GATE_HOP_LE"
                });
            }
            catch (Exception ex)
            {
                return OperationResult<ET_KetQuaSoatVe>.Fail(ex.Message);
            }
        }

        #endregion
    }
}



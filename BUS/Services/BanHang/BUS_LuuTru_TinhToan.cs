using System;
using System.Linq;
using ET.DTOs;
using ET.Constants;
using ET.Models.DanhMuc;
using ET.Models.BanHang;
using ET.Results;
using DAL;

namespace BUS.Services.BanHang
{
    public class BUS_LuuTru_TinhToan
    {
        public static BUS_LuuTru_TinhToan Instance { get; } = new BUS_LuuTru_TinhToan();

        private BUS_LuuTru_TinhToan() { }

        public OperationResult<decimal> TinhTienPhongVaPhatLoGio(int idChiTietDatPhong, out decimal tienPhat, out string ghiChuPhat)
        {
            tienPhat = 0;
            ghiChuPhat = "";

            try
            {
                using (var db = new DaiNamDBDataContext())
                {
                    var ct = db.ChiTietDatPhongs.FirstOrDefault(x => x.Id == idChiTietDatPhong);
                    if (ct == null) return OperationResult<decimal>.Fail("Không tìm thấy chi tiết phòng");

                    // 1. Tính giá phòng gốc mỗi đêm
                    decimal giaMoiDem = ct.GiaBanDem;
                    if (giaMoiDem <= 0)
                    {
                        giaMoiDem = BUS.Services.DanhMuc.BUS_BangGia.Instance.GetDynamicPrice(ct.IdLoaiPhong, DateTime.Now, out _);
                    }

                    // 2. Tính tổng tiền phòng = số đêm × giá/đêm
                    // Ưu tiên lấy từ ChiTietDonHang (nguồn chính xác nhất, đã bao gồm gia hạn)
                    decimal tongTienPhong = giaMoiDem;
                    if (ct.IdChiTietDonHang > 0)
                    {
                        var ctDH = db.ChiTietDonHangs.FirstOrDefault(x => x.Id == ct.IdChiTietDonHang);
                        if (ctDH != null)
                            tongTienPhong = ctDH.SoLuong * ctDH.DonGiaThucTe;
                    }
                    else if (ct.NgayCheckIn.HasValue && ct.NgayCheckOut.HasValue)
                    {
                        int soNgay = (ct.NgayCheckOut.Value.Date - ct.NgayCheckIn.Value.Date).Days;
                        if (soNgay <= 0) soNgay = 1;
                        tongTienPhong = giaMoiDem * soNgay;
                    }

                    // 3. Tính tiền phạt lố giờ (dựa trên giá 1 đêm)
                    if (ct.NgayCheckOut.HasValue && DateTime.Now > ct.NgayCheckOut.Value)
                    {
                        TimeSpan treGio = DateTime.Now - ct.NgayCheckOut.Value;
                        int soGioTre = (int)Math.Ceiling(treGio.TotalHours);

                        if (soGioTre > 0)
                        {
                            decimal phanTramPhatMoiGio = BUS_CauHinh.Instance.LayGiaTriDecimal(AppConstants.ConfigKeys.PHAT_LO_GIO_PHAN_TRAM_MOI_GIO, 10);
                            decimal tyLePhat = Math.Min(soGioTre * (phanTramPhatMoiGio / 100m), 1.0m);
                            tienPhat = giaMoiDem * tyLePhat;
                            
                            var phong = db.Phongs.FirstOrDefault(p => p.Id == ct.IdPhong);
                            string tenPhong = phong != null ? phong.MaPhong : "";
                            
                            ghiChuPhat = $"Phạt lố giờ: {soGioTre}h (Phòng {tenPhong})";
                        }
                    }

                    return OperationResult<decimal>.Ok(tongTienPhong);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<decimal>.Fail(ex.Message);
            }
        }
    }
}



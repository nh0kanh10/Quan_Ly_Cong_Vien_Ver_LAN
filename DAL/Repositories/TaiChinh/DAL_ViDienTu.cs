using System;
using System.Collections.Generic;
using System.Linq;
using ET.Models.TaiChinh;

namespace DAL.Repositories.TaiChinh
{
    public class DAL_ViDienTu
    {
        #region Khởi tạo (Singleton)

        public static DAL_ViDienTu Instance { get; } = new DAL_ViDienTu();

        #endregion

        #region Truy vấn dữ liệu

        public ET_ViDienTu LayViTheoKhach(int idKhachHang)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var vi = db.ViDienTus.FirstOrDefault(v => v.IdKhachHang == idKhachHang);
                if (vi == null) return null;

                return new ET_ViDienTu
                {
                    Id = vi.Id,
                    IdKhachHang = vi.IdKhachHang,
                    MaVi = vi.MaVi,
                    ConHoatDong = vi.ConHoatDong,
                    NgayTao = vi.NgayTao
                };
            }
        }

        /// <summary>
        /// Tính số dư ví theo công thức: Tổng(Cộng + Nạp) - Tổng(Trừ).
        /// Phải tách theo LoaiPhep vì SoTien luôn dương (CHECK > 0 trong DB).
        /// </summary>
        public decimal LaySoDu(int idVi)
        {
            using (var db = new DaiNamDBDataContext())
            {
                decimal tongCong = db.SoCaiVis
                    .Where(s => s.IdVi == idVi && s.LoaiPhep != ET.Constants.AppConstants.LoaiPhepVi.Tru)
                    .Sum(s => (decimal?)s.SoTien) ?? 0;

                decimal tongTru = db.SoCaiVis
                    .Where(s => s.IdVi == idVi && s.LoaiPhep == ET.Constants.AppConstants.LoaiPhepVi.Tru)
                    .Sum(s => (decimal?)s.SoTien) ?? 0;

                return tongCong - tongTru;
            }
        }

        public List<ET_LichSuGiaoDich> LayLichSuVi(int idKhachHang)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var vi = db.ViDienTus.FirstOrDefault(v => v.IdKhachHang == idKhachHang);
                if (vi == null) return new List<ET_LichSuGiaoDich>();

                var rawData = db.SoCaiVis.Where(s => s.IdVi == vi.Id)
                    .OrderByDescending(s => s.NgayTao)
                    .Select(s => new { s.Id, s.SoTien, s.LoaiPhep, s.NgayTao })
                    .ToList();

                return rawData.Select(s => new ET_LichSuGiaoDich
                {
                    MaGiaoDich = "SCV" + s.Id.ToString("D6"),
                    NhomGiaoDich = "Vi",
                    SoTien = s.SoTien,
                    LoaiGiaoDich = s.LoaiPhep,
                    ThoiGian = s.NgayTao
                }).ToList();
            }
        }

        #endregion

        #region Thêm / Sửa / Xoá

        public int TaoVi(int idKhachHang, string maVi)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var vi = new ViDienTu
                {
                    IdKhachHang = idKhachHang,
                    MaVi = maVi,
                    ConHoatDong = true,
                    NgayTao = DateTime.Now
                };
                db.ViDienTus.InsertOnSubmit(vi);
                db.SubmitChanges();
                return vi.Id;
            }
        }

        #endregion

        #region Nghiệp vụ đặc thù

        /// <summary>
        /// Nạp tiền vào ví: insert 1 dòng SoCaiVi (ghi Có) + insert 1 ChungTuTC (chứng từ thu).
        /// Cả 2 trong 1 transaction để đảm bảo toàn vẹn tài chính.
        /// </summary>
        public void NapTien(int idVi, decimal soTien, string phuongThuc, int idNguoiTao)
        {
            using (var db = new DaiNamDBDataContext())
            {
                using (var tx = new System.Transactions.TransactionScope())
                {
                    var soCai = new SoCaiVi
                    {
                        IdVi = idVi,
                        LoaiPhep = ET.Constants.AppConstants.LoaiPhepVi.Nap,
                        SoTien = soTien,
                        MoTa = phuongThuc,
                        NgayTao = DateTime.Now,
                        NguoiTao = idNguoiTao
                    };
                    db.SoCaiVis.InsertOnSubmit(soCai);

                    string maChungTu = "CT" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
                        + new Random().Next(10, 99).ToString();

                    var chungTu = new ChungTuTC
                    {
                        MaChungTu = maChungTu,
                        LoaiChungTu = ET.Constants.AppConstants.LoaiChungTuTC.THU_NAP_VI,
                        MaGiaoDichClient = Guid.NewGuid(),
                        SoTien = soTien,
                        PhuongThuc = phuongThuc,
                        NgayChungTu = DateTime.Now,
                        IdNguoiTao = idNguoiTao,
                        MoTa = "Nạp ví điện tử",
                        NgayTao = DateTime.Now
                    };
                    db.ChungTuTCs.InsertOnSubmit(chungTu);

                    db.SubmitChanges();
                    tx.Complete();
                }
            }
        }

        #endregion
    }
}



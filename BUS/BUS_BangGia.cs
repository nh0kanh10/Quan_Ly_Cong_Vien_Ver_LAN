using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace BUS
{
    public class BUS_BangGia
    {
        private readonly IBangGiaGateway _gateway;
        private readonly ISanPhamGateway _sanPhamGateway;
        private readonly ICauHinhNgayLeGateway _ngayLeGateway;
        private readonly IQuyDoiDonViGateway _quyDoiGateway;

        private static BUS_BangGia instance;
        public static BUS_BangGia Instance
        {
            get
            {
                if (instance == null) instance = new BUS_BangGia();
                return instance;
            }
        }

        private BUS_BangGia() : this(new DefaultBangGiaGateway(), new DefaultSanPhamGateway(), new DefaultCauHinhNgayLeGateway(), new DefaultQuyDoiDonViGateway()) { }
        public BUS_BangGia(IBangGiaGateway gw, ISanPhamGateway spGw, ICauHinhNgayLeGateway nlGw, IQuyDoiDonViGateway qdGw)
        {
            _gateway = gw;
            _sanPhamGateway = spGw;
            _ngayLeGateway = nlGw;
            _quyDoiGateway = qdGw;
        }

        // 
        //  ENGINE 1: LẤY GIÁ BÁN (POS, Vé, F&B)
        //  Input: idSanPham + thời điểm
        //  Output: 1 con số giá duy nhất
        // 
        public decimal GetDynamicPrice(int idSanPham, DateTime thoiDiem)
        {
            var bg = TimBangGia(idSanPham, thoiDiem);
            if (bg == null)
            {
                // Fallback về DonGia gốc của SanPham
                var sp = _sanPhamGateway.LayTheoId(idSanPham);
                return sp?.DonGia ?? 0m;
            }
            return ChonGiaTheoNgay(bg, thoiDiem);
        }

        /// <summary>
        /// Lấy Tiền Cọc của sản phẩm (trả 0 nếu không cần cọc)
        /// </summary>
        public decimal GetTienCoc(int idSanPham)
        {
            var list = _gateway.LayTheoSanPham(idSanPham);
            var bg = list.FirstOrDefault(x => x.TienCoc.HasValue);
            return bg?.TienCoc ?? 0m;
        }

        // ═══════════════════════════════════════════════
        //  ENGINE 2: TÍNH TIỀN THUÊ THEO PHÚT (Block)
        //  Input: idSanPham + thời điểm bắt đầu + tổng phút
        //  Output: tổng tiền thuê
        // ═══════════════════════════════════════════════
        public decimal TinhTienThueTheoPhut(int idSanPham, DateTime thoiDiem, int tongSoPhutThue)
        {
            var bg = TimBangGia(idSanPham, thoiDiem);
            if (bg == null) return 0m;

            decimal giaBlock1 = ChonGiaTheoNgay(bg, thoiDiem);
            int phutBlock1 = bg.PhutBlock ?? 60;

            if (tongSoPhutThue <= phutBlock1)
                return giaBlock1; // Nằm trong block đầu, tính giá gốc

            // Tính phụ thu lố giờ
            int phutLo = tongSoPhutThue - phutBlock1;
            int phutTiep = bg.PhutTiep ?? 30;
            decimal giaPhuThu = bg.GiaPhuThu ?? 0m;

            int soBlockLo = (int)Math.Ceiling((double)phutLo / phutTiep);
            return giaBlock1 + (soBlockLo * giaPhuThu);
        }

        // ═══════════════════════════════════════════════
        //  CORE: Tìm dòng BangGia phù hợp nhất
        // ═══════════════════════════════════════════════
        private ET_BangGia TimBangGia(int idSanPham, DateTime thoiDiem)
        {
            var gio = thoiDiem.TimeOfDay;
            var list = _gateway.LayGiaHienTai(idSanPham, gio);
            return list.FirstOrDefault(); // Đã sort khung hẹp ưu tiên trong DAL
        }

        /// <summary>
        /// Chọn 1 trong 3 cột giá dựa theo ngày (Lễ > Cuối tuần > Thường)
        /// </summary>
        private decimal ChonGiaTheoNgay(ET_BangGia bg, DateTime thoiDiem)
        {
            // Ưu tiên 1: Ngày lễ
            if (_ngayLeGateway.LaNgayLe(thoiDiem))
                return bg.GiaNgayLe;

            // Ưu tiên 2: Cuối tuần (T7, CN)
            if (thoiDiem.DayOfWeek == DayOfWeek.Saturday || thoiDiem.DayOfWeek == DayOfWeek.Sunday)
                return bg.GiaCuoiTuan;

            // Mặc định: Ngày thường
            return bg.GiaNgayThuong;
        }

        // ═══════════════════════════════════════════════
        //  CRUD cho UI (frmSanPham Tab BảngGiá)
        // ═══════════════════════════════════════════════
        public List<ET_BangGia> LayGiaTheoSP(int idSanPham) => _gateway.LayTheoSanPham(idSanPham);

        /// <summary>
        /// Tính giá bán khi Thu ngân chọn ĐVT cụ thể trong giỏ hàng.
        /// - DVT cơ bản → GetDynamicPrice như cũ
        /// - DVT lớn có GiaBanRieng → dùng giá ưu đãi đó
        /// - DVT lớn không có GiaBanRieng → TyLeQuyDoi × giá lẻ (không ưu đãi)
        /// </summary>
        public decimal GetPriceByUnit(int idSanPham, int idDVTChon, DateTime thoiDiem)
        {
            // Tìm dòng quy đổi có IdDonViLon = idDVTChon
            var quyDoi = _quyDoiGateway.LoadDS()
                .FirstOrDefault(x => x.IdSanPham == idSanPham && x.IdDonViLon == idDVTChon);

            if (quyDoi == null)
                // Không tìm thấy quy đổi → trả về giá lẻ bình thường
                return GetDynamicPrice(idSanPham, thoiDiem);

            // Ưu tiên GiaBanRieng nếu đã cấu hình
            if (quyDoi.GiaBanRieng.HasValue && quyDoi.GiaBanRieng.Value > 0)
                return quyDoi.GiaBanRieng.Value;

            // Fallback: tính theo tỉ lệ × giá lẻ
            decimal giaLe = GetDynamicPrice(idSanPham, thoiDiem);
            return Math.Round(giaLe * quyDoi.TyLeQuyDoi, 0);
        }

        public ResponseResult ThemGia(ET_BangGia et)
        {
            if (et.GiaNgayThuong < 0 || et.GiaCuoiTuan < 0 || et.GiaNgayLe < 0)
                return ResponseResult.Error("Giá tiền không được âm.");
            et.CreatedAt = DateTime.Now;
            try
            {
                bool ok = _gateway.Them(et);
                return ok ? ResponseResult.Success() : ResponseResult.Error("Không thể thêm mức giá.");
            }
            catch (Exception ex) when (ex.InnerException?.Message?.Contains("UxBangGia_ActiveSPGio") == true
                                    || ex.Message.Contains("UxBangGia_ActiveSPGio")
                                    || ex.Message.Contains("duplicate key"))
            {
                return ResponseResult.Error("Đã tồn tại mức giá active cho sản phẩm này với cùng khung giờ.");
            }
        }

        public ResponseResult SuaGia(ET_BangGia et)
        {
            bool ok = _gateway.Sua(et);
            return ok ? ResponseResult.Success() : ResponseResult.Error("Không thể cập nhật.");
        }

        public ResponseResult XoaGia(int id)
        {
            bool ok = _gateway.Xoa(id);
            return ok ? ResponseResult.Success() : ResponseResult.Error("Không thể xóa.");
        }
    }
}

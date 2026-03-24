using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace BUS
{
    /// <summary>
    /// Engine tích điểm / tiêu điểm Loyalty.
    /// Công thức: SoDiem = floor(TongTien / DonViTich) * HeSoLoaiKhach
    /// Quy đổi: 1 điểm = 1,000 VND
    /// Giới hạn tiêu: tối đa 50% giá trị đơn hàng
    /// </summary>
    public class BUS_TichDiem
    {
        private readonly IQuyTacDiemGateway _quyTacDiemGateway;
        private readonly IKhachHangGateway _khachHangGateway;
        private readonly ILichSuDiemGateway _lichSuDiemGateway;

        private static BUS_TichDiem instance;
        public static BUS_TichDiem Instance
        {
            get
            {
                if (instance == null) instance = new BUS_TichDiem();
                return instance;
            }
        }

        public BUS_TichDiem() : this(new DefaultQuyTacDiemGateway(), new DefaultKhachHangGateway(), new DefaultLichSuDiemGateway()) { }
        public BUS_TichDiem(IQuyTacDiemGateway qtdGw, IKhachHangGateway khGw, ILichSuDiemGateway lsdGw)
        {
            _quyTacDiemGateway = qtdGw;
            _khachHangGateway = khGw;
            _lichSuDiemGateway = lsdGw;
        }

        // === CONSTANTS ===
        private const decimal GIA_TRI_MOT_DIEM = 1000m;   // 1 điểm = 1,000 VND
        private const decimal TY_LE_TIEU_TOI_DA = 0.50m;  // Tối đa 50% đơn hàng
        private const decimal DON_VI_TICH = 100000m;       // Mỗi 100k = 1 đơn vị điểm

        // Hệ số nhân theo loại khách — O(1) lookup
        private static readonly Dictionary<string, decimal> HE_SO_LOAI_KHACH = new Dictionary<string, decimal>
        {
            { AppConstants.LoaiKhachHang.CaNhan,          1.0m },
            { AppConstants.LoaiKhachHang.HocSinhSinhVien,  1.0m },
            { AppConstants.LoaiKhachHang.Vip,             2.0m },
            { AppConstants.LoaiKhachHang.VVIP,            3.0m },
            { AppConstants.LoaiKhachHang.Doan,            0.5m },
            { AppConstants.LoaiKhachHang.DoanhNghiep,     1.5m },
            { AppConstants.LoaiKhachHang.NoiBo,           0.0m }  // Nhân viên không tích điểm
        };

        #region Tính toán điểm

        /// <summary>
        /// Tính số điểm thưởng dựa trên tổng đơn + loại khách.
        /// Công thức: floor(tongTien / 100,000) * hệ số
        /// </summary>
        public int TinhDiemThuong(decimal tongTien, string loaiKhach)
        {
            if (tongTien <= 0 || string.IsNullOrEmpty(loaiKhach)) return 0;

            decimal heSo;
            if (!HE_SO_LOAI_KHACH.TryGetValue(loaiKhach, out heSo) || heSo <= 0) return 0;

            // Kiểm tra đơn tối thiểu theo QuyTacDiem (nếu có)
            decimal donToiThieu = DON_VI_TICH;
            var quyTac = _quyTacDiemGateway.LayQuyTacActive()
                .FirstOrDefault(x => x.LoaiKhachApDung == loaiKhach);
            if (quyTac != null)
            {
                donToiThieu = quyTac.TongDonToiThieu;
                // Nếu QuyTacDiem có SoDiemThuong custom thì dùng
                if (quyTac.SoDiemThuong > 0)
                    heSo = quyTac.SoDiemThuong; // Override hệ số từ DB
            }

            if (tongTien < donToiThieu) return 0;

            int soDiem = (int)Math.Floor(tongTien / DON_VI_TICH * heSo);
            return Math.Max(soDiem, 0);
        }

        /// <summary>
        /// Tính giá trị VND tương ứng với số điểm. 1 điểm = 1,000đ.
        /// </summary>
        public decimal TinhGiaTriDiem(int soDiem)
        {
            return soDiem * GIA_TRI_MOT_DIEM;
        }

        /// <summary>
        /// Tính số điểm có thể dùng cho đơn hàng (cap tại 50% tổng đơn).
        /// </summary>
        public int TinhDiemKhaDung(int diemHienCo, decimal tongDon)
        {
            if (diemHienCo <= 0 || tongDon <= 0) return 0;

            // Số tiền giảm tối đa = 50% đơn hàng
            decimal maxGiam = tongDon * TY_LE_TIEU_TOI_DA;
            // Số điểm cần để đạt maxGiam
            int maxDiemDung = (int)Math.Floor(maxGiam / GIA_TRI_MOT_DIEM);

            return Math.Min(diemHienCo, maxDiemDung);
        }

        #endregion

        #region Ghi sổ điểm

        /// <summary>
        /// Cộng điểm sau thanh toán thành công. Cập nhật KH.DiemTichLuy + ghi LichSuDiem.
        /// </summary>
        public OperationResult CongDiem(int idKhachHang, int soDiem, int? idDonHang, string lyDo, int createdBy)
        {
            if (soDiem <= 0) return OperationResult.Success(); // Không có điểm để cộng -> OK silently

            try
            {
                var kh = _khachHangGateway.LayTheoId(idKhachHang);
                if (kh == null) return OperationResult.Failed("Không tìm thấy khách hàng.");

                int soDuTruoc = kh.DiemTichLuy;
                int soDuSau = soDuTruoc + soDiem;

                // Cập nhật tổng điểm trên KhachHang
                kh.DiemTichLuy = soDuSau;

                // === AUTO UPGRADE ===
                if (kh.LoaiKhach == AppConstants.LoaiKhachHang.CaNhan ||
                    kh.LoaiKhach == AppConstants.LoaiKhachHang.HocSinhSinhVien ||
                    kh.LoaiKhach == AppConstants.LoaiKhachHang.Vip)
                {
                    if (soDuSau >= 500 && kh.LoaiKhach != AppConstants.LoaiKhachHang.VVIP)
                    {
                        kh.LoaiKhach = AppConstants.LoaiKhachHang.VVIP;
                    }
                    else if (soDuSau >= 200 && soDuSau < 500 && kh.LoaiKhach != AppConstants.LoaiKhachHang.Vip)
                    {
                        kh.LoaiKhach = AppConstants.LoaiKhachHang.Vip;
                    }
                }

                var resSua = BUS_KhachHang.Instance.Sua(kh);
                if (!resSua.IsSuccess) return OperationResult.Failed("Không thể cập nhật điểm khách hàng.");

                // Ghi lịch sử
                var lichSu = new ET_LichSuDiem
                {
                    IdKhachHang = idKhachHang,
                    LoaiGiaoDich = AppConstants.LoaiGiaoDichDiem.TichLuy,
                    SoDiem = soDiem,
                    SoDuTruoc = soDuTruoc,
                    SoDuSau = soDuSau,
                    IdDonHang = idDonHang,
                    LyDo = lyDo ?? "Tích điểm thanh toán",
                    ThoiGian = DateTime.Now,
                    CreatedBy = createdBy
                };
                _lichSuDiemGateway.Them(lichSu);

                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                return OperationResult.Failed("Lỗi cộng điểm: " + ex.Message);
            }
        }

        /// <summary>
        /// Trừ điểm khi KH dùng điểm thanh toán. Cập nhật KH.DiemTichLuy + ghi LichSuDiem.
        /// </summary>
        public OperationResult TieuDiem(int idKhachHang, int soDiem, int? idDonHang, string lyDo, int createdBy)
        {
            if (soDiem <= 0) return OperationResult.Success();

            try
            {
                var kh = _khachHangGateway.LayTheoId(idKhachHang);
                if (kh == null) return OperationResult.Failed("Không tìm thấy khách hàng.");
                if (kh.DiemTichLuy < soDiem)
                    return OperationResult.Failed(string.Format("Không đủ điểm. Hiện có: {0}, cần: {1}", kh.DiemTichLuy, soDiem));

                int soDuTruoc = kh.DiemTichLuy;
                int soDuSau = soDuTruoc - soDiem;

                kh.DiemTichLuy = soDuSau;
                var resSua = BUS_KhachHang.Instance.Sua(kh);
                if (!resSua.IsSuccess) return OperationResult.Failed("Không thể cập nhật điểm khách hàng.");

                var lichSu = new ET_LichSuDiem
                {
                    IdKhachHang = idKhachHang,
                    LoaiGiaoDich = AppConstants.LoaiGiaoDichDiem.SuDung,
                    SoDiem = -Math.Abs(soDiem),
                    SoDuTruoc = soDuTruoc,
                    SoDuSau = soDuSau,
                    IdDonHang = idDonHang,
                    LyDo = lyDo ?? "Dùng điểm thanh toán",
                    ThoiGian = DateTime.Now,
                    CreatedBy = createdBy
                };
                _lichSuDiemGateway.Them(lichSu);

                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                return OperationResult.Failed("Lỗi tiêu điểm: " + ex.Message);
            }
        }

        #endregion

        #region Edit Points
        public OperationResult DieuChinhDiem(int idKhachHang, int soDiemDieuChinh, string lyDo, int createdBy)
        {
            try
            {
                if (soDiemDieuChinh == 0) return OperationResult.Failed("Số điểm điều chỉnh phải khác 0.");

                var kh = _khachHangGateway.LayTheoId(idKhachHang);
                if (kh == null) return OperationResult.Failed("Không tìm thấy khách hàng.");
                
                int soDuTruoc = kh.DiemTichLuy;
                int soDuSau = soDuTruoc + soDiemDieuChinh;

                if (soDuSau < 0)
                    return OperationResult.Failed("Không đủ điểm để trừ.");

                kh.DiemTichLuy = soDuSau;
                
                var resSua = BUS_KhachHang.Instance.Sua(kh);
                if (!resSua.IsSuccess) return OperationResult.Failed("Không thể cập nhật điểm khách hàng.");

                var lichSu = new ET_LichSuDiem
                {
                    IdKhachHang = idKhachHang,
                    LoaiGiaoDich = AppConstants.LoaiGiaoDichDiem.DieuChinh,
                    SoDiem = soDiemDieuChinh,
                    SoDuTruoc = soDuTruoc,
                    SoDuSau = soDuSau,
                    IdDonHang = null,
                    LyDo = lyDo ?? "Điều chỉnh tay",
                    ThoiGian = DateTime.Now,
                    CreatedBy = createdBy
                };
                _lichSuDiemGateway.Them(lichSu);

                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                return OperationResult.Failed("Lỗi điều chỉnh điểm: " + ex.Message);
            }
        }
        #endregion
    }
}

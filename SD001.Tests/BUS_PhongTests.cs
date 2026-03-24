using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BUS;
using ET;

namespace SD001.Tests
{
    [TestClass]
    public class BUS_PhongTests
    {
        #region Setup & Mock Fields
        private Mock<IDoanKhachGateway> _mockDoanKhach;
        private Mock<IGiaoDichViGateway> _mockGiaoDich;
        private Mock<IPhieuChiGateway> _mockPhieuChi;
        private Mock<IKhachHangGateway> _mockKhachHang;
        private Mock<IPhongGateway> _mockPhong;
        private Mock<IChiTietDonHangGateway> _mockChiTiet;
        private Mock<IKhuVucGateway> _mockKhuVuc;
        private Mock<ISanPhamGateway> _mockSanPham;
        private Mock<IDatPhongChiTietGateway> _mockDatPhong;
        private Mock<IDonHangGateway> _mockDonHang;
        private Mock<IChiTietDatPhongGateway> _mockChiTietDatPhong;
        private Mock<IDoanKhachDichVuGateway> _mockDoanKhachDichVu;
        private Mock<IViDienTuGateway> _mockVi;
        private Mock<IPhieuThuGateway> _mockPhieuThu;
        private BUS_Phong _bus;

        [TestInitialize]
        public void Setup()
        {
            _mockDoanKhach = new Mock<IDoanKhachGateway>();
            _mockGiaoDich = new Mock<IGiaoDichViGateway>();
            _mockPhieuChi = new Mock<IPhieuChiGateway>();
            _mockKhachHang = new Mock<IKhachHangGateway>();
            _mockPhong = new Mock<IPhongGateway>();
            _mockChiTiet = new Mock<IChiTietDonHangGateway>();
            _mockKhuVuc = new Mock<IKhuVucGateway>();
            _mockSanPham = new Mock<ISanPhamGateway>();
            _mockDatPhong = new Mock<IDatPhongChiTietGateway>();
            _mockDonHang = new Mock<IDonHangGateway>();
            _mockChiTietDatPhong = new Mock<IChiTietDatPhongGateway>();
            _mockDoanKhachDichVu = new Mock<IDoanKhachDichVuGateway>();
            _mockVi = new Mock<IViDienTuGateway>();
            _mockPhieuThu = new Mock<IPhieuThuGateway>();

            _bus = new BUS_Phong(
                _mockDoanKhach.Object, _mockGiaoDich.Object, _mockPhieuChi.Object,
                _mockKhachHang.Object, _mockPhong.Object, _mockChiTiet.Object,
                _mockKhuVuc.Object, _mockSanPham.Object, _mockDatPhong.Object,
                _mockDonHang.Object, _mockChiTietDatPhong.Object, _mockDoanKhachDichVu.Object,
                _mockVi.Object, _mockPhieuThu.Object
            );
        }
        #endregion

        // =========================================================================
        // NHOM 1 — CRUD (Basic)
        // =========================================================================

        #region CRUD
        [TestMethod]
        public void LoadDS_TraVeDungSoLuong()
        {
            _mockPhong.Setup(x => x.LoadDS()).Returns(new List<ET_Phong>
            {
                new ET_Phong { Id = 1, TenPhong = "P101", TrangThai = "Trong" },
                new ET_Phong { Id = 2, TenPhong = "P102", TrangThai = "DangSuDung" },
                new ET_Phong { Id = 3, TenPhong = "P201", TrangThai = "Trong" }
            });

            Assert.AreEqual(3, _bus.LoadDS().Count);
        }

        [TestMethod]
        public void LayTheoId_CoTonTai_TraVeDungTenPhong()
        {
            _mockPhong.Setup(x => x.LayTheoId(1)).Returns(new ET_Phong { Id = 1, TenPhong = "P101" });
            
            var result = _bus.LayTheoId(1);
            
            Assert.IsNotNull(result);
            Assert.AreEqual("P101", result.TenPhong);
        }

        [TestMethod]
        public void LayTheoId_KhongTonTai_TraVeNull()
        {
            _mockPhong.Setup(x => x.LayTheoId(999)).Returns((ET_Phong)null);
            Assert.IsNull(_bus.LayTheoId(999));
        }

        [TestMethod]
        public void ThemPhong_VerifyGatewayNhanDungObject()
        {
            _mockPhong.Setup(x => x.Them(It.IsAny<ET_Phong>())).Returns(true);
            var phong = new ET_Phong { TenPhong = "P301" };

            Assert.IsTrue(_bus.Them(phong));
            _mockPhong.Verify(x => x.Them(phong), Times.Once);
        }

        [TestMethod]
        public void XoaPhong_VerifyGoiDungId()
        {
            _mockPhong.Setup(x => x.Xoa(42)).Returns(true);

            Assert.IsTrue(_bus.Xoa(42));
            _mockPhong.Verify(x => x.Xoa(42), Times.Once);
        }
        #endregion

        // =========================================================================
        // NHOM 2 — CHECKIN NGAY (Walk-in)
        // =========================================================================

        #region CheckIn
        [TestMethod]
        public void CheckIn_PhongDangBan_TuChoiNgay()
        {
            // QUY TAC: Phong co trong GetBusyRoomIds -> KHONG cho check-in
            _mockPhong.Setup(x => x.GetBusyRoomIds(It.IsAny<DateTime>(), It.IsAny<DateTime>(), 1))
                .Returns(new List<int> { 1 }); // Phong 1 dang co nguoi

            var result = _bus.CheckIn(1, null, 500000, "TienMat");

            Assert.IsFalse(result);
            _mockDonHang.Verify(x => x.ThemVaLayId(It.IsAny<ET_DonHang>()), Times.Never); // KHONG tao DH
        }

        [TestMethod]
        public void CheckIn_TienMat_ThanhCong_VerifyPhieuThuDungSoTien()
        {
            SetupCheckInInfra();

            var result = _bus.CheckIn(1, null, 500000, "TienMat", 1, DateTime.Now.AddDays(1));

            Assert.IsTrue(result);
            // ASSERT NGHIEP VU: Phieu thu PHAI co so tien = 500000
            _mockPhieuThu.Verify(x => x.Them(It.Is<ET_PhieuThu>(pt => pt.SoTien == 500000)), Times.Once);
            // ASSERT: Phong PHAI chuyen sang DangSuDung
            _mockPhong.Verify(x => x.Sua(It.Is<ET_Phong>(p => p.TrangThai == "DangSuDung")), Times.Once);
        }

        [TestMethod]
        public void CheckIn_ViRFID_SoDuDu_VerifyViTruDungSoTien()
        {
            SetupCheckInInfra();
            
            // Vi co 1.000.000, thanh toan 500.000
            _mockVi.Setup(x => x.LayTheoKhachHang(1)).Returns(new ET_ViDienTu { Id = 10, SoDuKhaDung = 1000000 });
            _mockVi.Setup(x => x.Sua(It.IsAny<ET_ViDienTu>())).Returns(true);
            _mockGiaoDich.Setup(x => x.Them(It.IsAny<ET_GiaoDichVi>())).Returns(true);

            var result = _bus.CheckIn(1, 1, 500000, "ViRFID", 1, DateTime.Now.AddDays(1));

            Assert.IsTrue(result);
            // ASSERT NGHIEP VU: Vi phai bi tru dung 500k -> con lai 500k
            _mockVi.Verify(x => x.Sua(It.Is<ET_ViDienTu>(v => v.SoDuKhaDung == 500000)), Times.Once);
            // Phieu thu KHONG duoc tao (vi xai RFID, khong xai tien mat)
            _mockPhieuThu.Verify(x => x.Them(It.IsAny<ET_PhieuThu>()), Times.Never);
            // Giao dich vi PHAI duoc tao
            _mockGiaoDich.Verify(x => x.Them(It.Is<ET_GiaoDichVi>(gd => gd.SoTien == 500000)), Times.Once);
        }

        [TestMethod]
        public void CheckIn_ViRFID_SoDu100k_Can500k_TuChoi()
        {
            SetupCheckInInfra();

            _mockVi.Setup(x => x.LayTheoKhachHang(1)).Returns(new ET_ViDienTu { Id = 10, SoDuKhaDung = 100000 });

            var result = _bus.CheckIn(1, 1, 500000, "ViRFID", 1, DateTime.Now.AddDays(1));

            Assert.IsFalse(result);
            _mockVi.Verify(x => x.Sua(It.IsAny<ET_ViDienTu>()), Times.Never); // KHONG tru vi
        }

        [TestMethod]
        public void CheckIn_ViRFID_KhongCoKhachHang_TuChoi()
        {
            SetupCheckInInfra();

            var result = _bus.CheckIn(1, null, 500000, "ViRFID", 1, DateTime.Now.AddDays(1));

            Assert.IsFalse(result); // idKhachHang = null -> khong the tru vi
        }

        [TestMethod]
        public void CheckIn_ViRFID_KhongCoVi_TuChoi()
        {
            SetupCheckInInfra();

            _mockVi.Setup(x => x.LayTheoKhachHang(1)).Returns((ET_ViDienTu)null);

            var result = _bus.CheckIn(1, 1, 500000, "ViRFID", 1, DateTime.Now.AddDays(1));

            Assert.IsFalse(result);
        }
        #endregion

        // =========================================================================
        // NHOM 3 — DAT GIU CHO (Reservation)
        // =========================================================================

        #region ReserveRoom
        [TestMethod]
        public void ReserveRoom_PhongDaBan_TuChoi()
        {
            _mockPhong.Setup(x => x.GetBusyRoomIds(It.IsAny<DateTime>(), It.IsAny<DateTime>(), 1))
                .Returns(new List<int> { 1 });

            Assert.IsFalse(_bus.ReserveRoom(1, 1, DateTime.Now, DateTime.Now.AddDays(2), 200000, 1));
        }

        [TestMethod]
        public void ReserveRoom_CoCoc_VerifyPhieuThuTienCoc()
        {
            SetupReserveInfra();

            var result = _bus.ReserveRoom(1, 1, DateTime.Now, DateTime.Now.AddDays(2), 200000, 1);

            Assert.IsTrue(result);
            // ASSERT: Phieu thu coc phai dung 200k
            _mockPhieuThu.Verify(x => x.Them(It.Is<ET_PhieuThu>(pt => pt.SoTien == 200000)), Times.Once);
        }

        [TestMethod]
        public void ReserveRoom_KhongCoc_KhongTaoPhieuThu()
        {
            SetupReserveInfra();

            var result = _bus.ReserveRoom(1, 1, DateTime.Now, DateTime.Now.AddDays(2), 0, 1);

            Assert.IsTrue(result);
            _mockPhieuThu.Verify(x => x.Them(It.IsAny<ET_PhieuThu>()), Times.Never);
        }

        [TestMethod]
        public void ReserveRoom_DonHangTrangThai_DaDatCoc_KhiCoCoc()
        {
            SetupReserveInfra();

            ET_DonHang dhCapture = null;
            _mockDonHang.Setup(x => x.ThemVaLayId(It.IsAny<ET_DonHang>()))
                .Callback<ET_DonHang>(dh => dhCapture = dh)
                .Returns(100);

            _bus.ReserveRoom(1, 1, DateTime.Now, DateTime.Now.AddDays(2), 200000, 1);

            Assert.IsNotNull(dhCapture);
            Assert.AreEqual("DaDatCoc", dhCapture.TrangThai, "Co coc -> trang thai phai la DaDatCoc");
        }

        [TestMethod]
        public void ReserveRoom_DonHangTrangThai_ChoThanhToan_KhiKhongCoc()
        {
            SetupReserveInfra();

            ET_DonHang dhCapture = null;
            _mockDonHang.Setup(x => x.ThemVaLayId(It.IsAny<ET_DonHang>()))
                .Callback<ET_DonHang>(dh => dhCapture = dh)
                .Returns(100);

            _bus.ReserveRoom(1, 1, DateTime.Now, DateTime.Now.AddDays(2), 0, 1);

            Assert.IsNotNull(dhCapture);
            Assert.AreEqual("ChoThanhToan", dhCapture.TrangThai, "Khong coc -> trang thai phai la ChoThanhToan");
        }
        #endregion

        // =========================================================================
        // NHOM 4 — PHU THU (Minibar / Don dep)
        // =========================================================================

        #region AddSurcharge
        [TestMethod]
        public void AddSurcharge_PhongKhongCoDatPhong_TraVeLoi()
        {
            _mockChiTietDatPhong.Setup(x => x.LoadDS()).Returns(new List<ET_ChiTietDatPhong>());
            _mockDatPhong.Setup(x => x.LoadDS()).Returns(new List<ET_DatPhongChiTiet>());

            var result = _bus.AddSurcharge(1, 50000, "Minibar");

            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.Contains("Không tìm thấy"));
        }
        #endregion

        // =========================================================================
        // NHOM 5 — DAT PHONG DOAN
        // =========================================================================

        #region ReserveGroup
        [TestMethod]
        public void ReserveGroup_1PhongBiTrung_ToanBoTuChoi()
        {
            // QUY TAC: 1 phong trung -> KHONG dat bat ky phong nao (All or Nothing)
            _mockPhong.Setup(x => x.GetBusyRoomIds(It.IsAny<DateTime>(), It.IsAny<DateTime>(), 1))
                .Returns(new List<int> { 2 }); // Phong 2 bi trung

            var result = _bus.ReserveGroup(
                new List<int> { 1, 2, 3 },
                new ET_DoanKhach { TenDoan = "Doan ABC" },
                DateTime.Now, DateTime.Now.AddDays(3),
                500000, "TienMat", 1
            );

            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.Contains("đã bị đặt"));
            // Verify KHONG tao bat ky DonHang nao
            _mockDonHang.Verify(x => x.ThemVaLayId(It.IsAny<ET_DonHang>()), Times.Never);
        }
        #endregion

        // =========================================================================
        // NHOM 6 — CHECKOUT
        // =========================================================================

        #region Checkout
        [TestMethod]
        public void ConfirmCheckOut_PhongKhongCoDatPhong_TraVeLoi()
        {
            _mockDatPhong.Setup(x => x.LoadDS()).Returns(new List<ET_DatPhongChiTiet>());
            _mockChiTietDatPhong.Setup(x => x.LoadDS()).Returns(new List<ET_ChiTietDatPhong>());

            var result = _bus.ConfirmCheckOut(999, 0, "TienMat", 1);

            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.Contains("Không tìm thấy booking"));
        }

        [TestMethod]
        public void CheckOut_SimpleShorcut_GoiConfirmCheckOut()
        {
            // CheckOut(idPhong) goi ConfirmCheckOut(phong, 0, "TienMat", 1)
            _mockDatPhong.Setup(x => x.LoadDS()).Returns(new List<ET_DatPhongChiTiet>());
            _mockChiTietDatPhong.Setup(x => x.LoadDS()).Returns(new List<ET_ChiTietDatPhong>());

            var result = _bus.CheckOut(999);

            Assert.IsFalse(result); // Phong khong ton tai -> false
        }
        #endregion

        // =========================================================================
        // NHOM 7 — FINISH CLEANING (Don dep xong)
        // =========================================================================

        #region FinishCleaning
        [TestMethod]
        public void FinishCleaning_ThanhCong_VerifyTrangThaiVeTrong()
        {
            _mockPhong.Setup(x => x.LayTheoId(1)).Returns(new ET_Phong { Id = 1, TenPhong = "P101", TrangThai = "DonDep" });
            _mockPhong.Setup(x => x.Sua(It.IsAny<ET_Phong>())).Returns(true);

            var result = _bus.FinishCleaning(1);

            Assert.IsTrue(result);
            // ASSERT SIDE EFFECT: Phong PHAI chuyen ve "Trong"
            _mockPhong.Verify(x => x.Sua(It.Is<ET_Phong>(p => p.TrangThai == "Trong")), Times.Once);
        }

        [TestMethod]
        public void FinishCleaning_PhongKhongTonTai_TraVeFalse()
        {
            _mockPhong.Setup(x => x.LayTheoId(999)).Returns((ET_Phong)null);

            Assert.IsFalse(_bus.FinishCleaning(999));
        }

        [TestMethod]
        public void FinishCleaning_DBThatBai_TraVeFalse()
        {
            _mockPhong.Setup(x => x.LayTheoId(1)).Returns(new ET_Phong { Id = 1, TrangThai = "DonDep" });
            _mockPhong.Setup(x => x.Sua(It.IsAny<ET_Phong>())).Returns(false);

            Assert.IsFalse(_bus.FinishCleaning(1));
        }
        #endregion

        // =========================================================================
        // NHOM 8 — TINH PHU THU TRE GIO
        // =========================================================================

        #region TinhPhuThuTreGio
        [TestMethod]
        public void TinhPhuThuTreGio_KhongTre_TraVe0()
        {
            // QUY TAC: Tra truoc hoac dung gio -> Phu thu = 0
            DateTime ngayTraDuKien = DateTime.Now.AddHours(2);
            DateTime ngayTraThucTe = DateTime.Now; // Tra SOM hon du kien

            Assert.AreEqual(0m, _bus.TinhPhuThuTreGio(1, ngayTraDuKien, ngayTraThucTe));
        }

        [TestMethod]
        public void TinhPhuThuTreGio_TraDungGio_TraVe0()
        {
            DateTime ngayTra = DateTime.Now;
            Assert.AreEqual(0m, _bus.TinhPhuThuTreGio(1, ngayTra, ngayTra));
        }
        #endregion

        // =========================================================================
        // NHOM 9 — CALCULATE CHECKOUT (DU TINH HOA DON TRUOC KHI TRA PHONG)
        // =========================================================================

        #region CalculateCheckOut
        [TestMethod]
        public void CalculateCheckOut_PhongKhongCoDatPhong_TraVeNull()
        {
            _mockDatPhong.Setup(x => x.LoadDS()).Returns(new List<ET_DatPhongChiTiet>());
            _mockChiTietDatPhong.Setup(x => x.LoadDS()).Returns(new List<ET_ChiTietDatPhong>());

            var result = _bus.CalculateCheckOut(999);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void CalculateCheckOut_CoBooking_TraVeThongTinDayDu()
        {
            // Setup: Phong 1, khach nhan ngay hom qua, du kien tra ngay mai
            var dpct = new ET_DatPhongChiTiet
            {
                Id = 10,
                IdChiTietDonHang = 20,
                NgayNhan = DateTime.Now.AddDays(-1),
                NgayTra = DateTime.Now.AddDays(1),
                TrangThai = "DaNhan"
            };
            var ctdp = new ET_ChiTietDatPhong
            {
                Id = 30,
                IdDatPhongChiTiet = 10,
                IdPhong = 1
            };

            _mockDatPhong.Setup(x => x.LoadDS()).Returns(new List<ET_DatPhongChiTiet> { dpct });
            _mockChiTietDatPhong.Setup(x => x.LoadDS()).Returns(new List<ET_ChiTietDatPhong> { ctdp });
            _mockPhong.Setup(x => x.LayTheoId(1)).Returns(new ET_Phong { Id = 1, TenPhong = "P101", IdSanPham = 5 });
            _mockChiTiet.Setup(x => x.LayTheoId(20)).Returns(new ET_ChiTietDonHang { Id = 20, IdDonHang = 100 });
            _mockDonHang.Setup(x => x.LayTheoId(100)).Returns(new ET_DonHang { Id = 100, TongTien = 500000 });

            var result = _bus.CalculateCheckOut(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.IdPhong);
            Assert.AreEqual("P101", result.TenPhong);
            Assert.AreEqual(500000m, result.DaThanhToan, "DaThanhToan phai = TongTien cua DonHang goc");
        }
        #endregion

        // =========================================================================
        // HELPERS
        // =========================================================================

        #region Helpers
        private void SetupCheckInInfra()
        {
            _mockPhong.Setup(x => x.GetBusyRoomIds(It.IsAny<DateTime>(), It.IsAny<DateTime>(), 1))
                .Returns(new List<int>()); // Phong trong

            _mockDonHang.Setup(x => x.ThemVaLayId(It.IsAny<ET_DonHang>())).Returns(100);
            _mockPhong.Setup(x => x.LayTheoId(1)).Returns(new ET_Phong { Id = 1, TenPhong = "P101", IdSanPham = 5 });
            _mockPhong.Setup(x => x.Sua(It.IsAny<ET_Phong>())).Returns(true);
            _mockChiTiet.Setup(x => x.ThemVaLayId(It.IsAny<ET_ChiTietDonHang>())).Returns(200);
            _mockDatPhong.Setup(x => x.ThemVaLayId(It.IsAny<ET_DatPhongChiTiet>())).Returns(300);
            _mockChiTietDatPhong.Setup(x => x.ThemVaLayId(It.IsAny<ET_ChiTietDatPhong>())).Returns(400);
        }

        private void SetupReserveInfra()
        {
            _mockPhong.Setup(x => x.GetBusyRoomIds(It.IsAny<DateTime>(), It.IsAny<DateTime>(), 1))
                .Returns(new List<int>());

            _mockDonHang.Setup(x => x.ThemVaLayId(It.IsAny<ET_DonHang>())).Returns(100);
            _mockPhieuThu.Setup(x => x.Them(It.IsAny<ET_PhieuThu>())).Returns(true);
            _mockPhong.Setup(x => x.LayTheoId(1)).Returns(new ET_Phong { Id = 1, TenPhong = "P101", IdSanPham = 5 });
            _mockPhong.Setup(x => x.Sua(It.IsAny<ET_Phong>())).Returns(true);
            _mockChiTiet.Setup(x => x.ThemVaLayId(It.IsAny<ET_ChiTietDonHang>())).Returns(200);
            _mockDatPhong.Setup(x => x.ThemVaLayId(It.IsAny<ET_DatPhongChiTiet>())).Returns(300);
            _mockChiTietDatPhong.Setup(x => x.ThemVaLayId(It.IsAny<ET_ChiTietDatPhong>())).Returns(400);
        }
        #endregion
    }
}

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
    public class BUS_ThueDoTests
    {
        private Mock<IThueDoChiTietGateway> _mockThueDoGw;
        private Mock<IDonHangGateway> _mockDonHangGw;
        private Mock<IViDienTuGateway> _mockViDienTuGw;
        private Mock<IGiaoDichViGateway> _mockGiaoDichGw;
        private Mock<IPhieuThuGateway> _mockPhieuThuGw;
        private Mock<IPhieuChiGateway> _mockPhieuChiGw;
        private Mock<IChiTietDonHangGateway> _mockCTDHGw;
        
        private BUS_ThueDo _bus;

        [TestInitialize]
        public void Setup()
        {
            _mockThueDoGw = new Mock<IThueDoChiTietGateway>();
            _mockDonHangGw = new Mock<IDonHangGateway>();
            _mockViDienTuGw = new Mock<IViDienTuGateway>();
            _mockGiaoDichGw = new Mock<IGiaoDichViGateway>();
            _mockPhieuThuGw = new Mock<IPhieuThuGateway>();
            _mockPhieuChiGw = new Mock<IPhieuChiGateway>();
            _mockCTDHGw = new Mock<IChiTietDonHangGateway>();

            _bus = new BUS_ThueDo(
                _mockThueDoGw.Object,
                _mockDonHangGw.Object,
                _mockViDienTuGw.Object,
                _mockGiaoDichGw.Object,
                _mockPhieuThuGw.Object,
                _mockPhieuChiGw.Object,
                _mockCTDHGw.Object
            );
        }

        #region Kịch bản Cơ Bản & General
        [TestMethod]
        public void LoadDS_GoiDungGateway_TraVeDuLieu()
        {
            _mockThueDoGw.Setup(g => g.LoadDS()).Returns(new List<ET_ThueDoChiTiet>());
            var result = _bus.LoadDS();
            Assert.IsNotNull(result);
            _mockThueDoGw.Verify(g => g.LoadDS(), Times.Once);
        }
        #endregion

        #region Kịch bản Giao Đồ (RentMultipleItems) - Happy Path & Boundary

        [TestMethod]
        public void RentMultipleItems_GioHangTrong_TraVeFail()
        {
            var result = _bus.RentMultipleItems(new ET_DonHang(), new List<RentalCartItem>(), AppConstants.PhuongThucThanhToan.TienMat, 1);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Giỏ hàng trống.", result.ErrorMessage);
        }

        [TestMethod]
        public void RentMultipleItems_TienMat_ThemDonHangThanhCong()
        {
            // Arrange
            var dh = new ET_DonHang { IdKhachHang = null };
            var cart = new List<RentalCartItem> 
            { 
                new RentalCartItem { IdSanPham = 1, SoLuong = 2, TienThue = 50000, TienCoc = 100000 } 
            };
            
            _mockDonHangGw.Setup(g => g.ThemVaLayId(It.IsAny<ET_DonHang>())).Returns(100);
            _mockPhieuThuGw.Setup(g => g.Them(It.IsAny<ET_PhieuThu>())).Returns(true);
            _mockCTDHGw.Setup(g => g.ThemVaLayId(It.IsAny<ET_ChiTietDonHang>())).Returns(200);
            _mockThueDoGw.Setup(g => g.Them(It.IsAny<ET_ThueDoChiTiet>())).Returns(true);

            // Act
            var result = _bus.RentMultipleItems(dh, cart, AppConstants.PhuongThucThanhToan.TienMat, 1);

            // Assert
            Assert.IsTrue(result.IsSuccess, "Nên cho thuê thành công bằng tiền mặt");
            _mockDonHangGw.Verify(g => g.ThemVaLayId(It.IsAny<ET_DonHang>()), Times.Once, "Phải tạo 1 đơn hàng gốc");
            _mockPhieuThuGw.Verify(g => g.Them(It.IsAny<ET_PhieuThu>()), Times.Once, "Phải tạo 1 phiếu thu cho tiền mặt");
            
            // TDD: Có 2 món (SoLuong = 2) nên hệ thống phải rã ra làm 2 CTDH và 2 ThueDoChiTiet
            _mockCTDHGw.Verify(g => g.ThemVaLayId(It.IsAny<ET_ChiTietDonHang>()), Times.Exactly(2), "Phải rã giỏ hàng thành 2 CTDH độc lập");
            _mockThueDoGw.Verify(g => g.Them(It.IsAny<ET_ThueDoChiTiet>()), Times.Exactly(2), "Phải rã giỏ hàng thành 2 ThueDoChiTiet độc lập");
        }

        [TestMethod]
        public void RentMultipleItems_ViRfid_ThanhToanThanhCong()
        {
            var dh = new ET_DonHang { IdKhachHang = 10 };
            var cart = new List<RentalCartItem> { new RentalCartItem { SoLuong = 1, TienThue = 50000, TienCoc = 100000 } };
            
            _mockDonHangGw.Setup(g => g.ThemVaLayId(It.IsAny<ET_DonHang>())).Returns(100);

            var vi = new ET_ViDienTu { Id = 1, IdKhachHang = 10, SoDuKhaDung = 200000, SoDuDongBang = 0 };
            _mockViDienTuGw.Setup(g => g.LayTheoKhachHang(10)).Returns(vi);
            _mockViDienTuGw.Setup(g => g.Sua(It.IsAny<ET_ViDienTu>())).Returns(true);
            _mockGiaoDichGw.Setup(g => g.ThemVaLayId(It.IsAny<ET_GiaoDichVi>())).Returns(300);
            _mockCTDHGw.Setup(g => g.ThemVaLayId(It.IsAny<ET_ChiTietDonHang>())).Returns(200);
            _mockThueDoGw.Setup(g => g.Them(It.IsAny<ET_ThueDoChiTiet>())).Returns(true);

            var result = _bus.RentMultipleItems(dh, cart, AppConstants.PhuongThucThanhToan.ViRfid, 1);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(50000, vi.SoDuKhaDung, "Số dư khả dụng = 200k - 50k(Thue) - 100k(Coc)");
            Assert.AreEqual(100000, vi.SoDuDongBang, "Tiền cọc phải được chuyển sang phần đóng băng");
            _mockGiaoDichGw.Verify(g => g.ThemVaLayId(It.IsAny<ET_GiaoDichVi>()), Times.Exactly(2), "Phải sinh ra 2 giao dịch ví: Thuê dịch vụ và Thu cọc");
        }

        #endregion

        #region Kịch bản Giao Đồ (RentMultipleItems) - Rollback & Exceptions (Full Error Branches)
        
        [TestMethod]
        public void RentMultipleItems_LoiTaoDonHangGoc_DatabaseTraLoi_ExceptionCaught()
        {
            var cart = new List<RentalCartItem> { new RentalCartItem { SoLuong = 1, TienThue = 50, TienCoc = 100 } };
            _mockDonHangGw.Setup(g => g.ThemVaLayId(It.IsAny<ET_DonHang>())).Returns(0); // Simulate DB fail

            var result = _bus.RentMultipleItems(new ET_DonHang(), cart, AppConstants.PhuongThucThanhToan.TienMat, 1);
            
            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.Contains("Không thể tạo đơn hàng gốc."));
        }

        [TestMethod]
        public void RentMultipleItems_ViRfid_KhongTonTaiViKhachHang_TraVeFail()
        {
            var cart = new List<RentalCartItem> { new RentalCartItem { SoLuong = 1, TienThue = 50, TienCoc = 100 } };
            _mockDonHangGw.Setup(g => g.ThemVaLayId(It.IsAny<ET_DonHang>())).Returns(1); 
            _mockViDienTuGw.Setup(g => g.LayTheoKhachHang(It.IsAny<int>())).Returns((ET_ViDienTu)null); // Missing wallet

            var result = _bus.RentMultipleItems(new ET_DonHang { IdKhachHang = 1 }, cart, AppConstants.PhuongThucThanhToan.ViRfid, 1);
            
            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.Contains("Số dư ví không đủ.")); // Because vi == null drops into same check block
        }

        [TestMethod]
        public void RentMultipleItems_ViRfid_KhongTraDuTien_TraVeFail()
        {
            var dh = new ET_DonHang { IdKhachHang = 10 };
            var cart = new List<RentalCartItem> { new RentalCartItem { SoLuong = 1, TienThue = 50000, TienCoc = 100000 } };
            _mockDonHangGw.Setup(g => g.ThemVaLayId(It.IsAny<ET_DonHang>())).Returns(100);

            var vi = new ET_ViDienTu { Id = 1, IdKhachHang = 10, SoDuKhaDung = 100000 };
            _mockViDienTuGw.Setup(g => g.LayTheoKhachHang(10)).Returns(vi);

            var result = _bus.RentMultipleItems(dh, cart, AppConstants.PhuongThucThanhToan.ViRfid, 1);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Số dư ví không đủ.", result.ErrorMessage);
        }

        [TestMethod]
        public void RentMultipleItems_ViRfid_LoiCapNhatViXongRollback_TraVeFail()
        {
            var cart = new List<RentalCartItem> { new RentalCartItem { SoLuong = 1, TienThue = 50, TienCoc = 100 } };
            _mockDonHangGw.Setup(g => g.ThemVaLayId(It.IsAny<ET_DonHang>())).Returns(1); 
            _mockViDienTuGw.Setup(g => g.LayTheoKhachHang(It.IsAny<int>())).Returns(new ET_ViDienTu { SoDuKhaDung = 500000 });
            _mockViDienTuGw.Setup(g => g.Sua(It.IsAny<ET_ViDienTu>())).Returns(false); // Update Wallet fails

            var result = _bus.RentMultipleItems(new ET_DonHang { IdKhachHang = 1 }, cart, AppConstants.PhuongThucThanhToan.ViRfid, 1);
            
            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.Contains("Lỗi cập nhật số dư ví."));
        }

        [TestMethod]
        public void RentMultipleItems_TienMat_TaoPhieuThuThatBai_TraVeFail()
        {
            var cart = new List<RentalCartItem> { new RentalCartItem { SoLuong = 1, TienThue = 50, TienCoc = 100 } };
            _mockDonHangGw.Setup(g => g.ThemVaLayId(It.IsAny<ET_DonHang>())).Returns(1); 
            _mockPhieuThuGw.Setup(g => g.Them(It.IsAny<ET_PhieuThu>())).Returns(false); // Fail

            var result = _bus.RentMultipleItems(new ET_DonHang { IdKhachHang = 1 }, cart, AppConstants.PhuongThucThanhToan.TienMat, 1);
            
            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.Contains("Không tạo được Phiếu Thu."));
        }

        [TestMethod]
        public void RentMultipleItems_LoiLuuChiTietCTDH_TraVeFail()
        {
            var cart = new List<RentalCartItem> { new RentalCartItem { SoLuong = 1, TienThue = 50, TienCoc = 100 } };
            _mockDonHangGw.Setup(g => g.ThemVaLayId(It.IsAny<ET_DonHang>())).Returns(1); 
            _mockPhieuThuGw.Setup(g => g.Them(It.IsAny<ET_PhieuThu>())).Returns(true);
            _mockCTDHGw.Setup(g => g.ThemVaLayId(It.IsAny<ET_ChiTietDonHang>())).Returns(0); // Fail to sub-item
            
            var result = _bus.RentMultipleItems(new ET_DonHang { IdKhachHang = 1 }, cart, AppConstants.PhuongThucThanhToan.TienMat, 1);
            
            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.Contains("Tạo Line Item (Chi Tiết CTDH) thất bại."));
        }

        #endregion

        #region Kịch bản Trả Đồ (ReturnItem) - Happy Path

        [TestMethod]
        public void ReturnItem_TienMat_HoanFullCoc_ThanhCong()
        {
            var td = new ET_ThueDoChiTiet { Id = 5, IdGiaoDichCoc = null, SoTienCoc = 200000, TrangThaiCoc = "ChuaHoan", ThoiGianBatDau = DateTime.Now.AddHours(-1) };
            _mockThueDoGw.Setup(g => g.LayTheoId(5)).Returns(td);
            _mockPhieuChiGw.Setup(g => g.Them(It.IsAny<ET_PhieuChi>())).Returns(true);

            var result = _bus.ReturnItem(5, false, 0, 1);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("DaHoan", td.TrangThaiCoc);
            _mockPhieuChiGw.Verify(g => g.Them(It.Is<ET_PhieuChi>(p => p.SoTien == 200000)), Times.Once, "Phải xuất 1 Phiếu Chi trị giá 200k hoàn cọc cho khách");
        }

        [TestMethod]
        public void ReturnItem_ViRfid_HoanFullCoc_ThanhCong()
        {
            // Trả bằng RFID không bị phạt, tiền được nhả từ SoDuDongBang về SoDuKhaDung
            var td = new ET_ThueDoChiTiet { Id = 5, IdGiaoDichCoc = 999, SoTienCoc = 100000, TrangThaiCoc = "ChuaHoan" };
            _mockThueDoGw.Setup(g => g.LayTheoId(5)).Returns(td);
            
            var gdCoc = new ET_GiaoDichVi { Id = 999, IdVi = 10, IdDonHangLienQuan = 888 };
            _mockGiaoDichGw.Setup(g => g.LayTheoId(999)).Returns(gdCoc);

            var vi = new ET_ViDienTu { Id = 10, SoDuDongBang = 100000, SoDuKhaDung = 500000 };
            _mockViDienTuGw.Setup(g => g.LayTheoId(10)).Returns(vi);

            var result = _bus.ReturnItem(5, false, 0, 1);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(0, vi.SoDuDongBang, "Hệ thống phải gỡ 100k băng");
            Assert.AreEqual(600000, vi.SoDuKhaDung, "Hệ thống cộng dồn 100k cọc về khả dụng");
            
            _mockGiaoDichGw.Verify(g => g.ThemVaLayId(It.Is<ET_GiaoDichVi>(x => x.LoaiGiaoDich == AppConstants.LoaiGiaoDichVi.HoanCoc && x.SoTien == 100000)), Times.Once);
        }

        [TestMethod]
        public void ReturnItem_ViRfid_BiPhatVuotCoc_ThanhCongVaTuTaoPhieuThu()
        {
            var td = new ET_ThueDoChiTiet { Id = 5, IdGiaoDichCoc = 999, SoTienCoc = 100000, TrangThaiCoc = "ChuaHoan" };
            _mockThueDoGw.Setup(g => g.LayTheoId(5)).Returns(td);
            
            var gdCoc = new ET_GiaoDichVi { Id = 999, IdVi = 10, IdDonHangLienQuan = 888 };
            _mockGiaoDichGw.Setup(g => g.LayTheoId(999)).Returns(gdCoc);

            var vi = new ET_ViDienTu { Id = 10, SoDuDongBang = 100000, SoDuKhaDung = 500000 };
            _mockViDienTuGw.Setup(g => g.LayTheoId(10)).Returns(vi);

            var result = _bus.ReturnItem(5, true, 150000, 1);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.Contains("PHẠT VƯỢT CỌC: Cần thu thêm 50,000đ TIỀN MẶT"));
            
            Assert.AreEqual(0, vi.SoDuDongBang, "Phải gỡ băng 100k tiền cọc của món đồ");
            Assert.AreEqual(500000, vi.SoDuKhaDung, "Tiền hoàn về ví = 0 vì đã bị thu làm tiền phạt");
            Assert.AreEqual("DaPhat", td.TrangThaiCoc);

            _mockPhieuThuGw.Verify(g => g.Them(It.Is<ET_PhieuThu>(p => p.SoTien == 50000 && p.IdDonHang == 888)), Times.Once, "Phải sinh ra Phiếu Thu 50k truy thu");
            _mockGiaoDichGw.Verify(g => g.ThemVaLayId(It.Is<ET_GiaoDichVi>(x => x.LoaiGiaoDich == AppConstants.LoaiGiaoDichVi.HoanCoc)), Times.Never);
        }

        #endregion

        #region Kịch bản Trả Đồ (ReturnItem) - Rollback & Exceptions (Full Coverage)

        [TestMethod]
        public void ReturnItem_KhongTimThay_TraVeLoi()
        {
            _mockThueDoGw.Setup(g => g.LayTheoId(1)).Returns((ET_ThueDoChiTiet)null);
            var result = _bus.ReturnItem(1, false, 0, 1);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Không tìm thấy dữ liệu thuê đồ.", result.ErrorMessage);
        }

        [TestMethod]
        public void ReturnItem_DaHoanCoc_TraVeLoi()
        {
            var td = new ET_ThueDoChiTiet { Id = 5, TrangThaiCoc = "DaHoan" };
            _mockThueDoGw.Setup(g => g.LayTheoId(5)).Returns(td);

            var result = _bus.ReturnItem(5, false, 0, 1);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Món này đã được hoàn cọc rồi.", result.ErrorMessage);
        }

        [TestMethod]
        public void ReturnItem_CrashTrongQuaTrinhCapNhat_TraVeLoiBatKy()
        {
            _mockThueDoGw.Setup(g => g.LayTheoId(It.IsAny<int>())).Throws(new Exception("Database down"));
            var result = _bus.ReturnItem(1, false, 0, 1);
            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.Contains("Lỗi hoàn cọc: Database down"));
        }

        [TestMethod]
        public void ReturnItem_DBConcurrencyException_TraVeLoiTieuChuan()
        {
            var td = new ET_ThueDoChiTiet { Id = 5, TrangThaiCoc = "ChuaHoan", SoTienCoc = 100000 };
            _mockThueDoGw.Setup(g => g.LayTheoId(5)).Returns(td);

            // Giả lập lưu lại bị đồng thời ghi đè (Optimistic Concurrency)
            _mockThueDoGw.Setup(g => g.Sua(It.IsAny<ET_ThueDoChiTiet>())).Throws(new System.Data.DBConcurrencyException());
            
            var result = _bus.ReturnItem(5, false, 0, 1);
            
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Ví vừa bị thay đổi bởi giao dịch khác.", result.ErrorMessage);
        }

        #endregion

        #region Kịch bản CỐ TÌNH TẠO LỖI (Failed do code hiện tại thiếu Validate)

        [TestMethod]
        public void ReturnItem_SoTienPhatAm_PhaiTraVeFail()
        {
            // TDD: Expectation - Hệ thống không được phép truyền tiền phạt < 0 vì sẽ gây hoàn dương tiền
            var td = new ET_ThueDoChiTiet { Id = 5, IdGiaoDichCoc = null, SoTienCoc = 100000, TrangThaiCoc = "ChuaHoan" };
            _mockThueDoGw.Setup(g => g.LayTheoId(5)).Returns(td);

            var result = _bus.ReturnItem(5, true, -50000, 1);

            // Mong đợi BUS sẽ chốt chặn không cho tiền phạt âm
            Assert.IsFalse(result.IsSuccess, "[TDD Expectation] BUS_ThueDo cần bắt lỗi tiền phạt bị âm (< 0).");
        }

        #endregion
    }
}

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
    public class BUS_DatBanTests
    {
        #region Setup & Mock Fields
        private Mock<IDatBanGateway> _mockDatBan;
        private Mock<IBanAnGateway> _mockBanAn;
        private Mock<IChiTietDatBanGateway> _mockChiTietDatBan;
        private Mock<IDonHangGateway> _mockDonHang;
        private Mock<IChiTietDonHangGateway> _mockChiTietDonHang;
        private Mock<IPhieuThuGateway> _mockPhieuThu;
        private Mock<IDoanKhachDichVuGateway> _mockDoanKhachDichVu;
        private Mock<IBUS_GiaoDichVi> _mockGiaoDichVi;
        private Mock<IBUS_DoanKhach> _mockDoanKhach;
        private BUS_DatBan _bus;

        [TestInitialize]
        public void Setup()
        {
            _mockDatBan = new Mock<IDatBanGateway>();
            _mockBanAn = new Mock<IBanAnGateway>();
            _mockChiTietDatBan = new Mock<IChiTietDatBanGateway>();
            _mockDonHang = new Mock<IDonHangGateway>();
            _mockChiTietDonHang = new Mock<IChiTietDonHangGateway>();
            _mockPhieuThu = new Mock<IPhieuThuGateway>();
            _mockDoanKhachDichVu = new Mock<IDoanKhachDichVuGateway>();
            _mockGiaoDichVi = new Mock<IBUS_GiaoDichVi>();
            _mockDoanKhach = new Mock<IBUS_DoanKhach>();

            _bus = new BUS_DatBan(
                _mockDatBan.Object, _mockBanAn.Object, _mockChiTietDatBan.Object,
                _mockDonHang.Object, _mockChiTietDonHang.Object, _mockPhieuThu.Object,
                _mockDoanKhachDichVu.Object, _mockGiaoDichVi.Object, _mockDoanKhach.Object
            );
        }
        #endregion

        // =
        // NHOM 1 — CRUD (Basic)
        // =

        #region CRUD
        [TestMethod]
        public void LoadDS_TraVeDungSoLuong()
        {
            _mockDatBan.Setup(x => x.LoadDS()).Returns(new List<ET_DatBan>
            {
                new ET_DatBan { Id = 1, TrangThai = "DaNhan" },
                new ET_DatBan { Id = 2, TrangThai = "DaDat" }
            });

            Assert.AreEqual(2, _bus.LoadDS().Count);
        }

        [TestMethod]
        public void LayTheoId_CoTonTai_TraVeDungSoKhach()
        {
            _mockDatBan.Setup(x => x.LayTheoId(1)).Returns(new ET_DatBan { Id = 1, SoLuongKhach = 4 });

            var result = _bus.LayTheoId(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.SoLuongKhach);
        }
        #endregion

        // =
        // NHOM 2 — MO BAN (Check-in tai cho)
        // =

        #region MoBan
        [TestMethod]
        public void MoBan_DanhSachRong_TuChoiNgay()
        {
            var result = _bus.MoBan(1, new List<int>(), 4);

            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.Contains("chọn ít nhất 1 bàn"));
        }

        [TestMethod]
        public void MoBan_DanhSachNull_TuChoiNgay()
        {
            var result = _bus.MoBan(1, null, 4);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void MoBan_BanDangSuDung_ChongOverbooking_TuChoi()
        {
            // QUY TAC NGHIEP VU: Ban co TrangThai != "Trong" -> KHONG cho mo
            SetupMoBanInfra();

            _mockBanAn.Setup(x => x.LayTheoId(5)).Returns(new ET_BanAn
            {
                Id = 5,
                TrangThai = AppConstants.TrangThaiBanAn.DangSuDung // Co nguoi ngoi
            });

            var result = _bus.MoBan(1, new List<int> { 5 }, 4);

            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.Contains("không còn trống"));
            _mockBanAn.Verify(x => x.Sua(It.IsAny<ET_BanAn>()), Times.Never); // KHONG cap nhat ban
        }

        [TestMethod]
        public void MoBan_BanDaDat_ChongOverbooking_TuChoi()
        {
            SetupMoBanInfra();

            _mockBanAn.Setup(x => x.LayTheoId(5)).Returns(new ET_BanAn
            {
                Id = 5,
                TrangThai = AppConstants.TrangThaiBanAn.DaDat // Nguoi khac da dat truoc
            });

            var result = _bus.MoBan(1, new List<int> { 5 }, 4);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void MoBan_BanTrong_ThanhCong_VerifyBanChuyenDangSuDung()
        {
            SetupMoBanInfra();

            _mockBanAn.Setup(x => x.LayTheoId(5)).Returns(new ET_BanAn
            {
                Id = 5,
                TrangThai = AppConstants.TrangThaiBanAn.Trong
            });
            _mockBanAn.Setup(x => x.Sua(It.IsAny<ET_BanAn>())).Returns(true);

            var result = _bus.MoBan(1, new List<int> { 5 }, 4);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(100, result.Data); // Tra ve idDonHang = 100
            // VERIFY SIDE EFFECT: Ban PHAI chuyen sang DangSuDung
            _mockBanAn.Verify(x => x.Sua(It.Is<ET_BanAn>(b => b.TrangThai == AppConstants.TrangThaiBanAn.DangSuDung)), Times.Once);
        }

        [TestMethod]
        public void MoBan_NhieuBan_1BanKhongTrong_ToanBoDeuBiTuChoi()
        {
            // QUY TAC: Neu 1 trong cac ban da bi chiem -> KHONG mo bat ky ban nao (Transaction Rollback)
            SetupMoBanInfra();

            _mockBanAn.Setup(x => x.LayTheoId(1)).Returns(new ET_BanAn { Id = 1, TrangThai = AppConstants.TrangThaiBanAn.Trong });
            _mockBanAn.Setup(x => x.LayTheoId(2)).Returns(new ET_BanAn { Id = 2, TrangThai = AppConstants.TrangThaiBanAn.DangSuDung }); // BAN NAY BI CHIEM

            var result = _bus.MoBan(1, new List<int> { 1, 2 }, 8);

            Assert.IsFalse(result.IsSuccess);
        }
        #endregion

        // =
        // NHOM 3 — DAT BAN TRUOC (Reservation)
        // =

        #region DatBanTruoc
        [TestMethod]
        public void DatBanTruoc_KhongChonBan_TuChoi()
        {
            var result = _bus.DatBanTruoc(1, null, new ET_DatBan(), "");
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void DatBanTruoc_DsRong_TuChoi()
        {
            var result = _bus.DatBanTruoc(1, new List<int>(), new ET_DatBan(), "");
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void DatBanTruoc_BanDaDat_ChongOverbooking_TuChoi()
        {
            // QUY TAC NGHIEP VU: CHONG OVERBOOKING
            SetupMoBanInfra();

            _mockBanAn.Setup(x => x.LayTheoId(10)).Returns(new ET_BanAn
            {
                Id = 10,
                TrangThai = AppConstants.TrangThaiBanAn.DaDat
            });

            var info = new ET_DatBan
            {
                TenNguoiDat = "Nguyen Van A",
                SoDienThoai = "0901234567",
                SoLuongKhach = 6,
                ThoiGianDenDuKien = DateTime.Now.AddHours(2),
                TienCoc = 0
            };

            var result = _bus.DatBanTruoc(1, new List<int> { 10 }, info, "");

            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.Contains("không còn trống"));
        }

        [TestMethod]
        public void DatBanTruoc_BanTrong_ThanhCong_VerifyBanChuyenDaDat()
        {
            SetupMoBanInfra();

            _mockBanAn.Setup(x => x.LayTheoId(10)).Returns(new ET_BanAn
            {
                Id = 10,
                TrangThai = AppConstants.TrangThaiBanAn.Trong
            });
            _mockBanAn.Setup(x => x.Sua(It.IsAny<ET_BanAn>())).Returns(true);

            var info = new ET_DatBan
            {
                TenNguoiDat = "Nguyen Van B",
                SoDienThoai = "0909876543",
                SoLuongKhach = 4,
                ThoiGianDenDuKien = DateTime.Now.AddHours(1),
                TienCoc = 0
            };

            var result = _bus.DatBanTruoc(1, new List<int> { 10 }, info, "");

            Assert.IsTrue(result.IsSuccess);
            // VERIFY SIDE EFFECT: Ban PHAI chuyen sang DaDat
            _mockBanAn.Verify(x => x.Sua(It.Is<ET_BanAn>(b => b.TrangThai == AppConstants.TrangThaiBanAn.DaDat)), Times.Once);
        }
        #endregion

        // =
        // NHOM 4 — NHAN BAN (State Machine: DaDat -> DaNhan)
        // =

        #region NhanBan
        [TestMethod]
        public void NhanBan_PhieuKhongTonTai_TuChoi()
        {
            _mockDatBan.Setup(x => x.LayTheoId(999)).Returns((ET_DatBan)null);

            var result = _bus.NhanBan(999);

            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.Contains("không tồn tại"));
        }

        [TestMethod]
        public void NhanBan_TrangThaiDaNhan_KhongChoNhanLai()
        {
            // QUY TAC: Chi nhan khi TrangThai == "DaDat". Moi trang thai khac deu bi tu choi.
            _mockDatBan.Setup(x => x.LayTheoId(1)).Returns(new ET_DatBan
            {
                Id = 1,
                TrangThai = "DaNhan" // Da nhan roi
            });

            var result = _bus.NhanBan(1);

            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.Contains("không hợp lệ"));
        }

        [TestMethod]
        public void NhanBan_TrangThaiHoanTat_KhongChoNhanLai()
        {
            _mockDatBan.Setup(x => x.LayTheoId(1)).Returns(new ET_DatBan
            {
                Id = 1,
                TrangThai = "HoanTat"
            });

            var result = _bus.NhanBan(1);

            Assert.IsFalse(result.IsSuccess);
        }
        #endregion

        // =
        // NHOM 5 — HUY DAT BAN
        // =

        #region HuyDatBan
        [TestMethod]
        public void HuyDatBan_PhieuKhongTonTai_TuChoi()
        {
            _mockDatBan.Setup(x => x.LayTheoId(999)).Returns((ET_DatBan)null);

            var result = _bus.HuyDatBan(999);

            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.Contains("không tồn tại"));
        }
        #endregion

        // =
        // NHOM 6 — THEM MON / PHU THU / XOA MON (Quan ly Bill)
        // =

        #region Quan ly Bill
        [TestMethod]
        public void ThemMon_IdDonHangBang0_TuChoi()
        {
            var result = _bus.ThemMon(0, 1, 1, 10000);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void ThemMon_SoLuongBang0_TuChoi()
        {
            var result = _bus.ThemMon(1, 1, 0, 10000);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void ThemMon_ThanhCong_VerifyTongTienCapNhat()
        {
            ET_DonHang dhCapture = null;

            _mockChiTietDonHang.Setup(x => x.Them(It.IsAny<ET_ChiTietDonHang>())).Returns(true);
            _mockChiTietDonHang.Setup(x => x.LoadByDonHang(100)).Returns(new List<ET_ChiTietDonHang>
            {
                new ET_ChiTietDonHang { SoLuong = 1, DonGiaThucTe = 50000 },
                new ET_ChiTietDonHang { SoLuong = 2, DonGiaThucTe = 30000 } // Mon moi them
            });
            _mockDonHang.Setup(x => x.LayTheoId(100)).Returns(new ET_DonHang { Id = 100, TongTien = 50000 });
            _mockDonHang.Setup(x => x.Sua(It.IsAny<ET_DonHang>()))
                .Callback<ET_DonHang>(dh => dhCapture = dh)
                .Returns(true);

            var result = _bus.ThemMon(100, 1, 2, 30000);

            Assert.IsTrue(result.IsSuccess);
            // ASSERT NGHIEP VU: TongTien = (1*50000) + (2*30000) = 110000
            Assert.IsNotNull(dhCapture);
            Assert.AreEqual(110000m, dhCapture.TongTien, "TongTien phai duoc cap nhat lai bang tong cac chi tiet");
        }

        [TestMethod]
        public void ThemPhuThu_SoTienAm_TuChoi()
        {
            var result = _bus.ThemPhuThu(1, "Test", -100);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void ThemPhuThu_SoTienBang0_TuChoi()
        {
            var result = _bus.ThemPhuThu(1, "Test", 0);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void XoaMon_ThanhCong_VerifyGatewayDuocGoi()
        {
            _mockChiTietDonHang.Setup(x => x.Xoa(5)).Returns(true);
            _mockChiTietDonHang.Setup(x => x.LoadByDonHang(100)).Returns(new List<ET_ChiTietDonHang>());
            _mockDonHang.Setup(x => x.LayTheoId(100)).Returns(new ET_DonHang { Id = 100 });
            _mockDonHang.Setup(x => x.Sua(It.IsAny<ET_DonHang>())).Returns(true);

            var result = _bus.XoaMon(5, 100);

            Assert.IsTrue(result.IsSuccess);
            _mockChiTietDonHang.Verify(x => x.Xoa(5), Times.Once);
        }

        [TestMethod]
        public void XoaMon_DBThatBai_TraVeLoi()
        {
            _mockChiTietDonHang.Setup(x => x.Xoa(999)).Returns(false);

            var result = _bus.XoaMon(999, 100);

            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.Contains("Không thể xóa"));
        }
        #endregion

        // =
        // NHOM 7 — HUY DON HANG
        // =

        #region HuyDonHang
        [TestMethod]
        public void HuyDonHang_KhongTonTai_TuChoi()
        {
            _mockDonHang.Setup(x => x.LayTheoId(999)).Returns((ET_DonHang)null);

            var result = _bus.HuyDonHang(999);

            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.Contains("không tồn tại"));
        }

        [TestMethod]
        public void HuyDonHang_ThanhCong_VerifyTrangThaiChuyenDaHuy()
        {
            ET_DonHang dhCapture = null;

            _mockDonHang.Setup(x => x.LayTheoId(1)).Returns(new ET_DonHang { Id = 1, TrangThai = "DangXuLy" });
            _mockDonHang.Setup(x => x.Sua(It.IsAny<ET_DonHang>()))
                .Callback<ET_DonHang>(dh => dhCapture = dh)
                .Returns(true);

            var result = _bus.HuyDonHang(1);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(dhCapture);
            Assert.AreEqual(AppConstants.TrangThaiDonHang.DaHuy, dhCapture.TrangThai, "Trang thai phai chuyen sang DaHuy");
        }
        #endregion

        // =
        // NHOM 8 — TINH TONG COC (Phep toan tai chinh)
        // =

        #region TinhTongCoc
        [TestMethod]
        public void TinhTongCoc_100kVa50k_ChiTinhBanLienQuan_TraVe150k()
        {
            // QUY TAC: Chi tinh coc cua cac DatBan MA co IdChiTietDonHang thuoc DonHang nay
            // Cac DatBan khac (Id = 99) khong lien quan -> KHONG tinh
            _mockChiTietDonHang.Setup(x => x.LoadByDonHang(100)).Returns(new List<ET_ChiTietDonHang>
            {
                new ET_ChiTietDonHang { Id = 1 },
                new ET_ChiTietDonHang { Id = 2 }
            });
            _mockDatBan.Setup(x => x.LoadDS()).Returns(new List<ET_DatBan>
            {
                new ET_DatBan { IdChiTietDonHang = 1, TienCoc = 100000 },
                new ET_DatBan { IdChiTietDonHang = 2, TienCoc = 50000 },
                new ET_DatBan { IdChiTietDonHang = 99, TienCoc = 200000 } // KHONG lien quan -> KHONG tinh
            });

            var result = _bus.TinhTongCoc(100);

            Assert.AreEqual(150000m, result, "Chi tinh 100k + 50k = 150k. Ko tinh 200k cua ban khac");
        }

        [TestMethod]
        public void TinhTongCoc_KhongCoCoc_TraVe0()
        {
            _mockChiTietDonHang.Setup(x => x.LoadByDonHang(100)).Returns(new List<ET_ChiTietDonHang>
            {
                new ET_ChiTietDonHang { Id = 1 }
            });
            _mockDatBan.Setup(x => x.LoadDS()).Returns(new List<ET_DatBan>
            {
                new ET_DatBan { IdChiTietDonHang = 1, TienCoc = 0 }
            });

            Assert.AreEqual(0m, _bus.TinhTongCoc(100));
        }
        #endregion

        // =
        // HELPERS
        // =

        #region Helpers
        private void SetupMoBanInfra()
        {
            _mockDonHang.Setup(x => x.ThemVaLayId(It.IsAny<ET_DonHang>())).Returns(100);
            _mockChiTietDonHang.Setup(x => x.ThemVaLayId(It.IsAny<ET_ChiTietDonHang>())).Returns(200);
            _mockDatBan.Setup(x => x.ThemVaLayId(It.IsAny<ET_DatBan>())).Returns(300);
            _mockChiTietDatBan.Setup(x => x.Them(It.IsAny<ET_ChiTietDatBan>())).Returns(true);
        }
        #endregion
    }
}

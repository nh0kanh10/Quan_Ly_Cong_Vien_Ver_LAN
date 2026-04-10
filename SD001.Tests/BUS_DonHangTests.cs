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
    public class BUS_DonHangTests
    {
        #region Setup & Mock Fields
        private Mock<IDonHangGateway> _mockDonHang;
        private Mock<IChiTietDonHangGateway> _mockChiTiet;
        private Mock<IPhieuThuGateway> _mockPhieuThu;
        private Mock<ISanPhamGateway> _mockSanPham;
        private Mock<IComboGateway> _mockCombo;
        private Mock<IComboChiTietGateway> _mockComboChiTiet;
        private Mock<ISanPhamVeGateway> _mockSanPhamVe;
        private Mock<IViDienTuGateway> _mockVi;
        private Mock<IGiaoDichViGateway> _mockGiaoDich;
        private Mock<IKhachHangGateway> _mockKhachHang;
        private BUS_DonHang _bus;

        [TestInitialize]
        public void Setup()
        {
            _mockDonHang = new Mock<IDonHangGateway>();
            _mockChiTiet = new Mock<IChiTietDonHangGateway>();
            _mockPhieuThu = new Mock<IPhieuThuGateway>();
            _mockSanPham = new Mock<ISanPhamGateway>();
            _mockCombo = new Mock<IComboGateway>();
            _mockComboChiTiet = new Mock<IComboChiTietGateway>();
            _mockSanPhamVe = new Mock<ISanPhamVeGateway>();
            _mockVi = new Mock<IViDienTuGateway>();
            _mockGiaoDich = new Mock<IGiaoDichViGateway>();
            _mockKhachHang = new Mock<IKhachHangGateway>();

            _bus = new BUS_DonHang(
                _mockDonHang.Object, _mockChiTiet.Object, _mockPhieuThu.Object,
                _mockSanPham.Object, _mockCombo.Object, _mockComboChiTiet.Object,
                _mockSanPhamVe.Object, _mockVi.Object, _mockGiaoDich.Object,
                _mockKhachHang.Object
            );
        }
        #endregion

        // =========================================================================
        // NHOM 1 — CRUD (Basic Smoke Tests)
        // =========================================================================

        #region CRUD
        [TestMethod]
        public void LoadDS_TraVeDanhSach_DemDungSoLuong()
        {
            _mockDonHang.Setup(x => x.LoadDS()).Returns(new List<ET_DonHang>
            {
                new ET_DonHang { Id = 1, MaCode = "DH-001" },
                new ET_DonHang { Id = 2, MaCode = "DH-002" }
            });

            var result = _bus.LoadDS();

            Assert.AreEqual(2, result.Count);
            _mockDonHang.Verify(x => x.LoadDS(), Times.Once); // Dam bao gateway THAT SU duoc goi
        }

        [TestMethod]
        public void GetById_CoTonTai_TraVeDonHangDungMaCode()
        {
            _mockDonHang.Setup(x => x.LayTheoId(1)).Returns(new ET_DonHang { Id = 1, MaCode = "DH-001" });

            var result = _bus.GetById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("DH-001", result.MaCode);
        }

        [TestMethod]
        public void GetById_KhongTonTai_TraVeNull()
        {
            _mockDonHang.Setup(x => x.LayTheoId(999)).Returns((ET_DonHang)null);
            Assert.IsNull(_bus.GetById(999));
        }

        [TestMethod]
        public void ThemDonHang_DuLieuNull_TraVeLoiVaKhongGoiGateway()
        {
            var result = _bus.ThemDonHang(null);

            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.Contains("không hợp lệ"));
            _mockDonHang.Verify(x => x.Them(It.IsAny<ET_DonHang>()), Times.Never); // KHONG duoc goi DB
        }

        [TestMethod]
        public void ThemDonHang_ThanhCong_VerifyGatewayDuocGoi()
        {
            _mockDonHang.Setup(x => x.Them(It.IsAny<ET_DonHang>())).Returns(true);

            var dh = new ET_DonHang { MaCode = "DH-TEST" };
            var result = _bus.ThemDonHang(dh);

            Assert.IsTrue(result.IsSuccess);
            _mockDonHang.Verify(x => x.Them(dh), Times.Once); // Verify gateway NHAN dung object
        }

        [TestMethod]
        public void ThemDonHang_DBThatBai_TraVeErrorVoiThongBao()
        {
            _mockDonHang.Setup(x => x.Them(It.IsAny<ET_DonHang>())).Returns(false);

            var result = _bus.ThemDonHang(new ET_DonHang { MaCode = "DH-TEST" });

            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.Contains("Không thể thêm"));
        }
        #endregion

        // =========================================================================
        // NHOM 2 — VALIDATION (Input Guards)
        // =========================================================================

        #region Validation Input
        [TestMethod]
        public void ThemDonHangVaChiTiet_DonHangNull_TuChoiNgay()
        {
            var result = _bus.ThemDonHangVaChiTiet(null, new List<ET_ChiTietDonHang>(), null);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void ThemDonHangVaChiTiet_ChiTietListNull_TuChoiNgay()
        {
            var result = _bus.ThemDonHangVaChiTiet(new ET_DonHang(), null, null);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void ThemDonHangVaChiTiet_ChiTietListRong_TuChoiNgay()
        {
            var result = _bus.ThemDonHangVaChiTiet(new ET_DonHang(), new List<ET_ChiTietDonHang>(), null);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void ThemDonHangVaChiTiet_ChiTietChuaItemNull_TraVeLoiCuThe()
        {
            var chiTietList = new List<ET_ChiTietDonHang> { null };
            var result = _bus.ThemDonHangVaChiTiet(new ET_DonHang(), chiTietList, null);

            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.Contains("rỗng"));
        }

        [TestMethod]
        public void ThemDonHangVaChiTiet_ChiTietCoGiaNhungKhongGanSanPhamHayCombo_ChanNgay()
        {
            // QUY TAC: Neu DonGiaGoc > 0 nhung khong lien ket SP hay Combo -> TU CHOI
            // Muc dich: Ngan chan hacker gui du lieu gia
            var chiTietList = new List<ET_ChiTietDonHang>
            {
                new ET_ChiTietDonHang { IdSanPham = null, IdCombo = null, DonGiaGoc = 50000, SoLuong = 1 }
            };

            _mockSanPham.Setup(x => x.LoadDS()).Returns(new List<ET_SanPham>());
            _mockCombo.Setup(x => x.LoadDS()).Returns(new List<ET_Combo>());

            var result = _bus.ThemDonHangVaChiTiet(new ET_DonHang(), chiTietList, null);

            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.Contains("không gắn sản phẩm"));
        }

        [TestMethod]
        public void ThemDonHangVaChiTiet_SoLuongBang0_AnNinh_TuChoi()
        {
            var chiTietList = new List<ET_ChiTietDonHang>
            {
                new ET_ChiTietDonHang { IdSanPham = 1, SoLuong = 0, DonGiaGoc = 10000 }
            };

            SetupSanPhamCache(new ET_SanPham { Id = 1, DonGia = 10000, LoaiSanPham = "DoAn" });

            var result = _bus.ThemDonHangVaChiTiet(new ET_DonHang(), chiTietList, null);

            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.Contains("Số lượng"));
        }

        [TestMethod]
        public void ThemDonHangVaChiTiet_SoLuongAm_AnNinh_TuChoi()
        {
            var chiTietList = new List<ET_ChiTietDonHang>
            {
                new ET_ChiTietDonHang { IdSanPham = 1, SoLuong = -3, DonGiaGoc = 10000 }
            };

            SetupSanPhamCache(new ET_SanPham { Id = 1, DonGia = 10000, LoaiSanPham = "DoAn" });

            var result = _bus.ThemDonHangVaChiTiet(new ET_DonHang(), chiTietList, null);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void ThemDonHangVaChiTiet_GiamGia999999VuotTongTien10000_ChanAnNinh()
        {
            // QUY TAC: TienGiamGia KHONG duoc > TongTien va KHONG duoc am
            var donHang = new ET_DonHang { TienGiamGia = 999999 };
            var chiTietList = new List<ET_ChiTietDonHang>
            {
                new ET_ChiTietDonHang { IdSanPham = 1, SoLuong = 1, DonGiaGoc = 10000 }
            };

            SetupSanPhamCache(new ET_SanPham { Id = 1, DonGia = 10000, LoaiSanPham = "DoAn" });

            var result = _bus.ThemDonHangVaChiTiet(donHang, chiTietList, null);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void ThemDonHangVaChiTiet_GiamGiaAm_ChanAnNinh()
        {
            var donHang = new ET_DonHang { TienGiamGia = -5000 };
            var chiTietList = new List<ET_ChiTietDonHang>
            {
                new ET_ChiTietDonHang { IdSanPham = 1, SoLuong = 1, DonGiaGoc = 10000 }
            };

            SetupSanPhamCache(new ET_SanPham { Id = 1, DonGia = 10000, LoaiSanPham = "DoAn" });

            var result = _bus.ThemDonHangVaChiTiet(donHang, chiTietList, null);

            Assert.IsFalse(result.IsSuccess);
        }
        #endregion

        // =========================================================================
        // NHOM 3 — CHONG HACK GIA (Toan ven tai chinh)
        // =========================================================================

        #region Anti-Price-Hack (Server-side Recalculation)
        [TestMethod]
        public void ChongHackGia_ClientGui1Dong_ServerTinhLai50000()
        {
            // KICH BAN: Hacker sua request, gui DonGiaGoc = 1 dong
            // KY VONG: Server phai quet bang SanPham, cap nhat lai DonGiaGoc = 50000 (gia that)
            var donHang = new ET_DonHang { TienGiamGia = 0 };
            var chiTietList = new List<ET_ChiTietDonHang>
            {
                new ET_ChiTietDonHang { IdSanPham = 1, SoLuong = 2, DonGiaGoc = 1 } // HACK: 1 dong
            };

            SetupSanPhamCache(new ET_SanPham { Id = 1, DonGia = 50000, LoaiSanPham = "DoAn" }); // GIA THAT: 50k
            _mockComboChiTiet.Setup(x => x.LoadDS()).Returns(new List<ET_ComboChiTiet>());
            _mockDonHang.Setup(x => x.ThemVaLayId(It.IsAny<ET_DonHang>())).Returns(100);
            _mockChiTiet.Setup(x => x.Them(It.IsAny<ET_ChiTietDonHang>())).Returns(true);

            _bus.ThemDonHangVaChiTiet(donHang, chiTietList, null);

            // ASSERT NGHIEP VU:
            Assert.AreEqual(50000m, chiTietList[0].DonGiaGoc, "DonGiaGoc phai la 50000 (tu DB), KHONG PHAI 1 (tu client)");
            Assert.AreEqual(100000m, donHang.TongTien, "TongTien = 50000 * 2 = 100000. KHONG PHAI 1 * 2 = 2");
        }

        [TestMethod]
        public void ChongHackGia_ComboGia200k_ClientGui0_ServerTinhLai()
        {
            var donHang = new ET_DonHang { TienGiamGia = 0 };
            var chiTietList = new List<ET_ChiTietDonHang>
            {
                new ET_ChiTietDonHang { IdCombo = 10, SoLuong = 1, DonGiaGoc = 0 } 
            };

            _mockSanPham.Setup(x => x.LoadDS()).Returns(new List<ET_SanPham>());
            _mockCombo.Setup(x => x.LoadDS()).Returns(new List<ET_Combo>
            {
                new ET_Combo { Id = 10, Gia = 200000 } // GIA THAT: 200k
            });
            _mockComboChiTiet.Setup(x => x.LoadDS()).Returns(new List<ET_ComboChiTiet>());
            _mockDonHang.Setup(x => x.ThemVaLayId(It.IsAny<ET_DonHang>())).Returns(100);
            _mockChiTiet.Setup(x => x.Them(It.IsAny<ET_ChiTietDonHang>())).Returns(true);

            _bus.ThemDonHangVaChiTiet(donHang, chiTietList, null);

            Assert.AreEqual(200000m, chiTietList[0].DonGiaGoc, "DonGia combo phai la 200k tu DB");
            Assert.AreEqual(200000m, donHang.TongTien, "TongTien = 200k * 1 = 200k");
        }

        [TestMethod]
        public void ChongHackGia_NhieuSanPham_TinhDungTong()
        {
            var donHang = new ET_DonHang { TienGiamGia = 0 };
            var chiTietList = new List<ET_ChiTietDonHang>
            {
                new ET_ChiTietDonHang { IdSanPham = 1, SoLuong = 3, DonGiaGoc = 999 },
                new ET_ChiTietDonHang { IdSanPham = 2, SoLuong = 2, DonGiaGoc = 999 }
            };

            _mockSanPham.Setup(x => x.LoadDS()).Returns(new List<ET_SanPham>
            {
                new ET_SanPham { Id = 1, DonGia = 30000, LoaiSanPham = "DoAn" },
                new ET_SanPham { Id = 2, DonGia = 45000, LoaiSanPham = "DoUong" }
            });
            _mockCombo.Setup(x => x.LoadDS()).Returns(new List<ET_Combo>());
            _mockComboChiTiet.Setup(x => x.LoadDS()).Returns(new List<ET_ComboChiTiet>());
            _mockDonHang.Setup(x => x.ThemVaLayId(It.IsAny<ET_DonHang>())).Returns(100);
            _mockChiTiet.Setup(x => x.Them(It.IsAny<ET_ChiTietDonHang>())).Returns(true);

            _bus.ThemDonHangVaChiTiet(donHang, chiTietList, null);

            // 30000 * 3 + 45000 * 2 = 90000 + 90000 = 180000
            Assert.AreEqual(180000m, donHang.TongTien, "TongTien phai bang tong cac (Gia_DB * SoLuong)");
        }
        #endregion

        // =========================================================================
        // NHOM 4 — THANH TOAN BANG VI (RFID Wallet)
        // =========================================================================

        #region Thanh toan Vi dien tu
        [TestMethod]
        public void ThanhToanBangVi_DonHangNull_TuChoiNgay()
        {
            var result = _bus.ThanhToanBangVi(null, new List<ET_ChiTietDonHang>(), 1, 1);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void ThanhToanBangVi_ChiTietRong_TuChoiNgay()
        {
            var result = _bus.ThanhToanBangVi(new ET_DonHang(), new List<ET_ChiTietDonHang>(), 1, 1);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void ThanhToanBangVi_KhachChuaCoVi_TraVeLoiCuThe()
        {
            var donHang = new ET_DonHang { TienGiamGia = 0 };
            var chiTiet = new List<ET_ChiTietDonHang>
            {
                new ET_ChiTietDonHang { IdSanPham = 1, SoLuong = 1, DonGiaGoc = 10000 }
            };

            SetupSanPhamCache(new ET_SanPham { Id = 1, DonGia = 10000, LoaiSanPham = "DoAn" });
            _mockSanPhamVe.Setup(x => x.LayTheoIdSanPham(It.IsAny<int>())).Returns((ET_SanPham_Ve)null);
            _mockVi.Setup(x => x.LayTheoKhachHang(It.IsAny<int>())).Returns((ET_ViDienTu)null);

            var result = _bus.ThanhToanBangVi(donHang, chiTiet, 1, 1);

            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.Contains("chưa có ví"));
        }

        [TestMethod]
        public void ThanhToanBangVi_SoDu100k_Can500k_TraVeLoiVoiSoTienCuThe()
        {
            var donHang = new ET_DonHang { TienGiamGia = 0 };
            var chiTiet = new List<ET_ChiTietDonHang>
            {
                new ET_ChiTietDonHang { IdSanPham = 1, SoLuong = 1, DonGiaGoc = 500000 }
            };

            SetupSanPhamCache(new ET_SanPham { Id = 1, DonGia = 500000, LoaiSanPham = "DoAn" });
            _mockSanPhamVe.Setup(x => x.LayTheoIdSanPham(It.IsAny<int>())).Returns((ET_SanPham_Ve)null);
            _mockVi.Setup(x => x.LayTheoKhachHang(1)).Returns(new ET_ViDienTu { Id = 1, SoDuKhaDung = 100000 });

            var result = _bus.ThanhToanBangVi(donHang, chiTiet, 1, 1);

            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.Contains("không đủ"));
        }

        [TestMethod]
        public void ThanhToanBangVi_SoLuongAm_AnNinh_TuChoi()
        {
            var donHang = new ET_DonHang { TienGiamGia = 0 };
            var chiTiet = new List<ET_ChiTietDonHang>
            {
                new ET_ChiTietDonHang { IdSanPham = 1, SoLuong = -5, DonGiaGoc = 10000 }
            };

            SetupSanPhamCache(new ET_SanPham { Id = 1, DonGia = 10000, LoaiSanPham = "DoAn" });

            var result = _bus.ThanhToanBangVi(donHang, chiTiet, 1, 1);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void ThanhToanBangVi_GiamGiaAm_AnNinh_TuChoi()
        {
            var donHang = new ET_DonHang { TienGiamGia = -10000 };
            var chiTiet = new List<ET_ChiTietDonHang>
            {
                new ET_ChiTietDonHang { IdSanPham = 1, SoLuong = 1, DonGiaGoc = 50000 }
            };

            SetupSanPhamCache(new ET_SanPham { Id = 1, DonGia = 50000, LoaiSanPham = "DoAn" });
            _mockSanPhamVe.Setup(x => x.LayTheoIdSanPham(It.IsAny<int>())).Returns((ET_SanPham_Ve)null);

            var result = _bus.ThanhToanBangVi(donHang, chiTiet, 1, 1);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void ThanhToanBangVi_ThanhToanThucTe0Dong_TuChoi()
        {
            // Gia 50k, giam gia 50k -> thuc thu = 0 -> phai tu choi
            var donHang = new ET_DonHang { TienGiamGia = 50000 };
            var chiTiet = new List<ET_ChiTietDonHang>
            {
                new ET_ChiTietDonHang { IdSanPham = 1, SoLuong = 1, DonGiaGoc = 50000 }
            };

            SetupSanPhamCache(new ET_SanPham { Id = 1, DonGia = 50000, LoaiSanPham = "DoAn" });
            _mockSanPhamVe.Setup(x => x.LayTheoIdSanPham(It.IsAny<int>())).Returns((ET_SanPham_Ve)null);

            var result = _bus.ThanhToanBangVi(donHang, chiTiet, 1, 1);

            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.Contains("lớn hơn 0"));
        }
        #endregion

        // =========================================================================
        // NHOM 5 — SUA / XOA DON HANG
        // =========================================================================

        #region Sua & Xoa
        [TestMethod]
        public void SuaDonHang_Null_TuChoiVaKhongGoiDB()
        {
            var result = _bus.SuaDonHang(null);

            Assert.IsFalse(result.IsSuccess);
            _mockDonHang.Verify(x => x.Sua(It.IsAny<ET_DonHang>()), Times.Never);
        }

        [TestMethod]
        public void SuaDonHang_ThanhCong_VerifyGatewayDuocGoi()
        {
            _mockDonHang.Setup(x => x.Sua(It.IsAny<ET_DonHang>())).Returns(true);
            var dh = new ET_DonHang { Id = 1 };

            var result = _bus.SuaDonHang(dh);

            Assert.IsTrue(result.IsSuccess);
            _mockDonHang.Verify(x => x.Sua(dh), Times.Once);
        }

        [TestMethod]
        public void XoaDonHang_ThanhCong_VerifyGoiDungId()
        {
            _mockDonHang.Setup(x => x.Xoa(42)).Returns(true);

            var result = _bus.XoaDonHang(42);

            Assert.IsTrue(result.IsSuccess);
            _mockDonHang.Verify(x => x.Xoa(42), Times.Once);
        }

        [TestMethod]
        public void XoaDonHang_DBThatBai_TraVeError()
        {
            _mockDonHang.Setup(x => x.Xoa(999)).Returns(false);

            var result = _bus.XoaDonHang(999);

            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.Contains("Không thể xóa"));
        }
        #endregion

        // =========================================================================
        // HELPER — Tao mock data chung
        // =========================================================================

        #region Helpers
        private void SetupSanPhamCache(params ET_SanPham[] sanPhams)
        {
            _mockSanPham.Setup(x => x.LoadDS()).Returns(sanPhams.ToList());
            _mockCombo.Setup(x => x.LoadDS()).Returns(new List<ET_Combo>());
        }
        #endregion
    }
}

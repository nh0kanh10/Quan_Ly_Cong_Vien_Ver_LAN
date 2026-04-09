using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace BUS
{
    public class BUS_PhieuThuChi
    {
        private readonly IPhieuThuGateway _phieuThuGateway;
        private readonly IPhieuChiGateway _phieuChiGateway;
        private static BUS_PhieuThuChi instance;
        public static BUS_PhieuThuChi Instance
        {
            get { return instance ?? (instance = new BUS_PhieuThuChi()); }
        }
        public BUS_PhieuThuChi() : this(new DefaultPhieuThuGateway(), new DefaultPhieuChiGateway()) { }
        public BUS_PhieuThuChi(IPhieuThuGateway phieuThuGateway, IPhieuChiGateway phieuChiGateway)
        {
            _phieuThuGateway = phieuThuGateway;
            _phieuChiGateway = phieuChiGateway;
        }

        public List<ET_PhieuThu> LoadPhieuThu(DateTime fromDate, DateTime toDate)
        {
            return _phieuThuGateway.LoadDS()
                .Where(x => x.ThoiGian >= fromDate && x.ThoiGian <= toDate)
                .OrderByDescending(x => x.ThoiGian)
                .ToList();
        }

        public List<ET_PhieuChi> LoadPhieuChi(DateTime fromDate, DateTime toDate)
        {
            return _phieuChiGateway.LoadDS()
                .Where(x => x.ThoiGian >= fromDate && x.ThoiGian <= toDate)
                .OrderByDescending(x => x.ThoiGian)
                .ToList();
        }
    }
}

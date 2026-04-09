using System.Collections.Generic;
using System.Linq;
using ET;

namespace BUS
{
    public class BUS_TheRFID
    {
        private readonly ITheRfidGateway _theRfidGateway;
        private static BUS_TheRFID instance;
        public static BUS_TheRFID Instance
        {
            get { return instance ?? (instance = new BUS_TheRFID()); }
        }
        public BUS_TheRFID() : this(new DefaultTheRfidGateway()) { }
        public BUS_TheRFID(ITheRfidGateway theRfidGateway) { _theRfidGateway = theRfidGateway; }

        public List<ET_TheRFID> LoadDS()
        {
            return _theRfidGateway.LoadDS();
        }

        public OperationResult KhoaThe(string maRfid)
        {
            if (string.IsNullOrWhiteSpace(maRfid)) return OperationResult.Failed("Mã RFID không hợp lệ.");
            var the = _theRfidGateway.LayTheoId(maRfid);
            if (the == null) return OperationResult.Failed("Không tìm thấy thẻ RFID.");
            the.TrangThai = AppConstants.TrangThaiTheRfid.Locked;
            the.NgayHuy = System.DateTime.Now;
            return _theRfidGateway.Sua(the) ? OperationResult.Success() : OperationResult.Failed("Không thể khóa thẻ.");
        }

        public OperationResult MoThe(string maRfid)
        {
            if (string.IsNullOrWhiteSpace(maRfid)) return OperationResult.Failed("Mã RFID không hợp lệ.");
            var the = _theRfidGateway.LayTheoId(maRfid);
            if (the == null) return OperationResult.Failed("Không tìm thấy thẻ RFID.");
            the.TrangThai = AppConstants.TrangThaiTheRfid.Active;
            the.NgayHuy = null;
            return _theRfidGateway.Sua(the) ? OperationResult.Success() : OperationResult.Failed("Không thể mở khóa thẻ.");
        }

        public List<ET_TheRFID> TimKiem(string kw)
        {
            var ds = LoadDS();
            if (string.IsNullOrWhiteSpace(kw)) return ds;
            kw = kw.ToLowerInvariant();
            return ds.Where(x => x.MaRfid.ToLowerInvariant().Contains(kw)).ToList();
        }
    }
}

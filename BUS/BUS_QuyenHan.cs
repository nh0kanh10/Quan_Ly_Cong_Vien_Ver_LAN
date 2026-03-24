using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace BUS
{
    public class BUS_QuyenHan
    {
        private readonly IQuyenHanGateway _gateway;
        private readonly IPhanQuyenGateway _phanQuyenGateway;

        private static BUS_QuyenHan instance;
        public static BUS_QuyenHan Instance
        {
            get
            {
                if (instance == null) instance = new BUS_QuyenHan();
                return instance;
            }
        }

        public BUS_QuyenHan() : this(new DefaultQuyenHanGateway(), new DefaultPhanQuyenGateway()) { }
        public BUS_QuyenHan(IQuyenHanGateway gw, IPhanQuyenGateway pqGw)
        {
            _gateway = gw;
            _phanQuyenGateway = pqGw;
        }

        public List<ET_QuyenHan> LoadDS() => _gateway.LoadDS();

        // Cache ma quyen theo VaiTro để UI gọi nhiều lần không hit DB.
        private readonly Dictionary<int, HashSet<string>> _permissionsByRole = new Dictionary<int, HashSet<string>>();
        private HashSet<string> _knownPermissionKeys;
        private bool _knownPermissionKeysLoaded = false;

        private void EnsureKnownPermissionKeysLoaded()
        {
            if (_knownPermissionKeysLoaded) return;

            var all = _gateway.LoadDS();
            _knownPermissionKeys = new HashSet<string>(all.Select(x => x.MaQuyen));
            _knownPermissionKeysLoaded = true;
        }

        public HashSet<string> GetPermissionsByRole(int idVaiTro)
        {
            if (_permissionsByRole.TryGetValue(idVaiTro, out var cached))
                return cached;

            // Lấy ánh xạ IdQuyen -> MaQuyen
            var allQuyen = _gateway.LoadDS();
            var idToKey = allQuyen.ToDictionary(x => x.Id, x => x.MaQuyen);

            // Lấy danh sách IdQuyen mà VaiTro có trong PhanQuyen
            var listPQ = _phanQuyenGateway.LoadDS().Where(x => x.IdVaiTro == idVaiTro);
            var keys = listPQ
                .Select(x => x.IdQuyen)
                .Where(idQuyen => idToKey.ContainsKey(idQuyen))
                .Select(idQuyen => idToKey[idQuyen])
                .ToHashSet();

            _permissionsByRole[idVaiTro] = keys;
            return keys;
        }

        /// <summary>
        /// Trả về quyền cho UI.
        /// Lưu ý: để tránh "ẩn chức năng" khi DB chưa seed đủ MaQuyen,
        /// nếu MaQuyen không tồn tại trong QuyenHan thì trả true (compat mode).
        /// </summary>
        public bool HasPermission(int idVaiTro, string maQuyen)
        {
            EnsureKnownPermissionKeysLoaded();

            // Compat mode: MaQuyen chưa tồn tại trong DB seed => cho phép để không phá UI.
            if (!_knownPermissionKeys.Contains(maQuyen))
                return true;

            return GetPermissionsByRole(idVaiTro).Contains(maQuyen);
        }

        public void ClearCache()
        {
            _permissionsByRole.Clear();
            _knownPermissionKeysLoaded = false;
        }
    }
}

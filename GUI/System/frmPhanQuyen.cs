using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BUS;
using ET;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using FontAwesome.Sharp;

namespace GUI
{
    public partial class frmPhanQuyen : Form, IBaseForm
    {
        private class PermissionNode
        {
            public int Id { get; set; }
            public int ParentId { get; set; }
            public string DisplayName { get; set; }
            public string MaQuyen { get; set; }
            public bool IsModule { get; set; }
        }

        public frmPhanQuyen()
        {
            InitializeComponent();
            if (ThemeManager.IsInDesignMode(this)) return;

            InitIcons();
            ApplyStyles();
            ApplyPermissions();
            LoadInitialData();
        }

        public void ApplyPermissions()
        {
            // Only Boss/Admin can manage permissions. 
            // Mock logic: check current user role.
        }

        public void ApplyStyles()
        {
            ThemeManager.ApplyTheme(this);
            
            treeQuyen.OptionsView.ShowCheckBoxes = true;
            treeQuyen.OptionsBehavior.AllowRecursiveNodeChecking = true;
        }

        public void InitIcons()
        {
            btnLuu.Image = IconHelper.GetBitmap(IconChar.Save, Color.White, 20);
            btnLamMoi.Image = IconHelper.GetBitmap(IconChar.Sync, Color.White, 20);
        }



        private void TreeQuyen_AfterCheckNode(object sender, NodeEventArgs e)
        {
            // Standard DevExpress recursive checking is handled by AllowRecursiveNodeChecking = true
        }

        private void LoadInitialData()
        {
            // Load Roles
            lbVaiTro.DataSource = BUS_VaiTro.Instance.LoadDS();
            lbVaiTro.DisplayMember = "TenVaiTro";
            lbVaiTro.ValueMember = "Id";

            // Load Tree
            BuildPermissionTree();
        }

        private void BuildPermissionTree()
        {
            var allQuyen = BUS_QuyenHan.Instance.LoadDS();
            var nodes = new List<PermissionNode>();

            // Root Node
            nodes.Add(new PermissionNode { Id = -1, ParentId = -2, DisplayName = "Tất cả quyền hệ thống", MaQuyen = "ROOT", IsModule = true });

            // Group by Module (Parsing MaQuyen: VIEW_STAFF -> STAFF)
            var modules = allQuyen.Select(q => GetModuleName(q.MaQuyen)).Distinct();
            int moduleId = 1000;
            
            foreach (var mod in modules)
            {
                int currentModId = moduleId++;
                nodes.Add(new PermissionNode { 
                    Id = currentModId, 
                    ParentId = -1, 
                    DisplayName = TranslateModule(mod), 
                    MaQuyen = mod, 
                    IsModule = true 
                });

                var modQuyens = allQuyen.Where(q => GetModuleName(q.MaQuyen) == mod);
                foreach (var q in modQuyens)
                {
                    nodes.Add(new PermissionNode { 
                        Id = q.Id, 
                        ParentId = currentModId, 
                        DisplayName = TranslateAction(q.MaQuyen), 
                        MaQuyen = q.MaQuyen, 
                        IsModule = false 
                    });
                }
            }

            treeQuyen.DataSource = nodes;
            treeQuyen.KeyFieldName = "Id";
            treeQuyen.ParentFieldName = "ParentId";
            treeQuyen.ExpandAll();
        }

        private string GetModuleName(string maQuyen)
        {
            if (maQuyen.Contains("_")) return maQuyen.Substring(maQuyen.IndexOf("_") + 1);
            return maQuyen;
        }

        private string TranslateModule(string mod)
        {
            switch (mod.ToUpper())
            {
                case "STAFF": return "Quản lý Nhân viên";
                case "POS": return "Bán hàng & Hóa đơn";
                case "INVENTORY": return "Kho hàng & Sản phẩm";
                case "PROMOTION": return "Marketing & Khuyến mãi";
                case "RFID": return "Thẻ RFID & Ví";
                case "REPORT": return "Báo cáo & Thống kê";
                default: return mod;
            }
        }

        private string TranslateAction(string maQuyen)
        {
            if (maQuyen.StartsWith("VIEW_")) return "Xem dữ liệu";
            if (maQuyen.StartsWith("MANAGE_")) return "Quản lý (Thêm/Sửa/Xóa)";
            return maQuyen;
        }

        private void LbVaiTro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbVaiTro.SelectedValue is int roleId)
            {
                LoadRolePermissions(roleId);
            }
        }

        private void LoadRolePermissions(int roleId)
        {
            var myQuyenIds = BUS_PhanQuyen.Instance.LayQuyenTheoVaiTro(roleId);
            
            treeQuyen.BeginUpdate();
            treeQuyen.UncheckAll();
            
            foreach (TreeListNode node in treeQuyen.GetNodeList())
            {
                var data = treeQuyen.GetRow(node.Id) as PermissionNode;
                if (data != null && !data.IsModule && myQuyenIds.Contains(data.Id))
                {
                    node.Checked = true;
                }
            }
            treeQuyen.EndUpdate();
        }

        private void frmPhanQuyen_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            LoadInitialData();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!(lbVaiTro.SelectedValue is int roleId)) return;

            var selectedIds = new List<int>();
            foreach (TreeListNode node in treeQuyen.GetNodeList())
            {
                var data = treeQuyen.GetRow(node.Id) as PermissionNode;
                // Only save leaves (actual permissions), not modules/root
                if (node.Checked && data != null && !data.IsModule)
                {
                    selectedIds.Add(data.Id);
                }
            }

            if (BUS_PhanQuyen.Instance.CapNhatQuyen(roleId, selectedIds))
            {
                TDCMessageBox.Show("Cập nhật phân quyền thành công!", "Thông báo");
            }
            else
            {
                TDCMessageBox.Show("Cập nhật thất bại!", "Lỗi");
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadInitialData();
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BUS.Services.NhanSu;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using ET.Models.NhanSu;
using GUI.Infrastructure;

namespace GUI.Modules.HeThong
{
    public partial class frmPhanQuyen : XtraForm
    {
        // Class cục bộ dùng để bind dữ liệu lên TreeList
        private class PermissionNode
        {
            public int Id { get; set; }
            public int ParentId { get; set; }
            public string DisplayName { get; set; }
            public string NhomQuyen { get; set; }
            public bool IsModule { get; set; }
        }

        #region Khởi tạo và tải dữ liệu

        public frmPhanQuyen()
        {
            InitializeComponent();
            ApplyLanguage();
            LoadData();
        }

        /// <summary>
        /// Gán text đa ngôn ngữ cho form
        /// </summary>
        private void ApplyLanguage()
        {
            this.Text = LanguageManager.GetString("FRM_PHANQUYEN_TITLE") ?? "Thiết lập Phân Quyền";
            gbVaiTro.Text = LanguageManager.GetString("LBL_VAITRO_TITLE") ?? "Chọn Vai Trò";
            gbQuyenHan.Text = LanguageManager.GetString("LBL_QUYENHAN_TITLE") ?? "Phân quyền (Hierarchy Tree)";
            btnLuu.Text = LanguageManager.GetString("BTN_LUU") ?? "LƯU THAY ĐỔI";
            btnLamMoi.Text = LanguageManager.GetString("BTN_LAMMOI") ?? "LÀM MỚI";
            
            // Text tĩnh cho cột TreeList
            colDisplayName.Caption = LanguageManager.GetString("COL_DANHMUC_QUYEN") ?? "Danh mục quyền";
        }

        private void LoadData()
        {
            LoadVaiTro();
            BuildPermissionTree();
        }

        private void LoadVaiTro()
        {
            var result = BUS_VaiTro.Instance.LoadDS();
            if (result.Success)
            {
                var list = result.Data as List<ET_VaiTro>;
                lbVaiTro.DataSource = list;
                lbVaiTro.DisplayMember = "TenVaiTro";
                lbVaiTro.ValueMember = "Id";
            }
        }

        /// <summary>
        /// Tạo cây phân quyền dựa trên dữ liệu NhomQuyen từ CSDL
        /// </summary>
        private void BuildPermissionTree()
        {
            var result = BUS_QuyenHan.Instance.LoadDS();
            if (!result.Success) return;

            var allQuyen = result.Data as List<ET_QuyenHan>;
            if (allQuyen == null) return;

            var nodes = new List<PermissionNode>();

            // Nhóm quyền theo NhomQuyen
            // Sử dụng một Dictionary hoặc phân tách các NhomQuyen
            var danhSachNhom = allQuyen
                .Where(q => !string.IsNullOrEmpty(q.NhomQuyen))
                .Select(q => q.NhomQuyen)
                .Distinct()
                .ToList();

            // Nhóm các quyền không có NhomQuyen vào nhóm "Khác"
            if (allQuyen.Any(q => string.IsNullOrEmpty(q.NhomQuyen)))
            {
                danhSachNhom.Add("Khác");
            }

            int moduleId = -100; // ID âm để không trùng với ID Quyền từ database (ID quyền là số dương)

            foreach (var nhom in danhSachNhom)
            {
                int currentModId = moduleId--;
                
                // Thêm Node cha đại diện cho Nhóm Quyền (VD: Quản lý Bán Hàng)
                nodes.Add(new PermissionNode
                {
                    Id = currentModId,
                    ParentId = 0, // Root
                    DisplayName = LanguageManager.GetString("MOD_" + nhom.ToUpper()) ?? nhom,
                    NhomQuyen = nhom,
                    IsModule = true
                });

                // Lọc các quyền thuộc nhóm này
                var cacQuyenTrongNhom = nhom == "Khác" 
                    ? allQuyen.Where(q => string.IsNullOrEmpty(q.NhomQuyen)) 
                    : allQuyen.Where(q => q.NhomQuyen == nhom);

                foreach (var q in cacQuyenTrongNhom)
                {
                    // Thêm Node con đại diện cho Quyền cụ thể (VD: Thêm Đơn Hàng)
                    nodes.Add(new PermissionNode
                    {
                        Id = q.Id, // ID gốc từ database
                        ParentId = currentModId,
                        DisplayName = LanguageManager.GetString("PQ_QUYEN_" + q.MaQuyen) ?? q.TenQuyen,
                        NhomQuyen = nhom,
                        IsModule = false
                    });
                }
            }

            treeQuyen.DataSource = nodes;
            treeQuyen.KeyFieldName = "Id";
            treeQuyen.ParentFieldName = "ParentId";
            treeQuyen.ExpandAll();
        }

        private void LoadRolePermissions(int roleId)
        {
            var result = BUS_PhanQuyen.Instance.LayQuyenTheoVaiTro(roleId);
            if (!result.Success) return;

            var myQuyenIds = result.Data as List<int> ?? new List<int>();

            treeQuyen.BeginUpdate();
            // Bỏ tick tất cả trước khi gán mới
            treeQuyen.UncheckAll();

            foreach (TreeListNode node in treeQuyen.GetNodeList())
            {
                var data = treeQuyen.GetRow(node.Id) as PermissionNode;
                // Nếu node này không phải Node Cha (không phải module) và ID nằm trong danh sách quyền của Role
                if (data != null && !data.IsModule && myQuyenIds.Contains(data.Id))
                {
                    node.Checked = true;
                }
            }
            treeQuyen.EndUpdate();
        }

        #endregion

        #region Xử lý sự kiện (Click, SelectedChanged...)

        private void LbVaiTro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbVaiTro.SelectedValue is int roleId)
            {
                LoadRolePermissions(roleId);
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (!(lbVaiTro.SelectedValue is int roleId)) return;

            var selectedIds = new List<int>();

            // Quét qua các node để lấy ID các node đã được Check
            foreach (TreeListNode node in treeQuyen.GetNodeList())
            {
                var data = treeQuyen.GetRow(node.Id) as PermissionNode;
                if (node.Checked && data != null && !data.IsModule)
                {
                    selectedIds.Add(data.Id);
                }
            }

            var result = BUS_PhanQuyen.Instance.CapNhatQuyen(roleId, selectedIds);
            
            if (result.Success)
            {
                XtraMessageBox.Show(
                    LanguageManager.GetString(result.Message) ?? "Cập nhật thành công",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(
                    LanguageManager.GetString(result.Message) ?? result.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}

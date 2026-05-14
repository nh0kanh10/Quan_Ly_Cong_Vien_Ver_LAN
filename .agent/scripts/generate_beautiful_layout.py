import re

def generate_beautiful_layout():
    filepath = r"c:\Users\ADMIN\Desktop\DaiNamNew\GUI\Modules\BanHang\ucLuuTru_Main.Designer.cs"
    with open(filepath, "r", encoding="utf-8") as f:
        content = f.read()

    # Define the new components
    components_decl = """        private DevExpress.XtraLayout.LayoutControl layoutControlRight;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupRight;
        private DevExpress.XtraLayout.LayoutControlGroup groupInfo;
        private DevExpress.XtraLayout.LayoutControlItem lciRoomTitle;
        private DevExpress.XtraLayout.LayoutControlItem lciStatusBadge;
        private DevExpress.XtraLayout.LayoutControlGroup groupKhachHang;
        private DevExpress.XtraLayout.LayoutControlItem lciGuestInfo;
        private DevExpress.XtraLayout.LayoutControlGroup groupThoiGian;
        private DevExpress.XtraLayout.LayoutControlItem lciTimeInfo;
        private DevExpress.XtraLayout.LayoutControlGroup groupTaiChinh;
        private DevExpress.XtraLayout.LayoutControlItem lciFinancialInfo;
        private DevExpress.XtraLayout.LayoutControlGroup groupThaoTac;
        private DevExpress.XtraLayout.LayoutControlItem lciBtnCheckOut;
        private DevExpress.XtraLayout.LayoutControlItem lciBtnMinibar;
        private DevExpress.XtraLayout.LayoutControlItem lciBtnDoiPhong;
        private DevExpress.XtraLayout.LayoutControlItem lciBtnGiaHan;
        private DevExpress.XtraLayout.LayoutControlItem lciBtnThemDichVu;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceRight;"""

    # We need to replace the old layout control declarations (which I previously added)
    # The old ones start with 'private DevExpress.XtraLayout.LayoutControl layoutControlRight;'
    # and end with 'private DevExpress.XtraLayout.EmptySpaceItem emptySpaceRight;'
    old_decl_regex = r'private DevExpress\.XtraLayout\.LayoutControl layoutControlRight;.*?private DevExpress\.XtraLayout\.EmptySpaceItem emptySpaceRight;'
    content = re.sub(old_decl_regex, components_decl, content, flags=re.DOTALL)

    # Replace Initialization
    old_init_regex = r'this\.layoutControlRight = new DevExpress\.XtraLayout\.LayoutControl\(\);.*?this\.emptySpaceRight = new DevExpress\.XtraLayout\.EmptySpaceItem\(\);'
    new_init = """this.layoutControlRight = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroupRight = new DevExpress.XtraLayout.LayoutControlGroup();
            this.groupInfo = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciRoomTitle = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciStatusBadge = new DevExpress.XtraLayout.LayoutControlItem();
            this.groupKhachHang = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciGuestInfo = new DevExpress.XtraLayout.LayoutControlItem();
            this.groupThoiGian = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciTimeInfo = new DevExpress.XtraLayout.LayoutControlItem();
            this.groupTaiChinh = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciFinancialInfo = new DevExpress.XtraLayout.LayoutControlItem();
            this.groupThaoTac = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciBtnCheckOut = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciBtnMinibar = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciBtnDoiPhong = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciBtnGiaHan = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciBtnThemDichVu = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceRight = new DevExpress.XtraLayout.EmptySpaceItem();"""
    content = re.sub(old_init_regex, new_init, content, flags=re.DOTALL)

    # Replace BeginInit
    old_begin_init_regex = r'\(\(System\.ComponentModel\.ISupportInitialize\)\(this\.layoutControlRight\)\)\.BeginInit\(\);.*?\(\(System\.ComponentModel\.ISupportInitialize\)\(this\.emptySpaceRight\)\)\.BeginInit\(\);'
    new_begin_init = """((System.ComponentModel.ISupportInitialize)(this.layoutControlRight)).BeginInit();
            this.layoutControlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciRoomTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciStatusBadge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupKhachHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciGuestInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupThoiGian)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciTimeInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupTaiChinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciFinancialInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupThaoTac)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnCheckOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnMinibar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnDoiPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnGiaHan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnThemDichVu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceRight)).BeginInit();"""
    content = re.sub(old_begin_init_regex, new_begin_init, content, flags=re.DOTALL)

    # Replace EndInit
    old_end_init_regex = r'\(\(System\.ComponentModel\.ISupportInitialize\)\(this\.layoutControlGroupRight\)\)\.EndInit\(\);.*?\(\(System\.ComponentModel\.ISupportInitialize\)\(this\.emptySpaceRight\)\)\.EndInit\(\);\s*\(\(System\.ComponentModel\.ISupportInitialize\)\(this\.layoutControlRight\)\)\.EndInit\(\);\s*this\.layoutControlRight\.ResumeLayout\(false\);'
    new_end_init = """((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciRoomTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciStatusBadge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupKhachHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciGuestInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupThoiGian)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciTimeInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupTaiChinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciFinancialInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupThaoTac)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnCheckOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnMinibar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnDoiPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnGiaHan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnThemDichVu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlRight)).EndInit();
            this.layoutControlRight.ResumeLayout(false);"""
    content = re.sub(old_end_init_regex, new_end_init, content, flags=re.DOTALL)

    # Replace layout definition block
    old_layout_def_regex = r'// layoutControlRight.*?// emptySpaceRight.*?this\.emptySpaceRight\.TextSize = new System\.Drawing\.Size\(0, 0\);'
    new_layout_def = """// layoutControlRight
            // 
            this.layoutControlRight.Controls.Add(this.lblRoomTitle);
            this.layoutControlRight.Controls.Add(this.lblStatusBadge);
            this.layoutControlRight.Controls.Add(this.lblGuestInfo);
            this.layoutControlRight.Controls.Add(this.lblTimeInfo);
            this.layoutControlRight.Controls.Add(this.lblFinancialInfo);
            this.layoutControlRight.Controls.Add(this.btnActionCheckOut);
            this.layoutControlRight.Controls.Add(this.btnActionMinibar);
            this.layoutControlRight.Controls.Add(this.btnActionDoiPhong);
            this.layoutControlRight.Controls.Add(this.btnActionGiaHan);
            this.layoutControlRight.Controls.Add(this.btnActionThemDichVu);
            this.layoutControlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlRight.Location = new System.Drawing.Point(0, 0);
            this.layoutControlRight.Name = "layoutControlRight";
            this.layoutControlRight.Root = this.layoutControlGroupRight;
            this.layoutControlRight.Size = new System.Drawing.Size(350, 652);
            this.layoutControlRight.TabIndex = 0;
            // 
            // layoutControlGroupRight
            // 
            this.layoutControlGroupRight.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupRight.GroupBordersVisible = false;
            this.layoutControlGroupRight.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.groupInfo,
            this.groupKhachHang,
            this.groupThoiGian,
            this.groupTaiChinh,
            this.groupThaoTac,
            this.emptySpaceRight});
            this.layoutControlGroupRight.Name = "layoutControlGroupRight";
            this.layoutControlGroupRight.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 10, 10);
            this.layoutControlGroupRight.Size = new System.Drawing.Size(350, 652);
            this.layoutControlGroupRight.TextVisible = false;
            // 
            // groupInfo
            // 
            this.groupInfo.GroupBordersVisible = false;
            this.groupInfo.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciRoomTitle,
            this.lciStatusBadge});
            this.groupInfo.Location = new System.Drawing.Point(0, 0);
            this.groupInfo.Name = "groupInfo";
            this.groupInfo.Size = new System.Drawing.Size(330, 60);
            this.groupInfo.TextVisible = false;
            // 
            // lciRoomTitle
            // 
            this.lciRoomTitle.Control = this.lblRoomTitle;
            this.lciRoomTitle.Location = new System.Drawing.Point(0, 0);
            this.lciRoomTitle.Name = "lciRoomTitle";
            this.lciRoomTitle.Size = new System.Drawing.Size(330, 34);
            this.lciRoomTitle.TextSize = new System.Drawing.Size(0, 0);
            this.lciRoomTitle.TextVisible = false;
            // 
            // lciStatusBadge
            // 
            this.lciStatusBadge.Control = this.lblStatusBadge;
            this.lciStatusBadge.Location = new System.Drawing.Point(0, 34);
            this.lciStatusBadge.Name = "lciStatusBadge";
            this.lciStatusBadge.Size = new System.Drawing.Size(330, 26);
            this.lciStatusBadge.TextSize = new System.Drawing.Size(0, 0);
            this.lciStatusBadge.TextVisible = false;
            // 
            // groupKhachHang
            // 
            this.groupKhachHang.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupKhachHang.AppearanceGroup.Options.UseFont = true;
            this.groupKhachHang.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciGuestInfo});
            this.groupKhachHang.Location = new System.Drawing.Point(0, 60);
            this.groupKhachHang.Name = "groupKhachHang";
            this.groupKhachHang.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 10, 10);
            this.groupKhachHang.Size = new System.Drawing.Size(330, 80);
            this.groupKhachHang.Text = "KHÁCH HÀNG";
            // 
            // lciGuestInfo
            // 
            this.lciGuestInfo.Control = this.lblGuestInfo;
            this.lciGuestInfo.Location = new System.Drawing.Point(0, 0);
            this.lciGuestInfo.Name = "lciGuestInfo";
            this.lciGuestInfo.Size = new System.Drawing.Size(304, 31);
            this.lciGuestInfo.TextSize = new System.Drawing.Size(0, 0);
            this.lciGuestInfo.TextVisible = false;
            // 
            // groupThoiGian
            // 
            this.groupThoiGian.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupThoiGian.AppearanceGroup.Options.UseFont = true;
            this.groupThoiGian.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciTimeInfo});
            this.groupThoiGian.Location = new System.Drawing.Point(0, 140);
            this.groupThoiGian.Name = "groupThoiGian";
            this.groupThoiGian.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 10, 10);
            this.groupThoiGian.Size = new System.Drawing.Size(330, 80);
            this.groupThoiGian.Text = "THỜI GIAN";
            // 
            // lciTimeInfo
            // 
            this.lciTimeInfo.Control = this.lblTimeInfo;
            this.lciTimeInfo.Location = new System.Drawing.Point(0, 0);
            this.lciTimeInfo.Name = "lciTimeInfo";
            this.lciTimeInfo.Size = new System.Drawing.Size(304, 31);
            this.lciTimeInfo.TextSize = new System.Drawing.Size(0, 0);
            this.lciTimeInfo.TextVisible = false;
            // 
            // groupTaiChinh
            // 
            this.groupTaiChinh.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupTaiChinh.AppearanceGroup.Options.UseFont = true;
            this.groupTaiChinh.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciFinancialInfo});
            this.groupTaiChinh.Location = new System.Drawing.Point(0, 220);
            this.groupTaiChinh.Name = "groupTaiChinh";
            this.groupTaiChinh.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 10, 10);
            this.groupTaiChinh.Size = new System.Drawing.Size(330, 80);
            this.groupTaiChinh.Text = "TÀI CHÍNH";
            // 
            // lciFinancialInfo
            // 
            this.lciFinancialInfo.Control = this.lblFinancialInfo;
            this.lciFinancialInfo.Location = new System.Drawing.Point(0, 0);
            this.lciFinancialInfo.Name = "lciFinancialInfo";
            this.lciFinancialInfo.Size = new System.Drawing.Size(304, 31);
            this.lciFinancialInfo.TextSize = new System.Drawing.Size(0, 0);
            this.lciFinancialInfo.TextVisible = false;
            // 
            // groupThaoTac
            // 
            this.groupThaoTac.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupThaoTac.AppearanceGroup.Options.UseFont = true;
            this.groupThaoTac.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciBtnCheckOut,
            this.lciBtnMinibar,
            this.lciBtnDoiPhong,
            this.lciBtnGiaHan,
            this.lciBtnThemDichVu});
            this.groupThaoTac.Location = new System.Drawing.Point(0, 300);
            this.groupThaoTac.Name = "groupThaoTac";
            this.groupThaoTac.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 10, 10);
            this.groupThaoTac.Size = new System.Drawing.Size(330, 180);
            this.groupThaoTac.Text = "THAO TÁC NHANH";
            // 
            // lciBtnCheckOut
            // 
            this.lciBtnCheckOut.Control = this.btnActionCheckOut;
            this.lciBtnCheckOut.Location = new System.Drawing.Point(0, 0);
            this.lciBtnCheckOut.MinSize = new System.Drawing.Size(1, 48);
            this.lciBtnCheckOut.Name = "lciBtnCheckOut";
            this.lciBtnCheckOut.Size = new System.Drawing.Size(304, 48);
            this.lciBtnCheckOut.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciBtnCheckOut.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 5);
            this.lciBtnCheckOut.TextSize = new System.Drawing.Size(0, 0);
            this.lciBtnCheckOut.TextVisible = false;
            // 
            // lciBtnMinibar
            // 
            this.lciBtnMinibar.Control = this.btnActionMinibar;
            this.lciBtnMinibar.Location = new System.Drawing.Point(0, 48);
            this.lciBtnMinibar.MinSize = new System.Drawing.Size(1, 40);
            this.lciBtnMinibar.Name = "lciBtnMinibar";
            this.lciBtnMinibar.Size = new System.Drawing.Size(152, 40);
            this.lciBtnMinibar.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciBtnMinibar.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 5, 0, 5);
            this.lciBtnMinibar.TextSize = new System.Drawing.Size(0, 0);
            this.lciBtnMinibar.TextVisible = false;
            // 
            // lciBtnDoiPhong
            // 
            this.lciBtnDoiPhong.Control = this.btnActionDoiPhong;
            this.lciBtnDoiPhong.Location = new System.Drawing.Point(152, 48);
            this.lciBtnDoiPhong.MinSize = new System.Drawing.Size(1, 40);
            this.lciBtnDoiPhong.Name = "lciBtnDoiPhong";
            this.lciBtnDoiPhong.Size = new System.Drawing.Size(152, 40);
            this.lciBtnDoiPhong.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciBtnDoiPhong.Spacing = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 5);
            this.lciBtnDoiPhong.TextSize = new System.Drawing.Size(0, 0);
            this.lciBtnDoiPhong.TextVisible = false;
            // 
            // lciBtnGiaHan
            // 
            this.lciBtnGiaHan.Control = this.btnActionGiaHan;
            this.lciBtnGiaHan.Location = new System.Drawing.Point(0, 88);
            this.lciBtnGiaHan.MinSize = new System.Drawing.Size(1, 40);
            this.lciBtnGiaHan.Name = "lciBtnGiaHan";
            this.lciBtnGiaHan.Size = new System.Drawing.Size(152, 43);
            this.lciBtnGiaHan.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciBtnGiaHan.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 5, 0, 0);
            this.lciBtnGiaHan.TextSize = new System.Drawing.Size(0, 0);
            this.lciBtnGiaHan.TextVisible = false;
            // 
            // lciBtnThemDichVu
            // 
            this.lciBtnThemDichVu.Control = this.btnActionThemDichVu;
            this.lciBtnThemDichVu.Location = new System.Drawing.Point(152, 88);
            this.lciBtnThemDichVu.MinSize = new System.Drawing.Size(1, 40);
            this.lciBtnThemDichVu.Name = "lciBtnThemDichVu";
            this.lciBtnThemDichVu.Size = new System.Drawing.Size(152, 43);
            this.lciBtnThemDichVu.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciBtnThemDichVu.Spacing = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0);
            this.lciBtnThemDichVu.TextSize = new System.Drawing.Size(0, 0);
            this.lciBtnThemDichVu.TextVisible = false;
            // 
            // emptySpaceRight
            // 
            this.emptySpaceRight.AllowHotTrack = false;
            this.emptySpaceRight.Location = new System.Drawing.Point(0, 480);
            this.emptySpaceRight.Name = "emptySpaceRight";
            this.emptySpaceRight.Size = new System.Drawing.Size(330, 152);
            this.emptySpaceRight.TextSize = new System.Drawing.Size(0, 0);"""
    
    content = re.sub(old_layout_def_regex, new_layout_def, content, flags=re.DOTALL)
    
    # We also want to widen the panelRight so that 50/50 buttons don't get squished
    # In splitContainerControl1.Panel2, it is docked. But splitContainerControl1.SplitterPosition is currently 953 out of 1200.
    # So right panel is 247px. Let's make it 350px. So SplitterPosition = 1200 - 350 = 850
    content = content.replace("this.splitContainerControl1.SplitterPosition = 953;", "this.splitContainerControl1.SplitterPosition = 850;")

    # We also want to rename `btnActionThemDichVu` text to "In Bill Tạm" or keep it "Thêm Dịch Vụ" but the text says "In bill tạm"
    # Wait, the user mockup says: `[ Thêm Minibar ] [ Đổi phòng ]` on one row, and `[ Gia hạn ] [ In bill tạm ]` on another row.
    content = content.replace('this.btnActionThemDichVu.Text = "Thêm Dịch Vụ";', 'this.btnActionThemDichVu.Text = "In Bill Tạm";')

    with open(filepath, "w", encoding="utf-8") as f:
        f.write(content)

if __name__ == "__main__":
    generate_beautiful_layout()

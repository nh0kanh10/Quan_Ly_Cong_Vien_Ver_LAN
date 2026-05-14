import re
import sys

def replace_panel_with_layout_control():
    filepath = r"c:\Users\ADMIN\Desktop\DaiNamNew\GUI\Modules\BanHang\ucLuuTru_Main.Designer.cs"
    with open(filepath, "r", encoding="utf-8") as f:
        content = f.read()

    # 1. Replace declarations
    content = content.replace("private DevExpress.XtraEditors.PanelControl panelRight;", 
        """private DevExpress.XtraLayout.LayoutControl layoutControlRight;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupRight;
        private DevExpress.XtraLayout.LayoutControlItem lciRoomTitle;
        private DevExpress.XtraLayout.LayoutControlItem lciStatusBadge;
        private DevExpress.XtraLayout.LayoutControlItem lciGuestInfo;
        private DevExpress.XtraLayout.LayoutControlItem lciTimeInfo;
        private DevExpress.XtraLayout.LayoutControlItem lciFinancialInfo;
        private DevExpress.XtraLayout.LayoutControlItem lciBtnCheckOut;
        private DevExpress.XtraLayout.LayoutControlItem lciBtnMinibar;
        private DevExpress.XtraLayout.LayoutControlItem lciBtnThemDichVu;
        private DevExpress.XtraLayout.LayoutControlItem lciBtnGiaHan;
        private DevExpress.XtraLayout.LayoutControlItem lciBtnDoiPhong;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceRight;""")

    # 2. Replace instantiation
    content = content.replace("this.panelRight = new DevExpress.XtraEditors.PanelControl();", 
        """this.layoutControlRight = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroupRight = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciRoomTitle = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciStatusBadge = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciGuestInfo = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciTimeInfo = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciFinancialInfo = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciBtnCheckOut = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciBtnMinibar = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciBtnThemDichVu = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciBtnGiaHan = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciBtnDoiPhong = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceRight = new DevExpress.XtraLayout.EmptySpaceItem();""")

    # 3. Replace BeginInit
    content = content.replace("((System.ComponentModel.ISupportInitialize)(this.panelRight)).BeginInit();",
        """((System.ComponentModel.ISupportInitialize)(this.layoutControlRight)).BeginInit();
            this.layoutControlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciRoomTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciStatusBadge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciGuestInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciTimeInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciFinancialInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnCheckOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnMinibar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnThemDichVu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnGiaHan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnDoiPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceRight)).BeginInit();""")

    # 4. Remove SuspendLayout of panelRight
    content = content.replace("this.panelRight.SuspendLayout();", "")

    # 5. SplitContainer assignment
    content = content.replace("this.splitContainerControl1.Panel2.Controls.Add(this.panelRight);", 
        "this.splitContainerControl1.Panel2.Controls.Add(this.layoutControlRight);")

    # 6. Replace properties of buttons to use layoutControlRight
    content = re.sub(r'this\.btnAction([A-Za-z]+)\.Location = new System\.Drawing\.Point\(\d+, \d+\);', 
                     r'this.btnAction\1.StyleController = this.layoutControlRight;', content)
    
    # 7. Replace properties of labels
    content = re.sub(r'this\.lbl([A-Za-z]+)\.Location = new System\.Drawing\.Point\(\d+, \d+\);', 
                     r'this.lbl\1.StyleController = this.layoutControlRight;', content)

    # 8. Replace panelRight definition
    panel_def = """            // panelRight
            // 
            this.panelRight.Controls.Add(this.btnActionCheckOut);
            this.panelRight.Controls.Add(this.btnActionMinibar);
            this.panelRight.Controls.Add(this.btnActionThemDichVu);
            this.panelRight.Controls.Add(this.btnActionGiaHan);
            this.panelRight.Controls.Add(this.btnActionDoiPhong);
            this.panelRight.Controls.Add(this.lblFinancialInfo);
            this.panelRight.Controls.Add(this.lblTimeInfo);
            this.panelRight.Controls.Add(this.lblGuestInfo);
            this.panelRight.Controls.Add(this.lblStatusBadge);
            this.panelRight.Controls.Add(this.lblRoomTitle);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(0, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Padding = new System.Windows.Forms.Padding(16);
            this.panelRight.Size = new System.Drawing.Size(237, 652);
            this.panelRight.TabIndex = 0;"""

    layout_def = """            // layoutControlRight
            // 
            this.layoutControlRight.Controls.Add(this.lblRoomTitle);
            this.layoutControlRight.Controls.Add(this.lblStatusBadge);
            this.layoutControlRight.Controls.Add(this.lblGuestInfo);
            this.layoutControlRight.Controls.Add(this.lblTimeInfo);
            this.layoutControlRight.Controls.Add(this.lblFinancialInfo);
            this.layoutControlRight.Controls.Add(this.btnActionCheckOut);
            this.layoutControlRight.Controls.Add(this.btnActionMinibar);
            this.layoutControlRight.Controls.Add(this.btnActionThemDichVu);
            this.layoutControlRight.Controls.Add(this.btnActionGiaHan);
            this.layoutControlRight.Controls.Add(this.btnActionDoiPhong);
            this.layoutControlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlRight.Location = new System.Drawing.Point(0, 0);
            this.layoutControlRight.Name = "layoutControlRight";
            this.layoutControlRight.Root = this.layoutControlGroupRight;
            this.layoutControlRight.Size = new System.Drawing.Size(237, 652);
            this.layoutControlRight.TabIndex = 0;
            // 
            // layoutControlGroupRight
            // 
            this.layoutControlGroupRight.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupRight.GroupBordersVisible = false;
            this.layoutControlGroupRight.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciRoomTitle,
            this.lciStatusBadge,
            this.lciGuestInfo,
            this.lciTimeInfo,
            this.lciFinancialInfo,
            this.lciBtnCheckOut,
            this.lciBtnMinibar,
            this.lciBtnThemDichVu,
            this.lciBtnGiaHan,
            this.lciBtnDoiPhong,
            this.emptySpaceRight});
            this.layoutControlGroupRight.Name = "layoutControlGroupRight";
            this.layoutControlGroupRight.Size = new System.Drawing.Size(237, 652);
            this.layoutControlGroupRight.TextVisible = false;
            // 
            // lciRoomTitle
            // 
            this.lciRoomTitle.Control = this.lblRoomTitle;
            this.lciRoomTitle.Location = new System.Drawing.Point(0, 0);
            this.lciRoomTitle.Name = "lciRoomTitle";
            this.lciRoomTitle.Size = new System.Drawing.Size(217, 34);
            this.lciRoomTitle.TextSize = new System.Drawing.Size(0, 0);
            this.lciRoomTitle.TextVisible = false;
            // 
            // lciStatusBadge
            // 
            this.lciStatusBadge.Control = this.lblStatusBadge;
            this.lciStatusBadge.Location = new System.Drawing.Point(0, 34);
            this.lciStatusBadge.Name = "lciStatusBadge";
            this.lciStatusBadge.Size = new System.Drawing.Size(217, 21);
            this.lciStatusBadge.TextSize = new System.Drawing.Size(0, 0);
            this.lciStatusBadge.TextVisible = false;
            // 
            // lciGuestInfo
            // 
            this.lciGuestInfo.Control = this.lblGuestInfo;
            this.lciGuestInfo.Location = new System.Drawing.Point(0, 55);
            this.lciGuestInfo.Name = "lciGuestInfo";
            this.lciGuestInfo.Size = new System.Drawing.Size(217, 44);
            this.lciGuestInfo.TextSize = new System.Drawing.Size(0, 0);
            this.lciGuestInfo.TextVisible = false;
            // 
            // lciTimeInfo
            // 
            this.lciTimeInfo.Control = this.lblTimeInfo;
            this.lciTimeInfo.Location = new System.Drawing.Point(0, 99);
            this.lciTimeInfo.Name = "lciTimeInfo";
            this.lciTimeInfo.Size = new System.Drawing.Size(217, 42);
            this.lciTimeInfo.TextSize = new System.Drawing.Size(0, 0);
            this.lciTimeInfo.TextVisible = false;
            // 
            // lciFinancialInfo
            // 
            this.lciFinancialInfo.Control = this.lblFinancialInfo;
            this.lciFinancialInfo.Location = new System.Drawing.Point(0, 141);
            this.lciFinancialInfo.Name = "lciFinancialInfo";
            this.lciFinancialInfo.Size = new System.Drawing.Size(217, 46);
            this.lciFinancialInfo.TextSize = new System.Drawing.Size(0, 0);
            this.lciFinancialInfo.TextVisible = false;
            // 
            // lciBtnCheckOut
            // 
            this.lciBtnCheckOut.Control = this.btnActionCheckOut;
            this.lciBtnCheckOut.Location = new System.Drawing.Point(0, 187);
            this.lciBtnCheckOut.MinSize = new System.Drawing.Size(1, 52);
            this.lciBtnCheckOut.Name = "lciBtnCheckOut";
            this.lciBtnCheckOut.Size = new System.Drawing.Size(217, 52);
            this.lciBtnCheckOut.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciBtnCheckOut.TextSize = new System.Drawing.Size(0, 0);
            this.lciBtnCheckOut.TextVisible = false;
            // 
            // lciBtnMinibar
            // 
            this.lciBtnMinibar.Control = this.btnActionMinibar;
            this.lciBtnMinibar.Location = new System.Drawing.Point(0, 239);
            this.lciBtnMinibar.MinSize = new System.Drawing.Size(1, 44);
            this.lciBtnMinibar.Name = "lciBtnMinibar";
            this.lciBtnMinibar.Size = new System.Drawing.Size(217, 44);
            this.lciBtnMinibar.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciBtnMinibar.TextSize = new System.Drawing.Size(0, 0);
            this.lciBtnMinibar.TextVisible = false;
            // 
            // lciBtnThemDichVu
            // 
            this.lciBtnThemDichVu.Control = this.btnActionThemDichVu;
            this.lciBtnThemDichVu.Location = new System.Drawing.Point(0, 283);
            this.lciBtnThemDichVu.MinSize = new System.Drawing.Size(1, 44);
            this.lciBtnThemDichVu.Name = "lciBtnThemDichVu";
            this.lciBtnThemDichVu.Size = new System.Drawing.Size(217, 44);
            this.lciBtnThemDichVu.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciBtnThemDichVu.TextSize = new System.Drawing.Size(0, 0);
            this.lciBtnThemDichVu.TextVisible = false;
            // 
            // lciBtnGiaHan
            // 
            this.lciBtnGiaHan.Control = this.btnActionGiaHan;
            this.lciBtnGiaHan.Location = new System.Drawing.Point(0, 327);
            this.lciBtnGiaHan.MinSize = new System.Drawing.Size(1, 44);
            this.lciBtnGiaHan.Name = "lciBtnGiaHan";
            this.lciBtnGiaHan.Size = new System.Drawing.Size(217, 44);
            this.lciBtnGiaHan.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciBtnGiaHan.TextSize = new System.Drawing.Size(0, 0);
            this.lciBtnGiaHan.TextVisible = false;
            // 
            // lciBtnDoiPhong
            // 
            this.lciBtnDoiPhong.Control = this.btnActionDoiPhong;
            this.lciBtnDoiPhong.Location = new System.Drawing.Point(0, 371);
            this.lciBtnDoiPhong.MinSize = new System.Drawing.Size(1, 44);
            this.lciBtnDoiPhong.Name = "lciBtnDoiPhong";
            this.lciBtnDoiPhong.Size = new System.Drawing.Size(217, 44);
            this.lciBtnDoiPhong.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciBtnDoiPhong.TextSize = new System.Drawing.Size(0, 0);
            this.lciBtnDoiPhong.TextVisible = false;
            // 
            // emptySpaceRight
            // 
            this.emptySpaceRight.AllowHotTrack = false;
            this.emptySpaceRight.Location = new System.Drawing.Point(0, 415);
            this.emptySpaceRight.Name = "emptySpaceRight";
            this.emptySpaceRight.Size = new System.Drawing.Size(217, 217);
            this.emptySpaceRight.TextSize = new System.Drawing.Size(0, 0);"""

    content = content.replace(panel_def, layout_def)

    # 9. Replace EndInit
    content = content.replace("((System.ComponentModel.ISupportInitialize)(this.panelRight)).EndInit();", 
        """((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciRoomTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciStatusBadge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciGuestInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciTimeInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciFinancialInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnCheckOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnMinibar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnThemDichVu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnGiaHan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnDoiPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlRight)).EndInit();
            this.layoutControlRight.ResumeLayout(false);""")

    content = content.replace("this.panelRight.ResumeLayout(false);", "")
    content = content.replace("this.panelRight.PerformLayout();", "")

    with open(filepath, "w", encoding="utf-8") as f:
        f.write(content)

if __name__ == "__main__":
    replace_panel_with_layout_control()

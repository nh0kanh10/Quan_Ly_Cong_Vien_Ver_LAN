import re

designer_path = r'c:\Users\ADMIN\Desktop\Quan_Ly_Cong_Vien_Ver_1.2\GUI\Staff\frmNhanVien.Designer.cs'
with open(designer_path, 'r', encoding='utf-8') as f:
    code = f.read()

# Add tab pages to tabControlDetails
tab_controls_add = '''            this.tabControlDetails.Controls.Add(this.tabPagePersonalInfo);
            this.tabControlDetails.Controls.Add(this.tabPageAccount);
            this.tabControlDetails.Controls.Add(this.tabPageChungChi);
            this.tabControlDetails.Controls.Add(this.tabPageKyLuat);
            this.tabControlDetails.Controls.Add(this.tabPageDonXinNghi);
            this.tabControlDetails.Controls.Add(this.tabPageTaiNanLaoDong);'''
code = re.sub(r'this\.tabControlDetails\.Controls\.Add\(this\.tabPagePersonalInfo\);\s*this\.tabControlDetails\.Controls\.Add\(this\.tabPageAccount\);', tab_controls_add, code, count=1)

# Define tab pages
tab_pages_def = """            // tabPageChungChi
            this.tabPageChungChi.Controls.Add(this.gridChungChi);
            this.tabPageChungChi.Controls.Add(this.pnlTopChungChi);
            this.tabPageChungChi.Location = new System.Drawing.Point(4, 44);
            this.tabPageChungChi.Name = "tabPageChungChi";
            this.tabPageChungChi.Size = new System.Drawing.Size(407, 672);
            this.tabPageChungChi.TabIndex = 2;
            this.tabPageChungChi.Text = "Chứng chỉ";

            // pnlTopChungChi
            this.pnlTopChungChi.Controls.Add(this.btnThemCC);
            this.pnlTopChungChi.Controls.Add(this.btnXoaCC);
            this.pnlTopChungChi.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopChungChi.Height = 44;
            this.pnlTopChungChi.Name = "pnlTopChungChi";

            // btnThemCC
            this.btnThemCC.Location = new System.Drawing.Point(10, 5);
            this.btnThemCC.Size = new System.Drawing.Size(140, 34);
            this.btnThemCC.Text = "+ Thêm chứng chỉ";
            this.btnThemCC.FillColor = System.Drawing.Color.FromArgb(22, 163, 74);
            this.btnThemCC.Click += new System.EventHandler(this.BtnThemCC_Click);

            // btnXoaCC
            this.btnXoaCC.Location = new System.Drawing.Point(158, 5);
            this.btnXoaCC.Size = new System.Drawing.Size(70, 34);
            this.btnXoaCC.Text = "Xóa";
            this.btnXoaCC.FillColor = System.Drawing.Color.FromArgb(185, 28, 28);
            this.btnXoaCC.Click += new System.EventHandler(this.BtnXoaCC_Click);

            // gridChungChi
            this.gridChungChi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridChungChi.MainView = this.gridViewChungChi;
            this.gridChungChi.Name = "gridChungChi";
            this.gridChungChi.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.gridViewChungChi });

            // gridViewChungChi
            this.gridViewChungChi.GridControl = this.gridChungChi;
            this.gridViewChungChi.Name = "gridViewChungChi";
            this.gridViewChungChi.OptionsView.ShowGroupPanel = false;

            // tabPageKyLuat
            this.tabPageKyLuat.Controls.Add(this.gridKyLuat);
            this.tabPageKyLuat.Controls.Add(this.pnlTopKyLuat);
            this.tabPageKyLuat.Location = new System.Drawing.Point(4, 44);
            this.tabPageKyLuat.Name = "tabPageKyLuat";
            this.tabPageKyLuat.Size = new System.Drawing.Size(407, 672);
            this.tabPageKyLuat.TabIndex = 3;
            this.tabPageKyLuat.Text = "Kỷ luật";

            // pnlTopKyLuat
            this.pnlTopKyLuat.Controls.Add(this.btnThemKL);
            this.pnlTopKyLuat.Controls.Add(this.btnXoaKL);
            this.pnlTopKyLuat.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopKyLuat.Height = 44;
            this.pnlTopKyLuat.Name = "pnlTopKyLuat";

            // btnThemKL
            this.btnThemKL.Location = new System.Drawing.Point(10, 5);
            this.btnThemKL.Size = new System.Drawing.Size(130, 34);
            this.btnThemKL.Text = "+ Thêm kỷ luật";
            this.btnThemKL.FillColor = System.Drawing.Color.FromArgb(234, 88, 12);
            this.btnThemKL.Click += new System.EventHandler(this.BtnThemKL_Click);

            // btnXoaKL
            this.btnXoaKL.Location = new System.Drawing.Point(148, 5);
            this.btnXoaKL.Size = new System.Drawing.Size(70, 34);
            this.btnXoaKL.Text = "Xóa";
            this.btnXoaKL.FillColor = System.Drawing.Color.FromArgb(185, 28, 28);
            this.btnXoaKL.Click += new System.EventHandler(this.BtnXoaKL_Click);

            // gridKyLuat
            this.gridKyLuat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridKyLuat.MainView = this.gridViewKyLuat;
            this.gridKyLuat.Name = "gridKyLuat";
            this.gridKyLuat.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.gridViewKyLuat });

            // gridViewKyLuat
            this.gridViewKyLuat.GridControl = this.gridKyLuat;
            this.gridViewKyLuat.Name = "gridViewKyLuat";
            this.gridViewKyLuat.OptionsView.ShowGroupPanel = false;

            // tabPageDonXinNghi
            this.tabPageDonXinNghi.Controls.Add(this.gridDonXinNghi);
            this.tabPageDonXinNghi.Controls.Add(this.pnlTopDonXinNghi);
            this.tabPageDonXinNghi.Location = new System.Drawing.Point(4, 44);
            this.tabPageDonXinNghi.Name = "tabPageDonXinNghi";
            this.tabPageDonXinNghi.Size = new System.Drawing.Size(407, 672);
            this.tabPageDonXinNghi.TabIndex = 4;
            this.tabPageDonXinNghi.Text = "Đơn xin nghỉ";

            // pnlTopDonXinNghi
            this.pnlTopDonXinNghi.Controls.Add(this.btnThemDXN);
            this.pnlTopDonXinNghi.Controls.Add(this.btnXoaDXN);
            this.pnlTopDonXinNghi.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopDonXinNghi.Height = 44;
            this.pnlTopDonXinNghi.Name = "pnlTopDonXinNghi";

            // btnThemDXN
            this.btnThemDXN.Location = new System.Drawing.Point(10, 5);
            this.btnThemDXN.Size = new System.Drawing.Size(150, 34);
            this.btnThemDXN.Text = "+ Đơn xin nghỉ";
            this.btnThemDXN.FillColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.btnThemDXN.Click += new System.EventHandler(this.BtnThemDXN_Click);

            // btnXoaDXN
            this.btnXoaDXN.Location = new System.Drawing.Point(168, 5);
            this.btnXoaDXN.Size = new System.Drawing.Size(70, 34);
            this.btnXoaDXN.Text = "Xóa";
            this.btnXoaDXN.FillColor = System.Drawing.Color.FromArgb(185, 28, 28);
            this.btnXoaDXN.Click += new System.EventHandler(this.BtnXoaDXN_Click);

            // gridDonXinNghi
            this.gridDonXinNghi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDonXinNghi.MainView = this.gridViewDonXinNghi;
            this.gridDonXinNghi.Name = "gridDonXinNghi";
            this.gridDonXinNghi.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.gridViewDonXinNghi });

            // gridViewDonXinNghi
            this.gridViewDonXinNghi.GridControl = this.gridDonXinNghi;
            this.gridViewDonXinNghi.Name = "gridViewDonXinNghi";
            this.gridViewDonXinNghi.OptionsView.ShowGroupPanel = false;

            // tabPageTaiNanLaoDong
            this.tabPageTaiNanLaoDong.Controls.Add(this.gridTaiNanLaoDong);
            this.tabPageTaiNanLaoDong.Controls.Add(this.pnlTopTaiNan);
            this.tabPageTaiNanLaoDong.Location = new System.Drawing.Point(4, 44);
            this.tabPageTaiNanLaoDong.Name = "tabPageTaiNanLaoDong";
            this.tabPageTaiNanLaoDong.Size = new System.Drawing.Size(407, 672);
            this.tabPageTaiNanLaoDong.TabIndex = 5;
            this.tabPageTaiNanLaoDong.Text = "Tai nạn LĐ";

            // pnlTopTaiNan
            this.pnlTopTaiNan.Controls.Add(this.btnThemTN);
            this.pnlTopTaiNan.Controls.Add(this.btnXoaTN);
            this.pnlTopTaiNan.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopTaiNan.Height = 44;
            this.pnlTopTaiNan.Name = "pnlTopTaiNan";

            // btnThemTN
            this.btnThemTN.Location = new System.Drawing.Point(10, 5);
            this.btnThemTN.Size = new System.Drawing.Size(150, 34);
            this.btnThemTN.Text = "+ Ghi nhận";
            this.btnThemTN.FillColor = System.Drawing.Color.FromArgb(147, 51, 234);
            this.btnThemTN.Click += new System.EventHandler(this.BtnThemTN_Click);

            // btnXoaTN
            this.btnXoaTN.Location = new System.Drawing.Point(168, 5);
            this.btnXoaTN.Size = new System.Drawing.Size(70, 34);
            this.btnXoaTN.Text = "Xóa";
            this.btnXoaTN.FillColor = System.Drawing.Color.FromArgb(185, 28, 28);
            this.btnXoaTN.Click += new System.EventHandler(this.BtnXoaTN_Click);

            // gridTaiNanLaoDong
            this.gridTaiNanLaoDong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTaiNanLaoDong.MainView = this.gridViewTaiNanLaoDong;
            this.gridTaiNanLaoDong.Name = "gridTaiNanLaoDong";
            this.gridTaiNanLaoDong.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.gridViewTaiNanLaoDong });

            // gridViewTaiNanLaoDong
            this.gridViewTaiNanLaoDong.GridControl = this.gridTaiNanLaoDong;
            this.gridViewTaiNanLaoDong.Name = "gridViewTaiNanLaoDong";
            this.gridViewTaiNanLaoDong.OptionsView.ShowGroupPanel = false;"""

code = code.replace('            // tabPageAccount', tab_pages_def + '\n\n            // tabPageAccount')

# Declaration of variables at the bottom
declarations = """        private System.Windows.Forms.TabPage tabPageChungChi;
        private System.Windows.Forms.Panel pnlTopChungChi;
        private DevExpress.XtraGrid.GridControl gridChungChi;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewChungChi;
        private Guna.UI2.WinForms.Guna2Button btnThemCC;
        private Guna.UI2.WinForms.Guna2Button btnXoaCC;

        private System.Windows.Forms.TabPage tabPageKyLuat;
        private System.Windows.Forms.Panel pnlTopKyLuat;
        private DevExpress.XtraGrid.GridControl gridKyLuat;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewKyLuat;
        private Guna.UI2.WinForms.Guna2Button btnThemKL;
        private Guna.UI2.WinForms.Guna2Button btnXoaKL;

        private System.Windows.Forms.TabPage tabPageDonXinNghi;
        private System.Windows.Forms.Panel pnlTopDonXinNghi;
        private DevExpress.XtraGrid.GridControl gridDonXinNghi;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDonXinNghi;
        private Guna.UI2.WinForms.Guna2Button btnThemDXN;
        private Guna.UI2.WinForms.Guna2Button btnXoaDXN;

        private System.Windows.Forms.TabPage tabPageTaiNanLaoDong;
        private System.Windows.Forms.Panel pnlTopTaiNan;
        private DevExpress.XtraGrid.GridControl gridTaiNanLaoDong;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewTaiNanLaoDong;
        private Guna.UI2.WinForms.Guna2Button btnThemTN;
        private Guna.UI2.WinForms.Guna2Button btnXoaTN;"""

code = code.replace('        private System.Windows.Forms.Label lblLocChucVu;', '        private System.Windows.Forms.Label lblLocChucVu;\n' + declarations)

# Add init controls in InitializeComponent
init_controls = """            this.tabPageChungChi = new System.Windows.Forms.TabPage();
            this.pnlTopChungChi = new System.Windows.Forms.Panel();
            this.gridChungChi = new DevExpress.XtraGrid.GridControl();
            this.gridViewChungChi = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnThemCC = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoaCC = new Guna.UI2.WinForms.Guna2Button();

            this.tabPageKyLuat = new System.Windows.Forms.TabPage();
            this.pnlTopKyLuat = new System.Windows.Forms.Panel();
            this.gridKyLuat = new DevExpress.XtraGrid.GridControl();
            this.gridViewKyLuat = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnThemKL = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoaKL = new Guna.UI2.WinForms.Guna2Button();

            this.tabPageDonXinNghi = new System.Windows.Forms.TabPage();
            this.pnlTopDonXinNghi = new System.Windows.Forms.Panel();
            this.gridDonXinNghi = new DevExpress.XtraGrid.GridControl();
            this.gridViewDonXinNghi = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnThemDXN = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoaDXN = new Guna.UI2.WinForms.Guna2Button();

            this.tabPageTaiNanLaoDong = new System.Windows.Forms.TabPage();
            this.pnlTopTaiNan = new System.Windows.Forms.Panel();
            this.gridTaiNanLaoDong = new DevExpress.XtraGrid.GridControl();
            this.gridViewTaiNanLaoDong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnThemTN = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoaTN = new Guna.UI2.WinForms.Guna2Button();"""
            
code = code.replace('            this.tabControlDetails = new Guna.UI2.WinForms.Guna2TabControl();', '            this.tabControlDetails = new Guna.UI2.WinForms.Guna2TabControl();\n' + init_controls)

with open(designer_path, 'w', encoding='utf-8') as f:
    f.write(code)

print('Designer updated successfully!')

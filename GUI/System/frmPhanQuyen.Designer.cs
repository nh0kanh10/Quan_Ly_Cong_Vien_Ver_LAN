namespace GUI
{
    partial class frmPhanQuyen
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.gbVaiTro = new Guna.UI2.WinForms.Guna2GroupBox();
            this.lbVaiTro = new DevExpress.XtraEditors.ListBoxControl();
            this.gbQuyenHan = new Guna.UI2.WinForms.Guna2GroupBox();
            this.treeQuyen = new DevExpress.XtraTreeList.TreeList();
            this.pnlFooter = new Guna.UI2.WinForms.Guna2Panel();
            this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain.Panel1)).BeginInit();
            this.pnlMain.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain.Panel2)).BeginInit();
            this.pnlMain.Panel2.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.gbVaiTro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lbVaiTro)).BeginInit();
            this.gbQuyenHan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeQuyen)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            // 
            // pnlMain.Panel1
            // 
            this.pnlMain.Panel1.Controls.Add(this.gbVaiTro);
            this.pnlMain.Panel1.Padding = new System.Windows.Forms.Padding(10);
            // 
            // pnlMain.Panel2
            // 
            this.pnlMain.Panel2.Controls.Add(this.gbQuyenHan);
            this.pnlMain.Panel2.Padding = new System.Windows.Forms.Padding(10);
            this.pnlMain.Size = new System.Drawing.Size(1200, 640);
            this.pnlMain.SplitterPosition = 300;
            this.pnlMain.TabIndex = 0;
            // 
            // gbVaiTro
            // 
            this.gbVaiTro.Controls.Add(this.lbVaiTro);
            this.gbVaiTro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbVaiTro.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbVaiTro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.gbVaiTro.Location = new System.Drawing.Point(10, 10);
            this.gbVaiTro.Name = "gbVaiTro";
            this.gbVaiTro.Size = new System.Drawing.Size(280, 620);
            this.gbVaiTro.TabIndex = 0;
            this.gbVaiTro.Text = "Chọn Vai Trò";
            // 
            // lbVaiTro
            // 
            this.lbVaiTro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbVaiTro.Location = new System.Drawing.Point(0, 40);
            this.lbVaiTro.Name = "lbVaiTro";
            this.lbVaiTro.Size = new System.Drawing.Size(280, 580);
            this.lbVaiTro.TabIndex = 0;
            // 
            // gbQuyenHan
            // 
            this.gbQuyenHan.Controls.Add(this.treeQuyen);
            this.gbQuyenHan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbQuyenHan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbQuyenHan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.gbQuyenHan.Location = new System.Drawing.Point(10, 10);
            this.gbQuyenHan.Name = "gbQuyenHan";
            this.gbQuyenHan.Size = new System.Drawing.Size(870, 620);
            this.gbQuyenHan.TabIndex = 0;
            this.gbQuyenHan.Text = "Phân quyền (Hierarchy Tree)";
            // 
            // treeQuyen
            // 
            this.treeQuyen.CheckBoxFieldName = "Checked";
            this.treeQuyen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeQuyen.Location = new System.Drawing.Point(0, 40);
            this.treeQuyen.Name = "treeQuyen";
            this.treeQuyen.OptionsView.ShowCheckBoxes = true;
            this.treeQuyen.Size = new System.Drawing.Size(870, 580);
            this.treeQuyen.TabIndex = 0;
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.White;
            this.pnlFooter.BorderColor = System.Drawing.Color.Gainsboro;
            this.pnlFooter.BorderThickness = 1;
            this.pnlFooter.Controls.Add(this.btnLuu);
            this.pnlFooter.Controls.Add(this.btnLamMoi);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 640);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(1200, 60);
            this.pnlFooter.TabIndex = 1;
            // 
            // btnLuu
            // 
            this.btnLuu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuu.BorderRadius = 15;
            this.btnLuu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(1030, 10);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(150, 40);
            this.btnLuu.TabIndex = 0;
            this.btnLuu.Text = "Lưu Thay Đổi";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLamMoi.BorderRadius = 15;
            this.btnLamMoi.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(860, 10);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(150, 40);
            this.btnLamMoi.TabIndex = 1;
            this.btnLamMoi.Text = "Làm Mới";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // frmPhanQuyen
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlFooter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPhanQuyen";
            this.Text = "Thiết lập Phân Quyền";
            this.Load += new System.EventHandler(this.frmPhanQuyen_Load);
            this.lbVaiTro.SelectedIndexChanged += new System.EventHandler(this.LbVaiTro_SelectedIndexChanged);
            this.treeQuyen.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.TreeQuyen_AfterCheckNode);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain.Panel1)).EndInit();
            this.pnlMain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain.Panel2)).EndInit();
            this.pnlMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.gbVaiTro.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lbVaiTro)).EndInit();
            this.gbQuyenHan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeQuyen)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl pnlMain;
        private Guna.UI2.WinForms.Guna2GroupBox gbVaiTro;
        private DevExpress.XtraEditors.ListBoxControl lbVaiTro;
        private Guna.UI2.WinForms.Guna2GroupBox gbQuyenHan;
        private DevExpress.XtraTreeList.TreeList treeQuyen;
        private Guna.UI2.WinForms.Guna2Panel pnlFooter;
        private Guna.UI2.WinForms.Guna2Button btnLuu;
        private Guna.UI2.WinForms.Guna2Button btnLamMoi;
    }
}

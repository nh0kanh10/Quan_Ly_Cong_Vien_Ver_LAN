namespace GUI
{
    partial class frmChungChiDialog
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblLoai         = new System.Windows.Forms.Label();
            this.cboLoaiChungChi = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblSo           = new System.Windows.Forms.Label();
            this.txtSoChungChi   = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblNhaCap       = new System.Windows.Forms.Label();
            this.txtNhaCap       = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblNgayCap      = new System.Windows.Forms.Label();
            this.dtpNgayCap      = new DevExpress.XtraEditors.DateEdit();
            this.lblNgayHetHan   = new System.Windows.Forms.Label();
            this.dtpNgayHetHan   = new DevExpress.XtraEditors.DateEdit();
            this.pnlCanhBao      = new System.Windows.Forms.Panel();
            this.lblCanhBao      = new System.Windows.Forms.Label();
            this.btnLuu          = new Guna.UI2.WinForms.Guna2Button();
            this.btnHuy          = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayCap.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayHetHan.Properties)).BeginInit();
            this.pnlCanhBao.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblLoai
            // 
            this.lblLoai.AutoSize  = true;
            this.lblLoai.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLoai.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblLoai.Location  = new System.Drawing.Point(20, 20);
            this.lblLoai.Name      = "lblLoai";
            this.lblLoai.Size      = new System.Drawing.Size(120, 15);
            this.lblLoai.Text      = "Loại chứng chỉ: *";
            // 
            // cboLoaiChungChi
            // 
            this.cboLoaiChungChi.DrawMode      = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLoaiChungChi.DropDownStyle  = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiChungChi.ItemHeight     = 30;
            this.cboLoaiChungChi.Location       = new System.Drawing.Point(20, 40);
            this.cboLoaiChungChi.Name           = "cboLoaiChungChi";
            this.cboLoaiChungChi.Size           = new System.Drawing.Size(370, 36);
            this.cboLoaiChungChi.TabIndex       = 0;
            // 
            // lblSo
            // 
            this.lblSo.AutoSize  = true;
            this.lblSo.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSo.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblSo.Location  = new System.Drawing.Point(20, 88);
            this.lblSo.Name      = "lblSo";
            this.lblSo.Size      = new System.Drawing.Size(80, 15);
            this.lblSo.Text      = "Số chứng chỉ:";
            // 
            // txtSoChungChi
            // 
            this.txtSoChungChi.Cursor          = System.Windows.Forms.Cursors.IBeam;
            this.txtSoChungChi.DefaultText      = "";
            this.txtSoChungChi.Location         = new System.Drawing.Point(20, 108);
            this.txtSoChungChi.Name             = "txtSoChungChi";
            this.txtSoChungChi.PlaceholderText  = "VD: CC-2024-001";
            this.txtSoChungChi.SelectedText     = "";
            this.txtSoChungChi.Size             = new System.Drawing.Size(370, 36);
            this.txtSoChungChi.TabIndex         = 1;
            // 
            // lblNhaCap
            // 
            this.lblNhaCap.AutoSize  = true;
            this.lblNhaCap.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNhaCap.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblNhaCap.Location  = new System.Drawing.Point(20, 156);
            this.lblNhaCap.Name      = "lblNhaCap";
            this.lblNhaCap.Size      = new System.Drawing.Size(50, 15);
            this.lblNhaCap.Text      = "Nơi cấp:";
            // 
            // txtNhaCap
            // 
            this.txtNhaCap.Cursor          = System.Windows.Forms.Cursors.IBeam;
            this.txtNhaCap.DefaultText      = "";
            this.txtNhaCap.Location         = new System.Drawing.Point(20, 176);
            this.txtNhaCap.Name             = "txtNhaCap";
            this.txtNhaCap.PlaceholderText  = "VD: Sở LĐTBXH Bình Dương";
            this.txtNhaCap.SelectedText     = "";
            this.txtNhaCap.Size             = new System.Drawing.Size(370, 36);
            this.txtNhaCap.TabIndex         = 2;
            // 
            // lblNgayCap
            // 
            this.lblNgayCap.AutoSize  = true;
            this.lblNgayCap.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNgayCap.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblNgayCap.Location  = new System.Drawing.Point(20, 224);
            this.lblNgayCap.Name      = "lblNgayCap";
            this.lblNgayCap.Size      = new System.Drawing.Size(80, 15);
            this.lblNgayCap.Text      = "Ngày cấp: *";
            // 
            // dtpNgayCap
            // 
            this.dtpNgayCap.Location  = new System.Drawing.Point(20, 244);
            this.dtpNgayCap.Name      = "dtpNgayCap";
            this.dtpNgayCap.Size      = new System.Drawing.Size(174, 20);
            this.dtpNgayCap.TabIndex  = 3;
            this.dtpNgayCap.DateTime  = System.DateTime.Now;
            // 
            // lblNgayHetHan
            // 
            this.lblNgayHetHan.AutoSize  = true;
            this.lblNgayHetHan.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNgayHetHan.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblNgayHetHan.Location  = new System.Drawing.Point(214, 224);
            this.lblNgayHetHan.Name      = "lblNgayHetHan";
            this.lblNgayHetHan.Size      = new System.Drawing.Size(100, 15);
            this.lblNgayHetHan.Text      = "Ngày hết hạn: *";
            // 
            // dtpNgayHetHan
            // 
            this.dtpNgayHetHan.Location  = new System.Drawing.Point(214, 244);
            this.dtpNgayHetHan.Name      = "dtpNgayHetHan";
            this.dtpNgayHetHan.Size      = new System.Drawing.Size(176, 20);
            this.dtpNgayHetHan.TabIndex  = 4;
            this.dtpNgayHetHan.DateTime  = System.DateTime.Now.AddYears(2);
            // 
            // pnlCanhBao
            // 
            this.pnlCanhBao.BackColor = System.Drawing.Color.FromArgb(254, 243, 199);
            this.pnlCanhBao.Controls.Add(this.lblCanhBao);
            this.pnlCanhBao.Location  = new System.Drawing.Point(20, 280);
            this.pnlCanhBao.Name      = "pnlCanhBao";
            this.pnlCanhBao.Size      = new System.Drawing.Size(370, 40);
            this.pnlCanhBao.TabIndex  = 5;
            // 
            // lblCanhBao
            // 
            this.lblCanhBao.Dock      = System.Windows.Forms.DockStyle.Fill;
            this.lblCanhBao.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblCanhBao.ForeColor = System.Drawing.Color.FromArgb(120, 53, 15);
            this.lblCanhBao.Location  = new System.Drawing.Point(0, 0);
            this.lblCanhBao.Name      = "lblCanhBao";
            this.lblCanhBao.Padding   = new System.Windows.Forms.Padding(6, 4, 0, 0);
            this.lblCanhBao.Size      = new System.Drawing.Size(370, 40);
            this.lblCanhBao.Text      = "⚠️  Chứng chỉ cứu hộ / vận hành bắt buộc theo NĐ 113/2015/NĐ-CP";
            // 
            // btnLuu
            // 
            this.btnLuu.FillColor   = System.Drawing.Color.FromArgb(22, 163, 74);
            this.btnLuu.Font        = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor   = System.Drawing.Color.White;
            this.btnLuu.Location    = new System.Drawing.Point(20, 338);
            this.btnLuu.Name        = "btnLuu";
            this.btnLuu.Size        = new System.Drawing.Size(175, 40);
            this.btnLuu.TabIndex    = 6;
            this.btnLuu.Text        = "💾  Lưu chứng chỉ";
            this.btnLuu.Click      += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.FillColor   = System.Drawing.Color.FromArgb(107, 114, 128);
            this.btnHuy.ForeColor   = System.Drawing.Color.White;
            this.btnHuy.Location    = new System.Drawing.Point(215, 338);
            this.btnHuy.Name        = "btnHuy";
            this.btnHuy.Size        = new System.Drawing.Size(175, 40);
            this.btnHuy.TabIndex    = 7;
            this.btnHuy.Text        = "Hủy";
            this.btnHuy.Click      += new System.EventHandler(this.btnHuy_Click);
            // 
            // frmChungChiDialog
            // 
            this.AutoScaleMode      = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor          = System.Drawing.Color.FromArgb(248, 250, 252);
            this.ClientSize         = new System.Drawing.Size(413, 398);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblLoai, this.cboLoaiChungChi,
                this.lblSo,   this.txtSoChungChi,
                this.lblNhaCap, this.txtNhaCap,
                this.lblNgayCap, this.dtpNgayCap,
                this.lblNgayHetHan, this.dtpNgayHetHan,
                this.pnlCanhBao,
                this.btnLuu, this.btnHuy });
            this.Font               = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle    = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox        = false;
            this.MinimizeBox        = false;
            this.Name               = "frmChungChiDialog";
            this.StartPosition      = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text               = "Thêm chứng chỉ hành nghề";
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayCap.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayHetHan.Properties)).EndInit();
            this.pnlCanhBao.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label              lblLoai;
        private Guna.UI2.WinForms.Guna2ComboBox         cboLoaiChungChi;
        private System.Windows.Forms.Label              lblSo;
        private Guna.UI2.WinForms.Guna2TextBox          txtSoChungChi;
        private System.Windows.Forms.Label              lblNhaCap;
        private Guna.UI2.WinForms.Guna2TextBox          txtNhaCap;
        private System.Windows.Forms.Label              lblNgayCap;
        private DevExpress.XtraEditors.DateEdit          dtpNgayCap;
        private System.Windows.Forms.Label              lblNgayHetHan;
        private DevExpress.XtraEditors.DateEdit          dtpNgayHetHan;
        private System.Windows.Forms.Panel              pnlCanhBao;
        private System.Windows.Forms.Label              lblCanhBao;
        private Guna.UI2.WinForms.Guna2Button           btnLuu;
        private Guna.UI2.WinForms.Guna2Button           btnHuy;
    }
}

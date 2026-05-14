using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace GUI.Modules.BanHang
{
    partial class frmPhienThuNgan
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }


        #region Windows Form Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTieuDe = new DevExpress.XtraEditors.LabelControl();
            this.lblMaMay = new DevExpress.XtraEditors.LabelControl();
            this.cboMaMay = new DevExpress.XtraEditors.LookUpEdit();
            this.lblTienDau = new DevExpress.XtraEditors.LabelControl();
            this.txtTienDauCa = new DevExpress.XtraEditors.TextEdit();
            this.lblKho = new DevExpress.XtraEditors.LabelControl();
            this.cboKho = new DevExpress.XtraEditors.LookUpEdit();
            this.lblGhiChu = new DevExpress.XtraEditors.LabelControl();
            this.txtGhiChu = new DevExpress.XtraEditors.MemoEdit();
            this.lblTienCuoi = new DevExpress.XtraEditors.LabelControl();
            this.txtTienCuoiCa = new DevExpress.XtraEditors.TextEdit();
            this.lblTongThu = new DevExpress.XtraEditors.LabelControl();
            this.lblTongThuValue = new DevExpress.XtraEditors.LabelControl();
            this.lblChenhLech = new DevExpress.XtraEditors.LabelControl();
            this.lblChenhLechValue = new DevExpress.XtraEditors.LabelControl();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnXacNhan = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.cboMaMay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTienDauCa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboKho.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGhiChu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTienCuoiCa.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.Appearance.Options.UseFont = true;
            this.lblTieuDe.Location = new System.Drawing.Point(20, 15);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(200, 25);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "MỞ PHIÊN THU NGÂN";
            // 
            // lblMaMay
            // 
            this.lblMaMay.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMaMay.Appearance.Options.UseFont = true;
            this.lblMaMay.Location = new System.Drawing.Point(20, 60);
            this.lblMaMay.Name = "lblMaMay";
            this.lblMaMay.Text = "Mã máy POS:";
            // 
            // txtMaMay
            // 
            this.cboMaMay.Location = new System.Drawing.Point(150, 58);
            this.cboMaMay.Name = "cboMaMay";
            this.cboMaMay.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboMaMay.Properties.Appearance.Options.UseFont = true;
            this.cboMaMay.Size = new System.Drawing.Size(280, 24);
            this.cboMaMay.TabIndex = 1;
            // 
            // lblTienDau
            // 
            this.lblTienDau.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTienDau.Appearance.Options.UseFont = true;
            this.lblTienDau.Location = new System.Drawing.Point(20, 95);
            this.lblTienDau.Name = "lblTienDau";
            this.lblTienDau.Text = "Tiền đầu ca:";
            // 
            // txtTienDauCa
            // 
            this.txtTienDauCa.Location = new System.Drawing.Point(150, 93);
            this.txtTienDauCa.Name = "txtTienDauCa";
            this.txtTienDauCa.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTienDauCa.Properties.Appearance.Options.UseFont = true;
            this.txtTienDauCa.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtTienDauCa.Properties.DisplayFormat.FormatString = "#,##0";
            this.txtTienDauCa.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtTienDauCa.Properties.EditFormat.FormatString = "#,##0";
            this.txtTienDauCa.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtTienDauCa.Properties.Mask.EditMask = "n0";
            this.txtTienDauCa.Size = new System.Drawing.Size(280, 24);
            this.txtTienDauCa.TabIndex = 2;
            // 
            // lblKho
            // 
            this.lblKho.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblKho.Appearance.Options.UseFont = true;
            this.lblKho.Location = new System.Drawing.Point(20, 130);
            this.lblKho.Name = "lblKho";
            this.lblKho.Text = "Kho bán hàng:";
            // 
            // cboKho
            // 
            this.cboKho.Location = new System.Drawing.Point(150, 128);
            this.cboKho.Name = "cboKho";
            this.cboKho.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboKho.Properties.Appearance.Options.UseFont = true;
            this.cboKho.Properties.NullText = "-- Chọn kho --";
            this.cboKho.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Id", 50),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKho", "Tên kho", 200)});
            this.cboKho.Properties.DisplayMember = "TenKho";
            this.cboKho.Properties.ValueMember = "Id";
            this.cboKho.Size = new System.Drawing.Size(280, 24);
            this.cboKho.TabIndex = 3;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGhiChu.Appearance.Options.UseFont = true;
            this.lblGhiChu.Location = new System.Drawing.Point(20, 165);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Text = "Ghi chú:";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Location = new System.Drawing.Point(150, 163);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGhiChu.Properties.Appearance.Options.UseFont = true;
            this.txtGhiChu.Size = new System.Drawing.Size(280, 55);
            this.txtGhiChu.TabIndex = 4;
            // 
            // lblTienCuoi
            // 
            this.lblTienCuoi.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTienCuoi.Appearance.Options.UseFont = true;
            this.lblTienCuoi.Location = new System.Drawing.Point(20, 240);
            this.lblTienCuoi.Name = "lblTienCuoi";
            this.lblTienCuoi.Text = "Tiền cuối ca:";
            this.lblTienCuoi.Visible = false;
            // 
            // txtTienCuoiCa
            // 
            this.txtTienCuoiCa.Location = new System.Drawing.Point(150, 238);
            this.txtTienCuoiCa.Name = "txtTienCuoiCa";
            this.txtTienCuoiCa.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTienCuoiCa.Properties.Appearance.Options.UseFont = true;
            this.txtTienCuoiCa.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtTienCuoiCa.Properties.Mask.EditMask = "n0";
            this.txtTienCuoiCa.Size = new System.Drawing.Size(280, 24);
            this.txtTienCuoiCa.TabIndex = 5;
            this.txtTienCuoiCa.Visible = false;
            // 
            // lblTongThu
            // 
            this.lblTongThu.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTongThu.Appearance.Options.UseFont = true;
            this.lblTongThu.Location = new System.Drawing.Point(20, 275);
            this.lblTongThu.Name = "lblTongThu";
            this.lblTongThu.Text = "Tổng thu trong ca:";
            this.lblTongThu.Visible = false;
            // 
            // lblTongThuValue
            // 
            this.lblTongThuValue.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTongThuValue.Appearance.Options.UseFont = true;
            this.lblTongThuValue.Location = new System.Drawing.Point(180, 275);
            this.lblTongThuValue.Name = "lblTongThuValue";
            this.lblTongThuValue.Text = "0";
            this.lblTongThuValue.Visible = false;
            // 
            // lblChenhLech
            // 
            this.lblChenhLech.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblChenhLech.Appearance.Options.UseFont = true;
            this.lblChenhLech.Location = new System.Drawing.Point(20, 305);
            this.lblChenhLech.Name = "lblChenhLech";
            this.lblChenhLech.Text = "Chênh lệch:";
            this.lblChenhLech.Visible = false;
            // 
            // lblChenhLechValue
            // 
            this.lblChenhLechValue.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblChenhLechValue.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblChenhLechValue.Appearance.Options.UseFont = true;
            this.lblChenhLechValue.Appearance.Options.UseForeColor = true;
            this.lblChenhLechValue.Location = new System.Drawing.Point(180, 302);
            this.lblChenhLechValue.Name = "lblChenhLechValue";
            this.lblChenhLechValue.Text = "0";
            this.lblChenhLechValue.Visible = false;
            // 
            // btnHuy
            // 
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Location = new System.Drawing.Point(230, 350);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(90, 35);
            this.btnHuy.TabIndex = 6;
            this.btnHuy.Text = "HỦY";
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(80)))));
            this.btnXacNhan.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnXacNhan.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnXacNhan.Appearance.Options.UseBackColor = true;
            this.btnXacNhan.Appearance.Options.UseFont = true;
            this.btnXacNhan.Appearance.Options.UseForeColor = true;
            this.btnXacNhan.Location = new System.Drawing.Point(330, 350);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(100, 35);
            this.btnXacNhan.TabIndex = 7;
            this.btnXacNhan.Text = "XÁC NHẬN";
            this.btnXacNhan.Click += new System.EventHandler(this.BtnXacNhan_Click);
            // 
            // frmPhienThuNgan
            // 
            this.AcceptButton = this.btnXacNhan;
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(450, 400);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.lblChenhLechValue);
            this.Controls.Add(this.lblChenhLech);
            this.Controls.Add(this.lblTongThuValue);
            this.Controls.Add(this.lblTongThu);
            this.Controls.Add(this.txtTienCuoiCa);
            this.Controls.Add(this.lblTienCuoi);
            this.Controls.Add(this.txtGhiChu);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.cboKho);
            this.Controls.Add(this.lblKho);
            this.Controls.Add(this.txtTienDauCa);
            this.Controls.Add(this.lblTienDau);
            this.Controls.Add(this.cboMaMay);
            this.Controls.Add(this.lblMaMay);
            this.Controls.Add(this.lblTieuDe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPhienThuNgan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Phiên thu ngân";
            ((System.ComponentModel.ISupportInitialize)(this.cboMaMay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTienDauCa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboKho.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGhiChu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTienCuoiCa.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
        private DevExpress.XtraEditors.LabelControl lblTieuDe;
        private DevExpress.XtraEditors.LabelControl lblMaMay;
        private DevExpress.XtraEditors.LookUpEdit cboMaMay;
        private DevExpress.XtraEditors.LabelControl lblTienDau;
        private DevExpress.XtraEditors.TextEdit txtTienDauCa;
        private DevExpress.XtraEditors.LabelControl lblKho;
        private DevExpress.XtraEditors.LookUpEdit cboKho;
        private DevExpress.XtraEditors.LabelControl lblGhiChu;
        private DevExpress.XtraEditors.MemoEdit txtGhiChu;
        private DevExpress.XtraEditors.LabelControl lblTienCuoi;
        private DevExpress.XtraEditors.TextEdit txtTienCuoiCa;
        private DevExpress.XtraEditors.LabelControl lblTongThu;
        private DevExpress.XtraEditors.LabelControl lblTongThuValue;
        private DevExpress.XtraEditors.LabelControl lblChenhLech;
        private DevExpress.XtraEditors.LabelControl lblChenhLechValue;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnXacNhan;
        private System.ComponentModel.IContainer components = null;




    }}

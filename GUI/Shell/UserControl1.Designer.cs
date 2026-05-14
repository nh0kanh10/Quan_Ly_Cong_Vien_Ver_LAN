namespace GUI.Shell
{
    partial class UserControl1
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(382, 242);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(240, 150);
            this.dgv.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(562, 91);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            // 
            // popupMenu1
            // 
            this.popupMenu1.Name = "popupMenu1";
            // 
            // groupControl1
            // 
            this.groupControl1.Location = new System.Drawing.Point(815, 165);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(200, 100);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "groupControl1";
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dgv);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(1001, 498);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.TextBox textBox1;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraEditors.GroupControl groupControl1;





    }}
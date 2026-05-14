namespace GUI.Shell
{
    partial class frmMain
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


        #region Windows Form Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.fluentDesignFormContainer = new DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer();
            this.accordionControl = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.fluentDesignFormControl = new DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl();
            this.documentManager = new DevExpress.XtraBars.Docking2010.DocumentManager(this.components);
            this.tabbedView = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(this.components);


            ((System.ComponentModel.ISupportInitialize)(this.accordionControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentDesignFormControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView)).BeginInit();
            this.SuspendLayout();

            // 
            // fluentDesignFormContainer
            // 
            this.fluentDesignFormContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fluentDesignFormContainer.Location = new System.Drawing.Point(260, 40);
            this.fluentDesignFormContainer.Name = "fluentDesignFormContainer";
            this.fluentDesignFormContainer.Size = new System.Drawing.Size(940, 760);
            this.fluentDesignFormContainer.TabIndex = 0;

            // 
            // accordionControl
            // 
            this.accordionControl.Dock = System.Windows.Forms.DockStyle.Left;

            this.accordionControl.Location = new System.Drawing.Point(0, 40);
            this.accordionControl.Name = "accordionControl";
            this.accordionControl.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Touch;
            this.accordionControl.Size = new System.Drawing.Size(260, 760);
            this.accordionControl.TabIndex = 1;
            this.accordionControl.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            this.accordionControl.AllowItemSelection = true;
            this.accordionControl.ElementClick += this.AccordionControl_ElementClick;



            // 
            // fluentDesignFormControl
            // 
            this.fluentDesignFormControl.FluentDesignForm = this;
            this.fluentDesignFormControl.Location = new System.Drawing.Point(0, 0);
            this.fluentDesignFormControl.Name = "fluentDesignFormControl";
            this.fluentDesignFormControl.Size = new System.Drawing.Size(1200, 40);
            this.fluentDesignFormControl.TabIndex = 2;
            this.fluentDesignFormControl.TabStop = false;

            // 
            // documentManager
            // 
            this.documentManager.ContainerControl = this;
            this.documentManager.View = this.tabbedView;
            this.documentManager.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] {
            this.tabbedView});
            this.tabbedView.DocumentProperties.AllowClose = true;

            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.ControlContainer = this.fluentDesignFormContainer;
            this.Controls.Add(this.fluentDesignFormContainer);
            this.Controls.Add(this.accordionControl);
            this.Controls.Add(this.fluentDesignFormControl);
            this.FluentDesignFormControl = this.fluentDesignFormControl;
            this.Name = "frmMain";
            this.NavigationControl = this.accordionControl;
            this.Text = "HỆ THỐNG QUẢN LÝ KHU DU LỊCH ĐẠI NAM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            ((System.ComponentModel.ISupportInitialize)(this.accordionControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentDesignFormControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
        private System.ComponentModel.IContainer components = null;
        private DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer fluentDesignFormContainer;
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl;
        private DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl fluentDesignFormControl;
        private DevExpress.XtraBars.Docking2010.DocumentManager documentManager;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView tabbedView;





        

    }}
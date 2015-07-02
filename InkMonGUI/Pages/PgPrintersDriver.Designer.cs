using InkMonGUI.UserControls;

namespace InkMonGUI.Pages
{
    partial class PgPrintersDriver
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDrivers = new System.Windows.Forms.ComboBox();
            this.numLevel = new System.Windows.Forms.NumericUpDown();
            this.lvPrinters = new ExListViewControl();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.numLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(360, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Identify InkMon driver for the selected printer(s)";
            // 
            // cmbDrivers
            // 
            this.cmbDrivers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDrivers.FormattingEnabled = true;
            this.cmbDrivers.Location = new System.Drawing.Point(238, 56);
            this.cmbDrivers.Name = "cmbDrivers";
            this.cmbDrivers.Size = new System.Drawing.Size(134, 21);
            this.cmbDrivers.TabIndex = 2;
            this.cmbDrivers.Visible = false;
            this.cmbDrivers.SelectedValueChanged += new System.EventHandler(this.cmbDrivers_SelectedValueChanged);
            this.cmbDrivers.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbDrivers_KeyPress);
            this.cmbDrivers.Leave += new System.EventHandler(this.cmbDrivers_Leave);
            // 
            // numLevel
            // 
            this.numLevel.Location = new System.Drawing.Point(358, 40);
            this.numLevel.Name = "numLevel";
            this.numLevel.Size = new System.Drawing.Size(31, 20);
            this.numLevel.TabIndex = 3;
            this.numLevel.Visible = false;
            this.numLevel.ValueChanged += new System.EventHandler(this.numLevel_ValueChanged);
            this.numLevel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numLevel_KeyPress);
            this.numLevel.Leave += new System.EventHandler(this.numLevel_Leave);
            // 
            // lvPrinters
            // 
            this.lvPrinters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3,
            this.columnHeader2});
            this.lvPrinters.FullRowSelect = true;
            this.lvPrinters.Location = new System.Drawing.Point(8, 75);
            this.lvPrinters.Name = "lvPrinters";
            this.lvPrinters.Size = new System.Drawing.Size(384, 196);
            this.lvPrinters.TabIndex = 1;
            this.lvPrinters.UseCompatibleStateImageBehavior = false;
            this.lvPrinters.View = System.Windows.Forms.View.Details;
            this.lvPrinters.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvPrinters_MouseUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Printer";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Level";
            this.columnHeader3.Width = 50;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Driver";
            this.columnHeader2.Width = 100;
            // 
            // PgPrintersDriver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.numLevel);
            this.Controls.Add(this.cmbDrivers);
            this.Controls.Add(this.lvPrinters);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(400, 290);
            this.MinimumSize = new System.Drawing.Size(400, 290);
            this.Name = "PgPrintersDriver";
            this.Size = new System.Drawing.Size(400, 290);
            ((System.ComponentModel.ISupportInitialize)(this.numLevel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private ExListViewControl lvPrinters;
        private System.Windows.Forms.ComboBox cmbDrivers;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.NumericUpDown numLevel;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}

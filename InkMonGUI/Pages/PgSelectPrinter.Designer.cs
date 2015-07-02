namespace InkMonGUI.Pages
{
    partial class PgSelectPrinter
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
            this.lvPrinters = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(360, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select printer(s) from your installed printers to be monitored:";
            // 
            // lvPrinters
            // 
            this.lvPrinters.CheckBoxes = true;
            this.lvPrinters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvPrinters.FullRowSelect = true;
            this.lvPrinters.Location = new System.Drawing.Point(8, 75);
            this.lvPrinters.Name = "lvPrinters";
            this.lvPrinters.Size = new System.Drawing.Size(384, 196);
            this.lvPrinters.TabIndex = 1;
            this.lvPrinters.UseCompatibleStateImageBehavior = false;
            this.lvPrinters.View = System.Windows.Forms.View.Details;
            this.lvPrinters.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvPrinters_ItemChecked);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Installed printers";
            this.columnHeader1.Width = 360;
            // 
            // PgSelectPrinter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lvPrinters);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(400, 290);
            this.MinimumSize = new System.Drawing.Size(400, 290);
            this.Name = "PgSelectPrinter";
            this.Size = new System.Drawing.Size(400, 290);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lvPrinters;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}

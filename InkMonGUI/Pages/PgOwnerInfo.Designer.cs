namespace InkMonGUI.Pages
{
    partial class PgOwnerInfo
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PgOwnerInfo));
            this.label1 = new System.Windows.Forms.Label();
            this.txtReferenceCode = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtReferralCode = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPaypalEmail = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnGetReferenceCode = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnShowInfo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fill out your contact information:";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // txtReferenceCode
            // 
            this.txtReferenceCode.Location = new System.Drawing.Point(123, 238);
            this.txtReferenceCode.Name = "txtReferenceCode";
            this.txtReferenceCode.ReadOnly = true;
            this.txtReferenceCode.Size = new System.Drawing.Size(112, 20);
            this.txtReferenceCode.TabIndex = 19;
            this.txtReferenceCode.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 241);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Reference Code:";
            // 
            // txtReferralCode
            // 
            this.txtReferralCode.Location = new System.Drawing.Point(123, 175);
            this.txtReferralCode.Name = "txtReferralCode";
            this.txtReferralCode.Size = new System.Drawing.Size(112, 20);
            this.txtReferralCode.TabIndex = 17;
            this.toolTip1.SetToolTip(this.txtReferralCode, "Enter the referral code sent to you by your referrer if you have one \r\n\r\nThis all" +
        "ows them to be paid.");
            this.txtReferralCode.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 178);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Referral Code:";
            // 
            // txtPaypalEmail
            // 
            this.txtPaypalEmail.Location = new System.Drawing.Point(123, 118);
            this.txtPaypalEmail.Name = "txtPaypalEmail";
            this.txtPaypalEmail.Size = new System.Drawing.Size(251, 20);
            this.txtPaypalEmail.TabIndex = 15;
            this.txtPaypalEmail.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Paypal Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(123, 92);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(251, 20);
            this.txtEmail.TabIndex = 13;
            this.txtEmail.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Email:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(123, 66);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(251, 20);
            this.txtName.TabIndex = 11;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Name:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // btnGetReferenceCode
            // 
            this.btnGetReferenceCode.Location = new System.Drawing.Point(251, 236);
            this.btnGetReferenceCode.Name = "btnGetReferenceCode";
            this.btnGetReferenceCode.Size = new System.Drawing.Size(131, 23);
            this.btnGetReferenceCode.TabIndex = 20;
            this.btnGetReferenceCode.Text = "Get Reference Code";
            this.toolTip1.SetToolTip(this.btnGetReferenceCode, "This will generate a Referral Code for you\r\nso you can send it to any one to reci" +
        "eve\r\nthe payment once they purchased from us");
            this.btnGetReferenceCode.UseVisualStyleBackColor = true;
            this.btnGetReferenceCode.Click += new System.EventHandler(this.btnGetReferenceCode_Click);
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(120, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(254, 31);
            this.label2.TabIndex = 21;
            this.label2.Text = "* To receive payment for your referrals please enter your paypal email address";
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(120, 198);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(254, 31);
            this.label3.TabIndex = 22;
            this.label3.Text = "* Please enter your reference code if you have one so your referrer can receive p" +
    "ayment";
            // 
            // btnShowInfo
            // 
            this.btnShowInfo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnShowInfo.ForeColor = System.Drawing.Color.Red;
            this.btnShowInfo.Location = new System.Drawing.Point(251, 17);
            this.btnShowInfo.Name = "btnShowInfo";
            this.btnShowInfo.Size = new System.Drawing.Size(123, 27);
            this.btnShowInfo.TabIndex = 23;
            this.btnShowInfo.Text = "What\'s that for?";
            this.toolTip1.SetToolTip(this.btnShowInfo, resources.GetString("btnShowInfo.ToolTip"));
            this.btnShowInfo.UseVisualStyleBackColor = true;
            this.btnShowInfo.Click += new System.EventHandler(this.btnShowInfo_Click);
            // 
            // PgOwnerInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnShowInfo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnGetReferenceCode);
            this.Controls.Add(this.txtReferenceCode);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtReferralCode);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtPaypalEmail);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(400, 290);
            this.MinimumSize = new System.Drawing.Size(400, 290);
            this.Name = "PgOwnerInfo";
            this.Size = new System.Drawing.Size(400, 290);
            this.Validating += new System.ComponentModel.CancelEventHandler(this.PgOwnerInfo_Validating);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtReferenceCode;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtReferralCode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPaypalEmail;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button btnGetReferenceCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnShowInfo;
    }
}

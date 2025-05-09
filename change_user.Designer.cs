namespace AdminTool
{
    partial class change_user
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtSamAccountName = new System.Windows.Forms.TextBox();
            this.description_lbl = new System.Windows.Forms.Label();
            this.Depart_lbl = new System.Windows.Forms.Label();
            this.email_lbl = new System.Windows.Forms.Label();
            this.displayname_lbl = new System.Windows.Forms.Label();
            this.SAM_lbl = new System.Windows.Forms.Label();
            this.changeUsrInfoBtn = new System.Windows.Forms.Button();
            this.changeUsrPswd_lbl = new System.Windows.Forms.Label();
            this.changeUsrPswrd_txtbx = new System.Windows.Forms.TextBox();
            this.GnrtUsrPswd_btn = new System.Windows.Forms.Button();
            this.UsrNm2change_txtbx = new System.Windows.Forms.TextBox();
            this.UsrSn2change_txtbx = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbxDepartment = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePickerExpiration = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.chBx_unlockUsr = new System.Windows.Forms.CheckBox();
            this.chBx_enableUsr = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(160, 19);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(165, 20);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(331, 17);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(52, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Meklēt";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.Location = new System.Drawing.Point(160, 119);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(223, 20);
            this.txtDisplayName.TabIndex = 2;
            this.txtDisplayName.TextChanged += new System.EventHandler(this.txtDisplayName_TextChanged);
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(160, 149);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(223, 20);
            this.txtEmail.TabIndex = 3;
            this.txtEmail.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmail_KeyPress);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(160, 209);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(223, 20);
            this.txtDescription.TabIndex = 5;
            // 
            // txtSamAccountName
            // 
            this.txtSamAccountName.Location = new System.Drawing.Point(160, 239);
            this.txtSamAccountName.Name = "txtSamAccountName";
            this.txtSamAccountName.Size = new System.Drawing.Size(223, 20);
            this.txtSamAccountName.TabIndex = 6;
            this.txtSamAccountName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSamAccountName_KeyPress);
            // 
            // description_lbl
            // 
            this.description_lbl.AutoSize = true;
            this.description_lbl.Location = new System.Drawing.Point(20, 212);
            this.description_lbl.Name = "description_lbl";
            this.description_lbl.Size = new System.Drawing.Size(48, 13);
            this.description_lbl.TabIndex = 20;
            this.description_lbl.Text = "Apraksts";
            // 
            // Depart_lbl
            // 
            this.Depart_lbl.AutoSize = true;
            this.Depart_lbl.Location = new System.Drawing.Point(20, 182);
            this.Depart_lbl.Name = "Depart_lbl";
            this.Depart_lbl.Size = new System.Drawing.Size(73, 13);
            this.Depart_lbl.TabIndex = 19;
            this.Depart_lbl.Text = "Departaments";
            // 
            // email_lbl
            // 
            this.email_lbl.AutoSize = true;
            this.email_lbl.Location = new System.Drawing.Point(20, 152);
            this.email_lbl.Name = "email_lbl";
            this.email_lbl.Size = new System.Drawing.Size(78, 13);
            this.email_lbl.TabIndex = 18;
            this.email_lbl.Text = "E-pasta adrese";
            // 
            // displayname_lbl
            // 
            this.displayname_lbl.AutoSize = true;
            this.displayname_lbl.Location = new System.Drawing.Point(20, 122);
            this.displayname_lbl.Name = "displayname_lbl";
            this.displayname_lbl.Size = new System.Drawing.Size(119, 13);
            this.displayname_lbl.TabIndex = 17;
            this.displayname_lbl.Text = "Parādāmais nosaukums";
            // 
            // SAM_lbl
            // 
            this.SAM_lbl.AutoSize = true;
            this.SAM_lbl.Location = new System.Drawing.Point(22, 242);
            this.SAM_lbl.Name = "SAM_lbl";
            this.SAM_lbl.Size = new System.Drawing.Size(76, 13);
            this.SAM_lbl.TabIndex = 21;
            this.SAM_lbl.Text = "Lietotāja vārds";
            // 
            // changeUsrInfoBtn
            // 
            this.changeUsrInfoBtn.Location = new System.Drawing.Point(25, 397);
            this.changeUsrInfoBtn.Name = "changeUsrInfoBtn";
            this.changeUsrInfoBtn.Size = new System.Drawing.Size(354, 43);
            this.changeUsrInfoBtn.TabIndex = 22;
            this.changeUsrInfoBtn.Text = "Saglabāt";
            this.changeUsrInfoBtn.UseVisualStyleBackColor = true;
            this.changeUsrInfoBtn.Click += new System.EventHandler(this.changeUsrInfoBtn_Click);
            // 
            // changeUsrPswd_lbl
            // 
            this.changeUsrPswd_lbl.AutoSize = true;
            this.changeUsrPswd_lbl.Location = new System.Drawing.Point(20, 274);
            this.changeUsrPswd_lbl.Name = "changeUsrPswd_lbl";
            this.changeUsrPswd_lbl.Size = new System.Drawing.Size(37, 13);
            this.changeUsrPswd_lbl.TabIndex = 24;
            this.changeUsrPswd_lbl.Text = "Parole";
            // 
            // changeUsrPswrd_txtbx
            // 
            this.changeUsrPswrd_txtbx.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.changeUsrPswrd_txtbx.Location = new System.Drawing.Point(224, 269);
            this.changeUsrPswrd_txtbx.Name = "changeUsrPswrd_txtbx";
            this.changeUsrPswrd_txtbx.Size = new System.Drawing.Size(159, 22);
            this.changeUsrPswrd_txtbx.TabIndex = 23;
            // 
            // GnrtUsrPswd_btn
            // 
            this.GnrtUsrPswd_btn.Location = new System.Drawing.Point(160, 269);
            this.GnrtUsrPswd_btn.Name = "GnrtUsrPswd_btn";
            this.GnrtUsrPswd_btn.Size = new System.Drawing.Size(58, 22);
            this.GnrtUsrPswd_btn.TabIndex = 25;
            this.GnrtUsrPswd_btn.Text = "Ģenerēt";
            this.GnrtUsrPswd_btn.UseVisualStyleBackColor = true;
            this.GnrtUsrPswd_btn.Click += new System.EventHandler(this.GnrtUsrPswd_btn_Click);
            // 
            // UsrNm2change_txtbx
            // 
            this.UsrNm2change_txtbx.Location = new System.Drawing.Point(160, 59);
            this.UsrNm2change_txtbx.Name = "UsrNm2change_txtbx";
            this.UsrNm2change_txtbx.Size = new System.Drawing.Size(223, 20);
            this.UsrNm2change_txtbx.TabIndex = 26;
            this.UsrNm2change_txtbx.TextChanged += new System.EventHandler(this.UsrNm2change_txtbx_TextChanged);
            this.UsrNm2change_txtbx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UsrNm2change_txtbx_KeyPress);
            // 
            // UsrSn2change_txtbx
            // 
            this.UsrSn2change_txtbx.Location = new System.Drawing.Point(160, 89);
            this.UsrSn2change_txtbx.Name = "UsrSn2change_txtbx";
            this.UsrSn2change_txtbx.Size = new System.Drawing.Size(223, 20);
            this.UsrSn2change_txtbx.TabIndex = 27;
            this.UsrSn2change_txtbx.TextChanged += new System.EventHandler(this.UsrSn2change_txtbx_TextChanged);
            this.UsrSn2change_txtbx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UsrSn2change_txtbx_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Vārds";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Uzvārds";
            // 
            // cmbxDepartment
            // 
            this.cmbxDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxDepartment.FormattingEnabled = true;
            this.cmbxDepartment.Location = new System.Drawing.Point(160, 179);
            this.cmbxDepartment.Name = "cmbxDepartment";
            this.cmbxDepartment.Size = new System.Drawing.Size(223, 21);
            this.cmbxDepartment.TabIndex = 30;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Ievadiet vārdu / uzvārdu";
            // 
            // dateTimePickerExpiration
            // 
            this.dateTimePickerExpiration.Location = new System.Drawing.Point(160, 300);
            this.dateTimePickerExpiration.Name = "dateTimePickerExpiration";
            this.dateTimePickerExpiration.Size = new System.Drawing.Size(223, 20);
            this.dateTimePickerExpiration.TabIndex = 32;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 306);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "Derīguma tērmiņš";
            // 
            // chBx_unlockUsr
            // 
            this.chBx_unlockUsr.AutoSize = true;
            this.chBx_unlockUsr.Location = new System.Drawing.Point(195, 344);
            this.chBx_unlockUsr.Name = "chBx_unlockUsr";
            this.chBx_unlockUsr.Size = new System.Drawing.Size(65, 17);
            this.chBx_unlockUsr.TabIndex = 34;
            this.chBx_unlockUsr.Text = "Atbloķēt";
            this.chBx_unlockUsr.UseVisualStyleBackColor = true;
            // 
            // chBx_enableUsr
            // 
            this.chBx_enableUsr.AutoSize = true;
            this.chBx_enableUsr.Location = new System.Drawing.Point(272, 344);
            this.chBx_enableUsr.Name = "chBx_enableUsr";
            this.chBx_enableUsr.Size = new System.Drawing.Size(63, 17);
            this.chBx_enableUsr.TabIndex = 35;
            this.chBx_enableUsr.Text = "Iespējot";
            this.chBx_enableUsr.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.Location = new System.Drawing.Point(25, 327);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(121, 62);
            this.textBox1.TabIndex = 36;
            // 
            // change_user
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 450);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.chBx_enableUsr);
            this.Controls.Add(this.chBx_unlockUsr);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dateTimePickerExpiration);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbxDepartment);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UsrSn2change_txtbx);
            this.Controls.Add(this.UsrNm2change_txtbx);
            this.Controls.Add(this.GnrtUsrPswd_btn);
            this.Controls.Add(this.changeUsrPswd_lbl);
            this.Controls.Add(this.changeUsrPswrd_txtbx);
            this.Controls.Add(this.changeUsrInfoBtn);
            this.Controls.Add(this.SAM_lbl);
            this.Controls.Add(this.description_lbl);
            this.Controls.Add(this.Depart_lbl);
            this.Controls.Add(this.email_lbl);
            this.Controls.Add(this.displayname_lbl);
            this.Controls.Add(this.txtSamAccountName);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtDisplayName);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "change_user";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rediģēt lietotāju";
            this.Load += new System.EventHandler(this.change_user_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtDisplayName;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtSamAccountName;
        private System.Windows.Forms.Label description_lbl;
        private System.Windows.Forms.Label Depart_lbl;
        private System.Windows.Forms.Label email_lbl;
        private System.Windows.Forms.Label displayname_lbl;
        private System.Windows.Forms.Label SAM_lbl;
        private System.Windows.Forms.Button changeUsrInfoBtn;
        private System.Windows.Forms.Label changeUsrPswd_lbl;
        private System.Windows.Forms.TextBox changeUsrPswrd_txtbx;
        private System.Windows.Forms.Button GnrtUsrPswd_btn;
        private System.Windows.Forms.TextBox UsrNm2change_txtbx;
        private System.Windows.Forms.TextBox UsrSn2change_txtbx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbxDepartment;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePickerExpiration;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chBx_unlockUsr;
        private System.Windows.Forms.CheckBox chBx_enableUsr;
        private System.Windows.Forms.TextBox textBox1;
    }
}
namespace AdminTool
{
    partial class block_user
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
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.UsrSn2change_txtbx = new System.Windows.Forms.TextBox();
            this.UsrNm2change_txtbx = new System.Windows.Forms.TextBox();
            this.SAM_lbl = new System.Windows.Forms.Label();
            this.description_lbl = new System.Windows.Forms.Label();
            this.Depart_lbl = new System.Windows.Forms.Label();
            this.email_lbl = new System.Windows.Forms.Label();
            this.displayname_lbl = new System.Windows.Forms.Label();
            this.txtSamAccountName = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.blockUserBtn = new System.Windows.Forms.Button();
            this.cmbxDepartment = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "Ievadiet vārdu / uzvārdu";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(335, 21);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(52, 23);
            this.btnSearch.TabIndex = 33;
            this.btnSearch.Text = "Meklēt";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(164, 23);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(165, 20);
            this.txtSearch.TabIndex = 32;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 52;
            this.label2.Text = "Uzvārds";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 51;
            this.label1.Text = "Vārds";
            // 
            // UsrSn2change_txtbx
            // 
            this.UsrSn2change_txtbx.Location = new System.Drawing.Point(165, 102);
            this.UsrSn2change_txtbx.Name = "UsrSn2change_txtbx";
            this.UsrSn2change_txtbx.ReadOnly = true;
            this.UsrSn2change_txtbx.Size = new System.Drawing.Size(223, 20);
            this.UsrSn2change_txtbx.TabIndex = 50;
            // 
            // UsrNm2change_txtbx
            // 
            this.UsrNm2change_txtbx.Location = new System.Drawing.Point(165, 72);
            this.UsrNm2change_txtbx.Name = "UsrNm2change_txtbx";
            this.UsrNm2change_txtbx.ReadOnly = true;
            this.UsrNm2change_txtbx.Size = new System.Drawing.Size(223, 20);
            this.UsrNm2change_txtbx.TabIndex = 49;
            // 
            // SAM_lbl
            // 
            this.SAM_lbl.AutoSize = true;
            this.SAM_lbl.Location = new System.Drawing.Point(27, 255);
            this.SAM_lbl.Name = "SAM_lbl";
            this.SAM_lbl.Size = new System.Drawing.Size(76, 13);
            this.SAM_lbl.TabIndex = 45;
            this.SAM_lbl.Text = "Lietotāja vārds";
            // 
            // description_lbl
            // 
            this.description_lbl.AutoSize = true;
            this.description_lbl.Location = new System.Drawing.Point(25, 225);
            this.description_lbl.Name = "description_lbl";
            this.description_lbl.Size = new System.Drawing.Size(48, 13);
            this.description_lbl.TabIndex = 44;
            this.description_lbl.Text = "Apraksts";
            // 
            // Depart_lbl
            // 
            this.Depart_lbl.AutoSize = true;
            this.Depart_lbl.Location = new System.Drawing.Point(25, 195);
            this.Depart_lbl.Name = "Depart_lbl";
            this.Depart_lbl.Size = new System.Drawing.Size(73, 13);
            this.Depart_lbl.TabIndex = 43;
            this.Depart_lbl.Text = "Departaments";
            // 
            // email_lbl
            // 
            this.email_lbl.AutoSize = true;
            this.email_lbl.Location = new System.Drawing.Point(25, 165);
            this.email_lbl.Name = "email_lbl";
            this.email_lbl.Size = new System.Drawing.Size(78, 13);
            this.email_lbl.TabIndex = 42;
            this.email_lbl.Text = "E-pasta adrese";
            // 
            // displayname_lbl
            // 
            this.displayname_lbl.AutoSize = true;
            this.displayname_lbl.Location = new System.Drawing.Point(25, 135);
            this.displayname_lbl.Name = "displayname_lbl";
            this.displayname_lbl.Size = new System.Drawing.Size(119, 13);
            this.displayname_lbl.TabIndex = 41;
            this.displayname_lbl.Text = "Parādāmais nosaukums";
            // 
            // txtSamAccountName
            // 
            this.txtSamAccountName.Location = new System.Drawing.Point(165, 252);
            this.txtSamAccountName.Name = "txtSamAccountName";
            this.txtSamAccountName.ReadOnly = true;
            this.txtSamAccountName.Size = new System.Drawing.Size(223, 20);
            this.txtSamAccountName.TabIndex = 40;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(165, 222);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(223, 20);
            this.txtDescription.TabIndex = 39;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(165, 162);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.ReadOnly = true;
            this.txtEmail.Size = new System.Drawing.Size(223, 20);
            this.txtEmail.TabIndex = 38;
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.Location = new System.Drawing.Point(165, 132);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.ReadOnly = true;
            this.txtDisplayName.Size = new System.Drawing.Size(223, 20);
            this.txtDisplayName.TabIndex = 37;
            // 
            // blockUserBtn
            // 
            this.blockUserBtn.Location = new System.Drawing.Point(33, 376);
            this.blockUserBtn.Name = "blockUserBtn";
            this.blockUserBtn.Size = new System.Drawing.Size(354, 43);
            this.blockUserBtn.TabIndex = 56;
            this.blockUserBtn.Text = "Bloķēt";
            this.blockUserBtn.UseVisualStyleBackColor = true;
            this.blockUserBtn.Click += new System.EventHandler(this.blockUserBtn_Click);
            // 
            // cmbxDepartment
            // 
            this.cmbxDepartment.Location = new System.Drawing.Point(165, 192);
            this.cmbxDepartment.Name = "cmbxDepartment";
            this.cmbxDepartment.ReadOnly = true;
            this.cmbxDepartment.Size = new System.Drawing.Size(223, 20);
            this.cmbxDepartment.TabIndex = 57;
            // 
            // block_user
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 450);
            this.Controls.Add(this.cmbxDepartment);
            this.Controls.Add(this.blockUserBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UsrSn2change_txtbx);
            this.Controls.Add(this.UsrNm2change_txtbx);
            this.Controls.Add(this.SAM_lbl);
            this.Controls.Add(this.description_lbl);
            this.Controls.Add(this.Depart_lbl);
            this.Controls.Add(this.email_lbl);
            this.Controls.Add(this.displayname_lbl);
            this.Controls.Add(this.txtSamAccountName);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtDisplayName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "block_user";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bloķēt lietotāju";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox UsrSn2change_txtbx;
        private System.Windows.Forms.TextBox UsrNm2change_txtbx;
        private System.Windows.Forms.Label SAM_lbl;
        private System.Windows.Forms.Label description_lbl;
        private System.Windows.Forms.Label Depart_lbl;
        private System.Windows.Forms.Label email_lbl;
        private System.Windows.Forms.Label displayname_lbl;
        private System.Windows.Forms.TextBox txtSamAccountName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtDisplayName;
        private System.Windows.Forms.Button blockUserBtn;
        private System.Windows.Forms.TextBox cmbxDepartment;
    }
}
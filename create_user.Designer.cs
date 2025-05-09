namespace AdminTool
{
    partial class create_user
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
            this.name_lbl = new System.Windows.Forms.Label();
            this.lastname_lbl = new System.Windows.Forms.Label();
            this.displayname_lbl = new System.Windows.Forms.Label();
            this.email_lbl = new System.Windows.Forms.Label();
            this.password_lbl = new System.Windows.Forms.Label();
            this.userExpirationDateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.Expiration_lbl = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.Name_txtbx = new System.Windows.Forms.TextBox();
            this.lastname_txtbx = new System.Windows.Forms.TextBox();
            this.displayname_txtbx = new System.Windows.Forms.TextBox();
            this.email_txtbx = new System.Windows.Forms.TextBox();
            this.password_txtbx = new System.Windows.Forms.TextBox();
            this.createUserbtn = new System.Windows.Forms.Button();
            this.depart_cmbbx = new System.Windows.Forms.ComboBox();
            this.Depart_lbl = new System.Windows.Forms.Label();
            this.description_lbl = new System.Windows.Forms.Label();
            this.Description_txtbx = new System.Windows.Forms.TextBox();
            this.clearbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // name_lbl
            // 
            this.name_lbl.AutoSize = true;
            this.name_lbl.Location = new System.Drawing.Point(30, 33);
            this.name_lbl.Name = "name_lbl";
            this.name_lbl.Size = new System.Drawing.Size(34, 13);
            this.name_lbl.TabIndex = 0;
            this.name_lbl.Text = "Vārds";
            // 
            // lastname_lbl
            // 
            this.lastname_lbl.AutoSize = true;
            this.lastname_lbl.Location = new System.Drawing.Point(30, 63);
            this.lastname_lbl.Name = "lastname_lbl";
            this.lastname_lbl.Size = new System.Drawing.Size(46, 13);
            this.lastname_lbl.TabIndex = 1;
            this.lastname_lbl.Text = "Uzvārds";
            // 
            // displayname_lbl
            // 
            this.displayname_lbl.AutoSize = true;
            this.displayname_lbl.Location = new System.Drawing.Point(30, 93);
            this.displayname_lbl.Name = "displayname_lbl";
            this.displayname_lbl.Size = new System.Drawing.Size(119, 13);
            this.displayname_lbl.TabIndex = 2;
            this.displayname_lbl.Text = "Parādāmais nosaukums";
            // 
            // email_lbl
            // 
            this.email_lbl.AutoSize = true;
            this.email_lbl.Location = new System.Drawing.Point(30, 123);
            this.email_lbl.Name = "email_lbl";
            this.email_lbl.Size = new System.Drawing.Size(78, 13);
            this.email_lbl.TabIndex = 3;
            this.email_lbl.Text = "E-pasta adrese";
            // 
            // password_lbl
            // 
            this.password_lbl.AutoSize = true;
            this.password_lbl.Location = new System.Drawing.Point(30, 153);
            this.password_lbl.Name = "password_lbl";
            this.password_lbl.Size = new System.Drawing.Size(37, 13);
            this.password_lbl.TabIndex = 4;
            this.password_lbl.Text = "Parole";
            // 
            // userExpirationDateTimePicker1
            // 
            this.userExpirationDateTimePicker1.Location = new System.Drawing.Point(190, 180);
            this.userExpirationDateTimePicker1.MaxDate = new System.DateTime(2035, 12, 31, 0, 0, 0, 0);
            this.userExpirationDateTimePicker1.MinDate = new System.DateTime(2023, 1, 1, 0, 0, 0, 0);
            this.userExpirationDateTimePicker1.Name = "userExpirationDateTimePicker1";
            this.userExpirationDateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.userExpirationDateTimePicker1.TabIndex = 5;
            // 
            // Expiration_lbl
            // 
            this.Expiration_lbl.AutoSize = true;
            this.Expiration_lbl.Location = new System.Drawing.Point(30, 183);
            this.Expiration_lbl.Name = "Expiration_lbl";
            this.Expiration_lbl.Size = new System.Drawing.Size(52, 13);
            this.Expiration_lbl.TabIndex = 6;
            this.Expiration_lbl.Text = "Derīgums";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(189, 150);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(53, 22);
            this.button1.TabIndex = 7;
            this.button1.Text = "Ģenerēt";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Name_txtbx
            // 
            this.Name_txtbx.Location = new System.Drawing.Point(190, 30);
            this.Name_txtbx.Name = "Name_txtbx";
            this.Name_txtbx.Size = new System.Drawing.Size(200, 20);
            this.Name_txtbx.TabIndex = 8;
            this.Name_txtbx.TextChanged += new System.EventHandler(this.Name_txtbx_TextChanged);
            // 
            // lastname_txtbx
            // 
            this.lastname_txtbx.Location = new System.Drawing.Point(190, 60);
            this.lastname_txtbx.Name = "lastname_txtbx";
            this.lastname_txtbx.Size = new System.Drawing.Size(200, 20);
            this.lastname_txtbx.TabIndex = 9;
            this.lastname_txtbx.TextChanged += new System.EventHandler(this.lastname_txtbx_TextChanged);
            // 
            // displayname_txtbx
            // 
            this.displayname_txtbx.Location = new System.Drawing.Point(190, 90);
            this.displayname_txtbx.Name = "displayname_txtbx";
            this.displayname_txtbx.Size = new System.Drawing.Size(200, 20);
            this.displayname_txtbx.TabIndex = 10;
            this.displayname_txtbx.TextChanged += new System.EventHandler(this.displayname_txtbx_TextChanged);
            // 
            // email_txtbx
            // 
            this.email_txtbx.Location = new System.Drawing.Point(190, 120);
            this.email_txtbx.Name = "email_txtbx";
            this.email_txtbx.ReadOnly = true;
            this.email_txtbx.Size = new System.Drawing.Size(200, 20);
            this.email_txtbx.TabIndex = 11;
            // 
            // password_txtbx
            // 
            this.password_txtbx.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.password_txtbx.Location = new System.Drawing.Point(249, 150);
            this.password_txtbx.Name = "password_txtbx";
            this.password_txtbx.ReadOnly = true;
            this.password_txtbx.Size = new System.Drawing.Size(141, 22);
            this.password_txtbx.TabIndex = 12;
            // 
            // createUserbtn
            // 
            this.createUserbtn.Location = new System.Drawing.Point(33, 373);
            this.createUserbtn.Name = "createUserbtn";
            this.createUserbtn.Size = new System.Drawing.Size(357, 43);
            this.createUserbtn.TabIndex = 13;
            this.createUserbtn.Text = "Izveidot";
            this.createUserbtn.UseVisualStyleBackColor = true;
            this.createUserbtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // depart_cmbbx
            // 
            this.depart_cmbbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.depart_cmbbx.Location = new System.Drawing.Point(190, 210);
            this.depart_cmbbx.Name = "depart_cmbbx";
            this.depart_cmbbx.Size = new System.Drawing.Size(200, 21);
            this.depart_cmbbx.TabIndex = 14;
            // 
            // Depart_lbl
            // 
            this.Depart_lbl.AutoSize = true;
            this.Depart_lbl.Location = new System.Drawing.Point(30, 213);
            this.Depart_lbl.Name = "Depart_lbl";
            this.Depart_lbl.Size = new System.Drawing.Size(73, 13);
            this.Depart_lbl.TabIndex = 15;
            this.Depart_lbl.Text = "Departaments";
            // 
            // description_lbl
            // 
            this.description_lbl.AutoSize = true;
            this.description_lbl.Location = new System.Drawing.Point(30, 243);
            this.description_lbl.Name = "description_lbl";
            this.description_lbl.Size = new System.Drawing.Size(48, 13);
            this.description_lbl.TabIndex = 16;
            this.description_lbl.Text = "Apraksts";
            // 
            // Description_txtbx
            // 
            this.Description_txtbx.Location = new System.Drawing.Point(190, 240);
            this.Description_txtbx.Name = "Description_txtbx";
            this.Description_txtbx.Size = new System.Drawing.Size(200, 20);
            this.Description_txtbx.TabIndex = 17;
            // 
            // clearbtn
            // 
            this.clearbtn.BackgroundImage = global::AdminTool.Properties.Resources.eraser;
            this.clearbtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.clearbtn.Location = new System.Drawing.Point(355, 268);
            this.clearbtn.Name = "clearbtn";
            this.clearbtn.Size = new System.Drawing.Size(35, 35);
            this.clearbtn.TabIndex = 19;
            this.clearbtn.UseVisualStyleBackColor = true;
            this.clearbtn.Click += new System.EventHandler(this.clearbtn_Click);
            // 
            // create_user
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 450);
            this.Controls.Add(this.clearbtn);
            this.Controls.Add(this.Description_txtbx);
            this.Controls.Add(this.description_lbl);
            this.Controls.Add(this.Depart_lbl);
            this.Controls.Add(this.depart_cmbbx);
            this.Controls.Add(this.createUserbtn);
            this.Controls.Add(this.password_txtbx);
            this.Controls.Add(this.email_txtbx);
            this.Controls.Add(this.displayname_txtbx);
            this.Controls.Add(this.lastname_txtbx);
            this.Controls.Add(this.Name_txtbx);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Expiration_lbl);
            this.Controls.Add(this.userExpirationDateTimePicker1);
            this.Controls.Add(this.password_lbl);
            this.Controls.Add(this.email_lbl);
            this.Controls.Add(this.displayname_lbl);
            this.Controls.Add(this.lastname_lbl);
            this.Controls.Add(this.name_lbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "create_user";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Izveidot jaunu lietotāju";
            this.Load += new System.EventHandler(this.create_user_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label name_lbl;
        private System.Windows.Forms.Label lastname_lbl;
        private System.Windows.Forms.Label displayname_lbl;
        private System.Windows.Forms.Label email_lbl;
        private System.Windows.Forms.Label password_lbl;
        private System.Windows.Forms.DateTimePicker userExpirationDateTimePicker1;
        private System.Windows.Forms.Label Expiration_lbl;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox Name_txtbx;
        private System.Windows.Forms.TextBox lastname_txtbx;
        private System.Windows.Forms.TextBox displayname_txtbx;
        private System.Windows.Forms.TextBox email_txtbx;
        private System.Windows.Forms.TextBox password_txtbx;
        private System.Windows.Forms.Button createUserbtn;
        private System.Windows.Forms.ComboBox depart_cmbbx;
        private System.Windows.Forms.Label Depart_lbl;
        private System.Windows.Forms.Label description_lbl;
        private System.Windows.Forms.TextBox Description_txtbx;
        private System.Windows.Forms.Button clearbtn;
    }
}
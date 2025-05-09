namespace AdminTool
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.change_user = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.izvelneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iestatījumiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aizvertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informācijaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CreateGroup_btn = new System.Windows.Forms.Button();
            this.blockuser_btn = new System.Windows.Forms.Button();
            this.group_membership_btn = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 100);
            this.button1.TabIndex = 0;
            this.button1.Text = "Izveidot jaunu lietotāju";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(285, 288);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 100);
            this.button2.TabIndex = 1;
            this.button2.Text = "Logi";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // change_user
            // 
            this.change_user.Location = new System.Drawing.Point(12, 167);
            this.change_user.Name = "change_user";
            this.change_user.Size = new System.Drawing.Size(100, 100);
            this.change_user.TabIndex = 2;
            this.change_user.Text = "Rediģēt lietotāju";
            this.change_user.UseVisualStyleBackColor = true;
            this.change_user.Click += new System.EventHandler(this.change_user_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(413, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(398, 17);
            this.statusLabel.Spring = true;
            this.statusLabel.Text = "DomainConnection";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.izvelneToolStripMenuItem,
            this.informācijaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(413, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // izvelneToolStripMenuItem
            // 
            this.izvelneToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iestatījumiToolStripMenuItem,
            this.aizvertToolStripMenuItem});
            this.izvelneToolStripMenuItem.Name = "izvelneToolStripMenuItem";
            this.izvelneToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.izvelneToolStripMenuItem.Text = "Izvelne";
            // 
            // iestatījumiToolStripMenuItem
            // 
            this.iestatījumiToolStripMenuItem.Name = "iestatījumiToolStripMenuItem";
            this.iestatījumiToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.iestatījumiToolStripMenuItem.Text = "Iestatījumi";
            // 
            // aizvertToolStripMenuItem
            // 
            this.aizvertToolStripMenuItem.Name = "aizvertToolStripMenuItem";
            this.aizvertToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.aizvertToolStripMenuItem.Text = "Aizvert";
            this.aizvertToolStripMenuItem.Click += new System.EventHandler(this.aizvertToolStripMenuItem_Click);
            // 
            // informācijaToolStripMenuItem
            // 
            this.informācijaToolStripMenuItem.Name = "informācijaToolStripMenuItem";
            this.informācijaToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.informācijaToolStripMenuItem.Text = "Informācija";
            // 
            // CreateGroup_btn
            // 
            this.CreateGroup_btn.Location = new System.Drawing.Point(146, 50);
            this.CreateGroup_btn.Name = "CreateGroup_btn";
            this.CreateGroup_btn.Size = new System.Drawing.Size(100, 100);
            this.CreateGroup_btn.TabIndex = 6;
            this.CreateGroup_btn.Text = "Grupu pārvalde";
            this.CreateGroup_btn.UseVisualStyleBackColor = true;
            this.CreateGroup_btn.Click += new System.EventHandler(this.CreateGroup_btn_Click);
            // 
            // blockuser_btn
            // 
            this.blockuser_btn.Location = new System.Drawing.Point(12, 288);
            this.blockuser_btn.Name = "blockuser_btn";
            this.blockuser_btn.Size = new System.Drawing.Size(100, 100);
            this.blockuser_btn.TabIndex = 7;
            this.blockuser_btn.Text = "Bloķēt lietotāju";
            this.blockuser_btn.UseVisualStyleBackColor = true;
            this.blockuser_btn.Click += new System.EventHandler(this.blockuser_btn_Click);
            // 
            // group_membership_btn
            // 
            this.group_membership_btn.Location = new System.Drawing.Point(146, 167);
            this.group_membership_btn.Name = "group_membership_btn";
            this.group_membership_btn.Size = new System.Drawing.Size(100, 100);
            this.group_membership_btn.TabIndex = 8;
            this.group_membership_btn.Text = "Dalība grupās";
            this.group_membership_btn.UseVisualStyleBackColor = true;
            this.group_membership_btn.Click += new System.EventHandler(this.group_membership_btn_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 450);
            this.Controls.Add(this.group_membership_btn);
            this.Controls.Add(this.blockuser_btn);
            this.Controls.Add(this.CreateGroup_btn);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.change_user);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AD Lietotāju Pārvaldības Rīks";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button change_user;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem izvelneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iestatījumiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aizvertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informācijaToolStripMenuItem;
        private System.Windows.Forms.Button CreateGroup_btn;
        private System.Windows.Forms.Button blockuser_btn;
        private System.Windows.Forms.Button group_membership_btn;
    }
}


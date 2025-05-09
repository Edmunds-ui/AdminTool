namespace AdminTool
{
    partial class group_membership
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
            this.grps_lstbx = new System.Windows.Forms.ListBox();
            this.groupmembers_lstbx = new System.Windows.Forms.ListBox();
            this.allusers_lstbox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.add_btn = new System.Windows.Forms.Button();
            this.rem_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // grps_lstbx
            // 
            this.grps_lstbx.FormattingEnabled = true;
            this.grps_lstbx.Location = new System.Drawing.Point(12, 38);
            this.grps_lstbx.Name = "grps_lstbx";
            this.grps_lstbx.Size = new System.Drawing.Size(389, 82);
            this.grps_lstbx.TabIndex = 0;
            this.grps_lstbx.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // groupmembers_lstbx
            // 
            this.groupmembers_lstbx.FormattingEnabled = true;
            this.groupmembers_lstbx.Location = new System.Drawing.Point(236, 161);
            this.groupmembers_lstbx.Name = "groupmembers_lstbx";
            this.groupmembers_lstbx.Size = new System.Drawing.Size(165, 277);
            this.groupmembers_lstbx.TabIndex = 1;
            // 
            // allusers_lstbox
            // 
            this.allusers_lstbox.FormattingEnabled = true;
            this.allusers_lstbox.Location = new System.Drawing.Point(12, 161);
            this.allusers_lstbox.Name = "allusers_lstbox";
            this.allusers_lstbox.Size = new System.Drawing.Size(165, 277);
            this.allusers_lstbox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Grupu saraksts";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Lietotāju saraksts";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(233, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Grupas dalībnieki";
            // 
            // add_btn
            // 
            this.add_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.add_btn.Location = new System.Drawing.Point(187, 214);
            this.add_btn.Name = "add_btn";
            this.add_btn.Size = new System.Drawing.Size(39, 37);
            this.add_btn.TabIndex = 6;
            this.add_btn.Text = "->";
            this.add_btn.UseVisualStyleBackColor = true;
            this.add_btn.Click += new System.EventHandler(this.add_btn_Click);
            // 
            // rem_btn
            // 
            this.rem_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rem_btn.Location = new System.Drawing.Point(187, 257);
            this.rem_btn.Name = "rem_btn";
            this.rem_btn.Size = new System.Drawing.Size(39, 37);
            this.rem_btn.TabIndex = 7;
            this.rem_btn.Text = "<-";
            this.rem_btn.UseVisualStyleBackColor = true;
            this.rem_btn.Click += new System.EventHandler(this.rem_btn_Click);
            // 
            // group_membership
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 450);
            this.Controls.Add(this.rem_btn);
            this.Controls.Add(this.add_btn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.allusers_lstbox);
            this.Controls.Add(this.groupmembers_lstbx);
            this.Controls.Add(this.grps_lstbx);
            this.MaximizeBox = false;
            this.Name = "group_membership";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dalība grupās";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox grps_lstbx;
        private System.Windows.Forms.ListBox groupmembers_lstbx;
        private System.Windows.Forms.ListBox allusers_lstbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button add_btn;
        private System.Windows.Forms.Button rem_btn;
    }
}
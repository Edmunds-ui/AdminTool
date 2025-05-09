namespace AdminTool
{
    partial class create_group
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
            this.newGroupName = new System.Windows.Forms.TextBox();
            this.groups_listBox = new System.Windows.Forms.ListBox();
            this.create_group_btn = new System.Windows.Forms.Button();
            this.delete_group_btn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // newGroupName
            // 
            this.newGroupName.Location = new System.Drawing.Point(20, 27);
            this.newGroupName.Name = "newGroupName";
            this.newGroupName.Size = new System.Drawing.Size(349, 20);
            this.newGroupName.TabIndex = 1;
            // 
            // groups_listBox
            // 
            this.groups_listBox.FormattingEnabled = true;
            this.groups_listBox.Location = new System.Drawing.Point(20, 19);
            this.groups_listBox.Name = "groups_listBox";
            this.groups_listBox.Size = new System.Drawing.Size(349, 251);
            this.groups_listBox.TabIndex = 2;
            // 
            // create_group_btn
            // 
            this.create_group_btn.Location = new System.Drawing.Point(224, 53);
            this.create_group_btn.Name = "create_group_btn";
            this.create_group_btn.Size = new System.Drawing.Size(145, 23);
            this.create_group_btn.TabIndex = 4;
            this.create_group_btn.Text = "Izveidot";
            this.create_group_btn.UseVisualStyleBackColor = true;
            this.create_group_btn.Click += new System.EventHandler(this.create_group_btn_Click);
            // 
            // delete_group_btn
            // 
            this.delete_group_btn.Location = new System.Drawing.Point(224, 287);
            this.delete_group_btn.Name = "delete_group_btn";
            this.delete_group_btn.Size = new System.Drawing.Size(145, 23);
            this.delete_group_btn.TabIndex = 5;
            this.delete_group_btn.Text = "Izdzēst";
            this.delete_group_btn.UseVisualStyleBackColor = true;
            this.delete_group_btn.Click += new System.EventHandler(this.delete_group_btn_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.create_group_btn);
            this.groupBox1.Controls.Add(this.newGroupName);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(389, 90);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Izveidot jaunu grupu";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.delete_group_btn);
            this.groupBox2.Controls.Add(this.groups_listBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 108);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(389, 330);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Grupu saraksts";
            // 
            // create_group
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "create_group";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Grupu pārvalde";
            this.Load += new System.EventHandler(this.create_group_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox newGroupName;
        private System.Windows.Forms.ListBox groups_listBox;
        private System.Windows.Forms.Button create_group_btn;
        private System.Windows.Forms.Button delete_group_btn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}
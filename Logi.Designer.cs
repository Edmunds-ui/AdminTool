namespace AdminTool
{
    partial class Logi
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.SQLQueryFilter_valueTXT = new System.Windows.Forms.TextBox();
            this.btn_SQLQueryLogs = new System.Windows.Forms.Button();
            this.btn_ResetSQLQuery = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 39);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(847, 399);
            this.dataGridView1.TabIndex = 0;
            // 
            // SQLQueryFilter_valueTXT
            // 
            this.SQLQueryFilter_valueTXT.Location = new System.Drawing.Point(104, 12);
            this.SQLQueryFilter_valueTXT.Name = "SQLQueryFilter_valueTXT";
            this.SQLQueryFilter_valueTXT.Size = new System.Drawing.Size(234, 20);
            this.SQLQueryFilter_valueTXT.TabIndex = 1;
            // 
            // btn_SQLQueryLogs
            // 
            this.btn_SQLQueryLogs.Location = new System.Drawing.Point(353, 10);
            this.btn_SQLQueryLogs.Name = "btn_SQLQueryLogs";
            this.btn_SQLQueryLogs.Size = new System.Drawing.Size(75, 23);
            this.btn_SQLQueryLogs.TabIndex = 2;
            this.btn_SQLQueryLogs.Text = "Meklēt";
            this.btn_SQLQueryLogs.UseVisualStyleBackColor = true;
            this.btn_SQLQueryLogs.Click += new System.EventHandler(this.btn_SQLQueryLogs_Click);
            // 
            // btn_ResetSQLQuery
            // 
            this.btn_ResetSQLQuery.Location = new System.Drawing.Point(434, 9);
            this.btn_ResetSQLQuery.Name = "btn_ResetSQLQuery";
            this.btn_ResetSQLQuery.Size = new System.Drawing.Size(85, 23);
            this.btn_ResetSQLQuery.TabIndex = 3;
            this.btn_ResetSQLQuery.Text = "Atiestatīt filtrus";
            this.btn_ResetSQLQuery.UseVisualStyleBackColor = true;
            this.btn_ResetSQLQuery.Click += new System.EventHandler(this.btn_ResetSQLQuery_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Meklēt:";
            // 
            // Logi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_ResetSQLQuery);
            this.Controls.Add(this.btn_SQLQueryLogs);
            this.Controls.Add(this.SQLQueryFilter_valueTXT);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Logi";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Logi";
            this.Load += new System.EventHandler(this.Logi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox SQLQueryFilter_valueTXT;
        private System.Windows.Forms.Button btn_SQLQueryLogs;
        private System.Windows.Forms.Button btn_ResetSQLQuery;
        private System.Windows.Forms.Label label1;
    }
}
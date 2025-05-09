using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AdminTool
{
    public partial class Logi : Form
    {
        public Logi()
        {
            InitializeComponent();
        }

        private void SQLQueryForLogs(string filter_value)
        {
            string connectionString = @"Server=mssql;Database=bakalaura_darbs;Integrated Security=True;";

            string selectQuery = "SELECT * FROM dbo.logi WHERE EventMessage LIKE @filter ORDER BY LogID DESC";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        // Добавляем параметр для фильтрации
                        command.Parameters.AddWithValue("@filter", "%" + filter_value + "%");

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("SQL Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Logi_Load(object sender, EventArgs e)
        {
            SQLQueryForLogs("");
        }

        private void btn_ResetSQLQuery_Click(object sender, EventArgs e)
        {
            SQLQueryForLogs("");
        }

        private void btn_SQLQueryLogs_Click(object sender, EventArgs e)
        {
            SQLQueryForLogs(SQLQueryFilter_valueTXT.Text);
        }
    }
}

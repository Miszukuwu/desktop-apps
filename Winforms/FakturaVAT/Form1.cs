using System.Data;
using Microsoft.Data.SqlClient;

namespace FakturaVAT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void plikPlikToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewProductForm form2 = new NewProductForm(this);
            form2.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadData();
        }
        public void loadData()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=X:\FakturaVAT\Database\base.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True";
            string query = "SELECT * FROM Towary";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }
        public void DataGridView1_DeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int id = (int)e.Row.Cells[0].Value;
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=X:\FakturaVAT\Database\base.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True";
            string query = $"DELETE FROM Towary WHERE Id={id}";
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand command = conn.CreateCommand();
            command.CommandText = query;
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
        }
        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int id = (int)dataGridView1.Rows[e.RowIndex].Cells["Id"].Value;
            string changedColumn = dataGridView1.Columns[e.ColumnIndex].HeaderText;
            object changedValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            string query;
            if (changedValue.GetType() == typeof(int)) {
                query = $"UPDATE Towary SET {changedColumn}={changedValue} WHERE Id={id}";
            }
            else {
                query = $"UPDATE Towary SET {changedColumn}='{changedValue}' WHERE Id={id}";
            }
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=X:\FakturaVAT\Database\base.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True";
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand command = conn.CreateCommand();
            command.CommandText = query;
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
        }
    }
}

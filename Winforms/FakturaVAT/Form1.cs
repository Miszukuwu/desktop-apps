using System.Data;
using System.Reflection;
using Microsoft.Data.SqlClient;

namespace FakturaVAT
{
    public partial class Form1 : Form
    {
        public static string ConnectionString { get; private set; }
        public Form1()
        {
            string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Form1.ConnectionString = @$"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={directory}\Database\base.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True";
            InitializeComponent();
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
            string query = "SELECT * FROM Towary";
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }
        public void DataGridView1_DeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (e.Row == null) { return; }

            DialogResult result = MessageBox.Show("Czy napewno chcesz usun¹æ towar?", "Usuwanie Towaru", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                int id = (int)e.Row.Cells[0].Value;
                deleteRecord(id);
            }
            else
            {
                e.Cancel = true;
            }
        }
        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int id = (int)dataGridView1.Rows[e.RowIndex].Cells["Id"].Value;
            string changedColumn = dataGridView1.Columns[e.ColumnIndex].HeaderText;
            object changedValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            string query;
            if (changedValue is double || changedValue is int || changedValue is float)
            {
                query = $"UPDATE Towary SET {changedColumn}={changedValue.ToString().Replace(',','.')} WHERE Id={id}";
            }
            else
            {
                query = $"UPDATE Towary SET {changedColumn}='{changedValue}' WHERE Id={id}";
            }
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand command = conn.CreateCommand();
            command.CommandText = query;
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
        }
        private void deleteRecord(int id)
        {
            string query = $"DELETE FROM Towary WHERE Id={id}";
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand command = conn.CreateCommand();
            command.CommandText = query;
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
        }
    }
}

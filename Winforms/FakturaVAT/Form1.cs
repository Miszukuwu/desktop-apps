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
            AddNewProduct form2 = new AddNewProduct();
            form2.parentForm = this;
            form2.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadData();
        }
        public void loadData()
        {
            string path = @"X:\FAKTURAVAT\DATABASE\BASE.MDF";
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
    }
}

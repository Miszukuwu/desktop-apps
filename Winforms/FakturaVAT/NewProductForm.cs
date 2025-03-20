using System.Data;
using System.Security.Cryptography;
using Microsoft.Data.SqlClient;

namespace FakturaVAT
{
    public partial class NewProductForm : Form
    {
        public readonly Form1 parentForm;
        
        public NewProductForm(Form1 parentForm)
        {
            this.parentForm = parentForm;
            InitializeComponent();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void add_Click(object sender, EventArgs e)
        {
            String name = textBox1.Text;
            int amount = (int)numericUpDown1.Value;
            float price = (float)numericUpDown2.Value;
            int vat = Convert.ToInt32(comboBox1.SelectedItem);
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=X:\FakturaVAT\Database\base.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            command.CommandText = "SELECT id FROM Towary ORDER BY id DESC";
            SqlDataReader dataReader = command.ExecuteReader();
            int id = 1;
            if (dataReader.Read())
            {
                id = dataReader.GetInt32("id") + 1;
            }
            dataReader.Close();
            command.CommandText = $"INSERT INTO Towary VALUES ({id},'{name}', {amount}, {price.ToString().Replace(',', '.')}, {vat});";
            command.ExecuteNonQuery();
            conn.Close();
            parentForm.loadData();
            this.Close();
        }
    }
}

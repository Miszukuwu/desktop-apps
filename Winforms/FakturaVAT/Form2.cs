using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FakturaVAT
{
    public partial class AddNewProduct : Form
    {
        public Form1 parentForm;
        public AddNewProduct()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void add_Click(object sender, EventArgs e)
        {
            String name = textBox1.Text;
            int amount = Convert.ToInt32(textBox2.Text);
            float price = float.Parse(textBox3.Text);
            int vat = Convert.ToInt32(comboBox1.Text);
            string path = @"X:\FAKTURAVAT\DATABASE\BASE.MDF";
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=X:\FakturaVAT\Database\base.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            command.CommandText = "SELECT id FROM Towary ORDER BY id DESC";
            SqlDataReader dataReader = command.ExecuteReader();
            int id = 1;
            if (dataReader.Read())
            {
                id = dataReader.GetInt32("id")+1;
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

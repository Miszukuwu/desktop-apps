using System.Data.Common;
using System.Data.SqlClient;
using System.Xml.XPath;
namespace dbconnection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection connection;
        private void button1_Click(object sender, EventArgs e)
        {

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aowsinski\Documents\desktop-apps\Winforms\dbconnection\db.mdf;Integrated Security=True;Connect Timeout=30";
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    Result.Text = "Po³¹czono z baz¹ danych";
                }
            }
            catch (Exception ex)
            {
                Result.Text = ex.Message;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (connection == null || connection.State != System.Data.ConnectionState.Open)
            {
                Result.Text = "Nie po³¹czono z baz¹ danych";
                return;
            }
            connection.Close();
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                Result.Text = "Roz³¹czono z baz¹";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (connection == null || connection.State != System.Data.ConnectionState.Open)
            {
                Result.Text = "Nie po³¹czono z baz¹ danych";
                return;
            }
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "CREATE TABLE Students(id int PRIMARY KEY, firstname VARCHAR(50), lastname VARCHAR(50));";
            try
            {
                command.ExecuteNonQuery();
                Result.Text = "Dodano tabele";
            }
            catch (Exception ex)
            {
                Result.Text = ex.Message;
            }
        }
    }
}

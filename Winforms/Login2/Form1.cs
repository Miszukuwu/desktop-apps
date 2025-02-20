namespace Login2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = Login.Text;
            string pass = Pass.Text;
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Uzupe³nij dane!");
                return;
            }
            if (login == "Admin" && pass == "Admin")
            {
                MessageBox.Show("Logowanie OK");
            }
            else
            {
                MessageBox.Show("Niepoprawne dane");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

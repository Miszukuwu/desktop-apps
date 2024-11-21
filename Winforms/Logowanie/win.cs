using System;
using System.Windows.Forms;
using System.Drawing;

public class Form1 : Form
{
    Label formLabel = new Label();
    TextBox loginTextBox = new TextBox();
    TextBox passTextBox = new TextBox();
    Label loginLabel = new Label();
    Label passLabel = new Label();
    PictureBox pictureBox = new PictureBox();
    Button loginButton = new Button();
    
    string login = "User";
    string pass = "123";
    
    public static void Main ()
    {
        Application.Run(new Form1());
    }

    public Form1 ()
    {
        formLabel.Font = new Font("Arial", 15f);
        formLabel.Text = "LOGOWANIE";
        formLabel.AutoSize = true;
        formLabel.Location = new Point(this.Width/2-formLabel.Width/2, 10);
        
        pictureBox.Size = new Size(75, 75);
        pictureBox.Location = new Point(10, 50);
        pictureBox.ImageLocation = "./padlock.png";
        pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        
        loginTextBox.Location = new Point(this.Width/2-10, pictureBox.Top);
        loginLabel.Text = "LOGIN";
        loginLabel.AutoSize = true;
        loginLabel.Location = new Point(loginTextBox.Left - loginLabel.Width - 10, loginTextBox.Location.Y);
        
        passTextBox.Location = new Point(loginTextBox.Left, loginTextBox.Top + loginTextBox.Height + 10);
        passTextBox.PasswordChar = '*';
        passLabel.Text = "HASŁO";
        passLabel.AutoSize = true;
        passLabel.Location = new Point(passTextBox.Left - passLabel.Width - 10, passTextBox.Location.Y);
        
        loginButton.Size = new Size(passTextBox.Width, passTextBox.Height+5);
        loginButton.Location = new Point(passTextBox.Left, passTextBox.Top + loginButton.Height + 10);
        loginButton.Click += new EventHandler(przycisk_click);
        loginButton.Text = "Ok";

        pictureBox.Location = new Point(pictureBox.Location.X, pictureBox.Location.Y-10);
        pictureBox.Size = new Size(pictureBox.Size.Width, passTextBox.Top+passTextBox.Height-pictureBox.Top+15);
        
        /*
        formLabel.BorderStyle = BorderStyle.FixedSingle;
        pictureBox.BorderStyle = BorderStyle.FixedSingle;
        loginLabel.BorderStyle = BorderStyle.FixedSingle;
        passLabel.BorderStyle = BorderStyle.FixedSingle;
        */
        
        this.Controls.Add(formLabel);
        this.Controls.Add(pictureBox);
        this.Controls.Add(loginLabel);
        this.Controls.Add(loginTextBox);
        this.Controls.Add(passLabel);
        this.Controls.Add(passTextBox);
        this.Controls.Add(loginButton);
        
        this.Size = new Size(300, 200);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
    }

    private void przycisk_click(object sender, EventArgs e)
    {
        string message;
        string title = "Logowanie";
        if (loginTextBox.Text.Equals(login) && passTextBox.Text.Equals(pass)) {
            message = "Zalogowano";
        } else {
            message = "Nie zalogowano! Błędne dane";
        }
        MessageBox.Show(message, title);
    }
}
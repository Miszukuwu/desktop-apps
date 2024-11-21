using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using System.Data;

namespace mp3Player;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        string myConnString = "Server=ubuntu.domena.net;Database=aowsinski;Uid=aowsinski;Pwd=" + Connection.getPass();
        try
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = myConnString;
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM ustawienia WHERE id=1";
            MySqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();
            this.Title = dataReader.GetString("nazwa");
            t1.Text = dataReader.GetString("nazwa");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    public void ClickHandel(object sender, RoutedEventArgs e)
    {
        message.Text = "Kliknięto!";   
    }
}
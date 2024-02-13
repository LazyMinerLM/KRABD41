using System.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace UPIlyaM41;

public partial class Vhod : Window
{
    private string connectionString = "server=localhost;database=ilyam41;port=3306;User Id=root;password=root";
    private MySqlConnection connection;
    public Vhod()
    {
        InitializeComponent();
    }

    private void Login22(object? sender, RoutedEventArgs e)
    {
        connection = new MySqlConnection(connectionString);
        connection.Open();
        DataTable table = new DataTable();
        string query = $"SELECT LogIn, Password FROM Users WHERE LogIn LIKE '{LogIn.Text}' AND Password LIKE '{Password.Text}'";
        MySqlDataAdapter adapter = new MySqlDataAdapter(query,connection);
        adapter.Fill(table);
        if (LogIn.Text == "sa" && Password.Text == "root")
        {
            DevelWin dw = new DevelWin();
            dw.Show();
            this.Hide();
        }
        else
        {
            if (table.Rows.Count > 0)
            {
                Main main = new Main();
                main.Show();
                this.Hide();
            }
            LogIn.Text = "";
            Password.Text = "";
        }

        
        connection.Close();
    }
}

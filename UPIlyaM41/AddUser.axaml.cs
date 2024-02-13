using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace UPIlyaM41;

public partial class AddUser : Window
{
    private string connectionString = "server=localhost;database=ilyam41;port=3306;User Id=root;password=root";
    private MySqlConnection connection;
    public AddUser()
    {
        InitializeComponent();
    }

    private void AddUser_OnClick(object? sender, RoutedEventArgs e)
    {
        connection = new MySqlConnection(connectionString);
        connection.Open();
        string query = $"INSERT INTO Users(LogIn, Password) VALUES ('{LogInTxt.Text}', '{PasswordTxt.Text}')";
        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.ExecuteNonQuery();
        connection.Close();
    }

    private void CloseAdd(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace UPIlyaM41;

public partial class EditUser : Window
{
    private string connectionString = "server=localhost;database=ilyam41;port=3306;User Id=root;password=root";
    private MySqlConnection connection;
    public EditUser()
    {
        InitializeComponent();
    }

    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        connection = new MySqlConnection(connectionString);
        connection.Open();
        string query = $"UPDATE Users SET LogIn ='{LogInTxt.Text}', Password ='{PasswordTxt.Text}' WHERE id ={Convert.ToInt32(ID.Text)}";
        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.ExecuteNonQuery();
        connection.Close();
    }

    private void CloseAddEdit(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace UPIlyaM41;

public partial class UsersTable : Window
{
    private List<Users> _userList;
    private string connectionString = "server=localhost;database=ilyam41;port=3306;User Id=root;password=root";
    private MySqlConnection connection;
    public UsersTable()
    {
        InitializeComponent();
        string query = "SELECT * FROM Users";
        ShowTable(query);
    }
    public void ShowTable(string query)
    {
        _userList = new List<Users>();
        connection = new MySqlConnection(connectionString);
        connection.Open();
        MySqlCommand command = new MySqlCommand(query, connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentUser = new Users()
            {
                id = reader.GetInt32("id"),
                LogIn = reader.GetString("LogIn"),
                Password = reader.GetString("Password")
            };
            _userList.Add(currentUser);
        }

        connection.Close();
        PlanGrid.ItemsSource = _userList;
    }
    private void SearchButton_OnClick(object? sender, RoutedEventArgs e)
    {
            string search = $"SELECT * FROM Users WHERE LogIn LIKE '%{Search.Text}%' OR Password LIKE '%{Search.Text}%'";
            ShowTable(search);
    }
    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        DevelWin Dmain = new DevelWin();
        Dmain.Show();
        this.Close();
    }

    private void AddData(object? sender, RoutedEventArgs e)
    {
        AddUser au = new AddUser();
        au.Show();
    }

    private void EditData(object? sender, RoutedEventArgs e)
    {
        EditUser eu = new EditUser();
        eu.Show();
    }

    private void DeleteData(object? sender, RoutedEventArgs e)
    {
        Users currentUser = PlanGrid.SelectedItem as Users;
        if (currentUser == null)
        {
            return;
        }
        connection = new MySqlConnection(connectionString);
        connection.Open();
        string query = "DELETE FROM Users WHERE ID = " + currentUser.id;
        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.ExecuteNonQuery();
        connection.Close();
        _userList.Remove(currentUser);
        ShowTable("SELECT * FROM Users");
    }

}
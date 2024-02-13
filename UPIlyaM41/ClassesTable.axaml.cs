using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace UPIlyaM41;

public partial class ClassesTable : Window
{
    private string connectionString = "server=localhost;database=ilyam41;port=3306;User Id=root;password=root";
    private MySqlConnection connection;
    public ClassesTable()
    {
        InitializeComponent();
        string query = "SELECT * FROM Classes";
        ShowTable(query);
    }
    public void ShowTable(string query)
    {
        List<Classes> _classList = new List<Classes>();
        connection = new MySqlConnection(connectionString);
        connection.Open();
        MySqlCommand command = new MySqlCommand(query, connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentClass = new Classes()
            {
                id = reader.GetInt32("id"),
                className = reader.GetString("class")
            };
            _classList.Add(currentClass);
        }

        connection.Close();
        PlanGrid.ItemsSource = _classList;
    }

    private void Search_OnClick(object? sender, RoutedEventArgs e)
    {
        string search = $"SELECT * FROM Classes WHERE Class LIKE '%{Search.Text}%'";
        ShowTable(search);
    }

    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        Main main = new Main();
        main.Show();
        this.Close();
    }
}
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace UPIlyaM41;

public partial class TeachersTable : Window
{
    private string connectionString = "server=localhost;database=ilyam41;port=3306;User Id=root;password=root";
    private MySqlConnection connection;
    public TeachersTable()
    {
        InitializeComponent();
        string query = "SELECT * FROM Teachers";
        ShowTable(query);

    }
    public void ShowTable(string query)
    {
        List<Teachers> _teachertList = new List<Teachers>();
        connection = new MySqlConnection(connectionString);
        connection.Open();
        MySqlCommand command = new MySqlCommand(query, connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentTeacher = new Teachers()
            {
                id = reader.GetInt32("id"),
                surname = reader.GetString("surname"),
                name = reader.GetString("name"),
                middleName = reader.GetString("middleName")
            };
            _teachertList.Add(currentTeacher);
        }

        connection.Close();
        PlanGrid.ItemsSource = _teachertList;
    }
    private void Search_OnClick(object? sender, RoutedEventArgs e)
    {
        string search = $"SELECT * FROM Teachers WHERE surname LIKE '%{Search.Text}%' OR name LIKE '%{Search.Text}%' OR middleName LIKE '%{Search.Text}%'";
        ShowTable(search);
    }

    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        Main main = new Main();
        main.Show();
        this.Close();
        
    }
}
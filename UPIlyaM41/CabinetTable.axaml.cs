using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace UPIlyaM41;

public partial class CabinetTable : Window
{
    private List<Cabinets> _cabinetsList;
    private string connectionString = "server=localhost;database=ilyam41;port=3306;User Id=root;password=root";
    private MySqlConnection connection;
    public CabinetTable()
    {
        InitializeComponent();
        string query = "SELECT * FROM Cabinets";
        ShowTable(query);
    }

    public void ShowTable(string query)
    {
        List<Cabinets> _cabinetList = new List<Cabinets>();
        connection = new MySqlConnection(connectionString);
        connection.Open();
        MySqlCommand command = new MySqlCommand(query, connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentCabinet = new Cabinets()
            {
                id = reader.GetInt32("id"),
                cabinet = reader.GetString("cabinet")
            };
            _cabinetList.Add(currentCabinet);
        }

        connection.Close();
        PlanGrid.ItemsSource = _cabinetList;
    }

    private void Search_OnClick(object? sender, RoutedEventArgs e)
    {
        string search = $"SELECT * FROM Cabinets WHERE Cabinet LIKE '%{Search.Text}%'";
        ShowTable(search);
    }

    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        Main main = new Main();
        main.Show();
        this.Close();
    }
}
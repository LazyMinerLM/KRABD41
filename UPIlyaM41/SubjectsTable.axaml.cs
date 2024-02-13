using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace UPIlyaM41;

public partial class SubjectsTable : Window
{
    private string connectionString = "server=localhost;database=ilyam41;port=3306;User Id=root;password=root";
    private MySqlConnection connection;
    public SubjectsTable()
    {
        InitializeComponent();
        string query = "SELECT * FROM Subjects";
        ShowTable(query);
    }
    public void ShowTable(string query)
    {
        List<Subjects> _subjectList = new List<Subjects>();
        connection = new MySqlConnection(connectionString);
        connection.Open();
        MySqlCommand command = new MySqlCommand(query, connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentSubject = new Subjects()
            {
                id = reader.GetInt32("id"),
                subject = reader.GetString("subject")
            };
            _subjectList.Add(currentSubject);
        }

        connection.Close();
        PlanGrid.ItemsSource = _subjectList;
    }

    private void Search_OnClick(object? sender, RoutedEventArgs e)
    {
        string search = $"SELECT * FROM Subjects WHERE subject LIKE '%{Search.Text}%'";
        ShowTable(search);
    }

    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        Main main = new Main();
        main.Show();
        this.Close();
        
    }
}
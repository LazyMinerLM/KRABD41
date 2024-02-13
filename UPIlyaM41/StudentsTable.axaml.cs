using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace UPIlyaM41;

public partial class StudentsTable : Window
{
    private string connectionString = "server=localhost;database=ilyam41;port=3306;User Id=root;password=root";
    private MySqlConnection connection;
    public StudentsTable()
    {
        InitializeComponent();
        string query = "SELECT Students.id, Students.surname, Students.name, Classes.class FROM Students INNER JOIN Classes ON Students.class_id = Classes.id";
        ShowTable(query);
    }
    public void ShowTable(string query)
    {
        List<Students> _studentList = new List<Students>();
        connection = new MySqlConnection(connectionString);
        connection.Open();
        MySqlCommand command = new MySqlCommand(query, connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentStudent = new Students()
            {
                id = reader.GetInt32("id"),
                surname = reader.GetString("surname"),
                name = reader.GetString("name"),
                class_id = reader.GetString("class")
            };
            _studentList.Add(currentStudent);
        }

        connection.Close();
        PlanGrid.ItemsSource = _studentList;
    }
    private void Search_OnClick(object? sender, RoutedEventArgs e)
    {
        string search = $"SELECT Students.id, Students.surname, Students.name, Classes.class FROM Students INNER JOIN Classes ON Students.class_id = Classes.id WHERE Students.surname LIKE '%{Search.Text}%' OR Students.name LIKE '%{Search.Text}%' OR Classes.Class LIKE '%{Search.Text}%'";
        ShowTable(search);
    }

    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        Main main = new Main();
        main.Show();
        this.Close();
        
    }
}
using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace UPIlyaM41;

public partial class ScoresTable : Window
{
    private string connectionString = "server=localhost;database=ilyam41;port=3306;User Id=root;password=root";
    private MySqlConnection connection;
    public ScoresTable()
    {
        InitializeComponent();
        string query = "SELECT Scores.id, Students.surname, Students.name, Subjects.subject, Scores.score FROM Scores INNER JOIN Students ON Scores.student_id = Students.id INNER JOIN Subjects ON Scores.subject_id = Subjects.id";
        ShowTable(query);
    }
    public void ShowTable(string query)
    {
        List<Scores> _scoreList = new List<Scores>();
        connection = new MySqlConnection(connectionString);
        connection.Open();
        MySqlCommand command = new MySqlCommand(query, connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentScore = new Scores()
            {
                id = reader.GetInt32("id"),
                surname = reader.GetString("surname"),
                name = reader.GetString("name"),
                subject = reader.GetString("subject"),
                score = reader.GetInt32("score")
            };
            _scoreList.Add(currentScore);
        }

        connection.Close();
        PlanGrid.ItemsSource = _scoreList;
    }
    private void Search_OnClick(object? sender, RoutedEventArgs e)
    {
        string search = $"SELECT Scores.id, Students.surname, Students.name, Subjects.subject, Scores.score FROM Scores INNER JOIN Students ON Scores.student_id = Students.id INNER JOIN Subjects ON Scores.subject_id = Subjects.id WHERE Students.surname LIKE '%{Search.Text}%' OR Students.name LIKE '%{Search.Text}%' OR Subjects.subject LIKE '%{Search.Text}%'";
        ShowTable(search);
    }

    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        Main main = new Main();
        main.Show();
        this.Close();
        
    }
}
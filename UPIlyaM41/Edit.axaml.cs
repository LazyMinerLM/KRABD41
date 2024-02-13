using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace UPIlyaM41;

public partial class Edit : Window
{
    private string connectionString = "server=localhost;database=ilyam41;port=3306;User Id=root;password=root";
    private MySqlConnection connection;
    public Edit()
    {
         InitializeComponent();
         FillSubjects();
         FillCabinets();
         FillClasses();
    }

    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        connection = new MySqlConnection(connectionString);
        connection.Open();
        var subject = CmbSubject.SelectedIndex;
        var cabinet = CmbCabinet.SelectedIndex;
        var className = CmbClass.SelectedIndex;
        if (subject == 0) subject = 1;
        if (cabinet == 0) cabinet = 1;
        if (className == 0) className = 1;
        string query = $"UPDATE Plan SET teacher_id = {Convert.ToInt32(teacher_id.Text)}, subject_id = {subject}, class_id = {className}, data = '{Data.Text}', cabinet_id = {cabinet} WHERE id = {Convert.ToInt32(ID.Text)}";
        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.ExecuteNonQuery();
        connection.Close();
    }

    public void FillSubjects()
    {
        List<Subjects> _subjectsList = new List<Subjects>();
        connection = new MySqlConnection(connectionString);
        connection.Open();
        MySqlCommand command = new MySqlCommand("SELECT Subjects.id, Subjects.subject FROM Subjects", connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentSubject = new Subjects()
            {
                subject = reader.GetString("Subject")
                
            };
            _subjectsList.Add(currentSubject);
        }
        connection.Close();
        var SubjectCmb = this.Find<ComboBox>(name:"CmbSubject");
        SubjectCmb.ItemsSource = _subjectsList;
    }
    public void FillCabinets()
    {
        List<Cabinets> _cabinetsList = new List<Cabinets>();
        connection = new MySqlConnection(connectionString);
        connection.Open();
        MySqlCommand command = new MySqlCommand("SELECT Cabinets.id, Cabinets.cabinet FROM Cabinets", connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentCabinet = new Cabinets()
            {
                cabinet = reader.GetString("cabinet")
            };
            _cabinetsList.Add(currentCabinet);
        }
        connection.Close();
        var CmbCabinet = this.Find<ComboBox>(name:"CmbCabinet");
        CmbCabinet.ItemsSource = _cabinetsList;
    }
    public void FillClasses()
    {
        List<Classes> _classesList = new List<Classes>();
        connection = new MySqlConnection(connectionString);
        connection.Open();
        MySqlCommand command = new MySqlCommand("SELECT Classes.id, Classes.class FROM Classes", connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentClass = new Classes()
            {
                className = reader.GetString("class")
            };
            _classesList.Add(currentClass);
        }
        connection.Close();
        var CmbClass = this.Find<ComboBox>(name:"CmbClass");
        CmbClass.ItemsSource = _classesList;
    }
    private void CloseAddEdit(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
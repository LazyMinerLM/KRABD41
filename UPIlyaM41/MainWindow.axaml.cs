using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;

namespace UPIlyaM41;

public partial class MainWindow : Window
{
     
    //лист 
    private List<Plan> _planList;
    //строка подключения к БД
    private string connectionString = "server=localhost;database=ilyam41;port=3306;User Id=root;password=root";
    //объявление подключения
    private MySqlConnection connection;
    //метод отображения таблицы
    public MainWindow()
    {
        InitializeComponent();
        //SQL запрос
        string FullTable = "SELECT plan.id, plan.teacher_id, teachers.surname, teachers.name, teachers.middleName, subjects.subject, classes.class, plan.data, cabinets.cabinet FROM plan INNER JOIN teachers on plan.teacher_id = teachers.id INNER JOIN subjects on plan.subject_id = subjects.id INNER JOIN classes on plan.class_id = classes.id INNER JOIN cabinets on plan.cabinet_id = cabinets.id;";
        //вызов метода для заполнения таблицы результатом вышеуказанного запроса
        ShowTable(FullTable);
        //вызов метода для заполнения выпадающего списка результатом вышеуказанного запроса
        FillCmb("SELECT Cabinets.id, Cabinets.cabinet FROM Cabinets");
    }
   public void ShowTable(string query)
    {
        //создание нового листа 
        _planList = new List<Plan>();
        //создание подключения, используя строку подключения
        connection = new MySqlConnection(connectionString); 
        //открытие подключения
        connection.Open();
        //создание команды на выполнение SQL запроса
        MySqlCommand cmd = new MySqlCommand(query, connection);
        //создание ридера, который читает команду
        MySqlDataReader reader = cmd.ExecuteReader();
        //цикл заполнения листа
        while (reader.Read() && reader.HasRows)
        {
            var currentPlan = new Plan()
            {
                id = reader.GetInt32("id"),
                surname = reader.GetString("surname"),
                name = reader.GetString("name"),
                middleName = reader.GetString("middleName"),
                data = reader.GetDateTime("data"),
                cabinet = reader.GetString("cabinet")
            };
            _planList.Add(currentPlan);
        }
        //закрытие подключения
        connection.Close();
        //выбор заполненного выше листа источником данных для таблицы
        PlanGrid.ItemsSource = _planList;
    }
    
    //метод для заполнения выпадающего списка данными
    public void FillCmb(string query)
    {
        //создание нового листа
        List <Cabinets> _cabinetList = new List<Cabinets>();
        //создание подключения
        connection = new MySqlConnection(connectionString);
        //открытие подключения
        connection.Open();
        //создание команды на выполнение SQL запроса
        MySqlCommand command = new MySqlCommand(query, connection);
        //создание ридера, который читает команду
        MySqlDataReader reader = command.ExecuteReader();
        //цикл заполнения листа
        while (reader.Read() && reader.HasRows)
        {
            var currentCabinet = new Cabinets()
            {
                id = reader.GetInt32("id"),
                cabinet = reader.GetString("cabinet")
            };
            _cabinetList.Add(currentCabinet);
        }
        //закрытие подключения
        connection.Close();
        //выбор выпадающего списка
        var typeCmb = this.Find<ComboBox>(name:"CmbCabinet");
        //выбор заполненного выше листа источником данных для выпадающего списка 
        typeCmb.ItemsSource = _cabinetList;
    }

    //обработчик события выбора из выпадающего списка
    private void CmbCabinet_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var CabinetCmb = (ComboBox)sender;
        var currentCabinet = CabinetCmb.SelectedItem as Cabinets;
        //LINQ запрос
        var filterPlan = _planList
        .Where(x => x.cabinet == currentCabinet.cabinet).ToList();
        //выбор результата выполнения LINQ запроса источником данных для таблицы
        PlanGrid.ItemsSource = filterPlan;
    } 

    //обработчик события нажатия на кнопку "Добавить"
    private void AddData(object? sender, RoutedEventArgs e)
    {
        //объявление формы "Добавление"
        AddForm addWindow = new AddForm();
        //отображение формы "Добавление
        addWindow.Show();
    }

    //обработчик события нажатия на кнопку "Изменить"
    private void EditData(object? sender, RoutedEventArgs e)
    { 
       //объявление формы "Редактирование" 
        Edit editWindow = new Edit();
        //отображение формы "Редактирование"
        editWindow.Show();
    }

    //обработчик события нажатия на кнопку "Удалить"
    private void DeleteData(object? sender, RoutedEventArgs e)
    {
        //запоминание выбранной строки в таблице
        Plan currentPlan = PlanGrid.SelectedItem as Plan;
        if (currentPlan == null)
        {
            return;
        }
        //создание подключения
        connection = new MySqlConnection(connectionString);
        //открытие подключения
        connection.Open();
        //SQL запрос
        string query = "DELETE FROM Plan WHERE ID = " + currentPlan.id;
        //объявление команды для выполнения sql запроса
        MySqlCommand cmd = new MySqlCommand(query, connection);
        //выполнение SQL запроса
        cmd.ExecuteNonQuery();
        //закрытие подключения
        connection.Close();
        //удаление выбранной строки из листа
        _planList.Remove(currentPlan);
        //вызов метода отображения данных результата выполнения SQL запроса
        ShowTable("SELECT plan.id, plan.teacher_id, teachers.surname, teachers.name, teachers.middleName, subjects.subject, classes.class, plan.data, cabinets.cabinet FROM plan INNER JOIN teachers on plan.teacher_id = teachers.id INNER JOIN subjects on plan.subject_id = subjects.id INNER JOIN classes on plan.class_id = classes.id INNER JOIN cabinets on plan.cabinet_id = cabinets.id;");
    }

    //метод поиска по фамилии
    private void Search_OnClick(object? sender, RoutedEventArgs e)
    {
        //SQL запрос
        string search = $"SELECT plan.id, plan.teacher_id, teachers.surname, teachers.name, teachers.middleName, subjects.subject, classes.class, plan.data, cabinets.cabinet FROM plan INNER JOIN teachers on plan.teacher_id = teachers.id INNER JOIN subjects on plan.subject_id = subjects.id INNER JOIN classes on plan.class_id = classes.id INNER JOIN cabinets on plan.cabinet_id = cabinets.id WHERE teachers.surname LIKE '%{SearchSurname.Text}%'";
        //вызов метода для заполнения таблицы
        ShowTable(search);
    }

    //обработчик события нажатия на кнопку "Сброс фильтра"
    private void Reset_OnClick(object? sender, RoutedEventArgs e)
    {
        //SQL запрос
        string reset = "SELECT plan.id, plan.teacher_id, teachers.surname, teachers.name, teachers.middleName, subjects.subject, classes.class, plan.data, cabinets.cabinet FROM plan INNER JOIN teachers on plan.teacher_id = teachers.id INNER JOIN subjects on plan.subject_id = subjects.id INNER JOIN classes on plan.class_id = classes.id INNER JOIN cabinets on plan.cabinet_id = cabinets.id;";
        //вызов метода для заполнения таблицы результатом вышеуказанного запроса
        ShowTable(reset);
        //очистка строки для поиска по фамилии
        SearchSurname.Text = string.Empty;
    }

    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        Main main = new Main();
        main.Show();
        this.Close();
    }
}

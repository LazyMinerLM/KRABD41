using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace UPIlyaM41;

public partial class Main : Window
{
    public Main()
    {
        InitializeComponent();
    }

    private void Cabinets_OnClick(object? sender, RoutedEventArgs e)
    {
        CabinetTable ct = new CabinetTable();
        ct.Show();
        this.Hide();
    }

    private void Plan_OnClick(object? sender, RoutedEventArgs e)
    {
        MainWindow plan = new MainWindow();
        plan.Show();
        this.Hide();
    }

    private void Classes_OnClick(object? sender, RoutedEventArgs e)
    {
        ClassesTable ct = new ClassesTable();
        ct.Show();
        this.Hide();
    }

    private void Scores_OnClick(object? sender, RoutedEventArgs e)
    {
        ScoresTable sct = new ScoresTable();
        sct.Show();
        this.Hide();
    }

    private void Students_OnClick(object? sender, RoutedEventArgs e)
    {
        StudentsTable st = new StudentsTable();
        st.Show();
        this.Hide();
    }

    private void Subjects_OnClick(object? sender, RoutedEventArgs e)
    {
        SubjectsTable st = new SubjectsTable();
        st.Show();
        this.Hide();
    }

    private void Teachers_OnClick(object? sender, RoutedEventArgs e)
    {
        TeachersTable tt = new TeachersTable();
        tt.Show();
        this.Hide();
    }

    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        Vhod v = new Vhod();
        v.Show();
        this.Close();
    }
}
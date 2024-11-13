using Microsoft.EntityFrameworkCore;
using StudentManagementApp.Context;
using StudentManagementApp.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace StudentManagementApp;

public partial class MainWindow : Window
{
    AppDbContext db = new AppDbContext();
    public MainWindow()
    {
        InitializeComponent();

        Loaded += MainWindow_Loaded;
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        await db.Database.EnsureCreatedAsync();
        var students = await db.Students.ToListAsync();
        DataContext = new ObservableCollection<Student>(students);
    }

    private void Add_Click(object sender, RoutedEventArgs e)
    {
        UserWindow UserWindow = new UserWindow(new Student());
        if (UserWindow.ShowDialog() == true)
        {
            Student Student = UserWindow.Student;
            db.Students.Add(Student);
            db.SaveChanges();
            UpdateStudentList();
        }
    }

    private void Edit_Click(object sender, RoutedEventArgs e)
    {
        Student? student = studentsList.SelectedItem as Student;

        if (student is null) return;

        UserWindow UserWindow = new UserWindow(new Student
        {
            Id = student.Id,
            Age = student.Age,
            Name = student.Name,
            Group = student.Group
        });

        if (UserWindow.ShowDialog() == true)
        {

            student = db.Students.Find(UserWindow.Student.Id);
            if (student != null)
            {
                student.Age = UserWindow.Student.Age;
                student.Name = UserWindow.Student.Name;
                db.SaveChanges();

                studentsList.Items.Refresh();
            }
            UpdateStudentList();
        }
    }

    private void Delete_Click(object sender, RoutedEventArgs e)
    {

        Student? student = studentsList.SelectedItem as Student;

        if (student is null) return;
        db.Students.Remove(student);
        db.SaveChanges();

        UpdateStudentList();
    }

    private void UpdateStudentList()
    {
        var students = db.Students.ToList();
        DataContext = new ObservableCollection<Student>(students);
    }

}
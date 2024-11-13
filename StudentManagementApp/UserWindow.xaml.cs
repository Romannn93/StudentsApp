using StudentManagementApp.Models;
using System.Windows;

namespace StudentManagementApp;

public partial class UserWindow : Window
{
    public Student Student { get; private set; }
    public UserWindow(Student student)
    {
        InitializeComponent();
        Student = student;
        DataContext = Student;
    }

    void Accept_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
    }
}

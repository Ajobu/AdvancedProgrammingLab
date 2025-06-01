using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Student> Students { get; } = new ObservableCollection<Student>();
        public ObservableCollection<Teacher> Teachers { get; } = new ObservableCollection<Teacher>();
        public ObservableCollection<Course> Courses { get; } = new ObservableCollection<Course>();

        public MainWindow()
        {
            InitializeComponent();

            StudentsList.ItemsSource = Students;
            TeachersList.ItemsSource = Teachers;
            CoursesList.ItemsSource = Courses;

            StudentsList.Focusable = true;
            Loaded += (s, e) => StudentsList.Focus();
        }

        private void AddStudent_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(FirstNameInput.Text) && !string.IsNullOrWhiteSpace(LastNameInput.Text))
            {
                Students.Add(new Student
                {
                    FirstName = FirstNameInput.Text,
                    LastName = LastNameInput.Text
                });
                FirstNameInput.Clear();
                LastNameInput.Clear();
            }
        }

        private void RemoveStudent_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).DataContext is Student student)
            {
                Students.Remove(student);
            }
        }

        private void AddTeacher_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TeacherFirstNameInput.Text) &&
                !string.IsNullOrWhiteSpace(TeacherLastNameInput.Text))
            {
                Teachers.Add(new Teacher
                {
                    FirstName = TeacherFirstNameInput.Text,
                    LastName = TeacherLastNameInput.Text
                });
                TeacherFirstNameInput.Clear();
                TeacherLastNameInput.Clear();
            }
        }

        private void RemoveTeacher_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).DataContext is Teacher teacher)
            {
                Teachers.Remove(teacher);
            }
        }

        private void AddCourse_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddEditCourseWindow(Students.ToList(), Teachers.ToList());
            if (dialog.ShowDialog() == true)
            {
                Courses.Add(dialog.Course);
            }
        }

        private void RemoveCourse_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).DataContext is Course course)
            {
                Courses.Remove(course);
            }
        }

        private void CoursesList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (CoursesList.SelectedItem is Course selectedCourse)
            {
                var dialog = new AddEditCourseWindow(Students.ToList(), Teachers.ToList(), selectedCourse);
                if (dialog.ShowDialog() == true)
                {
                }
            }
        }

        private void CopyStudentCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = StudentsList.SelectedItem != null;
        }

       private void CopyStudentCommand_Executed(object sender, ExecutedRoutedEventArgs e)
{
    try
    {
        if (StudentsList.SelectedItem is Student student)
        {
            Clipboard.Clear(); 
            Clipboard.SetData("Student", student);
            MessageBox.Show($"Student {student} copied to clipboard", "Information", 
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Failed to copy student: {ex.Message}", "Error", 
            MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
    }
}

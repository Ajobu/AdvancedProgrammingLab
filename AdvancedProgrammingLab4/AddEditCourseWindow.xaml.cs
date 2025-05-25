using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1
{
    public partial class AddEditCourseWindow : Window
    {
        public Course Course { get; private set; }
        public List<Student> AvailableStudents { get; private set; }
        public string ButtonText { get; private set; } = "Add";

        public AddEditCourseWindow(List<Student> allStudents, List<Teacher> teachers, Course existingCourse = null)
        {
            InitializeComponent();
            DataContext = this;

            Course = existingCourse ?? new Course();
            AvailableStudents = new List<Student>(allStudents);

            TeacherComboBox.ItemsSource = teachers;

            if (existingCourse != null)
            {
                ButtonText = "Save";
                Title = "Edit Course";

                foreach (var student in existingCourse.Students)
                {
                    AvailableStudents.RemoveAll(s => s.FirstName == student.FirstName && s.LastName == student.LastName);
                }
            }
        }

        private void AddStudent_Click(object sender, RoutedEventArgs e)
        {
            if (AvailableStudentsList.SelectedItem is Student selectedStudent)
            {
                Course.Students.Add(selectedStudent);
                AvailableStudents.Remove(selectedStudent);
            }
        }

        private void RemoveStudent_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedStudentsList.SelectedItem is Student selectedStudent)
            {
                Course.Students.Remove(selectedStudent);
                AvailableStudents.Add(selectedStudent);
            }
        }

        private void AvailableStudentsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AddStudent_Click(sender, e);
        }

        private void SelectedStudentsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            RemoveStudent_Click(sender, e);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Course.Name))
            {
                MessageBox.Show("Please enter a course name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Course.Teacher == null)
            {
                MessageBox.Show("Please select a teacher.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DialogResult = true;
            Close();
        }
    }
}
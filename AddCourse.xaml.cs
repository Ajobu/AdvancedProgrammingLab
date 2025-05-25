using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace WpfApp1
{
    public partial class AddCourse : Window
    {
        public Course Course { get; private set; } = new Course();
        private List<Student> availableStudents;
        private List<Teacher> teachers;

        public AddCourse(List<Student> students, List<Teacher> teachers)
        {
            InitializeComponent();
            this.availableStudents = new List<Student>(students);
            this.teachers = teachers;

            TeacherComboBox.ItemsSource = teachers;
            AvailableStudentsList.ItemsSource = availableStudents;
            SelectedStudentsList.ItemsSource = Course.Students;
        }

        private void AddStudent_Click(object sender, RoutedEventArgs e)
        {
            if (AvailableStudentsList.SelectedItem is Student selectedStudent)
            {
                Course.Students.Add(selectedStudent);
                availableStudents.Remove(selectedStudent);
                RefreshStudentLists();
            }
        }

        private void RemoveStudent_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedStudentsList.SelectedItem is Student selectedStudent)
            {
                availableStudents.Add(selectedStudent);
                Course.Students.Remove(selectedStudent);
                RefreshStudentLists();
            }
        }

        private void RefreshStudentLists()
        {
            AvailableStudentsList.Items.Refresh();
            SelectedStudentsList.Items.Refresh();
        }

        private void OnResult_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextInput.Text))
            {
                MessageBox.Show("Please enter a course name.");
                return;
            }

            if (TeacherComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a teacher.");
                return;
            }

            Course.Name = TextInput.Text;
            Course.Teacher = (Teacher)TeacherComboBox.SelectedItem;

            DialogResult = true;
            Close();
        }
    }
}
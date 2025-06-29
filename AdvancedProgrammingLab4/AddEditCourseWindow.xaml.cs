using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace WpfApp1
{
    public partial class AddEditCourseWindow : Window
    {
        public Course Course { get; private set; }
        public ObservableCollection<Student> AvailableStudents { get; private set; }
        public string ButtonText { get; private set; } = "Add";

        public AddEditCourseWindow(List<Student> allStudents, List<Teacher> teachers, Course existingCourse = null)
        {
            InitializeComponent();
            DataContext = this;

            Course = existingCourse ?? new Course();
            AvailableStudents = new ObservableCollection<Student>();
            TeacherComboBox.ItemsSource = teachers;

            var availableStudents = allStudents.ToList();
            if (existingCourse != null)
            {
                ButtonText = "Save";
                Title = "Edit Course";

                foreach (var student in existingCourse.Students)
                {
                    availableStudents.RemoveAll(s =>
                        s.FirstName == student.FirstName &&
                        s.LastName == student.LastName);
                }
            }

            foreach (var student in availableStudents)
            {
                AvailableStudents.Add(student);
            }
        }

        private void AddStudent_Click(object sender, RoutedEventArgs e)
        {
            if (AvailableStudentsList.SelectedItem is Student selectedStudent)
            {
                if (!Course.Students.Any(s => s.FirstName == selectedStudent.FirstName &&
                                             s.LastName == selectedStudent.LastName))
                {
                    Course.Students.Add(selectedStudent);
                    AvailableStudents.Remove(selectedStudent);
                }
            }
        }

        private void RemoveStudent_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedStudentsList.SelectedItem is Student selectedStudent)
            {
                Course.Students.Remove(selectedStudent);
                if (!AvailableStudents.Any(s => s.FirstName == selectedStudent.FirstName &&
                                              s.LastName == selectedStudent.LastName))
                {
                    AvailableStudents.Add(selectedStudent);
                }
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

        private void PasteStudentCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Clipboard.ContainsData("Student");
        }

        private void PasteStudentCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (Clipboard.ContainsData("Student") &&
                Clipboard.GetData("Student") is Student student)
            {
                if (!Course.Students.Any(s => s.FirstName == student.FirstName &&
                                            s.LastName == student.LastName))
                {
                    Course.Students.Add(student);
                    var studentToRemove = AvailableStudents.FirstOrDefault(s =>
                        s.FirstName == student.FirstName &&
                        s.LastName == student.LastName);
                    if (studentToRemove != null)
                    {
                        AvailableStudents.Remove(studentToRemove);
                    }
                }
            }
        }
    }
}

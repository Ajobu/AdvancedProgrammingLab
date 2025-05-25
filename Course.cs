using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Course
    {
        public string Name { get; set; }
        public Teacher Teacher { get; set; }
        public ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student>();

        public override string ToString() => $"{Name} (Teacher: {Teacher}, Students: {Students.Count})";
    }
}

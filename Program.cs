// See https://aka.ms/new-console-template for more information
using static System.Formats.Asn1.AsnWriter;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using System;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    public static MyDatabaseContext db = new MyDatabaseContext();

    private static void Main(string[] args)
    {
        // Cleanup: Clear existing data
        db.Students.RemoveRange(db.Students);
        db.Teachers.RemoveRange(db.Teachers);
        db.Classes.RemoveRange(db.Classes);
        db.SaveChanges();

        // Add students
        var student1 = new Student { FirstName = "Alice", LastName = "Smith" };
        var student2 = new Student { FirstName = "Bob", LastName = "Jones" };
        db.Students.AddRange(student1, student2);
        db.SaveChanges();

        // Add class
        var class1 = new Class { Name = "Math 101" };
        db.Classes.Add(class1);
        db.SaveChanges();

        // Add a teacher and assign to class
        var teacher1 = new Teacher { Name = "Dr. Brown" };
        db.Teachers.Add(teacher1);
        db.SaveChanges();

        class1 = db.Classes.First(c => c.Name == "Math 101");
        class1.TeacherId = teacher1.Id;
        db.SaveChanges();

        // Verify assignment
        class1 = db.Classes.Include(c => c.Teacher).First(c => c.Name == "Math 101");
        Console.WriteLine(class1);

        // Add another teacher and class
        var teacher2 = new Teacher { Name = "Prof. Green" };
        var class2 = new Class { Name = "Physics 202", Teacher = teacher2 };
        db.Teachers.Add(teacher2);
        db.Classes.Add(class2);
        db.SaveChanges();

        // Delete a teacher
        db.Teachers.Remove(teacher2);
        db.SaveChanges();

        // Print all classes
        var allClasses = db.Classes.Include(c => c.Teacher).ToList();
        Console.WriteLine("All Classes:");
        foreach (var c in allClasses)
            Console.WriteLine(c);

        // Delete remaining class
        db.Classes.Remove(class1);
        db.SaveChanges();

        // Print all teachers
        var allTeachers = db.Teachers.ToList();
        Console.WriteLine("Remaining Teachers:");
        foreach (var t in allTeachers)
            Console.WriteLine(t);

        // Add many-to-many relationship
        class2 = new Class { Name = "Chemistry 303", Students = new[] { student1, student2 } };
        db.Classes.Add(class2);
        db.SaveChanges();

        // Print all students and their classes
        var studentsWithClasses = db.Students.Include(s => s.Classes).ToList();
        foreach (var student in studentsWithClasses)
        {
            Console.WriteLine(student);
            foreach (var c in student.Classes)
                Console.WriteLine($"\tClass: {c.Name}");
        }

        // Print teacher and their classes
        var teacherWithClasses = db.Teachers.Include(t => t.Classes).FirstOrDefault();
        if (teacherWithClasses != null)
        {
            Console.WriteLine(teacherWithClasses);
            foreach (var c in teacherWithClasses.Classes)
                Console.WriteLine($"\tClass: {c.Name}");
        }
    }
}
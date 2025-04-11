using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


public class MyDatabaseContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Teacher> Teachers { get; set; }

    public string DbPath { get; }

    public MyDatabaseContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "blogging.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public ICollection<Class> Classes { get; set; }

    public override string ToString() =>
        $"Student: {Id} - {FirstName} {LastName}";
}

public class Class
{
    public int Id { get; set; }
    public string Name { get; set; }

    public int? TeacherId { get; set; }
    public Teacher Teacher { get; set; }

    public ICollection<Student> Students { get; set; }

    public override string ToString() =>
        $"Class: {Id} - {Name} | Teacher: {Teacher?.Name ?? "None"}";
}

public class Teacher
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Class> Classes { get; set; }

    public override string ToString() =>
        $"Teacher: {Id} - {Name}";
}

using StudentManagementSystem.Models;
using StudentManagementSystem.Services;
using StudentManagementSystem.Utilities;

namespace StudentManagementSystem;

public class Program
{
    private static readonly StudentService studentService = new();

    public static void Main()
    {
        while (true)
        {
            Console.Clear();
            ConsoleHelper.Header("STUDENT MANAGEMENT SYSTEM");

            Console.WriteLine("1. Dashboard");
            Console.WriteLine("2. Add Student");
            Console.WriteLine("3. View All Students");
            Console.WriteLine("4. Search Student");
            Console.WriteLine("5. Update Student");
            Console.WriteLine("6. Delete Student");
            Console.WriteLine("7. Statistics");
            Console.WriteLine("8. Exit");

            Console.Write("\nEnter your choice: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ShowDashboard();
                    break;

                case "2":
                    AddStudent();
                    break;

                case "3":
                    ViewStudents();
                    break;

                case "4":
                    SearchStudent();
                    break;

                case "5":
                    UpdateStudent();
                    break;

                case "6":
                    DeleteStudent();
                    break;

                case "7":
                    ShowStatistics();
                    break;

                case "8":
                    ConsoleHelper.Success("Thank you for using Student Management System!");
                    return;

                default:
                    ConsoleHelper.Error("Invalid choice. Please select 1-8.");
                    ConsoleHelper.Pause();
                    break;
            }
        }
    }

    private static void ShowDashboard()
    {
        Console.Clear();
        ConsoleHelper.Header("DASHBOARD");

        var students = studentService.GetAllStudents();

        Console.WriteLine($"Total Students : {students.Count}");

        if (students.Count > 0)
        {
            Console.WriteLine($"Average Marks  : {students.Average(s => s.Marks):F2}");
            Console.WriteLine($"Top Student    : {studentService.GetTopStudent()?.Name ?? "N/A"}");
        }
        else
        {
            Console.WriteLine("Average Marks  : N/A");
            Console.WriteLine("Top Student    : N/A");
        }

        ConsoleHelper.Pause();
    }

    private static void AddStudent()
    {
        Console.Clear();
        ConsoleHelper.Header("ADD NEW STUDENT");

        Console.Write("Enter Student ID: ");
        int id = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Enter Name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Enter Age: ");
        int age = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Enter Course: ");
        string course = Console.ReadLine() ?? "";

        Console.Write("Enter Marks: ");
        double marks = double.Parse(Console.ReadLine() ?? "0");

        var student = new Student
        {
            Id = id,
            Name = name,
            Age = age,
            Course = course,
            Marks = marks
        };

        studentService.AddStudent(student);

        ConsoleHelper.Success("Student added successfully!");
        ConsoleHelper.Pause();
    }

    private static void ViewStudents()
    {
        Console.Clear();
        ConsoleHelper.Header("ALL STUDENTS");

        var students = studentService.GetAllStudents();

        if (students.Count == 0)
        {
            ConsoleHelper.Error("No students found.");
            ConsoleHelper.Pause();
            return;
        }

        foreach (var student in students)
        {
            Console.WriteLine(
                $"ID: {student.Id} | " +
                $"Name: {student.Name} | " +
                $"Age: {student.Age} | " +
                $"Course: {student.Course} | " +
                $"Marks: {student.Marks:F2} | " +
                $"Grade: {student.Grade}");
        }

        ConsoleHelper.Pause();
    }

    private static void SearchStudent()
    {
        Console.Clear();
        ConsoleHelper.Header("SEARCH STUDENT");

        Console.Write("Enter Student ID: ");
        int id = int.Parse(Console.ReadLine() ?? "0");

        var student = studentService.GetStudentById(id);

        if (student == null)
        {
            ConsoleHelper.Error("Student not found.");
        }
        else
        {
            Console.WriteLine($"\nID      : {student.Id}");
            Console.WriteLine($"Name    : {student.Name}");
            Console.WriteLine($"Age     : {student.Age}");
            Console.WriteLine($"Course  : {student.Course}");
            Console.WriteLine($"Marks   : {student.Marks:F2}");
            Console.WriteLine($"Grade   : {student.Grade}");
        }

        ConsoleHelper.Pause();
    }

    private static void UpdateStudent()
    {
        Console.Clear();
        ConsoleHelper.Header("UPDATE STUDENT");

        Console.Write("Enter Student ID: ");
        int id = int.Parse(Console.ReadLine() ?? "0");

        var student = studentService.GetStudentById(id);

        if (student == null)
        {
            ConsoleHelper.Error("Student not found.");
            ConsoleHelper.Pause();
            return;
        }

        Console.Write($"Enter new name ({student.Name}): ");
        string name = Console.ReadLine() ?? student.Name;

        Console.Write($"Enter new age ({student.Age}): ");
        int age = int.Parse(Console.ReadLine() ?? student.Age.ToString());

        Console.Write($"Enter new course ({student.Course}): ");
        string course = Console.ReadLine() ?? student.Course;

        Console.Write($"Enter new marks ({student.Marks}): ");
        double marks = double.Parse(Console.ReadLine() ?? student.Marks.ToString());

        student.Name = name;
        student.Age = age;
        student.Course = course;
        student.Marks = marks;

        studentService.UpdateStudent(student);

        ConsoleHelper.Success("Student updated successfully!");
        ConsoleHelper.Pause();
    }

    private static void DeleteStudent()
    {
        Console.Clear();
        ConsoleHelper.Header("DELETE STUDENT");

        Console.Write("Enter Student ID: ");
        int id = int.Parse(Console.ReadLine() ?? "0");

        bool deleted = studentService.DeleteStudent(id);

        if (deleted)
            ConsoleHelper.Success("Student deleted successfully!");
        else
            ConsoleHelper.Error("Student not found.");

        ConsoleHelper.Pause();
    }

    private static void ShowStatistics()
    {
        Console.Clear();
        ConsoleHelper.Header("STUDENT STATISTICS");

        var students = studentService.GetAllStudents();

        if (students.Count == 0)
        {
            ConsoleHelper.Error("No student data available.");
            ConsoleHelper.Pause();
            return;
        }

        Console.WriteLine($"Total Students : {students.Count}");
        Console.WriteLine($"Average Marks  : {students.Average(s => s.Marks):F2}");
        Console.WriteLine($"Highest Marks  : {students.Max(s => s.Marks):F2}");
        Console.WriteLine($"Lowest Marks   : {students.Min(s => s.Marks):F2}");

        ConsoleHelper.Pause();
    }
}
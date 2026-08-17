using StudentManagementSystem.Models;
using StudentManagementSystem.Services;
using StudentManagementSystem.Utilities;

class Program
{
    private static readonly StudentService studentService = new();

    static void Main()
    {
        while (true)
        {
            ShowDashboard();

            string choice = Console.ReadLine()?.Trim() ?? "";

            switch (choice)
            {
                case "1":
                    AddStudent();
                    break;

                case "2":
                    ViewStudents();
                    break;

                case "3":
                    SearchStudent();
                    break;

                case "4":
                    UpdateStudent();
                    break;

                case "5":
                    DeleteStudent();
                    break;

                case "6":
                    ShowStatistics();
                    break;

                case "7":
                    ConsoleHelper.Header("EXIT");
                    Console.WriteLine("Thank you for using Student Management System.");
                    Console.WriteLine("Goodbye!");
                    return;

                default:
                    ConsoleHelper.Error("Invalid option. Please select 1-7.");
                    ConsoleHelper.Pause();
                    break;
            }
        }
    }

    static void ShowDashboard()
    {
        Console.Clear();

        List<Student> students = studentService.GetAllStudents();

        Console.WriteLine();
        Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
        Console.WriteLine("║              STUDENT MANAGEMENT SYSTEM                  ║");
        Console.WriteLine("║                  PHASE 1 • CONSOLE APP                  ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════╝");

        Console.WriteLine();
        Console.WriteLine($"  Total Students : {students.Count}");
        Console.WriteLine($"  Average Marks  : {studentService.GetAverageMarks():F2}");

        Student? topStudent = studentService.GetTopStudent();

        Console.WriteLine(
            $"  Top Performer  : {(topStudent == null ? "N/A" : topStudent.Name)}");

        Console.WriteLine();
        Console.WriteLine("────────────────────── MAIN MENU ─────────────────────────");
        Console.WriteLine();
        Console.WriteLine("  [1]  Add Student");
        Console.WriteLine("  [2]  View All Students");
        Console.WriteLine("  [3]  Search Student");
        Console.WriteLine("  [4]  Update Student");
        Console.WriteLine("  [5]  Delete Student");
        Console.WriteLine("  [6]  Statistics & Reports");
        Console.WriteLine("  [7]  Exit");
        Console.WriteLine();
        Console.Write("  Select an option: ");
    }

    static void AddStudent()
    {
        ConsoleHelper.Header("ADD NEW STUDENT");

        int id = ConsoleHelper.ReadInt("Student ID: ");

        if (studentService.GetStudentById(id) != null)
        {
            ConsoleHelper.Error("A student with this ID already exists.");
            ConsoleHelper.Pause();
            return;
        }

        string name = ConsoleHelper.ReadRequired("Student Name: ");

        int age;

        do
        {
            age = ConsoleHelper.ReadInt("Age: ");

            if (age < 15 || age > 100)
                ConsoleHelper.Error("Age must be between 15 and 100.");

        } while (age < 15 || age > 100);

        string course = ConsoleHelper.ReadRequired("Course: ");

        double marks;

        do
        {
            marks = ConsoleHelper.ReadDouble("Marks (0-100): ");

            if (marks < 0 || marks > 100)
                ConsoleHelper.Error("Marks must be between 0 and 100.");

        } while (marks < 0 || marks > 100);

        Student student = new Student
        {
            Id = id,
            Name = name,
            Age = age,
            Course = course,
            Marks = marks
        };

        studentService.AddStudent(student);

        ConsoleHelper.Success(
            $"Student added successfully! Grade: {student.Grade}");

        ConsoleHelper.Pause();
    }

    static void ViewStudents()
    {
        ConsoleHelper.Header("ALL STUDENTS");

        List<Student> students = studentService.GetAllStudents();

        if (students.Count == 0)
        {
            ConsoleHelper.Error("No students found.");
            ConsoleHelper.Pause();
            return;
        }

        Console.WriteLine(
            $"{"ID",-6}{"Name",-22}{"Age",-6}{"Course",-18}{"Marks",-10}{"Grade",-8}");

        Console.WriteLine(new string('─', 70));

        foreach (Student student in students)
        {
            Console.WriteLine(
                $"{student.Id,-6}" +
                $"{student.Name,-22}" +
                $"{student.Age,-6}" +
                $"{student.Course,-18}" +
                $"{student.Marks,-10:F2}" +
                $"{student.Grade,-8}");
        }

        Console.WriteLine();
        Console.WriteLine($"Total Students: {students.Count}");

        ConsoleHelper.Pause();
    }

    static void SearchStudent()
    {
        ConsoleHelper.Header("SEARCH STUDENT");

        string keyword =
            ConsoleHelper.ReadRequired(
                "Enter ID, name or course: ");

        List<Student> results =
            studentService.SearchStudents(keyword);

        if (results.Count == 0)
        {
            ConsoleHelper.Error("No matching students found.");
            ConsoleHelper.Pause();
            return;
        }

        Console.WriteLine();
        Console.WriteLine($"Found {results.Count} student(s):");
        Console.WriteLine();

        foreach (Student student in results)
        {
            DisplayStudent(student);
        }

        ConsoleHelper.Pause();
    }

    static void UpdateStudent()
    {
        ConsoleHelper.Header("UPDATE STUDENT");

        int id = ConsoleHelper.ReadInt("Enter Student ID: ");

        Student? existing =
            studentService.GetStudentById(id);

        if (existing == null)
        {
            ConsoleHelper.Error("Student not found.");
            ConsoleHelper.Pause();
            return;
        }

        Console.WriteLine("\nCurrent Details:");
        DisplayStudent(existing);

        Console.WriteLine("\nEnter new details:");

        string name =
            ConsoleHelper.ReadRequired("New Name: ");

        int age;

        do
        {
            age = ConsoleHelper.ReadInt("New Age: ");

            if (age < 15 || age > 100)
                ConsoleHelper.Error("Age must be between 15 and 100.");

        } while (age < 15 || age > 100);

        string course =
            ConsoleHelper.ReadRequired("New Course: ");

        double marks;

        do
        {
            marks =
                ConsoleHelper.ReadDouble("New Marks (0-100): ");

            if (marks < 0 || marks > 100)
                ConsoleHelper.Error("Marks must be between 0 and 100.");

        } while (marks < 0 || marks > 100);

        Student updatedStudent = new Student
        {
            Id = id,
            Name = name,
            Age = age,
            Course = course,
            Marks = marks
        };

        studentService.UpdateStudent(updatedStudent);

        ConsoleHelper.Success("Student updated successfully.");
        ConsoleHelper.Pause();
    }

    static void DeleteStudent()
    {
        ConsoleHelper.Header("DELETE STUDENT");

        int id = ConsoleHelper.ReadInt("Enter Student ID: ");

        Student? student =
            studentService.GetStudentById(id);

        if (student == null)
        {
            ConsoleHelper.Error("Student not found.");
            ConsoleHelper.Pause();
            return;
        }

        Console.WriteLine("\nStudent to be deleted:");
        DisplayStudent(student);

        Console.Write("\nAre you sure? (Y/N): ");

        string confirmation =
            Console.ReadLine()?.Trim().ToUpper() ?? "";

        if (confirmation == "Y")
        {
            studentService.DeleteStudent(id);
            ConsoleHelper.Success("Student deleted successfully.");
        }
        else
        {
            Console.WriteLine("\nDelete operation cancelled.");
        }

        ConsoleHelper.Pause();
    }

    static void ShowStatistics()
    {
        ConsoleHelper.Header("STATISTICS & REPORTS");

        List<Student> students =
            studentService.GetAllStudents();

        if (students.Count == 0)
        {
            ConsoleHelper.Error("No student data available.");
            ConsoleHelper.Pause();
            return;
        }

        Student? topStudent =
            studentService.GetTopStudent();

        Console.WriteLine($"Total Students     : {students.Count}");
        Console.WriteLine(
            $"Average Marks      : {studentService.GetAverageMarks():F2}");

        Console.WriteLine(
            $"Highest Marks      : {students.Max(s => s.Marks):F2}");

        Console.WriteLine(
            $"Lowest Marks       : {students.Min(s => s.Marks):F2}");

        Console.WriteLine(
            $"Passing Students   : {students.Count(s => s.Marks >= 50)}");

        Console.WriteLine(
            $"Failing Students   : {students.Count(s => s.Marks < 50)}");

        if (topStudent != null)
        {
            Console.WriteLine();
            Console.WriteLine("🏆 TOP PERFORMER");
            DisplayStudent(topStudent);
        }

        Console.WriteLine();
        Console.WriteLine("COURSE-WISE STUDENT COUNT");
        Console.WriteLine("--------------------------------");

        Dictionary<string, int> statistics =
            studentService.GetCourseStatistics();

        foreach (var course in statistics)
        {
            Console.WriteLine($"{course.Key,-20} : {course.Value}");
        }

        ConsoleHelper.Pause();
    }

    static void DisplayStudent(Student student)
    {
        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"ID      : {student.Id}");
        Console.WriteLine($"Name    : {student.Name}");
        Console.WriteLine($"Age     : {student.Age}");
        Console.WriteLine($"Course  : {student.Course}");
        Console.WriteLine($"Marks   : {student.Marks:F2}");
        Console.WriteLine($"Grade   : {student.Grade}");
        Console.WriteLine("----------------------------------------");
    }
}
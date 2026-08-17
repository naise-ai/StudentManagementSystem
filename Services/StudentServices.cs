using System.Text.Json;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Services;

public class StudentService
{
    private readonly List<Student> students = new();

    private readonly string dataFolder =
        Path.Combine(AppContext.BaseDirectory, "Data");

    private readonly string dataFile;

    public StudentService()
    {
        dataFile = Path.Combine(dataFolder, "students.json");
        LoadStudents();
    }

    public List<Student> GetAllStudents()
    {
        return students;
    }

    public Student? GetStudentById(int id)
    {
        return students.FirstOrDefault(s => s.Id == id);
    }

    public List<Student> SearchStudents(string keyword)
    {
        return students
            .Where(s =>
                s.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                s.Course.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                s.Id.ToString() == keyword)
            .ToList();
    }

    public bool AddStudent(Student student)
    {
        if (students.Any(s => s.Id == student.Id))
            return false;

        students.Add(student);
        SaveStudents();
        return true;
    }

    public bool UpdateStudent(Student updatedStudent)
    {
        Student? existing = GetStudentById(updatedStudent.Id);

        if (existing == null)
            return false;

        existing.Name = updatedStudent.Name;
        existing.Age = updatedStudent.Age;
        existing.Course = updatedStudent.Course;
        existing.Marks = updatedStudent.Marks;

        SaveStudents();
        return true;
    }

    public bool DeleteStudent(int id)
    {
        Student? student = GetStudentById(id);

        if (student == null)
            return false;

        students.Remove(student);
        SaveStudents();
        return true;
    }

    public double GetAverageMarks()
    {
        return students.Count == 0 ? 0 : students.Average(s => s.Marks);
    }

    public Student? GetTopStudent()
    {
        return students.Count == 0
            ? null
            : students.OrderByDescending(s => s.Marks).First();
    }

    public Dictionary<string, int> GetCourseStatistics()
    {
        return students
            .GroupBy(s => s.Course)
            .ToDictionary(g => g.Key, g => g.Count());
    }

    private void SaveStudents()
    {
        Directory.CreateDirectory(dataFolder);

        string json = JsonSerializer.Serialize(
            students,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        File.WriteAllText(dataFile, json);
    }

    private void LoadStudents()
    {
        try
        {
            if (!File.Exists(dataFile))
                return;

            string json = File.ReadAllText(dataFile);

            List<Student>? loadedStudents =
                JsonSerializer.Deserialize<List<Student>>(json);

            if (loadedStudents != null)
                students.AddRange(loadedStudents);
        }
        catch
        {
            // If the file is corrupted, start with an empty list.
            students.Clear();
        }
    }
}
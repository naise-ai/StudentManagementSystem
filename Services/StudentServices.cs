using StudentManagementSystem.Models;

namespace StudentManagementSystem.Services;

public class StudentService
{
    private readonly List<Student> students = new();

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
                s.Course.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public void AddStudent(Student student)
    {
        students.Add(student);
    }

    public bool UpdateStudent(Student updatedStudent)
    {
        var student = GetStudentById(updatedStudent.Id);

        if (student == null)
            return false;

        student.Name = updatedStudent.Name;
        student.Age = updatedStudent.Age;
        student.Course = updatedStudent.Course;
        student.Marks = updatedStudent.Marks;

        return true;
    }

    public bool DeleteStudent(int id)
    {
        var student = GetStudentById(id);

        if (student == null)
            return false;

        students.Remove(student);
        return true;
    }

    public Student? GetTopStudent()
    {
        return students
            .OrderByDescending(s => s.Marks)
            .FirstOrDefault();
    }

    public double GetAverageMarks()
    {
        return students.Count == 0
            ? 0
            : students.Average(s => s.Marks);
    }
}
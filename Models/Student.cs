namespace StudentManagementSystem.Models;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Age { get; set; }
    public string Course { get; set; } = "";
    public double Marks { get; set; }

    public string Grade
    {
        get
        {
            if (Marks >= 90) return "A+";
            if (Marks >= 80) return "A";
            if (Marks >= 70) return "B";
            if (Marks >= 60) return "C";
            if (Marks >= 50) return "D";
            return "F";
        }
    }
}
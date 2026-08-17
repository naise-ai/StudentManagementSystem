namespace StudentManagementSystem.Utilities;

public static class ConsoleHelper
{
    public static void Header(string title)
    {
        Console.Clear();

        Console.WriteLine();
        Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
        Console.WriteLine($"║ {title,-56}║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
        Console.WriteLine();
    }

    public static void Pause()
    {
        Console.WriteLine();
        Console.Write("Press any key to continue...");
        Console.ReadKey();
    }

    public static void Success(string message)
    {
        Console.WriteLine($"\n✓ {message}");
    }

    public static void Error(string message)
    {
        Console.WriteLine($"\n✗ {message}");
    }

    public static int ReadInt(string message)
    {
        while (true)
        {
            Console.Write(message);

            if (int.TryParse(Console.ReadLine(), out int value))
                return value;

            Error("Please enter a valid number.");
        }
    }

    public static double ReadDouble(string message)
    {
        while (true)
        {
            Console.Write(message);

            if (double.TryParse(Console.ReadLine(), out double value))
                return value;

            Error("Please enter a valid number.");
        }
    }

    public static string ReadRequired(string message)
    {
        while (true)
        {
            Console.Write(message);
            string value = Console.ReadLine()?.Trim() ?? "";

            if (!string.IsNullOrWhiteSpace(value))
                return value;

            Error("This field cannot be empty.");
        }
    }
}
namespace StudentManagementSystem.Utilities;

public static class ConsoleHelper
{
    public static void Header(string title)
    {
        Console.Clear();

        Console.WriteLine("╔══════════════════════════════════════════════╗");
        Console.WriteLine($"║ {title.PadRight(44)}║");
        Console.WriteLine("╚══════════════════════════════════════════════╝");
        Console.WriteLine();
    }

    public static void Success(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n✓ {message}");
        Console.ResetColor();
    }

    public static void Error(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n✗ {message}");
        Console.ResetColor();
    }

    public static void Pause()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}
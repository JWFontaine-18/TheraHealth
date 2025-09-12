using System;

namespace ChartingSystem;

public static class InputHelper
{
    public static DateOnly GetValidDate(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine() ?? string.Empty;
            
            if (DateOnly.TryParseExact(input, "MM/dd/yyyy", out DateOnly date))
            {
                return date;
            }
            
            Console.WriteLine("Invalid date format. Please use MM/DD/YYYY.");
        }
    }

    public static DateTime GetValidDateTime(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine() ?? string.Empty;
            
            if (DateTime.TryParseExact(input, "MM/dd/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime dateTime))
            {
                return dateTime;
            }
            
            Console.WriteLine("Invalid date/time format. Please use MM/DD/YYYY HH:MM (24-hour format).");
        }
    }

    public static int GetValidInteger(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine() ?? string.Empty;
            
            if (int.TryParse(input, out int result))
            {
                return result;
            }
            
            Console.WriteLine("Invalid number format. Please enter a valid integer.");
        }
    }

    public static T GetValidEnum<T>(string prompt) where T : struct, Enum
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine() ?? string.Empty;
            
            if (Enum.TryParse(input, true, out T result))
            {
                return result;
            }
            
            Console.WriteLine($"Invalid option. Please choose from: {GetEnumOptions<T>()}");
        }
    }

    public static string GetEnumOptions<T>() where T : struct, Enum
    {
        return string.Join(", ", Enum.GetNames<T>());
    }

    public static string GetValidString(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? string.Empty;
    }
}
using System;
using System.IO;

public static class Logger
{
    private static string logFilePath;

    public static void Initialize(string filePath)
    {
        logFilePath = filePath;
        File.WriteAllText(logFilePath, string.Empty); // Clear the log file
    }

    public static void LogInfo(string message)
    {
        Log("INFO", message);
    }

    public static void LogError(string message)
    {
        Log("ERROR", message);
    }

    private static void Log(string logLevel, string message)
    {
        string logMessage = $"{DateTime.Now} [{logLevel}]: {message}";
        File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
        Console.WriteLine(logMessage);
    }
}

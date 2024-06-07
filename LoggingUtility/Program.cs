using LoggingUtility;

class Program
{
    static async Task Main(string[] args)
    {
        var logger = new Logger("logs/application.log");

        // Log messages using convenience methods
        await logger.LogDebugAsync("This is a debug message.");
        await logger.LogInfoAsync("This is an info message.");
        await logger.LogWarningAsync("This is a warning message.");
        await logger.LogErrorAsync("This is an error message.");

        // Log messages manually specifying the log level and additional data
        await logger.LogAsync("This is a manually logged info message.", LogLevel.Information);
        await logger.LogAsync("This is a manually logged error message with additional data.", LogLevel.Error, new { ErrorCode = 123, Details = "Something went wrong" });

        // Log a critical message with additional data
        await logger.LogAsync("This is a critical error message with additional data.", LogLevel.Critical, new { ErrorCode = 500, Details = "Critical failure" });

        Console.WriteLine("Logging complete. Check the log file for entries.");
    }
}
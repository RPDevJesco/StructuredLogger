using System.Collections.Concurrent;
using System.Text.Json;

namespace LoggingUtility
{
    /// <summary>
    /// Structured Data Logger that writes messages to a log file.
    /// </summary>
    public class Logger
    {
        private readonly string _logFilePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class with the specified log file path.
        /// </summary>
        /// <param name="logFilePath">The path to the log file.</param>
        public Logger(string logFilePath)
        {
            _logFilePath = logFilePath;
            EnsureLogFilePathExists();
        }

        /// <summary>
        /// Logs a message asynchronously to the log file with a structured format.
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <param name="level">The log level. Default is <see cref="LogLevel.Information"/>.</param>
        /// <param name="additionalData">Additional data to be included in the log entry.</param>
        /// <returns>A task representing the asynchronous log operation.</returns>
        public async Task LogAsync(string message, LogLevel level = LogLevel.Information, object additionalData = null)
        {
            var logEntry = new LogEntry
            {
                Timestamp = DateTime.UtcNow,
                Level = level.ToString(),
                Message = message,
                AdditionalData = additionalData
            };

            var serializedLogEntry = JsonSerializer.Serialize(logEntry, new JsonSerializerOptions { WriteIndented = true });

            try
            {
                await File.AppendAllTextAsync(_logFilePath, serializedLogEntry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to log message: {ex.Message}");
            }
        }

        /// <summary>
        /// Ensures the log file path exists by creating directories and the file if necessary.
        /// </summary>
        private void EnsureLogFilePathExists()
        {
            var directory = Path.GetDirectoryName(_logFilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(_logFilePath))
            {
                File.Create(_logFilePath).Dispose();
            }
        }

        /// <summary>
        /// Logs a debug-level message asynchronously to the log file with a structured format.
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <param name="additionalData">Additional data to be included in the log entry.</param>
        /// <returns>A task representing the asynchronous log operation.</returns>
        public Task LogDebugAsync(string message, object additionalData = null) =>
            LogAsync(message, LogLevel.Debug, additionalData);

        /// <summary>
        /// Logs an informational message asynchronously to the log file with a structured format.
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <param name="additionalData">Additional data to be included in the log entry.</param>
        /// <returns>A task representing the asynchronous log operation.</returns>
        public Task LogInfoAsync(string message, object additionalData = null) =>
            LogAsync(message, LogLevel.Information, additionalData);

        /// <summary>
        /// Logs a warning message asynchronously to the log file with a structured format.
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <param name="additionalData">Additional data to be included in the log entry.</param>
        /// <returns>A task representing the asynchronous log operation.</returns>
        public Task LogWarningAsync(string message, object additionalData = null) =>
            LogAsync(message, LogLevel.Warning, additionalData);

        /// <summary>
        /// Logs an error message asynchronously to the log file with a structured format.
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <param name="additionalData">Additional data to be included in the log entry.</param>
        /// <returns>A task representing the asynchronous log operation.</returns>
        public Task LogErrorAsync(string message, object additionalData = null) =>
            LogAsync(message, LogLevel.Error, additionalData);
    }
}
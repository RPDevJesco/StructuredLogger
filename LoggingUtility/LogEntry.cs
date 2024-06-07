namespace LoggingUtility
{
    /// <summary>
    /// Represents a structured log entry.
    /// </summary>
    public class LogEntry
    {
        /// <summary>
        /// Timestamp for when the log has been created.
        /// </summary>
        public DateTime Timestamp { get; set; }
        
        /// <summary>
        /// Level of severity for the log.
        /// </summary>
        public string Level { get; set; }
        
        /// <summary>
        /// Message that should accompany the log file.
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// Additional Details such as an error code and the details of why it was thrown.
        /// </summary>
        public object AdditionalData { get; set; }
    }
}
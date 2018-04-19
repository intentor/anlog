namespace Anlog.Entries
{
    /// <summary>
    /// Represents a basic key/value pair log entry.
    /// </summary>
    public sealed class LogEntry : ILogEntry
    {
        /// <inheritdoc />
        public string Key { get; set; }
        
        /// <summary>
        /// Log value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="LogEntry"/>.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <param name="value">Entry value.</param>
        public LogEntry(string key, string value)
        {
            Key = key;
            Value =  value;
        }
    }
}
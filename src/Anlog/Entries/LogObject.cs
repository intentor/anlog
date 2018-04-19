using System.Collections.Generic;

namespace Anlog.Entries
{
    /// <summary>
    /// Represents a log of an object composed of log entries.
    /// </summary>
    public class LogObject : ILogEntry
    {
        /// <inheritdoc />
        public string Key { get; set; }
        
        /// <summary>
        /// Log values.
        /// </summary>
        public List<ILogEntry> Entries { get; set; } = new List<ILogEntry>();

        /// <summary>
        /// Initializes a new instance of <see cref="LogObject"/>.
        /// </summary>
        /// <param name="key">Entry key.</param>
        public LogObject(string key)
        {
            Key = key;
        }
    }
}
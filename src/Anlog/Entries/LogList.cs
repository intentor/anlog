using System.Collections.Generic;

namespace Anlog.Entries
{
    /// <summary>
    /// Represents an log list of objects.
    /// </summary>
    public class LogList : ILogEntry
    {
        /// <inheritdoc />
        public string Key { get; set; }

        /// <summary>
        /// Log values. Can be other <see cref="ILogEntry"/> objects or a string.
        /// </summary>
        public List<object> Entries { get; set; } = new List<object>();

        /// <summary>
        /// Initializes a new instance of <see cref="LogList"/>.
        /// </summary>
        /// <param name="key">Entry key.</param>
        public LogList(string key)
        {
            Key = key;
        }
    }
}
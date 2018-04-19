using System.Collections.Generic;
using Anlog.Entries;

namespace Anlog
{
    /// <summary>
    /// Writes a list of log entries.
    /// </summary>
    public interface ILogEntriesWriter
    {
        /// <summary>
        /// Formats a list of log entries.
        /// </summary>
        /// <param name="level">Log level to format.</param>
        /// <param name="entries">Log entries.</param>
        /// <returns>Formatted log.</returns>
        void Write(LogLevelName level, List<ILogEntry> entries);
    }
}
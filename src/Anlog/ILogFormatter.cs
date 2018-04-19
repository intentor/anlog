using System;
using System.Text;
using Anlog.Entries;

namespace Anlog
{
    /// <summary>
    /// Performs formatting of a log.
    /// </summary>
    interface ILogFormatter
    {
        /// <summary>
        /// Internal string Builder used to format a log.
        /// </summary>
        StringBuilder Builder { get; }
        
        /// <summary>
        /// Formats the date/time in the log.
        /// </summary>
        /// <param name="date">Date/time to write to the log.</param>
        void FormatDate(DateTime date);

        /// <summary>
        /// Formats the log level in the log.
        /// </summary>
        /// <param name="level">Log level name details.</param>
        void FormatLevel(LogLevelName level);

        /// <summary>
        /// Formats a basic key value log entry.
        /// <para/>
        /// If the key is null, writes just the value.
        /// </summary>
        /// <param name="entry">Log entry.</param>
        void FormatEntry(ILogEntry entry);

        /// <summary>
        /// Formats a basic key value log entry.
        /// <para/>
        /// If the key is null, writes just the value.
        /// </summary>
        /// <param name="entry">Log entry.</param>
        void FormatEntry(LogEntry entry);

        /// <summary>
        /// Formats a log object entry.
        /// </summary>
        /// <para/>
        /// If the key is null, writes just the values.
        /// <param name="entry">Log entry.</param>
        void FormatEntry(LogObject entry);

        /// <summary>
        /// Formats a log list entry.
        /// </summary>
        /// <para/>
        /// If the key is null, writes just the values.
        /// <param name="entry">Log entry.</param>
        void FormatEntry(LogList entry);
        
        /// <summary>
        /// Formats a log.
        /// </summary>
        /// <returns>Formatted log.</returns>
        string Format();
    }
}
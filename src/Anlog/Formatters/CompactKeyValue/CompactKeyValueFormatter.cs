using System;
using System.Collections.Generic;
using System.Text;
using Anlog.Entries;
using static Anlog.Formatters.CompactKeyValue.CompactKeyValueFormatterConstants;
using static Anlog.Formatters.DefaultFormattingOptions;

namespace Anlog.Formatters.CompactKeyValue
{
    /// <summary>
    /// Formats a list of log entries.
    /// </summary>
    public class CompactKeyValueFormatter : ILogFormatter
    {
        /// <summary>
        /// Log entries.
        /// </summary>
        private List<ILogEntry> entries;

        /// <summary>
        /// String builder used to write logs.
        /// </summary>
        private StringBuilder builder = new StringBuilder();
        
        /// <summary>
        /// Initializes a new instance of <see cref="CompactKeyValueFormatter"/>.
        /// </summary>
        /// <param name="entries">Entries to format.</param>
        public CompactKeyValueFormatter(List<ILogEntry> entries)
        {
            this.entries = entries;
        }

        /// <summary>
        /// Formats the date/time in the log.
        /// </summary>
        /// <param name="date">Date/time to write to the log.</param>
        public void FormatDate(DateTime date)
        {
            builder.Append(string.Concat(date.ToString(DateTimeFormat), EntrySeparator));
        }

        /// <summary>
        /// Formats the log level in the log.
        /// </summary>
        /// <param name="levelName">Log level name details.</param>
        public void FormatLevel(LogLevelName levelName)
        {
            builder.Append(string.Concat(ListOpening, levelName.Entry, ListClosing, EntrySeparator));
        }
        
        /// <inheritdoc />
        public void FormatEntry(ILogEntry entry)
        {
            if (entry is LogEntry)
            {
                FormatEntry((LogEntry) entry);
            }
            else if (entry is LogObject)
            {
                FormatEntry((LogObject) entry);
            }
            else if (entry is LogList)
            {
                FormatEntry((LogList) entry);
            }
        }
        
        /// <inheritdoc />
        public void FormatEntry(LogEntry entry)
        {
            if (entry.Key == null)
            {
                builder.Append(entry.Value);
            }
            else
            {
                builder.Append(string.Concat(entry.Key, KeyValueSeparator, entry.Value));
            }
        }

        /// <inheritdoc />
        public void FormatEntry(LogObject entry)
        {
            if (entry.Key != null)
            {
                builder.Append(string.Concat(entry.Key, KeyValueSeparator));
            }
            
            builder.Append(ObjectOpening);

            if (entry.Entries.Count > 0)
            {
                foreach (var value in entry.Entries)
                {
                    FormatEntry(value);
                    builder.Append(EntrySeparator);
                }
            
                builder.Length--; // Removes the last separator.
            }

            builder.Append(ObjectClosing);
        }

        /// <inheritdoc />
        public void FormatEntry(LogList entry)
        {
            if (entry.Key != null)
            {
                builder.Append(string.Concat(entry.Key, KeyValueSeparator));
            }
            
            builder.Append(ListOpening);

            if (entry.Entries.Count > 0)
            {
                foreach (var value in entry.Entries)
                {
                    if (value is ILogEntry)
                    {
                        FormatEntry((ILogEntry) value);
                    }
                    else
                    {
                        builder.Append(value);
                    }

                    builder.Append(ListItemSeparator);
                }

                builder.Length--; // Removes the last separator.
            }

            builder.Append(ListClosing);
        }
        
        /// <inheritdoc />
        public string FormatLog(LogLevelName levelName)
        {
            FormatDate(DateTime.Now);
            FormatLevel(levelName);
            
            foreach (var entry in entries)
            {
                FormatEntry(entry);

                builder.Append(EntrySeparator);
            }
            builder.Length--; // Removes the last separator.

            return builder.ToString();
        }
    }
}
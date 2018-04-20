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
        /// Log level name details.
        /// </summary>
        private readonly LogLevelName logLevelName;
        
        /// <summary>
        /// Log entries.
        /// </summary>
        private readonly List<ILogEntry> entries;

        /// <summary>
        /// String Builder used to write logs.
        /// </summary>
        public StringBuilder Builder { get; } = new StringBuilder();
        
        /// <summary>
        /// Initializes a new instance of <see cref="CompactKeyValueFormatter"/>.
        /// </summary>
        /// <param name="level">Log level name details.</param>
        /// <param name="entries">Entries to format.</param>
        public CompactKeyValueFormatter(LogLevelName level, List<ILogEntry> entries)
        {
            logLevelName = level;
            this.entries = entries;
        }

        /// <summary>
        /// Formats the date/time in the log.
        /// </summary>
        /// <param name="date">Date/time to write to the log.</param>
        public virtual void FormatDate(DateTime date)
        {
            Builder.Append(string.Concat(date.ToString(DateTimeFormat), EntrySeparator));
        }

        /// <summary>
        /// Formats the log level in the log.
        /// </summary>
        /// <param name="level">Log level name details.</param>
        public virtual void FormatLevel(LogLevelName level)
        {
            Builder.Append(string.Concat(ListOpening, level.Entry, ListClosing, EntrySeparator));
        }
        
        /// <inheritdoc />
        public virtual void FormatEntry(ILogEntry entry)
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
        public virtual void FormatEntry(LogEntry entry)
        {
            if (string.IsNullOrEmpty(entry.Key))
            {
                Builder.Append(entry.Value);
            }
            else
            {
                Builder.Append(string.Concat(entry.Key, KeyValueSeparator, entry.Value));
            }
        }

        /// <inheritdoc />
        public virtual void FormatEntry(LogObject entry)
        {
            if (!string.IsNullOrEmpty(entry.Key))
            {
                Builder.Append(string.Concat(entry.Key, KeyValueSeparator));
            }
            
            Builder.Append(ObjectOpening);

            if (entry.Entries.Count > 0)
            {
                foreach (var value in entry.Entries)
                {
                    FormatEntry(value);
                    Builder.Append(EntrySeparator);
                }
            
                Builder.Length--; // Removes the last separator.
            }

            Builder.Append(ObjectClosing);
        }

        /// <inheritdoc />
        public virtual void FormatEntry(LogList entry)
        {
            if (!string.IsNullOrEmpty(entry.Key))
            {
                Builder.Append(string.Concat(entry.Key, KeyValueSeparator));
            }
            
            Builder.Append(ListOpening);

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
                        Builder.Append(value);
                    }

                    Builder.Append(ListItemSeparator);
                }

                Builder.Length--; // Removes the last separator.
            }

            Builder.Append(ListClosing);
        }
        
        /// <inheritdoc />
        public virtual string Format()
        {
            FormatDate(DateTime.Now);
            FormatLevel(logLevelName);
            
            foreach (var entry in entries)
            {
                FormatEntry(entry);

                Builder.Append(EntrySeparator);
            }
            Builder.Length--; // Removes the last separator.

            return Builder.ToString();
        }
    }
}
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
        /// Formats a basic key value log entry.
        /// <para/>
        /// If the key is null, writes just the value.
        /// </summary>
        /// <param name="entry">Log entry.</param>
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
        
        /// <summary>
        /// Formats a basic key value log entry.
        /// <para/>
        /// If the key is null, writes just the value.
        /// </summary>
        /// <param name="entry">Log entry.</param>
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

        /// <summary>
        /// Formats a log object entry.
        /// </summary>
        /// <para/>
        /// If the key is null, writes just the values.
        /// <param name="entry">Log entry.</param>
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

        /// <summary>
        /// Formats a log list entry.
        /// </summary>
        /// <para/>
        /// If the key is null, writes just the values.
        /// <param name="entry">Log entry.</param>
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
            foreach (var entry in entries)
            {
                FormatEntry(entry);

                builder.Append(EntrySeparator);
            }
            builder.Length--; // Removes the last separator.

            return string.Concat(DateTime.Now.ToString(DateTimeFormat),  
                EntrySeparator, ListOpening, levelName.Entry, ListClosing, EntrySeparator, builder.ToString());
        }
    }
}
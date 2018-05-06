using System;
using System.Collections.Generic;
using Anlog.Entries;
using Anlog.Time;
using static Anlog.Formatters.CompactKeyValue.CompactKeyValueFormatterConstants;
using static Anlog.Formatters.DefaultFormattingOptions;

namespace Anlog.Formatters.CompactKeyValue
{
    /// <summary>
    /// Formats a list of log entries.
    /// </summary>
    public sealed class CompactKeyValueFormatter : ILogFormatter
    {
        /// <inheritdoc />
        public string Format(LogLevelName level, List<ILogEntry> entries, IDataRenderer renderer)
        {
            FormatDate(TimeProvider.Now, renderer);
            FormatLevel(level, renderer);
            
            foreach (var entry in entries)
            {
                FormatEntry(entry, renderer);
                renderer.RenderInvariant(EntrySeparator);
            }
            renderer.RemoveLastCharacter(); // Removes the last separator.

            return renderer.Render();
        }
        
        /// <summary>
        /// Formats the date/time in the log.
        /// </summary>
        /// <param name="date">Date/time to write to the log.</param>
        /// <param name="renderer">Renderer for log data.</param>
        public void FormatDate(DateTime date, IDataRenderer renderer)
        {
            renderer.RenderDate(date.ToString(DefaultDateTimeFormat))
                .RenderInvariant(EntrySeparator);
        }

        /// <summary>
        /// Formats the log level in the log.
        /// </summary>
        /// <param name="level">Log level name details.</param>
        /// <param name="renderer">Renderer for log data.</param>
        private void FormatLevel(LogLevelName level, IDataRenderer renderer)
        {
            renderer.RenderLevel(level.Level, string.Concat(ListOpening, level.Entry, ListClosing))
                .RenderInvariant(EntrySeparator);
        }
        
        /// <summary>
        /// Formats a basic keylog entry.
        /// <para/>
        /// If the key is null, writes just the value.
        /// </summary>
        /// <param name="entry">Log entry.</param>
        /// <param name="renderer">Renderer for log data.</param>
        private void FormatEntry(ILogEntry entry, IDataRenderer renderer)
        {
            if (entry is LogEntry)
            {
                FormatEntry((LogEntry) entry, renderer);
            }
            else if (entry is LogObject)
            {
                FormatEntry((LogObject) entry, renderer);
            }
            else if (entry is LogList)
            {
                FormatEntry((LogList) entry, renderer);
            }
            else if (entry is LogException)
            {
                FormatEntry((LogException) entry, renderer);
            }
        }
        
        /// <summary>
        /// Formats a basic key/value log entry.
        /// <para/>
        /// If the key is null, writes just the value.
        /// </summary>
        /// <param name="entry">Log entry.</param>
        /// <param name="renderer">Renderer for log data.</param>
        private void FormatEntry(LogEntry entry, IDataRenderer renderer)
        {
            if (string.IsNullOrEmpty(entry.Key))
            {
                renderer.RenderValue(entry.Value);
            }
            else
            {
                renderer.RenderKey(entry.Key)
                    .RenderInvariant(KeyValueSeparator)
                    .RenderValue(entry.Value);
            }
        }

        /// <summary>
        /// Formats a log object entry.
        /// </summary>
        /// <para/>
        /// If the key is null, writes just the values.
        /// <param name="entry">Log entry.</param>
        /// <param name="renderer">Renderer for log data.</param>
        private void FormatEntry(LogObject entry, IDataRenderer renderer)
        {
            if (!string.IsNullOrEmpty(entry.Key))
            {
                renderer.RenderKey(entry.Key)
                    .RenderInvariant(KeyValueSeparator);
            }
            
            renderer.RenderInvariant(ObjectOpening);

            if (entry.Entries.Count > 0)
            {
                foreach (var value in entry.Entries)
                {
                    FormatEntry(value, renderer);
                    renderer.RenderInvariant(EntrySeparator);
                }
            
                renderer.RemoveLastCharacter(); // Removes the last separator.
            }

            renderer.RenderInvariant(ObjectClosing);
        }

        /// <summary>
        /// Formats a log list entry.
        /// </summary>
        /// <para/>
        /// If the key is null, writes just the values.
        /// <param name="entry">Log entry.</param>
        /// <param name="renderer">Renderer for log data.</param>
        private void FormatEntry(LogList entry, IDataRenderer renderer)
        {
            if (!string.IsNullOrEmpty(entry.Key))
            {
                renderer.RenderKey(entry.Key)
                    .RenderInvariant(KeyValueSeparator);
            }
            
            renderer.RenderInvariant(ListOpening);

            if (entry.Entries.Count > 0)
            {
                foreach (var value in entry.Entries)
                {
                    if (value is ILogEntry)
                    {
                        FormatEntry((ILogEntry) value, renderer);
                    }
                    else
                    {
                        renderer.RenderValue(value.ToString());
                    }

                    renderer.RenderInvariant(ListItemSeparator);
                }

                renderer.RemoveLastCharacter(); // Removes the last separator.
            }

            renderer.RenderInvariant(ListClosing);
        }

        /// <summary>
        /// Formats the log level in the log.
        /// </summary>
        /// <param name="entry">Log entry.</param>
        /// <param name="renderer">Renderer for log data.</param>
        private void FormatEntry(LogException entry, IDataRenderer renderer)
        {
            renderer.RemoveLastCharacter() // Always remove the last entry separator.
                .RenderInvariant(Environment.NewLine)
                .RenderException(entry.Details);
        }
    }
}
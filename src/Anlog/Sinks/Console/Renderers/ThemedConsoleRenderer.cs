using System;
using System.Collections.Generic;
using Anlog.Entries;
using Anlog.Formatters.CompactKeyValue;
using Anlog.Sinks.Console.Themes;

namespace Anlog.Sinks.Console.Renderers
{
    /// <summary>
    /// Default theme based console renderer.
    /// <para/>
    /// It acts as a decorator to the formatter in use.
    /// </summary>
    public class ThemedConsoleRenderer : CompactKeyValueFormatter
    {
        /// <summary>
        /// Ansi code to reset a color.
        /// </summary>
        private const string ResetColor = "\x1b[0m";
        
        /// <summary>
        /// Console theme.
        /// </summary>
        private IConsoleTheme theme;

        /// <summary>
        /// Initializes a new instance of see <see cref="ThemedConsoleRenderer"/>.
        /// </summary>
        /// <param name="theme">Console theme.</param>
        /// <param name="level">Log level name details.</param>
        /// <param name="entries">Entries to format.</param>
        public ThemedConsoleRenderer(IConsoleTheme theme, LogLevelName level, List<ILogEntry> entries)
            : base(level, entries)
        {
            this.theme = theme;
        }
        
        /// <inheritdoc />
        public override void FormatDate(DateTime date)
        {
            Builder.Append(theme.DefaultColor);
            base.FormatDate(date);
            Builder.Append(ResetColor);
        }

        /// <inheritdoc />
        public override void FormatLevel(LogLevelName level)
        {
            switch (level.Level)
            {
                case LogLevel.Debug:
                    Builder.Append(theme.LevelDebugColor);
                    break;
                
                case LogLevel.Info:
                    Builder.Append(theme.LevelInfoColor);
                    break;
                
                case LogLevel.Warn:
                    Builder.Append(theme.LevelWarnColor);
                    break;
                
                case LogLevel.Error:
                    Builder.Append(theme.LevelErrorColor);
                    break;
            }
            
            base.FormatLevel(level);
            Builder.Append(ResetColor);
        }

        /// <inheritdoc />
        public override void FormatEntry(LogEntry entry)
        {
            if (!string.IsNullOrEmpty(entry.Key))
            {
                entry.Key = string.Concat(theme.KeyColor, entry.Key, ResetColor);
            }
            
            if (!string.IsNullOrEmpty(entry.Value))
            {
                entry.Value = string.Concat(theme.ValueColor, entry.Value, ResetColor);
            }

            base.FormatEntry(entry);
        }
    }
}
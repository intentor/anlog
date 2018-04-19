using System.Collections.Generic;
using Anlog.Entries;
using Anlog.Formatters.CompactKeyValue;
using Anlog.Sinks.Console.Renderers;
using Anlog.Sinks.Console.Themes;

namespace Anlog.Sinks.Console
{
    /// <summary>
    /// Writes output directly to the console.
    /// <para />
    /// It may incur in a little performance hit. If performance is an issue, please use <see cref="AsyncConsoleSink"/>.
    /// </summary>
    public sealed class ConsoleSink : ILogSink
    {
        /// <inheritdoc />
        public LogLevel? MinimumLevel { get; set; }

        /// <summary>
        /// Output theme.
        /// </summary>
        private IConsoleTheme theme;

        /// <summary>
        /// Initiliazes a new instance of <see cref="ConsoleSink"/>.
        /// </summary>
        /// <param name="theme">Output theme.</param>
        public ConsoleSink(IConsoleTheme theme)
        {
            this.theme = theme;
        }
        
        /// <inheritdoc />
        public void Write(LogLevelName level, List<ILogEntry> entries)
        {
            if (MinimumLevel.HasValue && MinimumLevel > level.Level)
            {
                return;
            }
            
            var formatter = new ThemedConsoleRenderer(theme, level, entries);
            System.Console.WriteLine(formatter.Format());
        }
    }
}
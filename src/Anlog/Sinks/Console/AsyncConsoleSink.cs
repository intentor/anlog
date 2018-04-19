using System;
using System.Collections.Generic;
using Anlog.Entries;
using Anlog.Formatters.CompactKeyValue;
using Anlog.Sinks.Console.Renderers;
using Anlog.Sinks.Console.Themes;

namespace Anlog.Sinks.Console
{
    /// <summary>
    /// Writes output to the console asynchronously.
    /// <para/>
    /// The last log(s) may be lost it the program crashes or stops before the write task is executed.
    /// </summary>
    public sealed class AsyncConsoleSink : ILogSink, IDisposable
    {
        /// <inheritdoc />
        public LogLevel? MinimumLevel { get; set; }
        
        /// <summary>
        /// Log async writer.
        /// </summary>
        private AsyncWriter asyncWriter;

        /// <summary>
        /// Output theme.
        /// </summary>
        private IConsoleTheme theme;
        
        /// <summary>
        /// Initializes a new instance of <see cref="AsyncConsoleSink"/>.
        /// </summary>
        /// <param name="theme">Output theme.</param>
        public AsyncConsoleSink(IConsoleTheme theme)
        {
            this.theme = theme;
            
            asyncWriter = new AsyncWriter(log =>
            {
                System.Console.WriteLine(log);
            });
            asyncWriter.Start();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            asyncWriter.Dispose();
        }
        
        /// <inheritdoc />
        public void Write(LogLevelName level, List<ILogEntry> entries)
        { 
            if (MinimumLevel.HasValue && MinimumLevel > level.Level)
            {
                return;
            }
            
            var formatter = new ThemedConsoleRenderer(theme, level, entries);
            asyncWriter.Enqueue(formatter.Format());
        }
    }
}
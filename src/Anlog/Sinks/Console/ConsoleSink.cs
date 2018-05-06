using System;
using System.Collections.Generic;
using Anlog.Entries;

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
        
        /// <inheritdoc />
        public ILogFormatter Formatter { get; }

        /// <summary>
        /// Renderer factory method.
        /// </summary>
        private readonly Func<IDataRenderer> renderer;

        /// <summary>
        /// Initiliazes a new instance of <see cref="ConsoleSink"/>.
        /// </summary>
        /// <param name="formatter">Log formatter.</param>
        /// <param name="renderer">Renderer factory method.</param>
        public ConsoleSink(ILogFormatter formatter, Func<IDataRenderer> renderer)
        {
            Formatter = formatter;
            this.renderer = renderer;
        }
        
        /// <inheritdoc />
        public void Write(LogLevelName level, List<ILogEntry> entries)
        {
            if (MinimumLevel.HasValue && MinimumLevel > level.Level)
            {
                return;
            }
            
            System.Console.WriteLine(Formatter.Format(level, entries, renderer()));
        }
    }
}
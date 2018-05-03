using System;
using System.Collections.Generic;
using Anlog.Entries;

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
        
        /// <inheritdoc />
        public ILogFormatter Formatter { get; }

        /// <summary>
        /// Renderer factory method.
        /// </summary>
        private Func<IDataRenderer> renderer;
        
        /// <summary>
        /// Log async writer.
        /// </summary>
        private AsyncWriter asyncWriter;
        
        /// <summary>
        /// Initializes a new instance of <see cref="AsyncConsoleSink"/>.
        /// </summary>
        /// <param name="formatter">Log formatter.</param>
        /// <param name="renderer">Renderer factory method.</param>
        public AsyncConsoleSink(ILogFormatter formatter, Func<IDataRenderer> renderer)
        {
            Formatter = formatter;
            this.renderer = renderer;
            
            asyncWriter = new AsyncWriter(System.Console.WriteLine);
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
            
            asyncWriter.Enqueue(Formatter.Format(level, entries, renderer()));
        }
    }
}
using System;
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
        /// Output renderer
        /// </summary>
        private IConsoleRenderer renderer;
        
        /// <summary>
        /// Initializes a new instance of <see cref="AsyncConsoleSink"/>.
        /// </summary>
        /// <param name="theme">Output theme.</param>
        public AsyncConsoleSink(IConsoleTheme theme)
        {
            if (theme != null)
            {
                renderer = new CompactKeyValueRenderer(theme);
            }
            
            asyncWriter = new AsyncWriter(log =>
            {
                System.Console.WriteLine(renderer != null ? renderer.Render(log) : log);
            });
            asyncWriter.Start();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            asyncWriter.Dispose();
        }
        
        /// <inheritdoc />
        public void Write(LogLevel level, string log)
        { 
            if (MinimumLevel.HasValue && MinimumLevel > level)
            {
                return;
            }
            
            asyncWriter.Enqueue(log);
        }
    }
}
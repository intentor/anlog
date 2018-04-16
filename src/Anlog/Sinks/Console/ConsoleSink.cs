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
        /// Output renderer
        /// </summary>
        private IConsoleRenderer renderer;

        /// <summary>
        /// Initiliazes a new instance of <see cref="ConsoleSink"/>.
        /// </summary>
        /// <param name="theme">Output theme.</param>
        public ConsoleSink(IConsoleTheme theme)
        {
            if (theme != null)
            {
                renderer = new CompactKeyValueRenderer(theme);
            }
        }
        
        /// <inheritdoc />
        public void Write(LogLevel level, string log)
        {
            if (MinimumLevel.HasValue && MinimumLevel > level)
            {
                return;
            }
            
            System.Console.WriteLine(renderer != null ? renderer.Render(log) : log);
        }
    }
}
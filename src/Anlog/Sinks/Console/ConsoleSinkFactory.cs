using Anlog.Factories;
using Anlog.Sinks.Console.Themes;

namespace Anlog.Sinks.Console
{
    /// <summary>
    /// Factory for the <see cref="ConsoleSink"/>.
    /// </summary>
    public static class ConsoleSinkFactory
    {
        /// <summary>
        /// Writes the output to the console.
        /// </summary>
        /// <param name="sinksFactory">Sinks factory.</param>
        /// <param name="async">True if write to the console should be asynchronous, otherwise false.
        /// The default is false.</param>
        /// <param name="theme">Output color theme.</param>
        /// <param name="minimumLevel">Minimum log level. The default is the logger minimum level.</param>
        /// <returns>Logger factory.</returns>
        public static LoggerFactory Console(this LogSinksFactory sinksFactory, bool async = false, 
            IConsoleTheme theme = null, LogLevel? minimumLevel = null)
        {
            theme = theme ?? new DefaultConsoleTheme();
            var sink = async ? (ILogSink) new AsyncConsoleSink(theme) : new ConsoleSink(theme);
            sink.MinimumLevel = minimumLevel;
            
            sinksFactory.Sinks.Add(sink);
            
            return sinksFactory.Factory;
        }
    }
 }
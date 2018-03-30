using Anlog.Factories;

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
        /// <param name="async">True if write to the console should be asynchronous, otherwise false.</param>
        /// <returns>Logger factory.</returns>
        public static LoggerFactory Console(this LogSinksFactory sinksFactory, bool async = false)
        {
            sinksFactory.Sinks.Add(async ? (ILogSink) new AsyncConsoleSink() : new ConsoleSink());
            return sinksFactory.Factory;
        }
    }
 }
using Anlog.Factories;

namespace Anlog.Sinks.InMemory
{
    /// <summary>
    /// Factory for the <see cref="InMemorySink"/>.
    /// </summary>
    public static class InMemorySinkFactory
    {
        /// <summary>
        /// Writes the output to an in-memory variable.
        /// <para />
        /// Please use <see cref="InMemorySink.GetLogs"/> to read the logs.
        /// </summary>
        /// <param name="sinksFactory">Sinks factory.</param>
        /// <param name="appendNewLine">Indicates whether a new line should be appended at the end of each log.
        /// The default is true.</param>
        /// <param name="minimumLevel">Minimum log level. The default is the logger minimum level.</param>
        /// <returns>Logger factory.</returns>
        public static LoggerFactory InMemory(this LogSinksFactory sinksFactory, bool appendNewLine = true, 
            LogLevel? minimumLevel = null)
        {
            sinksFactory.Sinks.Add(new InMemorySink()
            {
                MinimumLevel =  minimumLevel,
                AppendNewLine = appendNewLine
            });
            return sinksFactory.Factory;
        }
    }
}
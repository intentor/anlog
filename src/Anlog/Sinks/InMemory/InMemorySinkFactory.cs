using Anlog.Factories;
using Anlog.Formatters.CompactKeyValue;
using Anlog.Renderers;

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
        /// <param name="formatter">Log formatter to be used. The default is
        /// <see cref="CompactKeyValueFormatter"/>.</param>
        /// <returns>Logger factory.</returns>
        public static LoggerFactory InMemory(this LogSinksFactory sinksFactory, bool appendNewLine = true, 
            LogLevel? minimumLevel = null, ILogFormatter formatter = null)
        {
            formatter = formatter ?? new CompactKeyValueFormatter();
            sinksFactory.Sinks.Add(new InMemorySink(formatter, () => new DefaultDataRenderer())
            {
                AppendNewLine = appendNewLine,
                MinimumLevel =  minimumLevel
            });
            return sinksFactory.Factory;
        }
    }
}
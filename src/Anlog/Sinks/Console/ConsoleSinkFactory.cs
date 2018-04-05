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
        /// <returns>Logger factory.</returns>
        public static LoggerFactory Console(this LogSinksFactory sinksFactory)
        {
            sinksFactory.Sinks.Add(new ConsoleSink());
            return sinksFactory.Factory;
        }
    }
 }
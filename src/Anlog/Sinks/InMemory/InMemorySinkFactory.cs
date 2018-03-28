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
        /// <returns>Logger factory.</returns>
        public static LoggerFactory InMemory(this LogSinksFactory sinksFactory)
        {
            sinksFactory.Sinks.Add(new InMemorySink());
            return sinksFactory.Factory;
        }
    }
}
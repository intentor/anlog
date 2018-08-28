using System.Collections.Generic;

namespace Anlog.Factories
{
    /// <summary>
    /// Creates output sinks.
    /// <para />
    /// It's used as basis for extension methods to chain the creation of sinks.
    /// </summary>
    public class LogSinksFactory
    {
        /// <summary>
        /// Logger factory.
        /// </summary>
        public LoggerFactory Factory { get; }
        
        /// <summary>
        /// Available sinks.
        /// </summary>
        public List<ILogSink> Sinks { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="LogSinksFactory"/>.
        /// </summary>
        /// <param name="factory">Base logger factory.</param>
        public LogSinksFactory(LoggerFactory factory)
        {
            Factory = factory;
            Sinks = new List<ILogSink>();
        }
    }
}
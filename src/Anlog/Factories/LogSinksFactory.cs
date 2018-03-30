using System.Collections.Generic;
using Anlog.Sinks.SingleFile;

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
        internal LoggerFactory Factory { get; }
        
        /// <summary>
        /// Available sinks.
        /// </summary>
        internal List<ILogSink> Sinks { get; }

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
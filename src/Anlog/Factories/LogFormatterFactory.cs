namespace Anlog.Factories
{
    /// <summary>
    /// Creates log formatters.
    /// <para />
    /// It's used as basis for extension methods to chain the creation of formatters.
    /// </summary>
    public class LogFormatterFactory
    {
        /// <summary>
        /// Logger factory.
        /// </summary>
        internal LoggerFactory Factory { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="LogFormatterFactory"/>.
        /// </summary>
        /// <param name="factory">Base logger factory.</param>
        public LogFormatterFactory(LoggerFactory factory)
        {
            Factory = factory;
        }
    }
}
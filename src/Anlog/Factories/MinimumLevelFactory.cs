namespace Anlog.Factories
{
    /// <summary>
    /// Factory for setting the minimum level of a log.
    /// </summary>
    public class MinimumLevelFactory
    {
        /// <summary>
        /// Logger factory.
        /// </summary>
        internal LoggerFactory Factory { get; }
        
        /// <summary>
        /// Selected log level. The default is Debug.
        /// </summary>
        internal LogLevel Level { get; private set; }

        /// <summary>
        /// Initializes a new instance of <see cref="MinimumLevelFactory"/>.
        /// </summary>
        /// <param name="factory">Base logger factory.</param>
        public MinimumLevelFactory(LoggerFactory factory)
        {
            Factory = factory;

            Level = LogLevel.Debug;
        }

        /// <summary>
        /// Sets the minimum level.
        /// </summary>
        /// <param name="level">Level to set.</param>
        /// <returns>Logger factory.</returns>
        public LoggerFactory Set(LogLevel level)
        {
            Level = level;
            return Factory;
        }

        /// <summary>
        /// Sets the minimum level as Debug.
        /// </summary>
        /// <returns>Logger factory.</returns>
        public LoggerFactory Debug()
        {
            Set(LogLevel.Debug);
            return Factory;
        }

        /// <summary>
        /// Sets the minimum level as Information.
        /// </summary>
        /// <returns>Logger factory.</returns>
        public LoggerFactory Info()
        {
            Set(LogLevel.Info);
            return Factory;
        }

        /// <summary>
        /// Sets the minimum level as Warning.
        /// </summary>
        /// <returns>Logger factory.</returns>
        public LoggerFactory Warn()
        {
            Set(LogLevel.Warn);
            return Factory;
        }

        /// <summary>
        /// Sets the minimum level as Error.
        /// </summary>
        /// <returns>Logger factory.</returns>
        public LoggerFactory Error()
        {
            Set(LogLevel.Error);
            return Factory;
        }
    }
}
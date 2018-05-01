using System;
using Anlog.Loggers;

namespace Anlog.Factories
{
    /// <summary>
    /// Creates loggers.
    /// </summary>
    public sealed class LoggerFactory
    {
        /// <summary>
        /// Sets the minimum level for all sinks.
        /// </summary>
        /// <remarks>
        /// Please note that every sink can also have its own minimum level.
        /// </remarks>
        public MinimumLevelFactory MinimumLevel { get; }

        /// <summary>
        /// Writes the logs to a given output.
        /// </summary>
        public LogSinksFactory WriteTo { get; }
        
        /// <summary>
        /// Logger type.
        /// </summary>
        private Type loggerType;
        
        /// <summary>
        /// Initializes a new instance of <see cref="LoggerFactory"/> using a <see cref="DefaultLogger"/>.
        /// </summary>
        public LoggerFactory() : this(typeof(DefaultLogger))
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="LoggerFactory"/>.
        /// </summary>
        /// <param name="loggerType">Logger type. Should have a parameterless constructor.</param>
        public LoggerFactory(Type loggerType)
        {
            this.loggerType = loggerType;

            MinimumLevel = new MinimumLevelFactory(this);
            WriteTo = new LogSinksFactory(this);
        }
        
        /// <summary>
        /// Creates the logger.
        /// </summary>
        /// <returns>Created logger.</returns>
        public ILogger CreateLogger()
        {
            var logger = (ILogger) Activator.CreateInstance(loggerType);

            // Add sinks first so minimum level can be set.
            logger.Sinks = WriteTo.Sinks;
            logger.MinimumLevel = MinimumLevel.Level;

            return logger;
        }
    }
}
using System;
using Anlog.Formatters.CompactKeyValue;
using Anlog.Loggers;

namespace Anlog.Factories
{
    /// <summary>
    /// Creates loggers.
    /// </summary>
    public sealed class LoggerFactory
    {
        /// <summary>
        /// Sets the minimum level for a log.
        /// </summary>
        public MinimumLevelFactory MinimumLevel { get; }
        
        /// <summary>
        /// Formats the logs with the given formatter.
        /// </summary>
        public LogFormatterFactory FormatAs { get; }

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
            FormatAs = new LogFormatterFactory(this);
            WriteTo = new LogSinksFactory(this);
        }
        
        /// <summary>
        /// Creates the logger.
        /// </summary>
        /// <returns>Created logger.</returns>
        public ILogger CreateLogger()
        {
            var logger = (ILogger) Activator.CreateInstance(loggerType);

            if (FormatAs.Formatter == null)
            {
                FormatAs.CompactKeyValue();
            }

            logger.MinimumLevel = MinimumLevel.Level;
            logger.Formatter = FormatAs.Formatter;
            logger.Sinks = WriteTo.Sinks;

            return logger;
        }
    }
}
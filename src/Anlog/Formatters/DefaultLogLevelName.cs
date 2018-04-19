namespace Anlog.Formatters
{
    /// <summary>
    /// Default log level details.
    /// </summary>
    public class DefaultLogLevelName : ILogLevelName
    {
        /// <inheritdoc />
        public LogLevelName Debug { get; }
        
        /// <inheritdoc />
        public LogLevelName Info { get; }
        
        /// <inheritdoc />
        public LogLevelName Warn { get; }
        
        /// <inheritdoc />
        public LogLevelName Error { get; }
        
        /// <summary>
        /// Initializes a new instance of <see cref="DefaultLogLevelName"/>.
        /// </summary>
        public DefaultLogLevelName()
        {
            Debug = new LogLevelName()
            {
                Level = LogLevel.Debug,
                Entry = "DBG",
                Key = "d"
            };
            Info = new LogLevelName()
            {
                Level = LogLevel.Info,
                Entry = "INF",
                Key = "i"
            };
            Warn = new LogLevelName()
            {
                Level = LogLevel.Warn,
                Entry = "WRN",
                Key = "w"
            };
            Error = new LogLevelName()
            {
                Level = LogLevel.Error,
                Entry = "ERR",
                Key = "e"
            };
        }
    }
}
namespace Anlog.Formatters.CompactKeyValue
{
    /// <summary>
    /// Log level details for the <see cref="CompactKeyValueFormatter"/>.
    /// </summary>
    public class CompactKeyValueLogLevelName : ILogLevelName
    {
        /// <inheritdoc />
        public LogLevelName Debug { get; }
        
        /// <inheritdoc />
        public LogLevelName Info { get; }
        
        /// <inheritdoc />
        public LogLevelName Warning { get; }
        
        /// <inheritdoc />
        public LogLevelName Error { get; }
        
        /// <summary>
        /// Initializes a new instance of <see cref="CompactKeyValueLogLevelName"/>.
        /// </summary>
        public CompactKeyValueLogLevelName()
        {
            Debug = new LogLevelName()
            {
                Entry = "DBG",
                Key = "d"
            };
            Info = new LogLevelName()
            {
                Entry = "INF",
                Key = "i"
            };
            Warning = new LogLevelName()
            {
                Entry = "WRN",
                Key = "w"
            };
            Error = new LogLevelName()
            {
                Entry = "ERR",
                Key = "e"
            };
        }
    }
}
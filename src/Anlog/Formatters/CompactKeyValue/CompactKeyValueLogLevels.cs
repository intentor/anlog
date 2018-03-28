namespace Anlog.Formatters.CompactKeyValue
{
    /// <summary>
    /// Log level details for the <see cref="CompactKeyValueFormatter"/>.
    /// </summary>
    public class CompactKeyValueLogLevels : ILogLevels
    {
        /// <inheritdoc />
        public LogLevel Debug { get; }
        
        /// <inheritdoc />
        public LogLevel Info { get; }
        
        /// <inheritdoc />
        public LogLevel Warning { get; }
        
        /// <inheritdoc />
        public LogLevel Error { get; }
        
        /// <summary>
        /// Initializes a new instance of <see cref="CompactKeyValueLogLevels"/>.
        /// </summary>
        public CompactKeyValueLogLevels()
        {
            Debug = new LogLevel()
            {
                Name = "DBG",
                Key = "d"
            };
            Info = new LogLevel()
            {
                Name = "INF",
                Key = "i"
            };
            Warning = new LogLevel()
            {
                Name = "WRN",
                Key = "w"
            };
            Error = new LogLevel()
            {
                Name = "ERR",
                Key = "e"
            };
        }
    }
}
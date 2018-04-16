namespace Anlog
{
    /// <summary>
    /// Provides details for a log level.
    /// </summary>
    public class LogLevelName
    {
        /// <summary>
        /// Entry in the log level enumeration related to this level name.
        /// </summary>
        public LogLevel Level { get; set; }
        
        /// <summary>
        /// Log level entry name.
        /// </summary>
        public string Entry { get; set; }

        /// <summary>
        /// Log level entry key.
        /// </summary>
        public string Key { get; set; }
    }
}
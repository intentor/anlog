namespace Anlog
{
    /// <summary>
    /// Details for log levels.
    /// </summary>
    public interface ILogLevelName
    {
        /// <summary>
        /// Debug log level.
        /// </summary>
        LogLevelName Debug { get; }
        
        /// <summary>
        /// Information log level.
        /// </summary>
        LogLevelName Info { get; }
        
        /// <summary>
        /// Warning log level.
        /// </summary>
        LogLevelName Warn { get; }
        
        /// <summary>
        /// Error log level.
        /// </summary>
        LogLevelName Error { get; }
    }
}
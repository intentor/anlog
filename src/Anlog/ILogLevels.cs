namespace Anlog
{
    /// <summary>
    /// Details for log levels.
    /// </summary>
    public interface ILogLevels
    {
        /// <summary>
        /// Debug log level.
        /// </summary>
        LogLevel Debug { get; }
        
        /// <summary>
        /// Information log level.
        /// </summary>
        LogLevel Info { get; }
        
        /// <summary>
        /// Warning log level.
        /// </summary>
        LogLevel Warning { get; }
        
        /// <summary>
        /// Error log level.
        /// </summary>
        LogLevel Error { get; }
    }
}
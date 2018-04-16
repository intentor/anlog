namespace Anlog
{
    /// <summary>
    /// Writes logs to an output.
    /// </summary>
    public interface ILogSink
    {
        /// <summary>
        /// Minimum log level for the sink. If no minimum level is defined, all logs will be written.
        /// </summary>
        LogLevel? MinimumLevel { get; set; }
        
        /// <summary>
        /// Writes a line of log to an output.
        /// </summary>
        /// <param name="level">Log level.</param>
        /// <param name="log">Log to write.</param>
        void Write(LogLevel level, string log);
    }
}
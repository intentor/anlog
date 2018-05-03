namespace Anlog
{
    /// <summary>
    /// Writes logs to an output.
    /// </summary>
    public interface ILogSink : ILogWriter
    {
        /// <summary>
        /// Minimum log level for the sink. If no minimum level is defined, all logs will be written.
        /// </summary>
        LogLevel? MinimumLevel { get; set; }

        /// <summary>
        /// Formatter used by the sink.
        /// </summary>
        ILogFormatter Formatter { get; }
    }
}
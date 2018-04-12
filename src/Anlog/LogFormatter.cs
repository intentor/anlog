namespace Anlog
{
    /// <summary>
    /// Creates a log formatter.
    /// </summary>
    /// <param name="minimumLevel">Minimum level to write the log to.</param>
    /// <param name="sink">Logger sinker.</param>
    /// <param name="callerFilePath">caller class file path that originated the log.</param>
    /// <param name="callerMemberName">caller class member name that originated the log.</param>
    /// <param name="callerLineNumber">caller line number that originated the log.</param>
    /// <returns>Created log formatter.</returns>
    public delegate ILogFormatter LogFormatter(LogLevel minimumLevel, ILogSink sink, string callerFilePath,
        string callerMemberName, int callerLineNumber);
}
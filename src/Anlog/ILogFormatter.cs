namespace Anlog
{
    /// <summary>
    /// Performs formatting of a log.
    /// </summary>
    public interface ILogFormatter
    {
        /// <summary>
        /// Formats a log.
        /// </summary>
        /// <param name="levelName">Log level to format to.</param>
        /// <returns>Formatted log.</returns>
        string FormatLog(LogLevelName levelName);
    }
}
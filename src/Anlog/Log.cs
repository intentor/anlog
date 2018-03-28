using System.Runtime.CompilerServices;

namespace Anlog
{
    /// <summary>
    /// Entry point for logging.
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// Logger to write the logs to.
        /// </summary>
        public static ILogger Logger;
        
        /// <summary>
        /// Appends an entry to the log.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <param name="value">Entry value.</param>
        /// <param name="callerFilePath">Caller class file path that originated the log.</param>
        /// <param name="callerMemberName">Caller class member name that originated the log.</param>
        /// <param name="callerLineNumber">Caller line number that originated the log.</param>
        /// <returns>Log writer, for chaining.</returns>
        public static ILogFormatter Append(string key, string value,
            [CallerFilePath] string callerFilePath = null,
            [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            return Logger.Append(key, value, callerFilePath, callerMemberName, callerLineNumber);
        }
    }
}
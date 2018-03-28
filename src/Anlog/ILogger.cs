using System.Collections.Generic;

namespace Anlog
{
    /// <summary>
    /// Provides logging capabilities.
    /// </summary>
    public interface ILogger : ILogSink
    {
        /// <summary>
        /// Log formatter factory.
        /// </summary>
        LogFormatter Formatter { get; set; }
        
        /// <summary>
        /// Log sinks.
        /// </summary>
        List<ILogSink> Sinks { get; set; }

        /// <summary>
        /// Appends an entry to the log.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <param name="value">Entry value.</param>
        /// <param name="callerFilePath">Caller class file path that originated the log.</param>
        /// <param name="callerMemberName">Caller class member name that originated the log.</param>
        /// <param name="callerLineNumber">Caller line number that originated the log.</param>
        /// <returns>Log writer, for chaining.</returns>
        ILogFormatter Append(string key, string value, string callerFilePath,  string callerMemberName, 
            int callerLineNumber);
    }
}
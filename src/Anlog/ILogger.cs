using System;
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
        ILogFormatter Append(string key, string value, string callerFilePath, string callerMemberName, 
            int callerLineNumber);
        
        /// <summary>
        /// Appends an entry to the log.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <param name="value">Entry value.</param>
        /// <param name="callerFilePath">Caller class file path that originated the log.</param>
        /// <param name="callerMemberName">Caller class member name that originated the log.</param>
        /// <param name="callerLineNumber">Caller line number that originated the log.</param>
        /// <returns>Log writer, for chaining.</returns>
        ILogFormatter Append(string key, object value, string callerFilePath, string callerMemberName, 
            int callerLineNumber);
        
        /// <summary>
        /// Appends an entry to the log.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <param name="values">Entry values.</param>
        /// <param name="callerFilePath">Caller class file path that originated the log.</param>
        /// <param name="callerMemberName">Caller class member name that originated the log.</param>
        /// <param name="callerLineNumber">Caller line number that originated the log.</param>
        /// <typeparam name="T">Object type.</typeparam>
        /// <returns>Log writer, for chaining.</returns>
        ILogFormatter Append<T>(string key, T[] values, string callerFilePath, string callerMemberName, 
            int callerLineNumber);
        
        /// <summary>
        /// Appends an entry to the log.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <param name="values">Entry values.</param>
        /// <param name="callerFilePath">Caller class file path that originated the log.</param>
        /// <param name="callerMemberName">Caller class member name that originated the log.</param>
        /// <param name="callerLineNumber">Caller line number that originated the log.</param>
        /// <typeparam name="T">Object type.</typeparam>
        /// <returns>Log writer, for chaining.</returns>
        ILogFormatter Append<T>(string key, IEnumerable<T> values, string callerFilePath, string callerMemberName, 
            int callerLineNumber);

        /// <summary>
        /// Writes the log as debug.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="callerFilePath">Caller class file path that originated the log.</param>
        /// <param name="callerMemberName">Caller class member name that originated the log.</param>
        /// <param name="callerLineNumber">Caller line number that originated the log.</param>
        void Debug(string message, string callerFilePath, string callerMemberName, int callerLineNumber);
        
        /// <summary>
        /// Writes the log as information.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="callerFilePath">Caller class file path that originated the log.</param>
        /// <param name="callerMemberName">Caller class member name that originated the log.</param>
        /// <param name="callerLineNumber">Caller line number that originated the log.</param>
        void Info(string message, string callerFilePath, string callerMemberName, int callerLineNumber);

        /// <summary>
        /// Writes the log as warning.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="callerFilePath">Caller class file path that originated the log.</param>
        /// <param name="callerMemberName">Caller class member name that originated the log.</param>
        /// <param name="callerLineNumber">Caller line number that originated the log.</param>
        void Warning(string message, string callerFilePath, string callerMemberName, int callerLineNumber);

        /// <summary>
        /// Writes the log as error.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="callerFilePath">Caller class file path that originated the log.</param>
        /// <param name="callerMemberName">Caller class member name that originated the log.</param>
        /// <param name="callerLineNumber">Caller line number that originated the log.</param>
        void Error(string message, string callerFilePath, string callerMemberName, int callerLineNumber);

        /// <summary>
        /// Writes the log as error.
        /// </summary>
        /// <param name="e">Exception details.</param>
        /// <param name="callerFilePath">Caller class file path that originated the log.</param>
        /// <param name="callerMemberName">Caller class member name that originated the log.</param>
        /// <param name="callerLineNumber">Caller line number that originated the log.</param>
        void Error(Exception e, string callerFilePath, string callerMemberName, int callerLineNumber);
        
        /// <summary>
        /// Writes the log as error.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="e">Exception details.</param>
        /// <param name="callerFilePath">Caller class file path that originated the log.</param>
        /// <param name="callerMemberName">Caller class member name that originated the log.</param>
        /// <param name="callerLineNumber">Caller line number that originated the log.</param>
        void Error(string message, Exception e, string callerFilePath, string callerMemberName, int callerLineNumber);
    }
}
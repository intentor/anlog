using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Anlog.Loggers;

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
        /// <remarks>
        /// As default, it's a dummy logger that logs to nothing.
        /// </remarks>
        public static ILogger Logger = new DummyLogger();

        /// <summary>
        /// Disposes the logging system.
        /// </summary>
        public static void Dispose()
        {
            Logger?.Dispose();
        }
        
        /// <summary>
        /// Appends an entry to the log.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <param name="value">Entry value.</param>
        /// <param name="callerFilePath">Caller class file path that originated the log.</param>
        /// <param name="callerMemberName">Caller class member name that originated the log.</param>
        /// <param name="callerLineNumber">Caller line number that originated the log.</param>
        /// <returns>Log writer, for chaining.</returns>
        public static ILogAppender Append(string key, string value,
            [CallerFilePath] string callerFilePath = null,
            [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            return Logger.Append(key, value, callerFilePath, callerMemberName, callerLineNumber);
        }
        
        /// <summary>
        /// Appends an entry to the log.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <param name="value">Entry value.</param>
        /// <param name="callerFilePath">Caller class file path that originated the log.</param>
        /// <param name="callerMemberName">Caller class member name that originated the log.</param>
        /// <param name="callerLineNumber">Caller line number that originated the log.</param>
        /// <returns>Log writer, for chaining.</returns>
        public static ILogAppender Append(string key, object value,
            [CallerFilePath] string callerFilePath = null,
            [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            return Logger.Append(key, value, callerFilePath, callerMemberName, callerLineNumber);
        }

        /// <summary>
        /// Writes the log as debug.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="callerFilePath">Caller class file path that originated the log.</param>
        /// <param name="callerMemberName">Caller class member name that originated the log.</param>
        /// <param name="callerLineNumber">Caller line number that originated the log.</param>
        public static void Debug(string message = null,
            [CallerFilePath] string callerFilePath = null,
            [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            Logger.Debug(message, callerFilePath, callerMemberName, callerLineNumber);
        }
        
        /// <summary>
        /// Writes the log as information.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="callerFilePath">Caller class file path that originated the log.</param>
        /// <param name="callerMemberName">Caller class member name that originated the log.</param>
        /// <param name="callerLineNumber">Caller line number that originated the log.</param>
        public static void Info(string message = null,
            [CallerFilePath] string callerFilePath = null,
            [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            Logger.Info(message, callerFilePath, callerMemberName, callerLineNumber);
        }

        /// <summary>
        /// Writes the log as warning.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="callerFilePath">Caller class file path that originated the log.</param>
        /// <param name="callerMemberName">Caller class member name that originated the log.</param>
        /// <param name="callerLineNumber">Caller line number that originated the log.</param>
        public static void Warn(string message = null,
            [CallerFilePath] string callerFilePath = null,
            [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            Logger.Warn(message, callerFilePath, callerMemberName, callerLineNumber);
        }

        /// <summary>
        /// Writes the log as error.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="callerFilePath">Caller class file path that originated the log.</param>
        /// <param name="callerMemberName">Caller class member name that originated the log.</param>
        /// <param name="callerLineNumber">Caller line number that originated the log.</param>
        public static void Error(string message = null,
            [CallerFilePath] string callerFilePath = null,
            [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            Logger.Error(message, callerFilePath, callerMemberName, callerLineNumber);
        }

        /// <summary>
        /// Writes the log as error.
        /// </summary>
        /// <param name="e">Exception details.</param>
        /// <param name="message">Log message.</param>
        /// <param name="callerFilePath">Caller class file path that originated the log.</param>
        /// <param name="callerMemberName">Caller class member name that originated the log.</param>
        /// <param name="callerLineNumber">Caller line number that originated the log.</param>
        public static void Error(Exception e, string message = null,
            [CallerFilePath] string callerFilePath = null,
            [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            Logger.Error(e, message, callerFilePath, callerMemberName, callerLineNumber);
        }

        /// <summary>
        /// Gets a sink from the <see cref="Logger"/>.
        /// </summary>
        /// <typeparam name="T">Sink type.</typeparam>
        /// <returns>Sink or null if no sink is found.</returns>
        public static T GetSink<T>() where T : class, ILogSink
        {
            return Logger?.GetSink<T>();
        }

        /// <summary>
        /// Gets a sink from the <see cref="Logger"/>.
        /// </summary>
        /// <param name="type">Sink type.</param>
        /// <returns>Sink or null if no sink is found.</returns>
        public static ILogSink GetSink(Type type)
        {
            return Logger?.GetSink(type);
        }
    }
}
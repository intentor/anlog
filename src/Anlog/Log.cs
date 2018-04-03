using System;
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

        /// <summary>
        /// Gets a sink from the <see cref="Logger"/>.
        /// </summary>
        /// <typeparam name="T">Sink type.</typeparam>
        /// <returns>Sink or null if no sink is found.</returns>
        public static T GetSink<T>() where T : ILogSink
        {
            return (T) GetSink(typeof(T));
        }

        /// <summary>
        /// Gets a sink from the <see cref="Logger"/>.
        /// </summary>
        /// <param name="type">Sink type.</param>
        /// <returns>Sink or null if no sink is found.</returns>
        public static ILogSink GetSink(Type type)
        {
            return Logger?.Sinks.Find(s => s.GetType() == type);
        }
    }
}
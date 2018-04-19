using System;
using System.Collections.Generic;
using Anlog.Appenders.Dummy;
using Anlog.Entries;

namespace Anlog.Loggers
{
    /// <summary>
    /// Logs to nothing.
    /// </summary>
    public sealed class DummyLogger : ILogger
    {
        /// <summary>
        /// Dummy formatter instance.
        /// </summary>
        private static readonly ILogAppender DummyAppender = new DummyLogAppender();
        
        /// <inheritdoc />
        public List<ILogSink> Sinks { get; set; }

        /// <inheritdoc />
        public LogLevel? MinimumLevel { get; set; }
        
        /// <inheritdoc />
        public void Dispose()
        {
            // Does nothing.
        }
        
        /// <inheritdoc />
        public T GetSink<T>() where T : ILogSink
        {
            return default(T);
        }

        /// <inheritdoc />
        public ILogSink GetSink(Type type)
        {
            return null;
        }

        /// <inheritdoc />
        public ILogAppender Append(string key, string value, string callerFilePath = null, 
            string callerMemberName = null, int callerLineNumber = 0)
        {
            return DummyAppender;
        }

        /// <inheritdoc />
        public ILogAppender Append(string key, object value, string callerFilePath = null, 
            string callerMemberName = null, int callerLineNumber = 0)
        {
            return DummyAppender;
        }

        /// <inheritdoc />
        public void Debug(string message, string callerFilePath = null,  string callerMemberName = null,
            int callerLineNumber = 0)
        {
            // Does nothing.
        }
        
        /// <inheritdoc />
        public void Info(string message, string callerFilePath = null,  string callerMemberName = null,
            int callerLineNumber = 0)
        {
            // Does nothing.
        }

        /// <inheritdoc />
        public void Warn(string message, string callerFilePath = null,  string callerMemberName = null,
            int callerLineNumber = 0)
        {
            // Does nothing.
        }

        /// <inheritdoc />
        public void Error(string message, string callerFilePath = null,  string callerMemberName = null,
            int callerLineNumber = 0)
        {
            // Does nothing.
        }
        
        /// <inheritdoc />
        public void Error(Exception e, string message, string callerFilePath = null,  string callerMemberName = null,
            int callerLineNumber = 0)
        {
            // Does nothing.
        }

        /// <inheritdoc />
        public void Write(LogLevelName level, List<ILogEntry> entries)
        {
            // Does nothing.
        }
    }
}
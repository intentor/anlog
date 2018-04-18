using System;
using System.Collections.Generic;
using Anlog.Formatters.Dummy;

namespace Anlog.Loggers
{
    /// <summary>
    /// Dummy logger that logs to nothing.
    /// </summary>
    public sealed class DummyLogger : ILogger
    {
        /// <summary>
        /// Dummy formatter instance.
        /// </summary>
        private static readonly ILogFormatter DummyFormatterInstance = new DummyFormatter();
            
        /// <inheritdoc />
        public LogFormatter Formatter { get; set; }
        
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
        public ILogFormatter Append(string key, string value, string callerFilePath = null, 
            string callerMemberName = null, int callerLineNumber = 0)
        {
            return DummyFormatterInstance;
        }

        /// <inheritdoc />
        public ILogFormatter Append(string key, object value, string callerFilePath = null, 
            string callerMemberName = null, int callerLineNumber = 0)
        {
            return DummyFormatterInstance;
        }
        
        /// <inheritdoc />
        public ILogFormatter Append<T>(string key, T[] values, string callerFilePath = null, 
            string callerMemberName = null, int callerLineNumber = 0)
        {
            return DummyFormatterInstance;
        }
        
        /// <inheritdoc />
        public ILogFormatter Append<T>(string key, IEnumerable<T> values, string callerFilePath = null, 
            string callerMemberName = null, int callerLineNumber = 0)
        {
            return DummyFormatterInstance;
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
        public void Error(Exception e, string callerFilePath = null,  string callerMemberName = null,
            int callerLineNumber = 0)
        {
            // Does nothing.
        }
        
        /// <inheritdoc />
        public void Error(string message, Exception e, string callerFilePath = null,  string callerMemberName = null,
            int callerLineNumber = 0)
        {
            // Does nothing.
        }

        /// <inheritdoc />
        public void Write(LogLevel level, string log)
        {
            // Writes to nothing.
        }
    }
}
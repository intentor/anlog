using System;
using System.Collections.Generic;

namespace Anlog.Loggers
{
    /// <summary>
    /// Default logger.
    /// </summary>
    public sealed class DefaultLogger : ILogger
    {
        /// <inheritdoc />
        public LogFormatter Formatter { get; set; }
        
        /// <inheritdoc />
        public List<ILogSink> Sinks { get; set; } = new List<ILogSink>();

        /// <inheritdoc />
        public LogLevel MinimumLevel { get; set; } = LogLevel.Debug;

        /// <inheritdoc />
        public void Dispose()
        {
            foreach (var sink in Sinks)
            {
                (sink as IDisposable)?.Dispose();
            }
        }
        
        /// <inheritdoc />
        public T GetSink<T>() where T : ILogSink
        {
            return (T) GetSink(typeof(T));
        }

        /// <inheritdoc />
        public ILogSink GetSink(Type type)
        {
            return Sinks.Find(s => s.GetType() == type);
        }

        /// <inheritdoc />
        public ILogFormatter Append(string key, string value, string callerFilePath, string callerMemberName, 
            int callerLineNumber)
        {
            return Formatter(MinimumLevel, this, callerFilePath, callerMemberName, callerLineNumber)
                .Append(key, value);
        }

        /// <inheritdoc />
        public ILogFormatter Append(string key, object value, string callerFilePath, string callerMemberName, 
            int callerLineNumber)
        {
            return Formatter(MinimumLevel, this, callerFilePath, callerMemberName, callerLineNumber)
                .Append(key, value);
        }
        
        /// <inheritdoc />
        public ILogFormatter Append<T>(string key, T[] values, string callerFilePath, string callerMemberName, 
            int callerLineNumber)
        {
            return Formatter(MinimumLevel, this, callerFilePath, callerMemberName, callerLineNumber)
                .Append(key, values);
        }
        
        /// <inheritdoc />
        public ILogFormatter Append<T>(string key, IEnumerable<T> values, string callerFilePath,
            string callerMemberName, int callerLineNumber)
        {
            return Formatter(MinimumLevel, this, callerFilePath, callerMemberName, callerLineNumber)
                .Append(key, values);
        }

        /// <inheritdoc />
        public void Debug(string message, string callerFilePath, string callerMemberName, int callerLineNumber)
        {
            Formatter(MinimumLevel, this, callerFilePath, callerMemberName, callerLineNumber)
                .Debug(message);
        }
        
        /// <inheritdoc />
        public void Info(string message, string callerFilePath, string callerMemberName, int callerLineNumber)
        {
            Formatter(MinimumLevel, this, callerFilePath, callerMemberName, callerLineNumber)
                .Info(message);
        }

        /// <inheritdoc />
        public void Warn(string message, string callerFilePath, string callerMemberName, int callerLineNumber)
        {
            Formatter(MinimumLevel, this, callerFilePath, callerMemberName, callerLineNumber)
                .Warn(message);
        }

        /// <inheritdoc />
        public void Error(string message, string callerFilePath, string callerMemberName, int callerLineNumber)
        {
            Formatter(MinimumLevel, this, callerFilePath, callerMemberName, callerLineNumber)
                .Error(message);
        }

        /// <inheritdoc />
        public void Error(Exception e, string callerFilePath, string callerMemberName, int callerLineNumber)
        {
            Formatter(MinimumLevel, this, callerFilePath, callerMemberName, callerLineNumber)
                .Error(e);
        }
        
        /// <inheritdoc />
        public void Error(string message, Exception e, string callerFilePath, string callerMemberName, 
            int callerLineNumber)
        {
            Formatter(MinimumLevel, this, callerFilePath, callerMemberName, callerLineNumber)
                .Error(message, e);
        }

        /// <inheritdoc />
        public void Write(string log)
        {
            for (var sinkIndex = 0; sinkIndex < Sinks.Count; sinkIndex++)
            {
                Sinks[sinkIndex].Write(log);
            }
        }
    }
}
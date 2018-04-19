using System;
using System.Collections.Generic;
using Anlog.Appenders.Default;
using Anlog.Entries;
using Anlog.Formatters.CompactKeyValue;

namespace Anlog.Loggers
{
    /// <summary>
    /// Default logger.
    /// </summary>
    public sealed class DefaultLogger : ILogger
    {
        /// <inheritdoc />
        public List<ILogSink> Sinks { get; set; } = new List<ILogSink>();

        /// <inheritdoc />
        public LogLevel? MinimumLevel
        {
            get => minimumLevel;
            set
            {
                if (value == null)
                {
                    return;
                }
                
                minimumLevel = value.Value;
                foreach (var sink in Sinks)
                {
                    if (!sink.MinimumLevel.HasValue)
                    {
                        sink.MinimumLevel = minimumLevel;
                    }
                }
            }
        }

        private LogLevel minimumLevel = LogLevel.Info;

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
        public ILogAppender Append(string key, string value, string callerFilePath = null, 
            string callerMemberName = null, int callerLineNumber = 0)
        {
            return new DefaultLogAppender(this, false, callerFilePath, callerMemberName, callerLineNumber)
                .Append(key, value);
        }

        /// <inheritdoc />
        public ILogAppender Append(string key, object value, string callerFilePath = null, 
            string callerMemberName = null, int callerLineNumber = 0)
        {
            return new DefaultLogAppender(this, false, callerFilePath, callerMemberName, callerLineNumber)
                .Append(key, value);
        }

        /// <inheritdoc />
        public void Debug(string message, string callerFilePath = null,  string callerMemberName = null,
            int callerLineNumber = 0)
        {
            new DefaultLogAppender(this, false, callerFilePath, callerMemberName, callerLineNumber)
                .Debug(message);
        }
        
        /// <inheritdoc />
        public void Info(string message, string callerFilePath = null,  string callerMemberName = null,
            int callerLineNumber = 0)
        {
            new DefaultLogAppender(this, false, callerFilePath, callerMemberName, callerLineNumber)
                .Info(message);
        }

        /// <inheritdoc />
        public void Warn(string message, string callerFilePath = null,  string callerMemberName = null,
            int callerLineNumber = 0)
        {
            new DefaultLogAppender(this, false, callerFilePath, callerMemberName, callerLineNumber)
                .Warn(message);
        }

        /// <inheritdoc />
        public void Error(string message, string callerFilePath = null,  string callerMemberName = null,
            int callerLineNumber = 0)
        {
            new DefaultLogAppender(this, false, callerFilePath, callerMemberName, callerLineNumber)
                .Error(message);
        }
        
        /// <inheritdoc />
        public void Error(Exception e, string message, string callerFilePath = null,  string callerMemberName = null,
            int callerLineNumber = 0)
        {
            new DefaultLogAppender(this, false, callerFilePath, callerMemberName, callerLineNumber)
                .Error(e, message);
        }

        /// <inheritdoc />
        public void Write(LogLevelName level, List<ILogEntry> entries)
        {
            for (var sinkIndex = 0; sinkIndex < Sinks.Count; sinkIndex++)
            {
                Sinks[sinkIndex].Write(level, entries);
            }
        }
    }
}
using System.Collections.Generic;

namespace Anlog.Loggers
{
    /// <summary>
    /// Default logger.
    /// </summary>
    public class DefaultLogger : ILogger
    {
        /// <inheritdoc />
        public LogFormatter Formatter { get; set; }
        
        /// <inheritdoc />
        public List<ILogSink> Sinks { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="DefaultLogger"/>.
        /// </summary>
        public DefaultLogger()
        {
            Sinks = new List<ILogSink>();
        }

        /// <inheritdoc />
        public ILogFormatter Append(string key, string value, string callerFilePath, string callerMemberName, 
            int callerLineNumber)
        {
            return Formatter(this, callerFilePath, callerMemberName, callerLineNumber)
                .Append(key, value);
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
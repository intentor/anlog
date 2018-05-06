using System;
using System.Text;
using Anlog.Factories;
using Anlog.Formatters.CompactKeyValue;
using Anlog.Renderers;
using static Anlog.Sinks.DefaultSinkOptions;

namespace Anlog.Sinks.RollingFile
{
    /// <summary>
    /// Factory for the Rolling File Sink.
    /// </summary>
    public static class RollingFileSinkFactory
    {
        /// <summary>
        /// Writes the output to files by period and size limit.
        /// </summary>
        /// <param name="sinksFactory">Sinks factory.</param>
        /// <param name="logFilePath">Log file path.</param>
        /// <param name="async">True if write to the console should be asynchronous, otherwise false.
        /// The default is false.</param>
        /// <param name="encoding">File encoding. The default is UTF8.</param>
        /// <param name="bufferSize">Buffer size to be used. The default is 4096.</param>
        /// <param name="minimumLevel">Minimum log level. The default is the logger minimum level.</param>
        /// <param name="formatter">Log formatter to be used. The default is
        /// <see cref="CompactKeyValueFormatter"/>.</param>
        /// <returns>Logger factory.</returns>
        public static LoggerFactory RollingFile(this LogSinksFactory sinksFactory, string logFilePath = null, 
            bool async = false, Encoding encoding = null, int bufferSize = 4096, LogLevel? minimumLevel = null, 
            ILogFormatter formatter = null)
        {
            return sinksFactory.Factory;
        }
    }
}
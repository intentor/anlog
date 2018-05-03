using System;
using System.IO;
using System.Text;
using Anlog.Factories;
using Anlog.Formatters.CompactKeyValue;
using Anlog.Renderers;

namespace Anlog.Sinks.SingleFile
{
    /// <summary>
    /// Factory for the Single File Sink.
    /// </summary>
    public static class SingleFileSinkFactory
    {
        /// <summary>
        /// Default log file path.
        /// </summary>
        public static readonly string DefaultLogFilePath = Path.Combine(Directory.GetCurrentDirectory(), "log.txt");
        
        /// <summary>
        /// Writes the output to a text writer.
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
        public static LoggerFactory SingleFile(this LogSinksFactory sinksFactory, string logFilePath = null, 
            bool async = false, Encoding encoding = null, int bufferSize = 4096, LogLevel? minimumLevel = null, 
            ILogFormatter formatter = null)
        {
            if (string.IsNullOrEmpty(logFilePath))
            {
                logFilePath = DefaultLogFilePath;
            }
            formatter = formatter ?? new CompactKeyValueFormatter();
            Func<IDataRenderer> renderer = () => new DefaultDataRenderer();

            var sink = async
                ? (ILogSink) new AsyncSingleFileSync(formatter, renderer, logFilePath, encoding, bufferSize)
                : new FileSink(formatter, renderer, logFilePath, encoding, bufferSize);
            sink.MinimumLevel = minimumLevel;
            
            sinksFactory.Sinks.Add(sink);
            return sinksFactory.Factory;
        }
    }
 }
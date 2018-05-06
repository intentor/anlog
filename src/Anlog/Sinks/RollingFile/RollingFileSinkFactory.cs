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
        /// <param name="logFileFolder">Log file path.</param>
        /// <param name="async">True if write to the console should be asynchronous, otherwise false.
        /// The default is false.</param>
        /// <param name="period">Period for rolling the files. The default is <see cref="RollingFilePeriod.Day"/>.</param>
        /// <param name="encoding">File encoding. The default is UTF8.</param>
        /// <param name="bufferSize">Buffer size to be used. The default is 4096.</param>
        /// <param name="minimumLevel">Minimum log level. The default is the logger minimum level.</param>
        /// <param name="formatter">Log formatter to be used. The default is
        /// <see cref="CompactKeyValueFormatter"/>.</param>
        /// <returns>Logger factory.</returns>
        public static LoggerFactory RollingFile(this LogSinksFactory sinksFactory, string logFileFolder = null, 
            bool async = false, RollingFilePeriod period = null, Encoding encoding = null, int bufferSize = 4096, 
            LogLevel? minimumLevel = null, 
            ILogFormatter formatter = null)
        {
            if (string.IsNullOrEmpty(logFileFolder))
            {
                logFileFolder = DefaultLogFolderPath;
            }

            if (period == null)
            {
                period = RollingFilePeriod.Day;
            }
            
            formatter = formatter ?? new CompactKeyValueFormatter();
            Func<IDataRenderer> renderer = () => new DefaultDataRenderer();
            var namer = new RollingFileNamer(logFileFolder, period);

            var sink = new RollingFileSink(formatter, renderer, namer, encoding, bufferSize)
            {
                MinimumLevel = minimumLevel
            };
            
            sinksFactory.Sinks.Add(sink);
            return sinksFactory.Factory;
        }
    }
}
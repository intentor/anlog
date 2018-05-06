using System;
using System.Collections.Generic;
using System.Text;
using Anlog.Entries;

namespace Anlog.Sinks.RollingFile
{
    /// <summary>
    /// Writes the output to files by period and size limit.
    /// <para/>
    /// Files are created using the format log-{date}-{sequence}.txt (log-YYYYMMDD-0001.txt).
    /// </summary>
    public class RollingFileSink : ILogSink, IDisposable
    {
        /// <inheritdoc />
        public LogLevel? MinimumLevel { get; set; }

        /// <inheritdoc />
        public ILogFormatter Formatter { get; }

        /// <summary>
        /// Maximum size of a file before creating a new one.
        /// </summary>
        private int maxSize;

        /// <summary>
        /// Renderer factory method.
        /// </summary>
        private Func<IDataRenderer> renderer;

        /// <summary>
        /// Internal file sink.
        /// </summary>
        private FileSink sink;

        /// <summary>
        /// Initializes a new instance of <see cref="RollingFileSink"/>.
        /// </summary>
        /// <param name="formatter">Log formatter.</param>
        /// <param name="renderer">Renderer factory method.</param>
        /// <param name="logFilePath">Log file path.</param>
        /// <param name="maxSize">Maximum size of a file before creating a new one.</param>
        /// <param name="encoding">File encoding. The default is UTF8.</param>
        /// <param name="bufferSize">Buffer size to be used. The default is 4096.</param>
        public RollingFileSink(ILogFormatter formatter, Func<IDataRenderer> renderer, string logFilePath,
            int maxSize = int.MaxValue, Encoding encoding = null,
            int bufferSize = 4096)
        {
            Formatter = formatter;
            this.maxSize = maxSize;
            this.renderer = renderer;

            sink = new FileSink(Formatter, renderer, logFilePath, encoding, bufferSize);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            sink.Dispose();
        }

        /// <inheritdoc />
        public void Write(LogLevelName level, List<ILogEntry> entries)
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Anlog.Entries;

namespace Anlog.Sinks
{
    /// <summary>
    /// Writes data to a file.
    /// </summary>
    public sealed class FileSink : ILogSink, IDisposable
    {
        /// <inheritdoc />
        public LogLevel? MinimumLevel { get; set; }
        
        /// <inheritdoc />
        public ILogFormatter Formatter { get; }

        /// <summary>
        /// Renderer factory method.
        /// </summary>
        private Func<IDataRenderer> renderer;
        
        /// <summary>
        /// Internal output stream.
        /// </summary>
        private Stream outputStream;

        /// <summary>
        /// Text writer.
        /// </summary>
        private TextWriter writer;

        /// <summary>
        /// Allows blocking of actions in a thread.
        /// </summary>
        private readonly object locker = new object();
        
        /// <summary>
        /// Initializes a new instance of <see cref="FileSink"/>.
        /// </summary>
        /// <param name="formatter">Log formatter.</param>
        /// <param name="renderer">Renderer factory method.</param>
        /// <param name="logFilePath">Log file path.</param>
        /// <param name="encoding">File encoding. The default is UTF8.</param>
        /// <param name="bufferSize">Buffer size to be used. The default is 4096.</param>
        public FileSink(ILogFormatter formatter, Func<IDataRenderer> renderer, string logFilePath, 
            Encoding encoding = null, int bufferSize = 4096)
        {
            Formatter = formatter;
            this.renderer = renderer;
            
            var directory = Path.GetDirectoryName(logFilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            
            outputStream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.Read, bufferSize);
            writer = new StreamWriter(outputStream, encoding ?? new UTF8Encoding());
        }

        /// <inheritdoc />
        public void Dispose()
        {
            writer.Dispose();
            outputStream.Dispose();
        }

        /// <inheritdoc />
        public void Write(LogLevelName level, List<ILogEntry> entries)
        {
            if (MinimumLevel.HasValue && MinimumLevel > level.Level)
            {
                return;
            }
            
            lock (locker)
            {
                writer.WriteLine(Formatter.Format(level, entries, renderer()));
                writer.Flush();
            }
        }
    }
}
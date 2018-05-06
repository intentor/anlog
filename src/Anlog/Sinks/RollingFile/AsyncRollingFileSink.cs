using System;
using System.Collections.Generic;
using System.Text;
using Anlog.Entries;

namespace Anlog.Sinks.RollingFile
{
    /// <summary>
    /// Writes the output to files by period and size limit.
    /// <para/>
    /// Please refer to <see cref="RollingFileSink"/> for more details.
    /// </summary>
    public class AsyncRollingFileSink : ILogSink, IDisposable
    {
        /// <inheritdoc />
        public LogLevel? MinimumLevel
        {
            get => sink.MinimumLevel;
            set => sink.MinimumLevel = value;
        }

        /// <inheritdoc />
        public ILogFormatter Formatter => sink.Formatter;
        
        /// <summary>
        /// Internal file sink.
        /// </summary>
        private RollingFileSink sink;
        
        /// <summary>
        /// Async writer.
        /// </summary>
        private AsyncWriter asyncWriter;

        /// <summary>
        /// Initializes a new instance of <see cref="AsyncRollingFileSink"/>.
        /// </summary>
        /// <param name="formatter">Log formatter.</param>
        /// <param name="renderer">Renderer factory method.</param>
        /// <param name="namer">Rolling filer namer.</param>
        /// <param name="encoding">File encoding. The default is UTF8.</param>
        /// <param name="bufferSize">Buffer size to be used. The default is 4096.</param>
        public AsyncRollingFileSink(ILogFormatter formatter, Func<IDataRenderer> renderer, RollingFileNamer namer,
            Encoding encoding = null, int bufferSize = 4096)
        {
            sink = new RollingFileSink(Formatter, renderer, namer, encoding, bufferSize);
            
            asyncWriter = new AsyncWriter((level, entries) =>
            {
                sink.Write(level, entries);
            });
            asyncWriter.Start();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            asyncWriter.Dispose();
            sink.Dispose();
        }
        
        /// <inheritdoc />
        public void Write(LogLevelName level, List<ILogEntry> entries)
        { 
            if (MinimumLevel.HasValue && MinimumLevel > level.Level)
            {
                return;
            }
            
            asyncWriter.Enqueue(level, entries);  
        }
    }
}
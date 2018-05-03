using System;
using System.Collections.Generic;
using System.Text;
using Anlog.Entries;
using static Anlog.Formatters.DefaultFormattingOptions;

namespace Anlog.Sinks.SingleFile
{
    /// <summary>
    /// Writes output to a single file asynchronously.
    /// <para/>
    /// The last log(s) may be lost it the program crashes or stops before the write task is executed.
    /// </summary>
    public class AsyncSingleFileSync : ILogSink, IDisposable
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
        /// Internal file sink.
        /// </summary>
        private FileSink sink;
        
        /// <summary>
        /// Async writer.
        /// </summary>
        private AsyncWriter asyncWriter;
        
        /// <summary>
        /// Initializes a new instance of <see cref="AsyncSingleFileSync"/>.
        /// </summary>
        /// <param name="formatter">Log formatter.</param>
        /// <param name="renderer">Renderer factory method.</param>
        /// <param name="logFilePath">Log file path.</param>
        /// <param name="encoding">File encoding. The default is UTF8.</param>
        /// <param name="bufferSize">Buffer size to be used. The default is 4096.</param>
        public AsyncSingleFileSync(ILogFormatter formatter, Func<IDataRenderer> renderer, string logFilePath, 
            Encoding encoding = null, int bufferSize = 4096)
        {
            Formatter = formatter;
            this.renderer = renderer;
            
            sink = new FileSink(Formatter, renderer, logFilePath, encoding, bufferSize);
            
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
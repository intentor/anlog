using System;
using System.Text;

namespace Anlog.Sinks.SingleFile
{
    /// <summary>
    /// Writes output to a single file asynchronously.
    /// <para/>
    /// The last log(s) may be lost it the program crashes or stops before the write task is executed.
    /// </summary>
    public class AsyncSingleFileSync : ILogSink, IDisposable
    {
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
        /// </summary>ØØ
        /// <param name="logFilePath">Log file path.</param>
        /// <param name="encoding">File encoding. The default is UTF8.</param>
        /// <param name="bufferSize">Buffer size to be used. The default is 4096.</param>
        public AsyncSingleFileSync(string logFilePath, Encoding encoding = null, int bufferSize = 4096)
        {
            sink = new FileSink(logFilePath, encoding, bufferSize);
            
            asyncWriter = new AsyncWriter(log =>
            {
                sink.Write(log);
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
        public void Write(string log)
        { 
            asyncWriter.Enqueue(log);  
        }
    }
}
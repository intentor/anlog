using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Anlog.Sinks.SingleFile
{
    /// <summary>
    /// Writes output to a single file.
    /// <para/>
    /// If flushes contents when a maximum buffer is reached or a certain buffer time is reached.
    /// </summary>
    public sealed class SingleFileSink : ILogSink, IDisposable
    {
        /// <summary>
        /// Log queue.
        /// </summary>
        private ConcurrentQueue<string> queue;
        
        /// <summary>
        /// Internal output stream.
        /// </summary>
        private Stream outputStream;

        /// <summary>
        /// Text writer.
        /// </summary>
        private TextWriter writer;
        
        /// <summary>
        /// Initializes a new instance of <see cref="SingleFileSink"/>.
        /// </summary>
        /// <param name="logFilePath">Log file path.</param>
        /// <param name="encoding">File encoding. The default is UTF8.</param>
        /// <param name="bufferSize">Buffer size to be used. The default is 4096.</param>
        /// <param name="bufferTimer">Timer to perform a flush (milisseconds). The default is 1000 ms.</param>
        public SingleFileSink(string logFilePath, Encoding encoding = null, int bufferSize = 4096)
        {
            queue = new ConcurrentQueue<string>();
            
            var directory = Path.GetDirectoryName(logFilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            
            outputStream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.Read, bufferSize);
            writer = new StreamWriter(outputStream, encoding ?? new UTF8Encoding());

            Task.Run(() => Writer());
        }

        /// <inheritdoc />
        public void Dispose()
        {
            writer.Dispose();
            outputStream.Dispose();
        }
        
        /// <inheritdoc />
        public void Write(string log)
        {
            queue.Enqueue(log);
        }

        /// <summary>
        /// Writer task executor.
        /// </summary>
        private void Writer()
        {
            while (true)
            {
                if (queue.Count > 0)
                {
                    while (queue.TryDequeue(out string value))
                    {
                        writer.WriteLine(value);
                    }
                    
                    writer.Flush();
                }
            }
        }
    }
}
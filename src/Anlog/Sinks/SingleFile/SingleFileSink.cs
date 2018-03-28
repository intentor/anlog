using System;
using System.Diagnostics;
using System.IO;
using System.Text;

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
        /// Timer to perform a flush (milisseconds).
        /// </summary>
        private int bufferTimer;
    
        /// <summary>
        /// Stopwatch to count time for flusing.
        /// </summary>
        private Stopwatch stopwatch;
        
        /// <summary>
        /// Internal output stream.
        /// </summary>
        private Stream outputStream;

        /// <summary>
        /// Text writer.
        /// </summary>
        private TextWriter writer;
        
        /// <summary>
        /// Object used to perform thread lock.
        /// </summary>
        private object locker;
        
        /// <summary>
        /// Initializes a new instance of <see cref="SingleFileSink"/>.
        /// </summary>
        /// <param name="logFilePath">Log file path.</param>
        /// <param name="encoding">File encoding. The default is UTF8.</param>
        /// <param name="bufferSize">Buffer size to be used. The default is 4096.</param>
        /// <param name="bufferTimer">Timer to perform a flush (milisseconds). The default is 1000 ms.</param>
        public SingleFileSink(string logFilePath, Encoding encoding = null, int bufferSize = 4096, 
            int bufferTimer = 1000)
        {
            this.bufferTimer = bufferTimer;
            locker = new object();
            
            var directory = Path.GetDirectoryName(logFilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            
            outputStream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.Read, bufferSize);
            writer = new StreamWriter(outputStream, encoding ?? new UTF8Encoding());
            
            stopwatch = Stopwatch.StartNew();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            lock (locker)
            {
                writer.Dispose();
                outputStream.Dispose();
            }
        }
        
        /// <inheritdoc />
        public void Write(string log)
        {
            lock (locker)
            {
                writer.WriteLine(log);

                if (stopwatch.ElapsedMilliseconds > bufferTimer)
                {
                    writer.Flush();
                    stopwatch.Reset();
                }
            }
        }
    }
}
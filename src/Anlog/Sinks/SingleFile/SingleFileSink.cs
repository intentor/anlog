using System;
using System.IO;
using System.Text;

namespace Anlog.Sinks.SingleFile
{
    /// <summary>
    /// Writes output to a single file.
    /// </summary>
    public sealed class SingleFileSink : ILogSink, IDisposable
    {
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
        /// </summary>ØØ
        /// <param name="logFilePath">Log file path.</param>
        /// <param name="encoding">File encoding. The default is UTF8.</param>
        /// <param name="bufferSize">Buffer size to be used. The default is 4096.</param>
        public SingleFileSink(string logFilePath, Encoding encoding = null, int bufferSize = 4096)
        {
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
        public void Write(string log)
        { 
            writer.WriteLine(log);
            writer.Flush();
        }
    }
}
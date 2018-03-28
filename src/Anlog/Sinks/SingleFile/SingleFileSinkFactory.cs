using System.IO;
using System.Text;
using Anlog.Factories;

namespace Anlog.Sinks.SingleFile
{
    /// <summary>
    /// Factory for the <see cref="SingleFileSink"/>.
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
        /// <param name="encoding">File encoding. The default is UTF8.</param>
        /// <param name="bufferSize">Buffer size to be used. The default is 4096.</param>
        /// <param name="bufferTimer">Timer to perform a flush (milisseconds). The default is 1000 ms.</param>
        /// <returns>Logger factory.</returns>
        public static LoggerFactory SingleFile(this LogSinksFactory sinksFactory, 
            string logFilePath = null, Encoding encoding = null, int bufferSize = 4096)
        {
            if (string.IsNullOrEmpty(logFilePath))
            {
                logFilePath = DefaultLogFilePath;
            }
            
            sinksFactory.Sinks.Add(new SingleFileSink(logFilePath, encoding, bufferSize));
            return sinksFactory.Factory;
        }
    }
 }
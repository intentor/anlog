using System;

namespace Anlog.Sinks.Console
{
    /// <summary>
    /// Writes output to the console asynchronously.
    /// <para/>
    /// The last log(s) may be lost it the program crashes or stops before the write task is executed.
    /// </summary>
    public sealed class AsyncConsoleSink : ILogSink, IDisposable
    {
        /// <summary>
        /// Log async writer.
        /// </summary>
        private AsyncWriter asyncWriter;
        
        /// <summary>
        /// Initializes a new instance of <see cref="AsyncConsoleSink"/>.
        /// </summary>
        public AsyncConsoleSink()
        {
            asyncWriter = new AsyncWriter(System.Console.WriteLine);
            asyncWriter.Start();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            asyncWriter.Dispose();
        }
        
        /// <inheritdoc />
        public void Write(string log)
        { 
            asyncWriter.Enqueue(log);
        }
    }
}
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Anlog.Sinks
{
    /// <summary>
    /// Creates an asynchronous writer.
    /// </summary>
    public class AsyncWriter : IDisposable
    {
        /// <summary>
        /// Log queue.
        /// </summary>
        private ConcurrentQueue<string> queue;

        /// <summary>
        /// Allows blocking of actions in a thread.
        /// </summary>
        private readonly object locker = new object();

        private Action<string> writer;
        
        /// <summary>
        /// Initializes a new instance of <see cref="AsyncWriter"/>.
        /// </summary>
        /// <param name="writer">Writer function.</param>
        public AsyncWriter(Action<string> writer)
        {
            this.writer = writer;
            queue = new ConcurrentQueue<string>();

            Task.Run(() => WriterTask());
        }

        /// <inheritdoc />
        public void Dispose()
        {
            WriteQueue();
        }
        
        /// <summary>
        /// Enqueue the log.
        /// </summary>
        /// <param name="log">Log to write.</param>
        public void Enqueue(string log)
        { 
            queue.Enqueue(log);
        }

        /// <summary>
        /// Writer task executor.
        /// </summary>
        private void WriterTask()
        {
            while (true)
            {
                WriteQueue();
            }
        }

        /// <summary>
        /// If there's any log in the queue, writes to the writer.
        /// </summary>
        private void WriteQueue()
        {
            if (queue.Count > 0)
            {
                lock (locker)
                {
                    while (queue.TryDequeue(out string value))
                    {
                        writer(value);
                    }
                }
            }
        }
    }
}
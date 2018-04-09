using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Threading;

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
        /// Writing action.
        /// </summary>
        private Action<string> writer;

        /// <summary>
        /// Background worker thread.
        /// </summary>
        private BackgroundWorker worker;
        
        /// <summary>
        /// Initializes a new instance of <see cref="AsyncWriter"/>.
        /// </summary>
        /// <param name="writer">Writer function.</param>
        public AsyncWriter(Action<string> writer)
        {
            this.writer = writer;
            queue = new ConcurrentQueue<string>();
            worker = new BackgroundWorker()
            {
                WorkerSupportsCancellation = true
            };
        }

        /// <summary>
        /// Starts the writer.
        /// </summary>
        public void Start()
        {
            worker.DoWork += (sender, args) =>
            {
                while (!worker.CancellationPending)
                {
                    WriteQueue();
                    Thread.Sleep(1);
                }
            };
            worker.RunWorkerAsync();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            worker.CancelAsync();
            worker.Dispose();
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
        /// If there's any log in the queue, writes to the writer.
        /// </summary>
        private void WriteQueue()
        {
            if (queue.Count > 0)
            {
                while (queue.TryDequeue(out string value))
                {
                    writer(value);
                }
            }
        }
    }
}
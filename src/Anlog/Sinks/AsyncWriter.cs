using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using Anlog.Entries;

namespace Anlog.Sinks
{
    /// <summary>
    /// Creates an asynchronous writer.
    /// </summary>
    public class AsyncWriter : IDisposable
    {
        /// <summary>
        /// Data to write to the log.
        /// </summary>
        public class WriterData
        {
            /// <summary>
            /// Log level name.
            /// </summary>
            public LogLevelName LevelName { get; set; }
            
            /// <summary>
            /// Log entries.
            /// </summary>
            public List<ILogEntry> Entries { get; set; }
        }
        
        /// <summary>
        /// Log queue.
        /// </summary>
        private readonly ConcurrentQueue<WriterData> queue;

        /// <summary>
        /// Writing action.
        /// </summary>
        private readonly Action<LogLevelName, List<ILogEntry>> writer;

        /// <summary>
        /// Background worker thread.
        /// </summary>
        private readonly BackgroundWorker worker;
        
        /// <summary>
        /// Initializes a new instance of <see cref="AsyncWriter"/>.
        /// </summary>
        /// <param name="writer">Writer function.</param>
        public AsyncWriter(Action<LogLevelName, List<ILogEntry>> writer)
        {
            this.writer = writer;
            queue = new ConcurrentQueue<WriterData>();
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
            WriteQueue();
            worker.CancelAsync();
            worker.Dispose();
            
            // Wait to ensure writing.
            Thread.Sleep(50);
        }
        
        /// <summary>
        /// Enqueue the log.
        /// </summary>
        /// <param name="logLevelName">Log level.</param>
        /// <param name="entries">Log entries.</param>
        public void Enqueue(LogLevelName logLevelName, List<ILogEntry> entries)
        { 
            queue.Enqueue(new WriterData()
            {
                LevelName = logLevelName,
                Entries = entries
            });
        }

        /// <summary>
        /// If there's any log in the queue, writes to the writer.
        /// </summary>
        private void WriteQueue()
        {
            if (queue.Count > 0)
            {
                while (queue.TryDequeue(out WriterData data))
                {
                    writer(data.LevelName, data.Entries);
                }
            }
        }
    }
}
using System;
using System.Text;

namespace Anlog.Sinks.InMemory
{
    /// <summary>
    /// Writes the log output to an in-memory variable until the user asks the log.
    /// <para />
    /// It's only recommended for test and debugging purposes.
    /// </summary>
    public class InMemorySink : ILogSink
    {
        /// <summary>
        /// Log buffer.
        /// </summary>
        private StringBuilder buffer;

        /// <summary>
        /// Initializes a new instance of <see cref="InMemorySink"/>.
        /// </summary>
        public InMemorySink()
        {
            buffer = new StringBuilder();
        }
        
        /// <inheritdoc />
        public void Write(string log)
        {
            buffer.Append(log);
            buffer.Append(Environment.NewLine);
        }

        /// <summary>
        /// Gets the in-memory logs and resets the internal buffer.
        /// </summary>
        /// <returns>Logs values.</returns>
        public string GetLogs()
        {
            var logs = buffer.ToString();
            buffer.Clear();

            return logs;
        }
    }
}
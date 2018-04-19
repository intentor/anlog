using System;

namespace Anlog.Appenders.Dummy
{
    /// <summary>
    /// Appends logs to nothing.
    /// </summary>
    public class DummyLogAppender : ILogAppender
    {
        /// <inheritdoc />
        public ILogAppender Append(string key, string value)
        {
            return this;
        }
        
        /// <inheritdoc />
        public ILogAppender Append(string key, object value)
        {
            return this;
        }

        /// <inheritdoc />
        public void Debug(string message = null)
        {
            // Does nothing.
        }

        /// <inheritdoc />
        public void Info(string message = null)
        {
            // Does nothing.
        }

        /// <inheritdoc />
        public void Warn(string message = null)
        {
            // Does nothing.
        }

        /// <inheritdoc />
        public void Error(string message = null)
        {
            // Does nothing.
        }

        /// <inheritdoc />
        public void Error(Exception e, string message = null)
        {
            // Does nothing.
        }
    }
}
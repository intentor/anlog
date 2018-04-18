using System;
using System.Collections.Generic;

namespace Anlog.Formatters.Dummy
{
    /// <summary>
    /// Dummy formatter that formats nothing.
    /// </summary>
    public sealed class DummyFormatter : ILogFormatter
    {
        /// <inheritdoc />
        public ILogFormatter Append(string key, string value)
        {
            return this;
        }
        
        /// <inheritdoc />
        public ILogFormatter Append(string key, object value)
        {
            return this;
        }

        /// <inheritdoc />
        public ILogFormatter Append<T>(T obj) where T : class
        {
            return this;
        }
        
        /// <inheritdoc />
        public ILogFormatter Append<T>(string key, T[] values)
        {
            return this;
        }
        
        /// <inheritdoc />
        public ILogFormatter Append<T>(string key, IEnumerable<T> values)
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
        public void Error(Exception e)
        {
            // Does nothing.
        }

        /// <inheritdoc />
        public void Error(string message, Exception e)
        {
            // Does nothing.
        }
    }
}
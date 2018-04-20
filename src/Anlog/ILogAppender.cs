using System;
using System.Collections.Generic;

namespace Anlog
{
    /// <summary>
    /// Allows appending of logs for writing.
    /// </summary>
    public interface ILogAppender
    {
        /// <summary>
        /// Appends an entry to the log.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <param name="value">Entry value.</param>
        /// <returns>Reference to this log appender.</returns>
        ILogAppender Append(string key, string value);
        
        /// <summary>
        /// Appends an entry to the log.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <param name="value">Entry value.</param>
        /// <returns>Reference to this log appender.</returns>
        ILogAppender Append(string key, object value);

        /// <summary>
        /// Writes the log as debug.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="values">Format values for the message.</param>
        void Debug(string message = null, params object[] values);
        /// <summary>
        /// Writes the log as information.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="values">Format values for the message.</param>
        void Info(string message = null, params object[] values);

        /// <summary>
        /// Writes the log as warning.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="values">Format values for the message.</param>
        void Warn(string message = null, params object[] values);

        /// <summary>
        /// Writes the log as error.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="values">Format values for the message.</param>
        void Error(string message = null, params object[] values);

        /// <summary>
        /// Writes the log as error.
        /// </summary>
        /// <param name="e">Exception details.</param>
        /// <param name="message">Log message.</param>
        /// <param name="values">Format values for the message.</param>
        void Error(Exception e, string message = null, params object[] values);
    }
}
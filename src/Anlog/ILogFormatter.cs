using System;
using System.Collections.Generic;

namespace Anlog
{
    /// <summary>
    /// Provides formatting to a log entry.
    /// </summary>
    public interface ILogFormatter
    {
        /// <summary>
        /// Appends an entry to the log.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <param name="value">Entry value.</param>
        /// <returns>Reference to this log writer.</returns>
        ILogFormatter Append(string key, string value);
        
        /// <summary>
        /// Appends an entry to the log.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <param name="value">Entry value.</param>
        /// <returns>Reference to this log writer.</returns>
        ILogFormatter Append(string key, object value);
        
        /// <summary>
        /// Appends an object entry to the log.
        /// <para />
        /// The object fields and/or properties will be added as key value pairs to the log.
        /// </summary>
        /// <param name="obj">Entry value.</param>
        /// <returns>Reference to this log writer.</returns>
        ILogFormatter Append<T>(T obj) where T : class;
        
        /// <summary>
        /// Appends an entry to the log.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <param name="values">Entry values.</param>
        /// <typeparam name="T">Object type.</typeparam>
        /// <returns>Reference to this log writer.</returns>
        ILogFormatter Append<T>(string key, T[] values);
        
        /// <summary>
        /// Appends an entry to the log.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <param name="values">Entry values.</param>
        /// <typeparam name="T">Object type.</typeparam>
        /// <returns>Reference to this log writer.</returns>
        ILogFormatter Append<T>(string key, IEnumerable<T> values);

        /// <summary>
        /// Writes the log as debug.
        /// </summary>
        /// <param name="message">Log message.</param>
        void Debug(string message = null);
        /// <summary>
        /// Writes the log as information.
        /// </summary>
        /// <param name="message">Log message.</param>
        void Info(string message = null);

        /// <summary>
        /// Writes the log as warning.
        /// </summary>
        /// <param name="message">Log message.</param>
        void Warning(string message = null);

        /// <summary>
        /// Writes the log as error.
        /// </summary>
        /// <param name="message">Log message.</param>
        void Error(string message = null);

        /// <summary>
        /// Writes the log as error.
        /// </summary>
        /// <param name="e">Exception details.</param>
        void Error(Exception e);
        
        /// <summary>
        /// Writes the log as error.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="e">Exception details.</param>
        void Error(string message, Exception e);
    }
}
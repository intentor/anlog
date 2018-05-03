using System;

namespace Anlog.Entries
{
    /// <summary>
    /// Represents an exception details entry.
    /// </summary>
    public class LogException : ILogEntry
    {
        /// <summary>
        /// Key value. It's always null for exceptions.
        /// </summary>
        public string Key { get; set; }
        
        /// <summary>
        /// Exception details.
        /// </summary>
        public string Details { get; set; }
        
        /// <summary>
        /// Initializes a new instance of <see cref="LogException"/>.
        /// </summary>
        /// <param name="e">Exception to be logged.</param>
        public LogException(Exception e)
        {
            Key = null;
            Details = string.Concat(e.ToString(),
                !string.IsNullOrEmpty(e.StackTrace) ? Environment.NewLine + e.StackTrace : string.Empty);
        }
    }
}
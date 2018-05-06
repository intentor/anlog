using System.IO;

namespace Anlog.Sinks
{
    /// <summary>
    /// Default options for sinks.
    /// </summary>
    public static class DefaultSinkOptions
    {
        /// <summary>
        /// Default log file path.
        /// </summary>
        public static readonly string DefaultLogFilePath = Directory.GetCurrentDirectory();
    }
}
using System.IO;

namespace Anlog.Sinks
{
    /// <summary>
    /// Default options for sinks.
    /// </summary>
    public static class DefaultSinkOptions
    {
        /// <summary>
        /// Default log files folder path.
        /// </summary>
        public static readonly string DefaultLogFolderPath = Directory.GetCurrentDirectory();

        /// <summary>
        /// Default log file name.
        /// </summary>
        public const string DefaultLogFileName = "log.txt";
    }
}
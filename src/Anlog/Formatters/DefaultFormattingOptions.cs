using System.Globalization;

namespace Anlog.Formatters
{
    /// <summary>
    /// Default formatting options.
    /// </summary>
    public static class DefaultFormattingOptions
    {
        /// <summary>
        /// Date/time format.
        /// </summary>
        public static string DateTimeFormat { get; set; } = "yyyy-MM-dd HH:mm:ss.fff";
        
        /// <summary>
        /// Culture details.
        /// </summary>
        public static CultureInfo Culture { get; set; } = CultureInfo.InvariantCulture;

        /// <summary>
        /// Log levels details.
        /// </summary>
        public static readonly ILogLevelName LogLevelNames = new DefaultLogLevelName();
    }
}
using System.Globalization;
using Anlog.Formatters.LogLevelNames;

namespace Anlog.Formatters
{
    /// <summary>
    /// Default options for formatters.
    /// </summary>
    public static class DefaultFormattingOptions
    {
        /// <summary>
        /// Date/time format.
        /// </summary>
        public static string DefaultDateTimeFormat { get; set; } = "yyyy-MM-dd HH:mm:ss.fff";
        
        /// <summary>
        /// DefaultCulture details.
        /// </summary>
        public static CultureInfo DefaultCulture { get; set; } = CultureInfo.InvariantCulture;

        /// <summary>
        /// Log levels details.
        /// </summary>
        public static readonly ILogLevelName DefaultLogLevelName = new ThreeLetterLogLevelName();
    }
}
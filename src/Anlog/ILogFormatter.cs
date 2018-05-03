using System.Collections.Generic;
using Anlog.Entries;

namespace Anlog
{
    /// <summary>
    /// Performs formatting of a log.
    /// <para/>
    /// Formatters should be stateless and rely on received renderers for log rendering.
    /// </summary>
    public interface ILogFormatter
    {
        /// <summary>
        /// Formats a log.
        /// </summary>
        /// <param name="level">Log level for the log.</param>
        /// <param name="entries">Entries to format.</param>
        /// <param name="renderer">Renderer for the log.</param>
        /// <returns>Formatted log.</returns>
        string Format(LogLevelName level, List<ILogEntry> entries, IDataRenderer renderer);
    }
}
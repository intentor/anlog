using System.Collections.Specialized;
using Anlog.Formatters.CompactKeyValue;
using Anlog.Sinks.Console.Themes;

namespace Anlog.Sinks.Console.Renderers
{
    /// <summary>
    /// Renders the console output from a compact key/value formatter.
    /// </summary>
    public sealed class CompactKeyValueRenderer : IConsoleRenderer
    {
        /// <summary>
        /// Code to reset color in the console.
        /// </summary>
        private const string ResetColor = "\x1b[0m";
        
        /// <inheritdoc />
        public IConsoleTheme Theme { get; private set; }

        /// <summary>
        /// Log levels names.
        /// </summary>
        private ILogLevelName levelsName = new CompactKeyValueLogLevelName();

        /// <summary>
        /// Initilizes a new instance of <see cref="CompactKeyValueRenderer"/>.
        /// </summary>
        /// <param name="theme">Theme to use for output.</param>
        public CompactKeyValueRenderer(IConsoleTheme theme)
        {
            Theme = theme;
        }

        /// <summary>
        /// Renders a log for writing.
        /// </summary>
        /// <param name="log"></param>
        public string Render(string log)
        {
            return log
                .Replace("[" + levelsName.Debug.Entry + "]", 
                    string.Concat(Theme.LevelDebugColor, "[", levelsName.Debug.Entry, "]", ResetColor))
                .Replace("[" + levelsName.Info.Entry + "]", 
                    string.Concat(Theme.LevelInfoColor, "[", levelsName.Info.Entry, "]", ResetColor))
                .Replace("[" + levelsName.Warn.Entry + "]", 
                    string.Concat(Theme.LevelWarnColor, "[", levelsName.Warn.Entry, "]", ResetColor))
                .Replace("[" + levelsName.Error.Entry + "]", 
                    string.Concat(Theme.LevelErrorColor, "[", levelsName.Error.Entry, "]", ResetColor));
        }
    }
}
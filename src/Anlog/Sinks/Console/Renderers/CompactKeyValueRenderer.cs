using Anlog.Sinks.Console.Themes;

namespace Anlog.Sinks.Console
{
    /// <summary>
    /// Renders the console output from a compact key/value formatter.
    /// </summary>
    public sealed class CompactKeyValueRenderer : IConsoleRenderer
    {
        /// <inheritdoc />
        public IConsoleTheme Theme { get; private set; }

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
            return log;
        }
    }
}
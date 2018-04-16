using Anlog.Sinks.Console.Themes;

namespace Anlog.Sinks.Console.Renderers
{
    /// <summary>
    /// Console renderer.
    /// </summary>
    public interface IConsoleRenderer
    {
        /// <summary>
        /// Theme used by the renderer.
        /// </summary>
        IConsoleTheme Theme { get; }

        /// <summary>
        /// Renders a log for writing.
        /// </summary>
        /// <param name="log">Log to render.</param>
        string Render(string log);
    }
}
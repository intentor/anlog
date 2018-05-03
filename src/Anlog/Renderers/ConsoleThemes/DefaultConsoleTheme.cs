namespace Anlog.Renderers.ConsoleThemes
{
    /// <summary>
    /// Default console theme.
    /// </summary>
    public sealed class DefaultConsoleTheme : IConsoleTheme
    {
        /// <inheritdoc />
        public string DefaultColor { get; } = "\x1b[38;5;250m";
        
        /// <inheritdoc />
        public string LevelDebugColor { get; } = "\x1b[38;5;014m";
        
        /// <inheritdoc />
        public string LevelInfoColor { get; } = "\x1b[38;5;035m";
        
        /// <inheritdoc />
        public string LevelWarnColor { get; } = "\x1b[38;5;228m";
        
        /// <inheritdoc />
        public string LevelErrorColor { get; } = "\x1b[38;5;196m";
        
        /// <inheritdoc />
        public string KeyColor { get; } = "\x1b[1m\x1b[38;5;038m";
        
        /// <inheritdoc />
        public string ValueColor { get; } = "\x1b[38;5;158m";
    }
}
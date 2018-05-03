namespace Anlog.Renderers.ConsoleThemes
{
    /// <summary>
    /// Theme for the console sink.
    /// </summary>
    public interface IConsoleTheme
    {
        /// <summary>
        /// Default color for texts.
        /// </summary>
        string DefaultColor { get; }
        
        /// <summary>
        /// Color for debug log data.
        /// </summary>
        string LevelDebugColor { get; }
        
        /// <summary>
        /// Color for information log data.
        /// </summary>
        string LevelInfoColor { get; }
        
        /// <summary>
        /// Color for warning log data.
        /// </summary>
        string LevelWarnColor { get; }
        
        /// <summary>
        /// Color for error log data.
        /// </summary>
        string LevelErrorColor { get; }
        
        /// <summary>
        /// Color for keys on key/value pairs.
        /// </summary>
        string KeyColor { get; }
        
        /// <summary>
        /// Color for values on key/value pairs.
        /// </summary>
        string ValueColor { get; }
        
        /// <summary>
        /// Color for exception details.
        /// </summary>
        string ExceptionColor { get; }
    }
}
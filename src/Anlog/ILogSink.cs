namespace Anlog
{
    /// <summary>
    /// Writes logs to an output.
    /// </summary>
    public interface ILogSink
    {
        /// <summary>
        /// Writes a line of log to an output.
        /// </summary>
        /// <param name="log">Log to write.</param>
        void Write(string log);
    }
}
namespace Anlog.Sinks.Console
{
    /// <summary>
    /// Writes output directly to the console.
    /// <para />
    /// It may incur in a little performance hit. If performance is an issue, please use <see cref="AsyncConsoleSink"/>.
    /// </summary>
    public sealed class ConsoleSink : ILogSink
    {
        /// <inheritdoc />
        public void Write(string log)
        {
            System.Console.WriteLine(log);
        }
    }
}
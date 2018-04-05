namespace Anlog.Sinks.Console
{
    /// <summary>
    /// Writes output directly to the console.
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
using Anlog.Factories;
using Anlog.Sinks.Console;
using Anlog.Sinks.RollingFile;

namespace Anlog.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creates the logger.
            Log.Logger = new LoggerFactory()
                .WriteTo.Console(async: true)
                .WriteTo.RollingFile(async: true)
                .CreateLogger();
            
            // Writes a log.
            Log.Append("key", "value").Info();
            
            // If possible, when the application ends, dispose the logger to ensure all logs are written.
            Log.Dispose();
        }
    }
}
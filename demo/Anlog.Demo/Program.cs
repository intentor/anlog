using Anlog.Factories;
using Anlog.Sinks.Console;

namespace Anlog.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creates the logger.
            Log.Logger = new LoggerFactory()
                .WriteTo.Console()
                .CreateLogger();
            
            // Writes a log.
            Log.Append("key", "value").Info();
        }
    }
}
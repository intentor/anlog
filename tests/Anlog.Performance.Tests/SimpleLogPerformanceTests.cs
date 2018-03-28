using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Anlog.Performance.Tests.TestObjects;
using Anlog.Tests.TestObjects;
using Xunit.Abstractions;
using static Anlog.Tests.TestObjects.TestConstants;

namespace Anlog.Performance.Tests
{
    /// <summary>
    /// A log test with simple logging details in different logging tools.
    /// </summary>
    public sealed class SimpleLogPerformanceTests : PerformanceTest
    {
        public SimpleLogPerformanceTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        protected override long TestAnlogLoggerWithCaller()
        {
            return MeasureExecution(() => { Log.Append(TestString.Key, TestString.Value).Info(); });
        }

        protected override long TestSerilogWithCaller(
            [CallerFilePath] string filePath = null,
            [CallerMemberName] string caller = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            return MeasureExecution(() =>
            {
                var callerName = "<unknown>";
                if (filePath != null)
                {
                    callerName = $"{Path.GetFileNameWithoutExtension(filePath)}.{caller}:{lineNumber}";
                }

                Serilog.Log.Information("c={caller},{key}={value}", callerName, TestString.Key, TestString.Value);
            });
        }

        protected override long TestSerilogWithoutCaller()
        {
            return MeasureExecution(() =>
            {
                Serilog.Log.Information("c=unknown,{key}={value}", TestString.Key, TestString.Value);
            });
        }

        protected override long TestSavingToFileWithCaller(
            [CallerFilePath] string filePath = null,
            [CallerMemberName] string caller = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            var path = tempFolder.GetLogFilePath("file.txt");
            var outputStream = File.Open(path, FileMode.Append, FileAccess.Write, FileShare.Read);
            var writer = new StreamWriter(outputStream, new UTF8Encoding());

            return MeasureExecution(() =>
            {
                var callerName = "<unknown>";
                if (filePath != null)
                {
                    callerName = string.Concat(Path.GetFileNameWithoutExtension(filePath), ".", caller, ":", 
                        lineNumber);
                }

                var text =
                    $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [INFO] c={callerName},{TestString.Key}={TestString.Value}";
                writer.Write(text);
            });
        }
    }
}
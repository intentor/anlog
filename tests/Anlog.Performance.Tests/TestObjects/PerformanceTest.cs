using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using Anlog.Factories;
using Anlog.Loggers;
using Anlog.Sinks.SingleFile;
using Anlog.Tests.TestObjects;
using Serilog;
using Serilog.Core;
using Xunit;
using Xunit.Abstractions;

namespace Anlog.Performance.Tests.TestObjects
{
    /// <summary>
    /// A base performance test.
    /// </summary>
    public abstract class PerformanceTest : IDisposable
    {
        /// <summary>
        /// Total number of performance test executions.
        /// </summary>
        protected const int TotalExecutions = 10000;

        /// <summary>
        /// Test output helper.
        /// </summary>
        private readonly ITestOutputHelper outputHelper;

        /// <summary>
        /// Logs saving folder.
        /// </summary>
        protected readonly TempFolder tempFolder = TempFolder.Create();

        public PerformanceTest(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;

            if (!(Serilog.Log.Logger is Logger))
            {
                Serilog.Log.Logger = new LoggerConfiguration()
                    .WriteTo.File(Path.Combine(tempFolder.GetLogFilePath("serilog.txt")))
                    .CreateLogger();
            }

            if (Log.Logger == null || Log.Logger is DummyLogger)
            {
                Log.Logger = new LoggerFactory()
                    .WriteTo.SingleFile(Path.Combine(tempFolder.GetLogFilePath("anlog.txt")), async: true)
                    .CreateLogger();
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            tempFolder.Dispose();
        }

        [Fact]
        public void ForDifferentLoggingSystems_TestPerformance()
        {
            var elapsed = new long[4];

            elapsed[0] = TestAnlogLoggerWithCaller();
            Thread.Sleep(250);
            
            elapsed[1] = TestSerilogWithCaller();
            Thread.Sleep(250);
            
            elapsed[2] = TestSerilogWithoutCaller();
            Thread.Sleep(250);
            
            elapsed[3] = TestSavingToFileWithCaller();
            Thread.Sleep(250);

            outputHelper.WriteLine($"Intentor            {elapsed[0]} ms");
            outputHelper.WriteLine($"Serilog with caller {elapsed[1]} ms");
            outputHelper.WriteLine($"Serilog no caller   {elapsed[2]} ms");
            outputHelper.WriteLine($"Write to file       {elapsed[3]} ms");
        }

        /// <summary>
        /// Tests Anlog log writing with caller details.
        /// </summary>
        /// <returns>Execution duration (milisseconds).</returns>
        protected abstract long TestAnlogLoggerWithCaller();

        /// <summary>
        /// Tests Serilog log writing with caller details.
        /// </summary>
        /// <returns>Execution duration (milisseconds).</returns>
        protected abstract long TestSerilogWithCaller(
            [CallerFilePath] string filePath = null,
            [CallerMemberName] string caller = null,
            [CallerLineNumber] int lineNumber = 0);

        /// <summary>
        /// Tests Serilog log writing without caller details.
        /// </summary>
        /// <returns>Execution duration (milisseconds).</returns>
        protected abstract long TestSerilogWithoutCaller();

        /// <summary>
        /// Tests writing with caller details to a single file.
        /// </summary>
        /// <returns>Execution duration (milisseconds).</returns>
        protected abstract long TestSavingToFileWithCaller(
            [CallerFilePath] string filePath = null,
            [CallerMemberName] string caller = null,
            [CallerLineNumber] int lineNumber = 0);

        /// <summary>
        /// Measures execution of an action.
        /// </summary>
        /// <param name="action">Action to execute.</param>
        /// <returns>Execution duration (milisseconds).</returns>
        protected long MeasureExecution(Action action)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (var quantity = 0; quantity < TotalExecutions; quantity++)
            {
                action();
            }

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
    }
}
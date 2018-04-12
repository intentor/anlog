using System;
using Anlog.Factories;
using Anlog.Formatters.CompactKeyValue;
using Anlog.Sinks.InMemory;
using Xunit;
using static Anlog.Tests.TestObjects.TestConstants;

namespace Anlog.Tests
{
    /// <summary>
    /// Tests for minimum level configuration.
    /// </summary>
    public sealed class MinimumLevelTests : IDisposable
    {
        /// <summary>
        /// Log levels names.
        /// </summary>
        private ILogLevelName logLevelName = new CompactKeyValueLogLevelName();
        
        /// <inheritdoc />
        public void Dispose()
        {
            Log.Logger = null;
        }
            
        [Fact]
        public void WhenMinimumLevelSetToDebug_WriteAll()
        {
            CreateLogger(LogLevel.Debug);
            
            WriteLogs();

            AssertLogs(4, logLevelName.Debug, logLevelName.Info, logLevelName.Warning, logLevelName.Error);
        }
        
        [Fact]
        public void WhenMinimumLevelSetToInfo_WriteAbove()
        {
            CreateLogger(LogLevel.Info);
            
            WriteLogs();

            AssertLogs(3, logLevelName.Info, logLevelName.Warning, logLevelName.Error);
        }
        
        [Fact]
        public void WhenMinimumLevelSetToWarning_WriteAbove()
        {
            CreateLogger(LogLevel.Warning);
            
            WriteLogs();

            AssertLogs(2, logLevelName.Warning, logLevelName.Error);
        }
        
        [Fact]
        public void WhenMinimumLevelSetToError_WriteOnlyError()
        {
            CreateLogger(LogLevel.Error);
            
            WriteLogs();

            AssertLogs(1, logLevelName.Error);
        }

        /// <summary>
        /// Creates the logger with a minimum level of writing.
        /// </summary>
        /// <param name="level">Level of writing.</param>
        private void CreateLogger(LogLevel level)
        {
            Log.Logger = new LoggerFactory()
                .MinimumLevel.Set(level)
                .WriteTo.InMemory()
                .CreateLogger();
        }

        /// <summary>
        /// Writes logs of all levels.
        /// </summary>
        private void WriteLogs()
        {
            Log.Append(TestString.Key, TestString.Value).Debug();
            Log.Append(TestString.Key, TestString.Value).Info();
            Log.Append(TestString.Key, TestString.Value).Warning();
            Log.Append(TestString.Key, TestString.Value).Error();
        }

        /// <summary>
        /// Assert the logs.
        /// </summary>
        /// <param name="quantity">Quantity of itens in the log.</param>
        /// <param name="levels">Levels in the logs</param>
        private void AssertLogs(int quantity, params LogLevelName[] levels)
        {
            var logs = Log.GetSink<InMemorySink>()?.GetLogs().Split(Environment.NewLine);
            
            Assert.NotNull(logs);
            Assert.Equal(string.Empty, logs[logs.Length - 1]);
            Assert.Equal(quantity + 1, logs.Length);

            for (var index = 0; index < logs.Length - 1; index++)
            {
                Assert.Equal(levels[index].Entry, logs[index].Substring(25, 3));
            }
        }
    }
}
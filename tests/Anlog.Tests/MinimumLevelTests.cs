using System;
using Anlog.Factories;
using Anlog.Sinks.InMemory;
using Xunit;
using static Anlog.Tests.TestObjects.TestConstants;
using static Anlog.Formatters.DefaultFormattingOptions;

namespace Anlog.Tests
{
    /// <summary>
    /// Tests for minimum level configuration.
    /// </summary>
    public sealed class MinimumLevelTests
    {
        /// <summary>
        /// Logger object.
        /// </summary>
        private ILogger logger;
            
        [Fact]
        public void WhenInDefaultLevel_WriteFromInfo()
        {
            logger = new LoggerFactory()
                .WriteTo.InMemory()
                .CreateLogger();
            
            WriteLogs();

            AssertLogs(3, LogLevelNames.Info, LogLevelNames.Warn, LogLevelNames.Error);
        }
            
        [Fact]
        public void WhenMinimumLevelSetToDebug_WriteAll()
        {
            CreateLogger(LogLevel.Debug);
            
            WriteLogs();

            AssertLogs(4, LogLevelNames.Debug, LogLevelNames.Info, LogLevelNames.Warn, LogLevelNames.Error);
        }
        
        [Fact]
        public void WhenMinimumLevelSetToInfo_WriteAbove()
        {
            CreateLogger(LogLevel.Info);
            
            WriteLogs();

            AssertLogs(3, LogLevelNames.Info, LogLevelNames.Warn, LogLevelNames.Error);
        }
        
        [Fact]
        public void WhenMinimumLevelSetToWarn_WriteAbove()
        {
            CreateLogger(LogLevel.Warn);
            
            WriteLogs();

            AssertLogs(2, LogLevelNames.Warn, LogLevelNames.Error);
        }
        
        [Fact]
        public void WhenMinimumLevelSetToError_WriteOnlyError()
        {
            CreateLogger(LogLevel.Error);
            
            WriteLogs();

            AssertLogs(1, LogLevelNames.Error);
        }

        /// <summary>
        /// Creates the logger with a minimum level of writing.
        /// </summary>
        /// <param name="level">Level of writing.</param>
        private void CreateLogger(LogLevel level)
        {
            logger = new LoggerFactory()
                .MinimumLevel.Set(level)
                .WriteTo.InMemory()
                .CreateLogger();
        }

        /// <summary>
        /// Writes logs of all levels.
        /// </summary>
        private void WriteLogs()
        {
            logger.Append(TestString.Key, TestString.Value).Debug();
            logger.Append(TestString.Key, TestString.Value).Info();
            logger.Append(TestString.Key, TestString.Value).Warn();
            logger.Append(TestString.Key, TestString.Value).Error();
        }

        /// <summary>
        /// Assert the logs.
        /// </summary>
        /// <param name="quantity">Quantity of itens in the log.</param>
        /// <param name="levels">Levels in the logs</param>
        private void AssertLogs(int quantity, params LogLevelName[] levels)
        {
            var logs = logger.GetSink<InMemorySink>()?.GetLogs().Split(Environment.NewLine);
            
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
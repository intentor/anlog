using Anlog.Factories;
using Anlog.Sinks;
using Anlog.Sinks.Console;
using Anlog.Sinks.InMemory;
using Anlog.Sinks.SingleFile;
using Anlog.Tests.TestObjects;
using Anlog.Tests.TestObjects.Models;
using Xunit;

namespace Anlog.Tests.Loggers
{
    /// <summary>
    /// Tests for the default logger.
    /// </summary>
    public sealed class DefaultLoggerTests
    {
        [Fact]
        public void WhenLoggingFromConstructor_LogCorrectMembersNames()
        {
            Log.Logger = new LoggerFactory()
                .WriteTo.InMemory(appendNewLine: false)
                .CreateLogger();
            
            var model = new TestConstructorLogModel();

            var log = Log.GetSink<InMemorySink>()?.GetLogs();
            
            Assert.Equal("TestConstructorLogModel.Constructor:13 key=value", log?.Substring(30));
        }

        [Fact]
        public void WhenHavingSinkWithCustomLogLevel_KeepIt()
        {
            var logger = new LoggerFactory()
                .WriteTo.Console()
                .WriteTo.InMemory(minimumLevel: LogLevel.Warn)
                .WriteTo.SingleFile(minimumLevel: LogLevel.Error)
                .CreateLogger();
            
            Assert.Equal(LogLevel.Info, logger.MinimumLevel);
            Assert.Equal(LogLevel.Info, logger.Sinks[0].MinimumLevel);
            Assert.Equal(LogLevel.Warn, logger.Sinks[1].MinimumLevel);
            Assert.Equal(LogLevel.Error, logger.Sinks[2].MinimumLevel);
        }

        [Fact]
        public void WhenHavingSinksWithCustomLogLevelAndMinimumLevel_KeepIt()
        {
            var logger = new LoggerFactory()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.InMemory(minimumLevel: LogLevel.Warn)
                .WriteTo.SingleFile(minimumLevel: LogLevel.Error)
                .CreateLogger();
            
            Assert.Equal(LogLevel.Debug, logger.MinimumLevel);
            Assert.Equal(LogLevel.Debug, logger.Sinks[0].MinimumLevel);
            Assert.Equal(LogLevel.Warn, logger.Sinks[1].MinimumLevel);
            Assert.Equal(LogLevel.Error, logger.Sinks[2].MinimumLevel);
        }

        [Fact]
        public void WhenAppendingNonDataContractModel_LogCorrectData()
        {
            var logger = CreateDefaultLogger();
            
            logger.Append("model", TestConstants.TestNonDataContractModelInstance).Info();

            var log = logger.GetSink<InMemorySink>()?.GetLogs();
            
            Assert.Equal("model={int=69 double=24.11 text=LogTest date=2018-03-25 23:00:00.000 shorts=[1,2,3,4,5]}", log?.Substring(38));
        }

        [Fact]
        public void WhenAddingSink_GetByGenerics()
        {
            Assert.NotNull(CreateDefaultLogger().GetSink<InMemorySink>());
        }
        
        [Fact]
        public void WhenAddingSink_GetByType()
        {
            Assert.NotNull(CreateDefaultLogger().GetSink(typeof(InMemorySink)));
        }
        
        [Fact]
        public void WhenHavingNoSink_ReturnNull()
        {
            Assert.Null(CreateDefaultLogger().GetSink(typeof(FileSink)));
        }

        /// <summary>
        /// Creates a default logger.
        /// </summary>
        /// <returns>A default logger.</returns>
        private ILogger CreateDefaultLogger()
        {
            return new LoggerFactory()
                .WriteTo.InMemory(appendNewLine: false)
                .CreateLogger();
        }
    }
}
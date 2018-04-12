using Anlog.Factories;
using Anlog.Sinks;
using Anlog.Sinks.InMemory;
using Anlog.Tests.TestObjects.Models;
using Xunit;
using static Anlog.Tests.TestObjects.TestConstants;

namespace Anlog.Tests
{
    /// <summary>
    /// Tests for the default logger.
    /// </summary>
    public sealed class DefaultLoggerTests
    {
        /// <summary>
        /// Object to test.
        /// </summary>
        private ILogger logger;
        
        public DefaultLoggerTests()
        {
            logger = new LoggerFactory()
                .WriteTo.InMemory(false)
                .CreateLogger();
        }
        
        [Fact]
        public void WhenLoggingFromConstructor_LogCorrectMembersNames()
        {
            Log.Logger = new LoggerFactory()
                .WriteTo.InMemory(false)
                .CreateLogger();
            
            var model = new TestConstructorLogModel();

            var log = Log.GetSink<InMemorySink>()?.GetLogs();
            
            Assert.Equal("c=TestConstructorLogModel.Constructor:13 key=value", log?.Substring(30));
        }

        [Fact]
        public void WhenAppendingNonDataContractModel_LogCorrectData()
        {
            logger.Append("model", TestNonDataContractModelInstance).Info();

            var log = logger.GetSink<InMemorySink>()?.GetLogs();
            
            Assert.Equal("model={int=69 double=24.11 text=LogTest date=2018-03-25 23:00:00.000 shorts=[1,2,3,4,5]}", log?.Substring(40));
        }

        [Fact]
        public void WhenAddingSink_GetByGenerics()
        {
            Assert.NotNull(logger.GetSink<InMemorySink>());
        }
        
        [Fact]
        public void WhenAddingSink_GetByType()
        {
            Assert.NotNull(logger.GetSink(typeof(InMemorySink)));
        }
        
        [Fact]
        public void WhenHavingNoSink_ReturnNull()
        {
            Assert.Null(logger.GetSink(typeof(FileSink)));
        }
    }
}
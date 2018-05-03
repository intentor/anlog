using Anlog.Loggers;
using Xunit;

namespace Anlog.Tests.Loggers
{
    /// <summary>
    /// Tests for the dummy logger.
    /// </summary>
    public sealed class DummyLoggerTests
    {
        /// <summary>
        /// Object to test.
        /// </summary>
        private ILogger dummyLogger = new DummyLogger();
        
        [Fact]
        public void WhenUsingLogWithoutSettingLogger_UseDummy()
        {
            dummyLogger.Append("key", "value").Debug("message");
            dummyLogger.Append("key", "value").Info("message");
            dummyLogger.Append("key", "value").Warn("message");
            dummyLogger.Append("key", "value").Error("message");
        }
        
        [Fact]
        public void WhenAppending_UseSameFormatter()
        {
            var appender1 = dummyLogger.Append("key", "value");
            var appender2 = dummyLogger.Append("key", "value");
            
            Assert.Equal(appender1, appender2);
        }
    }
}
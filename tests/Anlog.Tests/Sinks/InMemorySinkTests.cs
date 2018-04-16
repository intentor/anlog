using Anlog.Sinks.InMemory;
using Xunit;
using static Anlog.Tests.TestObjects.TestConstants;

namespace Anlog.Tests.Sinks
{
    /// <summary>
    /// Tests for <see cref="InMemorySink"/>.
    /// </summary>
    public sealed class InMemorySinkTests
    {
        /// <summary>
        /// Object to test.
        /// </summary>
        private InMemorySink sink = new InMemorySink();

        [Fact]
        public void WhenWriting_WriteToMemory()
        {
            sink.Write(LogLevel.Debug, GenericLog);
            
            Assert.Equal(GenericLog, sink.GetLogs());
        }
    }
}
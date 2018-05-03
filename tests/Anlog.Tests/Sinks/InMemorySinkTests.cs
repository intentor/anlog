using Anlog.Formatters.CompactKeyValue;
using Anlog.Renderers;
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
        [Fact]
        public void WhenWriting_WriteToMemory()
        {
            var sink = new InMemorySink(new CompactKeyValueFormatter(), () => new DefaultDataRenderer())
            {
                AppendNewLine = false
            };
            
            sink.Write(GenericLogLevelName.Debug, GenericLogEntries);
            
            Assert.Equal(GenericLogText, sink.GetLogs().Substring(30));
        }
    }
}
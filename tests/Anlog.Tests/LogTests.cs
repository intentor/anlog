using System;
using Anlog.Factories;
using Anlog.Sinks;
using Anlog.Sinks.InMemory;
using Xunit;

namespace Anlog.Tests
{
    /// <summary>
    /// Tests for <see cref="Log"/>.
    /// </summary>
    public sealed class LogTests : IDisposable
    {
        public LogTests()
        {
            Log.Logger = new LoggerFactory()
                .WriteTo.InMemory()
                .CreateLogger();
        }
        
        /// <inheritdoc />
        public void Dispose()
        {
            Log.Logger = null;
        }
        
        [Fact]
        public void WhenAddingSink_GetByGenerics()
        {
            Assert.NotNull(Log.GetSink<InMemorySink>());
        }
        
        [Fact]
        public void WhenAddingSink_GetByType()
        {
            Assert.NotNull(Log.GetSink(typeof(InMemorySink)));
        }
        
        [Fact]
        public void WhenHavingNoSink_ReturnNull()
        {
            Assert.Null(Log.GetSink(typeof(FileSink)));
        }
    }
}
using System.IO;
using Anlog.Sinks;
using Anlog.Tests.TestObjects;
using Xunit;
using static Anlog.Tests.TestObjects.TestConstants;

namespace Anlog.Tests.Sinks
{
    /// <summary>
    /// Tests for <see cref="FileSink"/>.
    /// </summary>
    public sealed class FileSinkTests
    {
        [Fact]
        public void WhenWritingToNewFile_CreateFile()
        {
            using (var temp = TempFolder.Create())
            {
                var path = temp.GetLogFilePath();

                using (var sink = new FileSink(path))
                {
                    sink.Write(GenericLog);
                }

                var contents = File.ReadAllLines(path);
                Assert.Equal(GenericLog, contents[0]);
            }
        }
        
        [Fact]
        public void WhenWritingToExistingFile_AppendToFile()
        {
            using (var temp = TempFolder.Create())
            {
                var path = temp.GetLogFilePath();

                using (var sink = new FileSink(path))
                {
                    sink.Write(GenericLog + "1");
                }

                using (var sink = new FileSink(path))
                {
                    sink.Write(GenericLog + "2");
                }

                var contents = File.ReadAllLines(path);
                Assert.Equal(GenericLog + "1", contents[0]);
                Assert.Equal(GenericLog + "2", contents[1]);
            }
        }
    }
}
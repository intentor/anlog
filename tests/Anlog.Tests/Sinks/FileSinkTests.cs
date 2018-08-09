using System.IO;
using Anlog.Formatters.CompactKeyValue;
using Anlog.Renderers;
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

                using (var sink = CreateSink(path))
                {
                    sink.Write(GenericLogLevelName.Debug, GenericLogEntries);
                }

                var contents = File.ReadAllLines(path);
                Assert.Equal(GenericLogText, contents[0].Substring(30));
            }
        }
        
        [Fact]
        public void WhenWritingToExistingFile_AppendToFile()
        {
            using (var temp = TempFolder.Create())
            {
                var path = temp.GetLogFilePath();

                using (var sink = CreateSink(path))
                {
                    sink.Write(GenericLogLevelName.Debug, GenericLogEntries);
                }

                using (var sink = CreateSink(path))
                {
                    sink.Write(GenericLogLevelName.Debug, GenericLogEntries);
                }

                var contents = File.ReadAllLines(path);
                Assert.Equal(GenericLogText, contents[0].Substring(30));
                Assert.Equal(GenericLogText, contents[1].Substring(30));
            }
        }

        [Fact]
        public void WhenWritingToNewFileDeleDirectoryAndWriteAgain_CreateDirectoryAndFile()
        {            
            using (var temp = TempFolder.Create())
            {
                var path = temp.GetLogFilePath();

                using (var sink = CreateSink(path))
                {
                    sink.Write(GenericLogLevelName.Debug, GenericLogEntries);
                    var contents = File.ReadAllLines(path);
                    Assert.Equal(GenericLogText, contents[0].Substring(30));
                
                    Directory.Delete(temp.FolderPath, true);
                    
                    sink.Write(GenericLogLevelName.Debug, GenericLogEntries);
                    contents = File.ReadAllLines(path);
                    Assert.Equal(GenericLogText, contents[0].Substring(30));
                }
            }
        }

        [Fact]
        public void WhenWritingToNewFileDeleFileAndWriteAgain_CreateFile()
        {            
            using (var temp = TempFolder.Create())
            {
                var path = temp.GetLogFilePath();

                using (var sink = CreateSink(path))
                {
                    sink.Write(GenericLogLevelName.Debug, GenericLogEntries);
                    var contents = File.ReadAllLines(path);
                    Assert.Equal(GenericLogText, contents[0].Substring(30));
                
                    File.Delete(path);
                    
                    sink.Write(GenericLogLevelName.Debug, GenericLogEntries);
                    contents = File.ReadAllLines(path);
                    Assert.Equal(GenericLogText, contents[0].Substring(30));
                }
            }
        }

        /// <summary>
        /// Creates a file sink for tests.
        /// </summary>
        /// <param name="path">Log path.</param>
        /// <returns>Created sink.</returns>
        private FileSink CreateSink(string path)
        {
            return new FileSink(new CompactKeyValueFormatter(), () => new DefaultDataRenderer(), path);
        }
    }
}
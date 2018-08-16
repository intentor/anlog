using System;
using System.IO;
using System.Threading;
using Anlog.Sinks.RollingFile;
using Anlog.Tests.TestObjects;
using Xunit;
using static Anlog.Sinks.RollingFile.RollingFilePeriod;
using static Anlog.Tests.TestObjects.TestConstants;

namespace Anlog.Tests.Sinks
{
    /// <summary>
    /// Tests for <see cref="RollingFileExpiryCheck"/>.
    /// </summary>
    public class RollingFileExpiryCheckTests : IDisposable
    {
        /// <summary>
        /// Logs saving folder.
        /// </summary>
        private readonly TempFolder tempFolder = TempFolder.Create();

        /// <inheritdoc />
        public void Dispose()
        {
            tempFolder.Dispose();
        }
        
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 4)]
        [InlineData(5, 5)]
        [InlineData(6, 6)]
        [InlineData(7, 7)]
        [InlineData(8, 8)]
        [InlineData(9, 9)]
        [InlineData(10, 10)]
        [InlineData(15, 10)]
        public void WhenCheckingForUpdate_ReturnBool(int expiryDate, int expectedFileCount)
        {
            CreateDummyFiles();
            var expiryCheck = new RollingFileExpiryCheck(tempFolder.FolderPath, Day, expiryDate, 100);

            Thread.Sleep(200);
            
            var files = Directory.GetFiles(tempFolder.FolderPath);
            Assert.Equal(expectedFileCount, files.Length);
        }

        /// <summary>
        /// Creates some dummy files with different previous dates, including the <see cref="BaseDate"/>.
        /// </summary>
        private void CreateDummyFiles()
        {
            for (var days = 0; days < 10; days++)
            {
                var date = DateTime.Now.AddDays(-days);
                File.Create(Path.Combine(tempFolder.FolderPath, Day.GetFileName(date, 1)));
            }
        }
    }
}
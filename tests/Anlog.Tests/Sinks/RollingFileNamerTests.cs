using System;
using System.Collections.Generic;
using System.IO;
using Anlog.Sinks.RollingFile;
using Anlog.Tests.TestObjects;
using Xunit;
using static Anlog.Sinks.RollingFile.RollingFilePeriod;
using static Anlog.Tests.TestObjects.TestConstants;

namespace Anlog.Tests.Sinks
{
    /// <summary>
    /// Testes of <see cref="RollingFileNamer"/>.
    /// </summary>
    public sealed class RollingFileNamerTests : IDisposable
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

        [Fact]
        public void WhenCreatingWithNonexistentPath_CreatesSuccessfully()
        {
            var nonexistentPath = Path.Combine(Directory.GetCurrentDirectory(), Guid.NewGuid().ToString());
            var namer = new RollingFileNamer(nonexistentPath, Day);

            Assert.NotNull(namer);
        }

        /// <summary>
        /// Test data for checking file update.
        /// </summary>
        public static IEnumerable<object[]> RollingUpdateData =>
            new List<object[]>
            {
                new object[] {Day, 0, false},
                new object[] {Day, 60, false},
                new object[] {Day, 1439, false},
                new object[] {Day, 1440, true},
                new object[] {Day, 1441, true},
                new object[] {Day, 2880, true},
                new object[] {Day, 43200, true},
                new object[] {Hour, 0, false},
                new object[] {Hour, 59, false},
                new object[] {Hour, 60, true},
                new object[] {Hour, 61, true},
                new object[] {Hour, 120, true}
            };

        [Theory]
        [MemberData(nameof(RollingUpdateData))]
        public void WhenCheckingForUpdate_ReturnBool(RollingFilePeriod period, int minutesToAdd, bool expected)
        {
            CreateDummyFiles(period);
            var namer = new RollingFileNamer(tempFolder.FolderPath, period);

            var shouldUpdate = namer.EvaluateFileUpdate(BaseDate.AddMinutes(minutesToAdd)).ShouldUpdate;

            Assert.Equal(expected, shouldUpdate);
        }

        /// <summary>
        /// Test data for getting file path.
        /// </summary>
        public static IEnumerable<object[]> RollingFilePathData =>
            new List<object[]>
            {
                new object[] {Day, 0, "log-20180506-1.txt"},
                new object[] {Day, 60, "log-20180506-1.txt"},
                new object[] {Day, 1439, "log-20180506-1.txt"},
                new object[] {Day, 1440, "log-20180507-1.txt"},
                new object[] {Day, 1441, "log-20180507-1.txt"},
                new object[] {Day, 2880, "log-20180508-1.txt"},
                new object[] {Day, 43200, "log-20180605-1.txt"},
                new object[] {Hour, 0, "log-2018050600-1.txt"},
                new object[] {Hour, 59, "log-2018050600-1.txt"},
                new object[] {Hour, 60, "log-2018050601-1.txt"},
                new object[] {Hour, 61, "log-2018050601-1.txt"},
                new object[] {Hour, 120, "log-2018050602-1.txt"},
                new object[] {Hour, 1440, "log-2018050700-1.txt"},
                new object[] {Hour, 2880, "log-2018050800-1.txt"},
                new object[] {Hour, 43200, "log-2018060500-1.txt"}
            };

        [Theory]
        [MemberData(nameof(RollingFilePathData))]
        public void WhenGettingFilePath_ReturnCorrectName(RollingFilePeriod period, int minutesToAdd, string expected)
        {
            CreateDummyFiles(period);
            var namer = new RollingFileNamer(tempFolder.FolderPath, period);

            var fileName = Path.GetFileName(namer.EvaluateFileUpdate(BaseDate.AddMinutes(minutesToAdd)).FilePath);

            Assert.Equal(expected, fileName);
        }

        [Fact]
        public void WhenGettingFilePathLastFileNotExceedsMaxSize_ReturnCorrectName()
        {
            CreateDummy1MbLogFiles(3, false);
            var namer = new RollingFileNamer(tempFolder.FolderPath, Day, 943718);

            var fileName = Path.GetFileName(namer.EvaluateFileUpdate(BaseDate).FilePath);

            Assert.Equal("log-20180506-4.txt", fileName);
        }

        [Fact]
        public void WhenGettingFilePathAllExceedsMaxSize_ReturnCorrectName()
        {
            CreateDummy1MbLogFiles(3, true);
            var namer = new RollingFileNamer(tempFolder.FolderPath, Day, 943718);

            var fileName = Path.GetFileName(namer.EvaluateFileUpdate(BaseDate).FilePath);

            Assert.Equal("log-20180506-3.txt", fileName);
        }

        /// <summary>
        /// Creates some dummy files with different previous dates, including the <see cref="BaseDate"/>.
        /// </summary>
        /// <param name="period">Rolling file period to consider.</param>
        private void CreateDummyFiles(RollingFilePeriod period)
        {
            for (var days = 0; days < 10; days++)
            {
                var date = BaseDate.AddDays(-days);
                File.Create(Path.Combine(tempFolder.FolderPath, period.GetFileName(date, 1)));
            }
        }

        /// <summary>
        /// Crates some dummy files with 1mb size.
        /// </summary>
        /// <param name="numberOfFiles">Number of files to be crated.</param>
        /// <param name="lastFileHalfSize">Creates last file with half of the size limit.</param>
        private void CreateDummy1MbLogFiles(int numberOfFiles, bool lastFileHalfSize)
        {
            var count = 1048576 / GenericLogText.Length;
            for (var filesCount = 1; filesCount <= numberOfFiles; filesCount++)
            {
                var filePath = Path.Combine(tempFolder.FolderPath, Day.GetFileName(BaseDate, filesCount));
                var logFile = File.CreateText(filePath);

                for (var fileContent = 0; fileContent < count; fileContent++)
                {
                    logFile.Write(GenericLogText);

                    if (lastFileHalfSize && filesCount == numberOfFiles && (fileContent * 2) >= count)
                        break;
                }
            }
        }
    }
}
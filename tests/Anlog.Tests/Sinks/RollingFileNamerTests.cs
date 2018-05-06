using System;
using System.Collections.Generic;
using System.IO;
using Anlog.Sinks.RollingFile;
using Anlog.Tests.TestObjects;
using Xunit;
using static Anlog.Sinks.RollingFile.RollingFilePeriod;

namespace Anlog.Tests.Sinks
{
    /// <summary>
    /// Testes of <see cref="RollingFileNamer"/>.
    /// </summary>
    public sealed class RollingFileNamerTests : IDisposable
    {
        /// <summary>
        /// Base date for tests.
        /// </summary>
        private static readonly DateTime BaseDate = new DateTime(2018, 5, 6, 0, 0, 0);

        /// <summary>
        /// Logs saving folder.
        /// </summary>
        private readonly TempFolder tempFolder = TempFolder.Create();

        /// <inheritdoc />
        public void Dispose()
        {
            tempFolder.Dispose();
        }

        /// <summary>
        /// Test data for checking file update.
        /// </summary>
        public static IEnumerable<object[]> RollingUpdateData  =>
            new List<object[]>
            {
                new object[] { Day, 0, false },
                new object[] { Day, 60, false },
                new object[] { Day, 1439, false },
                new object[] { Day, 1440, true },
                new object[] { Day, 1441, true },
                new object[] { Day, 2880, true },
                new object[] { Day, 43200, true },
                new object[] { Hour, 0, false },
                new object[] { Hour, 59, false },
                new object[] { Hour, 60, true },
                new object[] { Hour, 61, true },
                new object[] { Hour, 120, true }
            };

        [Theory]
        [MemberData(nameof(RollingUpdateData))]
        public void WhenCheckingForUpdate_ReturnBool(RollingFilePeriod period, int minutesToAdd, bool expected)
        {
            CreateDummyFiles(period);
            var namer = new RollingFileNamer(tempFolder.FolderPath, period);

            var shouldUpdate = namer.ShouldUpdateFile(BaseDate.AddMinutes(minutesToAdd));
            
            Assert.Equal(expected, shouldUpdate);
        }

        /// <summary>
        /// Test data for getting file path.
        /// </summary>
        public static IEnumerable<object[]> RollingFilePathData  =>
            new List<object[]>
            {
                new object[] { Day, 0, "log-20180506.txt" },
                new object[] { Day, 60, "log-20180506.txt" },
                new object[] { Day, 1439, "log-20180506.txt" },
                new object[] { Day, 1440, "log-20180507.txt" },
                new object[] { Day, 1441, "log-20180507.txt" },
                new object[] { Day, 2880, "log-20180508.txt" },
                new object[] { Day, 43200, "log-20180605.txt" },
                new object[] { Hour, 0, "log-2018050600.txt" },
                new object[] { Hour, 59, "log-2018050600.txt" },
                new object[] { Hour, 60, "log-2018050601.txt" },
                new object[] { Hour, 61, "log-2018050601.txt" },
                new object[] { Hour, 120, "log-2018050602.txt" },
                new object[] { Hour, 1440, "log-2018050700.txt" },
                new object[] { Hour, 2880, "log-2018050800.txt" },
                new object[] { Hour, 43200, "log-2018060500.txt" }
            };

        [Theory]
        [MemberData(nameof(RollingFilePathData))]
        public void WhenGettingFilePath_ReturnCorrectName(RollingFilePeriod period, int minutesToAdd, string expected)
        {
            CreateDummyFiles(period);
            var namer = new RollingFileNamer(tempFolder.FolderPath, period);

            var fileName = Path.GetFileName(namer.GetFilePath(BaseDate.AddMinutes(minutesToAdd)));
            
            Assert.Equal(expected, fileName);
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
                File.Create(Path.Combine(tempFolder.FolderPath, period.GetFileName(date)));
            }
        }
    }
}
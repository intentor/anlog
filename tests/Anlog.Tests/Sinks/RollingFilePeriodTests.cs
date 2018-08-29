using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Anlog.Sinks.RollingFile;
using Anlog.Tests.TestObjects;
using Xunit;
using static Anlog.Sinks.RollingFile.RollingFilePeriod;

namespace Anlog.Tests.Sinks
{
    /// <summary>
    /// Tests of <see cref="RollingFilePeriod"/>.
    /// </summary>
    public sealed class RollingFilePeriodTests
    {
        /// <summary>
        /// Data for rolling period check.
        /// </summary>
        public static IEnumerable<object[]> RollingPeriodCheckData  =>
            new List<object[]>
            {
                new object[] { Day, 0, false },
                new object[] { Day, 95, false },
                new object[] { Day, 135, true },
                new object[] { Day, 1439, true },
                new object[] { Day, 1440, true },
                new object[] { Day, 2880, true },
                new object[] { Day, 43200, true },
                new object[] { Day, 518400, true },
                new object[] { Hour, 0, false },
                new object[] { Hour, 30, false },
                new object[] { Hour, 44, false },
                new object[] { Hour, 45, true },
                new object[] { Hour, 48, true },
                new object[] { Hour, 55, true },
                new object[] { Hour, 95, true },
                new object[] { Hour, 135, true },
            };

        [Theory]
        [MemberData(nameof(RollingPeriodCheckData))]
        public void WhenRolling_CheckPeriodExceeded(RollingFilePeriod period, int minutesToAdd, bool expected)
        {
            var last = new DateTime(2018, 10, 5, 22, 15, 0);
            var current = last.AddMinutes(minutesToAdd);

            var isExceeded = period.IsPeriodExceeded(last, current);
            
            Assert.Equal(expected, isExceeded);
        }
        
        /// <summary>
        /// Data for file format evaluation.
        /// </summary>
        public static IEnumerable<object[]> RollingFileFormatData  =>
            new List<object[]>
            {
                new object[] { Day },
                new object[] { Hour },
            };
        
        [Theory]
        [MemberData(nameof(RollingFileFormatData))]
        public void WhenCreatingFileWithFormat_PatternMatches(RollingFilePeriod period)
        {
            using (var tempFolder = TempFolder.Create())
            {
                var date = DateTime.Now;
                File.Create(Path.Combine(tempFolder.FolderPath, period.GetFileName(date, 1)));

                var files = Directory.GetFiles(tempFolder.FolderPath);
            
                Assert.Single(files);
                Assert.Matches(new Regex(period.FileNamePattern), Path.GetFileName(files[0]));

                var match = Regex.Match(Path.GetFileName(files[0]), period.FileNamePattern);
                Assert.Equal(date.ToString(period.DateFormat), match.Groups[1].Value);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using Anlog.Entries;
using Anlog.Formatters.CompactKeyValue;
using Anlog.Renderers;
using Anlog.Sinks.RollingFile;
using Anlog.Tests.TestObjects;
using Anlog.Time;
using Moq;
using Xunit;
using static Anlog.Formatters.DefaultFormattingOptions;
using static Anlog.Tests.TestObjects.TestConstants;
using static Anlog.Sinks.RollingFile.RollingFilePeriod;

namespace Anlog.Tests.Sinks
{
    /// <summary>
    /// Tests of <see cref="RollingFileSink"/>.
    /// </summary>
    public sealed class RollingFileSinkTests : IDisposable
    {
        /// <summary>
        /// Mock for the time provider.
        /// </summary>
        private readonly Mock<TimeProvider> timeProviderMock;
        
        public RollingFileSinkTests()
        {
            timeProviderMock = new Mock<TimeProvider>();
            TimeProvider.Current = timeProviderMock.Object;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            TimeProvider.ResetToDefault();
        }
        
        /// <summary>
        /// Data for different period tests.
        /// </summary>
        public static IEnumerable<object[]> DifferentPeriodData  =>
            new List<object[]>
            {
                new object[] { Day, new int[] { 0, 1440, 2880, 43200 } },
                new object[] { Hour, new int[] { 0, 60, 120, 1440, 2880, 43200 } }
            };

        [Theory]
        [MemberData(nameof(DifferentPeriodData))]
        public void WhenWritingInDifferentPeriod_CreateNewFile(RollingFilePeriod period, int[] minutesToAdd)
        {
            using (var temp = TempFolder.Create())
            {
                WriteLogs(temp.FolderPath, period, minutesToAdd);
                
                foreach (var minutes in minutesToAdd)
                {
                    var date = BaseDate.AddMinutes(minutes);
                    var contentsFile = File.ReadAllLines(Path.Combine(temp.FolderPath, period.GetFileName(date)));
                    var log = string.Format("{0} [DBG] date={1}", date.ToString(DefaultDateTimeFormat),
                        date.ToString(period.DateFormat));
                    
                    Assert.Equal(log, contentsFile[0]);
                }
            }
        }
        
        /// <summary>
        /// Data for same period tests.
        /// </summary>
        public static IEnumerable<object[]> SamePeriodData  =>
            new List<object[]>
            {
                new object[] { Day, new int[] { 0, 60, 120, 240, 480, 1439 } },
                new object[] { Hour, new int[] { 0, 5, 10, 20, 40, 59 } }
            };

        [Theory]
        [MemberData(nameof(SamePeriodData))]
        public void WhenWritingInSamePeriod_DontCreateNewFile(RollingFilePeriod period, int[] minutesToAdd)
        {
            using (var temp = TempFolder.Create())
            {
                WriteLogs(temp.FolderPath, period, minutesToAdd);

                var files = Directory.GetFiles(temp.FolderPath);
                Assert.Single(files);

                var contents = File.ReadAllLines(files[0]);
                Assert.Equal(minutesToAdd.Length, contents.Length);
            }
        }

        /// <summary>
        /// Writes logs using a sink.
        /// </summary>
        /// <param name="logFolderPath">Log files folder path.</param>
        /// <param name="period">Rolling file period.</param>
        /// <param name="minutesToAdd">Minutes to add for each writing.</param>
        private void WriteLogs(string logFolderPath, RollingFilePeriod period, int[] minutesToAdd)
        {
            foreach (var minutes in minutesToAdd)
            {
                var date = BaseDate.AddMinutes(minutes);
                timeProviderMock.Setup(m => m.CurrentDateTime).Returns(date);
                using (var sink = CreateSink(logFolderPath, period))
                {
                    var entries = new List<ILogEntry>()
                    {
                        new LogEntry("date", date.ToString(period.DateFormat))
                    };
                        
                    sink.Write(GenericLogLevelName.Debug, entries);
                }
            }
        }

        /// <summary>
        /// Creates a file sink for tests.
        /// </summary>
        /// <param name="logFolderPath">Log files folder path.</param>
        /// <param name="period">Rolling file period.</param>
        /// <returns>Created sink.</returns>
        private RollingFileSink CreateSink(string logFolderPath, RollingFilePeriod period)
        {
            var namer = new RollingFileNamer(logFolderPath, period);
            return new RollingFileSink(new CompactKeyValueFormatter(), () => new DefaultDataRenderer(), namer);
        }
    }
}
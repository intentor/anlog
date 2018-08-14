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
        /// Approximately log entry size.
        /// </summary>
        private const int ApproximatelyLogEntrySize = 45;
        
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
                new object[] { true, Day, new int[] { 0, 1440, 2880, 43200 } },
                new object[] { false, Day, new int[] { 0, 1440, 2880, 43200 } },
                new object[] { true, Hour, new int[] { 0, 60, 120, 1440, 2880, 43200 } },
                new object[] { false, Hour, new int[] { 0, 60, 120, 1440, 2880, 43200 } }
            };

        [Theory]
        [MemberData(nameof(DifferentPeriodData))]
        public void WhenWritingInDifferentPeriod_CreateNewFile(bool async, RollingFilePeriod period, int[] minutesToAdd)
        {
            using (var temp = TempFolder.Create())
            {
                WriteLogs(async, temp.FolderPath, period, minutesToAdd);
                
                foreach (var minutes in minutesToAdd)
                {
                    var date = BaseDate.AddMinutes(minutes);
                    var contentsFile = File.ReadAllLines(Path.Combine(temp.FolderPath, period.GetFileName(date, 1)));
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
                new object[] { true, Day, new int[] { 0, 60, 120, 240, 480, 1439 } },
                new object[] { false, Day, new int[] { 0, 60, 120, 240, 480, 1439 } },
                new object[] { true, Hour, new int[] { 0, 5, 10, 20, 40, 59 } },
                new object[] { false, Hour, new int[] { 0, 5, 10, 20, 40, 59 } }
            };

        [Theory]
        [MemberData(nameof(SamePeriodData))]
        public void WhenWritingInSamePeriod_DontCreateNewFile(bool async, RollingFilePeriod period, int[] minutesToAdd)
        {
            using (var temp = TempFolder.Create())
            {
                WriteLogs(async, temp.FolderPath, period, minutesToAdd);

                var files = Directory.GetFiles(temp.FolderPath);
                Assert.Single(files);

                var contents = File.ReadAllLines(files[0]);
                Assert.Equal(minutesToAdd.Length, contents.Length);
            }
        }
        
        /// <summary>
        /// Data for same period tests.
        /// </summary>
        public static IEnumerable<object[]> Test  =>
            new List<object[]>
            {
                new object[] { true, Day },
                new object[] { false, Day },
                new object[] { true, Hour },
                new object[] { false, Hour }
            };
        
        [Theory]
        [MemberData(nameof(Test))]
        public void WhenWritingAndExceedsMaxSize_CreateNewFileCount(bool async, RollingFilePeriod period)
        {
            using (var temp = TempFolder.Create())
            {
                WriteLogs(async, temp.FolderPath, period, 3584);

                var files = Directory.GetFiles(temp.FolderPath);
                Assert.NotEmpty(files);
                Assert.True(files.Length == 4);
            }
        }

        /// <summary>
        /// Writes logs using a sink.
        /// </summary>
        /// <param name="async">Indicates whether the asynchronous sink should be created.</param>
        /// <param name="logFolderPath">Log files folder path.</param>
        /// <param name="period">Rolling file period.</param>
        /// <param name="minutesToAdd">Minutes to add for each writing.</param>
        private void WriteLogs(bool async, string logFolderPath, RollingFilePeriod period, int[] minutesToAdd)
        {
            foreach (var minutes in minutesToAdd)
            {
                var date = BaseDate.AddMinutes(minutes);
                timeProviderMock.Setup(m => m.CurrentDateTime).Returns(date);
                
                var sink = CreateSink(async, logFolderPath, period);
                var entries = new List<ILogEntry>()
                {
                    new LogEntry("date", date.ToString(period.DateFormat))
                };
                    
                sink.Write(GenericLogLevelName.Debug, entries);
                
                ((IDisposable) sink).Dispose();
            }
        }

        /// <summary>
        /// Writes logs using a sink.
        /// </summary>
        /// <param name="async">Indicates whether the asynchronous sink should be created.</param>
        /// <param name="logFolderPath">Log files folder path.</param>
        /// <param name="period">Rolling file period.</param>
        /// <param name="logSize">Size in bytes to be written.</param>
        private void WriteLogs(bool async, string logFolderPath, RollingFilePeriod period, int logSize)
        {
            var count = logSize / ApproximatelyLogEntrySize;
            var date = BaseDate;
            
            for (var fileContent = 0; fileContent < count; fileContent++)
            {
                date = date.AddSeconds(1);
                timeProviderMock.Setup(m => m.CurrentDateTime).Returns(date);
            
                var sink = CreateSink(async, logFolderPath, period);
                var entries = new List<ILogEntry>()
                {
                    new LogEntry("date", date.ToString(period.DateFormat))
                };
                    
                sink.Write(GenericLogLevelName.Info, entries);
                
                ((IDisposable) sink).Dispose();
            }
        }

        /// <summary>
        /// Creates a file sink for tests.
        /// </summary>
        /// <param name="async">Indicates whether the asynchronous sink should be created.</param>
        /// <param name="logFileFolder">Log files folder path.</param>
        /// <param name="period">Rolling file period.</param>
        /// <returns>Created sink.</returns>
        private ILogSink CreateSink(bool async, string logFileFolder, RollingFilePeriod period)
        {
            var formatter = new CompactKeyValueFormatter();
            Func<IDataRenderer> renderer = () => new DefaultDataRenderer();
            var namer = new RollingFileNamer(logFileFolder, period, 1024);
            
            return async
                ? (ILogSink) new AsyncRollingFileSink(formatter, renderer, namer)
                : new RollingFileSink(formatter, renderer, namer);
        }
    }
}
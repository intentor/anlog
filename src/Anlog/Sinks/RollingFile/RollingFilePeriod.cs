using System;

namespace Anlog.Sinks.RollingFile
{
    /// <summary>
    /// Period of creating a rolling file.
    /// </summary>
    public class RollingFilePeriod
    {
        /// <summary>
        /// Format of the file name.
        /// </summary>
        public string DateFormat { get; private set; }

        /// <summary>
        /// Format of the file name.
        /// <para/>
        /// There should be a "{0}" in the place where the <see cref="DateFormat"/> will be replaced.
        /// </summary>
        public string FileNameFormat { get; private set; }

        /// <summary>
        /// File name regex pattern.
        /// <para/>
        /// There should be only a single capture group in the pattern of the date in <see cref="DateFormat"/>.
        /// </summary>
        public string FileNamePattern { get; private set; }

        /// <summary>
        /// Indicates whether the period has been exceeded.
        /// <para/>
        /// The first parameter is the last date and the second the current one.
        /// </summary>
        public Func<DateTime, DateTime, bool> IsPeriodExceeded { get; private set; }

        /// <summary>
        /// Gets the file name for the given period.
        /// </summary>
        /// <param name="date">Date to get the file name from.</param>
        /// <param name="fileCount">File number.</param>
        /// <returns>File name.</returns>
        public string GetFileName(DateTime date, long fileCount)
        {
            return string.Format(FileNameFormat, date.ToString(DateFormat), fileCount);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{nameof(DateFormat)}: {DateFormat}";
        }

        /// <summary>
        /// Generates a new file each day.
        /// </summary>
        public static readonly RollingFilePeriod Day = new RollingFilePeriod()
        {
            DateFormat = "yyyyMMdd",
            FileNameFormat = "log-{0}-{1}.txt",
            FileNamePattern = "log-(\\d{8})(-\\d{1,})?\\.txt",
            IsPeriodExceeded = (lastDate, currentDate) =>
            {
                var last = new DateTime(lastDate.Year, lastDate.Month, lastDate.Day, 0, 0, 0);
                var curent = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 0, 0, 0);
                return (curent - last).TotalDays >= 1;
            }
        };

        /// <summary>
        /// Generates a new file each hour.
        /// </summary>
        public static readonly RollingFilePeriod Hour = new RollingFilePeriod()
        {
            DateFormat = "yyyyMMddHH",
            FileNameFormat = "log-{0}-{1}.txt",
            FileNamePattern = "log-(\\d{10})(-\\d{1,})?\\.txt",
            IsPeriodExceeded = (lastDate, currentDate) =>
            {
                var last = new DateTime(lastDate.Year, lastDate.Month, lastDate.Day, lastDate.Hour, 0, 0);
                var curent = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, currentDate.Hour, 0, 0);
                return (curent - last).TotalHours >= 1;
            }
        };
    }
}
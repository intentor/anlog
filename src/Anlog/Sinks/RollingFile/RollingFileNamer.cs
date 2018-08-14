using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Anlog.Time;

namespace Anlog.Sinks.RollingFile
{
    /// <summary>
    /// Defines rolling file names.
    /// </summary>
    public class RollingFileNamer
    {
        /// <summary>
        /// Default max file size in bytes. The default is 100mb.
        /// </summary>
        public const long DefaultMaxFileSize = 104857600;

        /// <summary>
        /// Log files folder path.
        /// </summary>
        private readonly string logFolderPath;

        /// <summary>
        /// Period for creating a new file.
        /// </summary>
        private readonly RollingFilePeriod period;

        /// <summary>
        /// Max file size in bytes. The default is 100mb.
        /// </summary>
        private readonly long maxFileSize;

        /// <summary>
        /// Date of the last file.
        /// </summary>
        private DateTime lastDate;

        /// <summary>
        /// Log file count.
        /// </summary>
        private int logFileCount = 0;

        /// <summary>
        /// Initializes a new instance of <see cref="RollingFileNamer"/>.
        /// </summary>
        /// <param name="logFolderPath">Log files folder path.</param>
        /// <param name="period">Period for creating a new file.</param>
        /// <param name="maxFileSize">Max file size in bytes. The default is 100mb.</param>
        public RollingFileNamer(string logFolderPath, RollingFilePeriod period, long maxFileSize = DefaultMaxFileSize)
        {
            this.logFolderPath = logFolderPath;
            this.period = period;
            this.maxFileSize = maxFileSize;

            FillLastDateAndLogFileCount();
        }

        /// <summary>
        /// Indicates whether the file should be updated and returns the new file path.
        /// </summary> 
        /// <param name="date">Date/time to evaluate.</param>
        /// <returns>True and the new file path if the file should be updated, otherwise false.</returns>
        public (bool ShouldUpdate, string FilePath) EvaluateFileUpdate(DateTime date)
        {
            var shouldUpdate = false;
            var dateTime = lastDate;

            if (period.IsPeriodExceeded(lastDate, date))
            {
                shouldUpdate = true;
                dateTime = date;
                logFileCount = 1;
            }
            else if (IsSizeLimitExceeded())
            {
                shouldUpdate = true;
                logFileCount++;
            }

            var filePath = Path.Combine(logFolderPath, period.GetFileName(dateTime, logFileCount));
            return (shouldUpdate, Path.Combine(logFolderPath, filePath));
        }

        /// <summary>
        /// Gets the most recent file.
        /// </summary>
        /// <returns>File path of the most recent file.</returns>
        private string GetMostRecentFile()
        {
            var regex = new Regex(period.FileNamePattern);
            return Directory.Exists(logFolderPath)
                ? Directory.GetFiles(logFolderPath)
                    .Where(path => regex.IsMatch(path))
                    .OrderByDescending(path => path)
                    .FirstOrDefault()
                : null;
        }

        /// <summary>
        /// Fills the last date and log file count based on files available in the <see cref="logFolderPath"/>.
        /// </summary>
        private void FillLastDateAndLogFileCount()
        {
            var mostRecentFilePath = GetMostRecentFile();
            if (mostRecentFilePath == null)
            {
                lastDate = TimeProvider.Now;
                logFileCount = 1;
            }
            else
            {
                var fileName = Path.GetFileName(mostRecentFilePath);
                var match = Regex.Match(fileName, period.FileNamePattern);
                lastDate = DateTime.ParseExact(match.Groups[1].Value, period.DateFormat, CultureInfo.InvariantCulture);
                logFileCount = Math.Abs(int.Parse(match.Groups[2].Value));
            }
        }

        /// <summary>
        /// Indicates whether the file size limit has been exceeded.
        /// </summary>
        private bool IsSizeLimitExceeded()
        {
            var mosRecentFile = GetMostRecentFile();
            return mosRecentFile != null && new FileInfo(mosRecentFile).Length >= maxFileSize;
        }
    }
}
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Anlog.Time;
using static Anlog.Sinks.RollingFile.RollingFileExpiryCheck;

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
        /// Period for creating a new file.
        /// </summary>
        private readonly RollingFileExpiryCheck expiryCheck;

        /// <summary>
        /// Date of the last log file.
        /// </summary>
        private DateTime lastDate;

        /// <summary>
        /// Count of the last log file.
        /// </summary>
        private int lastLogCount;

        /// <summary>
        /// Path of the last log file.
        /// </summary>
        private string lastLogPath;

        /// <summary>
        /// Initializes a new instance of <see cref="RollingFileNamer"/>.
        /// </summary>
        /// <param name="logFolderPath">Log files folder path.</param>
        /// <param name="period">Period for creating a new file.</param>
        /// <param name="maxFileSize">Max file size in bytes. The default is 100mb.</param>
        /// <param name="fileExpiryPeriod">File expiry period in days. The default is 0 (never).</param>
        public RollingFileNamer(string logFolderPath, RollingFilePeriod period, long maxFileSize = DefaultMaxFileSize,
            int fileExpiryPeriod = DefaultFileExpiryPeriod)
        {
            this.logFolderPath = logFolderPath;
            this.period = period;
            this.maxFileSize = maxFileSize;

            FillLastDateAndLogFileCount();

            if (fileExpiryPeriod > 0)
            {
                expiryCheck = new RollingFileExpiryCheck(logFolderPath, period, fileExpiryPeriod);
            }
        }

        /// <summary>
        /// Indicates whether the file should be updated and returns the new file path.
        /// </summary> 
        /// <param name="date">Date/time to evaluate.</param>
        /// <returns>True and the new file path if the file should be updated, otherwise false.</returns>
        public (bool ShouldUpdate, string FilePath) EvaluateFileUpdate(DateTime date)
        {
            var shouldUpdate = false;

            if (period.IsPeriodExceeded(lastDate, date))
            {
                shouldUpdate = true;
                lastDate = date;
                lastLogCount = 1;
                lastLogPath = Path.Combine(logFolderPath, period.GetFileName(lastDate, lastLogCount));
            }
            else if (IsSizeLimitExceeded())
            {
                shouldUpdate = true;
                lastLogCount++;
                lastLogPath = Path.Combine(logFolderPath, period.GetFileName(lastDate, lastLogCount));
            }

            return (shouldUpdate, lastLogPath);
        }

        /// <summary>
        /// Indicates whether the file size limit has been exceeded.
        /// </summary>
        private bool IsSizeLimitExceeded()
        {
            return !string.IsNullOrWhiteSpace(lastLogPath) && File.Exists(lastLogPath) &&
                   new FileInfo(lastLogPath).Length >= maxFileSize;
        }

        /// <summary>
        /// Gets the most recent file path.
        /// </summary>
        /// <returns>File path of the most recent file.</returns>
        private string GetMostRecentFilePath()
        {
            if (!Directory.Exists(logFolderPath))
                return null;

            var regex = new Regex(period.FileNamePattern);
            var fullPath = Directory.GetFiles(logFolderPath)
                .Where(path => regex.IsMatch(path))
                .OrderByDescending(path => GetDateAndCountFromLogFileName(path).LogDate)
                .ThenByDescending(path => GetDateAndCountFromLogFileName(path).LogCount)
                .FirstOrDefault();
            return fullPath;
        }

        /// <summary>
        /// Fills the last date and log file count based on files available in the <see cref="logFolderPath"/>.
        /// </summary>
        private void FillLastDateAndLogFileCount()
        {
            var mostRecentFilePath = GetMostRecentFilePath();
            if (string.IsNullOrWhiteSpace(mostRecentFilePath))
            {
                lastDate = TimeProvider.Now;
                lastLogCount = 1;
                lastLogPath = Path.Combine(logFolderPath, period.GetFileName(lastDate, lastLogCount));
            }
            else
            {
                var fileName = Path.GetFileName(mostRecentFilePath);
                lastLogPath = mostRecentFilePath;
                (lastDate, lastLogCount) = GetDateAndCountFromLogFileName(fileName);
            }
        }

        /// <summary>
        /// Reutrn 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>Tuple with </returns>
        private (DateTime LogDate, int LogCount) GetDateAndCountFromLogFileName(string fileName)
        {
            var match = Regex.Match(fileName, period.FileNamePattern);
            var logDate = DateTime.ParseExact(match.Groups[1].Value, period.DateFormat, CultureInfo.InvariantCulture);
            var logCount = Math.Abs(int.Parse(match.Groups[2].Value));
            return (logDate, logCount);
        }
    }
}
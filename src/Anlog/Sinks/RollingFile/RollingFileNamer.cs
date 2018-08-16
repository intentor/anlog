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
        /// Number of the last log file.
        /// </summary>
        private int lastLogNumber;

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

            FillLastDateAndLogFileNumber();

            if (fileExpiryPeriod > 0)
            {
                expiryCheck = new RollingFileExpiryCheck(logFolderPath, period, fileExpiryPeriod);
            }
        }

        /// <summary>
        /// Indicates whether the file should be updated and returns the new file path.
        /// </summary> 
        /// <param name="date">Date/time to evaluate.</param>
        /// <returns>True and the new file path if the file should be updated, otherwise false and the current file path.</returns>
        public (bool ShouldUpdate, string FilePath) EvaluateFileUpdate(DateTime date)
        {
            var shouldUpdate = false;

            if (period.IsPeriodExceeded(lastDate, date))
            {
                shouldUpdate = true;
                lastDate = date;
                lastLogNumber = 1;
                lastLogPath = Path.Combine(logFolderPath, period.GetFileName(lastDate, lastLogNumber));
            }
            else if (IsSizeLimitExceeded())
            {
                shouldUpdate = true;
                lastLogNumber++;
                lastLogPath = Path.Combine(logFolderPath, period.GetFileName(lastDate, lastLogNumber));
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
                .OrderByDescending(path => GetDateAndNumberFromLogFileName(path).LogDate)
                .ThenByDescending(path => GetDateAndNumberFromLogFileName(path).LogNumber)
                .FirstOrDefault();
            return fullPath;
        }

        /// <summary>
        /// Fills the last date and log file number based on files available in the <see cref="logFolderPath"/>.
        /// </summary>
        private void FillLastDateAndLogFileNumber()
        {
            var mostRecentFilePath = GetMostRecentFilePath();
            if (string.IsNullOrWhiteSpace(mostRecentFilePath))
            {
                lastDate = TimeProvider.Now;
                lastLogNumber = 1;
                lastLogPath = Path.Combine(logFolderPath, period.GetFileName(lastDate, lastLogNumber));
            }
            else
            {
                var fileName = Path.GetFileName(mostRecentFilePath);
                lastLogPath = mostRecentFilePath;
                (lastDate, lastLogNumber) = GetDateAndNumberFromLogFileName(fileName);
            }
        }

        /// <summary>
        /// Gets the log file datetime and number based on its name.  
        /// </summary>
        /// <param name="fileName">Log file name.</param>
        /// <returns>Tuple with log file datetime and number.</returns>
        private (DateTime LogDate, int LogNumber) GetDateAndNumberFromLogFileName(string fileName)
        {
            var match = Regex.Match(fileName, period.FileNamePattern);
            var logDate = DateTime.ParseExact(match.Groups[1].Value, period.DateFormat, CultureInfo.InvariantCulture);
            var logNumber = Math.Abs(int.Parse(match.Groups[2].Value));
            return (logDate, logNumber);
        }
    }
}
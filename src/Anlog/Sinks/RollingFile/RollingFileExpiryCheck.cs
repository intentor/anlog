using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Timers;

namespace Anlog.Sinks.RollingFile
{
    /// <summary>
    /// Checks if file was expired and should be removed.
    /// </summary>
    public class RollingFileExpiryCheck : IDisposable
    {
        /// <summary>
        /// Log files folder path.
        /// </summary>
        private readonly string logFolderPath;

        /// <summary>
        /// Rolling file period.
        /// </summary>
        private readonly RollingFilePeriod period;

        /// <summary>
        /// File expiry period in days.
        /// </summary>
        private readonly int fileExpiryPeriod = 0;

        /// <summary>
        /// Default file expiry period in days. The default is 0 (never).
        /// </summary>
        public const int DefaultFileExpiryPeriod = 0;

        /// <summary>
        /// Default expiry check timer interval in milliseconds. The default is 1000 milliseconds.
        /// </summary>
        public const int DefaultExpiryCheckInterval = 1000;

        /// <summary>
        /// File expiry check timer.
        /// </summary>
        private readonly Timer fileExpiryCheckTimer;

        /// <summary>
        /// Initializes a new instance of <see cref="RollingFileExpiryCheck"/>.
        /// </summary>
        /// <param name="logFolderPath">Log files folder path.</param>
        /// <param name="period">Rolling file period.</param>
        /// <param name="fileExpiryPeriod">File expiry period in days. The default is 0 (never).</param>
        /// <param name="expiryCheckInterval">File expiry check timer interval in milliseconds. The default is 1000 milliseconds.</param>
        public RollingFileExpiryCheck(string logFolderPath, RollingFilePeriod period,
            int fileExpiryPeriod = DefaultFileExpiryPeriod, int expiryCheckInterval = DefaultExpiryCheckInterval)
        {
            this.logFolderPath = logFolderPath;
            this.fileExpiryPeriod = fileExpiryPeriod;
            this.period = period;

            fileExpiryCheckTimer = new Timer {Interval = expiryCheckInterval};
            fileExpiryCheckTimer.Elapsed += RollingFileExpiryCheckTimerEvent;
            fileExpiryCheckTimer.Start();
        }

        /// <summary>
        /// Event timer execution of file expiry check timer.
        /// </summary>
        private void RollingFileExpiryCheckTimerEvent(object sender, ElapsedEventArgs e)
        {
            var regex = new Regex(period.FileNamePattern);
            var files = Directory.Exists(logFolderPath)
                ? Directory.GetFiles(logFolderPath)
                    .Where(path => regex.IsMatch(path))
                    .OrderByDescending(path => path)
                    .ToArray()
                : null;

            if (files == null)
                return;

            var curentData = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            for (var fileIndex = files.Length - 1; fileIndex >= 0; fileIndex--)
            {
                var fileName = Path.GetFileName(files[fileIndex]);
                var match = Regex.Match(fileName, period.FileNamePattern);
                var fileDate = DateTime.ParseExact(match.Groups[1].Value, period.DateFormat,
                    CultureInfo.InvariantCulture);

                if ((curentData - fileDate).TotalDays >= fileExpiryPeriod)
                {
                    File.Delete(files[fileIndex]);
                }
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            fileExpiryCheckTimer.Stop();
            fileExpiryCheckTimer.Dispose();
        }
    }
}
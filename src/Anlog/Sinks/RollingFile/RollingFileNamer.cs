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
        /// Log files folder path.
        /// </summary>
        private readonly string logFolderPath;
        
        /// <summary>
        /// Period for creating a new file.
        /// </summary>
        private readonly RollingFilePeriod period;

        /// <summary>
        /// Date of the last file.
        /// </summary>
        private DateTime lastDate;

        /// <summary>
        /// Initializes a new instance of <see cref="RollingFileNamer"/>.
        /// </summary>
        /// <param name="logFolderPath">Log files folder path.</param>
        /// <param name="period">Period for creating a new file.</param>
        public RollingFileNamer(string logFolderPath, RollingFilePeriod period)
        {
            this.logFolderPath = logFolderPath;
            this.period = period;
            
            FillLastDate();
        }

        /// <summary>
        /// Indicates whether the file should be updated.
        /// </summary>
        /// <param name="date">Date/time to evaluate.</param>
        /// <returns>True if the file should be updated, otherwise false.</returns>
        public bool ShouldUpdateFile(DateTime date)
        {
            return period.IsPeriodExceeded(lastDate, date);
        }

        /// <summary>
        /// Gets the file based based on the date.
        /// </summary>
        /// <param name="date">Date/time to evaluate.</param>
        /// <returns>File path.</returns>
        public string GetFilePath(DateTime date)
        {
            var fileName = period.GetFileName(date);
            return Path.Combine(logFolderPath, fileName);
        }

        /// <summary>
        /// Fills the last date based on files available in the <see cref="logFolderPath"/>.
        /// </summary>
        private void FillLastDate()
        {
            var regex = new Regex(period.FileNamePattern);
            var mostRecentFilePath = Directory.GetFiles(logFolderPath)
                .Where(path => regex.IsMatch(path))
                .OrderByDescending(path => path)
                .FirstOrDefault();

            if (mostRecentFilePath == null)
            {
                lastDate = TimeProvider.Now;
            }
            else
            {
                var fileName = Path.GetFileName(mostRecentFilePath);
                var match = Regex.Match(fileName, period.FileNamePattern);
                lastDate = DateTime.ParseExact(match.Groups[1].Value, period.DateFormat, CultureInfo.InvariantCulture);
            }
        }
    }
}
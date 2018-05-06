using System;
using System.IO;

namespace Anlog.Tests.TestObjects
{
    /// <summary>
    /// Represents a temporary folder.
    /// </summary>
    public class TempFolder : IDisposable
    {
        /// <summary>
        /// Temporary folder path.
        /// </summary>
        public string FolderPath { get; private set; }

        /// <inheritdoc />
        public void Dispose()
        {
            Directory.Delete(FolderPath, true);
        }

        /// <summary>
        /// Gets path for a generic log file.
        /// </summary>
        /// <param name="fileName">File name with extension.</param>
        /// <returns>Generic log file path with file name.</returns>
        public string GetLogFilePath(string fileName = null)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = string.Concat(Guid.NewGuid().ToString(), ".txt");
            }
            return Path.Combine(FolderPath, fileName);
        }
        
        /// <summary>
        /// Creates a new temporary folder.
        /// </summary>
        /// <returns>Temporary folder data.</returns>
        public static TempFolder Create()
        {
            var folder = new TempFolder()
            {
                FolderPath = Path.Combine(Directory.GetCurrentDirectory(), Guid.NewGuid().ToString())
            };

            Directory.CreateDirectory(folder.FolderPath);

            return folder;
        }
    }
}
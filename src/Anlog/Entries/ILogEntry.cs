namespace Anlog.Entries
{
    /// <summary>
    /// Represents a log entry.
    /// </summary>
    public interface ILogEntry
    {
        /// <summary>
        /// Log key. If null, indicates an entry with only value.
        /// </summary>
        string Key { get; set; }
    }
}
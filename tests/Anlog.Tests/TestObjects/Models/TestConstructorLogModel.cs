namespace Anlog.Tests.TestObjects.Models
{
    /// <summary>
    /// Writes a log from a constructor.
    /// </summary>
    public sealed class TestConstructorLogModel
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TestConstructorLogModel"/>.
        /// </summary>
        public TestConstructorLogModel()
        {
            Log.Append("key", "value").Info();
        }
    }
}
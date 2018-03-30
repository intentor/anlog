using Anlog.Formatters;
using Moq;
using Xunit;
using static Anlog.Tests.TestObjects.TestConstants;

namespace Anlog.Tests.Formatters
{
    /// <summary>
    /// Tests for <see cref="TypeGettersInfo"/>.
    /// </summary>
    public class TypeGettersInfoTests
    {
        /// <summary>
        /// Log formatter mock.
        /// </summary>
        private Mock<ILogFormatter> mockFormatter = new Mock<ILogFormatter>();

        [Fact]
        public void GivenType_FillGetters()
        {
            var typeInfo = new TypeGettersInfo(TestModelType);
            typeInfo.Append(TestModelInstance, mockFormatter.Object);

            mockFormatter.Verify(m => m.Append("int", TestModelInstance.IntValue));
            mockFormatter.Verify(m => m.Append("double", TestModelInstance.DoubleValue));
            mockFormatter.Verify(m => m.Append("text", (object) TestModelInstance.Text));
            mockFormatter.Verify(m => m.Append("date", TestModelInstance.Date));
            mockFormatter.Verify(m => m.Append("shorts", (object) TestModelInstance.ShortValues));
        }
    }
}
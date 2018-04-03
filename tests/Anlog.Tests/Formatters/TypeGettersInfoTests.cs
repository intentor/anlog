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
        public void GivenType_FillGettersNotIgnored()
        {
            var typeInfo = new TypeGettersInfo(TestModelType);
            typeInfo.Append(TestModelInstance, mockFormatter.Object);

            mockFormatter.Verify(m => m.Append("int", TestModelInstance.IntValue));
            mockFormatter.Verify(m => m.Append("double", TestModelInstance.DoubleValue));
            mockFormatter.Verify(m => m.Append("text", (object) TestModelInstance.Text));
            mockFormatter.Verify(m => m.Append("date", TestModelInstance.Date));
            mockFormatter.Verify(m => m.Append("shorts", (object) TestModelInstance.ShortValues));
        }

        [Fact]
        public void GivenType_DontFillIgnoredGetters()
        {
            var typeInfo = new TypeGettersInfo(TestModelType);
            typeInfo.Append(TestModelInstance, mockFormatter.Object);

            mockFormatter.Verify(m => m.Append("ignore_int", TestModelInstance.IgnoreIntValue), Times.Never);
            mockFormatter.Verify(m => m.Append("ignore_text", TestModelInstance.IgnoreText), Times.Never);
        }
    }
}
using Anlog.Formatters;
using Anlog.Tests.TestObjects;
using Moq;
using Xunit;

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
            var typeInfo = new TypeGettersInfo(TestConstants.TestModelType);
            typeInfo.Append(TestConstants.TestModelInstance, mockFormatter.Object);

            mockFormatter.Verify(m => m.Append("int", TestConstants.TestModelInstance.IntValue));
            mockFormatter.Verify(m => m.Append("double", TestConstants.TestModelInstance.DoubleValue));
            mockFormatter.Verify(m => m.Append("text", (object) TestConstants.TestModelInstance.Text));
            mockFormatter.Verify(m => m.Append("date", TestConstants.TestModelInstance.Date));
            mockFormatter.Verify(m => m.Append("shorts", (object) TestConstants.TestModelInstance.ShortValues));
        }
    }
}
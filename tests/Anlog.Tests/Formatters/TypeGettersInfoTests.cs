using Anlog.Formatters;
using Moq;
using Xunit;
using static Anlog.Tests.TestObjects.TestConstants;

namespace Anlog.Tests.Formatters
{
    /// <summary>
    /// Tests for <see cref="TypeGettersInfo"/>.
    /// </summary>
    public sealed class TypeGettersInfoTests
    {
        /// <summary>
        /// Log formatter mock.
        /// </summary>
        private Mock<ILogFormatter> mockFormatter = new Mock<ILogFormatter>();

        [Fact]
        public void GivenType_FillGettersNotIgnored()
        {
            var typeInfo = new TypeGettersInfo(TestModelType);
            typeInfo.Append(TestDataContractModelInstance, mockFormatter.Object);

            mockFormatter.Verify(m => m.Append("int", TestDataContractModelInstance.IntValue));
            mockFormatter.Verify(m => m.Append("double", TestDataContractModelInstance.DoubleValue));
            mockFormatter.Verify(m => m.Append("text", (object) TestDataContractModelInstance.Text));
            mockFormatter.Verify(m => m.Append("date", TestDataContractModelInstance.Date));
            mockFormatter.Verify(m => m.Append("shorts", (object) TestDataContractModelInstance.ShortValues));
        }

        [Fact]
        public void GivenType_DontFillIgnoredGetters()
        {
            var typeInfo = new TypeGettersInfo(TestModelType);
            typeInfo.Append(TestDataContractModelInstance, mockFormatter.Object);

            mockFormatter.Verify(m => m.Append("ignore_int", TestDataContractModelInstance.IgnoreIntValue), Times.Never);
            mockFormatter.Verify(m => m.Append("ignore_text", TestDataContractModelInstance.IgnoreText), Times.Never);
        }
    }
}
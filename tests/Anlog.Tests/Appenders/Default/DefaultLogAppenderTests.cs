using System;
using System.Collections.Generic;
using Anlog.Appenders.Default;
using Anlog.Entries;
using Anlog.Formatters.CompactKeyValue;
using Anlog.Renderers;
using Moq;
using Xunit;
using static Anlog.Tests.TestObjects.TestConstants;

namespace Anlog.Tests.Appenders.Default
{
    /// <summary>
    /// Tests for <see cref="DefaultLogAppender"/>
    /// </summary>
    public class DefaultLogAppenderTests
    {
        /// <summary>
        /// Object to test.
        /// </summary>
        private readonly DefaultLogAppender appender;

        /// <summary>
        /// Log written from the appender.
        /// </summary>
        private string log;
        
        public DefaultLogAppenderTests()
        {
            var writer = new Mock<ILogWriter>();
            writer.Setup(m => m.Write(It.IsAny<LogLevelName>(), It.IsAny<List<ILogEntry>>()))
                .Callback<LogLevelName, List<ILogEntry>>((level, entries) =>
                {
                    var formatter = new CompactKeyValueFormatter();
                    log = formatter.Format(level, entries, new DefaultDataRenderer());
                });
            
            appender = new DefaultLogAppender(writer.Object, false, "class", "member", 0);
        }
        
        /// <summary>
        /// Log append data used in tests.
        /// </summary>
        public static IEnumerable<object[]> LogAppendData  =>
            new List<object[]>
            {
                new object[] { "key", null, "key=null" },
                new object[] { "key", string.Empty, "key=" },
                new object[] { TestString.Key, TestString.Value, "string=value" },
                new object[] { TestShort.Key, TestShort.Value, "short=24" },
                new object[] { TestInt.Key, TestInt.Value, "int=69" },
                new object[] { TestLong.Key, TestLong.Value, "long=666" },
                new object[] { TestFloat.Key, TestFloat.Value, "float=24.11" },
                new object[] { TestDouble.Key, TestDouble.Value, "double=69.11" },
                new object[] { TestDecimal.Key, TestDecimal.Value, "decimal=666.11" },
                new object[] { TestDateTime.Key, TestDateTime.Value, "date=2018-03-25 23:00:00.000" },
                new object[] { TestObject.Key, TestObject.Value, "obj={int=24 double=666.11 text=LogTest date=2018-03-25 23:00:00.000 shorts=[1,2,3,4,5]}" },
                new object[] { TestEnum.Key, TestEnum.Value, "enum=Sunday" },
                new object[] { TestArrayInt.Key, TestArrayInt.Value, "arrInt=[11,24,69,666]" },
                new object[] { TestArrayDouble.Key, TestArrayDouble.Value, "arrDouble=[11.1,24.2,69.3,666.4]" },
                new object[] { TestArrayObject.Key, TestArrayObject.Value, "arrObj=[{int=24 double=666.11 text=LogTest date=2018-03-25 23:00:00.000 shorts=[1,2,3,4,5]},{int=24 double=666.11 text=LogTest date=2018-03-25 23:00:00.000 shorts=[1,2,3,4,5]},{int=24 double=666.11 text=LogTest date=2018-03-25 23:00:00.000 shorts=[1,2,3,4,5]}]" },
                new object[] { TestEmptyArray.Key, TestEmptyArray.Value, "emptyArrObj=[]" },
                new object[] { TestEnumerableInt.Key, TestEnumerableInt.Value, "listInt=[11,24,69,666]" },
                new object[] { TestEnumerableDouble.Key, TestEnumerableDouble.Value, "listDouble=[11.1,24.2,69.3,666.4]" },
                new object[] { TestEnumerableObject.Key, TestEnumerableObject.Value, "listObj=[{int=24 double=666.11 text=LogTest date=2018-03-25 23:00:00.000 shorts=[1,2,3,4,5]},{int=24 double=666.11 text=LogTest date=2018-03-25 23:00:00.000 shorts=[1,2,3,4,5]},{int=24 double=666.11 text=LogTest date=2018-03-25 23:00:00.000 shorts=[1,2,3,4,5]}]" },
                new object[] { TestEmptyEnumerable.Key, TestEmptyEnumerable.Value, "emptyListObj=[]" },
                new object[] { null, TestObject.Value, "{int=24 double=666.11 text=LogTest date=2018-03-25 23:00:00.000 shorts=[1,2,3,4,5]}" },
                new object[] { null, TestNonDataContractModelInstance, "{int=69 double=24.11 text=LogTest date=2018-03-25 23:00:00.000 shorts=[1,2,3,4,5]}" },
                new object[] { null, "string", "string" },
                new object[] { null, "", "" }
            };

        [Theory]
        [MemberData(nameof(LogAppendData))]
        public void WhenAppending_WritesCompactKeyValue(string key, object value, string expected)
        {
            appender.Append(key, value).Info();
            
            Assert.Equal(expected, log.Substring(45));
        }

        [Theory]
        [InlineData("Test {0:000}", 11, "d=Test 011", "[DBG]", LogLevel.Debug)]
        [InlineData("Test {0:000}", null, "d=Test {0:000}", "[DBG]", LogLevel.Debug)]
        [InlineData("{0}:Test", "24", "i=24:Test", "[INF]", LogLevel.Info)]
        [InlineData("{0}:Test", null, "i={0}:Test", "[INF]", LogLevel.Info)]
        [InlineData("Abc{0:0.0}", 1.234, "w=Abc1.2", "[WRN]", LogLevel.Warn)]
        [InlineData("Abc{0:0.0}", null, "w=Abc{0:0.0}", "[WRN]", LogLevel.Warn)]
        [InlineData("Err{0}Val", 666, "e=Err666Val", "[ERR]", LogLevel.Error)]
        [InlineData("Err{0}Val", null, "e=Err{0}Val", "[ERR]", LogLevel.Error)]
        public void WhenAppending_PerformMessageFormatting(string format, object value, string expectedLog, 
            string expectedLevel, LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    if (value == null)
                    {
                        appender.Debug(format);
                    }
                    else
                    {
                        appender.Debug(format, value);
                    }
                    break;
                
                case LogLevel.Info:
                    if (value == null)
                    {
                        appender.Info(format);
                    }
                    else
                    {
                        appender.Info(format, value);
                    }
                    break;
                
                case LogLevel.Warn:
                    if (value == null)
                    {
                        appender.Warn(format);
                    }
                    else
                    {
                        appender.Warn(format, value);
                    }
                    break;
                
                case LogLevel.Error:
                    if (value == null)
                    {
                        appender.Error(format);
                    }
                    else
                    {
                        appender.Error(format, value);
                    }
                    break;
            }
            
            Assert.Equal(expectedLevel, log.Substring(24, 5));
            Assert.Equal(expectedLog, log.Substring(45));
        }
        
        [Fact]
        public void WhenLoggingExceptionWithMessage_WritesCompactKeyValue()
        {
            appender.Error(new ArgumentException("Param invalid", "Param"), "Some error message");
            
            Assert.Equal("[ERR]", log.Substring(24, 5));
            Assert.Equal(" e=Some error message\nSystem.ArgumentException: Param invalid\nParameter name: Param", 
                log.Substring(44));
        }
        
        [Fact]
        public void WhenLoggingExceptionWithoutMessage_WritesCompactKeyValue()
        {
            appender.Error(new ArgumentException("Param invalid", "Param"));
            
            Assert.Equal("[ERR]", log.Substring(24, 5));
            Assert.Equal("\nSystem.ArgumentException: Param invalid\nParameter name: Param", log.Substring(44));
        }
        
        [Fact]
        public void WhenLoggingExceptionWithStackTrace_WritesWithStacktrace()
        {
            try
            {
                // Throw an exception to generate stack trace.
                throw new ArgumentException("Param invalid", "Param");
            }
            catch (Exception e)
            {
                appender.Error(e, "Some error message");
            }
            var l = " e=Some error message\nSystem.ArgumentException: Param invalid\nParameter name: Param\n   at Anlog.Tests.Formatters.CompactKeyValueFormatterTests.WhenLoggingExceptionWithStackTrace_WritesWithStacktrace()".Length;
            
            Assert.Equal("[ERR]", log.Substring(24, 5));
            Assert.True(log.Length > 246);
            Assert.Equal(" e=Some error message\nSystem.ArgumentException: Param invalid\n" 
                + "Parameter name: Param\n   at Anlog.Tests.Appenders.Default.DefaultLogAppenderTests."
                + "WhenLoggingExceptionWithStackTrace_WritesWithStacktrace()", 
                log.Substring(44, 201));
        }
        
        [Fact]
        public void WhenLoggingExceptionWithNoStackTrace_WritesWithNoStacktrace()
        {
            appender.Error(new ArgumentException("Param invalid", "Param"), "Some error message");
            
            Assert.Equal("[ERR]", log.Substring(24, 5));
            Assert.Equal(" e=Some error message\nSystem.ArgumentException: Param invalid\nParameter name: Param", 
                log.Substring(44));
        }
    }
}
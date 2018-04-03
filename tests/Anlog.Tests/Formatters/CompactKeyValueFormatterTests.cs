using System.Collections.Generic;
using Anlog.Formatters;
using Anlog.Formatters.CompactKeyValue;
using Anlog.Sinks.InMemory;
using Xunit;
using static Anlog.Tests.TestObjects.TestConstants;

namespace Anlog.Tests.Formatters
{
    /// <summary>
    /// Tests for <see cref="CompactKeyValueFormatter"/>
    /// </summary>
    public class CompactKeyValueFormatterTests
    {
        /// <summary>
        /// Prefix for the log displayed in tests.
        /// </summary>
        private const string LogPrefix = "[INF] c=Class.Test:666 ";
        
        /// <summary>
        /// Object to test.
        /// </summary>
        private CompactKeyValueFormatter formatter;

        /// <summary>
        /// Basic in memory sink.
        /// </summary>
        private InMemorySink sink;

        public CompactKeyValueFormatterTests()
        {
            sink = new InMemorySink();
            CompactKeyValueFormatterFactory.PrepareFormatter();
            formatter = new CompactKeyValueFormatter(sink, "/opt/test/Class.cs", "Test", 666);
        }
        
        /// <summary>
        /// Log data used in tests.
        /// </summary>
        public static IEnumerable<object[]> LogData  =>
            new List<object[]>
            {
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
                new object[] { null, TestObject.Value, "int=24 double=666.11 text=LogTest date=2018-03-25 23:00:00.000 shorts=[1,2,3,4,5]" }
            };

        [Theory]
        [MemberData(nameof(LogData))]
        public void WhenAppending_WritesCompactKeyValue(string key, object value, string expected)
        {
            if (key == null)
            {
                formatter.Append(value).Info();
            }
            else
            {
                formatter.Append(key, value).Info();
            }

            var log = sink.GetLogs();
            
            Assert.Equal(LogPrefix + expected, log.Substring(24));
        }
    }
}
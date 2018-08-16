using System.IO;
using System.Runtime.CompilerServices;
using Anlog.Performance.Tests.TestObjects;
using Xunit.Abstractions;
using static Anlog.Tests.TestObjects.TestConstants;

namespace Anlog.Performance.Tests
{
    /// <summary>
    /// A log test with complex logging details in different logging tools.
    /// </summary>
    public sealed class ComplexLogPerformanceTests : PerformanceTest
    {
        public ComplexLogPerformanceTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        protected override long TestAnlogLoggerWithCaller()
        {
            return MeasureExecution(() =>
            {
                Log.Append(TestString.Key, TestString.Value)
                    .Append(TestShort.Key, TestShort.Value)
                    .Append(TestInt.Key, TestInt.Value)
                    .Append(TestLong.Key, TestLong.Value)
                    .Append(TestFloat.Key, TestFloat.Value)
                    .Append(TestDouble.Key, TestDouble.Value)
                    .Append(TestDecimal.Key, TestDecimal.Value)
                    .Append(TestDateTime.Key, TestDateTime.Value)
                    .Append(TestObject.Key, TestObject.Value)
                    .Append(TestEnum.Key, TestEnum.Value)
                    .Append(TestArrayInt.Key, TestArrayInt.Value)
                    .Append(TestArrayDouble.Key, TestArrayDouble.Value)
                    .Append(TestArrayObject.Key, TestArrayObject.Value)
                    .Append(TestEmptyArray.Key, TestEmptyArray.Value)
                    .Append(TestEnumerableInt.Key, TestEnumerableInt.Value)
                    .Append(TestEnumerableDouble.Key, TestEnumerableDouble.Value)
                    .Append(TestEnumerableObject.Key, TestEnumerableObject.Value)
                    .Append(TestEmptyEnumerable.Key, TestEmptyEnumerable.Value)
                    .Info();
            });
        }

        protected override long TestSerilogWithCaller(
            [CallerFilePath] string filePath = null,
            [CallerMemberName] string caller = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            return MeasureExecution(() =>
            {
                var callerName = "<unknown>";
                if (filePath != null)
                {
                    callerName = string.Concat(Path.GetFileNameWithoutExtension(filePath), ".", caller, ":", 
                        lineNumber);
                }

                Serilog.Log.Information("c={caller},{key1}={value1},{key2}={value2},{key3}={value3},{key4}={value4},"
                    + "{key5}={value5},{key6}={value6},{key7}={value7},{key8}={value8},{key9}={@value9},"
                    + "{key10}={@value10},{key11}={@value11},{key12}={@value12},{key13}={@value13},{key14}={@value14},"
                    + "{key15}={@value15},{key16}={@value16},{key17}={@value17},{key18}={@value18}", callerName, 
                    TestString.Key, TestString.Value,
                    TestShort.Key, TestShort.Value,
                    TestInt.Key, TestInt.Value,
                    TestLong.Key, TestLong.Value,
                    TestFloat.Key, TestFloat.Value,
                    TestDouble.Key, TestDouble.Value,
                    TestDecimal.Key, TestDecimal.Value,
                    TestDateTime.Key, TestDateTime.Value,
                    TestObject.Key, TestObject.Value,
                    TestEnum.Key, TestEnum.Value,
                    TestArrayInt.Key, TestArrayInt.Value,
                    TestArrayDouble.Key, TestArrayDouble.Value,
                    TestArrayObject.Key, TestArrayObject.Value,
                    TestEmptyArray.Key, TestEmptyArray.Value,
                    TestEnumerableInt.Key, TestEnumerableInt.Value,
                    TestEnumerableDouble.Key, TestEnumerableDouble.Value,
                    TestEnumerableObject.Key, TestEnumerableObject.Value,
                    TestEmptyEnumerable.Key, TestEmptyEnumerable.Value);
            });
        }

        protected override long TestSerilogWithoutCaller()
        {
            return MeasureExecution(() =>
            {
                Serilog.Log.Information("c=unknown,{key1}={value1},{key2}={value2},{key3}={value3},{key4}={value4},"
                    + "{key5}={value5},{key6}={value6},{key7}={value7},{key8}={value8},{key9}={@value9},"
                    + "{key10}={@value10},{key11}={@value11},{key12}={@value12},{key13}={@value13},{key14}={@value14},"
                    + "{key15}={@value15},{key16}={@value16}", 
                    TestString.Key, TestString.Value,
                    TestShort.Key, TestShort.Value,
                    TestInt.Key, TestInt.Value,
                    TestLong.Key, TestLong.Value,
                    TestFloat.Key, TestFloat.Value,
                    TestDouble.Key, TestDouble.Value,
                    TestDecimal.Key, TestDecimal.Value,
                    TestDateTime.Key, TestDateTime.Value,
                    TestObject.Key, TestObject.Value,
                    TestEnum.Key, TestEnum.Value,
                    TestArrayInt.Key, TestArrayInt.Value,
                    TestArrayDouble.Key, TestArrayDouble.Value,
                    TestArrayObject.Key, TestArrayObject.Value,
                    TestEnumerableInt.Key, TestEnumerableInt.Value,
                    TestEnumerableDouble.Key, TestEnumerableDouble.Value,
                    TestEnumerableObject.Key, TestEnumerableObject.Value);
            });
        }

        protected override long TestSavingToFileWithCaller(
            [CallerFilePath] string filePath = null,
            [CallerMemberName] string caller = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            return 0;
        }
    }
}
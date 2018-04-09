using System;
using System.Runtime.Serialization;

namespace Anlog.Tests.TestObjects
{
    /// <summary>
    /// A test value object.
    /// </summary>
    [DataContract]
    public sealed class TestModel
    {
        /// <summary>
        /// Some int value to be ignored when writing logs.
        /// </summary>
        [DataMember(Name = "ignore_int")] 
        [LogIgnore]
        public int IgnoreIntValue;
        
        /// <summary>
        /// Some int value.
        /// </summary>
        [DataMember(Name = "int")] 
        public int IntValue;

        /// <summary>
        /// Some double value.
        /// </summary>
        [DataMember(Name = "double")] 
        public double DoubleValue;
        
        /// <summary>
        /// Some text value to be ignored when writing logs.
        /// </summary>
        [DataMember(Name = "ignore_text")] 
        [LogIgnore]
        public int IgnoreText { get; set; }
        
        /// <summary>
        /// Some text value.
        /// </summary>
        [DataMember(Name = "text")]
        public string Text { get; set; }
        
        /// <summary>
        /// Some decimal value.
        /// </summary>
        [DataMember(Name = "date")]
        public DateTime Date { get; set; }
        
        /// <summary>
        /// Some short values.
        /// </summary>
        [DataMember(Name = "shorts")]
        public short[] ShortValues { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="TestModel"/>.
        /// </summary>
        public TestModel()
        {
            var random = new Random();

            IntValue = random.Next();
            DoubleValue = random.NextDouble();
            Text = Guid.NewGuid().ToString();
            Date = DateTime.Now;
            ShortValues = new short[] { 1, 2, 3, 4, 5 };
        }
    }
}
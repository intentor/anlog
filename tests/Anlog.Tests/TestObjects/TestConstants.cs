using System;
using System.Collections.Generic;

namespace Anlog.Tests.TestObjects
{
    /// <summary>
    /// Test constants.
    /// </summary>
    public static class TestConstants
    {
        /// <summary>
        /// Test model type.
        /// </summary>
        public static readonly Type TestModelType = typeof(TestModel);

        /// <summary>
        /// Test model instance.
        /// </summary>
        public static readonly TestModel TestModelInstance = new TestModel()
        {
            IntValue = 24,
            DoubleValue = 666.11d,
            Text = "LogTest",
            Date = new DateTime(2018, 3, 25, 23, 0, 0, 0)
        };

        /// <summary>
        /// Generic log text.
        /// </summary>
        public const string GenericLog = "2018-03-28 00:13 [INF] key=value";
        
        /// <summary>
        /// Test string key/value pair.
        /// </summary>
        public static readonly KeyValuePair<string, string> TestString = 
            new KeyValuePair<string, string>("string", "value");
        
        /// <summary>
        /// Test short key/value pair.
        /// </summary>
        public static readonly KeyValuePair<string, short> TestShort = 
            new KeyValuePair<string, short>("short", 24);
        
        /// <summary>
        /// Test int key/value pair.
        /// </summary>
        public static readonly KeyValuePair<string, int> TestInt = 
            new KeyValuePair<string, int>("int", 69);
        
        /// <summary>
        /// Test long key/value pair.
        /// </summary>
        public static readonly KeyValuePair<string, long> TestLong = 
            new KeyValuePair<string, long>("long", 666);
        
        /// <summary>
        /// Test float key/value pair.
        /// </summary>
        public static readonly KeyValuePair<string, float> TestFloat = 
            new KeyValuePair<string, float>("float", 24.11f);
        
        /// <summary>
        /// Test double key/value pair.
        /// </summary>
        public static readonly KeyValuePair<string, double> TestDouble = 
            new KeyValuePair<string, double>("double", 69.11);
        
        /// <summary>
        /// Test decimal key/value pair.
        /// </summary>
        public static readonly KeyValuePair<string, decimal> TestDecimal = 
            new KeyValuePair<string, decimal>("decimal", 666.11M);
        
        /// <summary>
        /// Test date/time key/value pair.
        /// </summary>
        public static readonly KeyValuePair<string, DateTime> TestDateTime = 
            new KeyValuePair<string, DateTime>("date", new DateTime(2018, 3, 25, 23, 0, 0, 0));
        
        /// <summary>
        /// Test object key/value pair.
        /// </summary>
        public static readonly KeyValuePair<string, TestModel> TestObject = 
            new KeyValuePair<string, TestModel>("obj", TestModelInstance);
        
        /// <summary>
        /// Test enumeration key/value pair.
        /// </summary>
        public static readonly KeyValuePair<string, DayOfWeek> TestEnum = 
        new KeyValuePair<string, DayOfWeek>("enum", DayOfWeek.Sunday);
        
        /// <summary>
        /// Test int array key/value pair.
        /// </summary>
        public static readonly KeyValuePair<string, int[]> TestArrayInt = 
            new KeyValuePair<string, int[]>("arrInt", 
                new int[] { 11, 24, 69, 666 });
        
        /// <summary>
        /// Test double array key/value pair.
        /// </summary>
        public static readonly KeyValuePair<string, double[]> TestArrayDouble = 
            new KeyValuePair<string, double[]>("arrDouble", 
                new double[] { 11.1d, 24.2d, 69.3d, 666.4d });
        
        /// <summary>
        /// Test object array key/value pair.
        /// </summary>
        public static readonly KeyValuePair<string, TestModel[]> TestArrayObject = 
            new KeyValuePair<string, TestModel[]>("arrObj", 
                new TestModel[] { TestModelInstance, TestModelInstance, TestModelInstance });
        
        /// <summary>
        /// Test object empty array key/value pair.
        /// </summary>
        public static readonly KeyValuePair<string, TestModel[]> TestEmptyArray = 
            new KeyValuePair<string, TestModel[]>("emptyArrObj", new TestModel[0]);
        
        /// <summary>
        /// Test int enumeration key/value pair.
        /// </summary>
        public static readonly KeyValuePair<string, IEnumerable<int>> TestEnumerableInt = 
            new KeyValuePair<string, IEnumerable<int>>("listInt", 
                new List<int>() { 11, 24, 69, 666 });
        
        /// <summary>
        /// Test double enumeration key/value pair.
        /// </summary>
        public static readonly KeyValuePair<string, IEnumerable<double>> TestEnumerableDouble = 
            new KeyValuePair<string, IEnumerable<double>>("listDouble", 
                new List<double>() { 11.1d, 24.2d, 69.3d, 666.4d});
        
        /// <summary>
        /// Test object enumeration key/value pair.
        /// </summary>
        public static readonly KeyValuePair<string, IEnumerable<TestModel>> TestEnumerableObject = 
            new KeyValuePair<string, IEnumerable<TestModel>>("listObj", 
                new List<TestModel>() { TestModelInstance, TestModelInstance, TestModelInstance });
        
        /// <summary>
        /// Test object empty enumeration key/value pair.
        /// </summary>
        public static readonly KeyValuePair<string, IEnumerable<TestModel>> TestEmptyEnumerable = 
            new KeyValuePair<string, IEnumerable<TestModel>>("emptyListObj", new List<TestModel>());
    }
}
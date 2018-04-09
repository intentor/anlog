using System;
using System.Collections.Generic;
using Anlog.Tests.TestObjects.Models;

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
        public static readonly Type TestModelType = typeof(TestDataContractModel);

        /// <summary>
        /// Test model instance with data contract attribute.
        /// </summary>
        public static readonly TestDataContractModel TestDataContractModelInstance = new TestDataContractModel()
        {
            IntValue = 24,
            DoubleValue = 666.11d,
            Text = "LogTest",
            Date = new DateTime(2018, 3, 25, 23, 0, 0, 0)
        };
        
        /// <summary>
        /// Test model instance without data contract attribute.
        /// </summary>
        public static readonly TestDataContractModel TestNonDataContractModelInstance = new TestDataContractModel()
        {
            IntValue = 69,
            DoubleValue = 24.11d,
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
        public static readonly KeyValuePair<string, TestDataContractModel> TestObject = 
            new KeyValuePair<string, TestDataContractModel>("obj", TestDataContractModelInstance);
        
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
        public static readonly KeyValuePair<string, TestDataContractModel[]> TestArrayObject = 
            new KeyValuePair<string, TestDataContractModel[]>("arrObj", 
                new TestDataContractModel[] { TestDataContractModelInstance, TestDataContractModelInstance, TestDataContractModelInstance });
        
        /// <summary>
        /// Test object empty array key/value pair.
        /// </summary>
        public static readonly KeyValuePair<string, TestDataContractModel[]> TestEmptyArray = 
            new KeyValuePair<string, TestDataContractModel[]>("emptyArrObj", new TestDataContractModel[0]);
        
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
        public static readonly KeyValuePair<string, IEnumerable<TestDataContractModel>> TestEnumerableObject = 
            new KeyValuePair<string, IEnumerable<TestDataContractModel>>("listObj", 
                new List<TestDataContractModel>() { TestDataContractModelInstance, TestDataContractModelInstance, TestDataContractModelInstance });
        
        /// <summary>
        /// Test object empty enumeration key/value pair.
        /// </summary>
        public static readonly KeyValuePair<string, IEnumerable<TestDataContractModel>> TestEmptyEnumerable = 
            new KeyValuePair<string, IEnumerable<TestDataContractModel>>("emptyListObj", new List<TestDataContractModel>());
    }
}
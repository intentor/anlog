using System;

namespace Anlog.Formatters.CompactKeyValue
{
    /// <summary>
    /// Constants for the <see cref="CompactKeyValueFormatter"/>.
    /// </summary>
    internal static class CompactKeyValueFormatterConstants
    {
        /// <summary>
        /// String type.
        /// </summary>
        internal static readonly Type StringType = typeof(string);
        
        /// <summary>
        /// Log entries separator.
        /// </summary>
        internal const string EntrySeparator = " ";
        
        /// <summary>
        /// Key/value pair separator.
        /// </summary>
        internal const string KeyValueSeparator = "=";
        
        /// <summary>
        /// List items separator.
        /// </summary>
        internal const string ListItemSeparator = ",";
        
        /// <summary>
        /// List opening symbol.
        /// </summary>
        internal const string ListOpening = "[";
        
        /// <summary>
        /// List closing symbol.
        /// </summary>
        internal const string ListClosing = "]";
        
        /// <summary>
        /// Empty list value.
        /// </summary>
        internal const string EmptyList = "[]";

        /// <summary>
        /// Object opening symbol.
        /// </summary>
        internal const string ObjectOpening = "{";
        
        /// <summary>
        /// Object closing symbol.
        /// </summary>
        internal const string ObjectClosing = "}";
        
        /// <summary>
        /// Caller key constant.
        /// </summary>
        internal const string CallerKey = "c";
        
        /// <summary>
        /// Unknown caller value.
        /// </summary>
        internal const string UnknownCallerValue = "unknowm";
        
        /// <summary>
        /// Separator for caller members.
        /// </summary>
        internal const string CallerMembersSeparator = ".";
        
        /// <summary>
        /// Separator of caller and line numbers.
        /// </summary>
        internal const string CallerLineNumberSeparator = ":";
        
        /// <summary>
        /// Constructor caller name when receiving as input.
        /// </summary>
        internal const string ConstructorCallerInputName = ".ctor";

        /// <summary>
        /// Constructor name to output.
        /// </summary>
        internal const string ConstructorCallerOutputName = "Constructor";

        /// <summary>
        /// Null value text.
        /// </summary>
        internal const string NullValue = "null";

        /// <summary>
        /// Empty value text.
        /// </summary>
        internal const string EmptyValue = "";

        /// <summary>
        /// Generic key for a value that there's no key.
        /// </summary>
        internal const string GenericValueKey = "value";

        /// <summary>
        /// Log levels details.
        /// </summary>
        internal static readonly ILogLevelName LogLevelName = new CompactKeyValueLogLevelName();
    }
}
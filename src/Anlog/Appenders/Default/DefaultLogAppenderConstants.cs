using Anlog.Formatters;
using Anlog.Formatters.CompactKeyValue;

namespace Anlog.Appenders.Default
{
    /// <summary>
    /// Constants for the <see cref="DefaultLogAppender"/>.
    /// </summary>
    public static class DefaultLogAppenderConstants
    {
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
    }
}
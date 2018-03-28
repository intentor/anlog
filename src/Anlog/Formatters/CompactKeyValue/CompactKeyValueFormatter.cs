using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using static Anlog.Formatters.CompactKeyValue.CompactKeyValueFormatterConstants;

namespace Anlog.Formatters.CompactKeyValue
{
    /// <summary>
    /// Formats the output as a compact key/value pair.
    /// </summary>
    public class CompactKeyValueFormatter : ILogFormatter
    {
        /// <summary>
        /// Date/time format.
        /// </summary>
        internal static string DateTimeFormat { get; set; } = "yyyy-MM-dd HH:mm:ss.fff";
        
        /// <summary>
        /// Culture details.
        /// </summary>
        internal static CultureInfo Culture { get; set; } = CultureInfo.InvariantCulture;

        /// <summary>
        /// Available Getters.
        /// </summary>
        internal static Dictionary<Type, TypeGettersInfo> Getters { get; set; }
        
        /// <summary>
        /// Sink to write the log to.
        /// </summary>
        private ILogSink sink;

        /// <summary>
        /// String builder used to write logs.
        /// </summary>
        private StringBuilder builder;

        /// <summary>
        /// Initilizaes a new instance of <see cref="CompactKeyValueFormatter"/>.
        /// </summary>
        /// <param name="sink">Logger sinker.</param>
        /// <param name="callerFilePath">caller class file path that originated the log.</param>
        /// <param name="callerMemberName">caller class member name that originated the log.</param>
        /// <param name="callerLineNumber">caller line number that originated the log.</param>
        public CompactKeyValueFormatter(ILogSink sink, string callerFilePath, 
            string callerMemberName, int callerLineNumber)
        {
            this.sink = sink;
            builder = new StringBuilder();
            
            var caller = UnknownCallerValue;
            if (!string.IsNullOrEmpty(callerFilePath))
            {
                caller = string.Concat(Path.GetFileNameWithoutExtension(callerFilePath), CallerMembersSeparator, 
                    callerMemberName, CallerLineNumberSeparator, callerLineNumber);
            }
            
            WriteKey(CallerKey);
            WriteValue(caller);
        }
        
        /// <inheritdoc />
        public ILogFormatter Append(string key, string value)
        {
            WriteKey(key);
            WriteValue(value);
            return this;
        }
        
        /// <inheritdoc />
        public ILogFormatter Append(string key, object value)
        {
            WriteKey(key);
            WriteValue(value);
            return this;
        }
        
        /// <inheritdoc />
        public ILogFormatter Append<T>(string key, T[] values)
        {
            if (values == null || values.Length == 0)
            {
                return Append(key, EmptyList);
            }
            
            builder.Append(string.Concat(key, KeyValueSeparator, ListOpening));

            for (var index = 0; index < values.Length; index++)
            {
                WriteValue(values[index], false);
                builder.Append(ListItemSeparator);
            }

            // Removes the last comma.
            builder.Length--;
            builder.Append(string.Concat(ListClosing, EntrySeparator));
            
            return this;
        }
        
        /// <inheritdoc />
        public ILogFormatter Append<T>(string key, IEnumerable<T> values)
        {
            builder.Append(string.Concat(key, KeyValueSeparator, ListOpening));

            if (values != null)
            {
                var hasValues = false;
                foreach (var value in values)
                {
                    hasValues = true;
                    WriteValue(value, false);
                    builder.Append(ListItemSeparator);
                }

                if (hasValues)
                {
                    builder.Length--; // Removes the last comma.
                }
            }
            
            builder.Append(string.Concat(ListClosing, EntrySeparator));
            
            return this;
        }

        /// <inheritdoc />
        public void Debug(string message = null)
        {
            Write(LogLevels.Debug, message, null);
        }

        /// <inheritdoc />
        public void Info(string message = null)
        {
            Write(LogLevels.Info, message, null);
        }

        /// <inheritdoc />
        public void Warning(string message = null)
        {
            Write(LogLevels.Warning, message, null);
        }

        /// <inheritdoc />
        public void Error(string message = null)
        {
            Write(LogLevels.Error, message, null);
        }

        /// <inheritdoc />
        public void Error(Exception e)
        {
            Write(LogLevels.Error, null, e);
        }

        /// <inheritdoc />
        public void Error(string message, Exception e)
        {
            Write(LogLevels.Error, message, e);
        }

        /// <summary>
        /// Writes a key.
        /// </summary>
        /// <param name="key">Key to write.</param>
        private void WriteKey(string key)
        {
            builder.Append(string.Concat(key, KeyValueSeparator));
        }

        /// <summary>
        /// Writes a value.
        /// </summary>
        /// <param name="value">Value write.</param>
        /// <param name="writeEntrySeparator">True to write entry separator, otherwise false.</param>
        private void WriteValue(string value, bool writeEntrySeparator = true)
        {
            builder.Append(value);

            if (writeEntrySeparator)
            {
                builder.Append(EntrySeparator);
            }
        }
        
        /// <summary>
        /// Writes a value.
        /// </summary>
        /// <param name="value">Value write.</param>
        /// <param name="writeEntrySeparator">True to write entry separator, otherwise false.</param>
        private void WriteValue(object value, bool writeEntrySeparator = true)
        {
            if (value != null)
            {
                if (value is DateTime)
                {
                    WriteValue(((DateTime) value).ToString(DateTimeFormat), writeEntrySeparator);
                } 
                else if (value is IConvertible)
                {
                    WriteValue(((IConvertible) value).ToString(Culture), writeEntrySeparator);
                }
                else if (value is IEnumerable)
                {
                    var values = (IEnumerable) value;
                    builder.Append(ListOpening);

                    var hasValues = false;
                    foreach (var item in values)
                    {
                        hasValues = true;
                        WriteValue(item, false);
                        builder.Append(ListItemSeparator);
                    }

                    if (hasValues)
                    {
                        builder.Length--; // Removes the last comma.
                    }
            
                    WriteValue(ListClosing, writeEntrySeparator);
                }
                else if (Getters.ContainsKey(value.GetType()))
                {
                    builder.Append(ObjectOpening);
                    Getters[value.GetType()].Append(value, this);
                    builder.Length--; // Removes the last space.
                    WriteValue(ObjectClosing, writeEntrySeparator);
                }
                else
                {
                    WriteValue(value.ToString(), writeEntrySeparator);
                }
            }
        }

        /// <summary>
        /// Writes the log to the logger.
        /// </summary>
        /// <param name="level">Log level.</param>
        /// <param name="key">Log message key.</param>
        /// <param name="message">Log message.</param>
        private void Write(LogLevel level, string message, Exception e)
        {
            if (!string.IsNullOrEmpty(message))
            {
                WriteKey(level.Key);
                WriteValue(message);
            }

            builder.Length--; // Removes the last space.
            
            if (e != null)
            {
                builder.Append(string.Concat(Environment.NewLine, e));
            }

            var log = string.Concat(DateTime.Now.ToString(DateTimeFormat),  
                EntrySeparator, ListOpening, level.Name, ListClosing, EntrySeparator, builder.ToString());
            
            sink.Write(log);
        }
    }
}
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
        /// Minimum level to write the log to.
        /// </summary>
        private LogLevel minimumLevel;
        
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
        /// <param name="minimumLevel">Minimum level to write the log to.</param>
        /// <param name="sink">Logger sinker.</param>
        /// <param name="callerFilePath">caller class file path that originated the log.</param>
        /// <param name="callerMemberName">caller class member name that originated the log.</param>
        /// <param name="callerLineNumber">caller line number that originated the log.</param>
        public CompactKeyValueFormatter(LogLevel minimumLevel, ILogSink sink, string callerFilePath, 
            string callerMemberName, int callerLineNumber)
        {
            this.minimumLevel = minimumLevel;
            this.sink = sink;
            builder = new StringBuilder();
            
            var caller = UnknownCallerValue;
            if (!string.IsNullOrEmpty(callerFilePath))
            {
                if (callerMemberName == ConstructorCallerInputName)
                {
                    callerMemberName = ConstructorCallerOutputName;
                }
                
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
        public ILogFormatter Append<T>(T obj) where T : class
        {
            var type = obj?.GetType();
            if (type == null)
            {
                return this;
            }

            if (type == StringType)
            {
                Append(GenericValueKey, obj.ToString());
                return this;
            }
                
            if (!Getters.ContainsKey(type))
            {
                Getters.Add(type, new TypeGettersInfo(type));
            }

            var gettersInfo = Getters[type];
            if (gettersInfo.HasGetters())
            {
                Getters[type].Append(obj, this);
            }
            else
            {
                Append(GenericValueKey, obj.ToString());
            }
        
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
            if (minimumLevel > LogLevel.Debug)
            {
                return;
            }
            
            Write(CompactKeyValueFormatterConstants.LogLevelName.Debug, message, null);
        }

        /// <inheritdoc />
        public void Info(string message = null)
        {
            if (minimumLevel > LogLevel.Info)
            {
                return;
            }
            
            Write(CompactKeyValueFormatterConstants.LogLevelName.Info, message, null);
        }

        /// <inheritdoc />
        public void Warn(string message = null)
        {
            if (minimumLevel > LogLevel.Warn)
            {
                return;
            }
            
            Write(CompactKeyValueFormatterConstants.LogLevelName.Warn, message, null);
        }

        /// <inheritdoc />
        public void Error(string message = null)
        {
            Write(CompactKeyValueFormatterConstants.LogLevelName.Error, message, null);
        }

        /// <inheritdoc />
        public void Error(Exception e)
        {
            Write(CompactKeyValueFormatterConstants.LogLevelName.Error, null, e);
        }

        /// <inheritdoc />
        public void Error(string message, Exception e)
        {
            Write(CompactKeyValueFormatterConstants.LogLevelName.Error, message, e);
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
            if (value == null)
            {
                WriteValue(NullValue, writeEntrySeparator);
            }
            else
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
                else
                {
                    var valueType = value.GetType();
                    if (!Getters.ContainsKey(valueType))
                    {
                        Getters.Add(valueType, new TypeGettersInfo(valueType));
                    }
                    
                    var gettersInfo = Getters[valueType];
                    if (gettersInfo.HasGetters())
                    {
                        builder.Append(ObjectOpening);
                        Getters[valueType].Append(value, this);
                        builder.Length--; // Removes the last space.
                        WriteValue(ObjectClosing, writeEntrySeparator);
                    }
                    else
                    {
                        var stringValue = value.ToString();
                    
                        if (string.IsNullOrEmpty(stringValue))
                        {
                            WriteValue(EmptyValue, writeEntrySeparator);
                        }
                    
                        WriteValue(stringValue, writeEntrySeparator);
                    }
                }
            }
        }

        /// <summary>
        /// Writes the log to the logger.
        /// </summary>
        /// <param name="level">Log level.</param>
        /// <param name="message">Log message.</param>
        /// <param name="e">Log message.</param>
        private void Write(LogLevelName level, string message, Exception e)
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
                EntrySeparator, ListOpening, level.Entry, ListClosing, EntrySeparator, builder.ToString());
            
            sink.Write(log);
        }
    }
}
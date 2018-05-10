using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Anlog.Entries;
using Anlog.Formatters;
using static Anlog.Appenders.Default.DefaultLogAppenderConstants;
using static Anlog.Formatters.DefaultFormattingOptions;

namespace Anlog.Appenders.Default
{
    /// <summary>
    /// Appends logs to a list for formatting/writing.
    /// </summary>
    public sealed class DefaultLogAppender : ILogAppender
    {
        /// <summary>
        /// Available Getters.
        /// </summary>
        internal static Dictionary<Type, TypeGettersInfo> Getters { get; set; } = new Dictionary<Type, TypeGettersInfo>();
        
        /// <summary>
        /// Log entries.
        /// </summary>
        private readonly List<ILogEntry> entries = new List<ILogEntry>();

        /// <summary>
        /// Log writer to write entries to.
        /// </summary>
        private ILogWriter writer;
        
        /// <summary>
        /// Indicates whether appending should only occur with cached objects.
        /// </summary>
        private readonly bool useOnlyCachedObjects;
        
        /// <summary>
        /// Initializes a new instance of <see cref="DefaultLogAppender"/>.
        /// </summary>
        /// <param name="writer">Log writer to write entries to.</param>
        /// <param name="useOnlyCachedObjects">Indicates whether appending should only occur with
        /// cached objects.</param>
        /// <param name="callerFilePath">caller class file path that originated the log.</param>
        /// <param name="callerMemberName">caller class member name that originated the log.</param>
        /// <param name="callerLineNumber">caller line number that originated the log.</param>
        public DefaultLogAppender(ILogWriter writer, bool useOnlyCachedObjects, string callerFilePath, 
            string callerMemberName, int callerLineNumber)
        {
            this.writer = writer;
            this.useOnlyCachedObjects = useOnlyCachedObjects;
            
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
            
            Append(null, caller);
        }
        
        /// <inheritdoc />
        public ILogAppender Append(string key, string value)
        {
            entries.Add(new LogEntry(key, value));
            return this;
        }
        
        /// <inheritdoc />
        public ILogAppender Append(string key, object value)
        {
            entries.Add(GetEntry(key, value));
            
            return this;
        }

        /// <inheritdoc />
        public void Debug(string message = null, params object[] values)
        {
            Write(DefaultFormattingOptions.DefaultLogLevelName.Debug, message, values);
        }

        /// <inheritdoc />
        public void Info(string message = null, params object[] values)
        {
            Write(DefaultFormattingOptions.DefaultLogLevelName.Info, message, values);
        }

        /// <inheritdoc />
        public void Warn(string message = null, params object[] values)
        {
            Write(DefaultFormattingOptions.DefaultLogLevelName.Warn, message, values);
        }

        /// <inheritdoc />
        public void Error(string message = null, params object[] values)
        {
            Write(DefaultFormattingOptions.DefaultLogLevelName.Error, message, values);
        }

        /// <inheritdoc />
        public void Error(Exception e, string message = null, params object[] values)
        {
            Write(DefaultFormattingOptions.DefaultLogLevelName.Error, message, values, e);
        }

        /// <summary>
        /// Gets a log entry.
        /// </summary>
        /// <param name="key">Log key.</param>
        /// <param name="value">Log value.</param>
        /// <returns>Log entry.</returns>
        private ILogEntry GetEntry(string key, object value)
        {
            ILogEntry entry;

            if (value == null)
            {
                entry = new LogEntry(key, NullValue);
            }
            else
            {
                if (value is DateTime)
                {
                    entry = new LogEntry(key, ((DateTime) value).ToString(DefaultDateTimeFormat));
                } 
                else if (value is IConvertible)
                {
                    entry = new LogEntry(key, ((IConvertible) value).ToString(DefaultCulture));
                }
                else if (value is IEnumerable)
                {
                    var list = new LogList(key);
                    var values = (IEnumerable) value;

                    foreach (var item in values)
                    {
                        list.Entries.Add(GetEntry(null, item));
                    }

                    entry = list;
                }
                else
                {
                    var valueType = value.GetType();
                    if (!Getters.ContainsKey(valueType) && !useOnlyCachedObjects)
                    {
                        Getters.Add(valueType, new TypeGettersInfo(valueType));
                    }
                    
                    var gettersInfo = Getters[valueType];
                    if (gettersInfo.HasGetters())
                    {
                        var obj = new LogObject(key);
                        
                        foreach (var getter in gettersInfo.Getters)
                        {
                            obj.Entries.Add(GetEntry(getter.Key, getter.Value(value)));
                        }

                        entry = obj;
                    }
                    else
                    {
                        var stringValue = value.ToString();
                    
                        if (string.IsNullOrEmpty(stringValue))
                        {
                            entry = new LogEntry(key, EmptyValue);
                        }
                    
                        entry = new LogEntry(key, stringValue);
                    }
                }
            }

            return entry;
        }

        /// <summary>
        /// Writes the log entries to the log writer.
        /// </summary>
        /// <param name="level">Log level.</param>
        /// <param name="message">Log message.</param>
        /// <param name="values">Message format values.</param>
        /// <param name="e">Log exception</param>
        private void Write(LogLevelName level, string message, object[] values, Exception e = null)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (values.Length > 0)
                {
                    message = string.Format(DefaultCulture, message, values);
                }
                
                entries.Add(new LogEntry(level.Key, message));
            }
            
            if (e != null)
            {
                entries.Add(new LogException(e));
            }
            
            writer.Write(level, entries);
        }
    }
}
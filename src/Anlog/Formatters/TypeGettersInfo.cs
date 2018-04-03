using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

namespace Anlog.Formatters
{
    /// <summary>
    /// Cached getters info for a type.
    /// </summary>
    public sealed class TypeGettersInfo
    {
        /// <summary>
        /// Object type.
        /// </summary>
        private static readonly Type ObjectType = typeof(object);
        
        /// <summary>
        /// Type for this type info.
        /// </summary>
        private readonly Type relatedType;
        
        /// <summary>
        /// Getters of the type.
        /// </summary>
        private readonly Dictionary<string, Func<object, object>> getters
            = new Dictionary<string, Func<object, object>>();

        /// <summary>
        /// Initializes a new instance of <see cref="TypeGettersInfo"/>.
        /// </summary>
        /// <param name="type">Type to get setters from.</param>
        public TypeGettersInfo(Type type)
        {
            relatedType = type;
            
            FillFieldsGetters();
            FillPropertiesGetters();
        }

        /// <summary>
        /// Fill fields getters.
        /// </summary>
        private void FillFieldsGetters()
        {
            foreach (var field in relatedType.GetFields(BindingFlags.Instance | BindingFlags.Public))
            {
                if (field.GetCustomAttribute<LogIgnoreAttribute>() != null)
                {
                    continue;
                }
                
                var key = field.Name;
                var dataMemberAttibute = field.GetCustomAttribute<DataMemberAttribute>();
                if (dataMemberAttibute != null)
                {
                    key = dataMemberAttibute.Name;
                }
                
                var objectParameter = Expression.Parameter(ObjectType);
                var getExpression = Expression.Lambda<Func<object, object>>(
                    Expression.Convert(
                        Expression.Field(Expression.Convert(objectParameter, relatedType), field), ObjectType),
                    objectParameter);
                
                getters.Add(key, getExpression.Compile());
            }
        }

        /// <summary>
        /// Fill properties getters.
        /// </summary>
        private void FillPropertiesGetters()
        {
            foreach (var property in relatedType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (property.GetCustomAttribute<LogIgnoreAttribute>() != null)
                {
                    continue;
                }
                
                var key = property.Name;
                var dataMemberAttibute = property.GetCustomAttribute<DataMemberAttribute>();
                if (dataMemberAttibute != null)
                {
                    key = dataMemberAttibute.Name;
                }
                
                var objectParameter = Expression.Parameter(ObjectType);
                var getExpression = Expression.Lambda<Func<object, object>>(
                    Expression.Convert(
                        Expression.Property(Expression.Convert(objectParameter, relatedType), property), ObjectType),
                    objectParameter);
                
                getters.Add(key, getExpression.Compile());
            }
        }

        /// <summary>
        /// Apeends getters from an instance to a formatter.
        /// </summary>
        /// <param name="instance">Object instance to be formatted.</param>
        /// <param name="formatter">Log formatter to use for formatting.</param>
        public void Append(object instance, ILogFormatter formatter)
        {
            foreach (var getter in getters)
            {
                formatter.Append(getter.Key, getter.Value(instance));
            }
        }
    }
}
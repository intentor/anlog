using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

namespace Anlog.Formatters
{
    /// <summary>
    /// Cached Getters info for a type.
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
        public readonly Dictionary<string, Func<object, object>> Getters
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
        /// Fill fields Getters.
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
                
                Getters.Add(key, getExpression.Compile());
            }
        }

        /// <summary>
        /// Fill properties Getters.
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

                // Indexed properties are ignored.
                if (property.GetIndexParameters().Length > 0)
                {
                    continue;
                }
                
                var objectParameter = Expression.Parameter(ObjectType);
                var getExpression = Expression.Lambda<Func<object, object>>(
                    Expression.Convert(
                        Expression.Property(Expression.Convert(objectParameter, relatedType), property), ObjectType),
                    objectParameter);
                
                Getters.Add(key, getExpression.Compile());
            }
        }

        /// <summary>
        /// Indicates whether this type has Getters.
        /// </summary>
        /// <returns>True if there's Getters available, otherwise false.</returns>
        public bool HasGetters()
        {
            return Getters.Count > 0;
        }
    }
}
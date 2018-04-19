using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Anlog.Formatters
{
    /// <summary>
    /// Type Getters utils.
    /// </summary>
    public static class TypeGettersUtils
    {
        /// <summary>
        /// Finds all types that have a <see cref="DataContractAttribute"/> and adds their Getters.
        /// </summary>
        /// <returns>Available Getters.</returns>
        public static Dictionary<Type, TypeGettersInfo> GetDataContractGetters()
        {
            var getters = new Dictionary<Type, TypeGettersInfo>();

            var types = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic && !a.FullName.StartsWith("System") && !a.FullName.StartsWith("Microsoft"))
                .SelectMany(a => a.GetTypes())
                .Where(t => !t.IsInterface && !t.IsAbstract && t.IsPublic
                    && t.GetCustomAttribute<DataContractAttribute>() != null);

            foreach (var type in types)
            {
                getters.Add(type, new TypeGettersInfo(type));
            }

            return getters;
        }
    }
}
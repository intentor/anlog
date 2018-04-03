using System;

namespace Anlog
{
    /// <summary>
    /// Ignores a field or property in a class when writing logs.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class LogIgnoreAttribute : Attribute
    {
        
    }
}
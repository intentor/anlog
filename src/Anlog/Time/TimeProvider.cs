using System;

namespace Anlog.Time
{
    /// <summary>
    /// Provides date/time information.
    /// </summary>
    public abstract class TimeProvider
    {
        /// <summary>
        /// Internal current date/time provider.
        /// </summary>
        private static TimeProvider current = new DefaultTimeProvider();

        /// <summary>
        /// Current date/time provider.
        /// </summary>
        public static TimeProvider Current
        {
            get => current;
            set => current = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets the current date/time.
        /// </summary>
        public abstract DateTime CurrentDateTime { get; }

        /// <summary>
        /// Gets the current date/time.
        /// </summary>
        public static DateTime Now => current.CurrentDateTime;

        /// <summary>
        /// Resets the time provider to the default.
        /// </summary>
        public static void ResetToDefault()
        {    
            current = new DefaultTimeProvider();
        }            
    }
}
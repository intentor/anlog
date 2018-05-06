using System;

namespace Anlog.Time
{
    /// <summary>
    /// Default time provider, which uses an internal <see cref="DateTime"/>.
    /// </summary>
    public sealed class DefaultTimeProvider : TimeProvider
    {
        /// <inheritdoc />
        public override DateTime CurrentDateTime => DateTime.Now;
    }
}
using System.Text;

namespace Anlog.Renderers
{
    /// <summary>
    /// Writes log data to a string buffer.
    /// </summary>
    public sealed class DefaultDataRenderer : IDataRenderer
    {
        /// <summary>
        /// Internal string builder.
        /// </summary>
        private StringBuilder builder = new StringBuilder();

        /// <inheritdoc />
        public IDataRenderer RenderDate(string formattedDate)
        {
            builder.Append(formattedDate);
            return this;
        }

        /// <inheritdoc />
        public IDataRenderer RenderLevel(LogLevel level, string formattedLevel)
        {
            builder.Append(formattedLevel);
            return this;
        }

        /// <inheritdoc />
        public IDataRenderer RenderKey(string key)
        {
            builder.Append(key);
            return this;
        }

        /// <inheritdoc />
        public IDataRenderer RenderValue(string value)
        {
            builder.Append(value);
            return this;
        }

        /// <inheritdoc />
        public IDataRenderer RenderInvariant(string invariant)
        {
            builder.Append(invariant);
            return this;
        }

        /// <inheritdoc />
        public IDataRenderer RemoveLastCharacter()
        {
            builder.Length--;
            return this;
        }

        /// <inheritdoc />
        public string Render()
        {
            return builder.ToString();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Render();
        }
    }
}